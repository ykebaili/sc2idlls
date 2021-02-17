using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using sc2i.common;

namespace sc2i.common
{

	[Serializable]
	[ReplaceClass("sc2i.multitiers.client.CListeRestrictionsUtilisateurSurType")]
	public class CListeRestrictionsUtilisateurSurType : I2iSerializable, ICloneable
	{
		private bool m_bIsAdministrateur = false;
		private Hashtable m_tableRestrictions = new Hashtable();

		private int? m_nSeuilAnnulationPriorites = null;



		/// <summary>
		/// /////////////////////////////////////////////////////////
		/// </summary>
		public CListeRestrictionsUtilisateurSurType()
		{
		}

		/// <summary>
		/// /////////////////////////////////////////////////////////
		/// </summary>
		public CListeRestrictionsUtilisateurSurType(bool bIsAdministrateur)
		{
			m_bIsAdministrateur = bIsAdministrateur;
		}

		/// /////////////////////////////////////////////////////////
		public void ResetRestriction(Type tp)
		{
			m_tableRestrictions.Remove(tp);
		}

		/// /////////////////////////////////////////////////////////
		public int? SeuilAnnulationPriorites
		{
			get
			{
				return m_nSeuilAnnulationPriorites;
			}
			set
			{
				m_nSeuilAnnulationPriorites = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public List<CRestrictionUtilisateurSurType> ToutesLesRestrictionsAffectees
		{
			get
			{
				List<CRestrictionUtilisateurSurType> lst = new List<CRestrictionUtilisateurSurType>();
				foreach (CRestrictionUtilisateurSurType restriction in m_tableRestrictions.Values)
					if (restriction != null)
						lst.Add(restriction);
				return lst;
			}
		}

        ///--------------------------------------------------------------------
        /// <summary>
        /// remplace la restriction en cours
        /// </summary>
        /// <param name="restriction"></param>
        public void SetRestriction(CRestrictionUtilisateurSurType restriction)
        {
            m_tableRestrictions[restriction.TypeAssocie] = restriction;
        }


		/// /////////////////////////////////////////////////////////
		public void AddRestriction(CRestrictionUtilisateurSurType restriction)
		{
			if (restriction == null)
				return;
			Type tp = restriction.TypeAssocie;
			CRestrictionUtilisateurSurType resEx = GetRestriction(tp);
			if (SeuilAnnulationPriorites == null || resEx.Priorite >= (int)SeuilAnnulationPriorites)
			{
				if (resEx != null && resEx.HasRestrictions)
					m_tableRestrictions[tp] = CRestrictionUtilisateurSurType.Combine(resEx, restriction);
				else
					m_tableRestrictions[tp] = (CRestrictionUtilisateurSurType)restriction.Clone();
			}
		}

        


		/// /////////////////////////////////////////////////////////
		public CRestrictionUtilisateurSurType GetRestriction(Type tp)
		{
			if (m_bIsAdministrateur)
				return new CRestrictionUtilisateurSurType(tp);
			CRestrictionUtilisateurSurType rest = (CRestrictionUtilisateurSurType)m_tableRestrictions[tp];
			if (rest == null)
				rest = new CRestrictionUtilisateurSurType(tp);
			object[] attribs = tp.GetCustomAttributes(typeof(RestrictionHeriteeAttribute), true);
            CRestrictionUtilisateurSurType restClone = (CRestrictionUtilisateurSurType)rest.Clone();
			if (attribs != null && attribs.Length > 0)
			{
				Type newTp = tp.BaseType;
				if (tp != null)
					restClone.Combine(GetRestriction(newTp));
			}
			if (SeuilAnnulationPriorites != null)
			{
				restClone.SurchageComplete = true;
				restClone.Priorite = (int)SeuilAnnulationPriorites;
			}
			return restClone;
		}

		/// /////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
		}


		/// /////////////////////////////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			int nNb = m_tableRestrictions.Count;
			serializer.TraiteInt(ref nNb);
			switch (serializer.Mode)
			{
				case ModeSerialisation.Ecriture:
					foreach (Type tp in m_tableRestrictions.Keys)
					{
						Type tpTemp = tp;
						I2iSerializable restriction = (I2iSerializable)m_tableRestrictions[tp];
						serializer.TraiteType(ref tpTemp);
						result = serializer.TraiteObject(ref restriction);
						if (!result)
							return result;
					}
					break;
				case ModeSerialisation.Lecture:
					m_tableRestrictions.Clear();
					for (int nType = 0; nType < nNb; nType++)
					{
						Type tp = null;
						I2iSerializable rest = null;
						serializer.TraiteType(ref tp);
						result = serializer.TraiteObject(ref rest, tp);
						if (!result)
							return result;
						if (tp != null)
							m_tableRestrictions[tp] = rest;
					}
					break;
			}
			if (nVersion >= 1)
				serializer.TraiteBool(ref m_bIsAdministrateur);
			if (nVersion >= 2)
			{
				bool bHasAnnulation = SeuilAnnulationPriorites != null;
				serializer.TraiteBool(ref bHasAnnulation);
				if (bHasAnnulation)
				{
					int nTmp = SeuilAnnulationPriorites == null ? 0 : (int)m_nSeuilAnnulationPriorites;
					serializer.TraiteInt(ref nTmp);
					SeuilAnnulationPriorites = nTmp;
				}
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public object Clone()
		{
			CListeRestrictionsUtilisateurSurType liste = new CListeRestrictionsUtilisateurSurType();
			foreach (Type tp in m_tableRestrictions.Keys)
				liste.AddRestriction((CRestrictionUtilisateurSurType)GetRestriction(tp).Clone());
			liste.m_bIsAdministrateur = m_bIsAdministrateur;
			liste.m_nSeuilAnnulationPriorites = m_nSeuilAnnulationPriorites;
			return liste;
		}

		/// /////////////////////////////////////////////////////////
		public void Combine(CListeRestrictionsUtilisateurSurType liste)
		{
			if (liste == null)
				return;

			//Si la liste annule des restrictions inférieures
			if (liste.SeuilAnnulationPriorites != null)
			{
				if (SeuilAnnulationPriorites == null ||
					(int)liste.SeuilAnnulationPriorites > (int)SeuilAnnulationPriorites)
					SeuilAnnulationPriorites = liste.SeuilAnnulationPriorites;
			}

			foreach (Type tp in liste.m_tableRestrictions.Keys)
				AddRestriction(liste.GetRestriction(tp));
		}

        /// /////////////////////////////////////////////////////////
        public void AddRestrictionsHorsPriorites(CListeRestrictionsUtilisateurSurType liste)
        {
            foreach (DictionaryEntry entry in liste.m_tableRestrictions)
            {
                AddRestrictionsHorsPriorites((Type)entry.Key,
                    (CRestrictionUtilisateurSurType)entry.Value);
            }
        }

        /// /////////////////////////////////////////////////////////
        private void AddRestrictionsHorsPriorites(Type tp, CRestrictionUtilisateurSurType rest)
        {
            CRestrictionUtilisateurSurType restO = GetRestriction(tp);
            restO.AddRestrictionsHorsPriorite(rest);
            m_tableRestrictions[tp] = restO;
        }

        public void ApplyToObjet(object objet)
        {
            CRestrictionUtilisateurSurType rest = GetRestriction(objet.GetType()).Clone() as CRestrictionUtilisateurSurType;
            rest.ApplyToObjet(objet);
            SetRestriction(rest);
        }

		/// /////////////////////////////////////////////////////////
		public static CListeRestrictionsUtilisateurSurType Combine(CListeRestrictionsUtilisateurSurType liste1, CListeRestrictionsUtilisateurSurType liste2)
		{
			if (liste1 == null && liste2 == null)
				return null;
			if (liste1 == null)
				return (CListeRestrictionsUtilisateurSurType)liste2.Clone();
			if (liste2 == null)
				return (CListeRestrictionsUtilisateurSurType)liste1.Clone();
			CListeRestrictionsUtilisateurSurType newListe = (CListeRestrictionsUtilisateurSurType)liste1.Clone();
			newListe.Combine(liste2);
			return newListe;
		}


        
    }
}
