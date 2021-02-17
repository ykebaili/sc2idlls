namespace sc2i.win32.common.customizableList
{
    partial class CCustomizableList
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
                ClearImages();
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
            this.m_panelDessin = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // m_panelDessin
            // 
            this.m_panelDessin.AllowDrop = true;
            this.m_panelDessin.AutoScroll = true;
            this.m_panelDessin.BackColor = System.Drawing.SystemColors.Control;
            this.m_panelDessin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDessin.Location = new System.Drawing.Point(0, 0);
            this.m_panelDessin.Name = "m_panelDessin";
            this.m_panelDessin.Size = new System.Drawing.Size(450, 255);
            this.m_panelDessin.TabIndex = 0;
            this.m_panelDessin.Paint += new System.Windows.Forms.PaintEventHandler(this.m_PanelDessin_Paint);
            this.m_panelDessin.DragOver += new System.Windows.Forms.DragEventHandler(this.m_panelDessin_DragOver);
            this.m_panelDessin.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_panelDessin_DragDrop);
            this.m_panelDessin.Scroll += new System.Windows.Forms.ScrollEventHandler(this.m_panelDessin_Scroll);
            this.m_panelDessin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_panelDessin_MouseDown);
            this.m_panelDessin.DragLeave += new System.EventHandler(this.m_panelDessin_DragLeave);
            this.m_panelDessin.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_panelDessin_DragEnter);
            this.m_panelDessin.SizeChanged += new System.EventHandler(this.m_panelDessin_SizeChanged);
            // 
            // CCustomizableList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelDessin);
            this.DoubleBuffered = true;
            this.Name = "CCustomizableList";
            this.Size = new System.Drawing.Size(450, 255);
            this.BackColorChanged += new System.EventHandler(this.CCustomizableList_BackColorChanged);
            this.Enter += new System.EventHandler(this.CCustomizableList_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel m_panelDessin;


    }
}
