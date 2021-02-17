using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditComposantFiltreValeurChamp.
	/// </summary>
	public class CFormEditComposantFiltreValeurChamp : System.Windows.Forms.Form
	{
		private CDefinitionProprieteDynamique m_definitionRacineDeChampsFiltres = null;

		private CDefinitionProprieteDynamique m_champ = null;
		private CDefinitionProprieteDynamique m_champValeur = null;

		private const string c_champLibelle = "LIBELLE";
		private const string c_champIdOperateur = "OPERATEUR";

        private IFournisseurProprietesDynamiques m_fournisseurProprietesFiltrees = null;

		private CComposantFiltreDynamiqueValeurChamp m_composant = null;
		private CFiltreDynamique m_filtre = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_boutonDropList;
		private System.Windows.Forms.Label m_labelChamp;
		private System.Windows.Forms.Panel m_panelComboChamp;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox m_cmbOperateur;
        private System.Windows.Forms.Panel m_panelFiltre;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel m_btnCreerVariable;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.MenuItem m_menuVariableSaisie;
		private System.Windows.Forms.ContextMenu m_menuNewVariable;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private System.Windows.Forms.MenuItem m_menuVariableCalculée;
		private System.Windows.Forms.MenuItem m_menuVariableSelection;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.expression.CControleEditeFormule m_txtCondition;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel m_panelValeurAttendue;
		private System.Windows.Forms.Panel m_panelValeurChamp;
		private System.Windows.Forms.Panel m_panelValeurFormule;
		private System.Windows.Forms.RadioButton m_btnValeur;
		private System.Windows.Forms.RadioButton m_btnChamp;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button m_boutonChampValeurDropList;
		private System.Windows.Forms.Label m_labelChampValeur;
		private System.Windows.Forms.Panel m_panelComboChampValeur;
        private SplitContainer m_splitContainer;

		private sc2i.win32.expression.CControleEditeFormule m_lastTextBox = null;

		public CFormEditComposantFiltreValeurChamp()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			m_lastTextBox = m_txtFormule;
			m_txtFormule.BackColor = Color.LightGreen;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditComposantFiltreValeurChamp));
            this.label1 = new System.Windows.Forms.Label();
            this.m_boutonDropList = new System.Windows.Forms.Button();
            this.m_labelChamp = new System.Windows.Forms.Label();
            this.m_panelComboChamp = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbOperateur = new System.Windows.Forms.ComboBox();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelFiltre = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnValeur = new System.Windows.Forms.RadioButton();
            this.m_btnChamp = new System.Windows.Forms.RadioButton();
            this.m_panelValeurAttendue = new System.Windows.Forms.Panel();
            this.m_panelValeurFormule = new System.Windows.Forms.Panel();
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.m_panelValeurChamp = new System.Windows.Forms.Panel();
            this.m_panelComboChampValeur = new System.Windows.Forms.Panel();
            this.m_labelChampValeur = new System.Windows.Forms.Label();
            this.m_boutonChampValeurDropList = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtCondition = new sc2i.win32.expression.CControleEditeFormule();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCreerVariable = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_panelComboChamp.SuspendLayout();
            this.m_panelFiltre.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelValeurAttendue.SuspendLayout();
            this.m_panelValeurFormule.SuspendLayout();
            this.m_panelValeurChamp.SuspendLayout();
            this.m_panelComboChampValeur.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tested field|124";
            // 
            // m_boutonDropList
            // 
            this.m_boutonDropList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_boutonDropList.BackColor = System.Drawing.SystemColors.Control;
            this.m_boutonDropList.Image = ((System.Drawing.Image)(resources.GetObject("m_boutonDropList.Image")));
            this.m_boutonDropList.Location = new System.Drawing.Point(374, 0);
            this.m_boutonDropList.Name = "m_boutonDropList";
            this.m_boutonDropList.Size = new System.Drawing.Size(17, 17);
            this.m_boutonDropList.TabIndex = 3;
            this.m_boutonDropList.TabStop = false;
            this.m_boutonDropList.UseVisualStyleBackColor = false;
            this.m_boutonDropList.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_labelChamp
            // 
            this.m_labelChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChamp.BackColor = System.Drawing.Color.White;
            this.m_labelChamp.Location = new System.Drawing.Point(0, 0);
            this.m_labelChamp.Name = "m_labelChamp";
            this.m_labelChamp.Size = new System.Drawing.Size(374, 17);
            this.m_labelChamp.TabIndex = 2;
            this.m_labelChamp.Text = "label1";
            this.m_labelChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_tooltip.SetToolTip(this.m_labelChamp, "Enter the field to test|161");
            this.m_labelChamp.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_panelComboChamp
            // 
            this.m_panelComboChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelComboChamp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelComboChamp.Controls.Add(this.m_labelChamp);
            this.m_panelComboChamp.Controls.Add(this.m_boutonDropList);
            this.m_panelComboChamp.Location = new System.Drawing.Point(88, 8);
            this.m_panelComboChamp.Name = "m_panelComboChamp";
            this.m_panelComboChamp.Size = new System.Drawing.Size(394, 21);
            this.m_panelComboChamp.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Operator|128";
            // 
            // m_cmbOperateur
            // 
            this.m_cmbOperateur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOperateur.Location = new System.Drawing.Point(88, 40);
            this.m_cmbOperateur.Name = "m_cmbOperateur";
            this.m_cmbOperateur.Size = new System.Drawing.Size(120, 21);
            this.m_cmbOperateur.TabIndex = 6;
            this.m_tooltip.SetToolTip(this.m_cmbOperateur, "Selected coparison operator|162");
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(0, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(221, 415);
            this.m_wndAide.TabIndex = 7;
			this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            this.m_wndAide.Load += new System.EventHandler(this.m_wndAide_Load);
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.BackColor = System.Drawing.SystemColors.Control;
            this.m_panelFiltre.Controls.Add(this.panel2);
            this.m_panelFiltre.Controls.Add(this.m_panelValeurAttendue);
            this.m_panelFiltre.Controls.Add(this.label4);
            this.m_panelFiltre.Controls.Add(this.m_txtCondition);
            this.m_panelFiltre.Controls.Add(this.m_btnAnnuler);
            this.m_panelFiltre.Controls.Add(this.m_btnOk);
            this.m_panelFiltre.Controls.Add(this.m_btnCreerVariable);
            this.m_panelFiltre.Controls.Add(this.label3);
            this.m_panelFiltre.Controls.Add(this.label2);
            this.m_panelFiltre.Controls.Add(this.m_panelComboChamp);
            this.m_panelFiltre.Controls.Add(this.m_cmbOperateur);
            this.m_panelFiltre.Controls.Add(this.label1);
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(490, 415);
            this.m_panelFiltre.TabIndex = 8;
            this.m_panelFiltre.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnValeur);
            this.panel2.Controls.Add(this.m_btnChamp);
            this.panel2.Location = new System.Drawing.Point(8, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 16);
            this.panel2.TabIndex = 18;
            // 
            // m_btnValeur
            // 
            this.m_btnValeur.Location = new System.Drawing.Point(6, 0);
            this.m_btnValeur.Name = "m_btnValeur";
            this.m_btnValeur.Size = new System.Drawing.Size(104, 16);
            this.m_btnValeur.TabIndex = 16;
            this.m_btnValeur.Text = "Value|61";
            this.m_btnValeur.CheckedChanged += new System.EventHandler(this.m_btnValeur_CheckedChanged);
            // 
            // m_btnChamp
            // 
            this.m_btnChamp.Location = new System.Drawing.Point(113, 0);
            this.m_btnChamp.Name = "m_btnChamp";
            this.m_btnChamp.Size = new System.Drawing.Size(104, 16);
            this.m_btnChamp.TabIndex = 17;
            this.m_btnChamp.Text = "Field|60";
            this.m_btnChamp.CheckedChanged += new System.EventHandler(this.m_btnChamp_CheckedChanged);
            // 
            // m_panelValeurAttendue
            // 
            this.m_panelValeurAttendue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelValeurAttendue.Controls.Add(this.m_panelValeurFormule);
            this.m_panelValeurAttendue.Controls.Add(this.m_panelValeurChamp);
            this.m_panelValeurAttendue.Location = new System.Drawing.Point(8, 96);
            this.m_panelValeurAttendue.Name = "m_panelValeurAttendue";
            this.m_panelValeurAttendue.Size = new System.Drawing.Size(473, 197);
            this.m_panelValeurAttendue.TabIndex = 15;
            // 
            // m_panelValeurFormule
            // 
            this.m_panelValeurFormule.Controls.Add(this.m_txtFormule);
            this.m_panelValeurFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelValeurFormule.Location = new System.Drawing.Point(0, 32);
            this.m_panelValeurFormule.Name = "m_panelValeurFormule";
            this.m_panelValeurFormule.Size = new System.Drawing.Size(473, 165);
            this.m_panelValeurFormule.TabIndex = 1;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(470, 162);
            this.m_txtFormule.TabIndex = 8;
            this.m_tooltip.SetToolTip(this.m_txtFormule, "Enter the formula returning the expected value for the comparison test" +
                    "|159");
            this.m_txtFormule.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // m_panelValeurChamp
            // 
            this.m_panelValeurChamp.Controls.Add(this.m_panelComboChampValeur);
            this.m_panelValeurChamp.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelValeurChamp.Location = new System.Drawing.Point(0, 0);
            this.m_panelValeurChamp.Name = "m_panelValeurChamp";
            this.m_panelValeurChamp.Size = new System.Drawing.Size(473, 32);
            this.m_panelValeurChamp.TabIndex = 0;
            // 
            // m_panelComboChampValeur
            // 
            this.m_panelComboChampValeur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelComboChampValeur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelComboChampValeur.Controls.Add(this.m_labelChampValeur);
            this.m_panelComboChampValeur.Controls.Add(this.m_boutonChampValeurDropList);
            this.m_panelComboChampValeur.Location = new System.Drawing.Point(4, 5);
            this.m_panelComboChampValeur.Name = "m_panelComboChampValeur";
            this.m_panelComboChampValeur.Size = new System.Drawing.Size(457, 21);
            this.m_panelComboChampValeur.TabIndex = 5;
            // 
            // m_labelChampValeur
            // 
            this.m_labelChampValeur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChampValeur.BackColor = System.Drawing.Color.White;
            this.m_labelChampValeur.Location = new System.Drawing.Point(1, 0);
            this.m_labelChampValeur.Name = "m_labelChampValeur";
            this.m_labelChampValeur.Size = new System.Drawing.Size(437, 17);
            this.m_labelChampValeur.TabIndex = 2;
            this.m_labelChampValeur.Text = "label1";
            this.m_labelChampValeur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_tooltip.SetToolTip(this.m_labelChampValeur, "Enter the field to test161");
            // 
            // m_boutonChampValeurDropList
            // 
            this.m_boutonChampValeurDropList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_boutonChampValeurDropList.BackColor = System.Drawing.SystemColors.Control;
            this.m_boutonChampValeurDropList.Image = ((System.Drawing.Image)(resources.GetObject("m_boutonChampValeurDropList.Image")));
            this.m_boutonChampValeurDropList.Location = new System.Drawing.Point(437, 0);
            this.m_boutonChampValeurDropList.Name = "m_boutonChampValeurDropList";
            this.m_boutonChampValeurDropList.Size = new System.Drawing.Size(17, 17);
            this.m_boutonChampValeurDropList.TabIndex = 3;
            this.m_boutonChampValeurDropList.TabStop = false;
            this.m_boutonChampValeurDropList.UseVisualStyleBackColor = false;
            this.m_boutonChampValeurDropList.Click += new System.EventHandler(this.m_boutonChampValeurDropList_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Enable condition|115";
            // 
            // m_txtCondition
            // 
            this.m_txtCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCondition.BackColor = System.Drawing.Color.White;
            this.m_txtCondition.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtCondition.Formule = null;
            this.m_txtCondition.Location = new System.Drawing.Point(8, 321);
            this.m_txtCondition.LockEdition = false;
            this.m_txtCondition.Name = "m_txtCondition";
            this.m_txtCondition.Size = new System.Drawing.Size(470, 56);
            this.m_txtCondition.TabIndex = 13;
            this.m_tooltip.SetToolTip(this.m_txtCondition, "This test will be integrated to the filter only if the value condition is equal to 1|160");
            this.m_txtCondition.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(257, 385);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.m_btnAnnuler.TabIndex = 12;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(129, 385);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.m_btnOk.TabIndex = 11;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCreerVariable
            // 
            this.m_btnCreerVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreerVariable.Location = new System.Drawing.Point(289, 80);
            this.m_btnCreerVariable.Name = "m_btnCreerVariable";
            this.m_btnCreerVariable.Size = new System.Drawing.Size(169, 16);
            this.m_btnCreerVariable.TabIndex = 10;
            this.m_btnCreerVariable.TabStop = true;
            this.m_btnCreerVariable.Text = "Create new variable|130";
            this.m_btnCreerVariable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCreerVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnCreerVariable_LinkClicked);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Expected value|129";
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuVariableCalculée,
            this.m_menuVariableSelection});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Entered|30010";
            this.m_menuVariableSaisie.Click += new System.EventHandler(this.m_menuVariableSaisie_Click);
            // 
            // m_menuVariableCalculée
            // 
            this.m_menuVariableCalculée.Index = 1;
            this.m_menuVariableCalculée.Text = "Computed|30011";
            this.m_menuVariableCalculée.Click += new System.EventHandler(this.m_menuVariableCalculée_Click);
            // 
            // m_menuVariableSelection
            // 
            this.m_menuVariableSelection.Index = 2;
            this.m_menuVariableSelection.Text = "Selection|30012";
            this.m_menuVariableSelection.Click += new System.EventHandler(this.m_menuVariableSelection_Click);
            // 
            // m_tooltip
            // 
            this.m_tooltip.Popup += new System.Windows.Forms.PopupEventHandler(this.m_tooltip_Popup);
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
            this.m_splitContainer.Panel1.Controls.Add(this.m_panelFiltre);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAide);
            this.m_splitContainer.Size = new System.Drawing.Size(723, 419);
            this.m_splitContainer.SplitterDistance = 494;
            this.m_splitContainer.TabIndex = 9;
            // 
            // CFormEditComposantFiltreValeurChamp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(723, 419);
            this.Controls.Add(this.m_splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditComposantFiltreValeurChamp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Field test|248";
            this.Load += new System.EventHandler(this.CFormEditComposantFiltreValeurChamp_Load);
            this.m_panelComboChamp.ResumeLayout(false);
            this.m_panelFiltre.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_panelValeurAttendue.ResumeLayout(false);
            this.m_panelValeurFormule.ResumeLayout(false);
            this.m_panelValeurChamp.ResumeLayout(false);
            this.m_panelComboChampValeur.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_boutonDropList_Click(object sender, System.EventArgs e)
		{
			Rectangle rect = m_panelComboChamp.RectangleToScreen(new Rectangle ( 0, m_panelComboChamp.Height, m_panelComboChamp.Width, 230));
			bool bCancel = false;
			CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp( rect, m_filtre.TypeElements , m_fournisseurProprietesFiltrees, ref bCancel, null, m_definitionRacineDeChampsFiltres );
			if ( !bCancel )
			{
				m_champ = champ;
                m_labelChamp.Text = m_champ == null ? I.T("[UNDEFINED]|30013") : m_champ.Nom;
			}
				
		}

		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_boutonChampValeurDropList_Click(object sender, System.EventArgs e)
		{
			Rectangle rect = m_panelComboChampValeur.RectangleToScreen(new Rectangle ( 0, m_panelComboChampValeur.Height, m_panelComboChampValeur.Width, 230));
			bool bCancel = false;
			CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp( rect, m_filtre.TypeElements , new CFournisseurProprietesForFiltreDynamique(), ref bCancel, null, m_definitionRacineDeChampsFiltres );
			if ( !bCancel )
			{
				m_champValeur = champ;
                m_labelChampValeur.Text = m_champValeur == null ? I.T("[UNDEFINED]|30013") : m_champValeur.Nom;
			}
				
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void Init ( CComposantFiltreDynamiqueValeurChamp composant, CFiltreDynamique filtre, CDefinitionProprieteDynamique definitionRacineDeChampsFiltres )
		{
			m_composant = composant;
			m_filtre = filtre;
			m_definitionRacineDeChampsFiltres = definitionRacineDeChampsFiltres;
			
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public static bool EditeComposantValeurChamp ( 
			CComposantFiltreDynamiqueValeurChamp composant,
			CFiltreDynamique filtre,
			bool bAvecVariables,
			CDefinitionProprieteDynamique definitionRacineDeChampsFiltres/*pour traduction*/,
            IFournisseurProprietesDynamiques fournisseurProprietesFiltrées)
		{
			CFormEditComposantFiltreValeurChamp form = new CFormEditComposantFiltreValeurChamp();
			form.Init ( composant, filtre, definitionRacineDeChampsFiltres );
			if (!bAvecVariables )
				form.m_btnCreerVariable.Visible = false;
            form.m_fournisseurProprietesFiltrees = fournisseurProprietesFiltrées;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}


		/// ////////////////////////////////////////////////////////////////////////////
		private void CFormEditComposantFiltreValeurChamp_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            InitDialog();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public void InitDialog()
		{
            m_labelChamp.Text = m_composant.Champ == null ? I.T("[UNDEFINED]|30013") : m_composant.Champ.Nom;
			m_champ = m_composant.Champ;
			m_champValeur = m_composant.ChampValeur;
			InitOperateurs();
			m_cmbOperateur.SelectedValue = m_composant.IdOperateur;
			m_wndAide.FournisseurProprietes = m_filtre;
			m_wndAide.ObjetInterroge = typeof(CFiltreDynamique);

			if ( m_composant.ChampValeur != null )
				m_btnChamp.Checked = true;
			else
				m_btnValeur.Checked = true;
            m_labelChampValeur.Text = m_composant.ChampValeur == null ? I.T("[UNDEFINED]|30013") : m_composant.ChampValeur.Nom;

			m_txtFormule.Text = m_composant.ExpressionValeur == null?"":m_composant.ExpressionValeur.GetString();
			m_txtFormule.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);

			m_txtCondition.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtCondition.Text = m_composant.ConditionApplication.GetString();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void InitOperateurs()
		{
			DataTable table = new DataTable();
			table.Columns.Add ( c_champLibelle, typeof(string));
			table.Columns.Add ( c_champIdOperateur, typeof(string));
			foreach ( COperateurAnalysable operateur in CComposantFiltreOperateur.m_operateurs )
			{
				if ( operateur.Niveau == 3 )//comparaison
				{
					DataRow row = table.NewRow();
					row[c_champLibelle] = operateur.Texte;
					row[c_champIdOperateur] = operateur.Id;
					table.Rows.Add ( row );
				}
			}
			m_cmbOperateur.DataSource = table;
			m_cmbOperateur.DisplayMember = c_champLibelle;
			m_cmbOperateur.ValueMember = c_champIdOperateur;

			m_cmbOperateur.SelectedIndex = 0;
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_btnCreerVariable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_menuNewVariable.Show ( m_btnCreerVariable, new Point ( 0, m_btnCreerVariable.Height ) );
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableSaisie_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie ( m_filtre );
			if ( CFormEditVariableDynamiqueSaisie.EditeVariable ( variable, m_filtre ) )
			{
				m_filtre.AddVariable ( variable );
				m_wndAide.FournisseurProprietes = m_filtre;
				m_wndAide.RefillChamps();
			}
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_lastTextBox != null )
				m_wndAide.InsereInTextBox ( m_lastTextBox, nPosCurseur, strCommande );
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_champ == null )
			{
				CFormAlerte.Afficher(I.T("Select a field to test|30014"), EFormAlerteType.Exclamation);
				return;
			}
			if ( m_cmbOperateur.SelectedValue == null || !(m_cmbOperateur.SelectedValue is string)) 
			{
				CFormAlerte.Afficher(I.T("Select an operator|30009"), EFormAlerteType.Exclamation);
				return;
			}

			CResultAErreur result = CResultAErreur.True;

			C2iExpression expressionValeur = null;
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( m_filtre, typeof(CFiltreDynamique) );
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
			if ( m_btnValeur.Checked )
			{
				result = analyseur.AnalyseChaine ( m_txtFormule.Text );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in value formula|30015"));
					CFormAlerte.Afficher ( result);
					return;
				}
				expressionValeur = (C2iExpression)result.Data;
			}
			else
			{
				if (m_champValeur == null )
				{
					result.EmpileErreur(I.T("Select a value field|30018"));
					CFormAlerte.Afficher ( result);
					return ;
				}
			}

			result = analyseur.AnalyseChaine ( m_txtCondition.Text );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in codition formula|30016"));
				CFormAlerte.Afficher ( result);
				return;
			}
			C2iExpression expressionCondition = (C2iExpression)result.Data;

			
			
			m_composant.Champ = m_champ;
			m_composant.IdOperateur = (string)m_cmbOperateur.SelectedValue;
			m_composant.ExpressionValeur = expressionValeur;
			m_composant.ChampValeur = m_champValeur;
			m_composant.ConditionApplication = expressionCondition;
			DialogResult = DialogResult.OK;
			Close();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableCalculée_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(m_filtre );
			if ( CFormEditVariableFiltreCalculee.EditeVariable(variable, m_filtre) )
			{
				m_filtre.AddVariable ( variable );
				m_wndAide.RefillChamps();
			}
		}

		private void m_txtFormule_Enter(object sender, System.EventArgs e)
		{
			if ( !(sender is sc2i.win32.expression.CControleEditeFormule) )
				return;
			if ( m_lastTextBox != null )
			{
				m_lastTextBox.BackColor = Color.White;
			}
			m_lastTextBox = (sc2i.win32.expression.CControleEditeFormule)sender;
			m_lastTextBox.BackColor = Color.LightGreen;
		}

		private void m_menuVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_filtre );
			if ( CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable(variable) )
			{
                m_filtre.AddVariable(variable);
				m_wndAide.RefillChamps();
			}
		}

		private void m_btnValeur_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateVisu();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void UpdateVisu()
		{
			m_panelValeurChamp.Visible = m_btnChamp.Checked;
			m_panelValeurFormule.Visible = m_btnValeur.Checked;
		}

		private void m_btnChamp_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateVisu();
		}

        private void m_wndAide_Load(object sender, EventArgs e)
        {

        }

        private void m_tooltip_Popup(object sender, PopupEventArgs e)
        {

        }
		


	}
}
