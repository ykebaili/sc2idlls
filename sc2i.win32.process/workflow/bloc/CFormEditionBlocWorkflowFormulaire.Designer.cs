namespace sc2i.win32.process.workflow.bloc
{
    partial class CFormEditionBlocWorkflowFormulaire
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionBlocWorkflowFormulaire));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_txtFormuleElementEdite = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_chkSecondaireEnEdition = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtFormuleElementSecondaire = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtSelectFormulaireSecondaire = new sc2i.win32.data.dynamic.C2iTextBoxFiltreRapide();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_chkMasquerApresValidation = new System.Windows.Forms.CheckBox();
            this.m_chkLockItemWhenComplete = new System.Windows.Forms.CheckBox();
            this.m_chkHideOnChangeForm = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lnkSelectFormulaires = new System.Windows.Forms.LinkLabel();
            this.m_rbtnFormulaireSpecifique = new System.Windows.Forms.RadioButton();
            this.m_rbtnStandard = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnCopy = new System.Windows.Forms.PictureBox();
            this.m_btnPaste = new System.Windows.Forms.PictureBox();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageForm = new Crownwood.Magic.Controls.TabPage();
            this.m_panelChamps = new sc2i.win32.data.dynamic.CPanelChampsCustom();
            this.m_pageEndConditions = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeFormules = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_chkPasserSiPasErreur = new System.Windows.Forms.CheckBox();
            this.m_panelInstructions = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtFormuleInstructions = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_chkPromptToEnd = new System.Windows.Forms.CheckBox();
            this.m_pageRestrictions = new Crownwood.Magic.Controls.TabPage();
            this.m_panelRestrictions = new sc2i.win32.process.workflow.bloc.CPanelEditeRestrictionsBlocWorkflowFormulaire();
            this.m_pageAffectations = new Crownwood.Magic.Controls.TabPage();
            this.m_panelAffectations = new sc2i.win32.process.workflow.CPanelEditeParametresInitialisationEtape();
            this.m_pageGestionErreur = new Crownwood.Magic.Controls.TabPage();
            this.m_panelGestionErreur = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.m_pageStopHandler = new Crownwood.Magic.Controls.TabPage();
            this.m_panelParametreDeclenchement = new sc2i.win32.process.CPanelEditParametreDeclencheur();
            this.m_panelGauche = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel1 = new sc2i.win32.common.C2iPanel(this.components);
            this.m_chkUseStopHandler = new System.Windows.Forms.CheckBox();
            this.m_menuFormulaires = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.m_txtExceptionRestriction = new System.Windows.Forms.TextBox();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnPaste)).BeginInit();
            this.m_tab.SuspendLayout();
            this.m_pageForm.SuspendLayout();
            this.m_pageEndConditions.SuspendLayout();
            this.m_panelInstructions.SuspendLayout();
            this.m_pageRestrictions.SuspendLayout();
            this.m_pageAffectations.SuspendLayout();
            this.m_pageGestionErreur.SuspendLayout();
            this.m_pageStopHandler.SuspendLayout();
            this.m_panelParametreDeclenchement.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtFormuleElementEdite
            // 
            this.m_txtFormuleElementEdite.AllowGraphic = true;
            this.m_txtFormuleElementEdite.AllowNullFormula = false;
            this.m_txtFormuleElementEdite.AllowSaisieTexte = true;
            this.m_txtFormuleElementEdite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleElementEdite.Formule = null;
            this.m_txtFormuleElementEdite.Location = new System.Drawing.Point(186, 0);
            this.m_txtFormuleElementEdite.LockEdition = false;
            this.m_txtFormuleElementEdite.LockZoneTexte = false;
            this.m_txtFormuleElementEdite.Name = "m_txtFormuleElementEdite";
            this.m_txtFormuleElementEdite.Size = new System.Drawing.Size(846, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleElementEdite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleElementEdite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleElementEdite.TabIndex = 0;
            this.m_txtFormuleElementEdite.Validated += new System.EventHandler(this.m_txtFormuleElementEdite_Validated);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "Edited element|20047";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.panel4);
            this.c2iPanelOmbre1.Controls.Add(this.label5);
            this.c2iPanelOmbre1.Controls.Add(this.panel3);
            this.c2iPanelOmbre1.Controls.Add(this.label4);
            this.c2iPanelOmbre1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(1058, 203);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_chkSecondaireEnEdition);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.m_txtFormuleElementSecondaire);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.m_txtSelectFormulaireSecondaire);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 113);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1058, 73);
            this.m_extStyle.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 2;
            // 
            // m_chkSecondaireEnEdition
            // 
            this.m_chkSecondaireEnEdition.AutoSize = true;
            this.m_chkSecondaireEnEdition.Location = new System.Drawing.Point(186, 53);
            this.m_chkSecondaireEnEdition.Name = "m_chkSecondaireEnEdition";
            this.m_chkSecondaireEnEdition.Size = new System.Drawing.Size(219, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkSecondaireEnEdition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkSecondaireEnEdition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkSecondaireEnEdition.TabIndex = 5;
            this.m_chkSecondaireEnEdition.Text = "Secondary element is in edit mode|20100";
            this.m_chkSecondaireEnEdition.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 1;
            this.label6.Text = "Edited element|20047";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtFormuleElementSecondaire
            // 
            this.m_txtFormuleElementSecondaire.AllowGraphic = true;
            this.m_txtFormuleElementSecondaire.AllowNullFormula = false;
            this.m_txtFormuleElementSecondaire.AllowSaisieTexte = true;
            this.m_txtFormuleElementSecondaire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleElementSecondaire.Formule = null;
            this.m_txtFormuleElementSecondaire.Location = new System.Drawing.Point(186, 0);
            this.m_txtFormuleElementSecondaire.LockEdition = false;
            this.m_txtFormuleElementSecondaire.LockZoneTexte = false;
            this.m_txtFormuleElementSecondaire.Name = "m_txtFormuleElementSecondaire";
            this.m_txtFormuleElementSecondaire.Size = new System.Drawing.Size(846, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleElementSecondaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleElementSecondaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleElementSecondaire.TabIndex = 0;
            this.m_txtFormuleElementSecondaire.Validated += new System.EventHandler(this.m_txtFormuleElementEdite_Validated);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label7.TabIndex = 3;
            this.label7.Text = "Form to use|20048";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtSelectFormulaireSecondaire
            // 
            this.m_txtSelectFormulaireSecondaire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSelectFormulaireSecondaire.ElementSelectionne = null;
            this.m_txtSelectFormulaireSecondaire.FonctionTextNull = null;
            this.m_txtSelectFormulaireSecondaire.ImageDisplayMode = sc2i.win32.data.dynamic.EModeAffichageImageTextBoxRapide.Always;
            this.m_txtSelectFormulaireSecondaire.Location = new System.Drawing.Point(186, 29);
            this.m_txtSelectFormulaireSecondaire.LockEdition = false;
            this.m_txtSelectFormulaireSecondaire.Name = "m_txtSelectFormulaireSecondaire";
            this.m_txtSelectFormulaireSecondaire.SelectedObject = null;
            this.m_txtSelectFormulaireSecondaire.SelectionLength = 0;
            this.m_txtSelectFormulaireSecondaire.SelectionStart = 0;
            this.m_txtSelectFormulaireSecondaire.Size = new System.Drawing.Size(846, 25);
            this.m_txtSelectFormulaireSecondaire.SpecificImage = null;
            this.m_extStyle.SetStyleBackColor(this.m_txtSelectFormulaireSecondaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtSelectFormulaireSecondaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSelectFormulaireSecondaire.TabIndex = 8;
            this.m_txtSelectFormulaireSecondaire.TextNull = "";
            this.m_txtSelectFormulaireSecondaire.UseIntellisense = true;
            this.m_txtSelectFormulaireSecondaire.Enter += new System.EventHandler(this.m_txtSelectFormulaireSecondaire_Enter);
            this.m_txtSelectFormulaireSecondaire.ElementSelectionneChanged += new System.EventHandler(this.m_txtSelectFormulaireSecondaire_ElementSelectionneChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1058, 16);
            this.m_extStyle.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 7;
            this.label5.Text = "Secondary element|20085";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_chkMasquerApresValidation);
            this.panel3.Controls.Add(this.m_chkLockItemWhenComplete);
            this.panel3.Controls.Add(this.m_chkHideOnChangeForm);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.m_txtFormuleElementEdite);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1058, 81);
            this.m_extStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 6;
            // 
            // m_chkMasquerApresValidation
            // 
            this.m_chkMasquerApresValidation.AutoSize = true;
            this.m_chkMasquerApresValidation.Location = new System.Drawing.Point(501, 48);
            this.m_chkMasquerApresValidation.Name = "m_chkMasquerApresValidation";
            this.m_chkMasquerApresValidation.Size = new System.Drawing.Size(175, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkMasquerApresValidation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkMasquerApresValidation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkMasquerApresValidation.TabIndex = 7;
            this.m_chkMasquerApresValidation.Text = "Hide step after validation|20124";
            this.m_chkMasquerApresValidation.UseVisualStyleBackColor = true;
            // 
            // m_chkLockItemWhenComplete
            // 
            this.m_chkLockItemWhenComplete.AutoSize = true;
            this.m_chkLockItemWhenComplete.Location = new System.Drawing.Point(186, 64);
            this.m_chkLockItemWhenComplete.Name = "m_chkLockItemWhenComplete";
            this.m_chkLockItemWhenComplete.Size = new System.Drawing.Size(197, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkLockItemWhenComplete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkLockItemWhenComplete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkLockItemWhenComplete.TabIndex = 6;
            this.m_chkLockItemWhenComplete.Text = "Lock element when complete|20123";
            this.m_chkLockItemWhenComplete.UseVisualStyleBackColor = true;
            // 
            // m_chkHideOnChangeForm
            // 
            this.m_chkHideOnChangeForm.AutoSize = true;
            this.m_chkHideOnChangeForm.Location = new System.Drawing.Point(186, 48);
            this.m_chkHideOnChangeForm.Name = "m_chkHideOnChangeForm";
            this.m_chkHideOnChangeForm.Size = new System.Drawing.Size(188, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkHideOnChangeForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkHideOnChangeForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkHideOnChangeForm.TabIndex = 5;
            this.m_chkHideOnChangeForm.Text = "Hide step on changing form|20087";
            this.m_chkHideOnChangeForm.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 3;
            this.label2.Text = "Form to use|20048";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_lnkSelectFormulaires);
            this.panel2.Controls.Add(this.m_rbtnFormulaireSpecifique);
            this.panel2.Controls.Add(this.m_rbtnStandard);
            this.panel2.Location = new System.Drawing.Point(186, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 25);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4;
            // 
            // m_lnkSelectFormulaires
            // 
            this.m_lnkSelectFormulaires.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkSelectFormulaires.Location = new System.Drawing.Point(195, 0);
            this.m_lnkSelectFormulaires.Name = "m_lnkSelectFormulaires";
            this.m_lnkSelectFormulaires.Size = new System.Drawing.Size(338, 25);
            this.m_extStyle.SetStyleBackColor(this.m_lnkSelectFormulaires, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lnkSelectFormulaires, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkSelectFormulaires.TabIndex = 2;
            this.m_lnkSelectFormulaires.TabStop = true;
            this.m_lnkSelectFormulaires.Text = "Select specific Forms|10009";
            this.m_lnkSelectFormulaires.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkSelectFormulaires.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkSelectFormulaires_LinkClicked);
            // 
            // m_rbtnFormulaireSpecifique
            // 
            this.m_rbtnFormulaireSpecifique.AutoSize = true;
            this.m_rbtnFormulaireSpecifique.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnFormulaireSpecifique.Location = new System.Drawing.Point(100, 0);
            this.m_rbtnFormulaireSpecifique.Name = "m_rbtnFormulaireSpecifique";
            this.m_rbtnFormulaireSpecifique.Size = new System.Drawing.Size(95, 25);
            this.m_extStyle.SetStyleBackColor(this.m_rbtnFormulaireSpecifique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_rbtnFormulaireSpecifique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_rbtnFormulaireSpecifique.TabIndex = 1;
            this.m_rbtnFormulaireSpecifique.TabStop = true;
            this.m_rbtnFormulaireSpecifique.Text = "Specific|20050";
            this.m_rbtnFormulaireSpecifique.UseVisualStyleBackColor = true;
            this.m_rbtnFormulaireSpecifique.CheckedChanged += new System.EventHandler(this.m_rbtnFormulaireSpecifique_CheckedChanged);
            // 
            // m_rbtnStandard
            // 
            this.m_rbtnStandard.AutoSize = true;
            this.m_rbtnStandard.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnStandard.Location = new System.Drawing.Point(0, 0);
            this.m_rbtnStandard.Name = "m_rbtnStandard";
            this.m_rbtnStandard.Size = new System.Drawing.Size(100, 25);
            this.m_extStyle.SetStyleBackColor(this.m_rbtnStandard, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_rbtnStandard, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_rbtnStandard.TabIndex = 0;
            this.m_rbtnStandard.TabStop = true;
            this.m_rbtnStandard.Text = "Standard|20049";
            this.m_rbtnStandard.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1058, 16);
            this.m_extStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 5;
            this.label4.Text = "Main element|20084";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCopy);
            this.panel1.Controls.Add(this.m_btnPaste);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 4;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCopy.Image = global::sc2i.win32.process.Properties.Resources.copy;
            this.m_btnCopy.Location = new System.Drawing.Point(975, 0);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(28, 36);
            this.m_btnCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_extStyle.SetStyleBackColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCopy.TabIndex = 11;
            this.m_btnCopy.TabStop = false;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPaste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnPaste.Image = global::sc2i.win32.process.Properties.Resources.paste;
            this.m_btnPaste.Location = new System.Drawing.Point(1004, 0);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(28, 36);
            this.m_btnPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_extStyle.SetStyleBackColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPaste.TabIndex = 12;
            this.m_btnPaste.TabStop = false;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(536, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(482, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_tab
            // 
            this.m_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tab.BoldSelectedPage = true;
            this.m_tab.ControlBottomOffset = 16;
            this.m_tab.ControlRightOffset = 16;
            this.m_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tab.ForeColor = System.Drawing.Color.Black;
            this.m_tab.IDEPixelArea = false;
            this.m_tab.Location = new System.Drawing.Point(0, 203);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = true;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 1;
            this.m_tab.SelectedTab = this.m_pageRestrictions;
            this.m_tab.Size = new System.Drawing.Size(1058, 394);
            this.m_extStyle.SetStyleBackColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tab.TabIndex = 5;
            this.m_tab.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_pageEndConditions,
            this.m_pageRestrictions,
            this.m_pageAffectations,
            this.m_pageGestionErreur,
            this.m_pageStopHandler,
            this.m_pageForm});
            this.m_tab.TextColor = System.Drawing.Color.Black;
            // 
            // m_pageForm
            // 
            this.m_pageForm.Controls.Add(this.m_panelChamps);
            this.m_pageForm.Location = new System.Drawing.Point(0, 25);
            this.m_pageForm.Name = "m_pageForm";
            this.m_pageForm.Selected = false;
            this.m_pageForm.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_pageForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageForm.TabIndex = 15;
            this.m_pageForm.Title = "Properties|198";
            // 
            // m_panelChamps
            // 
            this.m_panelChamps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelChamps.BoldSelectedPage = true;
            this.m_panelChamps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelChamps.ElementEdite = null;
            this.m_panelChamps.ForeColor = System.Drawing.Color.Black;
            this.m_panelChamps.IDEPixelArea = false;
            this.m_panelChamps.Location = new System.Drawing.Point(0, 0);
            this.m_panelChamps.LockEdition = false;
            this.m_panelChamps.Name = "m_panelChamps";
            this.m_panelChamps.Ombre = false;
            this.m_panelChamps.PositionTop = true;
            this.m_panelChamps.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_panelChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_panelChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelChamps.TabIndex = 0;
            this.m_panelChamps.TextColor = System.Drawing.Color.Black;
            // 
            // m_pageEndConditions
            // 
            this.m_pageEndConditions.Controls.Add(this.m_wndListeFormules);
            this.m_pageEndConditions.Controls.Add(this.m_chkPasserSiPasErreur);
            this.m_pageEndConditions.Controls.Add(this.m_panelInstructions);
            this.m_pageEndConditions.Controls.Add(this.m_chkPromptToEnd);
            this.m_pageEndConditions.Location = new System.Drawing.Point(0, 25);
            this.m_pageEndConditions.Name = "m_pageEndConditions";
            this.m_pageEndConditions.Selected = false;
            this.m_pageEndConditions.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_pageEndConditions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageEndConditions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageEndConditions.TabIndex = 10;
            this.m_pageEndConditions.Title = "End conditions|20051";
            // 
            // m_wndListeFormules
            // 
            this.m_wndListeFormules.AutoScroll = true;
            this.m_wndListeFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeFormules.HasDeleteButton = true;
            this.m_wndListeFormules.HasHadButton = true;
            this.m_wndListeFormules.HeaderTextForFormula = "Condition (if false, error message will be shown)|20070";
            this.m_wndListeFormules.HeaderTextForName = "Error message|20069";
            this.m_wndListeFormules.HideNomFormule = false;
            this.m_wndListeFormules.Location = new System.Drawing.Point(0, 65);
            this.m_wndListeFormules.LockEdition = false;
            this.m_wndListeFormules.Name = "m_wndListeFormules";
            this.m_wndListeFormules.Size = new System.Drawing.Size(1042, 254);
            this.m_extStyle.SetStyleBackColor(this.m_wndListeFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_wndListeFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeFormules.TabIndex = 0;
            this.m_wndListeFormules.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // m_chkPasserSiPasErreur
            // 
            this.m_chkPasserSiPasErreur.AutoSize = true;
            this.m_chkPasserSiPasErreur.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_chkPasserSiPasErreur.Location = new System.Drawing.Point(0, 319);
            this.m_chkPasserSiPasErreur.Name = "m_chkPasserSiPasErreur";
            this.m_chkPasserSiPasErreur.Size = new System.Drawing.Size(1042, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkPasserSiPasErreur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkPasserSiPasErreur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkPasserSiPasErreur.TabIndex = 3;
            this.m_chkPasserSiPasErreur.Text = "Do not run step if no error when it starts|20129";
            this.m_chkPasserSiPasErreur.UseVisualStyleBackColor = true;
            // 
            // m_panelInstructions
            // 
            this.m_panelInstructions.Controls.Add(this.label3);
            this.m_panelInstructions.Controls.Add(this.m_txtFormuleInstructions);
            this.m_panelInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelInstructions.Location = new System.Drawing.Point(0, 0);
            this.m_panelInstructions.Name = "m_panelInstructions";
            this.m_panelInstructions.Size = new System.Drawing.Size(1042, 65);
            this.m_extStyle.SetStyleBackColor(this.m_panelInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelInstructions.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 1;
            this.label3.Text = "Instructions|20064";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtFormuleInstructions
            // 
            this.m_txtFormuleInstructions.AllowGraphic = true;
            this.m_txtFormuleInstructions.AllowNullFormula = false;
            this.m_txtFormuleInstructions.AllowSaisieTexte = true;
            this.m_txtFormuleInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleInstructions.Formule = null;
            this.m_txtFormuleInstructions.Location = new System.Drawing.Point(186, 3);
            this.m_txtFormuleInstructions.LockEdition = false;
            this.m_txtFormuleInstructions.LockZoneTexte = false;
            this.m_txtFormuleInstructions.Name = "m_txtFormuleInstructions";
            this.m_txtFormuleInstructions.Size = new System.Drawing.Size(846, 56);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleInstructions.TabIndex = 0;
            this.m_txtFormuleInstructions.Validated += new System.EventHandler(this.m_txtFormuleElementEdite_Validated);
            // 
            // m_chkPromptToEnd
            // 
            this.m_chkPromptToEnd.AutoSize = true;
            this.m_chkPromptToEnd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_chkPromptToEnd.Location = new System.Drawing.Point(0, 336);
            this.m_chkPromptToEnd.Name = "m_chkPromptToEnd";
            this.m_chkPromptToEnd.Size = new System.Drawing.Size(1042, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkPromptToEnd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkPromptToEnd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkPromptToEnd.TabIndex = 2;
            this.m_chkPromptToEnd.Text = "Prompt user to end step when all conditions are validated| 20119";
            this.m_chkPromptToEnd.UseVisualStyleBackColor = true;
            // 
            // m_pageRestrictions
            // 
            this.m_pageRestrictions.Controls.Add(this.m_panelRestrictions);
            this.m_pageRestrictions.Controls.Add(this.panel5);
            this.m_pageRestrictions.Location = new System.Drawing.Point(0, 25);
            this.m_pageRestrictions.Name = "m_pageRestrictions";
            this.m_pageRestrictions.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_pageRestrictions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageRestrictions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageRestrictions.TabIndex = 11;
            this.m_pageRestrictions.Title = "Restrictions|20060";
            // 
            // m_panelRestrictions
            // 
            this.m_panelRestrictions.AutoSize = true;
            this.m_panelRestrictions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelRestrictions.Location = new System.Drawing.Point(0, 28);
            this.m_panelRestrictions.LockEdition = false;
            this.m_panelRestrictions.Name = "m_panelRestrictions";
            this.m_panelRestrictions.Size = new System.Drawing.Size(1042, 325);
            this.m_extStyle.SetStyleBackColor(this.m_panelRestrictions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelRestrictions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelRestrictions.TabIndex = 0;
            // 
            // m_pageAffectations
            // 
            this.m_pageAffectations.Controls.Add(this.m_panelAffectations);
            this.m_pageAffectations.Location = new System.Drawing.Point(0, 25);
            this.m_pageAffectations.Name = "m_pageAffectations";
            this.m_pageAffectations.Selected = false;
            this.m_pageAffectations.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_pageAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageAffectations.TabIndex = 12;
            this.m_pageAffectations.Title = "Assignments|20063";
            // 
            // m_panelAffectations
            // 
            this.m_panelAffectations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelAffectations.Location = new System.Drawing.Point(0, 0);
            this.m_panelAffectations.LockEdition = false;
            this.m_panelAffectations.Name = "m_panelAffectations";
            this.m_panelAffectations.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_panelAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelAffectations.TabIndex = 0;
            // 
            // m_pageGestionErreur
            // 
            this.m_pageGestionErreur.Controls.Add(this.m_panelGestionErreur);
            this.m_pageGestionErreur.Controls.Add(this.label8);
            this.m_pageGestionErreur.Location = new System.Drawing.Point(0, 25);
            this.m_pageGestionErreur.Name = "m_pageGestionErreur";
            this.m_pageGestionErreur.Selected = false;
            this.m_pageGestionErreur.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_pageGestionErreur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageGestionErreur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageGestionErreur.TabIndex = 13;
            this.m_pageGestionErreur.Title = "Error handling|20126";
            // 
            // m_panelGestionErreur
            // 
            this.m_panelGestionErreur.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelGestionErreur.Location = new System.Drawing.Point(0, 23);
            this.m_panelGestionErreur.Name = "m_panelGestionErreur";
            this.m_panelGestionErreur.Size = new System.Drawing.Size(1042, 24);
            this.m_extStyle.SetStyleBackColor(this.m_panelGestionErreur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelGestionErreur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelGestionErreur.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1042, 23);
            this.m_extStyle.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 0;
            this.label8.Text = "If an error occurs, user can|20127";
            // 
            // m_pageStopHandler
            // 
            this.m_pageStopHandler.Controls.Add(this.m_panelParametreDeclenchement);
            this.m_pageStopHandler.Controls.Add(this.m_chkUseStopHandler);
            this.m_pageStopHandler.Location = new System.Drawing.Point(0, 25);
            this.m_pageStopHandler.Name = "m_pageStopHandler";
            this.m_pageStopHandler.Selected = false;
            this.m_pageStopHandler.Size = new System.Drawing.Size(1042, 353);
            this.m_extStyle.SetStyleBackColor(this.m_pageStopHandler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageStopHandler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageStopHandler.TabIndex = 14;
            this.m_pageStopHandler.Title = "Stop handler";
            // 
            // m_panelParametreDeclenchement
            // 
            this.m_panelParametreDeclenchement.AutoriseSurCreation = false;
            this.m_panelParametreDeclenchement.AutoriseSurDate = true;
            this.m_panelParametreDeclenchement.AutoriseSurManuel = true;
            this.m_panelParametreDeclenchement.AutoriseSurModification = true;
            this.m_panelParametreDeclenchement.AutoriseSurSuppression = false;
            this.m_panelParametreDeclenchement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.m_panelParametreDeclenchement.Controls.Add(this.m_panelGauche);
            this.m_panelParametreDeclenchement.Controls.Add(this.c2iPanel1);
            this.m_panelParametreDeclenchement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelParametreDeclenchement.Location = new System.Drawing.Point(0, 17);
            this.m_panelParametreDeclenchement.LockEdition = false;
            this.m_panelParametreDeclenchement.Name = "m_panelParametreDeclenchement";
            this.m_panelParametreDeclenchement.Size = new System.Drawing.Size(1042, 336);
            this.m_extStyle.SetStyleBackColor(this.m_panelParametreDeclenchement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelParametreDeclenchement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelParametreDeclenchement.TabIndex = 8;
            this.m_panelParametreDeclenchement.TypeCible = typeof(string);
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.LockEdition = false;
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(1042, 336);
            this.m_extStyle.SetStyleBackColor(this.m_panelGauche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelGauche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelGauche.TabIndex = 4004;
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.c2iPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanel1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel1.LockEdition = false;
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(1042, 336);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel1.TabIndex = 4004;
            // 
            // m_chkUseStopHandler
            // 
            this.m_chkUseStopHandler.AutoSize = true;
            this.m_chkUseStopHandler.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_chkUseStopHandler.Location = new System.Drawing.Point(0, 0);
            this.m_chkUseStopHandler.Name = "m_chkUseStopHandler";
            this.m_chkUseStopHandler.Size = new System.Drawing.Size(1042, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkUseStopHandler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkUseStopHandler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUseStopHandler.TabIndex = 0;
            this.m_chkUseStopHandler.Text = "Use stop handler|20300";
            this.m_chkUseStopHandler.UseVisualStyleBackColor = true;
            this.m_chkUseStopHandler.CheckedChanged += new System.EventHandler(this.m_chkUseStopHandler_CheckedChanged);
            // 
            // m_menuFormulaires
            // 
            this.m_menuFormulaires.Name = "m_menuSousTypes";
            this.m_menuFormulaires.Size = new System.Drawing.Size(61, 4);
            this.m_extStyle.SetStyleBackColor(this.m_menuFormulaires, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_menuFormulaires, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.m_txtExceptionRestriction);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1042, 28);
            this.m_extStyle.SetStyleBackColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel5.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 23);
            this.m_extStyle.SetStyleBackColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label9.TabIndex = 0;
            this.label9.Text = "Restriction exception context|20146";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtExceptionRestriction
            // 
            this.m_txtExceptionRestriction.Location = new System.Drawing.Point(254, 4);
            this.m_txtExceptionRestriction.Name = "m_txtExceptionRestriction";
            this.m_txtExceptionRestriction.Size = new System.Drawing.Size(248, 21);
            this.m_extStyle.SetStyleBackColor(this.m_txtExceptionRestriction, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtExceptionRestriction, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtExceptionRestriction.TabIndex = 1;
            // 
            // CFormEditionBlocWorkflowFormulaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 645);
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionBlocWorkflowFormulaire";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Form|20046";
            this.Load += new System.EventHandler(this.CFormEditionBlocWorkflowFormulaire_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnPaste)).EndInit();
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.m_pageForm.ResumeLayout(false);
            this.m_pageEndConditions.ResumeLayout(false);
            this.m_pageEndConditions.PerformLayout();
            this.m_panelInstructions.ResumeLayout(false);
            this.m_pageRestrictions.ResumeLayout(false);
            this.m_pageRestrictions.PerformLayout();
            this.m_pageAffectations.ResumeLayout(false);
            this.m_pageGestionErreur.ResumeLayout(false);
            this.m_pageStopHandler.ResumeLayout(false);
            this.m_pageStopHandler.PerformLayout();
            this.m_panelParametreDeclenchement.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleElementEdite;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton m_rbtnStandard;
        private System.Windows.Forms.RadioButton m_rbtnFormulaireSpecifique;
        private sc2i.win32.common.C2iTabControl m_tab;
        private Crownwood.Magic.Controls.TabPage m_pageEndConditions;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_wndListeFormules;
        private Crownwood.Magic.Controls.TabPage m_pageRestrictions;
        private sc2i.win32.process.workflow.bloc.CPanelEditeRestrictionsBlocWorkflowFormulaire m_panelRestrictions;
        private System.Windows.Forms.Panel m_panelInstructions;
        private System.Windows.Forms.Label label3;
        private Crownwood.Magic.Controls.TabPage m_pageAffectations;
        private sc2i.win32.process.workflow.CPanelEditeParametresInitialisationEtape m_panelAffectations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private sc2i.win32.data.dynamic.C2iTextBoxFiltreRapide m_txtSelectFormulaireSecondaire;
        private System.Windows.Forms.Label label6;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleElementSecondaire;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox m_chkHideOnChangeForm;
        private System.Windows.Forms.CheckBox m_chkSecondaireEnEdition;
        private System.Windows.Forms.LinkLabel m_lnkSelectFormulaires;
        private System.Windows.Forms.ContextMenuStrip m_menuFormulaires;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleInstructions;
        private System.Windows.Forms.CheckBox m_chkPromptToEnd;
        private System.Windows.Forms.CheckBox m_chkLockItemWhenComplete;
        private System.Windows.Forms.PictureBox m_btnCopy;
        private System.Windows.Forms.PictureBox m_btnPaste;
        private System.Windows.Forms.CheckBox m_chkMasquerApresValidation;
        private Crownwood.Magic.Controls.TabPage m_pageGestionErreur;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel m_panelGestionErreur;
        private System.Windows.Forms.CheckBox m_chkPasserSiPasErreur;
        private Crownwood.Magic.Controls.TabPage m_pageStopHandler;
        private CPanelEditParametreDeclencheur m_panelParametreDeclenchement;
        private sc2i.win32.common.C2iPanel m_panelGauche;
        private sc2i.win32.common.C2iPanel c2iPanel1;
        private System.Windows.Forms.CheckBox m_chkUseStopHandler;
        private Crownwood.Magic.Controls.TabPage m_pageForm;
        private sc2i.win32.data.dynamic.CPanelChampsCustom m_panelChamps;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox m_txtExceptionRestriction;
    }
}
