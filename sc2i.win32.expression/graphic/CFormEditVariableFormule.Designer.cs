namespace sc2i.win32.expression
{
    partial class CFormEditVariableFormule
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
            this.m_txtNom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtType = new System.Windows.Forms.TextBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_chkArray = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name|20020";
            // 
            // m_txtNom
            // 
            this.m_txtNom.Location = new System.Drawing.Point(95, 10);
            this.m_txtNom.Name = "m_txtNom";
            this.m_txtNom.Size = new System.Drawing.Size(385, 20);
            this.m_txtNom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type|20021";
            // 
            // m_txtType
            // 
            this.m_txtType.Location = new System.Drawing.Point(95, 46);
            this.m_txtType.Name = "m_txtType";
            this.m_txtType.Size = new System.Drawing.Size(385, 20);
            this.m_txtType.TabIndex = 4;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(155, 92);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 5;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancel|11";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_chkArray
            // 
            this.m_chkArray.AutoSize = true;
            this.m_chkArray.Location = new System.Drawing.Point(95, 73);
            this.m_chkArray.Name = "m_chkArray";
            this.m_chkArray.Size = new System.Drawing.Size(82, 17);
            this.m_chkArray.TabIndex = 7;
            this.m_chkArray.Text = "Array|20022";
            this.m_chkArray.UseVisualStyleBackColor = true;
            // 
            // CFormEditVariableFormule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 132);
            this.ControlBox = false;
            this.Controls.Add(this.m_chkArray);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_txtType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtNom);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CFormEditVariableFormule";
            this.Text = "Edit variable|20033";
            this.Load += new System.EventHandler(this.CFormEditVariableFormule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtNom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txtType;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox m_chkArray;
    }
}