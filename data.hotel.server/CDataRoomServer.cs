using sc2i.common.memorydb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using sc2i.common;
using System.IO;
using System.Threading;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using data.hotel.client;
using data.hotel.client.query;

namespace data.hotel.server
{
    public class CDataRoomServer : MarshalByRefObject, IDataRoomServer
    {
        private static CMemoryDb m_database;
        private static string m_strRoomId = "";
        private static CDataRoomOptimizedDataBase m_optimizedDb = new CDataRoomOptimizedDataBase(null);

        //----------------------------------------------------------------
        public static CResultAErreur Init ( string strFichierRemoting, bool bDoRemotingConfiguration )
        {
            m_strRoomId = "";
            CResultAErreur result = CResultAErreur.True;
            try
            {
                if (bDoRemotingConfiguration)
                {
                    RemotingConfiguration.Configure(strFichierRemoting, false);
                    RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
                }

                RemotingConfiguration.RegisterWellKnownServiceType(typeof(CDataRoomServer),
                    CDataHotelClient.c_RoomTypeName, WellKnownObjectMode.Singleton);
            }
            catch (Exception e )
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur("Remoting configuration error in file "+strFichierRemoting);
                return result;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(strFichierRemoting);
            XmlElement node = doc["configuration"];
            if (node != null)
            {
                node = node["data.hotel"];
                if (node != null)
                {
                    node = node["roomid"];
                    if (node != null)
                        m_strRoomId = node.InnerText;
                }
            }
            if ( m_strRoomId == "" )
            {
                result.EmpileErreur("Bad RoomId in configuration file " + strFichierRemoting);
            }
            ReadConfig();
            return result;
        }

        //----------------------------------------------------------------
        public static CResultAErreur InitForTests(string strRoomId )
        {
            m_strRoomId = strRoomId;
            ReadConfig();
            return CResultAErreur.True;
        }

        //----------------------------------------------------------------
        private static string GetHotelConfigPath()
        {
            string strPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            if (strPath[strPath.Length - 1] != '\\')
                strPath += "\\";
            strPath += "Futurocom.data.hotel\\";
            strPath += m_strRoomId + ".hotel.config";
            CUtilRepertoire.AssureRepertoirePourFichier(strPath);
            return strPath;
        }

        //----------------------------------------------------------------
        public static CResultAErreur ReadConfig()
        {
            string strFile = GetHotelConfigPath();
            CResultAErreur result = CResultAErreur.True;
            if (File.Exists(strFile))
            {
                CMemoryDb db = new CMemoryDb();
                result = CSerializerObjetInFile.ReadFromFile(db, "DATA_HOTEL_CONFIG", strFile);
                if (!result)
                    return result;
                m_database = db;
                UpdateOptimizedDb();
                
            }
            return result;
        }

        //----------------------------------------------------------------
        private static void UpdateOptimizedDb()
        {
            CDataHotel hotel = null;
            if (m_database != null )
            {
                CListeEntitesDeMemoryDb<CDataHotel> lst = new CListeEntitesDeMemoryDb<CDataHotel>(m_database);
                if (lst.Count() > 0)
                    hotel = lst.ElementAt(0);
            }
            m_optimizedDb = new CDataRoomOptimizedDataBase(hotel);
        }

        //----------------------------------------------------------------
        public static CResultAErreur SaveConfig(CMemoryDb db)
        {
            string StrFile = GetHotelConfigPath();
            CResultAErreur result = CSerializerObjetInFile.SaveToFile(db, "DATA_HOTEL_CONFIG", StrFile);
            return result;
        }

        //----------------------------------------------------------------
        public string RoomId
        {
            get
            {
                return m_strRoomId;
            }
        }

        //-------------------------------------------------------------------------------------
        public CMemoryDb GetConfig()
        {
            return m_database;
        }

