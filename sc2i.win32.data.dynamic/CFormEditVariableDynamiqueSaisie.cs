using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.win32.common;
using sc2i.common.unites;
using System.Collections.Generic;
using sc2i.win32.expression.variablesDynamiques;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditVariableDynamiqueSaisie.
	/// </summary>
    [AutoExec("Autoexec")]
    public class CFormEditVariableDynamiqueSaisie : System.Windows.Forms.Form, IFormEditVariableDynamique
	{
		private const string c_strColValeurAffichee = "Valeur affichée";
		private const string c_strColValeurStockee = "Valeur stockée";
		private const string c_nomTableValeurs = "VALEURS";


		private IElementAVariablesDynamiquesBase m_elementAVariables = null;
		private CVariableDynamiqueSaisie m_variable = null;
		

		private sc2i.win32.expression.CControleEditeFormule m_textBoxFormule = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_txtNomVariable;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iComboBox m_cmbType;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private System.Windows.Forms.Label label5;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox m_txtTest;
		private System.Windows.Forms.Button m_btnTester;
		private System.Windows.Forms.TextBox m_txtDescriptionFormat;
		private sc2i.win32.common.C2iPanelOmbre m_panelValeursPossibles;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DataGrid m_gridValeurs;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre3;
		private System.Windows.Forms.Label label8;
		private sc2i.win32.expression.CControleEditeFormule m_txtValeurParDefaut;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleValidation;
        private CExtStyle cExtStyle1;
        private SplitContainer m_splitContainer;
        private Panel m_panelUnite;
        private C2iTextBox m_txtFormatUnite;
        private Label label15;
        private CComboboxAutoFilled m_cmbSelectClasseUnite;
        private Label label14;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditVariableDynamiqueSaisie()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtNomVariable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cmbType = new sc2i.win32.common.C2iComboBox();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtFormuleValidation = new sc2i.win32.expression.CControleEditeFormule();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c2iPanelOmbre3 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtValeurParDefaut = new sc2i.win32.expression.CControleEditeFormule();
            this.label8 = new System.Windows.Forms.Label();
            this.m_panelValeursPossibles = new sc2i.win32.common.C2iPanelOmbre();
            this.label7 = new System.Windows.Forms.Label();
            this.m_gridValeurs = new System.Windows.Forms.DataGrid();
            this.c2iPanelOmbre2 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_btnTester = new System.Windows.Forms.Button();
            this.m_txtTest = new System.Windows.Forms.TextBox();
            this.m_txtDescriptionFormat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_panelUnite = new System.Windows.Forms.Panel();
            this.m_txtFormatUnite = new sc2i.win32.common.C2iTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.m_cmbSelectClasseUnite = new sc2i.win32.common.CComboboxAutoFilled();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.c2iPanelOmbre3.SuspendLayout();
            this.m_panelValeursPossibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridValeurs)).BeginInit();
            this.c2iPanelOmbre2.SuspendLayout();
            this.c2iPanelOmbre1.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.m_panelUnite.SuspendLayout();
            this.SuspendLayout();
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
            this.label1.Text = "User-defined variable|146";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable name|143";
            // 
            // m_txtNomVariable
            // 
            this.m_txtNomVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomVariable.Location = new System.Drawing.Point(112, 25);
            this.m_txtNomVariable.Name = "m_txtNomVariable";
            this.m_txtNomVariable.Size = new System.Drawing.Size(392, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNomVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomVariable.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 21);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data type|147";
            // 
            // m_cmbType
            // 
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(112, 47);
            this.m_cmbType.LockEdition = false;
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(392, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbType.TabIndex = 1;
            this.m_cmbType.SelectedValueChanged += new System.EventHandler(this.m_cmbType_SelectedValueChanged);
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(0, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(191, 461);
            this.cExtStyle1.SetStyleBackColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAide.TabIndex = 5;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 13);
            this.cExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 6;
            this.label4.Text = "Validation formula|148";
            // 
            // m_txtFormuleValidation
            // 
            this.m_txtFormuleValidation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleValidation.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleValidation.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleValidation.Formule = null;
            this.m_txtFormuleValidation.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormuleValidation.LockEdition = false;
            this.m_txtFormuleValidation.Name = "m_txtFormuleValidation";
            this.m_txtFormuleValidation.Size = new System.Drawing.Size(240, 48);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormuleValidation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormuleValidation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleValidation.TabIndex = 0;
            this.m_txtFormuleValidation.Enter += new System.EventHandler(this.OnEnterTextBoxFormule);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.c2iPanelOmbre3);
            this.panel1.Controls.Add(this.m_panelValeursPossibles);
            this.panel1.Controls.Add(this.c2iPanelOmbre2);
            this.panel1.Controls.Add(this.c2iPanelOmbre1);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 461);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 8;
            // 
            // c2iPanelOmbre3
            // 
            this.c2iPanelOmbre3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre3.Controls.Add(this.m_txtValeurParDefaut);
            this.c2iPanelOmbre3.Controls.Add(this.label8);
            this.c2iPanelOmbre3.Location = new System.Drawing.Point(8, 320);
            this.c2iPanelOmbre3.LockEdition = false;
            this.c2iPanelOmbre3.Name = "c2iPanelOmbre3";
            this.c2iPanelOmbre3.Size = new System.Drawing.Size(528, 104);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre3, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre3.TabIndex = 3;
            // 
            // m_txtValeurParDefaut
            // 
            this.m_txtValeurParDefaut.BackColor = System.Drawing.Color.White;
            this.m_txtValeurParDefaut.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtValeurParDefaut.Formule = null;
            this.m_txtValeurParDefaut.Location = new System.Drawing.Point(8, 24);
            this.m_txtValeurParDefaut.LockEdition = false;
            this.m_txtValeurParDefaut.Name = "m_txtValeurParDefaut";
            this.m_txtValeurParDefaut.Size = new System.Drawing.Size(496, 56);
            this.cExtStyle1.SetStyleBackColor(this.m_txtValeurParDefaut, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtValeurParDefaut, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeurParDefaut.TabIndex = 4;
            this.m_txtValeurParDefaut.Enter += new System.EventHandler(this.OnEnterTextBoxFormule);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 16);
            this.cExtStyle1.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 3;
            this.label8.Text = "Default value|152";
            // 
            // m_panelValeursPossibles
            // 
            this.m_panelValeursPossibles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelValeursPossibles.Controls.Add(this.label7);
            this.m_panelValeursPossibles.Controls.Add(this.m_gridValeurs);
            this.m_panelValeursPossibles.Location = new System.Drawing.Point(280, 120);
            this.m_panelValeursPossibles.LockEdition = false;
            this.m_panelValeursPossibles.Name = "m_panelValeursPossibles";
            this.m_panelValeursPossibles.Size = new System.Drawing.Size(256, 200);
            this.cExtStyle1.SetStyleBackColor(this.m_panelValeursPossibles, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelValeursPossibles, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelValeursPossibles.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.cExtStyle1.SetStyleBackColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label7.TabIndex = 19;
            this.label7.Text = "Possible values|149";
            // 
            // m_gridValeurs
            // 
            this.m_gridValeurs.BackgroundColor = System.Drawing.Color.White;
            this.m_gridValeurs.CaptionVisible = false;
            this.m_gridValeurs.DataMember = "";
            this.m_gridValeurs.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_gridValeurs.Location = new System.Drawing.Point(8, 24);
            this.m_gridValeurs.Name = "m_gridValeurs";
            this.m_gridValeurs.PreferredRowHeight = 20;
            this.m_gridValeurs.Size = new System.Drawing.Size(224, 152);
            this.cExtStyle1.SetStyleBackColor(this.m_gridValeurs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_gridValeurs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_gridValeurs.TabIndex = 0;
            // 
            // c2iPanelOmbre2
            // 
            this.c2iPanelOmbre2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre2.Controls.Add(this.m_btnTester);
            this.c2iPanelOmbre2.Controls.Add(this.m_txtTest);
            this.c2iPanelOmbre2.Controls.Add(this.label4);
            this.c2iPanelOmbre2.Controls.Add(this.m_txtFormuleValidation);
            this.c2iPanelOmbre2.Controls.Add(this.m_txtDescriptionFormat);
            this.c2iPanelOmbre2.Controls.Add(this.label6);
            this.c2iPanelOmbre2.Controls.Add(this.label5);
            this.c2iPanelOmbre2.Location = new System.Drawing.Point(8, 120);
            this.c2iPanelOmbre2.LockEdition = false;
            this.c2iPanelOmbre2.Name = "c2iPanelOmbre2";
            this.c2iPanelOmbre2.Size = new System.Drawing.Size(272, 200);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre2.TabIndex = 1;
            // 
            // m_btnTester
            // 
            this.m_btnTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTester.Location = new System.Drawing.Point(200, 150);
            this.m_btnTester.Name = "m_btnTester";
            this.m_btnTester.Size = new System.Drawing.Size(48, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnTester, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnTester, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnTester.TabIndex = 3;
            this.m_btnTester.Text = "Test|25";
            this.m_btnTester.Click += new System.EventHandler(this.m_btnTester_Click);
            // 
            // m_txtTest
            // 
            this.m_txtTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtTest.Location = new System.Drawing.Point(80, 152);
            this.m_txtTest.Name = "m_txtTest";
            this.m_txtTest.Size = new System.Drawing.Size(120, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtTest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtTest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtTest.TabIndex = 2;
            // 
            // m_txtDescriptionFormat
            // 
            this.m_txtDescriptionFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDescriptionFormat.Location = new System.Drawing.Point(8, 96);
            this.m_txtDescriptionFormat.Multiline = true;
            this.m_txtDescriptionFormat.Name = "m_txtDescriptionFormat";
            this.m_txtDescriptionFormat.Size = new System.Drawing.Size(240, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_txtDescriptionFormat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtDescriptionFormat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtDescriptionFormat.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(5, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 24);
            this.cExtStyle1.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 17;
            this.label6.Text = "Test zone|151";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(8, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 16);
            this.cExtStyle1.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 15;
            this.label5.Text = "Format error message|150";
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_panelUnite);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtNomVariable);
            this.c2iPanelOmbre1.Controls.Add(this.m_cmbType);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label3);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 8);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(528, 112);
            this.cExtStyle1.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 0;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(291, 431);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(163, 431);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.panel1);
            this.cExtStyle1.SetStyleBackColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAide);
            this.cExtStyle1.SetStyleBackColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.Size = new System.Drawing.Size(745, 465);
            this.m_splitContainer.SplitterDistance = 546;
            this.cExtStyle1.SetStyleBackColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.TabIndex = 9;
            // 
            // m_panelUnite
            // 
            this.m_panelUnite.Controls.Add(this.m_txtFormatUnite);
            this.m_panelUnite.Controls.Add(this.label15);
            this.m_panelUnite.Controls.Add(this.m_cmbSelectClasseUnite);
            this.m_panelUnite.Controls.Add(this.label14);
            this.m_panelUnite.Location = new System.Drawing.Point(20, 71);
            this.m_panelUnite.Name = "m_panelUnite";
            this.m_panelUnite.Size = new System.Drawing.Size(407, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_panelUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelUnite.TabIndex = 19;
            // 
            // m_txtFormatUnite
            // 
            this.m_txtFormatUnite.Location = new System.Drawing.Point(312, 0);
            this.m_txtFormatUnite.LockEdition = false;
            this.m_txtFormatUnite.Name = "m_txtFormatUnite";
            this.m_txtFormatUnite.Size = new System.Drawing.Size(92, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtFormatUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtFormatUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormatUnite.TabIndex = 22;
            this.m_txtFormatUnite.Text = "[FormatAffichageUnite]";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(216, 2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 19);
            this.cExtStyle1.SetStyleBackColor(this.label15, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label15, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label15.TabIndex = 21;
            this.label15.Text = "Display format|20088";
            // 
            // m_cmbSelectClasseUnite
            // 
            this.m_cmbSelectClasseUnite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSelectClasseUnite.FormattingEnabled = true;
            this.m_cmbSelectClasseUnite.IsLink = false;
            this.m_cmbSelectClasseUnite.ListDonnees = null;
            this.m_cmbSelectClasseUnite.Location = new System.Drawing.Point(93, 0);
            this.m_cmbSelectClasseUnite.LockEdition = false;
            this.m_cmbSelectClasseUnite.Name = "m_cmbSelectClasseUnite";
            this.m_cmbSelectClasseUnite.NullAutorise = true;
            this.m_cmbSelectClasseUnite.ProprieteAffichee = null;
            this.m_cmbSelectClasseUnite.Size = new System.Drawing.Size(121, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbSelectClasseUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbSelectClasseUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbSelectClasseUnite.TabIndex = 20;
            this.m_cmbSelectClasseUnite.TextNull = "(empty)";
            this.m_cmbSelectClasseUnite.Tri = true;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 19);
            this.cExtStyle1.SetStyleBackColor(this.label14, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label14, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label14.TabIndex = 19;
            this.label14.Text = "Unit type|20087";
            // 
            // CFormEditVariableDynamiqueSaisie
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(745, 465);
            this.Controls.Add(this.m_splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditVariableDynamiqueSaisie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Filter variable|145";
            this.Load += new System.EventHandler(this.CFormEditVariableDynamiqueSaisie_Load);
            this.panel1.ResumeLayout(false);
            this.c2iPanelOmbre3.ResumeLayout(false);
            this.c2iPanelOmbre3.PerformLayout();
            this.m_panelValeursPossibles.ResumeLayout(false);
            this.m_panelValeursPossibles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridValeurs)).EndInit();
            this.c2iPanelOmbre2.ResumeLayout(false);
            this.c2iPanelOmbre2.PerformLayout();
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.m_panelUnite.ResumeLayout(false);
            this.m_panelUnite.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        /// //////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireEditeursVariablesDynamiques.RegisterEditeur(typeof(CVariableDynamiqueSaisie),typeof(CFormEditVariableDynamiqueSaisie));
        }

		/// //////////////////////////////////////////////////////
		private void CFormEditVariableDynamiqueSaisie_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
                        
            m_cmbType.Items.Clear();

            List<C2iTypeDonnee> possibles = new List<C2iTypeDonnee>();
            possibles.Add(new C2iTypeDonnee(TypeDonnee.tString));
            possibles.Add(new C2iTypeDonnee(TypeDonnee.tEntier));
            possibles.Add(new C2iTypeDonnee(TypeDonnee.tDouble));
            possibles.Add(new C2iTypeDonnee(TypeDonnee.tDate));
            possibles.Add(new C2iTypeDonnee(TypeDonnee.tBool));
            
			m_cmbType.Items.AddRange(possibles.ToArray());
			m_wndAide.FournisseurProprietes = new CFournisseurPropDynStd(true);
			m_wndAide.ObjetInterroge = typeof(CObjetForTestValeurChampCustomString);

			m_txtNomVariable.Text = m_variable.Nom;
			m_cmbType.SelectedItem = m_variable.TypeDonnee2i;
			m_txtFormuleValidation.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtFormuleValidation.Text = m_variable.ExpressionValidation.GetString();
			m_txtDescriptionFormat.Text = m_variable.DescriptionFormat;

			m_txtValeurParDefaut.Init ( m_wndAide.FournisseurProprietes, null );

			DataTable table = new DataTable(c_nomTableValeurs);
			table.Columns.Add(c_strColValeurStockee,typeof(string));
			table.Columns.Add(c_strColValeurAffichee,typeof(string));

			foreach(CValeurVariableDynamiqueSaisie val in m_variable.Valeurs)
			{
				DataRow row = table.NewRow();
				row[c_strColValeurStockee] = val.Value;
				row[c_strColValeurAffichee] = val.Display;
				table.Rows.Add ( row );
			}

			if (m_gridValeurs.TableStyles[c_nomTableValeurs] == null)
			{
				DataGridTableStyle tableStyle = new DataGridTableStyle();
				tableStyle.MappingName = CValeurChampCustom.c_nomTable;

				DataGridTextBoxColumn colStyleValue = new DataGridTextBoxColumn();
				colStyleValue.MappingName = c_strColValeurStockee;
				colStyleValue.HeaderText  = c_strColValeurStockee;
				colStyleValue.Width = m_gridValeurs.Width *1/3;
				colStyleValue.ReadOnly = false;

				DataGridTextBoxColumn colStyleDisplay = new DataGridTextBoxColumn();
				colStyleDisplay.MappingName = c_strColValeurAffichee;
				colStyleDisplay.HeaderText = c_strColValeurAffichee;
				colStyleDisplay.Width = m_gridValeurs.Width *1/3;
				colStyleDisplay.ReadOnly = false;

				tableStyle.RowHeadersVisible = true;

				tableStyle.GridColumnStyles.AddRange( new DataGridColumnStyle[] {colStyleValue,colStyleDisplay} );

				m_gridValeurs.TableStyles.Add(tableStyle);
			}

			table.DefaultView.AllowNew = true;
			table.DefaultView.AllowDelete = true;
			
			m_gridValeurs.DataSource = table;

			if ( m_variable.ExpressionValeurParDefaut == null )
				m_txtValeurParDefaut.Text = "";
			else
				m_txtValeurParDefaut.Text = m_variable.ExpressionValeurParDefaut.GetString();

            m_cmbSelectClasseUnite.ListDonnees = CGestionnaireUnites.Classes;
            m_cmbSelectClasseUnite.ProprieteAffichee = "Libelle";
            m_cmbSelectClasseUnite.SelectedValue = m_variable.ClasseUnite;

            m_txtFormatUnite.Text = m_variable.FormatAffichageUnite;

            UpdateAffichagePanelUnite();
		}


        /// //////////////////////////////////////////////////////
        private void UpdateAffichagePanelUnite()
        {
            C2iTypeDonnee type = m_cmbType.SelectedItem as C2iTypeDonnee;
            m_panelUnite.Visible = type != null && type.TypeDonnee == TypeDonnee.tDouble;
        }

		/// //////////////////////////////////////////////////////
		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_textBoxFormule != null )
				m_wndAide.InsereInTextBox ( m_textBoxFormule, nPosCurseur, strCommande );
		}

		/// //////////////////////////////////////////////////////
		private void Init ( CVariableDynamiqueSaisie variable, IElementAVariablesDynamiquesBase filtre )
		{
			m_variable = variable;
			m_elementAVariables = filtre;
		}

		/// //////////////////////////////////////////////////////
		public static bool EditeVariable ( CVariableDynamiqueSaisie variable, IElementAVariablesDynamiquesBase filtre )
		{
			CFormEditVariableDynamiqueSaisie form = new CFormEditVariableDynamiqueSaisie();
			form.Init ( variable, filtre );
			Boolean bOk = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bOk;
		}

		/// //////////////////////////////////////////////////////
		protected C2iExpression GetExpression()
		{
			if ( m_cmbType.SelectedItem == null || !(m_cmbType.SelectedItem is C2iTypeDonnee))
			{
				CFormAlerte.Afficher(I.T("Enter a data type|30023"), EFormAlerteType.Exclamation);
				return null;
			}
			C2iTypeDonnee typeDonnees = (C2iTypeDonnee)m_cmbType.SelectedItem;
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression( new CFournisseurPropDynStd(true), CObjetForTestValeurChampCustom.GetNewForTypeDonnee(typeDonnees.TypeDonnee, null, null ).GetType());
			CAnalyseurSyntaxique analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			CResultAErreur result = analyseur.AnalyseChaine ( m_txtFormuleValidation.Text );
			if ( !result )
			{
				CFormAlerte.Afficher ( result);
				return null;
			}
			return (C2iExpression)result.Data;
		}

		/// //////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_cmbType.SelectedItem == null || !(m_cmbType.SelectedItem is C2iTypeDonnee))
			{
				CFormAlerte.Afficher(I.T("Enter a data type|30023"), EFormAlerteType.Exclamation);
				return;
			}
			C2iExpression expression = GetExpression();
			if ( expression == null )
				return;

			DataTable table = (DataTable)m_gridValeurs.DataSource;
			//Vérifie que toutes les données sont bien du type
			C2iTypeDonnee typeDonnees = (C2iTypeDonnee)m_cmbType.SelectedItem;
			ArrayList lstValeurs = new ArrayList();
			foreach ( DataRow row in new ArrayList(table.Rows) )
			{
				if ( !typeDonnees.IsDuBonType ( typeDonnees.ObjectToType(row[c_strColValeurStockee], null) ))
				{
					CFormAlerte.Afficher(I.T("The value @1 is not from type @2|30023", row[c_strColValeurStockee].ToString(),typeDonnees.Libelle), EFormAlerteType.Erreur);
					return;
				}
				object val = typeDonnees.StringToType(row[c_strColValeurStockee].ToString(), null );
				lstValeurs.Add ( new CValeurVariableDynamiqueSaisie( val, (string)row[c_strColValeurAffichee] ));
			}

            if (typeDonnees.TypeDonnee == TypeDonnee.tDouble)
            {
                IClasseUnite classe = m_cmbSelectClasseUnite.SelectedValue as IClasseUnite;
                m_variable.ClasseUnite = classe;
                m_variable.FormatAffichageUnite = m_txtFormatUnite.Text;
            }
            else
            {
                m_variable.ClasseUnite = null;
                m_variable.FormatAffichageUnite = "";
            }


			m_variable.Nom = m_txtNomVariable.Text.Replace(" ","_").Trim();
			m_variable.TypeDonnee2i = (C2iTypeDonnee)m_cmbType.SelectedItem;
			
			m_variable.ExpressionValidation = expression;
			m_variable.DescriptionFormat = m_txtDescriptionFormat.Text;
			m_variable.Valeurs.Clear();
			foreach ( CValeurVariableDynamiqueSaisie valeur in lstValeurs )
			{
				m_variable.Valeurs.Add ( valeur );
			}

			if ( m_txtValeurParDefaut.Text.Trim() == "" )
				m_variable.ExpressionValeurParDefaut = null;
			else
			{
				CContexteAnalyse2iExpression ctxAnalyse = new CContexteAnalyse2iExpression ( new CFournisseurPropDynStd(), null );
				CResultAErreur resultExp = new CAnalyseurSyntaxiqueExpression(ctxAnalyse).AnalyseChaine ( m_txtValeurParDefaut.Text );
				if ( !resultExp )
				{
					resultExp.EmpileErreur(I.T("Error in default value formula|30024"));
					CFormAlerte.Afficher(resultExp);
					return;
				}
				m_variable.ExpressionValeurParDefaut = (C2iExpression)resultExp.Data;
			}

			m_elementAVariables.OnChangeVariable ( m_variable );
			DialogResult = DialogResult.OK;
			Close();
		}

		/// //////////////////////////////////////////////////////
		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
		
		}

		/// //////////////////////////////////////////////////////
		private void m_btnTester_Click(object sender, System.EventArgs e)
		{
			C2iExpression expression = GetExpression();
			if ( expression == null )
				return;
			if ( m_cmbType.SelectedItem == null || !(m_cmbType.SelectedItem is C2iTypeDonnee) )
			{
				CFormAlerte.Afficher(I.T("Select a data type|30026"), EFormAlerteType.Exclamation);
				return;
			}
			TypeDonnee tp = ((C2iTypeDonnee)m_cmbType.SelectedItem).TypeDonnee;
			object obj = CObjetForTestValeurChampCustom .GetNewForTypeDonnee ( 	tp,
				null, m_txtTest.Text );
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( obj );
			CResultAErreur result = expression.Eval ( ctx );
			if ( !result )
			{
                result.EmpileErreur(I.T("Error during validation formula evaluation|30027"));
				CFormAlerte.Afficher ( result);
				return;
			}
			if ( (result.Data is bool && (bool)result.Data) || result.Data.ToString() =="1" )
				CFormAlerte.Afficher(I.T("Accepted value|30028"));
			else
				CFormAlerte.Afficher(I.T("Rejected value|30029"), EFormAlerteType.Erreur);

		}

		/// //////////////////////////////////////////////////////////////////////
		private void OnEnterTextBoxFormule(object sender, System.EventArgs e)
		{
			if ( sender is sc2i.win32.expression.CControleEditeFormule )
			{
				if ( m_textBoxFormule != null )
					m_textBoxFormule.BackColor = Color.White;
				m_textBoxFormule = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_textBoxFormule.BackColor = Color.LightGreen;
				if ( m_textBoxFormule == m_txtFormuleValidation )
					m_wndAide.ObjetInterroge = typeof(CObjetForTestValeurChampCustomString);
				else
					m_wndAide.ObjetInterroge = null;
			}
		}

        private void m_cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateAffichagePanelUnite();            
        }


        #region IFormEditVariableDynamique Membres

        public bool EditeLaVariable(IVariableDynamique variable, IElementAVariablesDynamiquesBase eltAVariables)
        {
            Init(variable as CVariableDynamiqueSaisie, eltAVariables);
            bool bResult = ShowDialog() == DialogResult.OK;
            Dispose();
            return bResult;
        }

        #endregion
    }
}
