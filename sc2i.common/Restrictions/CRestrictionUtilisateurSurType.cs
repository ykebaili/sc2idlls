using System;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;
using sc2i.common.Restrictions;

namespace sc2i.common
{
	/// <summary>
	/// - Une restriction systeme ne peut pas être surcharge
	/// - Une restriction système n'a pas de priorité > elle doit être fusionnée
	///   quelque soit la restriction avec laquelle on veut la combiner
	/// </summary>
	[Serializable]
	[ReplaceClass("sc2i.multitiers.client.CRestrictionUtilisateurSurType")]
	public class CRestrictionUtilisateurSurType : I2iSerializable, ICloneable
	{
		//Propriété->Restriction
        private ERestriction m_restrictionSysteme = ERestriction.Aucune;
        //restriction d'affichage
        private ERestriction m_restrictionHorsPriorité = ERestriction.Aucune;
        //restrictions d'affichage par champ
        private Hashtable m_tableRestrictionsHorsPriorites = new Hashtable();
        private Hashtable m_tableRestrictions = new Hashtable();
		private Type m_type;
		private ERestriction m_restrictionUtilisateur = ERestriction.Aucune;
		private int m_nPriorite = 0;

        private Dictionary<string, bool> m_dicContextesExceptions = new Dictionary<string, bool>();

		//Indique que la combinaison avec cette restriction retourne cette
		//restriction si elle est plus forte (priorité >) que celle
		//avec laquelle on la combine.
		private bool m_bSurchageComplete = false;

        private static IConvertisseurAncienneCleRestrictionEnNouvelleCleCompatibleIdUniversel m_convertisseurCle = null;

		/// ///////////////////////////////////////////
		public CRestrictionUtilisateurSurType(Type tp)
		{
			m_type = tp;
		}

        /// ///////////////////////////////////////////
		public CRestrictionUtilisateurSurType(Type tp, ERestriction restrictionSysteme)
		{
			m_type = tp;
            m_restrictionSysteme = restrictionSysteme;
			m_bSurchageComplete = false;
		}


        /// ///////////////////////////////////////////
        public ERestriction RestrictionSysteme
        {
            get
            {
                return m_restrictionSysteme;
            }
            set
            {
                m_restrictionSysteme = value;
            }
        }


        /// ///////////////////////////////////////////
        public string[] ContextesException
        {
            get{
                List<string> lst = new List<string>();
                foreach ( string strContexte in m_dicContextesExceptions.Keys )
                    lst.Add ( strContexte.ToUpper() );
                return lst.ToArray();
            }
            set{
                m_dicContextesExceptions.Clear();
                if (value != null)
                {
                    foreach (string strContexte in value)
                        m_dicContextesExceptions[strContexte.ToUpper()] = true;
                }
            }
        }

        /// ///////////////////////////////////////////
        ///Retourne TRUE si le contexte demandé fait partie des contextes d'exception
        public bool IsContexteException ( string strContexte )
        {
            return m_dicContextesExceptions.ContainsKey(strContexte.ToUpper());
        }

		/// ///////////////////////////////////////////
		public void ApplyToObjet ( object obj )
		{
            try
            {
                IObjetAModificationContextuelle objContextuel = obj as IObjetAModificationContextuelle;
                if (objContextuel != null)
                {
                    string strContexteModif = objContextuel.ContexteDeModification;
                    if (IsContexteException(strContexteModif))
                    {
                        m_tableRestrictions.Clear();
                        m_tableRestrictionsHorsPriorites.Clear();
                        m_restrictionUtilisateur = ERestriction.Aucune;
                        m_restrictionHorsPriorité = ERestriction.Aucune;
                        return;
                    }
                }

                if (obj is IObjetARestrictionSurEntite)
                    ((IObjetARestrictionSurEntite)obj).CompleteRestriction(this);
            }
            catch { }
		}

