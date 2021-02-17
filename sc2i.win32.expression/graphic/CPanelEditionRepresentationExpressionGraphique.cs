using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.win32.common;
using System.Drawing;
using System.Windows.Forms;
using sc2i.expression;
using System.Collections;
using sc2i.drawing;
using sc2i.common;

namespace sc2i.win32.expression
{
    public delegate bool AskCreationVariableEventHandler ( CDefinitionProprieteDynamiqueVariableFormule definition );

    public class CPanelEditionRepresentationExpressionGraphique : CPanelEditionObjetGraphique
    {
        private CObjetPourSousProprietes m_objetAnalyse = null;
        public CPanelEditionRepresentationExpressionGraphique()
            : base()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------
        public enum EModeSourisCustom
        {
            LienSequence,
            LienParametre
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
        public void Init ( C2iExpressionGraphique representation, 
            CObjetPourSousProprietes objetAnalyse )
        {
            ObjetEdite = representation;
            ObjetAnalyse = objetAnalyse;
            PreprareEdition();
        }

        public void PreprareEdition()
        {
            C2iExpressionGraphique expEditee = ObjetEdite as C2iExpressionGraphique;
            if (expEditee != null)
            {
                CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression(((C2iExpressionGraphique)ObjetEdite),
                    ObjetAnalyse);
                CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
                analyseur.VerifieParametresLorsDeLanalyse = false;
                foreach (I2iObjetGraphique child in expEditee.Childs)
                {
                    CRepresentationExpressionGraphique rep = child as CRepresentationExpressionGraphique;
                    if (rep != null)
                    {
                        if (rep.Formule != null)
                        {
                            if (expEditee.GetUtilisateurs(rep.Id).Count() == 0)
                            {
                                CResultAErreur result = analyseur.AnalyseChaine(rep.Formule.GetString());
                                if (result)
                                    rep.Formule = result.Data as C2iExpression;
                                else
                                    rep.LastErreur = result.Erreur.ToString();
                            }
                        }
                    }
                }
            }
        }
                


            



        //-----------------------------------------------------------------
        private CRepresentationExpressionGraphique m_expressionStartJoin = null;
        private CRepresentationExpressionGraphique m_expressionEndJoin = null;
        private Rectangle m_rectLien = Rectangle.Empty;
        private ContextMenuStrip m_menuNumeroParametre;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip m_menuRClick;
        private ToolStripSeparator toolStripMenuItem1;
        private ContextMenuStrip m_menuSortieSequence;
        private ToolStripMenuItem m_menuNext;
        private ToolStripMenuItem m_menuSetAsStartPoint;
        private ToolStripMenuItem m_menuTest;
        private ToolStripMenuItem m_mnuStockInVariable;
        private Rectangle m_rectStartLien = Rectangle.Empty;
        public override void OnMouseDownNonStandard(object sender, MouseEventArgs e)
        {
            m_expressionStartJoin = null;
            if (
                (ModeSourisCustom == EModeSourisCustom.LienSequence || ModeSourisCustom == EModeSourisCustom.LienParametre)
                && e.Button == MouseButtons.Left)
            {
                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                m_expressionStartJoin = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CRepresentationExpressionGraphique;
                if (m_expressionStartJoin != null)
                {
                    Rectangle rctStart = m_expressionStartJoin.RectangleAbsolu;
                    rctStart = new Rectangle(GetDisplayPointFromLogical(rctStart.Location),
                        GetDisplaySizeFromLogical(rctStart.Size));
                    m_rectStartLien = rctStart;
                    m_rectLien = m_rectStartLien;
                }
            }
        }


        //--------------------------------------------------------------------
        public CObjetPourSousProprietes ObjetAnalyse
        {
            get
            {
                return m_objetAnalyse;
            }
            set
            {
                m_objetAnalyse = value;
            }
        }

        /// //////////////////////////////////////////////////
        protected override void Editeur_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (ObjetEdite == null)
                return;
            if (LockEdition)
                return;
            List<CDonneeDragDropObjetGraphique> datas = GetDragDropData(e.Data);

            if (datas == null || datas.Count == 0)
                return;

            List<I2iObjetGraphique> candidats = new List<I2iObjetGraphique>();
            foreach (CRectangleDragForObjetGraphique rct in RectsDrags)
                candidats.Add(rct.ObjetGraphique);

            Point ptLocal = GetLogicalPointFromDisplay(PointToClient(new Point(e.X, e.Y)));
            I2iObjetGraphique parent = ObjetEdite.SelectionnerElementConteneurDuDessus(ptLocal, candidats);
            
            CRepresentationExpressionGraphique repForParametre = parent as CRepresentationExpressionGraphique;
            if (RectsDrags.Count() != 1)
                repForParametre = null;
            parent = ObjetEdite;

            

