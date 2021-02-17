namespace sc2i.win32.expression
{
    partial class CPanelEditeFonctionDynamique
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
            this.m_panelVariables = new System.Windows.Forms.Panel();
            this.m_wndListeParametres = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkRemoveVar = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddVar = new sc2i.win32.common.CWndLinkStd();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_panelNom = new System.Windows.Forms.Panel();
            this.m_txtNomFonction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_menuAddVariable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_panelVariables.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelNom.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelVariables
            // 
            this.m_panelVariables.Controls.Add(this.m_wndListeParametres);
            this.m_panelVariables.Controls.Add(this.panel1);
            this.m_panelVariables.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelVariables.Location = new System.Drawing.Point(436, 0);
            this.m_panelVariables.Name = "m_panelVariables";
            this.m_panelVariables.Size = new System.Drawing.Size(177, 197);
            this.m_panelVariables.TabIndex = 2;
            // 
            // m_wndListeParametres
            // 
            this.m_wndListeParametres.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeParametres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeParametres.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeParametres.MultiSelect = false;
            this.m_wndListeParametres.Name = "m_wndListeParametres";
            this.m_wndListeParametres.Size = new System.Drawing.Size(177, 176);
            this.m_wndListeParametres.TabIndex = 0;
            this.m_wndListeParametres.UseCompatibleStateImageBehavior = false;
            this.m_wndListeParametres.View = System.Windows.Forms.View.Details;
            this.m_wndListeParametres.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_wndListeVariables_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Parameters";
            this.columnHeader1.Width = 154;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkRemoveVar);
            this.panel1.Controls.Add(this.m_lnkAddVar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 21);
            this.panel1.TabIndex = 2;
            // 
            // m_lnkRemoveVar
            // 
            this.m_lnkRemoveVar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkRemoveVar.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkRemoveVar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkRemoveVar.Location = new System.Drawing.Point(87, 0);
            this.m_lnkRemoveVar.Name = "m_lnkRemoveVar";
            this.m_lnkRemoveVar.ShortMode = false;
            this.m_lnkRemoveVar.Size = new System.Drawing.Size(87, 21);
            this.m_lnkRemoveVar.TabIndex = 2;
            this.m_lnkRemoveVar.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkRemoveVar.LinkClicked += new System.EventHandler(this.m_lnkRemoveVar_LinkClicked);
            // 
            // m_lnkAddVar
            // 
            this.m_lnkAddVar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddVar.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddVar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddVar.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddVar.Name = "m_lnkAddVar";
            this.m_lnkAddVar.ShortMode = false;
            this.m_lnkAddVar.Size = new System.Drawing.Size(87, 21);
            this.m_lnkAddVar.TabIndex = 1;
            this.m_lnkAddVar.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddVar.LinkClicked += new System.EventHandler(this.m_lnkAddVar_LinkClicked);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(433, 29);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 168);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 29);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(433, 168);
            this.m_txtFormule.TabIndex = 3;
            // 
            // m_panelNom
            // 
            this.m_panelNom.Controls.Add(this.m_txtNomFonction);
            this.m_panelNom.Controls.Add(this.label1);
            this.m_panelNom.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelNom.Location = new System.Drawing.Point(0, 0);
            this.m_panelNom.Name = "m_panelNom";
            this.m_panelNom.Size = new System.Drawing.Size(436, 29);
            this.m_panelNom.TabIndex = 5;
            // 
            // m_txtNomFonction
            // 
            this.m_txtNomFonction.Location = new System.Drawing.Point(200, 6);
            this.m_txtNomFonction.Name = "m_txtNomFonction";
            this.m_txtNomFonction.Size = new System.Drawing.Size(230, 20);
            this.m_txtNomFonction.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Function name|20032";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_menuAddVariable
            // 
            this.m_menuAddVariable.Name = "m_menuAddVariable";
            this.m_menuAddVariable.Size = new System.Drawing.Size(61, 4);
            // 
            // CPanelEditeFonctionDynamique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelNom);
            this.Controls.Add(this.m_panelVariables);
            this.Name = "CPanelEditeFonctionDynamique";
            this.Size = new System.Drawing.Size(613, 197);
            this.m_panelVariables.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_panelNom.ResumeLayout(false);
            this.m_panelNom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelVariables;
        private System.Windows.Forms.ListView m_wndListeParametres;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CWndLinkStd m_lnkRemoveVar;
        private sc2i.win32.common.CWndLinkStd m_lnkAddVar;
        private CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel m_panelNom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtNomFonction;
        private System.Windows.Forms.ContextMenuStrip m_menuAddVariable;
    }
}
