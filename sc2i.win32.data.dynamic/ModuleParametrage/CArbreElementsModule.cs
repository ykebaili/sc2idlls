using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.common;
using sc2i.win32.common;
using System.Drawing;

namespace sc2i.win32.data.dynamic.ModuleParametrage
{
    public class CArbreElementsModule : TreeView
    {
        private ImageList m_imagesArbre;
        private System.ComponentModel.IContainer components;
        private CModuleParametrage m_moduleAffiche = null;
        private ContextMenuStrip m_menuElementsLies;
        private ToolStripMenuItem m_menuSupprimerRelationElement;
        private CContexteDonnee m_contexte = null;
        //---------------------------------------
        public CArbreElementsModule()
        {
            InitializeComponent();
            m_menuSupprimerRelationElement.Click += new EventHandler(m_menuSupprimerRelationElement_Click);
        }

       

        //---------------------------------------
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CArbreElementsModule));
            this.m_imagesArbre = new System.Windows.Forms.ImageList(this.components);
            this.m_menuElementsLies = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuSupprimerRelationElement = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuElementsLies.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_imagesArbre
            // 
            this.m_imagesArbre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesArbre.ImageStream")));
            this.m_imagesArbre.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesArbre.Images.SetKeyName(0, "Simplefolder.png");
            this.m_imagesArbre.Images.SetKeyName(1, "GenericObject16.png");
            // 
            // m_menuElementsLies
            // 
            this.m_menuElementsLies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuSupprimerRelationElement});
            this.m_menuElementsLies.Name = "m_menuElementsLies";
            this.m_menuElementsLies.ShowImageMargin = false;
            this.m_menuElementsLies.Size = new System.Drawing.Size(216, 26);
            this.m_menuElementsLies.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuElementsLies_Opening);
            // 
            // m_menuSupprimerRelationElement
            // 
            this.m_menuSupprimerRelationElement.Name = "m_menuSupprimerRelationElement";
            this.m_menuSupprimerRelationElement.Size = new System.Drawing.Size(215, 22);
            this.m_menuSupprimerRelationElement.Text = "Remove element from list|10004";
            // 
            // CArbreElementsModule
            // 
            this.AllowDrop = true;
            this.ContextMenuStrip = this.m_menuElementsLies;
            this.ImageIndex = 0;
            this.ImageList = this.m_imagesArbre;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CArbreElementsModule_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CArbreElementsModule_DragEnter);
            this.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CArbreElementsModule_NodeMouseClick);
            this.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.CArbreElementsModule_ItemDrag);
            this.m_menuElementsLies.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //---------------------------------------
        public void InitForModule(CModuleParametrage module, CContexteDonnee contexte)
        {
            CTreeViewNodeKeeper keeper = new CTreeViewNodeKeeper(this);
            m_contexte = contexte;
            m_moduleAffiche = module;
            BeginUpdate();
            Nodes.Clear();
            Dictionary<string, List<CRelationElement_ModuleParametrage>> dicTypeToObjets = new Dictionary<string, List<CRelationElement_ModuleParametrage>>();
            List<string> lstTypes = new List<string>();
            if (m_moduleAffiche != null)
            {
                foreach (CRelationElement_ModuleParametrage rel in m_moduleAffiche.RelationsElements)
                {
                    if (rel.ElementLie != null)
                    {
                        string strType = DynamicClassAttribute.GetNomConvivial(rel.ElementLie.GetType());
                        List<CRelationElement_ModuleParametrage> lst = null;
                        if (!dicTypeToObjets.TryGetValue(strType, out lst))
                        {
                            lstTypes.Add(strType);
                            lst = new List<CRelationElement_ModuleParametrage>();
                            dicTypeToObjets[strType] = lst;
                        }
                        lst.Add(rel);
                    }
                }
                lstTypes.Sort();
                foreach (string strType in lstTypes)
                {
                    TreeNode nodeType = new TreeNode(strType);
                    nodeType.SelectedImageIndex = 0;
                    nodeType.ImageIndex = 0;
                    nodeType.Tag = null;
                    Nodes.Add(nodeType);
                    List<CRelationElement_ModuleParametrage> lst = null;
                    if (dicTypeToObjets.TryGetValue(strType, out lst))
                    {
                        foreach (CRelationElement_ModuleParametrage rel in lst)
                        {
                            CObjetDonnee obj = rel.ElementLie;
                            string strDesc = DescriptionFieldAttribute.GetDescription(obj);
                            if (strDesc.Trim().Length == 0)
                                strDesc = obj.DescriptionElement;
                            TreeNode node = new TreeNode(strDesc);
                            node.Tag = rel;
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                            nodeType.Nodes.Add(node);
                        }
                    }
                }
            }
            EndUpdate();
            keeper.Apply(this);
        }

       

        //------------------------------------------------------------------------------
        private void CArbreElementsModule_DragEnter(object sender, DragEventArgs e)
        {
            VerifiePossibiliteDragDropSurListeElements(e);
        }

        //------------------------------------------------------------------
        private void VerifiePossibiliteDragDropSurListeElements(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(CReferenceObjetDonnee)) is CReferenceObjetDonnee &&
                m_moduleAffiche != null)
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;

        }

        //------------------------------------------------------------------
        private void CArbreElementsModule_DragDrop(object sender, DragEventArgs e)
        {
            List<CReferenceObjetDonnee> lst = new List<CReferenceObjetDonnee>();
            CReferenceObjetDonnee[] refs = e.Data.GetData(typeof(CReferenceObjetDonnee[])) as CReferenceObjetDonnee[];
            if (refs != null)
                lst.AddRange(refs);
            else
            {
                CReferenceObjetDonnee refSeule = e.Data.GetData(typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
                if (refSeule != null)
                    lst.Add(refSeule);
            }
            foreach (CReferenceObjetDonnee data in lst)
            {
                // Créer ici un nouvelle relation Element_ModuleParametrage, si elle n'éxiste pas déjà
                CObjetDonneeAIdNumerique element = data.GetObjet(m_contexte) as CObjetDonneeAIdNumerique;
                if (element != null && m_moduleAffiche != null)
                {
                    CRelationElement_ModuleParametrage newRelation = new CRelationElement_ModuleParametrage(m_contexte);
                    if (!newRelation.ReadIfExists(new CFiltreData(
                        CModuleParametrage.c_champId + " = @1 and " +
                        CRelationElement_ModuleParametrage.c_champTypeElement + " = @2 and " +
                        CRelationElement_ModuleParametrage.c_champIdElement + " = @3",
                        m_moduleAffiche.Id,
                        element.GetType().ToString(),
                        element.Id)))
                    {
                        // La relation n'éxiste pas il faut la créer
                        newRelation.CreateNewInCurrentContexte();
                        newRelation.ElementLie = element;
                        newRelation.ModuleParametrage = m_moduleAffiche;

                        InitForModule(m_moduleAffiche, m_contexte);
                    }
                }

            }
        }

        public event EventHandler OnListElementsItemDoubleClick;

        //------------------------------------------------------------------------------
        private void CArbreElementsModule_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = e.Item as TreeNode;
            CRelationElement_ModuleParametrage rel = node != null ?
                node.Tag as CRelationElement_ModuleParametrage :
                null;
            if ( rel != null )
                DoDragDrop ( rel, DragDropEffects.None | DragDropEffects.Move | DragDropEffects.Copy );
        }


        //------------------------------------------------------------------------------
        private void m_menuElementsLies_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode node = SelectedNode;
            if (node == null || node.Tag == null)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        //------------------------------------------------------------------------------
        void m_menuSupprimerRelationElement_Click(object sender, EventArgs e)
        {
            TreeNode node = SelectedNode;
            CRelationElement_ModuleParametrage rel = node != null ?
                node.Tag as CRelationElement_ModuleParametrage :
                null;
            if ( rel != null )
            {
                if (rel != null)
                {
                    rel.Delete();
                    node.Remove();
                }
            }
        }

        private void CArbreElementsModule_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
                SelectedNode = e.Node;
        }
            
    }
}
