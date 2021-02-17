using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Threading;
using System.Text;
using System.IO;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.server;
using sc2i.multitiers.client;

namespace sc2i.data.serveur
{
	/////////////////////////////////////////////////////////////////////////////////
	//Encapsule les accès à la base de données
	public class COracleDatabaseConnexion : C2iDbDatabaseConnexion, IObjetAttacheASession
	{
		public const string c_nomTableSysChampAuto = "SYS_CHAMP_AUTO";
		public const string c_nomChampSysChampAutoNomTable = "TABLE_NAME";
		public const string c_nomChampSysChampAutoNomChamp = "CHAMP_NAME";
		public const string c_nomChampSysChampAutoTriggerName = "TG_NAME";
		public const string c_nomChampSysChampAutoSeqName = "SQ_NAME";

		private ArrayList m_listeTables = null;
		private DataTable m_dtSYS_CHAMP_AUTO;	//Temporaire
		private string m_strNomTableSpaceIndex = "";

		/// <summary>
		/// Stocke les noms de séquence qui ne sont pas gerés dans la
		/// table SYS_CHAMP_AUTO
		/// </summary>
		private class CInfoChampAuto
		{
			public readonly string NomTable;
			public readonly string NomChamp;
			public readonly string NomSequence;
			public readonly string NomTrigger;
			public CInfoChampAuto(
				string strNomTable,
				string strNomChamp,
				string strNomSequence,
				string strNomTrigger)
			{
				NomTable = strNomTable;
				NomChamp = strNomChamp;
				NomSequence = strNomSequence;
				NomTrigger = strNomTrigger;
			}
		}
		private static List<CInfoChampAuto> m_listeChampsAutoHorsGestionSys = new List<CInfoChampAuto>();


		//////////////////////////////////////////////////////////////////
		public COracleDatabaseConnexion(int nIdSession)
			: base(nIdSession)
		{
		}

		//////////////////////////////////////////////////////////////////
		public static void SetSequenceForTableAIdAuto ( 
			string strNomTable, 
			string strNomChamp,
			string strNomSequence,
			string strNomTrigger)
		{
			m_listeChampsAutoHorsGestionSys.Add ( new CInfoChampAuto ( 
				strNomTable,
				strNomChamp,
				strNomSequence,
				strNomTrigger ));
		}

		public override IDataBaseCreator GetDataBaseCreator()
		{
			return (IDataBaseCreator)new COracleDataBaseCreator(this);
		}


		#region CONNEXION ET TRANSACTION
		/////////////////////////////////////////////////////////
		protected override IDbConnection GetNewConnexion(bool bOpenIfClose)
		{
			OracleConnection connexion = new OracleConnection(ConnexionString);
			if (bOpenIfClose)
			{
				switch (connexion.State)
				{
					case ConnectionState.Broken:
						connexion = null;
						return GetConnexion(bOpenIfClose);
					case ConnectionState.Closed:
						connexion.Open();
						break;
				}
			}
			return connexion;
		}
		
		/////////////////////////////////////////////////////////
		public override CResultAErreur BeginTrans( IsolationLevel isolationLevel )
		{
            return base.BeginTrans(GetIsolationLevel());
		}

        /// /////////////////////////////////////////////////////
        protected override CResultAErreur JusteBeforeTrans(IDbConnection connexion)
        {
            IDbCommand command = connexion.CreateCommand();
            command.CommandText = "Commit";
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                command.Dispose();
            }
            return CResultAErreur.True;
        }


        /// /////////////////////////////////////////////////////
        public override CResultAErreur CommitTrans()
        {
            return base.CommitTrans();
        }

        /// /////////////////////////////////////////////////////
        public override CResultAErreur RollbackTrans()
        {
            return base.RollbackTrans();
        }

		#endregion

		/// /////////////////////////////////////////////////////
		protected override IsolationLevel GetIsolationLevel()
		{
			return IsolationLevel.Unspecified;
		}

		/// /////////////////////////////////////////////////////
		public string NomTableSpaceIndex
		{
			get
			{
				return m_strNomTableSpaceIndex;
			}
			set
			{
				m_strNomTableSpaceIndex = value;
			}
		}


