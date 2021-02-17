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
	/// Description r�sum�e de IDatabaseConnexion.
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
		/// permettant d'�diter la structure de la base de donn�e
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
		/// <param name="strRequete">Requ�te SQL</param>
		/// <param name="filtre">Filtre � appliquer</param>
		/// <returns></returns>
		IDataAdapter GetSimpleReadAdapter ( string strRequete, CFiltreData filtre );

		/// <summary>
		/// Retourne un dataadapter pour la requete pass�e.
		/// </summary>
		/// <remarks>
		/// Contrairement au GetSimpleReadAdapter, la requ�te peut �tre
		/// une requ�te complexe Sql. <BR></BR>
		/// le second param�tre contient les valeurs des parametres.
		/// Les param�tres sont inclus dans la requ�te avec le fonction GetNomParametre(n)
		/// o� n indique le num�ro du param�tre
		/// </remarks>
		/// <param name="strRequete"></param>
		/// <param name="parametres"></param>
		/// <returns></returns>
		IDataAdapter GetAdapterForRequete ( string strRequete, params object[] parametres );


		int CommandTimeOut{get;set;}

		/// <summary>
		/// Retourne un IDataAdapter pour lire une table compl�te
		/// </summary>
		/// <param name="strTable">Nom de la table � lire</param>
		/// <returns></returns>
		IDataAdapter GetTableAdapter ( string strNomTableInDb );

		/// <summary>
		/// Retourne un IDataAdapter pour mettre � jour une table
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		IDataAdapter GetNewAdapterForUpdate ( string strNomTableInDb );

		/// <summary>
		/// Retourne un IDataAdapter pour un type poss�dant les attributs [Table][Relation] et [Champ]
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
		/// premiere ligne du jeu de r�sultat
		/// Utiliser pour les requetes retournant une seule valeur
		/// </summary>
		CResultAErreur ExecuteScalar( string strStatement );

		/// <summary>
		/// Retourne le nombre d'enregistrements correspondants au filtre
		/// </summary>
		int CountRecords( string strNomTableInDb, CFiltreData filtre );

		/// <summary>
		/// Retourne dans data le resultdata de la requ�te scalar
		/// </summary>
		CResultAErreur ExecuteScalar ( string strSelectClause, string strNomTableInDb, CFiltreData filtre );

		/// <summary>
		/// Retourne dans le data du result le r�sultat de la requ�te
		/// </summary>
		/// <param name="champs"></param>
		/// <param name="arbreTables"></param>
		/// <param name="filtre"></param>
		/// <returns></returns>
		CResultAErreur ExecuteRequeteComplexe ( C2iChampDeRequete[] champs, CArbreTable arbreTables, CFiltreData filtre );

		//Retourne une chaine contenant la valeur pass�e en param�tre acceptable par une requ�te
		string GetStringForRequete ( object obj );

		/// <summary>
		/// Retourne le num�ro auto maxi trouv� dans la table
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		//int GetMaxIdentity ( string strTable );

		/// <summary>
		/// Modifie une table avant l'�criture (modif des num synchro par exemple)
		/// </summary>
		/// <param name="table"></param>
		void PrepareTableToWriteDatabase ( DataTable tableInContexte );

		/// <summary>
		/// Active ou d�sactive les identifiants automatiques
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="bDesactiver"></param>
		void DesactiverIdAuto ( string strNomTableInDb, bool bDesactiver );

		/// <summary>
		/// Active ou d�sactive les contraintes d'int�grit� r�f�rentielle
		/// A UTILISER AVEC PRECAUTION
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="bDesactiver"></param>
		void DesactiverContraintes ( bool bDesactiver );


		/// <summary>
		/// Retourne true si le syst�me arrive � se connecter � la base demand�e
		/// </summary>
		/// <returns></returns>
		CResultAErreur IsConnexionValide();


		/////////////////////////////////////////////////////////
		/// <summary>
		/// Charge un blob � partir d'une ligne d'une table. Le resultat est 
		/// plac� dans le Data du CResultAErreur
		/// </summary>
		CResultAErreur ReadBlob ( string strNomTableInDb, string strChamp, CFiltreData filtre );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Sauve un blob dans une ligne d'une base de donn�es
		/// </summary>
		CResultAErreur SaveBlob ( string strNomTableInDb, string strChamp, CFiltreData filtre, byte[] data );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne les noms des tables de la base de donn�es
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
		/// soit accept� dans une requ�te SQL
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
