namespace timos
{
	partial class CFormFormuleRapide
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblEntite = new System.Windows.Forms.Label();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_btnNext = new System.Windows.Forms.Button();
            this.m_btnPrevious = new System.Windows.Forms.Button();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label8 = new System.Windows.Forms.Label();
            this.m_btnEvaluer = new System.Windows.Forms.Button();
            this.m_txtResultat = new sc2i.win32.common.C2iTextBox();
            this.m_btnClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_btnFermer = new System.Windows.Forms.Button();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.c2iPanelOmbre1.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 23);
            this.m_exStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Element|106";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblEntite
            // 
            this.m_lblEntite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblEntite.BackColor = System.Drawing.Color.White;
            this.m_lblEntite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblEntite.Location = new System.Drawing.Point(76, 4);
            this.m_extModeEdition.SetModeEdition(this.m_lblEntite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblEntite.Name = "m_lblEntite";
            this.m_lblEntite.Size = new System.Drawing.Size(337, 23);
            this.m_exStyle.SetStyleBackColor(this.m_lblEntite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lblEntite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblEntite.TabIndex = 1;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_splitContainer);
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(2, 12);
            this.c2iPanelOmbre1.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.c2iPanelOmbre1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(677, 409);
            this.m_exStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 2;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Location = new System.Drawing.Point(3, 3);
            this.m_extModeEdition.SetModeEdition(this.m_splitContainer, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.splitContainer1);
            this.m_splitContainer.Panel1.Controls.Add(this.panel1);
            this.m_extModeEdition.SetModeEdition(this.m_splitContainer.Panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_exStyle.SetStyleBackColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_extModeEdition.SetModeEdition(this.m_splitContainer.Panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_exStyle.SetStyleBackColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.Size = new System.Drawing.Size(653, 386);
            this.m_splitContainer.SplitterDistance = 416;
            this.m_exStyle.SetStyleBackColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.m_extModeEdition.SetModeEdition(this.splitContainer1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_btnNext);
            this.splitContainer1.Panel1.Controls.Add(this.m_btnPrevious);
            this.splitContainer1.Panel1.Controls.Add(this.m_txtFormule);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.m_extModeEdition.SetModeEdition(this.splitContainer1.Panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_exStyle.SetStyleBackColor(this.splitContainer1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitContainer1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_btnEvaluer);
            this.splitContainer1.Panel2.Controls.Add(this.m_txtResultat);
            this.splitContainer1.Panel2.Controls.Add(this.m_btnClear);
            this.m_extModeEdition.SetModeEdition(this.splitContainer1.Panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_exStyle.SetStyleBackColor(this.splitContainer1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitContainer1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitContainer1.Size = new System.Drawing.Size(416, 351);
            this.splitContainer1.SplitterDistance = 115;
            this.m_exStyle.SetStyleBackColor(this.splitContainer1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitContainer1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitContainer1.TabIndex = 10;
            // 
            // m_btnNext
            // 
            this.m_btnNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnNext.BackColor = System.Drawing.Color.Green;
            this.m_btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnNext.ForeColor = System.Drawing.Color.White;
            this.m_btnNext.Location = new System.Drawing.Point(200, 3);
            this.m_btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.m_extModeEdition.SetModeEdition(this.m_btnNext, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnNext.Name = "m_btnNext";
            this.m_btnNext.Size = new System.Drawing.Size(22, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnNext, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnNext, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnNext.TabIndex = 9;
            this.m_btnNext.Text = ">";
            this.m_btnNext.UseVisualStyleBackColor = false;
            this.m_btnNext.Click += new System.EventHandler(this.m_btnNext_Click);
            // 
            // m_btnPrevious
            // 
            this.m_btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnPrevious.BackColor = System.Drawing.Color.Green;
            this.m_btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnPrevious.ForeColor = System.Drawing.Color.White;
            this.m_btnPrevious.Location = new System.Drawing.Point(172, 3);
            this.m_btnPrevious.Margin = new System.Windows.Forms.Padding(0);
            this.m_extModeEdition.SetModeEdition(this.m_btnPrevious, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnPrevious.Name = "m_btnPrevious";
            this.m_btnPrevious.Size = new System.Drawing.Size(22, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnPrevious, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnPrevious, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPrevious.TabIndex = 9;
            this.m_btnPrevious.Text = "<";
            this.m_btnPrevious.UseVisualStyleBackColor = false;
            this.m_btnPrevious.Click += new System.EventHandler(this.m_btnPrevious_Click);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.LightGreen;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(3, 29);
            this.m_txtFormule.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtFormule, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(406, 80);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormule.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 8);
            this.m_extModeEdition.SetModeEdition(this.label8, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 16);
            this.m_exStyle.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 6;
            this.label8.Text = "Formula|102";
            // 
            // m_btnEvaluer
            // 
            this.m_btnEvaluer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEvaluer.BackColor = System.Drawing.Color.Green;
            this.m_btnEvaluer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnEvaluer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnEvaluer.ForeColor = System.Drawing.Color.White;
            this.m_btnEvaluer.Location = new System.Drawing.Point(3, 3);
            this.m_extModeEdition.SetModeEdition(this.m_btnEvaluer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnEvaluer.Name = "m_btnEvaluer";
            this.m_btnEvaluer.Size = new System.Drawing.Size(406, 22);
            this.m_exStyle.SetStyleBackColor(this.m_btnEvaluer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnEvaluer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnEvaluer.TabIndex = 3;
            this.m_btnEvaluer.Text = "Evaluate|105";
            this.m_btnEvaluer.UseVisualStyleBackColor = false;
            this.m_btnEvaluer.Click += new System.EventHandler(this.m_btnEvaluer_Click);
            this.m_btnEvaluer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_btnEvaluer_KeyDown);
            // 
            // m_txtResultat
            // 
            this.m_txtResultat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtResultat.BackColor = System.Drawing.Color.White;
            this.m_txtResultat.Location = new System.Drawing.Point(3, 31);
            this.m_txtResultat.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtResultat, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtResultat.Multiline = true;
            this.m_txtResultat.Name = "m_txtResultat";
            this.m_txtResultat.ReadOnly = true;
            this.m_txtResultat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtResultat.Size = new System.Drawing.Size(406, 165);
            this.m_exStyle.SetStyleBackColor(this.m_txtResultat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtResultat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtResultat.TabIndex = 7;
            // 
            // m_btnClear
            // 
            this.m_btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnClear.Location = new System.Drawing.Point(3, 199);
            this.m_extModeEdition.SetModeEdition(this.m_btnClear, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnClear.Name = "m_btnClear";
            this.m_btnClear.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnClear, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnClear, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnClear.TabIndex = 8;
            this.m_btnClear.Text = "Clear|108";
            this.m_btnClear.UseVisualStyleBackColor = true;
            this.m_btnClear.Click += new System.EventHandler(this.m_btnClear_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lblEntite);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 35);
            this.m_exStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 11;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_wndAideFormule, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(229, 382);
            this.m_exStyle.SetStyleBackColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAideFormule.TabIndex = 6;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_btnFermer
            // 
            this.m_btnFermer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnFermer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnFermer.Location = new System.Drawing.Point(262, 419);
            this.m_extModeEdition.SetModeEdition(this.m_btnFermer, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFermer.Name = "m_btnFermer";
            this.m_btnFermer.Size = new System.Drawing.Size(151, 29);
            this.m_exStyle.SetStyleBackColor(this.m_btnFermer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnFermer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnFermer.TabIndex = 4;
            this.m_btnFermer.Text = "Close|12";
            this.m_btnFermer.UseVisualStyleBackColor = true;
            // 
            // CFormFormuleRapide
            // 
            this.AcceptButton = this.m_btnEvaluer;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.m_btnFermer;
            this.ClientSize = new System.Drawing.Size(681, 460);
            this.Controls.Add(this.m_btnFermer);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CFormFormuleRapide";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Quick formula evaluation|107";
            this.Load += new System.EventHandler(this.CFormFormuleRapide_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_lblEntite;
		private sc2i.win32.common.CExtStyle m_exStyle;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Button m_btnFermer;
		private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.Button m_btnEvaluer;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
        private System.Windows.Forms.SplitContainer m_splitContainer;
        private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private System.Windows.Forms.Label label8;
        private sc2i.win32.common.C2iTextBox m_txtResultat;
        private System.Windows.Forms.Button m_btnClear;
        private System.Windows.Forms.Button m_btnNext;
        private System.Windows.Forms.Button m_btnPrevious;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
	}
}
