using sc2i.win32.expression.variablesDynamiques;
namespace futurocom.win32.easyquery
{
    partial class CEditeurEasyQuery
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEditeurEasyQuery));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_btnOptionsQuery = new System.Windows.Forms.LinkLabel();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkJointure = new System.Windows.Forms.RadioButton();
            this.m_chkZoom = new System.Windows.Forms.RadioButton();
            this.m_chkCurseur = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_panelVariables = new sc2i.win32.expression.variablesDynamiques.CControleVariablesDynamiques();
            this.label1 = new System.Windows.Forms.Label();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_arbre = new futurocom.win32.easyquery.CTreeListeTables();
            this.m_menuSaveOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuSaveWithSources = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSaveWithoutSources = new System.Windows.Forms.ToolStripMenuItem();
            this.m_editeur = new futurocom.win32.easyquery.CEditeurObjetsEasyQuery();
            this.m_panelBas.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_menuSaveOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(184, 0);
            this.m_extModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 319);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(434, 0);
            this.m_extModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 319);
            this.splitter2.TabIndex = 4;
            this.splitter2.TabStop = false;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.m_btnOptionsQuery);
            this.m_panelBas.Controls.Add(this.m_btnOpen);
            this.m_panelBas.Controls.Add(this.m_btnSave);
            this.m_panelBas.Controls.Add(this.m_btnPaste);
            this.m_panelBas.Controls.Add(this.m_btnCopy);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 319);
            this.m_extModeEdition.SetModeEdition(this.m_panelBas, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(437, 42);
            this.m_panelBas.TabIndex = 7;
            // 
            // m_btnOptionsQuery
            // 
            this.m_btnOptionsQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOptionsQuery.Location = new System.Drawing.Point(331, 24);
            this.m_extModeEdition.SetModeEdition(this.m_btnOptionsQuery, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnOptionsQuery.Name = "m_btnOptionsQuery";
            this.m_btnOptionsQuery.Size = new System.Drawing.Size(100, 18);
            this.m_btnOptionsQuery.TabIndex = 33;
            this.m_btnOptionsQuery.TabStop = true;
            this.m_btnOptionsQuery.Text = "Options|20076";
            this.m_btnOptionsQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnOptionsQuery.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnOptionsQuery_LinkClicked);
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(114, 10);
            this.m_extModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.m_btnOpen.TabIndex = 32;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnLoad_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(84, 10);
            this.m_extModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.m_btnSave.TabIndex = 31;
            this.m_btnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_btnSave_MouseUp);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(33, 10);
            this.m_extModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(24, 23);
            this.m_btnPaste.TabIndex = 30;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(3, 10);
            this.m_extModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.m_btnCopy.TabIndex = 29;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.m_chkJointure);
            this.panel1.Controls.Add(this.m_chkZoom);
            this.panel1.Controls.Add(this.m_chkCurseur);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(187, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 30);
            this.panel1.TabIndex = 8;
            // 
            // m_chkJointure
            // 
            this.m_chkJointure.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkJointure.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkJointure.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkJointure.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkJointure.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkJointure.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkJointure.Image = global::futurocom.win32.easyquery.Properties.Resources.Jointure;
            this.m_chkJointure.Location = new System.Drawing.Point(68, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkJointure, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkJointure.Name = "m_chkJointure";
            this.m_chkJointure.Size = new System.Drawing.Size(34, 30);
            this.m_chkJointure.TabIndex = 6;
            this.m_chkJointure.Click += new System.EventHandler(this.m_chkJointure_Click);
            // 
            // m_chkZoom
            // 
            this.m_chkZoom.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkZoom.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkZoom.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkZoom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkZoom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkZoom.Image = ((System.Drawing.Image)(resources.GetObject("m_chkZoom.Image")));
            this.m_chkZoom.Location = new System.Drawing.Point(34, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkZoom, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkZoom.Name = "m_chkZoom";
            this.m_chkZoom.Size = new System.Drawing.Size(34, 30);
            this.m_chkZoom.TabIndex = 5;
            this.m_chkZoom.Click += new System.EventHandler(this.m_chkZoom_Click);
            // 
            // m_chkCurseur
            // 
            this.m_chkCurseur.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkCurseur.Checked = true;
            this.m_chkCurseur.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkCurseur.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkCurseur.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkCurseur.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkCurseur.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkCurseur.Image = ((System.Drawing.Image)(resources.GetObject("m_chkCurseur.Image")));
            this.m_chkCurseur.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkCurseur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkCurseur.Name = "m_chkCurseur";
            this.m_chkCurseur.Size = new System.Drawing.Size(34, 30);
            this.m_chkCurseur.TabIndex = 4;
            this.m_chkCurseur.TabStop = true;
            this.m_chkCurseur.Click += new System.EventHandler(this.m_chkCurseur_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_panelVariables);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(437, 0);
            this.m_extModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(168, 361);
            this.panel2.TabIndex = 9;
            // 
            // m_panelVariables
            // 
            this.m_panelVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelVariables.Location = new System.Drawing.Point(0, 23);
            this.m_panelVariables.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelVariables.Name = "m_panelVariables";
            this.m_panelVariables.ShortMode = true;
            this.m_panelVariables.Size = new System.Drawing.Size(168, 338);
            this.m_panelVariables.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Variables|20053";
            // 
            // m_arbre
            // 
            this.m_arbre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_arbre.Location = new System.Drawing.Point(0, 0);
            this.m_arbre.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_arbre, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.Size = new System.Drawing.Size(184, 319);
            this.m_arbre.TabIndex = 0;
            // 
            // m_menuSaveOptions
            // 
            this.m_menuSaveOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuSaveWithSources,
            this.m_menuSaveWithoutSources});
            this.m_extModeEdition.SetModeEdition(this.m_menuSaveOptions, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuSaveOptions.Name = "m_menuSaveOptions";
            this.m_menuSaveOptions.Size = new System.Drawing.Size(219, 48);
            // 
            // m_menuSaveWithSources
            // 
            this.m_menuSaveWithSources.Name = "m_menuSaveWithSources";
            this.m_menuSaveWithSources.Size = new System.Drawing.Size(218, 22);
            this.m_menuSaveWithSources.Text = "Save with Sources|20067";
            this.m_menuSaveWithSources.Click += new System.EventHandler(this.m_menuSaveWithSources_Click);
            // 
            // m_menuSaveWithoutSources
            // 
            this.m_menuSaveWithoutSources.Name = "m_menuSaveWithoutSources";
            this.m_menuSaveWithoutSources.Size = new System.Drawing.Size(218, 22);
            this.m_menuSaveWithoutSources.Text = "Save without sources|20068";
            this.m_menuSaveWithoutSources.Click += new System.EventHandler(this.m_menuSaveWithoutSources_Click);
            // 
            // m_editeur
            // 
            this.m_editeur.AllowDrop = true;
            this.m_editeur.BackColor = System.Drawing.Color.White;
            this.m_editeur.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_editeur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_editeur.Echelle = 1F;
            this.m_editeur.EffetAjoutSuppression = false;
            this.m_editeur.EffetFonduMenu = true;
            this.m_editeur.EnDeplacement = true;
            this.m_editeur.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_editeur.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.m_editeur.HauteurMinimaleDesObjets = 10;
            this.m_editeur.HistorisationActive = true;
            this.m_editeur.LargeurMinimaleDesObjets = 10;
            this.m_editeur.Location = new System.Drawing.Point(187, 30);
            this.m_editeur.LockEdition = false;
            this.m_editeur.Marge = 10;
            this.m_editeur.MaxZoom = 6F;
            this.m_editeur.MinZoom = 0.2F;
            this.m_editeur.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_extModeEdition.SetModeEdition(this.m_editeur, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_editeur.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_editeur.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
            this.m_editeur.ModeSourisCustom = futurocom.win32.easyquery.CEditeurObjetsEasyQuery.EModeSourisCustom.Join;
            this.m_editeur.Name = "m_editeur";
            this.m_editeur.NoClipboard = false;
            this.m_editeur.NoDelete = false;
            this.m_editeur.NoDoubleClick = false;
            this.m_editeur.NombreHistorisation = 10;
            this.m_editeur.NoMenu = false;
            this.m_editeur.ObjetEdite = null;
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = true;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.m_editeur.Profil = cProfilEditeurObjetGraphique1;
            this.m_editeur.RefreshSelectionChanged = true;
            this.m_editeur.SelectionVisible = true;
            this.m_editeur.Size = new System.Drawing.Size(247, 289);
            this.m_editeur.TabIndex = 1;
            this.m_editeur.ToujoursAlignerSelonLesControles = true;
            this.m_editeur.ToujoursAlignerSurLaGrille = false;
            this.m_editeur.ElementPropertiesChanged += new System.EventHandler(this.m_editeur_ElementPropertiesChanged);
            this.m_editeur.AfterRemoveObjetGraphique += new System.EventHandler(this.m_editeur_AfterRemoveObjetGraphique);
            this.m_editeur.AfterAddElements += new sc2i.win32.common.EventHandlerPanelEditionGraphiqueSuppression(this.m_editeur_AfterAddElements);
            this.m_editeur.ModeSourisChanged += new System.EventHandler(this.m_editeur_ModeSourisChanged);
            // 
            // CEditeurEasyQuery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_editeur);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_arbre);
            this.Controls.Add(this.m_panelBas);
            this.Controls.Add(this.panel2);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CEditeurEasyQuery";
            this.Size = new System.Drawing.Size(605, 361);
            this.m_panelBas.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_menuSaveOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CTreeListeTables m_arbre;
        private CEditeurObjetsEasyQuery m_editeur;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel m_panelBas;
        private System.Windows.Forms.Button m_btnOpen;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.Button m_btnPaste;
        private System.Windows.Forms.Button m_btnCopy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton m_chkZoom;
        private System.Windows.Forms.RadioButton m_chkCurseur;
        private System.Windows.Forms.RadioButton m_chkJointure;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private CControleVariablesDynamiques m_panelVariables;
        private System.Windows.Forms.ContextMenuStrip m_menuSaveOptions;
        private System.Windows.Forms.ToolStripMenuItem m_menuSaveWithSources;
        private System.Windows.Forms.ToolStripMenuItem m_menuSaveWithoutSources;
        private System.Windows.Forms.LinkLabel m_btnOptionsQuery;
    }
}
