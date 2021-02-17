using sc2i.win32.common;
namespace sc2i.win32.data.dynamic
{
	partial class CPanelEditTableExportTableauCroise
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
            this.m_panelNomTableEtFiltre = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtNomTable = new sc2i.win32.common.C2iTextBox();
            this.m_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_chkSupprimerTablesTravail = new System.Windows.Forms.CheckBox();
            this.m_panelTableauCroise = new sc2i.win32.common.CPanelEditTableauCroise();
            this.m_panelNomTableEtFiltre.SuspendLayout();
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
            this.m_panelNomTableEtFiltre.Size = new System.Drawing.Size(633, 44);
            this.m_panelNomTableEtFiltre.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Source|20005";
            // 
            // m_lblType
            // 
            this.m_lblType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblType.BackColor = System.Drawing.Color.White;
            this.m_lblType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblType.Location = new System.Drawing.Point(120, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblType, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblType.Name = "m_lblType";
            this.m_lblType.Size = new System.Drawing.Size(510, 19);
            this.m_lblType.TabIndex = 13;
            this.m_lblType.Text = "Source infos|258";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
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
            this.m_txtNomTable.Size = new System.Drawing.Size(510, 22);
            this.m_txtNomTable.TabIndex = 0;
            // 
            // m_chkSupprimerTablesTravail
            // 
            this.m_chkSupprimerTablesTravail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkSupprimerTablesTravail.Location = new System.Drawing.Point(455, 339);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkSupprimerTablesTravail, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkSupprimerTablesTravail.Name = "m_chkSupprimerTablesTravail";
            this.m_chkSupprimerTablesTravail.Size = new System.Drawing.Size(175, 16);
            this.m_chkSupprimerTablesTravail.TabIndex = 14;
            this.m_chkSupprimerTablesTravail.Text = "Remove working tables|20004";
            this.m_chkSupprimerTablesTravail.UseVisualStyleBackColor = true;
            // 
            // m_panelTableauCroise
            // 
            this.m_panelTableauCroise.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelTableauCroise.AutoScroll = true;
            this.m_panelTableauCroise.Location = new System.Drawing.Point(0, 44);
            this.m_panelTableauCroise.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableauCroise, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelTableauCroise.Name = "m_panelTableauCroise";
            this.m_panelTableauCroise.Size = new System.Drawing.Size(630, 291);
            this.m_panelTableauCroise.TabIndex = 15;
            // 
            // CPanelEditTableExportTableauCroise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelTableauCroise);
            this.Controls.Add(this.m_chkSupprimerTablesTravail);
            this.Controls.Add(this.m_panelNomTableEtFiltre);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditTableExportTableauCroise";
            this.Size = new System.Drawing.Size(633, 358);
            this.Load += new System.EventHandler(this.CPanelEditTableExportTableauCroise_Load);
            this.m_panelNomTableEtFiltre.ResumeLayout(false);
            this.m_panelNomTableEtFiltre.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_panelNomTableEtFiltre;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iTextBox m_txtNomTable;
		private System.Windows.Forms.ToolTip m_tooltip;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_lblType;
		private System.Windows.Forms.CheckBox m_chkSupprimerTablesTravail;
		private CPanelEditTableauCroise m_panelTableauCroise;
	}
}
