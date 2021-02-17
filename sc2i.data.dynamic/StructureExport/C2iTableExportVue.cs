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
	/// Table vue : donne une vue de la table qui est sous elle
	/// </summary>
	[Serializable]
	public class C2iTableExportVue : C2iTableExportATableFille, I2iSerializable
	{
		private List<C2iChampExport> m_listeChamps = new List<C2iChampExport>();

		private C2iExpression m_formuleSelection = null;

		private CDefinitionProprieteDynamique m_champOrigine;

		private bool m_bSupprimerTablesTravail = true;

		//---------------------------------
		public C2iTableExportVue()
			:base()
		{
			NomTable = I.T("View @1|20009","Table");
		}

		/// //////////////////////////////////////////////////////////////
		public C2iTableExportVue(CDefinitionProprieteDynamique champOrigine)
			:base()
		{
			NomTable = I.T("View @1|20009", DynamicClassAttribute.GetNomConvivial(champOrigine.TypeDonnee.TypeDotNetNatif));
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
		public C2iExpression FormuleSelection
		{
			get
			{
				return m_formuleSelection;
			}
			set
			{
				m_formuleSelection = value;
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
				return m_listeChamps.ToArray();
			}
		}

		/// //////////////////////////////////////////////////////////////
		public void ClearChamps()
		{
			m_listeChamps.Clear();
		}

		/// //////////////////////////////////////////////////////////////
		public void AddChamp(C2iChampExport champ)
		{
			m_listeChamps.Add(champ);
		}

		/// //////////////////////////////////////////////////////////////
		public void RemoveChamp(C2iChampExport champ)
		{
			m_listeChamps.Remove(champ);
		}

		
		//----------------------------------------------------------------------------------
		public override CResultAErreur CreateChampInTable(IChampDeTable champExport, DataTable table)
		{
			CResultAErreur result = CResultAErreur.True;
			if (table.Columns.Contains(champExport.NomChamp))
				return result;
			Type tp = champExport.TypeDonnee;
			if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
				tp = tp.GetGenericArguments()[0];
			DataColumn col = new DataColumn(champExport.NomChamp, tp);
			table.Columns.Add(col);
			return result;
		}

		//-------------------------------------------------------------------------------
		protected override CResultAErreur InsereValeursChamps(object obj, DataRow row, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction)
		{
			return CResultAErreur.True;	
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
			foreach(IChampDeTable chp in Champs)
			{
				result = CreateChampInTable(chp, table);
				if (!result)
					return result;
			}
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
					string strColOldId = DynamicClassAttribute.GetNomConvivial(ChampOrigine.TypeDonnee.TypeDotNetNatif) + "_Id_0";
					colOldId = new DataColumn(strColOldId, typeof(int));
					colOldId.AllowDBNull = true;
					tableDest.Columns.Add(colOldId);
				}

				//Création des colonnes pour les valeurs
				foreach ( C2iChampExport champ in Champs )
					CreateChampInTable ( champ, tableDest );
				#endregion

				//Met en cache les clés filles vers les clés parentes
				Dictionary<int, int> dicIdsFillesToParent = new Dictionary<int, int>();
				string strPrimKey = tableDeLien.PrimaryKey[0].ColumnName;
				if (colLienFinale != null)
				{
					foreach (DataRow row in tableDeLien.Rows)
						dicIdsFillesToParent[(int)row[strPrimKey]] = (int)row[strFK];
				}

				DataColumn colLienDeFille = null;

				if ( TablesFilles.Length == 0 )
					return result;

				DataTable tableSource = ds.Tables[TablesFilles[0].NomTable];
				if (tableSource == null)
				{
				}

				//Trouve la colonne de lien avec la table de lien
				foreach (Constraint contrainte in tableSource.Constraints)
				{
					ForeignKeyConstraint fk = contrainte as ForeignKeyConstraint;
					if (fk != null &&
						fk.RelatedTable == tableDeLien)
					{
						colLienDeFille = fk.Columns[0];
						break;
					}
				}
				if (colLienDeFille == null)
				{
					result.EmpileErreur(I.T("Cannot find a link between @1 and @2|20007",
						tableDeLien.TableName, tableSource.TableName));
					return result;
				}
				CCacheValeursProprietes cacheValeurs = new CCacheValeursProprietes();
				cacheValeurs.CacheEnabled = true;

				
				foreach (DataRow rowSource in tableSource.Rows)
				{
					bool bIntegrerRow = true;
					if (m_formuleSelection != null)
					{
						CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(rowSource);
						result = m_formuleSelection.Eval(ctxEval);
						if (!result)
							return result;
						if (result.Data is bool)
							bIntegrerRow = (bool)result.Data;
						if (result.Data is int)
							bIntegrerRow = ((int)result.Data) != 0;
					}
					if (bIntegrerRow)
					{
						DataRow rowDest = tableDest.NewRow();
						foreach (C2iChampExport champ in m_listeChamps)
						{
							try
							{
								rowDest[champ.NomChamp] = champ.GetValeur(rowSource, cacheValeurs, null);
							}
							catch { }
						}
						if ( colOldId != null )
							rowDest[colOldId] = rowSource[colLienDeFille];
						if (colLienFinale != null)
						{
							int nIdLink = dicIdsFillesToParent[(int)rowSource[colLienDeFille]];
							rowDest[colLienFinale] = nIdLink;
						}
						tableDest.Rows.Add(rowDest);
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
			
			result = serializer.TraiteListe<C2iChampExport>(m_listeChamps);
			if (!result)
				return result;

			I2iSerializable objet = m_formuleSelection;
			result = serializer.TraiteObject(ref objet);
			if (!result)
				return result;
			m_formuleSelection = (C2iExpression)objet;

			return result;
		}

		/// /////////////////////////////////////////////////////////
		public override bool AcceptChilds()
		{
			return TablesFilles.Length == 0;
		}

	}
}
