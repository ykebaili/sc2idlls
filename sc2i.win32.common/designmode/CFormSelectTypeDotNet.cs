using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using sc2i.common;
using System.Drawing.Design;

namespace sc2i.win32.common
{
    public partial class CFormSelectTypeDotNet : Form
    {
        private Type m_type = null;
        private IServiceProvider m_provider = null;

        public CFormSelectTypeDotNet()
        {
            InitializeComponent();
        }

        //-----------------------------------------
        public static Type SelectType(IServiceProvider provider)
        {
            CFormSelectTypeDotNet form = new CFormSelectTypeDotNet();
            form.m_provider = provider;
            Type tp = null;
            if (form.ShowDialog() == DialogResult.OK)
            {
                tp = form.m_type;
            }
            form.Dispose();
            return tp;
        }



        //-----------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if ( m_arbre.SelectedNode != null && 
                m_arbre.SelectedNode.Tag is Type)
            {
                m_type = (Type)m_arbre.SelectedNode.Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        //-----------------------------------------
        private void FillArbre()
        {
            using (CWaitCursor curseur = new CWaitCursor())
            {
                m_arbre.BeginUpdate();
                m_arbre.Nodes.Clear();

                System.ComponentModel.Design.ITypeDiscoveryService service =
                 (System.ComponentModel.Design.ITypeDiscoveryService)
                  m_provider.GetService(
                        typeof(System.ComponentModel.Design.ITypeDiscoveryService)
                                        );

                if (service != null)
                {
                    System.Collections.ICollection coll = service.GetTypes(typeof(object), true);
                    Dictionary<string, TreeNode> namespacesToNode = new Dictionary<string, TreeNode>();
                    List<Type> lstTypes = new List<Type>();
                    foreach (Type tp in service.GetTypes(typeof(object), true))
                        lstTypes.Add(tp);
                    lstTypes.Sort((x, y) => x.FullName.CompareTo(y.FullName));
                    string strSearch = m_txtSearch.Text.Trim().ToUpper();
                    foreach (Type tp in lstTypes)
                    {
                        if (tp.Namespace != null)
                        {
                            if (strSearch.Length == 0 || tp.FullName.ToUpper().Contains(strSearch) )
                            {
                                TreeNode nodeParent = GetNodeNamespace(null, tp.Namespace, namespacesToNode);
                                if (nodeParent != null)
                                {
                                    TreeNode nodeType = new TreeNode(tp.Name);
                                    nodeType.Tag = tp;
                                    nodeParent.Nodes.Add(nodeType);
                                }
                            }
                        }
                    }
                    m_arbre.EndUpdate();
                    return;

                }

                List<Assembly> lstAss = new List<Assembly>();
                foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
                {
                    lstAss.Add(ass);
                }
                lstAss.Sort((x, y) => x.FullName.CompareTo(y.FullName));
                HashSet<string> lstFaits = new HashSet<string>();
                foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
                {
                    if (!lstFaits.Contains(ass.FullName))
                    {
                        string[] strParts = ass.FullName.Split(',');
                        TreeNode node = new TreeNode(strParts[0]);
                        node.Tag = ass;
                        node.Nodes.Add(new TreeNode(""));
                        m_arbre.Nodes.Add(node);
                        lstFaits.Add(ass.FullName);
                    }
                }
                m_arbre.EndUpdate();
            }
        }

        //-----------------------------------------
        private void m_arbre_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag == null)
            {
                e.Node.Nodes.Clear();
                Assembly ass = e.Node.Tag as Assembly;
                if (ass != null)
                {
                    List<Type> lstTypes = new List<Type>();
                    Dictionary<string, TreeNode> namespacesToNode = new Dictionary<string,TreeNode>();
                    foreach (Type tp in ass.GetTypes())
                        lstTypes.Add(tp);
                    lstTypes.Sort((x, y) => x.FullName.CompareTo(y.FullName));
                    foreach (Type tp in lstTypes)
                    {
                        if (tp.Namespace != null)
                        {
                            TreeNode nodeParent = GetNodeNamespace(e.Node, tp.Namespace, namespacesToNode);
                            TreeNode node = new TreeNode(tp.Name);
                            node.Tag = tp;
                            nodeParent.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        private TreeNode GetNodeNamespace(TreeNode nodeParent, string strNameSpace, Dictionary<string, TreeNode> namespacesToNode)
        {
            TreeNode node = null;
            if (namespacesToNode.TryGetValue(strNameSpace, out node))
                return node;
            string strFull = "";
            string[] strNames = strNameSpace.Split('.');
            foreach (string strName in strNames)
            {
                strFull += strName;
                if (!namespacesToNode.TryGetValue(strFull, out node))
                {
                    node = new TreeNode(strName);
                    node.Tag = strFull;
                    if (nodeParent == null)
                        m_arbre.Nodes.Add(node);
                    else
                        nodeParent.Nodes.Add(node);
                    namespacesToNode[strFull] = node;
                }
                nodeParent = node;
            }

            return nodeParent;
        }



        private void CFormSelectType_Load(object sender, EventArgs e)
        {
            FillArbre();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FillArbre();
        }

    }

    public class CSelectTypeUIEditor : UITypeEditor
    {
        /// ///////////////////////////////////////////
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
             
            Type tp = CFormSelectTypeDotNet.SelectType(provider);
            if (tp != null)
                return tp.ToString();
            return "";
        }

        /// ///////////////////////////////////////////
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
