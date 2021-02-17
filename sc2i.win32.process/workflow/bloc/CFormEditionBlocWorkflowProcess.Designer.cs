namespace sc2i.win32.process.workflow.bloc
{
    partial class CFormEditionBlocWorkflowProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionBlocWorkflowProcess));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_chkManualStart = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtSelectProcess = new sc2i.win32.data.CComboBoxListeObjetsDonnees();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageProcess = new Crownwood.Magic.Controls.TabPage();
            this.m_processEditor = new sc2i.win32.process.CProcessEditor();
            this.m_pageEndConditions = new Crownwood.Magic.Controls.TabPage();
            this.m_panelInstructions = new System.Windows.Forms.Panel();
            this.m_txtInstructions = new sc2i.win32.common.C2iTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_pageAffectations = new Crownwood.Magic.Controls.TabPage();
            this.m_panelAffectations = new sc2i.win32.process.workflow.CPanelEditeParametresInitialisationEtape();
            this.m_chkUtiliserSortieProcessCommeCodeRetour = new System.Windows.Forms.CheckBox();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_tab.SuspendLayout();
            this.m_pageProcess.SuspendLayout();
            this.m_pageEndConditions.SuspendLayout();
            this.m_panelInstructions.SuspendLayout();
            this.m_pageAffectations.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_chkUtiliserSortieProcessCommeCodeRetour);
            this.c2iPanelOmbre1.Controls.Add(this.m_chkManualStart);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtSelectProcess);
            this.c2iPanelOmbre1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(911, 72);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 3;
            // 
            // m_chkManualStart
            // 
            this.m_chkManualStart.AutoSize = true;
            this.m_chkManualStart.Location = new System.Drawing.Point(189, 38);
            this.m_chkManualStart.Name = "m_chkManualStart";
            this.m_chkManualStart.Size = new System.Drawing.Size(116, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkManualStart, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkManualStart, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkManualStart.TabIndex = 4;
            this.m_chkManualStart.Text = "Manual start|20078";
            this.m_chkManualStart.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 3;
            this.label2.Text = "Process to launch|20077";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtSelectProcess
            // 
            this.m_txtSelectProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_txtSelectProcess.ElementSelectionne = null;
            this.m_txtSelectProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtSelectProcess.FormattingEnabled = true;
            this.m_txtSelectProcess.IsLink = false;
            this.m_txtSelectProcess.ListDonnees = null;
            this.m_txtSelectProcess.Location = new System.Drawing.Point(189, 8);
            this.m_txtSelectProcess.LockEdition = false;
            this.m_txtSelectProcess.Name = "m_txtSelectProcess";
            this.m_txtSelectProcess.NullAutorise = true;
            this.m_txtSelectProcess.ProprieteAffichee = null;
            this.m_txtSelectProcess.ProprieteParentListeObjets = null;
            this.m_txtSelectProcess.SelectionneurParent = null;
            this.m_txtSelectProcess.Size = new System.Drawing.Size(696, 21);
            this.m_extStyle.SetStyleBackColor(this.m_txtSelectProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtSelectProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSelectProcess.TabIndex = 5;
            this.m_txtSelectProcess.TextNull = "(empty)";
            this.m_txtSelectProcess.Tri = true;
            this.m_txtSelectProcess.SelectionChangeCommitted += new System.EventHandler(this.m_txtSelectProcess_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 537);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 48);
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
            this.m_btnAnnuler.Location = new System.Drawing.Point(462, 2);
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
            this.m_btnOk.Location = new System.Drawing.Point(408, 2);
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
            this.m_tab.Location = new System.Drawing.Point(0, 72);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = true;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 0;
            this.m_tab.SelectedTab = this.m_pageProcess;
            this.m_tab.Size = new System.Drawing.Size(911, 465);
            this.m_extStyle.SetStyleBackColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tab.TabIndex = 5;
            this.m_tab.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_pageProcess,
            this.m_pageEndConditions,
            this.m_pageAffectations});
            this.m_tab.TextColor = System.Drawing.Color.Black;
            // 
            // m_pageProcess
            // 
            this.m_pageProcess.Controls.Add(this.m_processEditor);
            this.m_pageProcess.Location = new System.Drawing.Point(0, 25);
            this.m_pageProcess.Name = "m_pageProcess";
            this.m_pageProcess.Size = new System.Drawing.Size(895, 424);
            this.m_extStyle.SetStyleBackColor(this.m_pageProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageProcess.TabIndex = 13;
            this.m_pageProcess.Title = "Specific process|20079";
            // 
            // m_processEditor
            // 
            this.m_processEditor.BackColor = System.Drawing.Color.White;
            this.m_processEditor.DisableTypeElement = true;
            this.m_processEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_processEditor.ForEvent = false;
            this.m_processEditor.Location = new System.Drawing.Point(0, 0);
            this.m_processEditor.LockEdition = false;
            this.m_processEditor.Name = "m_processEditor";
            this.m_processEditor.Process = null;
            this.m_processEditor.Size = new System.Drawing.Size(895, 424);
            this.m_extStyle.SetStyleBackColor(this.m_processEditor, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_processEditor, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_processEditor.TabIndex = 0;
            // 
            // m_pageEndConditions
            // 
            this.m_pageEndConditions.Controls.Add(this.m_panelInstructions);
            this.m_pageEndConditions.Location = new System.Drawing.Point(0, 25);
            this.m_pageEndConditions.Name = "m_pageEndConditions";
            this.m_pageEndConditions.Selected = false;
            this.m_pageEndConditions.Size = new System.Drawing.Size(895, 424);
            this.m_extStyle.SetStyleBackColor(this.m_pageEndConditions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageEndConditions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageEndConditions.TabIndex = 10;
            this.m_pageEndConditions.Title = "Instruction|20064";
            // 
            // m_panelInstructions
            // 
            this.m_panelInstructions.Controls.Add(this.m_txtInstructions);
            this.m_panelInstructions.Controls.Add(this.label3);
            this.m_panelInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelInstructions.Location = new System.Drawing.Point(0, 0);
            this.m_panelInstructions.Name = "m_panelInstructions";
            this.m_panelInstructions.Size = new System.Drawing.Size(895, 65);
            this.m_extStyle.SetStyleBackColor(this.m_panelInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelInstructions.TabIndex = 1;
            // 
            // m_txtInstructions
            // 
            this.m_txtInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInstructions.EmptyText = "";
            this.m_txtInstructions.Location = new System.Drawing.Point(189, 3);
            this.m_txtInstructions.LockEdition = false;
            this.m_txtInstructions.Multiline = true;
            this.m_txtInstructions.Name = "m_txtInstructions";
            this.m_txtInstructions.Size = new System.Drawing.Size(699, 56);
            this.m_extStyle.SetStyleBackColor(this.m_txtInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtInstructions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtInstructions.TabIndex = 2;
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
            // m_pageAffectations
            // 
            this.m_pageAffectations.Controls.Add(this.m_panelAffectations);
            this.m_pageAffectations.Location = new System.Drawing.Point(0, 25);
            this.m_pageAffectations.Name = "m_pageAffectations";
            this.m_pageAffectations.Selected = false;
            this.m_pageAffectations.Size = new System.Drawing.Size(895, 424);
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
            this.m_panelAffectations.Size = new System.Drawing.Size(895, 424);
            this.m_extStyle.SetStyleBackColor(this.m_panelAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelAffectations, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelAffectations.TabIndex = 0;
            // 
            // m_chkUtiliserSortieProcessCommeCodeRetour
            // 
            this.m_chkUtiliserSortieProcessCommeCodeRetour.AutoSize = true;
            this.m_chkUtiliserSortieProcessCommeCodeRetour.Location = new System.Drawing.Point(329, 38);
            this.m_chkUtiliserSortieProcessCommeCodeRetour.Name = "m_chkUtiliserSortieProcessCommeCodeRetour";
            this.m_chkUtiliserSortieProcessCommeCodeRetour.Size = new System.Drawing.Size(244, 17);
            this.m_extStyle.SetStyleBackColor(this.m_chkUtiliserSortieProcessCommeCodeRetour, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkUtiliserSortieProcessCommeCodeRetour, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUtiliserSortieProcessCommeCodeRetour.TabIndex = 1;
            this.m_chkUtiliserSortieProcessCommeCodeRetour.Text = "Use process output as step return code|20122";
            this.m_chkUtiliserSortieProcessCommeCodeRetour.UseVisualStyleBackColor = true;
            // 
            // CFormEditionBlocWorkflowProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 585);
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionBlocWorkflowProcess";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Form|20076";
            this.Load += new System.EventHandler(this.CFormEditionBlocWorkflowProcess_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.m_pageProcess.ResumeLayout(false);
            this.m_pageEndConditions.ResumeLayout(false);
            this.m_panelInstructions.ResumeLayout(false);
            this.m_panelInstructions.PerformLayout();
            this.m_pageAffectations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.C2iTabControl m_tab;
        private Crownwood.Magic.Controls.TabPage m_pageEndConditions;
        private System.Windows.Forms.Panel m_panelInstructions;
        private sc2i.win32.common.C2iTextBox m_txtInstructions;
        private System.Windows.Forms.Label label3;
        private Crownwood.Magic.Controls.TabPage m_pageAffectations;
        private sc2i.win32.process.workflow.CPanelEditeParametresInitialisationEtape m_panelAffectations;
        private System.Windows.Forms.CheckBox m_chkManualStart;
        private sc2i.win32.data.CComboBoxListeObjetsDonnees m_txtSelectProcess;
        private Crownwood.Magic.Controls.TabPage m_pageProcess;
        private CProcessEditor m_processEditor;
        private System.Windows.Forms.CheckBox m_chkUtiliserSortieProcessCommeCodeRetour;
    }
}