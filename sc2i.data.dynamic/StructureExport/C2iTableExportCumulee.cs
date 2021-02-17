using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Linq;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iTableExportCumulee.
	/// </summary>
	[Serializable]
	public class C2iTableExportCumulee : I2iSerializable, ITableExportAOrigineModifiable
	{
		private string m_strNomTable = "";

		private CDefinitionProprieteDynamique m_champOrigine;

		private bool m_bNePasCalculer = false;

		private C2iRequeteAvancee m_requete = new C2iRequeteAvancee(null);



		/// <summary>
		/// Filtre à appliquer aux elements pour qu'ils entrent dans la table
		/// </summary>
		private CFiltreDynamique m_filtreAAppliquer = null;

		/// <summary>
		/// Pour ne pas recalculer x fois le filtre
		/// </summary>
		private CFiltreData m_filtreDataAAppliquerCache = null;

		/// <summary>
		/// Tableau croisé des résultats
		/// </summary>
		private CTableauCroise m_tableauCroise = null;

	
		/// //////////////////////////////////////////////////////////////
		public C2iTableExportCumulee()
		{
		}

		/// //////////////////////////////////////////////////////////////
		public C2iTableExportCumulee( CDefinitionProprieteDynamique champOrigine )
		{
			ChampOrigine = champOrigine;
		}

		/// //////////////////////////////////////////////////////////////
		public string NomTable
		{
			get
			{
				return m_strNomTable;
			}
			set
			{
				m_strNomTable = value;
			}
		}

        /// //////////////////////////////////////////////////////////////
        public Type TypeSource
        {
            get
            {
                if (m_requete != null)
                    return CContexteDonnee.GetTypeForTable(m_requete.TableInterrogee);
                return null;
            }
            set
            {
            }
        }


		/// //////////////////////////////////////////////////////////////
		public bool NePasCalculer
		{
			get
			{
				return m_bNePasCalculer;
			}
			set
			{
				m_bNePasCalculer = value;
			}
		}
		

		/// //////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique ChampOrigine
		{
			get
			{
				return m_champOrigine;
			}
			set
			{
				m_champOrigine = value;
                if ( value != null )
				    m_requete.TableInterrogee = CContexteDonnee.GetNomTableForType ( m_champOrigine.TypeDonnee.TypeDotNetNatif );
			}
		}

		//-------------------------------------------------------------
		public bool IsOptimisable(ITableExport table, Type typeDeMesElements)
		{
			return false;
		}


		/// //////////////////////////////////////////////////////////////
		public C2iRequeteAvancee Requete
		{
			get
			{
				if ( m_requete == null )
					m_requete = new C2iRequeteAvancee(null);
				return m_requete;
			}
			set
			{
				m_requete = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public CTableauCroise TableauCroise
		{
			get
			{
				return m_tableauCroise;
			}
			set
			{
				m_tableauCroise = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public IChampDeTable[] Champs
		{
			get
			{
				return (IChampDeTable[])m_requete.ListeChamps.ToArray ( typeof ( IChampDeTable ) );
			}
		}

		/// //////////////////////////////////////////////////////////////
		public ITableExport[] TablesFilles
		{
			get
			{
				return new ITableExport[0];
			}
		}

		/// //////////////////////////////////////////////////////////////
		public void AddTableFille(ITableExport table)
		{
		}

		/// //////////////////////////////////////////////////////////////
		public void RemoveTableFille(ITableExport table)
		{
		}
	
	
		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//1 : ajout du tableau croisé
			//2 : Ajout de Ne pas calculer
		}


		/// //////////////////////////////////////////////////////////////
		public CFiltreDynamique FiltreAAppliquer
		{
			get
			{
				return m_filtreAAppliquer;
			}
			set
			{
				m_filtreAAppliquer = value;
				m_filtreDataAAppliquerCache = null;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public CResultAErreur GetFiltreDataAAppliquer( IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( FiltreAAppliquer == null )
				return result;
			if ( m_filtreDataAAppliquerCache == null )
			{
				FiltreAAppliquer.ElementAVariablesExterne = elementAVariables;
				result = FiltreAAppliquer.GetFiltreData();
				if ( !result )
					return result;
				m_filtreDataAAppliquerCache = (CFiltreData)result.Data;
			}
			result.Data = m_filtreDataAAppliquerCache;
			return result;
		}

		/// /////////////////////////////////////////////
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			result = m_requete.VerifieDonnees();
			if ( !result )
				return result;
			if ( m_tableauCroise != null )
			{
				//Crée la structure de la table pour tester
				DataTable table = new DataTable();
				foreach ( C2iChampDeRequete champ in m_requete.Champs )
					table.Columns.Add ( champ.NomChamp, champ.TypeDonnee );
				result = m_tableauCroise.VerifieDonnees ( table );
			}
			return result;
		}
		//----------------------------------------------------------------------------------
		public CResultAErreur Serialize ( C2iSerializer serialiser )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serialiser.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serialiser.TraiteString ( ref m_strNomTable );
			
			I2iSerializable obj = ChampOrigine;
			result = serialiser.TraiteObject ( ref obj );
			if ( !result )
				return result;
			ChampOrigine = (CDefinitionProprieteDynamique)obj;

			obj = m_requete;
			result = serialiser.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_requete = (C2iRequeteAvancee)obj;
			if ( m_requete == null )
				m_requete = new C2iRequeteAvancee(null);

			obj = m_filtreAAppliquer;
			result = serialiser.TraiteObject ( ref obj );
			if ( !result )
				return result;
			m_filtreAAppliquer = (CFiltreDynamique)obj;

			if ( nVersion >= 1 )
			{
				obj = m_tableauCroise;
				result = serialiser.TraiteObject ( ref obj );
				if ( !result )
					return result;
				m_tableauCroise = (CTableauCroise)obj;
			}
			if (nVersion >= 2)
				serialiser.TraiteBool(ref m_bNePasCalculer);
			return result;
		}

		/// //////////////////////////////////////////////////////////////
		public CResultAErreur InsertColonnesInTable ( DataTable table )
		{
			
			CResultAErreur result = CResultAErreur.True;
			if ( m_tableauCroise == null )
			{
				foreach(IChampDeTable chp in Champs)
				{
					result = CreateChampInTable(chp, table);
					if (!result)
						return result;
				}
			}
			else
			{
				foreach ( CCleTableauCroise cle in m_tableauCroise.ChampsCle)
				{
					foreach ( IChampDeTable chp in Champs )
						if ( chp.NomChamp == cle.NomChamp )
						{
							result = CreateChampInTable ( chp, table );
							if ( !result )
								return result;
						}
				}
			}
			return result;
		}

		//----------------------------------------------------------------------------------
		public CResultAErreur CreateChampInTable(IChampDeTable champExport, DataTable table)
		{
			CResultAErreur result = CResultAErreur.True;
			if (table.Columns.Contains(champExport.NomChamp))
				return result;
			Type tp = champExport.TypeDonnee;
			if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
				tp = tp.GetGenericArguments()[0];
			DataColumn col = new DataColumn(champExport.NomChamp, tp);
			col.AllowDBNull = true;
			table.Columns.Add(col);
			return result;
		}

		/// /////////////////////////////////////////////
		[NonSerialized]
		private Hashtable m_mapIdBaseParentToReadToIdTable = new Hashtable();
		[NonSerialized]
		private string m_strChampIdParent = "";
		[NonSerialized]
		private ITableExport m_tableParente = null;
		[NonSerialized]
		private int m_nIdSession = 0;
		[NonSerialized]
		private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablePourFiltres = null;
		//----------------------------------------------------------------------------------
		public CResultAErreur InsertDataInDataSet(
			IEnumerable list, 
			DataSet ds, 
			ITableExport tableParente,
			int nValeurCle,
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cacheValeurs,
			ITableExport tableFilleANeParCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur)
		{
			return InsertDataInDataSet ( list, ds, tableParente, new int[]{nValeurCle}, null, elementAVariablePourFiltres, cacheValeurs, null, bAvecOptimisation, indicateur );
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur InsertDataInDataSet(
			IEnumerable list, 
			DataSet ds, 
			ITableExport tableParente,
			int[] nValeursCle,
			RelationAttribute relationToObjetParent,
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cacheValeurs,
			ITableExport tableFilleANeParCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (NePasCalculer)
				return result;
			int nValeurIndicateur = 0;

			indicateur.SetBornesSegment ( 0, m_tableauCroise==null?2:3 );

			if (list==null)
				return result;
			if ( !(list is CListeObjetsDonnees ))
			{
				result.EmpileErreur(I.T("Attempt of cumulated table on something other that a data list|125"));
				return result;
			}

			CListeObjetsDonnees listeDonnees = (CListeObjetsDonnees)list;

			if ( relationToObjetParent == null )
			{
				
				//Principe de la lecture en une fois : 
				//Si la liste des une liste objets contenus, c'est que chaque élément
				//est un composant d'un parent. On stock donc tous les parents
				//Pour lesquels on veut les données,
				//et elles seront lues dans le EndInsertTable.
				//Par contre si la liste contenu a un filtre, on ne peut plus 
				//stocker juste ça, il faudrait en plus stocker le filtre et ça
				//devient compliqué.
				if ( listeDonnees is CListeObjetsDonneesContenus && 
					(listeDonnees.Filtre == null ||
					!listeDonnees.Filtre.HasFiltre ) )
				{
					CListeObjetsDonneesContenus listeContenu = (CListeObjetsDonneesContenus)listeDonnees;
					if ( listeContenu.ObjetConteneur != null &&
						typeof(CObjetDonneeAIdNumerique).IsAssignableFrom ( listeContenu.ObjetConteneur.GetType() ) )
					{
						if ( m_mapIdBaseParentToReadToIdTable == null )
							m_mapIdBaseParentToReadToIdTable = new Hashtable();
						m_mapIdBaseParentToReadToIdTable[((CObjetDonneeAIdNumerique)listeContenu.ObjetConteneur).Id] = nValeursCle[0];
						m_strChampIdParent = listeContenu.ChampsFille[0];
						m_tableParente = tableParente;
						listeContenu.GetFiltreForRead();
						m_nIdSession = listeContenu.ContexteDonnee.IdSession;
						m_elementAVariablePourFiltres = elementAVariablePourFiltres;
						return result;
					}
				}
			}
				

			DataTable table = ds.Tables[NomTable];
			if (table==null)
			{
				result.EmpileErreur(I.T("Table @1 doesn't exist|116", NomTable));
				return result;
			}
			DataColumn colFille = null;
			if (tableParente!=null)
			{
				foreach(Constraint  constraint in table.Constraints)
				{
					if (constraint is ForeignKeyConstraint)
					{
						ForeignKeyConstraint fkConst = (ForeignKeyConstraint) constraint;
						colFille = fkConst.Columns[0];
					}
				}
			}

			CFiltreData filtreDeBase = listeDonnees.GetFiltreForRead();

			result = GetFiltreDataAAppliquer( elementAVariablePourFiltres );
			if ( !result )
			{
                result.EmpileErreur(I.T("Error in cumulated table @1 filter|126", NomTable));
				return result;
			}

			CFiltreData leFiltreComplet = CFiltreData.GetAndFiltre (
				filtreDeBase, (CFiltreData)result.Data );

			C2iRequeteAvancee requete = m_requete;
			CTableauCroise tableauCroise = m_tableauCroise;
			
			string strChampParent = "";
			if ( relationToObjetParent != null )
			{
				requete = (C2iRequeteAvancee)CCloner2iSerializable.Clone ( m_requete );
				strChampParent = relationToObjetParent.ChampsParent[0]+"_CLE_DB";
                
                Type typeChampParent = typeof(string);
                IChampDeTable champParent = tableParente.Champs.FirstOrDefault(c => c.NomChamp == strChampParent);
                if (champParent != null)
                    typeChampParent = champParent.TypeDonnee;

                
				C2iChampDeRequete champ = new C2iChampDeRequete (
					strChampParent,
					new CSourceDeChampDeRequete(relationToObjetParent.ChampsFils[0]),
					typeof(int),
					OperationsAgregation.None,
					true );
				requete.ListeChamps.Add ( champ );
				string strListe = "";
				
				foreach ( int nId in nValeursCle )
					strListe += nId.ToString()+";";
				strListe = strListe.Substring(0, strListe.Length-1);
				leFiltreComplet = CFiltreData.GetAndFiltre(leFiltreComplet, new CFiltreDataAvance (
					requete.TableInterrogee,
					relationToObjetParent.ChampsFils[0] + " in {" + strListe + "}" ));
				if ( tableauCroise !=null )
				{
					tableauCroise = (CTableauCroise)CCloner2iSerializable.Clone(m_tableauCroise);
                    tableauCroise.AddChampCle(new CCleTableauCroise(strChampParent, typeChampParent));
				}
			}
			requete.FiltreAAppliquer = leFiltreComplet;

			indicateur.SetInfo(I.T("Table @1 (request)|127",NomTable));

            if (ChampOrigine == null)
                requete.TableInterrogee = CContexteDonnee.GetNomTableForType(listeDonnees.TypeObjets);

			result = requete.ExecuteRequete ( listeDonnees.ContexteDonnee.IdSession );
			indicateur.SetValue ( nValeurIndicateur++ );
			if ( indicateur.CancelRequest )
			{
                result.EmpileErreur(I.T("Execution cancelled by the user|118"));
				return result;
			}

			if ( !result )
				return result;
			if ( result.Data == null )
				return null;
			DataTable tableDonnees = (DataTable)result.Data;

			if ( tableauCroise != null )
			{
				indicateur.SetInfo(I.T("Table @1 (cross table)|128",NomTable));
				result = tableauCroise.CreateTableCroisee ( tableDonnees );
				indicateur.SetValue ( nValeurIndicateur++ );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in data crossing|130"));
					return result;
				}
				if ( indicateur.CancelRequest )
				{
                    result.EmpileErreur(I.T("Execution cancelled by the user|118"));
					return result;
				}
				tableDonnees = (DataTable)result.Data;
				foreach ( DataColumn col in tableDonnees.Columns )
				{
					DataColumn laColonne = table.Columns[col.ColumnName];
					if ( laColonne == null )
						table.Columns.Add ( col.ColumnName, col.DataType );
					//Stef le 5/5/2005 : Si la colonne n'est pas du bon type,
					//son type est ajusté !
					else if ( laColonne.DataType != col.DataType )
						laColonne.DataType = col.DataType;
					
				}
			}
			else
			{
				//Ajuste le type des colonnes
				foreach ( DataColumn col in tableDonnees.Columns )
				{
					DataColumn laColonne = table.Columns[col.ColumnName];
					if ( laColonne != null && laColonne.DataType != col.DataType )
						laColonne.DataType = col.DataType;
				}
			}

			indicateur.SetInfo(I.T("Storage|129"));
			indicateur.PushSegment(nValeurIndicateur, nValeurIndicateur+1);
			indicateur.SetBornesSegment(0, tableDonnees.Rows.Count );

			int nFrequence = Math.Min ( tableDonnees.Rows.Count/20, 500 )+1;

			//Intègre les données dans la table
			ArrayList lstNewRows = new ArrayList();
			int nCompteur = 0;
			foreach ( DataRow row in tableDonnees.Rows )
			{
				nCompteur++;
				if ( nCompteur%nFrequence == 0 )
				{
					indicateur.SetValue ( nCompteur );
					if  ( indicateur.CancelRequest )
					{
                        result.EmpileErreur(I.T("Execution cancelled by the user|118"));
						return result;
					}
				}
				DataRow newRow = table.NewRow();
				foreach ( DataColumn col in row.Table.Columns )
				{
					if ( newRow.Table.Columns[col.ColumnName] != null )
						newRow[col.ColumnName] = row[col];
				}
				if ( colFille != null )
				{
					if ( relationToObjetParent == null )
						newRow[colFille] = nValeursCle[0];
					else
						newRow[colFille] = row[strChampParent];
				}
				table.Rows.Add ( newRow );
			}
			indicateur.PopSegment();
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur EndInsertData( DataSet ds )
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_bNePasCalculer)
				return result;
			if ( m_mapIdBaseParentToReadToIdTable == null || m_mapIdBaseParentToReadToIdTable.Count == 0 )
				return result;

			DataTable table = ds.Tables[NomTable];
			if (table==null)
			{
				result.EmpileErreur(I.T("Table @1 doesn't exist|116", NomTable));
				return result;
			}
			DataColumn colFille = null;
			if (m_tableParente !=null)
			{
				foreach(Constraint  constraint in table.Constraints)
				{
					if (constraint is ForeignKeyConstraint)
					{
						ForeignKeyConstraint fkConst = (ForeignKeyConstraint) constraint;
						colFille = fkConst.Columns[0];
					}
				}
			}
			string strListe = "";
			foreach ( int nId in m_mapIdBaseParentToReadToIdTable.Keys )
				strListe += nId.ToString()+";";
			strListe = strListe.Substring(0, strListe.Length-1);
			CFiltreData filtreDeBase = new CFiltreDataAvance (
				m_requete.TableInterrogee,
				m_strChampIdParent+" in {"+strListe+"}" );

			result = GetFiltreDataAAppliquer(m_elementAVariablePourFiltres);
			if ( !result )
			{
                result.EmpileErreur(I.T("Error in cumulated table @1 filter|126", NomTable));
				return result;
			}

			CFiltreData leFiltreComplet = CFiltreData.GetAndFiltre (
				filtreDeBase, (CFiltreData)result.Data );
			C2iRequeteAvancee requete = (C2iRequeteAvancee)CCloner2iSerializable.Clone ( m_requete );
			string strChampParent = m_strChampIdParent+"_CLE_DB";
			C2iChampDeRequete champ = new C2iChampDeRequete (
				strChampParent,
				new CSourceDeChampDeRequete(m_strChampIdParent),
				typeof(int),
				OperationsAgregation.None,
				true );
			requete.ListeChamps.Add ( champ );
			requete.FiltreAAppliquer = leFiltreComplet;

			result = requete.ExecuteRequete ( m_nIdSession );
			if ( !result )
				return result;
			if ( result.Data == null )
				return null;
			DataTable tableDonnees = (DataTable)result.Data;
			if ( m_tableauCroise != null )
			{
				CTableauCroise newTableau = (CTableauCroise)CCloner2iSerializable.Clone(m_tableauCroise);
                newTableau.AddChampCle(new CCleTableauCroise(strChampParent, typeof(int)));
				result = newTableau.CreateTableCroisee ( tableDonnees );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in data crossing|130"));
					return result;
				}
				tableDonnees = (DataTable)result.Data;
				foreach ( DataColumn col in tableDonnees.Columns )
				{
					DataColumn laColonne = table.Columns[col.ColumnName];
					if ( laColonne == null )
						table.Columns.Add ( col.ColumnName, col.DataType );
						//Stef le 5/5/2005 : Si la colonne n'est pas du bon type,
						//son type est ajusté !
					else if ( laColonne.DataType != col.DataType )
						laColonne.DataType = col.DataType;
				}
			}
			else
			{
				//Ajuste le type des colonnes
				foreach ( DataColumn col in tableDonnees.Columns )
				{
					DataColumn laColonne = table.Columns[col.ColumnName];
					if ( laColonne != null && laColonne.DataType != col.DataType )
						laColonne.DataType = col.DataType;
				}
			}


			//Intègre les données dans la table
			ArrayList lstNewRows = new ArrayList();
			foreach ( DataRow row in tableDonnees.Rows )
			{
				DataRow newRow = table.NewRow();
				foreach ( DataColumn col in row.Table.Columns )
				{
					if ( newRow.Table.Columns[col.ColumnName] != null )
						newRow[col.ColumnName] = row[col];
				}
				if ( colFille != null )
					newRow[colFille] = (int)m_mapIdBaseParentToReadToIdTable[row[strChampParent]];
				table.Rows.Add ( newRow );
			}
			return result;

		}

		/// /////////////////////////////////////////////////////////
		public void AddProprietesOrigineDesChampsToTable ( Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee )
		{
			if ( m_champOrigine != null && FiltreAAppliquer == null)
			{
                Type typeSource = null;
                typeSource = ChampOrigine.TypeDonnee.TypeDotNetNatif;
				C2iOrigineChampExportChamp org = new C2iOrigineChampExportChamp ( ChampOrigine );
				org.AddProprietesOrigineToTable ( 
                    typeSource, 
                    tableOrigines, 
                    strChemin, 
                    contexteDonnee );
			}
		}

		/// /////////////////////////////////////////////////////////
		public ITableExport GetTableFille(string strNomTable)
		{
			return null;
		}

		/// /////////////////////////////////////////////////////////
		public ITableExport[] GetToutesLesTablesFilles()
		{
			return new ITableExport[0];
		}

		/// /////////////////////////////////////////////////////////
		public bool AcceptChilds()
		{
			return false;//Pas de table fille pour les tables cumulées
		}
	}
}
