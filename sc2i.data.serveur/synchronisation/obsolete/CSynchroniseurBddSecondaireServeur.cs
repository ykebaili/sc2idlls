using System;
using System.Collections;
using System.Data;
using System.Threading;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using sc2i.data.synchronisation;


namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CSynchroniseurBddMainToSecondaireServeur.
	/// </summary>
	public class CSynchroniseurBddSecondaireServeur : MarshalByRefObject, ISynchroniseurBddSecondaire
	{
		private const int c_nNbRecordMaxParEnvoi = 5000;
		private const int c_nNbTablesMaxParEnvoi = 10;

		//IdProcess->CProcessSynchronisationMainToSecondaire
		private static Hashtable m_tableProcessSynchronsationMainToSecondaire = new Hashtable();

		//IdProcess->CProcessSynchronisationSecondaireToMain
		private static Hashtable m_tableProcessSynchronisationSecondaireToMain = new Hashtable();

		//Se charge de supprimer automatiquement les process à supprimer
		private static Timer m_timerNettoyage = null;

		/// //////////////////////////////////////////////////////////////////
		public void RenouvelleBailParAppel()
		{
		}

        ///////////////////////////////////////////////
        private string m_strIdUnique = "";
        public string UniqueId
        {
            get
            {
                if (m_strIdUnique.Length == 0)
                    m_strIdUnique = CUniqueIdentifier.GetNew();
                return m_strIdUnique;
            }
        }

		/// //////////////////////////////////////////////////////////////////
		public CSynchroniseurBddSecondaireServeur()
			:base (  )
		{
			if ( m_timerNettoyage == null )
			{
				//Toutes les minutes
				m_timerNettoyage = new Timer ( new TimerCallback ( OnNettoyage ), null, 60000, 60000 );
			}
		}

		/// //////////////////////////////////////////////////////////////////
		public static void OnNettoyage ( object state )
		{
			ArrayList lstToDelete = new ArrayList();
			foreach ( CProcessSynchronisationMainToSecondaire processMainToSec in m_tableProcessSynchronsationMainToSecondaire.Values )
				if ( processMainToSec.DateCancelProgramme < DateTime.Now )
					lstToDelete.Add ( processMainToSec );
			foreach ( CProcessSynchronisationMainToSecondaire processMainToSec in lstToDelete )
				new CSynchroniseurBddSecondaireServeur (  ).EndProcessSynchronisationMainToSecondaire ( processMainToSec.IdProcess );

			lstToDelete = new ArrayList();
			foreach ( CProcessSynchronisationSecondaireToMain processSecToMain in m_tableProcessSynchronisationSecondaireToMain.Values )
				if ( processSecToMain.DateCancelProgramme < DateTime.Now )
					lstToDelete.Add ( processSecToMain );
			foreach ( CProcessSynchronisationSecondaireToMain processSecToMain in lstToDelete )
				new CSynchroniseurBddSecondaireServeur (  ).CancelProcessSynchronisationSecondaireToMain ( processSecToMain.IdProcess );
		}

				

		#region Synchronisation Main->secondaire

		#region CProcessSynchronisationMainToSecondaire
		protected class CProcessSynchronisationMainToSecondaire : IDisposable
		{
			private static int m_nIdNextProcess = 0;

			private int m_nIdProcess;
			private int m_nIdSessionClient;
			private int m_nIdSyncSessionStartInMain = -1;
			private int m_nIdSyncSessionEndInMain = -1;
			private string m_strCodeGroupeSynchronisation = "";
			private string[] m_listeNomTablesAddOrUpdate = null;
			private string[] m_listeNomTablesDelete = null;

			
			//Prochaine table à envoyer
			private int m_nNextTableToAddOrUpdate = 0;
			private int m_nNextTableDelete = 0;
			
			//Prochain enregistrement de la table à envoyer
			//Si envoi partiel d'une table
			private string m_strTableEnCours = "";
			private int m_nNextRecord = 0;

			private CFiltresSynchronisation m_filtres = null;

			//Après ouverture du process, on a 2 minutes pour l'utiliser
			private DateTime m_dtCancelProgramme = DateTime.Now.AddMinutes ( 2 );

			/// //////////////////////////////////////////////////////////////////
			public CProcessSynchronisationMainToSecondaire( 
				int nIdSessionClient, 
				int nIdStartInMain, 
				string strCodeGroupeSynchro)
			{
				lock ( this )
				{
					m_nIdProcess = m_nIdNextProcess++;
				}
				m_nIdSessionClient = nIdSessionClient;
				m_nIdSyncSessionStartInMain = nIdStartInMain;
				IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( nIdSessionClient, "" );
				m_nIdSyncSessionEndInMain = connexion.IdSyncSession;
				connexion.IncrementeSyncSession();
				m_strCodeGroupeSynchronisation = strCodeGroupeSynchro;
				m_listeNomTablesAddOrUpdate = CContexteDonnee.GetFastListNomTablesOrderInsert();
				m_listeNomTablesDelete = CContexteDonnee.GetFastListNomTablesOrderDelete();
				
				//Prépare les filtres
				CSessionClient session = CSessionClient.GetSessionForIdSession ( nIdSessionClient );
				if ( session == null )
					throw new Exception(I.T("The session @1 has been closed|201", nIdSessionClient.ToString()));
				IFournisseurServicePourSessionClient fournisseur = session.GetFournisseur ( CSc2iDataConst.c_ServiceFiltresSynchronisation );
				if ( fournisseur == null )
					throw new Exception(I.T("No service has been defined for synchronization|202"));
				IServiceGetFiltresSynchronisation service = (IServiceGetFiltresSynchronisation)fournisseur.GetService ( nIdSessionClient );
				if ( service != null )
				{
					CResultAErreur result = service.GetFiltresSynchronisation ( m_strCodeGroupeSynchronisation );
					if ( result )
						m_filtres = ( CFiltresSynchronisation )result.Data;
					else
						throw new CExceptionErreur ( result.Erreur );
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public void Dispose()
			{
				EndProcess();
			}

			/// //////////////////////////////////////////////////////////////////
			public void EndProcess()
			{
				try
				{
					CSessionClient session = CSessionClient.GetSessionForIdSession ( m_nIdSessionClient );
					if ( session != null )
						session.CloseSession();
				}
				catch
				{
				}
			}

			/// ////////////////////////////////////////////////////////
			public int IdSessionClient
			{
				get
				{
					return m_nIdSessionClient;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public int IdProcess
			{
				get
				{
					return m_nIdProcess;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public int IdSyncSessionStartInMain
			{
				get
				{
					return m_nIdSyncSessionStartInMain;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public int IdSyncSessionEndInMain
			{
				get
				{
					return m_nIdSyncSessionEndInMain;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public string CodeGroupeSynchronisation
			{
				get
				{
					return m_strCodeGroupeSynchronisation;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public CFiltresSynchronisation Filtres
			{
				get
				{
					return m_filtres;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public string GetTableEnCours ( ref int nNumRecordToSend )
			{
				nNumRecordToSend = m_nNextRecord;
				return m_strTableEnCours;
			}

			/// //////////////////////////////////////////////////////////////////
			public void ResetTableEnCours()
			{
				m_strTableEnCours = "";
				m_nNextRecord = 0;
			}

			/// //////////////////////////////////////////////////////////////////
			public void SetTableEnCours ( string strTable, int nNumNextRecordToSend )
			{
				m_strTableEnCours = strTable;
				m_nNextRecord = nNumNextRecordToSend;
			}

			/// //////////////////////////////////////////////////////////////////
			public int GetNbTablesToAddOrUpdate()
			{
				return m_listeNomTablesAddOrUpdate.Length;
			}

			/// //////////////////////////////////////////////////////////////////
			public string PeekNomTableEnCoursAddOrUpdate()
			{
				if ( m_nNextTableToAddOrUpdate-1 > 0 && 
					m_nNextTableToAddOrUpdate-1 <= m_listeNomTablesAddOrUpdate.Length )
					return m_listeNomTablesAddOrUpdate[m_nNextTableToAddOrUpdate-1];
				return "";
			}

			/// //////////////////////////////////////////////////////////////////
			public int GetPourcentageAddOrUpdateEnCours()
			{
				if ( m_listeNomTablesAddOrUpdate.Length == 0 )
					return 100;
				return (int)(m_nNextTableToAddOrUpdate-1)*100/m_listeNomTablesAddOrUpdate.Length;
			}

			/// //////////////////////////////////////////////////////////////////
			public string PopNextNomTableToAddOrUpdate()
			{
				ResetTableEnCours();
				if ( m_nNextTableToAddOrUpdate < m_listeNomTablesAddOrUpdate.Length )
					return m_listeNomTablesAddOrUpdate[m_nNextTableToAddOrUpdate++];
				return null;
			}

			/// //////////////////////////////////////////////////////////////////
			public int GetNbTablesToDelete()
			{
				return m_listeNomTablesDelete.Length;
			}

			/// //////////////////////////////////////////////////////////////////
			public string PopNextNomTableDelete()
			{
				if ( m_nNextTableDelete < m_listeNomTablesDelete.Length )
					return m_listeNomTablesDelete[m_nNextTableDelete++];
				return null;
			}

			/// //////////////////////////////////////////////////////////////////
			public bool HasDataToAddOrUpdate()
			{
				return m_nNextTableToAddOrUpdate< m_listeNomTablesAddOrUpdate.Length;
			}

			/// //////////////////////////////////////////////////////////////////
			public bool HasDataToDelete()
			{
				return m_nNextTableDelete < m_listeNomTablesDelete.Length;
			}

			/// //////////////////////////////////////////////////////////////////
			public DateTime DateCancelProgramme
			{
				get
				{
					return m_dtCancelProgramme;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public void ProgrammeCancelIn ( int nNbMinutes )
			{
				m_dtCancelProgramme = DateTime.Now.AddMinutes ( nNbMinutes );
			}
		}

		#endregion

		/// //////////////////////////////////////////////////////////////////
		//Retourne un id de process de synchronisation
		public int StartProcessSynchronisationMainToSecondaire ( 
			IAuthentificationSession authentification,
			int nIdLastIdSyncMain,
			string strCodeGroupeSynchro,
			int nVersionParametre)
		{
			CSessionProcessServeurSuivi session = new CSessionProcessServeurSuivi ();
			CResultAErreur result = session.OpenSession ( authentification, "Synchronisation user", null );
			if ( !result )
				return -1;
			CProcessSynchronisationMainToSecondaire process = new CProcessSynchronisationMainToSecondaire(
				//TODO
				session.IdSession,
				nIdLastIdSyncMain,
				strCodeGroupeSynchro );
			m_tableProcessSynchronsationMainToSecondaire[process.IdProcess] = process;
			return process.IdProcess;
		}

		/// //////////////////////////////////////////////////////////////////
		public int GetVersionVueDeBaseMain ( int nIdProcess )
		{
			return GetProcessMainToSec ( nIdProcess ).IdSyncSessionEndInMain;
		}
		
		/// //////////////////////////////////////////////////////////////////
		protected CProcessSynchronisationMainToSecondaire GetProcessMainToSec ( int nIdProcess )
		{
			return ( CProcessSynchronisationMainToSecondaire )m_tableProcessSynchronsationMainToSecondaire[nIdProcess];
		}
		
		/// //////////////////////////////////////////////////////////////////
		//Retourne vrai s'il reste des données d'ajout ou update à synchronisation
		public bool HasDataToAddOrUpdateMainToSecondaire ( int nIdProcessSynchro )
		{
			return GetProcessMainToSec ( nIdProcessSynchro ).HasDataToAddOrUpdate();
		}

		/// //////////////////////////////////////////////////////////////////
		public string[] GetListeTablesConcerneesParAddOrUpdateMainToSecondaire ( int nIdProcessSynchro )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				return null;
			ArrayList lst = new ArrayList();
			foreach ( string strNomTable in CContexteDonnee.GetFastListNomTablesOrderInsert() )
			{
				if ( process.Filtres != null && !(process.Filtres.GetFiltreForTable ( process.IdSessionClient, strNomTable ) is CFiltreDataImpossible ))
					lst.Add ( strNomTable );
			}
			return (string[])lst.ToArray ( typeof(string));
		}

		/// //////////////////////////////////////////////////////////////////
		public DataTable GetDataToAddOrUpdateMainToSecondaire ( 
			int nIdProcessSynchro,
			string strNomTable,
			int[] listeIdsPresentsInSecondaire )
		{
			try
			{
				CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
				if ( process == null )
					return null;
				process.ProgrammeCancelIn ( 10 );
				using ( CContexteDonnee contexte = new CContexteDonnee ( process.IdSessionClient, true, false ))
				{
					Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
					if ( tp == null )
						return null;
					CStructureTable structure = CStructureTable.GetStructure(tp);

					CFiltreData filtreSynchro = null;
					if ( process.Filtres != null )
						filtreSynchro = process.Filtres.GetFiltreForTable ( process.IdSessionClient, strNomTable );

					if ( filtreSynchro == null )
						filtreSynchro = new CFiltreData();
					bool bVerifieIdSyncSession = process.IdSyncSessionStartInMain >= 0;
					if ( bVerifieIdSyncSession )
						if ( listeIdsPresentsInSecondaire != null && listeIdsPresentsInSecondaire.Length == 0 )
							bVerifieIdSyncSession = false;
					if ( bVerifieIdSyncSession )
					{
						int nNumParametreIdSync = filtreSynchro.Parametres.Count+1;
						CFiltreDataAvance filtreIdSynchro = new CFiltreDataAvance ( strNomTable,
							"("+CSc2iDataConst.c_champIdSynchro+">=@1 and "+
							CSc2iDataConst.c_champIdSynchro+"<=@2", 
							process.IdSyncSessionStartInMain,
							process.IdSyncSessionEndInMain );
						if ( process.IdSyncSessionStartInMain == -1 )
							//C'est la première synchro, intègre les éléments modifiés avant prise en charge
							//des synchros
						{
							filtreIdSynchro.Filtre += " or hasno("+CSc2iDataConst.c_champIdSynchro+")";
						}

						if ( 
							listeIdsPresentsInSecondaire != null && 
							listeIdsPresentsInSecondaire.Length > 0 && 
							filtreSynchro.HasFiltre/*pas si table complete !*/ )
						{
							filtreIdSynchro.Filtre += " or "+structure.ChampsId[0].NomChamp+" notin {";
							foreach ( int nId in listeIdsPresentsInSecondaire )
								filtreIdSynchro.Filtre += nId.ToString()+";";
							filtreIdSynchro.Filtre = filtreIdSynchro.Filtre.Substring(0, filtreIdSynchro.Filtre.Length-1);
							filtreIdSynchro.Filtre +="}";
						}
			
						filtreIdSynchro.Filtre+=")";
						filtreSynchro = CFiltreData.GetAndFiltre ( filtreIdSynchro, filtreSynchro );
					}
					CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte, tp, false );
					liste.Filtre = filtreSynchro;
					int nNumRec = 0;
					if ( strNomTable == process.GetTableEnCours(ref nNumRec) )
					{
						liste.StartAt = nNumRec;
						liste.EndAt = nNumRec + c_nNbRecordMaxParEnvoi;
					}
					else
					{
						nNumRec = 0;
						liste.StartAt = 0;
						liste.EndAt = c_nNbRecordMaxParEnvoi;
					}
					liste.AssureLectureFaite();
					process.SetTableEnCours ( strNomTable, nNumRec+liste.Count );
					return contexte.Tables[strNomTable];
				}
			}
			catch ( Exception e )
			{
				C2iEventLog.WriteErreur(I.T("Synchonization recovery of table @1 error @2|204",strNomTable,e.ToString()));
				return null;
			}
		}

		/// //////////////////////////////////////////////////////////////////
		public byte[] GetBlob ( int nIdProcessSynchro, string strNomTable, string strNomChampBlob, int nIdElement )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				throw new Exception(I.T("Impossible to connect to the '@1' process|203",nIdProcessSynchro.ToString()));
			try
			{
				using ( CContexteDonnee contexte = new CContexteDonnee ( process.IdSessionClient, true, false) )
				{
					DataTable table =  contexte.GetTableSafe ( strNomTable );
					if ( table == null )
						throw new Exception(I.T("Table '@1' doesn't exist|169", strNomTable));
					CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)contexte.GetNewObjetForTable ( table );
					if ( objet.ReadIfExists ( nIdElement ) )
					{
						CDonneeBinaireInRow donnee = new CDonneeBinaireInRow ( process.IdSessionClient, objet.Row.Row, strNomChampBlob );
						return donnee.Donnees;
					}
				}
			}
			catch ( Exception e )
			{
				throw e;
			}
			return null;
		}

		/// //////////////////////////////////////////////////////////////////
		//Retourne un dataset (partiel) avec des données à ajouter ou à modifier 
		public DataSet GetDataToAddOrUpdateMainToSecondaire ( int nIdProcessSynchro, IIndicateurProgression informateur )
		{
			/*try
			{
				int nNbRecords = 0;
				int nNbTables = 0;
				DataSet dsFinal = new DataSet();
				CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
				if ( process == null )
					return null;
				process.ProgrammeCancelIn ( 60 );
				bool bContinue = process.HasDataToAddOrUpdate();
				while ( bContinue )
				{
					using ( CContexteDonnee contexte = new CContexteDonnee ( process.IdSessionClient,true, false ) )
					{
						int nRecordStart = 0;
						DataTable tableToCopy = process.GetTableEnCours(ref nRecordStart);
						if ( tableToCopy == null )
							tableToCopy = ReadAddAndUpdatesInNextTableMainToSecondaire ( process, contexte );
						else
							process.ResetTableEnCours();

						if ( tableToCopy != null )
						{
							if ( informateur != null )
								informateur.SetInfo ( "Table "+tableToCopy.TableName );
							
							
							if ( tableToCopy.Rows.Count > 0 )
							{
								//Supprime les colonnes blob de la table pour le transfert web
								for ( int nColonne = tableToCopy.Columns.Count-1; nColonne>=0; nColonne-- )
									if ( tableToCopy.Columns[nColonne].DataType == typeof(CDonneeBinaireInRow) )
										tableToCopy.Columns.RemoveAt ( nColonne );
								nNbTables++;
								Console.WriteLine(tableToCopy.TableName);
								dsFinal.Tables.Add ( tableToCopy.Clone() );
								DataTable table = dsFinal.Tables[tableToCopy.TableName];
								int nNbRows = tableToCopy.Rows.Count;
								Type tpObjets = CContexteDonnee.GetTypeForTable ( tableToCopy.TableName );
								bool bIsAutoreferencee = typeof(IObjetDonneeAutoReference).IsAssignableFrom(tpObjets);
								for ( int nRow = nRecordStart; nRow < nNbRows; nRow++ )
								{
									table.ImportRow ( tableToCopy.Rows[nRow] );
									nNbRecords++;
									//Il ne faut pas scinder les autoreferencés
									if ( !bIsAutoreferencee && nNbRecords >= c_nNbRecordMaxParEnvoi )
									{
										//Conserve la table dans le process
										DataTable tableSave = tableToCopy.Clone();
										foreach ( DataRow row in tableToCopy.Rows )
											tableSave.ImportRow ( row );
										process.SetTableEnCours ( tableSave, nRow+1 );
										bContinue = false;
										break;
									}
								}
								if ( nNbTables >= c_nNbTablesMaxParEnvoi )
									bContinue = false;
							}
						}
					
						bContinue &= process.HasDataToAddOrUpdate();
					}
				}
				//le client a dix minutes pour prendre les modifs
				process.ProgrammeCancelIn ( 10 );

				return dsFinal;
			}
			catch ( Exception e )
			{
				throw ( e );
			}*/
			return null;

		}

		/// ////////////////////////////////////////////////////////
		public void RenouvelleBailMainToSecondaire ( int nProcess, int nNbMinutes )
		{
			GetProcessMainToSec ( nProcess ).ProgrammeCancelIn ( nNbMinutes );
		}

		/// //////////////////////////////////////////////////////////////////
		protected DataTable ReadAddAndUpdatesInNextTableMainToSecondaire ( CProcessSynchronisationMainToSecondaire process, CContexteDonnee contexte )
		{
			try
			{
				string strNomTable = process.PopNextNomTableToAddOrUpdate();
				string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable);
			
				//Vérifie s'il y a qq chose d'interessant ( hors filtres )
				string strRequete = "select count(*) from " + strNomTableInDb + " where " +
					CSc2iDataConst.c_champIdSynchro+">="+process.IdSyncSessionStartInMain+" and "+
					CSc2iDataConst.c_champIdSynchro+"<="+process.IdSyncSessionEndInMain;
				if ( process.IdSyncSessionStartInMain == -1 )
					strRequete += " or "+CSc2iDataConst.c_champIdSynchro+" is null";

				CResultAErreur result = CSc2iDataServer.GetInstance().GetDatabaseConnexion(process.IdSessionClient, "").ExecuteScalar(strRequete);
				if ( !result  || !(result.Data is int) || ((int)result.Data)== 0 )
					return null;

				if ( strNomTable == null )
					return null;
				Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
				if ( tp == null )
					return null;

				CFiltreData filtreSynchro = null;
				if ( process.Filtres != null )
					filtreSynchro = process.Filtres.GetFiltreForTable ( process.IdSessionClient, strNomTable );

				if ( filtreSynchro == null )
					filtreSynchro = new CFiltreData();
				int nNumParametreIdSync = filtreSynchro.Parametres.Count+1;
				string strFiltre = filtreSynchro.Filtre;
				filtreSynchro.Filtre = "("+CSc2iDataConst.c_champIdSynchro+">=@"+nNumParametreIdSync.ToString()+" and "+
					CSc2iDataConst.c_champIdSynchro+"<=@"+(nNumParametreIdSync+1).ToString();
				if ( process.IdSyncSessionStartInMain == -1 )
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
				filtreSynchro.Parametres.Add ( process.IdSyncSessionStartInMain );
				filtreSynchro.Parametres.Add ( process.IdSyncSessionEndInMain );
				CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte, tp, false );
				liste.Filtre = filtreSynchro;
				liste.AssureLectureFaite();
			
				return contexte.Tables[strNomTable];
			}
			catch ( Exception e )
			{
				throw ( e );
			}

		}

		/// ///////////////////////////////////////////////
		public string[] GetListeTablesConcerneesParDeleteSynchronisationMainToSecondaire ( int nIdProcessSynchro )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				return null;
			ArrayList lst = new ArrayList();
			foreach ( string strNomTable in CContexteDonnee.GetFastListNomTablesOrderDelete() )
			{
				//La table doit être synchronisable.
				//Par contre son filtre peut être un filtreDataImpossible, si
				//il y a eu une modif du paramétrage
				Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
				object[] attribs = tp.GetCustomAttributes(typeof(TableAttribute), true);
				if ( attribs.Length > 0 && ((TableAttribute)attribs[0]).Synchronizable )
					lst.Add ( strNomTable );
			}
			return (string[])lst.ToArray ( typeof(string));
		}

		/// ///////////////////////////////////////////////
		public int[] GetListeIdsToDeleteInTable ( int nIdProcessSynchro, string strNomTable, int[] listeIdsOnSecondaire )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				return null;
			Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
			if ( tp == null )
				return null;
			CFiltreData filtreSynchro = null;
			if ( process.Filtres != null )
				filtreSynchro = process.Filtres.GetFiltreForTable ( process.IdSessionClient, strNomTable );
			if ( filtreSynchro == null )
				filtreSynchro = new CFiltreData();
			if ( filtreSynchro is CFiltreDataImpossible )//Tout doit disparaitre
				return listeIdsOnSecondaire;
			CStructureTable structure = CStructureTable.GetStructure(tp);
			string strCle = structure.ChampsId[0].NomChamp;
			string strRequete = "select "+structure.NomTableInDb+"."+strCle+" from "+strNomTable;
			IDataAdapter adapter = CSc2iDataServer.GetInstance().GetDatabaseConnexion ( process.IdSessionClient, tp ).GetSimpleReadAdapter ( strRequete, filtreSynchro );
			DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
                DataTable table = ds.Tables[0];
                Hashtable tableIds = new Hashtable();
                foreach (DataRow row in table.Rows)
                    tableIds[(int)row[0]] = true;
                ArrayList lstToReturn = new ArrayList();
                foreach (int nIdInSecondaire in listeIdsOnSecondaire)
                    if (tableIds[nIdInSecondaire] == null)
                        lstToReturn.Add(nIdInSecondaire);
                return (int[])lstToReturn.ToArray(typeof(int));
            }
            catch
            {
                return null;
            }
            finally
            {
                CUtilDataAdapter.DisposeAdapter(adapter);
            }
		}
					 




		/// ///////////////////////////////////////////////
		public DataSet GetDeletesMainToSecondaire ( int nIdProcessSynchro, IIndicateurProgression indicateur )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				return null;
			DataSet ds = new DataSet();
			using ( CContexteDonnee contexte = new CContexteDonnee ( process.IdSessionClient, true, false ) )
			{
				CFiltreData filtre = new CFiltreData ( CEntreeLogSynchronisation.c_champType +"="+
					((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete).ToString()+" and "+
					CSc2iDataConst.c_champIdSynchro+">=@1 and "+
					CSc2iDataConst.c_champIdSynchro+"<=@2",
					process.IdSyncSessionStartInMain,
					process.IdSyncSessionEndInMain );
				CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte, typeof(CEntreeLogSynchronisation) );
				liste.Filtre = filtre;
				liste.AssureLectureFaite();
				if ( contexte.Tables[CEntreeLogSynchronisation.c_nomTable] != null )
					CUtilDataSet.Merge ( contexte.Tables[CEntreeLogSynchronisation.c_nomTable], ds, false );
			}
			return ds;
		}

		/// <summary>
		/// ///////////////////////////////////////////////
		/// </summary>
		/// <param name="nIdProcessSynchro"></param>
		public void EndProcessSynchronisationMainToSecondaire ( int nIdProcessSynchro )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process != null )
			{
				IDatabaseConnexionSynchronisable con = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(process.IdSessionClient, "");
				con.IncrementeSyncSession();
				m_tableProcessSynchronsationMainToSecondaire.Remove(nIdProcessSynchro);
				process.EndProcess();
			}
		}

		

		/// ///////////////////////////////////////////////
		/// //Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le nom de la table en cours de lecture
		public string GetNomTableAddOrUpdateEnCoursMainToSecondaire ( int nIdProcessSynchro )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				return "";
			return process.PeekNomTableEnCoursAddOrUpdate();

		}

		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le taux d'avancement du add entre 0 et 100
		public int GetPourcentageAddOrUpdateEnCoursMainToSecondaire ( int nIdProcessSynchro )
		{
			CProcessSynchronisationMainToSecondaire process = GetProcessMainToSec ( nIdProcessSynchro );
			if ( process == null )
				return 0;
			return process.GetPourcentageAddOrUpdateEnCours();
		}

		#endregion

		#region Synchronisation Secondaire->Main

		#region CProcessSynchronisationSecondaireToMain
		private class CProcessSynchronisationSecondaireToMain : IDisposable
		{
			private int m_nIdProcess = -1;
			private int m_nIdSyncSessionAlloueeInMain = -1;
			private int m_nIdLastVueDeMainInSecondaire = -1;
			private string m_strTableEnCours = "";
			private int m_nPourcentageAvancement = 0;
			private int m_nIdSessionClient = -1;

			//Après ouverture du process, on a 2 minutes pour l'utiliser
			private DateTime m_dtCancelProgramme = DateTime.Now.AddMinutes ( 2 );

			/// ////////////////////////////////////////////////////////
			public CProcessSynchronisationSecondaireToMain ( 
				int nIdSessionClient, 
				int nIdLastVueDeMainInSecondaire )
			{
				m_nIdSessionClient = nIdSessionClient;
                IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( nIdSessionClient, "" );
				connexion.IncrementeSyncSession();
				m_nIdProcess = connexion.IdSyncSession;
				m_nIdSyncSessionAlloueeInMain = m_nIdProcess;
				m_nIdLastVueDeMainInSecondaire = nIdLastVueDeMainInSecondaire;
				connexion.IncrementeSyncSession();
			}

			/// ////////////////////////////////////////////////////////
			public void Dispose()
			{
				EndProcess();
			}

			/// //////////////////////////////////////////////////////////////////
			public void EndProcess()
			{
				try
				{
					CSessionClient session = CSessionClient.GetSessionForIdSession ( m_nIdSessionClient );
					if ( session != null )
						session.CloseSession();
				}
				catch
				{}
			}

			/// ////////////////////////////////////////////////////////
			public int IdSessionClient
			{
				get
				{
					return m_nIdSessionClient;
				}
			}

			/// ////////////////////////////////////////////////////////
			public int IdProcess
			{
				get
				{
					return m_nIdProcess;
				}
			}

			/// ////////////////////////////////////////////////////////
			///Toutes les modifs dans la base main doivent être faites sous cet Id de session
			public int IdSyncSessionAlloueeInMain
			{
				get
				{
					return m_nIdSyncSessionAlloueeInMain;
				}
			}

			/// ////////////////////////////////////////////////////////
			public int IdLastVueDeMainInSecondaire
			{
				get
				{
					return m_nIdLastVueDeMainInSecondaire;
				}
			}

			/// ////////////////////////////////////////////////////////
			public string TableEnCours
			{
				get
				{
					return m_strTableEnCours;
				}
				set
				{
					m_strTableEnCours = value;
				}
			}

			/// ////////////////////////////////////////////////////////
			public int PourcentageAvancement
			{
				get
				{
					return m_nPourcentageAvancement;
				}
				set
				{
					m_nPourcentageAvancement = value;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public DateTime DateCancelProgramme
			{
				get
				{
					return m_dtCancelProgramme;
				}
			}

			/// //////////////////////////////////////////////////////////////////
			public void ProgrammeCancelIn ( int nNbMinutes )
			{
				m_dtCancelProgramme = DateTime.Now.AddMinutes ( nNbMinutes );
			}
		}
		#endregion

		/// ////////////////////////////////////////////////////////
		///Retourne l'id du process
		public int StartProcessSynchronisationSecondaireToMain ( IAuthentificationSession authentification, int nIdLastVersionVueDeMainParSecondaire )
		{
			CSessionProcessServeurSuivi session = new CSessionProcessServeurSuivi();
			CResultAErreur result = session.OpenSession ( 
				authentification, 
				"Synchronisation",
				null );
			if ( !result )
				return -1;
			CProcessSynchronisationSecondaireToMain process = new CProcessSynchronisationSecondaireToMain(
				session.IdSession, 
				nIdLastVersionVueDeMainParSecondaire);
			m_tableProcessSynchronisationSecondaireToMain[process.IdProcess] = process;
			return process.IdProcess;
		}

		/// ////////////////////////////////////////////////////////
		private CProcessSynchronisationSecondaireToMain GetProcessSecToMain ( int nIdProcess )
		{
			return ( CProcessSynchronisationSecondaireToMain )m_tableProcessSynchronisationSecondaireToMain[nIdProcess];
		}

		/// ////////////////////////////////////////////////////////
		public int GetIdSyncSessionAlloueeInBaseMain ( int nIdProcess )
		{
			return GetProcessSecToMain ( nIdProcess ).IdSyncSessionAlloueeInMain;
		}

		/// ////////////////////////////////////////////////////////
		/// Retourne un dataset avec la table de mappage des champs id nouveaux
		public DataSet SendAddsUpdateAndDelete ( int nIdProcess, DataSet ds, IIndicateurProgression indicateur )
		{
			try
			{
				DataSet dsRetour = new DataSet();
				CProcessSynchronisationSecondaireToMain process = GetProcessSecToMain ( nIdProcess );
				using ( CContexteDonnee contexte = new CContexteDonnee ( process.IdSessionClient, true, false ) )
				{
					contexte.BeginModeDeconnecte();
				
					if ( process == null )
						return null;
					process.ProgrammeCancelIn ( 60 );
				
					IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( process.IdSessionClient, "" );
					connexion.LockSyncSessionLocalTo ( process.IdSyncSessionAlloueeInMain );

				
					Hashtable mapOldRowToNew = new Hashtable();

					if ( indicateur != null )
						indicateur.SetBornesSegment ( 0, ds.Tables.Count*2 );

					DoAddsAndModifs ( process, ds, contexte, dsRetour, mapOldRowToNew, indicateur );

					DoDelete ( process, ds, contexte, dsRetour, indicateur );
				
					contexte.EndModeDeconnecteSansSauvegardeEtSansReject();
					//Démarre la transaction. Elle ne sera validée que dans le CommitProcessSynchronisationSecondaireToMain
					connexion.BeginTrans();
					try
					{
						//Sauve les données
						CResultAErreur result = contexte.SaveAll ( false );
						if ( !result )
							return null;
						foreach ( DataRow rowSource in mapOldRowToNew.Keys )
						{
							DataTable table = rowSource.Table;
							DataRow newRow = (DataRow)mapOldRowToNew[rowSource];
							string strKey = table.PrimaryKey[0].ColumnName;
							rowSource.Table.Columns[strKey].ReadOnly = false;
							rowSource.Table.Columns[strKey].AutoIncrement = false;
							rowSource[strKey] = newRow[strKey];
							CApplicateurSynchronisationDataSet.AddMapId ( dsRetour, table.TableName, 
								(int)rowSource[strKey,DataRowVersion.Original], (int)rowSource[strKey] );
						}
						//Le client a 5 minutes pour valider les modifs (il ne faut pas laisser trainer la transaction)
						process.ProgrammeCancelIn ( 5 );
					}
					catch
					{
						connexion.RollbackTrans();
						return null;
					}
				}
				return dsRetour;
			}
			catch ( Exception e )
			{
				throw e;
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool SendBlob ( 
			int nIdProcessSynchro,
			string strNomTable,
			string strNomChampBlob,
			int nIdElement,
			byte[] data)
		{
			CProcessSynchronisationSecondaireToMain process = GetProcessSecToMain ( nIdProcessSynchro );
			if ( process == null )
				return false;
			try
			{
				using ( CContexteDonnee contexte = new CContexteDonnee ( process.IdSessionClient, true, false ) )
				{
					DataTable table = contexte.GetTableSafe ( strNomTable );
					if ( table == null )
						return false;
					CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)contexte.GetNewObjetForTable ( table );
					if ( objet.ReadIfExists ( nIdElement ) )
					{
						CDonneeBinaireInRow donnee = new CDonneeBinaireInRow ( process.IdSessionClient, objet.Row.Row, strNomChampBlob );
						donnee.Donnees = data;
						if ( !donnee.SaveData (-1) )
							return false;
					}
				}
			}
			catch ( Exception e )
			{
				throw e;
			}
			return true;
		}

			

		/// ////////////////////////////////////////////////////////
		private void DoAddsAndModifs ( 
			CProcessSynchronisationSecondaireToMain process,
			DataSet dsSource,
			CContexteDonnee contexteDest,
			DataSet dsRetour,
			Hashtable mapOldRowToNew,
			IIndicateurProgression indicateur)
		{
			try
			{
				contexteDest.EnforceConstraints = false;
				DataTable tableSyncLog = dsSource.Tables[CEntreeLogSynchronisation.c_nomTable];
				int nTable = 0;
				//Fait les modifications dans le contexte
				foreach ( DataTable tableSource in CContexteDonnee.GetTablesOrderInsert ( dsSource ) )
				{
					nTable++;
					if ( indicateur != null )
					{
						indicateur.SetValue ( nTable );
						indicateur.SetInfo (I.T("Integration @1|205",tableSource.TableName));
					}
					process.TableEnCours = tableSource.TableName;
					process.PourcentageAvancement = nTable*100/(2*dsSource.Tables.Count);
					
					//Pour pouvoir les modifier !
					tableSource.PrimaryKey[0].ReadOnly = false;
					tableSource.PrimaryKey[0].AutoIncrement = false;
					
					if ( CContexteDonnee.MappeurTableToClass.IsSynchronisable(tableSource.TableName) )
					{
						DataTable tableDest = contexteDest.GetTableSafe(tableSource.TableName);
						string strPrimaryKey = tableDest.PrimaryKey[0].ColumnName;
						if ( tableDest != null )
						{
							if ( tableSyncLog != null )
							{
								//Crée les nouveaux enregistrements
								DataView view = new DataView ( tableSyncLog);
								view.RowFilter = 
									CEntreeLogSynchronisation.c_champTable+"='"+ tableSource.TableName+"' and "+
									CEntreeLogSynchronisation.c_champType+"="+((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd).ToString();
								foreach ( DataRowView rowView in view )
								{
									
									int nIdElement = (int)rowView[CEntreeLogSynchronisation.c_champIdElement];
									DataRow rowOriginale = tableSource.Rows.Find ( nIdElement );
									if ( rowOriginale != null )
									{
										CObjetDonneeAIdNumeriqueAuto newObjet = (CObjetDonneeAIdNumeriqueAuto)contexteDest.GetNewObjetForTable ( tableDest );
										newObjet.CreateNewInCurrentContexte();
										DataRow row = newObjet.Row.Row;
										Hashtable mapChampToNewIdParent = new Hashtable();
										ArrayList lstExclusions = new ArrayList();
										CopyRowToNewContexte ( 
											dsSource,
											rowOriginale,
											contexteDest,
											row,
											mapOldRowToNew );
										DataRow rowSource = tableSource.Rows.Find(nIdElement);
										mapOldRowToNew[rowSource] = row;
									}
								}
							}
							foreach( DataRow rowSource in tableSource.Rows )
							{
								int nId = (int)rowSource[strPrimaryKey];
								if ( mapOldRowToNew[rowSource] == null )//pas ajoutée
								{
									//Lit la ligne dans la base
									CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)contexteDest.GetNewObjetForTable ( tableDest );
									if ( objet.ReadIfExists ( nId ) )
									{
										DataRow rowDest = objet.Row.Row;
										object syncId = rowDest[CSc2iDataConst.c_champIdSynchro];
										if ( syncId == DBNull.Value ||
											 ((int)syncId) < process.IdLastVueDeMainInSecondaire )
											CopyRowToNewContexte ( dsSource, rowSource, contexteDest, rowDest, mapOldRowToNew );
										else
											Console.WriteLine(I.T("Line rejected in table @1|206",rowSource.Table.TableName));
									}
								}
							}
						}  
					}		
				}
				/*//change les clés etrangères des nouveaux éléments
				foreach ( DataRow rowSource in mapOldRowToNew.Keys )
				{
					DataRow rowDest = (DataRow)mapOldRowToNew[rowSource];
					int nOldId = (int)rowSource[rowSource.Table.PrimaryKey[0].ColumnName];
					int nNewId = (int)rowDest[rowDest.Table.PrimaryKey[0].ColumnName];
					string strNomTable = rowSource.Table.TableName;
					if ( nOldId != nNewId )
					{
						//Trouve toutes les dépendances
						foreach ( DataRelation relation in contexteDest.GetTableSafe(strNomTable).ChildRelations )
						{
							DataTable tableDest = contexteDest.Tables[relation.ChildTable.TableName];
							DataView view = new DataView ( dsSource.Tables[tableDest.TableName] );
							view.RowFilter = relation.ChildColumns[0].ColumnName+"="+nOldId.ToString();
							foreach ( DataRowView rowFilleOriginale in view )
							{
								DataRow rowChildDest = (DataRow)mapOldRowToNew[rowFilleOriginale.Row];
								if ( rowChildDest == null )//seulement modifiée !
									rowChildDest = tableDest.Rows.Find(rowFilleOriginale.Row[tableDest.PrimaryKey[0]]);
								if ( rowChildDest == null )
									throw new Exception("une ligne fille d'un nouvel élément est absente");
								foreach ( DataColumn col in relation.ChildColumns )
									rowChildDest[col.ColumnName] = rowFilleOriginale[col.ColumnName];
							}
						}
					}
				}*/
				contexteDest.EnforceConstraints = true;
			}
			catch ( Exception e )
			{
				throw e;
			}
		}

		/// ////////////////////////////////////////////////////////
		/// Copie une row dans le contexte serveur en modifiant éventuellement les ids des parents
		private void CopyRowToNewContexte ( 
			DataSet dsSource,
			DataRow rowOriginale, 
			CContexteDonnee contexteDest,
			DataRow newRow, 
			Hashtable mapOldRowToNew )
		{
			
			DataTable tableDest = newRow.Table;
			ArrayList lstExclusions = new ArrayList();
			lstExclusions.Add ( tableDest.PrimaryKey[0].ColumnName );
			foreach ( DataRelation relation in tableDest.ParentRelations )
			{
				DataTable tableParente = dsSource.Tables[relation.ParentTable.TableName];
				if ( tableParente != null )
				{
					int nOldId = (int)rowOriginale[relation.ChildColumns[0].ColumnName];
					DataRow rowParente = tableParente.Rows.Find(nOldId);
					if ( rowParente != null )
					{
						rowParente = (DataRow)mapOldRowToNew[rowParente];
						if ( rowParente != null )
						{
							lstExclusions.Add ( relation.ChildColumns[0].ColumnName );
							newRow[relation.ChildColumns[0].ColumnName] = rowParente[relation.ParentColumns[0].ColumnName];
						}
					}
				}
			}
			contexteDest.CopyRow ( rowOriginale, newRow, false, (string[])lstExclusions.ToArray(typeof(string)) );
		}


		/// ////////////////////////////////////////////////////////
		private void DoDelete ( 
			CProcessSynchronisationSecondaireToMain	process,
			DataSet dsSource,
			CContexteDonnee contexteDest,
			DataSet dsRetour,
			IIndicateurProgression indicateur)
		{
			DataTable tableSyncLog = dsSource.Tables[CEntreeLogSynchronisation.c_nomTable];
			int nTable = 0;
			foreach ( DataTable tableSource in CContexteDonnee.GetTableOrderDelete ( dsSource ) )
			{
				nTable++;
				if ( indicateur != null )
				{
					indicateur.SetValue ( nTable );
					indicateur.SetInfo (I.T("Deleting @1|207",tableSource.TableName));
				}
				process.TableEnCours = tableSource.TableName;
				process.PourcentageAvancement = nTable*100/(2*dsSource.Tables.Count);
				if ( CContexteDonnee.MappeurTableToClass.IsSynchronisable(tableSource.TableName) )
				{
					DataTable tableDest = contexteDest.GetTableSafe(tableSource.TableName);
					string strPrimaryKey = tableDest.PrimaryKey[0].ColumnName;
					if ( tableDest != null )
					{
						if ( tableSyncLog != null )
						{
							//Crée les nouveaux enregistrements
							DataView view = new DataView ( tableSyncLog);
							view.RowFilter = 
								CEntreeLogSynchronisation.c_champTable+"='"+ tableSource.TableName+"' and "+
								CEntreeLogSynchronisation.c_champType+"="+((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete).ToString();
							foreach ( DataRowView rowView in view )
							{
								int nIdElement = (int)rowView[CEntreeLogSynchronisation.c_champIdElement];
								CObjetDonneeAIdNumeriqueAuto objetToDelete = (CObjetDonneeAIdNumeriqueAuto)contexteDest.GetNewObjetForTable ( tableDest );
								if ( objetToDelete.ReadIfExists ( nIdElement ) )
								{
									if ( objetToDelete.CanDelete ( ) )
										objetToDelete.Delete();
									else
									{
										//On ne peut pas le supprimer, il faut annuler la suppression dans le contexte secondaire
										//On lui renvoie donc les données de cette ligne
										DataTable tableSecondaire = dsRetour.Tables[tableDest.TableName];
										if ( tableSecondaire == null )
											CUtilDataSet.AddTableCopie ( tableDest, dsRetour );
										DataRow rowSec = tableDest.NewRow();
										CUtilDataSet.CopyRow ( objetToDelete.Row.Row, rowSec, DataRowVersion.Current, false );
										tableSecondaire.Rows.Add ( rowSec );
									}
								}
							}
						}
					}
				}
			}
		}
		/// ////////////////////////////////////////////////////////
		public void RenouvelleBailSecondaireToMain ( int nProcess, int nNbMinutes )
		{
			GetProcessSecToMain ( nProcess ).ProgrammeCancelIn ( nNbMinutes );
		}

		/// ////////////////////////////////////////////////////////
		public void CancelProcessSynchronisationSecondaireToMain ( int nIdProcess )
		{
			CProcessSynchronisationSecondaireToMain process = GetProcessSecToMain ( nIdProcess );
			IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( process.IdSessionClient, "" );
			connexion.RollbackTrans();
			m_tableProcessSynchronisationSecondaireToMain.Remove ( nIdProcess );
			process.EndProcess();
		}

		/// ////////////////////////////////////////////////////////
		public void CommitProcessSynchronisationSecondaireToMain ( int nIdProcess )
		{
			CProcessSynchronisationSecondaireToMain process = GetProcessSecToMain ( nIdProcess );
			IDatabaseConnexionSynchronisable connexion = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion( process.IdSessionClient, "" );
			connexion.CommitTrans();
			m_tableProcessSynchronisationSecondaireToMain.Remove ( nIdProcess );
			process.EndProcess();
		}

		/// ////////////////////////////////////////////////////////
		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le nom de la table en cours d'intégration
		public string GetNomTableAddOrUpdateEnCoursSecondaireToMain ( int nIdProcessSynchro )
		{
			return GetProcessSecToMain ( nIdProcessSynchro ).TableEnCours;
		}

		/// ////////////////////////////////////////////////////////
		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le taux d'avancement du add entre 0 et 100
		public int GetPourcentageAddOrUpdateEnCoursSecondaireToMain ( int nIdProcessSynchro )
		{
			return GetProcessSecToMain ( nIdProcessSynchro ).PourcentageAvancement;
		}

		
        #endregion
		
	}
}
