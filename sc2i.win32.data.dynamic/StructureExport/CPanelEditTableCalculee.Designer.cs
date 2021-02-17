namespace sc2i.win32.data.dynamic
{
	partial class CPanelEditTableCalculee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditTableCalculee));
            this.m_panelNomTableEtFiltre = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtNomTable = new sc2i.win32.common.C2iTextBox();
            this.m_imagesFiltre = new System.Windows.Forms.ImageList(this.components);
            this.m_panelTableCalculee = new System.Windows.Forms.Panel();
            this.m_splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_panelDetailTableCalculee = new System.Windows.Forms.Panel();
            this.m_splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_txtFormuleNbRecords = new sc2i.win32.expression.CControleEditeFormule();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtFormuleValeur = new sc2i.win32.expression.CControleEditeFormule();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelNomTableEtFiltre.SuspendLayout();
            this.m_panelTableCalculee.SuspendLayout();
            this.m_splitContainer1.Panel1.SuspendLayout();
            this.m_splitContainer1.Panel2.SuspendLayout();
            this.m_splitContainer1.SuspendLayout();
            this.m_panelDetailTableCalculee.SuspendLayout();
            this.m_splitContainer2.Panel1.SuspendLayout();
            this.m_splitContainer2.Panel2.SuspendLayout();
            this.m_splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelNomTableEtFiltre
            // 
            this.m_panelNomTableEtFiltre.Controls.Add(this.label3);
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_txtNomTable);
            this.m_panelNomTableEtFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelNomTableEtFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelNomTableEtFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelNomTableEtFiltre.Name = "m_panelNomTableEtFiltre";
            this.m_panelNomTableEtFiltre.Size = new System.Drawing.Size(719, 25);
            this.m_panelNomTableEtFiltre.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Table|165";
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtNomTable.Location = new System.Drawing.Point(120, 0);
            this.m_txtNomTable.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtNomTable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(596, 22);
            this.m_txtNomTable.TabIndex = 0;
            // 
            // m_imagesFiltre
            // 
            this.m_imagesFiltre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesFiltre.ImageStream")));
            this.m_imagesFiltre.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesFiltre.Images.SetKeyName(0, "");
            this.m_imagesFiltre.Images.SetKeyName(1, "");
            // 
            // m_panelTableCalculee
            // 
            this.m_panelTableCalculee.Controls.Add(this.m_splitContainer1);
            this.m_panelTableCalculee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelTableCalculee.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableCalculee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableCalculee.Name = "m_panelTableCalculee";
            this.m_panelTableCalculee.Size = new System.Drawing.Size(719, 417);
            this.m_panelTableCalculee.TabIndex = 14;
            // 
            // m_splitContainer1
            // 
            this.m_splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer1.Name = "m_splitContainer1";
            // 
            // m_splitContainer1.Panel1
            // 
            this.m_splitContainer1.Panel1.Controls.Add(this.m_panelDetailTableCalculee);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer1.Panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            // 
            // m_splitContainer1.Panel2
            // 
            this.m_splitContainer1.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer1.Panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer1.Size = new System.Drawing.Size(719, 417);
            this.m_splitContainer1.SplitterDistance = 492;
            this.m_splitContainer1.TabIndex = 22;
            // 
            // m_panelDetailTableCalculee
            // 
            this.m_panelDetailTableCalculee.Controls.Add(this.m_splitContainer2);
            this.m_panelDetailTableCalculee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDetailTableCalculee.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDetailTableCalculee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDetailTableCalculee.Name = "m_panelDetailTableCalculee";
            this.m_panelDetailTableCalculee.Size = new System.Drawing.Size(492, 417);
            this.m_panelDetailTableCalculee.TabIndex = 21;
            // 
            // m_splitContainer2
            // 
            this.m_splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer2.Name = "m_splitContainer2";
            this.m_splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainer2.Panel1
            // 
            this.m_splitContainer2.Panel1.Controls.Add(this.m_txtFormuleNbRecords);
            this.m_splitContainer2.Panel1.Controls.Add(this.label5);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer2.Panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            // 
            // m_splitContainer2.Panel2
            // 
            this.m_splitContainer2.Panel2.Controls.Add(this.label6);
            this.m_splitContainer2.Panel2.Controls.Add(this.m_txtFormuleValeur);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer2.Panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer2.Size = new System.Drawing.Size(492, 417);
            this.m_splitContainer2.SplitterDistance = 125;
            this.m_splitContainer2.TabIndex = 5;
            // 
            // m_txtFormuleNbRecords
            // 
            this.m_txtFormuleNbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleNbRecords.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleNbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleNbRecords.Formule = null;
            this.m_txtFormuleNbRecords.Location = new System.Drawing.Point(7, 30);
            this.m_txtFormuleNbRecords.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFormuleNbRecords, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormuleNbRecords.Name = "m_txtFormuleNbRecords";
            this.m_txtFormuleNbRecords.Size = new System.Drawing.Size(471, 92);
            this.m_txtFormuleNbRecords.TabIndex = 4;
            this.m_txtFormuleNbRecords.Enter += new System.EventHandler(this.OnEnterFormule);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Number of records|238";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Value|239";
            // 
            // m_txtFormuleValeur
            // 
            this.m_txtFormuleValeur.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleValeur.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleValeur.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleValeur.Formule = null;
            this.m_txtFormuleValeur.Location = new System.Drawing.Point(7, 23);
            this.m_txtFormuleValeur.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFormuleValeur, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormuleValeur.Name = "m_txtFormuleValeur";
            this.m_txtFormuleValeur.Size = new System.Drawing.Size(471, 240);
            this.m_txtFormuleValeur.TabIndex = 2;
            this.m_txtFormuleValeur.Enter += new System.EventHandler(this.OnEnterFormule);
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(223, 417);
            this.m_wndAideFormule.TabIndex = 20;
			this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CPanelEditTableCalculee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelTableCalculee);
            this.Controls.Add(this.m_panelNomTableEtFiltre);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditTableCalculee";
            this.Size = new System.Drawing.Size(719, 442);
            this.Load += new System.EventHandler(this.CPanelEditTableCalculee_Load);
            this.m_panelNomTableEtFiltre.ResumeLayout(false);
            this.m_panelNomTableEtFiltre.PerformLayout();
            this.m_panelTableCalculee.ResumeLayout(false);
            this.m_splitContainer1.Panel1.ResumeLayout(false);
            this.m_splitContainer1.Panel2.ResumeLayout(false);
            this.m_splitContainer1.ResumeLayout(false);
            this.m_panelDetailTableCalculee.ResumeLayout(false);
            this.m_splitContainer2.Panel1.ResumeLayout(false);
            this.m_splitContainer2.Panel2.ResumeLayout(false);
            this.m_splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel m_panelNomTableEtFiltre;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iTextBox m_txtNomTable;
        private System.Windows.Forms.ImageList m_imagesFiltre;
        private System.Windows.Forms.Panel m_panelTableCalculee;
        private System.Windows.Forms.SplitContainer m_splitContainer1;
        private System.Windows.Forms.Panel m_panelDetailTableCalculee;
        private System.Windows.Forms.SplitContainer m_splitContainer2;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleNbRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleValeur;
        private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
	}
}
