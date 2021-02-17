using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Threading;
using System.Text;
using System.IO;

using sc2i.common;
using sc2i.data;
using System.Reflection;
using sc2i.multitiers.server;
using sc2i.multitiers.client;



namespace sc2i.data.serveur
{
	/// <summary>
	/// Retour de fonction contenant un dataset / une erreur
	/// </summary>

	/////////////////////////////////////////////////////////////////////////////////
	//Encapsule les accès à la base de données
	public class CAccess97DatabaseConnexion : COleDbDatabaseConnexion, IDatabaseConnexion, IObjetAttacheASession
	{

		//////////////////////////////////////////////////////////////////
		public CAccess97DatabaseConnexion( int nIdSession )
			:base ( nIdSession )
		{
		}

        /////////////////////////////////////////////////////////
        public override IDataBaseCreator GetDataBaseCreator()
        {
            return new CAccessDataBaseCreator(this);
        }


		/////////////////////////////////////////////////////////
		protected override IDbConnection GetNewConnexion ( bool bOpenIfClose )
		{
			OleDbConnection connexion = new OleDbConnection(ConnexionString);

			if ( bOpenIfClose )
			{
				switch ( connexion.State )
				{
					case ConnectionState.Broken :
						connexion = null;
						return GetConnexion(bOpenIfClose);
					case ConnectionState.Closed :
						connexion.Open();
						break;
				}
			}
			return connexion;
		}

        /////////////////////////////////////////////////////////////////////////////////////
        public override void CreateJoinPourLiens(
            CFiltreData filtre,
            CArbreTable arbreTables,
            CComposantFiltre composantFiltre,
            ref bool bDistinct,
            ref string strJoin,
            ref string strWhere,
            ref string strPrefixeFrom)
        {
            strJoin = "";
            arbreTables.SortTablesLiees();
            foreach (CArbreTableFille arbreFils in arbreTables.TablesLiees)
            {
                strPrefixeFrom += "(";
                if (arbreFils.IsLeftOuter)
                    strJoin += " LEFT OUTER JOIN ";
                else
                    strJoin += " INNER JOIN ";
                if (arbreFils.Relation.IsRelationFille)
                    bDistinct = true;
                string strTable;
                strTable = CContexteDonnee.GetNomTableInDbForNomTable(arbreFils.NomTable) +
                    GetSqlForAliasDecl(arbreFils.Alias);
                string strSuiteFrom = "";
                string strSuiteWhere = "";
                string strPrefixe = "";
                CreateJoinPourLiens(filtre, arbreFils, composantFiltre, ref bDistinct, ref strSuiteFrom, ref strSuiteWhere, ref strPrefixe);
                strJoin += strPrefixe;
                if (strSuiteFrom.Trim() != "")
                    strTable = "(" + strTable;
                strTable += strSuiteFrom;
                if (strSuiteFrom.Trim() != "")
                    strTable += ")";
                strJoin += strTable;
                strJoin += " ON (";
                CInfoRelationComposantFiltre relation = arbreFils.Relation;
                string strAliasParent, strAliasFille;
                string strTableDependante = "";
                if (relation.IsRelationFille)
                {
                    strAliasParent = arbreTables.Alias;
                    strAliasFille = arbreFils.Alias;
                    strTableDependante = arbreFils.NomTable;
                }
                else
                {
                    strAliasParent = arbreFils.Alias;
                    strAliasFille = arbreTables.Alias;
                    strTableDependante = arbreTables.NomTable;
                }
                string strJointure = relation.GetJoinClause(strAliasParent, "", strAliasFille, "");

                string strComplementVersion = "";
                if (EstCeQueLaTableGereLesVersions(strTableDependante) && !filtre.IntegrerLesElementsSupprimes)
                {
                    strComplementVersion = "(" + strAliasFille + "." + CSc2iDataConst.c_champIsDeleted + "=0 or " +
                        strAliasFille + "." + CSc2iDataConst.c_champIsDeleted + " is null)";
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
                            strAliasFille + "." + CSc2iDataConst.c_champIdVersion + " is null)";
                }
                if (strComplementVersion != "")
                {
                    if (strWhere != "")
                        strWhere += " and (" + strComplementVersion + ") and (";
                    else
                        strWhere += "(";
                    strWhere += strComplementVersion + ")";
                }
                strJoin += strJointure + "))";
                if (composantFiltre != null)
                    composantFiltre.DefinitAlias(arbreFils.CheminRelations, arbreFils.Alias);
                if (filtre is CFiltreDataAvance)
                {
                    foreach (CComposantFiltreChamp champ in ((CFiltreDataAvance)filtre).ChampsAAjouterAArbreTable)
                        champ.DefinitAlias(arbreFils.CheminRelations, arbreFils.Alias);
                }
            }
        }


		////////////////////////////////////////////////////////
		public override int GetMaxIdentity ( string strNomTableInDb )
		{
			string strSql = "select max(IDENTITYCOL) from "+GetNomTableForRequete(strNomTableInDb);
			IDbConnection con = GetConnexion (true);
			IDbCommand command = con.CreateCommand();
			command.CommandType = CommandType.Text;
			if (IsInTrans())
				command.Transaction = Transaction;
			command.CommandText = strSql;
			object obj = command.ExecuteScalar();
			int nVal = obj==DBNull.Value?0:(int)obj;
			if ( !IsInTrans() )
				con.Close();
            command.Dispose();
			return nVal;
		}

