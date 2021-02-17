namespace sc2i.win32.data.dynamic.import.sources
{
    partial class CControleEditeSourceImportFixedValue
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
            this.m_lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lblValue
            // 
            this.m_lblValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_lblValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblValue.Location = new System.Drawing.Point(0, 0);
            this.m_lblValue.Name = "m_lblValue";
            this.m_lblValue.Size = new System.Drawing.Size(267, 26);
            this.m_lblValue.TabIndex = 0;
            // 
            // CControleEditeSourceImportFixedValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lblValue);
            this.Name = "CControleEditeSourceImportFixedValue";
            this.Size = new System.Drawing.Size(267, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblValue;
    }
}
