using System;
using System.Data;

using sc2i.common;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Interface entre un la base de données et le
	/// dataset la représentant
	/// </summary>
	public interface IObjetServeur : I2iMarshalObjectDeSession
	{
		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique dans quel version de données les données sont lues
		/// </summary>
		int? IdVersionDeTravail { get;set;}

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Lit les objets en appliquant un filtre
		/// </summary>
		CDataTableFastSerialize Read ( CFiltreData filtre, params string[] strChampsARetourner );

        /////////////////////////////////////////////////////////////////
        /// <summary>
        /// Lit les objets en appliquant un filtre
        /// </summary>
        CDataTableFastSerialize Read(CFiltreData filtre, int nStart, int nEnd, params string[] strChampsARetourner);


		/////////////////////////////////////////////////////////////////
		bool DesactiverIdentifiantAutomatique{get;set;}

		/////////////////////////////////////////////////////////////////
		bool DesactiverContraintes{get;set;}

        /////////////////////////////////////////////////////////////////
        void ClearCache();

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Permet de faire un traitement sur le contexte avant la sauvegarde des données
		/// Tous les objets serveurs concernés par une sauvegarde sont appelés
		/// dans l'ordre de dépendance allant des composants vers les composés (les composants d'abord)
		/// </summary>
		/// <param name="contexte"></param>
		/// <returns></returns>
		CResultAErreur TraitementAvantSauvegarde ( CContexteDonnee contexte );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Traitement après la sauvegarde
		/// </summary>
		/// <param name="contexte"></param>
		/// <param name="bOperationReussie">Indique que l'opération de sauvegarde est un succès</param>
		/// <returns></returns>
		CResultAErreur TraitementApresSauvegarde ( CContexteDonnee contexte, bool bOperationReussie );

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Enregistre les modifications dans la base de données
		/// </summary>
		CResultAErreur SaveAll ( CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataRowState etatsAPrendreEnCompte );

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la table contenant le schéma de la base de données
		/// </summary>
		CDataTableFastSerialize FillSchema();

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie que l'objet est correct. S'il ne l'est pas,
		/// retourne une erreur avec les problèmes trouvés dans l'objet
		/// </summary>
		CResultAErreur VerifieDonnees( CValiseObjetDonnee valise );

		/////////////////////////////////////////////////////////
		bool HasBlobs();

		/////////////////////////////////////////////////////////
		//le data du resultAErreur contient les données (byte[])
		CResultAErreur ReadBlob ( string strChamp, object[] primaryKeys );

        /////////////////////////////////////////////////////////
        //Le data du resultAErreur contient les données List<byte[])
        CResultAErreur ReadBlobs(string strChamp, List<object[]> primaryKeys);

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Sauvegarde un blob
		/// </summary>
		/// <param name="strChamp"></param>
		/// <param name="filtre"></param>
		/// <param name="data"></param>
		/// <param name="nIdVersionArchive">id du CVersionDonnee dans lequel stocker la journalisation</param>
		/// <param name="dataOriginal">Données avant lecture</param>
		/// <returns></returns>
		CResultAErreur SaveBlob ( string strChamp, object[] primaryKeys, byte[] data, int? nIdVersionArchive, byte[] dataOriginal );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// retourne true si une requête sur le filtre demandé
		/// a des résultats. Les résultats ne sont pas chargés
		/// </summary>
		/// <param name="filtre"></param>
		/// <returns></returns>
		int CountRecords ( string strNomTableInContexte, CFiltreData filtre );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Execute une requête qui retourne un résultat et renvoie le résultat dans le data du result
		/// </summary>
		/// <param name="strSelectClause"></param>
		/// <param name="filtre"></param>
		/// <returns></returns>
		CResultAErreur ExecuteScalar ( string strSelectClause, CFiltreData filtre );

		
	}
}
