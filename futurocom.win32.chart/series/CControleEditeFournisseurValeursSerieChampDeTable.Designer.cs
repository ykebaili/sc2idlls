namespace futurocom.win32.chart.series
{
    partial class CControleEditeFournisseurValeursSerieChampDeTable
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_comboTable = new System.Windows.Forms.ComboBox();
            this.m_comboChamp = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_rbtnField = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_rbtnFormule = new System.Windows.Forms.RadioButton();
            this.m_panelColonne = new System.Windows.Forms.Panel();
            this.m_panelFormule = new System.Windows.Forms.Panel();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelColonne.SuspendLayout();
            this.m_panelFormule.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table|20018";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Column|20019";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_comboTable
            // 
            this.m_comboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboTable.FormattingEnabled = true;
            this.m_comboTable.Location = new System.Drawing.Point(109, 10);
            this.m_comboTable.Name = "m_comboTable";
            this.m_comboTable.Size = new System.Drawing.Size(372, 21);
            this.m_comboTable.TabIndex = 1;
            this.m_comboTable.SelectionChangeCommitted += new System.EventHandler(this.m_comboTable_SelectionChangeCommitted);
            // 
            // m_comboChamp
            // 
            this.m_comboChamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboChamp.FormattingEnabled = true;
            this.m_comboChamp.Location = new System.Drawing.Point(110, 3);
            this.m_comboChamp.Name = "m_comboChamp";
            this.m_comboChamp.Size = new System.Drawing.Size(372, 21);
            this.m_comboChamp.TabIndex = 1;
            this.m_comboChamp.SelectedIndexChanged += new System.EventHandler(this.m_comboChamp_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Formula|20033";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_comboTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 51);
            this.panel1.TabIndex = 3;
            // 
            // m_rbtnField
            // 
            this.m_rbtnField.AutoSize = true;
            this.m_rbtnField.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnField.Location = new System.Drawing.Point(0, 0);
            this.m_rbtnField.Name = "m_rbtnField";
            this.m_rbtnField.Size = new System.Drawing.Size(92, 17);
            this.m_rbtnField.TabIndex = 2;
            this.m_rbtnField.TabStop = true;
            this.m_rbtnField.Text = "Column|20019";
            this.m_rbtnField.UseVisualStyleBackColor = true;
            this.m_rbtnField.CheckedChanged += new System.EventHandler(this.m_rbtnField_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_rbtnFormule);
            this.panel2.Controls.Add(this.m_rbtnField);
            this.panel2.Location = new System.Drawing.Point(109, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(372, 17);
            this.panel2.TabIndex = 3;
            // 
            // m_rbtnFormule
            // 
            this.m_rbtnFormule.AutoSize = true;
            this.m_rbtnFormule.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnFormule.Location = new System.Drawing.Point(92, 0);
            this.m_rbtnFormule.Name = "m_rbtnFormule";
            this.m_rbtnFormule.Size = new System.Drawing.Size(94, 17);
            this.m_rbtnFormule.TabIndex = 3;
            this.m_rbtnFormule.TabStop = true;
            this.m_rbtnFormule.Text = "Formula|20033";
            this.m_rbtnFormule.UseVisualStyleBackColor = true;
            this.m_rbtnFormule.CheckedChanged += new System.EventHandler(this.m_rbtnFormule_CheckedChanged);
            // 
            // m_panelColonne
            // 
            this.m_panelColonne.Controls.Add(this.label2);
            this.m_panelColonne.Controls.Add(this.m_comboChamp);
            this.m_panelColonne.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelColonne.Location = new System.Drawing.Point(0, 51);
            this.m_panelColonne.Name = "m_panelColonne";
            this.m_panelColonne.Size = new System.Drawing.Size(498, 33);
            this.m_panelColonne.TabIndex = 5;
            // 
            // m_panelFormule
            // 
            this.m_panelFormule.Controls.Add(this.m_txtFormule);
            this.m_panelFormule.Controls.Add(this.label3);
            this.m_panelFormule.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFormule.Location = new System.Drawing.Point(0, 84);
            this.m_panelFormule.Name = "m_panelFormule";
            this.m_panelFormule.Size = new System.Drawing.Size(498, 70);
            this.m_panelFormule.TabIndex = 6;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(110, 3);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(372, 50);
            this.m_txtFormule.TabIndex = 4;
            // 
            // CControleEditeFournisseurValeursSerieChampDeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelFormule);
            this.Controls.Add(this.m_panelColonne);
            this.Controls.Add(this.panel1);
            this.Name = "CControleEditeFournisseurValeursSerieChampDeTable";
            this.Size = new System.Drawing.Size(498, 154);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_panelColonne.ResumeLayout(false);
            this.m_panelFormule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_comboTable;
        private System.Windows.Forms.ComboBox m_comboChamp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton m_rbtnField;
        private System.Windows.Forms.RadioButton m_rbtnFormule;
        private System.Windows.Forms.Panel m_panelColonne;
        private System.Windows.Forms.Panel m_panelFormule;
    }
}
