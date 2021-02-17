namespace data.hotel.easyquery.win32.calcul
{
    partial class CEditeurCalculHotelDuration
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
            this.m_cmbChampSource = new sc2i.win32.common.CComboboxAutoFilled();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_panelFiltre = new data.hotel.eastquery.win32.filtre.CPanelFiltreDataHotel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmbChampSource
            // 
            this.m_cmbChampSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbChampSource.FormattingEnabled = true;
            this.m_cmbChampSource.IsLink = false;
            this.m_cmbChampSource.ListDonnees = null;
            this.m_cmbChampSource.Location = new System.Drawing.Point(132, 3);
            this.m_cmbChampSource.LockEdition = false;
            this.m_cmbChampSource.Name = "m_cmbChampSource";
            this.m_cmbChampSource.NullAutorise = false;
            this.m_cmbChampSource.ProprieteAffichee = null;
            this.m_cmbChampSource.Size = new System.Drawing.Size(209, 21);
            this.m_cmbChampSource.TabIndex = 4;
            this.m_cmbChampSource.TextNull = "(empty)";
            this.m_cmbChampSource.Tri = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Source field|20039";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_cmbChampSource);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 33);
            this.panel1.TabIndex = 5;
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.BackColor = System.Drawing.Color.White;
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.ForeColor = System.Drawing.Color.Black;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 57);
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(347, 110);
            this.m_panelFiltre.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(347, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Filter|20036";
            // 
            // CEditeurCalculHotelDuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelFiltre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Name = "CEditeurCalculHotelDuration";
            this.Size = new System.Drawing.Size(347, 167);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CComboboxAutoFilled m_cmbChampSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private eastquery.win32.filtre.CPanelFiltreDataHotel m_panelFiltre;
        private System.Windows.Forms.Label label4;
    }
}
