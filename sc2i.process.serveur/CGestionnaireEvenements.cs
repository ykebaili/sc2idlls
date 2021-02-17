using System;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using sc2i.data.serveur;
using sc2i.process;
using System.Text;

namespace sc2i.process.serveur
{
	/// <summary>
	/// Fonctionnement : 
	/// Avant la sauvegarde, note tous les évenements à déclencher dans TraitementAvantSauvegardeExterneObjetServeur
	/// après la sauvegarde, appelle la fonction TraitementApresSauvegarde qui déclenche les
	/// évenement. Ce procedé permet d'avoir les id des nouveaux éléments positionnés
	/// lors du déclenchement.
	/// </summary>
	public class CGestionnaireEvenements
	{
		private const string c_cleDonneeListeTraitements = "TRAITEMENTS_EVENEMENTS";
		
		//Liste des évenements à déclencher après la sauvegarde
		private class CTraitementApresSauvegarde
		{
			public abstract class CCoupleDeclencheurObjet : IComparable
			{
				public readonly CObjetDonneeAIdNumerique Objet;
				public readonly CInfoDeclencheurProcess InfoDeclencheur;
				
				private bool m_bIsDeclenche = false;

				public abstract bool PeutEtreExecuteSurLePosteClient { get;}

				/// <summary>
				/// 
				/// </summary>
				/// <param name="objet"></param>
				public CCoupleDeclencheurObjet ( CObjetDonneeAIdNumerique objet, CInfoDeclencheurProcess infoDeclencheur )
				{
					Objet = objet;
					InfoDeclencheur = infoDeclencheur;
				}

				public abstract CResultAErreur OnDeclenche ( );
				public abstract CResultAErreur OnDeclencheSurClient();

				public abstract string CleUnique{get;}

				public abstract int OrdreExecution{get;}

				public int CompareTo(object obj)
				{
					if ( obj is CCoupleDeclencheurObjet )
						return OrdreExecution.CompareTo ( ((CCoupleDeclencheurObjet)obj).OrdreExecution );
					return 0;
				}

				protected void SetIsDeclenche(bool b)
				{
					m_bIsDeclenche = b;
				}

				public bool IsDejaDeclenche
				{
					get
					{
						return m_bIsDeclenche;
					}
				}
			}


			public class CCoupleEvenementObjet : CCoupleDeclencheurObjet
			{
				public readonly CEvenement Evenement;

				public CCoupleEvenementObjet ( CEvenement evt, CObjetDonneeAIdNumerique obj, CInfoDeclencheurProcess infoDeclencheur )
					:base ( obj, infoDeclencheur )
				{
					Evenement = evt;
				}

				//----------------------------------------------------
				public override bool PeutEtreExecuteSurLePosteClient
				{
					get
					{
                        if (Evenement != null)
                            return Evenement.DeclencherSurContexteClient || Evenement.TypeEvenement == TypeEvenement.Suppression;
						return false;
					}
				}

				//----------------------------------------------------
				public override CResultAErreur OnDeclencheSurClient( )
				{
					CResultAErreur result = CResultAErreur.True;
					if (Objet is CObjetDonneeAIdNumerique && !IsDejaDeclenche)
					{
						result = Evenement.EnregistreDeclenchementEvenementSurClient((CObjetDonneeAIdNumerique)Objet, InfoDeclencheur, null);
						if (result)
							SetIsDeclenche(true);
						else
							result.EmpileErreur(I.T("Error in @1 event|20000", Evenement.Libelle));
					}
					return result;
				}

				//----------------------------------------------------
				public override CResultAErreur OnDeclenche ( )
				{
					CResultAErreur result = CResultAErreur.True;
					if (!IsDejaDeclenche)
					{
						result = Evenement.EnregistreDeclenchementEvenement(Objet, InfoDeclencheur);
						if (result)
							SetIsDeclenche(true);
					}
					return result;
				}

				public override string CleUnique
				{
					get
					{
						return "EVT_"+Evenement.Id.ToString()+"_"+Objet.Id.ToString();
					}
				}

