using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.win32.common;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using sc2i.drawing;
using sc2i.common;
using sc2i.process.workflow;
using sc2i.process.workflow.dessin;
using sc2i.process.workflow.blocs;
using sc2i.win32.data;
using sc2i.win32.data.navigation;
using sc2i.win32.navigation;
using sc2i.data;

namespace sc2i.win32.process.workflow
{
    public class CControlEditeWorkflow : CPanelEditionObjetGraphique
    {
        public CControlEditeWorkflow()
            : base()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        public enum EModeSourisCustom
        {
            LienWorkflow
        }

        public EModeSourisCustom ModeSourisCustom { get; set; }

        //--------------------------------------------------------------------
        protected override void LoadCurseurAdapté()
        {
            if (ModeSouris != EModeSouris.Custom)
            {
                base.LoadCurseurAdapté();
            }

        }

        //--------------------------------------------------------------------
        public void Init ( CWorkflowDessin dessin )
        {
            ObjetEdite = dessin;
        }

        //-----------------------------------------------------------------
        public CWorkflowDessin DessinWorkflow
        {
            get{
                return ObjetEdite as CWorkflowDessin;
            }
        }


        //-----------------------------------------------------------------
        private CWorkflowEtapeDessin m_objetStartJoin = null;
        private CWorkflowEtapeDessin m_objetEndJoin = null;
        private CWorkflowEtapeDessin m_etapeStartLienACreer = null;
        private CWorkflowEtapeDessin m_etapeEndLienACreer = null;
        private Rectangle m_rectLien = Rectangle.Empty;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip m_menuRClick;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem m_menuEditWorkflow;
        private ContextMenuStrip m_menuCreateLien;
        private ToolStripMenuItem m_menuStartPoint;
        private Rectangle m_rectStartLien = Rectangle.Empty;
        public override void OnMouseDownNonStandard(object sender, MouseEventArgs e)
        {
            m_objetStartJoin = null;
            if (
                (ModeSourisCustom == EModeSourisCustom.LienWorkflow )
                && e.Button == MouseButtons.Left)
            {
                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                m_objetStartJoin = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CWorkflowEtapeDessin;
                if (m_objetStartJoin != null)
                {
                    Rectangle rctStart = m_objetStartJoin.RectangleAbsolu;
                    rctStart = new Rectangle(GetDisplayPointFromLogical(rctStart.Location),
                        GetDisplaySizeFromLogical(rctStart.Size));
                    m_rectStartLien = rctStart;
                    m_rectLien = m_rectStartLien;
                }
            }
        }

        /// //////////////////////////////////////////////////
        protected override void Editeur_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (ObjetEdite == null)
                return;
            if (LockEdition)
                return;

