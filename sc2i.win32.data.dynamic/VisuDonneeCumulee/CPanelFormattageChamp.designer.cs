namespace sc2i.win32.data.dynamic
{
    partial class CPanelFormattageChamp
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_chkBackColor = new System.Windows.Forms.CheckBox();
            this.m_colorBack = new sc2i.win32.common.C2iColorSelect();
            this.m_chkForeColor = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_colorFore = new sc2i.win32.common.C2iColorSelect();
            this.m_txtFontSize = new sc2i.win32.common.C2iTextBoxNumerique();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_cmbFont = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_cmbTextAlign = new System.Windows.Forms.ComboBox();
            this.m_btnFormuleSize = new System.Windows.Forms.Button();
            this.m_btnFormuleFore = new System.Windows.Forms.Button();
            this.m_btnFormuleBack = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_chkBold = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_btnFormuleBold = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtColumnWidth = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_panelWidth = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_colorSelected = new sc2i.win32.common.C2iColorSelect();
            this.m_chkSelectedColor = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelWidth.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_chkBackColor
            // 
            this.m_chkBackColor.AutoSize = true;
            this.m_chkBackColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkBackColor.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkBackColor, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkBackColor.Name = "m_chkBackColor";
            this.m_chkBackColor.Size = new System.Drawing.Size(15, 22);
            this.m_chkBackColor.TabIndex = 22;
            this.m_chkBackColor.UseVisualStyleBackColor = true;
            this.m_chkBackColor.CheckedChanged += new System.EventHandler(this.m_chkBackColor_CheckedChanged);
            // 
            // m_colorBack
            // 
            this.m_colorBack.BackColor = System.Drawing.Color.White;
            this.m_colorBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_colorBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_colorBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_colorBack.Location = new System.Drawing.Point(15, 0);
            this.m_colorBack.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_colorBack, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_colorBack.Name = "m_colorBack";
            this.m_colorBack.SelectedColor = System.Drawing.Color.White;
            this.m_colorBack.Size = new System.Drawing.Size(33, 22);
            this.m_colorBack.TabIndex = 21;
            // 
            // m_chkForeColor
            // 
            this.m_chkForeColor.AutoSize = true;
            this.m_chkForeColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkForeColor.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkForeColor, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkForeColor.Name = "m_chkForeColor";
            this.m_chkForeColor.Size = new System.Drawing.Size(15, 22);
            this.m_chkForeColor.TabIndex = 20;
            this.m_chkForeColor.UseVisualStyleBackColor = true;
            this.m_chkForeColor.CheckedChanged += new System.EventHandler(this.m_chkForeColor_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(163, 26);
            this.m_extModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Back color|10030";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.m_extModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Fore color|10031";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_colorFore
            // 
            this.m_colorFore.BackColor = System.Drawing.Color.White;
            this.m_colorFore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_colorFore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_colorFore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_colorFore.Location = new System.Drawing.Point(15, 0);
            this.m_colorFore.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_colorFore, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_colorFore.Name = "m_colorFore";
            this.m_colorFore.SelectedColor = System.Drawing.Color.White;
            this.m_colorFore.Size = new System.Drawing.Size(33, 22);
            this.m_colorFore.TabIndex = 17;
            // 
            // m_txtFontSize
            // 
            this.m_txtFontSize.Arrondi = 0;
            this.m_txtFontSize.DecimalAutorise = true;
            this.m_txtFontSize.DoubleValue = null;
            this.m_txtFontSize.EmptyText = "";
            this.m_txtFontSize.IntValue = null;
            this.m_txtFontSize.Location = new System.Drawing.Point(361, 0);
            this.m_txtFontSize.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtFontSize, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFontSize.Name = "m_txtFontSize";
            this.m_txtFontSize.NullAutorise = true;
            this.m_txtFontSize.SelectAllOnEnter = true;
            this.m_txtFontSize.SeparateurMilliers = "";
            this.m_txtFontSize.Size = new System.Drawing.Size(31, 20);
            this.m_txtFontSize.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(290, 1);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "size|20013";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Font|20014";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cmbFont
            // 
            this.m_cmbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbFont.FormattingEnabled = true;
            this.m_cmbFont.Location = new System.Drawing.Point(77, 0);
            this.m_extModeEdition.SetModeEdition(this.m_cmbFont, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbFont.Name = "m_cmbFont";
            this.m_cmbFont.Size = new System.Drawing.Size(204, 21);
            this.m_cmbFont.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 52);
            this.m_extModeEdition.SetModeEdition(this.label5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Text align|20015";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cmbTextAlign
            // 
            this.m_cmbTextAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTextAlign.FormattingEnabled = true;
            this.m_cmbTextAlign.Location = new System.Drawing.Point(77, 51);
            this.m_extModeEdition.SetModeEdition(this.m_cmbTextAlign, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbTextAlign.Name = "m_cmbTextAlign";
            this.m_cmbTextAlign.Size = new System.Drawing.Size(154, 21);
            this.m_cmbTextAlign.TabIndex = 25;
            // 
            // m_btnFormuleSize
            // 
            this.m_btnFormuleSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnFormuleSize.Location = new System.Drawing.Point(392, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnFormuleSize, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFormuleSize.Name = "m_btnFormuleSize";
            this.m_btnFormuleSize.Size = new System.Drawing.Size(31, 21);
            this.m_btnFormuleSize.TabIndex = 27;
            this.m_btnFormuleSize.Text = "f(x)";
            this.m_btnFormuleSize.UseVisualStyleBackColor = true;
            this.m_btnFormuleSize.Click += new System.EventHandler(this.m_btnFormuleSize_Click);
            // 
            // m_btnFormuleFore
            // 
            this.m_btnFormuleFore.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnFormuleFore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnFormuleFore.Location = new System.Drawing.Point(48, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnFormuleFore, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFormuleFore.Name = "m_btnFormuleFore";
            this.m_btnFormuleFore.Size = new System.Drawing.Size(31, 22);
            this.m_btnFormuleFore.TabIndex = 28;
            this.m_btnFormuleFore.Text = "f(x)";
            this.m_btnFormuleFore.UseVisualStyleBackColor = true;
            this.m_btnFormuleFore.Click += new System.EventHandler(this.m_btnFormuleFore_Click);
            // 
            // m_btnFormuleBack
            // 
            this.m_btnFormuleBack.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnFormuleBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnFormuleBack.Location = new System.Drawing.Point(48, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnFormuleBack, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFormuleBack.Name = "m_btnFormuleBack";
            this.m_btnFormuleBack.Size = new System.Drawing.Size(31, 22);
            this.m_btnFormuleBack.TabIndex = 29;
            this.m_btnFormuleBack.Text = "f(x)";
            this.m_btnFormuleBack.UseVisualStyleBackColor = true;
            this.m_btnFormuleBack.Click += new System.EventHandler(this.m_btnFormuleBack_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_colorBack);
            this.panel1.Controls.Add(this.m_btnFormuleBack);
            this.panel1.Controls.Add(this.m_chkBackColor);
            this.panel1.Location = new System.Drawing.Point(235, 25);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 22);
            this.panel1.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_colorFore);
            this.panel2.Controls.Add(this.m_chkForeColor);
            this.panel2.Controls.Add(this.m_btnFormuleFore);
            this.panel2.Location = new System.Drawing.Point(77, 26);
            this.m_extModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(79, 22);
            this.panel2.TabIndex = 31;
            // 
            // m_chkBold
            // 
            this.m_chkBold.AutoSize = true;
            this.m_chkBold.Location = new System.Drawing.Point(505, 5);
            this.m_extModeEdition.SetModeEdition(this.m_chkBold, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkBold.Name = "m_chkBold";
            this.m_chkBold.Size = new System.Drawing.Size(15, 14);
            this.m_chkBold.TabIndex = 32;
            this.m_chkBold.ThreeState = true;
            this.m_chkBold.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(435, 1);
            this.m_extModeEdition.SetModeEdition(this.label6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "Bold|20016";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_btnFormuleBold
            // 
            this.m_btnFormuleBold.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnFormuleBold.Location = new System.Drawing.Point(520, 1);
            this.m_extModeEdition.SetModeEdition(this.m_btnFormuleBold, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFormuleBold.Name = "m_btnFormuleBold";
            this.m_btnFormuleBold.Size = new System.Drawing.Size(31, 21);
            this.m_btnFormuleBold.TabIndex = 34;
            this.m_btnFormuleBold.Text = "f(x)";
            this.m_btnFormuleBold.UseVisualStyleBackColor = true;
            this.m_btnFormuleBold.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.m_extModeEdition.SetModeEdition(this.label7, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "Column width|10032";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtColumnWidth
            // 
            this.m_txtColumnWidth.Arrondi = 0;
            this.m_txtColumnWidth.DecimalAutorise = true;
            this.m_txtColumnWidth.DoubleValue = null;
            this.m_txtColumnWidth.EmptyText = "";
            this.m_txtColumnWidth.IntValue = null;
            this.m_txtColumnWidth.Location = new System.Drawing.Point(127, 0);
            this.m_txtColumnWidth.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtColumnWidth, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtColumnWidth.Name = "m_txtColumnWidth";
            this.m_txtColumnWidth.NullAutorise = true;
            this.m_txtColumnWidth.SelectAllOnEnter = true;
            this.m_txtColumnWidth.SeparateurMilliers = "";
            this.m_txtColumnWidth.Size = new System.Drawing.Size(31, 20);
            this.m_txtColumnWidth.TabIndex = 36;
            // 
            // m_panelWidth
            // 
            this.m_panelWidth.Controls.Add(this.label7);
            this.m_panelWidth.Controls.Add(this.m_txtColumnWidth);
            this.m_panelWidth.Location = new System.Drawing.Point(237, 53);
            this.m_extModeEdition.SetModeEdition(this.m_panelWidth, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelWidth.Name = "m_panelWidth";
            this.m_panelWidth.Size = new System.Drawing.Size(171, 30);
            this.m_panelWidth.TabIndex = 37;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_colorSelected);
            this.panel3.Controls.Add(this.m_chkSelectedColor);
            this.panel3.Location = new System.Drawing.Point(420, 24);
            this.m_extModeEdition.SetModeEdition(this.panel3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(47, 22);
            this.panel3.TabIndex = 30;
            // 
            // m_colorSelected
            // 
            this.m_colorSelected.BackColor = System.Drawing.Color.White;
            this.m_colorSelected.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_colorSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_colorSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_colorSelected.Location = new System.Drawing.Point(15, 0);
            this.m_colorSelected.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_colorSelected, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_colorSelected.Name = "m_colorSelected";
            this.m_colorSelected.SelectedColor = System.Drawing.Color.White;
            this.m_colorSelected.Size = new System.Drawing.Size(32, 22);
            this.m_colorSelected.TabIndex = 21;
            // 
            // m_chkSelectedColor
            // 
            this.m_chkSelectedColor.AutoSize = true;
            this.m_chkSelectedColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkSelectedColor.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkSelectedColor, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkSelectedColor.Name = "m_chkSelectedColor";
            this.m_chkSelectedColor.Size = new System.Drawing.Size(15, 22);
            this.m_chkSelectedColor.TabIndex = 22;
            this.m_chkSelectedColor.UseVisualStyleBackColor = true;
            this.m_chkSelectedColor.CheckedChanged += new System.EventHandler(this.m_chkSelectedColor_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(320, 25);
            this.m_extModeEdition.SetModeEdition(this.label8, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 20);
            this.label8.TabIndex = 19;
            this.label8.Text = "Selected|20089";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CPanelFormattageChamp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelWidth);
            this.Controls.Add(this.m_chkBold);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_btnFormuleSize);
            this.Controls.Add(this.m_cmbTextAlign);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_cmbFont);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtFontSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnFormuleBold);
            this.Controls.Add(this.label6);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelFormattageChamp";
            this.Size = new System.Drawing.Size(564, 85);
            this.Load += new System.EventHandler(this.CPanelFormattageChamp_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_panelWidth.ResumeLayout(false);
            this.m_panelWidth.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_chkBackColor;
        private sc2i.win32.common.C2iColorSelect m_colorBack;
        private System.Windows.Forms.CheckBox m_chkForeColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private sc2i.win32.common.C2iColorSelect m_colorFore;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtFontSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.ComboBox m_cmbFont;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox m_cmbTextAlign;
        private System.Windows.Forms.Button m_btnFormuleSize;
        private System.Windows.Forms.Button m_btnFormuleFore;
        private System.Windows.Forms.Button m_btnFormuleBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox m_chkBold;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button m_btnFormuleBold;
        private System.Windows.Forms.Label label7;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtColumnWidth;
        private System.Windows.Forms.Panel m_panelWidth;
        private System.Windows.Forms.Panel panel3;
        private sc2i.win32.common.C2iColorSelect m_colorSelected;
        private System.Windows.Forms.CheckBox m_chkSelectedColor;
        private System.Windows.Forms.Label label8;
    }
}
