using System;

namespace sc2i.win32.navigation
{
	/// <summary>
	/// Entrée définissant le contexte de la fenêtre, l'entrée précédente et l'entrée suivante
	/// </summary>
	public class CEntreeHistorique
	{
		CContexteFormNavigable m_contexte = null;
		CEntreeHistorique m_entreePrecedente = null;
		CEntreeHistorique m_entreeSuivante = null;
		//---------------------------------------------------------------------------
		public CEntreeHistorique(string titre)
		{
            Titre = titre;
		}
		//---------------------------------------------------------------------------
		public CEntreeHistorique(CContexteFormNavigable contexte)
		{
			m_contexte = contexte;
		}
        //---------------------------------------------------------------------------
        public string Titre { get; set; }
        //---------------------------------------------------------------------------
		public CContexteFormNavigable Contexte
		{
			get
			{
				return m_contexte;
			}
			set
			{
				m_contexte = value;
			}
		}
		//---------------------------------------------------------------------------
		public virtual IFormNavigable GetPage()
		{
            if (Contexte == null)
                return null;
			return Contexte.AllouePage();
		}
		//---------------------------------------------------------------------------
		public CEntreeHistorique EntreePrecedente
		{
			get
			{
				return m_entreePrecedente;
			}
			set
			{
				m_entreePrecedente = value;
			}
		}
		//---------------------------------------------------------------------------
		public CEntreeHistorique EntreeSuivante
		{
			get
			{
				return m_entreeSuivante;
			}
			set
			{
				m_entreeSuivante = value;
			}
		}
		//---------------------------------------------------------------------------
	}
}
