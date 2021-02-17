namespace sc2i.win32.data
{
    partial class CControlEditeParametreDeFiltreData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControlEditeParametreDeFiltreData));
            this.m_cmbType = new sc2i.win32.common.C2iComboBox();
            this.m_lblNumParametre = new System.Windows.Forms.Label();
            this.m_checkBox = new System.Windows.Forms.CheckBox();
            this.m_txtBox = new sc2i.win32.common.C2iTextBox();
            this.m_txtBoxInt = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_dtPicker = new sc2i.win32.common.C2iDateTimePicker();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_txtBoxDouble = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_btnDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // m_cmbType
            // 
            this.m_cmbType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.FormattingEnabled = true;
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(31, 0);
            this.m_cmbType.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbType, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(116, 21);
            this.m_cmbType.TabIndex = 0;
            this.m_cmbType.SelectionChangeCommitted += new System.EventHandler(this.m_cmbType_SelectionChangeCommitted);
            // 
            // m_lblNumParametre
            // 
            this.m_lblNumParametre.BackColor = System.Drawing.Color.White;
            this.m_lblNumParametre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblNumParametre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNumParametre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblNumParametre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblNumParametre.Name = "m_lblNumParametre";
            this.m_lblNumParametre.Size = new System.Drawing.Size(31, 20);
            this.m_lblNumParametre.TabIndex = 1;
            this.m_lblNumParametre.Text = "@1";
            // 
            // m_checkBox
            // 
            this.m_checkBox.AutoSize = true;
            this.m_checkBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_checkBox.Location = new System.Drawing.Point(147, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_checkBox, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_checkBox.Name = "m_checkBox";
            this.m_checkBox.Size = new System.Drawing.Size(15, 20);
            this.m_checkBox.TabIndex = 2;
            this.m_checkBox.UseVisualStyleBackColor = true;
            // 
            // m_txtBox
            // 
            this.m_txtBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtBox.Location = new System.Drawing.Point(162, 0);
            this.m_txtBox.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtBox, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtBox.Name = "m_txtBox";
            this.m_txtBox.Size = new System.Drawing.Size(41, 20);
            this.m_txtBox.TabIndex = 3;
            // 
            // m_txtBoxInt
            // 
            this.m_txtBoxInt.Arrondi = 0;
            this.m_txtBoxInt.DecimalAutorise = false;
            this.m_txtBoxInt.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtBoxInt.IntValue = 0;
            this.m_txtBoxInt.Location = new System.Drawing.Point(203, 0);
            this.m_txtBoxInt.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtBoxInt, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtBoxInt.Name = "m_txtBoxInt";
            this.m_txtBoxInt.NullAutorise = false;
            this.m_txtBoxInt.SelectAllOnEnter = true;
            this.m_txtBoxInt.Size = new System.Drawing.Size(25, 20);
            this.m_txtBoxInt.TabIndex = 4;
            this.m_txtBoxInt.Text = "0";
            // 
            // m_dtPicker
            // 
            this.m_dtPicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_dtPicker.Location = new System.Drawing.Point(228, 0);
            this.m_dtPicker.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_dtPicker, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_dtPicker.Name = "m_dtPicker";
            this.m_dtPicker.Size = new System.Drawing.Size(84, 20);
            this.m_dtPicker.TabIndex = 5;
            this.m_dtPicker.Value = new System.DateTime(2009, 6, 11, 15, 34, 40, 14);
            // 
            // m_txtBoxDouble
            // 
            this.m_txtBoxDouble.Arrondi = 20;
            this.m_txtBoxDouble.DecimalAutorise = true;
            this.m_txtBoxDouble.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtBoxDouble.IntValue = 0;
            this.m_txtBoxDouble.Location = new System.Drawing.Point(312, 0);
            this.m_txtBoxDouble.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtBoxDouble, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtBoxDouble.Name = "m_txtBoxDouble";
            this.m_txtBoxDouble.NullAutorise = false;
            this.m_txtBoxDouble.SelectAllOnEnter = true;
            this.m_txtBoxDouble.Size = new System.Drawing.Size(25, 20);
            this.m_txtBoxDouble.TabIndex = 6;
            this.m_txtBoxDouble.Text = "0";
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("m_btnDelete.Image")));
            this.m_btnDelete.Location = new System.Drawing.Point(375, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDelete, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(19, 20);
            this.m_btnDelete.TabIndex = 7;
            this.m_btnDelete.TabStop = false;
            this.m_btnDelete.Click += new System.EventHandler(this.m_btnDelete_Click);
            // 
            // CControlEditeParametreDeFiltreData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_txtBoxDouble);
            this.Controls.Add(this.m_dtPicker);
            this.Controls.Add(this.m_txtBoxInt);
            this.Controls.Add(this.m_txtBox);
            this.Controls.Add(this.m_checkBox);
            this.Controls.Add(this.m_cmbType);
            this.Controls.Add(this.m_lblNumParametre);
            this.Controls.Add(this.m_btnDelete);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlEditeParametreDeFiltreData";
            this.Size = new System.Drawing.Size(394, 20);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private sc2i.win32.common.C2iComboBox m_cmbType;
        private System.Windows.Forms.Label m_lblNumParametre;
        private System.Windows.Forms.CheckBox m_checkBox;
        private sc2i.win32.common.C2iTextBox m_txtBox;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtBoxInt;
        private sc2i.win32.common.C2iDateTimePicker m_dtPicker;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtBoxDouble;
        private System.Windows.Forms.PictureBox m_btnDelete;
    }
}
