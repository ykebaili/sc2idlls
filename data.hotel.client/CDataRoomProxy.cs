using data.hotel.client.query;
using sc2i.common;
using sc2i.common.memorydb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client
{
    public class CDataRoomProxy : MarshalByRefObject, IDataRoomServer
    {
        private IDataRoomServer m_server = null;

        //-----------------------------------------------------------
        public CDataRoomProxy ( IDataRoomServer serveur )
        {
            m_server = serveur;
        }

        //-----------------------------------------------------------
        public CResultAErreur UpdateConfig(CMemoryDb database)
        {
            return m_server.UpdateConfig(database);
        }

        //-----------------------------------------------------------
        public CMemoryDb GetConfig()
        {
            return m_server.GetConfig();
        }

        //-----------------------------------------------------------
        public bool Ping()
        {
            return m_server.Ping();
        }

        //-----------------------------------------------------------
        public string RoomId
        {
            get { return m_server.RoomId; }
        }

        //-----------------------------------------------------------
        public CResultAErreur SendData(string strIdTable, string strIdChamp, string strIdEntite, DateTime dateDate, double fValue)
        {
            return m_server.SendData(strIdTable, strIdChamp, strIdEntite, dateDate, fValue);
        }

        //-----------------------------------------------------------
        public CResultAErreur SendData(List<CDataHotelClient.SDataToSend> data)
        {
            return m_server.SendData(data);
        }

        //-----------------------------------------------------------
        public CResultAErreur SendDataDirect(string strIdTable, string strIdChamp, string strIdEntite, DateTime dateDate, double fValue)
        {
            return m_server.SendDataDirect(strIdTable, strIdChamp, strIdEntite, dateDate, fValue);
        }

        //-----------------------------------------------------------
        public void SendDataDirectWithTableAndFieldId(string strIdTable, string strIdChamp, string strIdEntite, DateTime dateDate, double fValue)
        {
            m_server.SendDataDirect(strIdTable, strIdChamp, strIdEntite, dateDate, fValue);
        }

        //-----------------------------------------------------------
        public IEnumerable<string> GetEntities(string strIdTable, DateTime dataStart, DateTime dataEnd)
        {
            return m_server.GetEntities(strIdTable, dataStart, dataEnd);
        }

        //-----------------------------------------------------------
        public IEnumerable<string> GetEntitiesDirect(string strIdTable, DateTime dataStart, DateTime dataEnd)
        {
            return m_server.GetEntitiesDirect(strIdTable, dataStart, dataEnd);
        }

        //-----------------------------------------------------------
        public CResultAErreurType<CDataTableFastSerialize> GetData(CDataHotelQuery query)
        {
            return m_server.GetData(query);
        }

        //-----------------------------------------------------------
        public CResultAErreurType<CDataTableFastSerialize> GetDataDirect(CDataHotelQuery query)
        {
            return m_server.GetDataDirect(query);
        }

        //-----------------------------------------------------------
        public CDataSetFastSerialize GetDataSetModele()
        {
            return m_server.GetDataSetModele();
        }

        //-----------------------------------------------------------
        public IDataRoomEntry GetFirstNotInSerie(string strTableId, string strEntityId, string strFieldId, DateTime dateRecherche, ITestDataHotel filtre)
        {
            return m_server.GetFirstNotInSerie(strTableId, strEntityId, strFieldId, dateRecherche, filtre);
        }

        //-----------------------------------------------------------
        public IDataRoomEntry GetFirstNotInSerieDirect(string strTableId, string strEntityId, string strFieldId, DateTime dateRecherche, ITestDataHotel filtre)
        {
            return m_server.GetFirstNotInSerieDirect(strTableId, strEntityId, strFieldId, dateRecherche, filtre);
        }

        //-----------------------------------------------------------
        public double GetDepuisCombienDeTempsEnSDirect(string strTableId, string strEntityId, string strFieldId, DateTime dateRecherche, ITestDataHotel filtre)
        {
            return m_server.GetDepuisCombienDeTempsEnSDirect(strTableId, strEntityId, strFieldId, dateRecherche, filtre);
        }
    }
}
