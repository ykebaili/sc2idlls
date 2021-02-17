using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
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
    public class C2iOracleDataAdapter : 
        IDbDataAdapter, 
        IDataAdapterARemplissagePartiel,
        IDataAdapterGerantLesModificationsParTrigger
	{
        //Indique si la table peut être modifiée par trigger lors d'insert ou update
        private bool m_bIsModifiedByTrigger = false;

        
		////////////////////////////////////////////////////
		public static C2iOracleDataAdapter GetOracleDataAdapter(IDbDataAdapter adapter)
		{
			C2iOracleDataAdapter oracleAdapter = null;
			if (adapter is C2iDataAdapterForClasseAutoReferencee)
				oracleAdapter = (C2iOracleDataAdapter)((C2iDataAdapterForClasseAutoReferencee)adapter).DataAdapterUtilise;
			else if (adapter is C2iOracleDataAdapter)
				oracleAdapter = (C2iOracleDataAdapter)adapter;

			return oracleAdapter;
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
		OracleDataAdapter m_adapter;

		string m_strRqtSelectionNewRowIDAvecTrigger="";
		string m_strRqtSelectionNewRowIDSansTrigger = "";
		private string m_champIDAuto;

		private bool m_bSynchro;
		private OracleConnection m_connexion;
		private OracleTransaction m_transaction;
		private COracleDatabaseConnexion m_encapsuleurConnexion;
		#endregion
		#region ++ Constructeur ++
		public C2iOracleDataAdapter(OracleCommand commande, COracleDatabaseConnexion OracleDbConnexion)
		{
			m_adapter = new OracleDataAdapter(commande);
			m_encapsuleurConnexion = OracleDbConnexion;
			m_connexion = commande.Connection;
			m_transaction = commande.Transaction;

			if (OracleDbConnexion is COracleDatabaseConnexionSynchronisable)
				m_bSynchro = true;
			else
				m_bSynchro = false;


			m_adapter.RowUpdated += new OracleRowUpdatedEventHandler(m_adapter_RowUpdated);
		}
		#endregion

		#region Avant Mise A Jour
        private static Dictionary<string, DataTable> m_dicTableToStructureOracle = new Dictionary<string, DataTable>();
        /// /////////////////////////////////////////////////////////////////
		private DataTable TyperLesColonnes(DataTable dt)
		{
			DataTable dtcol = new DataTable();
            string strNomTable = dt.TableName;
            foreach (DataTableMapping map in TableMappings)
            {
                if (map.SourceTable.ToUpper() == dt.TableName.ToUpper())
                    strNomTable = map.DataSetTable;
            }
			string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable);
			if (strNomTableInDb == null)
				strNomTableInDb = dt.TableName;
			string strRequeteInfoCols = "SELECT CNAME, COLTYPE, WIDTH, SCALE, \"PRECISION\" FROM SYS.COL WHERE ";
			strRequeteInfoCols += "TNAME = '" + strNomTableInDb + "'";
            if (!m_dicTableToStructureOracle.TryGetValue(strNomTable, out dtcol))
            {
                dtcol = ExecuterRequeteEnParallele(strRequeteInfoCols);
                m_dicTableToStructureOracle[strNomTable] = dtcol;
            }

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
					col.DataType = (new COracleTypeMapper()).GetTypeCSharpFromDBType(strTypeCol, strLngCol, strPrecisionCol, strEchelleCol);
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
		void m_adapter_RowUpdated(object sender, OracleRowUpdatedEventArgs e)
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
		public event OracleRowUpdatedEventHandler RowUpdated;

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
                    throw new Exception ("Oracle adapter error, Can not use 'Modified by trigger' on table with no Numeric ID");
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
            if (commande.Connection.State != ConnectionState.Open)
                commande.Connection.Open();
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
			//ParserStringOracleVersApplication(row);
			row.AcceptChanges();
		}
		private void MAJApresUpdate(DataRow row)
		{
			//ParserStringOracleVersApplication(row);
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
			ParserStringOracleVersApplication(row);
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
			if (m_bSynchro && row.Table.Columns[CSc2iDataConst.c_champIdSynchro] != null
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
				int nIdSync = ((COracleDatabaseConnexionSynchronisable)m_encapsuleurConnexion).IdSyncSession;
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
		private void ParserStringOracleVersApplication(DataRow row)
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
			OracleConnection connecoracle = m_connexion;
			OracleTransaction transacOracle = m_transaction;


			OracleDataAdapter infocolsadapter = new OracleDataAdapter(rqt, connecoracle);

			if (transacOracle != null)
				infocolsadapter.SelectCommand.Transaction = transacOracle;

            lock (connecoracle)
            {
                infocolsadapter.Fill(dsTmp);
            }
            
			if (dsTmp.Tables.Count == 1)
				return dsTmp.Tables[0];
			else
				return null;
		}

		#region IDataAdapter
        public static void ClearCacheSchemas()
        {
            m_cacheSchemas = new Dictionary<string, DataTable>();
        }

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
				foreach (OracleParameter par in m_adapter.SelectCommand.Parameters)
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
					ParserStringOracleVersApplication(dr);
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
            DateTime dt = DateTime.Now;
            int i = 0;
            if (true)
            {
                lock (m_connexion)
                {
                    if (m_adapter.SelectCommand != null)
                        foreach (OracleParameter par in m_adapter.SelectCommand.Parameters)
                            if ((par.DbType == DbType.String
                                || par.DbType == DbType.AnsiString
                                || par.DbType == DbType.AnsiStringFixedLength
                                || par.DbType == DbType.StringFixedLength)
                                && par.Value != null
                                && par.Value.ToString().Trim().Equals(""))
                                par.Value = " " + par.Value.ToString();
                    i = FillOptimized(dataSet, null, null);
                }
            }else
            {
                BeforeFill(dataSet);
                lock (m_connexion)
                {
                    i = m_adapter.Fill(dataSet);
                }
            }
            AfterFill(dataSet);
            TimeSpan sp = DateTime.Now - dt;
			return i;
		}

        /// /////////////////////////////////////////////////////////////////
        public int FillOptimized(DataSet dataSet, int? nStart, int? nMaxRecords)
        {
            bool bIsOpen = m_adapter.SelectCommand.Connection.State == ConnectionState.Open;
            if (!bIsOpen)
                m_adapter.SelectCommand.Connection.Open();
            OracleDataReader reader = m_adapter.SelectCommand.ExecuteReader();
            if (dataSet.Tables.Count == 0)
                dataSet.Tables.Add(new DataTable("table"));
            DataTable table = dataSet.Tables[0];
            Dictionary<int, string> mapChamps = null;
            table.BeginLoadData();
            if (reader != null)
            {
                int nLigne = 0;
                if (nStart != null && nStart.Value >= 0)
                    while (nLigne < nStart.Value && reader.Read())
                    {
                        nLigne++;
                    }
                nLigne = 0;
                while (reader.Read() && (nMaxRecords==null || nLigne <nMaxRecords.Value))
                {
                    nLigne++;
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
                        TyperLesColonnes(table);
                    }
                    DataRow row = table.NewRow();
                    foreach (KeyValuePair<int, string> champ in mapChamps)
                    {
                        object val = reader.GetValue(champ.Key);
                        if (val is Decimal)
                        {
                            if (table.Columns[champ.Value].DataType == typeof(int))
                                val = Convert.ToInt32(val);
                        }
                        row[champ.Value] = val;
                    }
                    table.Rows.Add(row);
                }
                reader.Close();
            }

            if (!bIsOpen)
                m_adapter.SelectCommand.Connection.Close();
            foreach ( DataTableMapping tableMapping in m_adapter.TableMappings )
            {
                if ( tableMapping.SourceTable == table.TableName )
                    table.TableName = tableMapping.DataSetTable;
            }
            table.AcceptChanges();
            table.EndLoadData();
            return table.Rows.Count;
        }

		/// /////////////////////////////////////////////////////////////////
		public int Fill(DataSet dataSet, int nStartRecord, int nMaxRecords, string srcTable)
		{
            DateTime dt = DateTime.Now;
            int i = 0;
            if (true)
            {
                lock (m_connexion)
                {
                    if (m_adapter.SelectCommand != null)
                        foreach (OracleParameter par in m_adapter.SelectCommand.Parameters)
                            if ((par.DbType == DbType.String
                                || par.DbType == DbType.AnsiString
                                || par.DbType == DbType.AnsiStringFixedLength
                                || par.DbType == DbType.StringFixedLength)
                                && par.Value != null
                                && par.Value.ToString().Trim().Equals(""))
                                par.Value = " " + par.Value.ToString();
                    i = FillOptimized(dataSet, nStartRecord, nMaxRecords);
                }
            }
            else
            {
                BeforeFill(dataSet);
                lock (m_connexion)
                {
                    i = m_adapter.Fill(dataSet, nStartRecord, nMaxRecords, srcTable);
                }
            }
            AfterFill(dataSet);
            TimeSpan sp = DateTime.Now - dt;
            return i;
			/*BeforeFill(dataSet);
            int i;
            i = m_adapter.Fill(dataSet, nStartRecord, nMaxRecords, srcTable);
			AfterFill(dataSet);

			return i;*/
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
				m_transaction = (OracleTransaction)command.Transaction;
				m_connexion = (OracleConnection)command.Connection;
                if (m_connexion.State != ConnectionState.Open)
                    m_connexion.Open();
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

					RowUpdated ( this, new OracleRowUpdatedEventArgs ( row, command, stType, null ));
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
            {
                throw new Exception(CObjetDonnee.GetMessageAccesConccurentiel(dr));
            }
             //   throw new Exception(I.T("Another program has modified the data.|103"));
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
				if(value is OracleCommand)
					m_adapter.DeleteCommand = (OracleCommand)value;
				else
					throw new Exception(I.T("The Delete command is not a valid Oracle command|117"));
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
				if (value is OracleCommand)
					m_adapter.InsertCommand = (OracleCommand)value;
				else
					throw new Exception(I.T("The Insert command is not a valid Oracle command|118"));
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
				if(value is OracleCommand)
					m_adapter.SelectCommand = (OracleCommand)value;
				else
					throw new Exception(I.T("The Select command is not a valid Oracle command|119"));
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
				if (value is OracleCommand)
					m_adapter.UpdateCommand = (OracleCommand)value;
				else
					throw new Exception(I.T("The Update command is not a valid Oracle command|120"));
			}
		}
		#endregion
	
	}
}
