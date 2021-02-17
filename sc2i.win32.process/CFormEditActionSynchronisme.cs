using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.data;
using sc2i.win32.common;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.multitiers.client;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionSynchronisme : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private C2iExpression m_expressionDest = null;
		private C2iExpression m_expressionSource = null;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule = null;
		private System.Windows.Forms.Panel panel2;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private sc2i.win32.expression.CControleEditeFormule m_txtDest;
		private Crownwood.Magic.Controls.TabPage tabPage1;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtConditionDest;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.expression.CControleEditeFormule m_txtSource;
		private sc2i.win32.common.CComboboxAutoFilled m_cmbChampSource;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbChampDest;
        protected CExtStyle m_ExtStyle1;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionSynchronisme()
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

		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionSynchronisme), typeof(CFormEditActionSynchronisme));
		}


		public CActionSynchronisme ActionSynchronisme
		{
			get
			{
				return (CActionSynchronisme)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_cmbChampDest = new sc2i.win32.common.CComboboxAutoFilled();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtConditionDest = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtDest = new sc2i.win32.expression.CControleEditeFormule();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_cmbChampSource = new sc2i.win32.common.CComboboxAutoFilled();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtSource = new sc2i.win32.expression.CControleEditeFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.panel2.SuspendLayout();
            this.c2iTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.c2iTabControl1);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(672, 391);
            this.m_ExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 2;
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.ControlBottomOffset = 16;
            this.c2iTabControl1.ControlRightOffset = 16;
            this.c2iTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 0);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = true;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.SelectedIndex = 1;
            this.c2iTabControl1.SelectedTab = this.tabPage2;
            this.c2iTabControl1.Size = new System.Drawing.Size(493, 391);
            this.m_ExtStyle1.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl1.TabIndex = 3;
            this.c2iTabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_cmbChampDest);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.m_txtConditionDest);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.m_txtDest);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(477, 350);
            this.m_ExtStyle1.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Synchronized element|162";
            // 
            // m_cmbChampDest
            // 
            this.m_cmbChampDest.DisplayMember = "NomConvivialPropOuChamp";
            this.m_cmbChampDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbChampDest.IsLink = false;
            this.m_cmbChampDest.ListDonnees = null;
            this.m_cmbChampDest.Location = new System.Drawing.Point(75, 152);
            this.m_cmbChampDest.LockEdition = false;
            this.m_cmbChampDest.Name = "m_cmbChampDest";
            this.m_cmbChampDest.NullAutorise = false;
            this.m_cmbChampDest.ProprieteAffichee = "NomConvivialPropOuChamp";
            this.m_cmbChampDest.Size = new System.Drawing.Size(328, 21);
            this.m_ExtStyle1.SetStyleBackColor(this.m_cmbChampDest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_cmbChampDest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbChampDest.TabIndex = 9;
            this.m_cmbChampDest.TextNull = I.T("(empty)|30018");
            this.m_cmbChampDest.Tri = true;
            this.m_cmbChampDest.ValueMember = "NomChamp";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 6;
            this.label2.Text = "Synchronization condition|161";
            // 
            // m_txtConditionDest
            // 
            this.m_txtConditionDest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtConditionDest.BackColor = System.Drawing.Color.White;
            this.m_txtConditionDest.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtConditionDest.Formule = null;
            this.m_txtConditionDest.Location = new System.Drawing.Point(8, 192);
            this.m_txtConditionDest.LockEdition = false;
            this.m_txtConditionDest.Name = "m_txtConditionDest";
            this.m_txtConditionDest.Size = new System.Drawing.Size(464, 152);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtConditionDest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtConditionDest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtConditionDest.TabIndex = 5;
            this.m_txtConditionDest.Enter += new System.EventHandler(this.OnEnterZoneEdition);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 3;
            this.label1.Text = "Field|160";
            // 
            // m_txtDest
            // 
            this.m_txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDest.BackColor = System.Drawing.Color.White;
            this.m_txtDest.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtDest.Formule = null;
            this.m_txtDest.Location = new System.Drawing.Point(8, 8);
            this.m_txtDest.LockEdition = false;
            this.m_txtDest.Name = "m_txtDest";
            this.m_txtDest.Size = new System.Drawing.Size(464, 136);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtDest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtDest, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtDest.TabIndex = 2;
            this.m_txtDest.Enter += new System.EventHandler(this.OnEnterZoneEdition);
            this.m_txtDest.Load += new System.EventHandler(this.m_txtDest_Load);
            this.m_txtDest.Validated += new System.EventHandler(this.m_txtDest_Validated);
            this.m_txtDest.TextChanged += new System.EventHandler(this.m_txtDest_Validated);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_cmbChampSource);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.m_txtSource);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(477, 350);
            this.m_ExtStyle1.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Watched event|163";
            // 
            // m_cmbChampSource
            // 
            this.m_cmbChampSource.DisplayMember = "NomConvivialPropOuChamp";
            this.m_cmbChampSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbChampSource.IsLink = false;
            this.m_cmbChampSource.ListDonnees = null;
            this.m_cmbChampSource.Location = new System.Drawing.Point(97, 150);
            this.m_cmbChampSource.LockEdition = false;
            this.m_cmbChampSource.Name = "m_cmbChampSource";
            this.m_cmbChampSource.NullAutorise = false;
            this.m_cmbChampSource.ProprieteAffichee = "NomConvivialPropOuChamp";
            this.m_cmbChampSource.Size = new System.Drawing.Size(328, 21);
            this.m_ExtStyle1.SetStyleBackColor(this.m_cmbChampSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_cmbChampSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbChampSource.TabIndex = 8;
            this.m_cmbChampSource.TextNull = I.T("(empty)|30018");
            this.m_cmbChampSource.Tri = true;
            this.m_cmbChampSource.ValueMember = "NomChamp";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 6;
            this.label3.Text = "Field|160";
            // 
            // m_txtSource
            // 
            this.m_txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSource.BackColor = System.Drawing.Color.White;
            this.m_txtSource.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtSource.Formule = null;
            this.m_txtSource.Location = new System.Drawing.Point(8, 8);
            this.m_txtSource.LockEdition = false;
            this.m_txtSource.Name = "m_txtSource";
            this.m_txtSource.Size = new System.Drawing.Size(464, 136);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSource.TabIndex = 5;
            this.m_txtSource.Enter += new System.EventHandler(this.OnEnterZoneEdition);
            this.m_txtSource.Validated += new System.EventHandler(this.m_txtSource_Validated);
            this.m_txtSource.TextChanged += new System.EventHandler(this.m_txtSource_Validated);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(493, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 391);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(496, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 391);
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndAideFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CFormEditActionSynchronisme
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(672, 437);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionSynchronisme";
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Synchronization|159";
            this.Load += new System.EventHandler(this.CFormEditActionSynchronisme_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.c2iTabControl1.ResumeLayout(false);
            this.c2iTabControl1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			result = analyseur.AnalyseChaine ( m_txtDest.Text );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in synchronized element formula|30019"));
				return result;
			}
			ActionSynchronisme.FormuleDest = (C2iExpression)result.Data;
			
			result = analyseur.AnalyseChaine ( m_txtSource.Text );
			if ( !result  )
			{
				result.EmpileErreur(I.T("Error in source element formula|30020"));
				return result;
			}
			ActionSynchronisme.FormuleSource = (C2iExpression)result.Data;

			contexte = new CContexteAnalyse2iExpression ( new CFournisseurPropDynStd(true), ActionSynchronisme.FormuleDest.TypeDonnee.TypeDotNetNatif);
			analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			result = analyseur.AnalyseChaine ( m_txtConditionDest.Text );
			if ( !result  )
			{
				result.EmpileErreur(I.T("Error in the condition on destination element formula|30021"));
				return result;
			}
			ActionSynchronisme.FormuleConditionDest = (C2iExpression)result.Data;

			if ( m_cmbChampDest.SelectedValue is CInfoChampTable )
				ActionSynchronisme.ChampDest = ((CInfoChampTable)m_cmbChampDest.SelectedValue).NomChamp;
			if ( m_cmbChampSource.SelectedValue is CInfoChampTable )
				ActionSynchronisme.ChampSource = ((CInfoChampTable)m_cmbChampSource.SelectedValue).NomChamp;
			/*ActionModifier.ExpressionValeur = (C2iExpression)result.Data;
			if ( m_comboChamp.DefinitionSelectionnee != null )
				ActionModifier.Propriete = m_comboChamp.DefinitionSelectionnee;
			else
				ActionModifier.Propriete = null;
			if ( m_comboVariables.SelectedValue is IVariableDynamique )
				ActionModifier.VariableAModifier = (CVariableDynamique)m_comboVariables.SelectedValue;
			*/
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();

			m_txtDest.Init ( ActionSynchronisme.Process, typeof ( CProcess ) );
			if ( ActionSynchronisme.FormuleDest != null )
				m_txtDest.Text = ActionSynchronisme.FormuleDest.GetString();
			else
				m_txtDest.Text = "";

			m_txtSource.Init ( ActionSynchronisme.Process, typeof(CProcess));
			if ( ActionSynchronisme.FormuleSource != null )
				m_txtSource.Text = ActionSynchronisme.FormuleSource.GetString();
			else
				m_txtSource.Text = "";

			m_txtSource_Validated(m_txtSource, new EventArgs() );
			m_txtDest_Validated(m_txtDest, new EventArgs() );
			if ( m_cmbChampDest.ListDonnees != null )
			{
				foreach ( CInfoChampTable info in m_cmbChampDest.ListDonnees )
					if ( info.NomChamp == ActionSynchronisme.ChampDest )
					{
						m_cmbChampDest.SelectedValue = info;
						break;
					}
			}
			if ( m_cmbChampSource.ListDonnees != null )
			{
				foreach ( CInfoChampTable info in m_cmbChampSource.ListDonnees )
					if ( info.NomChamp == ActionSynchronisme.ChampSource )
					{
						m_cmbChampSource.SelectedValue = info;
						break;
					}
			}
			if ( ActionSynchronisme.FormuleConditionDest != null )
				m_txtConditionDest.Text = ActionSynchronisme.FormuleConditionDest.GetString();
			else
				m_txtConditionDest.Text = "";
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		
		private void m_comboVariables_SelectedValueChanged(object sender, System.EventArgs e)
		{
			/*if ( !(m_comboVariables.SelectedValue is CVariableDynamique) )
			{
				m_comboChamp.Enabled = false;
				return;
			}
			m_comboChamp.Enabled = true;
			CVariableDynamique variable = (CVariableDynamique)m_comboVariables.SelectedValue;
			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = false;
			m_comboChamp.Init ( four, variable.TypeDonnee.TypeDotNetNatif );*/
		}

		private void OnEnterZoneEdition(object sender, System.EventArgs e)
		{
			if ( sender is sc2i.win32.expression.CControleEditeFormule )
			{
				if ( m_txtFormule != null )
					m_txtFormule.BackColor = Color.White;
				m_txtFormule = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_txtFormule.BackColor = Color.LightGreen;
				if ( m_txtFormule == m_txtConditionDest )
				{
					if ( m_expressionDest != null )
					{
						m_wndAideFormule.FournisseurProprietes = new CFournisseurPropDynStd(true);
						m_wndAideFormule.ObjetInterroge = m_expressionDest.TypeDonnee.TypeDotNetNatif;
					}
					else
					{
						m_wndAideFormule.FournisseurProprietes = ActionSynchronisme.Process;
						m_wndAideFormule.ObjetInterroge = typeof(CProcess);
					}
					m_txtConditionDest.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
				}
				else
				{
					m_wndAideFormule.FournisseurProprietes = ActionSynchronisme.Process;
					m_wndAideFormule.ObjetInterroge = typeof(CProcess);
				}
			}
		}

		private void CFormEditActionSynchronisme_Load(object sender, System.EventArgs e)
		{
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            OnEnterZoneEdition(m_txtSource, new EventArgs());
		}

		/// <summary>
		/// ///////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_txtDest_Validated(object sender, System.EventArgs e)
		{
			OnChangeExpression ( m_txtDest.Text, ref m_expressionDest, m_cmbChampDest );
		}

		////////////////////////////////////////////
		private void OnChangeExpression(string strTexte, ref C2iExpression expToSet, CComboboxAutoFilled combo)
		{
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( ActionSynchronisme.Process, typeof(CProcess) );
			CResultAErreur result = new CAnalyseurSyntaxiqueExpression ( ctx ).AnalyseChaine ( strTexte );
			if ( result )
			{
				ArrayList lstChamps = new ArrayList();
				expToSet = (C2iExpression)result.Data;
				Type tp = expToSet.TypeDonnee.TypeDotNetNatif;
				CStructureTable structure = null;
				try
				{
					structure = CStructureTable.GetStructure(tp);
					ArrayList lst = new ArrayList();
					foreach ( CInfoChampTable champ in structure.Champs )
						if ( champ.TypeDonnee == typeof(DateTime) || champ.TypeDonnee == typeof(DateTime?) )
							lst.Add ( champ );
					//Cherche les champs custom
					if ( typeof ( CElementAChamp ).IsAssignableFrom ( tp ) )
					{
						//Crée les infos champ custom pour le type
						using ( CContexteDonnee contexte = new CContexteDonnee ( 
									CSessionClient.GetSessionUnique().IdSession,
									true, false ) )
						{
							string strCodeRole = CRoleChampCustom.GetRoleForType ( tp ).CodeRole;
                            CListeObjetsDonnees listeChamps = CChampCustom.GetListeChampsForRole(
                                contexte, strCodeRole);
                                new CListeObjetsDonnees ( contexte, typeof(CChampCustom ) );
							listeChamps.Filtre = new CFiltreData ( 
								CChampCustom.c_champType +"=@1",
								(int)TypeDonnee.tDate );
							foreach ( CChampCustom champ in listeChamps )
							{
								CInfoChampTable info = new CInfoChampTable ( 
									CSynchronismeDonnees.c_idChampCustom+champ.Id.ToString(),
									typeof(DateTime),
									0,
									false,
									false,
									true,
									false,
									true );
								info.NomConvivial = champ.Nom;
								lst.Add ( info );
							}
						}
					}
					combo.ListDonnees = lst;
				}
				catch
				{
				}
			}
		}

		private void m_txtSource_Validated(object sender, System.EventArgs e)
		{
			OnChangeExpression( m_txtSource.Text, ref m_expressionSource, m_cmbChampSource );
		}

        private void m_txtDest_Load(object sender, EventArgs e)
        {

        }
						

			
		

		




	}
}

