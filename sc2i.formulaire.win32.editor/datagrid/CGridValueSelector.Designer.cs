namespace sc2i.formulaire.win32.datagrid
{
    partial class CGridValueSelector
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
            this.m_wndListeItems = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_txtSearch = new sc2i.win32.common.C2iTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndListeItems
            // 
            this.m_wndListeItems.CheckBoxes = true;
            this.m_wndListeItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_wndListeItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeItems.Location = new System.Drawing.Point(0, 20);
            this.m_wndListeItems.Name = "m_wndListeItems";
            this.m_wndListeItems.Size = new System.Drawing.Size(181, 144);
            this.m_wndListeItems.TabIndex = 0;
            this.m_wndListeItems.UseCompatibleStateImageBehavior = false;
            this.m_wndListeItems.View = System.Windows.Forms.View.Details;
            this.m_wndListeItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.m_wndListeItems_ItemChecked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 24);
            this.panel1.TabIndex = 1;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 160;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_wndListeItems);
            this.panel2.Controls.Add(this.m_txtSearch);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(181, 188);
            this.panel2.TabIndex = 2;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnOk.Image = global::sc2i.formulaire.win32.Properties.Resources.MiniOk;
            this.m_btnOk.Location = new System.Drawing.Point(156, 0);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(25, 24);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_txtSearch
            // 
            this.m_txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtSearch.EmptyText = "Search|20033";
            this.m_txtSearch.Location = new System.Drawing.Point(0, 0);
            this.m_txtSearch.LockEdition = false;
            this.m_txtSearch.Name = "m_txtSearch";
            this.m_txtSearch.Size = new System.Drawing.Size(181, 20);
            this.m_txtSearch.TabIndex = 2;
            this.m_txtSearch.TextChanged += new System.EventHandler(this.m_txtSearch_TextChanged);
            // 
            // CGridValueSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "CGridValueSelector";
            this.Size = new System.Drawing.Size(181, 188);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_wndListeItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.common.C2iTextBox m_txtSearch;
    }
}
