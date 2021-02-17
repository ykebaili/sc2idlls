using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using sc2i.expression;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression.expressions;
using sc2i.win32.expression.graphic;

namespace sc2i.win32.expression
{
    public partial class CEditeurExpressionGraphique : UserControl
    {
        /// <summary>
        /// Type de formule->Type d'éditeur de paramètres
        /// </summary>
        private static Dictionary<Type, Type> m_dicEditeursParametresSpecifiques = new Dictionary<Type, Type>();


        private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;
        private CObjetPourSousProprietes m_objetAnalyse = null;

        //L'éditeur de paramètres actuellement affiché
        private IEditeurParametresFormule m_editeurParametresActif = null;

        //-----------------------------------------------
        public CEditeurExpressionGraphique()
        {
            InitializeComponent();

            FillArbreFormules();
        }

        //-----------------------------------------------
        public static void RegisterEditeurSpecifique(Type typeFormule, Type typeEditeur)
        {
            m_dicEditeursParametresSpecifiques[typeFormule] = typeEditeur;
        }


        //-----------------------------------------------
        private void FillArbreFormules()
        {
            Hashtable table = new Hashtable();
            CAllocateur2iExpression allocateur = new CAllocateur2iExpression();
            ArrayList liste;
            foreach (C2iExpression exp in CAllocateur2iExpression.ToutesExpressions)
            {
                if (exp is C2iExpressionAnalysable)
                {
                    CInfo2iExpression info = ((C2iExpressionAnalysable)exp).GetInfos();
                    if (info != null && info.Categorie != "")
                    {
                        liste = (ArrayList)table[info.Categorie];
                        if (liste == null)
                        {
                            liste = new ArrayList();
                            table[info.Categorie] = liste;
                        }
                        liste.Add(exp);
                    }
                }
            }
            foreach (C2iExpressionConstanteDynamique exp in CAnalyseurSyntaxiqueExpression.GetConstantesDynamiques())
            {
                CInfo2iExpression info = exp.GetInfos();
                if (info != null && info.Categorie != "")
                {
                    liste = (ArrayList)table[info.Categorie];
                    if (liste == null)
                    {
                        liste = new ArrayList();
                        table[info.Categorie] = liste;
                    }
                    liste.Add(exp);
                }
            }

            m_arbreFormules.Sorted = true;
            foreach (string cat in table.Keys)
            {
                TreeNode node = m_arbreFormules.Nodes.Add(cat);
                node.Tag = null;
                liste = (ArrayList)table[cat];
                foreach (C2iExpressionAnalysable exp in liste)
                {
                    TreeNode nodeFils = node.Nodes.Add(exp.GetInfos().Texte);
                    nodeFils.Tag = exp;
                }
            }
            m_arbreFormules.Visible = false;
        }

        //--------------------------------------------------------------
        private C2iExpressionGraphique RepresentationEditee
        {
            get
            {
                return m_editeur.ObjetEdite as C2iExpressionGraphique;
            }
        }

        //--------------------------------------------------------------
        public void Init(C2iExpressionGraphique representation,
            IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes objetAnalyse)
        {
            representation.InitForAnalyse(fournisseur);
            m_editeur.Init(representation, objetAnalyse);
            m_editeur.ObjetEdite = representation;
            m_editeur.ObjetAnalyse = objetAnalyse;
            m_fournisseurProprietes = fournisseur;
            m_objetAnalyse = objetAnalyse;
            m_chkActivateDebug.Checked = representation.Debug;
            UpdateVariables();
        }

        //--------------------------------------------------------------
        public C2iExpressionGraphique ExpressionGraphique
        {
            get
            {
                C2iExpressionGraphique exp = m_editeur.ObjetEdite as C2iExpressionGraphique;
                if (exp != null)
                    exp.Debug = m_chkActivateDebug.Checked;
                return exp;
            }
        }




