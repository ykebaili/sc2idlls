using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;


namespace sc2i.data
{


	/// <summary>
	/// Interface de base de journalisation de données d'un objet.
	/// </summary>
	public interface IJournaliseurDonneesObjet
	{
		/// <summary>
		/// Journalise les données d'une ligne
		/// </summary>
		/// <param name="row"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		CVersionDonneesObjet JournaliseDonnees(DataRow row, CVersionDonnees version);

		/// <summary>
		/// Retourne un dictionnaire des champs modifiés pour chaque élément
		/// </summary>
		/// <param name="nIdVersion"></param>
		/// <param name="strIdsVersionsConcernees"></param>
		/// <param name="typeElements"></param>
		/// <param name="rowsConcernees"></param>
		/// <returns></returns>
		Dictionary<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> GetDictionnaireChampsModifies(
			int nIdSession,
			string strIdsVersionsConcernees,
			Type typeElements,
			DataRow[] rowsConcernees);

		/// <summary>
		/// Répercute les modifications d'une ligne sur une ligne de version prévisionnelle
		/// dérivée
		/// </summary>
		/// <param name="rowReference"></param>
		/// <param name="rowCopie"></param>
		void RepercuteModifsSurVersionFuture(
			DataRow rowReference,
			DataRow rowCopie,
			Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoDesModifs);

		//Si oui, indique que chaque élément journalisé par ce journaliseur possède son propre
		//CversionDonneesObjet. Si non, (c'est le cas pour les champs custom), indique que le journaliseur
		//utilise une autre entité.
		bool IsVersionObjetLinkToElement { get;}

		/// <summary>
		/// Retourne la dataRow qui doit être notée comme modifiée pour un
		/// objet qui n'a pas les ObjetLinkToElement (par exemple les valeurs
		/// de champs custom retournent l'élément à champs).
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		DataRow GetRowObjetPourDataVersionObject(DataRow row);
	}

}
