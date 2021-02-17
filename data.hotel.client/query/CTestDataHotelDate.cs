using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public class CTestDataHotelDate : ITestDataHotel
    {
        private EOperateurComparaisonMassStorage m_operateur = EOperateurComparaisonMassStorage.Superieur;
        private DateTime m_dateTest = DateTime.Now;
        
        //-------------------------------------------------------------------
        public CTestDataHotelDate()
        {

        }

        //-------------------------------------------------------------------
        public EOperateurComparaisonMassStorage Operateur
        {
            get
            {
                return m_operateur;
            }
            set
            {
                m_operateur = value;
            }
        }

        //-------------------------------------------------------------------
        public DateTime DateTest
        {
            get
            {
                return m_dateTest;
            }
            set
            {
                m_dateTest = value;
            }
        }



        //-------------------------------------------------------------------
        public bool IsInFilter(string strChamp, IDataRoomEntry record)
        {
            switch (m_operateur)
            {
                case EOperateurComparaisonMassStorage.Superieur:
                    return record.Date >= m_dateTest;
                case EOperateurComparaisonMassStorage.Inferieur:
                    return record.Date <= m_dateTest;
            }
            return true;
        }

        //-------------------------------------------------------------------
        public void ReplaceColumnId(string strOldId, string strNewId)
        {
        }
    }
}
