namespace sc2i.win32.process.workflow
{
    partial class CPanelEditeParametresInitialisationEtape
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
            this.components = new System.ComponentModel.Container();
            this.components = new System.ComponentModel.Container();
            this.m_panelFormules = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5 = new sc2i.win32.common.C2iPanel(this.components);
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_txtFormuleInitialisation = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_chkReevaluerFormuleSurRedemarrage = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_chkRecalculerAffectations = new System.Windows.Forms.CheckBox();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelFormules.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFormules.Controls.Add(this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5);
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.ForeColor = System.Drawing.Color.Black;
            this.m_panelFormules.HasDeleteButton = true;
            this.m_panelFormules.HasHadButton = true;
            this.m_panelFormules.HeaderTextForFormula = "Assignment formula|20042";
            this.m_panelFormules.HeaderTextForName = "Label|20041";
            this.m_panelFormules.HideNomFormule = false;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 19);
            this.m_panelFormules.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelFormules, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(607, 276);
            this.m_extStyle.SetStyleBackColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormules.TabIndex = 7;
            this.m_panelFormules.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5
            // 
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.AutoScroll = true;
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.Location = new System.Drawing.Point(0, 0);
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.Name = "object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5";
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.Size = new System.Drawing.Size(607, 276);
            this.m_extStyle.SetStyleBackColor(this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5.TabIndex = 2;
            // 
            // m_tabControl
            // 
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_tabControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = false;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 1;
            this.m_tabControl.SelectedTab = this.tabPage2;
            this.m_tabControl.Size = new System.Drawing.Size(607, 320);
            this.m_extStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 0;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_txtFormuleInitialisation);
            this.tabPage2.Controls.Add(this.m_chkReevaluerFormuleSurRedemarrage);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.m_extModeEdition.SetModeEdition(this.tabPage2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(607, 295);
            this.m_extStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Initialization|20067";
            // 
            // m_txtFormuleInitialisation
            // 
            this.m_txtFormuleInitialisation.AllowGraphic = true;
            this.m_txtFormuleInitialisation.AllowNullFormula = true;
            this.m_txtFormuleInitialisation.AllowSaisieTexte = true;
            this.m_txtFormuleInitialisation.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtFormuleInitialisation.Formule = null;
            this.m_txtFormuleInitialisation.Location = new System.Drawing.Point(0, 19);
            this.m_txtFormuleInitialisation.LockEdition = false;
            this.m_txtFormuleInitialisation.LockZoneTexte = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtFormuleInitialisation, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormuleInitialisation.Name = "m_txtFormuleInitialisation";
            this.m_txtFormuleInitialisation.Size = new System.Drawing.Size(607, 44);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleInitialisation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleInitialisation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleInitialisation.TabIndex = 0;
            // 
            // m_chkReevaluerFormuleSurRedemarrage
            // 
            this.m_chkReevaluerFormuleSurRedemarrage.AutoSize = true;
            this.m_chkReevaluerFormuleSurRedemarrage.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkReevaluerFormuleSurRedemarrage.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkReevaluerFormuleSurRedemarrage, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkReevaluerFormuleSurRedemarrage.Name = "m_chkReevaluerFormuleSurRedemarrage";
            this.m_chkReevaluerFormuleSurRedemarrage.Size = new System.Drawing.Size(607, 19);
            this.m_extStyle.SetStyleBackColor(this.m_chkReevaluerFormuleSurRedemarrage, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkReevaluerFormuleSurRedemarrage, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkReevaluerFormuleSurRedemarrage.TabIndex = 1;
            this.m_chkReevaluerFormuleSurRedemarrage.Text = "Apply initialization when step restard|20121";
            this.m_chkReevaluerFormuleSurRedemarrage.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelFormules);
            this.tabPage1.Controls.Add(this.m_chkRecalculerAffectations);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.m_extModeEdition.SetModeEdition(this.tabPage1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(607, 295);
            this.m_extStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Assignments|20063";
            // 
            // m_chkRecalculerAffectations
            // 
            this.m_chkRecalculerAffectations.AutoSize = true;
            this.m_chkRecalculerAffectations.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkRecalculerAffectations.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkRecalculerAffectations, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkRecalculerAffectations.Name = "m_chkRecalculerAffectations";
            this.m_chkRecalculerAffectations.Size = new System.Drawing.Size(607, 19);
            this.m_extStyle.SetStyleBackColor(this.m_chkRecalculerAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkRecalculerAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkRecalculerAffectations.TabIndex = 0;
            this.m_chkRecalculerAffectations.Text = "Reset assignments when step restarts|20120";
            this.m_chkRecalculerAffectations.UseVisualStyleBackColor = true;
            // 
            // CPanelEditeParametresInitialisationEtape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_tabControl);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditeParametresInitialisationEtape";
            this.Size = new System.Drawing.Size(607, 320);
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormules.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.expression.CControlEditListeFormulesNommees m_panelFormules;
        private sc2i.win32.common.C2iPanel object_aef29e4f_b505_4092_aecf_1ecaf39e8fb5;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private sc2i.win32.common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleInitialisation;
        private System.Windows.Forms.CheckBox m_chkRecalculerAffectations;
        private System.Windows.Forms.CheckBox m_chkReevaluerFormuleSurRedemarrage;
    }
}
