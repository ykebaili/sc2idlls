namespace sc2i.win32.common
{
    partial class CArbreChampsDotNet
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkAnnuler = new System.Windows.Forms.LinkLabel();
            this.m_lnkOk = new System.Windows.Forms.LinkLabel();
            this.m_txtNomChamp = new System.Windows.Forms.TextBox();
            this.m_arbreChamps = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkAnnuler);
            this.panel1.Controls.Add(this.m_lnkOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 22);
            this.panel1.TabIndex = 0;
            // 
            // m_lnkAnnuler
            // 
            this.m_lnkAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lnkAnnuler.AutoSize = true;
            this.m_lnkAnnuler.Location = new System.Drawing.Point(100, 5);
            this.m_lnkAnnuler.Name = "m_lnkAnnuler";
            this.m_lnkAnnuler.Size = new System.Drawing.Size(43, 13);
            this.m_lnkAnnuler.TabIndex = 1;
            this.m_lnkAnnuler.TabStop = true;
            this.m_lnkAnnuler.Text = "Annuler";
            this.m_lnkAnnuler.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAnnuler_LinkClicked);
            // 
            // m_lnkOk
            // 
            this.m_lnkOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lnkOk.AutoSize = true;
            this.m_lnkOk.Location = new System.Drawing.Point(73, 5);
            this.m_lnkOk.Name = "m_lnkOk";
            this.m_lnkOk.Size = new System.Drawing.Size(21, 13);
            this.m_lnkOk.TabIndex = 0;
            this.m_lnkOk.TabStop = true;
            this.m_lnkOk.Text = "Ok";
            this.m_lnkOk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOk_LinkClicked);
            // 
            // m_txtNomChamp
            // 
            this.m_txtNomChamp.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtNomChamp.Location = new System.Drawing.Point(0, 0);
            this.m_txtNomChamp.Name = "m_txtNomChamp";
            this.m_txtNomChamp.Size = new System.Drawing.Size(217, 20);
            this.m_txtNomChamp.TabIndex = 0;
            // 
            // m_arbreChamps
            // 
            this.m_arbreChamps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreChamps.Location = new System.Drawing.Point(0, 20);
            this.m_arbreChamps.Name = "m_arbreChamps";
            this.m_arbreChamps.Size = new System.Drawing.Size(217, 172);
            this.m_arbreChamps.TabIndex = 1;
            this.m_arbreChamps.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbreChamps_BeforeExpand);
            this.m_arbreChamps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreChamps_AfterSelect);
            // 
            // CArbreChampsDotNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_arbreChamps);
            this.Controls.Add(this.m_txtNomChamp);
            this.Controls.Add(this.panel1);
            this.Name = "CArbreChampsDotNet";
            this.Size = new System.Drawing.Size(217, 214);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel m_lnkAnnuler;
        private System.Windows.Forms.TextBox m_txtNomChamp;
        private System.Windows.Forms.TreeView m_arbreChamps;
        private System.Windows.Forms.LinkLabel m_lnkOk;
    }
}
