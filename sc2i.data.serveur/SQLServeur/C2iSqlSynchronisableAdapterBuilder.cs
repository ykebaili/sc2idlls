using System;
using System.Data;
using System.Data.SqlClient;

using sc2i.common;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de C2iSqlSynchronisableAdapterBuilderS.
	/// </summary>
	public class C2iSqlSynchronisableAdapterBuilder : C2iSqlAdapterBuilder
	{
	

		/// ///////////////////////////////////////////////////////////////////////////////////////////
		public C2iSqlSynchronisableAdapterBuilder( string strNomTable, CSqlDatabaseConnexion connexion)
			:base ( strNomTable, connexion )
		{
		}

		/// ///////////////////////////////////////////////////////////////////////////////////////////
		public override IDbCommand GetInsertCommand(DataTable table, IDbDataAdapter adapter, bool bDesactiverIdsAuto)
		{
			IDbCommand command = base.GetInsertCommand( table, adapter, bDesactiverIdsAuto );
			if ( !(m_connexion is CSqlDatabaseConnexionSynchronisable ))
				return command;
			//Si la table est synchronisable, on stock le nouvel id dans le log
			if ( table.Columns[CSc2iDataConst.c_champIdSynchro] != null )
			{
				DataColumn[] cols ;
				cols = table.PrimaryKey;
				if ( cols.Length == 1 && cols[0].AutoIncrement )
				{
					int nIdSync = ((CSqlDatabaseConnexionSynchronisable)m_connexion).IdSyncSession;
					string strReq = "Insert into "+
						CEntreeLogSynchronisation.c_nomTable+" ("+
						CEntreeLogSynchronisation.c_champTable+","+
						CEntreeLogSynchronisation.c_champIdElement+","+
						CEntreeLogSynchronisation.c_champType+","+
						CSc2iDataConst.c_champIdSynchro+") values ("+
						"'"+m_strNomTableInDb+"',"+
						"@@IDENTITY,"+
						((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd).ToString()+","+
						nIdSync.ToString()+")";
					command.CommandText += ";"+strReq;
				}
			}
			return command;
		}

		/// ///////////////////////////////////////////////////////////////////////////////////////////
		public override IDbCommand GetDeleteCommand ( DataTable table, IDbDataAdapter adapter )
		{
			
			IDbCommand command = base.GetDeleteCommand( table, adapter );
			if ( !(m_connexion is CSqlDatabaseConnexionSynchronisable ))
				return command;
			//Si la table est synchronisable, on stock le nouvel id dans le log
			if ( table.Columns[CSc2iDataConst.c_champIdSynchro] != null )
			{
				DataColumn[] cols ;
				cols = table.PrimaryKey;
				if ( cols.Length == 1 && cols[0].AutoIncrement )
				{
					//Trouve le paramètre correpondant à l'id
					string strParam = null;
					foreach ( SqlParameter parametre in command.Parameters )
						if ( parametre.SourceColumn == cols[0].ColumnName )
						{
							strParam = parametre.ParameterName;
							break;
						}
					if ( strParam == null )
						throw new Exception(I.T("Impossible to find the corresponding parameter to the id in the delete request|122"));
					int nIdSync = ((CSqlDatabaseConnexionSynchronisable)m_connexion).IdSyncSession;
					string strReq = "Insert into "+
						CEntreeLogSynchronisation.c_nomTable+" ("+
						CEntreeLogSynchronisation.c_champTable+","+
						CEntreeLogSynchronisation.c_champIdElement+","+
						CEntreeLogSynchronisation.c_champType+","+
						CSc2iDataConst.c_champIdSynchro+") values ("+
						"'"+m_strNomTableInDb+"',"+
						strParam+","+
						((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete).ToString()+","+
						nIdSync.ToString()+")";
					command.CommandText += ";"+strReq;
				}
			}
			return command;
		}
		
	}
}
