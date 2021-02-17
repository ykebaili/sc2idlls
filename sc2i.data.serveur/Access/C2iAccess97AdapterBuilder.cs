using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data.serveur.Access;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un OleDbDataAdapter
	/// </summary>
    public class CAccess97AdapterBuilder : C2iOleDbAdapterBuilder
	{
        private bool m_bAvecSynchro = false;

		/// <summary>
		/// </summary>
		public CAccess97AdapterBuilder( string strNomTableInDb, COleDbDatabaseConnexion connexion )
			:base(  strNomTableInDb, connexion )
		{
            m_bAvecSynchro = connexion is CAccess97DataBaseConnexionSynchronisable;
            if (m_bAvecSynchro)
                m_bAvecSynchro &= ((IDatabaseConnexionSynchronisable)connexion).EnableLogSynchronisation;
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
            

			OleDbCommandBuilder builder = new OleDbCommandBuilder((OleDbDataAdapter)adapter);
			OleDbCommand cmdInsert = builder.GetInsertCommand();
			if ( m_connexion.IsInTrans() )
				cmdInsert.Transaction = (OleDbTransaction)m_connexion.Transaction;
			if ( strChampId != null )
			{
				((OleDbDataAdapter)adapter).RowUpdated +=new OleDbRowUpdatedEventHandler(OnRowAIdAutoUpdated);
			}
            

			return cmdInsert;
		}

		////////////////////////////////////////////////////
		private void OnRowAIdAutoUpdated(object sender, OleDbRowUpdatedEventArgs e)
		{
			string strNomTableInDb = m_strNomTableInDb;

			string strId = e.Row.Table.PrimaryKey[0].ColumnName;
			string strRequete = "select max(["+strId+"]) from ["+strNomTableInDb+"]";
			CResultAErreur result = m_connexion.ExecuteScalar ( strRequete );
			if ( !result )
				throw new Exception(I.T("Access97 Recovery IdAuto Error|101"));
			e.Row[strId] = (int)result.Data;
            if (m_bAvecSynchro && e.Row.Table.Columns.Contains ( CSc2iDataConst.c_champIdSynchro ))
            {
                ((CAccess97DataBaseConnexionSynchronisable)m_connexion).RenseignerPourSynchro(e.Row, e.StatementType);
            }
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
            if ( table.Columns.Contains ( CSc2iDataConst.c_champIdSynchro ) )
                ((OleDbDataAdapter)adapter).RowUpdated += new OleDbRowUpdatedEventHandler(OnRowDeleted);
			return command;
		}

        ////////////////////////////////////////////////////
        void OnRowDeleted(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (m_bAvecSynchro)
            {
                ((CAccess97DataBaseConnexionSynchronisable)m_connexion).RenseignerPourSynchro(e.Row, e.StatementType);
            }
        }

        		
	}
}
