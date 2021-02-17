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
	/// Table TableauCroise : donne une TableauCroise de la table qui est sous elle
	/// </summary>
	[Serializable]
	public class C2iTableExportTableauCroise : C2iTableExportATableFille, I2iSerializable
	{
		private CDefinitionProprieteDynamique m_champOrigine;

		private CTableauCroise m_tableau = new CTableauCroise();

		private bool m_bSupprimerTablesTravail = true;

		//---------------------------------
		public C2iTableExportTableauCroise()
			:base()
		{
			NomTable = I.T("Cross table @1|20010","Table");
		}

		/// //////////////////////////////////////////////////////////////
		public C2iTableExportTableauCroise(CDefinitionProprieteDynamique champOrigine)
			:base()
		{
			NomTable = I.T("Cross table @1|20009", DynamicClassAttribute.GetNomConvivial(champOrigine.TypeDonnee.TypeDotNetNatif));
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
		public CTableauCroise TableauCroise
		{
			get
			{
				return m_tableau;
			}
			set
			{
				m_tableau = value;
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
		public override IChampDeTable[] Champs
		{
            get
            {
                List<IChampDeTable> listeChamps = new List<IChampDeTable>();

                Dictionary<string, Type> typesChampsFils = new Dictionary<string, Type>();
                if (TablesFilles.Length > 0)
                {
                    foreach (IChampDeTable champ in TablesFilles[0].Champs)
                        typesChampsFils[champ.NomChamp] = champ.TypeDonnee;
                }

                if (m_tableau != null)
                {
                    //On ne peut connaitre que les champs clés et les champs de cumul hors pivots
                    foreach (CCleTableauCroise cle in m_tableau.ChampsCle)
                    {
                        Type tp = null;
                        if (typesChampsFils.TryGetValue(cle.NomChamp, out tp))
                            listeChamps.Add(new CChampDeTableToutSimple(cle.NomChamp, tp));
                    }
                    foreach (CCumulCroise cumul in m_tableau.CumulsCroises)
                    {
                        Type tp = null;
                        if (typesChampsFils.TryGetValue(cumul.NomChamp, out tp))
                        {
                            if (cumul.HorsPivot)
                            {
                                COperateurCumul operateur = COperateurCumul.GetNewOperateur(cumul);
                                tp = operateur.GetTypeFinal(tp);
                                listeChamps.Add(new CChampDeTableToutSimple(cumul.PrefixFinal, tp));
                            }
                            else
                            {
                                foreach (CColonneePivot pivot in m_tableau.ChampsColonne)
                                {
                                    foreach (string strValeurSystematique in pivot.ValeursSystematiques)
                                    {
                                        if (strValeurSystematique.Trim().Length > 0)
                                        {
                                            StringBuilder bl = new StringBuilder();
                                            if (cumul.PrefixFinal.Length > 0)
                                            {
                                                bl.Append(cumul.PrefixFinal);
                                                bl.Append("_");
                                            }
                                            if (pivot.Prefixe.Length > 0)
                                            {
                                                bl.Append(pivot.Prefixe);
                                                bl.Append("_");
                                            }
                                            bl.Append(strValeurSystematique);
                                            listeChamps.Add(new CChampDeTableToutSimple(bl.ToString(), tp));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return listeChamps.ToArray();
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
			return CResultAErreur.True;	
		}

		//---------------------------------
		public override CResultAErreur InsertDataInDataSet(
			IEnumerable list,
			DataSet ds,
			ITableExport tableParente,
			int[] nValeursCle,
			RelationAttribute relationToObjetParent,
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cacheValeurs,
			ITableExport tableFilleANePasCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur)
		{
			if (tableParente != null)
			{
				return base.InsertDataInDataSet(
					list,
					ds,
					tableParente,
					nValeursCle,
					relationToObjetParent,
					elementAVariablePourFiltres,
					cacheValeurs,
					tableFilleANePasCharger,
					bAvecOptimisation,
					indicateur);
			}
			else
			{
				CResultAErreur result = CResultAErreur.True;
				foreach (ITableExport tableFille in TablesFilles)
				{
					result = tableFille.InsertDataInDataSet(
						list,
						ds,
						tableParente,
						nValeursCle,
						relationToObjetParent,
						elementAVariablePourFiltres,
						cacheValeurs,
						tableFilleANePasCharger,
						bAvecOptimisation,
						indicateur);
					if (!result)
						return result;
				}
				return result;
			}
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

		/// //////////////////////////////////////////////////////////////
		public override CResultAErreur InsertColonnesInTable ( DataTable table )
		{
			CResultAErreur result = CResultAErreur.True;
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
				//Renomme la table de lien
				//Table qui a été créé et sur laquelle les tables filles ont un lien
				DataTable tableDeLien = ds.Tables[NomTable];
				tableDeLien.TableName = NomTable + "_WK";

				if (TablesFilles.Length == 0)
					return result;
				//Trouve la table fille
				DataTable tableSource = ds.Tables[TablesFilles[0].NomTable];
				if (tableSource == null)
				{
					result.EmpileErreur(I.T("Table @1 doesn't exist|20004", TablesFilles[0].NomTable));
					return result;
				}
				
				CTableauCroise tableau = new CTableauCroise();
				tableau.CopieFrom(m_tableau);
				
				
				//Trouve le lien entre la table de lien et la table source
				DataColumn colDeLienSource = null;
				if (tableDeLien.Rows.Count != 0)//Sinon, c'est qu'on est 
					//Surement table racine, ou que l'on n'a pas de données
				{
					foreach (Constraint contrainte in tableSource.Constraints)
					{
						ForeignKeyConstraint fk = contrainte as ForeignKeyConstraint;
						if (fk != null &&
							fk.RelatedTable == tableDeLien)
						{
							colDeLienSource = fk.Columns[0];
							break;
						}
					}
				}
				/*if (colDeLienSource == null)
				{
					result.EmpileErreur(I.T("Cannot find a link between @1 and @2|20007",
						tableDeLien.TableName, tableSource.TableName));
					return result;
				}*/
				//Ajoute la colonne de lien comme clé
				if ( colDeLienSource != null )
					tableau.InsertChampCle(0, 
                        new CCleTableauCroise(colDeLienSource.ColumnName, colDeLienSource.DataType));

				result = tableau.CreateTableCroisee(tableSource);
				if (!result)
					return result;
				if (result)
				{
					DataTable tableFinale = (DataTable)result.Data;
					tableFinale.TableName = NomTable;

					ds.Tables.Add(tableFinale);

					//Crée le champ de lien avec la table parente
					//Recherche de la table parente
					string strFK = "";
					DataColumn colLienFinale = null;
					foreach (Constraint contrainte in tableDeLien.Constraints)
					{
						ForeignKeyConstraint fk = contrainte as ForeignKeyConstraint;
						if (fk != null)
						{
							if (fk.Table == tableDeLien)
							{
								strFK = fk.Columns[0].ColumnName;
								colLienFinale = C2iStructureExport.CreateForeignKeyInTable(fk.RelatedColumns[0], tableFinale, 0);
								break;
							}
						}
					}

					if (colLienFinale == null && ChampOrigine != null)
					{
						result.EmpileErreur(I.T("Cannot establish a link for Union table @1|20008", NomTable));
						return result;
					}

					//Met en cache les clés filles vers les clés parentes
					Dictionary<int, int> dicIdsFillesToParent = new Dictionary<int, int>();
					string strPrimKey = tableDeLien.PrimaryKey[0].ColumnName;
					if (colLienFinale != null)
					{
						foreach (DataRow row in tableDeLien.Rows)
							dicIdsFillesToParent[(int)row[strPrimKey]] = (int)row[strFK];
						if (colDeLienSource != null)
						{
							foreach (DataRow row in tableFinale.Rows)
							{
								row[colLienFinale.ColumnName] = dicIdsFillesToParent[(int)row[colDeLienSource.ColumnName]];
							}
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
			return 1;
			//1 : ajout de Supprimer tables de travail
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
			serializer.TraiteBool(ref m_bSupprimerTablesTravail);
			
			
			I2iSerializable objet = m_tableau;
			result = serializer.TraiteObject(ref objet);
			if (!result)
				return result;
			m_tableau = (CTableauCroise)objet;

			return result;
		}

		/// /////////////////////////////////////////////////////////
		public override bool AcceptChilds()
		{
			return TablesFilles.Length == 0;
		}

	}
}
