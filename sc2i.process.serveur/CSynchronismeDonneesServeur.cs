using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text;

using sc2i.data.dynamic;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.data;
using sc2i.process;



namespace sc2i.process.serveur
{
	/// <summary>
	/// Description résumée de CFormulaireServeur.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CSynchronismeDonneesServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CSynchronismeDonneesServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CSynchronismeDonneesServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CSynchronismeDonnees.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CSynchronismeDonnees);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CSynchronismeDonnees synchro = (CSynchronismeDonnees)objet;
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}


		/// <summary>
		/// Nom de table->ArrayList contenant les champs date de la table
		/// </summary>
		private static Hashtable m_tableChampsDate = new Hashtable();
        private static bool m_bIsAutoexecDone = false;
		public static void Autoexec()
		{
            if (m_bIsAutoexecDone)
                return;
			CContexteDonneeServeur.AddTraitementAvantSauvegarde ( new TraitementSauvegardeExterne(DoTraitementExterneAvantSauvegarde) );
            m_bIsAutoexecDone = true;
		}
		
		
		public static CResultAErreur DoTraitementExterneAvantSauvegarde(CContexteDonnee contexte, Hashtable tableData )
		{
            CResultAErreur result = CResultAErreur.True;
			Type tp;
			
			ArrayList lst = new ArrayList ( contexte.Tables );
			foreach ( DataTable table in lst )
			{
				tp = null;
				ArrayList listeChampsDate = (ArrayList)m_tableChampsDate[table.TableName];
				#region récupération des champs date
				if ( listeChampsDate == null )
				{
					listeChampsDate = new ArrayList();
					//Premier passage, cherche les champs date
					foreach ( DataColumn col in table.Columns )
					{
						if ( col.DataType == typeof(DateTime) ||
							col.DataType == typeof(CDateTimeEx)||
							col.DataType == typeof(DateTime?))
						{
							listeChampsDate.Add ( col.ColumnName );
						}
					}
					m_tableChampsDate[table.TableName] = listeChampsDate;
				}
				#endregion

				if ( listeChampsDate.Count > 0 && table.PrimaryKey.Length == 1)
				{
					Hashtable tablesQuiOntChange = new Hashtable();
					//NomChamp->Méthode set associée
					Hashtable tableChampToMethode = new Hashtable();
					string strCle = table.PrimaryKey[0].ColumnName;
					ArrayList lstRows = new ArrayList(table.Rows);
					
					if ( tp == null )
						tp = CContexteDonnee.GetTypeForTable ( table.TableName );
					
					//Travaille par paquets de 500
					for ( int nRowLot =0; nRowLot < lstRows.Count; nRowLot += 500 )
					{
						int nMin = Math.Min ( lstRows.Count, nRowLot+500 );
						StringBuilder bl = new StringBuilder();
						for (int nRow = nRowLot; nRow < nMin; nRow++)
						{
							DataRow rowTest = (DataRow)lstRows[nRow];
							if (rowTest.RowState == DataRowState.Modified)
							{
								bl.Append(rowTest[strCle]);
								bl.Append(",");
							}
						}
						string strIds = bl.ToString();
						if (strIds.Length > 0)
						{
							Type typeMainElementAChamp = null;
							if (typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
							{
								CRelationElementAChamp_ChampCustom relation = (CRelationElementAChamp_ChampCustom)Activator.CreateInstance(tp, new object[] { contexte });
								typeMainElementAChamp = relation.GetTypeElementAChamps();
							}
							strIds = strIds.Substring(0, strIds.Length - 1);
							CFiltreData filtrePrinc = new CFiltreData(
								CSynchronismeDonnees.c_champIdSource + " in (" + strIds + ") and "+
								CSynchronismeDonnees.c_champTypeSource + "=@1",								
								typeMainElementAChamp == null?tp.ToString():typeMainElementAChamp.ToString());
							CListeObjetsDonnees listeSynchro = new CListeObjetsDonnees(contexte, typeof(CSynchronismeDonnees), filtrePrinc);
							listeSynchro.AssureLectureFaite();
							listeSynchro.InterditLectureInDB = true;

							CFiltreData filtreChercheMesSynchros = new CFiltreData(
								CSynchronismeDonnees.c_champIdSource + "=@1", 0);

							CFiltreData filtreChercheMesSynchrosChamp = new CFiltreData(
								CSynchronismeDonnees.c_champIdSource + "=@1 and " +
								CSynchronismeDonnees.c_champChampSource + "=@2",
								0, "");

							if (listeSynchro.Count > 0)
							{
								foreach (DataRow row in lstRows)
								{
									if (row.RowState == DataRowState.Modified)
									{
										int nCleElement = (int)row[strCle];
										filtreChercheMesSynchros.Parametres[0] = nCleElement;
										listeSynchro.Filtre = filtreChercheMesSynchros;
										if (listeSynchro.Count > 0)
										{
											foreach (string strChamp in listeChampsDate)
											{
												if (row[strChamp, DataRowVersion.Original] != DBNull.Value &&
													row[strChamp, DataRowVersion.Current] != DBNull.Value)
												{
													string strIdChampSynchronisme = strChamp;
													DateTime dtOrg, dtNew;
													dtOrg = (DateTime)row[strChamp, DataRowVersion.Original];
													dtNew = (DateTime)row[strChamp, DataRowVersion.Current];
													if (!dtNew.Equals(dtOrg))
													{

														//La colonne a change. Cherche tous les synchronismes liés à cet élément


														//Si c'est un champ custom, on cherche un synchro sur le type
														//contenant le champ custom
														if (typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
														{
															CRelationElementAChamp_ChampCustom relation = (CRelationElementAChamp_ChampCustom)contexte.GetNewObjetForRow(row);
															strIdChampSynchronisme = CSynchronismeDonnees.c_idChampCustom + row[CChampCustom.c_champId];
															nCleElement = relation.ElementAChamps.Id;
														}
														if (tp != null && tp.IsSubclassOf(typeof(CObjetDonneeAIdNumerique)))
														{
															filtreChercheMesSynchrosChamp.Parametres[0] = nCleElement;
															filtreChercheMesSynchrosChamp.Parametres[1] = strIdChampSynchronisme;
															listeSynchro.Filtre = filtreChercheMesSynchrosChamp;
															if (listeSynchro.Count > 0)
															{
																TimeSpan sp = (DateTime)row[strChamp, DataRowVersion.Current] -
																	(DateTime)row[strChamp, DataRowVersion.Original];
																foreach (CSynchronismeDonnees synchro in listeSynchro)
																{
																	CObjetDonneeAIdNumerique objetDest = synchro.ObjetDest;
																	if (objetDest != null)
																	{
																		string strChampDest = synchro.ChampDest;
																		//La donnée n'est synchronisée que si elle est
																		//égale à sa valeur d'origine
																		if (synchro.ChampDest.IndexOf(CSynchronismeDonnees.c_idChampCustom) == 0)
																		{
																			//C'est un champ custom.
																			if (objetDest is IElementAChamps)
																			{
																				CListeObjetsDonnees listeRels = ((IElementAChamps)objetDest).RelationsChampsCustom;
																				int nIdChamp = Int32.Parse(synchro.ChampDest.Substring(
																					CSynchronismeDonnees.c_idChampCustom.Length));
																				listeRels.Filtre = new CFiltreData(CChampCustom.c_champId + "=@1",
																					nIdChamp);
																				if (listeRels.Count != 0)
																				{
																					objetDest = (CObjetDonneeAIdNumerique)listeRels[0];
																					strChampDest = CRelationElementAChamp_ChampCustom.c_champValeurDate;
																				}
																				else
																					objetDest = null;
																			}
																			else
																				objetDest = null;
																		}
																		if (objetDest != null && objetDest.Row[strChampDest] != DBNull.Value)
																		{
																			object valeurOld = null;
																			object valeurCourante = objetDest.Row[strChampDest, DataRowVersion.Current];
																			if (objetDest.Row.HasVersion(DataRowVersion.Original))
																				valeurOld = objetDest.Row[strChampDest, DataRowVersion.Original];
																			if (objetDest.Row.RowState != DataRowState.Deleted &&
																				(objetDest.Row.RowState != DataRowState.Modified ||
																				valeurCourante.Equals(valeurOld)))
																			{
																				MethodInfo method = (MethodInfo)tableChampToMethode[strChampDest];
																				if (method == null)
																				///Cherche la méthode associée au champ
																				{
																					CStructureTable structure = CStructureTable.GetStructure(objetDest.GetType());
																					foreach (CInfoChampTable champ in structure.Champs)
																						if (champ.NomChamp == strChampDest)
																						{
																							PropertyInfo prop = objetDest.GetType().GetProperty(champ.Propriete);
																							if (prop != null)
																							{
																								method = prop.GetSetMethod(true);
																								tableChampToMethode[strChampDest] = method;
																							}
																							break;
																						}
																				}
																				DateTime dt = (DateTime)objetDest.Row[strChampDest];
																				dt = dt.Add(sp);
																				if (method == null)
																					objetDest.Row[strChampDest] = dt;
																				else
																					method.Invoke(objetDest, new object[] { dt });
																				tablesQuiOntChange[objetDest.GetNomTable()] = true;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					foreach ( string strNomTable in tablesQuiOntChange.Keys )
					{
						result += contexte.GetTableLoader ( strNomTable ).TraitementAvantSauvegarde ( contexte );
						if (!result)
							break;
					}
				}
			}
            return result;
		}
		
	}
}