            if (e.Data.GetDataPresent(typeof(CReferenceObjetDonnee)))
            {
                CReferenceObjetDonnee reference = e.Data.GetData(typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
                if (reference.TypeObjet == typeof(CModeleAffectationUtilisateurs))
                {
                    Point pt = PointToClient(new Point(e.X, e.Y));
                    Point ptLogique = GetLogicalPointFromDisplay(pt);
                    CWorkflowEtapeDessin dessinEtape = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CWorkflowEtapeDessin;
                    if (dessinEtape != null)
                    {
                        CModeleAffectationUtilisateurs modele = reference.GetObjet(CSc2iWin32DataClient.ContexteCourant) as CModeleAffectationUtilisateurs;
                        if (modele != null)
                        {
                            CParametresAffectationEtape parametres = modele.ParametresAffectation;
                            if (parametres != null)
                            {
                                CParametresInitialisationEtape parametre = dessinEtape.Initializations;
                                CParametresAffectationEtape parAff = parametre.Affectations;
                                if (parAff != null)
                                {
                                    if (MessageBox.Show(I.T("Replace current assignments(Yes) or add new assignemnts(No) ?|20135"), "",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        parAff = new CParametresAffectationEtape();
                                    }
                                    parAff.AddFormules(parametres.Formules);
                                    parametre.Affectations = parAff;
                                    dessinEtape.Initializations = parametre;
                                    e.Effect = DragDropEffects.Link;
                                    Refresh();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            List<CDonneeDragDropObjetGraphique> datas = GetDragDropData(e.Data);

            if (datas == null || datas.Count == 0)
                return;

            List<I2iObjetGraphique> candidats = new List<I2iObjetGraphique>();
            foreach (CRectangleDragForObjetGraphique rct in RectsDrags)
                candidats.Add(rct.ObjetGraphique);

            Point ptLocal = GetLogicalPointFromDisplay(PointToClient(new Point(e.X, e.Y)));
            I2iObjetGraphique parent = ObjetEdite.SelectionnerElementConteneurDuDessus(ptLocal, candidats);
            
            parent = DessinWorkflow;;
            if (parent == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                List<I2iObjetGraphique> nouveaux = new List<I2iObjetGraphique>();
                foreach (CRectangleDragForObjetGraphique rct in RectsDrags)
                {
                    rct.RectangleDrag = rct.Datas.GetDragDropPosition(ptLocal);
                    rct.RectangleDrag = GetRectangleSelonModesActives(rct.RectangleDrag, ptLocal);
                    // rct.RectangleDrag.Offset((int)(AutoScrollPosition.X / Echelle), (int)(AutoScrollPosition.Y / Echelle));

                    //Si dummy de création (type de bloc), création d'une nouvelle étape
                    I2iObjetGraphique objetGraphique = rct.Datas.ObjetDragDrop;
                    CDummyObjetWorkflowPourCreation dummyCreation = objetGraphique as CDummyObjetWorkflowPourCreation;
                    if (dummyCreation != null)
                    {
                        CTypeEtapeWorkflow typeEtape = dummyCreation.TypeEtape;
                        if (typeEtape == null)
                        {
                            typeEtape = new CTypeEtapeWorkflow(DessinWorkflow.TypeWorkflow.ContexteDonnee);
                            typeEtape.CreateNewInCurrentContexte();
                            typeEtape.Workflow = DessinWorkflow.TypeWorkflow;
                            typeEtape.Bloc = Activator.CreateInstance(dummyCreation.TypeBloc, new object[] { typeEtape }) as CBlocWorkflow;
                        }
                        CWorkflowEtapeDessin graphEtape = new CWorkflowEtapeDessin();
                        graphEtape.TypeEtape = typeEtape;
                        objetGraphique = graphEtape;
                        objetGraphique.Size = dummyCreation.Size;
                    }

                    JusteBeforePositionneSurApresDragDrop(objetGraphique);
                    bool bParentIsInSelec = objetGraphique.Parent != null && candidats.Contains(objetGraphique.Parent);

                    bool bHasMove = false;

                    if (e.Effect == DragDropEffects.Copy)
                    {
                        Dictionary<Type, object> dicObjetsPourCloner = new Dictionary<Type, object>();
                        AddObjectsForClonerSerializer(dicObjetsPourCloner);
                        objetGraphique = (I2iObjetGraphique)objetGraphique.GetCloneAMettreDansParent(parent, dicObjetsPourCloner);

                        if (objetGraphique == null || !parent.AddChild(objetGraphique))
                        {
                            e.Effect = DragDropEffects.None;
                            objetGraphique.CancelClone();
                            continue;
                        }
                        else
                        {
                            objetGraphique.Parent = parent;
                            nouveaux.Add(objetGraphique);
                        }
                        bHasMove = true;
                    }
                    else
                    {
                        bHasMove = true;
                        if (objetGraphique.Parent != parent)
                        {
                            if (objetGraphique.Parent != null)
                            {
                                if (!bParentIsInSelec)
                                    objetGraphique.Parent.RemoveChild(objetGraphique);
                            }
                            else
                                nouveaux.Add(objetGraphique);
                            if (!bParentIsInSelec)
                                if (!parent.AddChild(objetGraphique))
                                {
                                    e.Effect = DragDropEffects.None;
                                    continue;
                                }
                                else
                                {
                                    objetGraphique.Parent = parent;
                                }
                        }
                    }
                    

                    if (!bParentIsInSelec && bHasMove)
                    {
                        Point ptDrop = new Point(rct.RectangleDrag.Left, rct.RectangleDrag.Top);
                        objetGraphique.PositionAbsolue = ptDrop;
                    }

                }
                if (nouveaux.Count > 0)
                {
                    RefreshSelectionChanged = false;
                    Selection.Clear();
                    Selection.AddRange(nouveaux);
                    RefreshSelectionChanged = true;
                    DeclencheAfterAddElements(nouveaux);
                    Refresh();

                }
            }

           
            ElementModifie();
            EnDeplacement = false;
            Dessiner(true, true);
            
        }

        //-----------------------------------------------------------------
        public override void OnMouseMoveNonStandard(object sender, MouseEventArgs e)
        {
            if ((ModeSourisCustom == EModeSourisCustom.LienWorkflow)
                && m_objetStartJoin != null && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();

                Point ptCentre = new Point(m_rectStartLien.Left + m_rectStartLien.Width / 2,
                    m_rectStartLien.Top + m_rectStartLien.Height / 2);
                Rectangle rct = m_rectLien;

                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                IWorflowDessin dest = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as IWorflowDessin;
                if (dest == m_objetStartJoin)
                    dest = null;
                Rectangle rctDest = new Rectangle(e.X, e.Y, 1, 1);
                if (dest != null)
                {
                    rctDest = dest.RectangleAbsolu;
                    rctDest = new Rectangle(GetDisplayPointFromLogical(rctDest.Location),
                        GetDisplaySizeFromLogical(rctDest.Size));
                }

                rct.Inflate(2, 2);
                using ( Bitmap bmp = DernierApercuToDispose )
                    g.DrawImage(bmp, rct, rct, GraphicsUnit.Pixel);


                if (Control.ModifierKeys == Keys.Control)
                {
                    Brush br = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
                    g.FillRectangle(br, m_rectStartLien);
                    m_rectLien = m_rectStartLien;
                }
                else
                {
                    Pen pen = new Pen(Color.Blue);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Brush br = new SolidBrush(Color.FromArgb(128, 0, 0, 255));
                    g.FillRectangle(br, m_rectStartLien);
                    if (dest != null)
                        g.FillRectangle(br, rctDest);
                    br.Dispose();
                    g.DrawLine(pen, ptCentre, new Point(e.X, e.Y));
                    pen.Dispose();
                    g.Dispose();
                    m_rectLien = Rectangle.Union(m_rectStartLien, rctDest);
                }
            }
        }

        //-----------------------------------------------------------------
        public override void OnMouseUpNonStandard(object sender, MouseEventArgs e)
        {
            if ((ModeSourisCustom == EModeSourisCustom.LienWorkflow ) && e.Button == MouseButtons.Left)
            {
                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                m_objetEndJoin = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CWorkflowEtapeDessin;
                if (m_objetEndJoin != null && m_objetEndJoin != m_objetStartJoin)
                {
                    if (m_objetEndJoin != null)
                    {
                        if (ModeSourisCustom == EModeSourisCustom.LienWorkflow)
                        {
                            if (m_objetStartJoin != null && m_objetEndJoin != null)
                            {
                                m_etapeStartLienACreer = m_objetStartJoin;
                                m_etapeEndLienACreer = m_objetEndJoin;
                                if (m_etapeStartLienACreer.TypeEtape != null && m_etapeStartLienACreer.TypeEtape.Bloc != null &&
                                    m_etapeStartLienACreer.TypeEtape.Bloc.CodesRetourPossibles.Length > 0)
                                    ShowMenuCreateLien();
                                else
                                    CreateLien("");
                            }
                        }
                    }
                }
                m_objetStartJoin = null;
            }
        }

        private void ShowMenuCreateLien()
        {
            m_menuCreateLien.Items.Clear();
            if (m_etapeStartLienACreer.TypeEtape != null && m_etapeStartLienACreer.TypeEtape.Bloc != null)
            {
                foreach (string strCodeActivation in m_etapeStartLienACreer.TypeEtape.Bloc.CodesRetourPossibles)
                {
                    ToolStripMenuItem itemCreateLienAvecCode = new ToolStripMenuItem(strCodeActivation);
                    itemCreateLienAvecCode.Tag = strCodeActivation;
                    m_menuCreateLien.Items.Add(itemCreateLienAvecCode);
                    itemCreateLienAvecCode.Click += new EventHandler(itemCreateLienAvecCode_Click);
                }
            }
            m_menuCreateLien.Show(Cursor.Position);
        }

        void itemCreateLienAvecCode_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                CreateLien(item.Tag as string);
            }
        }

        private void CreateLien(string strCodeActivation)
        {
            //Création d'un lien
            CLienEtapesWorkflow lien = new CLienEtapesWorkflow(DessinWorkflow.TypeWorkflow.ContexteDonnee);
            lien.CreateNewInCurrentContexte();
            lien.EtapeSource = m_etapeStartLienACreer.TypeEtape;
            lien.EtapeDestination = m_etapeEndLienACreer.TypeEtape;
            lien.TypeWorkflow = DessinWorkflow.TypeWorkflow;
            lien.ActivationCode = strCodeActivation;

            CWorkflowLienDessin lienDessin = new CWorkflowLienDessin();
            lienDessin.Lien = lien;
            DessinWorkflow.AddChild(lienDessin);
            lienDessin.Parent = DessinWorkflow;
            Refresh();
            m_etapeStartLienACreer = null;
            m_etapeEndLienACreer = null;
        }

        private void ShowInterfaceLienAction(IWorflowDessin start, IWorflowDessin end)
        {
           /* m_sourceLienParametre = start;
            m_destLienParametre = end;*/
        }


        /*public void menu_Click(object sender, EventArgs e)
        {
            CMenuNumero menu = sender as CMenuNumero;
            if (menu != null)
            {
                if (m_sourceLienParametre != null && m_destLienParametre != null)
                {
                    m_sourceLienParametre.SetExterne(menu.Numero, m_destLienParametre);
                    OnChangeFormules();
                }
            }
        }
        
        void OnChangeFormules()
        {
            Refresh();
            if (FormulesChanged != null)
                FormulesChanged(this, null);
        }
        */

        
        protected override void AfficherMenuAdditonnel(ContextMenuStrip menu)
        {
            base.AfficherMenuAdditonnel(menu);
            foreach (ToolStripItem item in new ArrayList(m_menuRClick.Items))
            {
                if (!menu.Items.Contains(item))
                    menu.Items.Add(item);
            }
            CWorkflowEtapeDessin etape = EtapeSelectionnee;
            m_menuEditWorkflow.Visible = etape != null && etape.TypeEtape != null &&
                etape.TypeEtape.Bloc is CBlocWorkflowWorkflow;
            m_menuStartPoint.Visible = etape != null;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique4 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique4 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_menuRClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuEditWorkflow = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuCreateLien = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuStartPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuRClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menuRClick
            // 
            this.m_menuRClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.m_menuStartPoint,
            this.m_menuEditWorkflow});
            this.m_menuRClick.Name = "m_menuRClick";
            this.m_menuRClick.Size = new System.Drawing.Size(237, 76);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(233, 6);
            // 
            // m_menuEditWorkflow
            // 
            this.m_menuEditWorkflow.Name = "m_menuEditWorkflow";
            this.m_menuEditWorkflow.Size = new System.Drawing.Size(236, 22);
            this.m_menuEditWorkflow.Text = "Edit Workflow|20062";
            this.m_menuEditWorkflow.Click += new System.EventHandler(this.m_menuEditWorkflow_Click);
            // 
            // m_menuCreateLien
            // 
            this.m_menuCreateLien.Name = "m_menuCreateLien";
            this.m_menuCreateLien.Size = new System.Drawing.Size(61, 4);
            // 
            // m_menuStartPoint
            // 
            this.m_menuStartPoint.Name = "m_menuStartPoint";
            this.m_menuStartPoint.Size = new System.Drawing.Size(236, 22);
            this.m_menuStartPoint.Text = "Set as default start step|20074";
            this.m_menuStartPoint.Click += new System.EventHandler(this.m_menuStartPoint_Click);
            // 
            // CControlEditeWorkflow
            // 
            cGrilleEditeurObjetGraphique4.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique4.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique4.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique4.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique4.TailleCarreau = new System.Drawing.Size(20, 20);
            this.GrilleAlignement = cGrilleEditeurObjetGraphique4;
            this.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.Name = "CControlEditeWorkflow";
            cProfilEditeurObjetGraphique4.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique4.Grille = cGrilleEditeurObjetGraphique4;
            cProfilEditeurObjetGraphique4.HistorisationActive = true;
            cProfilEditeurObjetGraphique4.Marge = 10;
            cProfilEditeurObjetGraphique4.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique4.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique4.ToujoursAlignerSurLaGrille = false;
            this.Profil = cProfilEditeurObjetGraphique4;
            this.ToujoursAlignerSurLaGrille = false;
            this.Load += new System.EventHandler(this.CControlEditeWorkflow_Load);
            this.m_menuRClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

       

        private void m_menuSetAsStartPoint_Click(object sender, EventArgs e)
        {
            /*C2iExpressionGraphique rep = ObjetEdite as C2iExpressionGraphique;
            if (rep != null && Selection.Count == 1)
            {
                IWorflowDessin gr = Selection[0] as IWorflowDessin;
                rep.StartPoint = gr;
                OnChangeFormules();
            }*/

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*((C2iExpressionGraphique)ObjetEdite).RefreshFormuleFinale();
            C2iExpression formule = ((C2iExpressionGraphique)ObjetEdite).FormuleFinale;
            if (formule != null)
                if (MessageBox.Show(formule.GetString() + "\nTest ?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_objetAnalyse!=null?m_objetAnalyse.ElementAVariableInstance:null);
                    sc2i.common.CResultAErreur result = formule.Eval(ctx);
                    if (!result)
                        CFormAfficheErreur.Show(result.Erreur);
                    else
                        MessageBox.Show(result.Data == null ? "null" : result.Data.ToString());
                }
             * */
        }

        //-------------------------------------------------------------------
        public CWorkflowEtapeDessin EtapeSelectionnee
        {
            get
            {
                CWorkflowEtapeDessin etape = Selection.Count == 1 ? Selection[0] as CWorkflowEtapeDessin : null;
                return etape;
            }
        }

        private void CControlEditeWorkflow_Load(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------
        private void m_menuEditWorkflow_Click(object sender, EventArgs e)
        {
            CWorkflowEtapeDessin etape = EtapeSelectionnee;
            if (etape == null)
                return;
            CBlocWorkflowWorkflow blocwkf = etape != null && etape.TypeEtape != null ?
                etape.TypeEtape.Bloc as CBlocWorkflowWorkflow : null;
            if (blocwkf != null)
            {
                CReferenceTypeForm refFrm = CFormFinder.GetRefFormToEdit(typeof(CTypeWorkflow));
                if (refFrm != null)
                {
                    CTypeWorkflow typeWorkflow = null;
                    if (blocwkf.DbKeyTypeWorkflow == null)
                    {
                        if (!LockEdition)
                        {
                            typeWorkflow = new CTypeWorkflow(CSc2iWin32DataClient.ContexteCourant);
                            typeWorkflow.CreateNew();
                            typeWorkflow.Libelle = blocwkf.TypeEtape.Libelle;
                        }
                    }
                    else
                    {
                        typeWorkflow = new CTypeWorkflow(CSc2iWin32DataClient.ContexteCourant);
                        if (!typeWorkflow.ReadIfExists(blocwkf.DbKeyTypeWorkflow))
                        {
                            typeWorkflow = null;
                        }
                    }
                    if (typeWorkflow != null)
                    {
                        CFormNavigateur navigateur = null;
                        if (LockEdition)
                        {
                            Form frm = FindForm();
                            if (frm != null)
                            {
                                Control ctrl = frm.Parent;
                                while (ctrl != null && ctrl.Parent != null && !(ctrl is CFormNavigateur))
                                    ctrl = ctrl.Parent;
                                if (typeof(CFormNavigateur).IsAssignableFrom(ctrl.GetType()))
                                    navigateur = (CFormNavigateur)ctrl;
                            }
                        }
                        CFormEditionStandard frmEdition = refFrm.GetForm(typeWorkflow) as CFormEditionStandard;
                        if (navigateur == null && frmEdition != null)
                        {
                            CFormNavigateurPopup.Show(frmEdition);
                            if (!LockEdition && typeWorkflow.IsValide() && typeWorkflow.Id >= 0)
                            {
                                blocwkf.DbKeyTypeWorkflow = typeWorkflow.DbKey;
                                etape.TypeEtape.Bloc = blocwkf;
                            }
                        }
                        else
                            navigateur.AffichePage(frmEdition);
                    }
                }
            }
        }

        //------------------------------------------------------------------------------
        private void m_menuStartPoint_Click(object sender, EventArgs e)
        {
            CWorkflowEtapeDessin etape = EtapeSelectionnee;
            if (etape != null && etape.TypeEtape != null)
                etape.TypeEtape.IsDefautStart = true;
            Refresh();
        }

        //------------------------------------------------------------------------------
        protected override void DragEnterTraitement(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CReferenceObjetDonnee)))
            {
                CReferenceObjetDonnee reference = e.Data.GetData ( typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
                if ( reference.TypeObjet == typeof(CModeleAffectationUtilisateurs) )
                {

                    e.Effect = DragDropEffects.Link;
                    return;
                }
            }
            base.DragEnterTraitement(e);
        }

        //------------------------------------------------------------------------------
        protected override void DragOverTraitement(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CReferenceObjetDonnee)))
            {
                CReferenceObjetDonnee reference = e.Data.GetData ( typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
                if (reference.TypeObjet == typeof(CModeleAffectationUtilisateurs))
                {
                    Point pt = PointToClient(new Point(e.X, e.Y));
                    Point ptLogique = GetLogicalPointFromDisplay(pt);
                    CWorkflowEtapeDessin dessinEtape = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CWorkflowEtapeDessin;
                    if (dessinEtape != null)
                    {
                        e.Effect = DragDropEffects.Link;
                        return;
                    }
                }
            }
            base.DragOverTraitement(e);
        }



       
        

    }
}
