using System;
using System.Collections;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.server;
using sc2i.multitiers.client;
using sc2i.data.synchronisation;

#if !PDA

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CSyncrhoniseurBdd.
	/// </summary>
	public class CSynchroniseurBddServeur : C2iObjetServeur, ISynchroniseurBdd
	{
		public const string c_cleIdSynchro = "SYNCHRO_ID";

		private string m_strIdConnexion = "";

		/// //////////////////////////////////////////////////////////////////
		public CSynchroniseurBddServeur ( int nIdSession )
			:base ( nIdSession )
		{
			m_strIdConnexion = CSc2iDataServer.IdConnexionParDefaut;
		}

		/// //////////////////////////////////////////////////////////////////
		public CSynchroniseurBddServeur( int nIdSession, string strIdConnexion )
			:base ( nIdSession )
		{
			m_strIdConnexion = strIdConnexion;
		}

		/// //////////////////////////////////////////////////////////////////
		public void LockSyncSessionLocalTo ( int nIdLockSyncSession )
		{
			IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( IdSession, "" );
			connexion.LockSyncSessionLocalTo ( nIdLockSyncSession );
		}

		///--------------------------------------
		public void UnlockSyncSessionLocale()
		{
			IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( IdSession, "" );
			connexion.UnlockSyncSessionLocal (  );
		}
		/// //////////////////////////////////////////////////////////////////
		//le result du dataset contient le dataset à appliquer à la base secondaire
		public CResultAErreur PutSecondaryIntoMain ( CContexteDonneesSynchroSecondaire dsSecondaire )
		{
			CResultAErreur result = CResultAErreur.True;

			//Répercute les modifs de la base secondaire sur la base primaire
			using (CContexteDonneesSynchroMain donneesMain = new CContexteDonneesSynchroMain(IdSession, true))
			{
				IDatabaseConnexionSynchronisable connexionMain;
				connexionMain = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);

				int nIdSyncMain = connexionMain.IdSyncSession;

				
				
				result = donneesMain.ChargeDonneesAMettreAJour ( dsSecondaire );

				if ( result )
					result = donneesMain.AjouteNouveaux ( dsSecondaire );
				if ( result )
					result = donneesMain.Update ( dsSecondaire );
				if ( result )
					result = donneesMain.Delete ( dsSecondaire );
					
				

				try
				{
					connexionMain.BeginTrans();
				}
				catch ( Exception e )
				{
					result.EmpileErreur(new CErreurException (e));
					result.EmpileErreur(I.T("Error while data synchronization|208"));
					return result;
				}

				if ( result )
					result = donneesMain.WriteChanges( dsSecondaire );

				//Augmente d'un le numéro de version de la base principale
						
				if ( result )
				{
					while ( connexionMain.IdSyncSession < nIdSyncMain+2 )
						connexionMain.IncrementeSyncSession();

					result = connexionMain.CommitTrans();

					result.Data = dsSecondaire;
				}
				else
					connexionMain.RollbackTrans();
			}
			return result;
		}
					

		/// //////////////////////////////////////////////////////////////////
		public CResultAErreur PutSecondaryIntoMain ( string strConnexionSecondaire )
		{
			CResultAErreur result = CResultAErreur.True;
			/*CSc2iDataServer connexionManager = CSc2iDataServer.GetInstance();

			string strConnexionMain = connexionManager.GetDatabaseConnexion (IdSession, m_strIdConnexion).ConnexionString;

			IDatabaseConnexionSynchronisable connexionSecondaire, connexionMain;

			
			connexionMain = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);
			

			//Crée la connexion à la base secondaire
			connexionSecondaire = new IDatabaseConnexionSynchronisable(IdSession);
			connexionSecondaire.ConnexionString =  strConnexionSecondaire;

			

			try
			{
				//Passe sur la base secondaire
				connexionManager.SetDatabaseConnexion(IdSession, m_strIdConnexion,connexionSecondaire );

				//Récupère l'id de la session hors connexion de la base secondaire
				int nIdSyncSecondaire = connexionSecondaire.IdSyncSession;
				
				//Jamais synchronisée avec la base main
				if ( nIdSyncSecondaire == -1 )
					return result;

				int nIdSyncMain = connexionMain.IdSyncSession;

				bool bHasChange = false;

				//Remplit le contexte secondaire avec les informations à mettre à jour
				using (CContexteDonneesSynchroSecondaire ctxSecondaire = new CContexteDonneesSynchroSecondaire(IdSession, true))
				{
					//Change le numéro de version de la base secondaire, ainsi,
						 // s'il y a des modifs après ou pendant la lecture, elles auront bien lieu dans une autre
						 // session de synchronisation
					//
					connexionSecondaire.IncrementeSyncSession();
					ctxSecondaire.FillWithModifsFromVersion(connexionSecondaire.LastSyncIdPutInMain+1, nIdSyncSecondaire, ref bHasChange, null, false);

					if ( bHasChange )
					{
						//Passe sur la base primaire
						connexionManager.SetDatabaseConnexion(IdSession, m_strIdConnexion, connexionMain);

						connexionMain.BeginTrans();

						result = PutSecondaryIntoMain ( ctxSecondaire );
						
						if ( result )
						{
							connexionSecondaire.LockSyncSessionLocalTo ( connexionSecondaire.IdSyncSession );
								
				
							//répercute les modifications d'id sur la base secondaire
							//La base secondaire ne doit pas enregistrer ces modifications comme des modifs à synchroniser,
							connexionManager.SetDatabaseConnexion ( IdSession, m_strIdConnexion, connexionSecondaire );
							connexionSecondaire.EnableLogSynchronisation = false;
							result = ctxSecondaire.SaveAll(true);
							if ( result )
							{
								while ( connexionSecondaire.IdSyncSession < nIdSyncMain+1 )
									connexionSecondaire.IncrementeSyncSession();
								connexionSecondaire.LastSyncIdPutInMain = nIdSyncSecondaire;
								connexionSecondaire.CommitTrans();
								connexionMain.CommitTrans();
							}
						}
						if ( !result )
						{
							connexionMain.RollbackTrans();
							connexionSecondaire.RollbackTrans();
						}
					}
					else
					{
						//Pas de changement, la dernière version mise dans la base est la version actuelle
						connexionSecondaire.LastSyncIdPutInMain = nIdSyncSecondaire;
					}
				}
			}
			catch(Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error while data synchronization|208"));
				if ( connexionMain.IsInTrans() )
					connexionMain.RollbackTrans();
				if ( connexionSecondaire.IsInTrans() )
					connexionSecondaire.RollbackTrans();
			}
			finally
			{
				CSc2iDataServer.GetInstance().SetDatabaseConnexion(IdSession, m_strIdConnexion, connexionMain);
				connexionSecondaire.Dispose();
			}*/
			return result;
		}


		private CFiltresSynchronisation GetFiltres ( string strCodeGroupeSynchronisation )
		{
			CSessionClient session = CSessionClient.GetSessionForIdSession ( IdSession );
			if ( session == null )
				return null;
			IFournisseurServicePourSessionClient fournisseur = session.GetFournisseur ( CSc2iDataConst.c_ServiceFiltresSynchronisation );
			if ( fournisseur == null )
				return null;
			IServiceGetFiltresSynchronisation service = (IServiceGetFiltresSynchronisation)fournisseur.GetService ( IdSession );
			if ( service != null )
			{
				CResultAErreur result = service.GetFiltresSynchronisation ( strCodeGroupeSynchronisation );
				if ( result )
					return ( CFiltresSynchronisation )result.Data;
				throw new CExceptionErreur ( result.Erreur );
			}
			return null;
		}

        //-------------------------------------------------------------------------------------
        public CResultAErreurType<System.Collections.Generic.List<CModifSynchronisation>> GetListeModifsInMain(int nIdSyncSessionFrom, int nIdSyncSessionTo, string strCodeGroupeSynchronisation)
        {
            throw new NotImplementedException();
        }

        //-------------------------------------------------------------------------------------
        public CResultAErreurType<DataTable> GetModifInMain(CModifSynchronisation modif)
        {
            throw new NotImplementedException();
        }

		
		/// //////////////////////////////////////////////////////////////////
		/// le data du result contient le dataset à appliquer à la base distante
		public CResultAErreur GetModifsInMain ( 
			int nIdSyncSessionFrom, 
			int nIdSyncSessionSecondaireCourant, 
			string strCodeGroupeSynchronisation )
		{
			CResultAErreur result = CResultAErreur.True;
			CFiltresSynchronisation filtres= GetFiltres ( strCodeGroupeSynchronisation );
			IDatabaseConnexionSynchronisable connexionMain = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);
			int nIdSyncMain = connexionMain.IdSyncSession;
			while ( connexionMain.IdSyncSession <= nIdSyncSessionSecondaireCourant )
				connexionMain.IncrementeSyncSession();

			CContexteDonneesSynchro ctxMain = new CContexteDonneesSynchro( IdSession, true );
			bool bHasChange = false;
			ctxMain.FillWithModifsFromVersion(nIdSyncSessionFrom==-1?-1:nIdSyncSessionFrom+1, nIdSyncMain, ref bHasChange, filtres, false);
			if ( bHasChange )
			{
				DataSet dsRetour = new DataSet();
				foreach ( DataTable table in ctxMain.Tables )
				{
					if ( table.Rows.Count != 0 )
					{
						DataTable tableCopie = CUtilDataSet.AddTableCopie	( table, dsRetour );
						ArrayList lst = new ArrayList();
						foreach ( DataColumn col in tableCopie.Columns )
							if ( col.DataType == typeof(CDonneeBinaireInRow) )
								lst.Add ( col );
						foreach ( DataColumn col in lst )
							tableCopie.Columns.Remove ( col );

						foreach ( DataRow row in table.Rows )
						{
							tableCopie.ImportRow ( row );
						}
					}
				}
				dsRetour.ExtendedProperties[c_cleIdSynchro] = nIdSyncMain;
				result.Data = dsRetour;
			}
			return result;
		}

		/// //////////////////////////////////////////////////////////////////
		public CResultAErreur PutMainIntoSecondary ( CContexteDonneesSynchro ctxMain )
		{
			CResultAErreur result = CResultAErreur.True;
			IDatabaseConnexionSynchronisable connexionSecondaire = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);

			//Toutes les modifs apportées sur la base secondaire doivent porter l'id de synchro
			//Initial de la base secondaire, sinon les modifs font du 
			//ping pong entre secondaire et main,
			//Ces modifs ne doivent surtout pas apparaitre
			//lors d'une synchro de sec->main !
			connexionSecondaire.LockSyncSessionLocalTo ( connexionSecondaire.LastSyncIdPutInBasePrincipale );
							
			//Répercute les modifs de la base secondaire sur la base primaire
			using (CContexteDonneesSynchroSecondaire donneeSec = new CContexteDonneesSynchroSecondaire(IdSession, true))
			{
				result = donneeSec.ChargeDonneesAMettreAJour ( ctxMain );

				donneeSec.EnforceConstraints = false;
				if ( result )
					result = donneeSec.AjouteNouveaux ( ctxMain );
				if ( result )
					result = donneeSec.Update ( ctxMain );
				if ( result )
					result = donneeSec.Delete ( ctxMain );
				donneeSec.EnforceConstraints = true;
				try
				{
					connexionSecondaire.BeginTrans();
				}
				catch ( Exception e )
				{
					result.EmpileErreur(new CErreurException (e));
					result.EmpileErreur(I.T("Error while data synchronization|208"));
					return result;
				}

				if ( result )
					result = donneeSec.WriteChanges( ctxMain );
								

				if ( result )
				{
					if ( connexionSecondaire.LastSyncIdPutInBasePrincipale == -1 )
						connexionSecondaire.LastSyncIdPutInBasePrincipale = connexionSecondaire.IdSyncSession;

					connexionSecondaire.UnlockSyncSessionLocal();
					while ( connexionSecondaire.IdSyncSession < ctxMain.IdSynchro )
						connexionSecondaire.IncrementeSyncSession();
								
					connexionSecondaire.LastSyncIdVueDeBasePrincipale = ctxMain.IdSynchro==-1?0:ctxMain.IdSynchro;
								
					connexionSecondaire.CommitTrans();
				}

				if ( !result )
				{
					connexionSecondaire.RollbackTrans();
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////////////////////
		public CResultAErreur PutMainIntoSecondary ( string strConnexionSecondaire, CFiltresSynchronisation filtres, string strCodeGroupeSynchronisation )
		{

			CResultAErreur result = CResultAErreur.True;
            /*
			CSc2iDataServer connexionManager = CSc2iDataServer.GetInstance();

			string strConnexionMain = connexionManager.GetDatabaseConnexion(IdSession, m_strIdConnexion).ConnexionString;

			IDatabaseConnexionSynchronisable connexionSecondaire, connexionMain;

			
			connexionMain = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);
			

			//Crée la connexion à la base secondaire
			connexionSecondaire = new IDatabaseConnexionSynchronisable(IdSession);
			connexionSecondaire.ConnexionString =  strConnexionSecondaire;

			
			try
			{
				int nIdLastSyncSecondaire = connexionSecondaire.LastVersionVueDeBasePrincipale;

				int nIdSyncMain = connexionMain.IdSyncSession;

				if ( nIdLastSyncSecondaire > connexionSecondaire.LastSyncIdPutInMain )
				{
					result.EmpileErreur(I.T("The synchronization Secondary -> primary must be done before this operation|209"));
					return result;
				}

				//Remplit le contexte primaire avec les informations à mettre à jour
				using (CContexteDonneesSynchroMain ctxMain = new CContexteDonneesSynchroMain(IdSession, true))
				{
					bool bHasChange = false;
					//Change le numéro de version de la base primaire, ainsi,
						 // s'il y a des modifs après ou pendant la lecture, elles auront bien lieu dans une autre
						 // session de synchronisation
						//
					while ( connexionMain.IdSyncSession <= connexionSecondaire.IdSyncSession )
						connexionMain.IncrementeSyncSession();
					ctxMain.FillWithModifsFromVersion(nIdLastSyncSecondaire==-1?-1:nIdLastSyncSecondaire+1, nIdSyncMain, ref bHasChange, filtres, false);
					if ( bHasChange )
					{
						//Passe sur la base secondaire
						connexionManager.SetDatabaseConnexion(IdSession, m_strIdConnexion, connexionSecondaire);

						//Toutes les modifs apportées sur la base secondaire doivent porter l'id de synchro
						//Initial de la base secondaire, sinon les modifs font du 
						//ping pong entre secondaire et main,
						//Ces modifs ne doivent surtout pas apparaitre
						//lors d'une synchro de sec->main !
						connexionSecondaire.LockSyncSessionLocalTo ( nIdLastSyncSecondaire );
							
						//Répercute les modifs de la base secondaire sur la base primaire
						using (CContexteDonneesSynchroSecondaire donneeSec = new CContexteDonneesSynchroSecondaire(IdSession, true))
						{
							result = donneeSec.ChargeDonneesAMettreAJour ( ctxMain );

							donneeSec.EnforceConstraints = false;
							if ( result )
								result = donneeSec.AjouteNouveaux ( ctxMain );
							if ( result )
								result = donneeSec.Update ( ctxMain );
							if ( result )
								result = donneeSec.Delete ( ctxMain );
							donneeSec.EnforceConstraints = true;
							try
							{
								connexionSecondaire.BeginTrans();
							}
							catch ( Exception e )
							{
								result.EmpileErreur(new CErreurException (e));
								result.EmpileErreur(I.T("Error while data synchronization|208"));
								return result;
							}

							if ( result )
								result = donneeSec.WriteChanges( ctxMain );
								

							if ( result )
							{
								if ( connexionSecondaire.LastSyncIdPutInMain == -1 )
									connexionSecondaire.LastSyncIdPutInMain = connexionSecondaire.IdSyncSession;

								connexionSecondaire.UnlockSyncSessionLocal();
								while ( connexionSecondaire.IdSyncSession < connexionMain.IdSyncSession )
									connexionSecondaire.IncrementeSyncSession();
								
								connexionSecondaire.LastVersionVueDeBasePrincipale = nIdSyncMain==-1?0:nIdSyncMain;
								
								while ( connexionMain.IdSyncSession <= connexionSecondaire.IdSyncSession )
									connexionMain.IncrementeSyncSession();
								connexionSecondaire.CommitTrans();
							}

							if ( !result )
							{
								connexionSecondaire.RollbackTrans();
							}
						}
					}
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error while data synchronization|208"));
			}
			finally
			{
				CSc2iDataServer.GetInstance().SetDatabaseConnexion(IdSession, m_strIdConnexion,connexionMain);
				connexionSecondaire.Dispose();
			}*/
			return result;
		}

		/// //////////////////////////////////////////////////////////////////
		public int GetIdSynchroMain()
		{
			IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);
			return connexion.IdSyncSession;
		}

		/// //////////////////////////////////////////////////////////////////
		public int IncrementeIdSynchroMain()
		{
			IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, m_strIdConnexion);
			connexion.IncrementeSyncSession();
			return connexion.IdSyncSession;
		}

		/// /////////////////////////////////////////////////////////
		public DataSet GetAddAndUpdateInTableMain ( 
			string strNomTable, 
			int nSessionSyncDebut, 
			int nSessionSyncFin, 
			string strCodeGroupeSynchronisation )
		{
			Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
			DataSet dsDest = null;
			if ( tp == null )
				return null;

			//Récupère les filtres
			CFiltresSynchronisation filtres = GetFiltres ( strCodeGroupeSynchronisation );

			using (CContexteDonnee contexte = new CContexteDonnee ( IdSession, true, false ) )
			{
				CFiltreData filtreSynchro = null;
				if ( filtres != null )
					filtreSynchro = filtres.GetFiltreForTable ( m_nIdSession, strNomTable );

				if ( filtreSynchro == null )
					filtreSynchro = new CFiltreData();
				int nNumParametreIdSync = filtreSynchro.Parametres.Count+1;
				string strFiltre = filtreSynchro.Filtre;
				filtreSynchro.Filtre = "("+CSc2iDataConst.c_champIdSynchro+">=@"+nNumParametreIdSync.ToString()+" and "+
					CSc2iDataConst.c_champIdSynchro+"<=@"+(nNumParametreIdSync+1).ToString();
				if ( nSessionSyncDebut == -1 )
					//C'est la première synchro, intègre les éléments modifiés avant prise en charge
					//des synchros
				{
					if ( filtreSynchro is CFiltreDataAvance )
						filtreSynchro.Filtre += " or hasno("+CSc2iDataConst.c_champIdSynchro+")";
					else
						filtreSynchro.Filtre += " or "+CSc2iDataConst.c_champIdSynchro+" is null";
				}
				filtreSynchro.Filtre+=")";
				if ( strFiltre != "" )
					filtreSynchro.Filtre += " and ("+strFiltre+")";
				filtreSynchro.Parametres.Add ( nSessionSyncDebut );
				filtreSynchro.Parametres.Add ( nSessionSyncFin );
				CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte, tp, false );
				liste.Filtre = filtreSynchro;
				liste.AssureLectureFaite();
				
				//Charge les données de synchronisation
				CFiltreData filtreDataSync = new CFiltreData ( CEntreeLogSynchronisation.c_champTable+"=@1 and "+
					CSc2iDataConst.c_champIdSynchro+">=@2 and "	+
					CSc2iDataConst.c_champIdSynchro+"<=@3",
					strNomTable, nSessionSyncDebut, nSessionSyncFin );
				liste = new CListeObjetsDonnees ( contexte, typeof(CEntreeLogSynchronisation), false );
				liste.Filtre = filtreDataSync;
				liste.AssureLectureFaite();

				//Transforme le contexte en dataset (pour sérialisation web !! )
				dsDest = new DataSet();
				foreach ( DataTable table in contexte.Tables )
				{
					DataTable tableDest = CUtilDataSet.AddTableCopie ( table, dsDest );
					foreach ( DataRow row in table.Rows )
						tableDest.ImportRow ( row );
					//Supprime les colonnes blob
					for ( int nCol = tableDest.Columns.Count-1; nCol >= 0; nCol-- )
					{
						if ( tableDest.Columns[nCol].DataType == typeof(CDonneeBinaireInRow))
							tableDest.Columns.RemoveAt ( nCol );
					}
				}
				
				
				
				
			}
			return dsDest;
		}
		

		/*/// //////////////////////////////////////////////////////////////////
		public CResultAErreur SynchroniseSecondaryWithMain ( CContexteDonneesSynchroSecondaire donneesSecondaires )
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteDonneesSynchroMain donneesMain = new CContexteDonneesSynchroMain(true);
			result = donneesMain.ChargeDonneesAMettreAJour ( donneesSecondaires );

			if ( result )
				result = donneesMain.AjouteNouveaux ( donneesSecondaires );
			if ( result )
				result = donneesMain.Update ( donneesSecondaires );
			if ( result )
				result = donneesMain.Delete ( donneesSecondaires );
			if ( result )
				result = donneesMain.WriteChanges( donneesSecondaires );
			return result;
		}

		/// //////////////////////////////////////////////////////////////////
		public CResultAErreur SynchroniseMainWithSecondary ( CContexteDonneesSynchroMain donneesMain )
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteDonneesSynchroSecondaire donneeSec = new CContexteDonneesSynchroSecondaire(true);
			result = donneeSec.ChargeDonneesAMettreAJour ( donneesMain );

			if ( result )
				result = donneeSec.AjouteNouveaux ( donneesMain );
			if ( result )
				result = donneeSec.Update ( donneesMain );
			if ( result )
				result = donneeSec.Delete ( donneesMain );
			if ( result )
				result = donneeSec.WriteChanges( donneesMain );
			return result;
		}*/


        

        
    }
}
#endif