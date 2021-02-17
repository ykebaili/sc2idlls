using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using System.Data;
using System.Drawing;
using sc2i.win32.common;

namespace futurocom.win32.easyquery
{
    public class CTreeListeTables : TreeView, IControlALockEdition
    {
        private bool m_bModeEdition = true;
        private ContextMenuStrip m_menuTree;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem m_menuBrowse;
        private CListeQuerySource m_sources = null;
        private ImageList m_imageList = null;
        private Dictionary<string, int> m_dicImageKeyToImageNum = new Dictionary<string, int>();
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem m_menuConnexions;
        private ToolStripMenuItem m_menuAjouterConnexion;
        private ToolStripMenuItem m_menuPropsConnexion;
        private ToolStripMenuItem m_menuRefreshConnexion;

        private bool m_bMenuEventsInitialised = false;

        //-------------------------------------------
        public CTreeListeTables()
            :base()
        {
            InitializeComponent();
            
        }

        //-------------------------------------------
        private int GetImageNum(string strImage)
        {
            int nResult;
            if (!m_dicImageKeyToImageNum.TryGetValue(strImage, out nResult))
                nResult = -1;
            return nResult;
        }
            

        //-------------------------------------------
        public void Init(CListeQuerySource sources)
        {
            m_sources = sources;
            m_imageList = new ImageList();
            m_dicImageKeyToImageNum.Clear();
            foreach (KeyValuePair<string, Image> kv in CEasyQuerySource.ToutesImages)
            {
                int nNum = m_imageList.Images.Count;
                m_imageList.Images.Add(kv.Value);
                m_dicImageKeyToImageNum[kv.Key] = nNum;
            }
            ImageList = m_imageList;
            RefreshTree();
        }

        //-------------------------------------------
        public void RefreshTree()
        {
            CTreeViewNodeKeeper keeper = new CTreeViewNodeKeeper(this);
            BeginUpdate();
            Nodes.Clear();
            foreach (CEasyQuerySource source in m_sources.Sources)
            {
                TreeNode node = new TreeNode(source.SourceName);
                node.Tag = source;
                Nodes.Add(node);
                TreeNode dummy = new TreeNode();
                node.Nodes.Add(dummy);
            }
            EndUpdate();
            keeper.Apply(this);
        }
        
        //-------------------------------------------
        private void FillNodes(
            TreeNodeCollection nodes, 
            CEasyQuerySource source,
            CEasyQuerySourceFolder folder)
        {
            foreach (CEasyQuerySourceFolder childFolder in folder.Childs)
            {
                TreeNode node = new TreeNode(childFolder.Name);
                node.Tag = childFolder;
                nodes.Add(node);
                node.ImageIndex = GetImageNum(childFolder.ImageKey);
                node.SelectedImageIndex = node.ImageIndex;
                node.Nodes.Add(new TreeNode());
            }
            foreach (ITableDefinition table in source.GetTablesForFolder(folder.Id))
            {
                TreeNode node = new TreeNode(table.TableName);
                node.Tag = table;
                node.ImageIndex = GetImageNum(table.ImageKey);
                node.SelectedImageIndex = node.ImageIndex;
                if (table.Columns.Count() > 0)
                    node.Nodes.Add(new TreeNode());
                nodes.Add(node);
            }
        }
        
