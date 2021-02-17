namespace sc2i.win32.process.workflow.bloc
{
    partial class CFormEditionBlocWorkflowStopStep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionBlocWorkflowStopStep));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelFormules = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel1 = new sc2i.win32.common.C2iPanel(this.components);
            this.m_cmbTypeEtape = new sc2i.win32.data.CComboBoxListeObjetsDonnees();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_rbtnTerminer = new System.Windows.Forms.RadioButton();
            this.m_rbtnAnnuler = new System.Windows.Forms.RadioButton();
            this.m_panelFormules.SuspendLayout();
            this.c2iPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Controls.Add(this.c2iPanel1);
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormules.LockEdition = false;
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(725, 112);
            this.m_extStyle.SetStyleBackColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormules.TabIndex = 2;
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.c2iPanel1.Controls.Add(this.panel2);
            this.c2iPanel1.Controls.Add(this.label3);
            this.c2iPanel1.Controls.Add(this.m_cmbTypeEtape);
            this.c2iPanel1.Controls.Add(this.label2);
            this.c2iPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanel1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel1.LockEdition = false;
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(725, 112);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel1.TabIndex = 0;
            this.c2iPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.c2iPanel1_Paint);
            // 
            // m_cmbTypeEtape
            // 
            this.m_cmbTypeEtape.DisplayMember = "Label";
            this.m_cmbTypeEtape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeEtape.ElementSelectionne = null;
            this.m_cmbTypeEtape.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeEtape.FormattingEnabled = true;
            this.m_cmbTypeEtape.IsLink = false;
            this.m_cmbTypeEtape.ListDonnees = null;
            this.m_cmbTypeEtape.Location = new System.Drawing.Point(138, 43);
            this.m_cmbTypeEtape.LockEdition = false;
            this.m_cmbTypeEtape.Name = "m_cmbTypeEtape";
            this.m_cmbTypeEtape.NullAutorise = false;
            this.m_cmbTypeEtape.ProprieteAffichee = null;
            this.m_cmbTypeEtape.ProprieteParentListeObjets = null;
            this.m_cmbTypeEtape.SelectionneurParent = null;
            this.m_cmbTypeEtape.Size = new System.Drawing.Size(566, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbTypeEtape, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbTypeEtape, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeEtape.TabIndex = 1;
            this.m_cmbTypeEtape.TextNull = "(empty)";
            this.m_cmbTypeEtape.Tri = true;
            this.m_cmbTypeEtape.ValueMember = "Label";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 0;
            this.label2.Text = "Step|20132";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(725, 30);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select step to stop|20101";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 112);
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
            this.label3.Location = new System.Drawing.Point(13, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 22);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 2;
            this.label3.Text = "Action|20131";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_rbtnAnnuler);
            this.panel2.Controls.Add(this.m_rbtnTerminer);
            this.panel2.Location = new System.Drawing.Point(138, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(566, 48);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 3;
            // 
            // m_rbtnTerminer
            // 
            this.m_rbtnTerminer.AutoSize = true;
            this.m_rbtnTerminer.Location = new System.Drawing.Point(19, 7);
            this.m_rbtnTerminer.Name = "m_rbtnTerminer";
            this.m_rbtnTerminer.Size = new System.Drawing.Size(99, 17);
            this.m_extStyle.SetStyleBackColor(this.m_rbtnTerminer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_rbtnTerminer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_rbtnTerminer.TabIndex = 0;
            this.m_rbtnTerminer.TabStop = true;
            this.m_rbtnTerminer.Text = "End step|20133";
            this.m_rbtnTerminer.UseVisualStyleBackColor = true;
            // 
            // m_rbtnAnnuler
            // 
            this.m_rbtnAnnuler.AutoSize = true;
            this.m_rbtnAnnuler.Location = new System.Drawing.Point(19, 25);
            this.m_rbtnAnnuler.Name = "m_rbtnAnnuler";
            this.m_rbtnAnnuler.Size = new System.Drawing.Size(113, 17);
            this.m_extStyle.SetStyleBackColor(this.m_rbtnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_rbtnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_rbtnAnnuler.TabIndex = 1;
            this.m_rbtnAnnuler.TabStop = true;
            this.m_rbtnAnnuler.Text = "Cancel step|20134";
            this.m_rbtnAnnuler.UseVisualStyleBackColor = true;
            // 
            // CFormEditionBlocWorkflowStopStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 160);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_panelFormules);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionBlocWorkflowStopStep";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Stop or cancel workflow step|20130";
            this.Load += new System.EventHandler(this.CFormEditionBlocWorkflowStopStep_Load);
            this.m_panelFormules.ResumeLayout(false);
            this.c2iPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.C2iPanel m_panelFormules;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iPanel c2iPanel1;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.data.CComboBoxListeObjetsDonnees m_cmbTypeEtape;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton m_rbtnAnnuler;
        private System.Windows.Forms.RadioButton m_rbtnTerminer;
    }
}