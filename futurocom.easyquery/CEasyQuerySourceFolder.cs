using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using System.Data;

namespace futurocom.easyquery
{
    /// <summary>
    /// Permet d'organiser les tables en arbre
    /// </summary>
    [AutoExec("Autoexec")]
    [Serializable]
    public class CEasyQuerySourceFolder : IElementDeQuerySource
    {
        private const string c_strImageKey = "STD_FOLDER";
        private CEasyQuerySource m_source = null;
        private const string c_nomColName = "Name";

        private string m_strImage = "";
        private string m_strName = "";
        private string m_strId = "";
        private string m_strImageKey = c_strImageKey;

        private List<CEasyQuerySourceFolder> m_childFolders = new List<CEasyQuerySourceFolder>();

        /// <summary>
        /// Permet de stocker des données dans le folder
        /// </summary>
        private Dictionary<string, IValeurExtendedProperiteFolder> m_dicExtendedProperties = new Dictionary<string, IValeurExtendedProperiteFolder>();

        //--------------------------------------------------
        public CEasyQuerySourceFolder(CEasyQuerySource source)
        {
            m_strId = Guid.NewGuid().ToString();
            m_source = source;
        }

        //--------------------------------------------------
        public CEasyQuerySourceFolder(string strName, CEasyQuerySource source)
        {
            m_strId = Guid.NewGuid().ToString();
            m_strName = strName;
            m_source = source;
        }

        //--------------------------------------------------
        public static void Autoexec()
        {
            CEasyQuerySource.RegisterImageForFolder(c_strImageKey, Resource1.Folder16);
        }

        //--------------------------------------------------
        public virtual string ImageKey
        {
            get
            {
                return m_strImageKey;
            }
            set
            {
                m_strImageKey = value;
            }
        }

        //--------------------------------------------------
        public void SetExtendedProperty(string strKey, IValeurExtendedProperiteFolder valeur)
        {
            m_dicExtendedProperties[strKey] = valeur;
        }

        //--------------------------------------------------
        public void SetExtendedProperty(string strKey, string strValeur)
        {
            m_dicExtendedProperties[strKey] = new CValeurExtendedProperieteFolderTexte(strValeur);
        }

        //--------------------------------------------------
        public virtual string GetExtendedPropery(string strKey)
        {
            IValeurExtendedProperiteFolder valeur = null;
            if (m_dicExtendedProperties.TryGetValue(strKey, out valeur))
                return valeur.GetValue(m_source);
            return null;
        }

        //--------------------------------------------------
        public virtual IEnumerable<KeyValuePair<string, IValeurExtendedProperiteFolder>> GetExtendedProperties()
        {
            return m_dicExtendedProperties;
        }


        //--------------------------------------------------
        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        //--------------------------------------------------
        public IEnumerable<CEasyQuerySourceFolder> Childs
        {
            get
            {
                return m_childFolders.AsReadOnly();
            }
        }

        //--------------------------------------------------
        public void AddChild(CEasyQuerySourceFolder folder)
        {
            m_childFolders.Add(folder);
        }

        //--------------------------------------------------
        public virtual string Id
        {
            get
            {
                return m_strId;
            }
        }

        //--------------------------------------------------
        public CEasyQuerySourceFolder GetSubFolder ( string strName )
        {
            string strUpper = strName.ToUpper();
            return m_childFolders.FirstOrDefault ( f=>f.Name.ToUpper() == strUpper );
        }

        //--------------------------------------------------
        public CEasyQuerySourceFolder GetSubFolderWithCreate(string strName)
        {
            CEasyQuerySourceFolder child = GetSubFolder(strName);
            if (child == null)
            {
                child = new CEasyQuerySourceFolder(m_source);
                child.Name = strName;
                m_childFolders.Add(child);
            }
            return child;
        }

        //--------------------------------------------------
        public CEasyQuerySourceFolder FindFolder(string strId)
        {
            if (strId == Id)
                return this;
            foreach (CEasyQuerySourceFolder folder in Childs)
            {
                CEasyQuerySourceFolder trouve = folder.FindFolder(strId);
                if (trouve != null)
                    return trouve;
            }
            return null;
        }

        //--------------------------------------------------
        public CEasyQuerySource Source
        {
            get
            {
                return m_source;
            }
        }

        //--------------------------------------------------
        /// <summary>
        /// Retourne une table qui contient les sous folders de ce folder
        /// </summary>
        /// <param name="bChildsRecursifs"></param>
        /// <returns></returns>
        public virtual ITableDefinition GetDefinitionTable( CEasyQuerySource source )
        {
            CTableDefinitionDonneesFixes table = new CTableDefinitionDonneesFixes();
            table.TableName = Name;
            table.AddColumn(new CColumnDefinitionSimple(c_nomColName, typeof(string)));
            foreach (CEasyQuerySourceFolder child in Childs)
            {
                IEnumerable<KeyValuePair<string, IValeurExtendedProperiteFolder>> exts = child.GetExtendedProperties();
                foreach (KeyValuePair<string, IValeurExtendedProperiteFolder> kv in exts)
                {
                    if (table.GetColumn(kv.Key) == null)
                        table.AddColumn(new CColumnDefinitionSimple(kv.Key, typeof(string)));
                }
            }
            DataTable tableData = table.TableContenu;
            foreach (CEasyQuerySourceFolder child in Childs)
            {
                DataRow row = tableData.NewRow();
                row[c_nomColName] = child.Name;
                foreach (KeyValuePair<string, IValeurExtendedProperiteFolder> kv in child.GetExtendedProperties())
                {
                    string  strValeur = kv.Value.GetValue ( m_source );
                    if (strValeur != null)
                        row[kv.Key] = strValeur;
                }
                tableData.Rows.Add(row);
            }
            return table;
        }
        
    }
}
