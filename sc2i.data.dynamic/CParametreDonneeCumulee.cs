using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.multitiers.client;
using System.Text;
using sc2i.data.dynamic.EasyQuery;

namespace sc2i.data.dynamic
{

	#region CCleDonneeCumulee
	[Serializable]
	public class CCleDonneeCumulee:I2iSerializable
	{
		private string m_strChamp="";
		private Type m_typeLie = null;

		/// /////////////////////////////////////////
		public CCleDonneeCumulee()
		{
		}

		/// /////////////////////////////////////////
		public CCleDonneeCumulee(string strChamp)
		{
			m_strChamp = strChamp;
		}

		/// /////////////////////////////////////////
		public CCleDonneeCumulee(string strChamp, Type typeLie)
		{
			m_strChamp = strChamp;
			m_typeLie = typeLie;
		}
			
		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strChamp );
			string strTmp = "";
			if ( m_typeLie != null )
				strTmp = m_typeLie.ToString();
			serializer.TraiteString ( ref strTmp );
			if ( serializer.Mode == ModeSerialisation.Lecture )
				m_typeLie = CActivatorSurChaine.GetType ( strTmp );
			return result;
		}

		/// /////////////////////////////////////////
		public string Champ
		{
			get
			{
				return m_strChamp;
			}
			set
			{
				m_strChamp = value;
			}
		}

		/// /////////////////////////////////////////
		public Type TypeLie
		{
			get
			{
				return m_typeLie;
			}
			set
			{
				m_typeLie = value;
			}
		}


	}
	#endregion


	
	/// <summary>
	/// Requete SQL avec paramêtres
	/// </summary>
	[Serializable]
	public class CParametreDonneeCumulee : I2iSerializable
	{
		#region CNomChampCumule
		/// <summary>
		/// Ne sert que pour les structures de données
		/// </summary>
		public class CNomChampCumule
		{
			private string m_strNomChamp;
			private int m_nNumChamp;

			public CNomChampCumule ( int nNum, string strNom )
			{
				m_strNomChamp = strNom;
				m_nNumChamp = nNum;
			}

			[DynamicField("Field name")]
			public string NomChamp
			{
				get
				{
					return m_strNomChamp;
				}
			}

			[DynamicField("Field number")]
			public int NumeroChamp
			{
				get
				{
					return m_nNumChamp;
				}
			}
		}
		#endregion
		public static int c_nbChampsCle = 10;
        public static int c_nbChampsValeur = 60;
        public static int c_nbChampsDate = 40;
        public static int c_nbChampsTexte = 40;


		//requete ou structure
		IDefinitionJeuDonnees m_definitionJeuDonnees = null;

		//Lorsque le jeu de données est un Structure d'export,
		//indique la table qui sert de source
		string m_strTableSourceDeDonnees = "";

		//Valeurs de clé (champ résultant de la requête placés dans les clés)
		private CCleDonneeCumulee[] m_cles = new CCleDonneeCumulee[c_nbChampsCle];

		//Valeurs de valeur ( champs résultant de la requête placés dans les valeurs
        private string[] m_strChampsValeurs = new string[c_nbChampsValeur];
        private string[] m_strChampsDates = new string[c_nbChampsDate];
        private string[] m_strChampsTextes = new string[c_nbChampsTexte];

		//Vide toutes les données liées au paramètre avant de recalculer
		private bool m_bViderAChaqueCalcul = false;

		//Ne supprime jamais de donnée : cela permet de lancer régulierement
		//des recalculs partiels, sans supprimer les données anciennes
		private bool m_bPasDeSuppression = false;
		
		/// <summary>
		/// //////////////////////////////////
		/// </summary>
		public CParametreDonneeCumulee()
		{
			InitChamps();
			m_definitionJeuDonnees = new CStructureExportAvecFiltre();
		}

		/// //////////////////////////////////
		/// </summary>
		public CParametreDonneeCumulee( CContexteDonnee contexteDonnee)
		{
			InitChamps();
			m_definitionJeuDonnees = new CStructureExportAvecFiltre();
		}

		/// //////////////////////////////////
		private void InitChamps()
		{
            for (int n = 0; n < c_nbChampsCle; n++)
                m_cles[n] = new CCleDonneeCumulee("");

            for (int n = 0; n < c_nbChampsValeur; n++)
				m_strChampsValeurs[n] = "";

            for (int n = 0; n < c_nbChampsDate; n++)
                m_strChampsDates[n] = "";

            for (int n = 0; n < c_nbChampsTexte; n++)
                m_strChampsTextes[n] = "";

		}

		/// //////////////////////////////////
		private int GetNumVersion()
		{
			///Remplace string[] par CCleDonneesCumulees[]
			///5 : Suppression de la requete pour un IDefinitionJeuDonnees
			///	Ajout de m_strTableSourceDeDonnees
			//return 5;
            return 6; // Modification de la liste des valeurs
                        // 60 valeurs Décimales
                        // 40 valeurs Dates
                        // 40 valeurs Textes
		}

		/// //////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			I2iSerializable objet = m_definitionJeuDonnees;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_definitionJeuDonnees = (IDefinitionJeuDonnees)objet;

			int nNbCles = c_nbChampsCle;
			if ( nVersion >= 1 )
				serializer.TraiteInt ( ref nNbCles );
			else
				nNbCles = 10;

			for ( int n = 0; n< nNbCles; n++ )
			{
				if ( n < nNbCles )
				{
					if ( nVersion < 3 )
					{
						string strTmp = "";
						serializer.TraiteString ( ref strTmp );
						m_cles[n] = new CCleDonneeCumulee ( strTmp );
					}
					else
					{
						CCleDonneeCumulee cle = m_cles[n];
						result = cle.Serialize(serializer);
						if( !result )
							return result;
						m_cles[n] = cle;
					}
				}
				else if ( n < c_nbChampsCle )
					m_cles[n] = new CCleDonneeCumulee("");
			}

			int nNbValeurs = c_nbChampsValeur;
            if (nVersion >= 1)
                serializer.TraiteInt(ref nNbValeurs);
            else
                nNbValeurs = 10;

			for ( int n = 0; n < c_nbChampsValeur; n++ )
			{
				if ( n < nNbValeurs )
					serializer.TraiteString ( ref m_strChampsValeurs[n] );
				else if ( n < c_nbChampsValeur )
					m_strChampsValeurs[n] = "";
			}

			if ( nVersion >= 2 )
				serializer.TraiteBool (ref m_bViderAChaqueCalcul);
			else
				m_bViderAChaqueCalcul = false;

			if ( nVersion >= 3 )
				serializer.TraiteBool ( ref m_bPasDeSuppression );
			else
				m_bPasDeSuppression = false;

			if (nVersion >= 5)
				serializer.TraiteString(ref m_strTableSourceDeDonnees);
			
            if(nVersion >= 6)
            {
                int nNbDates = c_nbChampsDate;
                serializer.TraiteInt(ref nNbDates);
                for (int n = 0; n < c_nbChampsDate; n++)
                {
                    if (n < nNbDates)
                        serializer.TraiteString(ref m_strChampsDates[n]);
                    else if (n < c_nbChampsDate)
                        m_strChampsDates[n] = "";
                }
                int nNbTextes = c_nbChampsTexte;
                serializer.TraiteInt(ref nNbTextes);
                for (int n = 0; n < c_nbChampsTexte; n++)
                {
                    if (n < nNbTextes)
                        serializer.TraiteString(ref m_strChampsTextes[n]);
                    else if (n < c_nbChampsTexte)
                        m_strChampsTextes[n] = "";
                }
            }

			return result;
		}

		//-------------------------------------------------
		public IDefinitionJeuDonnees DefinitionDeDonnees
		{
			get
			{
				return m_definitionJeuDonnees;
			}
			set
			{
				m_definitionJeuDonnees = value;
			}
		}

		//------------------------------------
		public string NomTableSourceDeDonnees
		{
			get
			{
				return m_strTableSourceDeDonnees;
			}
			set
			{
				m_strTableSourceDeDonnees = value;
			}
		}

		/// //////////////////////////////////
		public CCleDonneeCumulee GetChampCle ( int nCle )
		{
			if ( nCle < 0 || nCle >= c_nbChampsCle )
				return null;
			return m_cles[nCle];
		}

        /// //////////////////////////////////
        public CCleDonneeCumulee GetChampCle(string strChamp)
        {
            return m_cles.FirstOrDefault(c => c.Champ == strChamp);
        }

		/// //////////////////////////////////
		public CCleDonneeCumulee[] ChampsCle
		{
			get
			{
				return m_cles;
			}
		}

		/// //////////////////////////////////
		[DynamicField("Empty data before calculation")]
		public bool ViderAvantChaqueCalcul
		{
			get
			{
				return m_bViderAChaqueCalcul;
			}
			set
			{
				m_bViderAChaqueCalcul = value;
			}
		}

		/// //////////////////////////////////
		[DynamicField("No delete")]
		public bool PasDeSuppression
		{
			get
			{
				return m_bPasDeSuppression;
			}
			set
			{
				m_bPasDeSuppression = value;
			}
		}

		/// //////////////////////////////////
		public void SetChampCle ( int nCle, CCleDonneeCumulee cle )
		{
			if ( nCle < 0 || nCle >= c_nbChampsCle )
				return;
			m_cles[nCle] = cle;
		}

		/// //////////////////////////////////
        [DynamicMethod(typeof(string), "Return the name of a Decimal field", "")]
        public string GetValueField(int nValeur)
        {
            if (nValeur < 0 || nValeur >= c_nbChampsValeur)
                return "";
            return m_strChampsValeurs[nValeur];
        }

        /// //////////////////////////////////
        [DynamicMethod(typeof(string), "Return the name of a Date field", "")]
        public string GetDateField(int nValeur)
        {
            if (nValeur < 0 || nValeur >= c_nbChampsDate)
                return "";
            return m_strChampsDates[nValeur];
        }

        /// //////////////////////////////////
        [DynamicMethod(typeof(string), "Return the name of a Text field", "")]
        public string GetTextField(int nValeur)
        {
            if (nValeur < 0 || nValeur >= c_nbChampsTexte)
                return "";
            return m_strChampsTextes[nValeur];
        }


		/// //////////////////////////////////
        public void SetChampValeurDecimale(int nValeur, string strChamp)
        {
            if (nValeur < 0 || nValeur >= c_nbChampsValeur)
                return;
            m_strChampsValeurs[nValeur] = strChamp;
        }

        public void SetChampValeurDate(int nValeur, string strChamp)
        {
            if (nValeur < 0 || nValeur >= c_nbChampsDate)
                return;
            m_strChampsDates[nValeur] = strChamp;
        }

        public void SetChampValeurText(int nValeur, string strChamp)
        {
            if (nValeur < 0 || nValeur >= c_nbChampsTexte)
                return;
            m_strChampsTextes[nValeur] = strChamp;
        }

		/// //////////////////////////////////
        [DynamicChilds("Decimal Fields Names", typeof(CNomChampCumule))]
        public CNomChampCumule[] NomChampsDecimaux
        {
            get
            {
                ArrayList lst = new ArrayList();
                for (int n = 0; n < c_nbChampsValeur; n++)
                {
                    lst.Add(new CNomChampCumule(n, GetValueField(n)));
                }
                return (CNomChampCumule[])lst.ToArray(typeof(CNomChampCumule));
            }
        }

        [DynamicChilds("Date Fields Names", typeof(CNomChampCumule))]
        public CNomChampCumule[] NomChampsDates
        {
            get
            {
                ArrayList lst = new ArrayList();
                for (int n = 0; n < c_nbChampsDate; n++)
                {
                    lst.Add(new CNomChampCumule(n, GetDateField(n)));
                }
                return (CNomChampCumule[])lst.ToArray(typeof(CNomChampCumule));
            }
        }

        [DynamicChilds("Text Fields Names", typeof(CNomChampCumule))]
        public CNomChampCumule[] NomChampsTextes
        {
            get
            {
                ArrayList lst = new ArrayList();
                for (int n = 0; n < c_nbChampsTexte; n++)
                {
                    lst.Add(new CNomChampCumule(n, GetTextField(n)));
                }
                return (CNomChampCumule[])lst.ToArray(typeof(CNomChampCumule));
            }
        }

		/// //////////////////////////////////
		[DynamicChilds("Keys", typeof ( CNomChampCumule ))]
		public CNomChampCumule[] Cles
		{
			get
			{
				ArrayList lst = new ArrayList();
				for ( int n = 0; n < c_nbChampsCle; n++ )
				{
					lst.Add ( new CNomChampCumule ( n, GetChampCle ( n ).Champ ) );
				}
				return (CNomChampCumule[])lst.ToArray ( typeof ( CNomChampCumule ) );
			}
		}

		public static CResultAErreur GetTableSource( IElementAVariablesDynamiquesAvecContexteDonnee elt, IDefinitionJeuDonnees defDonnees, IIndicateurProgression indicateur )
		{
			CResultAErreur  result = CResultAErreur.True;
            if (!(defDonnees is CStructureExportAvecFiltre))
            {
                /*
            if (defDonnees is C2iRequete)
                return defDonnees.GetDonnees(elt, null, indicateur);
            if (defDonnees is CDefinitionJeuDonneesEasyQuery)
            {
                defDonnees.GetDonnees(elt, null, indicateur);

            }*/
                result = defDonnees.GetDonnees(elt, null, indicateur);
                if (result)
                {
                    DataSet ds = result.Data as DataSet;
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable tableOrg = ds.Tables[0];
                        DataTable tableRetour = tableOrg.Clone();
                        tableRetour.BeginLoadData();
                        foreach (DataRow row in tableOrg.Rows)
                            tableRetour.ImportRow(row);
                        tableRetour.EndLoadData();
                        result.Data = tableRetour;
                    }
                    return result;
                }
            }
			if (defDonnees is CStructureExportAvecFiltre)
			{
				C2iStructureExport structure = ((CStructureExportAvecFiltre)defDonnees).Structure;
				ITableExport table = structure.Table;
                List<C2iChampDeRequete> lstGroupBy = new List<C2iChampDeRequete>();
                if (table is C2iTableExport)
                {
                    bool bFirst = true;
                    if (table is C2iTableExport)
                    {
                        //Vérifie que toutes les tables filles sont similaires
                        foreach (ITableExport tableFilleTmp in table.TablesFilles)
                        {
                            if (!(tableFilleTmp is C2iTableExportCumulee))
                            {
                                result.EmpileErreur(I.T("Child tables must all be Cumulated tables|186"));
                                return result;
                            }
                            C2iRequeteAvancee requete = ((C2iTableExportCumulee)tableFilleTmp).Requete;
                            if (bFirst)
                            {
                                foreach (C2iChampDeRequete champ in requete.Champs)
                                {
                                    if (champ.GroupBy)
                                        lstGroupBy.Add(champ);

                                }
                            }
                            else
                            {
                                List<C2iChampDeRequete> lst = new List<C2iChampDeRequete>();
                                foreach (C2iChampDeRequete champ in requete.Champs)
                                    if (champ.GroupBy)
                                        lst.Add(champ);
                                bool bErreur = false;
                                if (lst.Count == lstGroupBy.Count)
                                {
                                    foreach (C2iChampDeRequete champ in lst)
                                        if (!lstGroupBy.Contains(champ))
                                        {
                                            bErreur = true;
                                            break;
                                        }
                                }
                                else
                                    bErreur = true;
                                if (bErreur)
                                {
                                    result.EmpileErreur(I.T("All child tables must have the same 'group by' clause|286"));
                                    return result;
                                }
                            }
                            bFirst = false;
                        }
                    }
                }
				
				result = defDonnees.GetDonnees(elt, null, indicateur);
				if (!result)
					return result;
				DataSet ds = (DataSet)result.Data;
				if (table is C2iTableExport && table.TablesFilles.Length != 0)
				{
					//Met à plat la table

					ds.EnforceConstraints = false;
					DataTable tableParent = ds.Tables[structure.Table.NomTable];
					DataTable tableFille = ds.Tables[structure.Table.TablesFilles[0].NomTable];
					DataRelation relation = null;
					//Cherche la relation entre la table Fille et la table parente
					if (tableFille != null && tableParent != null)
					{
						foreach (DataRelation rel in tableParent.ChildRelations)
							if (rel.ChildTable.TableName == tableFille.TableName)
							{
								relation = rel;
								break;
							}
					}
					if (tableParent == null || tableFille == null || relation == null)
					{
						result.EmpileErreur(I.T("Inexpected error on exported data|187"));
						return result;
					}
					//Ajoute les colonnes de la table parente
					List<string> strCols = new List<String>();
					foreach (DataColumn col in tableParent.Columns)
					{
						string strNewCol = tableParent.TableName + "_" + col.ColumnName;
						DataColumn newCol = tableFille.Columns.Add(strNewCol, col.DataType);
						newCol.AllowDBNull = col.AllowDBNull;
						strCols.Add(strNewCol);
					}
					//Rempli les données dans la table filles
					foreach (DataRow row in tableParent.Rows)
					{
						object[] datas = row.ItemArray;
						foreach (DataRow rowFille in row.GetChildRows(relation))
						{
							for (int i = 0; i < datas.Length; i++)
							{
								rowFille[strCols[i]] = datas[i];
							}
						}
					}
					if (table.TablesFilles.Length > 1)
					{
						//Plus d'1 table, ajoute les colonnes des autres tables

						//Repère les lignes par group by
						Hashtable tableGrpByToRow = new Hashtable();
						List<string> lstNomsGroupBy = new List<string>();
						foreach (DataColumn col in relation.ChildColumns)
							lstNomsGroupBy.Add(col.ColumnName);
						foreach (C2iChampDeRequete champ in lstGroupBy)
							lstNomsGroupBy.Add(champ.NomChamp);
						foreach (DataRow row in tableFille.Rows)
							tableGrpByToRow[GetCleLigne(row, lstNomsGroupBy)] = row;
						//Ajoute les données des tables filles
						for (int nTableFille = 1; nTableFille < table.TablesFilles.Length; nTableFille++)
						{
							//Crée la liste des champs groupBy
							DataTable tableTmp = ds.Tables[structure.Table.TablesFilles[nTableFille].NomTable];
							lstNomsGroupBy.Clear();
							foreach (DataColumn col in tableTmp.ParentRelations[0].ChildColumns)
								lstNomsGroupBy.Add(col.ColumnName);
							foreach (C2iChampDeRequete champ in lstGroupBy)
								lstNomsGroupBy.Add(champ.NomChamp);
							//AJoute les colonnes
							foreach (DataColumn col in tableTmp.Columns)
							{
								if (!col.AutoIncrement && !tableFille.Columns.Contains(col.ColumnName))
									tableFille.Columns.Add(col.ColumnName, col.DataType);
							}
							foreach (DataRow row in tableTmp.Rows)
							{
								DataRow rowDest = (DataRow)tableGrpByToRow[GetCleLigne(row, lstNomsGroupBy)];
                                if (rowDest == null)
                                {
                                    rowDest = tableFille.NewRow();
                                    //copie les valeurs parentes sur la nouvelle row
                                    DataRow rowParente = row.GetParentRow(tableTmp.ParentRelations[0]);
                                    foreach (DataColumn col in rowParente.Table.Columns)
                                    {
                                        string strCol = tableParent.TableName + "_" + col.ColumnName;
                                        if (tableFille.Columns[strCol] != null)
                                            rowDest[strCol] = rowParente[col.ColumnName];
                                    }
                                    tableGrpByToRow[GetCleLigne(row, lstNomsGroupBy)] = rowDest;
                                }
								foreach (DataColumn col in tableTmp.Columns)
									if (tableFille.Columns.Contains(col.ColumnName))
										rowDest[col.ColumnName] = row[col];
								if (rowDest.RowState == DataRowState.Detached)
									tableFille.Rows.Add(rowDest);
							}
						}
					}
					result.Data = tableFille;
				}
				else
					result.Data = ds.Tables[table.NomTable];
				return result;
			}
			return result;
		}

		private static string GetCleLigne(DataRow row, List<String> lstChamps)
		{
			StringBuilder bl = new StringBuilder();
			foreach (string strCol in lstChamps)
			{
				bl.Append(row[strCol].ToString());
				bl.Append("~");
			}
			return bl.ToString();
		}

	}

	/// ////////////////////////////////////////////////////////////////
	/// <summary>
	/// Relation TypeId
	/// </summary>
	[AutoExec("Autoexec")]
    [Serializable]
	public class CInfoRelationComposantFiltreDonneeCumulee : CInfoRelationComposantFiltre
	{
		private const string c_cleDonnee = "DONNEECUMUL";
		public static string GetIdTypeDonnee ( int nIdType, int nNumeroCle )
		{
			return c_cleDonnee+"_"+nIdType+"_"+nNumeroCle;
		}

        //DBKEYTODO : remplacer m_nIdTypeDonneeCumulee par un DbKeyChamp
		private int m_nIdTypeDonneeCumulee = -1;
		private int m_nNumeroCle = 0;
		private string m_strTableParente = "";

		/// ////////////////////////////////////////////////////////////////
		public CInfoRelationComposantFiltreDonneeCumulee ( )
		{
		}

		/// ////////////////////////////////////////////////////////////////
		public CInfoRelationComposantFiltreDonneeCumulee ( int nIdTypeDonnee, int nNumeroCle, string strTableParente )
		{
			m_nIdTypeDonneeCumulee = nIdTypeDonnee;
			m_nNumeroCle = nNumeroCle;
			m_strTableParente = strTableParente;
		}

		/// ////////////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CComposantFiltreChamp.FindRelationComplementaire += new FindRelationDelegate(FindRelation);
		}

		/// ////////////////////////////////////////////////////////////////
        public static void FindRelation(string strTable, Type type, ref CInfoRelationComposantFiltre relationTrouvee)
		{
			if (relationTrouvee != null)//déjà trouvée
				return;
			string[] strZones = strTable.Split('_');
			if ( strZones[0] != c_cleDonnee )
				return;
			//Oui, c'en est une
			try
			{
				int nIdTypeDonnee = Int32.Parse ( strZones[1] );
				int nNumeroCle = Int32.Parse ( strZones[2] );
				relationTrouvee = new CInfoRelationComposantFiltreDonneeCumulee ( nIdTypeDonnee, nNumeroCle, CContexteDonnee.GetNomTableForType ( type ) );
			}
			catch {}
		}

		/// ////////////////////////////////////////////////////////////////
		public override bool IsRelationFille
		{
			get
			{
				return true;
			}
		}

		/// ////////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			serializer.TraiteInt ( ref m_nIdTypeDonneeCumulee );
			serializer.TraiteInt ( ref m_nNumeroCle );
			serializer.TraiteString ( ref m_strTableParente );

			return result;
		}


		/// ////////////////////////////////////////////////////////////////
		public override string RelationKey
		{
			get
			{
				return "DOCCUM_"+m_nIdTypeDonneeCumulee+"_"+m_strTableParente;
			}
		}


		/// ////////////////////////////////////////////////////////////////
		public override string TableFille
		{
			get
			{
				return CDonneeCumulee.c_nomTable;
			}
		}

		/// ////////////////////////////////////////////////////////////////
		public override string TableParente
		{
			get
			{
				return m_strTableParente;
			}
		}

		/// ////////////////////////////////////////////////////////////////
		//Retourne le texte de la clause join à mettre dans SQL
		public override string GetJoinClause ( 
			string strAliasTableParente, 
			string strSuffixeParent,
			string strAliasTableFille,
			string strSuffixeFils) 
		{
			//Identifie le type parent
			Type tp = CContexteDonnee.GetTypeForTable ( m_strTableParente );
			if ( !tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) ) )
				throw new Exception (I.T("Cumulated data link on a table which isn't managed by a numerical identifier|188"));
			CStructureTable structure = CStructureTable.GetStructure(tp);
			string strJoin = strAliasTableParente+"."+structure.ChampsId[0].NomChamp+strSuffixeParent+"="+
				strAliasTableFille+"."+CDonneeCumulee.c_baseChampCle+m_nNumeroCle.ToString()+strSuffixeFils+" and "+
				strAliasTableFille+"."+CTypeDonneeCumulee.c_champId+strSuffixeFils+"="+
				m_nIdTypeDonneeCumulee.ToString();
			return strJoin;
		}
	}

}
