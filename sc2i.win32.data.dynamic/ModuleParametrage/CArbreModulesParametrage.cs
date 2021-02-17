using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{

    public partial class CArbreModulesParametrage : TreeView
    {

        private CContexteDonnee m_contexte = null;

        public CArbreModulesParametrage()
            : base()
        {
            InitializeComponent();
            
        }


        public void Init(CContexteDonnee contexte)
        {
            m_contexte = contexte;

            FillArbre();
        }

        public void SelectNode(CModuleParametrage module)
        {
            TreeNode node = GetNodeFor(module);
            if (node != null)
                SelectedNode = node;
        }

        private TreeNode GetNodeFor(CModuleParametrage module)
        {
            if (module == null)
                return null;
            TreeNodeCollection nodes = Nodes;
            if (nodes.Count == 1 && nodes[0].Tag == null)
                nodes = nodes[0].Nodes;
            if (module.ModuleParent != null)
            {
                TreeNode nodeParent = GetNodeFor(module.ModuleParent);
                if (nodeParent == null)
                    return null;
                AssureChilds(nodeParent);
                nodes = nodeParent.Nodes;
            }
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null && node.Tag.Equals(module))
                    return node;
            }
            return null;
        }


        private void FillArbre()
        {
            Nodes.Clear();

            // Charge tous les modules de paratmétrage de niveau 0
            CListeObjetsDonnees listeModules = new CListeObjetsDonnees(
                m_contexte,
                typeof(CModuleParametrage),
                new CFiltreData(CModuleParametrage.c_champNiveau + " = @1", 0));


            // Créer le Module Racine
            TreeNode nodeRacine = this.Nodes.Add(I.T("All Setting Modules|10001"));

            // Créé les modules de niveu 0
            foreach (CModuleParametrage module in listeModules)
            {
                TreeNode node = CreateNode(module);
                if (node != null)
                    nodeRacine.Nodes.Add(node);
            }


        }

        public TreeNode CreateNode(CModuleParametrage module)
        {
            TreeNode nodeModule = new TreeNode(module.Libelle);
            nodeModule.Tag = module;
            
            // Images modules
            nodeModule.ImageIndex = 0;
            nodeModule.SelectedImageIndex = 1;

            // Ajoute un node fils nul
            if (module.ModulesFils.Count > 0)
            {
                TreeNode nodeNull = new TreeNode("");
                nodeNull.Tag = null;
                nodeModule.Nodes.Add(nodeNull);
            }
            return nodeModule;
        }

        private void CArbreModulesParametrage_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            using (new CWaitCursor())
            {
                TreeNode node = e.Node;
                AssureChilds(node);

            }
        }

        private void AssureChilds(TreeNode node)
        {
                if (node.Nodes.Count == 1 && node.Nodes[0].Tag == null)
                {
                    node.Nodes.Clear();
                    CModuleParametrage module = node.Tag as CModuleParametrage;
                    if (module != null)
                    {
                        foreach (CModuleParametrage moduleFils in module.ModulesFils)
                        {
                            TreeNode nodeFils = CreateNode(moduleFils);
                            if (moduleFils != null)
                                node.Nodes.Add(nodeFils);
                        }
                    }

                }
        }

     
    }
}
