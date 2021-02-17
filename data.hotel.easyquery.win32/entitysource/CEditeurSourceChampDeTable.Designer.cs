namespace data.hotel.easyquery.win32.entitysource
{
    partial class CEditeurSourceChampDeTable
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cmbTable = new sc2i.win32.common.C2iComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelChamp = new System.Windows.Forms.Panel();
            this.m_cmbColonne = new sc2i.win32.common.C2iComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.m_panelChamp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmbTable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 21);
            this.panel1.TabIndex = 0;
            // 
            // m_cmbTable
            // 
            this.m_cmbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTable.FormattingEnabled = true;
            this.m_cmbTable.IsLink = false;
            this.m_cmbTable.Location = new System.Drawing.Point(117, 0);
            this.m_cmbTable.LockEdition = false;
            this.m_cmbTable.Name = "m_cmbTable";
            this.m_cmbTable.Size = new System.Drawing.Size(143, 21);
            this.m_cmbTable.TabIndex = 1;
            this.m_cmbTable.SelectionChangeCommitted += new System.EventHandler(this.m_cmbTable_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source table|20039";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_panelChamp
            // 
            this.m_panelChamp.Controls.Add(this.m_cmbColonne);
            this.m_panelChamp.Controls.Add(this.label2);
            this.m_panelChamp.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelChamp.Location = new System.Drawing.Point(0, 21);
            this.m_panelChamp.Name = "m_panelChamp";
            this.m_panelChamp.Size = new System.Drawing.Size(260, 22);
            this.m_panelChamp.TabIndex = 1;
            // 
            // m_cmbColonne
            // 
            this.m_cmbColonne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbColonne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbColonne.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbColonne.FormattingEnabled = true;
            this.m_cmbColonne.IsLink = false;
            this.m_cmbColonne.Location = new System.Drawing.Point(117, 0);
            this.m_cmbColonne.LockEdition = false;
            this.m_cmbColonne.Name = "m_cmbColonne";
            this.m_cmbColonne.Size = new System.Drawing.Size(143, 21);
            this.m_cmbColonne.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Source column|20040";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CEditeurSourceChampDeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelChamp);
            this.Controls.Add(this.panel1);
            this.Name = "CEditeurSourceChampDeTable";
            this.Size = new System.Drawing.Size(260, 55);
            this.panel1.ResumeLayout(false);
            this.m_panelChamp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel m_panelChamp;
        private sc2i.win32.common.C2iComboBox m_cmbTable;
        private sc2i.win32.common.C2iComboBox m_cmbColonne;
        private System.Windows.Forms.Label label2;

    }
}
