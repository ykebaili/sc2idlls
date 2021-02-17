namespace sc2i.win32.process.workflow.bloc
{
    partial class CFormEditionBlocWorkflowChoix
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionBlocWorkflowChoix));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_wndListeFormules = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_panelFormules = new sc2i.win32.common.C2iPanel(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_lblOther = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndListeFormules
            // 
            this.m_wndListeFormules.AutoScroll = true;
            this.m_wndListeFormules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_wndListeFormules.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndListeFormules.ForeColor = System.Drawing.Color.Black;
            this.m_wndListeFormules.HeaderTextForFormula = "Formula, if true associated return code will be activated)|20071";
            this.m_wndListeFormules.HeaderTextForName = "Return code|20072";
            this.m_wndListeFormules.Location = new System.Drawing.Point(0, 51);
            this.m_wndListeFormules.LockEdition = false;
            this.m_wndListeFormules.Name = "m_wndListeFormules";
            this.m_wndListeFormules.Size = new System.Drawing.Size(725, 291);
            this.m_extStyle.SetStyleBackColor(this.m_wndListeFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_wndListeFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_wndListeFormules.TabIndex = 3;
            this.m_wndListeFormules.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            this.m_wndListeFormules.Load += new System.EventHandler(this.m_wndListeFormules_Load);
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormules.LockEdition = false;
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(725, 381);
            this.m_extStyle.SetStyleBackColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormules.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(725, 51);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "A conditionnal branch return a list of return code. Each code is returned by the " +
                "step if its condition value is true|20073";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 381);
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
            // m_lblOther
            // 
            this.m_lblOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblOther.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblOther.Location = new System.Drawing.Point(0, 0);
            this.m_lblOther.Name = "m_lblOther";
            this.m_lblOther.Size = new System.Drawing.Size(160, 39);
            this.m_extStyle.SetStyleBackColor(this.m_lblOther, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lblOther, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblOther.TabIndex = 0;
            this.m_lblOther.Text = "OTHER";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.m_lblOther);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 342);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(725, 39);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(160, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(565, 39);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 1;
            this.label3.Text = "This code is activated if no formula returns true|20073";
            // 
            // CFormEditionBlocWorkflowChoix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 429);
            this.Controls.Add(this.m_wndListeFormules);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_panelFormules);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionBlocWorkflowChoix";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Conditionnal branch|20068";
            this.Load += new System.EventHandler(this.CFormEditionBlocWorkflowChoix_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_wndListeFormules;
        private sc2i.win32.common.C2iPanel m_panelFormules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label m_lblOther;
        private System.Windows.Forms.Label label3;
    }
}