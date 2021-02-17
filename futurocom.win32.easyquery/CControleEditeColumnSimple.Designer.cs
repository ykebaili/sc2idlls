namespace futurocom.win32.easyquery
{
    partial class CControleEditeColumnSimple
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
            this.m_cmbDataType = new sc2i.win32.common.C2iComboBox();
            this.m_txtColName = new sc2i.win32.common.C2iTextBox();
            this.SuspendLayout();
            // 
            // m_cmbDataType
            // 
            this.m_cmbDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbDataType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbDataType.FormattingEnabled = true;
            this.m_cmbDataType.IsLink = false;
            this.m_cmbDataType.Location = new System.Drawing.Point(292, 0);
            this.m_cmbDataType.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbDataType, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbDataType.Name = "m_cmbDataType";
            this.m_cmbDataType.Size = new System.Drawing.Size(151, 21);
            this.m_cmbDataType.TabIndex = 0;
            // 
            // m_txtColName
            // 
            this.m_txtColName.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtColName.EmptyText = "";
            this.m_txtColName.Location = new System.Drawing.Point(0, 0);
            this.m_txtColName.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtColName, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtColName.Name = "m_txtColName";
            this.m_txtColName.Size = new System.Drawing.Size(292, 20);
            this.m_txtColName.TabIndex = 3;
            // 
            // CControleEditeColumnSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ColorInactive = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.m_cmbDataType);
            this.Controls.Add(this.m_txtColName);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditeColumnSimple";
            this.Size = new System.Drawing.Size(443, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private sc2i.win32.common.C2iComboBox m_cmbDataType;
        private sc2i.win32.common.C2iTextBox m_txtColName;
    }
}
