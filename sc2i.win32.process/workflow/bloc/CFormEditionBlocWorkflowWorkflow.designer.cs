namespace sc2i.win32.process.workflow.bloc
{
    partial class CFormEditionBlocWorkflowWorkflow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionBlocWorkflowWorkflow));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_cmbStartStep = new sc2i.win32.data.CComboBoxListeObjetsDonnees();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtSelectWorkflow = new sc2i.win32.data.dynamic.C2iTextBoxFiltreRapide();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtWorkflowInit = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "Workflow|20061";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_txtWorkflowInit);
            this.c2iPanelOmbre1.Controls.Add(this.label3);
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbStartStep);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtSelectWorkflow);
            this.c2iPanelOmbre1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(725, 165);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 3;
            // 
            // m_cmbStartStep
            // 
            this.m_cmbStartStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbStartStep.ElementSelectionne = null;
            this.m_cmbStartStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbStartStep.FormattingEnabled = true;
            this.m_cmbStartStep.IsLink = false;
            this.m_cmbStartStep.ListDonnees = null;
            this.m_cmbStartStep.Location = new System.Drawing.Point(189, 33);
            this.m_cmbStartStep.LockEdition = false;
            this.m_cmbStartStep.Name = "m_cmbStartStep";
            this.m_cmbStartStep.NullAutorise = false;
            this.m_cmbStartStep.ProprieteAffichee = null;
            this.m_cmbStartStep.ProprieteParentListeObjets = null;
            this.m_cmbStartStep.SelectionneurParent = null;
            this.m_cmbStartStep.Size = new System.Drawing.Size(502, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbStartStep, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbStartStep, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbStartStep.TabIndex = 4;
            this.m_cmbStartStep.TextNull = "(empty)";
            this.m_cmbStartStep.Tri = true;
            this.m_cmbStartStep.SelectionChangeCommitted += new System.EventHandler(this.m_cmbStartStep_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start step|20075";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtSelectWorkflow
            // 
            this.m_txtSelectWorkflow.ElementSelectionne = null;
            this.m_txtSelectWorkflow.FonctionTextNull = null;
            this.m_txtSelectWorkflow.ImageDisplayMode = sc2i.win32.data.dynamic.EModeAffichageImageTextBoxRapide.Always;
            this.m_txtSelectWorkflow.Location = new System.Drawing.Point(189, 4);
            this.m_txtSelectWorkflow.LockEdition = false;
            this.m_txtSelectWorkflow.Name = "m_txtSelectWorkflow";
            this.m_txtSelectWorkflow.SelectedObject = null;
            this.m_txtSelectWorkflow.SelectionLength = 0;
            this.m_txtSelectWorkflow.SelectionStart = 0;
            this.m_txtSelectWorkflow.Size = new System.Drawing.Size(502, 25);
            this.m_txtSelectWorkflow.SpecificImage = null;
            this.m_extStyle.SetStyleBackColor(this.m_txtSelectWorkflow, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtSelectWorkflow, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSelectWorkflow.TabIndex = 1;
            this.m_txtSelectWorkflow.TextNull = "";
            this.m_txtSelectWorkflow.UseIntellisense = true;
            this.m_txtSelectWorkflow.Load += new System.EventHandler(this.CFormEditionBlocWorkflowWorkflow_Load);
            this.m_txtSelectWorkflow.ElementSelectionneChanged += new System.EventHandler(this.m_txtSelectWorkflow_ElementSelectionneChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 4;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(369, 2);
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
            this.m_btnOk.Location = new System.Drawing.Point(315, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 5;
            this.label3.Text = "Workflow intialization";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtWorkflowInit
            // 
            this.m_txtWorkflowInit.AllowGraphic = true;
            this.m_txtWorkflowInit.AllowNullFormula = false;
            this.m_txtWorkflowInit.AllowSaisieTexte = true;
            this.m_txtWorkflowInit.Formule = null;
            this.m_txtWorkflowInit.Location = new System.Drawing.Point(189, 63);
            this.m_txtWorkflowInit.LockEdition = false;
            this.m_txtWorkflowInit.LockZoneTexte = false;
            this.m_txtWorkflowInit.Name = "m_txtWorkflowInit";
            this.m_txtWorkflowInit.Size = new System.Drawing.Size(502, 73);
            this.m_extStyle.SetStyleBackColor(this.m_txtWorkflowInit, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtWorkflowInit, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtWorkflowInit.TabIndex = 6;
            // 
            // CFormEditionBlocWorkflowWorkflow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 210);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionBlocWorkflowWorkflow";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Workflow|20061";
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.data.dynamic.C2iTextBoxFiltreRapide m_txtSelectWorkflow;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.data.CComboBoxListeObjetsDonnees m_cmbStartStep;
        private System.Windows.Forms.Label label3;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtWorkflowInit;
    }
}