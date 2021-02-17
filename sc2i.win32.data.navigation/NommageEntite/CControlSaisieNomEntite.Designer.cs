namespace sc2i.win32.data.navigation
{
    partial class CControlSaisieNomEntite
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
            this.m_lblLabel = new System.Windows.Forms.Label();
            this.m_txtNomFort = new System.Windows.Forms.TextBox();
            this.m_lnkDelete = new sc2i.win32.common.CWndLinkStd();
            this.SuspendLayout();
            // 
            // m_lblLabel
            // 
            this.m_lblLabel.Location = new System.Drawing.Point(3, 4);
            this.m_lblLabel.Name = "m_lblLabel";
            this.m_lblLabel.Size = new System.Drawing.Size(100, 23);
            this.m_lblLabel.TabIndex = 0;
            this.m_lblLabel.Text = "label1";
            this.m_lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtNomFort
            // 
            this.m_txtNomFort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomFort.Location = new System.Drawing.Point(90, 5);
            this.m_txtNomFort.Name = "m_txtNomFort";
            this.m_txtNomFort.Size = new System.Drawing.Size(242, 20);
            this.m_txtNomFort.TabIndex = 1;
            // 
            // m_lnkDelete
            // 
            this.m_lnkDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDelete.Location = new System.Drawing.Point(338, 4);
            this.m_lnkDelete.Name = "m_lnkDelete";
            this.m_lnkDelete.Size = new System.Drawing.Size(27, 22);
            this.m_lnkDelete.TabIndex = 27;
            this.m_lnkDelete.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkDelete.LinkClicked += new System.EventHandler(this.m_lnkDelete_LinkClicked);
            // 
            // CControlSaisieNomEntite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lnkDelete);
            this.Controls.Add(this.m_txtNomFort);
            this.Controls.Add(this.m_lblLabel);
            this.Name = "CControlSaisieNomEntite";
            this.Size = new System.Drawing.Size(368, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblLabel;
        private System.Windows.Forms.TextBox m_txtNomFort;
        private sc2i.win32.common.CWndLinkStd m_lnkDelete;
    }
}
