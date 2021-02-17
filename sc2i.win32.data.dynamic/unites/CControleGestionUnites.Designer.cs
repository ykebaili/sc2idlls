namespace sc2i.win32.data.dynamic.unites
{
    partial class CControleGestionUnites
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
            this.m_arbreUnits = new System.Windows.Forms.TreeView();
            this.m_panelGauche = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkAddUnite = new System.Windows.Forms.LinkLabel();
            this.m_lnkAddClasse = new System.Windows.Forms.LinkLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_menuArbre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_panelClasse = new System.Windows.Forms.Panel();
            this.m_panelEditeClasse = new System.Windows.Forms.Panel();
            this.m_lnkSupprimerClasse = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkModifierClasse = new sc2i.win32.common.CWndLinkStd();
            this.m_txtUniteDeBase = new sc2i.win32.common.C2iTextBox();
            this.m_txtIdClasse = new sc2i.win32.common.C2iTextBox();
            this.m_txtLibelleClasse = new sc2i.win32.common.C2iTextBox();
            this.m_panelUnite = new System.Windows.Forms.Panel();
            this.m_panelEditeUnite = new System.Windows.Forms.Panel();
            this.m_lnkSupprimerUnite = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkModifierUnite = new sc2i.win32.common.CWndLinkStd();
            this.m_txtClasseDeUnite = new sc2i.win32.common.C2iTextBox();
            this.m_txtOffsetConversion = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtFacteurConversion = new sc2i.win32.common.C2iTextBoxNumerique();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_lblConversion = new System.Windows.Forms.Label();
            this.m_txtLibelleLongUnite = new sc2i.win32.common.C2iTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtLibellCourtUnite = new sc2i.win32.common.C2iTextBox();
            this.m_txtIdUnite = new sc2i.win32.common.C2iTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_panelGauche.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelClasse.SuspendLayout();
            this.m_panelEditeClasse.SuspendLayout();
            this.m_panelUnite.SuspendLayout();
            this.m_panelEditeUnite.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbreUnits
            // 
            this.m_arbreUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreUnits.Location = new System.Drawing.Point(0, 44);
            this.m_arbreUnits.Name = "m_arbreUnits";
            this.m_arbreUnits.Size = new System.Drawing.Size(200, 338);
            this.m_arbreUnits.TabIndex = 0;
            this.m_arbreUnits.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreUnits_AfterSelect);
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.Controls.Add(this.m_arbreUnits);
            this.m_panelGauche.Controls.Add(this.panel1);
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(200, 382);
            this.m_panelGauche.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkAddUnite);
            this.panel1.Controls.Add(this.m_lnkAddClasse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 44);
            this.panel1.TabIndex = 8;
            // 
            // m_lnkAddUnite
            // 
            this.m_lnkAddUnite.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lnkAddUnite.Location = new System.Drawing.Point(0, 21);
            this.m_lnkAddUnite.Name = "m_lnkAddUnite";
            this.m_lnkAddUnite.Size = new System.Drawing.Size(200, 21);
            this.m_lnkAddUnite.TabIndex = 9;
            this.m_lnkAddUnite.TabStop = true;
            this.m_lnkAddUnite.Text = "New unit|20071";
            this.m_lnkAddUnite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lnkAddUnite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAddUnite_LinkClicked);
            // 
            // m_lnkAddClasse
            // 
            this.m_lnkAddClasse.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lnkAddClasse.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddClasse.Name = "m_lnkAddClasse";
            this.m_lnkAddClasse.Size = new System.Drawing.Size(200, 21);
            this.m_lnkAddClasse.TabIndex = 8;
            this.m_lnkAddClasse.TabStop = true;
            this.m_lnkAddClasse.Text = "New unit class|20070";
            this.m_lnkAddClasse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lnkAddClasse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAddClasse_LinkClicked);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 382);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_menuArbre
            // 
            this.m_menuArbre.Name = "m_menuArbre";
            this.m_menuArbre.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class Label|20063";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id|20064";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "BaseUnit|20065";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_panelClasse
            // 
            this.m_panelClasse.Controls.Add(this.m_panelEditeClasse);
            this.m_panelClasse.Controls.Add(this.m_txtUniteDeBase);
            this.m_panelClasse.Controls.Add(this.m_txtIdClasse);
            this.m_panelClasse.Controls.Add(this.m_txtLibelleClasse);
            this.m_panelClasse.Controls.Add(this.label3);
            this.m_panelClasse.Controls.Add(this.label1);
            this.m_panelClasse.Controls.Add(this.label2);
            this.m_panelClasse.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelClasse.Location = new System.Drawing.Point(203, 0);
            this.m_panelClasse.Name = "m_panelClasse";
            this.m_panelClasse.Size = new System.Drawing.Size(400, 105);
            this.m_panelClasse.TabIndex = 6;
            this.m_panelClasse.Visible = false;
            // 
            // m_panelEditeClasse
            // 
            this.m_panelEditeClasse.Controls.Add(this.m_lnkSupprimerClasse);
            this.m_panelEditeClasse.Controls.Add(this.m_lnkModifierClasse);
            this.m_panelEditeClasse.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelEditeClasse.Location = new System.Drawing.Point(0, 0);
            this.m_panelEditeClasse.Name = "m_panelEditeClasse";
            this.m_panelEditeClasse.Size = new System.Drawing.Size(400, 21);
            this.m_panelEditeClasse.TabIndex = 7;
            // 
            // m_lnkSupprimerClasse
            // 
            this.m_lnkSupprimerClasse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimerClasse.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkSupprimerClasse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimerClasse.Location = new System.Drawing.Point(112, 0);
            this.m_lnkSupprimerClasse.Name = "m_lnkSupprimerClasse";
            this.m_lnkSupprimerClasse.Size = new System.Drawing.Size(112, 21);
            this.m_lnkSupprimerClasse.TabIndex = 7;
            this.m_lnkSupprimerClasse.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimerClasse.LinkClicked += new System.EventHandler(this.m_lnkSupprimerClasse_LinkClicked);
            // 
            // m_lnkModifierClasse
            // 
            this.m_lnkModifierClasse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkModifierClasse.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkModifierClasse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkModifierClasse.Location = new System.Drawing.Point(0, 0);
            this.m_lnkModifierClasse.Name = "m_lnkModifierClasse";
            this.m_lnkModifierClasse.Size = new System.Drawing.Size(112, 21);
            this.m_lnkModifierClasse.TabIndex = 6;
            this.m_lnkModifierClasse.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkModifierClasse.LinkClicked += new System.EventHandler(this.m_lnkModifierClasse_LinkClicked);
            // 
            // m_txtUniteDeBase
            // 
            this.m_txtUniteDeBase.Location = new System.Drawing.Point(109, 76);
            this.m_txtUniteDeBase.LockEdition = true;
            this.m_txtUniteDeBase.Name = "m_txtUniteDeBase";
            this.m_txtUniteDeBase.Size = new System.Drawing.Size(248, 20);
            this.m_txtUniteDeBase.TabIndex = 5;
            // 
            // m_txtIdClasse
            // 
            this.m_txtIdClasse.Location = new System.Drawing.Point(109, 50);
            this.m_txtIdClasse.LockEdition = true;
            this.m_txtIdClasse.Name = "m_txtIdClasse";
            this.m_txtIdClasse.Size = new System.Drawing.Size(248, 20);
            this.m_txtIdClasse.TabIndex = 4;
            // 
            // m_txtLibelleClasse
            // 
            this.m_txtLibelleClasse.Location = new System.Drawing.Point(109, 24);
            this.m_txtLibelleClasse.LockEdition = true;
            this.m_txtLibelleClasse.Name = "m_txtLibelleClasse";
            this.m_txtLibelleClasse.Size = new System.Drawing.Size(248, 20);
            this.m_txtLibelleClasse.TabIndex = 3;
            // 
            // m_panelUnite
            // 
            this.m_panelUnite.Controls.Add(this.m_panelEditeUnite);
            this.m_panelUnite.Controls.Add(this.m_txtClasseDeUnite);
            this.m_panelUnite.Controls.Add(this.m_txtOffsetConversion);
            this.m_panelUnite.Controls.Add(this.m_txtFacteurConversion);
            this.m_panelUnite.Controls.Add(this.label9);
            this.m_panelUnite.Controls.Add(this.label8);
            this.m_panelUnite.Controls.Add(this.m_lblConversion);
            this.m_panelUnite.Controls.Add(this.m_txtLibelleLongUnite);
            this.m_panelUnite.Controls.Add(this.label7);
            this.m_panelUnite.Controls.Add(this.m_txtLibellCourtUnite);
            this.m_panelUnite.Controls.Add(this.m_txtIdUnite);
            this.m_panelUnite.Controls.Add(this.label4);
            this.m_panelUnite.Controls.Add(this.label5);
            this.m_panelUnite.Controls.Add(this.label6);
            this.m_panelUnite.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelUnite.Location = new System.Drawing.Point(203, 105);
            this.m_panelUnite.Name = "m_panelUnite";
            this.m_panelUnite.Size = new System.Drawing.Size(400, 224);
            this.m_panelUnite.TabIndex = 7;
            this.m_panelUnite.Visible = false;
            // 
            // m_panelEditeUnite
            // 
            this.m_panelEditeUnite.Controls.Add(this.m_lnkSupprimerUnite);
            this.m_panelEditeUnite.Controls.Add(this.m_lnkModifierUnite);
            this.m_panelEditeUnite.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelEditeUnite.Location = new System.Drawing.Point(0, 0);
            this.m_panelEditeUnite.Name = "m_panelEditeUnite";
            this.m_panelEditeUnite.Size = new System.Drawing.Size(400, 21);
            this.m_panelEditeUnite.TabIndex = 15;
            // 
            // m_lnkSupprimerUnite
            // 
            this.m_lnkSupprimerUnite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimerUnite.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkSupprimerUnite.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimerUnite.Location = new System.Drawing.Point(112, 0);
            this.m_lnkSupprimerUnite.Name = "m_lnkSupprimerUnite";
            this.m_lnkSupprimerUnite.Size = new System.Drawing.Size(112, 21);
            this.m_lnkSupprimerUnite.TabIndex = 7;
            this.m_lnkSupprimerUnite.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimerUnite.LinkClicked += new System.EventHandler(this.m_lnkSupprimerUnite_LinkClicked);
            // 
            // m_lnkModifierUnite
            // 
            this.m_lnkModifierUnite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkModifierUnite.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkModifierUnite.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkModifierUnite.Location = new System.Drawing.Point(0, 0);
            this.m_lnkModifierUnite.Name = "m_lnkModifierUnite";
            this.m_lnkModifierUnite.Size = new System.Drawing.Size(112, 21);
            this.m_lnkModifierUnite.TabIndex = 6;
            this.m_lnkModifierUnite.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkModifierUnite.LinkClicked += new System.EventHandler(this.m_lnkModifierUnite_LinkClicked);
            // 
            // m_txtClasseDeUnite
            // 
            this.m_txtClasseDeUnite.Location = new System.Drawing.Point(109, 22);
            this.m_txtClasseDeUnite.LockEdition = true;
            this.m_txtClasseDeUnite.Name = "m_txtClasseDeUnite";
            this.m_txtClasseDeUnite.Size = new System.Drawing.Size(248, 20);
            this.m_txtClasseDeUnite.TabIndex = 14;
            // 
            // m_txtOffsetConversion
            // 
            this.m_txtOffsetConversion.Arrondi = 12;
            this.m_txtOffsetConversion.DecimalAutorise = true;
            this.m_txtOffsetConversion.IntValue = 0;
            this.m_txtOffsetConversion.Location = new System.Drawing.Point(112, 190);
            this.m_txtOffsetConversion.LockEdition = true;
            this.m_txtOffsetConversion.Name = "m_txtOffsetConversion";
            this.m_txtOffsetConversion.NullAutorise = false;
            this.m_txtOffsetConversion.SelectAllOnEnter = true;
            this.m_txtOffsetConversion.SeparateurMilliers = "";
            this.m_txtOffsetConversion.Size = new System.Drawing.Size(182, 20);
            this.m_txtOffsetConversion.TabIndex = 13;
            this.m_txtOffsetConversion.Text = "0";
            this.m_txtOffsetConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtFacteurConversion
            // 
            this.m_txtFacteurConversion.Arrondi = 12;
            this.m_txtFacteurConversion.DecimalAutorise = true;
            this.m_txtFacteurConversion.IntValue = 0;
            this.m_txtFacteurConversion.Location = new System.Drawing.Point(112, 161);
            this.m_txtFacteurConversion.LockEdition = true;
            this.m_txtFacteurConversion.Name = "m_txtFacteurConversion";
            this.m_txtFacteurConversion.NullAutorise = false;
            this.m_txtFacteurConversion.SelectAllOnEnter = true;
            this.m_txtFacteurConversion.SeparateurMilliers = "";
            this.m_txtFacteurConversion.Size = new System.Drawing.Size(182, 20);
            this.m_txtFacteurConversion.TabIndex = 12;
            this.m_txtFacteurConversion.Text = "0";
            this.m_txtFacteurConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 18);
            this.label9.TabIndex = 11;
            this.label9.Text = "B=";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 18);
            this.label8.TabIndex = 10;
            this.label8.Text = "A=";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblConversion
            // 
            this.m_lblConversion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_lblConversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblConversion.Location = new System.Drawing.Point(18, 134);
            this.m_lblConversion.Name = "m_lblConversion";
            this.m_lblConversion.Size = new System.Drawing.Size(339, 21);
            this.m_lblConversion.TabIndex = 9;
            // 
            // m_txtLibelleLongUnite
            // 
            this.m_txtLibelleLongUnite.Location = new System.Drawing.Point(109, 102);
            this.m_txtLibelleLongUnite.LockEdition = true;
            this.m_txtLibelleLongUnite.Name = "m_txtLibelleLongUnite";
            this.m_txtLibelleLongUnite.Size = new System.Drawing.Size(248, 20);
            this.m_txtLibelleLongUnite.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 18);
            this.label7.TabIndex = 7;
            this.label7.Text = "Long label|20068";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtLibellCourtUnite
            // 
            this.m_txtLibellCourtUnite.Location = new System.Drawing.Point(109, 76);
            this.m_txtLibellCourtUnite.LockEdition = true;
            this.m_txtLibellCourtUnite.Name = "m_txtLibellCourtUnite";
            this.m_txtLibellCourtUnite.Size = new System.Drawing.Size(248, 20);
            this.m_txtLibellCourtUnite.TabIndex = 5;
            // 
            // m_txtIdUnite
            // 
            this.m_txtIdUnite.Location = new System.Drawing.Point(109, 50);
            this.m_txtIdUnite.LockEdition = true;
            this.m_txtIdUnite.Name = "m_txtIdUnite";
            this.m_txtIdUnite.Size = new System.Drawing.Size(248, 20);
            this.m_txtIdUnite.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "Short label|20067";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Class|20066";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "Id|20064";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CControleGestionUnites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelUnite);
            this.Controls.Add(this.m_panelClasse);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelGauche);
            this.Name = "CControleGestionUnites";
            this.Size = new System.Drawing.Size(603, 382);
            this.m_panelGauche.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_panelClasse.ResumeLayout(false);
            this.m_panelClasse.PerformLayout();
            this.m_panelEditeClasse.ResumeLayout(false);
            this.m_panelUnite.ResumeLayout(false);
            this.m_panelUnite.PerformLayout();
            this.m_panelEditeUnite.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbreUnits;
        private System.Windows.Forms.Panel m_panelGauche;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ContextMenuStrip m_menuArbre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel m_panelClasse;
        private sc2i.win32.common.C2iTextBox m_txtUniteDeBase;
        private sc2i.win32.common.C2iTextBox m_txtIdClasse;
        private sc2i.win32.common.C2iTextBox m_txtLibelleClasse;
        private System.Windows.Forms.Panel m_panelUnite;
        private sc2i.win32.common.C2iTextBox m_txtLibellCourtUnite;
        private sc2i.win32.common.C2iTextBox m_txtIdUnite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private sc2i.win32.common.C2iTextBox m_txtLibelleLongUnite;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label m_lblConversion;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtOffsetConversion;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtFacteurConversion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private sc2i.win32.common.CWndLinkStd m_lnkModifierClasse;
        private sc2i.win32.common.C2iTextBox m_txtClasseDeUnite;
        private System.Windows.Forms.Panel m_panelEditeClasse;
        private sc2i.win32.common.CWndLinkStd m_lnkSupprimerClasse;
        private System.Windows.Forms.Panel m_panelEditeUnite;
        private sc2i.win32.common.CWndLinkStd m_lnkSupprimerUnite;
        private sc2i.win32.common.CWndLinkStd m_lnkModifierUnite;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel m_lnkAddClasse;
        private System.Windows.Forms.LinkLabel m_lnkAddUnite;
    }
}
