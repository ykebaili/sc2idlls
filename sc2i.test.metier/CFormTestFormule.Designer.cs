namespace sc2i.test.metier
{
    partial class CFormTestFormule
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
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_btnEval = new System.Windows.Forms.Button();
            this.m_txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(12, 12);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(669, 77);
            this.m_txtFormule.TabIndex = 0;
            // 
            // m_btnEval
            // 
            this.m_btnEval.Location = new System.Drawing.Point(290, 96);
            this.m_btnEval.Name = "m_btnEval";
            this.m_btnEval.Size = new System.Drawing.Size(75, 23);
            this.m_btnEval.TabIndex = 1;
            this.m_btnEval.Text = "button1";
            this.m_btnEval.UseVisualStyleBackColor = true;
            this.m_btnEval.Click += new System.EventHandler(this.m_btnEval_Click);
            // 
            // m_txtResult
            // 
            this.m_txtResult.Location = new System.Drawing.Point(12, 124);
            this.m_txtResult.Multiline = true;
            this.m_txtResult.Name = "m_txtResult";
            this.m_txtResult.Size = new System.Drawing.Size(669, 123);
            this.m_txtResult.TabIndex = 2;
            // 
            // CFormTestFormule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 259);
            this.Controls.Add(this.m_txtResult);
            this.Controls.Add(this.m_btnEval);
            this.Controls.Add(this.m_txtFormule);
            this.Name = "CFormTestFormule";
            this.Text = "CFormTestFormule";
            this.Load += new System.EventHandler(this.CFormTestFormule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Button m_btnEval;
        private System.Windows.Forms.TextBox m_txtResult;
    }
}