using System;
using System.Collections;

using sc2i.multitiers.client;
using sc2i.common;
using System.Text;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CReferenceObjetDonnee.
	/// </summary>
	[Serializable]
	public class CReferenceObjetDonnee : I2iSerializable, IObjetARestrictionSurEntite,
        IConteneurObjetARestriction
	{
		private static IFournisseurContexteDonnee m_fournisseurContexte = null;
		private Type m_typeObjet;

        //Si l'objet ne gère pas les DBKey, la référence utilise les clés
        //de l'objet.
        private CDbKey m_keyObjet = null;
		private object[] m_cles = null;
		
		public CReferenceObjetDonnee()
		{
			m_typeObjet = null;
			m_cles = new object[0];
            m_keyObjet = null;

		}

		/// /// ///////////////////////////////////////
		public static void SetFournisseurContexteDonnee ( IFournisseurContexteDonnee fournisseur )
		{
			m_fournisseurContexte = fournisseur;
		}
		
		/// <summary>
		/// /// ///////////////////////////////////////
		/// </summary>
		/// <param name="obj"></param>
		public CReferenceObjetDonnee(CObjetDonnee obj)
		{
			m_typeObjet = obj.GetType();
			m_cles = obj.GetValeursCles();
            if (obj.ManageIdUniversel)
                m_keyObjet = obj.DbKey;
		}

        /// /// ///////////////////////////////////////
        public Type TypeObjet
        {
            get
            {
                return m_typeObjet;
            }

        }

        /// /// ///////////////////////////////////////
        public CDbKey DbKeyObjet
        {
            get
            {
                return m_keyObjet;
            }
            set 
            { 
                m_keyObjet = value; 
            }
        }

        /// /// ///////////////////////////////////////
        public object[] ClesObjet
        {
            get
            {
                return m_cles;
            }
        }


        /// <summary>
        /// /// ///////////////////////////////////////
        /// </summary>
        /// <param name="obj"></param>
        public CReferenceObjetDonnee(Type typeObjet, CDbKey keyObjet)
        {
            m_typeObjet = typeObjet;
            m_keyObjet = keyObjet;
        }
        
        /// <summary>
        /// /// ///////////////////////////////////////
        /// </summary>
        /// <param name="obj"></param>
        public CReferenceObjetDonnee(Type typeObjet, object[] cles)
        {
            m_typeObjet = typeObjet;
            m_keyObjet = null;
            m_cles = cles;
        }

        /// /// ///////////////////////////////////////
        public CReferenceObjetDonnee GetCloneReferenceObjetDonnee()
        {
            CReferenceObjetDonnee r = new CReferenceObjetDonnee();
            r.m_cles = m_cles;
            r.m_keyObjet = m_keyObjet;
            r.m_typeObjet = m_typeObjet;
            return r;
        }

		/// ///////////////////////////////////////
		public virtual CObjetDonnee GetObjet()
		{
			if (m_fournisseurContexte == null)
				return null;
			CContexteDonnee contexte = m_fournisseurContexte.ContexteCourant;
			if (contexte == null)
				throw new Exception(I.T("The Data context supplier CReferenceObjetDonnee has returned a null context|164"));
			return GetObjet(contexte);
		}

		/// ///////////////////////////////////////
		public virtual CObjetDonnee GetObjet ( CContexteDonnee contexte )
		{

			CObjetDonnee obj = (CObjetDonnee)Activator.CreateInstance ( m_typeObjet, new object[]{contexte});
            if (m_keyObjet != null)
                if (obj.ReadIfExists(m_keyObjet))
                    return obj;

            // Modif YK 10/10/2008: Utiliser le ReadIfExiste c'est meiux que le PointeSurLigne
            //obj.PointeSurLigne ( m_cles );
            if(m_cles != null && m_cles.Length > 0 && obj.ReadIfExists(m_cles))
			    return obj;
            return null;
		}

		/// ///////////////////////////////////////
		public override int GetHashCode()
		{
            StringBuilder bl = new StringBuilder();
            bl.Append(m_typeObjet.ToString());
            bl.Append("_");
            if ( m_keyObjet != null )
                bl.Append(m_keyObjet.StringValue);
            else
            {
                foreach (object k in m_cles)
                {
                    if (k != null)
                    {
                        bl.Append(k.ToString());
                        bl.Append("_");
                    }
                }
            }
            return bl.ToString().GetHashCode();
		}

		/// ///////////////////////////////////////
		public override bool Equals ( object obj )
		{
			if ( !(obj is CReferenceObjetDonnee ) )
				return false;
			CReferenceObjetDonnee objRef = (CReferenceObjetDonnee)obj;
			if ( objRef.m_typeObjet != m_typeObjet )
				return false;
            if (m_keyObjet != null && objRef.DbKeyObjet != null)
                return m_keyObjet.Equals(objRef.DbKeyObjet);
            if ((m_keyObjet == null) != (objRef.DbKeyObjet == null))
                return false;

			if ( objRef.m_cles.Length != m_cles.Length )
				return false;
			for ( int nCle = 0; nCle < m_cles.Length; nCle++ )
				if ( !objRef.m_cles[nCle].Equals(m_cles[nCle]) )
					return false;
			return true;
		}

		/// ///////////////////////////////////////
		public override string ToString()
		{
			string strText =  DynamicClassAttribute.GetNomConvivial ( m_typeObjet )+"(";
            if (m_keyObjet != null)
                strText += m_keyObjet.StringValue;
            else
            {
                foreach (object obj in m_cles)
                {
                    if (obj == null)
                        strText += "null,";
                    else
                        strText += obj.ToString() + ",";
                }
                if (m_cles.Length > 0)
                    strText = strText.Substring(0, strText.Length - 1);
            }
            strText += ")";
			return strText;
		}


		#region Membres de I2iSerializable

		private int GetNumVersion()
		{
			return 1;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteType ( ref m_typeObjet );
            if (nVersion >= 1)
                serializer.TraiteDbKey(ref m_keyObjet);
			//ArrayList lst = new ArrayList ( m_cles );
			IList lst = (IList)m_cles;
			serializer.TraiteListeObjetsSimples ( ref lst );
			m_cles = new object[lst.Count];
			for ( int nObjet = 0; nObjet < lst.Count; nObjet++ )
				m_cles[nObjet] = lst[nObjet];
            if ( nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                if (m_cles.Length == 1 && m_cles[0] is int)
                    //Charge la DbKey
                    m_keyObjet = CDbKeyAddOn.GetDbKeyFromId(m_typeObjet, (int)m_cles[0]);
            }
			return result;
		}

		#endregion

		#region IObjetARestrictionSurEntite Membres

		//----------------------------------------------------------------------------
		public void CompleteRestriction(CRestrictionUtilisateurSurType restriction)
		{
			CObjetDonnee objet = GetObjet();
			if (objet is IObjetARestrictionSurEntite)
				((IObjetARestrictionSurEntite)objet).CompleteRestriction(restriction);
		}

		#endregion
        //----------------------------------------------------------------------------
        #region IConteneurObjetARestriction Membres

        public Type TypePourRestriction
        {
            get { return m_typeObjet; }
        }

        #endregion

        //----------------------------------------------------------------------------
        public static List<CObjetDonnee> GetObjets(
            IEnumerable<CReferenceObjetDonnee> lstReferences,
            CContexteDonnee contexte
            )
        {
            List<CObjetDonnee> lstObjets = new List<CObjetDonnee>();
            //Trie les références par type
            Dictionary<Type, List<CReferenceObjetDonnee>> dicTypeToDbKey = new Dictionary<Type, List<CReferenceObjetDonnee>>();
            foreach (CReferenceObjetDonnee reference in lstReferences)
            {
                if (reference.TypeObjet != null)
                {
                    if (reference.DbKeyObjet != null)
                    {
                        List<CReferenceObjetDonnee> lst = null;
                        if (!dicTypeToDbKey.TryGetValue(reference.TypeObjet, out lst))
                        {
                            lst = new List<CReferenceObjetDonnee>();
                            dicTypeToDbKey[reference.TypeObjet] = lst;
                        }
                        lst.Add(reference);
                    }
                    else
                    {
                        lstObjets.Add(reference.GetObjet(contexte));
                    }
                }
            }
            foreach (KeyValuePair<Type, List<CReferenceObjetDonnee>> kv in dicTypeToDbKey)
            {
                //Lecture par paquets de 200 éléments
                int nIndex = 0;
                int nNb = kv.Value.Count;
                int nNbParPaquet = 200;
                for (nIndex = 0; nIndex < nNb; nIndex += nNbParPaquet)
                {
                    StringBuilder bl = new StringBuilder();
                    int nMax = Math.Min(nIndex + nNbParPaquet, nNb);
                    for (int nRead = nIndex; nRead < nMax; nRead++)
                    {
                        CReferenceObjetDonnee rf = kv.Value[nRead];
                        {
                            bl.Append(rf.DbKeyObjet.GetValeurStringFiltre());
                            bl.Append(",");
                        }
                    }
                    if (bl.Length > 0)
                    {
                        bl.Remove(bl.Length - 1, 1);
                        CListeObjetsDonnees lst = new CListeObjetsDonnees(contexte, kv.Key);
                        lst.Filtre = new CFiltreData(CObjetDonnee.c_champIdUniversel + " in (" + bl.ToString() + ")");
                        foreach (CObjetDonnee objet in lst)
                        {
                            lstObjets.Add(objet);
                        }
                    }

                }
            }
            return lstObjets;
        }
    }
}
