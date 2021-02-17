using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    public enum ETypeLigneLogImport
    {
        Info =0,
        Alert = 1,
        Error = 2
    }

    [Serializable]
    public class CLigneLogImport : I2iSerializable
    {
        private ETypeLigneLogImport m_typeLigne = ETypeLigneLogImport.Info;
        [NonSerialized]
        private DataRow m_row = null;
        private string m_strColonne = "";
        private string m_strProprieteDest = "";
        private string m_strMessage = "";
        private int? m_nRowIndex=null;

        //Null = ça ne concerne qu'une seule ligne
        private int? m_nEndRowIndex = null;
        private string m_strIdsConfigMappage = "";

        //------------------------------------------------------------
        public CLigneLogImport()
        {
        }

        //------------------------------------------------------------
        public CLigneLogImport(ETypeLigneLogImport typeLigne,
            DataRow rowSource,
            string strColonne,
            CContexteImportDonnee contexteImport,
            string strMessage)
        {
            m_typeLigne = typeLigne;
            m_row = rowSource;
            m_strColonne = strColonne;
            m_strMessage = strMessage;
            if (contexteImport != null)
            {
                m_strProprieteDest = contexteImport.ChampEnCours;
                m_nRowIndex = contexteImport.CurrentRowIndex;
                m_strIdsConfigMappage = contexteImport.IdsConfigsEnCours;
            }
        }

        //------------------------------------------------------------
        public CLigneLogImport(ETypeLigneLogImport typeLigne,
            DataRow rowSource,
            string strColonne,
            int nStartRowIndex,
            int nEndRowIndex,
            string strMessage)
        {
            m_typeLigne = typeLigne;
            m_row = rowSource;
            m_strColonne = strColonne;
            m_strMessage = strMessage;
            m_nEndRowIndex = nEndRowIndex;
            m_nRowIndex = nStartRowIndex;
        }

        //------------------------------------------------------------
        public int? RowIndex
        {
            get
            {
                return m_nRowIndex;
            }
        }

        //------------------------------------------------------------
        public int? EndRowIndex
        {
            get
            {
                return m_nEndRowIndex;
            }
        }

        //------------------------------------------------------------
        public ETypeLigneLogImport TypeLigne
        {
            get { return m_typeLigne; }
        }

        //------------------------------------------------------------
        public DataRow SourceRow
        {
            get
            {
                return m_row;
            }
        }

        //------------------------------------------------------------
        public string SourceColumn
        {
            get { return m_strColonne;  }
        }

        //------------------------------------------------------------
        public string ProprieteDest
        {
            get
            {
                return m_strProprieteDest;
            }
        }

        //------------------------------------------------------------
        public string Message
        {
            get
            {
                return m_strMessage;
            }
        }

        //------------------------------------------------------------
        public string IdsConfigMappage
        {
            get
            {
                return m_strIdsConfigMappage;
            }
        }

        //---------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteEnum<ETypeLigneLogImport>(ref m_typeLigne);
            serializer.TraiteString(ref m_strColonne);
            serializer.TraiteString(ref m_strProprieteDest);
            serializer.TraiteString(ref m_strMessage);
            serializer.TraiteIntNullable(ref m_nRowIndex);
            serializer.TraiteIntNullable(ref m_nEndRowIndex);
            serializer.TraiteString(ref m_strIdsConfigMappage);
            return result;
        }

                    
    }
}
