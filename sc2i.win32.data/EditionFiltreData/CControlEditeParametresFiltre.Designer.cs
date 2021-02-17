namespace sc2i.win32.data.EditionFiltreData
{
    partial class CControlEditeParametresFiltre
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
            this.m_lblNumParametre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelTop = new sc2i.win32.common.C2iPanel(this.components);
            this.m_panelControls = new sc2i.win32.common.C2iPanel(this.components);
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelAddDelete = new sc2i.win32.common.C2iPanel(this.components);
            this.m_btnAdd = new sc2i.win32.common.CWndLinkStd();
            this.m_panelTop.SuspendLayout();
            this.m_panelAddDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblNumParametre
            // 
            this.m_lblNumParametre.BackColor = System.Drawing.Color.White;
            this.m_lblNumParametre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblNumParametre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNumParametre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblNumParametre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblNumParametre.Name = "m_lblNumParametre";
            this.m_lblNumParametre.Size = new System.Drawing.Size(31, 21);
            this.m_lblNumParametre.TabIndex = 2;
            this.m_lblNumParametre.Text = "n°|20000";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(31, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type|20001";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(147, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value|20002";
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.label2);
            this.m_panelTop.Controls.Add(this.label1);
            this.m_panelTop.Controls.Add(this.m_lblNumParametre);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 17);
            this.m_panelTop.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTop, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(361, 21);
            this.m_panelTop.TabIndex = 5;
            // 
            // m_panelControls
            // 
            this.m_panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelControls.Location = new System.Drawing.Point(0, 38);
            this.m_panelControls.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelControls, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelControls.Name = "m_panelControls";
            this.m_panelControls.Size = new System.Drawing.Size(361, 100);
            this.m_panelControls.TabIndex = 6;
            // 
            // m_panelAddDelete
            // 
            this.m_panelAddDelete.Controls.Add(this.m_btnAdd);
            this.m_panelAddDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelAddDelete.Location = new System.Drawing.Point(0, 0);
            this.m_panelAddDelete.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelAddDelete, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelAddDelete.Name = "m_panelAddDelete";
            this.m_panelAddDelete.Size = new System.Drawing.Size(361, 17);
            this.m_panelAddDelete.TabIndex = 0;
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAdd.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAdd, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(112, 17);
            this.m_btnAdd.TabIndex = 0;
            this.m_btnAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAdd.LinkClicked += new System.EventHandler(this.m_btnAdd_LinkClicked);
            // 
            // CControlEditeParametresFiltre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelControls);
            this.Controls.Add(this.m_panelTop);
            this.Controls.Add(this.m_panelAddDelete);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlEditeParametresFiltre";
            this.Size = new System.Drawing.Size(361, 138);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelAddDelete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblNumParametre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.common.C2iPanel m_panelTop;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
        private sc2i.win32.common.C2iPanel m_panelControls;
        private sc2i.win32.common.C2iPanel m_panelAddDelete;
        private sc2i.win32.common.CWndLinkStd m_btnAdd;
    }
}
