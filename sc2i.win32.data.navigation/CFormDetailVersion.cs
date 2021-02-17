using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
	public partial class CFormDetailVersion : Form
	{
		public CFormDetailVersion()
		{
			InitializeComponent();
		}

		private void m_btnFermer_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		public static void ShowDetail(CObjetDonneeAIdNumerique objet, CVersionDonnees version)
		{
			CFormDetailVersion form = new CFormDetailVersion();
			form.m_panelDetail.InitForObjet(objet, version);
			form.ShowDialog();
			form.Dispose();
		}

		private void CFormDetailVersion_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
		}

	}
}