using System;
using System.Data;
using System.Data.MySqlClient;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un MySqlDataAdapter
	/// </summary>
    public class C2iMySqlAdapterBuilder : C2iDbAdapterBuilder
	{
		/// <summary>
		/// </summary>
		public C2iMySqlAdapterBuilder( string strNomTableInDb, CMySqlDatabaseConnexion connexion )
			:base(  strNomTableInDb, connexion )
		{
		}


		////////////////////////////////////////////////////
		public override IDbCommand GetInsertCommand ( DataTable table, IDbDataAdapter adapter, bool bDisableIdsAuto )
		{
			DataColumn[] cols = table.PrimaryKey;
			string strChampId = null;

			if ( cols != null && cols.Length == 1 )
				if ( cols[0].AutoIncrement )
					strChampId = cols[0].ColumnName;

			MySqlCommandBuilder builder = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
			bool bAvecTriggerMajAuto = true;
			CMySqlDatabaseConnexion conMySql = m_connexion as CMySqlDatabaseConnexion;
			if ( conMySql != null )
			{
				if ( strChampId != null )
				{
					conMySql.GetNomSequenceColAuto ( table.TableName, strChampId, ref bAvecTriggerMajAuto );
				}
			}
			if ( strChampId != null && !bAvecTriggerMajAuto )
				table.Columns[strChampId].AutoIncrement = false;

			MySqlCommand cmdInsert = builder.GetInsertCommand();

			if (strChampId != null && !bAvecTriggerMajAuto)
				table.Columns[strChampId].AutoIncrement = true;
			if ( m_connexion.IsInTrans() )
				cmdInsert.Transaction = (MySqlTransaction)m_connexion.Transaction;
			if ( strChampId != null )
			{
				C2iMySqlDataAdapter MySqlAdapter = C2iMySqlDataAdapter.GetMySqlDataAdapter(adapter);
				if (MySqlAdapter != null && !bDisableIdsAuto)
					MySqlAdapter.PreparerInsertionLigneAvecAutoID(table.TableName, strChampId);

				cmdInsert.UpdatedRowSource = UpdateRowSource.Both;
			}

			return cmdInsert;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetUpdateCommand(DataTable table, IDbDataAdapter adapter)
		{
			MySqlCommandBuilder builder = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
			MySqlCommand command = builder.GetUpdateCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (MySqlTransaction)m_connexion.Transaction;
			return command;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetDeleteCommand(DataTable table, IDbDataAdapter adapter)
		{
			MySqlCommandBuilder builder = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
			MySqlCommand command = builder.GetDeleteCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (MySqlTransaction)m_connexion.Transaction;
			return command;
		}
	}
}
