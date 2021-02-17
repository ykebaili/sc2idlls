using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using System.Reflection;
using sc2i.common;
using System.Collections;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Utilisé pour compatibilité, pour pouvoir interpreter les vieilles formules qui
	/// étaient autrefois interpretés avec le CFournisseurPropDynStd pour récuperer les
	/// valeurs de propriétés
	/// </summary>
	[AutoExec("Autoexec")]
	public class COldInterpreteurProprieteDynamique : IOldInterpreteurProprieteDynamique
	{
		CCacheValeursProprietes m_cache = new CCacheValeursProprietes();

		//Id session->ContexteDonnee
		private static Hashtable m_tableContextesDonneesParSessions = new Hashtable();

		//---------------------------------------
		public COldInterpreteurProprieteDynamique()
		{
			m_cache.CacheEnabled = false;
		}

		//---------------------------------------
		public static void Autoexec()
		{
			CContexteEvaluationExpression.SetOldInterpreteurProprieteDynamique(new COldInterpreteurProprieteDynamique());
			CInterpreteurProprieteDynamique.SetOldInterpreteur(new COldInterpreteurProprieteDynamique());
		}
			
		//---------------------------------------
		//Vide le cache de valeurs de proprietes pour l'objet demandé
		public void ResetCache(object objet)
		{
			m_cache.ResetCache(objet);
		}

		
		//-----------------------------------
		//Vide completement le cache
		public void ResetCache()
		{
			m_cache.ResetCache();
		}

		//-----------------------------------
		//Active ou désactive le cache de valeurs de propriétés
		public bool CacheEnabled
		{
			get
			{
				return m_cache.CacheEnabled;
			}
			set
			{
				m_cache.CacheEnabled = value;
			}
		}

		/// ///////////////////////////////////////////////////////////
		private static CContexteDonnee ContexteDonneeCache
		{
			get
			{
				CSessionClient session = CSessionClient.GetSessionUnique();
				CContexteDonnee contexte = null;
				if ( session != null )
				{
					int nIdSession = session.IdSession;
					bool bIsLocale = CSessionClient.IsSessionLocale(session.IdSession);
					if (!bIsLocale)
						nIdSession = 0;
					contexte = (CContexteDonnee)m_tableContextesDonneesParSessions[nIdSession];
					if ( contexte == null )
					{
						contexte = new CContexteDonnee(nIdSession, true, true);
						contexte.GestionParTablesCompletes = true;
						if ( nIdSession != 0 )
							((CSessionClient)session).OnCloseSession += new EventHandler(session_OnCloseSession);
						m_tableContextesDonneesParSessions[nIdSession] = contexte;
					}
				}
				return contexte;
			}
		}

		/// ///////////////////////////////////////////////////////////
		public static void session_OnCloseSession(object sender, EventArgs e)
		{
			if (sender is CSessionClient)
			{
				CSessionClient session = (CSessionClient)sender;
				try
				{
					CContexteDonnee contexte = (CContexteDonnee)m_tableContextesDonneesParSessions[session.IdSession];
					contexte.Dispose();
				}
				catch { }
				m_tableContextesDonneesParSessions[session.IdSession] = null;
			}
		}

		/// ///////////////////////////////////////////////////////////
		public object GetValue(object objetInterroge, string strPropriete)
		{
			CFournisseurPropDynStd fournisseur = new CFournisseurPropDynStd(false);
			if (objetInterroge == null)
				return null;
			//Est-ce une propriété simple ?(majorité des cas )
			object objetAInterroger = null;
			MemberInfo membre = null;
			if (CInterpreteurTextePropriete.GetObjetFinalEtMemberInfo(objetInterroge, strPropriete, ref objetAInterroger, ref membre) && membre != null)
			{
				return CInterpreteurTextePropriete.GetValue(objetAInterroger, membre);
			}

			//Bon pas de bol, c'est autre chose, il faut donc chercher ce que c'est
			Type tp = objetInterroge.GetType();
			string strPropDebut = strPropriete.Split('.')[0];
			object objetPremier = CInterpreteurTextePropriete.GetValue(objetInterroge, strPropDebut);
			if (objetPremier == null)
			{
				string strIdChamp = CDefinitionProprieteDynamiqueChampCustom.GetIdPropriete(strPropDebut);
				foreach (CDefinitionProprieteDynamique def in fournisseur.GetDefinitionsChamps(tp, 0))
					if (def.NomPropriete == strPropDebut ||
                        (strIdChamp != "" && def is CDefinitionProprieteDynamiqueChampCustom) &&
							((CDefinitionProprieteDynamiqueChampCustom)def).DbKeyChamp == CDbKey.CreateFromStringValue(strIdChamp))
					{
						objetPremier = GetValue(objetInterroge, def);
						//Si la suite est une relation de la valeur champ vers l'objet, il ne faut pas
						//traier la suite
						if (strPropDebut != strPropriete)
						{
							string strSuiteTmp = strPropriete.Substring(strPropDebut.Length + 1);
							if (CInfoRelationComposantFiltreChampToEntite.IsRelationFromChampToEntite(strSuiteTmp))
							{
								//On a déjà traité ce lien par GetValeurPropriété (qui retourne la valeur
								//et nom pas le lien vers la valeur)
								strPropDebut += "." + strPropriete.Split('.')[1];
							}
						}

						break;
					}
			}
			if (objetPremier == null)
				return null;
			if (strPropDebut == strPropriete)
				return objetPremier;
			string strSuite = strPropriete.Substring(strPropDebut.Length + 1);
			return GetValue(objetPremier, strSuite);
		}

		/// ///////////////////////////////////////////////////////////
		public object GetValue(object objetInterroge, CDefinitionProprieteDynamique propriete)
		{
			//Récupère l'objet à interroger
			string strProp = propriete.NomPropriete;
			object valeur = m_cache.GetValeurCache (objetInterroge, strProp);
			if (valeur != null)
				return valeur;

			CContexteDonnee contexteForObjets = ContexteDonneeCache;
			if (objetInterroge is IObjetAContexteDonnee)
				contexteForObjets = ((IObjetAContexteDonnee)objetInterroge).ContexteDonnee;
			if (contexteForObjets == null)
				contexteForObjets = ContexteDonneeCache;


			object objetFinal = objetInterroge;
			int nPos = strProp.LastIndexOf('.');

			if (nPos > 0 && !(propriete is CDefinitionProprieteDynamiqueDonneeCumulee) &&
				 !(propriete is CDefinitionProprieteDynamiqueChampCustom) &&
				 !(propriete is CDefinitionProprieteDynamiqueChampCustomFils) &&
				 !(propriete is CDefinitionProprieteDynamiqueFormule))
			{
				string strPropDebut;
				strPropDebut = strProp.Substring(0, nPos);
				strProp = strProp.Substring(nPos + 1);
				objetFinal = m_cache.GetValeurCache(objetInterroge, strPropDebut);
				if (objetFinal == null)
				{
					objetFinal = GetValue(objetInterroge, strPropDebut);
					/*object objetAInterroger = null;
					MemberInfo membre = null;
					CInterpreteurTexteProptVariete.GetObjetFinalEtMemberInfo ( objetInterroge, strPropDebut, ref objetAInterroger, ref membre );
					if ( membre != null )
					{
						objetFinal = CInterpreteurTextePropriete.GetValue ( objetAInterroger, membre );
					}
					else
					{
						//On n'a pas trouvé le membre, il s'agit donc d'autre chose, il faut donc décomposer
						//Le champ 
					}*/
					//objetFinal = CInterpreteurTextePropriete.GetValue(objetInterroge, strPropDebut);
					m_cache.StockeValeurEnCache(objetInterroge, strPropDebut, objetFinal);
				}
				if (objetFinal == null)
					return null;
				valeur = m_cache.GetValeurCache(objetFinal, strProp);
				if (valeur != null)
					return valeur;
			}


			if (propriete is CDefinitionProprieteDynamiqueChampCustom)
			{
				if (!(objetFinal is IElementAChamps))
				{
					valeur = null;
				}

				else
				{
                    //TESTDBKEYTODO qui ne peut pas marche
					valeur = ((IElementAChamps)objetFinal).GetValeurChamp(((CDefinitionProprieteDynamiqueChampCustom)propriete).DbKeyChamp.InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn.Value, System.Data.DataRowVersion.Default);
					/*if (m_bValeurAfficheeDeChampsCustom)
					{
						CChampCustom champ = new CChampCustom(ContexteDonneeCache);
						if (champ.ReadIfExists(((CDefinitionProprieteDynamiqueChampCustom)propriete).IdChamp))
						{
							object valTmp = champ.DisplayFromValue(valeur);
							if (valTmp != null)
								valeur = valTmp;
						}
					}*/
				}
			}
			else if (propriete is CDefinitionProprieteDynamiqueChampCustomFils && objetInterroge is CObjetDonneeAIdNumerique)
			{
				CChampCustom champ = new CChampCustom(ContexteDonneeCache);
				if (champ.ReadIfExists(((CDefinitionProprieteDynamiqueChampCustomFils)propriete).KeyChamp))
				{
					Type tp = champ.Role.TypeAssocie;

					try
					{
						IObjetDonneeAChamps elt = (IObjetDonneeAChamps)Activator.CreateInstance(tp, new object[] { contexteForObjets });
						CRelationElementAChamp_ChampCustom rel = elt.GetNewRelationToChamp();
						CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteForObjets, tp);
						liste.Filtre = new CFiltreDataAvance(
							elt.GetNomTable(),
							rel.GetNomTable() + "." +
							CRelationElementAChamp_ChampCustom.c_champValeurString + "=@1 and " +
							rel.GetNomTable() + "." +
							CRelationElementAChamp_ChampCustom.c_champValeurInt + "=@2 and " +
							rel.GetNomTable() + "." +
							CChampCustom.c_champId + "=@3",
							objetInterroge.GetType().ToString(),
							((CObjetDonneeAIdNumerique)objetInterroge).Id,
							champ.Id);
						valeur = liste.ToArray(tp);
					}
					catch
					{
						valeur = null;
					}
				}
			}
			else if (propriete is CDefinitionProprieteDynamiqueListeEntites)
			{
				CListeEntites liste = new CListeEntites(contexteForObjets);
				try
				{
					if (liste.ReadIfExists(((CDefinitionProprieteDynamiqueListeEntites)propriete).DbKeyListeEntite))
					{
						CListeObjetsDonnees resultListe = liste.GetElementsLiesFor(objetInterroge);
						if (resultListe != null)
							valeur = resultListe.ToArray(liste.TypeElements);
					}
				}
				catch
				{
					valeur = null;
				}
			}


			else if (propriete is CDefinitionProprieteDynamiqueRelationTypeId)
			{
				if (!(objetFinal is CObjetDonneeAIdNumerique))
					valeur = null;
				else
				{
					CDefinitionProprieteDynamiqueRelationTypeId defRel = (CDefinitionProprieteDynamiqueRelationTypeId)propriete;
					return ((CObjetDonneeAIdNumerique)objetFinal).GetDependancesRelationTypeId(
						defRel.Relation.TableFille,
						defRel.Relation.ChampType,
						defRel.Relation.ChampId,
						false);
				}
			}
			else if (propriete is CDefinitionProprieteDynamiqueDonneeCumulee)
			{
				if (!(objetFinal is CObjetDonneeAIdNumerique))
					valeur = null;
				else
				{
					CDefinitionProprieteDynamiqueDonneeCumulee defType = (CDefinitionProprieteDynamiqueDonneeCumulee)propriete;
					CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)objetFinal;
					CTypeDonneeCumulee type = new CTypeDonneeCumulee(contexteForObjets);
					if (type.ReadIfExists(defType.DbKeyTypeDonnee))
						valeur = type.GetDonneesCumuleesForObjet(objet);
					else
						valeur = null;
				}
			}
			else if (propriete is CDefinitionProprieteDynamiqueChampCalcule)
			{
				CChampCalcule champ = new CChampCalcule(contexteForObjets);
				if (champ.ReadIfExists(((CDefinitionProprieteDynamiqueChampCalcule)propriete).DbKeyChamp))
				{
					valeur = champ.Calcule(objetFinal, new CFournisseurPropDynStd(true));
				}
				else
					valeur = null;
			}
			else if (propriete is CDefinitionProprieteDynamiqueFormule)
			{
				CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(objetInterroge);
				contexte.AttacheObjet(typeof(CContexteDonnee), contexteForObjets);
				CResultAErreur result = ((CDefinitionProprieteDynamiqueFormule)propriete).Formule.Eval(contexte);
				if (result)
					return result.Data;
			}
			else if ( propriete is CDefinitionProprieteDynamiqueVariableDynamique && objetFinal is IElementAVariables)
			{
				valeur = ((IElementAVariables)objetFinal).GetValeurChamp(((CDefinitionProprieteDynamiqueVariableDynamique)propriete).IdChamp);
			}
			else
				valeur = CInterpreteurTextePropriete.GetValue(objetFinal, strProp);
			m_cache.StockeValeurEnCache(objetInterroge, propriete.Nom, valeur);
			if (objetFinal != objetInterroge)
				m_cache.StockeValeurEnCache(objetFinal, strProp, valeur);
			return valeur;
		}
	}
}
