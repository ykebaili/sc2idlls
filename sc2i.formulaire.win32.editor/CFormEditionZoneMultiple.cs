using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using System.Collections;

namespace sc2i.formulaire.win32
{
	public partial class CFormEditionZoneMultiple : Form
	{
		private C2iWnd m_formulaire;
		private IFournisseurProprietesDynamiques m_fournisseurProprietes;
		private Type m_typeEdite = null;
		public CFormEditionZoneMultiple()
		{
			InitializeComponent();
		}

		//---------------------------------------------------------------
		public bool EditeZoneMultiple(C2iWndFenetre fenetre, Type typeEdite, IFournisseurProprietesDynamiques fournisseur)
		{
			m_formulaire = fenetre;
			m_fournisseurProprietes = fournisseur;
			m_typeEdite = typeEdite;
			DialogResult result = ShowDialog();
			return true;
		}

		//---------------------------------------------------------------
		private void CFormEditionZoneMultiple_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
			m_panelSousFormulaire.WndEditee = m_formulaire;
			m_panelSousFormulaire.FournisseurProprietes = m_fournisseurProprietes;
			m_panelSousFormulaire.TypeEdite = m_typeEdite;
            //m_panelSousFormulaire.Init(m_typeEdite, null, m_fournisseurProprietes);
            //m_panelSousFormulaire.Init(m_typeEdite, m_formulaire, m_fournisseurProprietes);
            m_panelSousFormulaire.Init(m_typeEdite, m_formulaire, m_formulaire, m_fournisseurProprietes);
		}

	}

	[AutoExec("Autoexec")]
	public class CEditeurSousFormulaireStd : IEditeurSousFormulaire
	{
		//-----------------------------------------------------------------------------------
		public static void Autoexec()
		{
			CEditeurSousFormulaire.SetTypeEditeur ( typeof(CEditeurSousFormulaireStd));
		}

		//-----------------------------------------------------------------------------------
		public bool EditeZoneMultiple(
			C2iWndFenetre sousFormulaire,
			Type typeEdite,
			IFournisseurProprietesDynamiques fournisseur)
		{
			CFormEditionZoneMultiple form = new CFormEditionZoneMultiple();
			bool bResult = form.EditeZoneMultiple(sousFormulaire, typeEdite, fournisseur);
			form.Dispose();
			return bResult;
		}
	}
}