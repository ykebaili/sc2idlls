using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.expression;
using sc2i.data;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Fournit la liste des champs correspondant à un type
	/// </summary>
	[Serializable]
	public class CFournisseurProprietesForFiltreDynamique : IFournisseurProprietesDynamiques
	{
		private static CContexteDonnee m_contexteDonneeCache = new CContexteDonnee(0, true, false);

		/// ///////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux )
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}
		/// ///////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente )
		{
			ArrayList lstProprietes = new ArrayList();
			GetDefinitionsChamps ( tp, nNbNiveaux, lstProprietes, "", "", defParente );
			lstProprietes.Sort();
			return (CDefinitionProprieteDynamique[])lstProprietes.ToArray(typeof(CDefinitionProprieteDynamique));
		}

		/// ///////////////////////////////////////////////////////////
		protected virtual void GetDefinitionsChamps ( Type tp, int nProfondeur, ArrayList lstProps, string strCheminConvivial, string strCheminReel, CDefinitionProprieteDynamique defParente )
		{
			/*ITraducteurNomChamps traducteur = null;
			if ( defParente is ITraducteurNomChamps )
				traducteur = (ITraducteurNomChamps)defParente;*/
			if ( nProfondeur < 0 )
				return;

			CStructureTable structure = null;
			try
			{
				structure = CStructureTable.GetStructure(tp);
			}
			catch 
			{
				return;
			}
			foreach ( CInfoChampTable info in structure.Champs )
			{
				//Trouve la méthode correspondante

                //18/09 bug : si un classe générique surcharge une propriété de sa classe de base,
                //GetProperty retourne une erreur sur cette propriété
				PropertyInfo propInfo = GetPropertySafe ( tp, info.Propriete );
				if ( propInfo != null )
				{
					CDefinitionProprieteDynamique def =new CDefinitionProprieteDynamiqueDotNet ( 
						strCheminConvivial+info.NomConvivial,
						strCheminReel+info.Propriete,
						new CTypeResultatExpression ( propInfo.PropertyType, false ), false, true,
						""
						);
					AddDefinition ( lstProps, def/*, traducteur*/ );
					GetDefinitionsChamps ( propInfo.PropertyType, nProfondeur-1, lstProps, strCheminConvivial+info.NomConvivial+".", strCheminReel+info.Propriete+".", def );
				}
			}

			//Ajoute les relations parentes
			foreach ( CInfoRelation relation in structure.RelationsParentes )
			{
				PropertyInfo propInfo = GetPropertySafe ( tp,relation.Propriete );
				if ( propInfo != null )
				{
					CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueRelation (
						strCheminConvivial+relation.NomConvivial,
						strCheminReel+relation.Propriete,
						relation, 
						new CTypeResultatExpression ( propInfo.PropertyType, false ));
					AddDefinition ( lstProps, def/*, traducteur*/ );
				}
			}
			//Ajoute les relations filles
			foreach ( CInfoRelation relation in structure.RelationsFilles )
			{
                PropertyInfo propInfo = GetPropertySafe(tp, relation.Propriete);
				if ( propInfo != null )
				{
					object[] attribs = propInfo.GetCustomAttributes ( typeof(RelationFilleAttribute), true);
					if ( attribs.Length != 0 )
					{
                        
                        string strNomConvivial = strCheminConvivial + relation.NomConvivial;
                        object[] attrsDynCh = propInfo.GetCustomAttributes(typeof(DynamicChildsAttribute), true);
                        if (attrsDynCh.Length != 0)
                        {
                            DynamicChildsAttribute dynAtt = attrsDynCh[0] as DynamicChildsAttribute;
                            strNomConvivial = strCheminConvivial + dynAtt.NomConvivial;
                        }
                            
						Type tpFille = ((RelationFilleAttribute)attribs[0]).TypeFille;
						//Pas les relations aux champs custom, elles sont gerées en dessous
                        //30/11/2011 stef : Euh, en fait, si, ça peut servir ! pour accélerer des filtres
						/*if ( !tpFille.IsSubclassOf ( typeof(CRelationElementAChamp_ChampCustom) ))*/
							AddDefinition ( lstProps,
								new CDefinitionProprieteDynamiqueRelation (
                                    strNomConvivial,
								strCheminReel+relation.Propriete,
								relation, 
								new CTypeResultatExpression ( tpFille, true ))/*,
								traducteur*/);
					}
				}
			}
			
			CRoleChampCustom role = CRoleChampCustom.GetRoleForType( tp );
			if ( role != null )
			{
                CListeObjetsDonnees listeChamps = CChampCustom.GetListeChampsForRole(m_contexteDonneeCache, role.CodeRole);
				foreach ( CChampCustom champ in listeChamps )
				{
					CDefinitionProprieteDynamiqueChampCustom def = new CDefinitionProprieteDynamiqueChampCustom ( champ );
					AddDefinition ( lstProps, def/*, traducteur*/ );
				}
			}

			//Ajoute les relations TypeId
			foreach ( RelationTypeIdAttribute relation in CContexteDonnee.RelationsTypeIds )
			{
				if ( relation.NomConvivialPourParent != "" && relation.IsAppliqueToType ( tp ) )
				{
					AddDefinition ( lstProps, new CDefinitionProprieteDynamiqueRelationTypeId(relation)/*, traducteur*/ );
				}
			}

			//Ajoute les données cumulées
			CTypeDonneeCumulee[] donnees = CFournisseurPropDynStd.GetTypesDonneesPourType ( tp );
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

			//Liens sur champs custom fils
			//Liens sur champs custom
			CListeObjetsDonnees listeChampsFils = new CListeObjetsDonnees(m_contexteDonneeCache, typeof(CChampCustom));
			listeChampsFils.Filtre = new CFiltreData(
				CChampCustom.c_champTypeObjetDonnee + "=@1",
				tp.ToString());
			foreach (CChampCustom champ in listeChampsFils)
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

        private PropertyInfo GetPropertySafe(Type tp, string strNomPropriete)
        {
            try
            {
                PropertyInfo info = tp.GetProperty(strNomPropriete);
                return info;
            }
            catch
            {
                foreach (PropertyInfo propInfo in tp.GetProperties(BindingFlags.DeclaredOnly))
                {
                    if (propInfo.Name == strNomPropriete)
                        return propInfo;
                }
                foreach (PropertyInfo propInfo in tp.GetProperties())
                {
                    if (propInfo.Name == strNomPropriete)
                        return propInfo;
                }
            }
            return null;
        }

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            if (objet != null)
            {
                lst.AddRange(GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente));
                try
                {
                    if (objet.ElementAVariableInstance != null)
                        lst.AddRange(objet.ElementAVariableInstance.GetProprietesInstance());
                }
                catch { }
            }
			return lst.ToArray();
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



		/// ///////////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( string strCategorie, Type tp, int nNbNiveaux )
		{
			return GetDefinitionsChamps( tp, nNbNiveaux );
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefintionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			if (objet != null)
			{
				lst.AddRange(GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente));
			}
			return lst.ToArray();
		}


	}
}
