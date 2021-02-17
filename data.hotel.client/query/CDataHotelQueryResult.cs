using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public class CDataHotelQueryResultLine
    {
        private string m_strIdEntite;
        private DateTime m_date;

        private List<double> m_listeDonnees = new List<double>();

        public CDataHotelQueryResultLine()
        {

        }
    }
    [Serializable]
    public class CDataHotelQueryResult
    {
        private List<string> m_listeChamps = new List<string>();
        private List<CDataHotelQueryResultLine> m_listeLignes = new List<CDataHotelQueryResultLine>();


        
    }
}
