using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire;
using System.Collections;
using sc2i.win32.common;

namespace sc2i.formulaire.win32
{
    public partial class CControleStructureFormulaire : UserControl, IControlALockEdition
    {
        private C2iWnd m_wnd = null;
        private Dictionary<Type, int?> m_dicTypeControlToIndexImage = new Dictionary<Type,int?>();
        private Dictionary<C2iWnd, TreeNode> m_dicWndToNode = new Dictionary<C2iWnd, TreeNode>();
        private CPanelEditionObjetGraphique m_editeur = null;

        public CControleStructureFormulaire()
        {
            InitializeComponent();
        }

        public void Init ( C2iWnd wnd )
        {
            m_dicWndToNode.Clear();
            m_arbre.Nodes.Clear();
            Update ( wnd );
        }

        public CPanelEditionObjetGraphique Editeur
        {
            get{
                return m_editeur;
            }
            set{
                m_editeur = value;
            }
        }

        public void Update(C2iWnd wnd)
        {
            m_wnd = wnd;
            if ( wnd == null )
                return;

            TreeNode node = GetNode ( wnd );
            bool bIsNew = false;
            TreeNode nodeParent = null;
            if ( wnd.Parent != null )
                nodeParent = GetNode ( (C2iWnd)wnd.Parent );
            if ( node == null )
            {
                    if ( nodeParent == null && wnd.Parent != null)
                    {
                        Update ( (C2iWnd)wnd.Parent );
                            return;
                    }
                node = CreateNode(wnd);
                bIsNew = true;
            }
            else
                UpdateNode(node, wnd);
            if (bIsNew)
            {
                if (nodeParent == null)
                    m_arbre.Nodes.Add(node);
                else
                {
                    nodeParent.Nodes.Add(node);
                    nodeParent.Expand();
                }
            }
            else
            {
                if (nodeParent == null && node.Parent != null)
                {
                    node.Parent.Nodes.Remove ( node );
                    m_arbre.Nodes.Add(node);
                }
                if (nodeParent != null && node.Parent != nodeParent)
                {
                    if (node.Parent == null)
                        m_arbre.Nodes.Remove(node);
                    else
                        node.Parent.Nodes.Remove(node);
                    nodeParent.Nodes.Add(node);
                    nodeParent.Expand();
                }
                HashSet<C2iWnd> childs = new HashSet<C2iWnd>();
                foreach (C2iWnd child in wnd.Childs)
                {
                    Update(child);
                    childs.Add(child);
                }
                //Supprime les noeuds disparus
                foreach (TreeNode nodeChild in new ArrayList(node.Nodes))
                {
                    C2iWnd wndChild = nodeChild.Tag as C2iWnd;
                    if (!childs.Contains(wndChild))
                    {
                        if (m_dicWndToNode.ContainsKey(wndChild))
                            m_dicWndToNode.Remove(wndChild);
                        nodeChild.Remove();
                    }
                }
            }
            SortChilds(node);
            node.Expand();
        }

        private void SortChilds(TreeNode node)
        {
            
            m_arbre.BeginUpdate();
            int nIndex = 0;
            C2iWnd parent = node.Tag as C2iWnd;
            if (parent == null)
                return;
            m_bIsSelecting = true;
            TreeNode selected = m_arbre.SelectedNode;
            m_arbre.SelectedNode = null;
            
            foreach (C2iWnd child in parent.Childs)
            {
                TreeNode nodeChild = GetNode(child);
                if (nodeChild != null)
                {
                    node.Nodes.Remove(nodeChild);
                    if (nIndex < node.Nodes.Count - 2)
                        node.Nodes.Insert(nIndex, nodeChild);
                    else
                        node.Nodes.Add(nodeChild);
                    nIndex++;
                }
            }
            m_arbre.SelectedNode = selected;
            m_arbre.EndUpdate();
            m_bIsSelecting = false;
        }


        private TreeNode GetNode ( C2iWnd wnd )
        {
            TreeNode node = null;
            m_dicWndToNode.TryGetValue ( wnd, out node );
            return node;
        }

        private TreeNode CreateNode(C2iWnd wnd)
        {
            TreeNode node = new TreeNode();
            UpdateNode(node, wnd);
            m_dicWndToNode[wnd] = node;
            foreach (C2iWnd child in wnd.Childs)
            {
                Update(child);
            }
            return node;
        }

        private void UpdateNode(TreeNode node, C2iWnd wnd)
        {
            string strText = "";

            object[] attribs = wnd.GetType().GetCustomAttributes(typeof(WndNameAttribute), true);
            if (attribs.Length > 0)
            {
                strText = ((WndNameAttribute)attribs[0]).Name;
            }
            else
                strText = sc2i.common.DynamicClassAttribute.GetNomConvivial(wnd.GetType());
            if (wnd.Name.Length > 0)
                strText += "(" + wnd.Name + ")";
            node.Text = strText;
            node.Tag = wnd;
            int? nIndice = 0;
            if (!m_dicTypeControlToIndexImage.TryGetValue(wnd.GetType(), out nIndice))
            {
                Image img = C2iWnd.GetImage(wnd.GetType());
                if (img != null)
                {
                    nIndice = m_images.Images.Count;
                    m_images.Images.Add(img);
                }
                else
                    nIndice = 0;
                m_dicTypeControlToIndexImage[wnd.GetType()] = nIndice.Value;
            }
            node.ImageIndex = nIndice.Value;
            if (wnd.GetHanlders().Length != 0)
                node.BackColor = Color.Red;
            else
                node.BackColor = Color.White;
            node.SelectedImageIndex = nIndice.Value;
        }


        private bool m_bIsSelecting = false;
        public void SelectWnd(C2iWnd wnd)
        {
            m_bIsSelecting = true;
            TreeNode node = null;
            if (m_dicWndToNode.TryGetValue(wnd, out node))
            {
                m_arbre.SelectedNode = node;
            }
            m_bIsSelecting = false;
        }

        
        private void m_arbre_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (!m_bIsSelecting && Editeur != null && !m_bIsSelecting)
            {
                TreeNode node = e.Node;
                if (node == null)
                    return;
                C2iWnd wnd = node.Tag as C2iWnd;
                if (wnd == null)
                    return;
                Editeur.Selection.Clear();
                Editeur.Selection.Add(wnd);
            }

        }

        //-------------------------------------------------------------
        private void m_btnDescendre_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbre.SelectedNode;
            if (node == null)
                return;
            C2iWnd wnd = node.Tag as C2iWnd;
            if (wnd != null && wnd.Parent != null)
            {
                ((C2iWnd)wnd.Parent).Front1(wnd);
                SortChilds(node.Parent);
                Editeur.Refresh();
                m_arbre.SelectedNode = node;
            }
        }

        //-------------------------------------------------------------
        private void m_btnMonter_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbre.SelectedNode;
            if (node == null)
                return;
            C2iWnd wnd = node.Tag as C2iWnd;
            if ( wnd != null && wnd.Parent != null)
            {
                ((C2iWnd)wnd.Parent).Back1(wnd);
                SortChilds(node.Parent);
                Editeur.Refresh();
                m_arbre.SelectedNode = node;
            }
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
                if ( OnChangeLockEdition != null )
                    OnChangeLockEdition ( this, new EventArgs() );
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
