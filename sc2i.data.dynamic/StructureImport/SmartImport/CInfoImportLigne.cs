using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CInfoElementImport : I2iSerializable
    {
        public Type TypeElement = null;
        private CDbKey KeyElement = null;

        public CInfoElementImport()
        {
        }

        public CInfoElementImport ( CObjetDonnee element )
        {
            TypeElement = element.GetType();
            KeyElement = element.DbKey;
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteType(ref TypeElement);
            serializer.TraiteDbKey(ref KeyElement);
            return result;
        }
    }

    //-----------------------------------------------------------
    [Serializable]
    public class CInfoImportLigne : I2iSerializable
    {
        private int m_nNumLigne;
        private CInfoElementImport m_infoElementRoot = null;

        //-----------------------------------------------------------
        public CInfoImportLigne()
        {
        }

        //-----------------------------------------------------------
        public CInfoImportLigne(int nNumLigne, CObjetDonnee objet)
        {
            m_nNumLigne = nNumLigne;
            m_infoElementRoot = new CInfoElementImport(objet);
        }

        //-----------------------------------------------------------
        public int NumLigne
        {
            get
            {
                return m_nNumLigne;
            }
        }

        //-----------------------------------------------------------
        public CInfoElementImport InfoElementRoot
        {
            get
            {
                return m_infoElementRoot;
            }
        }

        //-----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteInt(ref m_nNumLigne);
            result = serializer.TraiteObject<CInfoElementImport>(ref m_infoElementRoot);
            return result;
        }

       
    }

    //-----------------------------------------------------------
    [Serializable]
    public class CElementsAjoutesPendantImport : I2iSerializable
    {
        private Dictionary<Type, HashSet<CDbKey>> m_dicElementsAjoutes = new Dictionary<Type, HashSet<CDbKey>>();

        public CElementsAjoutesPendantImport()
        {
        }

        //-----------------------------------------------------------
        public void SetElementAjoute ( CObjetDonnee objet )
        {
            if ( objet == null )
                return;
            HashSet<CDbKey> set = null;
            if (!m_dicElementsAjoutes.TryGetValue(objet.GetType(), out set))
            {
                set = new HashSet<CDbKey>();
                m_dicElementsAjoutes[objet.GetType()] = set;
            }
            set.Add(objet.DbKey);
        }

        //-----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            switch ((serializer.Mode))
            {
                case ModeSerialisation.Lecture:
                    m_dicElementsAjoutes = new Dictionary<Type, HashSet<CDbKey>>();
                    int nNbSets = 0;
                    serializer.TraiteInt(ref nNbSets);
                    for ( int n = 0; n < nNbSets; n++ )
                    {
                        Type tp = null;
                        serializer.TraiteType(ref tp);
                        int nNbKeys = 0;
                        serializer.TraiteInt(ref nNbKeys);
                        HashSet<CDbKey> set = new HashSet<CDbKey>();
                        for (int nKey = 0; nKey < nNbKeys; nKey++)
                        {
                            CDbKey k = null;
                            serializer.TraiteDbKey(ref k);
                            set.Add(k);
                        }
                        m_dicElementsAjoutes[tp] = set;
                    }
                    break;
                case ModeSerialisation.Ecriture:
                    int nNbSetsW = m_dicElementsAjoutes.Count();
                    serializer.TraiteInt(ref nNbSetsW);
                    foreach (KeyValuePair<Type, HashSet<CDbKey>> kv in m_dicElementsAjoutes)
                    {
                        Type tp = kv.Key;
                        serializer.TraiteType(ref tp);
                        int nNbKeysW = kv.Value.Count();
                        serializer.TraiteInt(ref nNbKeysW);
                        foreach (CDbKey key in kv.Value)
                        {
                            CDbKey kw = key;
                            serializer.TraiteDbKey(ref kw);
                        }
                    }
                    break;
            }
            return result;
        }
    }

}
