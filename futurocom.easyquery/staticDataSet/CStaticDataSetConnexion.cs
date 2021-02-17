using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;

namespace futurocom.easyquery.staticDataSet
{
    [AutoExec("Autoexec")]
    public class CStaticDataSetConnexion : IEasyQueryConnexion
    {
        private const string c_strConnexionTypeId = "STATIC_DS";
        private const string c_strFileToImport = "FILE";

        private const string c_strTableId = "TABLE_ID";

        private DataSet m_dataset = new DataSet();
        private string m_strConnexionName = "Static Data";

        //-----------------------------------------------------
        public void ClearCache(ITableDefinition table)
        {
            
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CAllocateurEasyQueryConnexions.RegisterTypeConnexion(c_strConnexionTypeId, typeof(CStaticDataSetConnexion));
        }

        //-----------------------------------------------------
        public bool CanFill(ITableDefinition tableDefinition)
        {
            CTableDefinitionStaticDataSet tbl = tableDefinition as CTableDefinitionStaticDataSet;
            if ( tbl != null  )
                return true;
            return false;
        }

        //-----------------------------------------------------
        public DataTable GetData(
            ITableDefinition tableDefinition, 
            params string[] strIdsColonnesSource)
        {
            if (m_dataset != null)
            {
                foreach (DataTable table in m_dataset.Tables)
                {
                    if ((string)table.ExtendedProperties[c_strTableId] == tableDefinition.Id)
                    {
                        DataTable tableFinale = null;
                        tableFinale = table.Clone();
                        foreach (DataRow row in table.Rows)
                        {
                            tableFinale.ImportRow(row);
                        }
                        return tableFinale;
                    }
                }
            }
            return null;
        }

        //-----------------------------------------------------
        public string ConnexionTypeId
        {
            get 
            { 
                return c_strConnexionTypeId;
            }
        }

        //-----------------------------------------------------
        public string ConnexionTypeName
        {
            get { return m_strConnexionName; }
        }

        //-----------------------------------------------------
        public IEnumerable<CEasyQueryConnexionProperty> ConnexionProperties
        {
            get
            {
                List<CEasyQueryConnexionProperty> lst = new List<CEasyQueryConnexionProperty>();
                lst.Add ( new CEasyQueryConnexionProperty(
                    c_strFileToImport, ""));
                return lst.ToArray();
            }
            set
            {
                if ( value != null )
                {
                    foreach ( CEasyQueryConnexionProperty p in value )
                    {
                        if ( p.Property == c_strFileToImport )
                        {
                            ImportFile(p.Value);
                        }
                    }
                }
            }
        }

        //-----------------------------------------------------
        public string GetConnexionProperty(string strProperty)
        {
            return "";
        }

        //-----------------------------------------------------
        public void SetConnexionProperty(string strProperty, string strValeur)
        {
            if (strProperty == c_strFileToImport)
                ImportFile(strValeur);
        }

        //-----------------------------------------------------
        public void FillStructureQuerySource(CEasyQuerySource source)
        {
            if (m_dataset != null)
            {
                foreach (DataTable table in m_dataset.Tables)
                {
                    CTableDefinitionStaticDataSet def = new CTableDefinitionStaticDataSet(source);
                    def.TableName = table.TableName;
                    def.SetId(table.ExtendedProperties[c_strTableId] as string);
                    foreach (DataColumn col in table.Columns)
                    {
                        CColonneTableStaticDataset c = CColonneTableStaticDataset.GetForDataCol(col);
                        c.Table = def;
                        def.AddColumn(c);
                    }
                    source.AddTableUniquementPourObjetConnexion(def);
                }
                source.AddTableUniquementPourObjetConnexion(new CTableDefinitionManualTable());
            }
        }

        //-----------------------------------------------------
        public void ClearStructure()
        {
            
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
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString ( ref m_strConnexionName );
            object obj = m_dataset;
            result = serializer.TraiteSerializable( ref obj );
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                m_dataset = obj as DataSet;
            }
            return result;
        }

        //-----------------------------------------------------
        public CResultAErreur ImportFile(string strFichier)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                if (strFichier.Trim().Length == 0)
                    return result;
                DataSet ds = new DataSet();
                ds.ReadXml(strFichier);
                m_dataset = ds;
                //Attribut un ID à chaque table
                foreach ( DataTable table in m_dataset.Tables )
                {
                    table.ExtendedProperties[c_strTableId] = Guid.NewGuid().ToString();
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }
        
    }
}
