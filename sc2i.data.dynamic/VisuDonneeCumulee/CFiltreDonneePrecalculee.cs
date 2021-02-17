using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
    [Serializable]
    public class CFiltreDonneePrecalculee : I2iSerializable
    {
        private CFiltreDynamique m_filtre = null;
        private string m_strLibelle = "";
        private string m_strChampAssocie = "";
        private C2iExpression m_formuleDescriptif = null;

        public CFiltreDonneePrecalculee()
        {
        }

        public CFiltreDonneePrecalculee(string strChampAssocie,
            string strLibelle,
            CFiltreDynamique filtre)
        {
            m_filtre = filtre;
            m_strChampAssocie = strChampAssocie;
            m_strLibelle = strLibelle;
        }

        public CFiltreDynamique Filtre
        {
            get
            {
                return m_filtre;
            }
        }

        public string ChampAssocie
        {
            get
            {
                return m_strChampAssocie;
            }
        }

        public string Libelle
        {
            get
            {
                return m_strLibelle;
            }
            set
            {
                m_strLibelle = value;
            }
        }

        public C2iExpression FormuleDescription
        {
            get
            {
                return m_formuleDescriptif;
            }
            set
            {
                m_formuleDescriptif = value;
            }
        }



        #region I2iSerializable Membres
        private int GetNumVersion()
        {
            return 1;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strChampAssocie);
            serializer.TraiteString(ref m_strLibelle);
            result = serializer.TraiteObject<CFiltreDynamique>(ref m_filtre);
            if (!result)
                return result;
            if (nVersion >= 1)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleDescriptif);
            if (!result)
                return result;
            return result;
        }

        #endregion
    }
}
