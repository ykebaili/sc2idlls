using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.common;
using sc2i.win32.common;
using data.hotel.easyquery;
using data.hotel.easyquery.filtre;
using data.hotel.client.query;

namespace data.hotel.eastquery.win32.filtre
{
    public partial class CPanelFiltreDataHotel : UserControl
    {
        private CEasyQuery m_easyQuery = null;
        private IObjetDeEasyQuery m_table= null;
        private IDHFiltre m_filtreOriginal = null;

        //-----------------------------------------------------------------------------------
        public CPanelFiltreDataHotel()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------
        public void Init(
            CEasyQuery easyQuery,
            IDHFiltre filtre,
            CODEQTableFromDataHotel table)
        {
            m_easyQuery = easyQuery;
            m_table = table;
            m_filtreOriginal = filtre;
            InitArbre();
        }

        //-----------------------------------------------------------------------------------
        public void InitForChampFixe(
            CEasyQuery easyQuery,
            IDHFiltre filtre,
            CODEQTableFromDataHotel table,
            string strIdChamp)
        {
            m_easyQuery = easyQuery;
            m_table = table;
            m_filtreOriginal = filtre;
            InitArbre();
        }

        //-----------------------------------------------------------------------------------
        public IDHFiltre MajAndGetFiltre()
        { 
            return GetRootItem();
        }


        //-----------------------------------------------------------------------------------
        private void InitArbre()
        {
            m_arbreFiltre.BeginUpdate();
            m_arbreFiltre.Nodes.Clear();

            if (m_filtreOriginal != null)
            {
                TreeNode node = CreateNode(m_filtreOriginal);
                m_arbreFiltre.Nodes.Add(node);
            }
            m_arbreFiltre.EndUpdate();
        }

        //--------------------------------------------
        private TreeNode CreateNode(IDHFiltre filtre)
        {
            TreeNode node = new TreeNode(filtre.GetLibelle(m_table));
            node.Tag = filtre;
            CDHFiltreASousElements test = filtre as CDHFiltreASousElements;
            if (test != null)
            {
                foreach (IDHFiltre child in test.SousElements)
                {
                    TreeNode childNode = CreateNode(child);
                    node.Nodes.Add(childNode);
                }
            }
            return node;
        }

        //--------------------------------------------
        private void CFormEditeProprietesFiltreDataHotel_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //--------------------------------------------
        private IDHFiltre GetRootItem()
        {
            if (m_arbreFiltre.Nodes.Count == 0)
                return null;
            CreateHierarchie(m_arbreFiltre.Nodes[0]);
            return m_arbreFiltre.Nodes[0].Tag as IDHFiltre;
        }

        //--------------------------------------------
        private void CreateHierarchie(TreeNode node)
        {
            CDHFiltreASousElements itemLogique = node.Tag as CDHFiltreASousElements;
            if (itemLogique != null)
            {
                itemLogique.ClearSousElements();
                foreach (TreeNode child in node.Nodes)
                {
                    itemLogique.AddSousElement(child.Tag as IDHFiltre);
                    CreateHierarchie(child);
                }
            }
        }
 
