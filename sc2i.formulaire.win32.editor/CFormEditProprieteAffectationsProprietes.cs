using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using sc2i.formulaire;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.formulaire.win32
{
	public partial class CFormEditProprieteAffectationsProprietes : Form
	{
        private const string c_signatureClipboard = "AFFECTATION";
		//private Type m_typeSource;
        CObjetPourSousProprietes m_objetSource = null;
		private Type m_typeElement;
		private IFournisseurProprietesDynamiques m_fournisseur;

        private List<CAffectationsProprietes> m_listeEditee = new List<CAffectationsProprietes>();

		private CAffectationsProprietes m_affectationsEditee;


		public CFormEditProprieteAffectationsProprietes()
		{
			InitializeComponent();

		}

		//-----------------------------------------------------------------------
		public static List<CAffectationsProprietes> EditeLesAffectations(
            List<CAffectationsProprietes> affectations,
			CObjetPourSousProprietes objetSource,
			Type typeElement,
			IFournisseurProprietesDynamiques fournisseur)
		{
			CFormEditProprieteAffectationsProprietes form = new CFormEditProprieteAffectationsProprietes();
			form.m_typeElement = typeElement;
			form.m_objetSource = objetSource;
			form.m_listeEditee = new List<CAffectationsProprietes>(affectations);
			form.m_fournisseur = fournisseur;
			DialogResult result = form.ShowDialog();
            List<CAffectationsProprietes> retour = form.m_listeEditee;
			if (result == DialogResult.OK)
				retour = form.m_listeEditee;
			form.Dispose();
			return retour;
		}

		//-----------------------------------------------------------------------
		private void CFormEditProprieteAffectationsProprietes_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
            FillListe();
			m_txtCondition.Init(m_fournisseur, m_objetSource);
            m_txtFormuleGlobale.Init(m_fournisseur, new CObjetPourSousProprietes(new CDefinitionMultiSourceForExpression(
                m_objetSource, new CSourceSupplementaire("NewElement", m_objetSource))));
		}

        //-----------------------------------------------------------------------
        private void FillListe()
        {
            m_wndListe.Items.Clear();
            foreach (CAffectationsProprietes affectation in m_listeEditee)
            {
                ListViewItem item = new ListViewItem(affectation.Libelle);
                FillItem(item, affectation);
                m_wndListe.Items.Add(item);
            }
        }

        //-----------------------------------------------------------------------
        private void SaveCurrent()
        {
            if (m_affectationsEditee != null)
            {
                m_affectationsEditee.SetAffectations(m_panelAffectation.GetAffectations().GetAffectations());
                C2iExpression formule = m_txtCondition.Formule;
                m_affectationsEditee.FormuleCondition = formule;
                m_affectationsEditee.GlobalFormula = m_txtFormuleGlobale.Formule;
                m_affectationsEditee.Libelle = m_txtLibelle.Text;
                ListViewItem item = GetItem(m_affectationsEditee);
                if (item != null)
                    FillItem(item, m_affectationsEditee);
            }
        }

		//-----------------------------------------------------------------------
		private void m_btnValiderModifications_Click(object sender, EventArgs e)
		{
            SaveCurrent();
			DialogResult = DialogResult.OK;
			Close();
		}

		//-----------------------------------------------------------------------
		private void m_btnAnnulerModifications_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

        //-----------------------------------------------------------------------
        private void m_wndListe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_wndListe.SelectedItems.Count == 1)
            {
                AfficheAffectation(m_wndListe.SelectedItems[0].Tag as CAffectationsProprietes);
            }
        }

        //-----------------------------------------------------------------------
        void m_wndListe_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ( (e.Button & MouseButtons.Right) == MouseButtons.Right)
			{
                ContextMenuStrip menuDeroulant = new ContextMenuStrip();
                ToolStripMenuItem itemCopier = new ToolStripMenuItem(I.T("Copy|10004"));
                itemCopier.Click += new EventHandler(itemCopier_Click);
                itemCopier.Enabled = m_wndListe.SelectedItems.Count == 1;
                ToolStripMenuItem itemColler = new ToolStripMenuItem(I.T("Past|10005"));
                itemColler.Click += new EventHandler(itemColler_Click);
                itemColler.Enabled = m_wndListe.SelectedItems.Count == 0 && CSerializerObjetInClipBoard.IsObjetInClipboard(c_signatureClipboard);

                menuDeroulant.Items.Add(itemCopier);
                menuDeroulant.Items.Add(itemColler);
                menuDeroulant.Show(m_wndListe, e.Location);
            }
        }

        void itemCopier_Click(object sender, EventArgs e)
        {
            if(m_wndListe.SelectedItems.Count == 1)
            {
                CAffectationsProprietes affectation = m_wndListe.SelectedItems[0].Tag as CAffectationsProprietes;
                CResultAErreur result = CSerializerObjetInClipBoard.Copy(affectation, c_signatureClipboard);
                if (!result)
                    CFormAlerte.Afficher(result);
            }
        }

        void itemColler_Click(object sender, EventArgs e)
        {
            I2iSerializable objet = null;
            CResultAErreur result = CSerializerObjetInClipBoard.Paste(ref objet, c_signatureClipboard);
            if (!result)
            {
                CFormAlerte.Afficher(result);
                return;
            }
            CAffectationsProprietes affectation = objet as CAffectationsProprietes;
            if (affectation != null)
            {
                m_listeEditee.Add(affectation);
                ListViewItem item = new ListViewItem();
                FillItem(item, affectation);
                m_wndListe.Items.Add(item);
                item.Selected = true;
            }
        }

        //-----------------------------------------------------------------------
        private void FillItem(ListViewItem item, CAffectationsProprietes affectation)
        {
            item.Text = affectation.Libelle;
            item.Tag = affectation;
        }

        //-----------------------------------------------------------------------
        private void AfficheAffectation(CAffectationsProprietes affectation)
        {
            SaveCurrent();
            m_affectationsEditee = affectation;
            m_panelContientAffectation.Visible = m_affectationsEditee != null;
            if (m_affectationsEditee != null)
            {
                m_panelAffectation.Init(
                    m_affectationsEditee,
                    m_fournisseur,
                    m_typeElement,
                    m_objetSource);
                m_txtLibelle.Text = m_affectationsEditee.Libelle;
                m_txtCondition.Formule = m_affectationsEditee.FormuleCondition;
                m_txtFormuleGlobale.Formule = m_affectationsEditee.GlobalFormula;
            }
        }

        //-----------------------------------------------------------------------
        private ListViewItem GetItem(CAffectationsProprietes affectation)
        {
            foreach (ListViewItem item in m_wndListe.Items)
                if (item.Tag == affectation)
                    return item;
            return null;
        }

        private void m_wndAdd_LinkClicked(object sender, EventArgs e)
        {
            CAffectationsProprietes affectation = new CAffectationsProprietes();
            affectation.Libelle = I.T("Set @1|20015", (m_listeEditee.Count + 1).ToString());
            m_listeEditee.Add(affectation);
            ListViewItem item = new ListViewItem();
            FillItem(item, affectation);
            m_wndListe.Items.Add(item);
            item.Selected = true;
        }

        private void cWndLinkStd1_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListe.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListe.SelectedItems[0];
                m_listeEditee.Remove(item.Tag as CAffectationsProprietes);
                m_wndListe.Items.Remove(item);
                AfficheAffectation(null);
            }
        }

	}

	[AutoExec("Autoexec")]
	public class CEditeurProprieteAffectationsProprietes : IEditeurAffectationsProprietes
	{
		private Type m_typeElementAffecte = null;
		//private Type m_typeSource = null;
        private CObjetPourSousProprietes m_objetSource = null;
		private IFournisseurProprietesDynamiques m_fournisseur = null;

		//---------------------------------------------------
		public CEditeurProprieteAffectationsProprietes()
		{
		}

		//---------------------------------------------------
		public static void Autoexec()
		{
			CProprieteAffectationsProprietesEditor.SetTypeEditeur(typeof(CEditeurProprieteAffectationsProprietes));
		}

		//---------------------------------------------------
		public CEditeurProprieteAffectationsProprietes(Type typeElement, Type typeSource)
		{
			m_typeElementAffecte = typeElement;
			//m_typeSource = typeSource;
            m_objetSource = new CObjetPourSousProprietes(typeSource);
		}

		//---------------------------------------------------
		public Type TypeElementAffecte
		{
			get
			{
				return m_typeElementAffecte;
			}
			set
			{
				m_typeElementAffecte = value;
			}
		}

		//---------------------------------------------------
		public CObjetPourSousProprietes ObjetSource
		{
			get
			{
				return m_objetSource;
			}
			set
			{
				m_objetSource = value;
			}
		}

		//---------------------------------------------------
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseur;
			}
			set
			{
				m_fournisseur = value;
			}
		}

		//---------------------------------------------------
		public List<CAffectationsProprietes> EditeAffectationsProprietes(List<CAffectationsProprietes> listeAffectations)
		{

            return CFormEditProprieteAffectationsProprietes.EditeLesAffectations(
                listeAffectations,
				m_objetSource,
				m_typeElementAffecte,
				m_fournisseur);
		}
	}
}