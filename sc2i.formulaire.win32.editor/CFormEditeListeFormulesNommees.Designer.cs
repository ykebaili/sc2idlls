namespace sc2i.formulaire.win32
{
    partial class CFormEditeListeFormulesNommees
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_wndListeFormules = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndListeFormules
            // 
            this.m_wndListeFormules.AutoScroll = true;
            this.m_wndListeFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeFormules.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeFormules.LockEdition = false;
            this.m_wndListeFormules.Name = "m_wndListeFormules";
            this.m_wndListeFormules.Size = new System.Drawing.Size(424, 228);
            this.m_wndListeFormules.TabIndex = 0;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.button1);
            this.m_panelBas.Controls.Add(this.m_btnOk);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 228);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(424, 38);
            this.m_panelBas.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cancel|2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(126, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditeListeFormulesNommees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(424, 266);
            this.Controls.Add(this.m_wndListeFormules);
            this.Controls.Add(this.m_panelBas);
            this.Name = "CFormEditeListeFormulesNommees";
            this.Text = "Functions|20009";
            this.Load += new System.EventHandler(this.CFormEditeListeFormulesNommees_Load);
            this.m_panelBas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.expression.CControlEditListeFormulesNommees m_wndListeFormules;
        private System.Windows.Forms.Panel m_panelBas;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button button1;
    }
}