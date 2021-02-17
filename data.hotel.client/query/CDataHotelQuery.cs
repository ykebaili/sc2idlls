using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public class CDataHotelQuery
    {
        private DateTime ?m_dateDebut = DateTime.Now;
        private DateTime ?m_dateFin = DateTime.Now;
        private string m_strTableId = "";
        private List<string> m_listeIdsEntites = new List<string>();
        private List<string> m_listeIdsChamps = new List<string>();
        private List<IChampHotelCalcule> m_listeChampsCalcules = new List<IChampHotelCalcule>();
        private ITestDataHotel m_test = null;

        //---------------------------------------------------------------------------------------------
        public CDataHotelQuery()
        {
        }

        //---------------------------------------------------------------------------------------------
        public string TableId
        {
            get
            {
                return m_strTableId;
            }
            set
            {
                m_strTableId = value;
            }
        }

        //---------------------------------------------------------------------------------------------
        public List<string> EntitiesId
        {
            get
            {
                return m_listeIdsEntites;
            }
        }

        //---------------------------------------------------------------------------------------------
        public IEnumerable<string> ChampsId
        {
            get
            {
                return m_listeIdsChamps.AsReadOnly();
            }
            set
            {
                m_listeIdsChamps.Clear();
                if (value != null)
                    m_listeIdsChamps.AddRange(value);
            }
        }

        //---------------------------------------------------------------------------------------------
        public IEnumerable<IChampHotelCalcule> ChampsCalcules
        {
            get
            {
                return m_listeChampsCalcules.AsReadOnly();
            }
            set
            {
                m_listeChampsCalcules.Clear();
                if (value != null)
                    m_listeChampsCalcules.AddRange(value);
            }
        }

        //---------------------------------------------------------------------------------------------
        public ITestDataHotel Filtre
        {
            get
            {
                return m_test;
            }
            set
            {
                m_test = value;
            }
        }

        //---------------------------------------------------------------------------------------------
        public DateTime? DateDebut
        {
            get
            {
                return m_dateDebut;
            }
            set 
            {
                m_dateDebut = value;
            }
        }

        //---------------------------------------------------------------------------------------------
        public DateTime?  DateFin
        {
            get
            {
                return m_dateFin;
            }
            set
            {
                m_dateFin = value;
            }
        }

        //---------------------------------------------------------------------------------------------
        public CDataHotelQuery Clone(bool bAvecEntites)
        {
            CDataHotelQuery query = new CDataHotelQuery();
            query.m_dateDebut = m_dateDebut;
            query.m_dateFin = m_dateFin;
            query.m_strTableId = m_strTableId;
            if ( bAvecEntites)
                query.m_listeIdsEntites.AddRange(m_listeIdsEntites);
            query.m_listeIdsChamps.AddRange(m_listeIdsChamps);
            query.m_test = m_test;
            query.ChampsCalcules = ChampsCalcules;    
            return query;
        }
    }
}
