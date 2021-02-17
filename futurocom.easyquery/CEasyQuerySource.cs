using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.drawing;
using System.Drawing;
using sc2i.common;

namespace futurocom.easyquery
{


    public interface IElementDeQuerySource
    {
        /// <summary>
        /// Identifiant de l'image associée à l'élément qui est enregistrée dans les
        /// Images de CEasyQuerySource
        /// </summary>
        string ImageKey { get; set; }
    }

    /// <summary>
    /// Contient les structures des tables disponibles pour une CEasyQuery
    /// </summary>
    [Serializable]
    [DynamicClass("Runnable Easy Query source")]
    public class CEasyQuerySource : I2iSerializable
    {
        private static Dictionary<string, Image> m_dicImagesForArbre = new Dictionary<string, Image>();

        private List<ITableDefinition> m_tables = null;
        private Dictionary<string, ITableDefinition> m_dicIdToTable = new Dictionary<string, ITableDefinition>();

        private IEasyQueryConnexion m_connexion = null;

        private CEasyQuerySourceFolder m_folderRacine = null;

        private string m_strSourceName = "";
        private string m_strSourceId = "";

        //-----------------------------------------------
        public CEasyQuerySource()
        {
            m_strSourceId = Guid.NewGuid().ToString();
            m_folderRacine = new CEasyQuerySourceFolder(this);
        }

        //-----------------------------------------------
        public static void RegisterImageForFolder ( string strKey, Image img )
        {
            m_dicImagesForArbre[strKey] = img;
        }

        //-----------------------------------------------
        public static Image GetImage(string strKey)
        {
            Image img = null;
            m_dicImagesForArbre.TryGetValue(strKey, out img);
            return img;
        }

        //-----------------------------------------------
        public static List<KeyValuePair<string, Image>> ToutesImages
        {
            get
            {
                List<KeyValuePair<string, Image>> lst = new List<KeyValuePair<string, Image>>();
                foreach (KeyValuePair<string, Image> kv in m_dicImagesForArbre)
                {
                    KeyValuePair<string, Image> copie = new KeyValuePair<string, Image>(kv.Key, kv.Value);
                    lst.Add(copie);
                }
                return lst;
            }
        }

        //-----------------------------------------------
        [DynamicField("Source Id")]
        public string SourceId
        {
            get
            {
                return m_strSourceId;
            }
            set
            {
                m_strSourceId = value;
            }
        }

        //-----------------------------------------------
        [DynamicField("Source name")]
        public string SourceName
        {
            get
            {
                return m_strSourceName;
            }
            set
            {
                m_strSourceName = value;
            }
        }

        
        //-----------------------------------------------
        public IEasyQueryConnexion Connexion
        {
            get
            {
                return m_connexion;
            }
            set
            {
                m_connexion = value;
            }
        }

        //-----------------------------------------------
        public DataTable GetTable(ITableDefinition table, params string[] strIdsColonnesSources)
        {
            if (m_connexion != null)
                return m_connexion.GetData(table, strIdsColonnesSources);
            return null;
        }

        //-----------------------------------------------
        public IEnumerable<ITableDefinition> Tables
        {
            get
            {
                AssureTables();
                return m_tables.AsReadOnly();
            }
        }

        //-----------------------------------------------
        private void AssureTables()
        {
            if (m_tables == null)
            {
                m_tables = new List<ITableDefinition>();
                if (m_connexion != null)
                    m_connexion.FillStructureQuerySource(this);
            }
        }

        //-----------------------------------------------
        public void AddTableUniquementPourObjetConnexion(ITableDefinition table)
        {
            m_tables.Add(table);
        }

        //-----------------------------------------------
        public CEasyQuerySourceFolder RootFolder
        {
            get
            {
                AssureTables();
                return m_folderRacine;
            }
        }

        //-----------------------------------------------
        public ITableDefinition GetTable(string strIdOrName)
        {
            ITableDefinition def = null;
            if (m_dicIdToTable.TryGetValue(strIdOrName, out def))
                return def;
            return Tables.FirstOrDefault(table => table.TableName.ToUpper() == strIdOrName.ToUpper());
        }

        //-----------------------------------------------
        public IEnumerable<ITableDefinition> GetTablesForFolder(string strIdFolder)
        {
            AssureTables();
            List<ITableDefinition> lst = new List<ITableDefinition>();
            foreach (ITableDefinition table in Tables)
                if (table.FolderId == strIdFolder || table.FolderId == "" || table.FolderId == null 
                    && strIdFolder == RootFolder.Id)
                    lst.Add(table);
            return lst.AsReadOnly();
        }

        

        //-----------------------------------------------
        public void ClearCache ( ITableDefinition table)
        {
            if ( m_connexion != null )
                m_connexion.ClearCache(table);
        }

        //-----------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strSourceId);
            serializer.TraiteString(ref m_strSourceName);
            result = serializer.TraiteObject<IEasyQueryConnexion>(ref m_connexion);

            if ( serializer.IsForClone)
                m_strSourceId = Guid.NewGuid().ToString();
            return result;
        }



        //-----------------------------------------------
        public void ClearStructure()
        {
            m_tables = null;
            if (m_connexion != null)
                m_connexion.ClearStructure();
        }

        //-----------------------------------------------
        [DynamicMethod("Returns a connexion parameter value", "Parameter to return")]
        public string GetConnexionParameterValue(string strParameter)
        {
            if (m_connexion != null)
                return m_connexion.GetConnexionProperty(strParameter);
            return "";
        }

        //-----------------------------------------------
        [DynamicMethod("Change a connexion parameter value", "Paramter to set", "New value")]
        public void SetConnexionParameterValue(string strParameter, string strValue)
        {
            if (m_connexion != null)
                m_connexion.SetConnexionProperty(strParameter, strValue);
        }

    }
}
