using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.data.navigation
{
	public partial class CFormListeArchives : Form
	{
		private CObjetDonneeAIdNumerique m_objet = null;
		private bool m_bInclureEtiquettes = true;
		
		private enum ModeFiltre
		{
			VoirPourElement,
			VoirTout
		}

		private ModeFiltre m_modeFiltre = ModeFiltre.VoirPourElement;
		
		//--------------------------------------------------------------
		public CFormListeArchives()
		{
			InitializeComponent();
		}

		//--------------------------------------------------------------
		private void CFormListeArchives_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;
			CWin32Traducteur.Translate(this);
			FillListe();
		}

		//--------------------------------------------------------------
		public static void ShowArchives ( CObjetDonneeAIdNumerique objet )
		{
			CFormListeArchives form = new CFormListeArchives();
			form.m_objet = objet;
			form.ShowDialog();
			form.Dispose();
		}

		//--------------------------------------------------------------
		private void FillListe()
		{
			CListeObjetsDonnees liste = new CListeObjetsDonnees(m_objet.ContexteDonnee, typeof(CVersionDonneesObjet));
			CFiltreData filtre = new CFiltreDataAvance(CVersionDonneesObjet.c_nomTable,
				CVersionDonnees.c_nomTable + "." +
				CVersionDonnees.c_champTypeVersion + "=@1",
				(int)CTypeVersion.TypeVersion.Archive);
			if ( m_bInclureEtiquettes )
				filtre = CFiltreData.GetOrFiltre ( filtre, 
					new CFiltreDataAvance(
					CVersionDonneesObjet.c_nomTable,
					CVersionDonnees.c_nomTable+"."+
					CVersionDonnees.c_champTypeVersion+"=@1",
					(int)CTypeVersion.TypeVersion.Etiquette));
			if (m_modeFiltre == ModeFiltre.VoirPourElement)
				filtre = CFiltreData.GetAndFiltre(filtre,
				new CFiltreData(
				CVersionDonneesObjet.c_champIdElement + "=@1 and " +
				CVersionDonneesObjet.c_champTypeElement + "=@2",
				m_objet.Id,
				m_objet.GetType().ToString()));
			liste.Filtre = filtre;

			m_panelListe.InitFromListeObjets(liste,
				typeof(CVersionDonneesObjet),
				null,
				null,
				"");
		}

		private void m_btnFermer_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_lnkDetail_LinkClicked(object sender, EventArgs e)
		{
			ShowDetail();
		}

		private void m_panelListe_OnObjetDoubleClick(object sender, EventArgs e)
		{
			ShowDetail();
		}

		private void ShowDetail()
		{
			if (m_panelListe.ElementSelectionne is CVersionDonneesObjet)
			{
				CVersionDonneesObjet version = (CVersionDonneesObjet)m_panelListe.ElementSelectionne;
				CFormDetailVersion.ShowDetail(m_objet, version.VersionDonnees);
			}
		}

		private void onlyForElementToolStripMenuItem_Click(object sender, EventArgs e)
		{
			m_modeFiltre = ModeFiltre.VoirPourElement;
			FillListe();
		}

		private void m_lnkOptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			m_menuOptions.Show(m_lnkOptions, new Point(0, m_lnkOptions.Height));
		}

		private void m_menuOptions_Opening(object sender, CancelEventArgs e)
		{
			m_menuOnlyForElement.Checked = m_modeFiltre == ModeFiltre.VoirPourElement;
			m_menuShowAll.Checked = m_modeFiltre == ModeFiltre.VoirTout;
			m_menuSnapshot.Checked = m_bInclureEtiquettes;
		}

		private void m_menuShowAll_Click(object sender, EventArgs e)
		{
			m_modeFiltre = ModeFiltre.VoirTout;
			FillListe();
		}

		private void m_menuSnapshot_Click(object sender, EventArgs e)
		{
			m_bInclureEtiquettes = !m_bInclureEtiquettes;
			FillListe();
		}

	}
}