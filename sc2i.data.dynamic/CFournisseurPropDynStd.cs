using System;
using System.Reflection;
using System.Collections;

using sc2i.expression;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.data;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CFournisseurPropDynStd.
	/// </summary>
	[Serializable]
	public class CFournisseurPropDynStd : IFournisseurProprietesDynamiques
	{
		private bool m_bAvecMethodes = false;
	
        //Id session->ContexteDonnee
		private static Hashtable m_tableContextesDonneesParSessions = new Hashtable();
		
		private static Hashtable m_tableTypeToDonneesCumulees = null;

		private static CRecepteurNotification m_recepteurNotificationModifTypeDonneeCumulee = null;
		private static CRecepteurNotification m_recepteurNotificationAjoutTypeDonneeCumulee = null;

		private bool m_bAvecReadOnly = true;

		/// ///////////////////////////////////////////////////////////
		public CFournisseurPropDynStd( )
		{
			m_bAvecMethodes = false;
		}

		/// ///////////////////////////////////////////////////////////
		public CFournisseurPropDynStd( bool bAvecMethodes )
		{
			m_bAvecMethodes = bAvecMethodes;
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
		public virtual CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nProfondeur )
		{
			return GetDefinitionsChamps ( tp, nProfondeur, null );
		}

		/// ///////////////////////////////////////////////////////////
		public virtual CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nProfondeur, CDefinitionProprieteDynamique defParente )
		{
			CFournisseurGeneriqueProprietesDynamiques fournisseur = new CFournisseurGeneriqueProprietesDynamiques();
			fournisseur.AvecReadOnly = AvecReadOnly;
			return fournisseur.GetDefinitionsChamps(tp, nProfondeur, defParente);
			/*
			ArrayList lstProprietes = new ArrayList();
			GetDefinitionsChamps ( tp, nProfondeur, lstProprietes, "", "", defParente );
			lstProprietes.Sort();
			return (CDefinitionProprieteDynamique[])lstProprietes.ToArray(typeof(CDefinitionProprieteDynamique));*/
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			CFournisseurGeneriqueProprietesDynamiques fournisseur = new CFournisseurGeneriqueProprietesDynamiques();
			fournisseur.AvecReadOnly = AvecReadOnly;
			return fournisseur.GetDefinitionsChamps(objet, defParente);
			/*
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			if (objet != null)
			{
				lst.AddRange(GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente));
				if (objet.ElementAVariableInstance != null)
					lst.AddRange(objet.ElementAVariableInstance.GetProprietesInstance());
			}
			return lst.ToArray();
			 */
		}

		/// ///////////////////////////////////////////////////////////
		public virtual bool HasSubProperties ( Type tp )
		{
			if ( tp.IsSubclassOf(typeof(CObjetDonnee) ) )
				return true;
			foreach ( PropertyInfo info in tp.GetProperties() )
			{
				object[] attribs = info.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
				if ( attribs.Length == 1 )
					return true;
			}
			CRoleChampCustom role = CRoleChampCustom.GetRoleForType( tp );
			if ( role != null )
			{
                CListeObjetsDonnees listeChamps = CChampCustom.GetListeChampsForRole(ContexteDonneeCache, role.CodeRole);
				if ( listeChamps.Count > 0 )
					return true;
			}
			return false;
		}

		/// ///////////////////////////////////////////////////////////
		protected virtual void GetDefinitionsChamps ( Type tp, int nProfondeur, ArrayList lstProps, string strCheminConvivial, string strCheminReel )
		{
			GetDefinitionsChamps ( tp, nProfondeur, lstProps, strCheminConvivial, strCheminReel, null );
		}

		/// ///////////////////////////////////////////////////////////
		protected virtual void GetDefinitionsChamps ( 
			Type tp, 
			int nProfondeur, 
			ArrayList lstProps, 
			string strCheminConvivial, 
			string strCheminReel, 
			CDefinitionProprieteDynamique definitionParente
			 )
		{
			/*ITraducteurNomChamps traducteur = null;
			if ( definitionParente is ITraducteurNomChamps )
				traducteur = (ITraducteurNomChamps)definitionParente;*/
			if ( nProfondeur < 0 )
				return;
			if ( tp == null )
				return;

			//Proprietes
			foreach ( PropertyInfo info in tp.GetProperties() )
			{
				object[] attribs = info.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
				if (attribs.Length == 1)
				{
					DynamicFieldAttribute attrib = (DynamicFieldAttribute)attribs[0];
					bool bReadOnly = info.GetSetMethod() == null;
					Type tpProp = info.PropertyType;
					bool bIsArray = tpProp.IsArray;
					if (bIsArray)
						tpProp = tpProp.GetElementType();
					bool bHasSubProprietes = HasSubProperties(tpProp);
					if (m_bAvecReadOnly || !bReadOnly || bHasSubProprietes)
					{
						CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueDotNet(
							strCheminConvivial + attrib.NomConvivial,
							strCheminReel + info.Name,
							new CTypeResultatExpression(tpProp, bIsArray),
							bHasSubProprietes,
							bReadOnly,
							attrib.Rubrique);
						if (AddDefinition(lstProps, def/*, traducteur*/))
							GetDefinitionsChamps(info.PropertyType, nProfondeur - 1, lstProps, strCheminConvivial + attrib.NomConvivial + ".", strCheminReel + info.Name + ".", def);
					}
				}
				attribs = info.GetCustomAttributes(typeof(DynamicChildsAttribute), true);
				{
					if (attribs.Length == 1)
					{
						DynamicChildsAttribute attrib = (DynamicChildsAttribute)attribs[0];
						CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueDotNet(
							strCheminConvivial + attrib.NomConvivial,
							strCheminReel + info.Name,
							new CTypeResultatExpression(attrib.TypeFils, true)
							, true,
							true,
							attrib.Rubrique);
						if (AddDefinition(lstProps, def/*, traducteur*/))
							GetDefinitionsChamps(attrib.TypeFils, nProfondeur - 1, lstProps, strCheminConvivial + attrib.NomConvivial + ".", strCheminReel + info.Name + ".", def);
					}
				}
			}

			//Champs custom
			CRoleChampCustom role = CRoleChampCustom.GetRoleForType( tp );
			if ( role != null )
			{
                CListeObjetsDonnees listeChamps = CChampCustom.GetListeChampsForRole(ContexteDonneeCache, role.CodeRole);
				foreach ( CChampCustom champ in listeChamps )
				{
					CDefinitionProprieteDynamiqueChampCustom def = new CDefinitionProprieteDynamiqueChampCustom ( champ );
					if (champ.Categorie.Trim() != "")
						def.Rubrique = champ.Categorie;
					else
						def.Rubrique =I.T("Complementary informations|59");
					AddDefinition ( lstProps, def/*, traducteur*/ );
				}
			}

			//Champs calculés
			if ( AvecReadOnly )
			{
				CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonneeCache, typeof(CChampCalcule));
				liste.Filtre = new CFiltreData(CChampCalcule.c_champTypeObjets +"=@1", tp.ToString() );
				foreach ( CChampCalcule champ in liste )
				{
					CDefinitionProprieteDynamiqueChampCalcule def = new CDefinitionProprieteDynamiqueChampCalcule ( champ );
					def.HasSubProperties = HasSubProperties ( def.TypeDonnee.TypeDotNetNatif );
					def.Rubrique = I.T("Complementary informations|59");
					AddDefinition ( lstProps, def/*, traducteur*/ );
				}
			}

			//Méthodes
			if ( m_bAvecMethodes && AvecReadOnly)
			{
				//Va chercher les propriétés
				foreach ( MethodInfo methode in tp.GetMethods() )
				{
					object[] attribs = methode.GetCustomAttributes(typeof(DynamicMethodAttribute), true);
					if ( attribs.Length == 1 )
					{
						DynamicMethodAttribute attrib = (DynamicMethodAttribute)attribs[0];
						CTypeResultatExpression typeRes = new CTypeResultatExpression(methode.ReturnType, false);
						if ( methode.ReturnType.HasElementType )
							typeRes = new CTypeResultatExpression(methode.ReturnType.GetElementType(), true );
						CDefinitionProprieteDynamique def = new CDefinitionMethodeDynamique(
							strCheminConvivial+methode.Name, 
							strCheminReel+methode.Name, 
							typeRes,
							HasSubProperties ( methode.ReturnType ),
							attrib.Descriptif,
							attrib.InfosParametres);
						def.Rubrique = I.T("Methods|58");
						lstProps.Add ( def );
						GetDefinitionsChamps ( methode.ReturnType, nProfondeur-1, lstProps, strCheminConvivial+methode.Name+".", strCheminReel+methode.Name+".", def );
					}
				}
				foreach ( CMethodeSupplementaire methode in CGestionnaireMethodesSupplementaires.GetMethodsForType ( tp ) )
				{
					CDefinitionProprieteDynamique def = new CDefinitionMethodeDynamique(
						strCheminConvivial+methode.Name, 
						strCheminReel+methode.Name, 
						new CTypeResultatExpression(methode.ReturnType, methode.ReturnArrayOfReturnType),
						HasSubProperties ( methode.ReturnType ));
					def.Rubrique = I.T("Methods|58");
					lstProps.Add ( def );
					GetDefinitionsChamps ( methode.ReturnType, nProfondeur-1, lstProps, strCheminConvivial+methode.Name+".", strCheminReel+methode.Name+".", def );
				}
			}

			//Relations TypeID
			foreach ( RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds )
			{
				if ( relation.NomConvivialPourParent != "" && relation.IsAppliqueToType ( tp ) )
				{
					CDefinitionProprieteDynamiqueRelationTypeId def = new CDefinitionProprieteDynamiqueRelationTypeId(relation);
					AddDefinition ( lstProps, def/*, traducteur*/ );
				}
			}

			//Données ucmulées
			if ( m_bAvecReadOnly )
			{
				CTypeDonneeCumulee[] donnees = GetTypesDonneesPourType ( tp );
				foreach ( CTypeDonneeCumulee typeDonnee in donnees )
				{
					int nCle = 0;
					foreach ( CCleDonneeCumulee cle in typeDonnee.Parametre.ChampsCle )
					{
						if ( cle.TypeLie == tp )
						{
							CDefinitionProprieteDynamiqueDonneeCumulee def = 
								new CDefinitionProprieteDynamiqueDonneeCumulee ( 
								typeDonnee,
								nCle );
							AddDefinition ( lstProps, def/*, traducteur*/ );
							break;
						}
					}
				}
			}

			//Liens sur champs custom fils
			if (m_bAvecReadOnly)
			{
				//Liens sur champs custom
				CListeObjetsDonnees listeChamps = new CListeObjetsDonnees(ContexteDonneeCache, typeof(CChampCustom));
				listeChamps.Filtre = new CFiltreData(
					CChampCustom.c_champTypeObjetDonnee + "=@1",
					tp.ToString());
				foreach (CChampCustom champ in listeChamps)
				{
					CDefinitionProprieteDynamiqueChampCustomFils def = new CDefinitionProprieteDynamiqueChampCustomFils(
						champ);
					if (champ.Categorie.Trim() != "")
						def.Rubrique = champ.Categorie;
					else
						def.Rubrique = I.T("Complementary informations|59");
					AddDefinition(lstProps, def/*, traducteur*/);
				}
			}

			//Liens par CListeEntite
			if (m_bAvecReadOnly)
			{
				CListeObjetsDonnees listeEntites = new CListeObjetsDonnees(ContexteDonneeCache, typeof(CListeEntites));
				listeEntites.Filtre = new CFiltreData(
					CListeEntites.c_champTypeElementSourceDeRecherche + "=@1",
					tp.ToString());
				foreach (CListeEntites liste in listeEntites)
				{
					CDefinitionProprieteDynamiqueListeEntites def = new CDefinitionProprieteDynamiqueListeEntites(liste);
					def.Rubrique = I.T("Lists|60");
					AddDefinition(lstProps, def/*, traducteur*/);
				}
			}
				
		}

		/// ///////////////////////////////////////////////////////////
		private bool AddDefinition ( ArrayList lst, CDefinitionProprieteDynamique def/*, ITraducteurNomChamps traducteur*/ )
		{
			/*if ( traducteur != null )
				def = traducteur.GetDefinitionConvertie(def);*/
			if ( def != null )
				lst.Add ( def );
			return def != null;
		}

		/// ///////////////////////////////////////////////////////////
		public bool AvecReadOnly
		{
			get
			{
				return m_bAvecReadOnly;
			}
			set
			{
				m_bAvecReadOnly = value;
			}
		}


		/// <summary>
		/// </summary>
		/// <param name="donnee"></param>
		private static void m_recepteurNotificationTypeDonneeCumulee_OnReceiveNotification(IDonneeNotification donnee)
		{
			lock ( typeof(CFournisseurPropDynStd) )
			{
				if ( donnee is CDonneeNotificationAjoutEnregistrement )
				{
					if ( ((CDonneeNotificationAjoutEnregistrement)donnee).NomTable == CTypeDonneeCumulee.c_nomTable )
						m_tableTypeToDonneesCumulees = null;
				}
				else if (donnee is CDonneeNotificationModificationContexteDonnee )
				{
					foreach ( CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in 
						((CDonneeNotificationModificationContexteDonnee)donnee).ListeModifications )
						if ( info.NomTable == CTypeDonneeCumulee.c_nomTable )
						{
							m_tableTypeToDonneesCumulees = null;
							return;
						}
				}
			}
		}

		/// ///////////////////////////////////////////////////////////
		private static void UpdateTableDonneesCumulees()
		{
			int nIdSession = 0;
			try
			{
				nIdSession = CSessionClient.GetSessionUnique().IdSession;
			}
			catch
			{}
			if ( m_recepteurNotificationModifTypeDonneeCumulee == null )
			{
				m_recepteurNotificationModifTypeDonneeCumulee = new CRecepteurNotification ( nIdSession, typeof(CDonneeNotificationModificationContexteDonnee));
				m_recepteurNotificationModifTypeDonneeCumulee.OnReceiveNotification +=new NotificationEventHandler(m_recepteurNotificationTypeDonneeCumulee_OnReceiveNotification);
			}
			if ( m_recepteurNotificationAjoutTypeDonneeCumulee == null )
			{
				m_recepteurNotificationAjoutTypeDonneeCumulee = new CRecepteurNotification ( nIdSession, typeof(CDonneeNotificationAjoutEnregistrement));
				m_recepteurNotificationAjoutTypeDonneeCumulee.OnReceiveNotification +=new NotificationEventHandler(m_recepteurNotificationTypeDonneeCumulee_OnReceiveNotification);
			}
			if ( m_tableTypeToDonneesCumulees != null )
				m_tableTypeToDonneesCumulees.Clear();
			else
				m_tableTypeToDonneesCumulees = new Hashtable();
			CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonneeCache, typeof(CTypeDonneeCumulee));
			foreach ( CTypeDonneeCumulee type in liste )
			{
				CParametreDonneeCumulee parametre = type.Parametre;
				foreach ( CCleDonneeCumulee cle in parametre.ChampsCle )
				{
					if ( cle.TypeLie != null )
					{
						ArrayList lst = (ArrayList)m_tableTypeToDonneesCumulees[cle.TypeLie];
						if ( lst == null )
						{
							lst = new ArrayList();
							m_tableTypeToDonneesCumulees[cle.TypeLie] = lst;
						}
						lst.Add ( type );
					}
				}
			}
		}

		/// ///////////////////////////////////////////////////////////
		public static CTypeDonneeCumulee[] GetTypesDonneesPourType ( Type tp )
		{
			if ( m_tableTypeToDonneesCumulees == null )
				UpdateTableDonneesCumulees();
			ArrayList lst = (ArrayList)m_tableTypeToDonneesCumulees[tp];
			if ( lst == null )
				return new CTypeDonneeCumulee[0];
			return (CTypeDonneeCumulee[])lst.ToArray(typeof(CTypeDonneeCumulee));
		}

		/// ///////////////////////////////////////////////////////////
		public static void session_OnCloseSession(object sender, EventArgs e)
		{
			if ( sender is CSessionClient )
			{
				CSessionClient session = (CSessionClient)sender;
				try
				{
					CContexteDonnee contexte = (CContexteDonnee)m_tableContextesDonneesParSessions[session.IdSession];
					contexte.Dispose();
				}
				catch{}
				m_tableContextesDonneesParSessions[session.IdSession] = null;
			}
		}
	}

	
}
