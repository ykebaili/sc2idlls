using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.expression.FonctionsDynamiques
{
    [Serializable]
    public class CParametreFonctionDynamique : I2iSerializable
    {
        private int m_nNumParametre = 0;
        private string m_strNom = "";
        private CTypeResultatExpression m_dataType = new CTypeResultatExpression(typeof(string), false);

        //--------------------------------------------------
        public CParametreFonctionDynamique()
        {
        }

        //--------------------------------------------------
        public int NumParametre
        {
            get
            {
                return m_nNumParametre;
            }
            set
            {
                m_nNumParametre = value;
            }
        }

        //--------------------------------------------------
        public string Nom
        {
            get
            {
                return m_strNom;
            }
            set
            {
                m_strNom = value;
            }
        }



        //--------------------------------------------------
        public CTypeResultatExpression TypeResultatExpression
        {
            get
            {
                return m_dataType;
            }
            set
            {
                if (value != null)
                    m_dataType = value;
            }
        }

        //----------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteInt ( ref m_nNumParametre );
            serializer.TraiteString ( ref m_strNom );
            result = serializer.TraiteObject<CTypeResultatExpression>(ref m_dataType);
            if ( !result )
                return result;
            return result;
        }

    }
}
