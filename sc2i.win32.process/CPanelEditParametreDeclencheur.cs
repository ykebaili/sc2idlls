using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.process;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CPanelEditParametreDeclencheur.
	/// </summary>
	public class CPanelEditParametreDeclencheur : System.Windows.Forms.UserControl, IControlALockEdition	
	{
		private static ISelectionneurGroupesUtilisateurs m_selectionneurGroupes = null;
		private CDbKey[] m_keysGroupes = new CDbKey[0];
		private bool m_bAutoriseSurCreation = true;
        private bool m_bAutoriseSurSuppression = true;
		private bool m_bAutoriseSurModification = true;
		private bool m_bAutoriseSurDate = true;
		private bool m_bAutoriseSurManuel = true;
		private CParametreDeclencheurEvenement m_parametreDeclencheur = new CParametreDeclencheurEvenement();
		private sc2i.win32.common.C2iPanel m_panelDeclencheur;
		private sc2i.win32.common.C2iPanel m_panelConditionDeclenchement;
		private sc2i.win32.expression.CControleEditeFormule m_txtCondition;
		private System.Windows.Forms.Label label6;
		private sc2i.win32.common.C2iPanel m_panelSurModificationOuDate;
		
		private sc2i.win32.common.C2iPanel m_panelDate;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleDate;
		private sc2i.win32.common.CComboboxAutoFilled m_cmbValeurSurveillee;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iPanel m_panelModif;
		private sc2i.win32.expression.CControleEditeFormule m_txtValeurApres;
		private sc2i.win32.expression.CControleEditeFormule m_txtValeurAvant;
		private System.Windows.Forms.CheckBox m_chkValeurApres;
		private System.Windows.Forms.CheckBox m_chkValeurAvant;
		private System.Windows.Forms.RadioButton m_chkTypeDate;
		private System.Windows.Forms.RadioButton m_chkTypeModification;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton m_chkTypeCreation;
		private System.Windows.Forms.RadioButton m_chkTypeManuel;
		private sc2i.win32.expression.CControleEditeFormule m_receveurFormules = null;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private sc2i.win32.common.C2iPanel m_panelGauche;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitter2;
		private sc2i.win32.common.C2iPanel m_panelValeurAvant;
		private sc2i.win32.common.C2iPanel m_panelValeurApres;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox m_txtCodeDeclencheur;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel m_panelManuel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel m_panelDeclEtManuel;
		private System.Windows.Forms.LinkLabel m_lnkGroupes;
		private sc2i.win32.common.C2iTextBox m_txtMenuManuel;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.Windows.Forms.Panel m_panelOrdre;
		private System.Windows.Forms.Label label8;
		private sc2i.win32.common.C2iNumericUpDown m_numUpOrdre;
		private C2iPanel m_panelSpecifique;
		private CComboboxAutoFilled m_cmbEvtSpecifique;
		private Label label9;
		private RadioButton m_chkTypeSpecifique;
		private CheckBox m_chkMasquerBarreProgression;
        private RadioButton m_chkTypeDelete;
        private LinkLabel m_lnkExceptions;
		private System.ComponentModel.IContainer components;

		public CPanelEditParametreDeclencheur()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

			m_receveurFormules = m_txtCondition;
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
            this.m_panelDeclencheur = new sc2i.win32.common.C2iPanel(this.components);
            this.m_panelConditionDeclenchement = new sc2i.win32.common.C2iPanel(this.components);
            this.m_txtCondition = new sc2i.win32.expression.CControleEditeFormule();
            this.label6 = new System.Windows.Forms.Label();
            this.m_panelSpecifique = new sc2i.win32.common.C2iPanel(this.components);
            this.m_cmbEvtSpecifique = new sc2i.win32.common.CComboboxAutoFilled();
            this.label9 = new System.Windows.Forms.Label();
            this.m_panelSurModificationOuDate = new sc2i.win32.common.C2iPanel(this.components);
            this.m_cmbValeurSurveillee = new sc2i.win32.common.CComboboxAutoFilled();
            this.label3 = new System.Windows.Forms.Label();
            this.m_panelModif = new sc2i.win32.common.C2iPanel(this.components);
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_panelValeurAvant = new sc2i.win32.common.C2iPanel(this.components);
            this.m_chkValeurAvant = new System.Windows.Forms.CheckBox();
            this.m_txtValeurAvant = new sc2i.win32.expression.CControleEditeFormule();
            this.m_panelValeurApres = new sc2i.win32.common.C2iPanel(this.components);
            this.m_chkValeurApres = new System.Windows.Forms.CheckBox();
            this.m_txtValeurApres = new sc2i.win32.expression.CControleEditeFormule();
            this.m_panelDate = new sc2i.win32.common.C2iPanel(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtFormuleDate = new sc2i.win32.expression.CControleEditeFormule();
            this.m_chkTypeDate = new System.Windows.Forms.RadioButton();
            this.m_chkTypeModification = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.m_chkTypeCreation = new System.Windows.Forms.RadioButton();
            this.m_chkTypeManuel = new System.Windows.Forms.RadioButton();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelGauche = new sc2i.win32.common.C2iPanel(this.components);
            this.m_lnkExceptions = new System.Windows.Forms.LinkLabel();
            this.m_panelOrdre = new System.Windows.Forms.Panel();
            this.m_numUpOrdre = new sc2i.win32.common.C2iNumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.m_panelDeclEtManuel = new System.Windows.Forms.Panel();
            this.m_panelManuel = new System.Windows.Forms.Panel();
            this.m_chkMasquerBarreProgression = new System.Windows.Forms.CheckBox();
            this.m_txtMenuManuel = new sc2i.win32.common.C2iTextBox();
            this.m_lnkGroupes = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtCodeDeclencheur = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkTypeSpecifique = new System.Windows.Forms.RadioButton();
            this.m_chkTypeDelete = new System.Windows.Forms.RadioButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_panelDeclencheur.SuspendLayout();
            this.m_panelConditionDeclenchement.SuspendLayout();
            this.m_panelSpecifique.SuspendLayout();
            this.m_panelSurModificationOuDate.SuspendLayout();
            this.m_panelModif.SuspendLayout();
            this.m_panelValeurAvant.SuspendLayout();
            this.m_panelValeurApres.SuspendLayout();
            this.m_panelDate.SuspendLayout();
            this.m_panelGauche.SuspendLayout();
            this.m_panelOrdre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUpOrdre)).BeginInit();
            this.m_panelDeclEtManuel.SuspendLayout();
            this.m_panelManuel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelDeclencheur
            // 
            this.m_panelDeclencheur.Controls.Add(this.m_panelConditionDeclenchement);
            this.m_panelDeclencheur.Controls.Add(this.m_panelSpecifique);
            this.m_panelDeclencheur.Controls.Add(this.m_panelSurModificationOuDate);
            this.m_panelDeclencheur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDeclencheur.Location = new System.Drawing.Point(0, 60);
            this.m_panelDeclencheur.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDeclencheur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDeclencheur.Name = "m_panelDeclencheur";
            this.m_panelDeclencheur.Size = new System.Drawing.Size(651, 236);
            this.m_panelDeclencheur.TabIndex = 5;
            // 
            // m_panelConditionDeclenchement
            // 
            this.m_panelConditionDeclenchement.Controls.Add(this.m_txtCondition);
            this.m_panelConditionDeclenchement.Controls.Add(this.label6);
            this.m_panelConditionDeclenchement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelConditionDeclenchement.Location = new System.Drawing.Point(0, 155);
            this.m_panelConditionDeclenchement.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelConditionDeclenchement, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelConditionDeclenchement.Name = "m_panelConditionDeclenchement";
            this.m_panelConditionDeclenchement.Size = new System.Drawing.Size(651, 81);
            this.m_panelConditionDeclenchement.TabIndex = 1;
            // 
            // m_txtCondition
            // 
            this.m_txtCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCondition.BackColor = System.Drawing.Color.White;
            this.m_txtCondition.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtCondition.Formule = null;
            this.m_txtCondition.Location = new System.Drawing.Point(8, 16);
            this.m_txtCondition.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtCondition, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtCondition.Name = "m_txtCondition";
            this.m_txtCondition.Size = new System.Drawing.Size(635, 62);
            this.m_txtCondition.TabIndex = 0;
            this.m_txtCondition.Enter += new System.EventHandler(this.ZoneAForumle_Enter);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(276, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Triggering condition|194";
            // 
            // m_panelSpecifique
            // 
            this.m_panelSpecifique.Controls.Add(this.m_cmbEvtSpecifique);
            this.m_panelSpecifique.Controls.Add(this.label9);
            this.m_panelSpecifique.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelSpecifique.Location = new System.Drawing.Point(0, 120);
            this.m_panelSpecifique.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelSpecifique, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelSpecifique.Name = "m_panelSpecifique";
            this.m_panelSpecifique.Size = new System.Drawing.Size(651, 35);
            this.m_panelSpecifique.TabIndex = 5;
            this.m_panelSpecifique.Visible = false;
            // 
            // m_cmbEvtSpecifique
            // 
            this.m_cmbEvtSpecifique.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbEvtSpecifique.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbEvtSpecifique.IsLink = false;
            this.m_cmbEvtSpecifique.ListDonnees = null;
            this.m_cmbEvtSpecifique.Location = new System.Drawing.Point(124, 3);
            this.m_cmbEvtSpecifique.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbEvtSpecifique, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbEvtSpecifique.Name = "m_cmbEvtSpecifique";
            this.m_cmbEvtSpecifique.NullAutorise = true;
            this.m_cmbEvtSpecifique.ProprieteAffichee = null;
            this.m_cmbEvtSpecifique.Size = new System.Drawing.Size(519, 21);
            this.m_cmbEvtSpecifique.TabIndex = 2;
            this.m_cmbEvtSpecifique.TextNull = "(empty)";
            this.m_cmbEvtSpecifique.Tri = true;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label9, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 21);
            this.label9.TabIndex = 1;
            this.label9.Text = "Event|30027";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // m_panelSurModificationOuDate
            // 
            this.m_panelSurModificationOuDate.Controls.Add(this.m_cmbValeurSurveillee);
            this.m_panelSurModificationOuDate.Controls.Add(this.label3);
            this.m_panelSurModificationOuDate.Controls.Add(this.m_panelModif);
            this.m_panelSurModificationOuDate.Controls.Add(this.m_panelDate);
            this.m_panelSurModificationOuDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelSurModificationOuDate.Location = new System.Drawing.Point(0, 0);
            this.m_panelSurModificationOuDate.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelSurModificationOuDate, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelSurModificationOuDate.Name = "m_panelSurModificationOuDate";
            this.m_panelSurModificationOuDate.Size = new System.Drawing.Size(651, 120);
            this.m_panelSurModificationOuDate.TabIndex = 4;
            // 
            // m_cmbValeurSurveillee
            // 
            this.m_cmbValeurSurveillee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbValeurSurveillee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbValeurSurveillee.IsLink = false;
            this.m_cmbValeurSurveillee.ListDonnees = null;
            this.m_cmbValeurSurveillee.Location = new System.Drawing.Point(124, 8);
            this.m_cmbValeurSurveillee.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbValeurSurveillee, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbValeurSurveillee.Name = "m_cmbValeurSurveillee";
            this.m_cmbValeurSurveillee.NullAutorise = true;
            this.m_cmbValeurSurveillee.ProprieteAffichee = null;
            this.m_cmbValeurSurveillee.Size = new System.Drawing.Size(519, 21);
            this.m_cmbValeurSurveillee.TabIndex = 0;
            this.m_cmbValeurSurveillee.TextNull = "(empty)";
            this.m_cmbValeurSurveillee.Tri = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Watched value|190";
            // 
            // m_panelModif
            // 
            this.m_panelModif.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelModif.BackColor = System.Drawing.SystemColors.Control;
            this.m_panelModif.Controls.Add(this.splitter2);
            this.m_panelModif.Controls.Add(this.m_panelValeurAvant);
            this.m_panelModif.Controls.Add(this.m_panelValeurApres);
            this.m_panelModif.Location = new System.Drawing.Point(0, 32);
            this.m_panelModif.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelModif, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelModif.Name = "m_panelModif";
            this.m_panelModif.Size = new System.Drawing.Size(651, 80);
            this.m_panelModif.TabIndex = 4004;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(392, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 80);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // m_panelValeurAvant
            // 
            this.m_panelValeurAvant.Controls.Add(this.m_chkValeurAvant);
            this.m_panelValeurAvant.Controls.Add(this.m_txtValeurAvant);
            this.m_panelValeurAvant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelValeurAvant.Location = new System.Drawing.Point(0, 0);
            this.m_panelValeurAvant.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelValeurAvant, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelValeurAvant.Name = "m_panelValeurAvant";
            this.m_panelValeurAvant.Size = new System.Drawing.Size(395, 80);
            this.m_panelValeurAvant.TabIndex = 1;
            // 
            // m_chkValeurAvant
            // 
            this.m_chkValeurAvant.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkValeurAvant, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkValeurAvant.Name = "m_chkValeurAvant";
            this.m_chkValeurAvant.Size = new System.Drawing.Size(160, 16);
            this.m_chkValeurAvant.TabIndex = 3;
            this.m_chkValeurAvant.Text = "Previous value|192";
            this.m_chkValeurAvant.CheckedChanged += new System.EventHandler(this.OnCheckeValeurChanged);
            // 
            // m_txtValeurAvant
            // 
            this.m_txtValeurAvant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtValeurAvant.BackColor = System.Drawing.Color.White;
            this.m_txtValeurAvant.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtValeurAvant.Formule = null;
            this.m_txtValeurAvant.Location = new System.Drawing.Point(8, 24);
            this.m_txtValeurAvant.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtValeurAvant, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtValeurAvant.Name = "m_txtValeurAvant";
            this.m_txtValeurAvant.Size = new System.Drawing.Size(395, 40);
            this.m_txtValeurAvant.TabIndex = 1;
            this.m_txtValeurAvant.Enter += new System.EventHandler(this.ZoneAForumle_Enter);
            // 
            // m_panelValeurApres
            // 
            this.m_panelValeurApres.Controls.Add(this.m_chkValeurApres);
            this.m_panelValeurApres.Controls.Add(this.m_txtValeurApres);
            this.m_panelValeurApres.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelValeurApres.Location = new System.Drawing.Point(395, 0);
            this.m_panelValeurApres.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelValeurApres, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelValeurApres.Name = "m_panelValeurApres";
            this.m_panelValeurApres.Size = new System.Drawing.Size(256, 80);
            this.m_panelValeurApres.TabIndex = 1;
            // 
            // m_chkValeurApres
            // 
            this.m_chkValeurApres.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkValeurApres, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkValeurApres.Name = "m_chkValeurApres";
            this.m_chkValeurApres.Size = new System.Drawing.Size(120, 16);
            this.m_chkValeurApres.TabIndex = 4;
            this.m_chkValeurApres.Text = "Next value|193";
            this.m_chkValeurApres.CheckedChanged += new System.EventHandler(this.OnCheckeValeurChanged);
            // 
            // m_txtValeurApres
            // 
            this.m_txtValeurApres.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtValeurApres.BackColor = System.Drawing.Color.White;
            this.m_txtValeurApres.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtValeurApres.Formule = null;
            this.m_txtValeurApres.Location = new System.Drawing.Point(8, 24);
            this.m_txtValeurApres.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtValeurApres, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtValeurApres.Name = "m_txtValeurApres";
            this.m_txtValeurApres.Size = new System.Drawing.Size(240, 40);
            this.m_txtValeurApres.TabIndex = 2;
            this.m_txtValeurApres.Enter += new System.EventHandler(this.ZoneAForumle_Enter);
            // 
            // m_panelDate
            // 
            this.m_panelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelDate.BackColor = System.Drawing.SystemColors.Control;
            this.m_panelDate.Controls.Add(this.label4);
            this.m_panelDate.Controls.Add(this.m_txtFormuleDate);
            this.m_panelDate.Location = new System.Drawing.Point(0, 32);
            this.m_panelDate.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDate, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelDate.Name = "m_panelDate";
            this.m_panelDate.Size = new System.Drawing.Size(651, 88);
            this.m_panelDate.TabIndex = 4005;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Triggering Date|10003";
            // 
            // m_txtFormuleDate
            // 
            this.m_txtFormuleDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleDate.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleDate.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleDate.Formule = null;
            this.m_txtFormuleDate.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormuleDate.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFormuleDate, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtFormuleDate.Name = "m_txtFormuleDate";
            this.m_txtFormuleDate.Size = new System.Drawing.Size(635, 56);
            this.m_txtFormuleDate.TabIndex = 1;
            this.m_txtFormuleDate.Enter += new System.EventHandler(this.ZoneAForumle_Enter);
            // 
            // m_chkTypeDate
            // 
            this.m_chkTypeDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkTypeDate.Location = new System.Drawing.Point(257, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTypeDate, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTypeDate.Name = "m_chkTypeDate";
            this.m_chkTypeDate.Size = new System.Drawing.Size(64, 16);
            this.m_chkTypeDate.TabIndex = 2;
            this.m_chkTypeDate.Text = "Date|184";
            this.m_chkTypeDate.CheckedChanged += new System.EventHandler(this.OnChangeTypeEvenement);
            // 
            // m_chkTypeModification
            // 
            this.m_chkTypeModification.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkTypeModification.Location = new System.Drawing.Point(169, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTypeModification, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTypeModification.Name = "m_chkTypeModification";
            this.m_chkTypeModification.Size = new System.Drawing.Size(88, 16);
            this.m_chkTypeModification.TabIndex = 1;
            this.m_chkTypeModification.Text = "Modification|183";
            this.m_chkTypeModification.CheckedChanged += new System.EventHandler(this.OnChangeTypeEvenement);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Event type|181";
            // 
            // m_chkTypeCreation
            // 
            this.m_chkTypeCreation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkTypeCreation.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTypeCreation, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTypeCreation.Name = "m_chkTypeCreation";
            this.m_chkTypeCreation.Size = new System.Drawing.Size(72, 16);
            this.m_chkTypeCreation.TabIndex = 0;
            this.m_chkTypeCreation.Text = "Creation|182";
            this.m_chkTypeCreation.CheckedChanged += new System.EventHandler(this.OnChangeTypeEvenement);
            // 
            // m_chkTypeManuel
            // 
            this.m_chkTypeManuel.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkTypeManuel.Location = new System.Drawing.Point(321, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTypeManuel, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTypeManuel.Name = "m_chkTypeManuel";
            this.m_chkTypeManuel.Size = new System.Drawing.Size(72, 16);
            this.m_chkTypeManuel.TabIndex = 3;
            this.m_chkTypeManuel.Text = "Manual|185";
            this.m_chkTypeManuel.CheckedChanged += new System.EventHandler(this.OnChangeTypeEvenement);
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(651, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndAide, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(144, 361);
            this.m_wndAide.TabIndex = 4003;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande_1);
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.Controls.Add(this.m_lnkExceptions);
            this.m_panelGauche.Controls.Add(this.m_panelOrdre);
            this.m_panelGauche.Controls.Add(this.m_panelDeclEtManuel);
            this.m_panelGauche.Controls.Add(this.m_txtCodeDeclencheur);
            this.m_panelGauche.Controls.Add(this.label1);
            this.m_panelGauche.Controls.Add(this.label2);
            this.m_panelGauche.Controls.Add(this.panel1);
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelGauche, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(651, 361);
            this.m_panelGauche.TabIndex = 4004;
            // 
            // m_lnkExceptions
            // 
            this.m_lnkExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkExceptions.Location = new System.Drawing.Point(567, 4);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkExceptions, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkExceptions.Name = "m_lnkExceptions";
            this.m_lnkExceptions.Size = new System.Drawing.Size(73, 13);
            this.m_lnkExceptions.TabIndex = 11;
            this.m_lnkExceptions.TabStop = true;
            this.m_lnkExceptions.Text = "Exceptions";
            this.m_lnkExceptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lnkExceptions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkExceptions_LinkClicked);
            // 
            // m_panelOrdre
            // 
            this.m_panelOrdre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelOrdre.Controls.Add(this.m_numUpOrdre);
            this.m_panelOrdre.Controls.Add(this.label8);
            this.m_panelOrdre.Location = new System.Drawing.Point(506, 37);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelOrdre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelOrdre.Name = "m_panelOrdre";
            this.m_panelOrdre.Size = new System.Drawing.Size(137, 24);
            this.m_panelOrdre.TabIndex = 10;
            // 
            // m_numUpOrdre
            // 
            this.m_numUpOrdre.DoubleValue = 0;
            this.m_numUpOrdre.IntValue = 0;
            this.m_numUpOrdre.Location = new System.Drawing.Point(61, 2);
            this.m_numUpOrdre.LockEdition = false;
            this.m_numUpOrdre.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_numUpOrdre.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_numUpOrdre, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_numUpOrdre.Name = "m_numUpOrdre";
            this.m_numUpOrdre.Size = new System.Drawing.Size(72, 20);
            this.m_numUpOrdre.TabIndex = 1;
            this.m_numUpOrdre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_numUpOrdre.ThousandsSeparator = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label8, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Order|186";
            // 
            // m_panelDeclEtManuel
            // 
            this.m_panelDeclEtManuel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelDeclEtManuel.Controls.Add(this.m_panelDeclencheur);
            this.m_panelDeclEtManuel.Controls.Add(this.m_panelManuel);
            this.m_panelDeclEtManuel.Location = new System.Drawing.Point(0, 63);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDeclEtManuel, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDeclEtManuel.Name = "m_panelDeclEtManuel";
            this.m_panelDeclEtManuel.Size = new System.Drawing.Size(651, 296);
            this.m_panelDeclEtManuel.TabIndex = 9;
            // 
            // m_panelManuel
            // 
            this.m_panelManuel.Controls.Add(this.m_chkMasquerBarreProgression);
            this.m_panelManuel.Controls.Add(this.m_txtMenuManuel);
            this.m_panelManuel.Controls.Add(this.m_lnkGroupes);
            this.m_panelManuel.Controls.Add(this.label7);
            this.m_panelManuel.Controls.Add(this.label5);
            this.m_panelManuel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelManuel.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelManuel, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelManuel.Name = "m_panelManuel";
            this.m_panelManuel.Size = new System.Drawing.Size(651, 60);
            this.m_panelManuel.TabIndex = 8;
            // 
            // m_chkMasquerBarreProgression
            // 
            this.m_chkMasquerBarreProgression.AutoSize = true;
            this.m_chkMasquerBarreProgression.Location = new System.Drawing.Point(168, 37);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkMasquerBarreProgression, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkMasquerBarreProgression.Name = "m_chkMasquerBarreProgression";
            this.m_chkMasquerBarreProgression.Size = new System.Drawing.Size(141, 17);
            this.m_chkMasquerBarreProgression.TabIndex = 4;
            this.m_chkMasquerBarreProgression.Text = "Hide progress bar|20008";
            this.m_chkMasquerBarreProgression.UseVisualStyleBackColor = true;
            // 
            // m_txtMenuManuel
            // 
            this.m_txtMenuManuel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMenuManuel.Location = new System.Drawing.Point(80, 16);
            this.m_txtMenuManuel.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtMenuManuel, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtMenuManuel.Name = "m_txtMenuManuel";
            this.m_txtMenuManuel.Size = new System.Drawing.Size(461, 20);
            this.m_txtMenuManuel.TabIndex = 1;
            // 
            // m_lnkGroupes
            // 
            this.m_lnkGroupes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkGroupes.Location = new System.Drawing.Point(340, 37);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkGroupes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkGroupes.Name = "m_lnkGroupes";
            this.m_lnkGroupes.Size = new System.Drawing.Size(152, 15);
            this.m_lnkGroupes.TabIndex = 3;
            this.m_lnkGroupes.TabStop = true;
            this.m_lnkGroupes.Text = "Authorized groups|191";
            this.m_lnkGroupes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lnkGroupes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkGroupes_LinkClicked);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label7, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Manual execution|188";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 16);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Menu|189";
            // 
            // m_txtCodeDeclencheur
            // 
            this.m_txtCodeDeclencheur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCodeDeclencheur.Location = new System.Drawing.Point(418, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtCodeDeclencheur, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtCodeDeclencheur.Name = "m_txtCodeDeclencheur";
            this.m_txtCodeDeclencheur.Size = new System.Drawing.Size(149, 20);
            this.m_txtCodeDeclencheur.TabIndex = 7;
            this.m_txtCodeDeclencheur.Text = "textBox1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(306, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Trigger code|187";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkTypeSpecifique);
            this.panel1.Controls.Add(this.m_chkTypeManuel);
            this.panel1.Controls.Add(this.m_chkTypeDate);
            this.panel1.Controls.Add(this.m_chkTypeModification);
            this.panel1.Controls.Add(this.m_chkTypeDelete);
            this.panel1.Controls.Add(this.m_chkTypeCreation);
            this.panel1.Location = new System.Drawing.Point(8, 39);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 16);
            this.panel1.TabIndex = 3;
            // 
            // m_chkTypeSpecifique
            // 
            this.m_chkTypeSpecifique.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkTypeSpecifique.Location = new System.Drawing.Point(393, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTypeSpecifique, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTypeSpecifique.Name = "m_chkTypeSpecifique";
            this.m_chkTypeSpecifique.Size = new System.Drawing.Size(97, 16);
            this.m_chkTypeSpecifique.TabIndex = 4;
            this.m_chkTypeSpecifique.Text = "Specific|30026";
            this.m_chkTypeSpecifique.CheckedChanged += new System.EventHandler(this.m_chkTypeSpecifique_CheckedChanged);
            // 
            // m_chkTypeDelete
            // 
            this.m_chkTypeDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkTypeDelete.Location = new System.Drawing.Point(72, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkTypeDelete, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkTypeDelete.Name = "m_chkTypeDelete";
            this.m_chkTypeDelete.Size = new System.Drawing.Size(97, 16);
            this.m_chkTypeDelete.TabIndex = 5;
            this.m_chkTypeDelete.Text = "Delete|20027";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(648, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 361);
            this.splitter1.TabIndex = 4005;
            this.splitter1.TabStop = false;
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // CPanelEditParametreDeclencheur
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelGauche);
            this.Controls.Add(this.m_wndAide);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditParametreDeclencheur";
            this.Size = new System.Drawing.Size(795, 361);
            this.Load += new System.EventHandler(this.CPanelEditParametreDeclencheur_Load);
            this.BackColorChanged += new System.EventHandler(this.CPanelEditParametreDeclencheur_BackColorChanged);
            this.m_panelDeclencheur.ResumeLayout(false);
            this.m_panelConditionDeclenchement.ResumeLayout(false);
            this.m_panelSpecifique.ResumeLayout(false);
            this.m_panelSurModificationOuDate.ResumeLayout(false);
            this.m_panelModif.ResumeLayout(false);
            this.m_panelValeurAvant.ResumeLayout(false);
            this.m_panelValeurApres.ResumeLayout(false);
            this.m_panelDate.ResumeLayout(false);
            this.m_panelGauche.ResumeLayout(false);
            this.m_panelGauche.PerformLayout();
            this.m_panelOrdre.ResumeLayout(false);
            this.m_panelOrdre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUpOrdre)).EndInit();
            this.m_panelDeclEtManuel.ResumeLayout(false);
            this.m_panelManuel.ResumeLayout(false);
            this.m_panelManuel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void CPanelEditParametreDeclencheur_Load(object sender, System.EventArgs e)
		{
			if ( !DesignMode )
			{
				ZoneAForumle_Enter ( m_txtValeurAvant, new EventArgs() );
				m_wndAide.FournisseurProprietes = new CFournisseurPropDynStd(true);
				m_txtCondition.Init ( m_wndAide.FournisseurProprietes, TypeCible );
				m_txtFormuleDate.Init ( m_wndAide.FournisseurProprietes, typeof(sc2i.data.dynamic.CObjetForTestValeurChampCustomDateTime) );
			}
		}

		//-------------------------------------------------------------------------
		public CResultAErreur Init ( CParametreDeclencheurEvenement parametreDeclencheur )
		{
			m_parametreDeclencheur = (CParametreDeclencheurEvenement)CCloner2iSerializable.Clone ( parametreDeclencheur );
			CResultAErreur result = CResultAErreur.True;
			if ( m_parametreDeclencheur == null )
				return result;

			FillComboChamps();

			switch ( m_parametreDeclencheur.TypeEvenement )
			{
				case TypeEvenement.Creation :
					m_chkTypeCreation.Checked = true;
					break;
                case TypeEvenement.Suppression :
                    m_chkTypeDelete.Checked = true;
                    break;
				case TypeEvenement.Modification :
					m_chkTypeModification.Checked = true;
					break;
				case TypeEvenement.Date :
					m_chkTypeDate.Checked = true;
					break;
				case TypeEvenement.Manuel :
					m_chkTypeManuel.Checked = true;
					break;
				case TypeEvenement.Specifique :
					m_chkTypeSpecifique.Checked = true;
					break;
			}
			m_txtCondition.Text = "";
			m_txtFormuleDate.Text = "";
			m_txtValeurApres.Text = "";
			m_txtValeurAvant.Text = "";
			m_txtCodeDeclencheur.Text = m_parametreDeclencheur.Code;
			m_txtMenuManuel.Text = m_parametreDeclencheur.MenuManuel;
			m_chkMasquerBarreProgression.Checked = m_parametreDeclencheur.HideProgress;
			//TESTDBKEYTODO
            m_keysGroupes = m_parametreDeclencheur.KeysGroupesManuel;
			m_numUpOrdre.IntValue = m_parametreDeclencheur.OrdreExecution;
			UpdateVisuGroupes();
			


			if ( m_parametreDeclencheur.TypeEvenement == TypeEvenement.Modification )
			{
				FillComboChamps();
				C2iExpression exp = m_parametreDeclencheur.FormuleValeurAvant;
				if ( exp == null )
				{
					m_chkValeurAvant.Checked = false;
					m_txtValeurAvant.Text = "";
				}
				else
				{
					m_chkValeurAvant.Checked = true;
					m_txtValeurAvant.Text = exp.GetString();
				}
				exp = m_parametreDeclencheur.FormuleValeurApres;
				if ( exp == null )
				{
					m_chkValeurApres.Checked = false;
					m_txtValeurApres.Text = "";
				}
				else
				{
					m_chkValeurApres.Checked = true;
					m_txtValeurApres.Text = exp.GetString();
				}
			}

			if ( m_parametreDeclencheur.TypeEvenement == TypeEvenement.Date )
				if ( m_parametreDeclencheur.FormuleDateProgramme != null )
					m_txtFormuleDate.Text = m_parametreDeclencheur.FormuleDateProgramme.GetString();

			if ( m_parametreDeclencheur.FormuleConditionDeclenchement != null )
				m_txtCondition.Text = m_parametreDeclencheur.FormuleConditionDeclenchement.GetString();
			if (m_parametreDeclencheur.TypeEvenement == TypeEvenement.Specifique)
			{
				FillComboEvtSpecifique();
				string strEvt = m_parametreDeclencheur.IdEvenementSpecifique;
				if (strEvt == null)
					strEvt = "";
				foreach (EvenementAttribute evt in EvenementAttribute.GetEvenementsForType (m_parametreDeclencheur.TypeCible) )
					if (evt.Identifiant == strEvt)
					{
						m_cmbEvtSpecifique.SelectedValue = evt;
						break;
					}
			}
					

			m_cmbValeurSurveillee.SelectedValue = m_parametreDeclencheur.ProprieteASurveiller;
			UpdateLook();
			return result;
		}

		//-------------------------------------------------------------------------
		/// <summary>
		/// Définit le type cible du paramètre d'évenement
		/// </summary>
		public Type TypeCible
		{
			get
			{
				return m_parametreDeclencheur.TypeCible;
			}
			set
			{
				if ( value != null && value != m_parametreDeclencheur.TypeCible )
					m_parametreDeclencheur.TypeCible =  value;
				FillComboChamps();
				if ( m_receveurFormules == m_txtFormuleDate )
					m_wndAide.ObjetInterroge = typeof(sc2i.data.dynamic.CObjetForTestValeurChampCustomDateTime);
				else
					m_wndAide.ObjetInterroge = value;
				CFournisseurPropDynStd four = new CFournisseurPropDynStd();
				m_txtCondition.Init ( four, value );
				m_txtValeurApres.Init ( four, value );
				m_txtValeurAvant.Init ( four, value );
			}
		}

		//-------------------------------------------------------------------------
		public void SetSurTableau ( bool bSurTableau )
		{
			if ( bSurTableau )
			{
				m_chkTypeCreation.Visible = false;
                m_chkTypeDelete.Visible = false;
				m_chkTypeDate.Visible = false;
				m_chkTypeModification.Visible = false;
				m_chkTypeManuel.Checked = true;
			}
			else
			{
				m_chkTypeCreation.Visible = m_bAutoriseSurCreation;
                m_chkTypeDelete.Visible = m_bAutoriseSurSuppression;
				m_chkTypeDate.Visible = m_bAutoriseSurDate;
				m_chkTypeModification.Visible = m_bAutoriseSurModification;
			}
		}


		//-------------------------------------------------------------------------
		public CParametreDeclencheurEvenement ParametreDeclencheur
		{
			get
			{
				return m_parametreDeclencheur;
			}
		}
				
		//-------------------------------------------------------------------------
		public CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = CResultAErreur.True;

			m_parametreDeclencheur.Code = m_txtCodeDeclencheur.Text;
			m_parametreDeclencheur.MenuManuel = m_txtMenuManuel.Text;
			m_parametreDeclencheur.HideProgress = m_chkMasquerBarreProgression.Checked;
			//TESTDBKEYTODO
            m_parametreDeclencheur.KeysGroupesManuel = m_keysGroupes;
			m_parametreDeclencheur.OrdreExecution = m_numUpOrdre.IntValue;

			if ( m_chkTypeCreation.Checked )
				m_parametreDeclencheur.TypeEvenement = TypeEvenement.Creation;
            if (m_chkTypeDelete.Checked)
                m_parametreDeclencheur.TypeEvenement = TypeEvenement.Suppression;
			if ( m_chkTypeDate.Checked )
				m_parametreDeclencheur.TypeEvenement = TypeEvenement.Date;
			if ( m_chkTypeModification.Checked )
				m_parametreDeclencheur.TypeEvenement = TypeEvenement.Modification;
			if ( m_chkTypeManuel.Checked )
				m_parametreDeclencheur.TypeEvenement = TypeEvenement.Manuel;
			if ( m_chkTypeSpecifique.Checked )
				m_parametreDeclencheur.TypeEvenement = TypeEvenement.Specifique;

			if ( m_parametreDeclencheur.TypeEvenement == TypeEvenement.Modification  ||
				m_parametreDeclencheur.TypeEvenement == TypeEvenement.Date )
			{
				if ( !(m_cmbValeurSurveillee.SelectedValue is CDefinitionProprieteDynamique) && m_parametreDeclencheur.TypeEvenement == TypeEvenement.Modification )
				{
					m_parametreDeclencheur.ProprieteASurveiller = null;
				}
				else
					m_parametreDeclencheur.ProprieteASurveiller = (CDefinitionProprieteDynamique)m_cmbValeurSurveillee.SelectedValue;
			}
			if (m_parametreDeclencheur.TypeEvenement == TypeEvenement.Specifique)
			{
				if (!(m_cmbEvtSpecifique.SelectedValue is EvenementAttribute))
				{
					result.EmpileErreur(I.T("Select an event|30028"));
					return result;
				}
				m_parametreDeclencheur.IdEvenementSpecifique = ((EvenementAttribute)m_cmbEvtSpecifique.SelectedValue).Identifiant;
			}
			CContexteAnalyse2iExpression contexteAnalyse = new CContexteAnalyse2iExpression(new CFournisseurPropDynStd(true), m_parametreDeclencheur.TypeCible);
			CAnalyseurSyntaxique analyseur = new CAnalyseurSyntaxiqueExpression(contexteAnalyse);

			

			if ( m_parametreDeclencheur.TypeEvenement == TypeEvenement.Modification )
			{
				if ( m_chkValeurAvant.Checked )
				{
					result = analyseur.AnalyseChaine ( m_txtValeurAvant.Text );
					if ( !result )
					{
						result.EmpileErreur(I.T("Error in 'Previous value' formula|30029"));
						return result;
					}
					else
						m_parametreDeclencheur.FormuleValeurAvant = (C2iExpression)result.Data;
				}
				else
					m_parametreDeclencheur.FormuleValeurAvant = null;

				if ( m_chkValeurApres.Checked )
				{
					result = analyseur.AnalyseChaine ( m_txtValeurApres.Text );
					if ( !result )
					{
						result.EmpileErreur(I.T("Error in 'Next value' formula|30030"));
						return result;
					}
					else
						m_parametreDeclencheur.FormuleValeurApres = (C2iExpression)result.Data;
				}
				else
					m_parametreDeclencheur.FormuleValeurApres = null;
			}


			result = analyseur.AnalyseChaine(m_txtCondition.Text);
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in the condition|30031"));
				return result;
			}
			else
				m_parametreDeclencheur.FormuleConditionDeclenchement = (C2iExpression)result.Data;

			if ( m_parametreDeclencheur.TypeEvenement == TypeEvenement.Date )
			{
				contexteAnalyse = new CContexteAnalyse2iExpression(new CFournisseurPropDynStd(true), typeof(sc2i.data.dynamic.CObjetForTestValeurChampCustomDateTime));
				analyseur = new CAnalyseurSyntaxiqueExpression(contexteAnalyse);
				result = analyseur.AnalyseChaine ( m_txtFormuleDate.Text );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in the date formula|30032"));
				}
				else
					m_parametreDeclencheur.FormuleDateProgramme = (C2iExpression)result.Data;
			}

	
			return result;
		}

		//-------------------------------------------------------------------------
		private void ZoneAForumle_Enter(object sender, System.EventArgs e)
		{
			if ( m_receveurFormules != null )
				m_receveurFormules.BackColor = Color.White;
			if ( sender is sc2i.win32.expression.CControleEditeFormule )
			{
				m_receveurFormules = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_receveurFormules.BackColor = Color.LightGreen;
			}
			if ( sender == m_txtFormuleDate )
			{
				if (m_wndAide.ObjetInterroge == null || m_wndAide.ObjetInterroge.TypeAnalyse != typeof(sc2i.data.dynamic.CObjetForTestValeurChampCustomDateTime) )
					m_wndAide.ObjetInterroge = typeof(CObjetForTestValeurChampCustomDateTime);
			}
			else
			{
				Type tp = m_parametreDeclencheur.TypeCible;
				if (m_wndAide.ObjetInterroge == null || m_wndAide.ObjetInterroge.TypeAnalyse != m_parametreDeclencheur.TypeCible )
					m_wndAide.ObjetInterroge = m_parametreDeclencheur.TypeCible;
			}
		}



		//-------------------------------------------------------------------------
		private void CFormEditionEvenement_Load(object sender, System.EventArgs e)
		{
			ZoneAForumle_Enter ( m_txtValeurAvant, new EventArgs() );
			m_wndAide.FournisseurProprietes = new CFournisseurPropDynStd(true);
		}
		//-------------------------------------------------------------------------
		private void UpdateLook()
		{
			m_panelSurModificationOuDate.Visible = m_chkTypeModification.Checked || m_chkTypeDate.Checked;
			m_panelModif.Visible = m_chkTypeModification.Checked;
			m_panelDate.Visible = m_chkTypeDate.Checked;
			if ( m_receveurFormules !=  null && !m_receveurFormules.Visible )
				ZoneAForumle_Enter(m_txtCondition, new EventArgs());
			m_txtValeurAvant.Enabled = m_chkValeurAvant.Checked;
			m_txtValeurApres.Enabled = m_chkValeurApres.Checked;
            m_panelOrdre.Visible = m_chkTypeModification.Checked || m_chkTypeCreation.Checked || m_chkTypeDelete.Checked;
		}

		//-------------------------------------------------------------------------
		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_receveurFormules != null )
				m_wndAide.InsereInTextBox ( m_receveurFormules, nPosCurseur, strCommande);
		}

		//-------------------------------------------------------------------------
		private void FillComboChamps()
		{
			object oldSel = m_cmbValeurSurveillee.SelectedValue;
			m_cmbValeurSurveillee.DataSource = null;
			if ( m_parametreDeclencheur == null )
				return;
			Type tp = (Type)m_parametreDeclencheur.TypeCible;
			if ( tp == null )
				return;
			CDefinitionProprieteDynamique[] defs = CParametreDeclencheurEvenement.ProprietesSurveillables ( tp, m_chkTypeDate.Checked );
			m_cmbValeurSurveillee.ListDonnees = defs;
			m_cmbValeurSurveillee.ProprieteAffichee = "Nom";
			m_cmbValeurSurveillee.AssureRemplissage();
			m_cmbValeurSurveillee.SelectedValue = oldSel;
			if ( m_chkTypeDate.Checked )
				m_cmbValeurSurveillee.TextNull = I.T("(none)|30033");
			else
				m_cmbValeurSurveillee.TextNull = I.T("(to affect)|30017");
		}

		private void OnCheckeValeurChanged(object sender, System.EventArgs e)
		{
			UpdateLook();
		}

		private void OnChangeTypeEvenement(object sender, System.EventArgs e)
		{
			UpdateLook();
			FillComboChamps();
			m_panelManuel.Visible = m_chkTypeManuel.Checked;
		}

		private void m_chkTypeSpecifique_CheckedChanged(object sender, EventArgs e)
		{
			UpdateLook();
			if ( m_chkTypeSpecifique.Checked )
				FillComboEvtSpecifique();
			m_panelSpecifique.Visible = m_chkTypeSpecifique.Checked;
		}

		private void FillComboEvtSpecifique()
		{
			EvenementAttribute evtSel = (EvenementAttribute)m_cmbEvtSpecifique.SelectedValue;
			if ( m_parametreDeclencheur == null )
				return;
			Type tp = (Type)m_parametreDeclencheur.TypeCible;
			if ( tp == null )
				return;
			List<EvenementAttribute> lst = EvenementAttribute.GetEvenementsForType(tp);
			m_cmbEvtSpecifique.Fill(lst, "Libelle", true);
			m_cmbEvtSpecifique.SelectedValue = evtSel;
		}
		

		private void CPanelEditParametreDeclencheur_BackColorChanged(object sender, System.EventArgs e)
		{
			ChangePanelsColor ( this, BackColor );
		}

		private void ChangePanelsColor ( Control parent, Color couleur )
		{
			parent.BackColor = couleur;
			foreach ( Control ctrl in parent.Controls )
				if ( ctrl is Panel )
					ChangePanelsColor ( ctrl, couleur );
		}

		private void m_wndAide_OnSendCommande_1(string strCommande, int nPosCurseur)
		{
			if ( m_receveurFormules != null )
				m_wndAide.InsereInTextBox ( m_receveurFormules, nPosCurseur, strCommande );
		}

		/// //////////////////////////////////////
		private void m_lnkGroupes_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( m_selectionneurGroupes != null )
			{
                //TESTDBKEYTODO
                m_keysGroupes = m_selectionneurGroupes.GetKeysGroupes(m_keysGroupes);
				UpdateVisuGroupes();
			}
		}

		public event EventHandler OnChangeLockEdition;
		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				if ( value != !m_gestionnaireModeEdition.ModeEdition )
				{
					if ( OnChangeLockEdition != null )
						OnChangeLockEdition(this, new EventArgs());
					m_gestionnaireModeEdition.ModeEdition = !value;
				}
				
			}
		}

		/// /////////////////////////////////////////
		public bool AutoriseSurCreation
		{
			get
			{
				return m_bAutoriseSurCreation;
			}
			set
			{
				m_bAutoriseSurCreation = value;
				m_chkTypeCreation.Visible = value;
                if ( !value )
                    m_chkTypeCreation.Checked = false;
			}
		}

        /// /////////////////////////////////////////
        public bool AutoriseSurSuppression
        {
            get
            {
                return m_bAutoriseSurSuppression;
            }
            set
            {
                m_bAutoriseSurSuppression = value;
                m_chkTypeDelete.Visible = value;
                if ( !value )
                    m_chkTypeDelete.Checked = false;
            }
        }

		/// /////////////////////////////////////////
		public bool AutoriseSurModification
		{
			get
			{
				return m_bAutoriseSurModification;
			}
			set
			{
				m_bAutoriseSurModification = value;
				m_chkTypeModification.Visible = value;
                if ( !value )
                    m_chkTypeModification.Checked = false;
			}
		}

		/// /////////////////////////////////////////
		public bool AutoriseSurDate
		{
			get
			{
				return m_bAutoriseSurDate;
			}
			set
			{
				m_bAutoriseSurDate = value;
				m_chkTypeDate.Visible = value;
                if (!value)
                    m_chkTypeDelete.Checked = false;
			}
		}

		/// /////////////////////////////////////////
		public bool AutoriseSurManuel
		{
			get
			{
				return m_bAutoriseSurManuel;
			}
			set
			{
				m_bAutoriseSurManuel = value;
				m_chkTypeManuel.Visible = value;
				if (!m_bAutoriseSurManuel )
					m_panelManuel.Visible = false;
			}
		}

		/// /////////////////////////////////////////
		private void UpdateVisuGroupes()
		{
            //TESTDBKEYTODO
			m_tooltip.SetToolTip ( m_lnkGroupes, m_keysGroupes.Length+I.T(" group(s)|30034"));
			m_lnkGroupes.Visible = m_selectionneurGroupes != null;
		}

		/// /////////////////////////////////////////
		public static void SetSelectionneurGroupesUtilisateurs ( ISelectionneurGroupesUtilisateurs selectionneur )
		{
			m_selectionneurGroupes = selectionneur;
		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

        /// /////////////////////////////////////////
        private void m_lnkExceptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HashSet<string> contextes = CFormSelectContextesExceptions.EditeContextes(ParametreDeclencheur.ContextesException);
            ParametreDeclencheur.ContextesException = contextes;
        }
        
	}

	/// /////////////////////////////////////////
	public interface ISelectionneurGroupesUtilisateurs
	{
        CDbKey[] GetKeysGroupes(CDbKey[] keysGroupesSelectionnes);
	}


}

