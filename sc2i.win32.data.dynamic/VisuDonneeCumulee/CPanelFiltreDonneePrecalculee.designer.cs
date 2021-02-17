namespace sc2i.win32.data.dynamic
{
    partial class CPanelFiltreDonneePrecalculee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelFiltreDonneePrecalculee));
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnEditFiltre = new System.Windows.Forms.Button();
            this.m_imageHasFiltreRef = new System.Windows.Forms.PictureBox();
            this.m_txtLibelle = new System.Windows.Forms.TextBox();
            this.m_imageHasFiltre = new System.Windows.Forms.PictureBox();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_panelFormule = new System.Windows.Forms.Panel();
            this.m_txtFormuleDesc = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageHasFiltreRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageHasFiltre)).BeginInit();
            this.m_panelTop.SuspendLayout();
            this.m_panelFormule.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtered elements|20021";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnEditFiltre
            // 
            this.m_btnEditFiltre.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnEditFiltre.Location = new System.Drawing.Point(332, 0);
            this.m_btnEditFiltre.Name = "m_btnEditFiltre";
            this.m_btnEditFiltre.Size = new System.Drawing.Size(25, 22);
            this.m_btnEditFiltre.TabIndex = 2;
            this.m_btnEditFiltre.Text = "...";
            this.m_btnEditFiltre.UseVisualStyleBackColor = true;
            this.m_btnEditFiltre.Click += new System.EventHandler(this.m_btnEditFiltre_Click);
            // 
            // m_imageHasFiltreRef
            // 
            this.m_imageHasFiltreRef.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_imageHasFiltreRef.Image = ((System.Drawing.Image)(resources.GetObject("m_imageHasFiltreRef.Image")));
            this.m_imageHasFiltreRef.Location = new System.Drawing.Point(309, 0);
            this.m_imageHasFiltreRef.Name = "m_imageHasFiltreRef";
            this.m_imageHasFiltreRef.Size = new System.Drawing.Size(23, 22);
            this.m_imageHasFiltreRef.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_imageHasFiltreRef.TabIndex = 3;
            this.m_imageHasFiltreRef.TabStop = false;
            this.m_imageHasFiltreRef.Visible = false;
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtLibelle.Location = new System.Drawing.Point(123, 0);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(234, 20);
            this.m_txtLibelle.TabIndex = 4;
            this.m_txtLibelle.TextChanged += new System.EventHandler(this.m_txtLibelle_TextChanged);
            // 
            // m_imageHasFiltre
            // 
            this.m_imageHasFiltre.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_imageHasFiltre.Location = new System.Drawing.Point(286, 0);
            this.m_imageHasFiltre.Name = "m_imageHasFiltre";
            this.m_imageHasFiltre.Size = new System.Drawing.Size(23, 22);
            this.m_imageHasFiltre.TabIndex = 5;
            this.m_imageHasFiltre.TabStop = false;
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_imageHasFiltre);
            this.m_panelTop.Controls.Add(this.m_imageHasFiltreRef);
            this.m_panelTop.Controls.Add(this.m_btnEditFiltre);
            this.m_panelTop.Controls.Add(this.m_txtLibelle);
            this.m_panelTop.Controls.Add(this.label1);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(357, 22);
            this.m_panelTop.TabIndex = 6;
            // 
            // m_panelFormule
            // 
            this.m_panelFormule.Controls.Add(this.m_txtFormuleDesc);
            this.m_panelFormule.Controls.Add(this.label2);
            this.m_panelFormule.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFormule.Location = new System.Drawing.Point(0, 22);
            this.m_panelFormule.Name = "m_panelFormule";
            this.m_panelFormule.Size = new System.Drawing.Size(357, 23);
            this.m_panelFormule.TabIndex = 7;
            // 
            // m_txtFormuleDesc
            // 
            this.m_txtFormuleDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormuleDesc.Formule = null;
            this.m_txtFormuleDesc.Location = new System.Drawing.Point(123, 0);
            this.m_txtFormuleDesc.LockEdition = false;
            this.m_txtFormuleDesc.LockZoneTexte = false;
            this.m_txtFormuleDesc.Name = "m_txtFormuleDesc";
            this.m_txtFormuleDesc.Size = new System.Drawing.Size(234, 23);
            this.m_txtFormuleDesc.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description|20022";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 1);
            this.panel1.TabIndex = 8;
            // 
            // CPanelFiltreDonneePrecalculee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_panelFormule);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CPanelFiltreDonneePrecalculee";
            this.Size = new System.Drawing.Size(357, 48);
            this.Load += new System.EventHandler(this.CPanelFiltreDonneePrecalculee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageHasFiltreRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageHasFiltre)).EndInit();
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            this.m_panelFormule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnEditFiltre;
        private System.Windows.Forms.PictureBox m_imageHasFiltreRef;
        private System.Windows.Forms.TextBox m_txtLibelle;
        private System.Windows.Forms.PictureBox m_imageHasFiltre;
        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Panel m_panelFormule;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}
