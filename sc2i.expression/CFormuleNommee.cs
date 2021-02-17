using System;
using System.Collections.Generic;
using System.Text;

using sc2i.common;

namespace sc2i.expression
{
    /// <summary>
    /// Une formule avec un nom !
    /// </summary>
    [Serializable]
    public class CFormuleNommee : I2iSerializable
    {
        private C2iExpression m_formule = null;
        private string m_strLibelle = "";
        private string m_strId = "";


        public CFormuleNommee()
        {
            m_strId = Guid.NewGuid().ToString();
        }

        public CFormuleNommee(string strLibelle, C2iExpression formule)
            :this()
        {
            Libelle = strLibelle;
            m_formule = formule;
        }

        [DynamicField("Label")]
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

        [DynamicField("Formula string")]
        public string FormuleString
        {
            get
            {
                if (m_formule != null)
                    return m_formule.GetString();
                return "";
            }
        }

        public C2iExpression Formule
        {
            get
            {
                return m_formule;
            }
            set
            {
                m_formule = value;
            }
        }

        public string Id
        {
            get
            {
                return m_strId;
            }
            set
            {
                m_strId = value;
            }
        }

        #region I2iSerializable Membres
        private int GetNumVersion()
        {
            return 1;
            //1 : ajout de l'id
        }

        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strLibelle);
            result = serializer.TraiteObject<C2iExpression>(ref m_formule);
            if (nVersion >= 1)
                serializer.TraiteString(ref m_strId);
            if ( serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();
            return result;
        }

        #endregion
    }

    public class CTableauFormuleNommeeConvertor : CGenericObjectConverter<CFormuleNommee[]>
    {
        
        public override string GetString(CFormuleNommee[] value)
        {
            if (value != null)
                return I.T("@1 formulas|20145", value.Length.ToString());
            return "";
        }
    }
}
