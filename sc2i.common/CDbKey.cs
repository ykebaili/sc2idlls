using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.common
{
    [Serializable]
    [ReplaceClass("sc2i.data.CDbKey")]
    public class CDbKey
    {
        private const string c_IdInSerialize = "ID=";
        protected int? m_nIdPourCompatibliteQuonNeDoitPlusUtiliser = null;
        protected string m_strIdUniversel = "";

        //----------------------------------------------------
        protected CDbKey(int nId)
        {
            m_nIdPourCompatibliteQuonNeDoitPlusUtiliser = nId;
        }

        //----------------------------------------------------
        protected CDbKey(string strIdUniversel)
        {
            m_strIdUniversel = strIdUniversel;
            m_nIdPourCompatibliteQuonNeDoitPlusUtiliser = null;
        }

        //----------------------------------------------------
        public int? InternalIdNumeriqueANeJamaisUtiliserSaufDansCDbKeyAddOn
        {
            get
            {
                return m_nIdPourCompatibliteQuonNeDoitPlusUtiliser;
            }
            set
            {
                m_nIdPourCompatibliteQuonNeDoitPlusUtiliser = value;
            }
        }

        //----------------------------------------------------
        public string InternalUniversalIdANeJamaisUtiliserSaufDansCDbKeyAddOn
        {
            get
            {
                return m_strIdUniversel;
            }
            set
            {
                m_strIdUniversel = value;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Retourne true si la clé est un id numérique
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Returns True if Key is a numerical value")]
        public bool IsNumericalId()
        {
            return m_strIdUniversel.Length == 0 && m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null
                && m_nIdPourCompatibliteQuonNeDoitPlusUtiliser >= 0;

        }

        
        //----------------------------------------------------
        public static CDbKey GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(int nId)
        {
            return new CDbKey(nId);
        }

        //----------------------------------------------------
        public static CDbKey GetNewDbKeyOnUniversalIdANePasUtiliserEnDehorsDeCDbKeyAddOn(string strIdUniversel)        
        {
            CDbKey key = new CDbKey(strIdUniversel);
            //On ne doit pas stocker l'ancien ID
            //car sinon le hashcode n'est pas le bon, donc, on ne retrouve plus
            //la valeur dans un dictionnaire !
            //C'est pour cela que les CDbkey old sont convertis en DbKey à UniversalID
            //à la lecture
            return key;
        }

        //----------------------------------------------------
        public override bool Equals(object obj)
        {
            CDbKey obj2 = obj as CDbKey;
            if (obj2 != null)
            {
                if (obj2.m_strIdUniversel.Length > 0 &&
                    m_strIdUniversel.Length > 0)
                    return obj2.m_strIdUniversel.Equals(m_strIdUniversel);
                else
                {
                    if (m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null
                        && obj2.m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null)
                        return m_nIdPourCompatibliteQuonNeDoitPlusUtiliser.Equals(obj2.m_nIdPourCompatibliteQuonNeDoitPlusUtiliser);
                }
            }
            return false;
        }

        //----------------------------------------------------
        public static bool operator ==(CDbKey key1, CDbKey key2)
        {
            if ((object)key1 == null && (object)key2 == null)
                return true;
            if (((object)key1 == null) != ((object)key2 == null))
                return false;
            return key1.Equals(key2);
        }

        //----------------------------------------------------
        public static bool operator !=(CDbKey key1, CDbKey key2)
        {
            return !(key1 == key2);
        }

        //----------------------------------------------------
        public override int GetHashCode()
        {
            if (m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null)
                return m_nIdPourCompatibliteQuonNeDoitPlusUtiliser.Value;
            else
                return m_strIdUniversel.GetHashCode();
        }

        
        //----------------------------------------------------
        internal static void ReadFromOldId(C2iSerializer ser, ref CDbKey key)
        {
            int nTmp = 0;
            ser.TraiteInt(ref nTmp);
            if (nTmp >= 0)
                key = new CDbKey(nTmp);
            return;
        }
            

        //----------------------------------------------------
        internal static void SerializeKey(C2iSerializer ser, ref CDbKey key)
        {
            switch (ser.Mode)
            {
                case ModeSerialisation.Lecture:
                    key = null;
                    string strTmp = "";
                    ser.TraiteString(ref strTmp);
                    if (strTmp.Length > 0)
                        key = CDbKey.CreateFromStringValue(strTmp);
                    break;
                case ModeSerialisation.Ecriture:
                    string strTmpWrite = "";
                    if ( key != null )
                    {
                        strTmpWrite = key.StringValue;
                    }
                    ser.TraiteString(ref strTmpWrite);
                    break;
            }
        }

        //-----------------------------------------------
        /// <summary>
        /// Crée une clé à partir d'une valeur serializable
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static CDbKey CreateFromStringValue(string strValue)
        {
            if (strValue.ToUpper().StartsWith(c_IdInSerialize))
            {
                try
                {
                    int nTmp = Int32.Parse(strValue.Substring(c_IdInSerialize.Length));
                    return CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nTmp);
                }
                catch 
                {
                    return null;
                }
            }
            if ( strValue.Length < 10 )//Si ça peut être juste un entier !
            {
                int nId;
                if (Int32.TryParse(strValue, out nId))
                    return CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nId);
            }
            return new CDbKey(strValue);
        }

        //-----------------------------------------------
        /// <summary>
        /// Retourne la valeur de la clé sous forme de texte serializable
        /// </summary>
        [DynamicField("String_value")]
        public string StringValue
        {
            get
            {
                if (m_strIdUniversel.Length > 0)
                    return m_strIdUniversel;
                if (m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null)
                    return c_IdInSerialize + m_nIdPourCompatibliteQuonNeDoitPlusUtiliser.Value;
                return "";
            }
        }

        //-----------------------------------------------
        /// <summary>
        /// Retourne la valeur en base de cette dbKey (soit en entier,
        /// soit un texte)
        /// </summary>
        /// <returns></returns>
        public object GetValeurInDb()
        {
            if (m_strIdUniversel.Length > 0)
                return m_strIdUniversel;
            if (m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null)
                return m_nIdPourCompatibliteQuonNeDoitPlusUtiliser.Value;
            return "";
        }

        //-----------------------------------------------
        /// <summary>
        /// REtourne la valeur en base, prêt à être mise dans un filtre
        /// </summary>
        /// <returns></returns>
        public string GetValeurStringFiltre()
        {
            if (m_strIdUniversel.Length > 0)
                return "\'"+m_strIdUniversel+"\'";
            if (m_nIdPourCompatibliteQuonNeDoitPlusUtiliser != null)
                return m_nIdPourCompatibliteQuonNeDoitPlusUtiliser.Value.ToString();
            return "\'\'";
        }

        


    }

    [Serializable]
    public class CListeCDbKey : List<CDbKey>, I2iSerializable
    {
        //--------------------------------------------------
        public CListeCDbKey()
            : base()
        {
        }

        //--------------------------------------------------
        public CListeCDbKey(IEnumerable<CDbKey> lst)
            : base(lst)
        {
        }

        //--------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            int nNb = Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Lecture:
                    Clear();
                    for (int n = 0; n < nNb; n++)
                    {
                        CDbKey key = null;
                        serializer.TraiteDbKey(ref key);
                        if (key != null)
                            Add(key);
                    }
                    break;
                case ModeSerialisation.Ecriture:
                    foreach (CDbKey key in this)
                    {
                        CDbKey tmp = key;
                        serializer.TraiteDbKey(ref tmp);
                    }
                    break;
            }
            return result;
        }

       
    }


}
