namespace data.hotel.easyquery.win32.entitysource
{
    partial class CPanelEditSourceEntites
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cmbTypeSource = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_panelEditeSource = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Entities to read|20002";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmbTypeSource);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 23);
            this.panel1.TabIndex = 3;
            // 
            // m_cmbTypeSource
            // 
            this.m_cmbTypeSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbTypeSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeSource.FormattingEnabled = true;
            this.m_cmbTypeSource.IsLink = false;
            this.m_cmbTypeSource.ListDonnees = null;
            this.m_cmbTypeSource.Location = new System.Drawing.Point(150, 0);
            this.m_cmbTypeSource.LockEdition = false;
            this.m_cmbTypeSource.Name = "m_cmbTypeSource";
            this.m_cmbTypeSource.NullAutorise = true;
            this.m_cmbTypeSource.ProprieteAffichee = null;
            this.m_cmbTypeSource.Size = new System.Drawing.Size(227, 21);
            this.m_cmbTypeSource.TabIndex = 4;
            this.m_cmbTypeSource.TextNull = "(empty)";
            this.m_cmbTypeSource.Tri = true;
            this.m_cmbTypeSource.SelectionChangeCommitted += new System.EventHandler(this.m_cmbTypeSource_SelectionChangeCommitted);
            // 
            // m_panelEditeSource
            // 
            this.m_panelEditeSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEditeSource.Location = new System.Drawing.Point(0, 23);
            this.m_panelEditeSource.Name = "m_panelEditeSource";
            this.m_panelEditeSource.Size = new System.Drawing.Size(377, 59);
            this.m_panelEditeSource.TabIndex = 4;
            // 
            // CPanelEditSourceEntites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelEditeSource);
            this.Controls.Add(this.panel1);
            this.Name = "CPanelEditSourceEntites";
            this.Size = new System.Drawing.Size(377, 82);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbTypeSource;
        private System.Windows.Forms.Panel m_panelEditeSource;

    }
}
