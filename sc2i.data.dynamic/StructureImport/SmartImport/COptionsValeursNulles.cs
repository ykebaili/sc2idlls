using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class COptionsValeursNulles : I2iSerializable
    {
        private HashSet<string> m_setValeursNulles = new HashSet<string>();
        private bool m_bNullSiErreurConversion = false;

        //-------------------------------------------------
        public COptionsValeursNulles()
        {
        }

        //-------------------------------------------------
        public IEnumerable<string> ValeursNulles
        {
            get
            {
                List<string> lstVals = new List<string>(m_setValeursNulles.ToArray());
                lstVals.Sort((x, y) => x.CompareTo(y));
                return lstVals.AsReadOnly();
            }
            set
            {
                HashSet<string> set = new HashSet<string>();
                if ( value != null )
                    foreach (string strVal in value)
                    {
                        if (strVal != null)
                            set.Add(strVal.ToUpper().Trim());
                    }
                m_setValeursNulles = set;
            }
        }

        //-------------------------------------------------
        public bool NullOnConversionError
        {
            get
            {
                return m_bNullSiErreurConversion;
            }
            set { m_bNullSiErreurConversion = value; }
        }

        //-------------------------------------------------
        /// <summary>
        /// Retourne true si l'option en question n'a aucun comportement autre
        /// que le comportement par défaut
        /// </summary>
        public bool IsDefaultBehavior
        {
            get
            {
                if (NullOnConversionError)
                    return false;
                if (m_setValeursNulles.Count() != 0)
                    return false;
                return true;
            }
        }

        //-------------------------------------------------
        public bool IsValeurNulle ( object valeur )
        {
            if (valeur == null)
                return true;
            if (m_setValeursNulles.Contains(valeur.ToString().ToUpper().Trim()))
                return true;
            return false;
        }

        //-------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteBool(ref m_bNullSiErreurConversion);
            int nNb = m_setValeursNulles.Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Lecture:
                    HashSet<string> set = new HashSet<string>();
                    for (int n = 0; n < nNb; n++)
                    {
                        string strTmp = "";
                        serializer.TraiteString(ref strTmp);
                        set.Add(strTmp);
                    }
                    m_setValeursNulles = set;
                    break;
                case ModeSerialisation.Ecriture:
                    foreach (string strVal in m_setValeursNulles)
                    {
                        string strTmp = strVal;
                        serializer.TraiteString(ref strTmp);
                    }
                    break;
            }
            return result;
        }
    }
}
