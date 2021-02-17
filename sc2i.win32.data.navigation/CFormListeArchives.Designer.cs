namespace sc2i.win32.data.navigation
{
	partial class CFormListeArchives
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            sc2i.win32.common.GLColumn glColumn1 = new sc2i.win32.common.GLColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormListeArchives));
            sc2i.win32.common.GLColumn glColumn2 = new sc2i.win32.common.GLColumn();
            sc2i.win32.common.GLColumn glColumn3 = new sc2i.win32.common.GLColumn();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_btnFermer = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_lnkDetail = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkOptions = new System.Windows.Forms.LinkLabel();
            this.m_menuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuOnlyForElement = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuSnapshot = new System.Windows.Forms.ToolStripMenuItem();
            this.m_panelListe = new sc2i.win32.data.navigation.CPanelListeSpeedStandard();
            this.c2iPanelFondDegradeStd1 = new sc2i.win32.common.C2iPanelFondDegradeStd();
            this.m_panelBas.SuspendLayout();
            this.m_menuOptions.SuspendLayout();
            this.c2iPanelFondDegradeStd1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelBas
            // 
            this.m_panelBas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelBas.Controls.Add(this.m_btnFermer);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.ForeColor = System.Drawing.Color.Black;
            this.m_panelBas.Location = new System.Drawing.Point(0, 337);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(502, 33);
            this.cExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelBas.TabIndex = 1;
            // 
            // m_btnFermer
            // 
            this.m_btnFermer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnFermer.Location = new System.Drawing.Point(214, 7);
            this.m_btnFermer.Name = "m_btnFermer";
            this.m_btnFermer.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnFermer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnFermer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnFermer.TabIndex = 0;
            this.m_btnFermer.Text = "Close|12";
            this.m_btnFermer.UseVisualStyleBackColor = true;
            this.m_btnFermer.Click += new System.EventHandler(this.m_btnFermer_Click);
            // 
            // m_lnkDetail
            // 
            this.m_lnkDetail.BackColor = System.Drawing.Color.Transparent;
            this.m_lnkDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDetail.ForeColor = System.Drawing.Color.Black;
            this.m_lnkDetail.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDetail.Location = new System.Drawing.Point(8, 5);
            this.m_lnkDetail.Name = "m_lnkDetail";
            this.m_lnkDetail.Size = new System.Drawing.Size(78, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_lnkDetail.TabIndex = 2;
            this.m_lnkDetail.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkDetail.LinkClicked += new System.EventHandler(this.m_lnkDetail_LinkClicked);
            // 
            // m_lnkOptions
            // 
            this.m_lnkOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkOptions.BackColor = System.Drawing.Color.Transparent;
            this.m_lnkOptions.ForeColor = System.Drawing.Color.Black;
            this.m_lnkOptions.Location = new System.Drawing.Point(110, 7);
            this.m_lnkOptions.Name = "m_lnkOptions";
            this.m_lnkOptions.Size = new System.Drawing.Size(63, 13);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkOptions.TabIndex = 3;
            this.m_lnkOptions.TabStop = true;
            this.m_lnkOptions.Text = "Options|116";
            this.m_lnkOptions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOptions_LinkClicked);
            // 
            // m_menuOptions
            // 
            this.m_menuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuOnlyForElement,
            this.m_menuShowAll,
            this.toolStripMenuItem1,
            this.m_menuSnapshot});
            this.m_menuOptions.Name = "m_menuOptions";
            this.m_menuOptions.Size = new System.Drawing.Size(188, 76);
            this.cExtStyle1.SetStyleBackColor(this.m_menuOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_menuOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_menuOptions.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuOptions_Opening);
            // 
            // m_menuOnlyForElement
            // 
            this.m_menuOnlyForElement.Name = "m_menuOnlyForElement";
            this.m_menuOnlyForElement.Size = new System.Drawing.Size(187, 22);
            this.m_menuOnlyForElement.Text = "Only for element|117";
            this.m_menuOnlyForElement.Click += new System.EventHandler(this.onlyForElementToolStripMenuItem_Click);
            // 
            // m_menuShowAll
            // 
            this.m_menuShowAll.Name = "m_menuShowAll";
            this.m_menuShowAll.Size = new System.Drawing.Size(187, 22);
            this.m_menuShowAll.Text = "Show all|118";
            this.m_menuShowAll.Click += new System.EventHandler(this.m_menuShowAll_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
            // 
            // m_menuSnapshot
            // 
            this.m_menuSnapshot.Name = "m_menuSnapshot";
            this.m_menuSnapshot.Size = new System.Drawing.Size(187, 22);
            this.m_menuSnapshot.Text = "With snapshots|119";
            this.m_menuSnapshot.Click += new System.EventHandler(this.m_menuSnapshot_Click);
            // 
            // m_panelListe
            // 
            this.m_panelListe.AllowArbre = true;
            this.m_panelListe.AllowCustomisation = true;
            this.m_panelListe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelListe.BoutonAjouterVisible = false;
            this.m_panelListe.BoutonModifierVisible = false;
            this.m_panelListe.BoutonSupprimerVisible = false;
            glColumn1.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn1.ActiveControlItems")));
            glColumn1.BackColor = System.Drawing.Color.Transparent;
            glColumn1.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn1.ForColor = System.Drawing.Color.Black;
            glColumn1.ImageIndex = -1;
            glColumn1.IsCheckColumn = false;
            glColumn1.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn1.Name = "Name";
            glColumn1.Propriete = "TypeOperation.Libelle";
            glColumn1.Text = "Column";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 50;
            glColumn2.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn2.ActiveControlItems")));
            glColumn2.BackColor = System.Drawing.Color.Transparent;
            glColumn2.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn2.ForColor = System.Drawing.Color.Black;
            glColumn2.ImageIndex = -1;
            glColumn2.IsCheckColumn = false;
            glColumn2.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn2.Name = "Namex";
            glColumn2.Propriete = "VersionDonnees.Date";
            glColumn2.Text = "Date";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 150;
            glColumn3.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn3.ActiveControlItems")));
            glColumn3.BackColor = System.Drawing.Color.Transparent;
            glColumn3.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn3.ForColor = System.Drawing.Color.Black;
            glColumn3.ImageIndex = -1;
            glColumn3.IsCheckColumn = false;
            glColumn3.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn3.Name = "Namexx";
            glColumn3.Propriete = "VersionDonnees.Libelle";
            glColumn3.Text = "Label";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 200;
            this.m_panelListe.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3});
            this.m_panelListe.ContexteUtilisation = "";
            this.m_panelListe.ControlFiltreStandard = null;
            this.m_panelListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListe.ElementSelectionne = null;
            this.m_panelListe.EnableCustomisation = true;
            this.m_panelListe.FiltreDeBase = null;
            this.m_panelListe.FiltreDeBaseEnAjout = false;
            this.m_panelListe.FiltrePrefere = null;
            this.m_panelListe.FiltreRapide = null;
            this.m_panelListe.ForeColor = System.Drawing.Color.Black;
            this.m_panelListe.HasImages = false;
            this.m_panelListe.ListeObjets = null;
            this.m_panelListe.Location = new System.Drawing.Point(0, 0);
            this.m_panelListe.LockEdition = true;
            this.m_panelListe.ModeQuickSearch = false;
            this.m_panelListe.ModeSelection = false;
            this.m_panelListe.MultiSelect = false;
            this.m_panelListe.Name = "m_panelListe";
            this.m_panelListe.Navigateur = null;
            this.m_panelListe.ProprieteObjetAEditer = null;
            this.m_panelListe.QuickSearchText = "";
            this.m_panelListe.Size = new System.Drawing.Size(502, 337);
            this.cExtStyle1.SetStyleBackColor(this.m_panelListe, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelListe, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelListe.TabIndex = 0;
            this.m_panelListe.TrierAuClicSurEnteteColonne = true;
            this.m_panelListe.OnObjetDoubleClick += new System.EventHandler(this.m_panelListe_OnObjetDoubleClick);
            // 
            // c2iPanelFondDegradeStd1
            // 
            this.c2iPanelFondDegradeStd1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("c2iPanelFondDegradeStd1.BackgroundImage")));
            this.c2iPanelFondDegradeStd1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c2iPanelFondDegradeStd1.Controls.Add(this.m_lnkDetail);
            this.c2iPanelFondDegradeStd1.Controls.Add(this.m_lnkOptions);
            this.c2iPanelFondDegradeStd1.Location = new System.Drawing.Point(64, 0);
            this.c2iPanelFondDegradeStd1.Name = "c2iPanelFondDegradeStd1";
            this.c2iPanelFondDegradeStd1.Size = new System.Drawing.Size(197, 25);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelFondDegradeStd1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelFondDegradeStd1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelFondDegradeStd1.TabIndex = 4;
            // 
            // CFormListeArchives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(502, 370);
            this.Controls.Add(this.c2iPanelFondDegradeStd1);
            this.Controls.Add(this.m_panelListe);
            this.Controls.Add(this.m_panelBas);
            this.Name = "CFormListeArchives";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Archive list|105";
            this.Load += new System.EventHandler(this.CFormListeArchives_Load);
            this.m_panelBas.ResumeLayout(false);
            this.m_menuOptions.ResumeLayout(false);
            this.c2iPanelFondDegradeStd1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private sc2i.win32.data.navigation.CPanelListeSpeedStandard m_panelListe;
		private System.Windows.Forms.Panel m_panelBas;
		private System.Windows.Forms.Button m_btnFermer;
		private sc2i.win32.common.CExtStyle cExtStyle1;
		private sc2i.win32.common.CWndLinkStd m_lnkDetail;
		private System.Windows.Forms.LinkLabel m_lnkOptions;
		private System.Windows.Forms.ContextMenuStrip m_menuOptions;
		private System.Windows.Forms.ToolStripMenuItem m_menuOnlyForElement;
		private System.Windows.Forms.ToolStripMenuItem m_menuShowAll;
		private System.Windows.Forms.ToolStripMenuItem m_menuSnapshot;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private sc2i.win32.common.C2iPanelFondDegradeStd c2iPanelFondDegradeStd1;
	}
}