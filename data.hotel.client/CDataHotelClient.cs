using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client
{
    public class CDataHotelClient : MarshalByRefObject, IDataHotelClient
    {
        public const string c_RoomTypeName = "IDataRoomServer";
        public const string c_HotelAllocatorTypeName = "IDataHotelClientAllocator";
        private string m_strHotelURL = "";

        private static CDataHotelClient m_defaultInstance = null;

        //Si le client ne se connecte pas directement à l'hotel, mais 
        //passe par un proxy, m_strProxyURL contient l'adresse du proxy
        //Forme tcp://ip:port/NomClasse
        private static string m_strProxyURL = null;

        [Serializable]
        public struct SDataToSend
        {
            public string TableId;
            public string FieldId;
            public string EntityId;
            public DateTime DataDate;
            public double Value;

            public SDataToSend ( string strTableId, 
                string strFieldId, 
                string strEntityId, 
                DateTime dataDate,
                double fValue )
            {
                TableId = strTableId;
                FieldId = strFieldId;
                EntityId = strEntityId;
                DataDate = dataDate;
                Value = fValue;
            }
        }

        private static List<SDataToSend> m_listeDataToSend = new List<SDataToSend>();

        private static bool m_bUseTampon = true;

        //-----------------------------------------------
        public static CDataHotelClient DefaultInstance
        {
            get
            {
                if (m_defaultInstance == null)
                    m_defaultInstance = new CDataHotelClient();
                return m_defaultInstance;
            }
        }

        //-----------------------------------------------
        public static void InitForUseProxy ( string strProxyURL )
        {
            if (strProxyURL == "")
                strProxyURL = null;
            m_strProxyURL = strProxyURL;
        }

        //-----------------------------------------------
        //Permet d'allouer à distance un proxy client.
        public static void RegisterProxyForClients()
        {
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(CDataHotelClientAllocator), c_HotelAllocatorTypeName, WellKnownObjectMode.Singleton);
        }

        //-----------------------------------------------
        public CDataHotelClient()
        {
        }

        //-----------------------------------------------
        public CDataHotelClient(string strIp, int nPort)
        {
            Init(strIp, nPort);
        }

        //-----------------------------------------------
        public void Init ( string strHotelURL )
        {
            m_strHotelURL = strHotelURL;
        }

        //-----------------------------------------------
        public void Init(string strIp, int nPort)
        {
            m_strHotelURL = "tcp://" + strIp + ":" + nPort;
        }
        
        //-----------------------------------------------
        public IDataRoomServer GetRoomServer()
        {
            if ( m_strProxyURL!= null && m_strProxyURL.Length > 0 )
            {
                try
                {
                    IDataHotelClientAllocator al = Activator.GetObject(typeof(IDataHotelClientAllocator), m_strProxyURL + "/" + c_HotelAllocatorTypeName) as IDataHotelClientAllocator;
                    IDataHotelClient client = al.GetNewClient();
                    client.Init(m_strHotelURL);
                    return client.GetRoomServerProxy();
                }
                catch ( Exception e )
                {
                    throw e;
                }
            }
            IDataRoomServer srv = Activator.GetObject(typeof(IDataRoomServer), m_strHotelURL+"/"+c_RoomTypeName) as IDataRoomServer;
            return srv;
        }

        public class CLockerListToSend { }

        //-----------------------------------------------
        public void StoreDataToSend(string strIdTable, string strIdChamp, string strIdEntite, DateTime dataDate, double fValue)
        {
            lock (typeof(CLockerListToSend))
                m_listeDataToSend.Add(new SDataToSend(strIdTable, strIdChamp, strIdEntite, dataDate, fValue));
        }

        //-----------------------------------------------
        public CResultAErreur FlushData()
        {
            List<SDataToSend> lstToSend = null;
            lock (typeof(CLockerListToSend))
            {
                lstToSend = m_listeDataToSend;
                m_listeDataToSend = new List<SDataToSend>();
            }
            return GetRoomServer().SendData(lstToSend);
            
        }

        //-----------------------------------------------
        public IDataRoomServer GetRoomServerProxy()
        {
            IDataRoomServer srv = GetRoomServer();
            return new CDataRoomProxy(srv);
        }

        //-----------------------------------------------
        public bool Ping()
        {
            return GetRoomServer().Ping();
        }

        //-----------------------------------------------
        public bool SafePing()
        {
            try
            {
                return Ping();
            }
            catch
            {

            }
            return false;
        }
    }
}
