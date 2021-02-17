namespace sc2i.win32.data.dynamic
{
    partial class CFormInfosNouveauModule
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
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_ctrlEditModuleParametrage = new sc2i.win32.data.dynamic.CControlEditModuleParametrage();
            this.SuspendLayout();
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAdd.Location = new System.Drawing.Point(210, 123);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(93, 23);
            this.m_btnAdd.TabIndex = 1;
            this.m_btnAdd.Text = "Add|24";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(309, 123);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(93, 23);
            this.m_btnAnnuler.TabIndex = 2;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            // 
            // m_ctrlEditModuleParametrage
            // 
            this.m_ctrlEditModuleParametrage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctrlEditModuleParametrage.Location = new System.Drawing.Point(-1, 0);
            this.m_ctrlEditModuleParametrage.Name = "m_ctrlEditModuleParametrage";
            this.m_ctrlEditModuleParametrage.Size = new System.Drawing.Size(417, 117);
            this.m_ctrlEditModuleParametrage.TabIndex = 0;
            // 
            // CFormInfosNouveauModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(414, 158);
            this.Controls.Add(this.m_ctrlEditModuleParametrage);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CFormInfosNouveauModule";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Add a Setting Module|10002";
            this.Load += new System.EventHandler(this.CFormInfosNouveauModule_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnAdd;
        private System.Windows.Forms.Button m_btnAnnuler;
        private CControlEditModuleParametrage m_ctrlEditModuleParametrage;
    }
}