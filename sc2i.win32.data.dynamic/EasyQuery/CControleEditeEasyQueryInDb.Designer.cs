using futurocom.win32.easyquery;
namespace sc2i.win32.data.dynamic.EasyQuery
{
    partial class CControleEditeEasyQueryInDb
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
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.label1 = new System.Windows.Forms.Label();
            this.c2iTextBox1 = new sc2i.win32.common.C2iTextBox();
            this.c2iPanelOmbre2 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_panelQuery = new futurocom.win32.easyquery.CEditeurEasyQuery();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
            this.c2iPanelOmbre1.SuspendLayout();
            this.c2iPanelOmbre2.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.c2iTextBox1);
            this.c2iPanelOmbre1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.c2iPanelOmbre1, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.c2iPanelOmbre1, false);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.c2iPanelOmbre1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(788, 50);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 0;
            // 
            // label1
            // 
            this.m_extLinkField.SetLinkField(this.label1, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.label1, false);
            this.label1.Location = new System.Drawing.Point(13, 4);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 3;
            this.label1.Text = "Label|50";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c2iTextBox1
            // 
            this.c2iTextBox1.EmptyText = "";
            this.m_extLinkField.SetLinkField(this.c2iTextBox1, "Libelle");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.c2iTextBox1, true);
            this.c2iTextBox1.Location = new System.Drawing.Point(146, 6);
            this.c2iTextBox1.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.c2iTextBox1, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.c2iTextBox1.Name = "c2iTextBox1";
            this.c2iTextBox1.Size = new System.Drawing.Size(386, 20);
            this.m_extStyle.SetStyleBackColor(this.c2iTextBox1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iTextBox1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTextBox1.TabIndex = 4;
            this.c2iTextBox1.Text = "[Libelle]";
            // 
            // c2iPanelOmbre2
            // 
            this.c2iPanelOmbre2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre2.Controls.Add(this.m_panelQuery);
            this.c2iPanelOmbre2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanelOmbre2.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.c2iPanelOmbre2, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.c2iPanelOmbre2, false);
            this.c2iPanelOmbre2.Location = new System.Drawing.Point(0, 50);
            this.c2iPanelOmbre2.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.c2iPanelOmbre2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanelOmbre2.Name = "c2iPanelOmbre2";
            this.c2iPanelOmbre2.Size = new System.Drawing.Size(788, 273);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre2.TabIndex = 1;
            // 
            // m_panelQuery
            // 
            this.m_panelQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_extLinkField.SetLinkField(this.m_panelQuery, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_panelQuery, false);
            this.m_panelQuery.Location = new System.Drawing.Point(0, 0);
            this.m_panelQuery.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelQuery, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelQuery.Name = "m_panelQuery";
            this.m_panelQuery.Size = new System.Drawing.Size(772, 256);
            this.m_extStyle.SetStyleBackColor(this.m_panelQuery, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelQuery, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelQuery.TabIndex = 3;
            // 
            // m_extLinkField
            // 
            this.m_extLinkField.SourceTypeString = "sc2i.data.dynamic.CEasyQueryInDb";
            // 
            // CControleEditeEasyQueryInDb
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.c2iPanelOmbre2);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this, false);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditeEasyQueryInDb";
            this.Size = new System.Drawing.Size(788, 323);
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.c2iPanelOmbre2.ResumeLayout(false);
            this.c2iPanelOmbre2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.common.CExtLinkField m_extLinkField;
        private sc2i.win32.common.C2iTextBox c2iTextBox1;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre2;
        private CEditeurEasyQuery m_panelQuery;
    }
}