        //----------------------------------------------------------------
        public CResultAErreur UpdateConfig( CMemoryDb database )
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                result = SaveConfig(database);
                if (!result)
                    return result;
                m_database = database;
                UpdateOptimizedDb();
                CDataDispatch.ResetDispatchCache();
                return result;
            }
            catch ( Exception e )
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;            
        }

        //----------------------------------------------------------------
        private string GetRoomConnexionString ( CDataRoom room )
        {
            return "tcp://" + room.RoomIp + ":" + room.RoomPort;
        }

        //----------------------------------------------------------------
        public CResultAErreur SendData(string strIdTable, string strIdChamp, string strIdEntite, DateTime dataDate, double fValue)
        {
            CResultAErreur result = CResultAErreur.True;
            CDataRoom room = CDataDispatch.GetRoomFor(strIdEntite, m_database);

            try
            {
                IDataRoomServer srv = GetRoomServer(room);
                if (srv != null)
                    result = srv.SendDataDirect(strIdTable, strIdChamp, strIdEntite, dataDate, fValue);
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }


            return result;
        }

        //----------------------------------------------------------------
        public CResultAErreur SendData ( List<CDataHotelClient.SDataToSend> datas )
        {
            CResultAErreur result = CResultAErreur.True;
            foreach ( CDataHotelClient.SDataToSend data in datas )
            {
                CResultAErreur rs = SendDataDirect(data.TableId, data.FieldId, data.EntityId, data.DataDate, data.Value);
                if (!rs)
                    result.EmpileErreur(rs.Erreur);
            }
            return result;
        }

        //----------------------------------------------------------------
        public CResultAErreur SendDataDirect ( string strIdTable, string strIdChamp, string strIdEntite, DateTime dataDate, double fValue)
        {
            CResultAErreur result = CResultAErreur.True;
            COptimizedDataRoomTable table = m_optimizedDb.GetTable(strIdTable);
            if (table != null)
            {
                strIdTable = table.TableId;
                strIdChamp = table.GetFieldId(strIdChamp);
                if (strIdChamp != null)
                    return CDataRoomManager.Instance.SetData(strIdTable, strIdEntite, strIdChamp, dataDate, fValue);
                else
                    result.EmpileErreur("Unknown field '" + strIdChamp + "'");
            }
            else
                result.EmpileErreur("Unknown table '" + strIdTable + "'");
            return result;
        }

        //----------------------------------------------------------------
        public IEnumerable<string> GetEntities ( string strIdTable, DateTime dataStart, DateTime dataEnd )
        {
            HashSet<string> setIds = new HashSet<string>();
            COptimizedDataRoomTable table = m_optimizedDb.GetTable(strIdTable);
            if ( table != null )
                strIdTable = table.TableId;
            CListeEntitesDeMemoryDb<CDataRoom> lstRooms = new CListeEntitesDeMemoryDb<CDataRoom>(m_database);
            foreach ( CDataRoom room in lstRooms )
            {
                try
                {
                    IDataRoomServer srv = GetRoomServer(room);
                    if (srv != null)
                    {
                        IEnumerable<string> lst = srv.GetEntitiesDirect(strIdTable, dataStart, dataEnd);
                        if (lst != null)
                            foreach (string strId in lst)
                                setIds.Add(strId);
                    }
                }
                catch { }
            }
            return setIds;
        }

        //----------------------------------------------------------------
        public IEnumerable<string> GetEntitiesDirect ( string strIdTable, DateTime dataStart, DateTime dataEnd )
        {
            return CDataRoomManager.Instance.GetEntities(strIdTable, dataStart, dataEnd);
        }

        //----------------------------------------------------------------
        public void SendDataDirectWithTableAndFieldId ( 
            string strIdTable, 
            string strIdChamp, 
            string strIdEntite,
            DateTime dataDate, 
            double fValue)
        {
            CDataRoomManager.Instance.SetData(strIdTable, strIdEntite, strIdChamp, dataDate, fValue);
        }

        //----------------------------------------------------------------
        private IDataRoomServer GetRoomServer ( CDataRoom room )
        {
            if (room == null)
                return this;
            if (room.Id == RoomId)
                return this;
            return Activator.CreateInstance(typeof(IDataRoomServer), GetRoomConnexionString(room)) as IDataRoomServer;
        }


        //----------------------------------------------------------------
        public CResultAErreurType<CDataTableFastSerialize> GetData(CDataHotelQuery query)
        {
            //Explose la requête pour chaque serveur de destination
            Dictionary<CDataRoom, CDataHotelQuery> dicRoomToQuery = new Dictionary<CDataRoom, CDataHotelQuery>();
            CListeEntitesDeMemoryDb<CDataRoom> allRooms = new CListeEntitesDeMemoryDb<CDataRoom>(m_database);
            foreach ( string strIdEntite in query.EntitiesId)
            {
                //Si le dispatch ne sait pas qui interroger, on interroge tout le monde !
                CDataRoom room = CDataDispatch.GetRoomFor(strIdEntite, m_database);
                List<CDataRoom> lstToAsk = new List<CDataRoom>();
                if (room != null)
                    lstToAsk.Add(room);
                else
                    lstToAsk.AddRange(allRooms);
                foreach ( CDataRoom roomToAsk in lstToAsk )
                {
                    CDataHotelQuery specQuery = null;
                    if ( !dicRoomToQuery.TryGetValue(roomToAsk, out specQuery))
                    {
                        specQuery = query.Clone(false);
                        dicRoomToQuery[roomToAsk] = specQuery;
                    }
                    specQuery.EntitiesId.Add ( strIdEntite );
                }
            }
            CResultAErreurType<CDataTableFastSerialize> myResult = new CResultAErreurType<CDataTableFastSerialize>();
            DataTable tableResult = new DataTable();
            foreach ( KeyValuePair<CDataRoom, CDataHotelQuery> kv in dicRoomToQuery )
            {
                CDataRoom room = kv.Key;
                CResultAErreurType<CDataTableFastSerialize> res = new CResultAErreurType<CDataTableFastSerialize>();
                IDataRoomServer srv = GetRoomServer ( room );
                if ( srv!= null )
                {
                    res = srv.GetDataDirect(kv.Value);
                    if ( res && res.DataType != null )
                    {
                        DataTable tableTmp = res.DataType;
                        tableResult.Merge ( tableTmp);
                    }
                }
            }
            //S'assure que la table résultat contient bien toutes les colonnes demandées
            foreach ( string strIdCol in query.ChampsId )
            {
                if (tableResult.Columns[strIdCol] == null)
                    tableResult.Columns.Add(strIdCol, typeof(double));
            }
            tableResult.DefaultView.Sort = CDataHotelTable.c_nomChampTableDate+","+CDataHotelTable.c_nomChampTableEntiteId ;
            DataTable tableFinale = tableResult.Clone();
            tableFinale.BeginLoadData();
            foreach ( DataRowView rv in tableResult.DefaultView )
            {
                DataRow row = tableFinale.NewRow();

                foreach (DataColumn col in tableFinale.Columns)
                {
                    row[col.ColumnName] = rv[col.ColumnName];
                }
                tableFinale.Rows.Add(row);
            }
            tableFinale.EndLoadData();
            tableResult = tableFinale;
            foreach (IChampHotelCalcule champCalcule in query.ChampsCalcules)
            {
                champCalcule.FinaliseCalcul(
                    query.TableId,
                    tableResult,
                    this,
                    query.DateDebut,
                    query.DateFin);
            }

            myResult.DataType = tableResult;
            return myResult;
        }

        //-------------------------------------------------------------------------------------
        public CResultAErreurType<CDataTableFastSerialize> GetDataDirect(CDataHotelQuery query)
        {
            CResultAErreurType<CDataTableFastSerialize> res = new CResultAErreurType<CDataTableFastSerialize>();
            res.DataType = CDataRoomManager.Instance.GetData(query);
            return res;
        }

        //-------------------------------------------------------------------------------------
        public CDataSetFastSerialize GetDataSetModele()
        {
            DataSet ds = new DataSet();
            CListeEntitesDeMemoryDb<CDataHotelTable> lstTables = new CListeEntitesDeMemoryDb<CDataHotelTable>(m_database);
            foreach (CDataHotelTable table in lstTables)
            {
                DataTable dsTable = new DataTable( table.TableName);
                dsTable.ExtendedProperties[CDataHotelTable.c_extPropTableId] = table.Id;
                dsTable.Columns.Add(CDataHotelTable.c_nomChampTableEntiteId, typeof(string));
                dsTable.Columns.Add(CDataHotelTable.c_nomChampTableDate, typeof(DateTime));
                foreach (CDataHotelField field in table.Fields)
                {
                    DataColumn col = new DataColumn(field.FieldName, typeof(double));
                    col.AllowDBNull = true;
                    col.ExtendedProperties[CDataHotelField.c_extPropColumnId] = field.Id;
                    dsTable.Columns.Add(col);
                }
                    
                ds.Tables.Add(dsTable);
            }
            return ds;
        }

        //-------------------------------------------------------------------------------------
        public bool Ping()
        {
            return true;
        }

        //-------------------------------------------------------------------------------------
        public IDataRoomEntry GetFirstNotInSerie ( 
            string strTableId,
            string strEntityId,
            string strFieldId,
            DateTime dateRecherche,
            ITestDataHotel filtre )
        {
            CListeEntitesDeMemoryDb<CDataRoom> lstRooms = new CListeEntitesDeMemoryDb<CDataRoom>(m_database);
            IDataRoomEntry minEntry = null;
            foreach (CDataRoom room in lstRooms)
            {
                try
                {
                    IDataRoomServer srv = GetRoomServer(room);
                    if (srv != null)
                    {
                        IDataRoomEntry entry = srv.GetFirstNotInSerieDirect(
                            strTableId,
                            strEntityId,
                            strFieldId,
                            dateRecherche,
                            filtre);
                        if (entry != null)
                        {
                            if (minEntry == null || minEntry.Date > entry.Date)
                                minEntry = entry;
                        }
                    }
                }
                catch { }
            }
            return minEntry;
        }

        //-------------------------------------------------------------------------------------
        public IDataRoomEntry GetFirstNotInSerieDirect(
            string strTableId,
            string strEntityId,
            string strFieldId,
            DateTime dateRecherche,
            ITestDataHotel filtre)
        {
            return CDataRoomManager.Instance.GetFirstNotInSerie(
                strTableId,
                strEntityId,
                strFieldId,
                dateRecherche,
                filtre);
        }

        //-------------------------------------------------------------------------------------
        public double GetDepuisCombienDeTempsEnSDirect ( 
            string strTableId, 
            string strEntityId,
            string strFieldId,
            DateTime dateRecherche,
            ITestDataHotel filtre )
        {
            return CDataRoomManager.Instance.GetDepuisCombienDeTempsEnS(
                strTableId,
                strEntityId,
                strFieldId,
                dateRecherche,
                filtre);
        }
    }
}
