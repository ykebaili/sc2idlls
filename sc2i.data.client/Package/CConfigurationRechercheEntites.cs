using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.Package
{
    public class CConfigurationRechercheEntites : I2iSerializable
    {
        //Types qui ne feront pas partie du résultat de recherche
        private HashSet<Type> m_setTypesIgnores = new HashSet<Type>();

        private Dictionary<Type, COptionRechercheType> m_dicTypesAOption = new Dictionary<Type, COptionRechercheType>();

        private int m_nLimiteNbPourRechercheReference = 1000;

        //-------------------------------------------------------
        public CConfigurationRechercheEntites()
        {
        }

        //-------------------------------------------------------
        public int LimiteNbPourRechercheReference
        {
            get
            {
                return m_nLimiteNbPourRechercheReference;
            }
            set
            { 
                m_nLimiteNbPourRechercheReference = value;
            }
        }

        //-------------------------------------------------------
        public void AddTypeIgnore(Type tp)
        {
            m_setTypesIgnores.Add(tp);
            RemoveOptions(tp);
        }

        //-------------------------------------------------------
        public void RemoveTypeIgnore ( Type tp )
        {
            if ( tp != null )
                m_setTypesIgnores.Remove ( tp );
        }


        //-------------------------------------------------------
        public void RemoveOptions(Type tp)
        {
            if (tp != null && m_dicTypesAOption.ContainsKey(tp))
                m_dicTypesAOption.Remove(tp);
        }

        //-------------------------------------------------------
        public void AddOption(COptionRechercheType option)
        {
            if (option != null && option.Type != null)
            {
                m_dicTypesAOption[option.Type] = option;
                m_setTypesIgnores.Remove(option.Type);
            }
        }

        //-------------------------------------------------------
        public COptionRechercheType GetOption ( Type tp )
        {
            COptionRechercheType option = null;
            m_dicTypesAOption.TryGetValue(tp, out option);
            return option;
        }

        //-------------------------------------------------------
        public IEnumerable<Type> TypesIgnores
        {
            get
            {
                return m_setTypesIgnores.AsEnumerable<Type>();
            }
        }

        //-------------------------------------------------------
        public bool IsIgnore(Type tp )
        {
            return m_setTypesIgnores.Contains(tp);
        }

        //-------------------------------------------------------
        public IEnumerable<COptionRechercheType> TypesAOptions
        {
            get
            {
                return m_dicTypesAOption.Values.AsEnumerable<COptionRechercheType>();
            }
        }

        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            serializer.TraiteInt(ref m_nLimiteNbPourRechercheReference);
            int nNb = m_setTypesIgnores.Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (Type tp in m_setTypesIgnores)
                    {
                        Type tpTmp = tp;
                        serializer.TraiteType(ref tpTmp);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    m_setTypesIgnores.Clear();
                    for (int n = 0; n < nNb; n++)
                    {
                        Type tpTmp = null;
                        serializer.TraiteType(ref tpTmp);
                        m_setTypesIgnores.Add(tpTmp);
                    } break;
            }
            List<COptionRechercheType> lstOptions;
            switch ( serializer.Mode )
            {
                case ModeSerialisation.Ecriture :
                    lstOptions = new List<COptionRechercheType>(TypesAOptions);
                    result = serializer.TraiteListe<COptionRechercheType>(lstOptions);
                    if ( !result )
                        return result;
                    break;
                case ModeSerialisation.Lecture :
                    lstOptions = new List<COptionRechercheType>();
                    result = serializer.TraiteListe<COptionRechercheType>(lstOptions);
                    if (!result)
                        return result;
                    m_dicTypesAOption.Clear();
                    foreach (COptionRechercheType option in lstOptions)
                        AddOption(option);
                    break;
            }
            return result;
        }
    }
}
