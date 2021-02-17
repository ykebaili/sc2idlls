namespace futurocom.win32.easyquery.CAML
{
    partial class CFormEditeComposantCAMLNull
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_txtFormuleCondition = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.m_comboChamp = new sc2i.win32.common.CComboboxAutoFilled();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_rbtnEstNull = new System.Windows.Forms.RadioButton();
            this.m_rbtnIsNotNull = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field|20036";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 35);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 3;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Location = new System.Drawing.Point(252, 6);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 7;
            this.m_btnAnnuler.Text = "Cancel|2";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(157, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.m_txtFormuleCondition);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_comboChamp);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(464, 133);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel2.TabIndex = 4;
            // 
            // m_txtFormuleCondition
            // 
            this.m_txtFormuleCondition.AllowGraphic = false;
            this.m_txtFormuleCondition.AllowNullFormula = true;
            this.m_txtFormuleCondition.AllowSaisieTexte = true;
            this.m_txtFormuleCondition.Formule = null;
            this.m_txtFormuleCondition.Location = new System.Drawing.Point(109, 94);
            this.m_txtFormuleCondition.LockEdition = false;
            this.m_txtFormuleCondition.LockZoneTexte = false;
            this.m_txtFormuleCondition.Name = "m_txtFormuleCondition";
            this.m_txtFormuleCondition.Size = new System.Drawing.Size(343, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleCondition.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 18);
            this.m_extStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 6;
            this.label4.Text = "Condition|20039";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_comboChamp
            // 
            this.m_comboChamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_comboChamp.FormattingEnabled = true;
            this.m_comboChamp.IsLink = false;
            this.m_comboChamp.ListDonnees = null;
            this.m_comboChamp.Location = new System.Drawing.Point(109, 6);
            this.m_comboChamp.LockEdition = false;
            this.m_comboChamp.Name = "m_comboChamp";
            this.m_comboChamp.NullAutorise = false;
            this.m_comboChamp.ProprieteAffichee = null;
            this.m_comboChamp.Size = new System.Drawing.Size(343, 21);
            this.m_extStyle.SetStyleBackColor(this.m_comboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_comboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_comboChamp.TabIndex = 1;
            this.m_comboChamp.Text = "(empty)";
            this.m_comboChamp.TextNull = "(empty)";
            this.m_comboChamp.Tri = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_rbtnIsNotNull);
            this.panel3.Controls.Add(this.m_rbtnEstNull);
            this.panel3.Location = new System.Drawing.Point(109, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(343, 25);
            this.m_extStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 8;
            // 
            // m_rbtnEstNull
            // 
            this.m_rbtnEstNull.AutoSize = true;
            this.m_rbtnEstNull.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnEstNull.Location = new System.Drawing.Point(0, 0);
            this.m_rbtnEstNull.Name = "m_rbtnEstNull";
            this.m_rbtnEstNull.Size = new System.Drawing.Size(84, 25);
            this.m_extStyle.SetStyleBackColor(this.m_rbtnEstNull, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_rbtnEstNull, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_rbtnEstNull.TabIndex = 0;
            this.m_rbtnEstNull.TabStop = true;
            this.m_rbtnEstNull.Text = "Is null|20044";
            this.m_rbtnEstNull.UseVisualStyleBackColor = true;
            // 
            // m_rbtnIsNotNull
            // 
            this.m_rbtnIsNotNull.AutoSize = true;
            this.m_rbtnIsNotNull.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnIsNotNull.Location = new System.Drawing.Point(84, 0);
            this.m_rbtnIsNotNull.Name = "m_rbtnIsNotNull";
            this.m_rbtnIsNotNull.Size = new System.Drawing.Size(102, 25);
            this.m_extStyle.SetStyleBackColor(this.m_rbtnIsNotNull, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_rbtnIsNotNull, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_rbtnIsNotNull.TabIndex = 1;
            this.m_rbtnIsNotNull.TabStop = true;
            this.m_rbtnIsNotNull.Text = "Is not null|20045";
            this.m_rbtnIsNotNull.UseVisualStyleBackColor = true;
            // 
            // CFormEditeComposantCAMLNull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 168);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CFormEditeComposantCAMLNull";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Nullity test|20046";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleCondition;
        private System.Windows.Forms.Label label4;
        private sc2i.win32.common.CComboboxAutoFilled m_comboChamp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton m_rbtnIsNotNull;
        private System.Windows.Forms.RadioButton m_rbtnEstNull;
    }
}