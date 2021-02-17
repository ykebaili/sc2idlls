using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using sc2i.common.periodeactivation;
using sc2i.common;

namespace sc2i.win32.common.periodeactivation
{
    public partial class CControleEditePeriodeActivation : UserControl, IControlALockEdition
    {
        private static Dictionary<Type, Type> m_dicTypePeriodeToTypeEditeur = new Dictionary<Type, Type>();
        private IEditeurPeriodeActivation m_editeurEnCours = null;
        private TreeNode m_nodeEdite = null;

        public static void RegisterEditeurPeriode(Type typePeriode, Type typeEditeur)
        {
            m_dicTypePeriodeToTypeEditeur[typePeriode] = typeEditeur;
        }

        private IPeriodeActivation m_periodeRacine = null;
        //---------------------------------------------
        public CControleEditePeriodeActivation()
        {
            InitializeComponent();
            InitMenu();
            CWin32Traducteur.Translate(this);
        }

        //---------------------------------------------
        private void m_arbre_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Edite ( e.Node );
        }

        //---------------------------------------------
        private void Edite(TreeNode node)
        {
            if (m_editeurEnCours != null && m_nodeEdite != null)
            {
                MajFromEditeur();
                ((Control)m_editeurEnCours).Visible = false;
                m_panelEditPeriode.Controls.Remove((Control)m_editeurEnCours);
                ((Control)m_editeurEnCours).Dispose();
            }
            m_editeurEnCours = null;
            m_nodeEdite = null;
            IPeriodeActivation periode = null;
            if (node != null)
                periode = node.Tag as IPeriodeActivation;
            if (periode != null)
            {
                Type tpEditeur = null;
                if (m_dicTypePeriodeToTypeEditeur.TryGetValue(periode.GetType(), out tpEditeur))
                {
                    Control ctrl = Activator.CreateInstance(tpEditeur, new object[0]) as Control;
                    m_panelEditPeriode.Controls.Add(ctrl);
                    CWin32Traducteur.Translate(ctrl);
                    ctrl.Dock = DockStyle.Fill;
                    ((IEditeurPeriodeActivation)ctrl).Init(periode);
                    m_editeurEnCours = ctrl as IEditeurPeriodeActivation;
                    m_editeurEnCours.LockEdition = !m_extModeEdition.ModeEdition;
                    m_extModeEdition.SetModeEdition(ctrl, TypeModeEdition.EnableSurEdition);
                    m_nodeEdite = node;
                }
            }

        }

        //---------------------------------------------
        private void MajFromEditeur()
        {
            if (m_editeurEnCours != null && m_nodeEdite != null)
            {
                CResultAErreur result = m_editeurEnCours.MajChamps();
                FillNode(m_nodeEdite, m_nodeEdite.Tag as IPeriodeActivation);
            }
        }

        //---------------------------------------------
        private class CMenuPeriode : ToolStripMenuItem
        {
            private Type m_typePeriode = null;
            private bool m_bInsert = false;

            //------------------------------
            public CMenuPeriode ( Type typePeriode, bool bInsert )
            {
                m_typePeriode = typePeriode;
                m_bInsert = bInsert;
                IPeriodeActivation periode = Activator.CreateInstance ( typePeriode , new object[0] ) as IPeriodeActivation;
                Text = periode.GetLibelleType();
            }

            //------------------------------
            public Type TypePeriode 
            {
                get{
                    return m_typePeriode;
                }

            }

            //------------------------------
            public bool IsInsert
            {
                get{
                    return m_bInsert;
                }
            }
        }

        //---------------------------------------------
        private void InitMenu()
        {
            Assembly ass = typeof(IPeriodeActivation).Assembly;
            foreach (Type tp in ass.GetTypes())
            {
                if (typeof(IPeriodeActivation).IsAssignableFrom(tp) && !tp.IsAbstract && !tp.IsInterface)
                {
                    CMenuPeriode menu = new CMenuPeriode(tp, false);
                    m_menuAdd.DropDownItems.Add(menu);
                    menu.Click += new EventHandler(menuAdd_Click);
                    if (typeof(IPeriodeActivationMultiple).IsAssignableFrom(tp))
                    {
                        menu = new CMenuPeriode(tp, true);
                        m_menuInsert.DropDownItems.Add(menu);
                        menu.Click += new EventHandler(menuInsert_Click);
                    }
                }
            }
        }        

        //--------------------------------------------------------
        private TreeNode CreateNodeAndChilds ( IPeriodeActivation periode )
        {
            TreeNode node = new TreeNode();
            FillNode ( node, periode );
            IPeriodeActivationMultiple pm = periode as IPeriodeActivationMultiple;
            if ( pm != null )
            {
                foreach ( IPeriodeActivation child in pm.Periodes )
                {
                    TreeNode newNode = CreateNodeAndChilds ( child );
                    node.Nodes.Add ( newNode );
                }
            }
            return node;
        }

