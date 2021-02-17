using System;
using System.Collections.Generic;

namespace sc2i.common
{
    [Serializable]
    [DynamicClass("Operation result")]
    public class CResultAErreur
    {
        /////////////////////////////////////////////////////////////////////////////
        public static bool ToBool(CResultAErreur result)
        {
            return result.m_bResult;
        }
        public static implicit operator bool(CResultAErreur result)
        {
            return ToBool(result);
        }
        public static List<IErreur> ToList(CResultAErreur result)
        {
            List<IErreur> lstErrs = new List<IErreur>();
            foreach (IErreur err in result.Erreur.Erreurs)
                lstErrs.Add(err);
            return lstErrs;
        }
        public static implicit operator List<IErreur>(CResultAErreur result)
        {
            return ToList(result);
        }
        /////////////////////////////////////////////////////////////////////////////
        public static CResultAErreur BitwiseAnd(CResultAErreur r1, CResultAErreur r2)
        {
            CResultAErreur rNew = new CResultAErreur();
            rNew.m_bResult = r1.m_bResult && r2.m_bResult;
            rNew.m_erreur = r1.Erreur + r2.Erreur;
            rNew.Data = r2.Data != null ? r2.Data : r1.Data;
            return rNew;
        }
        public static CResultAErreur operator &(CResultAErreur r1, CResultAErreur r2)
        {
            return BitwiseAnd(r1, r2);
        }

        /////////////////////////////////////////////////////////////////////////////
        public static CResultAErreur BitwiseOr(CResultAErreur r1, CResultAErreur r2)
        {
            CResultAErreur rNew = new CResultAErreur();
            rNew.m_bResult = r1.m_bResult || r2.m_bResult;
            rNew.m_erreur = r1.Erreur + r2.Erreur;
            return rNew;
        }
        public static CResultAErreur operator |(CResultAErreur r1, CResultAErreur r2)
        {
            return BitwiseOr(r1, r2);
        }

        private CPileErreur m_erreur;
        private bool m_bResult;
        private object m_data;

        /////////////////////////////////////////////////////////////////////////////
        public CResultAErreur()
        {
            m_bResult = true;
            m_erreur = new CPileErreur();
        }



        /////////////////////////////////////////////////////////////////////////////
        [DynamicField("Result")]
        public bool Result
        {
            get { return m_bResult; }
            set
            {
                m_bResult = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        //retourne les données associées au result
        [DynamicField("Data")]
        public object Data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        public CResultAErreur SetTrue()
        {
            Result = true;
            return this;
        }

        /////////////////////////////////////////////////////////////////////////////
        public CResultAErreur SetFalse()
        {
            Result = false;
            return this;
        }


        /////////////////////////////////////////////////////////////////////////////
        public void EmpileErreur(CPileErreur erreurs)
        {
            m_erreur.EmpileErreurs(erreurs);
            if (erreurs != null && erreurs.Erreurs != null && erreurs.Erreurs.Length > 0)
                Result = false;
        }
        /////////////////////////////////////////////////////////////////////////////
        public void EmpileErreur(IErreur erreur)
        {
            m_erreur.EmpileErreur(erreur);
            Result = false;
        }

        /////////////////////////////////////////////////////////////////////////////
        public void EmpileErreur(string strErreur, params string[] strParametres)
        {
            for (int nTmp = strParametres.Length - 1; nTmp >= 0; nTmp--)
            {
                strErreur = strErreur.Replace("@" + (nTmp + 1), strParametres[nTmp]);
            }
            m_erreur.EmpileErreur(new CErreurSimple(strErreur));
            Result = false;
        }

        /////////////////////////////////////////////////////////////////////////////
        public CPileErreur Erreur
        {
            get { return m_erreur; }
            set { m_erreur = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        public static CResultAErreur True
        {
            get { return new CResultAErreur().SetTrue(); }
        }

        /////////////////////////////////////////////////////////////////////////////
        public static CResultAErreur False
        {
            get
            {
                return new CResultAErreur().SetFalse();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        [DynamicFieldAttribute("Error message")]
        public string MessageErreur
        {
            get
            {
                return Erreur.ToString();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique que ce n'est pas une erreur grave
        /// </summary>
        [DynamicField("Is warning")]
        public bool IsAvertissement
        {
            get
            {
                foreach (IErreur err in Erreur.Erreurs)
                {
                    if (!err.IsAvertissement)
                        return false;
                }
                return true;
            }
        }

        public static CResultAErreur operator +(CResultAErreur result, IErreur err)
        {
            result.EmpileErreur(err);
            return result;
        }
        public static CResultAErreur operator +(CResultAErreur result, string strErr)
        {
            result.EmpileErreur(strErr);
            return result;
        }
        public static CResultAErreur operator +(CResultAErreur result, CResultAErreur result2)
        {
            result.EmpileErreur(result2.Erreur);
            return result;
        }


    }

    /////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class CResultAErreurType<T> : CResultAErreur
    {
        //----------------------------------------------
        public CResultAErreurType()
        {
            Result = true;
        }
        //----------------------------------------------
        public T DataType
        {
            get
            {
                if (Data is T)
                    return (T)Data;
                return default(T);
            }
            set
            {
                Data = value;
            }
        }

    }
}


