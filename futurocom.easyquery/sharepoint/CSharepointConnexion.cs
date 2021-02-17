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

namespace futurocom.easyquery.sharepoint30
{
    [AutoExec("Autoexec")]
    public class CSharepointConnexion : IEasyQueryConnexion
    {
        public const string c_parametreURL = "URL";
        public const string c_parametreUser = "USER";
        public const string c_parametrePassword = "PASSWORD";

        public const string c_ConnexionTypeId = "SHAREPOINT";

        private string m_strUser = "";
        private string m_strPassword = "";
        private string m_strURL = "";

        private List<CTableDefinitionSharepoint> m_listeTables = null;

        public CSharepointConnexion()
        {
        }

        //---------------------------------------------------
        public string ConnexionTypeName
        {
            get
            {
                return "Sharepoint 3.0";
            }
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CAllocateurEasyQueryConnexions.RegisterTypeConnexion(c_ConnexionTypeId, typeof(CSharepointConnexion));
        }

        //---------------------------------------------------
        public CSharepointConnexion ( string strURL,
            string strUser,
            string strPassword )
        {
            m_strURL = strURL;
            m_strPassword = strPassword;
            m_strUser = strUser;
        }

        public string User
        {
            get{
                return m_strUser;
            }set{
                m_strUser = value;
            }
        }

        //----------------------------------------------------------
        public string Password
        {
            get{
                return m_strPassword;
            }
            set{m_strPassword = value;
            }
        }

