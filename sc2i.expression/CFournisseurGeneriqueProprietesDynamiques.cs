using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{
	public interface IFournisseurProprieteDynamiquesSimplifie
	{
		CDefinitionProprieteDynamique[] GetDefinitionsChamps(
			CObjetPourSousProprietes objet, 
			CDefinitionProprieteDynamique defParente);
	}

	public class CFournisseurGeneriqueProprietesDynamiques : IFournisseurProprietesDynamiques
	{
		private static List<IFournisseurProprieteDynamiquesSimplifie> m_listeSousFournisseurs = new List<IFournisseurProprieteDynamiquesSimplifie>();

		bool m_bAvecReadOnly = true;

		private static CAssociationsObjetsSupplementairesPourChamps m_associationsObjetsSupplementaires = new CAssociationsObjetsSupplementairesPourChamps();


        //--------------------------------------------------------------------------------------
		public static void AssocieObjetSupplementaire(object objetPrincipal, CObjetPourSousProprietes objetSupplementaire)
		{
			m_associationsObjetsSupplementaires.AssocieObjet(objetPrincipal, objetSupplementaire);
		}

        //--------------------------------------------------------------------------------------
		public static void DissocieObjetSupplementaire(object objetPrincipal, CObjetPourSousProprietes objetSupplementaire)
		{
			m_associationsObjetsSupplementaires.DissocieObjet(objetPrincipal, objetSupplementaire);
		}

        //--------------------------------------------------------------------------------------
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

        //--------------------------------------------------------------------------------------
		public static void RegisterTypeFournisseur(IFournisseurProprieteDynamiquesSimplifie fournisseur)
		{
			foreach (IFournisseurProprieteDynamiquesSimplifie fourTest in m_listeSousFournisseurs)
				if (fournisseur.GetType() == fourTest.GetType())
					return;
			m_listeSousFournisseurs.Add(fournisseur);
		}

		//------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
		{
			return GetDefinitionsChamps(typeInterroge, nNbNiveaux, null);
		}

		//------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			CObjetPourSousProprietes objet = new CObjetPourSousProprietes(typeInterroge);
			foreach (IFournisseurProprieteDynamiquesSimplifie fournisseur in m_listeSousFournisseurs)
			{
				lst.AddRange ( fournisseur.GetDefinitionsChamps ( objet, defParente ));
			}
			foreach (CObjetPourSousProprietes objetSecondaire in m_associationsObjetsSupplementaires.GetObjetsAssocies(objet))
			{
				foreach (IFournisseurProprieteDynamiquesSimplifie fournisseur in m_listeSousFournisseurs)
				{
					lst.AddRange(fournisseur.GetDefinitionsChamps(objetSecondaire, defParente));
				}
			}
			NettoieListe(lst);
            lst.Sort((x,y)=>x.Nom.CompareTo(y.Nom));
			return lst.ToArray();
		}

		private void NettoieListe(List<CDefinitionProprieteDynamique> lst)
		{
			if (AvecReadOnly)
				return;
			foreach (CDefinitionProprieteDynamique def in lst.ToArray())
			{
				if (def.IsReadOnly)
					lst.Remove(def);
			}
		}

		//------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
            
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();

            CDefinitionMultiSourceForExpression multi = objet.ElementAVariableInstance as CDefinitionMultiSourceForExpression;
            if (multi != null)
                lst.AddRange(GetDefinitionsChamps(multi.DefinitionObjetPrincipal));
			foreach (IFournisseurProprieteDynamiquesSimplifie fournisseur in m_listeSousFournisseurs)
			{
				lst.AddRange(fournisseur.GetDefinitionsChamps(objet, defParente));
			}
			foreach (CObjetPourSousProprietes objetSecondaire in m_associationsObjetsSupplementaires.GetObjetsAssocies(objet))
			{
				foreach (IFournisseurProprieteDynamiquesSimplifie fournisseur in m_listeSousFournisseurs)
				{
					lst.AddRange(fournisseur.GetDefinitionsChamps(objetSecondaire, defParente));
				}
			}
			NettoieListe(lst);
            lst.Sort();
			return lst.ToArray();
		}


		//------------------------------------------------------------------
		private static Dictionary<Type, bool> m_dicTypesSansSousProps = null;
		private class CLockerTypesSansSousProps
		{
		}
		public static bool HasSubProperties(Type tp)
		{

			lock (typeof(CLockerTypesSansSousProps))
			{
				if (m_dicTypesSansSousProps == null)
				{
					m_dicTypesSansSousProps = new Dictionary<Type, bool>();
					m_dicTypesSansSousProps[typeof(bool)]=true;
					m_dicTypesSansSousProps[typeof(byte)]=true;
					m_dicTypesSansSousProps[typeof(sbyte)]=true;
					m_dicTypesSansSousProps[typeof(char)]=true;
					m_dicTypesSansSousProps[typeof(decimal)]=true;
					m_dicTypesSansSousProps[typeof(double)]=true;
					m_dicTypesSansSousProps[typeof(float)]=true;
					m_dicTypesSansSousProps[typeof(int)]=true;
					m_dicTypesSansSousProps[typeof(uint)]=true;
					m_dicTypesSansSousProps[typeof(long)]=true;
					m_dicTypesSansSousProps[typeof(ulong)]=true;
					m_dicTypesSansSousProps[typeof(short)]=true;
					m_dicTypesSansSousProps[typeof(ushort)]=true;
					m_dicTypesSansSousProps[typeof(string)]=true;
					m_dicTypesSansSousProps[typeof(DateTime)]=true;
					m_dicTypesSansSousProps[typeof(CDateTimeEx)]=true;
				}
			}

			if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
				tp = tp.GetGenericArguments()[0];

			return !m_dicTypesSansSousProps.ContainsKey(tp);
		}
	}
}
