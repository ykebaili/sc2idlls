namespace sc2i.win32.expression.variablesDynamiques
{
    partial class CControleVariablesDynamiques
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnDeleteVariable = new sc2i.win32.common.CWndLinkStd();
            this.m_btnDetailVariable = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAddVariable = new sc2i.win32.common.CWndLinkStd();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_wndListeVariables = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_menuAddVariable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnDeleteVariable);
            this.panel1.Controls.Add(this.m_btnDetailVariable);
            this.panel1.Controls.Add(this.m_btnAddVariable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 26);
            this.panel1.TabIndex = 0;
            // 
            // m_btnDeleteVariable
            // 
            this.m_btnDeleteVariable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnDeleteVariable.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnDeleteVariable.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnDeleteVariable.Location = new System.Drawing.Point(216, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnDeleteVariable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDeleteVariable.Name = "m_btnDeleteVariable";
            this.m_btnDeleteVariable.ShortMode = false;
            this.m_btnDeleteVariable.Size = new System.Drawing.Size(108, 26);
            this.m_btnDeleteVariable.TabIndex = 2;
            this.m_btnDeleteVariable.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnDeleteVariable.LinkClicked += new System.EventHandler(this.m_btnDeleteVariable_LinkClicked);
            // 
            // m_btnDetailVariable
            // 
            this.m_btnDetailVariable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnDetailVariable.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnDetailVariable.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnDetailVariable.Location = new System.Drawing.Point(108, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnDetailVariable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDetailVariable.Name = "m_btnDetailVariable";
            this.m_btnDetailVariable.ShortMode = false;
            this.m_btnDetailVariable.Size = new System.Drawing.Size(108, 26);
            this.m_btnDetailVariable.TabIndex = 1;
            this.m_btnDetailVariable.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnDetailVariable.LinkClicked += new System.EventHandler(this.m_btnDetailVariable_LinkClicked);
            // 
            // m_btnAddVariable
            // 
            this.m_btnAddVariable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAddVariable.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnAddVariable.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAddVariable.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnAddVariable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAddVariable.Name = "m_btnAddVariable";
            this.m_btnAddVariable.ShortMode = false;
            this.m_btnAddVariable.Size = new System.Drawing.Size(108, 26);
            this.m_btnAddVariable.TabIndex = 0;
            this.m_btnAddVariable.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAddVariable.LinkClicked += new System.EventHandler(this.m_btnAddVariable_LinkClicked);
            // 
            // m_extModeEdition
            // 
            this.m_extModeEdition.ModeEdition = true;
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeVariables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeVariables.Location = new System.Drawing.Point(0, 26);
            this.m_extModeEdition.SetModeEdition(this.m_wndListeVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(424, 199);
            this.m_wndListeVariables.TabIndex = 1;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_wndListeVariables_MouseDoubleClick);
            this.m_wndListeVariables.SizeChanged += new System.EventHandler(this.m_wndListeVariables_SizeChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 403;
            // 
            // m_menuAddVariable
            // 
            this.m_extModeEdition.SetModeEdition(this.m_menuAddVariable, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuAddVariable.Name = "m_menuAddVariable";
            this.m_menuAddVariable.Size = new System.Drawing.Size(61, 4);
            // 
            // CControleVariablesDynamiques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_wndListeVariables);
            this.Controls.Add(this.panel1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleVariablesDynamiques";
            this.Size = new System.Drawing.Size(424, 225);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.ListView m_wndListeVariables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private sc2i.win32.common.CWndLinkStd m_btnDeleteVariable;
        private sc2i.win32.common.CWndLinkStd m_btnDetailVariable;
        private sc2i.win32.common.CWndLinkStd m_btnAddVariable;
        private System.Windows.Forms.ContextMenuStrip m_menuAddVariable;
    }
}
