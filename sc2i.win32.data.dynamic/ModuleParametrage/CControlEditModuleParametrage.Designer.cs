namespace sc2i.win32.data.dynamic
{
    partial class CControlEditModuleParametrage
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
            this.m_txtDescription = new System.Windows.Forms.TextBox();
            this.m_txtLibelle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_txtDescription
            // 
            this.m_txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDescription.Location = new System.Drawing.Point(92, 31);
            this.m_txtDescription.Multiline = true;
            this.m_txtDescription.Name = "m_txtDescription";
            this.m_txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtDescription.Size = new System.Drawing.Size(304, 73);
            this.m_txtDescription.TabIndex = 1;
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtLibelle.Location = new System.Drawing.Point(92, 5);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(304, 20);
            this.m_txtLibelle.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description|250";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Label|50";
            // 
            // CControlEditModuleParametrage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_txtDescription);
            this.Controls.Add(this.m_txtLibelle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CControlEditModuleParametrage";
            this.Size = new System.Drawing.Size(405, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtDescription;
        private System.Windows.Forms.TextBox m_txtLibelle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