		#region Gestion de la numérotation automatique
		internal void ChargementSYSCHAMPAUTO()
		{
			string strTableChampsAuto = "";
			foreach (string strNomTable in TablesNames)
			{
				if (strNomTable == c_nomTableSysChampAuto)
				{
					strTableChampsAuto = strNomTable;
					break;
				}
			}
			if (strTableChampsAuto != "")
			{
				m_dtSYS_CHAMP_AUTO = new DataTable();

				string strRequete = "SELECT * FROM "+c_nomTableSysChampAuto;
				C2iOracleDataAdapter adapter = (C2iOracleDataAdapter)GetSimpleReadAdapter(strRequete);
				DataSet ds = new DataSet();
				DataTable dt = new DataTable();
				dt.TableName = "strTableChampsAuto";
				ds.Tables.Add(dt);
				this.FillAdapter(adapter, ds);
                CUtilDataAdapter.DisposeAdapter(adapter);
				m_dtSYS_CHAMP_AUTO = ds.Tables[0];
			}
			if (m_dtSYS_CHAMP_AUTO == null)
			{
				//Création de la tbale
				m_dtSYS_CHAMP_AUTO = new DataTable(c_nomTableSysChampAuto);
				m_dtSYS_CHAMP_AUTO.Columns.Add(c_nomChampSysChampAutoNomTable, typeof(string));
				m_dtSYS_CHAMP_AUTO.Columns.Add(c_nomChampSysChampAutoNomChamp, typeof(string));
				m_dtSYS_CHAMP_AUTO.Columns.Add(c_nomChampSysChampAutoSeqName, typeof(string));
				m_dtSYS_CHAMP_AUTO.Columns.Add(c_nomChampSysChampAutoTriggerName, typeof(string));

			}
			foreach (CInfoChampAuto info in m_listeChampsAutoHorsGestionSys)
			{
				DataRow row = m_dtSYS_CHAMP_AUTO.NewRow();
				row[c_nomChampSysChampAutoNomChamp] = info.NomChamp;
				row[c_nomChampSysChampAutoNomTable] = info.NomTable;
				row[c_nomChampSysChampAutoTriggerName] = info.NomTrigger;
				row[c_nomChampSysChampAutoSeqName] = info.NomSequence;
				m_dtSYS_CHAMP_AUTO.Rows.Add(row);
			}
		}

		/// <summary>
		/// Retourne le nom de la séquence pour la table
		/// </summary>
		/// <param name="nomTable"></param>
		/// <param name="nomCol"></param>
		/// <param name="bAvecTrigger">contient true si le champ auto est geré par trigger, false sinon 
		/// (dans ce cas, c'est le code qui doit aller chercher la valeur)</param>
		/// <returns></returns>
		public string GetNomSequenceColAuto(string nomTable, string nomCol, ref bool bAvecTrigger)
		{
			if (m_dtSYS_CHAMP_AUTO == null || m_dtSYS_CHAMP_AUTO.Rows.Count == 0)
				ChargementSYSCHAMPAUTO();

			DataRow[] lstrows = m_dtSYS_CHAMP_AUTO.Select("TABLE_NAME = '" + nomTable + "' AND CHAMP_NAME = '" + nomCol +"'");
			object objAvecTrigger = lstrows[0][c_nomChampSysChampAutoTriggerName];
			bAvecTrigger = !(objAvecTrigger == null || objAvecTrigger.ToString().Trim() == "");
			return lstrows[0][c_nomChampSysChampAutoSeqName].ToString();
		}
		public string GetNomTriggerColAuto(string nomTable, string nomCol)
		{
			if (m_dtSYS_CHAMP_AUTO == null || m_dtSYS_CHAMP_AUTO.Rows.Count == 0)
				ChargementSYSCHAMPAUTO();

			DataRow[] lstrows = m_dtSYS_CHAMP_AUTO.Select("TABLE_NAME = '" + nomTable + "' AND CHAMP_NAME = '" + nomCol + "'");
			return lstrows[0]["TG_NAME"].ToString();
		}
		#endregion

		
		/// /////////////////////////////////////////////////////
		public override IDataAdapter GetTableAdapter(string strNomTableInDb)
		{
			IDbCommand commandtmp = GetConnexion().CreateCommand();
			OracleCommand command = new OracleCommand();
			command.CommandText = "Select * from " + GetNomTableForRequete(strNomTableInDb);
			command.Connection = new OracleConnection(commandtmp.Connection.ConnectionString);


			if (command.Connection.ConnectionString.Length < ConnexionString.Length)
				command.Connection.ConnectionString = ConnexionString;


			C2iOracleDataAdapter adapter = new C2iOracleDataAdapter(command, this);
			return adapter;
		}
		/// /////////////////////////////////////////////////////
		protected override IDbDataAdapter GetNewAdapter(IDbCommand selectCommand)
		{
			C2iOracleDataAdapter adapter = new C2iOracleDataAdapter((OracleCommand)selectCommand, this);
			return (IDbDataAdapter) adapter;
		}

