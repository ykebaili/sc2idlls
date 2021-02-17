using System;
using sc2i.common;

namespace sc2i.win32.navigation
{
	/// <summary>
	/// Entrée historique de la page d'accueil du navigateur
	/// </summary>
	public class CEntreeHistoriqueAccueil : CEntreeHistorique
	{
		IFormNavigable m_formAccueil = null;
		//---------------------------------------------------------------------------
		public CEntreeHistoriqueAccueil( IFormNavigable formAccueil, string titre )
			:base(titre)
		{
			m_formAccueil = formAccueil;
		}


		public override IFormNavigable GetPage()
		{
			m_formAccueil.InitFromContexte ( Contexte );
			return m_formAccueil;
		}
	}
}
