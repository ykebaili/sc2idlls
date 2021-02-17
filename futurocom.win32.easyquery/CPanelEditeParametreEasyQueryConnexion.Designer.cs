namespace futurocom.win32.easyquery
{
    partial class CPanelEditeParametreEasyQueryConnexion
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
            this.m_txtNomConnexion = new sc2i.win32.common.C2iTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_cmbTypeConnexion = new sc2i.win32.common.CComboboxAutoFilled();
            this.label2 = new System.Windows.Forms.Label();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_wndFormulesParametres = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtNomConnexion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 21);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 0;
            // 
            // m_txtNomConnexion
            // 
            this.m_txtNomConnexion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNomConnexion.EmptyText = "";
            this.m_txtNomConnexion.Location = new System.Drawing.Point(169, 0);
            this.m_txtNomConnexion.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtNomConnexion, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtNomConnexion.Name = "m_txtNomConnexion";
            this.m_txtNomConnexion.Size = new System.Drawing.Size(269, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNomConnexion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNomConnexion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomConnexion.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 21);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connexion name|20065";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_cmbTypeConnexion);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 21);
            this.m_extModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(438, 21);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 1;
            // 
            // m_cmbTypeConnexion
            // 
            this.m_cmbTypeConnexion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbTypeConnexion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeConnexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeConnexion.FormattingEnabled = true;
            this.m_cmbTypeConnexion.IsLink = false;
            this.m_cmbTypeConnexion.ListDonnees = null;
            this.m_cmbTypeConnexion.Location = new System.Drawing.Point(169, 0);
            this.m_cmbTypeConnexion.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbTypeConnexion, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeConnexion.Name = "m_cmbTypeConnexion";
            this.m_cmbTypeConnexion.NullAutorise = false;
            this.m_cmbTypeConnexion.ProprieteAffichee = null;
            this.m_cmbTypeConnexion.Size = new System.Drawing.Size(269, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbTypeConnexion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbTypeConnexion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeConnexion.TabIndex = 1;
            this.m_cmbTypeConnexion.TextNull = "(empty)";
            this.m_cmbTypeConnexion.Tri = true;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 21);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 0;
            this.label2.Text = "Connexion type|20066";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_wndFormulesParametres
            // 
            this.m_wndFormulesParametres.AutoScroll = true;
            this.m_wndFormulesParametres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndFormulesParametres.HasDeleteButton = true;
            this.m_wndFormulesParametres.HasHadButton = true;
            this.m_wndFormulesParametres.HeaderTextForFormula = "";
            this.m_wndFormulesParametres.HeaderTextForName = "";
            this.m_wndFormulesParametres.HideNomFormule = false;
            this.m_wndFormulesParametres.Location = new System.Drawing.Point(0, 42);
            this.m_wndFormulesParametres.LockEdition = true;
            this.m_extModeEdition.SetModeEdition(this.m_wndFormulesParametres, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndFormulesParametres.Name = "m_wndFormulesParametres";
            this.m_wndFormulesParametres.Size = new System.Drawing.Size(438, 132);
            this.cExtStyle1.SetStyleBackColor(this.m_wndFormulesParametres, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndFormulesParametres, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndFormulesParametres.TabIndex = 3;
            this.m_wndFormulesParametres.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // CPanelEditeParametreEasyQueryConnexion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_wndFormulesParametres);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditeParametreEasyQueryConnexion";
            this.Size = new System.Drawing.Size(438, 174);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iTextBox m_txtNomConnexion;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbTypeConnexion;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_wndFormulesParametres;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
    }
}
