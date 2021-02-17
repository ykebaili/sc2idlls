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
	/// Description résumée de CFormEditComposantFiltreSousFiltre.
	/// </summary>
	public class CFormEditComposantFiltreSousFiltre : System.Windows.Forms.Form
	{
		private CDefinitionProprieteDynamique m_definitionRacineDeChampsFiltres = null;

		private CDefinitionProprieteDynamique m_champTest = null;
		private CDefinitionProprieteDynamique m_champRetourneParRequete = null;

        private IFournisseurProprietesDynamiques m_fournisseurProprietesFiltrees = null;

		private CComposantFiltreDynamiqueSousFiltre m_composant = null;
		private CFiltreDynamique m_filtre = null;

        private CFiltreDynamique m_sousFiltre = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_boutonDropList;
		private System.Windows.Forms.Label m_labelChamp;
        private System.Windows.Forms.Panel m_panelComboChamp;
        private System.Windows.Forms.Panel m_panelFiltre;
		private System.Windows.Forms.LinkLabel m_btnCreerVariable;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.MenuItem m_menuVariableSaisie;
		private System.Windows.Forms.ContextMenu m_menuNewVariable;
        private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private System.Windows.Forms.MenuItem m_menuVariableCalculée;
		private System.Windows.Forms.MenuItem m_menuVariableSelection;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.expression.CControleEditeFormule m_txtCondition;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel m_panelValeurAttendue;
		private System.Windows.Forms.Panel m_panelValeurChamp;
		private System.Windows.Forms.Panel m_panelValeurFormule;
		private System.Windows.Forms.RadioButton m_btnIn;
		private System.Windows.Forms.RadioButton m_btnNotIn;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button m_boutonChampValeurDropList;
		private System.Windows.Forms.Label m_labelChampValeur;
		private System.Windows.Forms.Panel m_panelComboChampValeur;
        private SplitContainer m_splitContainer;
        private CPanelEditFiltreDynamique m_panelSousFiltre;
        private Label label2;
        private CExtStyle m_extStyle;

		private sc2i.win32.expression.CControleEditeFormule m_lastTextBox = null;

		public CFormEditComposantFiltreSousFiltre()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			m_lastTextBox = m_txtCondition;
            m_txtCondition.BackColor = Color.LightGreen;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditComposantFiltreSousFiltre));
            this.label1 = new System.Windows.Forms.Label();
            this.m_boutonDropList = new System.Windows.Forms.Button();
            this.m_labelChamp = new System.Windows.Forms.Label();
            this.m_panelComboChamp = new System.Windows.Forms.Panel();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelFiltre = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnIn = new System.Windows.Forms.RadioButton();
            this.m_btnNotIn = new System.Windows.Forms.RadioButton();
            this.m_panelValeurAttendue = new System.Windows.Forms.Panel();
            this.m_panelValeurFormule = new System.Windows.Forms.Panel();
            this.m_panelSousFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.m_panelValeurChamp = new System.Windows.Forms.Panel();
            this.m_panelComboChampValeur = new System.Windows.Forms.Panel();
            this.m_labelChampValeur = new System.Windows.Forms.Label();
            this.m_boutonChampValeurDropList = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtCondition = new sc2i.win32.expression.CControleEditeFormule();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCreerVariable = new System.Windows.Forms.LinkLabel();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
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
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_extStyle.SetStyleBackColor(this.m_boutonDropList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_boutonDropList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_extStyle.SetStyleBackColor(this.m_labelChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_labelChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_extStyle.SetStyleBackColor(this.m_panelComboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelComboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelComboChamp.TabIndex = 4;
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(0, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(221, 415);
            this.m_extStyle.SetStyleBackColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAide.TabIndex = 7;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFiltre.Controls.Add(this.panel2);
            this.m_panelFiltre.Controls.Add(this.m_panelValeurAttendue);
            this.m_panelFiltre.Controls.Add(this.label4);
            this.m_panelFiltre.Controls.Add(this.m_txtCondition);
            this.m_panelFiltre.Controls.Add(this.m_btnAnnuler);
            this.m_panelFiltre.Controls.Add(this.m_btnOk);
            this.m_panelFiltre.Controls.Add(this.m_btnCreerVariable);
            this.m_panelFiltre.Controls.Add(this.m_panelComboChamp);
            this.m_panelFiltre.Controls.Add(this.label1);
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.ForeColor = System.Drawing.Color.Black;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(490, 415);
            this.m_extStyle.SetStyleBackColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelFiltre.TabIndex = 8;
            this.m_panelFiltre.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnIn);
            this.panel2.Controls.Add(this.m_btnNotIn);
            this.panel2.Location = new System.Drawing.Point(88, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 16);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 18;
            // 
            // m_btnIn
            // 
            this.m_btnIn.Location = new System.Drawing.Point(6, 0);
            this.m_btnIn.Name = "m_btnIn";
            this.m_btnIn.Size = new System.Drawing.Size(104, 16);
            this.m_extStyle.SetStyleBackColor(this.m_btnIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnIn.TabIndex = 16;
            this.m_btnIn.Text = "In|20095";
            // 
            // m_btnNotIn
            // 
            this.m_btnNotIn.Location = new System.Drawing.Point(113, 0);
            this.m_btnNotIn.Name = "m_btnNotIn";
            this.m_btnNotIn.Size = new System.Drawing.Size(104, 16);
            this.m_extStyle.SetStyleBackColor(this.m_btnNotIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnNotIn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnNotIn.TabIndex = 17;
            this.m_btnNotIn.Text = "Not in|20096";
            // 
            // m_panelValeurAttendue
            // 
            this.m_panelValeurAttendue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelValeurAttendue.Controls.Add(this.m_panelValeurFormule);
            this.m_panelValeurAttendue.Controls.Add(this.m_panelValeurChamp);
            this.m_panelValeurAttendue.Location = new System.Drawing.Point(8, 52);
            this.m_panelValeurAttendue.Name = "m_panelValeurAttendue";
            this.m_panelValeurAttendue.Size = new System.Drawing.Size(473, 241);
            this.m_extStyle.SetStyleBackColor(this.m_panelValeurAttendue, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelValeurAttendue, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelValeurAttendue.TabIndex = 15;
            // 
            // m_panelValeurFormule
            // 
            this.m_panelValeurFormule.Controls.Add(this.m_panelSousFiltre);
            this.m_panelValeurFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelValeurFormule.Location = new System.Drawing.Point(0, 0);
            this.m_panelValeurFormule.Name = "m_panelValeurFormule";
            this.m_panelValeurFormule.Size = new System.Drawing.Size(473, 209);
            this.m_extStyle.SetStyleBackColor(this.m_panelValeurFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelValeurFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelValeurFormule.TabIndex = 1;
            // 
            // m_panelSousFiltre
            // 
            this.m_panelSousFiltre.BackColor = System.Drawing.Color.White;
            this.m_panelSousFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelSousFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelSousFiltre.FiltreDynamique = null;
            this.m_panelSousFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelSousFiltre.LockEdition = false;
            this.m_panelSousFiltre.ModeFiltreExpression = false;
            this.m_panelSousFiltre.ModeSansType = false;
            this.m_panelSousFiltre.Name = "m_panelSousFiltre";
            this.m_panelSousFiltre.Size = new System.Drawing.Size(473, 209);
            this.m_extStyle.SetStyleBackColor(this.m_panelSousFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelSousFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelSousFiltre.TabIndex = 0;
            this.m_panelSousFiltre.OnChangeTypeElements += new sc2i.win32.data.dynamic.ChangeTypeElementsEventHandler(this.m_panelSousFiltre_OnChangeTypeElements);
            // 
            // m_panelValeurChamp
            // 
            this.m_panelValeurChamp.Controls.Add(this.label2);
            this.m_panelValeurChamp.Controls.Add(this.m_panelComboChampValeur);
            this.m_panelValeurChamp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelValeurChamp.Location = new System.Drawing.Point(0, 209);
            this.m_panelValeurChamp.Name = "m_panelValeurChamp";
            this.m_panelValeurChamp.Size = new System.Drawing.Size(473, 32);
            this.m_extStyle.SetStyleBackColor(this.m_panelValeurChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelValeurChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelValeurChamp.TabIndex = 0;
            // 
            // m_panelComboChampValeur
            // 
            this.m_panelComboChampValeur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelComboChampValeur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelComboChampValeur.Controls.Add(this.m_labelChampValeur);
            this.m_panelComboChampValeur.Controls.Add(this.m_boutonChampValeurDropList);
            this.m_panelComboChampValeur.Location = new System.Drawing.Point(86, 5);
            this.m_panelComboChampValeur.Name = "m_panelComboChampValeur";
            this.m_panelComboChampValeur.Size = new System.Drawing.Size(375, 21);
            this.m_extStyle.SetStyleBackColor(this.m_panelComboChampValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelComboChampValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelComboChampValeur.TabIndex = 5;
            // 
            // m_labelChampValeur
            // 
            this.m_labelChampValeur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChampValeur.BackColor = System.Drawing.Color.White;
            this.m_labelChampValeur.Location = new System.Drawing.Point(1, 0);
            this.m_labelChampValeur.Name = "m_labelChampValeur";
            this.m_labelChampValeur.Size = new System.Drawing.Size(355, 17);
            this.m_extStyle.SetStyleBackColor(this.m_labelChampValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_labelChampValeur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_boutonChampValeurDropList.Location = new System.Drawing.Point(355, 0);
            this.m_boutonChampValeurDropList.Name = "m_boutonChampValeurDropList";
            this.m_boutonChampValeurDropList.Size = new System.Drawing.Size(17, 17);
            this.m_extStyle.SetStyleBackColor(this.m_boutonChampValeurDropList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_boutonChampValeurDropList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_extStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_extStyle.SetStyleBackColor(this.m_txtCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtCondition.TabIndex = 13;
            this.m_tooltip.SetToolTip(this.m_txtCondition, "This test will be integrated to the filter only if the value condition is equal t" +
                    "o 1|160");
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
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 11;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCreerVariable
            // 
            this.m_btnCreerVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreerVariable.Location = new System.Drawing.Point(342, 30);
            this.m_btnCreerVariable.Name = "m_btnCreerVariable";
            this.m_btnCreerVariable.Size = new System.Drawing.Size(140, 16);
            this.m_extStyle.SetStyleBackColor(this.m_btnCreerVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnCreerVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCreerVariable.TabIndex = 10;
            this.m_btnCreerVariable.TabStop = true;
            this.m_btnCreerVariable.Text = "Create new variable|130";
            this.m_btnCreerVariable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCreerVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnCreerVariable_LinkClicked);
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
            this.m_extStyle.SetStyleBackColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_splitContainer.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAide);
            this.m_extStyle.SetStyleBackColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_splitContainer.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.Size = new System.Drawing.Size(723, 419);
            this.m_splitContainer.SplitterDistance = 494;
            this.m_extStyle.SetStyleBackColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_splitContainer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitContainer.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 27);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sub filter value|20099";
            // 
            // CFormEditComposantFiltreSousFiltre
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(723, 419);
            this.Controls.Add(this.m_splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditComposantFiltreSousFiltre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Sub filter|20100";
            this.Load += new System.EventHandler(this.CFormEditComposantFiltreSousFiltre_Load);
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
			CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp( rect, m_filtre.TypeElements , m_fournisseurProprietesFiltrees, ref bCancel, null, null );
			if ( !bCancel )
			{
				m_champTest = champ;
                m_labelChamp.Text = m_champTest == null ? I.T("[UNDEFINED]|30013") : m_champTest.Nom;
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
            if (m_sousFiltre.TypeElements == null)
            {
                CFormAlerte.Afficher(I.T("Select a sub filter type before|20087"),
                    EFormAlerteBoutons.Ok,
                    EFormAlerteType.Exclamation
                    );
                return;
            }
            CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp(rect, m_sousFiltre.TypeElements, new CFournisseurProprietesForFiltreDynamique(), ref bCancel, null, m_definitionRacineDeChampsFiltres);
			if ( !bCancel )
			{
				m_champRetourneParRequete = champ;
                m_labelChampValeur.Text = m_champRetourneParRequete == null ? I.T("[UNDEFINED]|30013") : m_champRetourneParRequete.Nom;
			}
				
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void Init ( CComposantFiltreDynamiqueSousFiltre composant, 
            CFiltreDynamique filtre, 
            CDefinitionProprieteDynamique definitionRacineDeChampsFiltres )
		{
			m_composant = composant;
			m_filtre = filtre;
			m_definitionRacineDeChampsFiltres = definitionRacineDeChampsFiltres;
			
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public static bool EditeComposantSousFiltre (
            CComposantFiltreDynamiqueSousFiltre composant,
			CFiltreDynamique filtre,
			bool bAvecVariables,
			CDefinitionProprieteDynamique definitionRacineDeChampsFiltres/*pour traduction*/,
            IFournisseurProprietesDynamiques fournisseurProprietesFiltrées)
		{
			CFormEditComposantFiltreSousFiltre form = new CFormEditComposantFiltreSousFiltre();
			form.Init ( composant, filtre, definitionRacineDeChampsFiltres );
			if (!bAvecVariables )
				form.m_btnCreerVariable.Visible = false;
            form.m_fournisseurProprietesFiltrees = fournisseurProprietesFiltrées;
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}


		/// ////////////////////////////////////////////////////////////////////////////
		private void CFormEditComposantFiltreSousFiltre_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            InitDialog();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public void InitDialog()
		{
            m_labelChamp.Text = m_composant.Champ == null ? I.T("[UNDEFINED]|30013") : m_composant.Champ.Nom;
			m_champTest = m_composant.Champ;
			m_champRetourneParRequete = m_composant.ChampRetourneParSousFiltre;
			m_wndAide.FournisseurProprietes = m_filtre;
			m_wndAide.ObjetInterroge = typeof(CFiltreDynamique);

            m_btnIn.Checked = !m_composant.IsNotInTest;
            m_btnNotIn.Checked = m_composant.IsNotInTest;

            m_labelChampValeur.Text = m_champRetourneParRequete == null ? I.T("[UNDEFINED]|30013") : m_champRetourneParRequete.Nom;

            m_sousFiltre = m_composant.SousFiltre;
            if (m_sousFiltre == null)
                m_sousFiltre = new CFiltreDynamique();
            m_sousFiltre.ElementAVariablesExterne = m_filtre;


            m_panelSousFiltre.InitSansVariables(m_sousFiltre);

			m_txtCondition.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtCondition.Text = m_composant.ConditionApplication.GetString();
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
			if ( m_champTest == null )
			{
				CFormAlerte.Afficher(I.T("Select a field to test|30014"), EFormAlerteType.Exclamation);
				return;
			}
			

			CResultAErreur result = CResultAErreur.True;

			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( m_filtre, typeof(CFiltreDynamique) );
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);
            if (m_champRetourneParRequete == null )
			{
				result.EmpileErreur(I.T("Select a value field|30018"));
				CFormAlerte.Afficher ( result);
				return ;
			}

			result = analyseur.AnalyseChaine ( m_txtCondition.Text );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in codition formula|30016"));
				CFormAlerte.Afficher ( result);
				return;
			}
			C2iExpression expressionCondition = (C2iExpression)result.Data;



            m_composant.Champ = m_champTest;
            m_composant.ChampRetourneParSousFiltre = m_champRetourneParRequete;
            m_composant.IsNotInTest = m_btnNotIn.Checked;
            m_composant.SousFiltre = m_panelSousFiltre.FiltreDynamique;
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

        /// ////////////////////////////////////////////////////////////////////////////
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

        /// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_filtre );
			if ( CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable(variable) )
			{
                m_filtre.AddVariable(variable);
				m_wndAide.RefillChamps();
			}
		}

        /// ////////////////////////////////////////////////////////////////////////////
        private void m_panelSousFiltre_OnChangeTypeElements(object sender, Type typeSelectionne)
        {
            
        }

        
		


	}
}
