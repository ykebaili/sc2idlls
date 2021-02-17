using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using sc2i.data;
using sc2i.common;
using sc2i.multitiers.server;

namespace sc2i.data.serveur
{
	public class CSQLServeurDataBaseCreator : C2iDataBaseCreator
	{
		//------------------------------------------------------------------------------------------------
		public CSQLServeurDataBaseCreator(CSqlDatabaseConnexion connexion)
		{
			m_connection = connexion;
			m_connection.CommandTimeOut = 5 * 1000 * 60;
			m_mappeur = new CSQLServeurTypeMappeur();
		}

        protected override bool SaitGererLesIndexCluster()
        {
            return true;
        }

		private CSqlDatabaseConnexion m_connection;
		public override IDatabaseConnexion Connection
		{
			get { return (IDatabaseConnexion)m_connection; }
		}
		private CSQLServeurTypeMappeur m_mappeur;
		public override IDataBaseTypeMapper DataBaseTypesMappeur
		{
			get { return (IDataBaseTypeMapper)m_mappeur; }
		}


		#region Operations sur la structure
		//Database
		public override CResultAErreur CreateDatabase()
		{
			CResultAErreur result = CResultAErreur.True;
			if (!Connection.IsConnexionValide())
			{
				string strConnexionString = Connection.ConnexionString;
				//Trouve le nom de la base
				Regex findName = new Regex("Initial catalog[ ]*=[ ]*(.*);?", RegexOptions.IgnoreCase);
				Match match = findName.Match(strConnexionString);
				string strBase = "";
				if (match != null)
				{
					strBase = match.Value;
					string[] strVals = strBase.Split('=');
					if (strVals.Length > 1)
						strBase = strVals[1];
				}
				if (strBase == "")
				{
					result.EmpileErreur(I.T("Impossible to determine the database name|123"));
					return result;
				}
				Regex replaceName = new Regex("Initial catalog[ ]*=[ ]*(.*)(?<ptv>;?)", RegexOptions.IgnoreCase);
				strConnexionString = replaceName.Replace(strConnexionString, "Initial catalog=master${ptv}");

				SqlConnection sqlCon = new SqlConnection(strConnexionString);
				try
				{
					sqlCon.Open();
					SqlCommand command = new SqlCommand("Create database " + strBase, sqlCon);
					command.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
					result.EmpileErreur(I.T("Error while database creating|185"));
				}
				finally
				{
					try
					{
						sqlCon.Close();
					}
					catch
					{
					}
				}
				((CSqlDatabaseConnexion)Connection).GetConnexion().Open();
			}
			//result.EmpileErreur(I.T("'CreateDatabase' function not valid for connection @1|186", connexion.GetType().Name));
			return result;
		}
		public override CResultAErreur InitialiserDataBase()
		{
			CResultAErreur result = CResultAErreur.True;

			//Creation de la table du registre
			if (TableExists(CDatabaseRegistre.c_nomTable))
				result = DeleteTable(CDatabaseRegistre.c_nomTable);

			if(result)
			{
				result =
					m_connection.RunStatement(
					"Create table " + CDatabaseRegistre.c_nomTable + " (" +
					CDatabaseRegistre.c_champCle + " nvarchar(255) NOT NULL," +
					CDatabaseRegistre.c_champValeur + " nvarchar(255),"+
					CDatabaseRegistre.c_champBlob+" Image)");

				if (result)
					result =
						m_connection.RunStatement(
						"ALTER TABLE " + CDatabaseRegistre.c_nomTable +
						" ADD CONSTRAINT " +
						"PK_REGISTRE PRIMARY KEY NONCLUSTERED ( " +
						CDatabaseRegistre.c_champCle + " )"
						);
			}
			return result;

		}
		public override int NbTableInitialisation
		{
			get { return 1; }
		}

		//Table
		protected override DataTable GetDataTableForUpdateTable(CStructureTable structure)
		{
			IDataAdapter adapter = Connection.GetSimpleReadAdapter("SELECT * FROM " + structure.NomTableInDb);
			DataSet ds = new DataSet();
			adapter.FillSchema(ds, SchemaType.Mapped);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables["TABLE"];
			return dt;
		}
		public override CResultAErreur DeleteTable(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!TableExists(strNomTableInDb))
			{
				result.EmpileErreur(I.T("Impossible to delete the table @1 because is does not exist in the database|30013", strNomTableInDb));
				return result;
			}

