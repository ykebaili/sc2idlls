namespace sc2i.win32.data.navigation.RefTypeForm
{
    partial class CPanelRefTypeFormAvecCondition
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_txtCondition = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_lblFormule = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_picDragDrop = new System.Windows.Forms.PictureBox();
            this.m_lblIndex = new System.Windows.Forms.Label();
            this.m_comboDefaultForm = new sc2i.win32.data.navigation.RefTypeForm.CControlSelectTypeFormEditForType();
            this.cGestionnaireEditionSousObjetDonnee1 = new sc2i.win32.data.navigation.CGestionnaireEditionSousObjetDonnee(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picDragDrop)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(60, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Form|20025";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_comboDefaultForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(142, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 31);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(381, 0);
            this.m_extModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(266, 31);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_txtCondition);
            this.panel3.Controls.Add(this.m_lblFormule);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(100, 0);
            this.m_extModeEdition.SetModeEdition(this.panel3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(166, 31);
            this.panel3.TabIndex = 5;
            // 
            // m_txtCondition
            // 
            this.m_txtCondition.AllowGraphic = true;
            this.m_txtCondition.AllowNullFormula = true;
            this.m_txtCondition.AllowSaisieTexte = true;
            this.m_txtCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtCondition.Formule = null;
            this.m_txtCondition.Location = new System.Drawing.Point(22, 0);
            this.m_txtCondition.LockEdition = false;
            this.m_txtCondition.LockZoneTexte = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtCondition, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtCondition.Name = "m_txtCondition";
            this.m_txtCondition.Size = new System.Drawing.Size(144, 31);
            this.m_txtCondition.TabIndex = 0;
            // 
            // m_lblFormule
            // 
            this.m_lblFormule.BackColor = System.Drawing.Color.White;
            this.m_lblFormule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblFormule.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblFormule.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblFormule, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblFormule.Name = "m_lblFormule";
            this.m_lblFormule.Size = new System.Drawing.Size(22, 31);
            this.m_lblFormule.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Condition|20026";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 31);
            this.m_extModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(647, 1);
            this.label3.TabIndex = 6;
            // 
            // m_picDragDrop
            // 
            this.m_picDragDrop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_picDragDrop.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_picDragDrop.Image = global::sc2i.win32.data.navigation.Properties.Resources.Action1;
            this.m_picDragDrop.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_picDragDrop, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_picDragDrop.Name = "m_picDragDrop";
            this.m_picDragDrop.Size = new System.Drawing.Size(31, 31);
            this.m_picDragDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_picDragDrop.TabIndex = 8;
            this.m_picDragDrop.TabStop = false;
            this.m_picDragDrop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_picDragDrop_MouseMove);
            this.m_picDragDrop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_picDragDrop_MouseDown);
            // 
            // m_lblIndex
            // 
            this.m_lblIndex.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblIndex.Location = new System.Drawing.Point(31, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblIndex, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblIndex.Name = "m_lblIndex";
            this.m_lblIndex.Size = new System.Drawing.Size(29, 31);
            this.m_lblIndex.TabIndex = 9;
            this.m_lblIndex.Text = "1";
            this.m_lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_comboDefaultForm
            // 
            this.m_comboDefaultForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_comboDefaultForm.AutoSize = true;
            this.m_comboDefaultForm.Location = new System.Drawing.Point(10, 5);
            this.m_extModeEdition.SetModeEdition(this.m_comboDefaultForm, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_comboDefaultForm.Name = "m_comboDefaultForm";
            this.m_comboDefaultForm.Size = new System.Drawing.Size(223, 23);
            this.m_comboDefaultForm.TabIndex = 2;
            this.m_comboDefaultForm.TypeSelectionne = null;
            // 
            // cGestionnaireEditionSousObjetDonnee1
            // 
            this.cGestionnaireEditionSousObjetDonnee1.ObjetEdite = null;
            // 
            // CPanelRefTypeFormAvecCondition
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ColorInactive = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_lblIndex);
            this.Controls.Add(this.m_picDragDrop);
            this.Controls.Add(this.label3);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelRefTypeFormAvecCondition";
            this.Size = new System.Drawing.Size(647, 32);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picDragDrop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CControlSelectTypeFormEditForType m_comboDefaultForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtCondition;
        private System.Windows.Forms.Label label3;
        private CGestionnaireEditionSousObjetDonnee cGestionnaireEditionSousObjetDonnee1;
        private System.Windows.Forms.PictureBox m_picDragDrop;
        private System.Windows.Forms.Label m_lblFormule;
        private System.Windows.Forms.Label m_lblIndex;
    }
}
