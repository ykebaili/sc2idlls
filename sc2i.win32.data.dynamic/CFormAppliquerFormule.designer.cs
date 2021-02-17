namespace sc2i.win32.data.dynamic
{
    partial class CFormAppliquerFormule
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
            if (m_contexteModification != null)
            {
                m_contexteModification.Dispose();
                m_contexteModification = null;
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnAppliquer = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_lblApplyTo = new System.Windows.Forms.Label();
            this.m_txtResult = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtFormule);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 125);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 0;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(534, 125);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormule.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.m_btnOk);
            this.panel2.Controls.Add(this.m_btnCancel);
            this.panel2.Controls.Add(this.m_btnAppliquer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(534, 33);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel2.TabIndex = 0;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnCancel.Location = new System.Drawing.Point(456, 6);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "Cancel|11";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnAppliquer
            // 
            this.m_btnAppliquer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAppliquer.Enabled = true;
            this.m_btnAppliquer.Location = new System.Drawing.Point(4, 7);
            this.m_btnAppliquer.Name = "m_btnAppliquer";
            this.m_btnAppliquer.Size = new System.Drawing.Size(174, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAppliquer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAppliquer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAppliquer.TabIndex = 2;
            this.m_btnAppliquer.Text = "Apply formula|20094";
            this.m_btnAppliquer.UseVisualStyleBackColor = true;
            this.m_btnAppliquer.Click += new System.EventHandler(this.m_btnAppliquer_Click);
            // 
            // m_lblApplyTo
            // 
            this.m_lblApplyTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_lblApplyTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblApplyTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblApplyTo.ForeColor = System.Drawing.Color.Black;
            this.m_lblApplyTo.Location = new System.Drawing.Point(0, 0);
            this.m_lblApplyTo.Name = "m_lblApplyTo";
            this.m_lblApplyTo.Size = new System.Drawing.Size(534, 19);
            this.cExtStyle1.SetStyleBackColor(this.m_lblApplyTo, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_lblApplyTo, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_lblApplyTo.TabIndex = 1;
            // 
            // m_txtResult
            // 
            this.m_txtResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_txtResult.Location = new System.Drawing.Point(0, 180);
            this.m_txtResult.Multiline = true;
            this.m_txtResult.Name = "m_txtResult";
            this.m_txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtResult.Size = new System.Drawing.Size(534, 111);
            this.cExtStyle1.SetStyleBackColor(this.m_txtResult, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtResult, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtResult.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 177);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(534, 3);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.Enabled = false;
            this.m_btnOk.Location = new System.Drawing.Point(375, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormAppliquerFormule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 291);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_lblApplyTo);
            this.Controls.Add(this.m_txtResult);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormAppliquerFormule";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Apply formulas|20091";
            this.Load += new System.EventHandler(this.CFormRunFormula_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnAppliquer;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Label m_lblApplyTo;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox m_txtResult;
        private System.Windows.Forms.Button m_btnOk;
    }
}