        //--------------------------------------------------------------------------------------
        public void ConvertToRestrictionSansPriorite()
        {
            // Combine la restriction utilisateur avec la restriction hors priorité
            m_restrictionHorsPriorité = m_restrictionHorsPriorité | m_restrictionUtilisateur;
            m_restrictionUtilisateur = ERestriction.Aucune;

            // Combine toutes les restriction sur champ
            foreach (string strKey in m_tableRestrictionsHorsPriorites.Keys)
            {
                SetRestrictionHorsPriorite(strKey,
                    GetRestrictionHorsPriorite(strKey) | GetRestrictionLocale(strKey));
            }

            foreach (string strKey in m_tableRestrictions.Keys)
            {
                SetRestrictionHorsPriorite(strKey,
                    GetRestrictionHorsPriorite(strKey) | GetRestrictionLocale(strKey));
            }

            // On vide toutes les Restriction utilisateur, car elles sont maintenatn stockées dans
            // les restriction Hors Priorité
            m_tableRestrictions.Clear();
            m_restrictionUtilisateur = ERestriction.Aucune;

        }

		/// ///////////////////////////////////////////
		protected ERestriction GetRestrictionLocale ( string strPropriete )
		{
			ERestriction restriction = ERestriction.Aucune;
			if ( m_tableRestrictions[strPropriete] != null )
			{
				restriction = (ERestriction)m_tableRestrictions[strPropriete];
			}
			return restriction;
		}

        /// ///////////////////////////////////////////
        protected ERestriction GetRestrictionHorsPriorite(string strPropriete)
        {
            ERestriction restriction = ERestriction.Aucune;
            if (m_tableRestrictionsHorsPriorites[strPropriete] != null)
                restriction = (ERestriction)m_tableRestrictionsHorsPriorites[strPropriete];
            return restriction;
        }

        /// ///////////////////////////////////////////
        public ERestriction GetRestriction ( string strPropriete )
        {
            int nRestriction = (int)(GetRestrictionLocale(strPropriete) | GetRestrictionHorsPriorite(strPropriete));
            if (nRestriction < (int)RestrictionGlobale)
                nRestriction = (int)RestrictionGlobale;
            return (ERestriction)nRestriction;
        }

		/// ///////////////////////////////////////////
		public bool HasRestrictions
		{
			get
			{
				if ( m_bSurchageComplete )
					return true;

				if ( RestrictionGlobale != ERestriction.Aucune )
					return true;

				foreach ( string strPropriete in m_tableRestrictions.Keys )
				{
					if ( GetRestrictionLocale ( strPropriete ) != ERestriction.Aucune )
						return true;
				}
                foreach (string strPropriete in m_tableRestrictionsHorsPriorites.Keys)
                {
                    if (GetRestrictionHorsPriorite(strPropriete) != ERestriction.Aucune)
                        return true;
                }
				return false;
			}
		}

		/// ///////////////////////////////////////////
		public ERestriction RestrictionUtilisateur
		{
			get
			{
				return m_restrictionUtilisateur;
			}
			set
			{
				m_restrictionUtilisateur = value;
			}
		}

        /// ///////////////////////////////////////////
        public ERestriction RestrictionGlobale
        {
            get
            {
                return m_restrictionSysteme | m_restrictionUtilisateur | m_restrictionHorsPriorité;
            }
        }

		/// ///////////////////////////////////////////
		public int Priorite
		{
			get
			{
				return m_nPriorite;
			}
			set
			{
				m_nPriorite = value;
			}
		}
		
		/// ///////////////////////////////////////////
		public bool SurchageComplete
		{
			get
			{
				return m_bSurchageComplete;
			}
			set
			{
				m_bSurchageComplete = value;
			}
		}

		/// ///////////////////////////////////////////
		public static bool CanModify ( ERestriction restriction )
		{
			return ( restriction & ERestriction.ReadOnly ) != ERestriction.ReadOnly;
		}

        /// ///////////////////////////////////////////
        public string[] GetProprietesReadOnly()
        {
            List<string> lst = new List<string>();
            foreach (DictionaryEntry entry in m_tableRestrictions)
            {
                ERestriction rest = (ERestriction)entry.Value;
                if (!CanModify(rest))
                    lst.Add((string)entry.Key);
            }
            return lst.ToArray();
        }

