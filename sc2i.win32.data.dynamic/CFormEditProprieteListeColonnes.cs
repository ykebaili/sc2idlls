using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.data.dynamic;


namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditProprieteListeColonnes.
	/// </summary>
	public class CFormEditProprieteListeColonnes : System.Windows.Forms.Form
	{
		private Font m_font = null;
		private Color m_backColor = Color.Transparent;
		private Color m_textColor = Color.Transparent;
		private List<C2iWndListe.CColonne> m_listeColonnes = null;
		private Type m_typeObjetEdite = null;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Panel m_panelTotal;
		private System.Windows.Forms.Panel m_panelGauche;
		private CWndLinkStd m_btnRemove;
		private CWndLinkStd m_btnAdd;
		private ListBox m_wndListeColonnes;
		private Panel m_panelInfo;
		private Label label2;
		private TextBox m_txtTitre;
		private Label label1;
		private NumericUpDown m_numUpLargeur;
		private Label label3;
		private Button m_btnBas;
		private LinkLabel m_lnkAction;
		private CheckBox m_chkGrouperDonnees;
		private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
		private PictureBox m_imageLink;
		private Label label4;
		private Button m_btnNoTextColor;
		private PictureBox m_picTextColor;
		private Label label5;
		private Button m_btnNoBackgroundColor;
		private PictureBox m_picBkColor;
		private Label label6;
		private Button m_btnSelectFont;
		private Button m_btnCancelFont;
		private Label m_lblPolice;
		private Panel m_panelAgregation;
		private Label label7;
		private Label label8;
		private TextBox m_txtTitreTotal;
		private C2iComboBox m_cmbAgregate;
		private Panel m_panelLibTotal;
		private Button m_btnHaut;
		//private IContainer components;

		public CFormEditProprieteListeColonnes()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				/*if(components != null)
				{
					components.Dispose();
				}*/
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditProprieteListeColonnes));
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_panelTotal = new System.Windows.Forms.Panel();
            this.m_panelInfo = new System.Windows.Forms.Panel();
            this.m_panelLibTotal = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.m_txtTitreTotal = new System.Windows.Forms.TextBox();
            this.m_panelAgregation = new System.Windows.Forms.Panel();
            this.m_cmbAgregate = new sc2i.win32.common.C2iComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_btnSelectFont = new System.Windows.Forms.Button();
            this.m_btnCancelFont = new System.Windows.Forms.Button();
            this.m_lblPolice = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_btnNoTextColor = new System.Windows.Forms.Button();
            this.m_picTextColor = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_btnNoBackgroundColor = new System.Windows.Forms.Button();
            this.m_picBkColor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_imageLink = new System.Windows.Forms.PictureBox();
            this.m_lnkAction = new System.Windows.Forms.LinkLabel();
            this.m_chkGrouperDonnees = new System.Windows.Forms.CheckBox();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_numUpLargeur = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtTitre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelGauche = new System.Windows.Forms.Panel();
            this.m_btnBas = new System.Windows.Forms.Button();
            this.m_btnHaut = new System.Windows.Forms.Button();
            this.m_btnRemove = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAdd = new sc2i.win32.common.CWndLinkStd();
            this.m_wndListeColonnes = new System.Windows.Forms.ListBox();
            this.m_panelTotal.SuspendLayout();
            this.m_panelInfo.SuspendLayout();
            this.m_panelLibTotal.SuspendLayout();
            this.m_panelAgregation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picTextColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picBkColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUpLargeur)).BeginInit();
            this.m_panelGauche.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnOk.Location = new System.Drawing.Point(193, 280);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 24);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(284, 280);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(74, 24);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.Controls.Add(this.m_panelInfo);
            this.m_panelTotal.Controls.Add(this.m_panelGauche);
            this.m_panelTotal.Location = new System.Drawing.Point(0, 0);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(656, 280);
            this.m_panelTotal.TabIndex = 4;
            // 
            // m_panelInfo
            // 
            this.m_panelInfo.Controls.Add(this.m_panelLibTotal);
            this.m_panelInfo.Controls.Add(this.m_panelAgregation);
            this.m_panelInfo.Controls.Add(this.m_btnSelectFont);
            this.m_panelInfo.Controls.Add(this.m_btnCancelFont);
            this.m_panelInfo.Controls.Add(this.m_lblPolice);
            this.m_panelInfo.Controls.Add(this.label6);
            this.m_panelInfo.Controls.Add(this.m_btnNoTextColor);
            this.m_panelInfo.Controls.Add(this.m_picTextColor);
            this.m_panelInfo.Controls.Add(this.label5);
            this.m_panelInfo.Controls.Add(this.m_btnNoBackgroundColor);
            this.m_panelInfo.Controls.Add(this.m_picBkColor);
            this.m_panelInfo.Controls.Add(this.label4);
            this.m_panelInfo.Controls.Add(this.m_imageLink);
            this.m_panelInfo.Controls.Add(this.m_lnkAction);
            this.m_panelInfo.Controls.Add(this.m_chkGrouperDonnees);
            this.m_panelInfo.Controls.Add(this.m_txtFormule);
            this.m_panelInfo.Controls.Add(this.m_numUpLargeur);
            this.m_panelInfo.Controls.Add(this.label3);
            this.m_panelInfo.Controls.Add(this.label2);
            this.m_panelInfo.Controls.Add(this.m_txtTitre);
            this.m_panelInfo.Controls.Add(this.label1);
            this.m_panelInfo.Location = new System.Drawing.Point(239, 12);
            this.m_panelInfo.Name = "m_panelInfo";
            this.m_panelInfo.Size = new System.Drawing.Size(327, 262);
            this.m_panelInfo.TabIndex = 3;
            this.m_panelInfo.Visible = false;
            // 
            // m_panelLibTotal
            // 
            this.m_panelLibTotal.Controls.Add(this.label8);
            this.m_panelLibTotal.Controls.Add(this.m_txtTitreTotal);
            this.m_panelLibTotal.Location = new System.Drawing.Point(6, 211);
            this.m_panelLibTotal.Name = "m_panelLibTotal";
            this.m_panelLibTotal.Size = new System.Drawing.Size(312, 25);
            this.m_panelLibTotal.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 23);
            this.label8.TabIndex = 1;
            this.label8.Text = "Total label|224";
            // 
            // m_txtTitreTotal
            // 
            this.m_txtTitreTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtTitreTotal.Location = new System.Drawing.Point(85, 1);
            this.m_txtTitreTotal.Name = "m_txtTitreTotal";
            this.m_txtTitreTotal.Size = new System.Drawing.Size(224, 20);
            this.m_txtTitreTotal.TabIndex = 21;
            // 
            // m_panelAgregation
            // 
            this.m_panelAgregation.Controls.Add(this.m_cmbAgregate);
            this.m_panelAgregation.Controls.Add(this.label7);
            this.m_panelAgregation.Location = new System.Drawing.Point(6, 189);
            this.m_panelAgregation.Name = "m_panelAgregation";
            this.m_panelAgregation.Size = new System.Drawing.Size(312, 25);
            this.m_panelAgregation.TabIndex = 20;
            // 
            // m_cmbAgregate
            // 
            this.m_cmbAgregate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAgregate.FormattingEnabled = true;
            this.m_cmbAgregate.IsLink = false;
            this.m_cmbAgregate.Location = new System.Drawing.Point(85, 0);
            this.m_cmbAgregate.LockEdition = false;
            this.m_cmbAgregate.Name = "m_cmbAgregate";
            this.m_cmbAgregate.Size = new System.Drawing.Size(224, 21);
            this.m_cmbAgregate.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Agregate|223";
            // 
            // m_btnSelectFont
            // 
            this.m_btnSelectFont.Location = new System.Drawing.Point(276, 138);
            this.m_btnSelectFont.Name = "m_btnSelectFont";
            this.m_btnSelectFont.Size = new System.Drawing.Size(23, 23);
            this.m_btnSelectFont.TabIndex = 19;
            this.m_btnSelectFont.Text = "...";
            this.m_btnSelectFont.UseVisualStyleBackColor = true;
            this.m_btnSelectFont.Click += new System.EventHandler(this.m_btnSelectFont_Click);
            // 
            // m_btnCancelFont
            // 
            this.m_btnCancelFont.Location = new System.Drawing.Point(296, 138);
            this.m_btnCancelFont.Name = "m_btnCancelFont";
            this.m_btnCancelFont.Size = new System.Drawing.Size(23, 23);
            this.m_btnCancelFont.TabIndex = 18;
            this.m_btnCancelFont.Text = "x";
            this.m_btnCancelFont.UseVisualStyleBackColor = true;
            this.m_btnCancelFont.Click += new System.EventHandler(this.m_btnCancelFont_Click);
            // 
            // m_lblPolice
            // 
            this.m_lblPolice.BackColor = System.Drawing.Color.White;
            this.m_lblPolice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblPolice.Location = new System.Drawing.Point(91, 138);
            this.m_lblPolice.Name = "m_lblPolice";
            this.m_lblPolice.Size = new System.Drawing.Size(187, 23);
            this.m_lblPolice.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Font|222";
            // 
            // m_btnNoTextColor
            // 
            this.m_btnNoTextColor.Location = new System.Drawing.Point(158, 115);
            this.m_btnNoTextColor.Name = "m_btnNoTextColor";
            this.m_btnNoTextColor.Size = new System.Drawing.Size(23, 20);
            this.m_btnNoTextColor.TabIndex = 15;
            this.m_btnNoTextColor.Text = "x";
            this.m_btnNoTextColor.UseVisualStyleBackColor = true;
            this.m_btnNoTextColor.Click += new System.EventHandler(this.m_btnNoTextColor_Click);
            // 
            // m_picTextColor
            // 
            this.m_picTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_picTextColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_picTextColor.Location = new System.Drawing.Point(91, 115);
            this.m_picTextColor.Name = "m_picTextColor";
            this.m_picTextColor.Size = new System.Drawing.Size(66, 20);
            this.m_picTextColor.TabIndex = 14;
            this.m_picTextColor.TabStop = false;
            this.m_picTextColor.Click += new System.EventHandler(this.m_picTextColor_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Text|221";
            // 
            // m_btnNoBackgroundColor
            // 
            this.m_btnNoBackgroundColor.Location = new System.Drawing.Point(158, 89);
            this.m_btnNoBackgroundColor.Name = "m_btnNoBackgroundColor";
            this.m_btnNoBackgroundColor.Size = new System.Drawing.Size(23, 20);
            this.m_btnNoBackgroundColor.TabIndex = 12;
            this.m_btnNoBackgroundColor.Text = "x";
            this.m_btnNoBackgroundColor.UseVisualStyleBackColor = true;
            this.m_btnNoBackgroundColor.Click += new System.EventHandler(this.m_btnNoBackgroundColor_Click);
            // 
            // m_picBkColor
            // 
            this.m_picBkColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_picBkColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_picBkColor.Location = new System.Drawing.Point(91, 89);
            this.m_picBkColor.Name = "m_picBkColor";
            this.m_picBkColor.Size = new System.Drawing.Size(66, 20);
            this.m_picBkColor.TabIndex = 11;
            this.m_picBkColor.TabStop = false;
            this.m_picBkColor.Click += new System.EventHandler(this.m_picBkColor_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Background|220";
            // 
            // m_imageLink
            // 
            this.m_imageLink.Image = ((System.Drawing.Image)(resources.GetObject("m_imageLink.Image")));
            this.m_imageLink.Location = new System.Drawing.Point(45, 237);
            this.m_imageLink.Name = "m_imageLink";
            this.m_imageLink.Size = new System.Drawing.Size(16, 16);
            this.m_imageLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_imageLink.TabIndex = 9;
            this.m_imageLink.TabStop = false;
            // 
            // m_lnkAction
            // 
            this.m_lnkAction.AutoSize = true;
            this.m_lnkAction.Location = new System.Drawing.Point(62, 240);
            this.m_lnkAction.Name = "m_lnkAction";
            this.m_lnkAction.Size = new System.Drawing.Size(79, 13);
            this.m_lnkAction.TabIndex = 8;
            this.m_lnkAction.TabStop = true;
            this.m_lnkAction.Text = "Link action|217";
            this.m_lnkAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAction_LinkClicked);
            // 
            // m_chkGrouperDonnees
            // 
            this.m_chkGrouperDonnees.AutoSize = true;
            this.m_chkGrouperDonnees.Location = new System.Drawing.Point(91, 167);
            this.m_chkGrouperDonnees.Name = "m_chkGrouperDonnees";
            this.m_chkGrouperDonnees.Size = new System.Drawing.Size(120, 17);
            this.m_chkGrouperDonnees.TabIndex = 7;
            this.m_chkGrouperDonnees.Text = "Group the data |216";
            this.m_chkGrouperDonnees.UseVisualStyleBackColor = true;
            this.m_chkGrouperDonnees.CheckedChanged += new System.EventHandler(this.m_chkGrouperDonnees_CheckedChanged);
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(91, 33);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(226, 19);
            this.m_txtFormule.TabIndex = 6;
            // 
            // m_numUpLargeur
            // 
            this.m_numUpLargeur.Location = new System.Drawing.Point(91, 59);
            this.m_numUpLargeur.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numUpLargeur.Name = "m_numUpLargeur";
            this.m_numUpLargeur.Size = new System.Drawing.Size(66, 20);
            this.m_numUpLargeur.TabIndex = 5;
            this.m_numUpLargeur.ValueChanged += new System.EventHandler(this.m_numUpLargeur_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width|215";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Content|214";
            // 
            // m_txtTitre
            // 
            this.m_txtTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtTitre.Location = new System.Drawing.Point(91, 7);
            this.m_txtTitre.Name = "m_txtTitre";
            this.m_txtTitre.Size = new System.Drawing.Size(228, 20);
            this.m_txtTitre.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title|213";
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.Controls.Add(this.m_btnBas);
            this.m_panelGauche.Controls.Add(this.m_btnHaut);
            this.m_panelGauche.Controls.Add(this.m_btnRemove);
            this.m_panelGauche.Controls.Add(this.m_btnAdd);
            this.m_panelGauche.Controls.Add(this.m_wndListeColonnes);
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(233, 280);
            this.m_panelGauche.TabIndex = 2;
            // 
            // m_btnBas
            // 
            this.m_btnBas.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBas.Image")));
            this.m_btnBas.Location = new System.Drawing.Point(173, 252);
            this.m_btnBas.Name = "m_btnBas";
            this.m_btnBas.Size = new System.Drawing.Size(27, 22);
            this.m_btnBas.TabIndex = 6;
            this.m_btnBas.UseVisualStyleBackColor = true;
            this.m_btnBas.Click += new System.EventHandler(this.m_btnBas_Click);
            // 
            // m_btnHaut
            // 
            this.m_btnHaut.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHaut.Image")));
            this.m_btnHaut.Location = new System.Drawing.Point(199, 252);
            this.m_btnHaut.Name = "m_btnHaut";
            this.m_btnHaut.Size = new System.Drawing.Size(27, 22);
            this.m_btnHaut.TabIndex = 5;
            this.m_btnHaut.UseVisualStyleBackColor = true;
            this.m_btnHaut.Click += new System.EventHandler(this.m_btnHaut_Click);
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnRemove.Location = new System.Drawing.Point(87, 12);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(78, 16);
            this.m_btnRemove.TabIndex = 4;
            this.m_btnRemove.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnRemove.LinkClicked += new System.EventHandler(this.m_btnRemove_LinkClicked);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAdd.Location = new System.Drawing.Point(3, 12);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(78, 16);
            this.m_btnAdd.TabIndex = 3;
            this.m_btnAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAdd.LinkClicked += new System.EventHandler(this.m_btnAdd_LinkClicked);
            // 
            // m_wndListeColonnes
            // 
            this.m_wndListeColonnes.FormattingEnabled = true;
            this.m_wndListeColonnes.Location = new System.Drawing.Point(3, 29);
            this.m_wndListeColonnes.Name = "m_wndListeColonnes";
            this.m_wndListeColonnes.Size = new System.Drawing.Size(224, 225);
            this.m_wndListeColonnes.TabIndex = 3;
            this.m_wndListeColonnes.SelectedIndexChanged += new System.EventHandler(this.m_wndListeColonnes_SelectedIndexChanged);
            // 
            // CFormEditProprieteListeColonnes
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(568, 309);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelTotal);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnAnnuler);
            this.MinimizeBox = false;
            this.Name = "CFormEditProprieteListeColonnes";
            this.Text = "Rows|218";
            this.Load += new System.EventHandler(this.CFormEditProprieteListeColonnes_Load);
            this.m_panelTotal.ResumeLayout(false);
            this.m_panelInfo.ResumeLayout(false);
            this.m_panelInfo.PerformLayout();
            this.m_panelLibTotal.ResumeLayout(false);
            this.m_panelLibTotal.PerformLayout();
            this.m_panelAgregation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picTextColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_picBkColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUpLargeur)).EndInit();
            this.m_panelGauche.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public Type TypeEdite
		{
			get
			{
				return m_typeObjetEdite;
			}
			set
			{
				m_typeObjetEdite = value;
				m_txtFormule.Init(new CFournisseurPropDynStd(), m_typeObjetEdite);
			}
		}


		public List<C2iWndListe.CColonne> EditeColonnes(List<C2iWndListe.CColonne> liste)
		{
			List<C2iWndListe.CColonne> lstCopie = new List<C2iWndListe.CColonne>();
			foreach (C2iWndListe.CColonne col in liste)
			{
				lstCopie.Add((C2iWndListe.CColonne)CCloner2iSerializable.Clone(col));
			}
			m_listeColonnes = lstCopie;

			if ( ShowDialog() == DialogResult.OK )
				return m_listeColonnes;
			return m_listeColonnes;
		}

		private void CFormEditProprieteListeColonnes_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
			FillListeColonnes();
			
			m_cmbAgregate.DataSource = CUtilSurEnum.GetCouplesFromEnum( typeof(OperationsAgregation));
			m_cmbAgregate.DisplayMember = "Libelle";
			m_cmbAgregate.ValueMember = "Valeur";
		}

		private void FillListeColonnes()
		{
			m_wndListeColonnes.BeginUpdate();
			m_wndListeColonnes.Items.Clear();
			foreach (C2iWndListe.CColonne col in m_listeColonnes)
			{
				m_wndListeColonnes.Items.Add(col);
			}
			m_wndListeColonnes.EndUpdate();
		}



		private void m_btnAnnuler_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			ValideModifs();
			m_listeColonnes.Clear();
			foreach ( C2iWndListe.CColonne col in m_wndListeColonnes.Items )
				m_listeColonnes.Add ( col );
			DialogResult = DialogResult.OK;
			Close();
		}

		//----------------------------------------------------
		private void m_btnRemove_LinkClicked(object sender, EventArgs e)
		{
			if (m_wndListeColonnes.SelectedIndex >= 0)
			{
				C2iWndListe.CColonne col = (C2iWndListe.CColonne)m_wndListeColonnes.Items[m_wndListeColonnes.SelectedIndex];
				if (CFormAlerte.Afficher(I.T("Remove the column @1 ?|30020",col.Title ),
					EFormAlerteType.Question) == DialogResult.Yes)
				{
					m_wndListeColonnes.Items.RemoveAt(m_wndListeColonnes.SelectedIndex);
				}
			}
		}

		//------------------------------------------------------
		private void m_btnAdd_LinkClicked(object sender, EventArgs e)
		{
			ValideModifs();
			C2iWndListe.CColonne col = new C2iWndListe.CColonne();
			col.Title = "New column";
			int nIndex = m_wndListeColonnes.Items.Add(col);
			m_wndListeColonnes.SelectedIndex = nIndex;
		}

		//------------------------------------------------------
		private void m_btnBas_Click(object sender, EventArgs e)
		{
			int nIndex = m_wndListeColonnes.SelectedIndex;
			if (nIndex >= 0 && nIndex < m_wndListeColonnes.Items.Count - 2)
			{
				object item = m_wndListeColonnes.Items[nIndex];
				m_wndListeColonnes.Items.RemoveAt(nIndex);
				m_wndListeColonnes.Items.Insert(nIndex + 1, item);
				m_wndListeColonnes.SelectedIndex = nIndex + 1;
			}
		}

		//------------------------------------------------------
		private void m_btnHaut_Click(object sender, EventArgs e)
		{
			int nIndex = m_wndListeColonnes.SelectedIndex;
			if (nIndex > 1)
			{
				object item = m_wndListeColonnes.Items[nIndex];
				m_wndListeColonnes.Items.RemoveAt(nIndex);
				m_wndListeColonnes.Items.Insert(nIndex - 1, item);
				m_wndListeColonnes.SelectedIndex = nIndex - 1;
			}
		}

	
		//------------------------------------------------------
		C2iWndListe.CColonne m_colonneAffichee = null;
		private void ValideModifs()
		{
			if ( m_colonneAffichee != null )
			{
				m_colonneAffichee.Title = m_txtTitre.Text;
				m_colonneAffichee.Width = (int)m_numUpLargeur.Value;
				m_colonneAffichee.FormuleDonnee = m_txtFormule.Formule;
				m_colonneAffichee.Grouper = m_chkGrouperDonnees.Checked;
				m_colonneAffichee.BackColor = m_backColor;
				m_colonneAffichee.TextColor = m_textColor;
				m_colonneAffichee.Font = m_font;
				if (m_cmbAgregate.SelectedValue is int)
					m_colonneAffichee.OperationAgregation = (OperationsAgregation)m_cmbAgregate.SelectedValue;
				m_colonneAffichee.LibelleTotal = m_txtTitreTotal.Text;
				RefreshListe();
			}
		}

		//------------------------------------------------------
		private bool m_bIsRefreshing = false;
		private void RefreshListe()
		{
			m_bIsRefreshing = true;
			m_wndListeColonnes.BeginUpdate();
			for (int nCol = 0; nCol < m_wndListeColonnes.Items.Count; nCol++)
			{
				C2iWndListe.CColonne col = (C2iWndListe.CColonne)m_wndListeColonnes.Items[nCol];
				m_wndListeColonnes.Items[nCol] = "...";
				m_wndListeColonnes.Items[nCol] = col;
			}
			m_wndListeColonnes.EndUpdate();
			m_bIsRefreshing = false;
		}

		//------------------------------------------------------
		private void AfficheColonne(C2iWndListe.CColonne colonne)
		{
			ValideModifs();
			m_colonneAffichee = colonne;
			if ( colonne == null )
			{
				m_panelInfo.Visible = false;
			}
			else
			{
				m_panelInfo.Visible = true;
				m_txtTitre.Text = colonne.Title;
				m_numUpLargeur.Value = colonne.Width;
				m_txtFormule.Formule = colonne.FormuleDonnee;
				m_chkGrouperDonnees.Checked = colonne.Grouper;
				m_imageLink.Visible = colonne.ActionSurLink != null;
				m_picBkColor.BackColor = colonne.BackColor;
				m_picTextColor.BackColor = colonne.TextColor;
				if (colonne.Font != null)
					m_lblPolice.Text = colonne.Font.FontFamily.Name;
				else
					m_lblPolice.Text = "";
				m_backColor = colonne.BackColor;
				m_textColor = colonne.TextColor;
				m_font = colonne.Font;
				m_cmbAgregate.SelectedValue = (int)colonne.OperationAgregation;
				m_txtTitreTotal.Text = colonne.LibelleTotal;
			}
		}

		//-------------------------------------------------------------------------------
		private void m_wndListeColonnes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_wndListeColonnes.SelectedIndex >= 0 && !m_bIsRefreshing)
			{
				AfficheColonne((C2iWndListe.CColonne)m_wndListeColonnes.Items[m_wndListeColonnes.SelectedIndex]);
			}
		}

		//-------------------------------------------------------------------------------
		private void m_numUpLargeur_ValueChanged(object sender, EventArgs e)
		{

		}

		//-------------------------------------------------------------------------------
		private void m_lnkAction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CActionSur2iLink action = m_colonneAffichee.ActionSurLink;
			CActionSur2iLinkEditor.EditeAction(ref action, m_typeObjetEdite);
			m_imageLink.Visible = action != null;
			m_colonneAffichee.ActionSurLink = action;
		}

		private void m_picBkColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = m_backColor;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				m_backColor = dlg.Color;
				m_picBkColor.BackColor = m_backColor;
			}
		}

		private void m_btnNoBackgroundColor_Click(object sender, EventArgs e)
		{
			m_backColor = Color.Transparent;
			m_picBkColor.BackColor = m_backColor;
		}

		private void m_picTextColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = m_textColor;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				m_textColor = dlg.Color;
				m_picTextColor.BackColor = m_textColor;
			}
		}

		private void m_btnNoTextColor_Click(object sender, EventArgs e)
		{
			m_textColor = Color.Transparent;
			m_picTextColor.BackColor = m_textColor;
		}

		private void m_btnSelectFont_Click(object sender, EventArgs e)
		{
			FontDialog dlg = new FontDialog();
			if (m_font != null)
				dlg.Font = m_font;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				m_font = dlg.Font;
				m_lblPolice.Text = m_font.FontFamily.Name;
			}
		}

		private void m_btnCancelFont_Click(object sender, EventArgs e)
		{
			m_font = null;
			m_lblPolice.Text = "";
		}

		private void m_chkGrouperDonnees_CheckedChanged(object sender, EventArgs e)
		{
			m_panelAgregation.Visible = !m_chkGrouperDonnees.Checked;
			m_panelLibTotal.Visible = m_chkGrouperDonnees.Checked;
		}
	}

	[AutoExec("Autoexec")]
	public class CEditeurListeColonnesPopup : IEditeurColonnes
	{
		private C2iWndListe m_listeEditee = null;

		//---------------------------------------------------
		public CEditeurListeColonnesPopup()
		{
		}

		//---------------------------------------------------
		public static void Autoexec()
		{
			CListeColonnesEditor.SetTypeEditeur(typeof(CEditeurListeColonnesPopup));
		}

		//---------------------------------------------------
		public CEditeurListeColonnesPopup(C2iWndListe listeEditee)
		{
			m_listeEditee = listeEditee;
		}

		//---------------------------------------------------
		public C2iWndListe ListeEditee
		{
			get
			{
				return m_listeEditee;
			}
			set
			{
				m_listeEditee = value;
			}
		}

		//---------------------------------------------------
		public List<C2iWndListe.CColonne> EditeColonnes()
		{
			CFormEditProprieteListeColonnes form = new CFormEditProprieteListeColonnes();
			if ( m_listeEditee == null || m_listeEditee.SourceFormula == null )
			{
				CFormAlerte.Afficher(I.T("Indicate the source formula before editing the columns|30021"), EFormAlerteType.Exclamation);
				return m_listeEditee.Columns;
			}
			form.TypeEdite = m_listeEditee.SourceFormula.TypeDonnee.TypeDotNetNatif;
			List<C2iWndListe.CColonne> newListe = form.EditeColonnes(m_listeEditee.Columns);
			form.Dispose();
			return newListe;
		}
	}

		
}