        //-------------------------------------------
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_menuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuConnexions = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuAjouterConnexion = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuPropsConnexion = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuRefreshConnexion = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menuTree
            // 
            this.m_menuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuBrowse,
            this.toolStripSeparator1,
            this.m_menuConnexions});
            this.m_menuTree.Name = "m_menuTree";
            this.m_menuTree.Size = new System.Drawing.Size(179, 54);
            this.m_menuTree.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuTree_Opening);
            // 
            // m_menuBrowse
            // 
            this.m_menuBrowse.Name = "m_menuBrowse";
            this.m_menuBrowse.Size = new System.Drawing.Size(178, 22);
            this.m_menuBrowse.Text = "Browse";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // m_menuConnexions
            // 
            this.m_menuConnexions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuAjouterConnexion,
            this.m_menuPropsConnexion,
            this.m_menuRefreshConnexion});
            this.m_menuConnexions.Name = "m_menuConnexions";
            this.m_menuConnexions.Size = new System.Drawing.Size(178, 22);
            this.m_menuConnexions.Text = "Connections|20051";
            // 
            // m_menuAjouterConnexion
            // 
            this.m_menuAjouterConnexion.Name = "m_menuAjouterConnexion";
            this.m_menuAjouterConnexion.Size = new System.Drawing.Size(168, 22);
            this.m_menuAjouterConnexion.Text = "Add|20048";
            // 
            // m_menuPropsConnexion
            // 
            this.m_menuPropsConnexion.Name = "m_menuPropsConnexion";
            this.m_menuPropsConnexion.Size = new System.Drawing.Size(168, 22);
            this.m_menuPropsConnexion.Text = "Propriétés|20049";
            // 
            // m_menuRefreshConnexion
            // 
            this.m_menuRefreshConnexion.Name = "m_menuRefreshConnexion";
            this.m_menuRefreshConnexion.Size = new System.Drawing.Size(168, 22);
            this.m_menuRefreshConnexion.Text = "Reload|20050";
            // 
            // CTreeListeTables
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.CTreeListeTables_BeforeExpand);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CTreeListeTables_MouseUp);
            this.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.CTreeListeTables_ItemDrag);
            this.m_menuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //--------------------------------------------------------------------------------
        private void CTreeListeTables_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag is CEasyQuerySource)
            {
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
                {
                    using (CWaitCursor waiter = new CWaitCursor())
                    {
                        e.Node.Nodes.Clear();
                        CEasyQuerySource source = e.Node.Tag as CEasyQuerySource;
                        FillNodes(e.Node.Nodes, source, source.RootFolder);
                    }
                }
            }
            else if (e.Node.Tag is ITableDefinition)
            {
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();
                    ITableDefinition table = e.Node.Tag as ITableDefinition;
                    foreach (IColumnDefinition col in table.Columns)
                    {
                        TreeNode node = new TreeNode(col.ColumnName);
                        node.Tag = col;
                        node.ImageIndex = GetImageNum(col.ImageKey);
                        node.SelectedImageIndex = node.ImageIndex;
                        e.Node.Nodes.Add(node);
                    }
                }
            }
            else if (e.Node.Tag is CEasyQuerySourceFolder)
            {
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
                {
                    CEasyQuerySourceFolder folder = e.Node.Tag as CEasyQuerySourceFolder;
                    e.Node.Nodes.Clear();
                    FillNodes(e.Node.Nodes, folder.Source, folder);
                    if (Nodes.Count == 1)
                        Nodes[0].Expand();
                }
            }
        }

        //--------------------------------------------------------------------------------
        protected ITableDefinition GetTableForNode(TreeNode node)
        {
            ITableDefinition table = node.Tag as ITableDefinition;
            if (table == null)
            {
                return null;
                CEasyQuerySourceFolder folder = node.Tag as CEasyQuerySourceFolder;
                if (folder != null)
                    table = folder.GetDefinitionTable(m_sources.GetSourceFromId ( table.SourceId ));
            }
            return table;
        }

        //--------------------------------------------------------------------------------
        public void m_menuBrowse_Click(object sender, EventArgs e)
        {
            TreeNode node = SelectedNode;
            ITableDefinition table = GetTableForNode(node);
            if (table != null)
            {
                DataTable dataTable = m_sources.GetTable(table);
                if (dataTable != null)
                {
                    CFormVisuTable.ShowTable(dataTable);
                }
                else
                    MessageBox.Show("Impossible de voir les données");
            }
        }

        //--------------------------------------------------------------------------------
        private void CTreeListeTables_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = PointToScreen(new Point(e.X, e.Y));
                TreeViewHitTestInfo info = HitTest(e.X, e.Y);
                if (info != null && info.Node != null)
                {
                    SelectedNode = info.Node;
                }
                m_menuTree.Show(pt);
            }
        }

        //--------------------------------------------------------------------------------
        private void m_menuTree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AssureEventsMenus();
            if (m_menuAjouterConnexion.DropDownItems.Count == 0)
            {
                //Ajoute les items de connexions possibles
                foreach (IEasyQueryConnexion cnx in CAllocateurEasyQueryConnexions.GetConnexionsPossibles())
                {
                    ToolStripMenuItem itemNewCnx = new ToolStripMenuItem(cnx.ConnexionTypeName);
                    itemNewCnx.Tag = cnx;
                    itemNewCnx.Click += new EventHandler(itemNewCnx_Click);
                    m_menuAjouterConnexion.DropDownItems.Add(itemNewCnx);
                }
            }
            m_menuPropsConnexion.Visible = SelectedNode != null && SelectedNode.Tag is CEasyQuerySource;
            m_menuRefreshConnexion.Visible = SelectedNode != null && SelectedNode.Tag is CEasyQuerySource;
            m_menuBrowse.Visible = SelectedNode != null && GetTableForNode(SelectedNode) != null;

            m_menuAjouterConnexion.Enabled = !LockEdition;
            m_menuPropsConnexion.Enabled = !LockEdition;
        }

        //--------------------------------------------------------------------------------
        void itemNewCnx_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            IEasyQueryConnexion cnx = item != null ? item.Tag as IEasyQueryConnexion : null;
            if (cnx != null)
            {
                cnx = (IEasyQueryConnexion)Activator.CreateInstance(cnx.GetType(), new object[0]);
                CEasyQuerySource src = new CEasyQuerySource();
                src.Connexion = cnx;
                src.SourceName = cnx.ConnexionTypeName;
                if (CFormProprietesQuerySource.EditeParametres(src))
                {
                    m_sources.AddSource(src);
                    RefreshTree();
                }
            }   
        }

        //--------------------------------------------------------------------------------
        private void AssureEventsMenus()
        {
            if (m_bMenuEventsInitialised)
                return;
            m_menuBrowse.Click += new EventHandler(m_menuBrowse_Click);
            m_menuPropsConnexion.Click += new EventHandler(m_menuProprietesConnexion_Click);
            m_menuRefreshConnexion.Click += new EventHandler(m_menuRefreshConnexion_Click);
            m_bMenuEventsInitialised = true;
        }

        //--------------------------------------------------------------------------------
        void m_menuRefreshConnexion_Click(object sender, EventArgs e)
        {
            TreeNode node = SelectedNode;
            CEasyQuerySource source = node != null ? node.Tag as CEasyQuerySource : null;
            if (source != null)
            {
                node.Nodes.Clear();
                FillNodes(node.Nodes, source, source.RootFolder);
            }
        }

        //--------------------------------------------------------------------------------
        void m_menuProprietesConnexion_Click(object sender, EventArgs e)
        {
            TreeNode node = SelectedNode;
            CEasyQuerySource source = node != null ? node.Tag as CEasyQuerySource : null;
            if (source!= null)
            {
                if (CFormProprietesQuerySource.EditeParametres(source))
                {
                    node.Text = source.SourceName;
                    source.ClearStructure();
                    RefreshTree();
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void CTreeListeTables_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = e.Item as TreeNode;
            if ( node == null )
                return;
            ITableDefinition def = GetTableForNode(node);
            if (def != null)
            {
                IObjetDeEasyQuery objet = def.GetObjetDeEasyQueryParDefaut();
                if (objet != null)
                {
                    CDonneeDragDropObjetGraphique data = new CDonneeDragDropObjetGraphique(
                        Name,
                        objet,
                        new Point(objet.Size.Width / 2, objet.Size.Height / 2));
                    DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.None);
                }
            }
            
                        
        }




        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_bModeEdition;
            }
            set
            {
                m_bModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
