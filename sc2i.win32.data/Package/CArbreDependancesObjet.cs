using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.data;
using sc2i.common;
using sc2i.data.Package;
using System.Threading;
using sc2i.win32.common;
using sc2i.data.Package;

namespace sc2i.win32.data.Package
{
    public partial class CArbreDependancesObjet : UserControl
    {
        private CObjetDonnee m_objetRacine = null;
        private CEntitiesManager m_entitesManager = null;

        private Stack<CObjetDonnee> m_pileObjets = new Stack<CObjetDonnee>();

        private List<CObjetDonnee> m_listeObjetsAnalyses = new List<CObjetDonnee>();

        private static CConfigurationRechercheEntites m_configParDefaut = null;


        public CArbreDependancesObjet()
        {
            InitializeComponent();
        }

        //----------------------------------------------------
        public void Init ( CObjetDonnee objetRacine )
        {
            m_entitesManager = new CEntitiesManager(objetRacine.ContexteDonnee.IdSession);
            m_entitesManager.IndicateurProgression = m_lblProgression;
            if ( m_configParDefaut != null )
                m_entitesManager.ConfigurationRecherche = CCloner2iSerializable.CloneGeneric<CConfigurationRechercheEntites>(m_configParDefaut);
            m_objetRacine = objetRacine;
            m_pileObjets.Push(objetRacine);
            ShowObjetCourant();
        }

        //----------------------------------------------------
        private void ShowObjetCourant()
        {
            m_listeObjetsAnalyses.Clear();
            m_listeObjetsAnalyses.Add(m_pileObjets.Peek());
            m_panelTitres.SuspendDrawing();
            m_panelTitres.ClearAndDisposeControls();
            int nHeight = 0;
            foreach ( CObjetDonnee objet in m_pileObjets )
            {
                LinkLabel linkObjet = new LinkLabel();
                linkObjet.Text = DynamicClassAttribute.GetNomConvivial(objet.GetType()) + "-" + objet.DescriptionElement;
                if (nHeight == 0)
                    linkObjet.BackColor = m_arbre.BackColor;
                m_panelTitres.Controls.Add(linkObjet);
                linkObjet.Dock = DockStyle.Top;
                linkObjet.Tag = objet;
                linkObjet.AutoSize = true;
                nHeight += linkObjet.Height;
                linkObjet.SendToBack();
                linkObjet.Click += linkObjet_Click;
            }
            m_panelTitres.Height = nHeight;
            m_panelTitres.ResumeDrawing();
            RemplirArbre();
        }

