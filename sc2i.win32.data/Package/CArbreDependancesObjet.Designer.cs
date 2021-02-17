namespace sc2i.win32.data.Package
{
    partial class CArbreDependancesObjet
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
            if ( m_entitesManager != null )
            {
                m_configParDefaut = m_entitesManager.ConfigurationRecherche;
            }
            if (m_threadRemplissage != null)
            {
                m_threadRemplissage.Abort();
                m_threadRemplissage = null;
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CArbreDependancesObjet));
            this.m_arbre = new System.Windows.Forms.TreeView();
            this.m_menuArbre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuVoirDependances = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuAfficherEntite = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuIgnorerCeType = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuAnalyserAutomatiquementCeType = new System.Windows.Forms.ToolStripMenuItem();
            this.m_treeImages = new System.Windows.Forms.ImageList(this.components);
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_btnRefresh = new System.Windows.Forms.Button();
            this.m_btnParametres = new System.Windows.Forms.Button();
            this.m_panelTitres = new System.Windows.Forms.Panel();
            this.m_spinner = new System.Windows.Forms.PictureBox();
            this.m_panelProgression = new System.Windows.Forms.Panel();
            this.m_lblProgression = new sc2i.win32.common.CLabelProgression();
            this.m_menuArbre.SuspendLayout();
            this.m_panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinner)).BeginInit();
            this.m_panelProgression.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbre
            // 
            this.m_arbre.CheckBoxes = true;
            this.m_arbre.ContextMenuStrip = this.m_menuArbre;
            this.m_arbre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbre.ImageIndex = 0;
            this.m_arbre.ImageList = this.m_treeImages;
            this.m_arbre.Location = new System.Drawing.Point(0, 51);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.SelectedImageIndex = 0;
            this.m_arbre.Size = new System.Drawing.Size(320, 235);
            this.m_arbre.TabIndex = 0;
            this.m_arbre.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_arbre_NodeMouseClick);
            // 
            // m_menuArbre
            // 
            this.m_menuArbre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuVoirDependances,
            this.m_menuAfficherEntite,
            this.m_menuIgnorerCeType,
            this.m_menuAnalyserAutomatiquementCeType});
            this.m_menuArbre.Name = "m_menuArbre";
            this.m_menuArbre.Size = new System.Drawing.Size(239, 92);
            this.m_menuArbre.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuArbre_Opening);
            // 
            // m_menuVoirDependances
            // 
            this.m_menuVoirDependances.Name = "m_menuVoirDependances";
            this.m_menuVoirDependances.Size = new System.Drawing.Size(238, 22);
            this.m_menuVoirDependances.Text = "Search dependancies";
            this.m_menuVoirDependances.Click += new System.EventHandler(this.m_menuVoirDependances_Click);
            // 
            // m_menuAfficherEntite
            // 
            this.m_menuAfficherEntite.Name = "m_menuAfficherEntite";
            this.m_menuAfficherEntite.Size = new System.Drawing.Size(238, 22);
            this.m_menuAfficherEntite.Text = "Display";
            this.m_menuAfficherEntite.Click += new System.EventHandler(this.m_menuAfficherEntite_Click);
            // 
            // m_menuIgnorerCeType
            // 
            this.m_menuIgnorerCeType.CheckOnClick = true;
            this.m_menuIgnorerCeType.Name = "m_menuIgnorerCeType";
            this.m_menuIgnorerCeType.Size = new System.Drawing.Size(238, 22);
            this.m_menuIgnorerCeType.Text = "Ignore this type";
            this.m_menuIgnorerCeType.Click += new System.EventHandler(this.m_menuIgnorerCeType_Click);
            // 
            // m_menuAnalyserAutomatiquementCeType
            // 
            this.m_menuAnalyserAutomatiquementCeType.CheckOnClick = true;
            this.m_menuAnalyserAutomatiquementCeType.Name = "m_menuAnalyserAutomatiquementCeType";
            this.m_menuAnalyserAutomatiquementCeType.Size = new System.Drawing.Size(238, 22);
            this.m_menuAnalyserAutomatiquementCeType.Text = "Automatically analyse this type";
            this.m_menuAnalyserAutomatiquementCeType.Click += new System.EventHandler(this.m_menuAnalyserAutomatiquementCeType_Click);
            // 
            // m_treeImages
            // 
            this.m_treeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_treeImages.ImageStream")));
            this.m_treeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.m_treeImages.Images.SetKeyName(0, "Simplefolder.png");
            this.m_treeImages.Images.SetKeyName(1, "GenericObject16.png");
            this.m_treeImages.Images.SetKeyName(2, "GenericObject16 green.png");
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_btnRefresh);
            this.m_panelTop.Controls.Add(this.m_btnParametres);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(320, 25);
            this.m_panelTop.TabIndex = 1;
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnRefresh.Image = global::sc2i.win32.data.Properties.Resources.Refresh;
            this.m_btnRefresh.Location = new System.Drawing.Point(238, 0);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(41, 25);
            this.m_btnRefresh.TabIndex = 0;
            this.m_btnRefresh.UseVisualStyleBackColor = true;
            this.m_btnRefresh.Click += new System.EventHandler(this.m_btnRefresh_Click);
            // 
            // m_btnParametres
            // 
            this.m_btnParametres.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnParametres.Image = global::sc2i.win32.data.Properties.Resources.Parametres;
            this.m_btnParametres.Location = new System.Drawing.Point(279, 0);
            this.m_btnParametres.Name = "m_btnParametres";
            this.m_btnParametres.Size = new System.Drawing.Size(41, 25);
            this.m_btnParametres.TabIndex = 1;
            this.m_btnParametres.UseVisualStyleBackColor = true;
            this.m_btnParametres.Click += new System.EventHandler(this.m_btnParametres_Click);
            // 
            // m_panelTitres
            // 
            this.m_panelTitres.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTitres.Location = new System.Drawing.Point(0, 25);
            this.m_panelTitres.Name = "m_panelTitres";
            this.m_panelTitres.Size = new System.Drawing.Size(320, 26);
            this.m_panelTitres.TabIndex = 2;
            // 
            // m_spinner
            // 
            this.m_spinner.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_spinner.Image = global::sc2i.win32.data.Properties.Resources.spinner_white;
            this.m_spinner.Location = new System.Drawing.Point(279, 0);
            this.m_spinner.Name = "m_spinner";
            this.m_spinner.Size = new System.Drawing.Size(41, 41);
            this.m_spinner.TabIndex = 0;
            this.m_spinner.TabStop = false;
            this.m_spinner.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_spinner_MouseUp);
            // 
            // m_panelProgression
            // 
            this.m_panelProgression.Controls.Add(this.m_spinner);
            this.m_panelProgression.Controls.Add(this.m_lblProgression);
            this.m_panelProgression.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelProgression.Location = new System.Drawing.Point(0, 286);
            this.m_panelProgression.Name = "m_panelProgression";
            this.m_panelProgression.Size = new System.Drawing.Size(320, 41);
            this.m_panelProgression.TabIndex = 3;
            this.m_panelProgression.Visible = false;
            // 
            // m_lblProgression
            // 
            this.m_lblProgression.CanCancel = false;
            this.m_lblProgression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblProgression.LabelSeparator = "-";
            this.m_lblProgression.Location = new System.Drawing.Point(0, 0);
            this.m_lblProgression.Name = "m_lblProgression";
            this.m_lblProgression.Size = new System.Drawing.Size(320, 41);
            this.m_lblProgression.TabIndex = 1;
            // 
            // CArbreDependancesObjet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_arbre);
            this.Controls.Add(this.m_panelTitres);
            this.Controls.Add(this.m_panelTop);
            this.Controls.Add(this.m_panelProgression);
            this.Name = "CArbreDependancesObjet";
            this.Size = new System.Drawing.Size(320, 327);
            this.m_menuArbre.ResumeLayout(false);
            this.m_panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_spinner)).EndInit();
            this.m_panelProgression.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbre;
        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Panel m_panelTitres;
        private System.Windows.Forms.ImageList m_treeImages;
        private System.Windows.Forms.PictureBox m_spinner;
        private System.Windows.Forms.ContextMenuStrip m_menuArbre;
        private System.Windows.Forms.ToolStripMenuItem m_menuVoirDependances;
        private System.Windows.Forms.ToolStripMenuItem m_menuAfficherEntite;
        private System.Windows.Forms.Button m_btnRefresh;
        private System.Windows.Forms.Button m_btnParametres;
        private System.Windows.Forms.ToolStripMenuItem m_menuIgnorerCeType;
        private System.Windows.Forms.ToolStripMenuItem m_menuAnalyserAutomatiquementCeType;
        private System.Windows.Forms.Panel m_panelProgression;
        private common.CLabelProgression m_lblProgression;
    }
}