		/// ///////////////////////////////////////////
		public static bool CanCreate (ERestriction restriction)
		{
			return ( restriction & ERestriction.NoCreate ) != ERestriction.NoCreate;
		}

		/// ///////////////////////////////////////////
		public static bool CanDelete(ERestriction restriction)
		{
			return ( restriction & ERestriction.NoDelete ) != ERestriction.NoDelete;
		}

		/// ///////////////////////////////////////////
		public static bool CanShow(ERestriction restriction)
		{
			return (restriction & ERestriction.Hide ) != ERestriction.Hide;
		}

		/// ///////////////////////////////////////////
		public bool CanModifyType ( )
		{
			return CanModify ( RestrictionGlobale );
		}

		/// ///////////////////////////////////////////
		public bool CanCreateType ()
		{
			return CanCreate ( RestrictionGlobale );
		}

		/// ///////////////////////////////////////////
		public bool CanDeleteType()
		{
			return CanDelete ( RestrictionGlobale );
		}

		/// ///////////////////////////////////////////
		public bool CanShowType()
		{
			return CanShow ( RestrictionGlobale );
		}

		/// ///////////////////////////////////////////
		public bool CanModify ( string strPropriete )
		{
			return CanModify ( GetRestriction(strPropriete) );
		}

		/// ///////////////////////////////////////////
		public bool CanCreate ( string strPropriete )
		{
			return CanCreate (  GetRestriction(strPropriete)  );
		}

		/// ///////////////////////////////////////////
		public bool CanDelete( string strPropriete )
		{
			return CanDelete (  GetRestriction(strPropriete)  );
		}

		/// ///////////////////////////////////////////
		public bool CanShow( string strPropriete )
		{
			return CanShow (  GetRestriction(strPropriete)  );
		}

		/// ///////////////////////////////////////////
        public void SetRestrictionLocale(string strPropriete, ERestriction restriction)
        {
            m_tableRestrictions[strPropriete] = restriction;
            if (restriction == ERestriction.Aucune)
                m_tableRestrictions.Remove(strPropriete);
        }

        /// ///////////////////////////////////////////
        protected void SetRestrictionHorsPriorite(string strPropriete, ERestriction restriction)
        {
            m_tableRestrictionsHorsPriorites[strPropriete] = restriction;
            if (restriction == ERestriction.Aucune)
                m_tableRestrictionsHorsPriorites.Remove(strPropriete);
        }

		/// ///////////////////////////////////////////
		public object Clone()
		{
			CRestrictionUtilisateurSurType restriction = new CRestrictionUtilisateurSurType( m_type );
			foreach ( string strKey in m_tableRestrictions.Keys )
                restriction.m_tableRestrictions[strKey] = (ERestriction)m_tableRestrictions[strKey];
            foreach (string strKey in m_tableRestrictionsHorsPriorites.Keys)
                restriction.m_tableRestrictionsHorsPriorites[strKey] = m_tableRestrictionsHorsPriorites[strKey];
			restriction.RestrictionUtilisateur = RestrictionUtilisateur;
            restriction.RestrictionSysteme = RestrictionSysteme;
            restriction.m_restrictionHorsPriorité = m_restrictionHorsPriorité;
			restriction.m_nPriorite = Priorite;
			restriction.m_bSurchageComplete = SurchageComplete;
            restriction.ContextesException = ContextesException;
            
			return restriction;
		}
			
