using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.synchronisation;
using sc2i.multitiers.client;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CSynchroniseur.
	/// </summary>
	public delegate CResultAErreur AfterRecupElementFromMainEventHandler ( 
				int nIdProcessSynchronisation,
				CObjetDonnee objet, 
				ISynchroniseurBddSecondaire synchroniseur );
	public class CApplicateurSynchronisationDataSet
	{
		//Hashtable des éléments ajoutées sous la forme TABLE_ID
		private Hashtable m_tableAjouts = new Hashtable();

		public const string c_nomTableChangementId = "MAP_NEW_IDS";
		public const string c_champTable = "TABLENAME";
		public const string c_champOldId = "OLDID";
		public const string c_champNewId = "NEWID";

		////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Remplit le CContexteDonnee avec toutes les données modifiées depuis l'Id de synchro demandée
		/// </summary>
		/// <param name="nIdSynchro"></param>
		/// <param name="bHasData">Indique si les données ont changé</param>
		/// <returns></returns>
		public CResultAErreur  FillWithModifFromVersion ( 
			IDatabaseConnexionSynchronisable connexion,
			DataSet dsDest, 
			int nIdSynchroDebut, 
			ref bool bHasData,
			IIndicateurProgression indicateurProgression)
		{
			//m_nIdSynchro = nIdSynchroDebut;
			CResultAErreur result = CResultAErreur.True;

			//va chercher les informations dans la base pour chaque table
			string[] lstTables = CContexteDonnee.GetFastListNomTablesOrderInsert();
			int nTable = 0;
			if ( indicateurProgression != null )
				indicateurProgression.SetBornesSegment ( 0, lstTables.Length );
			foreach (string strNomTable in lstTables )
			{
				nTable++;
				string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable);
				if (CContexteDonnee.MappeurTableToClass.IsSynchronisable(strNomTableInDb))
				{
					if ( indicateurProgression != null )
					{
						indicateurProgression.SetInfo (I.T("Find @1|134",strNomTable ));
						indicateurProgression.SetValue ( nTable );
					}
					string strReq = "select count(*) from " + strNomTableInDb + " where " +
						CSc2iDataConst.c_champIdSynchro+">"+nIdSynchroDebut;
					result = connexion.ExecuteScalar ( strReq );
					if ( result && (result.Data is int) && ((int)result.Data)>0 )
					{
						IObjetServeur loader = CContexteDonnee.GetTableLoader(strNomTable, null, connexion.IdSession);
						CFiltreData filtreSynchro = null;
						filtreSynchro = new CFiltreData();
						string strFiltre = filtreSynchro.Filtre;
						filtreSynchro.Filtre = "("+CSc2iDataConst.c_champIdSynchro+">@1)";
						filtreSynchro.Parametres.Add ( nIdSynchroDebut );
						DataTable newTable = loader.Read(filtreSynchro);
						if ( !result )
							return result;
						if ( indicateurProgression != null )
						{
							indicateurProgression.SetInfo ( "Table "+strNomTable+" "+ newTable.Rows.Count+" Enrs" );
							indicateurProgression.SetValue ( nTable );
						}
						if ( newTable.Rows.Count != 0 )
						{
							bHasData = true;
							CUtilDataSet.Merge ( newTable, dsDest, true );
						}					
					}
					
				}
			}

			Hashtable tableRowAdd = new Hashtable();//Liste des éléments ajoutés
			ArrayList listRowToDelete = new ArrayList();
			//Les éléments sont supprimés s'ils ont été ajoutés et supprimés ensuite

			using  ( CContexteDonnee contexteTmp = new CContexteDonnee ( connexion.IdSession, true, false ) )
			{
				//Charge les logs de données ajoutées et supprimées
				CListeObjetsDonnees lstEntrees = new CListeObjetsDonnees ( contexteTmp, typeof(CEntreeLogSynchronisation));
				lstEntrees.Filtre = new CFiltreData ( CSc2iDataConst.c_champIdSynchro+">=@1", nIdSynchroDebut);
				foreach ( CEntreeLogSynchronisation entree in lstEntrees )
				{
					if ( entree.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd )
					{
						DataTable tableSource = contexteTmp.GetTableSafe ( entree.TableConcernee );
						CObjetDonneeAIdNumeriqueAuto obj = (CObjetDonneeAIdNumeriqueAuto)contexteTmp.GetNewObjetForTable ( tableSource );
						if ( obj.ReadIfExists(entree.IdElement) )
						{
							tableRowAdd[entree.TableConcernee+"_"+entree.IdElement] = true;
							bHasData = true;
							DataTable tableDest = dsDest.Tables[tableSource.TableName];
							if ( tableDest == null )
								CUtilDataSet.Merge ( tableSource, dsDest, true );
							else
							{
								DataRow row = tableDest.Rows.Find ( entree.IdElement );
								if ( row == null )
								{
									row = tableDest.NewRow();
									CUtilDataSet.CopyRow ( obj.Row.Row, row, DataRowVersion.Original, true );
									tableDest.Rows.Add ( row );
								}
							}
						}
					}
					else
					{
						if ( tableRowAdd[entree.TableConcernee+"_"+entree.IdElement] != null )
						{
							listRowToDelete.Add ( entree.Row );
							listRowToDelete.Add ( tableRowAdd[entree.TableConcernee+"_"+entree.IdElement] );
							bHasData = true;
						}
					}
				}
				foreach ( DataRow row in listRowToDelete )
				{
					row.Delete();
				}
				CUtilDataSet.Merge ( 
					contexteTmp.GetTableSafe ( CEntreeLogSynchronisation.c_nomTable ),
					dsDest, 
					true );
			}
				
			return result;
		}

		////////////////////////////////////////////////////////////////////////
		/// //////////////////////////////////////////
		///<summary>
		///Charge les données de la table qui vont devoir être mise à jour
		///à partir des données modifiées dans la table source
		///</summary>
		public CResultAErreur ChargeDonneesAMettreAJour ( CContexteDonnee contexteDest, DataTable tableSource )
		{
			CResultAErreur result = CResultAErreur.True;

			/*ArrayList lstTables = CContexteDonnee.GetTablesOrderInsert ( donneesSources );

			foreach ( DataTable table in lstTables )*/
			{
				DataTable tableDest = null;
				//S'assure que la table est bien chargée
				try
				{
					tableDest = contexteDest.GetTableSafe ( tableSource.TableName );
				}
				catch
				{
					//La table n'existe pas
				}
				if ( tableDest != null  && CContexteDonnee.MappeurTableToClass.IsSynchronisable(tableSource.TableName) && tableSource.Rows.Count != 0)
				{
					IObjetServeur serveur = CContexteDonnee.GetTableLoader(tableDest.TableName, null, contexteDest.IdSession);
					if ( serveur.CountRecords(tableDest.TableName, new CFiltreData("1=1")) != 0 )//Première maj : copie complète
					{
						string strColPrim = tableSource.PrimaryKey[0].ColumnName;
						string strFiltre = "";
						foreach ( DataRow row in tableSource.Rows )
							strFiltre += row[strColPrim].ToString()+",";
						if ( strFiltre.Length != 0 )
						{
							//Supprime la dernière virgule;
							strFiltre = strFiltre.Substring(0, strFiltre.Length-1);
							strFiltre = strColPrim += " in ("+strFiltre+")";
							IObjetServeur loader = contexteDest.GetTableLoader(tableSource.TableName);
							DataTable tableNew = loader.Read(new CFiltreData(strFiltre));
							if ( tableNew ==  null )
							{
								result.EmpileErreur(I.T("Error while reading table @1|135",tableSource.TableName));
								return result;
							}
							contexteDest.IntegreTable ( tableNew, false );
						}
					}
				}
			}
			/*Charge les éléments à supprimer
			CContexteDonnee contexteForListe = new CContexteDonnee ( contexteDest.IdSession, true, false );
			DataTable tableSync = donneesSources.Tables[CEntreeLogSynchronisation.c_nomTable];
			DataTable tableCopie = CUtilDataSet.AddTableCopie ( donneesSources.Tables[CEntreeLogSynchronisation.c_nomTable], contexteForListe );
			foreach ( DataRow row in tableSync.Rows )
				tableCopie.ImportRow ( row );
			CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteForListe, typeof(CEntreeLogSynchronisation));
			liste.InterditLectureInDB = true;
			liste.Filtre = new CFiltreData(CEntreeLogSynchronisation.c_champType+"=@1",(int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete );
			foreach ( CEntreeLogSynchronisation entree in liste )
			{
				DataTable table = contexteDest.GetTableSafe ( entree.TableConcernee );
				CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)contexteDest.GetNewObjetForTable ( table );
				objet.ReadIfExists(entree.IdElement);
			}*/
			return result;
		}

		///////////////////////////////////////////////////////////////////////////////
		protected ICollection GetListeRowsOrdreUpdate( DataTable table )
		{
			ArrayList listeRelationsAutoReference = new ArrayList();
			foreach ( DataRelation relation in table.ParentRelations )
			{
				if ( relation.ParentTable == table &&
					relation.ChildTable == table )
					listeRelationsAutoReference.Add ( relation );
			}
			if ( listeRelationsAutoReference.Count == 0 )
				return table.Rows;
			//Trie les éléments pour modifs et ajouts
			//Il faut modifier en premier les lignes qui sont dépendantes des autres
			/*Pour trier : On regarde  les fils de chaque ligne à modifier. On place
			 * Chaque ligne après ses fils dans la liste. A la fin, on inverse la liste
			 * */
			ArrayList listeLignes = new ArrayList();
			foreach ( DataRow row in table.Rows )
			{
				Hashtable tableFilles = new Hashtable();
				foreach ( DataRelation relation in listeRelationsAutoReference )
				{
					foreach ( DataRow rowFille in row.GetChildRows ( relation ) )
						tableFilles[rowFille] = true;
				}
				int nPosInsert = 0;
				for ( int nLook = 0; nLook < listeLignes.Count && tableFilles.Count > 0; nLook++ )
				{
					if ( tableFilles[listeLignes[nLook]] != null )
					{
						nPosInsert = nLook+1;
						tableFilles.Remove(listeLignes[nLook]);
					}
				}
				listeLignes.Insert ( nPosInsert, row );
			}
			listeLignes.Reverse();
			return listeLignes;
		}

		///////////////////////////////////////////////////////////////////////////////
		public CResultAErreur AjouteNouveaux ( 
			int nIdProcessSynchronisation,
			CContexteDonnee ctxDestination, 
			DataTable tableSource, 
			ISynchroniseurBddSecondaire synchroniseur )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( CContexteDonnee.MappeurTableToClass.IsSynchronisable(tableSource.TableName) && tableSource.Rows.Count > 0 )
			{
				bool bIsTableAutoReferencee = false;
				string strPrimKey = tableSource.PrimaryKey[0].ColumnName;
				DataTable tableDest = ctxDestination.Tables[tableSource.TableName];
				if ( tableDest != null )
				{
					bool bIsDestEmpty = tableDest.Rows.Count == 0;
					Type tp = CContexteDonnee.GetTypeForTable ( tableSource.TableName );
					if ( typeof(IObjetDonneeAutoReference).IsAssignableFrom( tp ))
					{
						bIsTableAutoReferencee = true;
						bIsDestEmpty = false;
					}
					//Commence par ajouter les éléments à updater qui ne sont pas là
					int nRow = 0;
					ICollection listeRows = GetListeRowsOrdreUpdate(tableSource);
					foreach ( DataRow rowSource in listeRows )
					{
						nRow++;
						object obj = rowSource[CSc2iDataConst.c_champIdSynchro];
						bool bShouldCreate = bIsDestEmpty;
						if ( !bShouldCreate )
						{
							DataRow newRow = tableDest.Rows.Find ( rowSource[strPrimKey] );
							bShouldCreate = newRow == null;
								
							if ( bIsTableAutoReferencee && newRow != null )
							{
								if ( ctxDestination.IsToRead ( newRow ) )
									//Si autoréférencé, une nouvelle ligne peut être créée automatiquement
									//par une de ses lignes filles.
									//Il peut s'agir d'une nouvelle ligne ou d'un ligne existante,
									//Il faut donc vérifier!
									tableDest.Rows.Remove ( newRow );
								CObjetDonnee objet = ctxDestination.GetNewObjetForTable ( tableDest );
								if ( !objet.ReadIfExists ( new object[]{rowSource[strPrimKey]} ) )
								{
									bShouldCreate = true;
								}
							}
						}
						if ( bShouldCreate )
						{
							DataRow newRow = CopieToNew ( ctxDestination, rowSource, tableDest );
							m_tableAjouts[tableDest.TableName+"_"+newRow[tableDest.PrimaryKey[0]]] = "";
							if ( AfterRecupElementFromMain != null )
							{
								result = AfterRecupElementFromMain ( 
									nIdProcessSynchronisation,
									ctxDestination.GetNewObjetForRow ( newRow ), 
									synchroniseur );
								if ( !result )
									return result;
							}
						}						
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////
		private DataRow  CopieToNew ( CContexteDonnee contexteDest, DataRow rowSource, DataTable tableDest )
		{
			string strPrim = tableDest.PrimaryKey[0].ColumnName;
			DataRow row = tableDest.NewRow();
			contexteDest.CopyRow ( rowSource, row );
			row[strPrim] = rowSource[strPrim];
			/*
						foreach ( DataRelation parentRelation in rowSource.Table.ParentRelations )
						{
							DataRow rowParent = rowSource.GetParentRow(parentRelation);
							if ( rowParent != null )
							{
								DataRow newRowParent = (DataRow)m_mapOldRowToNewRow[rowParent];
								if ( newRowParent != null )
								{
									string strChampFils, strChampParent;
									strChampFils = parentRelation.ChildColumns[0].ColumnName;
									strChampParent = parentRelation.ParentColumns[0].ColumnName;
									row[strChampFils] = newRowParent[strChampParent];
								}
							}
						}*/
			tableDest.Rows.Add ( row );
			return row;
		}

		/// //////////////////////////////////////////
		public CResultAErreur Update ( 
			int nIdProcessSynchronisation,
			CContexteDonnee ctxDestination, 
			DataTable tableSource, 
			ISynchroniseurBddSecondaire synchroniseur )
		{
			CResultAErreur result = CResultAErreur.True;
			//Met à jour les tables;
			if ( CContexteDonnee.MappeurTableToClass.IsSynchronisable(tableSource.TableName) )
			{
				DataTable tableDest = ctxDestination.Tables[tableSource.TableName];
				if ( tableDest != null )
				{
					string strPrimKey = tableDest.PrimaryKey[0].ColumnName;
				
					foreach ( DataRow rowSource in GetListeRowsOrdreUpdate ( tableSource ) )
					{
						if ( m_tableAjouts[tableSource.TableName+"_"+rowSource[strPrimKey].ToString()] == null )
							//Ce n'est pas un élément ajouté
						{
							DataRow rowDest = tableDest.Rows.Find ( rowSource[strPrimKey] );
							if ( rowDest != null )
							{
								object syncSource, syncDest;
								syncSource = rowSource[CSc2iDataConst.c_champIdSynchro];
								syncDest = rowDest[CSc2iDataConst.c_champIdSynchro];
								if ( syncSource == DBNull.Value || syncDest == DBNull.Value || (int)syncSource!=(int)syncDest  )
								{
									ctxDestination.CopyRow ( rowSource, rowDest, false );
									
								}
							}
							else
							{
								//L'élément n'existe pas, il faut le créer !!!!
								DataRow newRow = CopieToNew ( ctxDestination,rowSource, tableDest );
								m_tableAjouts[tableDest.TableName+"_"+newRow[tableDest.PrimaryKey[0]]] = "";
								rowDest = newRow;
							}
							if ( AfterRecupElementFromMain != null )
							{
								result = AfterRecupElementFromMain ( 
									nIdProcessSynchronisation,
									ctxDestination.GetNewObjetForRow ( rowDest ), 
									synchroniseur );
								if ( !result )
									return result;
							}
						}
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////
		protected CResultAErreur GetBlobs ( 
			int nIdProcess, 
			CContexteDonnee contexteDest, 
			DataTable tableSource, 
			ISynchroniseurBddSecondaire synchroniseur )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DataTable tableDest = contexteDest.GetTableSafe(tableSource.TableName);
				string strKey = tableDest.PrimaryKey[0].ColumnName;
				foreach ( DataColumn col in tableDest.Columns )
				{
					if ( col.DataType == typeof(CDonneeBinaireInRow) )
					{
						foreach ( DataRow row in tableDest.Rows )
						{
							if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified )
							{
								byte[] data = synchroniseur.GetBlob ( nIdProcess, tableSource.TableName, col.ColumnName, (int)row[strKey] );
								CDonneeBinaireInRow donnee = new CDonneeBinaireInRow ( contexteDest.IdSession ,row, col.ColumnName );
								donnee.Donnees = data;
								row[col.ColumnName] = donnee;
							}
						}
					}
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
				result.EmpileErreur(I.T("Error while BLOB recovering|136"));
			}
			return result;
		}




		/*/// //////////////////////////////////////////
		public CResultAErreur Delete ( CContexteDonnee ctxDestination, DataTable tableSource )
		{
			CResultAErreur result = CResultAErreur.True;

			DataTable tableEntreesLog = dsSource.Tables[CEntreeLogSynchronisation.c_nomTable];
			if ( tableEntreesLog == null )
				return result;
			foreach ( DataTable tableSource in lst )
			{
				if ( CContexteDonnee.MappeurTableToClass.IsSynchronisable(table.TableName) )
				{
					DataTable tableDest = ctxDestination.Tables[table.TableName];
					if ( tableDest != null )
					{
						DataView view = new DataView ( tableEntreesLog );
						string strFiltre = CEntreeLogSynchronisation.c_champTable+"='"+table.TableName+"'";
						view.RowFilter = strFiltre;
						string strCleMain = tableDest.PrimaryKey[0].ColumnName;
						foreach ( DataRowView row in view )
						{
							
							if ( (CEntreeLogSynchronisation.TypeModifLogSynchro)row[CEntreeLogSynchronisation.c_champType] == CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete )
							{
								DataRow[] rows = tableDest.Select (strCleMain+"="+row[CEntreeLogSynchronisation.c_champIdElement].ToString());
								if ( rows.Length == 1 )
									rows[0].Delete();
							}
						}
					}
				}
			}
			return result;
		}*/

		/// //////////////////////////////////////////
		public static void AddMapId ( DataSet ds, string strTable, int nOldId, int nNewId )
		{
			DataTable table = ds.Tables[c_nomTableChangementId];
			if ( table == null )
			{
				table = ds.Tables.Add ( c_nomTableChangementId );
				table.Columns.Add ( c_champTable, typeof(string));
				table.Columns.Add ( c_champOldId, typeof(int));
				table.Columns.Add ( c_champNewId, typeof(int));
			}
			DataRow row = table.NewRow();
			row[c_champTable] = strTable;
			row[c_champOldId] = nOldId;
			row[c_champNewId] = nNewId;
			table.Rows.Add ( row );
		}

		public event AfterRecupElementFromMainEventHandler AfterRecupElementFromMain;
		/// //////////////////////////////////////////
		/// si mode rapide, 
		/// certains éléments peuvent être ratés
		public CResultAErreur ApplyModifsFromMain ( 
			IAuthentificationSession authentificationSurServeurPrimaire,
			IDatabaseConnexionSynchronisable connexion,
			ISynchroniseurBddSecondaire synchroniseur,
			string strCodeGroupeSynchro,
			int nVersionParametreSynchronisation,
			IIndicateurProgression informateurIntegration,
			bool bModeRapide
			)
		{

			CResultAErreur result = CResultAErreur.True;
			int nIdProcess = synchroniseur.StartProcessSynchronisationMainToSecondaire ( 
				authentificationSurServeurPrimaire,
				connexion.LastSyncIdVueDeBasePrincipale,
				strCodeGroupeSynchro,
				nVersionParametreSynchronisation );
			if ( nIdProcess == -1 )
			{
				result.EmpileErreur(I.T("Error while opening of synchronization session|137"));
				return result;
			}
			
			
			if ( informateurIntegration != null )
				informateurIntegration.PushSegment ( 0, 80 );
			connexion.EnableLogSynchronisation = false;
			//ecrit tout comme étant déjà envoyé dans la base main
			connexion.LockSyncSessionLocalTo ( connexion.LastSyncIdPutInBasePrincipale );
			connexion.BeginTrans();

				

			try
			{
				string[] strTablesStoUpdate = synchroniseur.GetListeTablesConcerneesParAddOrUpdateMainToSecondaire(nIdProcess);
				if ( informateurIntegration != null )
					informateurIntegration.SetBornesSegment ( 0, strTablesStoUpdate.Length );
				int nTable = 0;
				foreach ( string strNomTable in strTablesStoUpdate )
				{
					nTable++;
					if ( informateurIntegration != null )
					{
						informateurIntegration.SetValue ( nTable );
						informateurIntegration.SetInfo (I.T("Request @1|138",strNomTable));
						if ( informateurIntegration.CancelRequest )
						{
							result.EmpileErreur(I.T("User Cancelling|139"));
							return result;
						}
					}
					int[] idsPresentes = null;
					Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
					if ( !bModeRapide && tp.GetCustomAttributes(typeof(FullTableSyncAttribute), true).Length == 0 )
						idsPresentes = GetIdsInTable ( connexion.IdSession, strNomTable, tp );
					DataTable table = synchroniseur.GetDataToAddOrUpdateMainToSecondaire ( nIdProcess, strNomTable, idsPresentes );
					if ( table == null )
					{ 
						result.EmpileErreur(I.T("Error while data recovering of the table @1|140",strNomTable));
						break;
					}
					int nNbLus = 0;
					while ( table.Rows.Count != 0 )
					{
						if ( informateurIntegration != null )
						{
							informateurIntegration.SetValue ( nTable );
							int nFin = nNbLus + table.Rows.Count;
							informateurIntegration.SetInfo(I.T("Integration @1 (@2)|143", strNomTable, nNbLus.ToString() + "-" + nFin.ToString()));
						}
						nNbLus += table.Rows.Count;
						using (CContexteDonnee ctx = new CContexteDonnee ( connexion.IdSession, true, false ) )
						{
							ctx.EnableTraitementsAvantSauvegarde = false;
							ctx.EnforceConstraints = false;
							result = ChargeDonneesAMettreAJour ( ctx, table );
							if ( ctx.Tables[table.TableName] != null )
							{
								if ( informateurIntegration != null )
									informateurIntegration.SetInfo(I.T("Adding @1|141",table.TableName));
								if ( result )
									result = AjouteNouveaux ( nIdProcess, ctx, table, synchroniseur );
								if ( informateurIntegration != null )
									informateurIntegration.SetInfo(I.T("Update @1|142",table.TableName));
								if ( result) 
									result = Update ( nIdProcess, ctx, table, synchroniseur );
								if ( result )
									result = GetBlobs ( nIdProcess, ctx, table, synchroniseur );

								/*if ( informateurIntegration != null )
										informateurIntegration.SetInfo("Supp "+table.TableName);*/
								/*if ( result )
										result = Delete ( ctx, ds );*/
								ctx.EnforceConstraints = true;
								string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(table.TableName);
								connexion.DesactiverIdAuto(strNomTableInDb, true);
								ctx.EnableTraitementsAvantSauvegarde = false;
								if ( informateurIntegration != null )
									informateurIntegration.SetInfo(I.T("Save @1|144",table.TableName));
								result = ctx.SaveAll(true);
								if ( informateurIntegration != null )
									informateurIntegration.SetInfo(I.T("Save : @1|145",(result?I.T("Succeeded|80"):result.Erreur.ToString())));
								ctx.EnableTraitementsAvantSauvegarde = true;
								connexion.DesactiverIdAuto(strNomTableInDb, false);
							}
						}
						if ( informateurIntegration != null )
							informateurIntegration.SetInfo (I.T("Request @1|138",strNomTable));
						table = synchroniseur.GetDataToAddOrUpdateMainToSecondaire ( nIdProcess, strNomTable, idsPresentes );
					}
					if ( !result )
						break;
				}

				if ( result )
				{
					//Récupère les suppressions
					if ( informateurIntegration != null )
					{
						informateurIntegration.PopSegment();
						informateurIntegration.PushSegment ( 80, 100 );
						informateurIntegration.SetBornesSegment ( 0, 100 );
						informateurIntegration.SetInfo(I.T("Deleting data recovery|146"));
					}

					result = DoDeletesFromMain ( connexion.IdSession, nIdProcess, synchroniseur, informateurIntegration );
					if ( !result )
						return result;

					if ( informateurIntegration != null )
					{
						informateurIntegration.PopSegment();
						informateurIntegration.SetValue ( 100 );
						informateurIntegration.SetInfo("");
					}
					connexion.UnlockSyncSessionLocal();
					if ( informateurIntegration != null )
						informateurIntegration.SetInfo(I.T("Changing last viewed version|147"));
					connexion.LastSyncIdVueDeBasePrincipale = synchroniseur.GetVersionVueDeBaseMain ( nIdProcess );
					if ( informateurIntegration != null )
						informateurIntegration.SetInfo(I.T("Closing of synchronization session|148"));
					synchroniseur.EndProcessSynchronisationMainToSecondaire ( nIdProcess );
				
					if ( informateurIntegration != null )
						informateurIntegration.SetInfo(I.T("Initial version modification|155"));
					connexion.SetSyncSession ( connexion.LastSyncIdVueDeBasePrincipale+1 );
				
						
				}
			}
			catch  ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				C2iEventLog.WriteErreur(I.T("Error while Main synchonizationing ->sec\r\n@1|149",e.ToString() ));
			}
			finally
			{
				if ( result )
				{
					if ( informateurIntegration != null )
						informateurIntegration.SetInfo(I.T("Transaction commiting|150"));
					result = connexion.CommitTrans();
					if ( informateurIntegration != null )
						informateurIntegration.SetInfo(I.T("End of synchronization|151"));
				}
					
				if ( !result )
				{
					connexion.UnlockSyncSessionLocal();
					connexion.RollbackTrans();
				}
				connexion.EnableLogSynchronisation = true;
				synchroniseur.EndProcessSynchronisationMainToSecondaire ( nIdProcess );
			}
		
			return result;
		}

		/// //////////////////////////////////////////
		private CResultAErreur DoDeletesFromMain ( int nIdSession, int nIdProcess, ISynchroniseurBddSecondaire synchroniseur, IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			using ( CContexteDonnee contexte = new CContexteDonnee ( nIdSession, true, false ) )
			{
				
				string[] strTablesToDelete = synchroniseur.GetListeTablesConcerneesParDeleteSynchronisationMainToSecondaire(nIdProcess);
				if ( strTablesToDelete == null )
				{
					result.EmpileErreur(I.T("Error while recovering of the tables to be removed|153"));
					return result;
				}
				if ( indicateur != null )
					indicateur.SetBornesSegment ( 0, strTablesToDelete.Length );
				int nTable = 0;
				foreach ( string strNomTable in strTablesToDelete )
				{
					nTable++;
					if ( indicateur != null )
					{
						indicateur.SetValue ( nTable );
						indicateur.SetInfo (I.T("Deleting @1|152",strNomTable ));
					}
					Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
					if ( tp != null )
					{
						int[] idsPresents = GetIdsInTable ( nIdSession, strNomTable, tp );
						int[] idsToDelete = synchroniseur.GetListeIdsToDeleteInTable ( nIdProcess, strNomTable, idsPresents );
						if ( idsToDelete == null )
							result.EmpileErreur(I.T("Error while recovering of ids to be removed|154"));
						else
						{
							foreach ( int nId in idsToDelete )
							{
								CObjetDonneeAIdNumeriqueAuto objetToDelete = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( tp, new object[]{contexte} );
								if ( objetToDelete.ReadIfExists ( nId ) )
								{
									//Pas besoin de tester la possibilité de supprimer !!
									result = objetToDelete.DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison();
									if ( !result )
										return result;
								}
							}
						}
					}
				} 
				
			}
			return result;
		}

		/// //////////////////////////////////////////
		private int[] GetIdsInTable ( int nIdSession, string strNomTable, Type tp )
		{
			CStructureTable structure = CStructureTable.GetStructure(tp);
			string strRequete = "select "+structure.ChampsId[0].NomChamp+" from "+strNomTable;
			IDataAdapter adapter = CSc2iDataServer.GetInstance().GetDatabaseConnexion ( nIdSession, tp ).GetSimpleReadAdapter ( strRequete );
			DataSet ds = new DataSet();
			adapter.Fill ( ds );
            CUtilDataAdapter.DisposeAdapter(adapter);
			ArrayList lst = new ArrayList();
			foreach ( DataRow row in ds.Tables[0].Rows )
				lst.Add ( (int)row[0] );
			return ( int[] )lst.ToArray ( typeof(int));
		}
				

		/// //////////////////////////////////////////
		public CResultAErreur SendModifsToMain ( 
			IAuthentificationSession authentificationSurServeurPrimaire,
			int nIdSession, 
			IDatabaseConnexionSynchronisable connexion,
			ISynchroniseurBddSecondaire synchroniseur,
			IIndicateurProgression indicateurProgression
			)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( indicateurProgression != null )
				indicateurProgression.SetBornesSegment ( 0, 100 );
			bool bHasData = false;
			if ( indicateurProgression != null )
				indicateurProgression.SetInfo(I.T("Reading data to be transferred|156"));
			DataSet dsModifs = new DataSet();
			if ( indicateurProgression != null )
				indicateurProgression.PushSegment ( 0, 20 );

			result = FillWithModifFromVersion ( 
				connexion, 
				dsModifs,
				connexion.LastSyncIdPutInBasePrincipale,
				ref bHasData,
				indicateurProgression );
			if ( indicateurProgression != null )
				indicateurProgression.PopSegment ( );
			if ( indicateurProgression != null )
				indicateurProgression.SetValue ( 20 );
			if ( bHasData )
			{
				if ( indicateurProgression != null )
					indicateurProgression.SetInfo (I.T("Opening synchornization session|157"));
				int nIdProcess = synchroniseur.StartProcessSynchronisationSecondaireToMain ( 
					authentificationSurServeurPrimaire,
					connexion.LastSyncIdVueDeBasePrincipale );
				if ( indicateurProgression != null )
				{
					indicateurProgression.SetValue ( 6 );
					indicateurProgression.SetInfo (I.T("Data transfer|158"));
					indicateurProgression.PushSegment ( 6, 60 );
					indicateurProgression.SetBornesSegment ( 0, 100 );
				}
				DataSet dsRetour = synchroniseur.SendAddsUpdateAndDelete ( nIdProcess, dsModifs, indicateurProgression );


				if ( indicateurProgression != null )
				{
					indicateurProgression.PopSegment();
					indicateurProgression.SetValue ( 70 );
				}
				if ( dsRetour == null )
				{
					result.EmpileErreur(I.T("Server Error|159"));
					synchroniseur.CancelProcessSynchronisationSecondaireToMain ( nIdProcess );
					return result;
				}
				connexion.BeginTrans();
				try
				{
					//5 minutes pour changer les ids et commit et tout
					synchroniseur.RenouvelleBailSecondaireToMain ( nIdProcess, 5 );
					//Crée un map des nouveaux identifiants
					result = ChangeIds ( connexion, dsRetour.Tables[c_nomTableChangementId] );

					//Envoie les blobs
					foreach ( DataTable table in dsModifs.Tables )
					{
						foreach ( DataColumn col in table.Columns )
						{
							if ( col.DataType == typeof(CDonneeBinaireInRow) )
							{
								string strKey = table.PrimaryKey[0].ColumnName;
								foreach ( DataRow rowBlob in table.Rows )
								{
									int nId = (int)rowBlob[strKey];
									//Cherche le nouvel id de l'élément
									DataView view = new DataView ( dsRetour.Tables[c_nomTableChangementId] );
									view.RowFilter = c_champOldId+"="+rowBlob[strKey].ToString()+" and "+
										c_champTable+"='"+table.TableName+"'";
									if ( view.Count > 0 )
										nId = (int)view[0][strKey];
									CDonneeBinaireInRow donnee = new CDonneeBinaireInRow( nIdSession, rowBlob, col.ColumnName);
									Byte[] bt = donnee.Donnees;
									if ( !synchroniseur.SendBlob( nIdProcess, table.TableName, col.ColumnName, nId, bt ) )
									{
										result.EmpileErreur(I.T("Error during the BLOB @1 saving of the table @2|111",col.ColumnName,table.TableName));
										return result;
									}
										
								}
							}
						}
					}
					if ( result )
						RecupereSuppressionsAnnulees ( connexion, dsRetour );
					if ( result )
						result = connexion.CommitTrans();
					else
						connexion.RollbackTrans();
					if ( result )
						synchroniseur.CommitProcessSynchronisationSecondaireToMain(nIdProcess);
					else
						synchroniseur.CancelProcessSynchronisationSecondaireToMain ( nIdProcess );
					connexion.LastSyncIdPutInBasePrincipale = connexion.IdSyncSession;
					connexion.IncrementeSyncSession();
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
				}
				finally
				{
					if ( !result )
						connexion.RollbackTrans();
				}
			}
			return result;
		}

		/// ////////////////////////////////////////////////////
		private CResultAErreur RecupereSuppressionsAnnulees ( IDatabaseConnexionSynchronisable connexion, DataSet dsAvecElementsAAjouter )
		{
			CResultAErreur result = CResultAErreur.True;
			using ( CContexteDonnee contexte = new CContexteDonnee ( connexion.IdSyncSession, true, false ) )
			{
				contexte.EnableTraitementsAvantSauvegarde = false;
				foreach ( DataTable table in CContexteDonnee.GetTablesOrderInsert ( dsAvecElementsAAjouter ) )
				{
					if ( CContexteDonnee.MappeurTableToClass.IsSynchronisable ( table.TableName ) )
						contexte.Merge ( table, true, MissingSchemaAction.Add );
				}
				//Et on sauve le tout
				foreach (DataTable table in contexte.Tables)
				{
					string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(table.TableName);
					connexion.DesactiverIdAuto(strNomTableInDb, true);
				}
				connexion.EnableLogSynchronisation = false;
				result = contexte.SaveAll(true);
				connexion.EnableLogSynchronisation = true;
				foreach (DataTable table in contexte.Tables)
				{
					string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(table.TableName);
					connexion.DesactiverIdAuto(strNomTableInDb, false);
				}
			}
			return result;
		}

		/// ////////////////////////////////////////////////////
		//Après une syncrho sec->Main, met les ids du sec sur les ids du main
		private CResultAErreur ChangeIds ( IDatabaseConnexionSynchronisable connexion, DataTable tableChangements )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( tableChangements == null )
				return result;
			using ( CContexteDonnee contexte = new CContexteDonnee(connexion.IdSession, true, false) )
			{
				//Travaille par tables
				//Nom de tables->liste de row de tableChangements
				Hashtable tableTables = new Hashtable();
				foreach ( DataRow row in tableChangements.Rows )
				{
					ArrayList lstRows = (ArrayList)tableTables[row[c_champTable]];
					if ( lstRows == null )
					{
						lstRows = new ArrayList();
						tableTables[row[c_champTable]] = lstRows;
					}
					lstRows.Add ( row );
				}

				foreach ( string strNomTable in contexte.GetTablesOrderInsert() )
				{
					if ( tableTables[strNomTable] != null )
					{
						ArrayList lst = (ArrayList)tableTables[strNomTable];
						result = ChangeIds ( connexion, (DataRow[])lst.ToArray(typeof(DataRow)));
						if ( !result )
							return result;
					}
				}
			}
			return result;
		}

		/// ////////////////////////////////////////////////////
		private CResultAErreur ChangeIds ( IDatabaseConnexion connexion, DataRow[] rowsChangements )
		{
			/*Principe de fonctionnement :
			 * rowSuivante = première row
			 * 1 Prend la row suivante ( oldId, newId )
			 * si newId existe déjà
				* supprime l'objet oldId de la base
				* crée un nouvel élément (nouvel id) avec les données
				* de l'objet oldId. enleve le datarow de rowsChangements correspondant et
				* crée un nouveau datarow de changement correspondant au nouvel élément
				*rowSuivante = la nouvelle row dont l'oldId = newId de la précédente
				* retour en 1
			 * sinon 
				* supprime oldId et crée l'objet avec nouvelId (pas de sauvegarde de base nécéssaire
			*/
				
			//Contient la liste des changements à faire
			ArrayList lst = new ArrayList ( rowsChangements );
			CResultAErreur result = CResultAErreur.True;
			
			DataRow nextRow = (DataRow)lst[0];
			string strNomTable = (string)nextRow[c_champTable];
			while ( nextRow != null )
			{
				int nOldId = (int)nextRow[c_champOldId];
				int nNewId = (int)nextRow[c_champNewId];
				if ( nOldId != nNewId )
				{
					using ( CContexteDonnee contexte = new CContexteDonnee ( connexion.IdSession, true, false ) )
					{
						contexte.EnableTraitementsAvantSauvegarde = false;
						DataTable table = contexte.GetTableSafe( strNomTable );
						string strPrimKey = table.PrimaryKey[0].ColumnName;
						CObjetDonneeAIdNumeriqueAuto oldObjet = (CObjetDonneeAIdNumeriqueAuto)contexte.GetNewObjetForTable ( table );
						if ( oldObjet.ReadIfExists ( nOldId ) )
						{
							DataRow newRow = table.NewRow();
							contexte.CopyRow ( oldObjet.Row.Row, newRow, false, strPrimKey );
							table.Rows.Add ( newRow );
							CObjetDonneeAIdNumeriqueAuto objetGenant = (CObjetDonneeAIdNumeriqueAuto)contexte.GetNewObjetForTable(table);
							bool bHasObjetGenant = false;//Indique s'il existe déjà un objet avec le nouvel id !!
							bHasObjetGenant = !objetGenant.ReadIfExists ( nNewId );
							if ( !bHasObjetGenant )
								newRow[strPrimKey] = nNewId;
							//Charge toutes les dépendances filles et les fait pointer sur le nouvel élément
							foreach ( CInfoRelation info in CContexteDonnee.GetListeRelationsTable ( strNomTable ) )
							{
								if ( info.TableParente == strNomTable )
								{
									CListeObjetsDonnees liste = oldObjet.GetDependancesListe ( info.TableFille, false, info.ChampsFille );
									foreach ( CObjetDonnee objetFils in liste )
									{
										objetFils.Row[info.ChampsFille[0]] = newRow[strPrimKey];
									}
								}
							}

							result = contexte.SaveAll(true);
							if ( !result )
								return result;
							lst.Remove ( nextRow );
							if ( bHasObjetGenant )
							{
								nOldId = (int)newRow[strPrimKey];
								nextRow[c_champOldId] = nOldId;
								lst.Add ( nextRow );
								nextRow = null;
								foreach ( DataRow row in lst )
									if ( (int)row[c_champOldId]==nNewId )
										nextRow = row;
								if ( nextRow == null )
								{
									result.EmpileErreur(I.T("Error : Element creation (id = @1)|160",nNewId.ToString()));
									return result;
								}
							}
							else if ( lst.Count > 0 )
								nextRow = (DataRow)lst[0];
						}
					}
				}
			}
			return result;
		}
				
			

			/*	
				//Pour chaque row, conserve son nouvel id
				Hashtable tableMapRowToNewId = new Hashtable();
				foreach ( DataRow row in tableChangements.Rows )
				{
					string strNomTable = (string)row[c_champTable];
					DataTable table = contexte.GetTableSafe ( strNomTable );
					string strPrimKey = table.PrimaryKey[0].ColumnName;
					//Lit la ligne pour laquelle il faut changer l'id
					int nOldId = (int)row[c_champOldId];
					CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)contexte.GetNewObjetForTable ( table );
					if ( objet.ReadIfExists ( nOldId ) )
					{
						//Crée le nouvel élément (avec un nouvel id)
						DataRow newRow = table.NewRow();
						contexte.CopyRow ( objet.Row.Row, newRow, false, strPrimKey );
						table.Rows.Add ( newRow );
						tableMapRowToNewId[newRow] = row[c_champNewId];
						//Charge toutes les dépendances filles et les fait pointer sur le nouvel élément
						foreach ( CInfoRelation info in CContexteDonnee.GetListeRelationsTable ( strNomTable ) )
						{
							if ( info.TableParente == strNomTable )
							{
								CListeObjetsDonnees liste = objet.GetDependancesListe ( info.TableFille, false, info.ChampsFille );
								foreach ( CObjetDonnee objetFils in liste )
								{
									objetFils.Row[info.ChampsFille[0]] = newRow[strPrimKey];
								}
							}
						}

						//Supprime l'objet
						objet.Delete();
					}
				}
				//voila, on a dégagé tous les anciens, il ne reste plus qu'à assigner 
				//les nouveaux ids aux nouveaux éléments
				foreach ( DataRow row in tableMapRowToNewId.Keys )
				{
					row[row.Table.PrimaryKey[0]] = tableMapRowToNewId[row];
				}
				//Et on sauve le tout
				foreach ( DataTable table in contexte.Tables )
					connexion.DesactiverIdAuto ( table.TableName, true );
				result = contexte.CommitModifsDeconnecte();
				foreach ( DataTable table in contexte.Tables )
					connexion.DesactiverIdAuto ( table.TableName, false );
			}
			return result;
		}*/

		
	}
}