        //--------------------------------------------------------
        public IPeriodeActivation Periode
        {
            get
            {
                MajFromEditeur();
                return m_periodeRacine;
            }
            set
            {
                m_periodeRacine = value;
                m_arbre.Nodes.Clear();
                if (value != null)
                {
                    TreeNode node = CreateNodeAndChilds(value);
                    m_arbre.Nodes.Add(node);
                }
            }
        }

        //--------------------------------------------------------
        private void FillNode ( TreeNode node, IPeriodeActivation periode )
        {
            node.Text = periode.Libelle;
            node.Tag = periode;
        }

        //--------------------------------------------------------
        void  menuInsert_Click(object sender, EventArgs e)
        {
            CMenuPeriode menu = sender as CMenuPeriode;
            if ( menu == null )
                return;

            TreeNode node = m_arbre.SelectedNode;
            if (node == null)
                return;
            IPeriodeActivation periodeSel = node.Tag as IPeriodeActivation;
            IPeriodeActivationMultiple periode = Activator.CreateInstance(menu.TypePeriode, new object[0]) as IPeriodeActivationMultiple;
            
            if (node.Parent == null)
            {
                m_periodeRacine = periode;
                periode.AddPeriode(periodeSel);
                m_arbre.Nodes.Clear();
                TreeNode newNode = CreateNodeAndChilds(periode);
                m_arbre.Nodes.Add ( newNode );
                newNode.Expand();
                newNode.EnsureVisible();
                m_arbre.SelectedNode = newNode;
            }
            else
            {
                TreeNode nodeParent = node.Parent;
                IPeriodeActivationMultiple periodeParente = nodeParent.Tag as IPeriodeActivationMultiple;
                nodeParent.Nodes.Remove ( node );
                periodeParente.RemovePeriode ( periodeSel );
                periode.AddPeriode ( periodeSel );
                TreeNode newNode = CreateNodeAndChilds(periode);
                nodeParent.Nodes.Add ( newNode );
                newNode.EnsureVisible();
                m_arbre.SelectedNode = newNode;
            }
            
        }

        void  menuAdd_Click(object sender, EventArgs e)
        {
            CMenuPeriode menu = sender as CMenuPeriode;
            if (menu == null)
                return;
            TreeNode nodeSel = m_arbre.SelectedNode;
            IPeriodeActivation periode = Activator.CreateInstance(menu.TypePeriode, new object[0]) as IPeriodeActivation;
            TreeNode newNode = CreateNodeAndChilds(periode);
            if (nodeSel != null)
            {
                nodeSel.Nodes.Add(newNode);
                ((IPeriodeActivationMultiple)nodeSel.Tag).AddPeriode(periode);
            }
            else
            {
                m_arbre.Nodes.Add(newNode);
                m_periodeRacine = periode;
            }
            m_arbre.SelectedNode = newNode;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!m_extModeEdition.ModeEdition)
            {
                e.Cancel = true;
                return;
            }
            TreeNode node = m_arbre.SelectedNode;
            if (node == null)
            {
                if (m_arbre.Nodes.Count == 0)
                {
                    m_menuAdd.Visible = true;
                    m_menuInsert.Visible = false;
                    m_menuDelete.Visible = false;
                }
            }
            else
            {
                IPeriodeActivation periode = node.Tag as IPeriodeActivation;
                bool bIsMultiple = typeof(IPeriodeActivationMultiple).IsAssignableFrom(periode.GetType());
                m_menuAdd.Visible = bIsMultiple;
                m_menuInsert.Visible = true;
                m_menuDelete.Visible = true;
                m_menuDeleteAndMoveToTop.Visible = node.Parent != null && periode is IPeriodeActivationMultiple && ((IPeriodeActivationMultiple)periode).Periodes.Count() > 0;
            }
        }

        private void m_arbre_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeViewHitTestInfo info = m_arbre.HitTest(new Point(e.X, e.Y));
                m_arbre.SelectedNode = info.Node;
                m_menuArbre.Show(m_arbre, new Point(e.X, e.Y));
            }
        }

        private void m_menuDeleteElement_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbre.SelectedNode;
            if (node == null)
                return;
            if (node.Parent == null)
            {
                m_arbre.Nodes.Remove(node);
                m_periodeRacine = null;
            }
            else
            {
                IPeriodeActivation periode = node.Tag as IPeriodeActivation;
                IPeriodeActivationMultiple periodeParente = node.Parent.Tag as IPeriodeActivationMultiple;
                periodeParente.RemovePeriode(periode);
                node.Parent.Nodes.Remove(node);
            }
        }

        private void m_menuDeleteAndMoveToTop_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbre.SelectedNode;
            if ( node == null )
                return;
            TreeNode nodeParent = node.Parent;
            if (nodeParent == null)
                return;
            IPeriodeActivationMultiple periodeParente = nodeParent.Tag as IPeriodeActivationMultiple;
            IPeriodeActivationMultiple periodeToDelete = node.Tag as IPeriodeActivationMultiple;
            foreach (IPeriodeActivation periode in periodeToDelete.Periodes)
            {
                periodeParente.AddPeriode(periode);
                TreeNode newNode = CreateNodeAndChilds(periode);
                nodeParent.Nodes.Add(newNode);
            }
            nodeParent.Nodes.Remove(node);

        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