		/// ///////////////////////////////////////////
		public void Combine ( CRestrictionUtilisateurSurType rest )
		{
			if (rest == null || rest.TypeAssocie != TypeAssocie)
				return;

            //Les restrictions d'affichage se combinent toujours
            m_restrictionHorsPriorité |= rest.m_restrictionHorsPriorité;
            foreach (DictionaryEntry entry in rest.m_tableRestrictionsHorsPriorites)
                SetRestrictionHorsPriorite((string)entry.Key,
                    GetRestrictionHorsPriorite((string)entry.Key) | rest.GetRestrictionHorsPriorite((string)entry.Key));

			//Dans le cas d'une restriction système les 2 restrictions doivent
			//se "fusionner" quelque soit leur priorité ex:
			//Une restriction système est créé au lancement d'une application mais
			//l'utilisateur à ajouté d'autres restriction

			int nCompare = EstCeQuiYaUnPlusFortEtSiOuiCestQui(this, rest);
			if (nCompare == 1)//c'est moi le plus fort
			{
                RestrictionSysteme |= rest.RestrictionSysteme;
				return;
			}
			else if (nCompare == -1)//C'est l'autre le plus fort
			{
				m_tableRestrictions.Clear();
				foreach (string strKey in rest.m_tableRestrictions.Keys)
					SetRestrictionLocale(strKey, rest.GetRestrictionLocale(strKey));
				Priorite = rest.Priorite;
				SurchageComplete = true;
				RestrictionUtilisateur = rest.RestrictionUtilisateur;
                RestrictionSysteme |= rest.RestrictionSysteme;
                ContextesException = rest.ContextesException;
				return;
			}
			else//Il n'y a pas de plus fort
			{
				//Les lignes suivantes fusionnent les restrictions :
				SurchageComplete = (SurchageComplete && Priorite >= rest.Priorite)
							|| (rest.SurchageComplete && rest.Priorite >= Priorite);
				Priorite = Math.Max(Priorite, rest.Priorite);
				RestrictionUtilisateur = rest.RestrictionUtilisateur | RestrictionUtilisateur;
                RestrictionSysteme |= rest.RestrictionSysteme;

				foreach (string strKey in rest.m_tableRestrictions.Keys)
				{
					ERestriction rest1 = GetRestrictionLocale(strKey);
					ERestriction rest2 = rest.GetRestrictionLocale(strKey);
					SetRestrictionLocale(strKey, rest1 | rest2);
				}

                // On passe par une liste tampon car la tables parcourue peut être modifiée
                ArrayList listeKeysDeLaTablesDesRestrictions = new ArrayList(m_tableRestrictions.Keys);
				foreach (string strKey in listeKeysDeLaTablesDesRestrictions)
				{
					ERestriction rest1 = GetRestrictionLocale(strKey);
					ERestriction rest2 = rest.GetRestrictionLocale(strKey);
					SetRestrictionLocale(strKey, rest1 | rest2);
				}
                Dictionary<string, bool> dicContextes = new Dictionary<string,bool>();
                foreach (string strContexte in ContextesException)
                    dicContextes[strContexte] = true;
                foreach (string strContexte in rest.ContextesException)
                    dicContextes[strContexte] = true;
                string[] strContextes = new string[dicContextes.Count];
                int nContexte = 0;
                foreach (string strcontexte in dicContextes.Keys)
                    strContextes[nContexte] = strcontexte;
                ContextesException = strContextes;
			}
		}

        /// ///////////////////////////////////////////
        public void AddRestrictionsHorsPriorite(CRestrictionUtilisateurSurType restriction)
        {
            m_restrictionHorsPriorité |= restriction.RestrictionGlobale;
            foreach (DictionaryEntry entry in restriction.m_tableRestrictions)
            {
                SetRestrictionHorsPriorite((string)entry.Key,
                    GetRestrictionHorsPriorite((string)entry.Key) |
                    restriction.GetRestriction((string)entry.Key));
            }
        }


		/// ///////////////////////////////////////////
		public static CRestrictionUtilisateurSurType Combine ( CRestrictionUtilisateurSurType restriction1, CRestrictionUtilisateurSurType restriction2 )
		{
			CRestrictionUtilisateurSurType rest = null;
			if ( restriction1 == null && restriction2 == null 
				|| restriction1.TypeAssocie != restriction2.TypeAssocie) 
				return null;

			if(restriction1 == null)
				return (CRestrictionUtilisateurSurType)restriction2.Clone();
			if(restriction2 == null)
				return (CRestrictionUtilisateurSurType)restriction1.Clone();

			rest = (CRestrictionUtilisateurSurType)restriction1.Clone();
			rest.Combine(restriction2);
			return rest;
		}