				public override int OrdreExecution
				{
					get
					{
						if ( Evenement != null )
							return Evenement.OrdreExecution;
						return 0;
					}
				}

			}

			
			public class CCoupleHandlerObjet : CCoupleDeclencheurObjet
			{
				public readonly CHandlerEvenement Handler;

				public CCoupleHandlerObjet ( CHandlerEvenement handler, CObjetDonneeAIdNumerique obj, CInfoDeclencheurProcess infoDeclencheur )
					:base(obj, infoDeclencheur)
				{
					Handler = handler;
				}

				public override CResultAErreur OnDeclencheSurClient()
				{
                    CResultAErreur result = CResultAErreur.True;
                    /*if (Handler.EtapeWorkflowATerminer == null)
                    {*/
                        //Truc qui n'arrive jamais !
                        result.EmpileErreur("Can not start that event on client context");
                        return result;
                    /*}
                    return Handler.RunEvent(this.Objet, this.InfoDeclencheur, null);*/

				}

				public override CResultAErreur OnDeclenche ( )
				{
					CResultAErreur result = CResultAErreur.True;
					if (!IsDejaDeclenche)
					{
						result = Handler.RunEvent(true, Objet, InfoDeclencheur, null);
					}
					return result;
				}

				//----------------------------------------------------
				public override bool PeutEtreExecuteSurLePosteClient
				{
					get
					{
                        return false;
					}
				}

				public override string CleUnique
				{
					get
					{
						return "HDL_"+Handler.Id.ToString()+"_"+Objet.Id.ToString();
					}
				}

				public override int OrdreExecution
				{
					get
					{
						if ( Handler != null )
							return Handler.OrdreExecution;
						return 0;
					}
				}
			}
			
			//Liste des évenements/Objet à déclencher
			//ou handler/objet
			private ArrayList m_listeCoupesEvtObj = new ArrayList();


			/// //// //// //// //// //// //// //// //// /
			public CTraitementApresSauvegarde (  )
			{
			}

			/// ///////////////////////////////////////////////
			public bool ContainsCouple(object evt, CObjetDonneeAIdNumerique objetDonneeAIdNumeriqueAuto)
			{
				foreach (CCoupleDeclencheurObjet couple in m_listeCoupesEvtObj)
				{
					if (couple is CCoupleEvenementObjet && evt is CEvenement)
					{
						CCoupleEvenementObjet cpl = (CCoupleEvenementObjet)couple;
						if (cpl.Evenement.Id == ((CEvenement)evt).Id &&
							objetDonneeAIdNumeriqueAuto.Equals(cpl.Objet))
							return true;
					}
				}
				return false;
			}

			/// ///////////////////////////////////////////////
			public void AddCoupleEvenementObjet ( CEvenement evt, CObjetDonneeAIdNumerique obj, CInfoDeclencheurProcess infoDeclencheur )
			{
				m_listeCoupesEvtObj.Add ( new CCoupleEvenementObjet ( evt, obj, infoDeclencheur ) );
			}

			/// ///////////////////////////////////////////////
			public void AddCoupleHandlerObjet ( CHandlerEvenement handler, CObjetDonneeAIdNumerique obj, CInfoDeclencheurProcess infoDeclencheur )
			{
				m_listeCoupesEvtObj.Add ( new CCoupleHandlerObjet ( handler, obj, infoDeclencheur ) );
			}

			/// ///////////////////////////////////////////////
			public ArrayList CouplesEvenementOuHandlerObjet
			{
				get
				{
					m_listeCoupesEvtObj.Sort();
					return m_listeCoupesEvtObj;
				}
			}




			
		}

		
		public static CContexteDonnee m_contexteCache = null;
		//Surveille les modifs dans la table des événements
		public static CRecepteurNotification m_recepteurModif = null;
		//Surveille les ajouts dans la table des évenements;
		public static CRecepteurNotification m_recepteurAjouts = null;

		//Table contenant pour chaque type d'objets la liste des évenements sur création ou modif qui existent
		private static Hashtable m_tableTypeToEvenements = new Hashtable();
		
