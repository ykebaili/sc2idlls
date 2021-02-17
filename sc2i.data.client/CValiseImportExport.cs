using System;
using System.Data;

using sc2i.common;
using System.Collections;

namespace sc2i.data
{
	public interface IInterfaceImportObjetDonnee
	{
		CResultAErreur MapObjet ( 
			DataRow rowAMapper,
			CListeObjetsDonnees listePossibles,
			ref CObjetDonneeAIdNumeriqueAuto objetEnRetour );
	}
	/// <summary>
	/// Permet de sauvegarder un élément sur le disque et de le restaurer sur une autre base
	/// </summary>
	public class CValiseImportExport
	{
		private const string c_colIsMappe = "MAP_ID3709";

		/// ///////////////////////////////////////////////////////////////
		public CValiseImportExport( )
		{
		}

		/// ///////////////////////////////////////////////////////////////
		private CResultAErreur ReadObjetEtDependances ( CObjetDonneeAIdNumeriqueAuto source )
		{
			CResultAErreur result = CResultAErreur.True;
			
			//Charge tous les parents et tous les fils
			foreach ( CInfoRelation relation in CContexteDonnee.GetListeRelationsTable ( source.GetNomTable() ))
			{
				//Relation parente
				if(  relation.TableFille == source.GetNomTable() )
				{
					CObjetDonneeAIdNumeriqueAuto parent = (CObjetDonneeAIdNumeriqueAuto)source.GetParent ( relation.ChampsFille[0], 
						CContexteDonnee.GetTypeForTable ( relation.TableParente ));
					if ( parent != null )
						parent.AssureData();
				}
				else
				{
						
					if ( relation.Composition )
					{
						CListeObjetsDonnees liste =  source.GetDependancesListe ( relation.TableFille, relation.ChampsFille[0] );
						foreach ( CObjetDonneeAIdNumeriqueAuto objetFils in liste )
						{
							objetFils.AssureData();
							ReadObjetEtDependances ( objetFils );
						}
					}
				}
			}
			return result;
		}



		/// ///////////////////////////////////////////////////////////////
		public CResultAErreur CreateValise ( CObjetDonneeAIdNumeriqueAuto objet, DataSet ds )
		{
			int nIdSession = objet.ContexteDonnee.IdSession;
			CResultAErreur result = CResultAErreur.True;
			using ( CContexteDonnee contextePourChargementComplet = new CContexteDonnee( nIdSession, true, false ) )
			{
				result = contextePourChargementComplet.SetVersionDeTravail(objet.ContexteDonnee.IdVersionDeTravail, true);
				if (!result)
					return result;
				CObjetDonneeAIdNumeriqueAuto source = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( objet.GetType(), new object[]{contextePourChargementComplet});
				if ( !source.ReadIfExists ( objet.Id ) )
				{
					result.EmpileErreur(I.T("Impossible to read the object @1 again|173",objet.Id.ToString() ));
					return result;
				}
				ReadObjetEtDependances ( source );

				//Toutes les données sont lues, il n'y a plus qu'à les copier dans le dataset valise
				foreach (DataTable tableSource in contextePourChargementComplet.Tables)
				{
					ds.Merge(tableSource);
					//Crée les colonnes byte[] en place des CDonneeBinaireInRow
					DataTable newTable = ds.Tables[tableSource.TableName];
					if (tableSource.PrimaryKey.Length != 0)
					{
						foreach (DataColumn col in tableSource.Columns)
						{
							if (col.DataType == typeof(CDonneeBinaireInRow))
							{
								newTable.Columns.Remove(col.ColumnName);
								DataColumn newCol = new DataColumn(col.ColumnName, typeof(string));
								newCol.AllowDBNull = true;
								newTable.Columns.Add(col.ColumnName);
								foreach (DataRow rowSource in tableSource.Rows)
								{
									ArrayList keys = new ArrayList();
									foreach (DataColumn primKey in tableSource.PrimaryKey)
										keys.Add(rowSource[primKey]);
									DataRow rowDest = newTable.Rows.Find(keys.ToArray());
									if (rowDest != null)
									{
										CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(objet.ContexteDonnee.IdSession, rowSource, col.ColumnName);
										byte[] dataBinaire = donnee.Donnees;
										CStringSerializer serializer = new CStringSerializer(ModeSerialisation.Ecriture);
										serializer.TraiteByteArray(ref dataBinaire);
										rowDest[col.ColumnName] = serializer.String;
									}
								}
							}
						}
					}
				}

								
				foreach ( DataRelation relation in contextePourChargementComplet.Relations )
				{
					DataColumn colParente, colFille;
					colParente = ds.Tables[relation.ParentTable.TableName].Columns[relation.ParentColumns[0].ColumnName];
					colFille = ds.Tables[relation.ChildTable.TableName].Columns[relation.ChildColumns[0].ColumnName];
					DataRelation rel = ds.Relations.Add ( 
						relation.RelationName,
						colParente,
						colFille, 
						true);
					rel.ChildKeyConstraint.UpdateRule = Rule.Cascade;
				}

			}
			return result;
		}

		

