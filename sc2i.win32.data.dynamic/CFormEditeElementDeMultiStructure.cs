using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditeElementDeMultiStructure.
	/// </summary>
	public class CFormEditeElementDeMultiStructure : System.Windows.Forms.Form
	{
		[NonSerialized]
		private CElementMultiStructureExport m_element;
		[NonSerialized]
		private CMultiStructureExport m_multiStructure;

		private System.Windows.Forms.Label label1;
		private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
		private System.Windows.Forms.TextBox m_txtLibelleElement;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_txtPrefix;
		private sc2i.win32.common.C2iPanelOmbre m_panelStructure;
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private Crownwood.Magic.Controls.TabPage tabPage1;
		private sc2i.win32.data.dynamic.CPanelEditionStructureDonnee m_panelEditStructure;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private sc2i.win32.data.dynamic.CPanelEditFiltreDynamique m_panelFiltre;
		private sc2i.win32.common.C2iTabControl c2iTabControl2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iComboBox m_cmbType;
		private sc2i.win32.data.dynamic.CPanelEditRequete m_panelEditRequete;
		private sc2i.win32.common.C2iPanelOmbre m_panelRequete;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnOk;
        protected sc2i.win32.common.CExtStyle m_ExtStyle;
		private System.ComponentModel.IContainer components;

		public CFormEditeElementDeMultiStructure()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeElementDeMultiStructure));
            this.label1 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtLibelleElement = new System.Windows.Forms.TextBox();
            this.m_txtPrefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelStructure = new sc2i.win32.common.C2iPanelOmbre();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelEditStructure = new sc2i.win32.data.dynamic.CPanelEditionStructureDonnee();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.c2iTabControl2 = new sc2i.win32.common.C2iTabControl(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.m_cmbType = new sc2i.win32.common.C2iComboBox();
            this.m_panelRequete = new sc2i.win32.common.C2iPanelOmbre();
            this.m_panelEditRequete = new sc2i.win32.data.dynamic.CPanelEditRequete();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_ExtStyle = new sc2i.win32.common.CExtStyle();
            this.c2iPanelOmbre1.SuspendLayout();
            this.m_panelStructure.SuspendLayout();
            this.c2iTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_panelFiltre.SuspendLayout();
            this.m_panelRequete.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.m_ExtStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Label|136";
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_txtLibelleElement);
            this.c2iPanelOmbre1.Controls.Add(this.m_txtPrefix);
            this.c2iPanelOmbre1.Controls.Add(this.label2);
            this.c2iPanelOmbre1.Controls.Add(this.label1);
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(8, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(600, 48);
            this.m_ExtStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre1.TabIndex = 1;
            // 
            // m_txtLibelleElement
            // 
            this.m_txtLibelleElement.Location = new System.Drawing.Point(112, 8);
            this.m_txtLibelleElement.Name = "m_txtLibelleElement";
            this.m_txtLibelleElement.Size = new System.Drawing.Size(256, 20);
            this.m_ExtStyle.SetStyleBackColor(this.m_txtLibelleElement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_txtLibelleElement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelleElement.TabIndex = 3;
            // 
            // m_txtPrefix
            // 
            this.m_txtPrefix.Location = new System.Drawing.Point(480, 8);
            this.m_txtPrefix.Name = "m_txtPrefix";
            this.m_txtPrefix.Size = new System.Drawing.Size(96, 20);
            this.m_ExtStyle.SetStyleBackColor(this.m_txtPrefix, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_txtPrefix, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtPrefix.TabIndex = 5;
            this.m_txtPrefix.Text = "textBox1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(374, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.m_ExtStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 4;
            this.label2.Text = "Table prefix|137";
            // 
            // m_panelStructure
            // 
            this.m_panelStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelStructure.Controls.Add(this.c2iTabControl1);
            this.m_panelStructure.Controls.Add(this.label3);
            this.m_panelStructure.Controls.Add(this.m_cmbType);
            this.m_panelStructure.Location = new System.Drawing.Point(8, 48);
            this.m_panelStructure.LockEdition = false;
            this.m_panelStructure.Name = "m_panelStructure";
            this.m_panelStructure.Size = new System.Drawing.Size(704, 376);
            this.m_ExtStyle.SetStyleBackColor(this.m_panelStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelStructure.TabIndex = 2;
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 28);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = false;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.SelectedIndex = 0;
            this.c2iTabControl1.SelectedTab = this.tabPage1;
            this.c2iTabControl1.Size = new System.Drawing.Size(688, 336);
            this.m_ExtStyle.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl1.TabIndex = 4047;
            this.c2iTabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.c2iTabControl1.SelectionChanged += new System.EventHandler(this.c2iTabControl1_SelectionChanged_1);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelEditStructure);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(688, 311);
            this.m_ExtStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Structure|230";
            // 
            // m_panelEditStructure
            // 
            this.m_panelEditStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelEditStructure.ComboTypeLockEdition = false;
            this.m_panelEditStructure.ElementAVariablesPourFiltre = null;
            this.m_panelEditStructure.Location = new System.Drawing.Point(4, 0);
            this.m_panelEditStructure.LockEdition = false;
            this.m_panelEditStructure.Name = "m_panelEditStructure";
            this.m_panelEditStructure.Size = new System.Drawing.Size(680, 304);
            this.m_panelEditStructure.StructureExport = ((sc2i.data.dynamic.C2iStructureExport)(resources.GetObject("m_panelEditStructure.StructureExport")));
            this.m_ExtStyle.SetStyleBackColor(this.m_panelEditStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelEditStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEditStructure.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_panelFiltre);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(688, 311);
            this.m_ExtStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Filtre";
            this.tabPage2.Visible = false;
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFiltre.Controls.Add(this.c2iTabControl2);
            this.m_panelFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelFiltre.FiltreDynamique = null;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelFiltre.LockEdition = false;
            this.m_panelFiltre.ModeSansType = true;
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(688, 304);
            this.m_ExtStyle.SetStyleBackColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltre.TabIndex = 4050;
            // 
            // c2iTabControl2
            // 
            this.c2iTabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iTabControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.c2iTabControl2.BoldSelectedPage = true;
            this.c2iTabControl2.ControlBottomOffset = 16;
            this.c2iTabControl2.ControlRightOffset = 16;
            this.c2iTabControl2.IDEPixelArea = false;
            this.c2iTabControl2.Location = new System.Drawing.Point(0, 32);
            this.c2iTabControl2.Name = "c2iTabControl2";
            this.c2iTabControl2.Ombre = true;
            this.c2iTabControl2.PositionTop = true;
            this.c2iTabControl2.Size = new System.Drawing.Size(688, 256);
            this.m_ExtStyle.SetStyleBackColor(this.c2iTabControl2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.c2iTabControl2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.m_ExtStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 4004;
            this.label3.Text = "Object type|138";
            // 
            // m_cmbType
            // 
            this.m_cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(112, 4);
            this.m_cmbType.LockEdition = false;
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(408, 21);
            this.m_ExtStyle.SetStyleBackColor(this.m_cmbType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_cmbType, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbType.TabIndex = 0;
            this.m_cmbType.SelectedValueChanged += new System.EventHandler(this.m_cmbType_SelectedValueChanged);
            // 
            // m_panelRequete
            // 
            this.m_panelRequete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelRequete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.m_panelRequete.Controls.Add(this.m_panelEditRequete);
            this.m_panelRequete.Location = new System.Drawing.Point(8, 48);
            this.m_panelRequete.LockEdition = false;
            this.m_panelRequete.Name = "m_panelRequete";
            this.m_panelRequete.Size = new System.Drawing.Size(704, 376);
            this.m_ExtStyle.SetStyleBackColor(this.m_panelRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelRequete.TabIndex = 4049;
            // 
            // m_panelEditRequete
            // 
            this.m_panelEditRequete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelEditRequete.BackColor = System.Drawing.Color.White;
            this.m_panelEditRequete.Location = new System.Drawing.Point(0, 0);
            this.m_panelEditRequete.LockEdition = false;
            this.m_panelEditRequete.Name = "m_panelEditRequete";
            this.m_panelEditRequete.RequeteEditee = null;
            this.m_panelEditRequete.Size = new System.Drawing.Size(688, 360);
            this.m_ExtStyle.SetStyleBackColor(this.m_panelEditRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_panelEditRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEditRequete.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 421);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 48);
            this.m_ExtStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 4050;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(363, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_ExtStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(309, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_ExtStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditeElementDeMultiStructure
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(712, 469);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_panelStructure);
            this.Controls.Add(this.m_panelRequete);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Name = "CFormEditeElementDeMultiStructure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.m_ExtStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CFormEditeElementDeMultiStructure_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.m_panelStructure.ResumeLayout(false);
            this.m_panelStructure.PerformLayout();
            this.c2iTabControl1.ResumeLayout(false);
            this.c2iTabControl1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_panelFiltre.ResumeLayout(false);
            this.m_panelRequete.ResumeLayout(false);
            this.m_panelRequete.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void c2iTabControl1_SelectionChanged(object sender, System.EventArgs e)
		{
		
		}

		//-------------------------------------------------------------------------
		private bool m_bComboInitialized = false;
		private CResultAErreur InitComboBoxType()
		{
			CResultAErreur result = CResultAErreur.True;

			if (m_bComboInitialized)
				return result;

			//CInfoClasseDynamique[] infosClasses = DynamicClassAttribute.GetAllDynamicClass();
			ArrayList infosClasses = new ArrayList(DynamicClassAttribute.GetAllDynamicClass());
			infosClasses.Insert(0, new CInfoClasseDynamique(typeof(DBNull), I.T("None|30048")));
			m_cmbType.DataSource = null;
			m_cmbType.DataSource = infosClasses;

			m_cmbType.ValueMember = "Classe";
			m_cmbType.DisplayMember = "Nom";

			m_bComboInitialized = true;
			return result;
		}

		//-------------------------------------------------------------------------
		public static bool EditeElement ( CElementMultiStructureExport element, CMultiStructureExport multiStructure )
		{
			CFormEditeElementDeMultiStructure form = new CFormEditeElementDeMultiStructure();
			form.m_multiStructure = multiStructure;
			form.m_element = element;
			form.InitChamps();
			bool bResult = form.ShowDialog( ) == DialogResult.OK;
			form.Dispose();
			return bResult;
		}


		//-------------------------------------------------------------------------
		protected CResultAErreur InitChamps()
		{
			CResultAErreur result = CResultAErreur.True;
			result = InitComboBoxType();
			if (!result)
				return result;

			if ( m_element.DefinitionJeu is C2iRequete )
			{
				m_panelEditRequete.MasquerFormulaire (true);
				m_panelRequete.Visible = true;
				C2iRequete requete = (C2iRequete)m_element.DefinitionJeu;
				if ( requete == null )
					requete = new C2iRequete(m_multiStructure.ContexteDonnee);
				requete.ElementAVariablesExterne = m_multiStructure;
				m_panelEditRequete.RequeteEditee = requete;
			}
			else
				 m_panelRequete.Visible = false;

			if ( m_element.DefinitionJeu is CStructureExportAvecFiltre )
			{
				m_panelFiltre.MasquerFormulaire(true);
				m_panelStructure.Visible = true;
				CStructureExportAvecFiltre structureAvecFiltre = (CStructureExportAvecFiltre)m_element.DefinitionJeu;
				C2iStructureExport structure = structureAvecFiltre.Structure;
				if ( structure == null )
					structure = new C2iStructureExport();
				if ( structure != null && structure.TypeSource != null )
				{
					m_cmbType.SelectedValue = structure.TypeSource;
					//m_cmbType_SelectedValueChanged ( m_cmbType, new EventArgs() );
				}
				structure.TypeSource = (Type) m_cmbType.SelectedValue;
				if (m_cmbType.SelectedValue != null && m_cmbType.SelectedValue == typeof(DBNull))
					structure.TypeSource = null;
				m_panelEditStructure.StructureExport = structure;
				m_panelEditStructure.ElementAVariablesPourFiltre = m_multiStructure;
		
				m_panelEditStructure.ComboTypeLockEdition = true;

				m_panelFiltre.ModeSansType = true;
				CFiltreDynamique filtre = structureAvecFiltre.Filtre;
				if ( filtre == null )
				{
					filtre = new CFiltreDynamique ( m_multiStructure.ContexteDonnee );
					filtre.TypeElements = structure.TypeSource;
				}
				filtre.ElementAVariablesExterne = m_multiStructure;
				m_panelFiltre.Init ( filtre );
			}
			else
				m_panelStructure.Visible = false;

			m_txtLibelleElement.Text = m_element.Libelle;
			m_txtPrefix.Text = m_element.Prefixe;

			return result;
		}

		
		//-------------------------------------------------------------------------
		protected CResultAErreur MAJ_Champs() 
		{
			CResultAErreur result = CResultAErreur.True;
			
			m_element.Libelle = m_txtLibelleElement.Text;
			m_element.Prefixe = m_txtPrefix.Text;
			if ( m_panelRequete.Visible )
			{
				m_element.DefinitionJeu = m_panelEditRequete.RequeteEditee;
			}
			if ( m_panelStructure.Visible )
			{
				CStructureExportAvecFiltre structureAvecFiltre = new CStructureExportAvecFiltre();
				structureAvecFiltre.Filtre = m_panelFiltre.FiltreDynamique;
				structureAvecFiltre.Structure = m_panelEditStructure.StructureExport;
				m_element.DefinitionJeu = structureAvecFiltre;
			}
			
			return result;
		}
		//-------------------------------------------------------------------------
        private Type m_oldType = null;
		private void m_cmbType_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (!m_bComboInitialized)
				return;
			Type tp = null;
            if (m_cmbType.SelectedValue != null && m_cmbType.SelectedValue != typeof(DBNull))
            {
                tp = (Type)m_cmbType.SelectedValue;
            }

            if (m_oldType != null && m_oldType == tp)
                return;

            m_oldType = tp;
			C2iStructureExport structure = new C2iStructureExport();
			structure.TypeSource = tp;
			m_panelEditStructure.StructureExport = structure;
			if (m_panelFiltre.FiltreDynamique != null)
			{
				m_panelFiltre.FiltreDynamique.TypeElements = structure.TypeSource;
				m_panelFiltre.Init(m_panelFiltre.FiltreDynamique);
			}
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			MAJ_Champs();
			DialogResult = DialogResult.OK;
			Close();
		}

		/// //////////////////////////////////////////////////////////////
		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

        private void c2iTabControl1_SelectionChanged_1(object sender, EventArgs e)
        {

        }

        private void CFormEditeElementDeMultiStructure_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

	}
}
