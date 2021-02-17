using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using sc2i.data;
using sc2i.win32.process;
using sc2i.win32.data;

using sc2i.documents;
using sc2i.win32.common;
using System.Collections.Generic;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditZipGedDansGed : sc2i.win32.process.CFormEditActionFonction
	{
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleEnCours = null;

		private sc2i.win32.common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage1;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeCategories;
        private Crownwood.Magic.Controls.TabPage tabPage3;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleCle;
		private System.Windows.Forms.Label label7;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleDescriptif;
		private System.Windows.Forms.Label label5;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleLibelle;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
        private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn2;
        private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private sc2i.win32.common.CExtStyle m_exStyle;
        private Label label1;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleListeDocuments;
        private Label label2;
        private SplitContainer m_splitterFormulesOnglet1;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleNomFichier;
		private System.ComponentModel.IContainer components = null;

		public CFormEditZipGedDansGed()
		{
			// Cet appel est requis par le Concepteur Windows Form.
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_splitterFormulesOnglet1 = new System.Windows.Forms.SplitContainer();
            this.m_txtFormuleListeDocuments = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtFormuleNomFichier = new sc2i.win32.expression.CControleEditeFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.m_wndListeCategories = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn2 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.m_txtFormuleCle = new sc2i.win32.expression.CControleEditeFormule();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtFormuleDescriptif = new sc2i.win32.expression.CControleEditeFormule();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtFormuleLibelle = new sc2i.win32.expression.CControleEditeFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_splitterFormulesOnglet1.Panel1.SuspendLayout();
            this.m_splitterFormulesOnglet1.Panel2.SuspendLayout();
            this.m_splitterFormulesOnglet1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_exStyle.SetStyleBackColor(this.m_lblStockerResIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lblStockerResIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblStockerResIn.Text = "Store the result in|112";
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
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(8, 38);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.SelectedTab = this.tabPage1;
            this.m_tabControl.Size = new System.Drawing.Size(476, 358);
            this.m_exStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 7;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.tabPage3});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_splitterFormulesOnglet1);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(460, 317);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Content|20016";
            // 
            // m_splitterFormulesOnglet1
            // 
            this.m_splitterFormulesOnglet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitterFormulesOnglet1.Location = new System.Drawing.Point(0, 0);
            this.m_splitterFormulesOnglet1.Name = "m_splitterFormulesOnglet1";
            this.m_splitterFormulesOnglet1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitterFormulesOnglet1.Panel1
            // 
            this.m_splitterFormulesOnglet1.Panel1.Controls.Add(this.m_txtFormuleListeDocuments);
            this.m_splitterFormulesOnglet1.Panel1.Controls.Add(this.label1);
            this.m_exStyle.SetStyleBackColor(this.m_splitterFormulesOnglet1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_splitterFormulesOnglet1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_splitterFormulesOnglet1.Panel2
            // 
            this.m_splitterFormulesOnglet1.Panel2.Controls.Add(this.m_txtFormuleNomFichier);
            this.m_splitterFormulesOnglet1.Panel2.Controls.Add(this.label2);
            this.m_exStyle.SetStyleBackColor(this.m_splitterFormulesOnglet1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_splitterFormulesOnglet1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitterFormulesOnglet1.Size = new System.Drawing.Size(460, 317);
            this.m_splitterFormulesOnglet1.SplitterDistance = 146;
            this.m_exStyle.SetStyleBackColor(this.m_splitterFormulesOnglet1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_splitterFormulesOnglet1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitterFormulesOnglet1.TabIndex = 5;
            // 
            // m_txtFormuleListeDocuments
            // 
            this.m_txtFormuleListeDocuments.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleListeDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormuleListeDocuments.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleListeDocuments.Formule = null;
            this.m_txtFormuleListeDocuments.Location = new System.Drawing.Point(0, 13);
            this.m_txtFormuleListeDocuments.LockEdition = false;
            this.m_txtFormuleListeDocuments.Name = "m_txtFormuleListeDocuments";
            this.m_txtFormuleListeDocuments.Size = new System.Drawing.Size(460, 133);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleListeDocuments, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleListeDocuments, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleListeDocuments.TabIndex = 2;
            this.m_txtFormuleListeDocuments.Enter += new System.EventHandler(this.OnEnterTextBoxFormuleSurProcess);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 13);
            this.m_exStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 3;
            this.label1.Text = "EDM document list|20041";
            // 
            // m_txtFormuleNomFichier
            // 
            this.m_txtFormuleNomFichier.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleNomFichier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormuleNomFichier.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleNomFichier.Formule = null;
            this.m_txtFormuleNomFichier.Location = new System.Drawing.Point(0, 13);
            this.m_txtFormuleNomFichier.LockEdition = false;
            this.m_txtFormuleNomFichier.Name = "m_txtFormuleNomFichier";
            this.m_txtFormuleNomFichier.Size = new System.Drawing.Size(460, 154);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleNomFichier, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleNomFichier, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleNomFichier.TabIndex = 5;
            this.m_txtFormuleNomFichier.Enter += new System.EventHandler(this.m_txtFormuleNomFichier_Enter);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(460, 13);
            this.m_exStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 4;
            this.label2.Text = "file name for each document|20042";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.m_wndListeCategories);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(460, 317);
            this.m_exStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "EDM category|176";
            this.tabPage2.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(322, 16);
            this.m_exStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 1;
            this.label3.Text = "The document will be stored in the following categories|30244";
            // 
            // m_wndListeCategories
            // 
            this.m_wndListeCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_wndListeCategories.CheckBoxes = true;
            this.m_wndListeCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn2});
            this.m_wndListeCategories.EnableCustomisation = true;
            this.m_wndListeCategories.FullRowSelect = true;
            this.m_wndListeCategories.Location = new System.Drawing.Point(8, 40);
            this.m_wndListeCategories.MultiSelect = false;
            this.m_wndListeCategories.Name = "m_wndListeCategories";
            this.m_wndListeCategories.Size = new System.Drawing.Size(296, 270);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeCategories, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeCategories, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeCategories.TabIndex = 0;
            this.m_wndListeCategories.UseCompatibleStateImageBehavior = false;
            this.m_wndListeCategories.View = System.Windows.Forms.View.Details;
            // 
            // listViewAutoFilledColumn2
            // 
            this.listViewAutoFilledColumn2.Field = "Libelle";
            this.listViewAutoFilledColumn2.PrecisionWidth = 0;
            this.listViewAutoFilledColumn2.ProportionnalSize = false;
            this.listViewAutoFilledColumn2.Text = "Category|30245";
            this.listViewAutoFilledColumn2.Visible = true;
            this.listViewAutoFilledColumn2.Width = 279;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_txtFormuleCle);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.m_txtFormuleDescriptif);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.m_txtFormuleLibelle);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(460, 317);
            this.m_exStyle.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "Document characteristics|1039";
            this.tabPage3.Visible = false;
            // 
            // m_txtFormuleCle
            // 
            this.m_txtFormuleCle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleCle.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleCle.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleCle.Formule = null;
            this.m_txtFormuleCle.Location = new System.Drawing.Point(8, 224);
            this.m_txtFormuleCle.LockEdition = false;
            this.m_txtFormuleCle.Name = "m_txtFormuleCle";
            this.m_txtFormuleCle.Size = new System.Drawing.Size(444, 86);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleCle.TabIndex = 7;
            this.m_txtFormuleCle.Enter += new System.EventHandler(this.OnEnterTextBoxFormuleSurProcess);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.m_exStyle.SetStyleBackColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label7.TabIndex = 6;
            this.label7.Text = "Key (unique Id)|1043";
            // 
            // m_txtFormuleDescriptif
            // 
            this.m_txtFormuleDescriptif.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleDescriptif.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleDescriptif.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleDescriptif.Formule = null;
            this.m_txtFormuleDescriptif.Location = new System.Drawing.Point(8, 136);
            this.m_txtFormuleDescriptif.LockEdition = false;
            this.m_txtFormuleDescriptif.Name = "m_txtFormuleDescriptif";
            this.m_txtFormuleDescriptif.Size = new System.Drawing.Size(444, 64);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleDescriptif, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleDescriptif, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleDescriptif.TabIndex = 3;
            this.m_txtFormuleDescriptif.Enter += new System.EventHandler(this.OnEnterTextBoxFormuleSurProcess);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 16);
            this.m_exStyle.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 2;
            this.label5.Text = "Document description|1042";
            // 
            // m_txtFormuleLibelle
            // 
            this.m_txtFormuleLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleLibelle.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleLibelle.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleLibelle.Formule = null;
            this.m_txtFormuleLibelle.Location = new System.Drawing.Point(8, 48);
            this.m_txtFormuleLibelle.LockEdition = false;
            this.m_txtFormuleLibelle.Name = "m_txtFormuleLibelle";
            this.m_txtFormuleLibelle.Size = new System.Drawing.Size(444, 64);
            this.m_exStyle.SetStyleBackColor(this.m_txtFormuleLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtFormuleLibelle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleLibelle.TabIndex = 1;
            this.m_txtFormuleLibelle.Enter += new System.EventHandler(this.OnEnterTextBoxFormuleSurProcess);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.m_exStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 0;
            this.label4.Text = "Document label|1041";
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "Nom";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Name |164";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 176;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(484, 32);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 361);
            this.m_exStyle.SetStyleBackColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAideFormule.TabIndex = 4;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CFormEditZipGedDansGed
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(660, 441);
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.m_wndAideFormule);
            this.Name = "CFormEditZipGedDansGed";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Create zip file from edm documents|20040";
            this.Load += new System.EventHandler(this.CFormEditZipGedDansGedCode_Load);
            this.Controls.SetChildIndex(this.m_wndAideFormule, 0);
            this.Controls.SetChildIndex(this.m_tabControl, 0);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.m_splitterFormulesOnglet1.Panel1.ResumeLayout(false);
            this.m_splitterFormulesOnglet1.Panel2.ResumeLayout(false);
            this.m_splitterFormulesOnglet1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionZipGedDansGed), typeof(CFormEditZipGedDansGed));
		}


		public CActionZipGedDansGed ZipGedDansGed
		{
			get
			{
				return (CActionZipGedDansGed)ObjetEdite;
			}
		}

		
	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();

            List<CDbKey> listeDbKeysCategoriesStockage = new List<CDbKey>();
			foreach ( ListViewItem item in m_wndListeCategories.CheckedItems )
			{
                listeDbKeysCategoriesStockage.Add(
                    ((CCategorieGED)item.Tag).DbKey);
			}
            ZipGedDansGed.ListeDbKeysCategoriesStockage = listeDbKeysCategoriesStockage.ToArray();

			CResultAErreur resultExp = GetFormule ( m_txtFormuleCle );
			if ( !resultExp )
			{
				resultExp.EmpileErreur(I.T("Error in key formula|30249"));
				result.Erreur += resultExp.Erreur;
				result.Result = false;
			}
			else
				ZipGedDansGed.ExpressionCle = (C2iExpression)resultExp.Data;

            resultExp = GetFormule(m_txtFormuleListeDocuments);
            if ( !resultExp )
            {
                resultExp.EmpileErreur(I.T("Error in document list formula|20044"));
                result.Erreur += resultExp.Erreur;
                result.Result = false;
            }
            else
                ZipGedDansGed.ExpressionListeDocuments = (C2iExpression)resultExp.Data;

			resultExp = GetFormule ( m_txtFormuleLibelle );
			if ( !resultExp )
			{
				resultExp.EmpileErreur(I.T("Error in label formula|30250"));
				result.Erreur += resultExp.Erreur;
				result.Result = false;
			}
			else
				ZipGedDansGed.ExpressionLibelle = (C2iExpression)resultExp.Data;

            C2iExpression formule = m_txtFormuleNomFichier.Formule;
            if ( formule == null || !m_txtFormuleNomFichier.ResultAnalyse )
            {
                result.EmpileErreur(m_txtFormuleNomFichier.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Error in Filename formula|20043"));
                result.Result = false;
            }
            else
                ZipGedDansGed.ExpressionNomsFichiers = formule;

			resultExp = GetFormule ( m_txtFormuleDescriptif );
			if ( !resultExp )
			{
                resultExp.EmpileErreur(I.T("Error in description formula|30251"));
				result.Erreur += resultExp.Erreur;
				result.Result = false;
			}
			else
				ZipGedDansGed.ExpressionDescriptif = (C2iExpression)resultExp.Data;

		
			return result;
		}

		

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			m_txtFormuleCle.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
			m_txtFormuleDescriptif.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
			m_txtFormuleLibelle.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            m_txtFormuleListeDocuments.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            m_txtFormuleNomFichier.Init(m_wndAideFormule.FournisseurProprietes, typeof(CDocumentGED));

			CListeObjetsDonnees liste = new CListeObjetsDonnees ( CSc2iWin32DataClient.ContexteCourant, typeof(CCategorieGED) );
			m_wndListeCategories.Remplir(liste, false);
            List<CDbKey> listeDbKeysCategorie = new List<CDbKey>(ZipGedDansGed.ListeDbKeysCategoriesStockage);
            foreach (ListViewItem item in m_wndListeCategories.Items)
            {
                CCategorieGED cat = (CCategorieGED)item.Tag;
                if (listeDbKeysCategorie.Contains(cat.DbKey))
                    item.Checked = true;
                else
                    item.Checked = false;
            }

            m_txtFormuleListeDocuments.Formule = ZipGedDansGed.ExpressionListeDocuments;
			m_txtFormuleCle.Text = ZipGedDansGed.ExpressionCle.GetString();
			m_txtFormuleDescriptif.Text = ZipGedDansGed.ExpressionDescriptif.GetString();
			m_txtFormuleLibelle.Text = ZipGedDansGed.ExpressionLibelle.GetString();
            m_txtFormuleNomFichier.Text = ZipGedDansGed.ExpressionNomsFichiers.GetString();
		}

		
		/// //////////////////////////////////////////
		private CResultAErreur GetFormule ( sc2i.win32.expression.CControleEditeFormule textBox )
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			result = analyseur.AnalyseChaine ( textBox.Text );
			return result;
		}

		/// //////////////////////////////////////////
		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_txtFormuleEnCours != null )
				m_wndAideFormule.InsereInTextBox ( m_txtFormuleEnCours, nPosCurseur, strCommande );
		}
	

		/// //////////////////////////////////////////
		private void OnEnterTextBoxFormuleSurProcess(object sender, System.EventArgs e)
		{
			if ( sender is sc2i.win32.expression.CControleEditeFormule )
			{
				if ( m_txtFormuleEnCours != null )
					m_txtFormuleEnCours.BackColor = Color.White;
				m_txtFormuleEnCours = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_txtFormuleEnCours.BackColor = Color.LightGreen;
                m_wndAideFormule.ObjetInterroge = typeof(CProcess);
			}
			
		}

        /// //////////////////////////////////////////
        private void m_txtFormuleNomFichier_Enter(object sender, EventArgs e)
        {
            if (sender is sc2i.win32.expression.CControleEditeFormule)
            {
                if (m_txtFormuleEnCours != null)
                    m_txtFormuleEnCours.BackColor = Color.White;
                m_txtFormuleEnCours = (sc2i.win32.expression.CControleEditeFormule)sender;
                m_txtFormuleEnCours.BackColor = Color.LightGreen;
                m_wndAideFormule.ObjetInterroge = typeof(CDocumentGED);
            }
        }


		/// //////////////////////////////////////////
		private void CFormEditZipGedDansGedCode_Load(object sender, System.EventArgs e)
		{
			OnEnterTextBoxFormuleSurProcess ( m_txtFormuleListeDocuments, new EventArgs() );
		}

       
	}
}

