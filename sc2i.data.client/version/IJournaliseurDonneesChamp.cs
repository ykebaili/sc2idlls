using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Interface globale pour journaliser les donn�es. 
	/// </summary>
	/// <remarks>
	/// Un IJournaliseur sait retourner un IChampPourVersion � partir d'un CVersionDonneesObjet
	/// </remarks>
	public interface IJournaliseurDonneesChamp
	{
		/// <summary>
		/// Retourne le type de champ que sait ger� ce journaliseur de donn�es
		/// </summary>
		string TypeChamp { get;}

		/// <summary>
		/// Retourne le nom convivial d'un champ archiv�
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