		public override CResultAErreur ExecuteRequeteComplexe(C2iChampDeRequete[] champs, CArbreTable arbreTables, CFiltreData filtre)
		{
			CResultAErreur result =  base.ExecuteRequeteComplexe(champs, arbreTables, filtre);
			if (!result)
				return result;
			else
			{
				DataTable dt = ((CDataTableFastSerialize)result.Data).DataTableObject;
				foreach (C2iChampDeRequete ch in champs)
				{
					string colch = ch.NomChamp.ToUpper();
					for (int ncol = dt.Columns.Count; ncol > 0; ncol -- )
					{
						DataColumn col = dt.Columns[ncol - 1];
						
						if (col.ColumnName == colch)
						{
                            Type tp = ch.TypeDonnee;
                            if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                                tp = tp.GetGenericArguments()[0];
							dt.Columns.Add(new DataColumn(col.ColumnName + "_2", tp));
							foreach(DataRow dr in dt.Rows)
								dr[col.ColumnName + "_2"] = dr[col];

							dt.Columns.Remove(col);
							dt.Columns[col.ColumnName + "_2"].ColumnName = col.ColumnName;
							break;
						}
					}
				}
			}
			return result;
		}	
		
		/////////////////////////////////////////////////////////////////////////////////////
		public override string GetSql(string strSelect, string strPrefixeFrom, string strJoin, string strWhere)
		{
			string strSql = strSelect + " " + strJoin;
            strWhere = PasseLaLimiteDesMilleIn(strWhere);
			if (strWhere.Trim() != "")
			{
				strSql += " WHERE (" + strWhere + ")";
			}
			return strSql;
		}


        /// /////////////////////////////////////////////////////
        public static string PasseLaLimiteDesMilleIn(string strWhere)
        {
            int nPos = strWhere.ToUpper().IndexOf(" IN ");
            while (nPos > 0)
            {
                //Trouve la parenthèse ouvrante
                int nPosOuvrante = strWhere.IndexOf('(', nPos);
                if (nPosOuvrante == -1)
                    return strWhere;


                int nFirstAvantNotSpace = nPos;
                while (strWhere[nFirstAvantNotSpace] == ' ')
                    nFirstAvantNotSpace--;

                //Cherche l'espace d'avant
                //Cherche l'espace d'avant
                int nStart = Math.Max(strWhere.LastIndexOf(" ", nFirstAvantNotSpace),
                    strWhere.LastIndexOf("(", nFirstAvantNotSpace));
                if (nStart == -1)
                    nStart = 0;
                while (strWhere[nStart] == '(')
                    nStart++;
                string strBefore = strWhere.Substring(nStart, nPos - nStart).Trim();

                //Cherche la parenthèse fermante
                int nPosFermante = strWhere.IndexOf(')', nPosOuvrante);
                if (nPosFermante == -1)
                    return strWhere;

                string strIn = strWhere.Substring(nPosOuvrante + 1, nPosFermante - nPosOuvrante - 1);

                if (strIn.Contains("'") || strIn.Contains("\""))
                    return strWhere;
                //Isole les valeurs
                string[] strVals = strIn.Split(',');
                if (strVals.Length > 999)
                {
                    //explosion avec un or
                    StringBuilder blNewRequete = new StringBuilder();
                    blNewRequete.Append(strWhere.Substring(0, nStart));
                    blNewRequete.Append(" (");
                    blNewRequete.Append(strBefore);
                    blNewRequete.Append(" in (");
                    int nNb = 0;
                    foreach (string strVal in strVals)
                    {
                        blNewRequete.Append(strVal);
                        nNb++;
                        if (nNb % 999 != 0 || nNb == strVals.Length)
                            blNewRequete.Append(',');
                        else
                        {
                            blNewRequete.Append(") or ");
                            blNewRequete.Append(strBefore);
                            blNewRequete.Append(" in (");
                        }
                    }
                    blNewRequete.Remove(blNewRequete.Length - 1, 1);
                    blNewRequete.Append(")) ");
                    blNewRequete.Append(strWhere.Substring(nPosFermante + 1));
                    strWhere = blNewRequete.ToString();
                }
                nPos = strWhere.ToUpper().IndexOf(" IN ", nPosFermante + 1);
                
            }
            return strWhere;
        }

