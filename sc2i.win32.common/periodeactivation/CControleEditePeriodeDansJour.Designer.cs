namespace sc2i.win32.common.periodeactivation
{
    partial class CControleEditePeriodeDansJour
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_wndFin = new sc2i.win32.common.CWndSaisieHeure();
            this.m_wndDebut = new sc2i.win32.common.CWndSaisieHeure();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "From|20006";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(120, 0);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "To|20007";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_wndFin);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_wndDebut);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 23);
            this.panel1.TabIndex = 2;
            // 
            // m_wndFin
            // 
            this.m_wndFin.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndFin.Location = new System.Drawing.Point(172, 0);
            this.m_wndFin.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_wndFin, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndFin.Name = "m_wndFin";
            this.m_wndFin.NullAutorise = false;
            this.m_wndFin.SaisieDuree = false;
            this.m_wndFin.Size = new System.Drawing.Size(58, 23);
            this.m_wndFin.TabIndex = 2;
            this.m_wndFin.ValeurHeure = 1.5;
            // 
            // m_wndDebut
            // 
            this.m_wndDebut.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndDebut.Location = new System.Drawing.Point(62, 0);
            this.m_wndDebut.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_wndDebut, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndDebut.Name = "m_wndDebut";
            this.m_wndDebut.NullAutorise = false;
            this.m_wndDebut.SaisieDuree = false;
            this.m_wndDebut.Size = new System.Drawing.Size(58, 23);
            this.m_wndDebut.TabIndex = 1;
            this.m_wndDebut.ValeurHeure = 0.5;
            // 
            // CControleEditePeriodeDansJour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditePeriodeDansJour";
            this.Size = new System.Drawing.Size(323, 44);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private CWndSaisieHeure m_wndDebut;
        private CWndSaisieHeure m_wndFin;
        private CExtModeEdition m_extModeEdition;
    }
}