        private void m_arbreFormules_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode)
            {
                C2iExpression formule = ((TreeNode)e.Item).Tag as C2iExpression;
                if (formule != null)
                {
                    CRepresentationExpressionGraphique obj = new CRepresentationExpressionGraphique(formule);
                    CDonneeDragDropObjetGraphique data = new CDonneeDragDropObjetGraphique(Name, obj);
                    DoDragDrop(data, DragDropEffects.Copy);
                }
            }
        }

        private void m_editeur_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePanelParametresDiffere();
            

        }

        private void UpdatePanelParametresDiffere()
        {
            m_timerSelection.Stop();
            m_timerSelection.Start();
        }

        private CObjetPourSousProprietes GetObjetPourAnalyse(CRepresentationExpressionGraphique rep)
        {
            while (rep.Prev != null)
                rep = rep.Prev;
            IEnumerable<CRepresentationExpressionGraphique> users = ExpressionGraphique.GetUtilisateurs(rep.Id);
            if (users.Count() == 1)
            {
                CRepresentationExpressionGraphique repParente = users.ElementAt(0);
                C2iExpressionObjet expObjet = repParente.Formule as C2iExpressionObjet;
                if (expObjet != null && repParente.GetExterne(1) == rep)
                {
                    return expObjet.Parametres2i[0].GetObjetPourSousProprietes();
                }
                C2iExpressionMethodeAnalysable formule = repParente.Formule as C2iExpressionMethodeAnalysable;
                if (formule != null)
                {
                    return formule.GetObjetAnalyseParametresFromObjetAnalyseSource(GetObjetPourAnalyse(repParente));
                }
                else
                {
                    GetObjetPourAnalyse(repParente);
                }
            }
            return m_objetAnalyse;
        }

        private void UpdatePanelParametres()
        {
            if (m_editeurParametresActif != null && m_editeurParametresActif != m_editeurParametresStandard)
            {
                m_panelDetailFormule.Controls.Remove((Control)m_editeurParametresActif);
                ((Control)m_editeurParametresActif).Visible = false;
                ((Control)m_editeurParametresActif).Dispose();
            }
            m_editeurParametresActif = null;
            if (m_editeur.Selection.Count == 1)
            {
                CRepresentationExpressionGraphique exp = m_editeur.Selection[0] as CRepresentationExpressionGraphique;
                if (exp != null)
                {
                    C2iExpression formuleEditee = exp.Formule;
                    Type tpEditeurParametres = null;
                    m_editeurParametresActif = m_editeurParametresStandard;
                    if (formuleEditee != null)
                    {
                        if (m_dicEditeursParametresSpecifiques.TryGetValue(formuleEditee.GetType(), out tpEditeurParametres))
                        {
                            if (tpEditeurParametres != null)
                            {
                                IEditeurParametresFormule editeur = Activator.CreateInstance(tpEditeurParametres) as IEditeurParametresFormule;
                                Control ctrl = editeur as Control;
                                m_editeurParametresStandard.Visible = false;
                                m_panelDetailFormule.Controls.Add(ctrl);
                                m_editeurParametresActif = editeur;
                                m_editeurParametresActif.OnChangeDessin += new EventHandler(m_editeurParametres_OnChangeDessin);
                            }
                        }
                    }
                    m_panelDetailFormule.Visible = true;
                    ((Control)m_editeurParametresActif).Visible = true;
                    m_editeurParametresActif.Init(exp, RepresentationEditee, GetObjetPourAnalyse(exp));
                }
                else
                    m_panelDetailFormule.Visible = false;
            }
            else
                m_panelDetailFormule.Visible = false;
        }

        private void m_editeurParametres_OnChangeDessin(object sender, EventArgs e)
        {
            m_editeur.Refresh();
        }

        private void m_btnSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (m_btnSelect.Checked)
                m_editeur.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Selection;
        }

        private void m_chkZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkZoom.Checked)
                m_editeur.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Zoom;
        }

        //-----------------------------------------------------------
        private void m_chkLien_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkLien.Checked)
            {
                m_editeur.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Custom;
                m_editeur.ModeSourisCustom = CPanelEditionRepresentationExpressionGraphique.EModeSourisCustom.LienSequence;
            }
        }

        //-----------------------------------------------------------
        private void m_lnkAddVar_LinkClicked(object sender, EventArgs e)
        {
            CDefinitionProprieteDynamiqueVariableFormule variable = CFormEditVariableFormule.EditeVariable(null);
            if (variable != null && RepresentationEditee != null)
            {
                RepresentationEditee.AddVariable(variable);
                UpdateVariables();
            }
        }

        //-----------------------------------------------------------
        private void UpdateVariables()
        {
            m_wndListeVariables.BeginUpdate();
            m_wndListeVariables.Items.Clear();
            if (RepresentationEditee != null)
            {
                foreach (CDefinitionProprieteDynamiqueVariableFormule def in RepresentationEditee.Variables)
                {
                    ListViewItem item = new ListViewItem();
                    FillItemVariable(item, def);
                    m_wndListeVariables.Items.Add(item);
                }
            }
            m_wndListeVariables.EndUpdate();
        }

        //-----------------------------------------------------------


        //-----------------------------------------------------------
        private void FillItemVariable(ListViewItem item, CDefinitionProprieteDynamiqueVariableFormule variable)
        {
            item.Text = variable.Nom;
            item.Tag = variable;
        }

        private void m_wndListeVariables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = m_wndListeVariables.HitTest(e.X, e.Y);
            if (info != null)
            {
                CDefinitionProprieteDynamiqueVariableFormule def = info.Item.Tag as CDefinitionProprieteDynamiqueVariableFormule;
                if (def != null)
                {
                    CDefinitionProprieteDynamiqueVariableFormule newDef = CFormEditVariableFormule.EditeVariable(def);
                    if (newDef != null)
                    {
                        RepresentationEditee.ReplaceVariable(def, newDef);
                        UpdateVariables();
                    }
                }
            }
        }

        private void m_btnLienParametre_CheckedChanged(object sender, EventArgs e)
        {
            if (m_btnLienParametre.Checked)
            {
                m_editeur.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Custom;
                m_editeur.ModeSourisCustom = CPanelEditionRepresentationExpressionGraphique.EModeSourisCustom.LienParametre;
            }
        }

        private void m_lnkRemoveVar_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeVariables.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeVariables.SelectedItems[0];
                CDefinitionProprieteDynamiqueVariableFormule def = item.Tag as CDefinitionProprieteDynamiqueVariableFormule;
                if (def != null && RepresentationEditee != null)
                {
                    RepresentationEditee.RemoveVariable(def);
                    UpdateVariables();
                }

            }

        }

        //---------------------------------------------------------------
        private void m_editeur_FormulesChanged(object sender, EventArgs e)
        {
            UpdatePanelParametres();
        }


        //---------------------------------------------------------------
        private void m_btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = I.T("Graphical formula|*.grafexp|All files|*.*|20004");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CResultAErreur result = CSerializerObjetInFile.SaveToFile(RepresentationEditee, C2iExpressionGraphique.c_cleFichier, dlg.FileName);
                if (!result)
                {
                    CFormAfficheErreur.Show(result.Erreur);
                }
            }
        }

        private void m_btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Graphical formula|*.grafexp|All files|*.*|20004");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                C2iExpressionGraphique rep = new C2iExpressionGraphique();
                CResultAErreur result = CSerializerObjetInFile.ReadFromFile(rep, C2iExpressionGraphique.c_cleFichier, dlg.FileName);
                if (!result)
                {
                    CFormAfficheErreur.Show(result.Erreur);
                }
                else
                {
                    this.Init(rep, m_fournisseurProprietes, m_objetAnalyse);
                }
            }

        }

        private void m_btnPaste_Click(object sender, EventArgs e)
        {
            I2iSerializable objet = null;
            CResultAErreur result = CSerializerObjetInClipBoard.Paste(ref objet, C2iExpressionGraphique.c_cleFichier);
            if (!result)
            {
                CFormAlerte.Afficher(result);
                return;
            }
            C2iExpressionGraphique rep = objet as C2iExpressionGraphique;
            if (rep != null)
            {
                if (CFormAlerte.Afficher(I.T("Formula will be replaced. Continue ?|20005"),
                    EFormAlerteType.Question) == DialogResult.No)
                    return;
                Init(rep, m_fournisseurProprietes, m_objetAnalyse);
            }
        }

        private void m_btnCopy_Click(object sender, EventArgs e)
        {
            CResultAErreur result = CSerializerObjetInClipBoard.Copy(RepresentationEditee, C2iExpressionGraphique.c_cleFichier);
            if (!result)
            {
                CFormAlerte.Afficher(result);
            }
        }


        private Point m_ptStartDrag = new Point(0, 0);
        private void m_btnIf_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_ptStartDrag = new Point(e.X, e.Y);
        }

        private void m_btnIf_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //-------------------------------------------------
        private void AddNewByDragDrop(C2iExpression formule)
        {
            if (formule != null)
            {
                if (formule is C2iExpressionObjetNeedTypeParent)
                {
                    C2iExpression formuleObj = new C2iExpressionObjet();
                    formuleObj.Parametres.Add(new C2iExpressionNull());
                    formuleObj.Parametres.Add(formule);
                    formule = formuleObj;
                }
                CRepresentationExpressionGraphique obj = new CRepresentationExpressionGraphique(formule);
                CDonneeDragDropObjetGraphique data = new CDonneeDragDropObjetGraphique(Name, obj);
                DoDragDrop(data, DragDropEffects.Copy);
                m_bGererMouseUp = false;
            }
        }

        //-------------------------------------------------
        private bool m_bGererMouseUp = true;

        //-------------------------------------------------
        private void m_DragAddNew_BeginDragDrop(object sender, EventArgs e)
        {
            AddNewByDragDrop(new C2iExpressionNull());
        }

        private void m_btnDragIf_BeginDragDrop(object sender, EventArgs e)
        {
            AddNewByDragDrop(new C2iExpressionSi());
        }

        private void m_dragConstante_BeginDragDrop(object sender, EventArgs e)
        {
            AddNewByDragDrop(new C2iExpressionConstante(0));
        }

        private void m_dragSetVar_BeginDragDrop(object sender, EventArgs e)
        {
            AddNewByDragDrop(new C2iExpressionSetVariable());
        }

        private void m_DragAddNew_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_bGererMouseUp)
            {
                AssureMenuAddFormule();
                m_menuAddFormule.Show(m_DragAddNew, new Point(0, m_DragAddNew.Height));
            }
            m_bGererMouseUp = true;
        }

        //-------------------------------------------------
        private class CMenuNewFormule : ToolStripMenuItem
        {
            public C2iExpressionAnalysable Formule { get; set; }
            public CMenuNewFormule(C2iExpressionAnalysable formule)
            {
                if (formule != null)
                {
                    Formule = formule;
                    CInfo2iExpression info = formule.GetInfos();
                    Text = info.Texte;
                    ToolTipText = info.Description;
                }
            }
        }

        //-------------------------------------------------
        public void AssureMenuAddFormule()
        {
            if (m_menuAddFormule.Items.Count != 0)
                return;
            Dictionary<string, ToolStripMenuItem> dicRubriques = new Dictionary<string, ToolStripMenuItem>();
            foreach (IExpression expression in CAllocateur2iExpression.ToutesExpressions)
            {
                C2iExpressionAnalysable expAn = expression as C2iExpressionAnalysable;
                if (expAn != null)
                {
                    CInfo2iExpression info = expAn.GetInfos();
                    if (info.Categorie.Length > 0 && info.Texte.Length > 0)
                    {
                        ToolStripMenuItem menuRubrique = null;
                        if (!dicRubriques.TryGetValue(info.Categorie, out menuRubrique))
                        {
                            menuRubrique = new ToolStripMenuItem(info.Categorie);
                            dicRubriques[info.Categorie] = menuRubrique;
                        }
                        CMenuNewFormule menuFormule = new CMenuNewFormule(expAn);
                        menuRubrique.DropDownItems.Add(menuFormule);
                        menuFormule.MouseDown += new MouseEventHandler(menuFormule_MouseDown);
                    }
                }
            }
            List<ToolStripMenuItem> lst = new List<ToolStripMenuItem>(dicRubriques.Values);
            lst.Sort( (m1, m2)=>m1.Text.CompareTo(m2.Text));
            m_menuAddFormule.Items.AddRange ( lst.ToArray() );
            foreach ( ToolStripMenuItem item in lst )
            {
                List<ToolStripMenuItem> childs = new List<ToolStripMenuItem>();
                foreach ( ToolStripMenuItem c in item.DropDownItems )
                    childs.Add ( c );
                childs.Sort( (m1, m2)=>m1.Text.CompareTo(m2.Text) );
                item.DropDownItems.Clear();
                item.DropDownItems.AddRange ( childs.ToArray() );
            }

        }

        //---------------------------------------------------------------
        void menuFormule_MouseDown(object sender, MouseEventArgs e)
        {
            CMenuNewFormule menu = sender as CMenuNewFormule;
            if (menu != null)
            {
                AddNewByDragDrop(menu.Formule);
            }
        }

        private void m_timerSelection_Tick(object sender, EventArgs e)
        {
            m_timerSelection.Stop();
            UpdatePanelParametres();
        }

        private bool m_editeur_AskCreationVariable(CDefinitionProprieteDynamiqueVariableFormule defACreer)
        {
            foreach (CDefinitionProprieteDynamiqueVariableFormule defExistante in RepresentationEditee.Variables)
            {
                if (defExistante.Nom.ToUpper() == defACreer.Nom.ToUpper())
                {
                    MessageBox.Show(I.T("Variable @1 already exists|20018", defACreer.Nom));
                    return false;
                }
            }
            RepresentationEditee.AddVariable(defACreer);
            UpdateVariables();
            return true;
        }

        private void m_menuAddFormule_Opening(object sender, CancelEventArgs e)
        {

        }
       



    }
}
