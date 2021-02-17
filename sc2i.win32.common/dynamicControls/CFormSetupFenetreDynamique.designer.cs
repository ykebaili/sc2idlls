namespace sc2i.win32.common.dynamicControls
{
    partial class CFormSetupFenetreDynamique
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormSetupFenetreDynamique));
            this.m_arbreControles = new System.Windows.Forms.TreeView();
            this.m_timerClignoteSel = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_btnReset = new System.Windows.Forms.Button();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbreControles
            // 
            this.m_arbreControles.CheckBoxes = true;
            this.m_arbreControles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreControles.HideSelection = false;
            this.m_arbreControles.Location = new System.Drawing.Point(0, 0);
            this.m_arbreControles.Name = "m_arbreControles";
            this.m_arbreControles.Size = new System.Drawing.Size(478, 452);
            this.m_arbreControles.TabIndex = 0;
            this.m_arbreControles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreControles_AfterCheck);
            this.m_arbreControles.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbreControles_BeforeExpand);
            this.m_arbreControles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreControles_AfterSelect);
            this.m_arbreControles.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_arbreControles_BeforeCheck);
            // 
            // m_timerClignoteSel
            // 
            this.m_timerClignoteSel.Enabled = true;
            this.m_timerClignoteSel.Interval = 500;
            this.m_timerClignoteSel.Tick += new System.EventHandler(this.m_timerClignoteSel_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "vide 16x16.bmp");
            this.imageList1.Images.SetKeyName(1, "Shell32 132.ico");
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.m_btnReset);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 452);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(478, 37);
            this.m_panelBas.TabIndex = 1;
            // 
            // m_btnReset
            // 
            this.m_btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnReset.Location = new System.Drawing.Point(400, 6);
            this.m_btnReset.Name = "m_btnReset";
            this.m_btnReset.Size = new System.Drawing.Size(75, 23);
            this.m_btnReset.TabIndex = 0;
            this.m_btnReset.Text = "Reset";
            this.m_btnReset.UseVisualStyleBackColor = true;
            this.m_btnReset.Click += new System.EventHandler(this.m_btnReset_Click);
            // 
            // CFormSetupFenetreDynamique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 489);
            this.Controls.Add(this.m_arbreControles);
            this.Controls.Add(this.m_panelBas);
            this.Name = "CFormSetupFenetreDynamique";
            this.Text = "Customize window";
            this.Load += new System.EventHandler(this.CFormSetupFenetreDynamique_Load);
            this.m_panelBas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbreControles;
        private System.Windows.Forms.Timer m_timerClignoteSel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel m_panelBas;
        private System.Windows.Forms.Button m_btnReset;
    }
}