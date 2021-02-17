using System;

using sc2i.common;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Tout élément capable de délcencher des actions
	/// (handler et Evenement notamment
	/// </summary>
	public interface IDeclencheurAction
	{
		string Libelle{get;}
		CResultAErreur RunEvent ( CObjetDonneeAIdNumerique objet, CInfoDeclencheurProcess infoDeclencheur, IIndicateurProgression indicateur );
		CResultAErreur RunEventMultiple ( CObjetDonneeAIdNumerique[] objets, CInfoDeclencheurProcess infoDeclencheur, IIndicateurProgression indicateur );
	}

	public interface IDeclencheurActionManuelle : IDeclencheurAction
	{
		string MenuManuel{get;}
		bool HideProgress { get;}
        bool DeclencherSurContexteClient { get; }
        CResultAErreur EnregistreDeclenchementEvenementSurClient(
            CObjetDonneeAIdNumerique objet,
            CInfoDeclencheurProcess infoDeclencheur,
            IIndicateurProgression indicateur);
	}
}
