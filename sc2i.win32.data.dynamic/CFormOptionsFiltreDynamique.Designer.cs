namespace sc2i.win32.data.dynamic
{
    partial class CFormOptionsFiltreDynamique
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_txtFormuleIncludeParents = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_txtFormuleIncludeChilds = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_txtFormuleRootOnly = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Include parent elements|20037";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Include child elements|20053";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Root elements only|20060";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(191, 223);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 1;
            this.m_btnOk.Text = "OK|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Location = new System.Drawing.Point(299, 223);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "Cancel|2";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_txtFormuleIncludeParents
            // 
            this.m_txtFormuleIncludeParents.AllowGraphic = true;
            this.m_txtFormuleIncludeParents.AllowSaisieTexte = true;
            this.m_txtFormuleIncludeParents.Formule = null;
            this.m_txtFormuleIncludeParents.Location = new System.Drawing.Point(78, 26);
            this.m_txtFormuleIncludeParents.LockEdition = false;
            this.m_txtFormuleIncludeParents.LockZoneTexte = false;
            this.m_txtFormuleIncludeParents.Name = "m_txtFormuleIncludeParents";
            this.m_txtFormuleIncludeParents.Size = new System.Drawing.Size(475, 42);
            this.m_txtFormuleIncludeParents.TabIndex = 2;
            // 
            // m_txtFormuleIncludeChilds
            // 
            this.m_txtFormuleIncludeChilds.AllowGraphic = true;
            this.m_txtFormuleIncludeChilds.AllowSaisieTexte = true;
            this.m_txtFormuleIncludeChilds.Formule = null;
            this.m_txtFormuleIncludeChilds.Location = new System.Drawing.Point(78, 97);
            this.m_txtFormuleIncludeChilds.LockEdition = false;
            this.m_txtFormuleIncludeChilds.LockZoneTexte = false;
            this.m_txtFormuleIncludeChilds.Name = "m_txtFormuleIncludeChilds";
            this.m_txtFormuleIncludeChilds.Size = new System.Drawing.Size(475, 42);
            this.m_txtFormuleIncludeChilds.TabIndex = 3;
            // 
            // m_txtFormuleRootOnly
            // 
            this.m_txtFormuleRootOnly.AllowGraphic = true;
            this.m_txtFormuleRootOnly.AllowSaisieTexte = true;
            this.m_txtFormuleRootOnly.Formule = null;
            this.m_txtFormuleRootOnly.Location = new System.Drawing.Point(78, 170);
            this.m_txtFormuleRootOnly.LockEdition = false;
            this.m_txtFormuleRootOnly.LockZoneTexte = false;
            this.m_txtFormuleRootOnly.Name = "m_txtFormuleRootOnly";
            this.m_txtFormuleRootOnly.Size = new System.Drawing.Size(475, 42);
            this.m_txtFormuleRootOnly.TabIndex = 4;
            // 
            // CFormOptionsFiltreDynamique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 260);
            this.ControlBox = false;
            this.Controls.Add(this.m_txtFormuleRootOnly);
            this.Controls.Add(this.m_txtFormuleIncludeChilds);
            this.Controls.Add(this.m_txtFormuleIncludeParents);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CFormOptionsFiltreDynamique";
            this.Text = "Filter options|20054";
            this.Load += new System.EventHandler(this.CFormOptionsFiltreDynamique_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleIncludeParents;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleIncludeChilds;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleRootOnly;
    }
}