        //-----------------------------------------------------------------------------------
        private Thread m_threadRemplissage = null;
        private void RemplirArbre()
        {
            CTreeViewNodeKeeper keeper = new CTreeViewNodeKeeper();
            HashSet<Type> setOpenedTypes = new HashSet<Type>();
            foreach (TreeNode node in m_arbre.Nodes)
                if (node.IsExpanded && node.Tag is Type)
                    setOpenedTypes.Add((Type)node.Tag);
            m_arbre.Nodes.Clear();
            for (int n = m_treeImages.Images.Count - 1; n >= 3; n--)
                m_treeImages.Images.RemoveAt(n);
                if (m_threadRemplissage != null)
                    m_threadRemplissage.Abort();
                m_threadRemplissage = new Thread(() =>
                {
                    Invoke((MethodInvoker)delegate
                    {
                        m_panelProgression.Visible = true;
                        keeper.KeepNodes ( m_arbre.Nodes );
                        
                    });
                    try
                    {
                        m_lblProgression.Init();
                        m_entitesManager.GetAllDependances(m_listeObjetsAnalyses, OnFournisseurTermine);
                    }
                    finally
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            m_arbre.EndUpdate();
                            m_panelProgression.Visible = false;
                            keeper.Apply(m_arbre.Nodes);
                        });
                        foreach ( TreeNode node in m_arbre.Nodes )
                        {
                            if ( node.Tag is Type && setOpenedTypes.Contains((Type)node.Tag))
                                node.Expand();
                        }
                        SortArbre();
                        AutoCheckParent();
                        m_threadRemplissage = null;
                    }
                });
            m_threadRemplissage.Start();
        }

        //-----------------------------------------------------------------------------------
        private void SortArbre()
        {
            Invoke((MethodInvoker)delegate
            {
                m_arbre.Sort();
            });
        }

        //-----------------------------------------------------------------------------------
        private void AutoCheckParent ( )
        {
            foreach (TreeNode node in m_arbre.Nodes)
            {
                bool bAllChecked = true;
                foreach (TreeNode child in node.Nodes)
                    if (!child.Checked)
                    {
                        bAllChecked = false;
                        break;
                    }
                node.Checked = bAllChecked;
            }
        }

        //----------------------------------
        private class CLockerArbre { }

        //-----------------------------------------------------------------------------------
        public void OnFournisseurTermine(HashSet<CReferenceObjetDependant> setDependances)
        {
            lock (typeof(CLockerArbre))
            {
                List<TreeNode> nodesTypeToCreate = new List<TreeNode>();
                Dictionary<TreeNode, List<TreeNode>> nodesObjetToCreate = new Dictionary<TreeNode, List<TreeNode>>();
                Dictionary<Type, TreeNode> dicTypeToNode = new Dictionary<Type, TreeNode>();
                foreach (TreeNode node in m_arbre.Nodes)
                {
                    if (node.Tag is Type)
                        dicTypeToNode[(Type)node.Tag] = node;
                }
                m_lblProgression.SetInfo(I.T("Reading @1 element(s)|20014", setDependances.Count.ToString()));
                List<CObjetDonnee> lstObjets = CReferenceObjetDonnee.GetObjets(setDependances, m_objetRacine.ContexteDonnee);
                m_lblProgression.SetInfo(I.T("Adding @1 element(s)|20013", setDependances.Count.ToString()));
                foreach (CObjetDonnee objet in lstObjets)
                {
                    if (objet == null)
                        continue;
                    Type tpObjet = objet.GetType();
                    TreeNode nodeType = null;
                    if (!dicTypeToNode.TryGetValue(tpObjet, out nodeType))
                    {
                        nodeType = new TreeNode();
                        FillNodeType(nodeType, tpObjet);
                        dicTypeToNode[tpObjet] = nodeType;
                        nodesTypeToCreate.Add(nodeType);
                    }
                    bool bExiste = false;
                    if (objet != null)
                    {
                        foreach (TreeNode node in nodeType.Nodes)
                        {
                            CObjetDonnee objetEx = node.Tag as CObjetDonnee;

                            if (objetEx != null && objetEx.Equals(objet))
                            {
                                bExiste = true;
                                break;
                            }
                        }
                        if (!bExiste)
                        {
                            TreeNode nodeObjet = new TreeNode();
                            FillNodeObjet(nodeObjet, objet);
                            List<TreeNode> lstNodes = null;
                            if ( !nodesObjetToCreate.TryGetValue(nodeType, out lstNodes ) )
                            {
                                lstNodes = new List<TreeNode>();
                                nodesObjetToCreate[nodeType] = lstNodes;
                            }
                            lstNodes.Add(nodeObjet);
                        }
                    }
                }
                Invoke((MethodInvoker)delegate
                {
                    m_arbre.BeginUpdate();
                    foreach (TreeNode node in nodesTypeToCreate)
                        m_arbre.Nodes.Add(node);
                    foreach (KeyValuePair<TreeNode, List<TreeNode>> kv in nodesObjetToCreate)
                        foreach ( TreeNode nodeFils in kv.Value ) 
                            kv.Key.Nodes.Add(nodeFils);
                    m_arbre.EndUpdate();
                });

            }
        }

        //-----------------------------------------------------------------------------------
        private void FillNodeType ( TreeNode node, Type tp )
        {
            node.ImageIndex = 0;
            node.Tag = tp;
            node.Text = DynamicClassAttribute.GetNomConvivial(tp);
            Image img = DynamicClassAttribute.GetImage(tp);
            if ( img != null )
            {
                m_treeImages.Images.Add(img);
                node.ImageIndex = m_treeImages.Images.Count - 1;
            }
            node.SelectedImageIndex = node.ImageIndex;
            node.Checked= false;
        }

        //-----------------------------------------------------------------------------------
        private void FillNodeObjet(TreeNode node, CObjetDonnee objet)
        {
            node.ImageIndex = 1;
            node.Tag = objet;
            node.Text = objet.DescriptionElement;
            COptionRechercheType option = m_entitesManager.ConfigurationRecherche.GetOption(objet.GetType());
            if (m_listeObjetsAnalyses.Contains(objet) || (option!=null && option.RecursiveSearch))
            {
                node.ImageIndex = 2;
                node.Checked = true;
            }
            else
                node.Checked = false;
            node.SelectedImageIndex = node.ImageIndex;
            
        }

        //-----------------------------------------------------------------------------------
        void linkObjet_Click(object sender, EventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            CObjetDonnee objet = link != null ? link.Tag as CObjetDonnee : null;
            if ( objet != null )
            {
                while (m_pileObjets.Count > 1)
                {
                    if (m_pileObjets.Peek().Equals(objet))
                        return;
                    m_pileObjets.Pop();
                }
            }
            ShowObjetCourant();
            
        }

        

        //---------------------------------------------------------------------
        private void m_menuVoirDependances_Click(object sender, EventArgs e)
        {
            TreeNode node = m_arbre.SelectedNode;
            CObjetDonnee objet = node != null ? node.Tag as CObjetDonnee : null;
            m_pileObjets.Push(objet);
            ShowObjetCourant();

        }

        //---------------------------------------------------------------------
        private void m_menuArbre_Opening(object sender, CancelEventArgs e)
        {
            if (m_arbre.SelectedNode == null)
                e.Cancel = true;
            else
            {
                CObjetDonnee objet = m_arbre.SelectedNode.Tag as CObjetDonnee;
                if (objet != null)
                {
                    m_menuIgnorerCeType.Visible = false;
                    m_menuAnalyserAutomatiquementCeType.Visible = false;
                    m_menuAfficherEntite.Visible = true;
                    m_menuVoirDependances.Visible = true;
                }
                Type tp = m_arbre.SelectedNode.Tag as Type;
                if ( tp != null )
                {
                    m_menuIgnorerCeType.Visible = true;
                    m_menuAnalyserAutomatiquementCeType.Visible = true;
                    m_menuAfficherEntite.Visible = false;
                    m_menuVoirDependances.Visible = false;
                    m_menuIgnorerCeType.Checked = m_entitesManager.ConfigurationRecherche.IsIgnore(tp);
                    COptionRechercheType option = m_entitesManager.ConfigurationRecherche.GetOption(tp);
                    m_menuAnalyserAutomatiquementCeType.Checked = option != null && option.RecursiveSearch;
                }
            }
        }

        //-------------------------------------------------------------------------------
        private void m_arbre_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_arbre.SelectedNode = e.Node;
        }

        //-------------------------------------------------------------------------------
        private void m_menuAfficherEntite_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------------
        private void m_btnRefresh_Click(object sender, EventArgs e)
        {
            m_listeObjetsAnalyses.Clear();
            m_listeObjetsAnalyses.Add(m_objetRacine);
            AddCheckedElement(m_arbre.Nodes, false);
            RemplirArbre();
        }

        //-------------------------------------------------------------------------------
        private void AddCheckedElement ( TreeNodeCollection nodes, bool bForceChecked )
        {
            foreach ( TreeNode node in nodes )
            {
                if ( node.Checked || bForceChecked )
                {
                    CObjetDonnee objet = node.Tag as CObjetDonnee;
                    if (objet != null)
                        m_listeObjetsAnalyses.Add(objet);
                }
                AddCheckedElement(node.Nodes, node.Checked || bForceChecked);
            }
        }

        //------------------------------------------------------------
        private void m_btnParametres_Click(object sender, EventArgs e)
        {
            CConfigurationRechercheEntites config = CFormParametresRecherche.EditeConfiguration(m_entitesManager.ConfigurationRecherche);
            if (config != null)
                m_entitesManager.ConfigurationRecherche = config;
        }


        //------------------------------------------------------------
        private void m_menuIgnorerCeType_Click(object sender, EventArgs e)
        {
            Type tp = m_arbre.SelectedNode != null ?
                m_arbre.SelectedNode.Tag as Type : null;
            if (tp != null)
            {
                if (m_entitesManager.ConfigurationRecherche.IsIgnore(tp))
                    m_entitesManager.ConfigurationRecherche.RemoveTypeIgnore(tp);
                else
                    m_entitesManager.ConfigurationRecherche.AddTypeIgnore(tp);
            }
        }

        //------------------------------------------------------------
        private void m_menuAnalyserAutomatiquementCeType_Click(object sender, EventArgs e)
        {
            Type tp = m_arbre.SelectedNode != null ?
                m_arbre.SelectedNode.Tag as Type : null;
            if (tp != null)
            {
                COptionRechercheType option = m_entitesManager.ConfigurationRecherche.GetOption(tp);
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item.Checked)
                {
                    if (option == null)
                    {
                        option = new COptionRechercheType(tp);
                        m_entitesManager.ConfigurationRecherche.AddOption(option);
                    }
                    option.RecursiveSearch = true;
                }
                else
                    m_entitesManager.ConfigurationRecherche.RemoveOptions(tp);
            }
        }

        private void m_spinner_MouseUp(object sender, MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Right && m_threadRemplissage != null )
            {
                if ( MessageBox.Show(I.T("Stop search ?|"),
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes )
                {
                    m_threadRemplissage.Abort();
                    m_panelProgression.Visible = false;
                    m_arbre.EndUpdate();
                }
            }
        }
        
    }
}
