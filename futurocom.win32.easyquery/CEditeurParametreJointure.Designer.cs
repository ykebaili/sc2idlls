namespace futurocom.win32.easyquery
{
    partial class CEditeurParametreJointure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEditeurParametreJointure));
            this.m_txtFormule1 = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_cmbOperateur = new System.Windows.Forms.ComboBox();
            this.m_txtFormule2 = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_lnkDelete = new sc2i.win32.common.CWndLinkStd();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtFormule1
            // 
            this.m_txtFormule1.AllowGraphic = true;
            this.m_txtFormule1.AllowNullFormula = false;
            this.m_txtFormule1.AllowSaisieTexte = true;
            this.m_txtFormule1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule1.Formule = null;
            this.m_txtFormule1.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule1.LockEdition = false;
            this.m_txtFormule1.LockZoneTexte = false;
            this.m_txtFormule1.Name = "m_txtFormule1";
            this.m_txtFormule1.Size = new System.Drawing.Size(203, 24);
            this.m_txtFormule1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_txtFormule1);
            this.splitContainer1.Panel1.Controls.Add(this.m_cmbOperateur);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_txtFormule2);
            this.splitContainer1.Panel2.Controls.Add(this.m_lnkDelete);
            this.splitContainer1.Size = new System.Drawing.Size(489, 24);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 1;
            // 
            // m_cmbOperateur
            // 
            this.m_cmbOperateur.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_cmbOperateur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOperateur.FormattingEnabled = true;
            this.m_cmbOperateur.Location = new System.Drawing.Point(203, 0);
            this.m_cmbOperateur.Name = "m_cmbOperateur";
            this.m_cmbOperateur.Size = new System.Drawing.Size(55, 21);
            this.m_cmbOperateur.TabIndex = 1;
            // 
            // m_txtFormule2
            // 
            this.m_txtFormule2.AllowGraphic = true;
            this.m_txtFormule2.AllowNullFormula = false;
            this.m_txtFormule2.AllowSaisieTexte = true;
            this.m_txtFormule2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule2.Formule = null;
            this.m_txtFormule2.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule2.LockEdition = false;
            this.m_txtFormule2.LockZoneTexte = false;
            this.m_txtFormule2.Name = "m_txtFormule2";
            this.m_txtFormule2.Size = new System.Drawing.Size(200, 24);
            this.m_txtFormule2.TabIndex = 1;
            // 
            // m_lnkDelete
            // 
            this.m_lnkDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDelete.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkDelete.CustomImage")));
            this.m_lnkDelete.CustomText = "Remove";
            this.m_lnkDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lnkDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDelete.Location = new System.Drawing.Point(200, 0);
            this.m_lnkDelete.Name = "m_lnkDelete";
            this.m_lnkDelete.ShortMode = false;
            this.m_lnkDelete.Size = new System.Drawing.Size(27, 24);
            this.m_lnkDelete.TabIndex = 2;
            this.m_lnkDelete.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkDelete.LinkClicked += new System.EventHandler(this.m_lnkDelete_LinkClicked);
            // 
            // CEditeurParametreJointure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CEditeurParametreJointure";
            this.Size = new System.Drawing.Size(489, 24);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox m_cmbOperateur;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule2;
        private sc2i.win32.common.CWndLinkStd m_lnkDelete;
    }
}
