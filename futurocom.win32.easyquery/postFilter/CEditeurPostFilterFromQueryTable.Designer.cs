namespace futurocom.win32.easyquery.postFilter
{
    partial class CEditeurPostFilterFromQueryTable
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
            this.m_panelSource = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbTableSource = new sc2i.win32.common.C2iComboBox();
            this.m_editeurParametre = new futurocom.win32.easyquery.CEditeurParametreJointure();
            this.m_panelSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelSource
            // 
            this.m_panelSource.Controls.Add(this.m_cmbTableSource);
            this.m_panelSource.Controls.Add(this.label1);
            this.m_panelSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelSource.Location = new System.Drawing.Point(0, 0);
            this.m_panelSource.Name = "m_panelSource";
            this.m_panelSource.Size = new System.Drawing.Size(445, 35);
            this.m_panelSource.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source table|20073";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cmbTableSource
            // 
            this.m_cmbTableSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTableSource.DisplayMember = "NomFinal";
            this.m_cmbTableSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTableSource.FormattingEnabled = true;
            this.m_cmbTableSource.IsLink = false;
            this.m_cmbTableSource.Location = new System.Drawing.Point(135, 6);
            this.m_cmbTableSource.LockEdition = false;
            this.m_cmbTableSource.Name = "m_cmbTableSource";
            this.m_cmbTableSource.Size = new System.Drawing.Size(307, 21);
            this.m_cmbTableSource.TabIndex = 1;
            this.m_cmbTableSource.SelectionChangeCommitted += new System.EventHandler(this.m_cmbTableSource_SelectionChangeCommitted);
            // 
            // m_editeurParametre
            // 
            this.m_editeurParametre.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_editeurParametre.Location = new System.Drawing.Point(0, 35);
            this.m_editeurParametre.Name = "m_editeurParametre";
            this.m_editeurParametre.Size = new System.Drawing.Size(445, 24);
            this.m_editeurParametre.TabIndex = 1;
            // 
            // CEditeurPostFilterFromQueryTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_editeurParametre);
            this.Controls.Add(this.m_panelSource);
            this.Name = "CEditeurPostFilterFromQueryTable";
            this.Size = new System.Drawing.Size(445, 62);
            this.m_panelSource.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelSource;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iComboBox m_cmbTableSource;
        private CEditeurParametreJointure m_editeurParametre;
    }
}
