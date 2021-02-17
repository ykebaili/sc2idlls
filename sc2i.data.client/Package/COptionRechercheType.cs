using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.Package
{
    [Serializable]
    public class COptionRechercheType : I2iSerializable
    {
        private Type m_type = null;

        //Indique qu'une recherche récursive est automatiquement 
        //appliquée sur les éléments de ce type
        private bool m_bRecursiveSearch = false;

        //-------------------------------------------------
        public COptionRechercheType()
        {

        }

        //-------------------------------------------------
        public COptionRechercheType(Type tp )
        {
            m_type = tp;
        }

        //-------------------------------------------------
        public Type Type
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

        //-------------------------------------------------
        public bool RecursiveSearch
        {
            get
            {
                return m_bRecursiveSearch;
            }
            set
            {
                m_bRecursiveSearch = value;
            }
        }

        //-------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteType(ref m_type);
            serializer.TraiteBool ( ref m_bRecursiveSearch );
            return result;
        }
    }
}