        //--------------------------------------------------------------------------------------------
        private void m_menuArbre_Popup(object sender, EventArgs e)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            if (node == null && m_arbreFiltre.Nodes.Count > 0)
                return;//Pas de menu
            IDHFiltre item = node != null?node.Tag as IDHFiltre:null;
            m_menuAjouter.Visible = item is CDHFiltreASousElements || item == null;
            m_menuInsert.Visible = item != null;
            m_menuDecalerFilsVersLeHaut.Visible = node != null && (node.Parent != null ||
                node.Nodes.Count == 1);
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuAddEt_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CDHFiltreAnd));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuAddOu_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CDHFiltreOr));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuAddCondition_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CDHFiltreValeur));
        }

      

        //--------------------------------------------------------------------------------------------
        private void m_menuInsererEt_Click(object sender, EventArgs e)
        {
            InsertChild(typeof(CDHFiltreAnd));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuInsererOu_Click(object sender, EventArgs e)
        {
            InsertChild(typeof(CDHFiltreOr));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuSupprimerElementEtFils_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            if (node != null)
                node.Remove();
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuDecalerFilsVersLeHaut_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            if (node == null)
                return;
            TreeNode parent = node.Parent;
            if (parent == null)
            {
                if (node.Nodes.Count == 1)
                {
                    TreeNode nodeTmp = node.Nodes[0];
                    TreeNode newNode = new TreeNode(nodeTmp.Text);
                    newNode.Tag = nodeTmp.Tag;
                    m_arbreFiltre.Nodes.Add(newNode);
                    node.Remove();
                    
                }
            }
            else
            {
                foreach (TreeNode nodeTmp in node.Nodes)
                {
                    TreeNode newNode = new TreeNode(nodeTmp.Text);
                    newNode.Tag = nodeTmp.Tag;
                    parent.Nodes.Add(newNode);
                    node.Nodes.Remove(nodeTmp);
                }
                node.Remove();
            }

        }

        //--------------------------------------------------------------------------------------------
        private void m_menuProprietes_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            if (node != null)
            {
                if (ShowProprietes(node.Tag as IDHFiltre))
                    node.Text = ((IDHFiltre)node.Tag).GetLibelle(m_table);
            }
        }

        //--------------------------------------------------------------------------------------------
        private bool ShowProprietes(IDHFiltre item)
        {
            CDHFiltreValeur dhValeur = item as CDHFiltreValeur;
            if (dhValeur == null)
                return true;
            bool bResult = false;
            
            bResult = CFormEditeComposantDHComparaison.EditeComparaison(
                m_easyQuery,
                m_table, 
                dhValeur);
            
            return bResult;
        }

        //--------------------------------------------------------------------------------------------
        private void AddChild(Type typeComposant)
        {
            IDHFiltre item = (IDHFiltre)Activator.CreateInstance(typeComposant);
            AddChild(item, true);
        }

        //--------------------------------------------------------------------------------------------
        private void AddChild(IDHFiltre item, bool bEditerAvantAjout)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            IDHFiltre parent = node != null ? node.Tag as IDHFiltre : null;
            if (parent == null && m_arbreFiltre.Nodes.Count > 0)
                return;
            if (bEditerAvantAjout && !ShowProprietes(item))
                return;
            if (parent != null && parent is CDHFiltreASousElements)
            {
                ((CDHFiltreASousElements)parent).AddSousElement ( item );
                TreeNode newNode = CreateNode(item);
                node.Nodes.Add(newNode);
            }
            else
            {
                TreeNode newNode = CreateNode(item);
                m_arbreFiltre.Nodes.Add(newNode);
            }
        }

        //--------------------------------------------------------------------------------------------
        private void InsertChild(Type typeItem)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            if ( node == null )
                return;
            CDHFiltreASousElements newItem = (CDHFiltreASousElements)Activator.CreateInstance(typeItem);
            
            TreeNode newChild = new TreeNode(node.Text);
            newChild.Tag = node.Tag;
            
            TreeNode newNode = CreateNode(newItem);
            if (node.Parent == null)
                m_arbreFiltre.Nodes.Add(newNode);
            else
                node.Parent.Nodes.Add(newNode);
            newNode.Nodes.Add(newChild);
            node.Remove();


            
        }
            
            

        //--------------------------------------------------------------------------------------------
        private void m_arbreFiltre_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Node != null)
            {
                m_arbreFiltre.SelectedNode = e.Node;
                m_menuArbre.Show(m_arbreFiltre, new Point(e.X, e.Y));
            }   
        }

        //--------------------------------------------------------------------------------------------
        private void m_arbreFiltre_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && m_arbreFiltre.SelectedNode == null)
                m_menuArbre.Show(m_arbreFiltre, new Point(e.X, e.Y));
        }
    }

    
}
