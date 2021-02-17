using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.expression.FonctionsDynamiques;
using sc2i.expression;
using sc2i.common;

namespace sc2i.win32.expression
{
    public partial class CFormEditeListeFonctionsDynamiques : Form
    {
        private IEnumerable<CFonctionDynamique> m_fonctions = new List<CFonctionDynamique>();
        private CObjetPourSousProprietes m_objetPourSousProprietes = null;

        //---------------------------------------------------------------------
        public CFormEditeListeFonctionsDynamiques()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //---------------------------------------------------------------------
        private void m_wndListeFonctions_Resize(object sender, EventArgs e)
        {
            m_wndListeFonctions.Columns[0].Width = m_wndListeFonctions.ClientRectangle.Width;
        }

        //---------------------------------------------------------------------
        private void CFormEditeListeFonctionsDynamiques_Load(object sender, EventArgs e)
        {
            FillListeFonctions();
        }

        //---------------------------------------------------------------------
        public static IEnumerable<CFonctionDynamique> EditeFonctions(IEnumerable<CFonctionDynamique> fonctions,
            CObjetPourSousProprietes objetPourSousProprietes)
        {
            using (CFormEditeListeFonctionsDynamiques frm = new CFormEditeListeFonctionsDynamiques())
            {
                if (fonctions != null)
                    frm.m_fonctions = fonctions;
                frm.m_objetPourSousProprietes = objetPourSousProprietes;
                if (frm.ShowDialog() == DialogResult.OK)
                    return frm.m_fonctions;
                return fonctions;
            }
        }

        //---------------------------------------------------------------------
        private void FillListeFonctions()
        {
            m_wndListeFonctions.BeginUpdate();
            m_wndListeFonctions.Items.Clear();
            foreach (CFonctionDynamique fonction in m_fonctions)
            {
                ListViewItem item = new ListViewItem();
                FillItem(item, fonction);
                m_wndListeFonctions.Items.Add(item);
            }
            m_wndListeFonctions.EndUpdate();
        }

        //---------------------------------------------------------------------
        private void FillItem(ListViewItem item, CFonctionDynamique fonction)
        {
            item.Text = fonction.Nom;
            item.Tag = fonction;
        }

        //---------------------------------------------------------------------
        private void m_btnAdd_LinkClicked(object sender, EventArgs e)
        {
            CFonctionDynamique fonction = new CFonctionDynamique();
            if ( CFormEditionFonctionDynamique.EditeFonction (
                ref fonction,
                m_objetPourSousProprietes ))
            {
                ListViewItem item = new ListViewItem();
                FillItem ( item, fonction );
                m_wndListeFonctions.Items.Add ( item );
            }
        }

        //---------------------------------------------------------------
        private void m_btnEdit_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeFonctions.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeFonctions.SelectedItems[0];
                CFonctionDynamique fonction = item.Tag as CFonctionDynamique;
                if (fonction != null)
                {
                    if (CFormEditionFonctionDynamique.EditeFonction(
                        ref fonction,
                        m_objetPourSousProprietes))
                    {
                        FillItem(item, fonction);
                    }
                }
            }
        }

        //---------------------------------------------------------------
        private void m_btnRemove_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeFonctions.SelectedItems.Count == 1)
            {
                CFonctionDynamique fonction = m_wndListeFonctions.SelectedItems[0].Tag as CFonctionDynamique;
                if (fonction != null)
                {
                    if (CFormAlerte.Afficher(I.T("Delete function @1 ?|20036", fonction.Nom),
                        EFormAlerteBoutons.OuiNon,
                        EFormAlerteType.Question) == DialogResult.Yes)
                    {
                        m_wndListeFonctions.Items.Remove(m_wndListeFonctions.SelectedItems[0]);
                    }
                }
            }
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_fonctions = new List<CFonctionDynamique>();
            foreach (ListViewItem item in m_wndListeFonctions.Items)
            {
                CFonctionDynamique fonction = item.Tag as CFonctionDynamique;
                if (fonction != null)
                    ((List<CFonctionDynamique>)m_fonctions).Add(fonction);
            }
            DialogResult = DialogResult.OK;
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    //---------------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurListeFonctionsDynamiques : IEditeurFonctionsDynamiques
    {

        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CFonctionDynamiqueEditor.SetTypeEditeur(typeof(CEditeurListeFonctionsDynamiques));
        }

        public IEnumerable<CFonctionDynamique> EditeFonctions(IEnumerable<CFonctionDynamique> fonctions, CObjetPourSousProprietes objet)
        {
            return CFormEditeListeFonctionsDynamiques.EditeFonctions ( fonctions, objet );
        }

       
    }
}