		/// /////////////////////////////////////////////////////
		public override string GetSqlForAliasDecl(string strAlias)
		{
			return " " + strAlias;
		}
		
		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder(string strNomTableInDb)
		{
			return new C2iOracleAdapterBuilder(strNomTableInDb, this);
		}
		protected override IAdapterBuilder GetBuilder(Type tp)
		{
			return new C2iOracleAdapterBuilderForType(tp, this);
		}

		/// /////////////////////////////////////////////////////
		public override void PrepareTableToWriteDatabase(DataTable table)
		{
			/*int nNbCols = table.Columns.Count;
		
			foreach (DataRow row in table.Rows)
			{
				if (row.RowState != DataRowState.Unchanged && row.RowState != DataRowState.Deleted)
				{
					object[] array = row.ItemArray;
					for (int nCol = 0; nCol < nNbCols; nCol++)
						if (table.Columns[nCol].DataType == typeof(string))
							if (array[nCol] != DBNull.Value && array[nCol].ToString().Trim().Equals(""))
								array[nCol] = array[nCol].ToString() + " ";

					row.ItemArray = array;
				}
			}*/
		}

		//Non table->True si desactivé, null sinon
		private Hashtable m_tableEtatIdAuto = new Hashtable();
		protected override bool IsIdAutoDesactive(string strNomTable)
		{
			return (m_tableEtatIdAuto[strNomTable] is bool) && (bool)m_tableEtatIdAuto[strNomTable];
		}

		#region A CONVERTIR ORACLE ...
		/// /////////////////////////////////////////////////////
		public override void DesactiverContraintes(bool bDesactiver)
		{
			
			foreach (string strTable in TablesNames)
			{
				string strRequeteGetConstraints = " SELECT Contrainte.CONSTRAINT_NAME ";
				strRequeteGetConstraints += "FROM SYS.USER_CONS_COLUMNS Colonne, SYS.USER_CONSTRAINTS Contrainte ";
				strRequeteGetConstraints += "WHERE Colonne.CONSTRAINT_NAME = Contrainte.CONSTRAINT_NAME ";
				strRequeteGetConstraints += "AND (Colonne.TABLE_NAME ='" + strTable + "') ";
				IDataAdapter adapter = GetSimpleReadAdapter(strRequeteGetConstraints);
				DataSet ds = new DataSet();
                this.FillAdapter(adapter, ds);
                CUtilDataAdapter.DisposeAdapter(adapter);
                DataTable table = ds.Tables[0];
				foreach (DataRow row in table.Rows)
				{
					string strRequete = "ALTER TABLE " + strTable + " " +
						(bDesactiver ? "DISABLE " : "Enable") + " CONSTRAINT " +
						row[0];
					RunStatement(strRequete);
				}
			}
		}
		
		
		

		//------------------------------------------------------------
		public override void DesactiverIdAuto(string strNomTableInDb, bool bDesactiver)
		{
			string strReq = "ALTER TABLE " + strNomTableInDb + " " +
				(bDesactiver ? "DISABLE" : "ENABLE") + " ALL TRIGGERS";
			RunStatement(strReq);
			m_tableEtatIdAuto[strNomTableInDb] = bDesactiver;
			
		}
		///PAS DEQUIVALENCE
		public override int GetMaxIdentity(string strNomTableInDb)
		{
			string strSql = "select max(IDENTITYCOL) from " + strNomTableInDb;
			IDbConnection con = GetConnexion(true);
			IDbCommand command = con.CreateCommand();
			command.CommandType = CommandType.Text;
			if (IsInTrans())
				command.Transaction = Transaction;
			command.CommandText = strSql;
			object obj = command.ExecuteScalar();
			int nVal = obj == DBNull.Value ? 0 : (int)obj;
			if (!IsInTrans())
				con.Close();
			return nVal;
		}
		#endregion


