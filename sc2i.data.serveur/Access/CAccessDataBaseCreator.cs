using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using sc2i.data;
using sc2i.common;
using sc2i.multitiers.server;
using System.Data.OleDb;
using System.Data.Common;

namespace sc2i.data.serveur
{
	public class CAccessDataBaseCreator : C2iDataBaseCreator
	{
        private class CCacheOleDbSchema
        {
            public DataTable TableSchemaColonnes = null;
            public DataTable TableSchemaForeignKeys = null;
            public DataTable TableSchemaIndexs = null;
        }

        private static Dictionary<string, CCacheOleDbSchema> m_dicCacheSchemas = new Dictionary<string, CCacheOleDbSchema>();

        
		//------------------------------------------------------------------------------------------------
        public CAccessDataBaseCreator(CAccess97DatabaseConnexion connexion)
		{
			m_connection = connexion;
			m_connection.CommandTimeOut = 5 * 1000 * 60;
            m_mappeur = new CAccessTypeMappeur();
		}

        protected override bool SaitGererLesIndexCluster()
        {
            return false;
        }

        public override bool AutoAdaptLongString
        {
            get
            {
                return true;
            }
        }

        private CCacheOleDbSchema CacheSchemas
        {
            get
            {
                CCacheOleDbSchema cache = null;
                if (!m_dicCacheSchemas.TryGetValue(m_connection.ConnexionString, out cache))
                {
                    cache = new CCacheOleDbSchema();
                    m_dicCacheSchemas[m_connection.ConnexionString] = cache;
                }
                return cache;
            }
        }

        protected override void AfterChangeChamps(string strNomTableInDb)
        {
            base.AfterChangeChamps(strNomTableInDb);
            CacheSchemas.TableSchemaColonnes = null;
        }

        protected override void AfterChangeIndexs(string strNomTableInDb)
        {
            base.AfterChangeIndexs(strNomTableInDb);
            CacheSchemas.TableSchemaIndexs = null;
        }

        protected override void AfterChangeContraintes(string strNomTableInDb)
        {
            base.AfterChangeContraintes(strNomTableInDb);
            CacheSchemas.TableSchemaForeignKeys = null;
        }

		private CAccess97DatabaseConnexion m_connection;
		public override IDatabaseConnexion Connection
		{
			get { return (IDatabaseConnexion)m_connection; }
		}
        private CAccessTypeMappeur m_mappeur;
		public override IDataBaseTypeMapper DataBaseTypesMappeur
		{
			get { return (IDataBaseTypeMapper)m_mappeur; }
		}

        private string GetStringValeurParDefaut ( Type tp )
        {
            if (tp.IsEnum)
                return "0";
            else if (tp == typeof(DateTime) || tp == typeof(DateTime?) || tp == typeof(CDateTimeEx))
                return "DATE()";
            else if (tp == typeof(double) || tp == typeof(double?) || tp == typeof(int) || tp == typeof(int?))
                return "0";
            else if (tp == typeof(string))
                return "''";
            else if (tp == typeof(bool) || tp == typeof(bool?))
                return "0";
            else
                return "";
        }

        /// <summary>
        /// Retourne la déclaration d'un colonne
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public override string GetDeclarationDefaultValueForType(Type tp)
        {
            string strDefault = GetStringValeurParDefaut(tp);
            if (tp.IsEnum)
                return "DEFAULT "+strDefault;
            else if (tp == typeof(DateTime) || tp == typeof(DateTime?) || tp == typeof(CDateTimeEx))
                return "DEFAULT "+strDefault;
            else if (tp == typeof(double) || tp == typeof(double?) || tp == typeof(int) || tp == typeof(int?))
                return "DEFAULT "+strDefault;
            else if (tp == typeof(string))
                return "DEFAULT "+strDefault;
            else if (tp == typeof(bool) || tp == typeof(bool?))
                return "DEFAULT "+strDefault;
            else
                return "";
        }


		#region Operations sur la structure
		//Database
		public override CResultAErreur CreateDatabase()
		{
			CResultAErreur result = CResultAErreur.True;
            //A faire
            result.EmpileErreur("Can not create Access Database");
            return result;
        }
			
        //--------------------------------------------------------
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
					CDatabaseRegistre.c_champBlob+" "+DataBaseTypesMappeur.GetStringDBTypeFromType ( typeof(byte[]) )+")");
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

