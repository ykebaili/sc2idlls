using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Interface globale pour journaliser les données. 
	/// </summary>
	/// <remarks>
	/// Un IJournaliseur sait retourner un IChampPourVersion à partir d'un CVersionDonneesObjet
	/// </remarks>
	public interface IJournaliseurDonneesChamp
	{
		/// <summary>
		/// Retourne le type de champ que sait geré ce journaliseur de données
		/// </summary>
		string TypeChamp { get;}

		/// <summary>
		/// Retourne le nom convivial d'un champ archivé
		/// </summary>
		/// <param name="version"></param>
		/// <returns></returns>
		IChampPourVersion GetChamp(IElementAChampPourVersion element);
		List<IChampPourVersion> GetChampsJournalisables(CObjetDonneeAIdNumerique objet);

		object GetValeur(CVersionDonneesObjetOperation version);
		object GetValeur(CObjetDonneeAIdNumerique obj, IChampPourVersion champ);

		CResultAErreur AppliqueValeur(int? nIdVersion, IChampPourVersion champ, CObjetDonneeAIdNumerique objet, object valeur);
	}


}
