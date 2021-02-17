using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using sc2i.drawing;
using sc2i.win32.common;
using System.Windows.Forms;
using System.Collections;
using futurocom.easyquery;
using System.Data;
using System.Drawing;


namespace futurocom.win32.easyquery
{
    public class CEditeurObjetsEasyQuery : CPanelEditionObjetGraphique
    {
        public enum EModeSourisCustom
        {
            Join
        }

        private static Dictionary<Type, Type> m_dicTypesEditeurs = new Dictionary<Type, Type>();

        //--------------------------------------------------------
        /// <summary>
        /// Enregistre un nouvel éditeur . L'éditeur doit implementer IEditeurProprietesTableDefinition
        /// </summary>
        /// <param name="typeTable"></param>
        /// <param name="typeEditeur"></param>
        public static void RegisterEditeurProprietes(Type typeObjetDeQuery, Type typeEditeur)
        {
            m_dicTypesEditeurs[typeObjetDeQuery] = typeEditeur;
        }

        private EModeSourisCustom m_modeCustom = EModeSourisCustom.Join;

        private System.Windows.Forms.ContextMenuStrip m_menuActionsSurTables;
        private System.ComponentModel.IContainer components;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem m_menuBrowse;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem m_menuAddSousElement;
        private ToolStripMenuItem m_menuExpandTable;
        private ToolStripMenuItem m_menuCommentaires;
        private ToolStripMenuItem m_menuRemplaceSource;
        private System.Windows.Forms.ToolStripMenuItem m_menuProperties;
    
