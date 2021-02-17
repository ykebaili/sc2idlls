using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery
{
    [Serializable]
    public class CParametreEasyQueryConnexion : I2iSerializable
    {
        private string m_strNomConnexion = "";
        private List<CFormuleNommee> m_listeFormulesParametres = new List<CFormuleNommee>();
        private string m_strIdTypeConnexion = "";
        private string m_strSourceId = "";

        //--------------------------------------------------------
        public CParametreEasyQueryConnexion()
        {
            m_strSourceId = CUniqueIdentifier.GetNew();
        }

        //--------------------------------------------------------
        public string SourceId
        {
            get
            {
                return m_strSourceId;
            }
        }

        //--------------------------------------------------------
        public string NomConnexion
        {
            get
            {
                return m_strNomConnexion;
            }
            set
            {
                m_strNomConnexion = value;
            }
        }

        //--------------------------------------------------------
        public IEnumerable<CFormuleNommee> FormulesParametres
        {
            get
            {
                return m_listeFormulesParametres.AsReadOnly();
            }
            set
            {
                List<CFormuleNommee> lst = new List<CFormuleNommee>();
                if (value != null)
                    lst.AddRange(value);
                m_listeFormulesParametres = lst;
            }
        }

        //--------------------------------------------------------
        public string IdTypeConnexion
        {
            get
            {
                return m_strIdTypeConnexion;
            }
            set
            {
                m_strIdTypeConnexion = value;
            }
        }

        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomConnexion);
            serializer.TraiteString(ref m_strSourceId);
            serializer.TraiteString(ref m_strIdTypeConnexion);
            result = serializer.TraiteListe<CFormuleNommee>(m_listeFormulesParametres);
            if (!result)
                return result;
            if (serializer.IsForClone)
                m_strSourceId = CUniqueIdentifier.GetNew();
            return result;
        }

        



    }

}
