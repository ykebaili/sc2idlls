namespace futurocom.win32.easyquery
{
    partial class CControleEditeSortColumn
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
            this.m_cmbField = new sc2i.win32.common.C2iComboBox();
            this.m_cmbCroissant = new sc2i.win32.common.C2iComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_cmbField
            // 
            this.m_cmbField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbField.FormattingEnabled = true;
            this.m_cmbField.IsLink = false;
            this.m_cmbField.Location = new System.Drawing.Point(35, 0);
            this.m_cmbField.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbField, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbField.Name = "m_cmbField";
            this.m_cmbField.Size = new System.Drawing.Size(285, 21);
            this.m_cmbField.TabIndex = 0;
            // 
            // m_cmbCroissant
            // 
            this.m_cmbCroissant.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_cmbCroissant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbCroissant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbCroissant.FormattingEnabled = true;
            this.m_cmbCroissant.IsLink = false;
            this.m_cmbCroissant.Location = new System.Drawing.Point(320, 0);
            this.m_cmbCroissant.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbCroissant, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbCroissant.Name = "m_cmbCroissant";
            this.m_cmbCroissant.Size = new System.Drawing.Size(123, 21);
            this.m_cmbCroissant.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 21);
            this.label1.TabIndex = 2;
            // 
            // CControleEditeSortColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ColorInactive = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.m_cmbField);
            this.Controls.Add(this.m_cmbCroissant);
            this.Controls.Add(this.label1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditeSortColumn";
            this.Size = new System.Drawing.Size(443, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.C2iComboBox m_cmbField;
        private sc2i.win32.common.C2iComboBox m_cmbCroissant;
        private System.Windows.Forms.Label label1;
    }
}