		/// <summary>
		/// Retourne une indication sur se qu'il faut faire pour la combinaison
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static int EstCeQuiYaUnPlusFortEtSiOuiCestQui(CRestrictionUtilisateurSurType x, CRestrictionUtilisateurSurType y)
		{
			if (x == null && y == null)
				return 0;
			else if (x == null)
				return -1;
			else if (y == null)
				return 1;
			if (x.TypeAssocie != y.TypeAssocie)
				return 0;
			
			if (x.Priorite == y.Priorite)
			{
				if (!x.SurchageComplete && y.SurchageComplete)
					return -1;
				else if (x.SurchageComplete && !y.SurchageComplete)
					return 1;
				else
					return 0;
			}
			else if (x.Priorite > y.Priorite)
			{
				if (x.SurchageComplete)
					return 1;
				else
					return 0;
			}
			else
			{
				if (y.SurchageComplete)
					return -1;
				else
					return 0;
			}
		}

		/// //////////////////////////////////////
		public Type TypeAssocie
		{
			get
			{
				return m_type;
			}
			set
			{
				m_type = value;
			}
		}
		
		#region Membres de I2iSerializable
		/// //////////////////////////////////////
		
        public static void RegisterConvertisseurCleRestriction(IConvertisseurAncienneCleRestrictionEnNouvelleCleCompatibleIdUniversel convertisseurCle)
        {
            m_convertisseurCle = convertisseurCle;    
        }
        
        
        private int GetNumVersion()
		{
			//return 2;//2 : Ajout des contextes d'exception
            return 3; // Mise à jour des Clés Restriction dans le cas des Champs Custom (serialisation en mode lecture seulement)
		}

		/// //////////////////////////////////////
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			int nRestriction = (int)RestrictionGlobale;
			serializer.TraiteInt ( ref nRestriction );
            RestrictionUtilisateur = (ERestriction)nRestriction;

			serializer.TraiteType ( ref m_type );
			int nNb = m_tableRestrictions.Count;
			serializer.TraiteInt ( ref nNb );
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (string strKey in m_tableRestrictions.Keys)
                    {
                        string strTmp = strKey;
                        nRestriction = (int)m_tableRestrictions[strKey];
                        serializer.TraiteString(ref strTmp);
                        serializer.TraiteInt(ref nRestriction);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    m_tableRestrictions.Clear();
                    for (int nKey = 0; nKey < nNb; nKey++)
                    {
                        string strKey = "";
                        int nRest = 0;
                        serializer.TraiteString(ref strKey);
                        if (nVersion < 3 && m_convertisseurCle != null)
                            strKey = m_convertisseurCle.GetCleRestrictionCompatible(strKey);
                        serializer.TraiteInt(ref nRest);
                        m_tableRestrictions[strKey] = (ERestriction)nRest;
                    }
                    break;
            }
			if ( nVersion >= 1 )
			{
				serializer.TraiteInt ( ref m_nPriorite );
				serializer.TraiteBool ( ref m_bSurchageComplete );
			}
            if ( nVersion >= 2 )
            {
                int nNbExceptions = m_dicContextesExceptions.Count;
                switch ( serializer.Mode )
                {
                    case ModeSerialisation.Ecriture :
                        serializer.TraiteInt ( ref nNbExceptions );
                        foreach ( string strTmp in m_dicContextesExceptions.Keys )
                        {
                            string strText = strTmp;
                            serializer.TraiteString ( ref strText );
                        }
                        break;
                    case ModeSerialisation.Lecture :
                        serializer.TraiteInt ( ref nNbExceptions );
                        List<String> lst = new List<string>();
                        for ( int nException = 0; nException < nNbExceptions; nException++ )
                        {
                            string strTmp = "";
                            serializer.TraiteString ( ref strTmp );
                            lst.Add ( strTmp );
                        }
                        ContextesException = lst.ToArray();
                        break;
                }
            }
			return result;
		}
		#endregion
	}

    public interface IConvertisseurAncienneCleRestrictionEnNouvelleCleCompatibleIdUniversel
    {
        string GetCleRestrictionCompatible(string strAncienneCleRestriction);
    }


}
