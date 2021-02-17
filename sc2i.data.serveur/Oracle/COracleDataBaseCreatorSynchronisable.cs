using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using sc2i.common;
using sc2i.multitiers.server;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

using sc2i.data;

namespace sc2i.data.serveur
{
	public class COracleDataBaseCreatorSynchronisable : COracleDataBaseCreator
	{
		//------------------------------------------------------------------------------------------------
		public COracleDataBaseCreatorSynchronisable(COracleDatabaseConnexionSynchronisable connexion)
			:base(connexion)
		{
		}

		public override CResultAErreur InitialiserDataBase()
		{
			CResultAErreur result = base.InitialiserDataBase();

			//Creation de la table de synchronisation
			if (result)
				DeleteTable("SC2I_SYNC_LOG");

			//Creation de la table
			if (result)
				result = Connection.RunStatement(
					"Create table SC2I_SYNC_LOG (" +
					"SSL_ID " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(Int32)) + " NOT NULL, " +
					"SSL_TABLE " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(String)) + "(255) not null," +
					"SSL_ELT_ID " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(Int32)) + " not null," +
					"SSL_TYPE " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(Int32)) + " not null," +
					CSc2iDataConst.c_champIdSynchro + " " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(Int32)) + " not null)");

			//Creation des sequences de numérotation
			if (result)
				result = CreerSequencesNumerotationTable("SC2I_SYNC_LOG");

			//Clef Primaire
			if (result)
				result = Connection.RunStatement(GetRequeteCreateClefPrimaire("SC2I_SYNC_LOG", "SSL_ID"));

			//Création de l'index pour synchro
			if (result)
				result = CreateIndex("SC2I_SYNC_LOG", false, "SC2I_SYNC_SESSION");

			//Création d'un champ auto incrémenté
			if (result)
				result = CreerSystemeAutoIncremente("SC2I_SYNC_LOG", "SSL_ID");

			//if (result && FinCreationOuMAJTable != null)
			//    FinCreationOuMAJTable(this, new ArgumentsEvenementOperationTable("SC2I_SYNC_LOG"));

			return result;
		}

		public override int NbTableInitialisation
		{
			get { return base.NbTableInitialisation + 1; }
		}
	}
}
