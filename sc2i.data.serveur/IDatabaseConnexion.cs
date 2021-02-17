using System;
using System.Data;
using System.Data.Common;

using sc2i.data;
using sc2i.common;
using sc2i.data.serveur;
using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de IDatabaseConnexion.
	/// </summary>
	public delegate CResultAErreur OnCommitTransEventHandler ( );

    

	public interface IDatabaseConnexion :  IDisposable, IServiceTransactions, I2iMarshalObjectDeSession
	{
		///Chaine de connexion
		string ConnexionString {get;set;}

		event OnCommitTransEventHandler OnCommitTrans;

        //Retourne true si la connexion est en transaction
        bool IsInTrans();

        string PrefixeTables { get;set;}


		/// <summary>
		/// Retourne l'instance (null non disponible)
		/// permettant d'éditer la structure de la base de donnée
		/// </summary>
		/// <returns></returns>
		IDataBaseCreator GetDataBaseCreator ();



		/// <summary>
		/// Retourne un IDataAdapter pour la lecture seulement
		/// </summary>
		/// <param name="strRequete">
		/// Requete SQL
		/// </param>
		/// <returns></returns>
		IDataAdapter GetSimpleReadAdapter ( string strRequete );

		/// <summary>
		/// Retourne un IDataAdapter pour la lecture seulement avec un filtre
		/// </summary>
		/// <param name="strRequete">Requête SQL</param>
		/// <param name="filtre">Filtre à appliquer</param>
		/// <returns></returns>
		IDataAdapter GetSimpleReadAdapter ( string strRequete, CFiltreData filtre );

		/// <summary>
		/// Retourne un dataadapter pour la requete passée.
		/// </summary>
		/// <remarks>
		/// Contrairement au GetSimpleReadAdapter, la requête peut être
		/// une requête complexe Sql. <BR></BR>
		/// le second paramètre contient les valeurs des parametres.
		/// Les paramètres sont inclus dans la requête avec le fonction GetNomParametre(n)
		/// où n indique le numéro du paramètre
		/// </remarks>
		/// <param name="strRequete"></param>
		/// <param name="parametres"></param>
		/// <returns></returns>
		IDataAdapter GetAdapterForRequete ( string strRequete, params object[] parametres );


		int CommandTimeOut{get;set;}

		/// <summary>
		/// Retourne un IDataAdapter pour lire une table complète
		/// </summary>
		/// <param name="strTable">Nom de la table à lire</param>
		/// <returns></returns>
		IDataAdapter GetTableAdapter ( string strNomTableInDb );

		/// <summary>
		/// Retourne un IDataAdapter pour mettre à jour une table
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		IDataAdapter GetNewAdapterForUpdate ( string strNomTableInDb );

		/// <summary>
		/// Retourne un IDataAdapter pour un type possédant les attributs [Table][Relation] et [Champ]
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		IDataAdapter GetNewAdapterForType ( Type tp, DataRowState etatsAPrendreEnCompte,bool bDisableIdAuto, params string[] champsExclus );


		/// <summary>
		/// Execute une requete ne renvoyant pas de ligne
		/// A utiliser uniquement pour modifier la structure de la base
		/// </summary>
		CResultAErreur RunStatement ( String strStatement);

		/// <summary>
		/// Execute une requete renvoyant la premiere colonne de la
		/// premiere ligne du jeu de résultat
		/// Utiliser pour les requetes retournant une seule valeur
		/// </summary>
		CResultAErreur ExecuteScalar( string strStatement );

		/// <summary>
		/// Retourne le nombre d'enregistrements correspondants au filtre
		/// </summary>
		int CountRecords( string strNomTableInDb, CFiltreData filtre );

		/// <summary>
		/// Retourne dans data le resultdata de la requête scalar
		/// </summary>
		CResultAErreur ExecuteScalar ( string strSelectClause, string strNomTableInDb, CFiltreData filtre );

		/// <summary>
		/// Retourne dans le data du result le résultat de la requête
		/// </summary>
		/// <param name="champs"></param>
		/// <param name="arbreTables"></param>
		/// <param name="filtre"></param>
		/// <returns></returns>
		CResultAErreur ExecuteRequeteComplexe ( C2iChampDeRequete[] champs, CArbreTable arbreTables, CFiltreData filtre );

		//Retourne une chaine contenant la valeur passée en paramètre acceptable par une requête
		string GetStringForRequete ( object obj );

		/// <summary>
		/// Retourne le numéro auto maxi trouvé dans la table
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		//int GetMaxIdentity ( string strTable );

		/// <summary>
		/// Modifie une table avant l'écriture (modif des num synchro par exemple)
		/// </summary>
		/// <param name="table"></param>
		void PrepareTableToWriteDatabase ( DataTable tableInContexte );

		/// <summary>
		/// Active ou désactive les identifiants automatiques
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="bDesactiver"></param>
		void DesactiverIdAuto ( string strNomTableInDb, bool bDesactiver );

		/// <summary>
		/// Active ou désactive les contraintes d'intégrité référentielle
		/// A UTILISER AVEC PRECAUTION
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="bDesactiver"></param>
		void DesactiverContraintes ( bool bDesactiver );


		/// <summary>
		/// Retourne true si le système arrive à se connecter à la base demandée
		/// </summary>
		/// <returns></returns>
		CResultAErreur IsConnexionValide();


		/////////////////////////////////////////////////////////
		/// <summary>
		/// Charge un blob à partir d'une ligne d'une table. Le resultat est 
		/// placé dans le Data du CResultAErreur
		/// </summary>
		CResultAErreur ReadBlob ( string strNomTableInDb, string strChamp, CFiltreData filtre );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Sauve un blob dans une ligne d'une base de données
		/// </summary>
		CResultAErreur SaveBlob ( string strNomTableInDb, string strChamp, CFiltreData filtre, byte[] data );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne les noms des tables de la base de données
		/// </summary>
		string[] TablesNames{get;}

		string[] GetColonnesDeTable(string strTableInDb);
		

		/////////////////////////////////////////////////////////
		CResultAErreur SetValeurChamp  (string strNomTableInDb, string[] strChamps, object[] valeur, CFiltreData filtre );

		/////////////////////////////////////////////////////////
		string GetNomParametre ( string strNomParametre );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Entoure le nom de [ ou autre flonsflons pour que le nom de la table
		/// soit accepté dans une requête SQL
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <returns></returns>
		string GetNomTableForRequete ( string strNomTableInDb );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <param name="?"></param>
        void FillAdapter(IDataAdapter adapter, DataSet ds);
        void FillAdapter(DbDataAdapter adapter, DataTable dt);
        void FillAdapter(DbDataAdapter adapter, DataSet ds, int startRecord, int maxRecords, string srcTable);
        void FillAdapter(IDataAdapterARemplissagePartiel adapter, DataSet ds, int startRecord, int maxRecords, string srcTable);
	}
}
