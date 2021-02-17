using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un OracleDataAdapter
	/// </summary>
    public class C2iOracleAdapterBuilder : C2iDbAdapterBuilder
	{
		/// <summary>
		/// </summary>
		public C2iOracleAdapterBuilder( string strNomTableInDb, COracleDatabaseConnexion connexion )
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

			OracleCommandBuilder builder = new OracleCommandBuilder((OracleDataAdapter)adapter);
			bool bAvecTriggerMajAuto = true;
			COracleDatabaseConnexion conOracle = m_connexion as COracleDatabaseConnexion;
			if ( conOracle != null )
			{
				if ( strChampId != null )
				{
					conOracle.GetNomSequenceColAuto ( table.TableName, strChampId, ref bAvecTriggerMajAuto );
				}
			}
			if ( strChampId != null && !bAvecTriggerMajAuto )
				table.Columns[strChampId].AutoIncrement = false;

			OracleCommand cmdInsert = builder.GetInsertCommand();

			if (strChampId != null && !bAvecTriggerMajAuto)
				table.Columns[strChampId].AutoIncrement = true;
			if ( m_connexion.IsInTrans() )
				cmdInsert.Transaction = (OracleTransaction)m_connexion.Transaction;
			if ( strChampId != null )
			{
				C2iOracleDataAdapter oracleAdapter = C2iOracleDataAdapter.GetOracleDataAdapter(adapter);
				if (oracleAdapter != null && !bDisableIdsAuto)
					oracleAdapter.PreparerInsertionLigneAvecAutoID(table.TableName, strChampId);

				cmdInsert.UpdatedRowSource = UpdateRowSource.Both;
			}

			return cmdInsert;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetUpdateCommand(DataTable table, IDbDataAdapter adapter)
		{
			OracleCommandBuilder builder = new OracleCommandBuilder((OracleDataAdapter)adapter);
			OracleCommand command = builder.GetUpdateCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (OracleTransaction)m_connexion.Transaction;
			return command;
		}

		////////////////////////////////////////////////////
		public override IDbCommand GetDeleteCommand(DataTable table, IDbDataAdapter adapter)
		{
			OracleCommandBuilder builder = new OracleCommandBuilder((OracleDataAdapter)adapter);
			OracleCommand command = builder.GetDeleteCommand();
			if ( m_connexion.IsInTrans() )
				command.Transaction = (OracleTransaction)m_connexion.Transaction;
			return command;
		}
	}
}
