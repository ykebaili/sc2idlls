using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.common.Restrictions;

namespace sc2i.formulaire
{
    /// <summary>
	/// Interface généralisant l'allocation de controles Win32 pour les C2iWndACreationDynamique
	/// </summary>
	public interface IRuntimeFor2iWnd : IWndAContainer, IConvertibleEnIElementAProprietesDynamiquesDeportees
	{
		
        /// <summary>
        /// Indique que les évenements sont déclenchés sur cet
        /// élément. Sinon, ils sont déclenchés sur le parent de cet élément
        /// </summary>
        bool IsRacineForEvenements { get;}

		/// <summary>
		/// REtourne le C2iWnd associé au ControleWndFor2iWnd
		/// </summary>
		C2iWnd WndAssociee { get;}

				/// <summary>
		/// Change l'élément édité par le contrôle dynamique
		/// </summary>
		/// <param name="element"></param>
		void SetElementEdite(object element);

        /// <summary>
        /// Recharge les données du controle
        /// </summary>
        void RefillControl();

        /// <summary>
        /// Change l'élément édite, mais ne modifie pas les valeurs affichées
        /// </summary>
        /// <param name="element"></param>
        void SetElementEditeSansChangerLesValeursAffichees(object element);

		/// <summary>
		/// Met à jour les propriétés de l'élément édité par le contrôle dynamique
		/// </summary>
		/// <param name="bControlerLesValeursAvantValidation"></param>
		/// <returns></returns>
		CResultAErreur MajChamps(bool bControlerLesValeursAvantValidation);

		/// <summary>
		/// Met à jour les valeurs calculées de ce contrôle
		/// </summary>
		void UpdateValeursCalculees();

        IRuntimeFor2iWnd[] Childs { get; }

		/// <summary>
		/// Applique une restriction au contrôles
		/// </summary>
        /// <param name="restrictionSurElementEdite">Restriction sur l'objet ElementEdite</param>
        /// <param name="listeRestriction">Liste des restrictions à appliquer</param>
		/// <param name="gestionnaire">Gestionnaire de ReadOnly</param>
		void AppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurElementEdite,
            CListeRestrictionsUtilisateurSurType listeRestriction,
            IGestionnaireReadOnlySysteme gestionnaire);

		/// <summary>
		/// Retourne l'élément édité
		/// </summary>
		object EditedElement { get;}

        /// <summary>
        /// Indique que le parent a changé de mode d'édition
        /// A l'appel de cette fonction, le contrôle réadapte son mode
        /// lockEdition en fonction de son LockMode
        /// </summary>
        /// <param name="bModeEdition"></param>
        void OnChangeParentModeEdition(bool bModeEdition);
	}
}
