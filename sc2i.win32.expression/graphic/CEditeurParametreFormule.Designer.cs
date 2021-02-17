namespace sc2i.win32.expression
{
    partial class CEditeurParametreFormule
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
            this.components = new System.ComponentModel.Container();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_chkVisible = new System.Windows.Forms.CheckBox();
            this.m_lblNomParametre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(203, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(177, 23);
            this.m_txtFormule.TabIndex = 0;
            this.m_txtFormule.DragDrop += new System.Windows.Forms.DragEventHandler(this.CEditeurParametreFormule_DragDrop);
            this.m_txtFormule.OnChangeTexteFormule += new System.EventHandler(this.m_txtFormule_OnChangeTexteFormule);
            this.m_txtFormule.DragOver += new System.Windows.Forms.DragEventHandler(this.CEditeurParametreFormule_DragOver);
            // 
            // m_chkVisible
            // 
            this.m_chkVisible.AutoSize = true;
            this.m_chkVisible.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_chkVisible.Location = new System.Drawing.Point(380, 0);
            this.m_chkVisible.Name = "m_chkVisible";
            this.m_chkVisible.Size = new System.Drawing.Size(56, 23);
            this.m_chkVisible.TabIndex = 1;
            this.m_chkVisible.Text = "Visible";
            this.m_chkVisible.UseVisualStyleBackColor = true;
            this.m_chkVisible.CheckedChanged += new System.EventHandler(this.m_chkVisible_CheckedChanged);
            // 
            // m_lblNomParametre
            // 
            this.m_lblNomParametre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNomParametre.Location = new System.Drawing.Point(0, 0);
            this.m_lblNomParametre.Name = "m_lblNomParametre";
            this.m_lblNomParametre.Size = new System.Drawing.Size(203, 23);
            this.m_lblNomParametre.TabIndex = 2;
            this.m_lblNomParametre.Text = "label1";
            this.m_lblNomParametre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 1);
            this.label1.TabIndex = 3;
            // 
            // CEditeurParametreFormule
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_chkVisible);
            this.Controls.Add(this.m_lblNomParametre);
            this.Controls.Add(this.label1);
            this.Name = "CEditeurParametreFormule";
            this.Size = new System.Drawing.Size(436, 24);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.CEditeurParametreFormule_DragOver);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CEditeurParametreFormule_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CEditeurParametreFormule_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.CheckBox m_chkVisible;
        private System.Windows.Forms.Label m_lblNomParametre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip m_toolTip;
    }
}