        public CEditeurObjetsEasyQuery()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_menuActionsSurTables = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuExpandTable = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuCommentaires = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuAddSousElement = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuRemplaceSource = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuActionsSurTables.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menuActionsSurTables
            // 
            this.m_menuActionsSurTables.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuExpandTable,
            this.m_menuCommentaires,
            this.toolStripMenuItem1,
            this.m_menuBrowse,
            this.toolStripMenuItem2,
            this.m_menuProperties,
            this.m_menuAddSousElement,
            this.m_menuRemplaceSource});
            this.m_menuActionsSurTables.Name = "m_menuActionsSurTables";
            this.m_menuActionsSurTables.Size = new System.Drawing.Size(187, 148);
            // 
            // m_menuExpandTable
            // 
            this.m_menuExpandTable.Name = "m_menuExpandTable";
            this.m_menuExpandTable.Size = new System.Drawing.Size(186, 22);
            this.m_menuExpandTable.Text = "Expand|20021";
            this.m_menuExpandTable.Click += new System.EventHandler(this.m_menuExpandTable_Click);
            // 
            // m_menuCommentaires
            // 
            this.m_menuCommentaires.Name = "m_menuCommentaires";
            this.m_menuCommentaires.Size = new System.Drawing.Size(186, 22);
            this.m_menuCommentaires.Text = "Comment|20021";
            this.m_menuCommentaires.Click += new System.EventHandler(this.m_menuCommentaires_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // m_menuBrowse
            // 
            this.m_menuBrowse.Name = "m_menuBrowse";
            this.m_menuBrowse.Size = new System.Drawing.Size(186, 22);
            this.m_menuBrowse.Text = "Browse|20006";
            this.m_menuBrowse.Click += new System.EventHandler(this.m_menuBrowse_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(183, 6);
            // 
            // m_menuProperties
            // 
            this.m_menuProperties.Name = "m_menuProperties";
            this.m_menuProperties.Size = new System.Drawing.Size(186, 22);
            this.m_menuProperties.Text = "Properties|20000";
            this.m_menuProperties.Click += new System.EventHandler(this.m_menuProperties_Click);
            // 
            // m_menuAddSousElement
            // 
            this.m_menuAddSousElement.Name = "m_menuAddSousElement";
            this.m_menuAddSousElement.Size = new System.Drawing.Size(186, 22);
            this.m_menuAddSousElement.Text = "Add|20009";
            this.m_menuAddSousElement.Click += new System.EventHandler(this.m_menuAddSousElement_Click);
            // 
            // m_menuRemplaceSource
            // 
            this.m_menuRemplaceSource.Name = "m_menuRemplaceSource";
            this.m_menuRemplaceSource.Size = new System.Drawing.Size(186, 22);
            this.m_menuRemplaceSource.Text = "Replace source|20074";
            // 
            // CEditeurObjetsEasyQuery
            // 
            this.BackColor = System.Drawing.Color.White;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.Name = "CEditeurObjetsEasyQuery";
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = true;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.Profil = cProfilEditeurObjetGraphique1;
            this.ToujoursAlignerSurLaGrille = false;
            this.m_menuActionsSurTables.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        //-----------------------------------------------------------------
        public EModeSourisCustom ModeSourisCustom
        {
            get
            {
                return m_modeCustom;
            }
            set
            {
                m_modeCustom = value;
                OnChangeModeSouris();
            }
        }

        //-----------------------------------------------------------------
        private class CMenuAddSousElement : ToolStripMenuItem
        {
            private CODEQFromObjetsSource m_sousElement = null;

            public CMenuAddSousElement(CODEQFromObjetsSource sousElement)
                : base(sousElement.TypeName)
            {
                m_sousElement = sousElement;
            }

            public CODEQFromObjetsSource SousElement
            {
                get
                {
                    return m_sousElement;
                }
            }
        }

        //-----------------------------------------------------------------
        protected override void AfficherMenuAdditonnel(System.Windows.Forms.ContextMenuStrip menu)
        {
            base.AfficherMenuAdditonnel(menu);
            //Ajoute les menus
            foreach (ToolStripItem item in new ArrayList(m_menuActionsSurTables.Items))
            {
                menu.Items.Add(item);
            }

            m_menuProperties.Visible = Selection.Count == 1 && CanEditeProprietes(Selection[0]);

            //Menu add
            m_menuAddSousElement.DropDownItems.Clear();
            if (Selection.Count >= 1)
            {
                CheckState? expandedState = null;
                Dictionary<Type, int> lstType = new Dictionary<Type, int>();
                foreach (I2iObjetGraphique obj in Selection)
                {
                    CODEQBase objet = obj as CODEQBase;
                    if (objet != null)
                    {
                        if ( expandedState == null )
                        {
                            if ( objet.IsExpanded )
                                expandedState = CheckState.Checked;
                            else
                                expandedState = CheckState.Unchecked;
                        }
                        else
                        {
                            if ( expandedState.Value == CheckState.Checked && !objet.IsExpanded ||
                                expandedState.Value == CheckState.Unchecked && objet.IsExpanded )
                                expandedState = CheckState.Indeterminate;
                        }
                        foreach (Type tp in objet.TypesDerivesPossibles)
                        {
                            int nNb = 0;
                            if (lstType.ContainsKey(tp))
                                nNb = lstType[tp];
                            nNb++;
                            lstType[tp] = nNb;
                        }
                    }
                }
                m_menuExpandTable.CheckState = expandedState != null ? expandedState.Value : CheckState.Unchecked;
                foreach ( KeyValuePair<Type,int> kv in lstType )
                {
                    if ( kv.Value == Selection.Count )
                    {
                        CODEQFromObjetsSource sousElement = Activator.CreateInstance(kv.Key) as CODEQFromObjetsSource;
                        if (sousElement != null && sousElement.NbSourceRequired == Selection.Count)
                        {
                            CMenuAddSousElement subMenu = new CMenuAddSousElement(sousElement);
                            subMenu.Click += new EventHandler(menuAddSousElement_Click);
                            m_menuAddSousElement.DropDownItems.Add(subMenu);
                        }
                    }
                }
            }
            m_menuAddSousElement.Visible = m_menuAddSousElement.DropDownItems.Count != 0;

            //Menu RemplaceSource
            m_menuRemplaceSource.DropDownItems.Clear();
            if ( Selection.Count == 1 )
            {
                CODEQFromObjetsSource tableFromSource = Selection[0] as CODEQFromObjetsSource;
                if ( tableFromSource == null )
                {
                    m_menuRemplaceSource.Visible = false;
                }
                else
                {
                    m_menuRemplaceSource.Visible = true;
                    IEnumerable<string> nomsParametres = tableFromSource.NomsSources;
                    IObjetDeEasyQuery[] sources = tableFromSource.ElementsSource;
                    for ( int n = 0; n < tableFromSource.NbSourceRequired; n++ )
                    {
                        ToolStripMenuItem itemSource = new ToolStripMenuItem();
                        string strText = n < nomsParametres.Count() ? nomsParametres.ElementAt(n) : (n + 1).ToString();
                        if ( sources.Length > n )
                        {
                            strText += " (" + sources[n].NomFinal+")";                            
                        }
                        itemSource.Text = strText;
                        itemSource.Tag = n;
                        itemSource.DropDownItems.Add(new ToolStripMenuItem(""));
                        itemSource.DropDownOpening += itemSource_DropDownOpening;
                        m_menuRemplaceSource.DropDownItems.Add(itemSource);
                    }
                }
            }

        }

        void itemSource_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CEasyQuery query = ObjetEdite as CEasyQuery;
            CODEQFromObjetsSource tableFromSource = Selection.Count==1?Selection[0] as CODEQFromObjetsSource:null;
            if (item != null && query != null && tableFromSource != null)
            {
                item.DropDownItems.Clear();
                List<IObjetDeEasyQuery> lst = new List<IObjetDeEasyQuery>();
                foreach (IObjetDeEasyQuery obj in query.Childs)
                    lst.Add(obj);
                lst.Sort((x, y) => x.NomFinal.CompareTo(y.NomFinal));
                foreach (IObjetDeEasyQuery objet in lst)
                {
                    if (objet.Id != tableFromSource.Id)
                    {
                        ToolStripMenuItem itemNewSource = new ToolStripMenuItem(objet.NomFinal);
                        itemNewSource.Tag = objet;
                        itemNewSource.Click += itemNewSource_Click;
                        item.DropDownItems.Add(itemNewSource);
                    }
                }
            }
        }

        //-----------------------------------------------------------------
        void itemNewSource_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CODEQFromObjetsSource tableFromSource = Selection.Count == 1 ? Selection[0] as CODEQFromObjetsSource : null;
            ToolStripMenuItem itemParent = item != null ? item.OwnerItem as ToolStripMenuItem : null;
            IObjetDeEasyQuery newSource = item != null?item.Tag as IObjetDeEasyQuery:null;
            int nIndexSource = itemParent != null && itemParent.Tag is int?(int)itemParent.Tag:-1;
            if ( newSource != null && nIndexSource >= 0 && tableFromSource != null )
            {
                if (MessageBox.Show(I.T("Replace Table @1 by table @2 ?",
                    itemParent.Text, item.Text),
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CResultAErreur result = tableFromSource.SetSource(nIndexSource, newSource);
                    if (!result)
                        CFormAlerte.Afficher(result.Erreur);
                    else
                        Refresh();
                }
            }
        }

        

        //-----------------------------------------------------------------
        void menuAddSousElement_Click(object sender, EventArgs e)
        {
            CMenuAddSousElement menu = sender as CMenuAddSousElement;
            if (menu != null)
            {
                CODEQFromObjetsSource objet = menu.SousElement;
                List<IObjetDeEasyQuery> lstSources = new List<IObjetDeEasyQuery>();
                foreach (I2iObjetGraphique obj in Selection)
                {
                    if (obj is IObjetDeEasyQuery)
                        lstSources.Add(obj as IObjetDeEasyQuery);
                }
                if ( lstSources.Count == Selection.Count )
                {
                    Point pt = lstSources[0].Position;
                    pt.Offset(new Point(lstSources[0].Size.Width * 2, lstSources[0].Size.Height / 3));
                    objet.Position = pt;
                    objet.Parent = ObjetEdite;
                    objet.ElementsSource = lstSources.ToArray();
                    if (EditeProprietes(objet))
                    {
                        ObjetEdite.AddChild(objet);
                        Refresh();
                    }
                }
            }
        }

        //-----------------------------------------------------------------
        private Type GetTypeEditeurForObjet(I2iObjetGraphique objet)
        {
            if (objet == null)
                return null;
            Type tp = objet.GetType();
            Type tpEditeur = null;
            while (tp != null)
            {
                if (m_dicTypesEditeurs.TryGetValue(tp, out tpEditeur))
                    return tpEditeur;
                tp = tp.BaseType;
            }
            return null;
        }

        //-----------------------------------------------------------------
        public bool CanEditeProprietes(I2iObjetGraphique objet)
        {
            return GetTypeEditeurForObjet(objet) != null;
        }

        public event EventHandler ElementPropertiesChanged;
        //-----------------------------------------------------------------
        public bool EditeProprietes(I2iObjetGraphique objet)
        {
            IObjetDeEasyQuery objetDeQuery = objet as IObjetDeEasyQuery;
            if (objetDeQuery == null)
                return false;
            Type tpEditeur = GetTypeEditeurForObjet(objetDeQuery);
            if (tpEditeur == null)
                return false;
            IEditeurProprietesObjetDeEasyQuery editeur = Activator.CreateInstance(tpEditeur) as IEditeurProprietesObjetDeEasyQuery;
            bool bResult = false;
            if (editeur != null)
            {
                bResult = editeur.EditeProprietes(objetDeQuery);
            }
            if (editeur is IDisposable)
                ((IDisposable)editeur).Dispose();
            if (bResult)
            {
                CEasyQuery query = ObjetEdite as CEasyQuery;
                if (query != null && query.ListeSources != null)
                {
                    CODEQTableFromBase odeqFromBase = objetDeQuery as CODEQTableFromBase;
                    if (odeqFromBase != null && odeqFromBase.TableDefinition != null)
                    {
                        query.ListeSources.ClearCache(odeqFromBase.TableDefinition);
                    }
                }
                Refresh();
                if (ElementPropertiesChanged != null)
                    ElementPropertiesChanged(objetDeQuery, null);
            }
            return bResult;
        }

        //-----------------------------------------------------------------
        private void m_menuProperties_Click(object sender, EventArgs e)
        {
            if (Selection.Count() == 1)
                EditeProprietes(Selection[0]);
        }

        //-----------------------------------------------------------------
        private void m_menuBrowse_Click(object sender, EventArgs e)
        {
            if (Selection.Count == 1)
            {
                IObjetDeEasyQuery obj = Selection[0] as IObjetDeEasyQuery;
                CResultAErreur result = CResultAErreur.True;
                ((CEasyQuery)ObjetEdite).ClearCache();
                if (obj != null)
                    result = obj.GetDatas(((CEasyQuery)ObjetEdite).ListeSources);
                if (!result || !(result.Data is DataTable))
                    CFormAfficheErreur.Show(result.Erreur);
                else
                    CFormVisuTable.ShowTable(result.Data as DataTable);
            }
            
        }

        //-----------------------------------------------------------------
        private void m_menuExpandTable_Click(object sender, EventArgs e)
        {
            foreach (IObjetDeEasyQuery objet in Selection)
            {
                CODEQBase o = objet as CODEQBase;
                if (o != null)
                    o.IsExpanded = m_menuExpandTable.CheckState == CheckState.Unchecked; ;
            }
            Refresh();
        }

        //-----------------------------------------------------------------
        private void m_menuCommentaires_Click(object sender, EventArgs e)
        {
            if (Selection.Count == 1)
            {
                CODEQBase ob = Selection[0] as CODEQBase;
                if (ob != null)
                {
                    ob.Commentaires = CFormCommentaires.GetTexte(ob.Commentaires);
                }
            }
        }

        //-----------------------------------------------------------------
        private IColumnDeEasyQuery m_colStartJoin = null;
        private CODEQBase m_table1Join = null;
        private Rectangle m_rectJoin = Rectangle.Empty;
        private Rectangle m_rectColStartJoin = Rectangle.Empty;
        public override void OnMouseDownNonStandard(object sender, MouseEventArgs e)
        {
            m_colStartJoin = null;
            if (ModeSourisCustom == EModeSourisCustom.Join && e.Button == MouseButtons.Left)
            {
                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                m_table1Join = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CODEQBase;
                if (m_table1Join != null)
                {
                    m_colStartJoin = m_table1Join.GetColonneAt(ptLogique);
                    if (m_colStartJoin != null)
                    {
                        Rectangle rctCol = m_table1Join.GetRectAbsoluColonne(m_colStartJoin);
                        rctCol = new Rectangle(GetDisplayPointFromLogical(rctCol.Location),
                            GetDisplaySizeFromLogical(rctCol.Size));
                        m_rectColStartJoin = rctCol;
                        m_rectJoin = m_rectColStartJoin;
                    }
                }
            }
        }

        //-----------------------------------------------------------------
        public override void OnMouseMoveNonStandard(object sender, MouseEventArgs e)
        {
            if (ModeSourisCustom == EModeSourisCustom.Join && m_colStartJoin != null && e.Button == MouseButtons.Left)
            {
                Graphics g = CreateGraphics();
                
                Point ptCentre = new Point(m_rectColStartJoin.Left + m_rectColStartJoin.Width / 2,
                    m_rectColStartJoin.Top + m_rectColStartJoin.Height / 2);
                Rectangle rct = m_rectJoin;

                Point ptLogique = GetLogicalPointFromDisplay ( new Point ( e.X, e.Y ));
                IColumnDeEasyQuery colDest = null;
                CODEQBase dest = ObjetEdite.SelectionnerElementDuDessus ( ptLogique ) as CODEQBase;
                if (dest == m_table1Join)
                    dest = null;
                colDest = dest != null ?dest.GetColonneAt ( ptLogique ):null;
                Rectangle rctDest = new Rectangle ( e.X, e.Y, 1, 1 );
                if ( colDest != null )
                {
                    rctDest = dest.GetRectAbsoluColonne ( colDest );
                    rctDest = new Rectangle ( GetDisplayPointFromLogical ( rctDest.Location ),
                        GetDisplaySizeFromLogical ( rctDest.Size ));
                }  
              
                rct.Inflate(2, 2);
                using ( Bitmap bmp = DernierApercuToDispose )
                    g.DrawImage(bmp, rct, rct, GraphicsUnit.Pixel);
                Pen pen = new Pen(Color.Blue);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                Brush br = new SolidBrush(Color.FromArgb(128, 0, 0, 255));
                g.FillRectangle(br, m_rectColStartJoin);
                if ( colDest != null )
                    g.FillRectangle ( br, rctDest );
                br.Dispose();
                g.DrawLine(pen, ptCentre, new Point ( e.X, e.Y));
                pen.Dispose();
                g.Dispose();
                m_rectJoin = Rectangle.Union ( m_rectColStartJoin, rctDest );
            }
        }

        //-----------------------------------------------------------------
        public override void OnMouseUpNonStandard(object sender, MouseEventArgs e)
        {
            if (ModeSourisCustom == EModeSourisCustom.Join && e.Button == MouseButtons.Left )
            {
                Point ptLogique = GetLogicalPointFromDisplay(new Point(e.X, e.Y));
                CODEQBase table = ObjetEdite.SelectionnerElementDuDessus(ptLogique) as CODEQBase;
                if (table != null && table != m_table1Join)
                {
                    IColumnDeEasyQuery colEndJoin = table.GetColonneAt(ptLogique);
                    if (colEndJoin != null)
                    {
                        CODEQJointure jointure = new CODEQJointure();
                        jointure.Parent = ObjetEdite;
                        jointure.ElementsSource = new IObjetDeEasyQuery[] { m_table1Join, table };
                        jointure.NomFinal = m_table1Join.NomFinal + "-" + table.NomFinal;
                        if (jointure.AddParametre(m_colStartJoin, colEndJoin))
                        {
                            Rectangle rct = Rectangle.Union(m_table1Join.RectangleAbsolu, table.RectangleAbsolu);
                            rct.Offset(rct.Width / 2 - jointure.Size.Width / 2, rct.Height / 2 - jointure.Size.Height / 2);
                            jointure.Position = new Point(rct.Left, rct.Top);
                            ObjetEdite.AddChild(jointure);
                            ModeSouris = EModeSouris.Selection;
                        }
                    }
                }
                m_colStartJoin = null;
                m_table1Join = null;
                Refresh();
            }
        }

        private void m_menuAddSousElement_Click(object sender, EventArgs e)
        {

        }

        
    }
}
