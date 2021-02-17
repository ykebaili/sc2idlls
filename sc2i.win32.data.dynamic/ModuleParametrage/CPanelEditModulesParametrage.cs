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
    public partial class CPanelEditModulesParametrage : UserControl, IFournisseurFiltreAdditionnel
    {
        public CPanelEditModulesParametrage()
        {
            InitializeComponent();
            CFiltreInterfaceExterne.RegisterFournisseur(this);
        }


        private CContexteDonnee m_contexte;
        public CResultAErreur InitChamps(CContexteDonnee contexte)
        {
            CResultAErreur result = CResultAErreur.True;

            m_contexte = contexte;
            m_ArbreModules.Init(contexte);


            return result;

        }

        public void SelectModule(CModuleParametrage module)
        {
            m_ArbreModules.SelectNode(module);
        }

        private void m_nouveauModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = m_ArbreModules.SelectedNode;
            if (node == null)
                return;

            CModuleParametrage moduleParent = node.Tag as CModuleParametrage;
            CModuleParametrage newModule = null;
            
            // Ajouter ici un Module 
            newModule = new CModuleParametrage(m_contexte);
            newModule.CreateNewInCurrentContexte();
            if (CFormInfosNouveauModule.EditModule(newModule))
            {
                newModule.CreateNewInCurrentContexte();

                if (moduleParent != null)
                {
                    newModule.ModuleParent = moduleParent;
                }

                TreeNode childNode = new TreeNode(newModule.Libelle);
                childNode.Tag = newModule;
                node.Nodes.Add(childNode);
                node.Expand();
            }
        }

        CModuleParametrage m_currentSelectedModule = null;
        TreeNode m_currentSelectedNode = null;
        private void m_ArbreModules_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            CModuleParametrage module = node.Tag as CModuleParametrage;

            if (module != null)
            {
                m_currentSelectedModule = module;
                m_currentSelectedNode = node;
                m_ctrlEditModuleParam.Visible = true;
                m_ctrlEditModuleParam.Init(module);
            }
            else
            {
                m_currentSelectedModule = null;
                m_currentSelectedNode = null;
                m_ctrlEditModuleParam.Visible = false;
            }

            // Affiche la liste des Relations elements
            InitListeRelationsElements(module);

        }

        private void InitListeRelationsElements(CModuleParametrage module)
        {
            m_treeItems.InitForModule(module, m_contexte);
        }

        private void m_deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = m_ArbreModules.SelectedNode;
            if (node == null)
                return;

            CModuleParametrage module = node.Tag as CModuleParametrage;

            // Supprime le Module et le noeud
            
            if (module != null)
            {
                if (MessageBox.Show(I.T("Delete the Setting Module and all its child modules ?|10007"),
                    I.T("Delete ?|10006"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    module.Delete();
                    node.Remove();
                }
                
            }
        }

        #region Gestion du Drag Drop sur la liste des Elements liés aux modules

        
        //------------------------------------------------------------------
        private void VerifiePossibiliteDragDropSurListeElements(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(CReferenceObjetDonnee)) is CReferenceObjetDonnee &&
                m_currentSelectedModule != null)
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;

        }



        #endregion

        #region Gestion du Drag Drop sur l'Arbre des Modules
        private void m_ArbreModules_DragEnter(object sender, DragEventArgs e)
        {
            VerifiePossibiliteDragDropSurArbreModules(e);
        }

        private void m_ArbreModules_DragOver(object sender, DragEventArgs e)
        {
            VerifiePossibiliteDragDropSurArbreModules(e);
        }
        
        private void m_ArbreModules_DragDrop(object sender, DragEventArgs e)
        {
            CModuleParametrage moduleDestination = null;

            Point pt = new Point(e.X, e.Y);
            pt = m_ArbreModules.PointToClient(pt);
            TreeNode nodeDestination = m_ArbreModules.GetNodeAt(pt);
            if (nodeDestination != null)
            {
                // Identifier le Module de paramétrage Sur lequel on a fait le Drop
                moduleDestination = nodeDestination.Tag as CModuleParametrage; //Celui-ci peut être null si c'est le node racine par exemple
                
            }
            
            // Si c'est un Module
            TreeNode nodeSource = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (nodeSource != null)
            {
                CModuleParametrage moduleSource = nodeSource.Tag as CModuleParametrage;
                if (moduleSource != null)
                {
                    // On ne peut pas déplacer un module su rlui-même
                    if (moduleDestination != null && moduleDestination.Id == moduleSource.Id)
                        return;

                    if (e.Effect == DragDropEffects.Copy)
                    {
                        // Faire une copipe du Module de paramétrage
                        CModuleParametrage nouveauModule = moduleSource.Clone(false) as CModuleParametrage;
                        if (nouveauModule != null)
                        {
                            nouveauModule.ModuleParent = moduleDestination;
                            TreeNode nouveauNode = m_ArbreModules.CreateNode(nouveauModule);
                            nodeDestination.Nodes.Add(nouveauNode);
                        }
                    }
                    else if (e.Effect == DragDropEffects.Move)
                    {
                        // Le module déplacé change de parent
                        moduleSource.ModuleParent = moduleDestination;
                        nodeSource.Remove();
                        nodeDestination.Nodes.Add(nodeSource);
                    }
                }
            }
            // Si c'est une relation avec un element
            else if ( e.Data.GetData(typeof(ListViewItem)) is ListViewItem )
            {
                ListViewItem itemSource = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;
                if (itemSource != null)
                {
                    CRelationElement_ModuleParametrage relationSource = itemSource.Tag as CRelationElement_ModuleParametrage;
                    if (relationSource != null)
                    {
                        if (e.Effect == DragDropEffects.Copy)
                        {
                            // Faire une copipe du Module de paramétrage
                            CRelationElement_ModuleParametrage nouvelleRelation = relationSource.Clone(false) as CRelationElement_ModuleParametrage;
                            if (nouvelleRelation != null)
                            {
                                nouvelleRelation.ModuleParametrage = moduleDestination;
                            }
                        }
                        else if (e.Effect == DragDropEffects.Move)
                        {
                            // Le module déplacé change de parent
                            relationSource.ModuleParametrage = moduleDestination;
                        }

                        InitListeRelationsElements(m_currentSelectedModule);
                    }
                }
            }
            // Si c'est une relation avec un element
            else if (e.Data.GetData(typeof(CRelationElement_ModuleParametrage)) is CRelationElement_ModuleParametrage)
            {
                CRelationElement_ModuleParametrage relationSource = e.Data.GetData(typeof(CRelationElement_ModuleParametrage)) as CRelationElement_ModuleParametrage;
                if (relationSource != null)
                {
                    if (e.Effect == DragDropEffects.Copy)
                    {
                        // Faire une copipe du Module de paramétrage
                        CRelationElement_ModuleParametrage nouvelleRelation = relationSource.Clone(false) as CRelationElement_ModuleParametrage;
                        if (nouvelleRelation != null)
                        {
                            nouvelleRelation.ModuleParametrage = moduleDestination;
                        }
                    }
                    else if (e.Effect == DragDropEffects.Move)
                    {
                        // Le module déplacé change de parent
                        relationSource.ModuleParametrage = moduleDestination;
                    }

                    InitListeRelationsElements(m_currentSelectedModule);
                }

            }

        }

        //------------------------------------------------------------------
        private void VerifiePossibiliteDragDropSurArbreModules(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(TreeNode)) is TreeNode  ||
                e.Data.GetData(typeof(ListViewItem)) is ListViewItem ||
                e.Data.GetData(typeof(CRelationElement_ModuleParametrage)) is CRelationElement_ModuleParametrage)
            {
                // Si la touche Ctrl est enfoncée on a un effet de Copier
                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void m_ArbreModules_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Item;

            if (node != null)
                DoDragDrop(node, DragDropEffects.None | DragDropEffects.Move | DragDropEffects.Copy);
        }

        #endregion

        private void m_ctrlEditModuleParam_Leave(object sender, EventArgs e)
        {
            m_ctrlEditModuleParam.MAJ_champs();
            if(m_currentSelectedModule != null)
                
                m_currentSelectedNode.Text = m_currentSelectedModule.Libelle;
            
        }

        public event EventHandler OnListElementsItemDoubleClick;
        

        public event EventHandler OrientationChanged;


        private void m_btnOrientationH_Click(object sender, EventArgs e)
        {
            m_splitContainer.Orientation = Orientation.Horizontal;
            if (OrientationChanged != null)
                OrientationChanged(this, null);

        }

        private void m_btnOrientationV_Click(object sender, EventArgs e)
        {
            m_splitContainer.Orientation = Orientation.Vertical;
            if (OrientationChanged != null)
                OrientationChanged(this, null);
        }

        public Orientation Orientation
        {
            get
            {
                return m_splitContainer.Orientation;
            }
            set
            {
                m_splitContainer.Orientation = value;
            }
        }


        public event OnChangeAlwaysVisibleModeEvent OnChangeAlwaysVisibleMode; 
        private void m_chkAlwaysVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (OnChangeAlwaysVisibleMode != null)
                OnChangeAlwaysVisibleMode(sender, m_chkAlwaysVisible.Checked);
        }

        public event EventHandler OnAutoArrangeWindow;
        private void m_btnAutoArrange_Click(object sender, EventArgs e)
        {
            if (OnAutoArrangeWindow != null)
                OnAutoArrangeWindow(sender, e);
        }

        


        #region IFournisseurFiltreAdditionnel Membres

        public CFiltreData GetFiltreForType(Type tp)
        {
            CFiltreData filtre = null;
            if (
                m_ArbreModules.SelectedNode != null &&
                m_chkAppliquerFiltre.Checked &&
                m_contexte != null &&
                typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(tp))
            {
                CModuleParametrage module = m_ArbreModules.SelectedNode.Tag as CModuleParametrage;
                if (module != null)
                {
                    CListeObjetsDonnees lst = new CListeObjetsDonnees(m_contexte, typeof(CRelationElement_ModuleParametrage));
                    lst.Filtre = new CFiltreDataAvance(
                        CRelationElement_ModuleParametrage.c_nomTable,
                        CRelationElement_ModuleParametrage.c_champTypeElement + "=@1 and "+
                        CModuleParametrage.c_nomTable+"."+CModuleParametrage.c_champCodeSystemeComplet+" like @2",
                        tp.ToString(),
                        module.CodeSystemeComplet+"%");
                    if (lst.Count != 0)
                    {
                        StringBuilder bl = new StringBuilder();
                        foreach (CRelationElement_ModuleParametrage rel in lst)
                        {
                            bl.Append(rel.IdElement);
                            bl.Append(';');
                        }
                        bl.Remove(bl.Length - 1, 1);
                        DataTable table = m_contexte.GetTableSafe(CContexteDonnee.GetNomTableForType(tp));
                        filtre = new CFiltreDataAvance(
                            CContexteDonnee.GetNomTableForType(tp),
                            table.PrimaryKey[0].ColumnName + " in {" +
                            bl.ToString() + "}");
                    }
                }
            }
            return filtre;
        }

        #endregion

        public void FillParametreAffichage(CParametreEditModulesParametrage parametre)
        {
            parametre.SplitterDistance = m_splitContainer.SplitterDistance;
            parametre.Orientation = Orientation;
            parametre.SelectionPath = new CTreeViewNodeKeeper(m_ArbreModules).SelectionPath;
        }

        public void SetParametreAffichage(CParametreEditModulesParametrage parametre)
        {
            m_splitContainer.SplitterDistance = parametre.SplitterDistance;
            Orientation = parametre.Orientation;
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    m_btnOrientationH.Checked = true;
                    break;
                case Orientation.Vertical:
                    m_btnOrientationV.Checked = true;
                    break;
                default:
                    break;
            }
            CTreeViewNodeKeeper keeper = new CTreeViewNodeKeeper(m_ArbreModules);
            keeper.SelectionPath = parametre.SelectionPath;
            keeper.Apply(m_ArbreModules);
        }

        private void CPanelEditModulesParametrage_Load(object sender, EventArgs e)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    m_btnOrientationH.Checked = true;
                    break;
                case Orientation.Vertical:
                    m_btnOrientationV.Checked = true;
                    break;
                default:
                    break;
            }
        }

        //---------------------------------------------------------------------------------------------
        private void m_treeItems_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            CRelationElement_ModuleParametrage rel = node != null ?
                node.Tag as CRelationElement_ModuleParametrage :
                null;
            if ( rel != null )
            {
                if (rel != null && rel.Row.RowState != DataRowState.Detached &&
                    rel.Row.RowState != DataRowState.Deleted)
                {
                    CObjetDonneeAIdNumerique element = rel.ElementLie;
                    if (element != null && OnListElementsItemDoubleClick != null)
                    {
                        OnListElementsItemDoubleClick(element, e);
                    }
                }
            }
        }
     
    }

    public delegate void OnChangeAlwaysVisibleModeEvent(object sender, bool bAlwaysVisible);
}
