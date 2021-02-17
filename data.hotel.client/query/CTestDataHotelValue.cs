using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public class CTestDataHotelValue : ITestDataHotel
    {
        private string m_strIdChamp = "";
        private EOperateurComparaisonMassStorage m_operateurComparaison;
        private double m_fValueTest = 0;

        //-------------------------------------------------------------------
        public CTestDataHotelValue()
        {

        }

        //-------------------------------------------------------------------
        public string IdChamp
        {
            get
            {
                return m_strIdChamp;
            }
            set
            {
                m_strIdChamp = value;
            }
        }

        //-------------------------------------------------------------------
        public EOperateurComparaisonMassStorage Operateur
        {
            get
            {
                return m_operateurComparaison;
            }
            set
            {
                m_operateurComparaison = value;
            }
        }

        //-------------------------------------------------------------------
        public double ValeurReference
        {
            get
            {
                return m_fValueTest;
            }
            set
            {
                m_fValueTest = value;
            }
        }

        //-------------------------------------------------------------------
        public bool IsInFilter(string strChamp, IDataRoomEntry record)
        {
            if (strChamp == m_strIdChamp)
            {
                switch (m_operateurComparaison)
                {
                    case EOperateurComparaisonMassStorage.Superieur:
                        return record.Value > m_fValueTest;
                    case EOperateurComparaisonMassStorage.Inferieur:
                        return record.Value < m_fValueTest;
                }
            }
            return true;
        }

        //-------------------------------------------------------------------
        public void ReplaceColumnId(string strOldId, string strNewId)
        {
            if (IdChamp == strOldId)
                IdChamp = strNewId;
        }
    }
}
