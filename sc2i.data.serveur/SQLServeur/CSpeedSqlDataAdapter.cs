using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace sc2i.data.serveur.SQLServeur
{
    public class CSpeedSqlDataAdapter : IDbDataAdapter
    {

        SqlDataAdapter m_adapter = null;
        string strRequete = "";
        private SqlCommand m_commandSelect = null;
        private SqlCommand m_commandUpdate = null;
        private SqlCommand m_commandInsert = null;
        private SqlCommand m_commandDelete = null;

        #region IDataAdapter Membres

        public CSpeedSqlDataAdapter(SqlCommand selectCommand)
        {
            m_commandSelect = selectCommand;
            m_adapter = new SqlDataAdapter(selectCommand);
        }

        public IDbCommand SelectCommand
        {
            get
            {
                return m_commandSelect;
            }
            set
            {
                m_commandSelect = value as SqlCommand;
                m_adapter.SelectCommand = m_commandSelect;
            }
        }

        public IDbCommand InsertCommand
        {
            get
            {
                return m_commandInsert;
            }
            set
            {
                m_commandInsert = value as SqlCommand;
                m_adapter.InsertCommand = m_commandInsert;
            }
        }

        public IDbCommand UpdateCommand
        {
            get
            {
                return m_commandUpdate;
            }
            set
            {
                m_commandUpdate = value as SqlCommand;
                m_adapter.UpdateCommand = m_commandUpdate;
            }
        }

        public IDbCommand DeleteCommand
        {
            get
            {
                return m_commandDelete;
            }
            set
            {
                m_commandDelete = value as SqlCommand;
                m_adapter.DeleteCommand = m_commandDelete;
            }
        }




        public int Fill(DataSet dataSet)
        {
            bool bIsOpen = m_commandSelect.Connection.State == ConnectionState.Open;
            if (!bIsOpen)
                m_commandSelect.Connection.Open();
            /*if (dataSet.Tables.Count == 0)
            {
                FillSchema(dataSet, SchemaType.Mapped);
            }*/
            SqlDataReader reader = m_commandSelect.ExecuteReader();

            if (dataSet.Tables.Count == 0)
                dataSet.Tables.Add(new DataTable("table"));
            DataTable table = dataSet.Tables[0];
            Dictionary<int, string> mapChamps = null;
            string strChampsInconnus = "";
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (mapChamps == null)
                    {
                        bool bCreateAll = table.Columns.Count == 0;
                        mapChamps = new Dictionary<int, string>();
                        for (int nChamp = 0; nChamp < reader.FieldCount; nChamp++)
                        {
                            string strChamp = reader.GetName(nChamp);
                            if (!bCreateAll)
                            {
                                if (table.Columns.Contains(strChamp))
                                    mapChamps[nChamp] = strChamp;
                                else
                                    strChamp += strChamp;
                            }
                            else
                            {
                                DataColumn col = new DataColumn(reader.GetName(nChamp));
                                col.DataType = reader.GetFieldType(nChamp);
                                table.Columns.Add(col);
                                mapChamps[nChamp] = col.ColumnName;
                            }
                        }
                    }
                    DataRow row = table.NewRow();
                    foreach (KeyValuePair<int, string> champ in mapChamps)
                    {
                        row[champ.Value] = reader.GetValue(champ.Key);
                    }
                    table.Rows.Add(row);
                }
                reader.Close();
            }
            if (!bIsOpen)
                m_commandSelect.Connection.Close();
            table.AcceptChanges();
            return table.Rows.Count;
        }

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            return m_adapter.FillSchema(dataSet, schemaType);
        }

        public IDataParameter[] GetFillParameters()
        {
            return m_adapter.GetFillParameters();
        }

        public MissingMappingAction MissingMappingAction
        {
            get
            {
                return m_adapter.MissingMappingAction;
            }
            set
            {
                m_adapter.MissingMappingAction = value;
            }
        }

        public MissingSchemaAction MissingSchemaAction
        {
            get
            {
                return m_adapter.MissingSchemaAction;
            }
            set
            {
                m_adapter.MissingSchemaAction = value;
            }
        }

        public ITableMappingCollection TableMappings
        {
            get
            {
                return m_adapter.TableMappings;
            }
        }

        public int Update(DataSet dataSet)
        {
            try
            {
                return m_adapter.Update(dataSet);
            }
            catch (DBConcurrencyException e)
            {
                throw new Exception(CObjetDonnee.GetMessageAccesConccurentiel(e.Row));
            }
        }


        #endregion

        
    }
}
