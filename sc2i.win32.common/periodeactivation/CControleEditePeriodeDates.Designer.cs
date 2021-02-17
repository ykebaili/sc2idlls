namespace sc2i.win32.common.periodeactivation
{
    partial class CControleEditePeriodeDates
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
            this.m_dtFin = new sc2i.win32.common.C2iDateTimePicker();
            this.m_dtDebut = new sc2i.win32.common.C2iDateTimePicker();
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
            this.label2.Location = new System.Drawing.Point(151, 0);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "To|20007";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_dtFin);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_dtDebut);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 23);
            this.panel1.TabIndex = 2;
            // 
            // m_dtFin
            // 
            this.m_dtFin.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtFin.Location = new System.Drawing.Point(203, 0);
            this.m_dtFin.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_dtFin, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_dtFin.Name = "m_dtFin";
            this.m_dtFin.Size = new System.Drawing.Size(89, 20);
            this.m_dtFin.TabIndex = 4;
            this.m_dtFin.Value = new System.DateTime(2011, 8, 24, 8, 39, 4, 578);
            // 
            // m_dtDebut
            // 
            this.m_dtDebut.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_dtDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtDebut.Location = new System.Drawing.Point(62, 0);
            this.m_dtDebut.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_dtDebut, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_dtDebut.Name = "m_dtDebut";
            this.m_dtDebut.Size = new System.Drawing.Size(89, 20);
            this.m_dtDebut.TabIndex = 3;
            this.m_dtDebut.Value = new System.DateTime(2011, 8, 24, 8, 39, 4, 578);
            // 
            // CControleEditePeriodeDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditePeriodeDates";
            this.Size = new System.Drawing.Size(323, 44);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private C2iDateTimePicker m_dtFin;
        private C2iDateTimePicker m_dtDebut;
        private CExtModeEdition m_extModeEdition;
    }
}
