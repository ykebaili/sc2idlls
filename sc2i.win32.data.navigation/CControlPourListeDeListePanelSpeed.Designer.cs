namespace sc2i.win32.data.navigation
{
    partial class CControlPourListeDeListePanelSpeed
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
            this.m_panelTotal = new System.Windows.Forms.Panel();
            this.m_lnkListe = new System.Windows.Forms.LinkLabel();
            this.m_picSelected = new System.Windows.Forms.PictureBox();
            this.m_panelTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelTotal.Controls.Add(this.m_lnkListe);
            this.m_panelTotal.Controls.Add(this.m_picSelected);
            this.m_panelTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelTotal.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelTotal, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(199, 30);
            this.m_panelTotal.TabIndex = 0;
            // 
            // m_lnkListe
            // 
            this.m_lnkListe.BackColor = System.Drawing.Color.White;
            this.m_lnkListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lnkListe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lnkListe.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lnkListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkListe.Name = "m_lnkListe";
            this.m_lnkListe.Size = new System.Drawing.Size(170, 28);
            this.m_lnkListe.TabIndex = 0;
            this.m_lnkListe.TabStop = true;
            this.m_lnkListe.Text = "Link";
            this.m_lnkListe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkListe.Click += new System.EventHandler(this.m_lnkListe_Click);
            // 
            // m_picSelected
            // 
            this.m_picSelected.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_picSelected.Image = global::sc2i.win32.data.navigation.Properties.Resources.Valider;
            this.m_picSelected.Location = new System.Drawing.Point(170, 0);
            this.m_extModeEdition.SetModeEdition(this.m_picSelected, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_picSelected.Name = "m_picSelected";
            this.m_picSelected.Size = new System.Drawing.Size(27, 28);
            this.m_picSelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_picSelected.TabIndex = 1;
            this.m_picSelected.TabStop = false;
            this.m_picSelected.Visible = false;
            // 
            // CControlPourListeDeListePanelSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ColorInactive = System.Drawing.Color.White;
            this.Controls.Add(this.m_panelTotal);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlPourListeDeListePanelSpeed";
            this.Size = new System.Drawing.Size(199, 30);
            this.m_panelTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelTotal;
        private System.Windows.Forms.LinkLabel m_lnkListe;
        private System.Windows.Forms.PictureBox m_picSelected;
    }
}