        //----------------------------------------------------------
        public string URL
        {
            get{
                return m_strURL;
            }
            set
            {
                m_strURL = value;
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
            return typeof(CTableDefinitionSharepoint).IsAssignableFrom(tableDefinition.GetType());
        }

        //----------------------------------------------------------
        public DataTable GetData(ITableDefinition tableDefinition, params string[] strIdsColonnesSource)
        {
            return GetDataWithCAML(tableDefinition, "", strIdsColonnesSource);
        }

        //----------------------------------------------------------
        public DataTable GetDataWithCAML(ITableDefinition tableDefinition, string strCAMLQuery, params string[] strIdsColonnesSource)
        {
            DataTable tableResult = null;
            CTableDefinitionSharepoint table = tableDefinition as CTableDefinitionSharepoint;
            if (table == null)
                return tableResult;

            futurocom.easyquery.Sharepoint30.Lists service = new futurocom.easyquery.Sharepoint30.Lists();
            if (m_strURL.Length > 0)
                service.Url = m_strURL;
            service.Credentials = new NetworkCredential(m_strUser, m_strPassword);

            //Création de la liste des colonnes attendues
            XmlDocument doc = new XmlDocument();
            XmlElement nodeFields = null;
            if (strIdsColonnesSource.Length > 0)
            {
                nodeFields = doc.CreateElement("ViewFields");
                foreach (string strIdColonne in strIdsColonnesSource)
                {
                    XmlElement nodeField = doc.CreateElement("FieldRef");
                    nodeFields.AppendChild(nodeField);
                    XmlAttribute att = doc.CreateAttribute("Name");
                    att.Value = strIdColonne;
                    nodeField.Attributes.Append(att);
                }
            }
            XmlElement nodeQuery = null;
            if (strCAMLQuery.Length > 0)
            {
                nodeQuery = doc.CreateElement("Query");
                XmlElement nodeWhere = doc.CreateElement("Where");
                nodeQuery.AppendChild(nodeWhere);
                nodeWhere.InnerXml = strCAMLQuery;
            }
            XmlElement nodeQueryOptions = doc.CreateElement("QueryOptions");
            nodeQueryOptions.InnerXml = "";
            XmlNode node = service.GetListItems(table.Id, null, nodeQuery, nodeFields, "", nodeQueryOptions, null);

            XmlNamespaceManager manager = new XmlNamespaceManager(node.OwnerDocument.NameTable);
            foreach (XmlAttribute att in node.Attributes)
            {
                if (att.Name.StartsWith("xmlns:"))
                {
                    string strAbrv = att.Name.Substring(att.Name.IndexOf(":") + 1);
                    manager.AddNamespace(strAbrv, att.Value);
                }
            }
            XmlNodeList liste = node.SelectNodes("/rs:data/z:row", manager);
            tableResult = new DataTable(tableDefinition.TableName);
            //Création de la structure de la table
            Dictionary<string, DataColumn> dicCols = new Dictionary<string, DataColumn>();
            foreach ( IColumnDefinition col in table.Columns )
            {
                CColonneDefinitionSharepoint colShp = col as CColonneDefinitionSharepoint;
                if ( colShp != null && (strIdsColonnesSource.Length ==0 || strIdsColonnesSource.Contains ( colShp.SharepointId) ))
                {
                    DataColumn newCol = new DataColumn ( col.ColumnName, col.DataType );
                    tableResult.Columns.Add ( newCol );
                    
                    dicCols[colShp.SharepointId] = newCol;
                }
            }

            foreach (XmlNode xmlRow in liste)
            {
                DataRow row = tableResult.NewRow();
                foreach (XmlAttribute att in xmlRow.Attributes)
                {
                    if (att.Name.StartsWith("ows_"))
                    {
                        string strNomCol = att.Name.Substring(4);
                        DataColumn colDeTable = null;
                        if ( dicCols.TryGetValue ( strNomCol, out colDeTable ) )
                        {
                            row[colDeTable] = att.Value;
                        }
                    }
                }
                try{
                    tableResult.Rows.Add ( row );
                }
                catch{}
            }
            return tableResult;

            
        }

        #endregion

        //----------------------------------------------------------
        private void AssureListeTables()
        {
            if (m_listeTables == null)
            {
                futurocom.easyquery.Sharepoint30.Lists service = new futurocom.easyquery.Sharepoint30.Lists();
                if (m_strURL.Length > 0)
                    service.Url = m_strURL;
                m_listeTables = new List<CTableDefinitionSharepoint>();
                service.Credentials = new NetworkCredential(m_strUser, m_strPassword);
                try
                {
                    XmlNode node = service.GetListCollection();
                    XmlNamespaceManager manager = new XmlNamespaceManager(node.OwnerDocument.NameTable);
                    manager.AddNamespace("sp", node.NamespaceURI);
                    XmlNodeList liste = node.SelectNodes("sp:List", manager);
                    foreach (XmlNode child in liste)
                    {
                        if (child.NodeType == XmlNodeType.Element &&
                            child.Name.ToUpper() == "LIST")
                        {
                            string strNomTable = "";
                            string strGUID = "";
                            string strDescription = "";
                            foreach (XmlAttribute attrib in child.Attributes)
                            {
                                if (attrib.Name.ToUpper() == "TITLE")
                                    strNomTable = XmlConvert.DecodeName(attrib.Value);
                                if (attrib.Name.ToUpper() == "DESCRIPTION")
                                    strDescription = XmlConvert.DecodeName(attrib.Value);
                                if (attrib.Name.ToUpper() == "ID")
                                    strGUID = attrib.Value;
                                if (strNomTable.Length > 0 && strGUID.Length > 0 && strDescription.Length > 0)
                                    break;
                            }
                            CTableDefinitionSharepoint table = new CTableDefinitionSharepoint(
                                strGUID, strNomTable,
                                strDescription);
                            m_listeTables.Add(table);
                            FillStructureTable(service, table);
                        }
                    }
                    m_listeTables.Sort((x, y) => x.TableName.CompareTo(y.TableName));
                }
                catch (Exception e)
                {
                }
            }
        }

        //----------------------------------------------------------
        private void FillStructureTable(futurocom.easyquery.Sharepoint30.Lists service,
            CTableDefinitionSharepoint table)
        {
            if (table == null)
                return;
            try
            {
                XmlNode node = service.GetList(table.TableName) as XmlNode;
                XmlNamespaceManager manager = new XmlNamespaceManager(node.OwnerDocument.NameTable);
                manager.AddNamespace("sp", node.NamespaceURI);
                XmlNodeList liste = node.SelectNodes("sp:Fields/sp:Field", manager);
                List<CColonneDefinitionSharepoint> lstCols = new List<CColonneDefinitionSharepoint>();

                foreach (XmlNode childNode in liste)
                {
                    string strNom = "";
                    string strId = "";
                    string strType = "";
                    string strDescription = "";
                    foreach (XmlAttribute attr in childNode.Attributes)
                    {
                        if (attr.Name.ToUpper() == "NAME")
                            strId = attr.Value;
                        if (attr.Name.ToUpper() == "NAME")
                            strNom = XmlConvert.DecodeName(attr.Value);
                        if (attr.Name.ToUpper() == "TYPE")
                            strType = attr.Value;
                        if (attr.Name.ToUpper() == "DESCRIPTION")
                            strDescription = XmlConvert.DecodeName(attr.Value);
                        if (strId.Length > 0 && strNom.Length > 0 && strType.Length > 0 && strDescription.Length > 0)
                            break;
                    }
                    CColonneDefinitionSharepoint col = new CColonneDefinitionSharepoint();
                    col.SharepointId = strId;
                    col.ColumnName = strNom;
                    col.DataType = typeof(string);
                    lstCols.Add(col);
                }
                lstCols.Sort((x, y) => x.ColumnName.CompareTo(y.ColumnName));
                foreach (CColonneDefinitionSharepoint c in lstCols)
                    table.AddColumn(c);
            }
            catch
            {
            }
        }

        //-----------------------------------
        private List<CTableDefinitionSharepoint> ListeTables
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
            List<CTableDefinitionSharepoint> lstTables = ListeTables;
            if (lstTables.Count > 0)
            {
                foreach (CTableDefinitionSharepoint table in lstTables)
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
                lst.Add ( new CEasyQueryConnexionProperty(c_parametreURL, GetConnexionProperty(c_parametreURL)));
                lst.Add( new CEasyQueryConnexionProperty(c_parametreUser, GetConnexionProperty(c_parametreUser)));
                lst.Add ( new CEasyQueryConnexionProperty(c_parametrePassword, GetConnexionProperty(c_parametrePassword)));
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
            if (strPar == c_parametreURL)
                m_strURL = strValeur;
            else if (strPar == c_parametreUser)
                m_strUser = strValeur;
            else if (strPar == c_parametrePassword)
                m_strPassword = strValeur;
        }

        //-----------------------------------------------------
        public string GetConnexionProperty(string strParametre)
        {
            string strUp = strParametre.ToUpper();
            if (strUp == c_parametreURL)
                return m_strURL;
            if (strUp == c_parametreUser)
                return m_strUser;
            if (strUp == c_parametrePassword)
                return m_strPassword;
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
            serializer.TraiteString(ref m_strURL);
            serializer.TraiteString(ref m_strUser);
            serializer.TraiteString(ref m_strPassword);
            return result;
        }

       
                    


                    



    }
}
