using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;
using System.Data;
using System.Collections;
using System.Reflection;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Toutes les tables d'export qui ont des tables filles
	/// </summary>
	[Serializable]
	public abstract class C2iTableExportATableFille : ITableExportAOrigineModifiable
	{
		private static int c_nNbLectureParLotFils = 1000;

		private string m_strNomTable = "";

		private bool m_bNePasCalculer = false;

		private List<ITableExport> m_listeTables = new List<ITableExport>();

        private Type m_typeSource = null;

		/// //////////////////////////////////////////////////////////////
		public C2iTableExportATableFille()
		{

		}

        /// //////////////////////////////////////////////////////////////
        public virtual Type TypeSource
        {
            get
            {
                if (ChampOrigine != null)
                    return ChampOrigine.TypeDonnee.TypeDotNetNatif;
                return m_typeSource;
            }
            set
            {
                m_typeSource = value;
            }
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
		public abstract CDefinitionProprieteDynamique ChampOrigine {get;set;}
		
		/// //////////////////////////////////////////////////////////////
		public abstract CResultAErreur InsertColonnesInTable(DataTable table);

		//----------------------------------------------------------------------------------
		public abstract CResultAErreur CreateChampInTable(IChampDeTable champExport, DataTable table);

		/// //////////////////////////////////////////////////////////////
		///Retourne la liste de toutes les definition propriete dynamique origines des
		///champs de cette structure
		public abstract void AddProprietesOrigineDesChampsToTable(Hashtable tableOrigines, string strChemin, CContexteDonnee contexteDonnee);
		
		
		/// //////////////////////////////////////////////////////////////
		public abstract IChampDeTable[] Champs { get;}

		/// //////////////////////////////////////////////////////////////
		public ITableExport[] TablesFilles
		{
			get
			{
				return m_listeTables.ToArray();
			}
		}

		/// //////////////////////////////////////////////////////////////
		protected void ClearTablesFilles()
		{
			m_listeTables.Clear();
		}

		/// /////////////////////////////////////////////////////////
		public void AddTableFille(ITableExport table)
		{
			m_listeTables.Add(table);
		}

		/// /////////////////////////////////////////////////////////
		public void RemoveTableFille(ITableExport table)
		{
			m_listeTables.Remove(table);
		}

		

		/// //////////////////////////////////////////////////////////////
		public ITableExport[] GetToutesLesTablesFilles()
		{
			List<ITableExport> arrTables = new List<ITableExport>();

			foreach (ITableExport tbl in this.TablesFilles)
			{
				arrTables.Add(tbl);
				foreach (ITableExport table in tbl.GetToutesLesTablesFilles())
						arrTables.Add(table);
			}
			return arrTables.ToArray(); ;
		}

		/// //////////////////////////////////////////////////////////////
		public abstract CFiltreDynamique FiltreAAppliquer { get;set;}

		/// //////////////////////////////////////////////////////////////
		public abstract CResultAErreur GetFiltreDataAAppliquer(IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables);



		/// /////////////////////////////////////////////
		public virtual CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (ITableExport table in this.TablesFilles)
			{
				CResultAErreur tempResult = CResultAErreur.True;
				tempResult = table.VerifieDonnees();
				if (!tempResult)
				{
					result.Erreur.EmpileErreurs(tempResult.Erreur);
					result.SetFalse();
					return result;
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//Version 1 : Ajout de "Ne pas calculer"
            //V2 : ajout du type source
		}

		//----------------------------------------------------------------------------------
		public virtual CResultAErreur Serialize(C2iSerializer serialiser)
		{
			int nVersion = GetNumVersion();
			
			CResultAErreur result = serialiser.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			serialiser.TraiteString(ref m_strNomTable);

			serialiser.AttacheObjet(typeof(ITableExport), this);
			result = serialiser.TraiteListe<ITableExport>(m_listeTables);
			if (!result)
				return result;
			if (nVersion >= 1)
			{
				serialiser.TraiteBool(ref m_bNePasCalculer);
			}
            if (nVersion >= 2)
                serialiser.TraiteType(ref m_typeSource);
			return result;
		}

		//----------------------------------------------------------------------------------
		public virtual CResultAErreur InsertDataInDataSet(
			IEnumerable list,
			DataSet ds,
			ITableExport tableParente,
			int nValeurCle,
            IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablePourFiltres,
			CCacheValeursProprietes cacheValeurs,
			ITableExport tableFilleANePasCharger,
			bool bAvecOptimisation,
			CConteneurIndicateurProgression indicateur)
		{
			return InsertDataInDataSet(
				list,
				ds,
				tableParente,
				new int[] { nValeurCle },
				null,
				elementAVariablePourFiltres,
				cacheValeurs,
				tableFilleANePasCharger,
				bAvecOptimisation,
				indicateur);
		}

		[NonSerialized]
		private string[] m_strDependancesToOptim = null;
		//----------------------------------------------------------------------------------
		/// <summary>
		/// Indique si une table fille peut être optimisée, et donc, chargée
		/// en une seule passe, ou s'il est nécéssaire de créer chaque enregistrement
		/// fils pour chaque enregistrement parent
		/// </summary>
		/// <param name="tableFille"></param>
		/// <param name="tpDeMesElements">Type des éléments contenus dans this</param>
		/// <returns></returns>
		public virtual bool IsOptimisable(ITableExport tableFille, Type tpDeMesElements)
		{
			if (ChampOrigine == null || ChampOrigine.NomPropriete.IndexOf('.') < 0 ||
				!ChampOrigine.TypeDonnee.IsArrayOfTypeNatif)
			{

				if (tableFille is C2iTableExportATableFille || tableFille is C2iTableExportCumulee)
				{
					if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueDonneeCumulee)
					{
						return true;
					}
					else if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueRelationTypeId)
					{
						return true;
					}
					else if (tableFille.ChampOrigine != null && !tableFille.ChampOrigine.TypeDonnee.IsArrayOfTypeNatif && tableFille.ChampOrigine.GetType() == typeof(CDefinitionProprieteDynamiqueDotNet))//Table parente
					{
						//est-ce que cette table parente peut être lue comme une dépendance ?
						return true;
					}
					else if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueThis)
					{
						return true;
					}
					else
					{
						string strPropOrigine = tableFille.ChampOrigine.NomProprieteSansCleTypeChamp;
						if (strPropOrigine.IndexOf('.') < 0)
						{
							//Seules les propriétés directes sont optimisées (pour le moment et peut être que ça suffit)
							PropertyInfo info = tpDeMesElements.GetProperty(strPropOrigine);
							if (info != null)
							{
								object[] attribs = info.GetCustomAttributes(typeof(RelationFilleAttribute), true);
								if (attribs.Length > 0)
								{
									RelationFilleAttribute attrFille = (RelationFilleAttribute)attribs[0];
									tpDeMesElements = attrFille.TypeFille;
									if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpDeMesElements))
									{
										info = tpDeMesElements.GetProperty(attrFille.ProprieteFille);
										if (info != null)
										{
											attribs = info.GetCustomAttributes(typeof(RelationAttribute), true);
											if (attribs.Length > 0)
											{
												RelationAttribute attrParent = (RelationAttribute)attribs[0];
												if (attrParent.ChampsFils.Length == 1)
													return true;
											}
										}
									}
								}
                                
							}
						}
					}
				}
			}
			return false;
		}

		//----------------------------------------------------------------------------------
		protected abstract CResultAErreur InsereValeursChamps(object obj, DataRow row, CCacheValeursProprietes cacheValeurs, CRestrictionUtilisateurSurType restriction);

		//----------------------------------------------------------------------------------
		public virtual CResultAErreur InsertDataInDataSet(
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
			CResultAErreur result = CResultAErreur.True;
			if (NePasCalculer)
				return result;
			if (tableParente != null && nValeursCle.Length == 0)
				return result;
			DataTable table = ds.Tables[NomTable];
			if (table == null)
			{
				result.EmpileErreur(I.T("Table @1 doesn't exist|116", NomTable));
				return result;
			}
			indicateur.SetInfo(I.T("Table @1|115", NomTable));
			if (nValeursCle.Length > 1 &&
				relationToObjetParent == null)
			{
				result.EmpileErreur(I.T("Error: Multiple child table loading without knowing the relation indicating how the parental link is established|117"));
				return result;
			}



			DataColumn colFilleDeContrainte = null;
			DataTable tableFilleDeContrainte = null;
			if (tableParente != null)
			{
				if (ChampOrigine.TypeDonnee.IsArrayOfTypeNatif || !bAvecOptimisation || 
					ChampOrigine is CDefinitionProprieteDynamiqueThis)
				{
					//On est dans une relation fille
					foreach (Constraint constraint in table.Constraints)
					{
						if (constraint is ForeignKeyConstraint)
						{
							ForeignKeyConstraint fkConst = (ForeignKeyConstraint)constraint;
							if (fkConst.RelatedTable.TableName == tableParente.NomTable)
							{
								colFilleDeContrainte = fkConst.Columns[0];
								break;
							}
						}
					}
					tableFilleDeContrainte = table;
				}
				else
				{
					//On est dans une relation parente
					DataTable tblP = ds.Tables[tableParente.NomTable];
					foreach (Constraint contraint in tblP.Constraints)
					{
						if (contraint is ForeignKeyConstraint)
						{
							ForeignKeyConstraint fk = (ForeignKeyConstraint)contraint;
							if (fk.RelatedTable.TableName == table.TableName)
							{
								colFilleDeContrainte = fk.Columns[0];
								tableFilleDeContrainte = tblP;
								break;
							}
						}
					}
				}

			}

			if (list == null)
				return result;


			//Désactive les ids auto sur les objetDonneeAIdNumerique.
			//Car on utilise alors les valeurs de clé des éléments
			bool bUtiliserIdObjets = false;
			if (bAvecOptimisation && (ChampOrigine == null || ChampOrigine.NomPropriete.IndexOf('.') < 0 || !ChampOrigine.TypeDonnee.IsArrayOfTypeNatif))
			{
				if (list is CListeObjetsDonnees && (tableParente==null || colFilleDeContrainte == null))
				{
					if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(((CListeObjetsDonnees)list).TypeObjets))
					{
						table.PrimaryKey[0].AutoIncrement = false;
						bUtiliserIdObjets = true;
					}
				}
				if (list is ArrayList)
				{
					ArrayList arrL = (ArrayList)list;
					if (arrL.Count > 0 &&
						typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(arrL[0].GetType()))
					{
						table.PrimaryKey[0].AutoIncrement = false;
						bUtiliserIdObjets = true;
					}
				}
			}

            if (FiltreAAppliquer != null)
            {
                CListeObjetsDonnees listeObjetsDonnee = list as CListeObjetsDonnees;
                if (listeObjetsDonnee == null)//Tente de convertir en liste d'objets
                {
                    //Récupère le contexte de données
                    CContexteDonnee ctx = null;
                    foreach (object obj in list)
                    {
                        IObjetAContexteDonnee objACtx = obj as IObjetAContexteDonnee;
                        if (objACtx != null)
                        {
                            ctx = objACtx.ContexteDonnee;
                            break;
                        }
                    }
                    listeObjetsDonnee = CListeObjetsDonnees.CreateListFrom(ctx, list);
                }
                if (listeObjetsDonnee != null)
                {
                    list = listeObjetsDonnee;
                    result = GetFiltreDataAAppliquer(elementAVariablePourFiltres);
                    if (!result)
                    {
                        result.EmpileErreur(I.T("Error in the filter of the table @1|119", NomTable));
                        return result;
                    }
                    try
                    {
                        if (result.Data != null)
                            listeObjetsDonnee.Filtre = CFiltreData.GetAndFiltre(listeObjetsDonnee.Filtre, (CFiltreData)result.Data);
                    }
                    catch (Exception e)
                    {
                        result.EmpileErreur(new CErreurException(e));
                        result.EmpileErreur(I.T("Error during combination of table @1 filter|120", NomTable));
                        return result;
                    }
                }
            }

			//Table fille->
			//si relation : Attribut relation (parente) représentant le lien entre la relation fille et cette tablle
			//Si donnée cumulée : true
			Hashtable tableTablesFillesToDependanceDirecte = new Hashtable();

			//Table parente->Champ fille contenant l'id
			Hashtable tableParentsCharges = new Hashtable();

			#region Optimisations des CListeObjetsDonnees


			if (bAvecOptimisation && list is CListeObjetsDonnees)
			{
				CListeObjetsDonnees listeObjets = (CListeObjetsDonnees)list;

				if (bUtiliserIdObjets)
				{
					#region Identifie les tables filles qui peuvent être remplies en une seule requête.
					//Identifie les sous tables qui peuvent être chargées en une seule fois :
					//Il s'agit des sous tables liée directement à une propriété par
					//des relations (attribut RelationFille ou Relation).
					foreach (ITableExport tableFille in TablesFilles)
					{
						Type tpAnalyse = listeObjets.TypeObjets;
						if (tableFille != tableFilleANePasCharger && (tableFille is C2iTableExportATableFille || tableFille is C2iTableExportCumulee))
						{
							if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueDonneeCumulee)
							{
								tableTablesFillesToDependanceDirecte[tableFille] = true;
							}
							else if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueRelationTypeId)
							{
								tableTablesFillesToDependanceDirecte[tableFille] = true;
							}
							else if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueThis)
							{
								tableTablesFillesToDependanceDirecte[tableFille] = true;
							}
							else if (tableFille.ChampOrigine != null)
							{
								string strPropOrigine = tableFille.ChampOrigine.NomProprieteSansCleTypeChamp;
								if (strPropOrigine.IndexOf('.') < 0)
								{
									//Seules les propriétés directes sont optimisées (pour le moment et peut être que ça suffit)
									PropertyInfo info = tpAnalyse.GetProperty(strPropOrigine);
									if (info != null)
									{
										object[] attribs = info.GetCustomAttributes(typeof(RelationFilleAttribute), true);
										if (attribs.Length > 0)
										{
											RelationFilleAttribute attrFille = (RelationFilleAttribute)attribs[0];
											tpAnalyse = attrFille.TypeFille;
											if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpAnalyse))
											{
												info = tpAnalyse.GetProperty(attrFille.ProprieteFille);
												if (info != null)
												{
													attribs = info.GetCustomAttributes(typeof(RelationAttribute), true);
													if (attribs.Length > 0)
													{
														RelationAttribute attrParent = (RelationAttribute)attribs[0];
														if (attrParent.ChampsFils.Length == 1)
															tableTablesFillesToDependanceDirecte[tableFille] = attrParent;
													}
												}
											}
										}
									}
								}
							}
						}
					}

					#endregion

					#region Charges les tables parentes qui peuvent être chargées
					if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(listeObjets.TypeObjets))
					{
						string strNomTableFille = listeObjets.NomTable;
						foreach (ITableExport tableParenteAOptimiser in TablesFilles)
						{
							if (tableParenteAOptimiser != tableFilleANePasCharger && tableParenteAOptimiser.ChampOrigine != null && !tableParenteAOptimiser.ChampOrigine.TypeDonnee.IsArrayOfTypeNatif)
							{
                                if (IsOptimisable(tableParenteAOptimiser, TypeSource))
                                {
                                    CListeObjetsDonnees listeMere = listeObjets.GetDependances(tableParenteAOptimiser.ChampOrigine.NomProprieteSansCleTypeChamp);
                                    if (listeMere != null)
                                    {
                                        result = tableParenteAOptimiser.InsertDataInDataSet(
                                            listeMere,
                                            ds,
                                            null,
                                            0,
                                            elementAVariablePourFiltres,
                                            cacheValeurs,
                                            this,
                                            true,
                                            indicateur);
                                        if (!result)
                                            return result;
                                        //Trouve le champ fille de lien
                                        foreach (Constraint contrainte in table.Constraints)
                                            if (contrainte is ForeignKeyConstraint)
                                            {
                                                ForeignKeyConstraint fk = (ForeignKeyConstraint)contrainte;
                                                if (fk.RelatedTable.TableName == tableParenteAOptimiser.NomTable)
                                                {
                                                    tableParentsCharges[tableParenteAOptimiser] = fk.Columns[0].ColumnName;
                                                    break;
                                                }
                                            }
                                    }
                                }
							}
						}
					}
					#endregion
				}

				#region Identification des dépendances
				if (m_strDependancesToOptim == null)
				{
					Hashtable tableDependances = new Hashtable();
					AddProprietesOrigineDesChampsToTable(tableDependances, "", listeObjets.ContexteDonnee);
					foreach (ITableExport tableFille in TablesFilles)
						if (tableFille != tableFilleANePasCharger &&
							!tableTablesFillesToDependanceDirecte.Contains(tableFille) &&
							!tableParentsCharges.Contains(tableFille))
						{
							string strChemin = "";
							if (ChampOrigine != null)
								strChemin = ChampOrigine.NomPropriete;
							if (tableFille.FiltreAAppliquer == null)
								tableFille.AddProprietesOrigineDesChampsToTable(tableDependances, strChemin, listeObjets.ContexteDonnee);
						}
					m_strDependancesToOptim = new string[tableDependances.Count];
					int nDep = 0;
					foreach (string strOrigine in tableDependances.Keys)
						m_strDependancesToOptim[nDep++] = strOrigine;
				}
				#endregion
			}


			#endregion

			indicateur.SetInfo(I.T("Table @1|115", NomTable));


			CFiltreData filtreDeBase = null;
			if (list is CListeObjetsDonnees)
				filtreDeBase = ((CListeObjetsDonnees)list).Filtre;

			ArrayList listeIds = new ArrayList();
			string strColonneCle = table.PrimaryKey[0].ColumnName;

			indicateur.SetBornesSegment(0, nValeursCle.Length);
			indicateur.SetValue(0);

			//Lecture par paquets de 1000 clés
			for (int n = 0; n < nValeursCle.Length; n += c_nNbLectureParLotFils)
			{
				if (bAvecOptimisation && (list is CListeObjetsDonnees) && relationToObjetParent != null)
				{
					CListeObjetsDonnees listeObjets = (CListeObjetsDonnees)list;
					StringBuilder blCles = new StringBuilder();
					char cSepIn = ',';
					if ( filtreDeBase is CFiltreDataAvance )
						cSepIn = ';';
					for (int nCle = n; nCle < Math.Min(n + c_nNbLectureParLotFils, nValeursCle.Length); nCle++)
					{
						blCles.Append(nValeursCle[nCle]);
						blCles.Append(cSepIn);
					}
					if (blCles.Length > 0)
					{
						blCles.Remove(blCles.Length - 1, 1);
						string strCles = blCles.ToString();
						if (filtreDeBase is CFiltreDataAvance)
						{
							listeObjets.Filtre = CFiltreData.GetAndFiltre(
								filtreDeBase, new CFiltreDataAvance(
								listeObjets.NomTable,
								relationToObjetParent.ChampsFils[0] + " in {" + strCles + "}"));
						}
						else
						{
							listeObjets.Filtre = CFiltreData.GetAndFiltre(
								filtreDeBase,
								new CFiltreData(
								relationToObjetParent.ChampsFils[0] + " in (" + strCles.Replace(';', ',') + ")"));
						}
					}
				}
				if (indicateur.CancelRequest)
				{
					result.EmpileErreur(I.T("Execution cancelled by the user|118"));
					return result;
				}
				if (list is CListeObjetsDonnees && m_strDependancesToOptim != null && m_strDependancesToOptim.Length > 0)
					((CListeObjetsDonnees)list).ReadDependances(m_strDependancesToOptim);
				int nCountTotal = 1;
				if (list is IList)
					nCountTotal = ((IList)list).Count;
				else if (list is Array)
					nCountTotal = ((Array)list).Length;
				indicateur.PushSegment(n, Math.Min(n + c_nNbLectureParLotFils, nValeursCle.Length));

				int nNbElements = 0;
				indicateur.SetBornesSegment(0, nCountTotal);
				int nFrequence = Math.Min(nCountTotal / 20, 500) + 1;

                CSessionClient session = CSessionClient.GetSessionUnique();
                IInfoUtilisateur infoUser = session != null?session.GetInfoUtilisateur():null;


				///AJOUT DES LIGNES DANS LA TABLE
				foreach (object obj in list)
				{
                    CRestrictionUtilisateurSurType restriction = null;
                    if (infoUser != null && obj != null)
                        restriction = infoUser.GetRestrictionsSur(obj.GetType(), obj is IObjetAContexteDonnee?((IObjetAContexteDonnee)obj).ContexteDonnee.IdVersionDeTravail:null);

					nNbElements++;
					if (nNbElements % nFrequence == 0)
					{
						indicateur.SetValue(nNbElements);
						if (indicateur.CancelRequest)
						{
                            result.EmpileErreur(I.T("Execution cancelled by the user|118"));
							return result;
						}
					}
					bool bShouldImporte = true;

                    DataRow row = null;

					if (bUtiliserIdObjets)
					{
						row = table.Rows.Find(((CObjetDonneeAIdNumerique)obj).Id);
                        bShouldImporte = row == null;
					}
					if (bShouldImporte)
					{
						row = table.NewRow();
						if (bUtiliserIdObjets)
						{
							int nId = ((CObjetDonneeAIdNumerique)obj).Id;
							row[strColonneCle] = nId;
							listeIds.Add(nId);
						}

						//Ajoute les valeurs de champs propres à cette table
						result = InsereValeursChamps(obj, row, cacheValeurs, restriction);

						if (!result)
							return result;
						
						if (colFilleDeContrainte != null && nValeursCle.Length > 0)
						{
							DataRow rowFille = row;
							if (tableFilleDeContrainte == table)
							{
								if (relationToObjetParent == null)
									rowFille[colFilleDeContrainte] = nValeursCle[0];
								else
									rowFille[colFilleDeContrainte] = ((CObjetDonnee)obj).Row[relationToObjetParent.ChampsFils[0]];
							}
						}
						table.Rows.Add(row);
                    }
                    //Dans tous les cas, met à jour la table dépendante si besoin est !
                	if (colFilleDeContrainte != null && nValeursCle.Length > 0)
					{
						DataRow rowFille = row;
						if (tableFilleDeContrainte != table)
						{
							rowFille = tableFilleDeContrainte.Rows.Find(nValeursCle[0]);
							rowFille[colFilleDeContrainte] = row[strColonneCle];
						}
					}
                    if ( bShouldImporte )
                    {
						//AJout des données des sous tables non optimisées
						foreach (ITableExport tbl in TablesFilles)
						{
							if (tbl.ChampOrigine != null && !tableTablesFillesToDependanceDirecte.Contains(tbl) && tbl != tableFilleANePasCharger)
							{
                                bool bChildIsOptimisable = IsOptimisable(tbl, TypeSource);
								//Impossible de lire en direct
								object objet = null;
								if (tbl.ChampOrigine is CDefinitionProprieteDynamiqueThis)
								{
									objet = obj;
								}
								else
									objet =  CInterpreteurProprieteDynamique.GetValue(obj, tbl.ChampOrigine, cacheValeurs).Data;
								string strNomCol = (string)tableParentsCharges[tbl];
								if (strNomCol != null)
								{
									if (objet != null)
										row[strNomCol] = ((CObjetDonneeAIdNumerique)objet).Id;
									else
										row[strNomCol] = DBNull.Value;
								}
								else
								{
									indicateur.PushSegment(nNbElements, nNbElements + 1);
									if (objet != null)
									{
										IEnumerable tempList;
										if (objet is IEnumerable)
											tempList = (IEnumerable)objet;
										else
										{

											ArrayList listeObjetUnique = new ArrayList();
											listeObjetUnique.Add(objet);
											tempList = listeObjetUnique;
										}
										if (tempList != null)
										{
											result = tbl.InsertDataInDataSet(
                                                tempList, 
                                                ds, 
                                                this, 
                                                (int)row[table.PrimaryKey[0]], 
                                                elementAVariablePourFiltres, 
                                                cacheValeurs, 
                                                this,
                                                bChildIsOptimisable, 
                                                indicateur);
											if (!result)
												return result;
										}
									}
									indicateur.PopSegment();
								}
							}
						}
						//vide le cache après chaque objet de la table principale
						if (tableParente == null)
							cacheValeurs.ResetCache();
					}
				}
				indicateur.PopSegment();
			}
			indicateur.SetValue(nValeursCle.Length);

			///Chargement des relations optimisées
			int nTable = 0;
			foreach (DictionaryEntry entry in tableTablesFillesToDependanceDirecte)
			{
				nTable++;
				ITableExport tableFille = (ITableExport)entry.Key;
				if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueDonneeCumulee)
				{
					CDefinitionProprieteDynamiqueDonneeCumulee defCum = (CDefinitionProprieteDynamiqueDonneeCumulee)tableFille.ChampOrigine;
					//Trouve les données cumulées correspondants aux éléments
					CListeObjetsDonnees listeInit = (CListeObjetsDonnees)list;
					CTypeDonneeCumulee typeCumule = new CTypeDonneeCumulee(listeInit.ContexteDonnee);
					if (!typeCumule.ReadIfExists(
						defCum.DbKeyTypeDonnee))
					{
						result.EmpileErreur(I.T("The cumulated data type @1 doesn't exist|122", defCum.DbKeyTypeDonnee.ToString()));
						return result;
					}
					RelationAttribute attr = typeCumule.GetRelationAttributeToType(listeInit.TypeObjets);
					string strChampIdOuDbKey = "";
                    if(defCum.DbKeyTypeDonnee.IsNumericalId())
                        strChampIdOuDbKey = CTypeDonneeCumulee.c_champId;
                    else
                        strChampIdOuDbKey = CObjetDonnee.c_champIdUniversel;

                    CListeObjetsDonnees listeFils = new CListeObjetsDonnees(
						listeInit.ContexteDonnee,
						typeof(CDonneeCumulee),
                        new CFiltreData(strChampIdOuDbKey + " = @1 ", defCum.DbKeyTypeDonnee.GetValeurInDb()));


					listeFils.ModeSansTri = true;//Optimisation pour ne pas utiliser de dataview
					result = tableFille.InsertDataInDataSet(
						listeFils,
						ds,
						this,
						(int[])listeIds.ToArray(typeof(int)),
						attr,
						elementAVariablePourFiltres,
						cacheValeurs,
						this,
						true,
						indicateur);
					if (!result)
						return result;
				}
				else if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueRelationTypeId)
				{
					CDefinitionProprieteDynamiqueRelationTypeId defTypeId = (CDefinitionProprieteDynamiqueRelationTypeId)tableFille.ChampOrigine;
					RelationTypeIdAttribute relTpIdAttr = defTypeId.Relation;

					//Trouve les données cumulées correspondants aux éléments
					CListeObjetsDonnees listeInit = (CListeObjetsDonnees)list;
					if (listeInit.Count != 0)
					{
						CListeObjetsDonnees listeFils = new CListeObjetsDonnees(
							listeInit.ContexteDonnee,
							CContexteDonnee.GetTypeForTable(relTpIdAttr.TableFille),
							new CFiltreData(relTpIdAttr.ChampType + "=@1", listeInit.TypeObjets.ToString()));
						listeFils.ModeSansTri = true;//Optimisation pour ne pas utiliser de dataview
						RelationAttribute attrTmp = new RelationAttribute(
							listeInit.NomTable,
							((CObjetDonneeAIdNumerique)listeInit[0]).GetChampId(),
							relTpIdAttr.ChampId, false, false);
						result = tableFille.InsertDataInDataSet(
							listeFils,
							ds,
							this,
							(int[])listeIds.ToArray(typeof(int)),
							attrTmp,
							elementAVariablePourFiltres,
							cacheValeurs,
							this,
							true,
							indicateur);
						if (!result)
							return result;
					}
				}
				else if (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueThis)
				{
					CListeObjetsDonnees listeInit = new CListeObjetsDonnees(
						((CListeObjetsDonnees)list).ContexteDonnee,
						tableFille.ChampOrigine.TypeDonnee.TypeDotNetNatif, true);
					listeInit.ModeSansTri = true;//Optimisation pour ne pas utiliser de dataview
					string strChampId = listeInit.ContexteDonnee.GetTableSafe(CContexteDonnee.GetNomTableForType(listeInit.TypeObjets)).PrimaryKey[0].ColumnName;
					RelationAttribute attrTmp = new RelationAttribute(
												listeInit.NomTable,
												strChampId,
												strChampId,
												false, false);
					//Copie les clés dans la clé et dans la valeur de champ externe
					result = tableFille.InsertDataInDataSet(
						listeInit,
						ds,
						this,
						(int[])listeIds.ToArray(typeof(int)),
						attrTmp,
						elementAVariablePourFiltres,
						cacheValeurs,
						this,
						true,
						indicateur);
					if (!result)
						return result;
					
				}
				else if (tableFille.ChampOrigine != null)
				{
					RelationAttribute attr = (RelationAttribute)entry.Value;
					CListeObjetsDonnees listeFils = new CListeObjetsDonnees(
						((CListeObjetsDonnees)list).ContexteDonnee,
						tableFille.ChampOrigine.TypeDonnee.TypeDotNetNatif, true);
					listeFils.ModeSansTri = true;//Optimisation pour ne pas utiliser de dataview
					result = tableFille.InsertDataInDataSet(
						listeFils,
						ds,
						this,
						(int[])listeIds.ToArray(typeof(int)),
						attr,
						elementAVariablePourFiltres,
						cacheValeurs,
						this,
						true,
						indicateur);
					if (!result)
						return result;
				}
				else
				{
					result = tableFille.InsertDataInDataSet(
						null,
						ds,
						this,
						(int[])listeIds.ToArray(typeof(int)),
						null,
						elementAVariablePourFiltres,
						cacheValeurs,
						this,
						true,
						indicateur);
					if (!result)
						return result;
				}
			}

			return result;
		}

		/// /////////////////////////////////////////////////////////
		public virtual CResultAErreur EndInsertData(DataSet ds)
		{
			CResultAErreur result = CResultAErreur.True;
			if (NePasCalculer)
				return result;
			foreach (ITableExport tableFille in TablesFilles)
			{
				result = tableFille.EndInsertData(ds);
				if (!result)
					return result;
			}
			return result;
		}


		/// /////////////////////////////////////////////////////////
		public ITableExport GetTableFille(string strNomTable)
		{
			foreach (ITableExport table in TablesFilles)
				if (table.NomTable == strNomTable)
					return table;
			return null;
		}

		/// /////////////////////////////////////////////////////////
		public abstract bool AcceptChilds();

		/// /////////////////////////////////////////////////////////
		protected CResultAErreur SupprimeTableEtDependances(DataSet ds, string strNomTable)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DataTable table = ds.Tables[strNomTable];
				if (table != null)
				{
					List<DataRelation> relationsToDelete = new List<DataRelation>();
					List<DataTable> tablesToDelete = new List<DataTable>();
					foreach (DataRelation relation in ds.Relations)
					{
						if (relation.ParentTable.TableName == strNomTable)
						{
							tablesToDelete.Add(relation.ChildTable);
							relationsToDelete.Add(relation);
						}
						else if (relation.ChildTable.TableName == strNomTable)
						{
							relationsToDelete.Add(relation);
						}
					}
					foreach (DataRelation relation in relationsToDelete)
						ds.Relations.Remove(relation);
					foreach (Constraint contrainte in new ArrayList(table.Constraints))
					{
						if (!(contrainte is UniqueConstraint))
							table.Constraints.Remove(contrainte);
					}
					foreach (DataTable tableFille in tablesToDelete)
					{
						result = SupprimeTableEtDependances(ds, tableFille.TableName);
						if (!result)
							return result;
					}
					ds.Tables.Remove(table);
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}
	}
}