		/// ///////////////////////////////////////////////////////////////

		/// <summary>
		/// Remplit l'objet à importer avec les données du XML.
		/// LA fonction ne fait pas de sauvegarde, et ne s'occupe pas du mode d'édition.
		/// C'est l'appelant qui doit s'en charger
		/// </summary>
		/// <param name="strNomFichier"></param>
		/// <param name="nIdSession"></param>
		/// <param name="objetAImporter"></param>
		/// <returns></returns>
		public CResultAErreur ImportXml ( 
			DataSet ds,
			int nIdSession, 
			CObjetDonneeAIdNumeriqueAuto objetAImporter,
			IInterfaceImportObjetDonnee interfaceImport )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DataTable tableSource = ds.Tables[objetAImporter.GetNomTable()];
				if ( tableSource == null || tableSource.Rows.Count == 0 )
				{
					result.EmpileErreur(I.T("The import file does not contain the table @1|174",objetAImporter.GetNomTable()));
					return result;
				}

				foreach ( DataTable table in ds.Tables )
				{
					DataColumn col = new DataColumn ( c_colIsMappe, typeof(bool) );
					col.DefaultValue = false;
					table.Columns.Add ( col );
				}	
			
				ReadObjetEtDependances ( objetAImporter );

				DataRow rowSource = tableSource.Rows[0];
				result = ImporteObjet ( rowSource, objetAImporter, interfaceImport );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e) );
			}
			return result;
		}

		/// ///////////////////////////// ///////////////////////////// //////////////////////////
		/// <summary>
		
		/// </summary>
		/// <param name="rowSource"></param>
		/// <param name="objetDest"></param>
		/// <param name="interfaceMapping"></param>
		/// <returns></returns>
		private CResultAErreur ImporteObjet ( DataRow rowSource, CObjetDonneeAIdNumeriqueAuto objetDest, IInterfaceImportObjetDonnee interfaceMapping )
		{
			CResultAErreur result = CResultAErreur.True;
			rowSource[objetDest.GetChampId()] = objetDest.Id;
			rowSource[c_colIsMappe] = true;

			//Mappe les parents de l'objet dest
			foreach ( DataRelation relation in rowSource.Table.ParentRelations )
			{
				DataRow rowParente = rowSource.GetParentRow ( relation );
				if ( rowParente != null && !(bool)rowParente[c_colIsMappe])
				{
					CObjetDonneeAIdNumeriqueAuto objetParent = null;
					CListeObjetsDonnees liste = new CListeObjetsDonnees ( objetDest.ContexteDonnee, CContexteDonnee.GetTypeForTable ( relation.ParentTable.TableName ) );
					result = interfaceMapping.MapObjet ( rowParente, liste, ref objetParent );
					if ( !result )
						return result;
					if ( objetParent != null )
						rowParente[objetParent.GetChampsId()[0]] = objetParent.Id;
					else
					{
						rowSource[relation.ChildColumns[0]] = DBNull.Value;
					}
					rowParente[c_colIsMappe] = true;
					
					objetDest.SetParent ( relation.ChildColumns[0].ColumnName, objetParent );
				}
			}
			objetDest.AssureData();
			DataTable tableDest = objetDest.Table;
			DataTable tableSource = rowSource.Table;
				

			//Copie les données
			objetDest.ContexteDonnee.CopyRow (rowSource, objetDest.Row.Row, 
				objetDest.GetChampId(), CSc2iDataConst.c_champIdSynchro, CContexteDonnee.c_colLocalKey,
				CContexteDonnee.c_colIsToRead, CContexteDonnee.c_colIsFromDb);

			//Convertit les données binaires
			foreach (DataColumn colDest in tableDest.Columns)
			{
				if (colDest.DataType == typeof(CDonneeBinaireInRow))
				{
					DataColumn colSource = tableSource.Columns[colDest.ColumnName];
					if (colSource != null && colSource.DataType == typeof(string))
					{
						object donneesSource = rowSource[colDest.ColumnName];
						if (donneesSource != DBNull.Value)
						{
							CStringSerializer serializer = new CStringSerializer((string)donneesSource, ModeSerialisation.Lecture);
							byte[] data = null;
							serializer.TraiteByteArray(ref data);
							CDonneeBinaireInRow donneeBinaire = new CDonneeBinaireInRow(objetDest.ContexteDonnee.IdSession,
								objetDest.Row.Row, colDest.ColumnName);
							if (data.Length == 0)
								donneeBinaire.Donnees = null;
							else
								donneeBinaire.Donnees = data;
						}
					}
				}
			}

			//Mappe les fils
			foreach ( DataRelation relation in rowSource.Table.ChildRelations )
			{
				DataRow[] rowsFilles = rowSource.GetChildRows ( relation );
				CObjetDonneeAIdNumeriqueAuto[] objetsFilles = new CObjetDonneeAIdNumeriqueAuto[rowsFilles.Length];
				int nIndex = 0;
				CListeObjetsDonnees listeFilles = objetDest.GetDependancesListe ( relation.ChildTable.TableName, relation.ChildColumns[0].ColumnName );
				if ( listeFilles.Count != 0 )
				{
					string strExclus = "";
					foreach ( DataRow rowFille in rowsFilles )
					{
						CObjetDonneeAIdNumeriqueAuto objetFils = null;
						result = interfaceMapping.MapObjet ( rowFille, listeFilles, ref objetFils );
						if ( !result )
							return result;
						objetsFilles[nIndex] = objetFils;
						if ( objetFils != null )
						{
							if ( strExclus.Length != 0 )
								strExclus += ",";
							strExclus += objetFils.Id.ToString();
							listeFilles.Filtre = new CFiltreData ( objetFils.GetChampId()+" not in ("+strExclus+")");
						}
						nIndex++;
					}
				}

				nIndex = 0;
				foreach ( DataRow rowFille in rowSource.GetChildRows ( relation ) )
				{
					CObjetDonneeAIdNumeriqueAuto objetFille = objetsFilles[nIndex];
					if ( objetFille == null )
					{
						objetFille = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance (
							CContexteDonnee.GetTypeForTable ( relation.ChildTable.TableName ),
							new object[]{objetDest.ContexteDonnee} );
						objetFille.CreateNewInCurrentContexte();
					}
					result = ImporteObjet ( rowFille, objetFille, interfaceMapping );
					if ( !result )
						return result;
					rowFille[c_colIsMappe] = true;
					nIndex++;
				}
			}
			return result;
		}

	}
}
