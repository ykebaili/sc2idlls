namespace sc2i.win32.process.multipledoctoged
{
    partial class CControlGedCategoryForDragFile
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
            this.m_lblCategorie = new System.Windows.Forms.Label();
            this.m_btnAddFile = new System.Windows.Forms.Button();
            this.m_panelMarge = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // m_lblCategorie
            // 
            this.m_lblCategorie.AllowDrop = true;
            this.m_lblCategorie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblCategorie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblCategorie.Location = new System.Drawing.Point(43, 0);
            this.m_lblCategorie.Name = "m_lblCategorie";
            this.m_lblCategorie.Size = new System.Drawing.Size(428, 21);
            this.m_lblCategorie.TabIndex = 0;
            this.m_lblCategorie.Text = "label1";
            this.m_lblCategorie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblCategorie.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lblCategorie_DragDrop);
            this.m_lblCategorie.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_lblCategorie_MouseUp);
            this.m_lblCategorie.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lblCategorie_DragEnter);
            // 
            // m_btnAddFile
            // 
            this.m_btnAddFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnAddFile.Image = global::sc2i.win32.process.Properties.Resources.Suivant;
            this.m_btnAddFile.Location = new System.Drawing.Point(22, 0);
            this.m_btnAddFile.Name = "m_btnAddFile";
            this.m_btnAddFile.Size = new System.Drawing.Size(21, 21);
            this.m_btnAddFile.TabIndex = 1;
            this.m_btnAddFile.UseVisualStyleBackColor = true;
            this.m_btnAddFile.Click += new System.EventHandler(this.m_btnAddFile_Click);
            // 
            // m_panelMarge
            // 
            this.m_panelMarge.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelMarge.Location = new System.Drawing.Point(0, 0);
            this.m_panelMarge.Name = "m_panelMarge";
            this.m_panelMarge.Size = new System.Drawing.Size(22, 21);
            this.m_panelMarge.TabIndex = 2;
            // 
            // CControlGedCategoryForDrawFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.m_lblCategorie);
            this.Controls.Add(this.m_btnAddFile);
            this.Controls.Add(this.m_panelMarge);
            this.Name = "CControlGedCategoryForDrawFile";
            this.Size = new System.Drawing.Size(471, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblCategorie;
        private System.Windows.Forms.Button m_btnAddFile;
        private System.Windows.Forms.Panel m_panelMarge;
    }
}
