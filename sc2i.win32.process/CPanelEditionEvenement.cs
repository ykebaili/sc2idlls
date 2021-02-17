using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.win32.data.navigation;
using sc2i.common;
using sc2i.win32.data;
using sc2i.win32.common;
using sc2i.process;
using sc2i.win32.expression;
using sc2i.expression;

namespace sc2i.win32.process
{
	public class CPanelEditionEvenement : UserControl, IControlALockEdition
	{
        private CEvenement m_evenement = null;
		private bool m_bComboTypeInitialized = false;

		public CPanelEditionEvenement()
			: base()
		{
			InitializeComponent();
		}

		#region Designer generated code

        private CExtLinkField m_extLinkField;
        private CExtStyle m_extStyle;
        private CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.common.C2iTextBox m_txtLibelle;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre4;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label7;
		private sc2i.win32.common.C2iComboBox m_cmbTypeElements;
		private sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees m_cmbProcess;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel m_lnkDefinisseur;
		private System.Windows.Forms.LinkLabel m_lnkGererProcess;
		private sc2i.win32.common.C2iTabControl m_tabControl;
		private Crownwood.Magic.Controls.TabPage tabPage1;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private sc2i.win32.process.CPanelEditParametreDeclencheur m_panelDeclencheur;		
		private sc2i.win32.common.C2iPanel c2iPanel1;
		private sc2i.win32.common.C2iPanel c2iPanel2;
		private sc2i.win32.common.C2iPanel c2iPanel3;
		private sc2i.win32.common.C2iPanel c2iPanel4;
		private sc2i.win32.common.C2iPanel c2iPanel5;
		private sc2i.win32.common.C2iPanel c2iPanel6;
		private sc2i.win32.common.C2iPanel c2iPanel7;
		private sc2i.win32.common.C2iPanel c2iPanel8;
		private sc2i.win32.process.CProcessEditor m_processEditor;
		private System.Windows.Forms.CheckBox m_chkAsynchrone;
		private System.Windows.Forms.CheckBox m_chkDeclenchementUnique;
		private System.Windows.Forms.CheckBox m_chkTableau;
		private System.Windows.Forms.CheckBox m_chkDansContexteClient;
		private sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees m_cmbGroupeParametrage;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components = null;



