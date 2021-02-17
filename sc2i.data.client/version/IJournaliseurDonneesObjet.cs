using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;


namespace sc2i.data
{


	/// <summary>
	/// Interface de base de journalisation de donn�es d'un objet.
	/// </summary>
	public interface IJournaliseurDonneesObjet
	{
		/// <summary>
		/// Journalise les donn�es d'une ligne
		/// </summary>
		/// <param name="row"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		CVersionDonneesObjet JournaliseDonnees(DataRow row, CVersionDonnees version);

		/// <summary>
		/// Retourne un dictionnaire des champs modifi�s pour chaque �l�ment
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
		/// R�percute les modifications d'une ligne sur une ligne de version pr�visionnelle
		/// d�riv�e
		/// </summary>
		/// <param name="rowReference"></param>
		/// <param name="rowCopie"></param>
		void RepercuteModifsSurVersionFuture(
			DataRow rowReference,
			DataRow rowCopie,
			Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoDesModifs);

		//Si oui, indique que chaque �l�ment journalis� par ce journaliseur poss�de son propre
		//CversionDonneesObjet. Si non, (c'est le cas pour les champs custom), indique que le journaliseur
		//utilise une autre entit�.
		bool IsVersionObjetLinkToElement { get;}

		/// <summary>
		/// Retourne la dataRow qui doit �tre not�e comme modifi�e pour un
		/// objet qui n'a pas les ObjetLinkToElement (par exemple les valeurs
		/// de champs custom retournent l'�l�ment � champs).
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		DataRow GetRowObjetPourDataVersionObject(DataRow row);
	}

}
