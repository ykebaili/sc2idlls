using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using sc2i.win32.common;
using sc2i.formulaire;
using System.Collections;
using sc2i.expression;
using sc2i.drawing;
using sc2i.common;
using sc2i.process.workflow.dessin;
using sc2i.process.workflow.blocs;
using sc2i.process.workflow;
using sc2i.win32.process.workflow.bloc;
using sc2i.win32.data;
using sc2i.win32.navigation;
using sc2i.win32.data.navigation;
using sc2i.data;

namespace sc2i.win32.process.workflow
{
    public partial class CPanelEditionWorkflow : UserControl, IControlALockEdition
    {
        public CPanelEditionWorkflow()
        {
            InitializeComponent();
            RefreshTrackZoom();
            
        }


        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        //---------------------------------
        public void Init(object workflowEdite)
        {
            WorkflowEdite = (CWorkflowDessin)workflowEdite;

        }

        public CWorkflowDessin WorkflowEdite
        {
            get
            {
                return (CWorkflowDessin)m_panelWorkflow.ObjetEdite;
            }
            set
            {
                if (!DesignMode)
                {
                    m_panelWorkflow.ObjetEdite = value;
                    InitListeUndrawns();
                    InitMenuBlocs();
                }
            }
        }


        public CPanelEditionObjetGraphique Editeur
        {
            get
            {
                return m_panelWorkflow;
            }

        }


        //---------------------------------
        private void m_panelFormulaire_SelectionChanged(object sender, EventArgs e)
        {
            if (m_panelWorkflow.Selection.Count == 1)
            {
                bool bDrawInfo = false;
                m_gridProprietes.SelectedObject = m_panelWorkflow.Selection[0];
                CWorkflowEtapeDessin objetDessin = m_panelWorkflow.Selection[0] as CWorkflowEtapeDessin;
                if (objetDessin != null && objetDessin.TypeEtape != null && objetDessin.TypeEtape.Bloc != null)
                {
                    m_imageElementSelectionne.Visible = true;
                    m_imageElementSelectionne.Image = objetDessin.TypeEtape.Bloc.BlocImage;
                    bDrawInfo = true;
                }
                else
                    m_imageElementSelectionne.Visible = false;
                IWorflowDessin dessin = m_panelWorkflow.Selection[0] as IWorflowDessin;
                if (dessin != null)
                {
                    m_lblElementSelectionne.Text = dessin.Text;
                    bDrawInfo = true;
                }
                m_panelInfoSelection.Visible = bDrawInfo;
            }
            else
            {
                m_panelInfoSelection.Visible = false;
                ArrayList lst = new ArrayList();
                foreach (C2iObjetGraphique element in m_panelWorkflow.Selection)
                    lst.Add(element);
                m_gridProprietes.SelectedObjects = lst.ToArray();
            }

        }

        //---------------------------------
        private void m_gridProprietes_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_panelWorkflow.Refresh(); ;
        }

        //----------------------------------------------------------------------
        private void CPanelEditionWorkflow_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------
        /// <summary>
        /// Permet d'ajouter des objets au Serialzer de clonage
        /// </summary>
        /// <param name="type"></param>
        /// <param name="objet"></param>
        public void AddObjetForClonerSerializer(Type type, object objet)
        {
            //m_panelWorkflow.AddObjetForClonerSerializer(type, objet);
        }

        //----------------------------------------------------------------------
        private void m_btnModeSelection_CheckedChanged(object sender, EventArgs e)
        {
            SelectModeSouris();
        }

        //----------------------------------------------------------------------
        private void SelectModeSouris()
        {
            if (m_btnModeSelection.Checked)
                m_panelWorkflow.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Selection;
            if (m_btnModeZoom.Checked)
                m_panelWorkflow.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Zoom;
        }

        //----------------------------------------------------------------------
        private void m_panelFormulaire_ElementMovedOrResized(object sender, EventArgs e)
        {
        }

        //----------------------------------------------------------------------
        private void m_panelFormulaire_AfterRemoveObjetGraphique(object sender, EventArgs args)
        {
        }