		//-------------------------------------------------------------------------
		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtLibelle = new sc2i.win32.common.C2iTextBox();
            this.c2iPanelOmbre4 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_lnkDefinisseur = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbTypeElements = new sc2i.win32.common.C2iComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_chkTableau = new System.Windows.Forms.CheckBox();
            this.m_chkDansContexteClient = new System.Windows.Forms.CheckBox();
            this.m_cmbProcess = new sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees();
            this.m_lnkGererProcess = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_cmbGroupeParametrage = new sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees();
            this.m_chkDeclenchementUnique = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_chkAsynchrone = new System.Windows.Forms.CheckBox();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_processEditor = new sc2i.win32.process.CProcessEditor();
            this.m_panelDeclencheur = new sc2i.win32.process.CPanelEditParametreDeclencheur();
            this.c2iPanel1 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel2 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel3 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel4 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel5 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel6 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel7 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel8 = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanelOmbre4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_panelDeclencheur.SuspendLayout();
            this.c2iPanel1.SuspendLayout();
            this.c2iPanel2.SuspendLayout();
            this.c2iPanel3.SuspendLayout();
            this.c2iPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.m_extLinkField.SetLinkField(this.label1, "");
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 4002;
            this.label1.Text = "Label|50";
            // 
            // m_txtLibelle
            // 
            this.m_extLinkField.SetLinkField(this.m_txtLibelle, "Libelle");
            this.m_txtLibelle.Location = new System.Drawing.Point(112, 5);
            this.m_txtLibelle.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtLibelle, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtLibelle.Name = "m_txtLibelle";
            this.m_txtLibelle.Size = new System.Drawing.Size(480, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelle.TabIndex = 0;
            this.m_txtLibelle.Text = "[Libelle]";
            // 
            // c2iPanelOmbre4
            // 
            this.c2iPanelOmbre4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre4.Controls.Add(this.m_lnkDefinisseur);
            this.c2iPanelOmbre4.Controls.Add(this.label2);
            this.c2iPanelOmbre4.Controls.Add(this.m_cmbTypeElements);
            this.c2iPanelOmbre4.Controls.Add(this.m_txtLibelle);
            this.c2iPanelOmbre4.Controls.Add(this.label1);
            this.c2iPanelOmbre4.Controls.Add(this.label7);
            this.c2iPanelOmbre4.Controls.Add(this.m_chkTableau);
            this.c2iPanelOmbre4.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.c2iPanelOmbre4, "");
            this.c2iPanelOmbre4.Location = new System.Drawing.Point(8, 8);
            this.c2iPanelOmbre4.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanelOmbre4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanelOmbre4.Name = "c2iPanelOmbre4";
            this.c2iPanelOmbre4.Size = new System.Drawing.Size(612, 96);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre4, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre4, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre4.TabIndex = 0;
            // 
            // m_lnkDefinisseur
            // 
            this.m_lnkDefinisseur.BackColor = System.Drawing.Color.White;
            this.m_lnkDefinisseur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_extLinkField.SetLinkField(this.m_lnkDefinisseur, "");
            this.m_lnkDefinisseur.Location = new System.Drawing.Point(112, 52);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkDefinisseur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkDefinisseur.Name = "m_lnkDefinisseur";
            this.m_lnkDefinisseur.Size = new System.Drawing.Size(480, 20);
            this.m_extStyle.SetStyleBackColor(this.m_lnkDefinisseur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lnkDefinisseur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkDefinisseur.TabIndex = 4007;
            this.m_lnkDefinisseur.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkDefinisseur_LinkClicked);
            // 
            // label2
            // 
            this.m_extLinkField.SetLinkField(this.label2, "");
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 4006;
            this.label2.Text = "Belongs to|20091";
            // 
            // m_cmbTypeElements
            // 
            this.m_cmbTypeElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeElements.IsLink = false;
            this.m_extLinkField.SetLinkField(this.m_cmbTypeElements, "");
            this.m_cmbTypeElements.Location = new System.Drawing.Point(112, 28);
            this.m_cmbTypeElements.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeElements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeElements.Name = "m_cmbTypeElements";
            this.m_cmbTypeElements.Size = new System.Drawing.Size(360, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeElements.TabIndex = 4004;
            this.m_cmbTypeElements.SelectedIndexChanged += new System.EventHandler(this.m_cmbTypeElements_SelectedIndexChanged);
            // 
            // label7
            // 
            this.m_extLinkField.SetLinkField(this.label7, "");
            this.label7.Location = new System.Drawing.Point(8, 30);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label7, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.m_extStyle.SetStyleBackColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label7.TabIndex = 4003;
            this.label7.Text = "Target Type|20089";
            // 
            // m_chkTableau
            // 
            this.m_chkTableau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_extLinkField.SetLinkField(this.m_chkTableau, "");
            this.m_chkTableau.Location = new System.Drawing.Point(491, 31);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTableau, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTableau.Name = "m_chkTableau";
            this.m_chkTableau.Size = new System.Drawing.Size(101, 16);
            this.m_extStyle.SetStyleBackColor(this.m_chkTableau, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkTableau, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkTableau.TabIndex = 6;
            this.m_chkTableau.Text = "Array|20090";
            this.m_chkTableau.CheckedChanged += new System.EventHandler(this.m_chkTableau_CheckedChanged);
            // 
            // m_chkDansContexteClient
            // 
            this.m_extLinkField.SetLinkField(this.m_chkDansContexteClient, "DeclencherSurContexteClient");
            this.m_chkDansContexteClient.Location = new System.Drawing.Point(602, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkDansContexteClient, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkDansContexteClient.Name = "m_chkDansContexteClient";
            this.m_chkDansContexteClient.Size = new System.Drawing.Size(139, 16);
            this.m_extStyle.SetStyleBackColor(this.m_chkDansContexteClient, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkDansContexteClient, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkDansContexteClient.TabIndex = 6;
            this.m_chkDansContexteClient.Text = "Optimize launch|20096";
            // 
            // m_cmbProcess
            // 
            this.m_cmbProcess.ComportementLinkStd = true;
            this.m_cmbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbProcess.ElementSelectionne = null;
            this.m_cmbProcess.IsLink = true;
            this.m_extLinkField.SetLinkField(this.m_cmbProcess, "");
            this.m_cmbProcess.LinkProperty = "";
            this.m_cmbProcess.ListDonnees = null;
            this.m_cmbProcess.Location = new System.Drawing.Point(104, 6);
            this.m_cmbProcess.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbProcess, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbProcess.Name = "m_cmbProcess";
            this.m_cmbProcess.NullAutorise = true;
            this.m_cmbProcess.ProprieteAffichee = null;
            this.m_cmbProcess.ProprieteParentListeObjets = null;
            this.m_cmbProcess.SelectionneurParent = null;
            this.m_cmbProcess.Size = new System.Drawing.Size(424, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbProcess.TabIndex = 4004;
            this.m_cmbProcess.TextNull = "(Specific)";
            this.m_cmbProcess.Tri = true;
            this.m_cmbProcess.TypeFormEdition = null;
            this.m_cmbProcess.SelectedIndexChanged += new System.EventHandler(this.m_cmbProcess_SelectedIndexChanged);
            // 
            // m_lnkGererProcess
            // 
            this.m_extLinkField.SetLinkField(this.m_lnkGererProcess, "");
            this.m_lnkGererProcess.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkGererProcess, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkGererProcess.Name = "m_lnkGererProcess";
            this.m_lnkGererProcess.Size = new System.Drawing.Size(88, 16);
            this.m_extStyle.SetStyleBackColor(this.m_lnkGererProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lnkGererProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkGererProcess.TabIndex = 1;
            this.m_lnkGererProcess.TabStop = true;
            this.m_lnkGererProcess.Text = "Action|20097";
            this.m_lnkGererProcess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkGererProcess_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.c2iPanelOmbre4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_extLinkField.SetLinkField(this.panel2, "");
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(856, 104);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4002;
            // 
            // m_tabControl
            // 
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ControlBottomOffset = 16;
            this.m_tabControl.ControlRightOffset = 16;
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_extLinkField.SetLinkField(this.m_tabControl, "");
            this.m_tabControl.Location = new System.Drawing.Point(0, 104);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tabControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 1;
            this.m_tabControl.SelectedTab = this.tabPage2;
            this.m_tabControl.Size = new System.Drawing.Size(856, 497);
            this.m_extStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 1;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.tabPage1.Controls.Add(this.m_chkDansContexteClient);
            this.tabPage1.Controls.Add(this.m_cmbGroupeParametrage);
            this.tabPage1.Controls.Add(this.m_chkDeclenchementUnique);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.m_panelDeclencheur);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.tabPage1, "");
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(840, 456);
            this.m_extStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Event|20092";
            // 
            // m_cmbGroupeParametrage
            // 
            this.m_cmbGroupeParametrage.ComportementLinkStd = true;
            this.m_cmbGroupeParametrage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbGroupeParametrage.ElementSelectionne = null;
            this.m_cmbGroupeParametrage.IsLink = true;
            this.m_extLinkField.SetLinkField(this.m_cmbGroupeParametrage, "");
            this.m_cmbGroupeParametrage.LinkProperty = "";
            this.m_cmbGroupeParametrage.ListDonnees = null;
            this.m_cmbGroupeParametrage.Location = new System.Drawing.Point(136, 1);
            this.m_cmbGroupeParametrage.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbGroupeParametrage, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbGroupeParametrage.Name = "m_cmbGroupeParametrage";
            this.m_cmbGroupeParametrage.NullAutorise = true;
            this.m_cmbGroupeParametrage.ProprieteAffichee = null;
            this.m_cmbGroupeParametrage.ProprieteParentListeObjets = null;
            this.m_cmbGroupeParametrage.SelectionneurParent = null;
            this.m_cmbGroupeParametrage.Size = new System.Drawing.Size(240, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbGroupeParametrage, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbGroupeParametrage, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbGroupeParametrage.TabIndex = 6;
            this.m_cmbGroupeParametrage.TextNull = "(none)";
            this.m_cmbGroupeParametrage.Tri = true;
            this.m_cmbGroupeParametrage.TypeFormEdition = null;
            // 
            // m_chkDeclenchementUnique
            // 
            this.m_extLinkField.SetLinkField(this.m_chkDeclenchementUnique, "DeclenchementUniqueParEntite");
            this.m_chkDeclenchementUnique.Location = new System.Drawing.Point(382, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkDeclenchementUnique, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkDeclenchementUnique.Name = "m_chkDeclenchementUnique";
            this.m_chkDeclenchementUnique.Size = new System.Drawing.Size(200, 16);
            this.m_extStyle.SetStyleBackColor(this.m_chkDeclenchementUnique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkDeclenchementUnique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkDeclenchementUnique.TabIndex = 5;
            this.m_chkDeclenchementUnique.Text = "Only one trigger per target|20095";
            // 
            // label3
            // 
            this.m_extLinkField.SetLinkField(this.label3, "");
            this.label3.Location = new System.Drawing.Point(8, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 4006;
            this.label3.Text = "Setting group|20094";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_chkAsynchrone);
            this.tabPage2.Controls.Add(this.m_processEditor);
            this.tabPage2.Controls.Add(this.m_lnkGererProcess);
            this.tabPage2.Controls.Add(this.m_cmbProcess);
            this.m_extLinkField.SetLinkField(this.tabPage2, "");
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(840, 456);
            this.m_extStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Action|20093";
            // 
            // m_chkAsynchrone
            // 
            this.m_extLinkField.SetLinkField(this.m_chkAsynchrone, "ExecutionAsynchrone");
            this.m_chkAsynchrone.Location = new System.Drawing.Point(576, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkAsynchrone, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkAsynchrone.Name = "m_chkAsynchrone";
            this.m_chkAsynchrone.Size = new System.Drawing.Size(208, 16);
            this.m_extStyle.SetStyleBackColor(this.m_chkAsynchrone, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkAsynchrone, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkAsynchrone.TabIndex = 4006;
            this.m_chkAsynchrone.Text = "Asynchronous execution|20098";
            // 
            // m_processEditor
            // 
            this.m_processEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_processEditor.BackColor = System.Drawing.Color.White;
            this.m_processEditor.DisableTypeElement = false;
            this.m_processEditor.ForEvent = true;
            this.m_extLinkField.SetLinkField(this.m_processEditor, "");
            this.m_processEditor.Location = new System.Drawing.Point(0, 32);
            this.m_processEditor.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_processEditor, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_processEditor.Name = "m_processEditor";
            this.m_processEditor.Process = null;
            this.m_processEditor.Size = new System.Drawing.Size(840, 421);
            this.m_extStyle.SetStyleBackColor(this.m_processEditor, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_processEditor, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_processEditor.TabIndex = 4005;
            // 
            // m_panelDeclencheur
            // 
            this.m_panelDeclencheur.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelDeclencheur.AutoriseSurCreation = true;
            this.m_panelDeclencheur.AutoriseSurDate = true;
            this.m_panelDeclencheur.AutoriseSurManuel = true;
            this.m_panelDeclencheur.AutoriseSurModification = true;
            this.m_panelDeclencheur.AutoriseSurSuppression = true;
            this.m_panelDeclencheur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelDeclencheur.Controls.Add(this.c2iPanel1);
            this.m_panelDeclencheur.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.m_panelDeclencheur, "");
            this.m_panelDeclencheur.Location = new System.Drawing.Point(0, 22);
            this.m_panelDeclencheur.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDeclencheur, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelDeclencheur.Name = "m_panelDeclencheur";
            this.m_panelDeclencheur.Size = new System.Drawing.Size(840, 434);
            this.m_extStyle.SetStyleBackColor(this.m_panelDeclencheur, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_panelDeclencheur, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelDeclencheur.TabIndex = 4;
            this.m_panelDeclencheur.TypeCible = typeof(string);
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel1.Controls.Add(this.c2iPanel2);
            this.c2iPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_extLinkField.SetLinkField(this.c2iPanel1, "");
            this.c2iPanel1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel1.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(840, 434);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel1.TabIndex = 4004;
            // 
            // c2iPanel2
            // 
            this.c2iPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel2.Controls.Add(this.c2iPanel3);
            this.c2iPanel2.Controls.Add(this.c2iPanel8);
            this.m_extLinkField.SetLinkField(this.c2iPanel2, "");
            this.c2iPanel2.Location = new System.Drawing.Point(8, 40);
            this.c2iPanel2.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel2.Name = "c2iPanel2";
            this.c2iPanel2.Size = new System.Drawing.Size(1224, 394);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel2.TabIndex = 5;
            // 
            // c2iPanel3
            // 
            this.c2iPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel3.Controls.Add(this.c2iPanel4);
            this.c2iPanel3.Controls.Add(this.c2iPanel5);
            this.c2iPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_extLinkField.SetLinkField(this.c2iPanel3, "");
            this.c2iPanel3.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel3.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel3.Name = "c2iPanel3";
            this.c2iPanel3.Size = new System.Drawing.Size(1224, 120);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel3.TabIndex = 4;
            // 
            // c2iPanel4
            // 
            this.c2iPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_extLinkField.SetLinkField(this.c2iPanel4, "");
            this.c2iPanel4.Location = new System.Drawing.Point(0, 32);
            this.c2iPanel4.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel4.Name = "c2iPanel4";
            this.c2iPanel4.Size = new System.Drawing.Size(832, 88);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel4.TabIndex = 4005;
            // 
            // c2iPanel5
            // 
            this.c2iPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel5.Controls.Add(this.c2iPanel6);
            this.c2iPanel5.Controls.Add(this.c2iPanel7);
            this.m_extLinkField.SetLinkField(this.c2iPanel5, "");
            this.c2iPanel5.Location = new System.Drawing.Point(0, 32);
            this.c2iPanel5.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel5.Name = "c2iPanel5";
            this.c2iPanel5.Size = new System.Drawing.Size(832, 104);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel5.TabIndex = 4004;
            // 
            // c2iPanel6
            // 
            this.c2iPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_extLinkField.SetLinkField(this.c2iPanel6, "");
            this.c2iPanel6.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel6.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel6.Name = "c2iPanel6";
            this.c2iPanel6.Size = new System.Drawing.Size(576, 104);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel6.TabIndex = 1;
            // 
            // c2iPanel7
            // 
            this.c2iPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_extLinkField.SetLinkField(this.c2iPanel7, "");
            this.c2iPanel7.Location = new System.Drawing.Point(576, 0);
            this.c2iPanel7.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel7, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel7.Name = "c2iPanel7";
            this.c2iPanel7.Size = new System.Drawing.Size(256, 104);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel7.TabIndex = 1;
            // 
            // c2iPanel8
            // 
            this.c2iPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_extLinkField.SetLinkField(this.c2iPanel8, "");
            this.c2iPanel8.Location = new System.Drawing.Point(0, 120);
            this.c2iPanel8.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.c2iPanel8, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel8.Name = "c2iPanel8";
            this.c2iPanel8.Size = new System.Drawing.Size(192, 178);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel8.TabIndex = 1;
            // 
            // CPanelEditionEvenement
            // 
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this, "");
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditionEvenement";
            this.Size = new System.Drawing.Size(856, 601);
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.c2iPanelOmbre4.ResumeLayout(false);
            this.c2iPanelOmbre4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_panelDeclencheur.ResumeLayout(false);
            this.c2iPanel1.ResumeLayout(false);
            this.c2iPanel2.ResumeLayout(false);
            this.c2iPanel3.ResumeLayout(false);
            this.c2iPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		//-------------------------------------------------------------------------
		private CEvenement Evenement
		{
			get
			{
                return m_evenement;
			}
		}
		//-------------------------------------------------------------------------
		public CResultAErreur InitChamps(CEvenement evenement)
		{
            m_evenement = evenement;
            CResultAErreur result = CResultAErreur.True;
            result = m_extLinkField.FillDialogFromObjet(m_evenement);
			if ( Evenement.IsNew() )
			{
				m_gestionnaireModeEdition.SetModeEdition ( m_cmbTypeElements, TypeModeEdition.EnableSurEdition );
				m_cmbTypeElements.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
			}
			else
			{
				m_gestionnaireModeEdition.SetModeEdition ( m_cmbTypeElements, TypeModeEdition.Autonome);
				m_cmbTypeElements.LockEdition = true;
			}

            m_chkTableau.Checked = Evenement.Tableau;
            

			CObjetDonneeAIdNumerique definisseur = (CObjetDonneeAIdNumerique)Evenement.Definisseur;
			if ( definisseur == null )
			{
				m_lnkDefinisseur.Text = I.T( "All|913");
				m_lnkDefinisseur.Enabled = false;
			}
			else
			{
				m_lnkDefinisseur.Text = definisseur.DescriptionElement;
				m_lnkDefinisseur.Enabled = true;
			}


			InitComboBoxType();
			if ( Evenement.TypeCible != null )
				m_cmbTypeElements.SelectedValue = Evenement.TypeCible;

			CParametreDeclencheurEvenement parametre = Evenement.ParametreDeclencheur;

			m_panelDeclencheur.Init ( parametre );
			InitComboProcess();

			m_cmbProcess.SelectedValue = Evenement.ProcessInDbAssocie;
			
			m_processEditor.Process = Evenement.ProcessADeclencher;

			m_processEditor.DisableTypeElement = true;
			m_processEditor.SetTypeElement ( Evenement.TypeCible );

            CReferenceTypeFormBuiltIn refTypeForm = null;
            if (CFormNavigateur.FindNavigateur(this) != null)
            {
                refTypeForm = CFormFinder.GetRefFormToEdit(typeof(CGroupeParametrage)) as CReferenceTypeFormBuiltIn;
            }
			m_cmbGroupeParametrage.Init ( 
				typeof ( CGroupeParametrage ),
				null,
				"Libelle",
				refTypeForm != null?refTypeForm.TypeForm:null,
				false );
			m_cmbGroupeParametrage.ElementSelectionne = Evenement.GroupeParametrage;

			return result;
		}

		//-------------------------------------------------------------------------
		private void InitComboProcess()
		{
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( Evenement.ContexteDonnee, typeof(CProcessInDb));
			liste.Filtre = new CFiltreData ( 
				CProcessInDb.c_champTypeCible+" is null or " +
				CProcessInDb.c_champTypeCible+"=@1","");

			if ( m_cmbTypeElements.SelectedValue is Type && m_cmbTypeElements.SelectedValue != typeof(DBNull) )
			{
				Type  tp = (Type)m_cmbTypeElements.SelectedValue;
				liste.Filtre.Filtre += " or "+
					CProcessInDb.c_champTypeCible+" =@2";
				liste.Filtre.Parametres.Add ( tp.ToString() );
			}
            CReferenceTypeFormBuiltIn refTypeForm = null;
            if (CFormNavigateur.FindNavigateur(this) != null)
                refTypeForm = CFormFinder.GetRefFormToEdit(typeof(CProcessInDb)) as CReferenceTypeFormBuiltIn;
			m_cmbProcess.Init ( 
				liste,
				"Libelle",
				refTypeForm != null ? refTypeForm.TypeForm:null,
				true );
		}
				
		//-------------------------------------------------------------------------
		public CResultAErreur MAJ_Champs()
		{
            CResultAErreur result = m_extLinkField.FillObjetFromDialog(m_evenement);
			if ( !(m_cmbTypeElements.SelectedValue is Type ) || m_cmbTypeElements.SelectedValue == typeof(DBNull) )
			{
				result.EmpileErreur(I.T( "Select an element type|914"));
				return result;
			}
			Evenement.TypeCible = (Type)m_cmbTypeElements.SelectedValue;
            Evenement.Tableau = m_chkTableau.Checked;

			result = m_panelDeclencheur.MAJ_Champs();
			if ( !result )
				return result;
			Evenement.ParametreDeclencheur = m_panelDeclencheur.ParametreDeclencheur;


			result = Evenement.ParametreDeclencheur.VerifieDonnees();

			if ( !result )
				return result;

			Evenement.GroupeParametrage = (CGroupeParametrage)m_cmbGroupeParametrage.ElementSelectionne;

			if ( m_cmbProcess.SelectedValue is CProcessInDb )
				Evenement.ProcessInDbAssocie = (CProcessInDb)m_cmbProcess.SelectedValue;
			else
			{
				Evenement.ProcessInDbAssocie = null;
				Evenement.ProcessPropre = m_processEditor.Process;
			}

			//On le fait après le process, pour que la fonction DeclencherSurcontexteclient
			//Puisse vérifier que la valeur true (s'il y a lieu) est valide
			Evenement.DeclencherSurContexteClient = m_chkDansContexteClient.Checked;

			return result;
		}
		
		//-------------------------------------------------------------------------
		private CResultAErreur InitComboBoxType()
		{
			CResultAErreur result = CResultAErreur.True;
		
			if (m_bComboTypeInitialized)
				return result;

			ArrayList classes;
			if ( Evenement.Definisseur == null )
				classes = new ArrayList(DynamicClassAttribute.GetAllDynamicClass());
			else
			{
				Type[] types = Evenement.Definisseur.TypesCibleEvenement;
				classes = new ArrayList(types.Length);
				foreach ( Type tp in types )
					classes.Add(new CInfoClasseDynamique(tp, DynamicClassAttribute.GetNomConvivial ( tp )));
			}
            classes.Insert(0, new CInfoClasseDynamique(typeof(DBNull), "(none)|915"));
            m_cmbTypeElements.DataSource = null;
			m_cmbTypeElements.DataSource = classes;
			m_cmbTypeElements.ValueMember = "Classe";
			m_cmbTypeElements.DisplayMember = "Nom";

			m_bComboTypeInitialized = true;
			return result;
		}

		//-------------------------------------------------------------------------
		private void CFormEditionEvenement_Load(object sender, System.EventArgs e)
		{
		}
		

		//-------------------------------------------------------------------------
		private void m_cmbTypeElements_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( m_cmbTypeElements.SelectedValue is Type && m_cmbTypeElements.SelectedValue != typeof(DBNull))
			{
				m_panelDeclencheur.TypeCible = (Type)m_cmbTypeElements.SelectedValue;
				m_processEditor.SetTypeElement ( (Type)m_cmbTypeElements.SelectedValue );
			}
		}

		//-------------------------------------------------------------------------
		private void m_lnkDefinisseur_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
            CFormNavigateur nav = CFormNavigateur.FindNavigateur(this);
            if (nav != null)
            {
                CObjetDonneeAIdNumerique definisseur = (CObjetDonneeAIdNumerique)Evenement.Definisseur;
                if (definisseur != null)
                {
                    //Type tp = CFormFinder.GetTypeFormToEdit ( definisseur.GetType() );
                    CReferenceTypeForm refTypeForm = CFormFinder.GetRefFormToEdit(definisseur.GetType());
                    if (refTypeForm == null)
                        CFormAlerte.Afficher(I.T("Not available|917"), EFormAlerteType.Exclamation);
                    else
                    {
                        //CFormEditionStandard form = (CFormEditionStandard)Activator.CreateInstance ( tp, new object[]{definisseur} );
                        //CTimosApp.Navigateur.AffichePage ( form );
                        CFormEditionStandard form = refTypeForm.GetForm((CObjetDonneeAIdNumeriqueAuto)definisseur) as CFormEditionStandard;
                        if (form != null)
                            nav.AffichePage(form);

                    }
                }
            }
		}

		//-------------------------------------------------------------------------
		private void m_lnkGererProcess_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{

			Type tp = null;
			if ( m_cmbTypeElements.SelectedValue is Type && m_cmbTypeElements.SelectedValue != typeof(DBNull))
				tp = (Type)m_cmbTypeElements.SelectedValue;
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( CSc2iWin32DataClient.ContexteCourant, typeof(CProcessInDb) );
			if ( tp != null )
				liste.Filtre = new CFiltreData ( CProcessInDb.c_champTypeCible+"=@1",
					tp.ToString() );
            CReferenceTypeForm refTypeForm = CFormFinder.GetTypeFormToList(typeof(CProcessInDb));
            if (refTypeForm != null)
            {
                IFormNavigable form = refTypeForm.GetForm() as IFormNavigable;
                if ( form != null )
                    CFormNavigateurPopup.Show(form);
            }
			
			InitComboProcess();
			m_cmbProcess.SelectedValue = Evenement.ProcessADeclencher;
		}

		//-------------------------------------------------------------------------
		private void m_cmbProcess_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( m_cmbProcess.SelectedValue is CProcessInDb )
			{
				m_gestionnaireModeEdition.SetModeEdition ( m_processEditor, TypeModeEdition.Autonome );
				m_processEditor.Enabled = false;
				m_processEditor.Process = ((CProcessInDb)m_cmbProcess.SelectedValue).Process;
			}
			else
			{
				m_gestionnaireModeEdition.SetModeEdition ( m_processEditor, TypeModeEdition.EnableSurEdition );
				m_processEditor.Enabled = m_gestionnaireModeEdition.ModeEdition;
				if ( m_processEditor.Process == null )
					m_processEditor.Process = new CProcess ( Evenement.ContexteDonnee );
				if ( m_cmbTypeElements.SelectedValue is Type && m_cmbTypeElements.SelectedValue != typeof(DBNull))
					m_processEditor.Process.TypeCible = ((Type)m_cmbTypeElements.SelectedValue);
				else
					m_processEditor.Process.TypeCible = Evenement.TypeCible;
			}
		}
		private void m_chkTableau_CheckedChanged(object sender, System.EventArgs e)
		{
			m_panelDeclencheur.SetSurTableau ( m_chkTableau.Checked );
			m_processEditor.SetSurTableauDeTypeCible ( m_chkTableau.Checked );
		}

        //----------------------------------------------------------------
        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        //----------------------------------------------------------------
        public event EventHandler OnChangeLockEdition;

    }
}

