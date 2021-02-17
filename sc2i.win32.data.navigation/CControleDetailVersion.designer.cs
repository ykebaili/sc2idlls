namespace sc2i.win32.data.navigation
{
	partial class CControleDetailVersion
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
				if (m_contexteDonnees != null)
					m_contexteDonnees.Dispose();
				m_contexteDonnees = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControleDetailVersion));
            this.m_arbreObjet = new System.Windows.Forms.TreeView();
            this.m_listeImagesObjet = new System.Windows.Forms.ImageList(this.components);
            this.m_imagesHistoriqueActuel = new System.Windows.Forms.ImageList(this.components);
            this.m_grilleProprietes = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_lblInfo = new System.Windows.Forms.Label();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_panelBasGauche = new System.Windows.Forms.Panel();
            this.m_imageTypeOperation = new System.Windows.Forms.PictureBox();
            this.m_imageState = new System.Windows.Forms.PictureBox();
            this.m_panelRestore = new System.Windows.Forms.Panel();
            this.m_lnkRestore = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_grilleProprietes)).BeginInit();
            this.m_panelBas.SuspendLayout();
            this.m_panelBasGauche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageTypeOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageState)).BeginInit();
            this.m_panelRestore.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbreObjet
            // 
            this.m_arbreObjet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreObjet.ImageIndex = 0;
            this.m_arbreObjet.ImageList = this.m_listeImagesObjet;
            this.m_arbreObjet.Location = new System.Drawing.Point(0, 0);
            this.m_arbreObjet.Name = "m_arbreObjet";
            this.m_arbreObjet.SelectedImageIndex = 0;
            this.m_arbreObjet.Size = new System.Drawing.Size(168, 266);
            this.m_arbreObjet.StateImageList = this.m_imagesHistoriqueActuel;
            this.m_arbreObjet.TabIndex = 0;
            this.m_arbreObjet.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbreObjet_BeforeExpand);
            this.m_arbreObjet.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreObjet_AfterSelect);
            // 
            // m_listeImagesObjet
            // 
            this.m_listeImagesObjet.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_listeImagesObjet.ImageStream")));
            this.m_listeImagesObjet.TransparentColor = System.Drawing.Color.Transparent;
            this.m_listeImagesObjet.Images.SetKeyName(0, "PetiteNote.gif");
            this.m_listeImagesObjet.Images.SetKeyName(1, "add.gif");
            this.m_listeImagesObjet.Images.SetKeyName(2, "delete.gif");
            this.m_listeImagesObjet.Images.SetKeyName(3, "edit.gif");
            // 
            // m_imagesHistoriqueActuel
            // 
            this.m_imagesHistoriqueActuel.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesHistoriqueActuel.ImageStream")));
            this.m_imagesHistoriqueActuel.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesHistoriqueActuel.Images.SetKeyName(0, "Shell32 161.ico");
            this.m_imagesHistoriqueActuel.Images.SetKeyName(1, "historique.gif");
            this.m_imagesHistoriqueActuel.Images.SetKeyName(2, "plus.bmp");
            this.m_imagesHistoriqueActuel.Images.SetKeyName(3, "moins.bmp");
            // 
            // m_grilleProprietes
            // 
            this.m_grilleProprietes.AllowUserToAddRows = false;
            this.m_grilleProprietes.AllowUserToDeleteRows = false;
            this.m_grilleProprietes.BackgroundColor = System.Drawing.Color.White;
            this.m_grilleProprietes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_grilleProprietes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_grilleProprietes.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_grilleProprietes.Location = new System.Drawing.Point(171, 0);
            this.m_grilleProprietes.Name = "m_grilleProprietes";
            this.m_grilleProprietes.ReadOnly = true;
            this.m_grilleProprietes.RowHeadersVisible = false;
            this.m_grilleProprietes.Size = new System.Drawing.Size(355, 266);
            this.m_grilleProprietes.TabIndex = 1;
            this.m_grilleProprietes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.m_grilleProprietes_CellFormatting);
            this.m_grilleProprietes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(168, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 266);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_lblInfo
            // 
            this.m_lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblInfo.Location = new System.Drawing.Point(16, 0);
            this.m_lblInfo.Name = "m_lblInfo";
            this.m_lblInfo.Size = new System.Drawing.Size(510, 54);
            this.m_lblInfo.TabIndex = 3;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.m_lblInfo);
            this.m_panelBas.Controls.Add(this.m_panelBasGauche);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 284);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(526, 54);
            this.m_panelBas.TabIndex = 4;
            // 
            // m_panelBasGauche
            // 
            this.m_panelBasGauche.Controls.Add(this.m_imageTypeOperation);
            this.m_panelBasGauche.Controls.Add(this.m_imageState);
            this.m_panelBasGauche.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelBasGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelBasGauche.Name = "m_panelBasGauche";
            this.m_panelBasGauche.Size = new System.Drawing.Size(16, 54);
            this.m_panelBasGauche.TabIndex = 4;
            // 
            // m_imageTypeOperation
            // 
            this.m_imageTypeOperation.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_imageTypeOperation.Location = new System.Drawing.Point(0, 16);
            this.m_imageTypeOperation.Name = "m_imageTypeOperation";
            this.m_imageTypeOperation.Size = new System.Drawing.Size(16, 19);
            this.m_imageTypeOperation.TabIndex = 6;
            this.m_imageTypeOperation.TabStop = false;
            // 
            // m_imageState
            // 
            this.m_imageState.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_imageState.Location = new System.Drawing.Point(0, 0);
            this.m_imageState.Name = "m_imageState";
            this.m_imageState.Size = new System.Drawing.Size(16, 16);
            this.m_imageState.TabIndex = 5;
            this.m_imageState.TabStop = false;
            // 
            // m_panelRestore
            // 
            this.m_panelRestore.Controls.Add(this.m_lnkRestore);
            this.m_panelRestore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelRestore.Location = new System.Drawing.Point(0, 266);
            this.m_panelRestore.Name = "m_panelRestore";
            this.m_panelRestore.Size = new System.Drawing.Size(526, 18);
            this.m_panelRestore.TabIndex = 5;
            // 
            // m_lnkRestore
            // 
            this.m_lnkRestore.Location = new System.Drawing.Point(174, 0);
            this.m_lnkRestore.Name = "m_lnkRestore";
            this.m_lnkRestore.Size = new System.Drawing.Size(349, 13);
            this.m_lnkRestore.TabIndex = 0;
            this.m_lnkRestore.TabStop = true;
            this.m_lnkRestore.Text = "Restore selected fields|20001";
            this.m_lnkRestore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkRestore_LinkClicked);
            // 
            // CControleDetailVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_arbreObjet);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_grilleProprietes);
            this.Controls.Add(this.m_panelRestore);
            this.Controls.Add(this.m_panelBas);
            this.Name = "CControleDetailVersion";
            this.Size = new System.Drawing.Size(526, 338);
            this.BackColorChanged += new System.EventHandler(this.CControleDetailVersion_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.CControleDetailVersion_ForeColorChanged);
            ((System.ComponentModel.ISupportInitialize)(this.m_grilleProprietes)).EndInit();
            this.m_panelBas.ResumeLayout(false);
            this.m_panelBasGauche.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageTypeOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageState)).EndInit();
            this.m_panelRestore.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView m_arbreObjet;
		private System.Windows.Forms.ImageList m_listeImagesObjet;
		private System.Windows.Forms.DataGridView m_grilleProprietes;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ImageList m_imagesHistoriqueActuel;
		private System.Windows.Forms.Label m_lblInfo;
		private System.Windows.Forms.Panel m_panelBas;
		private System.Windows.Forms.Panel m_panelBasGauche;
		private System.Windows.Forms.PictureBox m_imageTypeOperation;
		private System.Windows.Forms.PictureBox m_imageState;
        private System.Windows.Forms.Panel m_panelRestore;
        private System.Windows.Forms.LinkLabel m_lnkRestore;
	}
}
