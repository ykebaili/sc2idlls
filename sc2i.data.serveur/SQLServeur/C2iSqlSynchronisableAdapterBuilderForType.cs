using System;
using System.Data.SqlClient;
using System.Data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de C2iSqlSynchronisableAdapterBuilderForType.
	/// </summary>
	public class C2iSqlSynchronisableAdapterBuilderForType : C2iSqlAdapterBuilderForType
	{
		/////////////////////////////////////////////////////////////////////
		public C2iSqlSynchronisableAdapterBuilderForType( Type leType, CSqlDatabaseConnexion connexion )
			:base( leType, connexion )
		{

		}

		/////////////////////////////////////////////////////////////////////
		public override IDbCommand GetInsertCommand ( CStructureTable structure, bool bDisableIdAuto, IDbDataAdapter adapter )
		{
			IDbCommand command = base.GetInsertCommand ( structure, bDisableIdAuto, adapter );
			if ( structure.IsSynchronisable)
			{
				int nIdSync = ((CSqlDatabaseConnexionSynchronisable)m_connexion).IdSyncSession;
				string strReq = "Insert into "+
					CEntreeLogSynchronisation.c_nomTable+" ("+
					CEntreeLogSynchronisation.c_champTable+","+
					CEntreeLogSynchronisation.c_champIdElement+","+
					CEntreeLogSynchronisation.c_champType+","+
					CSc2iDataConst.c_champIdSynchro+") values ("+
					"'"+structure.NomTable+"',"+
					"@@IDENTITY,"+
					((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd).ToString()+","+
					nIdSync.ToString()+")";
				command.CommandText += ";"+strReq;
			}
			return command;
		}

		/////////////////////////////////////////////////////////////////////
		public override IDbCommand GetDeleteCommand ( CStructureTable structure )
		{
			IDbCommand command = base.GetDeleteCommand ( structure );
			if ( structure.IsSynchronisable )
			{
				int nIdSync = ((CSqlDatabaseConnexionSynchronisable)m_connexion).IdSyncSession;
				string strReq = "Insert into "+
					CEntreeLogSynchronisation.c_nomTable+" ("+
					CEntreeLogSynchronisation.c_champTable+","+
					CEntreeLogSynchronisation.c_champIdElement+","+
					CEntreeLogSynchronisation.c_champType+","+
					CSc2iDataConst.c_champIdSynchro+") values ("+
					"'"+structure.NomTable+"',"+
					GetNomParametreFor(structure.ChampsId[0], DataRowVersion.Original)+","+
					((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete).ToString()+","+
					nIdSync.ToString()+")";
				command.CommandText += ";"+strReq;
			}
			return command;
		}
	}
}
