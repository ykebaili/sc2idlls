using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CValeurParDefautObjetDonnee : I2iSerializable
    {
        private Type m_typeObjet = null;
        private CDbKey m_keyObjet = null;
        private string m_strLibelleObjet = "";

        //-------------------------------------------------
        public CValeurParDefautObjetDonnee()
        {
        }

        //-------------------------------------------------
        public CValeurParDefautObjetDonnee ( Type typeObjet, CDbKey key, string strLibelle )
        {
            m_typeObjet = typeObjet;
            m_keyObjet = key;
            m_strLibelleObjet = strLibelle;
        }

        //-------------------------------------------------
        public Type TypeObjet
        {
            get
            {
                return m_typeObjet;
            }
            set { m_typeObjet = value; }
        }

        //-------------------------------------------------
        public CDbKey KeyObjet
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

        //-------------------------------------------------
        public string LibelleObjet
        {
            get
            {
                return m_strLibelleObjet;
            }
            set
            {
                m_strLibelleObjet = value;
            }
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
            serializer.TraiteType(ref m_typeObjet);
            serializer.TraiteDbKey(ref m_keyObjet);
            serializer.TraiteString(ref m_strLibelleObjet);
            return result;
        }

        //-------------------------------------------------
        public CObjetDonnee GetObjet ( CContexteDonnee ctx)
        {
            if ( m_typeObjet != null && m_keyObjet != null )
            {
                CObjetDonnee objet = Activator.CreateInstance(m_typeObjet, new object[] { ctx }) as CObjetDonnee;
                if (objet != null && objet.ReadIfExists(m_keyObjet))
                    return objet;
            }
            return null;
        }

        //-------------------------------------------------
        public override string ToString()
        {
            return m_strLibelleObjet;
        }
    }
}
