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
using futurocom.easyquery.CAML;
using futurocom.win32.easyquery.CAML;

namespace futurocom.win32.easyquery
{
    public partial class CPanelFiltreCAML : UserControl
    {
        private CEasyQuery m_easyQuery = null;
        private CCAMLQuery m_query = new CCAMLQuery();
        private IEnumerable<CCAMLItemField> m_fields = new List<CCAMLItemField>();

        //-----------------------------------------------------------------------------------
        public CPanelFiltreCAML()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------
        public void Init(
            CEasyQuery easyQuery,
            CCAMLQuery query, 
            IEnumerable<CCAMLItemField> fields)
        {
            m_easyQuery = easyQuery;
            m_fields = fields;
            if (query != null)
            {
                m_query = CCloner2iSerializable.Clone(query) as CCAMLQuery;
            }
            else
                m_query = new CCAMLQuery();
            InitArbre();
        }

        //-----------------------------------------------------------------------------------
        public CResultAErreurType<CCAMLQuery> MajChamps()
        {
            CResultAErreurType<CCAMLQuery> res = new CResultAErreurType<CCAMLQuery>();
            m_query.RootItem = GetRootItem();
            res.DataType = m_query;
            return res;
        }


        //-----------------------------------------------------------------------------------
        private void InitArbre()
        {
            m_arbreFiltre.BeginUpdate();
            m_arbreFiltre.Nodes.Clear();
            if (m_query.RootItem != null)
            {
                TreeNode node = CreateNode(m_query.RootItem);
                m_arbreFiltre.Nodes.Add(node);
            }
            m_arbreFiltre.EndUpdate();
        }

        //--------------------------------------------
        private TreeNode CreateNode(CCAMLItem item)
        {
            TreeNode node = new TreeNode(item.Libelle);
            node.Tag = item;
            CCAMLItemLogical log = item as CCAMLItemLogical;
            if (log != null)
            {
                foreach (CCAMLItem child in log.Childs)
                {
                    TreeNode childNode = CreateNode(child);
                    node.Nodes.Add(childNode);
                }
            }
            return node;
        }

        //--------------------------------------------
        private void CFormEditeProprietesFiltreCAML_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //--------------------------------------------
        private CCAMLItem GetRootItem()
        {
            if (m_arbreFiltre.Nodes.Count == 0)
                return null;
            CreateHierarchie(m_arbreFiltre.Nodes[0]);
            return m_arbreFiltre.Nodes[0].Tag as CCAMLItem;
        }

        //--------------------------------------------
        private void CreateHierarchie(TreeNode node)
        {
            CCAMLItemLogical itemLogique = node.Tag as CCAMLItemLogical;
            if (itemLogique != null)
            {
                itemLogique.ClearChilds();
                foreach (TreeNode child in node.Nodes)
                {
                    itemLogique.AddChild(child.Tag as CCAMLItem);
                    CreateHierarchie(child);
                }
            }
        }
        


        //--------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            /*if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }
            
            m_objetFiltre.NomFinal = m_txtNomTable.Text;

            m_query.RootItem = GetRootItem();
            m_objetFiltre.CAMLQuery = CCloner2iSerializable.Clone(m_query) as CCAMLQuery;

            List<CColonneEQCalculee> lst = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                lst.Add(col);
            m_objetFiltre.ColonnesCalculees = lst;
            DialogResult = DialogResult.OK;
            Close();*/
        }

        //--------------------------------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            /*DialogResult = DialogResult.Cancel;
            Close();*/
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuArbre_Popup(object sender, EventArgs e)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            if (node == null && m_arbreFiltre.Nodes.Count > 0)
                return;//Pas de menu
            CCAMLItem item = node != null?node.Tag as CCAMLItem:null;
            m_menuAjouter.Visible = item is CCAMLItemLogical || item == null;
            m_menuInsert.Visible = item != null;
            m_menuDecalerFilsVersLeHaut.Visible = node != null && (node.Parent != null ||
                node.Nodes.Count == 1);
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuAddEt_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CCAMLItemAnd));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuAddOu_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CCAMLItemOr));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuAddCondition_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CCAMLItemComparaison));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuEstNull_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CCAMLItemNull));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuInsererEt_Click(object sender, EventArgs e)
        {
            InsertChild(typeof(CCAMLItemAnd));
        }

        //--------------------------------------------------------------------------------------------
        private void m_menuInsererOu_Click(object sender, EventArgs e)
        {
            InsertChild(typeof(CCAMLItemOr));
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
                if (ShowProprietes(node.Tag as CCAMLItem))
                    node.Text = ((CCAMLItem)node.Tag).Libelle;
            }
        }

        //--------------------------------------------------------------------------------------------
        private bool ShowProprietes(CCAMLItem item)
        {
            if (item is CCAMLItemLogical)
                return true;
            bool bResult = false;
            if (item is CCAMLItemComparaison)
            {
                bResult = CFormEditeComposantCAMLComparaison.EditeComparaison(
                    m_easyQuery,
                    m_fields, 
                    item as CCAMLItemComparaison);
            }
            if (item is CCAMLItemNull)
            {
                bResult = CFormEditeComposantCAMLNull.EditeNull(
                    m_easyQuery,
                    m_fields, 
                    item as CCAMLItemNull);
            }
            return bResult;
        }

        //--------------------------------------------------------------------------------------------
        private void AddChild(Type typeComposant)
        {
            CCAMLItem item = (CCAMLItem)Activator.CreateInstance(typeComposant);
            AddChild(item, true);
        }

        //--------------------------------------------------------------------------------------------
        private void AddChild(CCAMLItem item, bool bEditerAvantAjout)
        {
            TreeNode node = m_arbreFiltre.SelectedNode;
            CCAMLItemLogical parent = node != null ? node.Tag as CCAMLItemLogical : null;
            if (parent == null && m_arbreFiltre.Nodes.Count > 0)
                return;
            if (bEditerAvantAjout && !ShowProprietes(item))
                return;
            if (parent != null)
            {
                parent.AddChild ( item );
                TreeNode newNode = CreateNode(item);
                node.Nodes.Add(newNode);
            }
            else
            {
                m_query.RootItem = item;
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
            CCAMLItemLogical newItem = (CCAMLItemLogical)Activator.CreateInstance(typeItem);
            
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
