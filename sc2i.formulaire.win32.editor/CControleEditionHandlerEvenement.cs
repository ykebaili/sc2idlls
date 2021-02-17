using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;

namespace sc2i.formulaire.win32
{
	public partial class CControleEditionHandlerEvenement : UserControl
	{
		private C2iWnd m_wndEditee = null;
		CDescriptionEvenementParFormule m_descriptionEvenement = null;
		public CControleEditionHandlerEvenement()
		{
			InitializeComponent();
		}

		public void Init(
			C2iWnd wnd, 
			CDescriptionEvenementParFormule descriptionEvenement,
			IFournisseurProprietesDynamiques fournisseur)
		{
			m_wndEditee = wnd;
			m_descriptionEvenement = descriptionEvenement;
			m_lblNomEvenement.Text = m_descriptionEvenement.NomEvenement;
			m_tooltip.SetToolTip(m_lblNomEvenement, m_descriptionEvenement.DescriptionEvenement);

			CHandlerEvenementParFormule handler = wnd.GetHandler(descriptionEvenement.IdEvenement);
			if (handler == null)
				m_txtEditFormule.Formule = null;
			else
				m_txtEditFormule.Formule = handler.FormuleEvenement;
			C2iWnd parent = wnd;
			while (parent.Parent as C2iWnd != null)
				parent = parent.Parent as C2iWnd;
			m_txtEditFormule.Init(fournisseur, new CObjetPourSousProprietes(parent));
		}

		private void m_txtEditFormule_Leave(object sender, EventArgs e)
		{
			if (m_wndEditee != null)
			{
				CHandlerEvenementParFormule handler = new CHandlerEvenementParFormule(
					m_descriptionEvenement.IdEvenement,
					m_txtEditFormule.Formule);
				m_wndEditee.SetHandler(handler);
			}
		}
	}
}
