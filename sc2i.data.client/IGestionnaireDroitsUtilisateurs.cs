using System;

using sc2i.common;
using sc2i.multitiers.client;
using sc2i.data;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de IGestionnaireDroitsUtilisateurs.
	/// </summary>
	public interface IGestionnaireDroitsUtilisateurs
	{
		/// <summary>
		/// Enregistre un nouveau droit utilisateur dans l'application
		/// </summary>
		/// <param name="strCode">Code unique du droit</param>
		/// <param name="strLibelle">Libellé court</param>
		/// <param name="nNumOrdre">Numéro d'ordre (tri dans le droit parent)</param>
		/// <param name="strDroitParent">Code du droit parent (null si aucun)</param>
		/// <param name="strInfoSurDroit">Descriptif du droit</param>
		/// <param name="optionsPossibles">Combinaison d'options (OptionsSurDroit) disponibles pour ce droit</param>
		/// <returns></returns>
		void RegisterDroitUtilisateur ( 
			string strCode,
			string strLibelle,
			int nNumOrdre,
			string strDroitParent,
			string strInfoSurDroit,
			OptionsDroit optionsPossibles );
	}
}