        //----------------------------------------------------------------------
        private void m_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }



        //----------------------------------------------------------------------
        private void m_panelFormulaire_FrontBackChanged(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------
        private void m_btnLoad_Click(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------
        private void m_btnSave_Click(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------
        private void m_chkLien_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkLien.Checked)
            {
                Editeur.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Custom;
                ((CControlEditeWorkflow)Editeur).ModeSourisCustom = CControlEditeWorkflow.EModeSourisCustom.LienWorkflow;
            }
        }



        //----------------------------------------------------------------------
        private void m_menuBlocs_Opening(object sender, CancelEventArgs e)
        {
            if (m_menuBlocs.Items.Count == 0)
            {
                InitMenuBlocs();
            }
        }

        //----------------------------------------------------------------------
        private void InitMenuBlocs()
        {
            m_tableAssembliesCharges.Clear();
            m_menuBlocs.Items.Clear();
            foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                AddAssemblyForBlocs(ass);

        }

        //----------------------------------------------------------------------
        private HashSet<Assembly> m_tableAssembliesCharges = new HashSet<Assembly>();
        private void AddAssemblyForBlocs(Assembly ass)
        {
            if (m_tableAssembliesCharges.Contains(ass))
                return;
            m_tableAssembliesCharges.Add(ass);
            foreach (Type tp in ass.GetExportedTypes())
            {
                if (typeof(CBlocWorkflow).IsAssignableFrom(tp) && !tp.IsAbstract)
                {
                    AddMenuBloc(tp);
                }
            }
        }

        //----------------------------------------------------------------------
        private void AddMenuBloc(Type tp)
        {
            try
            {
                CBlocWorkflow bloc = Activator.CreateInstance(tp, new object[0]) as CBlocWorkflow;
                if (bloc != null)
                {
                    ToolStripMenuItem itemNewBloc = new ToolStripMenuItem(bloc.BlocName);
                    itemNewBloc.Tag = bloc;
                    itemNewBloc.Image = bloc.BlocImage;
                    itemNewBloc.Height = bloc.BlocImage != null ? bloc.BlocImage.Height : itemNewBloc.Height;
                    m_menuBlocs.Items.Add(itemNewBloc);
                    itemNewBloc.MouseDown += new MouseEventHandler(itemNewBloc_MouseDown);

                }
            }
            catch { }
        }

        //-------------------------------------------------
        void itemNewBloc_MouseDown(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CBlocWorkflow bloc = item != null ? item.Tag as CBlocWorkflow : null;
            if (bloc != null)
            {
                AddNewByDragDrop(bloc);
            }
        }

        //-------------------------------------------------
        private void AddNewByDragDrop(CBlocWorkflow bloc)
        {
            if (bloc != null)
            {
                CDonneeDragDropObjetGraphique data = new CDonneeDragDropObjetGraphique(Name, new CDummyObjetWorkflowPourCreation(bloc));
                DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        //-------------------------------------------------
        private void m_btnBlocs_Click(object sender, EventArgs e)
        {
            m_menuBlocs.Show(m_btnBlocs, new Point(m_btnBlocs.Width, 0));
        }

        //-------------------------------------------------
        private void InitListeUndrawns()
        {
            m_wndListeUndrawn.BeginUpdate();
            m_wndListeUndrawn.Items.Clear();
            if (m_gestionnaireModeEdition.ModeEdition)
            {
                if (WorkflowEdite != null && WorkflowEdite.TypeWorkflow != null)
                {
                    foreach (CTypeEtapeWorkflow etape in WorkflowEdite.TypeWorkflow.Etapes)
                    {
                        if (WorkflowEdite.GetChild(etape.IdUniversel) == null)
                        {
                            ListViewItem item = new ListViewItem(etape.Libelle);
                            item.Tag = etape;
                            m_wndListeUndrawn.Items.Add(item);
                        }
                    }
                }
            }
            m_wndListeUndrawn.EndUpdate();
            m_panelUndrawns.Visible = m_wndListeUndrawn.Items.Count > 0;
        }

        //-------------------------------------------------
        private void m_wndListeUndrawn_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem item = e.Item as ListViewItem;
            if (item == null)
                return;
            CTypeEtapeWorkflow typeEtape = item.Tag as CTypeEtapeWorkflow;
            if (typeEtape != null)
            {
                CDummyObjetWorkflowPourCreation dummy = new CDummyObjetWorkflowPourCreation(typeEtape);
                CDonneeDragDropObjetGraphique donnee = new CDonneeDragDropObjetGraphique(Name, dummy);
                DoDragDrop(donnee, DragDropEffects.Copy);
                InitListeUndrawns();
            }
        }


        //------------------------------------------------------------------
        private bool m_bIsRefreshingTrack = false;
        private void RefreshTrackZoom()
        {
            try
            {
                m_bIsRefreshingTrack = true;
                m_trackZoom.Minimum = 0;
                m_trackZoom.Maximum = 20;
                if (m_panelWorkflow.Echelle == 1)
                    m_trackZoom.Value = 10;
                else if (m_panelWorkflow.Echelle < 1)
                {
                    double fVal = (m_panelWorkflow.Echelle - m_panelWorkflow.MinZoom) * 10;
                    fVal /= (1 - m_panelWorkflow.MinZoom);
                    m_trackZoom.Value = (int)fVal;
                }
                else
                {
                    double fVal = (m_panelWorkflow.Echelle - 1) * 10;
                    fVal /= (m_panelWorkflow.MaxZoom - 1);
                    m_trackZoom.Value = (int)fVal + 10;
                }
            }
            catch { }
            m_lblZoom.Text = "x" + m_panelWorkflow.Echelle.ToString("0.#");
            m_bIsRefreshingTrack = false;
        }

        //------------------------------------------------------------------
        private void SetZoomFromTrack()
        {
            int nVal = m_trackZoom.Value;
            if (nVal == 10)
                m_panelWorkflow.Echelle = 1;
            else if (nVal < 10)
            {
                float fVal = nVal * (1 - m_panelWorkflow.MinZoom) / 10 + m_panelWorkflow.MinZoom;
                m_panelWorkflow.Echelle = fVal;
            }
            else
            {
                float fVal = (nVal - 10) * (m_panelWorkflow.MaxZoom - 1) / 10 + 1;
                m_panelWorkflow.Echelle = fVal;
            }
            m_panelWorkflow.Refresh();
        }





        //------------------------------------------------------------------
        private void m_trackZoom_ValueChanged(object sender, EventArgs e)
        {
            if (!m_bIsRefreshingTrack)
                SetZoomFromTrack();
        }

        //------------------------------------------------------------------
        private void m_btnModeZoom_CheckedChanged(object sender, EventArgs e)
        {
            SelectModeSouris();
        }

        //------------------------------------------------------------------
        private void m_panelWorkflow_EchelleChanged(object sender, EventArgs e)
        {
            RefreshTrackZoom();
        }

        //------------------------------------------------------------------
        private void m_btnDetailEtape_Click(object sender, EventArgs e)
        {
            EditeCurrentEtape();
            
        }

        //------------------------------------------------------------------
        private void EditeCurrentEtape()
        {
            if (m_gestionnaireModeEdition.ModeEdition )
            {
                CWorkflowEtapeDessin etape = m_panelWorkflow.EtapeSelectionnee;
                if (etape != null && etape.TypeEtape != null)
                {
                    System.Console.WriteLine("Edition de " + etape.TypeEtape.Libelle);
                    CBlocWorkflow bloc = etape.TypeEtape.Bloc;
                    if (CAllocateurEditeurBlocWorkflow.EditeBloc(bloc))
                    {
                        System.Console.WriteLine("Maj champ de "+etape.TypeEtape.Libelle);
                        etape.TypeEtape.Bloc = bloc;
                    }
                }
            }
        }

        //------------------------------------------------------------------
        private void m_panelWorkflow_DoubleClicSurElement(object sender, EventArgs e)
        {
            if ( !LockEdition )
                EditeCurrentEtape();
            else
            {
                CWorkflowEtapeDessin etape = m_panelWorkflow.EtapeSelectionnee;
                if (etape == null)
                    return;
                CBlocWorkflow bloc = etape.TypeEtape != null ? etape.TypeEtape.Bloc : null;
                CBlocWorkflowWorkflow blocWorkflow = bloc as CBlocWorkflowWorkflow;
                if ( blocWorkflow != null && blocWorkflow.DbKeyTypeWorkflow != null )
                {
                    CTypeWorkflow typeWorkflow = new CTypeWorkflow ( CSc2iWin32DataClient.ContexteCourant );
                    if ( typeWorkflow.ReadIfExists ( blocWorkflow.DbKeyTypeWorkflow) )
                    {
                        CFormNavigateur navigateur = CFormNavigateur.FindNavigateur(this);
                        if ( navigateur != null )
                        {
                            CReferenceTypeForm refFrm = CFormFinder.GetRefFormToEdit ( typeof(CTypeWorkflow));
                            if ( refFrm != null )
                            {
                                CFormEditionStandard frm = refFrm.GetForm ( typeWorkflow ) as CFormEditionStandard;
                                if ( frm != null )
                                    navigateur.AffichePage ( frm );
                                
                            }
                        }
                    }
                }
            }
        }

        private void m_wndListeAffectations_Resize(object sender, EventArgs e)
        {
            if (m_wndListeAffectations.Columns.Count > 0)
                m_wndListeAffectations.Columns[0].Width = m_wndListeAffectations.Size.Width - 25;
        }

        //--------------------------------------------------------------------------
        private void RefillListeModelesAffectation()
        {
            try
            {
                CListeObjetDonneeGenerique<CModeleAffectationUtilisateurs> lstModeles = new CListeObjetDonneeGenerique<CModeleAffectationUtilisateurs>(CSc2iWin32DataClient.ContexteCourant);
                if (m_txtFiltreAffectations.Text != "")
                {
                    lstModeles.Filtre = new CFiltreData(CModeleAffectationUtilisateurs.c_champLibelle + " like @1",
                        "%" + m_txtFiltreAffectations.Text + "%");
                }
                m_wndListeAffectations.BeginUpdate();
                m_wndListeAffectations.Items.Clear();
                foreach (CModeleAffectationUtilisateurs modele in lstModeles)
                {
                    ListViewItem item = new ListViewItem(modele.Libelle);
                    FillItemModeleAffectation(item, modele);
                    m_wndListeAffectations.Items.Add(item);
                }
                m_wndListeAffectations.EndUpdate();
            }
            catch { }
        }

        //--------------------------------------------------------------------------
        private void FillItemModeleAffectation(ListViewItem item, CModeleAffectationUtilisateurs modele)
        {
            item.Text = modele.Libelle;
            item.Tag = modele;
            item.ImageIndex = 0;
        }


        //--------------------------------------------------------------------------
        private void m_lnkAddModeleAffectation_LinkClicked(object sender, EventArgs e)
        {
            CModeleAffectationUtilisateurs modele = new CModeleAffectationUtilisateurs(CSc2iWin32DataClient.ContexteCourant);
            modele.CreateNew();
            modele.Libelle = m_txtFiltreAffectations.Text;
            if (CFormEditeModeleAffectationPopup.EditeModele(modele))
            {
                ListViewItem item = new ListViewItem ();
                FillItemModeleAffectation ( item, modele );
                m_wndListeAffectations.Items.Add ( item );
                item.Selected = true;
                m_wndListeAffectations.EnsureVisible ( item.Index );
            }
        }

        //--------------------------------------------------------------------------
        private void m_lnkEditModeleAffectation_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeAffectations.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeAffectations.SelectedItems[0];
                CModeleAffectationUtilisateurs modele = item.Tag as CModeleAffectationUtilisateurs;
                if (modele != null)
                {
                    if (CFormEditeModeleAffectationPopup.EditeModele(modele))
                        FillItemModeleAffectation(item, modele);
                }
            }
        }

        //--------------------------------------------------------------------------
        private void m_lnkDeleteModeleAffectation_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeAffectations.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeAffectations.SelectedItems[0];
                CModeleAffectationUtilisateurs modele = item.Tag as CModeleAffectationUtilisateurs;
                if (modele != null)
                {
                    if (MessageBox.Show(I.T("Delete assignment template @1 ?|20083", modele.Libelle),
                        "",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CResultAErreur result = modele.Delete();
                        if (!result)
                        {
                            CFormAlerte.Afficher(result.Erreur);
                        }
                        else
                            m_wndListeAffectations.Items.Remove(item);
                    }
                }
            }
        }


        private void m_btnSearchAffectation_Click(object sender, EventArgs e)
        {

        }

        private void m_wndListeAffectations_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem item = e.Item as ListViewItem;
            if (item == null)
                return;
            CModeleAffectationUtilisateurs modele = item.Tag as CModeleAffectationUtilisateurs;
            if ( modele != null )
                DoDragDrop ( new CReferenceObjetDonneeDragDropData ( modele ), DragDropEffects.Link);
        }

        private void m_panelWorkflow_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                RefillListeModelesAffectation();
        }

        private void m_trackZoom_Scroll(object sender, EventArgs e)
        {

        }



        
    }
}