		//Indique que les evenements ont changé
		public static bool m_bEvenementsChanged = true;

		private class CLockerCacheEvenements
		{
		}

		private static ArrayList GetListeEvenementsAutoSur ( Type tp, int? nIdVersion )
		{
			ArrayList lst = null; 
			if ( nIdVersion == null )
				lst =(ArrayList)m_tableTypeToEvenements[tp];
			if ( lst == null )
			{
				CContexteDonnee contextePourRechercheDeProcess = Contexte;
				if (nIdVersion != null)
				{
					contextePourRechercheDeProcess = new CContexteDonnee(0, true, false);
					contextePourRechercheDeProcess.SetVersionDeTravail(nIdVersion, false);
				}
				try
				{
					CListeObjetsDonnees liste = new CListeObjetsDonnees(contextePourRechercheDeProcess, typeof(CEvenement));
					if (m_bEvenementsChanged && nIdVersion == null)
						liste.AssureLectureFaite();
					if ( nIdVersion == null )
						m_bEvenementsChanged = false;

					lst = new ArrayList();

					liste.InterditLectureInDB = nIdVersion == null;
					liste.Filtre = new CFiltreData(
						CEvenement.c_champTypeCible + "=@1 and " +
						CEvenement.c_champTypeEvenement + "<>@2"/* and " +
						CEvenement.c_champTypeEvenement + "<>@3"*/,
						tp.ToString(),
						(int)TypeEvenement.Manuel/*,
						(int)TypeEvenement.Date*/);
					foreach (CEvenement evt in liste)
						lst.Add(evt);
					lock (typeof(CLockerCacheEvenements))
					{
						if ( nIdVersion == null )
							m_tableTypeToEvenements[tp] = lst;
					}
				}
				catch
				{
				}
				finally
				{
					if (nIdVersion != null)
						contextePourRechercheDeProcess.Dispose();
				}
			}
			return lst;
		}

