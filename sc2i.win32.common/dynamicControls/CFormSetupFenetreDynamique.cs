using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common.dynamicControls
{
    public partial class CFormSetupFenetreDynamique : Form
    {
        //---------------------------------------------------
        private CSetupVisibiliteControles m_setup = new CSetupVisibiliteControles();
        private Control m_controleRacine;
        private Control m_controleSelectionne = null;

        
        public CFormSetupFenetreDynamique()
        {
            InitializeComponent();
        }

        //---------------------------------------------------
        public static void ShowArbre(Control ctrl, CSetupVisibiliteControles setup)
        {
            CFormSetupFenetreDynamique form = new CFormSetupFenetreDynamique();
            form.m_setup = setup;
            form.m_controleRacine = ctrl;
            form.ShowDialog();
            form.StoppeClignote();
            form.Dispose();
        }

        //---------------------------------------------------
        private void FillArbre(Control ctrl, TreeNodeCollection nodes)
        {
            m_arbreControles.BeginUpdate();
            m_arbreControles.Nodes.Clear();
            CreateNode(ctrl, m_arbreControles.Nodes);
            m_arbreControles.EndUpdate();
        }

        //---------------------------------------------------
        private TreeNode CreateNode(Control ctrl, TreeNodeCollection nodes)
        {
            string strNom = ctrl.Text + " (";
            strNom += ctrl.Name+")"+" (";
            string[] strParts = ctrl.GetType().ToString().Split('.');
            strNom += strParts[strParts.Length - 1];
            strNom += ")";
            TreeNode node = new TreeNode(strNom);
            node.Tag = ctrl;
            if (ctrl.Controls.Count > 0)
            {
                TreeNode dummy = new TreeNode("");
                node.Nodes.Add(dummy);
            }
            node.Checked = !m_setup.IsHidden(ctrl, m_controleRacine);
            if ( ctrl.Controls.Count != 0 || ctrl.Name.Trim().Length > 0 )
                nodes.Add(node);
            return node;
        }

        //---------------------------------------------------
        private void CFormSetupFenetreDynamique_Load(object sender, EventArgs e)
        {
            m_arbreControles.BeginUpdate();
            m_arbreControles.Nodes.Clear();
            FillArbre(m_controleRacine, m_arbreControles.Nodes);
            m_arbreControles.EndUpdate();
        }

        //---------------------------------------------------
        private void m_arbreControles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
            {
                e.Node.Nodes.Clear();
                Control ctrl = e.Node.Tag as Control;
                if (ctrl != null)
                {
                    foreach (Control child in ctrl.Controls)
                    {
                        CreateNode(child, e.Node.Nodes);
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        private void m_arbreControles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control ctrl = e.Node.Tag as Control;
            
            StartClignote(ctrl);
            Crownwood.Magic.Controls.TabPage page = ctrl as Crownwood.Magic.Controls.TabPage;
            if (page != null && page.Parent is Crownwood.Magic.Controls.TabControl)
            {
                ((Crownwood.Magic.Controls.TabControl)page.Parent).SelectedTab = page;
            }
        }

        //---------------------------------------------------------------------
        private void StoppeClignote()
        {
            m_timerClignoteSel.Enabled = false;
            if (m_controleSelectionne != null)
            {
                m_controleSelectionne.Visible = true;
                m_setup.Apply(m_controleRacine, true);
            }
        }

        //---------------------------------------------------------------------
        private void StartClignote ( Control ctrl )
        {
            StoppeClignote();
            m_controleSelectionne = ctrl;
            if ( ctrl != null )
                m_timerClignoteSel.Enabled = true;
        }

        private void m_timerClignoteSel_Tick(object sender, EventArgs e)
        {
            if ( m_controleSelectionne != null && m_controleSelectionne.Parent != null)
                m_controleSelectionne.Visible =! m_controleSelectionne.Visible;

        }

        private void m_arbreControles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Control ctrl = e.Node.Tag as Control;
            if ( ctrl != null )
            {
                if (!e.Node.Checked)
                {
                    m_setup.HideControle(ctrl, m_controleRacine);
                    m_setup.Apply(m_controleRacine, true);
                }
                else
                {
                    m_setup.ShowControle(ctrl, m_controleRacine);
                    m_controleSelectionne.Visible = true;
                    m_setup.Apply(m_controleRacine, true);
                    if (ctrl is Crownwood.Magic.Controls.TabPage)
                        ((Crownwood.Magic.Controls.TabPage)ctrl).Icon = null;
                }
                m_arbreControles.SelectedNode = e.Node;
            }
                
        }

        private void m_arbreControles_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            Control ctrl = e.Node.Tag as Control;
            if (ctrl == null || ctrl.Name.Trim().Length == 0)
                e.Cancel = true;
        }

        private void m_btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset all values", "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                m_setup.Reset();
                m_arbreControles.Nodes.Clear();
                FillArbre(m_controleRacine, m_arbreControles.Nodes);
            }
                
        }




    }
}
