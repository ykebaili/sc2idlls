using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CControlEditeMatriceDonnee.
	/// </summary>
	public class CControlEditeMatriceDonnee : System.Windows.Forms.UserControl, IControlALockEdition
	{
		private CMatriceDonnee m_matrice = new CMatriceDonnee();

		//ArrayList de ArrayList
		//Le premier des arraylist contient la valeur d'indice de ligne,
		//Les autres, les valeur des colonnes
		private ArrayList m_listeDonnees = new ArrayList();


		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private sc2i.win32.common.C2iComboBox m_cmbTypeColonnes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.PictureBox pictureBox1;
		private Crownwood.Magic.Controls.TabPage m_pageAxes;
		private Crownwood.Magic.Controls.TabPage m_pageDonnees;
		private sc2i.win32.common.C2iComboBox m_cmbTypeLignes;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.C2iNumericUpDown m_wndValDef;
		private System.Windows.Forms.PictureBox pictureBox2;
		private sc2i.win32.common.CComboboxAutoFilled m_cmbResolutionColonnes;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbResolutionLignes;
		private sc2i.win32.common.C2iDataGrid m_gridColonnes;
		private sc2i.win32.common.C2iTabControl m_tabControl;
		private sc2i.win32.common.C2iDataGrid m_gridDonnees;
		private System.Windows.Forms.Panel panel2;
		private sc2i.win32.common.C2iTextBox m_txtEnteteString;
		private System.Windows.Forms.Panel panel1;
		private sc2i.win32.common.C2iTextBoxNumerique m_numUpValeur;
		private sc2i.win32.common.C2iTextBox m_txtValeur;
		private sc2i.win32.common.C2iTextBoxNumerique m_numUpEnteteDouble;
		private System.Windows.Forms.LinkLabel m_lnkAjouterLigne;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button m_btnValeur;
		private System.Windows.Forms.TextBox m_txtLigne;
        private System.Windows.Forms.TextBox m_txtColonne;
        protected CExtStyle m_extStyle;
		private System.ComponentModel.IContainer components;

		public CControlEditeMatriceDonnee()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_cmbTypeColonnes = new sc2i.win32.common.C2iComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageAxes = new Crownwood.Magic.Controls.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_numUpEnteteDouble = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtEnteteString = new sc2i.win32.common.C2iTextBox();
            this.m_cmbResolutionLignes = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_cmbResolutionColonnes = new sc2i.win32.common.CComboboxAutoFilled();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.m_wndValDef = new sc2i.win32.common.C2iNumericUpDown();
            this.m_cmbTypeLignes = new sc2i.win32.common.C2iComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_gridColonnes = new sc2i.win32.common.C2iDataGrid(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.m_pageDonnees = new Crownwood.Magic.Controls.TabPage();
            this.m_btnValeur = new System.Windows.Forms.Button();
            this.m_txtColonne = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_txtLigne = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_lnkAjouterLigne = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_numUpValeur = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtValeur = new sc2i.win32.common.C2iTextBox();
            this.m_gridDonnees = new sc2i.win32.common.C2iDataGrid(this.components);
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.m_tabControl.SuspendLayout();
            this.m_pageAxes.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_wndValDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridColonnes)).BeginInit();
            this.m_pageDonnees.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridDonnees)).BeginInit();
            this.SuspendLayout();
            // 
            // m_cmbTypeColonnes
            // 
            this.m_cmbTypeColonnes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeColonnes.IsLink = false;
            this.m_cmbTypeColonnes.Items.AddRange(new object[] {
            "Numérique",
            "Texte"});
            this.m_cmbTypeColonnes.Location = new System.Drawing.Point(416, 48);
            this.m_cmbTypeColonnes.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeColonnes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeColonnes.Name = "m_cmbTypeColonnes";
            this.m_cmbTypeColonnes.Size = new System.Drawing.Size(152, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbTypeColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbTypeColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeColonnes.TabIndex = 7;
            this.m_cmbTypeColonnes.SelectedIndexChanged += new System.EventHandler(this.m_cmbTypeColonnes_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(352, 74);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.m_extStyle.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 6;
            this.label5.Text = "Search|115";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(352, 50);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 16);
            this.m_extStyle.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 5;
            this.label6.Text = "Type|114";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(288, 50);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label8, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 16);
            this.m_extStyle.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 3;
            this.label8.Text = "Columns|123";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(280, 48);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 320);
            this.m_extStyle.SetStyleBackColor(this.pictureBox1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.pictureBox1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ControlBottomOffset = 16;
            this.m_tabControl.ControlRightOffset = 16;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tabControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 1;
            this.m_tabControl.SelectedTab = this.m_pageDonnees;
            this.m_tabControl.Size = new System.Drawing.Size(624, 416);
            this.m_extStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_tabControl.TabIndex = 1;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_pageAxes,
            this.m_pageDonnees});
            this.m_tabControl.SelectionChanged += new System.EventHandler(this.c2iTabControl1_SelectionChanged);
            // 
            // m_pageAxes
            // 
            this.m_pageAxes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_pageAxes.Controls.Add(this.panel2);
            this.m_pageAxes.Controls.Add(this.m_cmbResolutionLignes);
            this.m_pageAxes.Controls.Add(this.m_cmbResolutionColonnes);
            this.m_pageAxes.Controls.Add(this.pictureBox2);
            this.m_pageAxes.Controls.Add(this.m_wndValDef);
            this.m_pageAxes.Controls.Add(this.m_cmbTypeLignes);
            this.m_pageAxes.Controls.Add(this.label4);
            this.m_pageAxes.Controls.Add(this.label3);
            this.m_pageAxes.Controls.Add(this.label1);
            this.m_pageAxes.Controls.Add(this.pictureBox1);
            this.m_pageAxes.Controls.Add(this.label8);
            this.m_pageAxes.Controls.Add(this.label5);
            this.m_pageAxes.Controls.Add(this.m_cmbTypeColonnes);
            this.m_pageAxes.Controls.Add(this.label6);
            this.m_pageAxes.Controls.Add(this.m_gridColonnes);
            this.m_pageAxes.Controls.Add(this.label2);
            this.m_pageAxes.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageAxes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageAxes.Name = "m_pageAxes";
            this.m_pageAxes.Selected = false;
            this.m_pageAxes.Size = new System.Drawing.Size(608, 375);
            this.m_extStyle.SetStyleBackColor(this.m_pageAxes, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_pageAxes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageAxes.TabIndex = 10;
            this.m_pageAxes.Title = "Axes|100";
            this.m_pageAxes.PropertyChanged += new Crownwood.Magic.Controls.TabPage.PropChangeHandler(this.m_pageAxes_PropertyChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.m_numUpEnteteDouble);
            this.panel2.Controls.Add(this.m_txtEnteteString);
            this.panel2.Location = new System.Drawing.Point(204, 137);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 22;
            this.panel2.Visible = false;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // m_numUpEnteteDouble
            // 
            this.m_numUpEnteteDouble.Arrondi = 4;
            this.m_numUpEnteteDouble.BackColor = System.Drawing.Color.White;
            this.m_numUpEnteteDouble.DecimalAutorise = true;
            this.m_numUpEnteteDouble.DoubleValue = 3.1125;
            this.m_numUpEnteteDouble.IntValue = 0;
            this.m_numUpEnteteDouble.Location = new System.Drawing.Point(16, 40);
            this.m_numUpEnteteDouble.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_numUpEnteteDouble, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_numUpEnteteDouble.Name = "m_numUpEnteteDouble";
            this.m_numUpEnteteDouble.NullAutorise = false;
            this.m_numUpEnteteDouble.SelectAllOnEnter = true;
            this.m_numUpEnteteDouble.Size = new System.Drawing.Size(100, 21);
            this.m_extStyle.SetStyleBackColor(this.m_numUpEnteteDouble, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this.m_numUpEnteteDouble, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_numUpEnteteDouble.TabIndex = 1;
            this.m_numUpEnteteDouble.Text = "3,1125";
            this.m_numUpEnteteDouble.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtEnteteString
            // 
            this.m_txtEnteteString.BackColor = System.Drawing.Color.White;
            this.m_txtEnteteString.Location = new System.Drawing.Point(16, 8);
            this.m_txtEnteteString.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtEnteteString, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtEnteteString.Name = "m_txtEnteteString";
            this.m_txtEnteteString.Size = new System.Drawing.Size(100, 21);
            this.m_extStyle.SetStyleBackColor(this.m_txtEnteteString, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this.m_txtEnteteString, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtEnteteString.TabIndex = 0;
            this.m_txtEnteteString.Text = "c2iTextBox1";
            // 
            // m_cmbResolutionLignes
            // 
            this.m_cmbResolutionLignes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbResolutionLignes.IsLink = false;
            this.m_cmbResolutionLignes.Items.AddRange(new object[] {
            "Numérique",
            "Texte"});
            this.m_cmbResolutionLignes.ListDonnees = null;
            this.m_cmbResolutionLignes.Location = new System.Drawing.Point(120, 72);
            this.m_cmbResolutionLignes.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbResolutionLignes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbResolutionLignes.Name = "m_cmbResolutionLignes";
            this.m_cmbResolutionLignes.NullAutorise = false;
            this.m_cmbResolutionLignes.ProprieteAffichee = null;
            this.m_cmbResolutionLignes.Size = new System.Drawing.Size(152, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbResolutionLignes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbResolutionLignes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbResolutionLignes.TabIndex = 19;
            this.m_cmbResolutionLignes.TextNull = "(empty)";
            this.m_cmbResolutionLignes.Tri = true;
            // 
            // m_cmbResolutionColonnes
            // 
            this.m_cmbResolutionColonnes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbResolutionColonnes.IsLink = false;
            this.m_cmbResolutionColonnes.Items.AddRange(new object[] {
            "Numérique",
            "Texte"});
            this.m_cmbResolutionColonnes.ListDonnees = null;
            this.m_cmbResolutionColonnes.Location = new System.Drawing.Point(416, 72);
            this.m_cmbResolutionColonnes.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbResolutionColonnes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbResolutionColonnes.Name = "m_cmbResolutionColonnes";
            this.m_cmbResolutionColonnes.NullAutorise = false;
            this.m_cmbResolutionColonnes.ProprieteAffichee = null;
            this.m_cmbResolutionColonnes.Size = new System.Drawing.Size(152, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbResolutionColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbResolutionColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbResolutionColonnes.TabIndex = 18;
            this.m_cmbResolutionColonnes.TextNull = "(empty)";
            this.m_cmbResolutionColonnes.Tri = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(8, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(592, 1);
            this.m_extStyle.SetStyleBackColor(this.pictureBox2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.pictureBox2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // m_wndValDef
            // 
            this.m_wndValDef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_wndValDef.DoubleValue = 0;
            this.m_wndValDef.IntValue = 0;
            this.m_wndValDef.Location = new System.Drawing.Point(148, 6);
            this.m_wndValDef.LockEdition = false;
            this.m_wndValDef.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndValDef, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndValDef.Name = "m_wndValDef";
            this.m_wndValDef.Size = new System.Drawing.Size(72, 21);
            this.m_extStyle.SetStyleBackColor(this.m_wndValDef, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_wndValDef, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndValDef.TabIndex = 16;
            this.m_wndValDef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_wndValDef.ThousandsSeparator = true;
            // 
            // m_cmbTypeLignes
            // 
            this.m_cmbTypeLignes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeLignes.IsLink = false;
            this.m_cmbTypeLignes.Items.AddRange(new object[] {
            "Numérique",
            "Texte"});
            this.m_cmbTypeLignes.Location = new System.Drawing.Point(120, 48);
            this.m_cmbTypeLignes.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeLignes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeLignes.Name = "m_cmbTypeLignes";
            this.m_cmbTypeLignes.Size = new System.Drawing.Size(152, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbTypeLignes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbTypeLignes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeLignes.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(60, 75);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.m_extStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 13;
            this.label4.Text = "Search|115";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(60, 50);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type|114";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 50);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 24);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 10;
            this.label1.Text = "Raws|112";
            // 
            // m_gridColonnes
            // 
            this.m_gridColonnes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gridColonnes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_gridColonnes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_gridColonnes.CaptionText = "Columns";
            this.m_gridColonnes.CurrentElement = null;
            this.m_gridColonnes.DataMember = "";
            this.m_gridColonnes.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_gridColonnes.Location = new System.Drawing.Point(288, 96);
            this.m_gridColonnes.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_gridColonnes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_gridColonnes.Name = "m_gridColonnes";
            this.m_gridColonnes.PreferredRowHeight = 24;
            this.m_gridColonnes.Size = new System.Drawing.Size(280, 272);
            this.m_extStyle.SetStyleBackColor(this.m_gridColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_gridColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_gridColonnes.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 19);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 3;
            this.label2.Text = "Default value|111";
            // 
            // m_pageDonnees
            // 
            this.m_pageDonnees.Controls.Add(this.m_btnValeur);
            this.m_pageDonnees.Controls.Add(this.m_txtColonne);
            this.m_pageDonnees.Controls.Add(this.label9);
            this.m_pageDonnees.Controls.Add(this.m_txtLigne);
            this.m_pageDonnees.Controls.Add(this.label7);
            this.m_pageDonnees.Controls.Add(this.m_lnkAjouterLigne);
            this.m_pageDonnees.Controls.Add(this.panel1);
            this.m_pageDonnees.Controls.Add(this.m_gridDonnees);
            this.m_pageDonnees.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageDonnees, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageDonnees.Name = "m_pageDonnees";
            this.m_pageDonnees.Size = new System.Drawing.Size(608, 375);
            this.m_extStyle.SetStyleBackColor(this.m_pageDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_pageDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageDonnees.TabIndex = 11;
            this.m_pageDonnees.Title = "Datas|101";
            // 
            // m_btnValeur
            // 
            this.m_btnValeur.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnValeur.Location = new System.Drawing.Point(496, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnValeur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnValeur.Name = "m_btnValeur";
            this.m_btnValeur.Size = new System.Drawing.Size(72, 23);
            this.m_extStyle.SetStyleBackColor(this.m_btnValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnValeur.TabIndex = 27;
            this.m_btnValeur.Text = "Value ?|16";
            this.m_btnValeur.UseVisualStyleBackColor = false;
            this.m_btnValeur.Click += new System.EventHandler(this.m_btnValeur_Click);
            // 
            // m_txtColonne
            // 
            this.m_txtColonne.Location = new System.Drawing.Point(403, 1);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtColonne, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtColonne.Name = "m_txtColonne";
            this.m_txtColonne.Size = new System.Drawing.Size(80, 21);
            this.m_extStyle.SetStyleBackColor(this.m_txtColonne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtColonne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtColonne.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(355, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label9, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 23);
            this.m_extStyle.SetStyleBackColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label9.TabIndex = 25;
            this.label9.Text = "Column|113";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtLigne
            // 
            this.m_txtLigne.Location = new System.Drawing.Point(256, 1);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtLigne, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtLigne.Name = "m_txtLigne";
            this.m_txtLigne.Size = new System.Drawing.Size(80, 21);
            this.m_extStyle.SetStyleBackColor(this.m_txtLigne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLigne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLigne.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(171, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label7, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 23);
            this.m_extStyle.SetStyleBackColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label7.TabIndex = 23;
            this.label7.Text = "Raw test|116";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lnkAjouterLigne
            // 
            this.m_lnkAjouterLigne.Location = new System.Drawing.Point(8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAjouterLigne, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAjouterLigne.Name = "m_lnkAjouterLigne";
            this.m_lnkAjouterLigne.Size = new System.Drawing.Size(138, 23);
            this.m_extStyle.SetStyleBackColor(this.m_lnkAjouterLigne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lnkAjouterLigne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAjouterLigne.TabIndex = 22;
            this.m_lnkAjouterLigne.TabStop = true;
            this.m_lnkAjouterLigne.Text = "Add a line|15";
            this.m_lnkAjouterLigne.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkAjouterLigne.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAjouterLigne_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_numUpValeur);
            this.panel1.Controls.Add(this.m_txtValeur);
            this.panel1.Location = new System.Drawing.Point(204, 137);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 21;
            this.panel1.Visible = false;
            // 
            // m_numUpValeur
            // 
            this.m_numUpValeur.Arrondi = 4;
            this.m_numUpValeur.DecimalAutorise = true;
            this.m_numUpValeur.DoubleValue = 3.1125;
            this.m_numUpValeur.IntValue = 0;
            this.m_numUpValeur.Location = new System.Drawing.Point(16, 40);
            this.m_numUpValeur.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_numUpValeur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_numUpValeur.Name = "m_numUpValeur";
            this.m_numUpValeur.NullAutorise = false;
            this.m_numUpValeur.SelectAllOnEnter = true;
            this.m_numUpValeur.Size = new System.Drawing.Size(100, 21);
            this.m_extStyle.SetStyleBackColor(this.m_numUpValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_numUpValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_numUpValeur.TabIndex = 1;
            this.m_numUpValeur.Text = "3,1125";
            this.m_numUpValeur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtValeur
            // 
            this.m_txtValeur.Location = new System.Drawing.Point(16, 8);
            this.m_txtValeur.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtValeur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtValeur.Name = "m_txtValeur";
            this.m_txtValeur.Size = new System.Drawing.Size(100, 21);
            this.m_extStyle.SetStyleBackColor(this.m_txtValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeur.TabIndex = 0;
            this.m_txtValeur.Text = "c2iTextBox1";
            // 
            // m_gridDonnees
            // 
            this.m_gridDonnees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gridDonnees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_gridDonnees.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_gridDonnees.CaptionText = "Datas";
            this.m_gridDonnees.CurrentElement = null;
            this.m_gridDonnees.DataMember = "";
            this.m_gridDonnees.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_gridDonnees.Location = new System.Drawing.Point(8, 24);
            this.m_gridDonnees.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_gridDonnees, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_gridDonnees.Name = "m_gridDonnees";
            this.m_gridDonnees.PreferredRowHeight = 24;
            this.m_gridDonnees.Size = new System.Drawing.Size(592, 344);
            this.m_extStyle.SetStyleBackColor(this.m_gridDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_gridDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_gridDonnees.TabIndex = 16;
            // 
            // CControlEditeMatriceDonnee
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabControl);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlEditeMatriceDonnee";
            this.Size = new System.Drawing.Size(624, 416);
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CControlEditeMatriceDonnee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.m_pageAxes.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_wndValDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridColonnes)).EndInit();
            this.m_pageDonnees.ResumeLayout(false);
            this.m_pageDonnees.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridDonnees)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void m_pageAxes_PropertyChanged(Crownwood.Magic.Controls.TabPage page, Crownwood.Magic.Controls.TabPage.Property prop, object oldValue)
		{
		
		}

		/// ////////////////////////////////////////////////
		public void InitChamps ( CMatriceDonnee matrice )
		{
			m_matrice = matrice;

            //CUtilSurEnum.CCoupleEnumLibelle[] typesRecherche = CUtilSurEnum.GetCouplesFromEnum ( typeof(MethodeResolutionValeurMatrice) );
            //m_cmbResolutionColonnes.DisplayMember = "Libelle";
            //m_cmbResolutionColonnes.ValueMember = "Valeur";
            //m_cmbResolutionColonnes.DataSource = typesRecherche;
            m_cmbResolutionColonnes.Fill(
                CUtilSurEnum.GetEnumsALibelle(typeof(CMethodeResolutionValeurMatrice)),
                "Libelle",
                false);

            //typesRecherche = CUtilSurEnum.GetCouplesFromEnum ( typeof(MethodeResolutionValeurMatrice) );
            //m_cmbResolutionLignes.DisplayMember = "Libelle";
            //m_cmbResolutionLignes.ValueMember = "Valeur";
            //m_cmbResolutionLignes.DataSource = typesRecherche;
            m_cmbResolutionLignes.Fill(
                CUtilSurEnum.GetEnumsALibelle(typeof(CMethodeResolutionValeurMatrice)),
                "Libelle",
                false);
			
			m_cmbResolutionLignes.SelectedValue = new CMethodeResolutionValeurMatrice(matrice.MethodeResolutionLignes);

			m_cmbResolutionColonnes.SelectedValue = new CMethodeResolutionValeurMatrice(matrice.MethodeResolutionColonne);

			m_wndValDef.DoubleValue = matrice.ValeurDefaut;

            m_cmbTypeLignes.Items.Clear();
            m_cmbTypeColonnes.Items.Clear();
            m_cmbTypeLignes.Items.AddRange(new object[] {
                I.T("Number|124"),
                I.T("Text|125")});
            m_cmbTypeColonnes.Items.AddRange(new object[] {
                I.T("Number|124"),
                I.T("Text|125")});


			m_cmbTypeLignes.SelectedIndex = matrice.LignesString?1:0;
			m_cmbTypeColonnes.SelectedIndex = matrice.ColonnesString?1:0;

			FillValeursEntete(m_gridColonnes, matrice.Colonnes,
				m_cmbTypeColonnes.SelectedIndex == 0?typeof(double):typeof(string) );

			m_listeDonnees.Clear();

		
			for ( int nLigne = 0; nLigne < m_matrice.Lignes.Length; nLigne++ )
			{
				ArrayList lst = new ArrayList();
				m_listeDonnees.Add ( lst );
				lst.Add ( matrice.Lignes[nLigne] );
				for ( int nCol = 0; nCol < m_matrice.Colonnes.Length; nCol++ )
					lst.Add ( m_matrice.Valeurs[nLigne, nCol] );
			}
			FillGrilleDonnees();
		}

		/// ////////////////////////////////////////////////
		private void FillValeursEntete( C2iDataGrid grid, object[] valeurs, Type tp)
		{
			if ( valeurs == null )
				return;
			DataTable table = new DataTable();
			table.Columns.Add ( "VALEUR", tp );
			
			DataView view = table.DefaultView;
			view.AllowDelete = true;
			view.AllowEdit = true;
			view.AllowNew = true;
			
			foreach ( object valeur in valeurs )
			{
				DataRow row = table.NewRow();
				if ( tp.Equals(typeof(double)) )
				{
					try
					{
						row["VALEUR"] = Convert.ToDouble ( valeur );
					}
					catch
					{
						row["VALEUR"] = 0;
					}
				}
				else 
				{
					if ( valeur != null )
						row["VALEUR"] = valeur.ToString();
					else
						row["VALEUR"] = "";
				}
				table.Rows.Add ( row );
			}

			grid.DataSource = view;
			DataGridTableStyle style = grid.TableStyle;

			style.GridColumnStyles.Clear();
			if ( tp==typeof(double) )
			{
				//C2iDataGridColumnStyleAControle col = new C2iDataGridColumnStyleAControle(m_numUpValeur, "DoubleValue");
				DataGridColumnStyle col = new DataGridTextBoxColumn();
				col.MappingName = "VALEUR";
				col.HeaderText = "Valeur";
				//col.TextAlign = HorizontalAlignment.Right;
				style.GridColumnStyles.Add ( col );
			}
			else
			{
				//C2iDataGridColumnStyleAControle col = new C2iDataGridColumnStyleAControle ( m_txtValeur, "Text");
				DataGridColumnStyle col = new DataGridTextBoxColumn();
				col.MappingName = "VALEUR";
				col.HeaderText = "Valeur";
				//col.TextAlign = HorizontalAlignment.Left;
				style.GridColumnStyles.Add ( col );
			}

			

			grid.Refresh();
		}

		/// ////////////////////////////////////////////////
		private object[] GetArrayFromDataTable ( DataView view )
		{
			ArrayList lst = new ArrayList();
			foreach ( DataRowView row in view )
				lst.Add ( row["VALEUR"] );
			return (object[])lst.ToArray(typeof(object));
		}

		/// ////////////////////////////////////////////////
		private void MajDonnees()
		{
			if ( m_gridDonnees.DataSource is DataView )
			{
				m_listeDonnees.Clear();
				foreach ( DataRowView rowView in ((DataView)m_gridDonnees.DataSource) )
				{
					ArrayList lst = new ArrayList();
					m_listeDonnees.Add ( lst );
					foreach ( object obj in rowView.Row.ItemArray )
						lst.Add ( obj );
				}
			}
		}

		/// ////////////////////////////////////////////////
		private void FillGrilleDonnees()
		{
			MajDonnees();
			DataTable table = new DataTable();
			int nIndex = 0;
			if ( !(m_gridColonnes.DataSource is DataView)  )
			{
				return;
			}
			DataView cols = (DataView)m_gridColonnes.DataSource;
			Type tp = m_cmbTypeLignes.SelectedIndex == 0?typeof(double):typeof(string);
			DataColumn colTable = table.Columns.Add("LIGNE", tp );
			if ( tp == typeof(string) )
				colTable.DefaultValue = "";
			else
				colTable.DefaultValue = 0;

			for ( int nCol = 0; nCol < cols.Count; nCol++ )
			{
				table.Columns.Add ( "VAL"+nCol.ToString(), typeof(double) ).DefaultValue = 0;
			}
			foreach ( ArrayList lstDonnees in m_listeDonnees )
			{
				DataRow newRow = table.NewRow();
				if ( tp == typeof(double) )
				{
					try
					{
						newRow["LIGNE"] = Convert.ToDouble(lstDonnees[0]);
					}
					catch
					{
						newRow["LIGNE"] = 0;
					}
				}
				else
					newRow["LIGNE"] = lstDonnees[0].ToString();
				for ( int nCol = 0; nCol < cols.Count; nCol++ )
				{
					double fVal = 0;
					if ( nCol < lstDonnees.Count-1 )
						fVal = (double)lstDonnees[nCol+1];
					else
						lstDonnees.Add ( 0 );
					newRow["VAL"+nCol.ToString()] = fVal;
				}
				table.Rows.Add ( newRow );
			}

			DataView view = new DataView ( table );
			view.AllowNew = false;
			view.AllowEdit = true;
			view.AllowDelete = true;

			m_gridDonnees.DataSource = view;
			DataGridTableStyle style = m_gridDonnees.TableStyle;

			style.GridColumnStyles.Clear();

			style.RowHeadersVisible = true;
			style.ColumnHeadersVisible = true;

			if ( tp == typeof(double) )
			{
				C2iDataGridColumnStyleAControle col = new C2iDataGridColumnStyleAControle(m_numUpEnteteDouble, "DoubleValue");
				col.MappingName = "LIGNE";
				col.HeaderText = "Id Ligne";
				col.TextAlign = HorizontalAlignment.Right;
				style.GridColumnStyles.Add ( col );
			}
			else
			{
				C2iDataGridColumnStyleAControle col = new C2iDataGridColumnStyleAControle ( m_txtEnteteString, "Text");
				col.MappingName = "LIGNE";
				col.HeaderText ="Id Ligne";
				col.TextAlign = HorizontalAlignment.Left;
				style.GridColumnStyles.Add ( col );
			}
            				
			nIndex = 0;
			foreach ( DataRowView row in cols )
			{
				C2iDataGridColumnStyleAControle col = new C2iDataGridColumnStyleAControle(m_numUpValeur, "DoubleValue");
				col.MappingName = "VAL"+nIndex.ToString();
				nIndex++;
				col.HeaderText = row["VALEUR"].ToString();
				col.TextAlign = HorizontalAlignment.Right;
				style.GridColumnStyles.Add ( col );
			}
			m_gridDonnees.Refresh();

		}

		/// ////////////////////////////////////////////////
		private void m_cmbTypeColonnes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			object[] vals = null;
			if ( m_gridColonnes.DataSource is DataView )
			{
				vals = GetArrayFromDataTable ( (DataView)m_gridColonnes.DataSource );
			}
			Type tp = m_cmbTypeColonnes.SelectedIndex == 0?typeof(double):typeof(string);
			FillValeursEntete ( m_gridColonnes, vals, tp );
		}

		/// ////////////////////////////////////////////////
		private void c2iTabControl1_SelectionChanged(object sender, System.EventArgs e)
		{
			if ( m_tabControl.SelectedTab == m_pageDonnees )
			{
				FillGrilleDonnees();
			}
		}

		/// ////////////////////////////////////////////////
		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		/// ////////////////////////////////////////////////
		private void m_lnkAjouterLigne_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( m_gridDonnees.DataSource is DataView )
			{
				DataView view = (DataView)m_gridDonnees.DataSource;
				view.Table.Rows.Add ( view.Table.NewRow() );
				m_gridDonnees.Refresh();
			}
		}
		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

		/// ////////////////////////////////////////////////
		public CResultAErreur MajChamps()
		{
			m_matrice.LignesString = m_cmbTypeLignes.SelectedIndex == 1;
			m_matrice.ColonnesString = m_cmbTypeColonnes.SelectedIndex == 1;
            if (m_cmbResolutionLignes.SelectedValue is CMethodeResolutionValeurMatrice)
                m_matrice.MethodeResolutionLignes = ((CMethodeResolutionValeurMatrice)m_cmbResolutionLignes.SelectedValue).Code;

            if (m_cmbResolutionColonnes.SelectedValue is CMethodeResolutionValeurMatrice)
				m_matrice.MethodeResolutionColonne = ((CMethodeResolutionValeurMatrice)m_cmbResolutionColonnes.SelectedValue).Code;

			m_matrice.ValeurDefaut = m_wndValDef.DoubleValue;

			//REmplit les colonnes
			ArrayList lstColonnes = new ArrayList();
			if ( m_gridColonnes.DataSource is DataView )
			{
				foreach ( DataRowView row in (DataView)m_gridColonnes.DataSource )
					lstColonnes.Add ( row["VALEUR"] );
				m_matrice.Colonnes = (object[])lstColonnes.ToArray ();
			}

			//Met à jour la structure de liste de listes
			MajDonnees();
			//Remplit les lignes et les valeurs en même temps
			if ( m_gridDonnees.DataSource is DataView )
			{
				int nNbLignes = ((DataView)m_gridDonnees.DataSource).Count;
				int nNbColonnes = lstColonnes.Count;
				ArrayList lstLignes = new ArrayList();
				
				double[,] valeurs= new double[nNbLignes, nNbColonnes];
				
				for ( int nLigne = 0; nLigne < nNbLignes; nLigne++ )
				{
					ArrayList lstDonnees = (ArrayList)m_listeDonnees[nLigne];
					lstLignes.Add ( lstDonnees[0] );
					for ( int nCol = 1; nCol <= nNbColonnes; nCol++ ) //<= car l'entete de ligne est la première colonne
					{
						valeurs[nLigne, nCol-1] = (double)lstDonnees[nCol];

					}
				}
				m_matrice.Lignes = (object[])lstLignes.ToArray();
				
				m_matrice.Valeurs = valeurs;
			}
			return m_matrice.VerifieCoherence();

		}

		private void m_btnValeur_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = MajChamps();
			if ( !result )
			{
				CFormAlerte.Afficher( result );
				return;
			}
			object ligne;
			object colonne;
			if ( m_matrice.LignesString )
				ligne = m_txtLigne.Text;
			else
			{
				ligne = CConvertisseurDoubleString.Convert ( m_txtLigne.Text );
			}

			if ( m_matrice.ColonnesString )
				colonne = m_txtColonne.Text;
			else
				colonne = CConvertisseurDoubleString.Convert ( m_txtColonne.Text );
			double fVal = m_matrice.GetValeur ( ligne, colonne );
			CFormAlerte.Afficher(I.T("Result: @1|30050",fVal.ToString()));
		}

		public CMatriceDonnee MatriceDonnee
		{
			get
			{
				return m_matrice;
			}
		}

		private void CControlEditeMatriceDonnee_Load(object sender, EventArgs e)
		{
		}

	}
}