        //-----------------------------------------------
        private DataTable GetOleDbSchemaColonnes()
        {
            if (CacheSchemas.TableSchemaColonnes == null)
            {
                OleDbConnection cnx = m_connection.GetConnexion() as OleDbConnection;
                ConnectionState oldState = cnx.State;
                if (oldState != ConnectionState.Open)
                    cnx.Open();
                CacheSchemas.TableSchemaColonnes = cnx.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new string[] { null });
                if (oldState != ConnectionState.Open)
                    cnx.Close();
            }
            return CacheSchemas.TableSchemaColonnes;
        }

        //-----------------------------------------------
        private DataTable GetOleDbSchemaForeignKeys()
        {
            if (CacheSchemas.TableSchemaForeignKeys == null)
            {
                OleDbConnection cnx = m_connection.GetConnexion() as OleDbConnection;
                ConnectionState oldState = cnx.State;
                if (oldState != ConnectionState.Open)
                    cnx.Open();
                CacheSchemas.TableSchemaForeignKeys = cnx.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new string[] { null });
                if (oldState != ConnectionState.Open)
                    cnx.Close();
            }
            return CacheSchemas.TableSchemaForeignKeys;
        }

        //-----------------------------------------------
        private DataTable GetOleDbSchemaIndexs()
        {
            if (CacheSchemas.TableSchemaIndexs == null)
            {
                OleDbConnection cnx = m_connection.GetConnexion() as OleDbConnection;
                ConnectionState oldState = cnx.State;
                if (oldState != ConnectionState.Open)
                    cnx.Open();
                CacheSchemas.TableSchemaIndexs = cnx.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, new string[] { null });
                if (oldState != ConnectionState.Open)
                    cnx.Close();
            }
            return CacheSchemas.TableSchemaIndexs;
        }

		//Table
		protected override DataTable GetDataTableForUpdateTable(CStructureTable structure)
		{
            DataTable schema = GetOleDbSchemaColonnes();
			IDataAdapter adapter = Connection.GetSimpleReadAdapter("SELECT * FROM " + structure.NomTableInDb);
			DataSet ds = new DataSet();
			adapter.FillSchema(ds, SchemaType.Mapped);
			DataTable dt = ds.Tables["TABLE"];
            DataView view = new DataView(schema);
            foreach (DataColumn col in dt.Columns)
            {
                view.RowFilter = "TABLE_NAME='" + structure.NomTableInDb + "' and " +
                    "COLUMN_NAME='" + col.ColumnName + "'";
                if (view.Count > 0)
                    col.AllowDBNull = (bool)view[0]["IS_NULLABLE"];
            }
            CUtilDataAdapter.DisposeAdapter(adapter);
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

            IDataAdapter adapter = m_connection.GetTableAdapter(strNomTable);
            adapter.TableMappings.Add("Table", strNomTable);
            DataSet ds = new DataSet();
            adapter.FillSchema(ds, SchemaType.Mapped);
            CUtilDataAdapter.DisposeAdapter(adapter);
            DataTable tbl = ds.Tables[strNomTable];
            foreach (DataColumn col in tbl.Columns)
                cols.Add(col.ColumnName);
			return cols;
		}

        //------------------------------------------------------------------------------------------------
		public override CResultAErreur GetRelationsExistantes(string strNomTable, ref List<CInfoRelation> relationsTable)
		{
            CResultAErreur result = CResultAErreur.True;
            DataTable schema = GetOleDbSchemaForeignKeys();
            
            DataView view = new DataView(schema);
            view.RowFilter = "FK_TABLE_NAME='" + strNomTable+"'";
            foreach (DataRowView row in view)
            {
                CInfoRelationAClefDefinissable info = new CInfoRelationAClefDefinissable(
                    (string)row["FK_NAME"],
                    (string)row["PK_TABLE_NAME"],
                    (string)row["FK_TABLE_NAME"],
                    new string[] { (string)row["PK_COLUMN_NAME"] },
                    new string[] { (string)row["FK_COLUMN_NAME"] });
                relationsTable.Add(info);
            }

            return result;

		}

		//Champ
		public override CResultAErreur DeleteChamp_Dependances(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;
		
			//Suppression des clefs etrangeres
			result = DeleteChamp_ClesEtrangeres(strNomTableInDb, strNomChamp);

            //Suppression des clés étrangères qui pointent sur ce champ
            DataTable schema = GetOleDbSchemaForeignKeys();
            DataView view = new DataView(schema);
            view.RowFilter = "PK_TABLE_NAME='" + strNomTableInDb + "'";

            foreach (DataRowView row in view)
            {
                string strRequete = "ALTER TABLE " + (string)row["FK_TABLE_NAME"] +
                    " drop constraint " + (string)row["FK_NAME"];
                result = m_connection.RunStatement(strRequete);
                if (result)
                    AfterChangeContraintes((string)row["FK_TABLE_NAME"]);
                if (!result)
                    return result;
            }
               
   

			//Suppression des indexs
			result = DeleteIndex(strNomTableInDb, strNomChamp);
            
			return result;

		}
		public override CResultAErreur DeleteChamp_ClesEtrangeres(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;
            DataTable schema = GetOleDbSchemaForeignKeys();
            DataView view = new DataView(schema);
            view.RowFilter = "FK_TABLE_NAME='" + strNomTableInDb + "'";
            foreach (DataRowView rowView in view)
            {
                string strRequete = "alter table " + strNomTableInDb + " drop constraint " +
                    (string)rowView["FK_NAME"];
                result = m_connection.RunStatement(strRequete);
                if (result)
                    AfterChangeContraintes(strNomTableInDb);
                if (!result)
                    return result;
            }
            return result;

			
		}

		public CResultAErreur DeleteTable_ClesPrimaires(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;

            DataTable schema = GetOleDbSchemaIndexs();
            DataView view = new DataView(schema);
            view.RowFilter = "TABLE_NAME='" + strNomTableInDb + "'";
            HashSet<string> indexFaits = new HashSet<string>();
            foreach (DataRowView rowView in view)
            {
                if (!indexFaits.Contains((string)rowView["INDEX_NAME"]))
                {
                    string strRequete = GetRequeteDeleteIndex(strNomTableInDb, (string)rowView["INDEX_NAME"]);
                    result = m_connection.RunStatement(strRequete);
                    if (result)
                    {
                        AfterChangeIndexs(strNomTableInDb);
                    }
                    if (!result)
                        return result;
                    indexFaits.Add((string)rowView["INDEX_NAME"]);
                }
            }

			return result;
		}

		public CResultAErreur DeleteTable_ClesEtrangeres(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;

            DataTable schema = GetOleDbSchemaForeignKeys();
            DataView view = new DataView(schema);
            view.RowFilter = "PK_TABLE_NAME='" + strNomTableInDb + "'";
            foreach (DataRowView rowView in view)
            {
                string strRequete = "alter table " + (string)rowView["FK_TABLE_NAME"] + " drop constraint " +
                    (string)rowView["FK_NAME"];
                result = m_connection.RunStatement(strRequete);
                if (result)
                    AfterChangeContraintes((string)rowView["FK_TABLE_NAME"]);
                if (!result)
                    return result;
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
			return rqt;
		}

        private string GetNewId(string strPrefixe)
        {
            return strPrefixe + CUniqueIdentifier.GetNew("ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890_");
        }

        protected override string GetNewNomClefEtrangere(CInfoRelation rel)
        {
            return GetNewId("FK_");
        }

        protected override string GetNewNomSequence(string strNomTable)
        {
            return GetNewId("SQ_");
        }

        protected override string GetNewNomTrigger(string strNomTable)
        {
            return GetNewId("TR_");
        }

        protected override string GetNewNomIndex(string strNomTable, params string[] strFields)
        {
            return GetNewId("IX_");
        }

        protected override bool IndexAllowed(string strNomTable, string[] strChamps)
        {
            DataTable schema = GetOleDbSchemaColonnes();
            DataView view = new DataView(schema);
            foreach ( string strChamp in strChamps )
            {
                view.RowFilter = "TABLE_NAME='"+strNomTable+"' and COLUMN_NAME='"+strChamp+"'";
                if (view.Count > 0)
                {
                    int nType = (int)view[0]["DATA_TYPE"];
                    OleDbType tp = (OleDbType)nType;
                    if (tp == OleDbType.LongVarChar || tp == OleDbType.LongVarWChar ||
                        tp == OleDbType.WChar )
                        return false;
                }
            }
            return true;
        }

		protected override string GetRequeteCreateIndex(string strNomTable, bool bCluster, params string[] strFields)
		{
            return base.GetRequeteCreateIndex(strNomTable, bCluster, strFields);
		}

		#endregion

		#region Nommage
		/*//------------------------------------------------------------------------------------------------
		protected override string GetNomIndex(string strNomTable, params string[] strFields)
		{
			string strNomIndex = "IX_" + strNomTable;
			foreach (string strField in strFields)
				strNomIndex += "_" + strField;
			return strNomIndex;
		}*/

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
            return false;
			
		}
		//--------------------------------------------------------------------------------------
		public override bool TriggerExists(string NomTrigger)
		{
            return false;
        }

        //--------------------------------------------------------------------------------------
        protected override bool Champ_ModifiedIndex(string strNomTable, CInfoChampTable champ)
        {
            bool bModifier = base.Champ_ModifiedIndex(strNomTable, champ);
            if (bModifier && !champ.IsIndex)
            {
                //Si le champ fait partie d'une clé externe, c'est 
                //Normal qu'il y a ait un index dessus
                List<CInfoRelation> lstRels = new List<CInfoRelation>();
                GetRelationsExistantes(strNomTable, ref lstRels);
                foreach (CInfoRelation rel in lstRels)
                {
                    if (rel.ChampsFille.Length == 1 &&
                        rel.ChampsFille[0] == champ.NomChamp)
                        return false;
                }
            }
            return bModifier;

        }

        //--------------------------------------------------------------------------------------
        protected override bool ShouldDeleteIndexRelation(string strNomTable, CInfoRelation infoRelation)
        {
            return false;//Toutes les relations sont indexées dans ACCESS
        }
		

        //--------------------------------------------------------------------------------------
        protected override string GetNomIndex(string strNomTable, params string[] strChamps)
        {
            DataTable schema = GetOleDbSchemaIndexs();
            DataView view = new DataView(schema);
            string strFiltreIndexs = "";
            List<string> lstIndex = new List<string>();
            for (int nChamp = 0; nChamp < strChamps.Length; nChamp++)
            {
                lstIndex = new List<string>();
                view.RowFilter = "TABLE_NAME='" + strNomTable + "' and COLUMN_NAME='" + strChamps[nChamp] + "' and " +
                    "ORDINAL_POSITION=" + (nChamp + 1);
                if (strFiltreIndexs != "")
                    view.RowFilter += " and INDEX_NAME in (" + strFiltreIndexs + ")";
                strFiltreIndexs = "";
                foreach (DataRowView row in view)
                {
                    strFiltreIndexs += "'" + (string)row["INDEX_NAME"] + "',";
                    lstIndex.Add ( (string)row["INDEX_NAME"] );
                }
                if (strFiltreIndexs == "")
                    return "";
                strFiltreIndexs = strFiltreIndexs.Substring(0, strFiltreIndexs.Length - 1);
            }
            if (lstIndex.Count != 0)
                return lstIndex[0];
            return "";
        }
		//--------------------------------------------------------------------------------------
		public override bool IndexExists(string strNomTable, params string[] strChamps)
		{
			//Vérifie l'existance de l'index
			string strNomIndex = GetNomIndex(strNomTable, strChamps);
            return strNomIndex.Length > 0;
        }


        //--------------------------------------------------------------------------------------
        public override bool IsCluster(string strNomTable, params string[] strChamps)
        {
            return false;
        }

		#endregion

        public override CResultAErreur CreateChamp(string strNomTable, CInfoChampTable champ)
        {
            CResultAErreur result =  base.CreateChamp(strNomTable, champ);
            if (result && !champ.NullAuthorized)
            {
                result = SetValeursParDefautAuxDonneesNulles ( strNomTable, champ );
            }
            return result;
        }

        public override CResultAErreur UpdateChamp(string strNomTable, CInfoChampTable champ, bool bModifiedNotNull, bool bModifiedLength, bool bModifiedType)
        {
            CResultAErreur result = base.UpdateChamp(strNomTable, champ, bModifiedNotNull, bModifiedLength, bModifiedType);
            if (bModifiedNotNull && !champ.NullAuthorized)
            {
                result = SetValeursParDefautAuxDonneesNulles ( strNomTable, champ );
            }
            return result;
        }

        public CResultAErreur SetValeursParDefautAuxDonneesNulles(string strNomTable, CInfoChampTable champ)
        {
            if (!champ.NullAuthorized)
            {
                string strRequete = "Update " + strNomTable + " set " + champ.NomChamp + "=" +
                    GetStringValeurParDefaut(champ.TypeDonnee) + " where " + champ.NomChamp + " is null";
                return m_connection.RunStatement(strRequete);
            }
            return CResultAErreur.True;
        }

	}
}