            List<I2iObjetGraphique> nouveaux = new List<I2iObjetGraphique>();
            foreach (CRectangleDragForObjetGraphique rct in RectsDrags)
            {
                rct.RectangleDrag = rct.Datas.GetDragDropPosition(ptLocal);
                rct.RectangleDrag = GetRectangleSelonModesActives(rct.RectangleDrag, ptLocal);
                // rct.RectangleDrag.Offset((int)(AutoScrollPosition.X / Echelle), (int)(AutoScrollPosition.Y / Echelle));

                I2iObjetGraphique objetGraphique = rct.Datas.ObjetDragDrop;
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
                    if (repForParametre == null)
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
            ElementModifie();
            EnDeplacement = false;
            Dessiner(true, true);
            if (repForParametre != null)
            {
                CRepresentationExpressionGraphique repParametre = RectsDrags[0].Datas.ObjetDragDrop as CRepresentationExpressionGraphique;
                if ( repParametre != null )
                    ShowInterfaceLienParametre(repParametre, repForParametre);
            }
        }

        //-----------------------------------------------------------------
        public override void OnMouseMoveNonStandard(object sender, MouseEventArgs e)
        {
            if ((ModeSourisCustom == EModeSourisCustom.LienSequence || ModeSourisCustom == EModeSourisCustom.LienParametre)
                && m_expressionStartJoin != null && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();

                Point ptCentre = new Point(m_rectStartLien.Left + m_rectStartLien.Width / 2,
                    m_rectStartLien.Top + m_rectStartLien.Height / 2);
                Rectangle rct = m_rectLien;

                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                CRepresentationExpressionGraphique dest = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CRepresentationExpressionGraphique;
                if (dest == m_expressionStartJoin)
                    dest = null;
                Rectangle rctDest = new Rectangle(e.X, e.Y, 1, 1);
                if (dest != null)
                {
                    rctDest = dest.RectangleAbsolu;
                    rctDest = new Rectangle(GetDisplayPointFromLogical(rctDest.Location),
                        GetDisplaySizeFromLogical(rctDest.Size));
                }

                rct.Inflate(2, 2);
                using ( Bitmap bmp = DernierApercuToDispose)
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
            if ((ModeSourisCustom == EModeSourisCustom.LienSequence || ModeSourisCustom == EModeSourisCustom.LienParametre) && e.Button == MouseButtons.Left)
            {
                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                CRepresentationExpressionGraphique dest = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CRepresentationExpressionGraphique;
                if (dest != null && dest != m_expressionStartJoin)
                {
                    if (dest != null)
                    {
                        bool bTraite = false;
                        if (ModeSourisCustom == EModeSourisCustom.LienSequence)
                        {
                            foreach ( ToolStripMenuItem item in new ArrayList(m_menuSortieSequence.Items) )
                            {
                                if ( item != m_menuNext )
                                {
                                m_menuSortieSequence.Items.Remove ( item );
                                item.Dispose();
                                }
                            }
                            C2iExpressionAnalysable expAn = m_expressionStartJoin.Formule as C2iExpressionAnalysable;
                            if (expAn != null)
                            {
                                CInfo2iExpression info = expAn.GetInfos();
                                if (info.InfosParametres.Count() == 1)
                                {
                                    CInfo2iDefinitionParametres defs = info.InfosParametres[0];
                                    int nNum = 0;
                                    foreach (CInfoUnParametreExpression par in defs.InfosParametres)
                                    {
                                        if (par.IsSortieSequence)
                                        {
                                            CMenuNumero menu = new CMenuNumero(par.NomParametre, nNum);
                                            m_menuSortieSequence.Items.Add(menu);
                                            menu.Click += new EventHandler(menu_Click);
                                            if (ModifierKeys == Keys.Control)
                                            {
                                                if (m_expressionStartJoin.GetExterne(nNum) == dest)
                                                {
                                                    m_expressionStartJoin.SetExterne(nNum, null);
                                                    bTraite = true;
                                                }
                                            }
                                        }
                                        nNum++;
                                    }
                                }
                            }
                            if (!bTraite)
                            {
                                if (ModifierKeys == Keys.Control)
                                    m_expressionStartJoin.Next = null;
                                else
                                {
                                    if (m_menuSortieSequence.Items.Count != 1)
                                    {
                                        ShowInterfaceLienAction(m_expressionStartJoin, dest);
                                    }
                                    else
                                        m_expressionStartJoin.Next = dest;
                                }
                            }
                        }
                        else if (ModeSourisCustom == EModeSourisCustom.LienParametre)
                        {
                            if ( ModifierKeys == Keys.Control )
                                dest.StopUseExterne ( m_expressionStartJoin );
                            else
                                ShowInterfaceLienParametre(m_expressionStartJoin, dest);
                        }
                    }
                }
                m_expressionStartJoin = null;
                OnChangeFormules();
            }
        }

        private void ShowInterfaceLienAction(CRepresentationExpressionGraphique start, CRepresentationExpressionGraphique end)
        {
            m_sourceLienParametre = start;
            m_destLienParametre = end;
            m_menuSortieSequence.Show(Cursor.Position);
        }


        public void menu_Click(object sender, EventArgs e)
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

        public event EventHandler FormulesChanged;

        private class CMenuNumero : ToolStripMenuItem
        {
            public int Numero { get; set; }

            public CMenuNumero(string strLibelle, int nNumero)
                : base(strLibelle)
            {
                Numero = nNumero;
            }
        }

        private CRepresentationExpressionGraphique m_sourceLienParametre = null;
        private CRepresentationExpressionGraphique m_destLienParametre = null;
        private void ShowInterfaceLienParametre(
            CRepresentationExpressionGraphique source, 
            CRepresentationExpressionGraphique dest)
        {
            Dictionary<int, CMenuNumero> dicMenus = new Dictionary<int, CMenuNumero>();
            if (source == null || dest == null)
                return;
            if (m_menuNumeroParametre.Items.Count == 0)
            {
                //Création initiale des sous menus
                for (int n = 0; n < 20; n++)
                {
                    CMenuNumero menu = new CMenuNumero(n.ToString(), n);
                    m_menuNumeroParametre.Items.Add(menu);
                    menu.Click += new EventHandler(menuNumero_Click);
                }
            }
            foreach (ToolStripMenuItem item in m_menuNumeroParametre.Items)
            {
                CMenuNumero num = item as CMenuNumero;
                if (num != null)
                    dicMenus[num.Numero] = num;
            }

            C2iExpressionAnalysable expAn = dest.Formule == null ? null : dest.Formule as C2iExpressionAnalysable;

            if (expAn != null)
            {
                CInfo2iExpression info = null;
                info = expAn.GetInfos();
                CInfo2iDefinitionParametres[] infosParams = info.InfosParametres;
                for (int n = 0; n < 20; n++)
                {
                    string strNom = info.GetNomParametre(n);
                    if (strNom.Trim() == "")
                    {
                        dicMenus[n].Visible = false;
                    }
                    else
                    {
                        dicMenus[n].Visible = true;
                        dicMenus[n].Text = strNom;
                    }
                }

                sc2i.expression.C2iExpression[] parametres = dest.Formule.Parametres2i;
                foreach (ToolStripMenuItem item in m_menuNumeroParametre.Items)
                {
                    int nPos = item.Text.IndexOf('(');
                    if (nPos > 0)
                        item.Text = item.Text.Substring(0, nPos).Trim();
                    CMenuNumero numero = item as CMenuNumero;
                    if (numero != null)
                    {
                        if (numero.Numero < parametres.Length)
                        {
                            sc2i.expression.C2iExpression exp = parametres[numero.Numero];
                            if (exp != null)
                            {
                                item.Text += " (" + exp.GetString() + ")";
                            }
                        }
                    }
                }
                m_sourceLienParametre = source;
                m_destLienParametre = dest;
                m_menuNumeroParametre.Show(Cursor.Position);
            }



        }

        void menuNumero_Click(object sender, EventArgs e)
        {
            CMenuNumero numero = sender as CMenuNumero;
            if (m_sourceLienParametre != null && m_destLienParametre != null && numero != null)
            {
                m_destLienParametre.SetExterne(numero.Numero, m_sourceLienParametre);
                OnChangeFormules();
            }
            m_sourceLienParametre = null;
            m_destLienParametre = null;
        }

        protected override void AfficherMenuAdditonnel(ContextMenuStrip menu)
        {
            base.AfficherMenuAdditonnel(menu);
            foreach (ToolStripItem item in new ArrayList(m_menuRClick.Items))
            {
                if (!menu.Items.Contains(item))
                    menu.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique2 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique2 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_menuNumeroParametre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuRClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuSetAsStartPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSortieSequence = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuNext = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuStockInVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuRClick.SuspendLayout();
            this.m_menuSortieSequence.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menuNumeroParametre
            // 
            this.m_menuNumeroParametre.Name = "m_menuNumeroParametre";
            this.m_menuNumeroParametre.Size = new System.Drawing.Size(61, 4);
            // 
            // m_menuRClick
            // 
            this.m_menuRClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.m_menuSetAsStartPoint,
            this.m_menuTest,
            this.m_mnuStockInVariable});
            this.m_menuRClick.Name = "m_menuRClick";
            this.m_menuRClick.Size = new System.Drawing.Size(203, 98);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // m_menuSetAsStartPoint
            // 
            this.m_menuSetAsStartPoint.Name = "m_menuSetAsStartPoint";
            this.m_menuSetAsStartPoint.Size = new System.Drawing.Size(202, 22);
            this.m_menuSetAsStartPoint.Text = "Set as start point|20003";
            this.m_menuSetAsStartPoint.Click += new System.EventHandler(this.m_menuSetAsStartPoint_Click);
            // 
            // m_menuTest
            // 
            this.m_menuTest.Name = "m_menuTest";
            this.m_menuTest.Size = new System.Drawing.Size(202, 22);
            this.m_menuTest.Text = "Test";
            this.m_menuTest.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // m_menuSortieSequence
            // 
            this.m_menuSortieSequence.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuNext});
            this.m_menuSortieSequence.Name = "m_menuSortieSequence";
            this.m_menuSortieSequence.Size = new System.Drawing.Size(143, 26);
            this.m_menuSortieSequence.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.m_menuSortieSequence_Closed);
            // 
            // m_menuNext
            // 
            this.m_menuNext.Name = "m_menuNext";
            this.m_menuNext.Size = new System.Drawing.Size(142, 22);
            this.m_menuNext.Text = "Next|20002";
            this.m_menuNext.Click += new System.EventHandler(this.m_menuNext_Click);
            // 
            // m_mnuStockInVariable
            // 
            this.m_mnuStockInVariable.Name = "m_mnuStockInVariable";
            this.m_mnuStockInVariable.Size = new System.Drawing.Size(202, 22);
            this.m_mnuStockInVariable.Text = "Store in variable|20016";
            this.m_mnuStockInVariable.Click += new System.EventHandler(this.m_mnuStockInVariable_Click);
            // 
            // CPanelEditionRepresentationExpressionGraphique
            // 
            cGrilleEditeurObjetGraphique2.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique2.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique2.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique2.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique2.TailleCarreau = new System.Drawing.Size(20, 20);
            this.GrilleAlignement = cGrilleEditeurObjetGraphique2;
            this.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.Name = "CPanelEditionRepresentationExpressionGraphique";
            cProfilEditeurObjetGraphique2.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique2.Grille = cGrilleEditeurObjetGraphique2;
            cProfilEditeurObjetGraphique2.HistorisationActive = true;
            cProfilEditeurObjetGraphique2.Marge = 10;
            cProfilEditeurObjetGraphique2.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique2.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique2.ToujoursAlignerSurLaGrille = false;
            this.Profil = cProfilEditeurObjetGraphique2;
            this.ToujoursAlignerSurLaGrille = false;
            this.m_menuRClick.ResumeLayout(false);
            this.m_menuSortieSequence.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        

