namespace sc2i.win32.data.dynamic
{
	partial class CPanelEditTableUnion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditTableUnion));
            this.m_panelNomTableEtFiltre = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtNomTable = new sc2i.win32.common.C2iTextBox();
            this.m_imagesFiltre = new System.Windows.Forms.ImageList(this.components);
            this.m_panelTableCalculee = new System.Windows.Forms.Panel();
            this.m_wndListeChamps = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_imagesChamps = new System.Windows.Forms.ImageList(this.components);
            this.m_chkSupprimerTablesTravail = new System.Windows.Forms.CheckBox();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelNomTableEtFiltre.SuspendLayout();
            this.m_panelTableCalculee.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelNomTableEtFiltre
            // 
            this.m_panelNomTableEtFiltre.Controls.Add(this.label1);
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_lblType);
            this.m_panelNomTableEtFiltre.Controls.Add(this.label3);
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_txtNomTable);
            this.m_panelNomTableEtFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelNomTableEtFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelNomTableEtFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelNomTableEtFiltre.Name = "m_panelNomTableEtFiltre";
            this.m_panelNomTableEtFiltre.Size = new System.Drawing.Size(393, 42);
            this.m_panelNomTableEtFiltre.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source|20005";
            // 
            // m_lblType
            // 
            this.m_lblType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblType.BackColor = System.Drawing.Color.White;
            this.m_lblType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblType.Location = new System.Drawing.Point(120, 23);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblType, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblType.Name = "m_lblType";
            this.m_lblType.Size = new System.Drawing.Size(270, 19);
            this.m_lblType.TabIndex = 2;
            this.m_lblType.Text = "Source infos|258";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
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
            this.m_txtNomTable.Size = new System.Drawing.Size(270, 22);
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
            this.m_panelTableCalculee.Controls.Add(this.m_wndListeChamps);
            this.m_panelTableCalculee.Controls.Add(this.m_chkSupprimerTablesTravail);
            this.m_panelTableCalculee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelTableCalculee.Location = new System.Drawing.Point(0, 42);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableCalculee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableCalculee.Name = "m_panelTableCalculee";
            this.m_panelTableCalculee.Size = new System.Drawing.Size(393, 264);
            this.m_panelTableCalculee.TabIndex = 14;
            // 
            // m_wndListeChamps
            // 
            this.m_wndListeChamps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeChamps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeChamps.GridLines = true;
            this.m_wndListeChamps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeChamps.Location = new System.Drawing.Point(3, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeChamps, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeChamps.Name = "m_wndListeChamps";
            this.m_wndListeChamps.Size = new System.Drawing.Size(387, 242);
            this.m_wndListeChamps.SmallImageList = this.m_imagesChamps;
            this.m_wndListeChamps.TabIndex = 2;
            this.m_wndListeChamps.UseCompatibleStateImageBehavior = false;
            this.m_wndListeChamps.View = System.Windows.Forms.View.Details;
            this.m_wndListeChamps.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndListeChamps_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 339;
            // 
            // m_imagesChamps
            // 
            this.m_imagesChamps.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesChamps.ImageStream")));
            this.m_imagesChamps.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesChamps.Images.SetKeyName(0, "");
            this.m_imagesChamps.Images.SetKeyName(1, "cle.gif");
            // 
            // m_chkSupprimerTablesTravail
            // 
            this.m_chkSupprimerTablesTravail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_chkSupprimerTablesTravail.Location = new System.Drawing.Point(0, 248);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkSupprimerTablesTravail, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkSupprimerTablesTravail.Name = "m_chkSupprimerTablesTravail";
            this.m_chkSupprimerTablesTravail.Size = new System.Drawing.Size(393, 16);
            this.m_chkSupprimerTablesTravail.TabIndex = 1;
            this.m_chkSupprimerTablesTravail.Text = "Remove working tables|20004";
            this.m_chkSupprimerTablesTravail.UseVisualStyleBackColor = true;
            // 
            // CPanelEditTableUnion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelTableCalculee);
            this.Controls.Add(this.m_panelNomTableEtFiltre);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditTableUnion";
            this.Size = new System.Drawing.Size(393, 306);
            this.Load += new System.EventHandler(this.CPanelEditTableUnion_Load);
            this.m_panelNomTableEtFiltre.ResumeLayout(false);
            this.m_panelNomTableEtFiltre.PerformLayout();
            this.m_panelTableCalculee.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel m_panelNomTableEtFiltre;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iTextBox m_txtNomTable;
        private System.Windows.Forms.ImageList m_imagesFiltre;
		private System.Windows.Forms.Panel m_panelTableCalculee;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.Label m_lblType;
		private System.Windows.Forms.CheckBox m_chkSupprimerTablesTravail;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList m_imagesChamps;
		private System.Windows.Forms.ListView m_wndListeChamps;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}