		/// /////////////////////////////////////////////////////
		protected override IDbDataAdapter GetNewAdapter( IDbCommand selectCommand )
		{
			return new OleDbDataAdapter ( (OleDbCommand)selectCommand );
		}

		/// /////////////////////////////////////////////////////
		public override string GetNomParametre(string strNomParametre )
		{
			return "@"+strNomParametre;
		}
		
		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder(string strNomTableInDb)
		{
			return new CAccess97AdapterBuilder ( strNomTableInDb, this );
		}

		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder ( Type tp )
		{
			return new CAccess97AdapterBuilderForType ( tp, this );
		}

        /////////////////////////////////////////////////////////
        ///règle le problème des colonnes non null qui peuvent avoir
        /////des valeurs nulles (mauvais contrôle de la part de ACCESS)
        private void CorrigeErreursDbNull(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                CorrigeErreursDbNull(table);
            }
        }

        private void CorrigeErreursDbNull(DataTable table)
        {
            bool bHasChange = false;
            foreach (DataColumn col in table.Columns)
            {
                if (!col.AllowDBNull)
                {
                    DataRow[] rows = table.Select(col.ColumnName + " is null");
                    if (rows.Length > 0)
                    {
                        object defVal = null;
                        if (col.DataType == typeof(string))
                            defVal = "";
                        if (col.DataType == typeof(int))
                            defVal = 0;
                        if (col.DataType == typeof(double))
                            defVal = 0.0;
                        if (col.DataType == typeof(bool))
                            defVal = false;
                        if (defVal != null)
                        {
                            foreach (DataRow row in rows)
                            {
                                bHasChange = true;
                                row[col] = defVal;
                            }
                        }
                    }
                }
            }
            if ( bHasChange )
                table.AcceptChanges();

        }

        /////////////////////////////////////////////////////////
        public override void FillAdapter(IDataAdapter adapter, DataSet ds)
        {
            ds.EnforceConstraints = false;
            int nNbTry = 3;
            while (nNbTry > 0)
            {
                try
                {
                    base.FillAdapter(adapter, ds);
                    break;
                }
                catch (Exception e)
                {
                    nNbTry--;
                    if (nNbTry == 0)
                        throw e;
                    GC.Collect();
                    System.Threading.Thread.Sleep(100);
                }
            }
            CorrigeErreursDbNull(ds);
            ds.EnforceConstraints = true;
        }

        public override void FillAdapter(IDataAdapterARemplissagePartiel adapter, DataSet ds, int startRecord, int maxRecords, string srcTable)
        {
            ds.EnforceConstraints = false;
            int nNbTry = 3;
            while (nNbTry > 0)
            {
                try
                {
                    base.FillAdapter(adapter, ds, startRecord, maxRecords, srcTable);
                    break;
                }
                catch (Exception e)
                {
                    nNbTry--;
                    if (nNbTry == 0)
                        throw e;
                    GC.Collect();
                    System.Threading.Thread.Sleep(100);
                }
            }
            CorrigeErreursDbNull(ds);
            ds.EnforceConstraints = true;
        }

        public override void FillAdapter(System.Data.Common.DbDataAdapter adapter, DataSet ds, int startRecord, int maxRecords, string srcTable)
        {
            ds.EnforceConstraints = false;
            int nNbTry = 3;
            while (nNbTry > 0)
            {
                try
                {
                    base.FillAdapter(adapter, ds, startRecord, maxRecords, srcTable);
                    break;
                }
                catch (Exception e)
                {
                    nNbTry--;
                    if (nNbTry == 0)
                        throw e;
                    GC.Collect();
                    System.Threading.Thread.Sleep(100);
                }
            }
            CorrigeErreursDbNull(ds);
            ds.EnforceConstraints = true;
        }

        public override void FillAdapter(System.Data.Common.DbDataAdapter adapter, DataTable dt)
        {
            if ( dt.DataSet != null )
                dt.DataSet.EnforceConstraints = false;
            int nNbTry = 3;
            while (nNbTry > 0)
            {
                try
                {
                    base.FillAdapter(adapter, dt);
                    break;
                }
                catch (Exception e)
                {
                    nNbTry--;
                    if (nNbTry == 0)
                        throw e;
                    GC.Collect();
                    System.Threading.Thread.Sleep(100);
                }
            }
            CorrigeErreursDbNull(dt);
            if (dt.DataSet != null)
                dt.DataSet.EnforceConstraints = true;
        }

        //------------------------------------------------------------------------------------
        public override CResultAErreur ReadBlob(string strNomTableInDb, string strChamp, CFiltreData filtre)
        {
            int nNbTry = 3;
            CResultAErreur result = CResultAErreur.True;
            while (nNbTry > 0)
            {
                try
                {
                    result = base.ReadBlob(strNomTableInDb, strChamp, filtre);
                    return result;
                }
                catch ( Exception e )
                {
                    nNbTry--;
                    GC.Collect();
                    System.Threading.Thread.Sleep(150);
                    result.EmpileErreur(new CErreurException(e));
                }
            }
            return result;
        }

	

				

	}
}
