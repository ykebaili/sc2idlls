namespace futurocom.win32.easyquery.postFilter
{
    partial class CPanelPostFilter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbTypePostFilter = new sc2i.win32.common.C2iComboBox();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelFiltre = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmbTypePostFilter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 34);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Post filter type|20071";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cmbTypePostFilter
            // 
            this.m_cmbTypePostFilter.DisplayMember = "Libelle";
            this.m_cmbTypePostFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypePostFilter.FormattingEnabled = true;
            this.m_cmbTypePostFilter.IsLink = false;
            this.m_cmbTypePostFilter.Location = new System.Drawing.Point(130, 6);
            this.m_cmbTypePostFilter.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbTypePostFilter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypePostFilter.Name = "m_cmbTypePostFilter";
            this.m_cmbTypePostFilter.Size = new System.Drawing.Size(235, 21);
            this.m_cmbTypePostFilter.TabIndex = 1;
            this.m_cmbTypePostFilter.SelectionChangeCommitted += new System.EventHandler(this.m_cmbTypePostFilter_SelectionChangeCommitted);
            // 
            // m_extModeEdition
            // 
            this.m_extModeEdition.ModeEdition = true;
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.AutoScroll = true;
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 34);
            this.m_extModeEdition.SetModeEdition(this.m_panelFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(466, 150);
            this.m_panelFiltre.TabIndex = 1;
            // 
            // CPanelPostFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelFiltre);
            this.Controls.Add(this.panel1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelPostFilter";
            this.Size = new System.Drawing.Size(466, 184);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iComboBox m_cmbTypePostFilter;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.Panel m_panelFiltre;
    }
}
