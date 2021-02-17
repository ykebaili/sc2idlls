using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.win32.expression.variablesDynamiques;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditVariableDynamiqueSelectionObjetDonnee.
	/// </summary>
    [AutoExec("Autoexec")]
    public class CFormEditVariableDynamiqueSelectionObjetDonnee : System.Windows.Forms.Form, IFormEditVariableDynamique
	{
		private sc2i.win32.expression.CControleEditeFormule m_textBoxReceiveFormules = null;
		private CFiltreDynamique m_filtreDynamique = null	;
		private CVariableDynamiqueSelectionObjetDonnee m_variable;

		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.TextBox m_txtNomVariable;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private sc2i.win32.expression.CControleEditeFormule m_txtValeurAffichee;
		private sc2i.win32.common.C2iPanelOmbre m_panelNom;
		private sc2i.win32.common.C2iPanelOmbre m_panelValeurs;
		private sc2i.win32.expression.CControleEditeFormule m_txtValeurStockee;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private sc2i.win32.data.dynamic.CPanelEditFiltreDynamique m_panelFiltre;
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private System.Windows.Forms.LinkLabel m_lnkTester;
		private System.Windows.Forms.CheckBox m_chkCanBeNull;
		private System.Windows.Forms.TextBox m_txtNullValue;
		private System.Windows.Forms.CheckBox m_chkRechercheRapide;
        private CExtStyle cExtStyle1;
		private System.ComponentModel.IContainer components;

		public CFormEditVariableDynamiqueSelectionObjetDonnee()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.components = new System.ComponentModel.Container();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_panelNom = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtNomVariable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtValeurStockee = new sc2i.win32.expression.CControleEditeFormule();
            this.m_txtValeurAffichee = new sc2i.win32.expression.CControleEditeFormule();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkTester = new System.Windows.Forms.LinkLabel();
            this.m_panelFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.m_panelValeurs = new sc2i.win32.common.C2iPanelOmbre();
            this.m_chkRechercheRapide = new System.Windows.Forms.CheckBox();
            this.m_chkCanBeNull = new System.Windows.Forms.CheckBox();
            this.m_txtNullValue = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelNom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelValeurs.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(296, 434);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 4;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(168, 434);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_panelNom
            // 
            this.m_panelNom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelNom.Controls.Add(this.m_txtNomVariable);
            this.m_panelNom.Controls.Add(this.label2);
            this.m_panelNom.Controls.Add(this.label1);
            this.m_panelNom.Location = new System.Drawing.Point(8, 8);
            this.m_panelNom.LockEdition = false;
            this.m_panelNom.Name = "m_panelNom";
            this.m_panelNom.Size = new System.Drawing.Size(536, 80);
            this.cExtStyle1.SetStyleBackColor(this.m_panelNom, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelNom, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelNom.TabIndex = 0;
            // 
            // m_txtNomVariable
            // 
            this.m_txtNomVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomVariable.Location = new System.Drawing.Point(136, 32);
            this.m_txtNomVariable.Name = "m_txtNomVariable";
            this.m_txtNomVariable.Size = new System.Drawing.Size(376, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomVariable.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable name|143";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 16);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object list selection variable|153";
            // 
            // m_txtValeurStockee
            // 
            this.m_txtValeurStockee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtValeurStockee.BackColor = System.Drawing.Color.White;
            this.m_txtValeurStockee.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtValeurStockee.Location = new System.Drawing.Point(136, 48);
            this.m_txtValeurStockee.LockEdition = false;
            this.m_txtValeurStockee.Name = "m_txtValeurStockee";
            this.m_txtValeurStockee.Size = new System.Drawing.Size(376, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_txtValeurStockee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtValeurStockee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeurStockee.TabIndex = 1;
            this.m_txtValeurStockee.Enter += new System.EventHandler(this.m_txtValeur_Enter);
            // 
            // m_txtValeurAffichee
            // 
            this.m_txtValeurAffichee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtValeurAffichee.BackColor = System.Drawing.Color.White;
            this.m_txtValeurAffichee.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtValeurAffichee.Location = new System.Drawing.Point(136, 8);
            this.m_txtValeurAffichee.LockEdition = false;
            this.m_txtValeurAffichee.Name = "m_txtValeurAffichee";
            this.m_txtValeurAffichee.Size = new System.Drawing.Size(376, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_txtValeurAffichee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtValeurAffichee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeurAffichee.TabIndex = 0;
            this.m_txtValeurAffichee.Enter += new System.EventHandler(this.m_txtValeur_Enter);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 16);
            this.cExtStyle1.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 7;
            this.label6.Text = "Stored value|155";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.cExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 4;
            this.label4.Text = "Displayed value|154";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkTester);
            this.panel1.Controls.Add(this.m_panelFiltre);
            this.panel1.Controls.Add(this.m_panelValeurs);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.m_panelNom);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 461);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 8;
            // 
            // m_lnkTester
            // 
            this.m_lnkTester.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.m_lnkTester.Location = new System.Drawing.Point(448, 430);
            this.m_lnkTester.Name = "m_lnkTester";
            this.m_lnkTester.Size = new System.Drawing.Size(88, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkTester, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkTester, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkTester.TabIndex = 12;
            this.m_lnkTester.TabStop = true;
            this.m_lnkTester.Text = "Test|25";
            this.m_lnkTester.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_lnkTester.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkTester_LinkClicked);
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelFiltre.FiltreDynamique = null;
            this.m_panelFiltre.Location = new System.Drawing.Point(10, 88);
            this.m_panelFiltre.LockEdition = false;
            this.m_panelFiltre.ModeSansType = false;
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(534, 191);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltre.TabIndex = 1;
            this.m_panelFiltre.Load += new System.EventHandler(this.m_panelFiltre_Load);
            this.m_panelFiltre.OnChangeTypeElements += new sc2i.win32.data.dynamic.ChangeTypeElementsEventHandler(this.m_panelFiltre_OnChangeTypeElements);
            // 
            // m_panelValeurs
            // 
            this.m_panelValeurs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelValeurs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelValeurs.Controls.Add(this.m_txtValeurAffichee);
            this.m_panelValeurs.Controls.Add(this.m_txtValeurStockee);
            this.m_panelValeurs.Controls.Add(this.label6);
            this.m_panelValeurs.Controls.Add(this.m_chkRechercheRapide);
            this.m_panelValeurs.Controls.Add(this.label4);
            this.m_panelValeurs.Controls.Add(this.m_chkCanBeNull);
            this.m_panelValeurs.Controls.Add(this.m_txtNullValue);
            this.m_panelValeurs.Location = new System.Drawing.Point(8, 279);
            this.m_panelValeurs.LockEdition = false;
            this.m_panelValeurs.Name = "m_panelValeurs";
            this.m_panelValeurs.Size = new System.Drawing.Size(536, 152);
            this.cExtStyle1.SetStyleBackColor(this.m_panelValeurs, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelValeurs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelValeurs.TabIndex = 2;
            // 
            // m_chkRechercheRapide
            // 
            this.m_chkRechercheRapide.Location = new System.Drawing.Point(122, 114);
            this.m_chkRechercheRapide.Name = "m_chkRechercheRapide";
            this.m_chkRechercheRapide.Size = new System.Drawing.Size(288, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_chkRechercheRapide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkRechercheRapide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkRechercheRapide.TabIndex = 8;
            this.m_chkRechercheRapide.Text = "Use quick serach|157";
            // 
            // m_chkCanBeNull
            // 
            this.m_chkCanBeNull.Location = new System.Drawing.Point(122, 88);
            this.m_chkCanBeNull.Name = "m_chkCanBeNull";
            this.m_chkCanBeNull.Size = new System.Drawing.Size(123, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_chkCanBeNull, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkCanBeNull, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkCanBeNull.TabIndex = 2;
            this.m_chkCanBeNull.Text = "Enable NULL|156";
            this.m_chkCanBeNull.CheckedChanged += new System.EventHandler(this.m_chkCanBeNull_CheckedChanged);
            // 
            // m_txtNullValue
            // 
            this.m_txtNullValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNullValue.Location = new System.Drawing.Point(248, 88);
            this.m_txtNullValue.Name = "m_txtNullValue";
            this.m_txtNullValue.Size = new System.Drawing.Size(264, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNullValue, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNullValue, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNullValue.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 461);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.ControlBottomOffset = 16;
            this.c2iTabControl1.ControlRightOffset = 16;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 0);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = true;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.Size = new System.Drawing.Size(496, 272);
            this.cExtStyle1.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl1.TabIndex = 0;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(544, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(184, 461);
            this.cExtStyle1.SetStyleBackColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAideFormule.TabIndex = 5;
			this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CFormEditVariableDynamiqueSelectionObjetDonnee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(728, 461);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_wndAideFormule);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditVariableDynamiqueSelectionObjetDonnee";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Filter variable|145";
            this.Load += new System.EventHandler(this.CFormEditVariableDynamiqueSelectionObjetDonnee_Load);
            this.m_panelNom.ResumeLayout(false);
            this.m_panelNom.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.m_panelValeurs.ResumeLayout(false);
            this.m_panelValeurs.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        /// //////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireEditeursVariablesDynamiques.RegisterEditeur(typeof(CVariableDynamiqueSelectionObjetDonnee), typeof(CFormEditVariableDynamiqueSelectionObjetDonnee));
        }

		/// //////////////////////////////////////////////////
		private void CFormEditVariableDynamiqueSelectionObjetDonnee_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            m_wndAideFormule.FournisseurProprietes = new CFournisseurPropDynStd(true);
			m_txtNomVariable.Text = m_variable.Nom;

		}

		/// //////////////////////////////////////////////////
		private void Init ( CVariableDynamiqueSelectionObjetDonnee variable )
		{
			m_wndAideFormule.FournisseurProprietes = new CFournisseurPropDynStd(true);
			m_variable = variable;
			//Clone le filtre
			m_filtreDynamique = (CFiltreDynamique)CCloner2iSerializable.Clone(m_variable.FiltreSelection);
			if ( m_filtreDynamique == null )
				m_filtreDynamique = new CFiltreDynamique(CSc2iWin32DataClient.ContexteCourant);
			if (m_variable.FiltreSelection != null)
				m_filtreDynamique.ElementAVariablesExterne = m_variable.FiltreSelection.ElementAVariablesExterne;
			m_wndAideFormule.ObjetInterroge = m_filtreDynamique.TypeElements;

			m_txtValeurAffichee.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
			m_panelFiltre.InitSansVariables ( m_filtreDynamique );
			if ( m_variable.ExpressionAffichee != null )
				m_txtValeurAffichee.Text = m_variable.ExpressionAffichee.GetString();
			else
				m_txtValeurAffichee.Text = "";
			if ( m_variable.ExpressionRetournee != null )
				m_txtValeurStockee.Text = m_variable.ExpressionRetournee.GetString();
			else
				m_txtValeurStockee.Text = "";
			m_txtValeurStockee.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
			m_textBoxReceiveFormules = m_txtValeurAffichee;
			m_txtValeurAffichee.BackColor = Color.LightGreen;
			m_chkCanBeNull.Checked = m_variable.CanBeNull;
			m_txtNullValue.Text = m_variable.TextNull;
			m_txtNullValue.Visible = m_chkCanBeNull.Checked;
			m_chkRechercheRapide.Checked = m_variable.UtiliserRechercheRapide;
		}


		/// //////////////////////////////////////////////////
		public static bool EditeVariable ( CVariableDynamiqueSelectionObjetDonnee variable )
		{
			CFormEditVariableDynamiqueSelectionObjetDonnee form = new CFormEditVariableDynamiqueSelectionObjetDonnee();
			form.Init ( variable );
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}

		/// //////////////////////////////////////////////////
		private void m_panelFiltre_OnChangeTypeElements(object sender, System.Type typeSelectionne)
		{
			m_wndAideFormule.ObjetInterroge = typeSelectionne;
			m_txtValeurAffichee.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
			m_txtValeurStockee.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
		}

		private void m_panelFiltre_Load(object sender, System.EventArgs e)
		{
		
		}

		private void m_txtValeur_Enter(object sender, System.EventArgs e)
		{
			if ( sender is sc2i.win32.expression.CControleEditeFormule )
			{
				m_textBoxReceiveFormules.BackColor = Color.White;
				m_textBoxReceiveFormules = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_textBoxReceiveFormules.BackColor = Color.LightGreen;
			}

		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_textBoxReceiveFormules != null )
				m_wndAideFormule.InsereInTextBox ( m_textBoxReceiveFormules, nPosCurseur, strCommande );
		}

		/// //////////////////////////////////////////////////////////
		private CResultAErreur FillVariable ( CVariableDynamiqueSelectionObjetDonnee variable )
		{
			CResultAErreur result = CResultAErreur.True;
			CResultAErreur resultTmp = CResultAErreur.True;

			m_txtNomVariable.Text = m_txtNomVariable.Text.Trim();
			
			if ( m_txtNomVariable.Text == "" )
				result.EmpileErreur(I.T("Incorrect variable name|30030"));
			
			resultTmp = m_filtreDynamique.VerifieIntegrite();
			if ( !resultTmp )
			{
				resultTmp.EmpileErreur(I.T("Error in the filter|30031"));
				result &= resultTmp;
			}

			C2iExpression expressionValeurAffichee = null;

			C2iExpression expressionValeurRetournee = null;
			if (m_filtreDynamique.TypeElements != null)
			{
				

				CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression(new CFournisseurPropDynStd(true), m_filtreDynamique.TypeElements);
				CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
				resultTmp = analyseur.AnalyseChaine(m_txtValeurAffichee.Text);
				if (!resultTmp)
				{
					resultTmp.EmpileErreur(I.T("Error in displayed value formula|30032"));
					result &= resultTmp;
				}
				else
					expressionValeurAffichee = (C2iExpression)resultTmp.Data;

				
				resultTmp = analyseur.AnalyseChaine(m_txtValeurStockee.Text);
				if (!resultTmp)
				{
					resultTmp.EmpileErreur(I.T("Error in returned value formula|30033"));
					result &= resultTmp;
				}
				else
					expressionValeurRetournee = (C2iExpression)resultTmp.Data;
			}
			if ( result )
			{
				variable.FiltreSelection = m_filtreDynamique;
				variable.ExpressionAffichee = expressionValeurAffichee;
				variable.ExpressionRetournee = expressionValeurRetournee;
				variable.Nom = m_txtNomVariable.Text;
				variable.Description = "";
				variable.CanBeNull = m_chkCanBeNull.Checked;
				variable.TextNull = m_txtNullValue.Text;
				variable.UtiliserRechercheRapide = m_chkRechercheRapide.Checked;
			}
			return result;

		}

		/// <summary>
		/// //////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_variable.ElementAVariables);
			CResultAErreur result = FillVariable ( variable );
			if ( !result )
			{
				CFormAlerte.Afficher ( result);
				return;
			}
			FillVariable ( m_variable );
			DialogResult = DialogResult.OK;
			Close();

				
		}

		private void m_lnkTester_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_variable.ElementAVariables);
			CResultAErreur result = FillVariable ( variable );
			if ( !result )
			{
				CFormAlerte.Afficher ( result);
				return;
			}
			object[] sels = null;
			CFormSelectionFromVariableSelectionObjetDonnee.Selectionne ( variable, ref sels );
		}

		private void m_chkCanBeNull_CheckedChanged(object sender, System.EventArgs e)
		{
			m_txtNullValue.Visible = m_chkCanBeNull.Checked;
		}




        #region IFormEditVariableDynamique Membres

        public bool EditeLaVariable(IVariableDynamique variable, IElementAVariablesDynamiquesBase eltAVariables)
        {
            Init(variable as CVariableDynamiqueSelectionObjetDonnee);
            bool bResult = ShowDialog() == DialogResult.OK;
            Dispose();
            return bResult;
        }

        #endregion
    }
}
