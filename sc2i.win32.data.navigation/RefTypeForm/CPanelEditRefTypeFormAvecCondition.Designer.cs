namespace sc2i.win32.data.navigation.RefTypeForm
{
    partial class CPanelEditRefTypeFormAvecCondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditRefTypeFormAvecCondition));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_comboDefaultForm = new sc2i.win32.data.navigation.RefTypeForm.CControlSelectTypeFormEditForType();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_wndListeForms = new sc2i.win32.common.customizableList.CCustomizableList();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_lnkRemove = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAdd = new sc2i.win32.common.CWndLinkStd();
            this.label2 = new System.Windows.Forms.Label();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_comboDefaultForm);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 33);
            this.panel1.TabIndex = 0;
            // 
            // m_comboDefaultForm
            // 
            this.m_comboDefaultForm.AutoSize = true;
            this.m_comboDefaultForm.Location = new System.Drawing.Point(150, 5);
            this.m_extModeEdition.SetModeEdition(this.m_comboDefaultForm, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_comboDefaultForm.Name = "m_comboDefaultForm";
            this.m_comboDefaultForm.Size = new System.Drawing.Size(356, 23);
            this.m_comboDefaultForm.TabIndex = 1;
            this.m_comboDefaultForm.TypeSelectionne = null;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 5);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default form|20023";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_wndListeForms);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.m_extModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(519, 111);
            this.panel2.TabIndex = 1;
            // 
            // m_wndListeForms
            // 
            this.m_wndListeForms.CurrentItemIndex = null;
            this.m_wndListeForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeForms.ItemControl = null;
            this.m_wndListeForms.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_wndListeForms.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeForms.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_wndListeForms, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeForms.Name = "m_wndListeForms";
            this.m_wndListeForms.Size = new System.Drawing.Size(519, 90);
            this.m_wndListeForms.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_lnkRemove);
            this.panel3.Controls.Add(this.m_lnkAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(519, 21);
            this.panel3.TabIndex = 0;
            // 
            // m_lnkRemove
            // 
            this.m_lnkRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkRemove.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkRemove.CustomImage")));
            this.m_lnkRemove.CustomText = "Remove";
            this.m_lnkRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkRemove.Location = new System.Drawing.Point(95, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lnkRemove, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkRemove.Name = "m_lnkRemove";
            this.m_lnkRemove.ShortMode = false;
            this.m_lnkRemove.Size = new System.Drawing.Size(95, 21);
            this.m_lnkRemove.TabIndex = 1;
            this.m_lnkRemove.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkRemove.LinkClicked += new System.EventHandler(this.m_lnkRemove_LinkClicked);
            // 
            // m_lnkAdd
            // 
            this.m_lnkAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAdd.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkAdd.CustomImage")));
            this.m_lnkAdd.CustomText = "Add";
            this.m_lnkAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAdd.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lnkAdd, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAdd.Name = "m_lnkAdd";
            this.m_lnkAdd.ShortMode = false;
            this.m_lnkAdd.Size = new System.Drawing.Size(95, 21);
            this.m_lnkAdd.TabIndex = 0;
            this.m_lnkAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAdd.LinkClicked += new System.EventHandler(this.m_lnkAdd_LinkClicked);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 33);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(519, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Conditionnal forms|20024";
            // 
            // CPanelEditRefTypeFormAvecCondition
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditRefTypeFormAvecCondition";
            this.Size = new System.Drawing.Size(519, 164);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private CControlSelectTypeFormEditForType m_comboDefaultForm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private sc2i.win32.common.CWndLinkStd m_lnkRemove;
        private sc2i.win32.common.CWndLinkStd m_lnkAdd;
        private sc2i.win32.common.customizableList.CCustomizableList m_wndListeForms;
        protected sc2i.win32.common.CExtModeEdition m_extModeEdition;
    }
}
