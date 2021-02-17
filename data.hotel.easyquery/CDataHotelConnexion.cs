using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using sc2i.common;
using data.hotel.client;
using data.hotel.client.query;

namespace data.hotel.easyquery
{
    [AutoExec("Autoexec")]
    public class CDataHotelConnexion : IEasyQueryConnexion
    {
        public const string c_parametreIP = "IP";
        public const string c_parametrePort = "PORT";

        public const string c_ConnexionTypeId = "DATAHOTEL";

        private string m_strIp = "127.0.0.1";
        private int m_nPort = 8200;

        private List<ITableDefinition> m_listeTables = null;

        public CDataHotelConnexion()
        {
        }

        //---------------------------------------------------
        public string ConnexionTypeName
        {
            get
            {
                return "Data Hotel";
            }
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CAllocateurEasyQueryConnexions.RegisterTypeConnexion(c_ConnexionTypeId, typeof(CDataHotelConnexion));
        }

        //---------------------------------------------------
        public CDataHotelConnexion ( string strIp, int nPort )
        {
            m_strIp = strIp;
            m_nPort = nPort;
        }

        //----------------------------------------------------------
        public string IP
        {
            get
            {
                return m_strIp;
            }
            set
            {
                m_strIp = value;
            }
        }

        //----------------------------------------------------------
        public int Port
        {
            get
            {
                return m_nPort;
            }
            set
            {
                m_nPort = value;
            }
        }


        #region ITableFiller Membres

        //----------------------------------------------------------
        public void ClearCache(ITableDefinition table)
        {
        }

        //----------------------------------------------------------
        public bool CanFill(ITableDefinition tableDefinition)
        {
            return typeof(CTableDefinitionDataHotel).IsAssignableFrom(tableDefinition.GetType()) ||
                typeof(CTableDefinitionEntitiesDataHotel).IsAssignableFrom(tableDefinition.GetType());
        }

        //----------------------------------------------------------
        public DataTable GetData(ITableDefinition tableDefinition, params string[] strIdsColonnesSource)
        {
            CTableDefinitionDataHotel tableHotel = tableDefinition as CTableDefinitionDataHotel;
            if (tableHotel == null)
                return GetTableEntitiesVide(tableDefinition, strIdsColonnesSource);
            CDataHotelQuery query = new CDataHotelQuery();
            query.DateDebut = DateTime.Now;
            query.DateFin = DateTime.Now;
            query.TableId = tableHotel.Id;
            query.EntitiesId.Add("DUMMYTEST");
            if (strIdsColonnesSource == null || strIdsColonnesSource.Count() == 0)
            {
                List<string> lstCols = new List<string>();
                foreach (IColumnDefinition col in tableDefinition.Columns)
                    if (col is CColonneDefinitionDataHotel)
                        lstCols.Add(((CColonneDefinitionDataHotel)col).Id);
                query.ChampsId = lstCols;
            }
            else
                query.ChampsId = strIdsColonnesSource;
            return GetData(tableHotel, query);
        }

        //----------------------------------------------------------
        public DataTable GetTableEntitiesVide(ITableDefinition tableDefinition, params string[] strIdColonnesSource)
        {
            CTableDefinitionEntitiesDataHotel tableEntites = tableDefinition as CTableDefinitionEntitiesDataHotel;
            if (tableEntites == null)
                return null;
            DataTable table = new DataTable(tableDefinition.TableName);
            DataColumn col = new DataColumn ( CTableDefinitionEntitiesDataHotel.c_strNomEntityId, typeof(string));
            col.ExtendedProperties[CODEQBase.c_extPropColonneId] = CTableDefinitionEntitiesDataHotel.c_strIdColEntityId;
            table.Columns.Add(col);
            return table;
        }