        private void m_menuSortieSequence_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            m_expressionStartJoin = null;
        }

        private void m_menuNext_Click(object sender, EventArgs e)
        {
            if (m_sourceLienParametre != null && m_destLienParametre != null)
            {
                m_sourceLienParametre.Next = m_destLienParametre;
                OnChangeFormules();
            }
            m_sourceLienParametre = null;
            m_destLienParametre = null;
        }

        private void m_menuSetAsStartPoint_Click(object sender, EventArgs e)
        {
            C2iExpressionGraphique rep = ObjetEdite as C2iExpressionGraphique;
            if (rep != null && Selection.Count == 1)
            {
                CRepresentationExpressionGraphique gr = Selection[0] as CRepresentationExpressionGraphique;
                /*while (gr.Prev != null)
                    gr = gr.Prev;*/
                rep.StartPoint = gr;
                OnChangeFormules();
            }

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((C2iExpressionGraphique)ObjetEdite).RefreshFormuleFinale();
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
        }

        public event AskCreationVariableEventHandler AskCreationVariable;

        private void m_mnuStockInVariable_Click(object sender, EventArgs e)
        {
            if (Selection.Count != 1 || AskCreationVariable == null)
                return;
            CRepresentationExpressionGraphique rep = Selection[0] as CRepresentationExpressionGraphique;
            if (rep == null || rep.Formule == null)
                return;
            CTypeResultatExpression typeRes = rep.Formule.TypeDonnee;
            CDefinitionProprieteDynamiqueVariableFormule def = new CDefinitionProprieteDynamiqueVariableFormule(
                "", typeRes, true);
            def = CFormEditVariableFormule.EditeVariable(def);
            if (def != null)
            {
                if (AskCreationVariable(def))
                {
                    C2iExpressionSetVariable exp = new C2iExpressionSetVariable();
                    C2iExpressionChamp expVar = new C2iExpressionChamp(def);
                    exp.Parametres.Add(expVar);
                    exp.Parametres.Add(rep.Formule);
                    rep.Formule = exp;
                    Refresh();
                }
            }
        }



    }
}
