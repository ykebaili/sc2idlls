using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un SqlDataAdapter
	/// </summary>
    public class C2iSqlAdapterBuilder : C2iDbAdapterBuilder
	{
		/// <summary>
		/// </summary>
		public C2iSqlAdapterBuilder( string strNomTableInDb, CSqlDatabaseConnexion connexion )
			:base(  strNomTableInDb, connexion )
		{
		}


		////////////////////////////////////////////////////
		public override IDbCommand GetInsertCommand(DataTable table, IDbDataAdapter adapter, bool bDesactiverIdsAuto)
		{
			DataColumn[] cols = table.PrimaryKey;
			string strChampId = null;
			if ( cols != null && cols.Length == 1 )
			{
				if ( cols[0].AutoIncrement )
					strChampId = cols[0].ColumnName;
			}

			SqlCommandBuilder builder = new SqlCommandBuilder((SqlDataAdapter)adapter);
			SqlCommand cmdInsert = builder.GetInsertCommand();
			if ( m_connexion.IsInTrans() )
				cmdInsert.Transaction = (SqlTransaction)m_connexion.Transaction;
			if ( strChampId != null )
			{
				string strReq = cmdInsert.CommandText;
				strReq +=";SELECT "+strChampId +
					" FROM "+m_strNomTableInDb+" WHERE ("+strChampId+" = @@IDENTITY)";
				cmdInsert.CommandText = strReq;
				cmdInsert.UpdatedRowSource = UpdateRowSource.Both;
			}

			return cmdInsert;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetUpdateCommand(DataTable table, IDbDataAdapter adapter)
		{
			SqlCommandBuilder builder = new SqlCommandBuilder((SqlDataAdapter)adapter);
			SqlCommand command = builder.GetUpdateCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (SqlTransaction)m_connexion.Transaction;
			return command;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetDeleteCommand(DataTable table, IDbDataAdapter adapter)
		{
			SqlCommandBuilder builder = new SqlCommandBuilder((SqlDataAdapter)adapter);
			SqlCommand command = builder.GetDeleteCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (SqlTransaction)m_connexion.Transaction;
			return command;
		}
	}
}
