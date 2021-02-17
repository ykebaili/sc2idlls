using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data.synchronisation
{
    /// <summary>
    /// Décrit des modifications faites dans une table au titre d'une synchronisation
    /// </summary>
    [Serializable]
    //--------------------------------------
    public class CModifSynchronisation : I2iSerializable
    {
        private string m_strTable = "";
        private int m_nNbRows = 0;
        private CFiltreData m_filtreSynchronisation = null;
        private int m_nIdSyncStart = -1;
        private int m_nIdSyncEnd = -1;

        //--------------------------------------
        public CModifSynchronisation()
        {
        }

        //--------------------------------------
        public CModifSynchronisation(
            string strTable,
            int nNbRows,
            CFiltreData filtreSynchronisation,
            int nIdSyncStart,
            int nIdSyncEnd)
        {
            m_strTable = strTable;
            m_nNbRows = nNbRows;
            m_filtreSynchronisation = filtreSynchronisation;
            m_nIdSyncStart = nIdSyncStart;
            m_nIdSyncEnd = nIdSyncEnd;
        }

        //--------------------------------------
        public string TableName
        {
            get
            {
                return m_strTable;
            }
        }

        //--------------------------------------
        public int NbRows
        {
            get
            {
                return m_nNbRows;
            }
        }

        //--------------------------------------
        public CFiltreData FiltreSynchronisation
        {
            get
            {
                return m_filtreSynchronisation;
            }
        }

        //--------------------------------------
        public int IdSyncStart
        {
            get
            {
                return m_nIdSyncStart;
            }
        }

        //--------------------------------------
        public int IdSyncEnd
        {
            get
            {
                return m_nIdSyncEnd;
            }
            set
            {
                m_nIdSyncEnd = value;
            }
        }

        //--------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strTable);
            serializer.TraiteInt(ref m_nNbRows);
            serializer.TraiteInt(ref m_nIdSyncStart);
            serializer.TraiteInt(ref m_nIdSyncEnd);
            result = serializer.TraiteObject<CFiltreData>(ref m_filtreSynchronisation);
            return result;
        }



    }

    public class CInfoSuppressionSynchronisation : I2iSerializable
    {
        private string m_strNomTable;
        private int m_nIdElementASupprimer;
        private int m_nIdSyncSession;

        //---------------------------------------------
        public CInfoSuppressionSynchronisation()
        {
        }

        //---------------------------------------------
        public CInfoSuppressionSynchronisation(string strTable, int nIdElementASupprimer, int nIdSyncSession)
        {
            m_strNomTable = strTable;
            m_nIdElementASupprimer = nIdElementASupprimer;
            m_nIdSyncSession = nIdSyncSession;
        }

        //---------------------------------------------
        public string TableName
        {
            get
            {
                return m_strNomTable;
            }
        }

        //---------------------------------------------
        public int IdElementASupprimer
        {
            get
            {
                return m_nIdElementASupprimer;
            }
        }
        //---------------------------------------------
        public int IdSyncSession
        {
            get
            {
                return m_nIdSyncSession;
            }
        }

        //---------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomTable);
            serializer.TraiteInt(ref m_nIdElementASupprimer);
            return result;
        }
    }
}
