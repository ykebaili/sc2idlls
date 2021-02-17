using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Collections;
using System.Data;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// table de fusion : permet de fusionner plusieurs tables 
	/// ayant le même parent en une seule table.
	/// </summary>
	[Serializable]
	public class C2iTableExportUnion : C2iTableExportATableFille, I2iSerializable
	{
		private List<IChampDeTable> m_listeChamps = null;

		private CDefinitionProprieteDynamique m_champOrigine;

		private bool m_bSupprimerTablesTravail = true;

		private List<string> m_listeChampsClesExplicites = new List<string>();

		//---------------------------------
		public C2iTableExportUnion()
			:base()
		{
			NomTable = I.T("Union @1|20000","Table");
		}

		/// //////////////////////////////////////////////////////////////
		public C2iTableExportUnion(CDefinitionProprieteDynamique champOrigine)
			:base()
		{
			NomTable = I.T("Union @1|20000", DynamicClassAttribute.GetNomConvivial(champOrigine.TypeDonnee.TypeDotNetNatif));
			m_champOrigine = champOrigine;
		}

       
		/// //////////////////////////////////////////////////////////////
		public override CDefinitionProprieteDynamique ChampOrigine
		{
			get
			{
				return m_champOrigine;
			}
			set
			{
				m_champOrigine = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool SupprimerTablesTravail
		{
			get
			{
				return m_bSupprimerTablesTravail;
			}
			set
			{
				m_bSupprimerTablesTravail = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public override void AddProprietesOrigineDesChampsToTable(Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee)
		{
			if (m_champOrigine != null)
			{
				C2iOrigineChampExportChamp org = new C2iOrigineChampExportChamp(ChampOrigine);
				org.AddProprietesOrigineToTable(
                    m_champOrigine.TypeDonnee.TypeDotNetNatif,
                    tableOrigines, 
                    strChemin, 
                    contexteDonnee);
			}
			if (strChemin.Length > 0)
				strChemin += ".";
			if (m_champOrigine != null)
				strChemin += m_champOrigine.NomPropriete;
			foreach (ITableExport table in TablesFilles)
				if (table.FiltreAAppliquer == null)
					table.AddProprietesOrigineDesChampsToTable(tableOrigines, strChemin, contexteDonnee);
		}

		//---------------------------------
		public void RecalcStructure()
		{
			Dictionary<string, bool> champsClesDefinis = new Dictionary<string, bool>();
			foreach ( string strChampCle in  m_listeChampsClesExplicites )
				champsClesDefinis[strChampCle] = true;
			m_listeChampsClesExplicites = new List<string>();
			m_listeChamps = new List<IChampDeTable>(); 
			Dictionary<string, IChampDeTable> dicChampsExistants = new Dictionary<string,IChampDeTable>();
			foreach (ITableExport table in TablesFilles)
			{
				foreach (IChampDeTable champSource in table.Champs)
				{
					IChampDeTable champ = null;
					if (!dicChampsExistants.TryGetValue(champSource.NomChamp, out champ))
					{
						champ = new C2iChampFusion(champSource.NomChamp, champSource.TypeDonnee);
						dicChampsExistants.Add(champ.NomChamp, champ);
						m_listeChamps.Add(champ);
						if (champsClesDefinis.ContainsKey(champ.NomChamp))
							m_listeChampsClesExplicites.Add(champ.NomChamp);
					}
				}
			}
		}

		//---------------------------------
		/// <summary>
		/// S'il n'y en a pas, ce sont tous les champs 
		/// communs à toutes les tables
		/// </summary>
		public string[] ChampsClesExplicites
		{
			get
			{
				return m_listeChampsClesExplicites.ToArray();
			}
			set
			{
				m_listeChampsClesExplicites = new List<string>(value);
			}
		}

		//---------------------------------
		public override IChampDeTable[] Champs
		{
			get
			{
				if (m_listeChamps == null)
					RecalcStructure();
				return m_listeChamps.ToArray();
			}
		}

		//----------------------------------------------------------------------------------
		public override CResultAErreur CreateChampInTable(IChampDeTable champExport, DataTable table)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		//-------------------------------------------------------------------------------
        protected override CResultAErreur InsereValeursChamps(object obj, DataRow row, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		
		
		//---------------------------------
		public override CResultAErreur GetFiltreDataAAppliquer(IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		//---------------------------------
		public override CFiltreDynamique FiltreAAppliquer
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		//---------------------------------
		public override CResultAErreur InsertColonnesInTable(System.Data.DataTable table)
		{
			CResultAErreur result = CResultAErreur.True;
			/*les colonnes sont inserées lors de l'insertion des données */
			return result;
		}

		//---------------------------------
		public override CResultAErreur EndInsertData(System.Data.DataSet ds)
		{
			CResultAErreur result = base.EndInsertData(ds);
			if (NePasCalculer)
				return result;
			if (!result)
				return result;
			try
			{
				//Renomme la table de travail avec un _ derrière

				
				//Table qui a été créé et sur laquelle les tables filles ont un lien
				DataTable tableDeLien = ds.Tables[NomTable];
				tableDeLien.TableName = NomTable + "_WK";

				//TAble qui va stocker les données finales
				DataTable tableDest = new DataTable(NomTable);
				ds.Tables.Add(tableDest);
				

				#region Création de la table finale
				//Crée le champ d'id 
				C2iStructureExport.CreateChampInTableAIdAuto(tableDest, 0);

				DataColumn colLienFinale = null;
				
				//Crée le champ de lien avec la table parente
				//Recherche de la table parente
				string strFK = "";
				foreach (Constraint contrainte in tableDeLien.Constraints)
				{
					ForeignKeyConstraint fk = contrainte as ForeignKeyConstraint;
					if (fk != null)
					{
						if (fk.Table == tableDeLien)
						{
							strFK = fk.Columns[0].ColumnName;
							colLienFinale = C2iStructureExport.CreateForeignKeyInTable(fk.RelatedColumns[0], tableDest, 0);
							break;
						}
					}
				}

				if (colLienFinale == null && ChampOrigine != null)
				{
					result.EmpileErreur(I.T("Cannot establish a link for Union table @1|20008", NomTable));
					return result;
				}

				//Création de l'ancienne colonne Id
				DataColumn colOldId = null;
				if (ChampOrigine != null && !(ChampOrigine is CDefinitionProprieteDynamiqueThis))
				{
					string strColOldId = DynamicClassAttribute.GetNomConvivial(ChampOrigine.TypeDonnee.TypeDotNetNatif) + "_Id_0"; ;
					colOldId = new DataColumn(strColOldId, typeof(int));
					colOldId.AllowDBNull = true;
					tableDest.Columns.Add(colOldId);
				}
				#endregion

				//Met en cache les clés filles vers les clés parentes
				Dictionary<int, int> dicIdsFillesToParent = new Dictionary<int, int>();
				string strPrimKey = tableDeLien.PrimaryKey[0].ColumnName;
				if (colLienFinale != null)
				{
					foreach (DataRow row in tableDeLien.Rows)
						dicIdsFillesToParent[(int)row[strPrimKey]] = (int)row[strFK];
				}

				#region Création des champs
				Dictionary<string, int> dicChampsDesTables = new Dictionary<string, int>();
				List<DataColumn> listeColonnesACreer = new List<DataColumn>();
				Dictionary<string, Type> typesDesChamps = new Dictionary<string,Type>();
				
				Dictionary<string, DataColumn> colsDeLien = new Dictionary<string, DataColumn>();

				Dictionary<string, List<string>> listeColsToCopyParTable = new Dictionary<string, List<string>>();
				
				foreach (ITableExport uneTableSource in TablesFilles)
				{
					DataTable tableSource = ds.Tables[uneTableSource.NomTable];
					if (tableSource == null)
					{
						result.EmpileErreur(I.T("Table @1 doesn't exist|20004", uneTableSource.NomTable));
						return result;
					}
					DataColumn colDeLien = null;
					//Trouve la colonne de lien avec la table de lien
					foreach ( Constraint contrainte in tableSource.Constraints )
					{
						ForeignKeyConstraint fk = contrainte as ForeignKeyConstraint;
						if ( fk != null && 
							fk.RelatedTable == tableDeLien )
						{
							colDeLien = fk.Columns[0];
							break;
						}
					}
					if ( colDeLien == null )
					{
						result.EmpileErreur(I.T("Cannot find a link between @1 and @2|20007",
							tableDeLien.TableName, tableSource.TableName ));
						return result;
					}
					colsDeLien[tableSource.TableName] = colDeLien;

					List<string> colsToCopy = new List<string>();
					listeColsToCopyParTable[tableSource.TableName] = colsToCopy;

					foreach (DataColumn col in tableSource.Columns)
					{
						//Ignore les clés
						if (tableSource.PrimaryKey.Length > 0 &&
							tableSource.PrimaryKey[0] == col)
							continue;
						if ( col == colDeLien )
							continue;
						int nNb = 0;
						colsToCopy.Add(col.ColumnName);
						if (!dicChampsDesTables.TryGetValue(col.ColumnName.ToUpper(), out nNb))
						{
							dicChampsDesTables[col.ColumnName.ToUpper()] = 1;
							listeColonnesACreer.Add ( col );
						}
						else
						{
							nNb++;
							dicChampsDesTables[col.ColumnName.ToUpper()] = nNb;
						}
					}
				}
				foreach (DataColumn col in listeColonnesACreer)
				{
					DataColumn newCol = new DataColumn(col.ColumnName, col.DataType);
					newCol.AllowDBNull = true;
					tableDest.Columns.Add(newCol);
				}
				#endregion

				//Trouve les clés : les clés sont les champs communs à toutes les tables
				//ou les champs explicites
				List<string> champsCles = new List<string>();
				if (m_listeChampsClesExplicites.Count > 0)
				{
					foreach (string strChampTmp in m_listeChampsClesExplicites)
						if (dicChampsDesTables.ContainsKey(strChampTmp.ToUpper()))
							champsCles.Add(strChampTmp);
					if (champsCles.Count != m_listeChampsClesExplicites.Count)
					{
						result.EmpileErreur(I.T("Some keys are not in source tables|20012"));
						return result;
					}
				}
				else
				{
					int nNbTablesSource = TablesFilles.Length;
					foreach (KeyValuePair<string, int> tableNb in dicChampsDesTables)
					{
						if (tableNb.Value == nNbTablesSource)
							champsCles.Add(tableNb.Key);
					}
				}

				string[] strChampsCles = champsCles.ToArray();

				//Il faut traiter les tables filles de celle qui a le plus de clés 
				//à celle qui en a le moins
				List<string> lstNomTablesDansOrdre = new List<string>();
				for (int nNbClesTest = strChampsCles.Length; nNbClesTest >= 1; nNbClesTest--)
				{
					foreach (ITableExport tableSource in TablesFilles)
					{
						int nNbClesDansTable = 0;
						//Compte les champs clés présents
						foreach (string strCol in listeColsToCopyParTable[tableSource.NomTable])
							if (champsCles.Contains(strCol))
								nNbClesDansTable++;
						if (nNbClesDansTable == nNbClesTest)
							lstNomTablesDansOrdre.Add(tableSource.NomTable);
					}
				}

				if (strChampsCles.Length == 0)
					foreach (ITableExport table in TablesFilles)
						lstNomTablesDansOrdre.Add(table.NomTable);
					


				///Index : clé (texte)->DataRow
				Dictionary<string, DataRow> dicIndex = new Dictionary<string, DataRow>();	
			
				CFormatteurFiltreDataToStringDataTable formateurFiltre = new CFormatteurFiltreDataToStringDataTable();

				//Insere les données table par table
				foreach (string strNomTableSource in lstNomTablesDansOrdre)
				{
					DataTable tableSource = ds.Tables[strNomTableSource];
					DataColumn colDeLien = colsDeLien[tableSource.TableName];

					bool bHasAllKeys = false;
					List<string> strClesDansTable = new List<string>();
					foreach (string strColKey in champsCles)
					{
						if (tableSource.Columns[strColKey] != null)
							strClesDansTable.Add(strColKey);
					}
					if (strClesDansTable.Count == champsCles.Count)
						bHasAllKeys = true;

					/*if (strClesDansTable.Count == 0)
					{
						//Aucune clé pour cette table, on ne sait pas quoi en faire !
						result.EmpileErreur(I.T("Table @1 doesn't have any key|20013", tableSource.TableName));
						return result;
					}*/

					CFiltreData filtreDonnees = null;
					if (!bHasAllKeys)
					{
						StringBuilder blFiltre = new StringBuilder();
						int nParametre = 2;
						blFiltre.Append(colDeLien.ColumnName);
						blFiltre.Append("=@");
						blFiltre.Append(1);
						blFiltre.Append(" and ");
						foreach (string strCle in strClesDansTable)
						{
							blFiltre.Append(strCle);
							blFiltre.Append("=@");
							blFiltre.Append(nParametre);
							blFiltre.Append(" and ");
							nParametre++;
						}
						blFiltre.Remove(blFiltre.Length - 5,5);
						filtreDonnees = new CFiltreData(blFiltre.ToString());
					}

					foreach (DataRow rowSource in tableSource.Rows)
					{
						StringBuilder blKey = new StringBuilder();
						blKey.Append(rowSource[colDeLien].ToString() + "¤");
						if (filtreDonnees != null)
						{
							filtreDonnees.Parametres.Clear();
							filtreDonnees.Parametres.Add(rowSource[colDeLien]);
						}
						for (int nKey = 0; nKey < strClesDansTable.Count; nKey++)
						{
							object val = rowSource[strClesDansTable[nKey]];
							if (filtreDonnees != null)
								filtreDonnees.Parametres.Add(val);
							else
							{
								if (val == null)
									blKey.Append("@NULL@");
								else
									blKey.Append(val.ToString());
								blKey.Append("¤");
							}
						}
						DataRow[] rowsDest = null;
						bool bIsNew = false;
						string strKey = "";
						if (filtreDonnees == null)
						{
							strKey = blKey.ToString();
							DataRow rowTmp = null;
							bIsNew = !dicIndex.TryGetValue(strKey, out rowTmp);
							if (bIsNew)
								rowTmp = tableDest.NewRow();
							rowsDest = new DataRow[] { rowTmp };
						}
						else
						{
							string strFiltre = formateurFiltre.GetString(filtreDonnees);
							rowsDest = tableDest.Select(strFiltre);
							if (rowsDest.Length == 0)
							{
								rowsDest = new DataRow[] { tableDest.NewRow() };
								bIsNew = true;
							}
						}
						foreach (DataRow rowDest in rowsDest)
						{
							foreach (string strCol in listeColsToCopyParTable[tableSource.TableName])
								rowDest[strCol] = rowSource[strCol];
							if (colOldId != null)
								rowDest[colOldId] = rowSource[colDeLien];

							if (colLienFinale != null)
							{
								int nIdLink = dicIdsFillesToParent[(int)rowSource[colDeLien]];
								rowDest[colLienFinale] = nIdLink;
							}
						}
						if (bIsNew)
						{
							tableDest.Rows.Add(rowsDest[0]);
							if ( bHasAllKeys )
								dicIndex[strKey] = rowsDest[0];
						}
					}
				}
				if (SupprimerTablesTravail)
				{
					SupprimeTableEtDependances(ds, tableDeLien.TableName);
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			
			return result;
		}

		//---------------------------------
		private int GetNumVersion()
		{
			return 2;
			//1 : ajout de Supprimer tables de travail
			//2 : Ajout des champs clés explicites
		}

		//---------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
 			CResultAErreur  result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize(serializer);
			if ( !result )
				return result;

			I2iSerializable obj = ChampOrigine;
			result = serializer.TraiteObject(ref obj);
			if (!result)
				return result;
			ChampOrigine = (CDefinitionProprieteDynamique)obj;

			if (nVersion >= 1)
				serializer.TraiteBool(ref m_bSupprimerTablesTravail);
			else
				m_bSupprimerTablesTravail = false;

			if (nVersion >= 2)
			{
				IList lstTmp = new ArrayList(m_listeChampsClesExplicites);
				serializer.TraiteListeObjetsSimples(ref lstTmp);
				if (serializer.Mode == ModeSerialisation.Lecture)
				{
					m_listeChampsClesExplicites.Clear();
					foreach (string strChamp in lstTmp)
						m_listeChampsClesExplicites.Add(strChamp);
				}
			}


			return result;
		}

		/// /////////////////////////////////////////////////////////
		public override bool AcceptChilds()
		{
			return true;
		}

	}
}
