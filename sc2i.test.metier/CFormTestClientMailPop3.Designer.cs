namespace sc2i.test.metier
{
    partial class CFormTestClientMailPop3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormTestClientMailPop3));
            sc2i.win32.common.GLColumn glColumn5 = new sc2i.win32.common.GLColumn();
            sc2i.win32.common.GLColumn glColumn6 = new sc2i.win32.common.GLColumn();
            sc2i.win32.common.GLColumn glColumn7 = new sc2i.win32.common.GLColumn();
            sc2i.win32.common.GLColumn glColumn8 = new sc2i.win32.common.GLColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_chkSSL = new System.Windows.Forms.CheckBox();
            this.m_numPort = new System.Windows.Forms.NumericUpDown();
            this.m_txtPass = new System.Windows.Forms.TextBox();
            this.m_btnRetrieveList = new System.Windows.Forms.Button();
            this.m_txtUser = new System.Windows.Forms.TextBox();
            this.m_txtServeur = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_glListeMails = new sc2i.win32.common.GlacialList();
            this.m_webBrowser = new System.Windows.Forms.WebBrowser();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkPièceJointe = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_lblTo = new System.Windows.Forms.Label();
            this.m_lblFrom = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_txtBody = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numPort)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_chkSSL);
            this.groupBox1.Controls.Add(this.m_numPort);
            this.groupBox1.Controls.Add(this.m_txtPass);
            this.groupBox1.Controls.Add(this.m_btnRetrieveList);
            this.groupBox1.Controls.Add(this.m_txtUser);
            this.groupBox1.Controls.Add(this.m_txtServeur);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(810, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paramètres de Compte MAIL POP3";
            // 
            // m_chkSSL
            // 
            this.m_chkSSL.AutoSize = true;
            this.m_chkSSL.Location = new System.Drawing.Point(343, 60);
            this.m_chkSSL.Name = "m_chkSSL";
            this.m_chkSSL.Size = new System.Drawing.Size(46, 17);
            this.m_chkSSL.TabIndex = 3;
            this.m_chkSSL.Text = "SSL";
            this.m_chkSSL.UseVisualStyleBackColor = true;
            // 
            // m_numPort
            // 
            this.m_numPort.Location = new System.Drawing.Point(518, 29);
            this.m_numPort.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numPort.Name = "m_numPort";
            this.m_numPort.Size = new System.Drawing.Size(85, 20);
            this.m_numPort.TabIndex = 2;
            this.m_numPort.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            // 
            // m_txtPass
            // 
            this.m_txtPass.Location = new System.Drawing.Point(126, 93);
            this.m_txtPass.Name = "m_txtPass";
            this.m_txtPass.Size = new System.Drawing.Size(158, 20);
            this.m_txtPass.TabIndex = 1;
            this.m_txtPass.Text = "yk8989";
            // 
            // m_btnRetrieveList
            // 
            this.m_btnRetrieveList.Location = new System.Drawing.Point(353, 91);
            this.m_btnRetrieveList.Name = "m_btnRetrieveList";
            this.m_btnRetrieveList.Size = new System.Drawing.Size(250, 23);
            this.m_btnRetrieveList.TabIndex = 1;
            this.m_btnRetrieveList.Text = "Recevoir la liste";
            this.m_btnRetrieveList.UseVisualStyleBackColor = true;
            this.m_btnRetrieveList.Click += new System.EventHandler(this.m_btnRetrieveList_Click);
            // 
            // m_txtUser
            // 
            this.m_txtUser.Location = new System.Drawing.Point(126, 58);
            this.m_txtUser.Name = "m_txtUser";
            this.m_txtUser.Size = new System.Drawing.Size(158, 20);
            this.m_txtUser.TabIndex = 1;
            this.m_txtUser.Text = "ykebaili@futurocom.com";
            // 
            // m_txtServeur
            // 
            this.m_txtServeur.Location = new System.Drawing.Point(126, 27);
            this.m_txtServeur.Name = "m_txtServeur";
            this.m_txtServeur.Size = new System.Drawing.Size(298, 20);
            this.m_txtServeur.TabIndex = 1;
            this.m_txtServeur.Text = "pop.futurocom.com";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(485, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "POP Serveur";
            // 
            // m_glListeMails
            // 
            this.m_glListeMails.AllowColumnResize = true;
            this.m_glListeMails.AllowMultiselect = false;
            this.m_glListeMails.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_glListeMails.AlternatingColors = false;
            this.m_glListeMails.AutoHeight = true;
            this.m_glListeMails.AutoSort = true;
            this.m_glListeMails.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_glListeMails.CanChangeActivationCheckBoxes = false;
            this.m_glListeMails.CheckBoxes = false;
            this.m_glListeMails.CheckedItems = ((System.Collections.ArrayList)(resources.GetObject("m_glListeMails.CheckedItems")));
            glColumn5.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn5.ActiveControlItems")));
            glColumn5.BackColor = System.Drawing.Color.Transparent;
            glColumn5.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn5.ForColor = System.Drawing.Color.Black;
            glColumn5.ImageIndex = -1;
            glColumn5.IsCheckColumn = false;
            glColumn5.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn5.Name = "Column1";
            glColumn5.Propriete = "MessageId";
            glColumn5.Text = "Identifiant du message";
            glColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn5.Width = 200;
            glColumn6.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn6.ActiveControlItems")));
            glColumn6.BackColor = System.Drawing.Color.Transparent;
            glColumn6.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn6.ForColor = System.Drawing.Color.Black;
            glColumn6.ImageIndex = -1;
            glColumn6.IsCheckColumn = false;
            glColumn6.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn6.Name = "Column2";
            glColumn6.Propriete = "Subject";
            glColumn6.Text = "Sujet";
            glColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn6.Width = 200;
            glColumn7.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn7.ActiveControlItems")));
            glColumn7.BackColor = System.Drawing.Color.Transparent;
            glColumn7.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn7.ForColor = System.Drawing.Color.Black;
            glColumn7.ImageIndex = -1;
            glColumn7.IsCheckColumn = false;
            glColumn7.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn7.Name = "Column3";
            glColumn7.Propriete = "Attachments.Count";
            glColumn7.Text = "Nb pièces jointes";
            glColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn7.Width = 100;
            glColumn8.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn8.ActiveControlItems")));
            glColumn8.BackColor = System.Drawing.Color.Transparent;
            glColumn8.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn8.ForColor = System.Drawing.Color.Black;
            glColumn8.ImageIndex = -1;
            glColumn8.IsCheckColumn = false;
            glColumn8.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn8.Name = "Column4";
            glColumn8.Propriete = "Octets";
            glColumn8.Text = "Octets";
            glColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn8.Width = 100;
            this.m_glListeMails.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn5,
            glColumn6,
            glColumn7,
            glColumn8});
            this.m_glListeMails.ContexteUtilisation = "";
            this.m_glListeMails.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_glListeMails.EnableCustomisation = true;
            this.m_glListeMails.FocusedItem = null;
            this.m_glListeMails.FullRowSelect = true;
            this.m_glListeMails.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_glListeMails.GridColor = System.Drawing.SystemColors.ControlLight;
            this.m_glListeMails.HasImages = false;
            this.m_glListeMails.HeaderHeight = 22;
            this.m_glListeMails.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_glListeMails.HeaderTextColor = System.Drawing.Color.Black;
            this.m_glListeMails.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_glListeMails.HeaderVisible = true;
            this.m_glListeMails.HeaderWordWrap = false;
            this.m_glListeMails.HotColumnIndex = -1;
            this.m_glListeMails.HotItemIndex = -1;
            this.m_glListeMails.HotTracking = false;
            this.m_glListeMails.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_glListeMails.ImageList = null;
            this.m_glListeMails.ItemHeight = 17;
            this.m_glListeMails.ItemWordWrap = false;
            this.m_glListeMails.ListeSource = null;
            this.m_glListeMails.Location = new System.Drawing.Point(0, 134);
            this.m_glListeMails.MaxHeight = 17;
            this.m_glListeMails.Name = "m_glListeMails";
            this.m_glListeMails.SelectedTextColor = System.Drawing.Color.White;
            this.m_glListeMails.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_glListeMails.ShowBorder = true;
            this.m_glListeMails.ShowFocusRect = true;
            this.m_glListeMails.Size = new System.Drawing.Size(810, 153);
            this.m_glListeMails.SortIndex = 0;
            this.m_glListeMails.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_glListeMails.TabIndex = 3;
            this.m_glListeMails.Text = "glacialList1";
            this.m_glListeMails.TrierAuClicSurEnteteColonne = true;
            this.m_glListeMails.DoubleClick += new System.EventHandler(this.m_glListeMails_DoubleClick);
            // 
            // m_webBrowser
            // 
            this.m_webBrowser.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_webBrowser.Location = new System.Drawing.Point(0, 376);
            this.m_webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_webBrowser.Name = "m_webBrowser";
            this.m_webBrowser.Size = new System.Drawing.Size(409, 158);
            this.m_webBrowser.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 287);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(810, 5);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkPièceJointe);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.m_lblDate);
            this.panel1.Controls.Add(this.m_lblTo);
            this.panel1.Controls.Add(this.m_lblFrom);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 292);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 84);
            this.panel1.TabIndex = 6;
            // 
            // m_lnkPièceJointe
            // 
            this.m_lnkPièceJointe.Location = new System.Drawing.Point(475, 57);
            this.m_lnkPièceJointe.Name = "m_lnkPièceJointe";
            this.m_lnkPièceJointe.Size = new System.Drawing.Size(323, 19);
            this.m_lnkPièceJointe.TabIndex = 1;
            this.m_lnkPièceJointe.TabStop = true;
            this.m_lnkPièceJointe.Text = "première pièce jointe";
            this.m_lnkPièceJointe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkPièceJointe_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "To";
            // 
            // m_lblDate
            // 
            this.m_lblDate.BackColor = System.Drawing.Color.White;
            this.m_lblDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblDate.Location = new System.Drawing.Point(70, 53);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(214, 22);
            this.m_lblDate.TabIndex = 0;
            // 
            // m_lblTo
            // 
            this.m_lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTo.BackColor = System.Drawing.Color.White;
            this.m_lblTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblTo.Location = new System.Drawing.Point(70, 29);
            this.m_lblTo.Name = "m_lblTo";
            this.m_lblTo.Size = new System.Drawing.Size(728, 22);
            this.m_lblTo.TabIndex = 0;
            // 
            // m_lblFrom
            // 
            this.m_lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblFrom.BackColor = System.Drawing.Color.White;
            this.m_lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblFrom.Location = new System.Drawing.Point(70, 5);
            this.m_lblFrom.Name = "m_lblFrom";
            this.m_lblFrom.Size = new System.Drawing.Size(728, 22);
            this.m_lblFrom.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "From ";
            // 
            // splitter2
            // 
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter2.Location = new System.Drawing.Point(409, 376);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 158);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // m_txtBody
            // 
            this.m_txtBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtBody.Location = new System.Drawing.Point(419, 376);
            this.m_txtBody.Multiline = true;
            this.m_txtBody.Name = "m_txtBody";
            this.m_txtBody.Size = new System.Drawing.Size(391, 158);
            this.m_txtBody.TabIndex = 8;
            // 
            // CFormTestClientMailPop3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 534);
            this.Controls.Add(this.m_txtBody);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.m_webBrowser);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_glListeMails);
            this.Controls.Add(this.groupBox1);
            this.Name = "CFormTestClientMailPop3";
            this.Text = "CFormTestClientMailPop3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CFormTestClientMailPop3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox m_txtServeur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_numPort;
        private System.Windows.Forms.TextBox m_txtPass;
        private System.Windows.Forms.TextBox m_txtUser;
        private System.Windows.Forms.Button m_btnRetrieveList;
        private System.Windows.Forms.CheckBox m_chkSSL;
        private sc2i.win32.common.GlacialList m_glListeMails;
        private System.Windows.Forms.WebBrowser m_webBrowser;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label m_lblFrom;
        private System.Windows.Forms.LinkLabel m_lnkPièceJointe;
        private System.Windows.Forms.Label m_lblDate;
        private System.Windows.Forms.Label m_lblTo;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TextBox m_txtBody;
    }
}