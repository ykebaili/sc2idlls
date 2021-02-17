using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un OleDbDataAdapter
	/// </summary>
    public class C2iOleDbAdapterBuilder : C2iDbAdapterBuilder
	{
		/// <summary>
		/// </summary>
		public C2iOleDbAdapterBuilder( string strNomTableInDb, COleDbDatabaseConnexion connexion )
			:base(  strNomTableInDb, connexion )
		{
		}


		////////////////////////////////////////////////////
		public override IDbCommand GetInsertCommand ( DataTable table, IDbDataAdapter adapter, bool bDesactiverIdsAuto )
		{
			string strNomTableInDb = m_strNomTableInDb;
			DataColumn[] cols = table.PrimaryKey;
			string strChampId = null;
			if ( cols != null && cols.Length == 1 )
			{
				if ( cols[0].AutoIncrement )
					strChampId = cols[0].ColumnName;
			}

			OleDbCommandBuilder builder = new OleDbCommandBuilder((OleDbDataAdapter)adapter);
			OleDbCommand cmdInsert = builder.GetInsertCommand();
			if ( m_connexion.IsInTrans() )
				cmdInsert.Transaction = (OleDbTransaction)m_connexion.Transaction;
			if ( strChampId != null )
			{
				string strReq = cmdInsert.CommandText;
				strReq +=";SELECT "+strChampId +
					" FROM " + strNomTableInDb + " WHERE (" + strChampId + " = @@IDENTITY)";
				cmdInsert.CommandText = strReq;
				cmdInsert.UpdatedRowSource = UpdateRowSource.Both;
			}

			return cmdInsert;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetUpdateCommand(DataTable table, IDbDataAdapter adapter)
		{
			OleDbCommandBuilder builder = new OleDbCommandBuilder((OleDbDataAdapter)adapter);
			OleDbCommand command = builder.GetUpdateCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (OleDbTransaction)m_connexion.Transaction;
			return command;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetDeleteCommand(DataTable table, IDbDataAdapter adapter)
		{
			OleDbCommandBuilder builder = new OleDbCommandBuilder((OleDbDataAdapter)adapter);
			OleDbCommand command = builder.GetDeleteCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (OleDbTransaction)m_connexion.Transaction;
			return command;
		}
	}
}
