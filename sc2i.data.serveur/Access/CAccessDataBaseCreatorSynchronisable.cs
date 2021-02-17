using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data.serveur.Access
{
    public class CAccessDataBaseCreatorSynchronisable : CAccessDataBaseCreator
    {
        //------------------------------------------------------------------------------------------------
        public CAccessDataBaseCreatorSynchronisable(CAccess97DataBaseConnexionSynchronisable connexion)
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
					@"CREATE INDEX IX_SC2I_SYNC_LOG ON SC2I_SYNC_LOG
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
