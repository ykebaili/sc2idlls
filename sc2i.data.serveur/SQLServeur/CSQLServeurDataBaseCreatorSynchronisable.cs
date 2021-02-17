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
	public class CSQLServeurDataBaseCreatorSynchronisable : CSQLServeurDataBaseCreator
	{
		//------------------------------------------------------------------------------------------------
		public CSQLServeurDataBaseCreatorSynchronisable(CSqlDatabaseConnexionSynchronisable connexion)
			:base(connexion)
		{
		}

		public override CResultAErreur InitialiserDataBase()
		{
			CResultAErreur result = base.InitialiserDataBase();

			//Creation de la table de synchronisation
			if (result)
				result =
					Connection.RunStatement(
					"Create table SC2I_SYNC_LOG (" +
					"SSL_TABLE nvarchar(255) not null," +
					"SSL_ELT_ID int not null," +
					"SSL_TYPE int not null," +
					CSc2iDataConst.c_champIdSynchro + " int not null)");

			//Création de l'index pour synchro
			if (result)
				result = Connection.RunStatement(
					@"CREATE NONCLUSTERED INDEX IX_SC2I_SYNC_LOG ON SC2I_SYNC_LOG
					(
						SC2I_SYNC_SESSION
					) ");

			//Champ Auto Incremente
			if (result)
				result =
					Connection.RunStatement(
					@"Alter table SC2I_SYNC_LOG 
					Add SSL_ID int NOT NULL IDENTITY (1, 1)");

			//Cle primaire
			if (result)
				result = Connection.RunStatement(
					@"ALTER TABLE SC2I_SYNC_LOG ADD CONSTRAINT
					PK_SC2I_SYNC_LOG PRIMARY KEY NONCLUSTERED 
					(
						SSL_ID
					)");

			return result;
		}
		public override int NbTableInitialisation
		{
			get { return base.NbTableInitialisation + 1; }
		}
	}
}
