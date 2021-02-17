using sc2i.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public class CChampHotelCalculeDuree : IChampHotelCalcule
    {
        private string m_strNomChampFinal = "";
        private string m_strIdChampSource = "";
        private ITestDataHotel m_filtre = null;

        //-------------------------------------------------
        public CChampHotelCalculeDuree()
        {
        }

        //-------------------------------------------------
        public CChampHotelCalculeDuree (  
            string strNomChampFinal,
            string strIdChampSource,
            ITestDataHotel filtre)
        {
            m_strNomChampFinal = strNomChampFinal;
            m_strIdChampSource = strIdChampSource;
            m_filtre = filtre;
        }


        //-------------------------------------------------
        public string NomChampFinal
        {
            get
            {
                return m_strNomChampFinal;
            }
            set
            {
                m_strNomChampFinal = value;
            }
        }

        //-------------------------------------------------
        public Type TypeChampFinal
        {
            get
            {
                return typeof(double);
            }
        }

        //-------------------------------------------------
        public string IdChampSource
        {
            get
            {
                return m_strIdChampSource;
            }
            set
            {
                m_strIdChampSource = value;
            }
        }

        //-------------------------------------------------
        public IEnumerable<string> IdsChampsSource
        {
            get
            {
                if (m_strIdChampSource.Length > 0)
                    return new string[] { m_strIdChampSource };
                return new string[0];
            }
        }

        //-------------------------------------------------
        public ITestDataHotel Filtre
        {
            get
            {
                return m_filtre;
            }
            set
            {
                m_filtre = value;
            }
        }

        //-------------------------------------------------
        public void FinaliseCalcul ( 
            string strIdTable,
            DataTable tableRemplieEtTriee,
            IDataRoomServer server,
            DateTime? dateDebut,
            DateTime? dateFin)
        {
            if (dateDebut == null)
                return;
            if ( !tableRemplieEtTriee.Columns.Contains ( NomChampFinal ))
            {
                DataColumn col = new DataColumn(NomChampFinal, typeof(double));
                col.AllowDBNull = true;
                tableRemplieEtTriee.Columns.Add(col);
            }
            Dictionary<string, double?> dicDureesParEntite = new Dictionary<string,double?>();
            Dictionary<string, DateTime> dicLastDates = new Dictionary<string,DateTime>();
            //Identifie toutes les entités
            foreach ( DataRow row in tableRemplieEtTriee.Rows )
            {
                string strEttId = (string)row[CDataHotelTable.c_nomChampTableEntiteId];
                double? fVal = null;
                if (!dicDureesParEntite.TryGetValue(strEttId, out fVal))
                {
                    IDataRoomEntry entry = server.GetFirstNotInSerie(
                    strIdTable,
                    strEttId,
                    IdChampSource,
                    dateDebut.Value,
                    m_filtre);
                    if (entry == null)
                        fVal = 0;
                    else
                    {
                        fVal = ((DateTime)row[CDataHotelTable.c_nomChampTableDate] - entry.Date).TotalSeconds;
                    }
                    dicDureesParEntite[strEttId] = fVal;
                    dicLastDates[strEttId] = (DateTime)row[CDataHotelTable.c_nomChampTableDate];
                    row[NomChampFinal] = fVal.Value;
                }
                else
                {
                    if ( row[IdChampSource] != DBNull.Value )
                    {
                        
                        CDataRoomEntry entry = new CDataRoomEntry(
                            (DateTime)row[CDataHotelTable.c_nomChampTableDate],
                            (double)row[IdChampSource]);
                        if (Filtre.IsInFilter(IdChampSource, entry))
                        {
                            DateTime dt = dicLastDates[strEttId];
                            fVal = dicDureesParEntite[strEttId];
                            fVal += ((DateTime)row[CDataHotelTable.c_nomChampTableDate] - dt).TotalSeconds;
                        }
                        else
                            fVal = 0;
                        dicDureesParEntite[strEttId] = fVal;
                        dicLastDates[strEttId] = (DateTime)row[CDataHotelTable.c_nomChampTableDate];
                        row[NomChampFinal] = fVal.Value;
                    }
                }
            }
        }

    }
}
