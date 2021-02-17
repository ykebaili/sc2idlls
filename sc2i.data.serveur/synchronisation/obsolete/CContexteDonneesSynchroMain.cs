using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Contexte de données contenant les données de la base Main lors d'une synchronisation
	/// </summary>
	public class CContexteDonneesSynchroMain : CContexteDonneesSynchro
	{
		private Hashtable m_mapOldRowToNewRow = new Hashtable();
		
		//Hashtable des éléments ajoutées sous la forme TABLE_ID
		private Hashtable m_tableAjouts = new Hashtable();


		/// //////////////////////////////////////////
		internal CContexteDonneesSynchroMain( )
			:base()
		{
		}

		/// //////////////////////////////////////////
		public CContexteDonneesSynchroMain (  int nIdSession, bool bEnableAutoStructure )
			:base ( nIdSession, bEnableAutoStructure )
		{
		}

		


		#region Recupération des infos du secondaire
		/// //////////////////////////////////////////
		///<summary>
		///Ajoute les nouveaux éléments à partir du domaine Secondaire
		///</summary>
		public CResultAErreur AjouteNouveaux ( CContexteDonneesSynchroSecondaire donneesSecondaires )
		{
			CResultAErreur result = CResultAErreur.True;
			ArrayList lst = donneesSecondaires.GetTablesOrderInsert();
			DataTable tableEntreesLog = donneesSecondaires.GetTableSafe(CEntreeLogSynchronisation.c_nomTable);
			foreach ( DataTable table in lst )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(table.TableName) )
				{
					//int nIdMaxInSecondaire = (int)table.ExtendedProperties[CContexteDonneesSynchroSecondaire.c_extMaxIdInBase];
					//int nIdMaxInMain = (int)table.ExtendedProperties[CContexteDonneesSynchroSecondaire.c_extMaxIdInBase];
					int nNextId = 0;//Math.Max(nIdMaxInMain, nIdMaxInSecondaire)+1;
					DataTable tableDest = Tables[table.TableName];
					DataView view = new DataView ( tableEntreesLog );
					string strFiltre = CEntreeLogSynchronisation.c_champTable+"='"+table.TableName+"'";
					view.RowFilter = strFiltre;
					foreach ( DataRowView row in view )
					{
						CEntreeLogSynchronisation entree = new CEntreeLogSynchronisation(row.Row);
						if ( entree.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd )
						{
							CObjetDonneeAIdNumerique obj = entree.GetObjet();
							DataRow newRow = CopieToNew ( obj.Row, tableDest, nNextId );
							nNextId++;
							m_mapOldRowToNewRow[obj.Row.Row] = newRow;
							m_tableAjouts[table.TableName+"_"+obj.Id] = "";
						}
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////
		private DataRow  CopieToNew ( DataRow rowSecondaire, DataTable tableDest, int nNewId )
		{
			string strPrim = tableDest.PrimaryKey[0].ColumnName;
			DataRow row = tableDest.NewRow();
			CopyRow ( rowSecondaire, row, strPrim );
			//row[strPrim] = nNewId;
			foreach ( DataRelation parentRelation in rowSecondaire.Table.ParentRelations )
			{
				DataRow rowParent = rowSecondaire.GetParentRow(parentRelation);
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
			}
			tableDest.Rows.Add ( row );
			return row;
		}


		/// //////////////////////////////////////////
		public CResultAErreur Update ( CContexteDonneesSynchroSecondaire donneesSecondaires )
		{
			CResultAErreur result = CResultAErreur.True;
			//Met à jour les tables;
			foreach ( DataTable tableSecondaire in donneesSecondaires.Tables )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(tableSecondaire.TableName) )
				{
					DataTable tableDest = Tables[tableSecondaire.TableName];
					string strPrimKey = tableDest.PrimaryKey[0].ColumnName;
				
					foreach ( DataRow rowSecondaire in tableSecondaire.Rows )
					{
						object obj = rowSecondaire[CSc2iDataConst.c_champIdSynchro];
						if ( m_tableAjouts[tableSecondaire.TableName+"_"+rowSecondaire[strPrimKey].ToString()] == null )
							//Ce n'est pas un élément ajouté
						{
							if ( obj is int && ((int)obj)>= donneesSecondaires.IdSynchro )
							{
								DataRow rowToUpdate = tableDest.Rows.Find ( rowSecondaire[strPrimKey] );
								if ( rowToUpdate != null )
								{
									DataRow rowDest = rowToUpdate;
									CopyRow ( rowSecondaire, rowDest, false );
								}
							}
						}
					}
				}
			}
			return result;
		}

		/// //////////////////////////////////////////
		public CResultAErreur Delete ( CContexteDonneesSynchroSecondaire donneesSecondaires )
		{
			CResultAErreur result = CResultAErreur.True;
			ArrayList lst = donneesSecondaires.GetTablesOrderDelete();

			DataTable tableEntreesLog = donneesSecondaires.GetTableSafe(CEntreeLogSynchronisation.c_nomTable);
			foreach ( DataTable table in lst )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(table.TableName) )
				{
					DataTable tableDest = Tables[table.TableName];
					DataView view = new DataView ( tableEntreesLog );
					string strFiltre = CEntreeLogSynchronisation.c_champTable+"='"+table.TableName+"'";
					view.RowFilter = strFiltre;
					string strCleMain = tableDest.PrimaryKey[0].ColumnName;
					foreach ( DataRowView row in view )
					{
						CEntreeLogSynchronisation entree = new CEntreeLogSynchronisation(row.Row);
						if ( entree.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete )
						{
							DataRow rowDest = tableDest.Rows.Find ( entree.IdElement);
							if ( rowDest != null )
								rowDest.Delete();
						}
					}
				}
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////////////////////////////////
		public CResultAErreur WriteChanges(CContexteDonneesSynchroSecondaire donneesSecondaires)
		{
			CResultAErreur result = base.SaveAll(true);
			if ( !result )
				return result;


			//Crée une table des conversions d'id par table
			Hashtable mapTablesOldToNew = new Hashtable();
			Hashtable mapTablesNewToOld = new Hashtable();
			Hashtable mapMaxParTable = new Hashtable();
			FillMapsId ( mapTablesOldToNew, mapTablesNewToOld, mapMaxParTable );
			
			ChangeOldIdParNew ( donneesSecondaires, mapTablesOldToNew, mapTablesNewToOld, mapMaxParTable );
			
			return result;
		}

		/// ////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Remplit une hashtable par table avec le map des ids old to new, new to old, et l'indice
		/// max utilisé par table.
		/// </summary>
		/// <param name="mapTableOldToNew"></param>
		/// <param name="mapTableNewToOld"></param>
		/// <param name="mapMaxParTable"></param>
		private void FillMapsId ( Hashtable mapTablesOldToNew, Hashtable mapTablesNewToOld, Hashtable mapMaxParTable )
		{
			foreach ( DataRow row in m_mapOldRowToNewRow.Keys )
			{
				DataRow newRow = (DataRow)m_mapOldRowToNewRow[row];
				Hashtable tableOldToNew = (Hashtable)mapTablesOldToNew[row.Table.TableName];
				string strPrim = newRow.Table.PrimaryKey[0].ColumnName;
				if ( tableOldToNew == null )
				{
					tableOldToNew = new Hashtable();
					mapTablesOldToNew[row.Table.TableName] = tableOldToNew;

					//Identifie l'id max utilisée dans la table
					DataView view = new DataView ( row.Table );
					view.Sort = strPrim+" desc";
					int nMax = (int)view[0][strPrim];
					view = new DataView(newRow.Table);
					view.Sort = strPrim+" desc";
					nMax = Math.Max ( nMax, (int)view[0][strPrim] );
					mapMaxParTable[row.Table.TableName] = nMax;
				}
				tableOldToNew[row] = newRow;
			}
		}

		/// ////////////////////////////////////////////////////////////////////////////////////
		private void ChangeOldIdParNew ( CContexteDonneesSynchroSecondaire donneesSecondaires, Hashtable mapTablesOldToNew, Hashtable mapTablesNewToOld, Hashtable mapMaxParTable )
		{
			ArrayList lst = donneesSecondaires.GetTablesOrderInsert();
			CListeObjetsDonnees listeEntreesLog = new CListeObjetsDonnees(donneesSecondaires, typeof(CEntreeLogSynchronisation));
			listeEntreesLog.InterditLectureInDB = true;
			foreach ( DataTable table in lst )
			{
				string strTable = table.TableName;
				if ( mapMaxParTable[strTable] != null )
				{
					//DataTable table = donneesSecondaires.Tables[strTable];
					string strPrim = table.PrimaryKey[0].ColumnName;
					int nIndice = (int)mapMaxParTable[strTable]+1;
					Hashtable tableOldToNew = (Hashtable)mapTablesOldToNew[strTable];
					Hashtable tableIdOriginauxDansSecondaire = new Hashtable();
					
					//Commence par renumeroter les indices des nouveaux éléments
					//pour être sur de ne pas affecter à un élément un id déjà existant
					foreach ( DataRow row in tableOldToNew.Keys )
					{
						tableIdOriginauxDansSecondaire[row] = row[strPrim];
						row[strPrim] = nIndice;
						nIndice++;
					}

					foreach ( DataRow row in tableOldToNew.Keys )
					{
						DataRow newRowPrim = (DataRow)tableOldToNew[row];
						int nNewId = (int)newRowPrim[strPrim];
						int nIdOriginalDansSecondaire = (int)tableIdOriginauxDansSecondaire[row];
						if ( nNewId != nIdOriginalDansSecondaire)
							/*L'élement a changé d'id. On va donc en créer
							 * un nouveau avec le nouvel id, modifier les liens fils
							 * et supprimer l'ancien
							 * */
						{					
							//Crée la nouvelle ligne (avec le bon id) (copie de la ligne d'origine)
							DataRow newRowSec = table.NewRow();
							CopyRow ( newRowPrim, newRowSec );
							table.Rows.Add ( newRowSec );

							//Change l'info de log synchro
							listeEntreesLog.Filtre = new CFiltreData(
								CEntreeLogSynchronisation.c_champTable+"=@1 and "+
								CEntreeLogSynchronisation.c_champIdElement+"=@2 and "+
								CEntreeLogSynchronisation.c_champType+"=@3",
								strTable, 
								nIdOriginalDansSecondaire,
								(int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd);
							if ( listeEntreesLog.Count > 0 )
							{
								CEntreeLogSynchronisation entree = (CEntreeLogSynchronisation)listeEntreesLog[0];
								entree.IdElement = nNewId;
							}


							//Change toutes les liaisons filles sur le nouvel élément
							foreach ( DataRelation rel in table.ChildRelations )
							{
								if ( rel.ParentColumns.Length == 1  && rel.ParentColumns[0].ColumnName == strPrim )
								{
									foreach ( DataRow childRow in row.GetChildRows(rel) )
										childRow[rel.ChildColumns[0].ColumnName] = nNewId;
								}
							}
							//Supprime l'ancien élément
							row.Delete();
						}
						else
							row[strPrim] = nIdOriginalDansSecondaire;
					}
				}
			}
		}
		#endregion
		

	}
}
