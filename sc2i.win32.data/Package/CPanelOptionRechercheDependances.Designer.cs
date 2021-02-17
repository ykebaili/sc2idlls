namespace sc2i.win32.data.Package
{
    partial class CPanelOptionRechercheDependances
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
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtNbRecMax = new sc2i.win32.common.C2iTextBoxNumerique();
            this.label2 = new System.Windows.Forms.Label();
            this.m_wndListeTypes = new sc2i.win32.common.customizableList.CCustomizableList();
            this.m_panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.label2);
            this.m_panelTop.Controls.Add(this.m_txtNbRecMax);
            this.m_panelTop.Controls.Add(this.label1);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(451, 19);
            this.m_panelTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Don\'t search reference if type contains more than|20005";
            // 
            // m_txtNbRecMax
            // 
            this.m_txtNbRecMax.Arrondi = 0;
            this.m_txtNbRecMax.DecimalAutorise = false;
            this.m_txtNbRecMax.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtNbRecMax.EmptyText = "";
            this.m_txtNbRecMax.IntValue = 0;
            this.m_txtNbRecMax.Location = new System.Drawing.Point(271, 0);
            this.m_txtNbRecMax.LockEdition = false;
            this.m_txtNbRecMax.Name = "m_txtNbRecMax";
            this.m_txtNbRecMax.NullAutorise = false;
            this.m_txtNbRecMax.SelectAllOnEnter = true;
            this.m_txtNbRecMax.SeparateurMilliers = "";
            this.m_txtNbRecMax.Size = new System.Drawing.Size(100, 20);
            this.m_txtNbRecMax.TabIndex = 1;
            this.m_txtNbRecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(371, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "items|20006";
            // 
            // m_wndListeTypes
            // 
            this.m_wndListeTypes.CurrentItemIndex = null;
            this.m_wndListeTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeTypes.ItemControl = null;
            this.m_wndListeTypes.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_wndListeTypes.Location = new System.Drawing.Point(0, 19);
            this.m_wndListeTypes.LockEdition = false;
            this.m_wndListeTypes.Name = "m_wndListeTypes";
            this.m_wndListeTypes.Size = new System.Drawing.Size(451, 349);
            this.m_wndListeTypes.TabIndex = 1;
            // 
            // CPanelOptionRechercheDependances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_wndListeTypes);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CPanelOptionRechercheDependances";
            this.Size = new System.Drawing.Size(451, 368);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Label label1;
        private common.C2iTextBoxNumerique m_txtNbRecMax;
        private System.Windows.Forms.Label label2;
        private common.customizableList.CCustomizableList m_wndListeTypes;
    }
}