			//Supprime les clés étrangeres des autres tables liées à la table que nous voulons supprimer
			result = DeleteTable_ClesEtrangeres(strNomTableInDb);
			if (!result)
				return result;

			result = DeleteTable_ClesPrimaires(strNomTableInDb);
			if (!result)
				return result;

			//Récupération de la liste des colonnes de la table
			List<string> cols = GetNomColonnes(strNomTableInDb);

			//Creation d'une colonne bidon pour pouvoir supprimer les autres colonnes proprement
			if (!cols.Contains("COLBIDON"))
				result = Connection.RunStatement("ALTER TABLE " + strNomTableInDb + " ADD COLBIDON NText NULL");
			else
				cols.Remove("COLBIDON");

			//Suppression des colonnes proprement
			if (result)
				foreach (string col in cols)
				{
					result = DeleteChamp(strNomTableInDb, col);
					if (!result)
						break;
				}

			//Suppression de la table
			if (result)
				result = Connection.RunStatement("DROP TABLE " + strNomTableInDb);


			return result;
		}

		//------------------------------------------------------------------------------------------------
		public List<string> GetNomColonnes(string strNomTable)
		{
			List<string> cols = new List<string>();

			string strCols = "SELECT col.name FROM sys.all_columns as col "+
								"INNER JOIN sys.all_objects as tb " +
								"ON tb.object_id = col.object_id " +
								"WHERE tb.type = 'U' and tb.name = '" + strNomTable + "'";
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strCols);
			DataSet ds = new DataSet();
			Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);

			DataTable dt = ds.Tables[0];

			foreach (DataRow dr in dt.Rows)
				cols.Add(dr[0].ToString());

			return cols;
		}
		public override CResultAErreur GetRelationsExistantes(string strNomTable, ref List<CInfoRelation> relationsTable)
		{
			CResultAErreur result = CResultAErreur.True;
			string strNomTableInContexte = m_connection.GetNomTableInContexteFromNomTableInDb(strNomTable);
			if (strNomTableInContexte == null)
				strNomTableInContexte = strNomTable;

			#region 1 - Selection des clefs etrangeres de la table
			string strRequeteGetForeignKey =
				"SELECT DISTINCT obj2.id as IDCles, obj2.[name] as Nom " +
				"FROM dbo.sysobjects AS obj1 " +
				"INNER JOIN syscolumns AS sc ON sc.id=obj1.id " +
				"INNER JOIN sysforeignkeys AS fk ON (fk.fkeyid=obj1.id AND colid=fk.fkey) " +
				"INNER JOIN sysobjects AS obj2 ON fk.constid=obj2.id " +
				"WHERE obj1.xtype='U' " +
				"AND obj1.name='" + strNomTable + "'";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			if (ds.Tables.Count != 1)
			{
				result.EmpileErreur(I.T("Impossible to recover the foreign keys of table '@1' to update them|213", strNomTable));
				return result;
			}
			DataTable dt = ds.Tables[0];
			#endregion
			
			#region 2 - Recupération des relations des clé etrangeres
			foreach (DataRow row in dt.Rows)
			{
				string[] strChampsParents;
				string[] strChampsFils;
				string strTableParente;
				string strIdContrainte = row[0].ToString();
				string strNomRelation = row[1].ToString();

				#region Recuperation des champs fils
				string strRequeteChampsFils =
					"SELECT DISTINCT syscolumns.[name] FROM sysforeignkeys " +
					"INNER JOIN syscolumns ON fkeyid = id " +
					"WHERE constid = '" + strIdContrainte + "' AND fkey = colid";
				IDataAdapter adapterChampsFils = Connection.GetSimpleReadAdapter(strRequeteChampsFils);
				DataSet dsChampsFils = new DataSet();
                Connection.FillAdapter(adapterChampsFils, dsChampsFils);
                CUtilDataAdapter.DisposeAdapter(adapterChampsFils);

				if (dsChampsFils.Tables.Count != 1 
					|| dsChampsFils.Tables[0].Rows.Count == 0
					|| dsChampsFils.Tables[0].Columns.Count != 1)
				{
					result.EmpileErreur(I.T("Impossible to recover the fields of table '@1' concerned with the relation '@2'|214", strNomTable, strNomRelation));
					return result;
				}
				ArrayList champsFils = new ArrayList();
				foreach(DataRow rowFils in dsChampsFils.Tables[0].Rows)
					champsFils.Add(rowFils[0].ToString());
				strChampsFils = (string[])champsFils.ToArray(typeof(string));
				#endregion

				#region Recuperation du nom de la table parente
				string strRequeteNomTableParente = 
						"SELECT DISTINCT sysobjects.[name] FROM sysforeignkeys " +
						"LEFT JOIN sysobjects ON rkeyid = id " +
						"WHERE constid = '" + strIdContrainte + "'";
				IDataAdapter adapterNomTableParente = Connection.GetSimpleReadAdapter(strRequeteNomTableParente);
				DataSet dsNomTableParente = new DataSet();
                Connection.FillAdapter(adapterNomTableParente, dsNomTableParente);
                CUtilDataAdapter.DisposeAdapter(adapterNomTableParente);

				if (dsNomTableParente.Tables.Count != 1 
					|| dsNomTableParente.Tables[0].Rows.Count != 1
					|| dsNomTableParente.Tables[0].Columns.Count != 1)
				{
					result.EmpileErreur(I.T("Impossible to recover the table name associated with the table '@1' by the relation '@2'|217", strNomTable, strNomRelation));
					return result;
				}
				strTableParente = dsNomTableParente.Tables[0].Rows[0][0].ToString();
				#endregion

				#region Recuperation des champs parents
				string strRequeteChampsParents =
					"SELECT DISTINCT syscolumns.[name] FROM sysforeignkeys " +
					"INNER JOIN syscolumns ON rkeyid = id " +
					"WHERE constid = '" + strIdContrainte + "' AND rkey = colid";
				IDataAdapter adapterChampsPeres = Connection.GetSimpleReadAdapter(strRequeteChampsParents);
				DataSet dsChampsPeres = new DataSet();
                Connection.FillAdapter(adapterChampsPeres, dsChampsPeres);
                CUtilDataAdapter.DisposeAdapter(adapterChampsPeres);
				if (dsChampsPeres.Tables.Count != 1
					|| dsChampsPeres.Tables[0].Rows.Count == 0
					|| dsChampsPeres.Tables[0].Columns.Count != 1)
				{
					result.EmpileErreur(I.T("Impossible to recover the fields of the parent table '@1' of the relation '@2' with the table '@3'|216",strTableParente, strNomRelation, strNomTable));
					return result;
				}
				ArrayList champsPeres = new ArrayList();
				foreach (DataRow rowPere in dsChampsPeres.Tables[0].Rows)
					champsPeres.Add(rowPere[0].ToString());
				strChampsParents = (string[])champsPeres.ToArray(typeof(string));
				#endregion

				string strTableParenteInContexte = m_connection.GetNomTableInContexteFromNomTableInDb(strTableParente);
				if (strTableParenteInContexte == null)
					strTableParenteInContexte = strTableParente;
				relationsTable.Add(new CInfoRelationAClefDefinissable(strNomRelation, strTableParenteInContexte, strNomTableInContexte, strChampsParents, strChampsFils));
			}
			#endregion

			return result;
		}

		//Champ
		public override CResultAErreur DeleteChamp_Dependances(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;
		
			//Suppression des clefs etrangeres
			result = DeleteChamp_ClesEtrangeres(strNomTableInDb, strNomChamp);



			//Regarde les autres contraintes
			string strRequeteGetForeignKey =
			"SELECT obj2.name " +
			"FROM dbo.sysobjects AS obj1 " +
			"INNER JOIN syscolumns AS sc ON sc.id=obj1.id " +
			"INNER JOIN sysconstraints AS scon ON (scon.id=obj1.id AND sc.colid=scon.colid) " +
			"INNER JOIN sysobjects AS obj2 ON scon.constid=obj2.id " +
			"AND obj1.name='" + strNomTableInDb + "' " +
			"AND sc.name='" + strNomChamp + "'";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];

			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTableInDb + " DROP CONSTRAINT " + (string)row[0];

				result = Connection.RunStatement(strRequeteSuppressionRelation);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting constrain @1 of table @2|133", (string)row[0], strNomTableInDb));
					return result;
				}
			}

			//Suppression des indexs
			result = DeleteIndex(strNomTableInDb, strNomChamp);

			return result;

		}
		public override CResultAErreur DeleteChamp_ClesEtrangeres(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;

			//Suppression des clefs etrangeres
			string strRequeteGetForeignKey =
				"SELECT DISTINCT fk.[name] " +
				"FROM sys.foreign_key_columns as fks " +
				"INNER JOIN sysobjects as tb on tb.id = fks.parent_object_id " +
				"INNER JOIN sysobjects as fk on fk.id = fks.constraint_object_id " +
				"LEFT JOIN syscolumns as col on (col.id = tb.id AND col.colid = fks.parent_column_id) " +
				"WHERE tb.[name] = '" + strNomTableInDb + "' " +
				"AND col.[name] = '" + strNomChamp + "'";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];
			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTableInDb + " DROP CONSTRAINT " + row[0].ToString();
				result = Connection.RunStatement(strRequeteSuppressionRelation);
				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting constraint @1 of table @2|133", row[0].ToString(), strNomTableInDb));
					return result;
				}
			}
			return result;
		}

		public CResultAErreur DeleteTable_ClesPrimaires(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;

			string strRequeteGetForeignKey =
				"SELECT pk.name FROM sys.all_objects as pk " +
				"INNER JOIN sys.all_objects as tb " +
				"ON tb.object_id = pk.parent_object_id " +
				"WHERE pk.type = 'PK' " +
				"AND tb.name = '" + strNomTableInDb + "'";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
			Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];
			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTableInDb + " DROP CONSTRAINT " + row[0].ToString();
				result = Connection.RunStatement(strRequeteSuppressionRelation);
				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting primary key @1 of table @2|30009", row[0].ToString(), strNomTableInDb));
					return result;
				}
			}
			return result;
		}
		public CResultAErreur DeleteTable_ClesEtrangeres(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;

			string strRequeteGetForeignKey =
				"SELECT tb1.name, relNom.name FROM sys.foreign_key_columns as rel " +
				"INNER JOIN sys.all_objects as tb1 " +
				"on tb1.object_id = rel.parent_object_id " +
				"INNER JOIN sys.all_objects as tb2 " +
				"on tb2.object_id = rel.referenced_object_id " +
				"INNER JOIN sys.all_objects as relNom " +
				"on relNom.object_id = rel.constraint_object_id " +
				"WHERE tb2.name = '"+strNomTableInDb+"'";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
			Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];
			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + row[0].ToString() + " DROP CONSTRAINT " + row[1].ToString();
				result = Connection.RunStatement(strRequeteSuppressionRelation);
				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting foreign key @1 attached at table @2|30010", row[1].ToString(), strNomTableInDb));
					return result;
				}
			}
			return result;
		}
		#endregion

		#region Creation de Requetes
		protected override string GetDeclarationChampIdForCreateTable(CStructureTable structure)
		{
			string strDeclaration = base.GetDeclarationChampIdForCreateTable(structure);
			strDeclaration = strDeclaration != ""? strDeclaration + " IDENTITY (1, 1)":"";
			return strDeclaration;
		}
		protected override string GetRequeteCreateClefPrimaire(string strNomTable, params string[] strFields)
		{
			string rqt = base.GetRequeteCreateClefPrimaire(strNomTable, strFields);
			rqt = rqt.Replace("PRIMARY KEY (", "PRIMARY KEY NONCLUSTERED (");
			rqt += " ON[PRIMARY]";
			return rqt;
		}
		protected override string GetRequeteCreateIndex(string strNomTable, bool bCluster, params string[] strFields)
		{
            Type tp = CContexteDonnee.GetTypeForTable(m_connection.GetNomTableInContexteFromNomTableInDb(strNomTable));
            if (tp != null && !bCluster)
            {
                CStructureTable structure = CStructureTable.GetStructure(tp);
                List<string> lstChamps = new List<string>();
                List<string> lstInclude = new List<string>();
                foreach (string strChamp in strFields)
                {
                    foreach (CInfoChampTable info in structure.Champs)
                    {
                        if (info.NomChamp == strChamp)
                        {
                            if (info.TypeDonnee == typeof(string) && info.Longueur > 900)
                            {
                                lstInclude.Add(strChamp);
                                break;
                            }
                            else
                            {
                                lstChamps.Add(strChamp);
                                break;
                            }
                        }
                    }
                }
                if (lstInclude.Count > 0)
                {
                    if (lstChamps.Count == 0)
                        lstChamps.Add(structure.ChampsId[0].NomChamp);
                    string strNomIndex = GetNewNomIndex(strNomTable, strFields);
                    string strRequeteIndex = "CREATE " + (bCluster ? "CLUSTERED" : "NONCLUSTERED") + " INDEX " + strNomIndex + " on " + strNomTable + " ";
                    if (lstChamps.Count > 0)
                    {
                        strRequeteIndex += "(";
                        foreach (string strchamp in lstChamps)
                            strRequeteIndex += strchamp + ",";
                        strRequeteIndex = strRequeteIndex.Substring(0, strRequeteIndex.Length - 1);
                        strRequeteIndex += ")";
                    }
                    if (lstInclude.Count > 0)
                    {
                        strRequeteIndex += "include (";
                        foreach (string strchamp in lstInclude)
                            strRequeteIndex += strchamp + ",";
                        strRequeteIndex = strRequeteIndex.Substring(0, strRequeteIndex.Length - 1);
                        strRequeteIndex += ")";
                    }
                    return strRequeteIndex;
                }
            }
			string rqt = base.GetRequeteCreateIndex(strNomTable, bCluster, strFields);
			rqt = rqt.Replace("CREATE INDEX", "CREATE "+(bCluster?"CLUSTERED":"NONCLUSTERED")+" INDEX");
			return rqt;
		}

		#endregion

		#region Nommage
		//------------------------------------------------------------------------------------------------
		protected override string GetNomIndex(string strNomTable, params string[] strFields)
		{
			string strNomIndex = "IX_" + strNomTable;
			foreach (string strField in strFields)
				strNomIndex += "_" + strField;
			return strNomIndex;
		}

		#endregion
		#region Verification Nommage
		#endregion

		#region Tests d'existances
		public override bool ChampExists(string strTableName, string strChampName)
		{
            Type tp = CContexteDonnee.GetTypeForTable ( strTableName );
            if ( tp == null )
                return true;
            CStructureTable structure = CStructureTable.GetStructure ( tp );
            DataTable dt = GetDataTableForUpdateTable(structure);
            return dt.Columns.Contains(strChampName);
		}
		//--------------------------------------------------------------------------------------
		public override bool SequenceExists(string NomSequence)
		{
			string strSeqExist = "SELECT * FROM SYS.USER_SEQUENCES ";
			strSeqExist += "WHERE SEQUENCE_NAME ='" + NomSequence + "'";
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strSeqExist);
			DataSet dsTmp = new DataSet();
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter);
			if (dsTmp.Tables[0].Rows.Count > 0)
				return true;
			else
				return false;
		}
		//--------------------------------------------------------------------------------------
		public override bool TriggerExists(string NomTrigger)
		{
			string strTriExist = "SELECT * FROM SYS.USER_TRIGGERS ";
			strTriExist += "WHERE SEQUENCE_NAME ='" + NomTrigger + "'";
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strTriExist);
			DataSet dsTmp = new DataSet();
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter);
			if (dsTmp.Tables[0].Rows.Count > 0)
				return true;
			else
				return false;
		}
		//--------------------------------------------------------------------------------------
		public override bool IndexExists(string strNomTable, params string[] strChamps)
		{
			//Vérifie l'existance de l'index
			string strNomIndex = GetNomIndex(strNomTable, strChamps);
			string strRequete = "select name from dbo.sysindexes where name='" + strNomIndex + "'";
			DataSet dsTmp = new DataSet();
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequete);
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter); 

			return dsTmp.Tables[0].Rows.Count != 0;
		}

        //--------------------------------------------------------------------------------------
        public override bool IsCluster(string strNomTable, params string[] strChamps)
        {
            string strNomIndex = GetNomIndex(strNomTable, strChamps);
            string strRequete = "select count(*) from dbo.sysindexes where name='" + strNomIndex + "' and indid=1";
            CResultAErreur result = Connection.ExecuteScalar(strRequete);
            if (result)
                return ((int)result.Data) > 0;
            return false;
        }

		#endregion
	}
}
