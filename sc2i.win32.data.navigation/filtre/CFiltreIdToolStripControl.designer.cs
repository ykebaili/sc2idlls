using sc2i.win32.common;
namespace sc2i.win32.data.navigation.filtre
{
    partial class CFiltreIdToolStripControl
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
            this.m_txtId = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_btnValide = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnValide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_txtId
            // 
            this.m_txtId.Arrondi = 0;
            this.m_txtId.DecimalAutorise = false;
            this.m_txtId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtId.DoubleValue = null;
            this.m_txtId.EmptyText = "";
            this.m_txtId.IntValue = null;
            this.m_txtId.Location = new System.Drawing.Point(21, 0);
            this.m_txtId.LockEdition = false;
            this.m_txtId.Name = "m_txtId";
            this.m_txtId.NullAutorise = true;
            this.m_txtId.SelectAllOnEnter = true;
            this.m_txtId.SeparateurMilliers = "";
            this.m_txtId.Size = new System.Drawing.Size(116, 20);
            this.m_txtId.TabIndex = 0;
            this.m_txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtId_KeyDown);
            // 
            // m_btnValide
            // 
            this.m_btnValide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnValide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnValide.Image = global::sc2i.win32.data.navigation.Properties.Resources.filtre_standard;
            this.m_btnValide.Location = new System.Drawing.Point(137, 0);
            this.m_btnValide.Name = "m_btnValide";
            this.m_btnValide.Size = new System.Drawing.Size(21, 20);
            this.m_btnValide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_btnValide.TabIndex = 1;
            this.m_btnValide.TabStop = false;
            this.m_btnValide.Click += new System.EventHandler(this.m_btnValide_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::sc2i.win32.data.navigation.Properties.Resources.key1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // CFiltreIdToolStripControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtId);
            this.Controls.Add(this.m_btnValide);
            this.Controls.Add(this.pictureBox1);
            this.Name = "CFiltreIdToolStripControl";
            this.Size = new System.Drawing.Size(158, 20);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnValide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C2iTextBoxNumerique m_txtId;
        private System.Windows.Forms.PictureBox m_btnValide;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