		///Liste des éléments à repasser pour un id de contexte après execution des actions 
		///dans le contexte courant
		private static Dictionary<CContexteDonnee, Dictionary<string, Dictionary<int, bool>>> m_listeElementsARepasser = new Dictionary<CContexteDonnee, Dictionary<string, Dictionary<int, bool>>>();
		/// <summary>
		/// /////////////////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="table"></param>
		/// <param name="result"></param>
		/// 
		private static CResultAErreur TraitementAvantSauvegardeExterne ( CContexteDonnee contexte, Hashtable tableData )
		{
            CResultAErreur result = CResultAErreur.True;

			///Stef 22/07/08 : l'appel à shouldDeclenche peut 
			///nécessiter GetValeurChamp (si conditions sur des champs). Or, si un élément n'a pas
			///de valeur pour un champ, l'appli va aller chercher la valeur par défaut de ce
			///champ, si le champ n'est pas chargé, l'appli va le lire. Comme on est souvent
			///dans un contexte sans gestion par tables complètes, on est mal, parce que 
			///ça va génerer une requête par champ.
			///Donc, on lit tous les champs custom avant de commencer
			CListeObjetsDonnees listeChamps = new CListeObjetsDonnees(contexte, typeof(CChampCustom));
			listeChamps.PreserveChanges = true;//Pour ne pas modifier les champs modifiés
			listeChamps.AssureLectureFaite();

			DateTime dt = DateTime.Now;			
			CTraitementApresSauvegarde traitement = new CTraitementApresSauvegarde ( );

			ArrayList lstTables = CContexteDonnee.GetTablesOrderInsert ( contexte );

			//Pour empêcher de regarder deux fois les évenements d'un même objet
			//Type->Dictionnaire des ids vus
			Dictionary<Type, Dictionary<int, bool>> elementsVus = new Dictionary<Type, Dictionary<int, bool>>();

			DataRowChangeEventHandler handlerRedo = new DataRowChangeEventHandler(table_RowChanged);
			
			Dictionary<string, Dictionary<int, bool>> listeElementsARepasser = null;
			if (!m_listeElementsARepasser.TryGetValue(contexte, out listeElementsARepasser))
			{
				listeElementsARepasser = new Dictionary<string, Dictionary<int, bool>>();
				m_listeElementsARepasser[contexte] = listeElementsARepasser;
			}

			bool bFirstPasse = true;
			int nLimiteurPasses = 0;
			while ( (bFirstPasse | listeElementsARepasser.Count > 0) && nLimiteurPasses < 5 )
			{
				nLimiteurPasses++;

				foreach (DataTable table in lstTables)
				{
					if (table.Rows.Count > 0 && table.PrimaryKey.Length == 1)
					{
						string strChampCle = table.PrimaryKey[0].ColumnName;

						Type tpObjets = CContexteDonnee.GetTypeForTable(table.TableName);
						if (!typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpObjets))
							continue;

						Type typeOriginal = tpObjets;

						//Lors de la modif de champs custom, l'élement parent n'est pas forcement modifié
						//Mais le champ peut l'être
						if (tpObjets.IsSubclassOf(typeof(CRelationElementAChamp_ChampCustom)))
						{
							int nLigne = 0;
							bool bGoOut = false;
							while (table.Rows.Count > nLigne && !bGoOut)
							{
								if (table.Rows[nLigne].RowState != DataRowState.Deleted)
								{
									CRelationElementAChamp_ChampCustom rel = (CRelationElementAChamp_ChampCustom)contexte.GetNewObjetForRow(table.Rows[nLigne]);
									tpObjets = rel.ElementAChamps.GetType();
									strChampCle = ((CObjetDonnee)rel.ElementAChamps).GetChampsId()[0];
									bGoOut = true;
								}
								nLigne++;
							}
						}

						//Les objets qui n'ont pas de relationTypeId ne peuvent pas avoir d'évenements
						if (tpObjets.GetCustomAttributes(typeof(NoRelationTypeIdAttribute), true).Length > 0)
							continue;

						ArrayList lstEvenements = GetListeEvenementsAutoSur(tpObjets, contexte.IdVersionDeTravail);

						//Id des éléments modifiés
						List<int> listeIdsElementsAVerifierHandlers = new List<int>();
						string strPrimaryKey = "";
						if (table.PrimaryKey.Length == 1 &&
							table.PrimaryKey[0].DataType == typeof(int))
							strPrimaryKey = table.PrimaryKey[0].ColumnName;

						Dictionary<int, bool> tableIdsVues = null;
						if (!elementsVus.TryGetValue(tpObjets, out tableIdsVues))
						{
							tableIdsVues = new Dictionary<int, bool>();
							elementsVus[tpObjets] = tableIdsVues;
						}

						Dictionary<int, bool> listeARepasserPourTable = null;
						if ( !listeElementsARepasser.TryGetValue ( table.TableName, out listeARepasserPourTable ))
						{
							listeARepasserPourTable = null;
                        }

                        List<CObjetDonnee> lstObjetsAvecEvenementsSpecifiques = new List<CObjetDonnee>();

                        //Stef 16/11/2012 : 
                        //Si c'est un élément à champs, il est consideré comme modifié
                        //si un de ses champs custom est modifiés
                        //Ca a été fait parce que les handlers d'évenement n'étaient
                        //Pas pris en compte sur modif de champ custom
                        //On n'enlève pas pour autant l'ancienne méthode qui consiste
                        //à considérer l'élément modifié losrqu'on passe sur la table des
                        //valeurs de champs custom
                        if (typeof(IObjetDonneeAChamps).IsAssignableFrom(typeOriginal) && table.Rows.Count > 0)
                        {
                            //Regarde s'il y a des relations 
                            IObjetDonneeAChamps objAChamp = contexte.GetNewObjetForRow(table.Rows[0]) as IObjetDonneeAChamps;
                            string strTableChamps = objAChamp.GetNomTableRelationToChamps();
                            //Trouve la relation à la table
                            DataTable tableChamps = contexte.Tables[strTableChamps];
                            if (tableChamps != null)//Si la table champs n'est pas là, c'est
                            //qu'elle n'a pas été modifiée !! c'est logique çà
                            {
                                DataRelation rel = null;
                                foreach (DataRelation relTest in tableChamps.ParentRelations)
                                {
                                    if (relTest.ParentTable.TableName == table.TableName)
                                    {
                                        rel = relTest;
                                        break;
                                    }
                                }
                                if (rel != null)//On peut vérifier !
                                {
                                    foreach (DataRow row in new ArrayList(table.Rows))
                                    {
                                        if (row.RowState == DataRowState.Unchanged)//sinon, ce n'est pas la peine
                                        {
                                            DataRow[] rowsChamps = row.GetChildRows(rel);
                                            foreach (DataRow rowChamp in rowsChamps)
                                            {
                                                if (rowChamp.RowState != DataRowState.Unchanged)
                                                {
                                                    //Aloue l'objet pour s'assurer que la ligne est bien pleine
                                                    CObjetDonnee objTmp = contexte.GetNewObjetForRow(row);
                                                    objTmp.AssureData();
                                                    row.SetModified();
                                                    string strOldContexte = (string)row[CObjetDonnee.c_champContexteModification];
                                                    if (strOldContexte.Length == 0)
                                                    {
                                                        row[CObjetDonnee.c_champContexteModification] = rowChamp[CObjetDonnee.c_champContexteModification,
                                                           rowChamp.RowState == DataRowState.Deleted ? DataRowVersion.Original : DataRowVersion.Current] as string;
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (DataRow row in new ArrayList(table.Rows))
						{
							CObjetDonneeAIdNumerique objet = null;
                            if (lstEvenements.Count > 0)
							{
								if (row.RowState == DataRowState.Added ||
									row.RowState == DataRowState.Modified ||
                                    row.RowState == DataRowState.Deleted)
								{
									objet = (CObjetDonneeAIdNumerique)((CContexteDonnee)table.DataSet).GetNewObjetForRow(row);
                                    if (objet.Row.RowState == DataRowState.Deleted)
                                        objet.VersionToReturn = DataRowVersion.Original;
                                    if (objet.Row.RowState != DataRowState.Deleted && EvenementAttribute.HasEventsSpecifiques(objet))
                                        lstObjetsAvecEvenementsSpecifiques.Add(objet);

									if (typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(typeOriginal))
									{
                                        CRelationElementAChamp_ChampCustom rel = objet as CRelationElementAChamp_ChampCustom;
										objet = (CObjetDonneeAIdNumerique)((CRelationElementAChamp_ChampCustom)objet).ElementAChamps;
                                        if (objet.Row.RowState == DataRowState.Unchanged)
                                            objet.Row.Row.SetModified();
                                        if (objet.Row.RowState == DataRowState.Deleted)
                                            objet.VersionToReturn = DataRowVersion.Original;
                                        if (rel.ContexteDeModification.Length != 0 &&
                                            objet.ContexteDeModification.Length == 0)
                                            objet.ContexteDeModification = rel.ContexteDeModification;

									}
									if (!tableIdsVues.ContainsKey(objet.Id) || (
										listeARepasserPourTable != null &&  listeARepasserPourTable.ContainsKey ( objet.Id )))
									{
										tableIdsVues[objet.Id] = true;
										foreach (CEvenement evt in lstEvenements)
										{
											if (!traitement.ContainsCouple(evt, (CObjetDonneeAIdNumerique)objet))
											{
												//Attention, si c'est une valeur de champ custom, envoie la valeur,
												//c'est elle qui sera testée.
												CInfoDeclencheurProcess infoDeclencheur = null;
												if (evt.ShouldDeclenche((CObjetDonneeAIdNumerique)objet, ref infoDeclencheur))
												{
                                                    infoDeclencheur.DbKeyEvenementDeclencheur = evt.DbKey;
													infoDeclencheur.Info = evt.Libelle;
													traitement.AddCoupleEvenementObjet(evt, (CObjetDonneeAIdNumerique)objet, infoDeclencheur);
												}
											}
										}
									}

								}
							}

							//Regarde les handle d'évenement sur l'objet
							if (strPrimaryKey != "" && (row.RowState == DataRowState.Modified))
							{
								listeIdsElementsAVerifierHandlers.Add((int)row[strPrimaryKey]);
							}
						}
						if ( listeARepasserPourTable != null )
							listeARepasserPourTable.Clear();

						if (listeIdsElementsAVerifierHandlers.Count > 0 && bFirstPasse)
						{
							//traitement par paquet de 500
							for (int nIndexLot = 0; nIndexLot < listeIdsElementsAVerifierHandlers.Count; nIndexLot += 500)
							{
								int nMin = Math.Min(nIndexLot + 500, listeIdsElementsAVerifierHandlers.Count);
								StringBuilder bl = new StringBuilder();
								for (int nIndex = nIndexLot; nIndex < nMin; nIndex++)
								{
									bl.Append(listeIdsElementsAVerifierHandlers[nIndex].ToString());
									bl.Append(",");
								}
								string strIdsElementsAVerifierHandlers = bl.ToString().Substring(0, bl.ToString().Length - 1);
								//Recherche tous les handlers d'évenement pour les objets concernés
								CListeObjetsDonnees listeHandler = new CListeObjetsDonnees(contexte, typeof(CHandlerEvenement));
								listeHandler.Filtre = new CFiltreData(
									CHandlerEvenement.c_champIdCible + " in (" + strIdsElementsAVerifierHandlers + ") and " +
									CHandlerEvenement.c_champTypeCible + "=@1 and " +
									CHandlerEvenement.c_champTypeEvenement + "=@2",
									tpObjets.ToString(),
									(int)TypeEvenement.Modification);
								listeHandler.PreserveChanges = true;
								foreach (CHandlerEvenement handler in listeHandler)
								{
									if (handler.Row.RowState != DataRowState.Deleted)
									{
										CObjetDonneeAIdNumerique objetTmp = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tpObjets, new object[] { contexte });
										if (objetTmp.ReadIfExists(handler.IdElementCible))
										{
											CInfoDeclencheurProcess infoDeclencheur = null;
											if (handler.ShoulDeclenche(objetTmp, ref infoDeclencheur))
											{
												if (infoDeclencheur != null && handler.EvenementLie != null)
													infoDeclencheur.Info = handler.EvenementLie.Libelle;
												traitement.AddCoupleHandlerObjet(handler, (CObjetDonneeAIdNumerique)objetTmp, infoDeclencheur);
											}
										}
									}
								}
							}
						}
                        //Annule les évenements spécifiques, ils ont été traités !
                        foreach (CObjetDonnee objet in lstObjetsAvecEvenementsSpecifiques)
                            EvenementAttribute.ClearEvenements(objet);
					}
				}

				//Execute ce qui peut être executé tout de suite
				foreach (DataTable table in contexte.Tables)
					table.RowChanged += handlerRedo;
				listeElementsARepasser.Clear();
				foreach (CTraitementApresSauvegarde.CCoupleDeclencheurObjet couple in traitement.CouplesEvenementOuHandlerObjet)
				{
					if (couple.Objet is CObjetDonneeAIdNumerique &&
						 couple.PeutEtreExecuteSurLePosteClient)
					{
						result = couple.OnDeclencheSurClient();
                        if (!result)
                            return result;

					}
				}
				foreach (DataTable table in contexte.Tables)
					table.RowChanged -= handlerRedo;
                bFirstPasse = false;
			}

			if ( traitement.CouplesEvenementOuHandlerObjet.Count != 0 )
				tableData[c_cleDonneeListeTraitements] = traitement;
			m_listeElementsARepasser.Remove ( contexte );
            return result;
		}

		static void table_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			if ( e.Row.RowState != DataRowState.Deleted ) 
			{
				DataTable table = e.Row.Table;

				if ( table.DataSet is CContexteDonnee && table.PrimaryKey.Length == 1 && table.PrimaryKey[0].DataType == typeof(int))
				{
					Dictionary<string, Dictionary<int, bool>> listeARepasser = null;
					if ( m_listeElementsARepasser.TryGetValue((CContexteDonnee)table.DataSet, out listeARepasser ))
					{
						Dictionary<int, bool> listePourTable = null;
						if (!listeARepasser.TryGetValue(table.TableName, out listePourTable))
						{
							listePourTable = new Dictionary<int, bool>();
							listeARepasser[table.TableName] = listePourTable;
						}
						listePourTable[(int)e.Row[table.PrimaryKey[0]]] = true;
					}
				}
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////////
		//Contient les ids des handlers en cours d'execution pour éviter des appels récursifs
		private static Hashtable m_tableCouplesEnCoursExecution = new Hashtable();
		private static CResultAErreur TraitementApresSauvegardeExterne ( CContexteDonnee contexte, Hashtable tableData )
		{
            CResultAErreur result = CResultAErreur.True;

			CTraitementApresSauvegarde traitement  = (CTraitementApresSauvegarde )tableData[c_cleDonneeListeTraitements];
			if ( traitement == null )
                return result;
			foreach ( CTraitementApresSauvegarde.CCoupleDeclencheurObjet couple in traitement.CouplesEvenementOuHandlerObjet )
			{
				if ( m_tableCouplesEnCoursExecution[couple.CleUnique] == null )
				{
					m_tableCouplesEnCoursExecution[couple.CleUnique] = true;
					try
					{
						result = couple.OnDeclenche ( );
					}
					catch ( Exception e )
					{
						result.EmpileErreur(new CErreurException(e));
						result.EmpileErreur("Error in event @1 on @2|117", 
							(couple.InfoDeclencheur==null?"?":couple.InfoDeclencheur.Info),
							couple.Objet.DescriptionElement);
					}
					m_tableCouplesEnCoursExecution.Remove ( couple.CleUnique );
					if ( !result )
                        return result;
				}
			}
            return result;
		}

		/// /////////////////////////////////////////////////////////////////////////////////
		public static void Init()
		{
			CContexteDonneeServeur.AddTraitementAvantSauvegarde ( new TraitementSauvegardeExterne ( TraitementAvantSauvegardeExterne ) );
			CContexteDonneeServeur.AddTraitementApresSauvegarde ( new TraitementSauvegardeExterne ( TraitementApresSauvegardeExterne ) );
		}

		/// /////////////////////////////////////////////////////////////////////////////////
		private static void OnModifSurDonnees ( IDonneeNotification donnee )
		{
			
			if ( donnee is CDonneeNotificationModificationContexteDonnee )
			{
				CDonneeNotificationModificationContexteDonnee donneeContexte = (CDonneeNotificationModificationContexteDonnee)donnee;
				foreach ( CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in donneeContexte.ListeModifications )
				{
					if ( info.NomTable == CEvenement.c_nomTable )
					{
						m_bEvenementsChanged = true;
						lock ( typeof(CLockerCacheEvenements) )
						{
							m_tableTypeToEvenements.Clear();
						}
						break;
					}
				}
			}
			if ( donnee is CDonneeNotificationAjoutEnregistrement )
			{
				CDonneeNotificationAjoutEnregistrement donneeAjout = (CDonneeNotificationAjoutEnregistrement)donnee;
				if ( donneeAjout.NomTable == CEvenement.c_nomTable )
				{
					m_bEvenementsChanged = true;
					lock ( typeof(CLockerCacheEvenements) )
					{
						m_tableTypeToEvenements.Clear();
					}
				}
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////////
		protected static CContexteDonnee Contexte
		{
			get
			{
				if ( m_contexteCache == null )
				{
					m_contexteCache = new CContexteDonnee ( 0, true, true );
					m_recepteurModif = new CRecepteurNotification(0, typeof(CDonneeNotificationModificationContexteDonnee) );
					m_recepteurModif.OnReceiveNotification += new NotificationEventHandler ( OnModifSurDonnees );

					m_recepteurAjouts= new CRecepteurNotification ( 0, typeof(CDonneeNotificationAjoutEnregistrement) );
					m_recepteurAjouts.OnReceiveNotification += new NotificationEventHandler ( OnModifSurDonnees );
					
				}
				return m_contexteCache;
			}
		}



		

	}
}