		/////////////////////////////////////////////////////////////////////////////////////
		public override string[] TablesNames
		{
			get
			{
				string strRequete = "";
				if ( m_listeTables != null )
				{
					strRequete = "select count(*) from SYS.USER_ALL_TABLES";
					CResultAErreur result = ExecuteScalar ( strRequete );
					if ( result && (decimal)result.Data == m_listeTables.Count )
					{
						return (string[])m_listeTables.ToArray(typeof(string));
					}
				}
						
				m_listeTables = new ArrayList();
				string strBDD = this.GetConnexion().Database.ToString();
				strRequete = "SELECT * FROM SYS.USER_TABLES";
				OracleDataAdapter adapter = new OracleDataAdapter(strRequete, (OracleConnection)this.GetConnexion());
				adapter.SelectCommand.Transaction = (OracleTransaction)Transaction;
				DataTable tableTablesNames = new DataTable();
                this.FillAdapter(adapter, tableTablesNames);
                
                foreach(DataRow row in tableTablesNames.Rows)
				{
					m_listeTables.Add(row["TABLE_NAME"].ToString());
				}
				return (string[]) m_listeTables.ToArray(typeof(string));
			}
		}
		
		

		#region Generation Filtre & Requetes
		/// /////////////////////////////////////////////////////
		public override string GetNomTableForRequete(string strNomTableInDb)
		{
			return strNomTableInDb;
		}
		/// /////////////////////////////////////////////////////
		public override string GetNomParametre(string strNomParametre)
		{
			return ":" + strNomParametre;
		}
		/////////////////////////////////////////////////////////////////////////////////////
		public override string GetStringForRequete(object valeur)
		{
			return CConvertisseurObjetToOracle.ConvertToOracle(valeur);
		}
		/////////////////////////////////////////////////////////////////////////////////////
		public override IFormatteurFiltreDataToString GetFormatteurFiltre()
		{
			return new CFormatteurFiltreDataToStringOracle(this);
		}
		/// /////////////////////////////////////////////////////		
		public override void CreateJoinPourLiens(
			CFiltreData filtre,
			CArbreTable arbreTables, 
			CComposantFiltre composantFiltre, 
			ref bool bDistinct,
			ref string strFrom,
			ref string strWhere,
            ref string strPrefixeFrom)
		{
			strFrom = "";
			strWhere = "";
			foreach (CArbreTableFille arbreFils in arbreTables.TablesLiees)
			{
				if (arbreFils.Relation.IsRelationFille)
					bDistinct = true;

                string strNomTableFils = CContexteDonnee.GetNomTableInDbForNomTable(arbreFils.NomTable);
                strNomTableFils = GetPrefixeForTable(arbreFils.NomTable) + strNomTableFils;

				strFrom += ","+strNomTableFils + GetSqlForAliasDecl ( arbreFils.Alias );

				string strSuiteFrom = "";
				string strSuiteWhere  = "";
				CreateJoinPourLiens(filtre, arbreFils, composantFiltre, ref bDistinct, ref strSuiteFrom, ref strSuiteWhere, ref strPrefixeFrom);

				if (strSuiteFrom != "")
					strFrom += strSuiteFrom;
				if (strWhere.Trim() != "" && strSuiteWhere.Trim() != "")
					strWhere += " and (" + strSuiteWhere + ")";
				if (strWhere.Trim() == "" && strSuiteWhere.Trim() != "")
					strWhere = strSuiteWhere;

				//Equivalence WHERE ( MACOL = MACOL (+))
				CInfoRelationComposantFiltre relation = arbreFils.Relation;
				string strAliasParent, strAliasFille;
				string strSuffixeParent = "";
				string strSuffixeFils = "";
				string strTableDependante = "";
				if (relation.IsRelationFille)
				{
					strAliasParent = arbreTables.Alias;
					strAliasFille = arbreFils.Alias;
					strTableDependante = arbreFils.NomTable;
					if (arbreFils.IsLeftOuter)
						strSuffixeFils = "(+)";
				}
				else
				{
					strAliasParent = arbreFils.Alias;
					strAliasFille = arbreTables.Alias;
					strTableDependante = arbreTables.Alias;
					if ( arbreFils.IsLeftOuter )
						strSuffixeParent = "(+)";
				}
				string strTmp = relation.GetJoinClause(strAliasParent, strSuffixeParent, strAliasFille, strSuffixeFils );
				string strComplementVersion = "";
				if (EstCeQueLaTableGereLesVersions(strTableDependante) && !filtre.IntegrerLesElementsSupprimes)
				{
					strComplementVersion = "("+strAliasFille + "." + CSc2iDataConst.c_champIsDeleted + "=0 or "+
						strAliasFille+"."+CSc2iDataConst.c_champIsDeleted +" is null)";
				}
				string strIdsVersionsALire = filtre.GetStringListeIdsVersionsALire(',');
				if (EstCeQueLaTableGereLesVersions(strTableDependante))
				{
					if (strComplementVersion != "")
						strComplementVersion += " and ";
					if (strIdsVersionsALire == null)
						strComplementVersion += strAliasFille + "." + CSc2iDataConst.c_champIdVersion + " is null";
					else
						strComplementVersion += "(" + strAliasFille + "." + CSc2iDataConst.c_champIdVersion + " in (" + strIdsVersionsALire + ") or " +
							strAliasFille+"."+CSc2iDataConst.c_champIdVersion + " is null)";
				}
				if (strComplementVersion != "")
				{
					if (strTmp != "")
						strTmp = "((" + strTmp + ") and ";
					else
						strAliasFille += "(";
					strTmp += strComplementVersion + ")";
				}
				if (strWhere.Trim() != "")
					strWhere = "(" + strTmp + ") and (" + strWhere + ")";
				else strWhere = strTmp;

				if (composantFiltre != null)
					composantFiltre.DefinitAlias(arbreFils.CheminRelations, arbreFils.Alias);
                if (filtre is CFiltreDataAvance)
                {
                    foreach (CComposantFiltreChamp champ in ((CFiltreDataAvance)filtre).ChampsAAjouterAArbreTable)
                        champ.DefinitAlias(arbreFils.CheminRelations, arbreFils.Alias);
                }
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////
		public override int CountRecords(string strNomTableIndb, CFiltreData filtre)
		{
			filtre.SortOrder = "";
			string strNomTableInContexte = GetNomTableInContexteFromNomTableInDb(strNomTableIndb);
			Type tp = CContexteDonnee.GetTypeForTable(strNomTableInContexte);
			string strRequete = "";
			if (tp != null)
			{
				
				CStructureTable structure = CStructureTable.GetStructure(tp);
				if (structure.ChampsId.Length == 1)
					strRequete = "select count ( distinct " + strNomTableIndb + "." + structure.ChampsId[0].NomChamp + ") COMBIEN from " + strNomTableIndb;
			}
			if (strRequete == "")
				strRequete = "select count (*) COMBIEN from " + strNomTableIndb;


			IDataAdapter adapter = GetSimpleReadAdapter(strRequete, filtre);

			DataSet ds = new DataSet();
            FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			return Convert.ToInt32(ds.Tables["Table"].Rows[0]["COMBIEN"]);
		}
		/// /////////////////////////////////////////////////////
		public override DbType GetDbType(Type tp)
		{

			if (tp == typeof(String))
				return DbType.String;
			if (tp == typeof(int) || tp == typeof(int?))
				return DbType.Int32;
			if (tp == typeof(double) || tp == typeof(double?))
				return DbType.Double;
			if (tp == typeof(DateTime) || tp == typeof(DateTime?) || tp == typeof(CDateTimeEx))
				return DbType.DateTime;
			if (tp == typeof(bool) || tp == typeof(bool?))
				return DbType.Boolean;
			if (tp == typeof(CDonneeBinaireInRow))
				return DbType.Binary;
			if (tp == typeof(byte[]))
				return DbType.Binary;
			if (tp == typeof(Decimal))
				return DbType.Decimal;
			if (tp == typeof(Single))
				return DbType.Single;
			if (tp == typeof(Int16) || tp == typeof(Int16?))
				return DbType.Int16;
			if (tp == typeof(Byte) || tp == typeof(Byte?))
				return DbType.Byte;
			if (tp.IsEnum)
				return DbType.Int32;
			Console.WriteLine(I.T("Data type without Sql conversion : @1|112", tp.ToString()));
			return DbType.String;
		}

		#endregion

	}
}
