using System;
using System.Data;
using System.Data.Common;
using System.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using System.Text;


namespace sc2i.data.serveur
{
	/// <summary>
	/// Obtient le schéma de la base
	/// </summary>
    public class C2iMySqlDataAdapter : 
        IDbDataAdapter, 
        IDataAdapterARemplissagePartiel,
        IDataAdapterGerantLesModificationsParTrigger
	{
        //Indique si la table peut être modifiée par trigger lors d'insert ou update
        private bool m_bIsModifiedByTrigger = false;

        
		////////////////////////////////////////////////////
		public static C2iMySqlDataAdapter GetMySqlDataAdapter(IDbDataAdapter adapter)
		{
			C2iMySqlDataAdapter MySqlAdapter = null;
			if (adapter is C2iDataAdapterForClasseAutoReferencee)
				MySqlAdapter = (C2iMySqlDataAdapter)((C2iDataAdapterForClasseAutoReferencee)adapter).DataAdapterUtilise;
			else if (adapter is C2iMySqlDataAdapter)
				MySqlAdapter = (C2iMySqlDataAdapter)adapter;

			return MySqlAdapter;
		}

        public bool TableIsModifiedByTrigger
        {
            get
            {
                return m_bIsModifiedByTrigger;
            }
            set
            {
                m_bIsModifiedByTrigger = value;
            }
        }

		#region :: Propriétés ::
		MySqlDataAdapter m_adapter;

		string m_strRqtSelectionNewRowIDAvecTrigger="";
		string m_strRqtSelectionNewRowIDSansTrigger = "";
		private string m_champIDAuto;

		private bool m_synchro;
		private MySqlConnection m_connexion;
		private MySqlTransaction m_transaction;
		private CMySqlDatabaseConnexion m_encapsuleurConnexion;
		#endregion
		#region ++ Constructeur ++
		public C2iMySqlDataAdapter(MySqlCommand commande, CMySqlDatabaseConnexion MySqlDbConnexion)
		{
			m_adapter = new MySqlDataAdapter(commande);
			m_encapsuleurConnexion = MySqlDbConnexion;
			m_connexion = commande.Connection;
			m_transaction = commande.Transaction;

			if (MySqlDbConnexion is CMySqlDatabaseConnexionSynchronisable)
				m_synchro = true;
			else
				m_synchro = false;


			m_adapter.RowUpdated += new MySqlRowUpdatedEventHandler(m_adapter_RowUpdated);
		}
		#endregion

		#region Avant Mise A Jour
		/// /////////////////////////////////////////////////////////////////
		private DataTable TyperLesColonnes(DataTable dt)
		{
			DataTable dtcol = new DataTable();
			string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(dt.TableName);
			if (strNomTableInDb == null)
				strNomTableInDb = dt.TableName;
			string strRequeteInfoCols = "SELECT CNAME, COLTYPE, WIDTH, SCALE, \"PRECISION\" FROM SYS.COL WHERE ";
			strRequeteInfoCols += "TNAME = '" + strNomTableInDb + "'";

			dtcol = ExecuterRequeteEnParallele(strRequeteInfoCols);

			foreach (DataColumn col in dt.Columns)
			{
				//On récupère le type et la longueur de la colonne
				DataRow[] infocol = dtcol.Select("CNAME = '" + col.ColumnName + "'");
				if (infocol.Length > 0)
				{
					string strTypeCol = infocol[0]["COLTYPE"].ToString();
					string strLngCol = infocol[0]["WIDTH"].ToString();
					string strPrecisionCol = infocol[0]["PRECISION"].ToString();
					string strEchelleCol = infocol[0]["SCALE"].ToString();
					col.DataType = (new CMySqlTypeMapper()).GetTypeCSharpFromDBType(strTypeCol, strLngCol, strPrecisionCol, strEchelleCol);
				}
			}

			return dt;
		}

		/// /////////////////////////////////////////////////////////////////
		public void PreparerInsertionLigneAvecAutoID(string nomTableInDb, string champIDAuto)
		{
			bool bAvecTrigger = false;
			string strSequence = m_encapsuleurConnexion.GetNomSequenceColAuto(nomTableInDb, champIDAuto, ref bAvecTrigger );
			if (bAvecTrigger)
			{
				m_strRqtSelectionNewRowIDAvecTrigger = "SELECT " + strSequence + ".CURRVAL from DUAL";
				m_strRqtSelectionNewRowIDSansTrigger = "";
			}
			else
			{
				m_strRqtSelectionNewRowIDAvecTrigger = "";
				m_strRqtSelectionNewRowIDSansTrigger = "SELECT " + strSequence + ".NEXTVAL from DUAL"; ;
			}

			m_champIDAuto = champIDAuto;
		}
		#endregion
		#region Apres Mise à Jour
		/// /////////////////////////////////////////////////////////////////
		void m_adapter_RowUpdated(object sender, MySqlRowUpdatedEventArgs e)
		{
			switch (e.StatementType)
			{
				case StatementType.Delete:							break;
				case StatementType.Insert:	MAJApresInsert(e.Row);	break;
				case StatementType.Update:	MAJApresUpdate(e.Row);	break;
				case StatementType.Select:
				case StatementType.Batch:
				default:					MAJApresSelect(e.Row);	break;
			}
			if (RowUpdated != null)
				RowUpdated(this, e);
		}

		/// /////////////////////////////////////////////////////////////////
		public event MySqlRowUpdatedEventHandler RowUpdated;

        /// <summary>
        /// Nom de champ Id entier de la table, utilisée pour récuperer les données après modification
        /// </summary>
        private string m_strChampIdCache = null;
        private string GetRequetePourRecupererLesDonnéesApresModification( DataRow row )
        {
            if ( m_strChampIdCache == null )
            {
                DataColumn[] colKeys = row.Table.PrimaryKey;
                if ( colKeys.Length != 1 || colKeys[0].DataType != typeof(int) )
                    throw new Exception ("MySql adapter error, Can not use 'Modified by trigger' on table with no Numeric ID");
                m_strChampIdCache = colKeys[0].ColumnName;
            }
            string strNomTable = CContexteDonnee.GetNomTableInDbForNomTable(row.Table.TableName);
            return "select * from "+strNomTable+" where "+m_strChampIdCache+"="+row[m_strChampIdCache].ToString();
        }

        private void RecopieDonneesDeBase ( DataRow row )
        {
            string strRequete = GetRequetePourRecupererLesDonnéesApresModification ( row );
            IDbCommand commande = UpdateCommand.Connection.CreateCommand();
            commande.Connection = UpdateCommand.Connection;
            commande.Transaction = UpdateCommand.Transaction;
            commande.CommandText = strRequete;
            bool bOldEnforce = row.Table.DataSet.EnforceConstraints;
            row.Table.DataSet.EnforceConstraints = false;
            IDataReader reader = commande.ExecuteReader ( CommandBehavior.SingleRow );
            if ( reader != null && reader.Read () )
                for (int nField = 0; nField < reader.FieldCount; nField++)
                    if (row.Table.Columns[reader.GetName(nField)] != null)
                    {
                        object value = reader.GetValue(nField);
                        string strValue = value as string;
                        if (strValue != null && strValue.Trim().Length == 0)
                            value = strValue.Substring(1, strValue.Length - 1);
                        row[reader.GetName(nField)] = value;
                    }
            try
            {
                row.Table.DataSet.EnforceConstraints = bOldEnforce;
            }
            catch
            {
                //Erreur lors de l'activation des contraintes,
                //c'est probablement que les données modifiées par la base ont besoin d'un élément
                //Parent affecté par le trigger, mais qu'on ne connait pas encore
                CContexteDonnee ctxDonnee = row.Table.DataSet as CContexteDonnee;
                if (ctxDonnee != null)
                    ctxDonnee.AssureParents(row);
                row.Table.DataSet.EnforceConstraints = bOldEnforce;
            }
        }

		
        private void MAJApresInsert(DataRow row)
		{
			RecupererIDAvecTrigger(row);
			RenseignerPourSynchro(row, StatementType.Insert);
            if ( m_bIsModifiedByTrigger )
                RecopieDonneesDeBase ( row );
			//ParserStringMySqlVersApplication(row);
			row.AcceptChanges();
		}
		private void MAJApresUpdate(DataRow row)
		{
			//ParserStringMySqlVersApplication(row);
            if (m_bIsModifiedByTrigger)
                RecopieDonneesDeBase(row);
			row.AcceptChanges();
		}
		private void MAJApresDelete(DataRow row)
		{
			//RenseignerPourSynchro(row, StatementType.Delete);
			row.AcceptChanges();
		}
		private void MAJApresSelect(DataRow row)
		{
			ParserStringMySqlVersApplication(row);
			row.AcceptChanges();
		}

		/// /////////////////////////////////////////////////////////////////
		private void RecupererIDAvecTrigger(DataRow row)
		{
			if (m_strRqtSelectionNewRowIDAvecTrigger != "")
			{
				DataTable dtRowLigne = new DataTable();
				dtRowLigne = ExecuterRequeteEnParallele(m_strRqtSelectionNewRowIDAvecTrigger);
				string nID = dtRowLigne.Rows[0][0].ToString();
				row[m_champIDAuto] = nID;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		private void RecupererIDSansTrigger(DataRow row)
		{
			if (row.RowState == DataRowState.Added && m_strRqtSelectionNewRowIDSansTrigger != "")
			{
				DataTable dtRowLigne = new DataTable();
				dtRowLigne = ExecuterRequeteEnParallele(m_strRqtSelectionNewRowIDSansTrigger);
				string nID = dtRowLigne.Rows[0][0].ToString();
				row[m_champIDAuto] = nID;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		private void RenseignerPourSynchro(DataRow row, StatementType typeOperation)
		{
			string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(row.Table.TableName);
			if (strNomTableInDb == null)
				strNomTableInDb = row.Table.TableName;
			//Si la synchro est active et que la table est synchronisable...
			if (m_synchro && row.Table.Columns[CSc2iDataConst.c_champIdSynchro] != null
			&& (typeOperation == StatementType.Insert || typeOperation == StatementType.Delete))
			{
				//Recupération de l'ID de l'élément
				string strID;
				DataColumn[] colsID = row.Table.PrimaryKey;
				if (colsID.Length == 1 && colsID[0].AutoIncrement)
					strID = row[colsID[0]].ToString();
				else
					return;

				//Creation de la requete
				int nIdSync = ((CMySqlDatabaseConnexionSynchronisable)m_encapsuleurConnexion).IdSyncSession;
				string rqtMAJTableSynchro = "Insert into " +
					CEntreeLogSynchronisation.c_nomTable + " (" +
					CEntreeLogSynchronisation.c_champTable + "," +
					CEntreeLogSynchronisation.c_champIdElement + "," +
					CEntreeLogSynchronisation.c_champType + "," +
					CSc2iDataConst.c_champIdSynchro + ") values (" +
					"'" + strNomTableInDb + "'," +
					strID + ",";

				if (typeOperation == StatementType.Insert)
					rqtMAJTableSynchro += ((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd).ToString();
				else
					rqtMAJTableSynchro += ((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete).ToString();

				rqtMAJTableSynchro += "," +	nIdSync.ToString() + ")";

				ExecuterRequeteEnParallele(rqtMAJTableSynchro);
			}
		}

		/// /////////////////////////////////////////////////////////////////
		private void ParserStringMySqlVersApplication(DataRow row)
		{
			object[] array = row.ItemArray;
			int nNbCols = row.Table.Columns.Count;
			bool bHasChange = false;
			for (int nCol = 0; nCol < nNbCols; nCol++)
				if (row.Table.Columns[nCol].DataType == typeof(string))
				{
					if (array[nCol] != DBNull.Value)
					{
						string strValeur = (string)array[nCol];
						if (strValeur.Length >= 1 && strValeur.Trim().Length == 0)
						//if (array[nCol] != DBNull.Value && array[nCol].ToString().Length >= 1 && array[nCol].ToString().Trim().Equals(""))
						{
							array[nCol] = strValeur.Substring(1);
							bHasChange = true;
						}
					}
				}

			if ( bHasChange )
				row.ItemArray = array;
		}
		#endregion

		/// /////////////////////////////////////////////////////////////////
		public DataTable ExecuterRequeteEnParallele(string rqt)
		{
			DataSet dsTmp = new DataSet();
			MySqlConnection connecMySql = m_connexion;
			MySqlTransaction transacMySql = m_transaction;


			MySqlDataAdapter infocolsadapter = new MySqlDataAdapter(rqt, connecMySql);

			if (transacMySql != null)
				infocolsadapter.SelectCommand.Transaction = transacMySql;

            lock (connecMySql)
            {
                infocolsadapter.Fill(dsTmp);
            }
            
			if (dsTmp.Tables.Count == 1)
				return dsTmp.Tables[0];
			else
				return null;
		}

		#region IDataAdapter
		/// /////////////////////////////////////////////////////////////////
		///Stef 22/07/08 : mise en cache des schémas des tables
		private static Dictionary<string, DataTable> m_cacheSchemas = new Dictionary<string, DataTable>();
		public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			if (dataSet.Tables.Count > 0 && dataSet.Tables[0].TableName != "Table" && m_adapter.TableMappings.Count == 0)
				m_adapter.TableMappings.Add("Table", dataSet.Tables[0].TableName);
			//Isole la partie de requête avant le where
			int nIndexWhere = m_adapter.SelectCommand.CommandText.ToUpper().IndexOf("WHERE");
			string strRequetePourSchema = m_adapter.SelectCommand.CommandText.Trim();
			if (nIndexWhere > 0)
				strRequetePourSchema = strRequetePourSchema.Substring(0, nIndexWhere - 1).Trim();
			DataTable tableCache = null;
			string strKeySchema = m_connexion.ConnectionString+"/"+strRequetePourSchema.ToUpper();

			//ATTENTION : la requête de schéma peut être la même s'il y a deux tables
			//du même nom dans deux bases différentes. Par contre, on est sûr que le nom
			//dans le CContexteDonnee est unique, donc on l'ajoute à la suite de la requête
			//pour la clé de cache
			if (dataSet.Tables.Count > 0)
				strKeySchema += "_" + dataSet.Tables[0].TableName;
			else
				if  (m_adapter.TableMappings.Count > 0 )
					strKeySchema += "_"+m_adapter.TableMappings[0].DataSetTable;
			if (m_cacheSchemas.TryGetValue(strKeySchema, out tableCache))
			{
				DataTable tableDest = dataSet.Tables[tableCache.TableName];
				if (tableDest != null)
				{
					//Copie la structure de la table modèle dans la table du DataSet
					if (tableDest.Columns.Count == 0)
					{
						foreach (DataColumn col in tableCache.Columns)
						{
							DataColumn colCopie = new DataColumn();
							colCopie.AllowDBNull = col.AllowDBNull;
							colCopie.AutoIncrement = col.AutoIncrement;
							colCopie.AutoIncrementSeed = col.AutoIncrementSeed;
							colCopie.AutoIncrementStep = col.AutoIncrementStep;
							colCopie.Caption = col.Caption;
							colCopie.ColumnMapping = col.ColumnMapping;
							colCopie.ColumnName = col.ColumnName;
							colCopie.DataType = col.DataType;
							colCopie.DateTimeMode = col.DateTimeMode;
							colCopie.DefaultValue = col.DefaultValue;
							colCopie.Expression = col.Expression;
							colCopie.MaxLength = col.MaxLength;
							colCopie.ReadOnly = col.ReadOnly;
							colCopie.Unique = col.Unique;
							tableDest.Columns.Add(colCopie);
						}
						List<DataColumn> keys = new List<DataColumn>();
						foreach (DataColumn col in tableCache.PrimaryKey)
						{
							keys.Add(tableDest.Columns[col.ColumnName]);
						}
						tableDest.PrimaryKey = keys.ToArray();
					}
					return new DataTable[] { tableDest };
				}
				else
				{
					tableDest = (DataTable)tableCache.Clone();
					dataSet.Tables.Add(tableDest);
					return new DataTable[] { tableDest };
				}
			}
			else
			{
				DataTable[] tables = m_adapter.FillSchema(dataSet, schemaType);

				for (int ndt = tables.Length; ndt > 0; ndt--)
				{
					DataTable dt = tables[ndt - 1];
					dt = TyperLesColonnes(dt);
					m_cacheSchemas[strKeySchema] = (DataTable)dt.Clone();
				}
				return tables;
			}


		}
		
		/// /////////////////////////////////////////////////////////////////
		private void BeforeFill( DataSet dataSet)
		{
			//Si il y a un table mapping ou une table dans le data set
			if (dataSet.Tables.Count > 0 || m_adapter.TableMappings.Count > 0)
				FillSchema(dataSet, SchemaType.Mapped);

			if (m_adapter.SelectCommand != null)
				foreach (MySqlParameter par in m_adapter.SelectCommand.Parameters)
					if ((par.DbType == DbType.String
						|| par.DbType == DbType.AnsiString
						|| par.DbType == DbType.AnsiStringFixedLength
						|| par.DbType == DbType.StringFixedLength)
						&& par.Value != null 
						&& par.Value.ToString().Trim().Equals(""))
						par.Value = " " + par.Value.ToString();
		}

		/// /////////////////////////////////////////////////////////////////
		private void AfterFill(DataSet dataSet)
		{
			//On reparse les chaines string
			bool bOldEnforce = dataSet.EnforceConstraints;
			dataSet.EnforceConstraints = false;
			foreach (DataTable dt in dataSet.Tables)
			{
				dt.BeginLoadData();
				for (int r = 0; r < dt.Rows.Count; r++)
				{
					DataRow dr = dt.Rows[r];
					ParserStringMySqlVersApplication(dr);
				}
				dt.EndLoadData();
				dt.AcceptChanges();
			}
			m_strRqtSelectionNewRowIDAvecTrigger = "";
			dataSet.EnforceConstraints = bOldEnforce;
		}

		/// /////////////////////////////////////////////////////////////////
		public int Fill(DataSet dataSet)
		{
			BeforeFill(dataSet);
            int i;
            lock (m_connexion)
            {
                i = m_adapter.Fill(dataSet);
            }
            
			AfterFill(dataSet);

			return i;
		}

		/// /////////////////////////////////////////////////////////////////
		public int Fill(DataSet dataSet, int nStartRecord, int nMaxRecords, string srcTable)
		{
			BeforeFill(dataSet);
            int i;
            i = m_adapter.Fill(dataSet, nStartRecord, nMaxRecords, srcTable);
			AfterFill(dataSet);

			return i;
		}

		/// /////////////////////////////////////////////////////////////////
		public int Update(DataSet dataSet)
		{
			int nbLigneAffectes = 0;
			if (m_adapter.TableMappings.Count > 0 && m_adapter.TableMappings[0].DataSetTable != "")
				nbLigneAffectes = ExecuteCommande(dataSet.Tables[m_adapter.TableMappings[0].DataSetTable]);
			else
				foreach (DataTable dt in dataSet.Tables)
					nbLigneAffectes += ExecuteCommande(dt);

			m_strRqtSelectionNewRowIDSansTrigger = "";
			m_strRqtSelectionNewRowIDAvecTrigger = "";
			return nbLigneAffectes;
		}

		/// /////////////////////////////////////////////////////////////////
		public int ExecuteCommande(IDbCommand command, DataRow row)
		{
			RecupererIDSansTrigger(row);
			foreach (IDataParameter parametre in command.Parameters)
			{
				if ((parametre.DbType == DbType.String
					|| parametre.DbType == DbType.AnsiString
					|| parametre.DbType == DbType.AnsiStringFixedLength
					|| parametre.DbType == DbType.StringFixedLength)
				&& row[parametre.SourceColumn, parametre.SourceVersion] != DBNull.Value
				&& ((string)row[parametre.SourceColumn, parametre.SourceVersion]).Trim() == "")
					parametre.Value = " " + row[parametre.SourceColumn, parametre.SourceVersion];
				else
					parametre.Value = row[parametre.SourceColumn, parametre.SourceVersion];
			}
			int nRetour = 0;
			IDataReader reader = null;
			try
			{
				m_transaction = (MySqlTransaction)command.Transaction;
				m_connexion = (MySqlConnection)command.Connection;
				reader = command.ExecuteReader();
				if (reader != null && reader.Read())
					for (int nField = 0; nField < reader.FieldCount; nField++)
						if (row.Table.Columns[reader.GetName(nField)] != null)
							row[reader.GetName(nField)] = reader.GetValue(nField);


				StatementType stType = StatementType.Select;
				switch (row.RowState)
				{
					case DataRowState.Added:		
						MAJApresInsert(row);	
						stType = StatementType.Insert;
						break;
					case DataRowState.Deleted:
						stType = StatementType.Delete;
						break;
					case DataRowState.Modified:		
						stType = StatementType.Update;
						MAJApresUpdate(row);	
						break;
					case DataRowState.Detached:
					case DataRowState.Unchanged:					
					default:						MAJApresSelect(row);	break;
				}
				if ( RowUpdated != null )
				{

					RowUpdated ( this, new MySqlRowUpdatedEventArgs ( row, command, stType, null ));
				}

				
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				if (reader != null)
					nRetour = reader.RecordsAffected;
				if (reader != null)
					reader.Close();
			}

			return nRetour;
		}
		private int ExecuteCommande(DataTable dt)
		{
			int nbLigneAffectes = 0;
			foreach (DataRow dr in dt.Rows)
                if ( dr.RowState != DataRowState.Unchanged )
				    nbLigneAffectes += ExecuteCommande(dr);
			return nbLigneAffectes;
		}
		private int ExecuteCommande(DataRow dr)
		{
			int nbLigneAffectes = 0;
            try
            {
                switch (dr.RowState)
                {
                    case DataRowState.Added:
                        if (!m_adapter.InsertCommand.CommandText.ToUpper().Contains("INSERT"))//Pas de commande
                            nbLigneAffectes++;
                        else
                            nbLigneAffectes += ExecuteCommande(m_adapter.InsertCommand, dr);
                        break;
                    case DataRowState.Deleted:
                        if (!m_adapter.DeleteCommand.CommandText.ToUpper().Contains("DELETE"))//Pas de commande
                            nbLigneAffectes++;
                        else
                            nbLigneAffectes += ExecuteCommande(m_adapter.DeleteCommand, dr);
                        break;
                    case DataRowState.Modified:
                        if (!m_adapter.UpdateCommand.CommandText.ToUpper().Contains("UPDATE"))//Pas de commande
                            nbLigneAffectes++;
                        else
                            nbLigneAffectes += ExecuteCommande(m_adapter.UpdateCommand, dr);
                        break;
                    case DataRowState.Detached:
                    case DataRowState.Unchanged:
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                StringBuilder bl = new StringBuilder();
                DataRowVersion version = DataRowVersion.Current;
                if ( dr.RowState == DataRowState.Deleted )
                    version = DataRowVersion.Original;
                foreach (DataColumn col in dr.Table.Columns)
                {
                    try
                    {
                        bl.Append(col.ColumnName);
                        bl.Append('=');
                        object val = dr[col, version];
                        if (val != null)
                            bl.Append(val.ToString());
                        else
                            bl.Append("null");
                        bl.Append('\t');
                    }
                    catch
                    {
                        
                    }
                }
                string strMes = "Error on row \r\n" + bl.ToString()+"\r\n"+e.ToString();
                Exception exDetail = new Exception(strMes, e);
                throw (exDetail);
            }
            if ( nbLigneAffectes != 1 && dr.Table.TableName   != sc2i.data.CVersionDonnees.c_nomTable)
                throw new Exception(I.T("Another program has modified the data.|103"));
			return nbLigneAffectes;
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
			get { return m_adapter.TableMappings; }
		}

		#endregion

		#region IDbDataAdapter
		public IDbCommand DeleteCommand
		{
			get
			{
				return m_adapter.DeleteCommand;
			}
			set
			{
				if(value is MySqlCommand)
					m_adapter.DeleteCommand = (MySqlCommand)value;
				else
					throw new Exception(I.T("The Delete command is not a valid MySql command|117"));
			}
		}
		public IDbCommand InsertCommand
		{
			get
			{
				return m_adapter.InsertCommand;
			}
			set
			{
				if (value is MySqlCommand)
					m_adapter.InsertCommand = (MySqlCommand)value;
				else
					throw new Exception(I.T("The Insert command is not a valid MySql command|118"));
			}
		}
		public IDbCommand SelectCommand
		{
			get
			{
				return m_adapter.SelectCommand;
			}
			set
			{
				if(value is MySqlCommand)
					m_adapter.SelectCommand = (MySqlCommand)value;
				else
					throw new Exception(I.T("The Select command is not a valid MySql command|119"));
			}
		}
		public IDbCommand UpdateCommand
		{
			get
			{
				return m_adapter.UpdateCommand;
			}
			set
			{
				if (value is MySqlCommand)
					m_adapter.UpdateCommand = (MySqlCommand)value;
				else
					throw new Exception(I.T("The Update command is not a valid MySql command|120"));
			}
		}
		#endregion
	
	}
}