        //----------------------------------------------------------
        public DataTable GetData ( CTableDefinitionDataHotel tableHotel, CDataHotelQuery query )
        {
            DataTable tableResult = null;
            try
            {
                CDataHotelClient client = new CDataHotelClient(m_strIp, m_nPort);
                //Convertit les ids de colonne en id de colonne DataHotel;
                List<string> lstIds = new List<string>();
                Dictionary<string, IColumnDefinition> dicIdToColDef = new Dictionary<string, IColumnDefinition>();

                //remarque sur le code :
                //au début, les CColonneDefinitionDataHotel avaient un id qui leur était propre
                //au lieu de prendre l'id de la colonne de DataHotel, du coup,
                //il fallait faire une conversion des IdCol -> IdHotel.
                //Cette notion a été corrigée, mais pour compatiblité, on continue à
                //convertir. 
                foreach ( string strIdCol in query.ChampsId)
                {
                    IColumnDefinition col = tableHotel.GetColumn(strIdCol);
                    CColonneDefinitionDataHotel colHot = col as CColonneDefinitionDataHotel;
                    if (colHot != null)
                    {
                        lstIds.Add(colHot.HotelColumnId);
                        dicIdToColDef[colHot.HotelColumnId] = col;
                        if ( query.Filtre != null )
                            query.Filtre.ReplaceColumnId(strIdCol, colHot.HotelColumnId);
                    }
                    if (col is CColonneDefinitionHotelDate)
                        dicIdToColDef[CDataHotelTable.c_nomChampTableDate] = col;
                    if (col is CColonneDefinitionHotelEntiteId)
                        dicIdToColDef[CDataHotelTable.c_nomChampTableEntiteId] = col;
                }
                query.ChampsId = lstIds;
                CResultAErreurType<CDataTableFastSerialize> res = client.GetRoomServer().GetData(query);
                if (res && res.Data != null)
                {
                    tableResult = res.DataType;
                    foreach ( DataColumn col in tableResult.Columns )
                    {
                        IColumnDefinition colDef = null;
                        if (dicIdToColDef.TryGetValue(col.ColumnName, out colDef))
                        {
                            col.ExtendedProperties[CODEQBase.c_extPropColonneId] = col.ColumnName;
                            col.ColumnName = colDef.ColumnName;
                        }
                    }
                }

                return tableResult;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        //----------------------------------------------------------
        public DataTable GetData ( CTableDefinitionEntitiesDataHotel table, DateTime dataStart, DateTime dataEnd )
        {
            CDataHotelClient client = new CDataHotelClient(m_strIp, m_nPort);
            try
            {
                IEnumerable<string> lstIds = client.GetRoomServer().GetEntities(table.Id, dataStart, dataEnd);
                DataTable tableRetour = GetTableEntitiesVide(table);
                tableRetour.BeginLoadData();
                foreach (string strId in lstIds)
                {
                    DataRow row = tableRetour.NewRow();
                    row[CTableDefinitionEntitiesDataHotel.c_strNomEntityId] = strId;
                    tableRetour.Rows.Add(row);
                }
                return tableRetour;
            }
            catch
            {
            }
            return null;
        }
        

        #endregion

        //----------------------------------------------------------
        private void AssureListeTables()
        {
            if (m_listeTables == null)
            {
                CDataHotelClient client = new CDataHotelClient(m_strIp, m_nPort);
                try
                {
                    DataSet ds = client.GetRoomServer().GetDataSetModele();
                    if (ds != null)
                    {
                        m_listeTables = new List<ITableDefinition>();
                        foreach (DataTable table in ds.Tables)
                        {
                            string strTableId = table.ExtendedProperties[CDataHotelTable.c_extPropTableId] as string;
                            if (strTableId != null)
                            {
                                CTableDefinitionDataHotel tableDef = new CTableDefinitionDataHotel(strTableId, table.TableName);
                                tableDef.AddColumn(new CColonneDefinitionHotelEntiteId("Data entity id"));
                                tableDef.AddColumn(new CColonneDefinitionHotelDate("Data date"));

                                foreach (DataColumn col in table.Columns)
                                {
                                    string strColId = col.ExtendedProperties[CDataHotelField.c_extPropColumnId] as string;
                                    if (strColId != null)
                                    {
                                        CColonneDefinitionDataHotel colHotel = new CColonneDefinitionDataHotel(strColId, col.ColumnName);
                                        tableDef.AddColumn(colHotel);
                                    }
                                }
                                m_listeTables.Add(tableDef);

                                CTableDefinitionEntitiesDataHotel tableE = new CTableDefinitionEntitiesDataHotel(strTableId, table.TableName);
                                m_listeTables.Add(tableE);
                            }
                        }
                    }
                }
                catch (Exception e )
                {
                }
            }
        }

        

        //-----------------------------------
        private List<ITableDefinition> ListeTables
        {
            get
            {
                AssureListeTables();
                return m_listeTables;
            }
        }

        //-----------------------------------
        public void FillStructureQuerySource(
            CEasyQuerySource source)
        {
            List<ITableDefinition> lstTables = ListeTables;
            if (lstTables != null &&   lstTables.Count > 0)
            {
                foreach (ITableDefinition table in lstTables)
                {
                    table.FolderId = source.RootFolder.Id;
                    table.SourceId = source.SourceId;
                    source.AddTableUniquementPourObjetConnexion(table);
                }
            }
        }

        //-----------------------------------------------------
        public string ConnexionTypeId
        {
            get
            {
                return c_ConnexionTypeId;
            }
        }

        //-----------------------------------------------------
        public IEnumerable<CEasyQueryConnexionProperty> ConnexionProperties
        {
            get
            {
                List<CEasyQueryConnexionProperty> lst = new List<CEasyQueryConnexionProperty>();
                lst.Add ( new CEasyQueryConnexionProperty(c_parametreIP, GetConnexionProperty(c_parametreIP)));
                lst.Add( new CEasyQueryConnexionProperty(c_parametrePort, GetConnexionProperty(c_parametrePort)));
                return lst.AsReadOnly();
            }
            set{
                if ( value != null )
                {
                foreach ( CEasyQueryConnexionProperty prop in value )
                    SetConnexionProperty ( prop.Property, prop.Value );
                }
            }
        }

        //-----------------------------------------------------
        public void SetConnexionProperty(string strParametre, string strValeur)
        {
            string strPar = strParametre.ToUpper();
            if (strPar == c_parametreIP)
                m_strIp = strValeur;
            else if (strPar == c_parametrePort)
            {
                int nVal;
                if (Int32.TryParse(strValeur, out nVal))
                    m_nPort = nVal;
            }
        }

        //-----------------------------------------------------
        public string GetConnexionProperty(string strParametre)
        {
            string strUp = strParametre.ToUpper();
            if (strUp == c_parametreIP)
                return m_strIp;
            if (strUp == c_parametrePort)
                return m_nPort.ToString();
            return "";
        }

        //-----------------------------------------------------
        public void ClearStructure()
        {
            m_listeTables = null;
        }


        //-----------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strIp);
            serializer.TraiteInt(ref m_nPort);
            return result;
        }

       
                    


                    



    }
}
