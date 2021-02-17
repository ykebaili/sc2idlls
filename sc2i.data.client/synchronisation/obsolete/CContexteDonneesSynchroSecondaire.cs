using System;
using System.Collections;
using System.Data;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Contexte de données utilisé pour contenir les données
	/// de la base Secondaire à synchroniser avec la base primaire
	/// </summary>
	public class CContexteDonneesSynchroSecondaire : CContexteDonneesSynchro
	{
		private Hashtable m_mapOldRowToNewRow = new Hashtable();
		
		//Hashtable des éléments ajoutées sous la forme TABLE_ID
		private Hashtable m_tableAjouts = new Hashtable();

		public CContexteDonneesSynchroSecondaire( int nIdSession, bool bAutoStructure )
			:base (nIdSession, bAutoStructure)
		{
		}

		////////////////////////////////////////////////////////////////////////
		public CContexteDonneesSynchroSecondaire ()
			:base()
		{
		}

		///////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////
		public override string GetContexteDonneeServeurURI
		{
			get
			{
				return "CContexteDonneeSynchroSecondaireServeur";
			}
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
		public CResultAErreur AjouteNouveaux ( CContexteDonneesSynchro donneesSources )
		{
			CResultAErreur result = CResultAErreur.True;
			ArrayList lst = donneesSources.GetTablesOrderInsert();
			DataTable tableEntreesLog = donneesSources.GetTableSafe(CEntreeLogSynchronisation.c_nomTable);
			foreach ( DataTable tableSource in lst )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(tableSource.TableName) && tableSource.Rows.Count > 0 )
				{
					string strPrimKey = tableSource.PrimaryKey[0].ColumnName;
					DataTable tableDest = GetTableSafe(tableSource.TableName);
					if ( tableDest != null )
					{
						bool bIsDestEmpty = tableDest.Rows.Count == 0;
						//Commence par ajouter les éléments à updater qui ne sont pas là
						//Normallement, il ne devrait pas y en avoir, mais sait-on jamais
						int nRow = 0;
						ICollection listeRows = GetListeRowsOrdreUpdate(tableSource);
						foreach ( DataRow rowSource in listeRows )
						{
							nRow++;
							object obj = rowSource[CSc2iDataConst.c_champIdSynchro];
							if ( m_tableAjouts[tableSource.TableName+"_"+rowSource[strPrimKey].ToString()] == null )
								//Ce n'est pas un élément ajouté
							{
								if ( obj == DBNull.Value || obj is int && ((int)obj)>= donneesSources.IdSynchro )
								{
									bool bShouldCreate = bIsDestEmpty;
									if ( !bShouldCreate )
									{
										DataRow rowToUpdate = tableDest.Rows.Find(rowSource[strPrimKey]);
										bShouldCreate = rowToUpdate != null;
									}
									if ( bShouldCreate )
									{
										//L'élément n'existe pas, il faut le créer !!!!
										DataRow newRow = CopieToNew ( rowSource, tableDest );
										m_mapOldRowToNewRow[rowSource] = newRow;
										m_tableAjouts[tableDest.TableName+"_"+newRow[tableDest.PrimaryKey[0]]] = "";
									}
								}
							}
						}
						/*string strPrim = tableDest.PrimaryKey[0].ColumnName;
						DataView view = new DataView ( tableEntreesLog );
						string strFiltre = CEntreeLogSynchronisation.c_champTable+"='"+table.TableName+"'";
						view.RowFilter = strFiltre;
						foreach ( DataRowView row in view )
						{
							CEntreeLogSynchronisation entree = new CEntreeLogSynchronisation(row.Row);
							if ( entree.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd )
							{
								//Vérifie que l'objet n'éxiste pas déjà
								if ( tableDest.Select(strPrim+"="+entree.IdElement).Length== 0 )
								{
									CObjetDonneeAIdNumeriqueAuto obj = entree.GetObjet();
									DataRow newRow = CopieToNew ( obj.Row, tableDest );
									m_mapOldRowToNewRow[obj.Row] = newRow;
									m_tableAjouts[table.TableName+"_"+obj.Id] = "";
								}
							}
						}*/
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////
		private DataRow  CopieToNew ( DataRow rowSource, DataTable tableDest )
		{
			string strPrim = tableDest.PrimaryKey[0].ColumnName;
			DataRow row = tableDest.NewRow();
			CopyRow ( rowSource, row );
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
		public CResultAErreur Update ( CContexteDonneesSynchro donneesSources )
		{
			CResultAErreur result = CResultAErreur.True;
			//Met à jour les tables;
			foreach ( DataTable tableSecondaire in donneesSources.Tables )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(tableSecondaire.TableName) )
				{
					DataTable tableDest = Tables[tableSecondaire.TableName];
					if ( tableDest != null )
					{
						string strPrimKey = tableDest.PrimaryKey[0].ColumnName;
				
						foreach ( DataRow rowSource in GetListeRowsOrdreUpdate ( tableSecondaire ) )
						{
							object obj = rowSource[CSc2iDataConst.c_champIdSynchro];
							if ( m_tableAjouts[tableSecondaire.TableName+"_"+rowSource[strPrimKey].ToString()] == null )
								//Ce n'est pas un élément ajouté
							{
								if ( obj is int && ((int)obj)>= donneesSources.IdSynchro )
								{
									DataRow rowToUpdate = tableDest.Rows.Find(rowSource[strPrimKey]);
									if ( rowToUpdate != null )
									{
										DataRow rowDest = rowToUpdate;
										object syncSource, syncDest;
										syncSource = rowSource[CSc2iDataConst.c_champIdSynchro];
										syncDest = rowDest[CSc2iDataConst.c_champIdSynchro];
										if ( syncSource == null || syncDest == null || (int)syncSource!=(int)syncDest  )
											CopyRow ( rowSource, rowDest, false );
									}
									else
									{
										//L'élément n'existe pas, il faut le créer !!!!
										DataRow newRow = CopieToNew ( rowSource, tableDest );
										m_mapOldRowToNewRow[rowSource] = newRow;
										m_tableAjouts[tableDest.TableName+"_"+newRow[tableDest.PrimaryKey[0]]] = "";
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////
		public CResultAErreur Delete ( CContexteDonneesSynchro donneesSources )
		{
			CResultAErreur result = CResultAErreur.True;
			ArrayList lst = donneesSources.GetTablesOrderDelete();

			DataTable tableEntreesLog = donneesSources.GetTableSafe(CEntreeLogSynchronisation.c_nomTable);
			foreach ( DataTable table in lst )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(table.TableName) )
				{
					DataTable tableDest = Tables[table.TableName];
					if ( tableDest != null )
					{
						DataView view = new DataView ( tableEntreesLog );
						string strFiltre = CEntreeLogSynchronisation.c_champTable+"='"+table.TableName+"'";
						view.RowFilter = strFiltre;
						foreach ( DataRowView row in view )
						{
							CEntreeLogSynchronisation entree = new CEntreeLogSynchronisation(row.Row);
							if ( entree.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete )
							{
								DataRow rowDest = tableDest.Rows.Find(entree.IdElement);
								if ( rowDest != null )
									rowDest.Delete();
							}
						}
					}
				}
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////////////////////////////////
		public CResultAErreur WriteChanges(CContexteDonneesSynchro donneesSources)
		{
			CResultAErreur result = base.SaveAll(true);
			if ( !result )
				return result;
			
			return result;
		}

		/// ////////////////////////////////////////////////////////////////////////////////////
		///Retourne une copie du contexte
		public override CContexteDonnee GetCompleteChanges ( DataRowState state )
		{
			/*CContexteDonnee contexte = new CContexteDonnee(IdSession,true, false);*/
			CContexteDonneesSynchroSecondaire contexte = (CContexteDonneesSynchroSecondaire)Clone();
			contexte.EnforceConstraints = false;
			contexte.SetEnableAutoStructure ( true );
			contexte.CanReceiveNotifications = false;
			foreach( DataTable table in GetTablesOrderInsert() )
			{
				contexte.Merge (table, false, MissingSchemaAction.Add);
			}
			contexte.EnforceConstraints = true;
			return contexte;
		}

	



	}
}
