using System;
using System.Collections;
using System.Text;

using sc2i.common;

namespace sc2i.data.serveur
{
	public interface IDataBaseCreator
	{

		IDatabaseConnexion Connection { get; }
		IDataBaseTypeMapper DataBaseTypesMappeur { get; }
		int NbTableInitialisation { get; }


		CResultAErreur InitialiserDataBase();
		CResultAErreur CreateDatabase();

		bool TableExists(string strNomTableInDb);
        bool ChampExists(string strTableName, string strChampName);

		/// <summary>
		/// Retourne la déclaration de la valeur ou la méthode
		/// devant être renseigner dans la création d'une colonne
		/// ayant une valeur par défaut (colonne not null)
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		string GetDeclarationDefaultValueForType(Type tp);

		//Creation de la table
		CResultAErreur CreateTable(CStructureTable structure);
		CResultAErreur CreationOuUpdateTableFromType(Type tp);
		CResultAErreur CreationOuUpdateTableFromType(Type tp, ArrayList lstChampsAForcerANull);

		CResultAErreur DeleteTable(string strNomTable);
		CResultAErreur UpdateTable(CStructureTable structure);
        CResultAErreur DeleteChamp(string strNomTable, string strNomChamp);

		/// <summary>
		/// Vérifie et met à jour si besoin la table de registre
		/// </summary>
		/// <returns></returns>
		CResultAErreur UpdateStructureTableRegistre();


	}
}
