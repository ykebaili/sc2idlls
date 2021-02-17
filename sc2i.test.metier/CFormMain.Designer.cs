namespace sc2i.test.metier
{
	partial class CFormMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtSeparateur = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.c2iTextBoxNumerique1 = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtSelectTypeEquipement = new sc2i.win32.common.MemoryDb.C2iTextBoxSelectionEntiteMemoryDb();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_txtSelectTypeEquipement);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_txtSeparateur);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.c2iTextBoxNumerique1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 127);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Séparateur milliers";
            // 
            // m_txtSeparateur
            // 
            this.m_txtSeparateur.Location = new System.Drawing.Point(192, 77);
            this.m_txtSeparateur.Name = "m_txtSeparateur";
            this.m_txtSeparateur.Size = new System.Drawing.Size(100, 20);
            this.m_txtSeparateur.TabIndex = 3;
            this.m_txtSeparateur.TextChanged += new System.EventHandler(this.m_txtSeparateur_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(360, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "LockEdition";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // c2iTextBoxNumerique1
            // 
            this.c2iTextBoxNumerique1.Arrondi = 6;
            this.c2iTextBoxNumerique1.DecimalAutorise = true;
            this.c2iTextBoxNumerique1.EmptyText = "";
            this.c2iTextBoxNumerique1.IntValue = 0;
            this.c2iTextBoxNumerique1.Location = new System.Drawing.Point(315, 54);
            this.c2iTextBoxNumerique1.LockEdition = false;
            this.c2iTextBoxNumerique1.Name = "c2iTextBoxNumerique1";
            this.c2iTextBoxNumerique1.NullAutorise = false;
            this.c2iTextBoxNumerique1.SelectAllOnEnter = true;
            this.c2iTextBoxNumerique1.SeparateurMilliers = " ";
            this.c2iTextBoxNumerique1.Size = new System.Drawing.Size(260, 20);
            this.c2iTextBoxNumerique1.TabIndex = 1;
            this.c2iTextBoxNumerique1.Text = "0";
            // 
            // m_txtSelectTypeEquipement
            // 
            this.m_txtSelectTypeEquipement.FonctionTextNull = null;
            this.m_txtSelectTypeEquipement.ImageDisplayMode = sc2i.win32.common.MemoryDb.EModeAffichageImageTextBoxRapide.Always;
            this.m_txtSelectTypeEquipement.Location = new System.Drawing.Point(135, 12);
            this.m_txtSelectTypeEquipement.LockEdition = false;
            this.m_txtSelectTypeEquipement.Name = "m_txtSelectTypeEquipement";
            this.m_txtSelectTypeEquipement.SelectedObject = null;
            this.m_txtSelectTypeEquipement.Size = new System.Drawing.Size(254, 21);
            this.m_txtSelectTypeEquipement.SpecificImage = null;
            this.m_txtSelectTypeEquipement.TabIndex = 5;
            this.m_txtSelectTypeEquipement.TextNull = "";
            this.m_txtSelectTypeEquipement.UseIntellisense = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Type d\'équipement";
            // 
            // CFormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(623, 185);
            this.Controls.Add(this.panel1);
            this.Name = "CFormMain";
            this.Text = "Tests sc2i Dlls";
            this.Load += new System.EventHandler(this.CFormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.C2iTextBoxNumerique c2iTextBoxNumerique1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtSeparateur;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.common.MemoryDb.C2iTextBoxSelectionEntiteMemoryDb m_txtSelectTypeEquipement;
	}
}