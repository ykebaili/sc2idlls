using System;
using System.Data;

using sc2i.common;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Interface entre un la base de donn�es et le
	/// dataset la repr�sentant
	/// </summary>
	public interface IObjetServeur : I2iMarshalObjectDeSession
	{
		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique dans quel version de donn�es les donn�es sont lues
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
		/// Permet de faire un traitement sur le contexte avant la sauvegarde des donn�es
		/// Tous les objets serveurs concern�s par une sauvegarde sont appel�s
		/// dans l'ordre de d�pendance allant des composants vers les compos�s (les composants d'abord)
		/// </summary>
		/// <param name="contexte"></param>
		/// <returns></returns>
		CResultAErreur TraitementAvantSauvegarde ( CContexteDonnee contexte );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Traitement apr�s la sauvegarde
		/// </summary>
		/// <param name="contexte"></param>
		/// <param name="bOperationReussie">Indique que l'op�ration de sauvegarde est un succ�s</param>
		/// <returns></returns>
		CResultAErreur TraitementApresSauvegarde ( CContexteDonnee contexte, bool bOperationReussie );

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Enregistre les modifications dans la base de donn�es
		/// </summary>
		CResultAErreur SaveAll ( CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataRowState etatsAPrendreEnCompte );

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la table contenant le sch�ma de la base de donn�es
		/// </summary>
		CDataTableFastSerialize FillSchema();

		/////////////////////////////////////////////////////////
		/// <summary>
		/// V�rifie que l'objet est correct. S'il ne l'est pas,
		/// retourne une erreur avec les probl�mes trouv�s dans l'objet
		/// </summary>
		CResultAErreur VerifieDonnees( CValiseObjetDonnee valise );

		/////////////////////////////////////////////////////////
		bool HasBlobs();

		/////////////////////////////////////////////////////////
		//le data du resultAErreur contient les donn�es (byte[])
		CResultAErreur ReadBlob ( string strChamp, object[] primaryKeys );

        /////////////////////////////////////////////////////////
        //Le data du resultAErreur contient les donn�es List<byte[])
        CResultAErreur ReadBlobs(string strChamp, List<object[]> primaryKeys);

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Sauvegarde un blob
		/// </summary>
		/// <param name="strChamp"></param>
		/// <param name="filtre"></param>
		/// <param name="data"></param>
		/// <param name="nIdVersionArchive">id du CVersionDonnee dans lequel stocker la journalisation</param>
		/// <param name="dataOriginal">Donn�es avant lecture</param>
		/// <returns></returns>
		CResultAErreur SaveBlob ( string strChamp, object[] primaryKeys, byte[] data, int? nIdVersionArchive, byte[] dataOriginal );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// retourne true si une requ�te sur le filtre demand�
		/// a des r�sultats. Les r�sultats ne sont pas charg�s
		/// </summary>
		/// <param name="filtre"></param>
		/// <returns></returns>
		int CountRecords ( string strNomTableInContexte, CFiltreData filtre );

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Execute une requ�te qui retourne un r�sultat et renvoie le r�sultat dans le data du result
		/// </summary>
		/// <param name="strSelectClause"></param>
		/// <param name="filtre"></param>
		/// <returns></returns>
		CResultAErreur ExecuteScalar ( string strSelectClause, CFiltreData filtre );

		
	}
}
