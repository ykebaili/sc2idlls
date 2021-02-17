namespace sc2i.formulaire.win32
{
	partial class CFormEditProprieteAffectationsProprietes
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditProprieteAffectationsProprietes));
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_btnValiderModifications = new System.Windows.Forms.Button();
            this.m_btnAnnulerModifications = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelCondition = new System.Windows.Forms.Panel();
            this.m_txtCondition = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtLibelle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelAffectation = new sc2i.formulaire.win32.CPanelAffectationsProprietes();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_wndListe = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_panelAddRemove = new System.Windows.Forms.Panel();
            this.cWndLinkStd1 = new sc2i.win32.common.CWndLinkStd();
            this.m_wndAdd = new sc2i.win32.common.CWndLinkStd();
            this.m_panelContientAffectation = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_txtFormuleGlobale = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label3 = new System.Windows.Forms.Label();
            this.m_panelBas.SuspendLayout();
            this.m_panelCondition.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelAddRemove.SuspendLayout();
            this.m_panelContientAffectation.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelBas
            // 
            this.m_panelBas.BackColor = System.Drawing.Color.White;
            this.m_panelBas.Controls.Add(this.m_btnValiderModifications);
            this.m_panelBas.Controls.Add(this.m_btnAnnulerModifications);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 353);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(694, 43);
            this.cExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelBas.TabIndex = 3;
            // 
            // m_btnValiderModifications
            // 
            this.m_btnValiderModifications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnValiderModifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnValiderModifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnValiderModifications.ForeColor = System.Drawing.Color.White;
            this.m_btnValiderModifications.Image = ((System.Drawing.Image)(resources.GetObject("m_btnValiderModifications.Image")));
            this.m_btnValiderModifications.Location = new System.Drawing.Point(312, 6);
            this.m_btnValiderModifications.Name = "m_btnValiderModifications";
            this.m_btnValiderModifications.Size = new System.Drawing.Size(32, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnValiderModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnValiderModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnValiderModifications.TabIndex = 4;
            this.m_btnValiderModifications.TabStop = false;
            this.m_btnValiderModifications.Click += new System.EventHandler(this.m_btnValiderModifications_Click);
            // 
            // m_btnAnnulerModifications
            // 
            this.m_btnAnnulerModifications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnulerModifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAnnulerModifications.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnulerModifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnulerModifications.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnulerModifications.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnulerModifications.Image")));
            this.m_btnAnnulerModifications.Location = new System.Drawing.Point(350, 6);
            this.m_btnAnnulerModifications.Name = "m_btnAnnulerModifications";
            this.m_btnAnnulerModifications.Size = new System.Drawing.Size(32, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnulerModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnulerModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnulerModifications.TabIndex = 5;
            this.m_btnAnnulerModifications.TabStop = false;
            this.m_btnAnnulerModifications.Click += new System.EventHandler(this.m_btnAnnulerModifications_Click);
            // 
            // m_panelCondition
            // 
            this.m_panelCondition.Controls.Add(this.m_txtCondition);
            this.m_panelCondition.Controls.Add(this.label1);
            this.m_panelCondition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelCondition.Location = new System.Drawing.Point(0, 325);
            this.m_panelCondition.Name = "m_panelCondition";
            this.m_panelCondition.Size = new System.Drawing.Size(494, 28);
            this.cExtStyle1.SetStyleBackColor(this.m_panelCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelCondition.TabIndex = 2;
            // 
            // m_txtCondition
            // 
            this.m_txtCondition.AllowGraphic = true;
            this.m_txtCondition.AllowNullFormula = false;
            this.m_txtCondition.AllowSaisieTexte = true;
            this.m_txtCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtCondition.Formule = null;
            this.m_txtCondition.Location = new System.Drawing.Point(112, 0);
            this.m_txtCondition.LockEdition = false;
            this.m_txtCondition.LockZoneTexte = false;
            this.m_txtCondition.Name = "m_txtCondition";
            this.m_txtCondition.Size = new System.Drawing.Size(382, 28);
            this.cExtStyle1.SetStyleBackColor(this.m_txtCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtCondition.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 28);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Condition|20011";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtLibelle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 22);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 0;
            // 
            // m_txtLibelle
            // 
            this.m_txtLibelle.Location = new System.Drawing.Point(104, 1);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(346, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelle.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Label|20013";
            // 
            // m_panelAffectation
            // 
            this.m_panelAffectation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelAffectation.Location = new System.Drawing.Point(0, 22);
            this.m_panelAffectation.Name = "m_panelAffectation";
            this.m_panelAffectation.Size = new System.Drawing.Size(494, 275);
            this.cExtStyle1.SetStyleBackColor(this.m_panelAffectation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelAffectation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelAffectation.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_wndListe);
            this.panel2.Controls.Add(this.m_panelAddRemove);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 353);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4;
            // 
            // m_wndListe
            // 
            this.m_wndListe.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListe.Location = new System.Drawing.Point(0, 22);
            this.m_wndListe.MultiSelect = false;
            this.m_wndListe.Name = "m_wndListe";
            this.m_wndListe.Size = new System.Drawing.Size(200, 331);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListe.TabIndex = 4;
            this.m_wndListe.UseCompatibleStateImageBehavior = false;
            this.m_wndListe.View = System.Windows.Forms.View.Details;
            this.m_wndListe.SelectedIndexChanged += new System.EventHandler(this.m_wndListe_SelectedIndexChanged);
            this.m_wndListe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndListe_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 181;
            // 
            // m_panelAddRemove
            // 
            this.m_panelAddRemove.Controls.Add(this.cWndLinkStd1);
            this.m_panelAddRemove.Controls.Add(this.m_wndAdd);
            this.m_panelAddRemove.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelAddRemove.Location = new System.Drawing.Point(0, 0);
            this.m_panelAddRemove.Name = "m_panelAddRemove";
            this.m_panelAddRemove.Size = new System.Drawing.Size(200, 22);
            this.cExtStyle1.SetStyleBackColor(this.m_panelAddRemove, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelAddRemove, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelAddRemove.TabIndex = 3;
            // 
            // cWndLinkStd1
            // 
            this.cWndLinkStd1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cWndLinkStd1.CustomImage = ((System.Drawing.Image)(resources.GetObject("cWndLinkStd1.CustomImage")));
            this.cWndLinkStd1.CustomText = "Remove";
            this.cWndLinkStd1.Dock = System.Windows.Forms.DockStyle.Left;
            this.cWndLinkStd1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.cWndLinkStd1.Location = new System.Drawing.Point(92, 0);
            this.cWndLinkStd1.Name = "cWndLinkStd1";
            this.cWndLinkStd1.ShortMode = false;
            this.cWndLinkStd1.Size = new System.Drawing.Size(102, 22);
            this.cExtStyle1.SetStyleBackColor(this.cWndLinkStd1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.cWndLinkStd1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cWndLinkStd1.TabIndex = 2;
            this.cWndLinkStd1.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.cWndLinkStd1.LinkClicked += new System.EventHandler(this.cWndLinkStd1_LinkClicked);
            // 
            // m_wndAdd
            // 
            this.m_wndAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_wndAdd.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_wndAdd.CustomImage")));
            this.m_wndAdd.CustomText = "Add";
            this.m_wndAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_wndAdd.Location = new System.Drawing.Point(0, 0);
            this.m_wndAdd.Name = "m_wndAdd";
            this.m_wndAdd.ShortMode = false;
            this.m_wndAdd.Size = new System.Drawing.Size(92, 22);
            this.cExtStyle1.SetStyleBackColor(this.m_wndAdd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndAdd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAdd.TabIndex = 1;
            this.m_wndAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_wndAdd.LinkClicked += new System.EventHandler(this.m_wndAdd_LinkClicked);
            // 
            // m_panelContientAffectation
            // 
            this.m_panelContientAffectation.Controls.Add(this.m_panelAffectation);
            this.m_panelContientAffectation.Controls.Add(this.panel3);
            this.m_panelContientAffectation.Controls.Add(this.panel1);
            this.m_panelContientAffectation.Controls.Add(this.m_panelCondition);
            this.m_panelContientAffectation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelContientAffectation.Location = new System.Drawing.Point(200, 0);
            this.m_panelContientAffectation.Name = "m_panelContientAffectation";
            this.m_panelContientAffectation.Size = new System.Drawing.Size(494, 353);
            this.cExtStyle1.SetStyleBackColor(this.m_panelContientAffectation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelContientAffectation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelContientAffectation.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_txtFormuleGlobale);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 297);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(494, 28);
            this.cExtStyle1.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 3;
            // 
            // m_txtFormuleGlobale
            // 
            this.m_txtFormuleGlobale.AllowGraphic = true;
            this.m_txtFormuleGlobale.AllowNullFormula = false;
            this.m_txtFormuleGlobale.AllowSaisieTexte = true;
            this.m_txtFormuleGlobale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormuleGlobale.Formule = null;
            this.m_txtFormuleGlobale.Location = new System.Drawing.Point(112, 0);
            this.m_txtFormuleGlobale.LockEdition = false;
            this.m_txtFormuleGlobale.LockZoneTexte = false;
            this.m_txtFormuleGlobale.Name = "m_txtFormuleGlobale";
            this.m_txtFormuleGlobale.Size = new System.Drawing.Size(382, 28);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormuleGlobale, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormuleGlobale, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleGlobale.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 28);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 0;
            this.label3.Text = "Global formula|20042";
            // 
            // CFormEditProprieteAffectationsProprietes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(694, 396);
            this.Controls.Add(this.m_panelContientAffectation);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_panelBas);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditProprieteAffectationsProprietes";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Affect properties|20000";
            this.Load += new System.EventHandler(this.CFormEditProprieteAffectationsProprietes_Load);
            this.m_panelBas.ResumeLayout(false);
            this.m_panelCondition.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.m_panelAddRemove.ResumeLayout(false);
            this.m_panelContientAffectation.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}


		#endregion

		private CPanelAffectationsProprietes m_panelAffectation;
		private System.Windows.Forms.Panel m_panelBas;
		protected System.Windows.Forms.Button m_btnValiderModifications;
		protected System.Windows.Forms.Button m_btnAnnulerModifications;
		private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel m_panelCondition;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtCondition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txtLibelle;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.common.CWndLinkStd cWndLinkStd1;
        private sc2i.win32.common.CWndLinkStd m_wndAdd;
        private System.Windows.Forms.Panel m_panelAddRemove;
        private System.Windows.Forms.ListView m_wndListe;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel m_panelContientAffectation;
        private System.Windows.Forms.Panel panel3;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleGlobale;
        private System.Windows.Forms.Label label3;
	}
}