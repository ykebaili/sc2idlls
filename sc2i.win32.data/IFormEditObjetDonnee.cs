using System;

using sc2i.data;
using sc2i.common;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de IFormEditObjetDonnee.
	/// </summary>
	public interface IFormEditObjetDonnee
	{
		event EventHandler ObjetEditeChanged;

		CObjetDonnee GetObjetEdite();

        bool EtatEdition { get; }

        bool ValiderModifications();

        void AnnulerModifications();

        void AddRestrictionComplementaire(string strTag, CListeRestrictionsUtilisateurSurType restrictions, bool bApplicationImmediate);
	}
}
