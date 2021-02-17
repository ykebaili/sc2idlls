using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.win32.data;
using sc2i.win32.common;

//Masqué par stef le 07/01/2013 (voir commentaire sur ActionProcessDistant
/*
namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionLancerProcessDistant : sc2i.win32.process.CFormEditActionFonction
	{
		private Hashtable m_tableIdVariableToFormule = new Hashtable();
		private CVariableDynamiqueSaisie m_variableEnCoursEdition = null;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleEnCours = null;
		private sc2i.win32.common.CComboboxAutoFilled m_cmbProcess;
		private System.Windows.Forms.Label m_lblNomVariable;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleVariable;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeVariables;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeCategories;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn2;
		private sc2i.win32.common.C2iPanelOmbre m_panelCentre;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private sc2i.win32.common.C2iPanel m_panelHaut;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox m_cmbURL;
		private System.Windows.Forms.LinkLabel m_lnkMAJ;
        private SplitContainer m_splitContainer2;
        private SplitContainer m_splitContainer1;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionLancerProcessDistant()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionLancerProcessDistant), typeof(CFormEditActionLancerProcessDistant));
		}


		public CActionLancerProcessDistant ActionLancerProcessDistant
		{
			get
			{
				return (CActionLancerProcessDistant)ObjetEdite;
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
            this.m_cmbProcess = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_lblNomVariable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtFormuleVariable = new sc2i.win32.expression.CControleEditeFormule();
            this.m_wndListeVariables = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_wndListeCategories = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn2 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_panelCentre = new sc2i.win32.common.C2iPanelOmbre();
            this.m_splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelHaut = new sc2i.win32.common.C2iPanel(this.components);
            this.m_lnkMAJ = new System.Windows.Forms.LinkLabel();
            this.m_cmbURL = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_panelCentre.SuspendLayout();
            this.m_splitContainer2.Panel1.SuspendLayout();
            this.m_splitContainer2.Panel2.SuspendLayout();
            this.m_splitContainer2.SuspendLayout();
            this.m_panelHaut.SuspendLayout();
            this.m_splitContainer1.Panel1.SuspendLayout();
            this.m_splitContainer1.Panel2.SuspendLayout();
            this.m_splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Location = new System.Drawing.Point(3, 4);
            this.m_lblStockerResIn.Text = "Store the result in|30003";
            // 
            // m_cmbProcess
            // 
            this.m_cmbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbProcess.IsLink = false;
            this.m_cmbProcess.ListDonnees = null;
            this.m_cmbProcess.Location = new System.Drawing.Point(80, 48);
            this.m_cmbProcess.LockEdition = false;
            this.m_cmbProcess.Name = "m_cmbProcess";
            this.m_cmbProcess.NullAutorise = false;
            this.m_cmbProcess.ProprieteAffichee = null;
            this.m_cmbProcess.Size = new System.Drawing.Size(568, 21);
            this.m_cmbProcess.TabIndex = 3;
            this.m_cmbProcess.TextNull = I.T("(empty)|30018");
            this.m_cmbProcess.Tri = true;
            this.m_cmbProcess.SelectedIndexChanged += new System.EventHandler(this.m_cmbProcess_SelectedIndexChanged);
            // 
            // m_lblNomVariable
            // 
            this.m_lblNomVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblNomVariable.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNomVariable.Location = new System.Drawing.Point(5, 6);
            this.m_lblNomVariable.Name = "m_lblNomVariable";
            this.m_lblNomVariable.Size = new System.Drawing.Size(207, 16);
            this.m_lblNomVariable.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Variable value|137";
            // 
            // m_txtFormuleVariable
            // 
            this.m_txtFormuleVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleVariable.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleVariable.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleVariable.Formule = null;
            this.m_txtFormuleVariable.Location = new System.Drawing.Point(5, 47);
            this.m_txtFormuleVariable.LockEdition = false;
            this.m_txtFormuleVariable.Name = "m_txtFormuleVariable";
            this.m_txtFormuleVariable.Size = new System.Drawing.Size(202, 249);
            this.m_txtFormuleVariable.TabIndex = 1;
            this.m_txtFormuleVariable.Enter += new System.EventHandler(this.OnEnterTextBoxFormule);
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn1});
            this.m_wndListeVariables.EnableCustomisation = true;
            this.m_wndListeVariables.FullRowSelect = true;
            this.m_wndListeVariables.Location = new System.Drawing.Point(11, 28);
            this.m_wndListeVariables.MultiSelect = false;
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(229, 268);
            this.m_wndListeVariables.TabIndex = 0;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.SelectedIndexChanged += new System.EventHandler(this.m_wndListeVariables_SelectedIndexChanged);
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "Nom";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Name|205";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 176;
            // 
            // m_wndListeCategories
            // 
            this.m_wndListeCategories.CheckBoxes = true;
            this.m_wndListeCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn2});
            this.m_wndListeCategories.EnableCustomisation = true;
            this.m_wndListeCategories.FullRowSelect = true;
            this.m_wndListeCategories.Location = new System.Drawing.Point(8, 24);
            this.m_wndListeCategories.MultiSelect = false;
            this.m_wndListeCategories.Name = "m_wndListeCategories";
            this.m_wndListeCategories.Size = new System.Drawing.Size(296, 240);
            this.m_wndListeCategories.TabIndex = 0;
            this.m_wndListeCategories.UseCompatibleStateImageBehavior = false;
            this.m_wndListeCategories.View = System.Windows.Forms.View.Details;
            // 
            // listViewAutoFilledColumn2
            // 
            this.listViewAutoFilledColumn2.Field = "Libelle";
            this.listViewAutoFilledColumn2.PrecisionWidth = 0;
            this.listViewAutoFilledColumn2.ProportionnalSize = false;
            this.listViewAutoFilledColumn2.Text = "Category|10002";
            this.listViewAutoFilledColumn2.Visible = true;
            this.listViewAutoFilledColumn2.Width = 279;
            // 
            // m_panelCentre
            // 
            this.m_panelCentre.Controls.Add(this.m_splitContainer2);
            this.m_panelCentre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCentre.Location = new System.Drawing.Point(0, 0);
            this.m_panelCentre.LockEdition = false;
            this.m_panelCentre.Name = "m_panelCentre";
            this.m_panelCentre.Size = new System.Drawing.Size(480, 318);
            this.m_panelCentre.TabIndex = 6;
            // 
            // m_splitContainer2
            // 
            this.m_splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer2.Name = "m_splitContainer2";
            // 
            // m_splitContainer2.Panel1
            // 
            this.m_splitContainer2.Panel1.Controls.Add(this.m_wndListeVariables);
            this.m_splitContainer2.Panel1.Controls.Add(this.label3);
            // 
            // m_splitContainer2.Panel2
            // 
            this.m_splitContainer2.Panel2.Controls.Add(this.m_lblNomVariable);
            this.m_splitContainer2.Panel2.Controls.Add(this.label2);
            this.m_splitContainer2.Panel2.Controls.Add(this.m_txtFormuleVariable);
            this.m_splitContainer2.Size = new System.Drawing.Size(480, 318);
            this.m_splitContainer2.SplitterDistance = 243;
            this.m_splitContainer2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Variables|136";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(213, 318);
            this.m_wndAideFormule.TabIndex = 7;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_panelHaut
            // 
            this.m_panelHaut.Controls.Add(this.m_lnkMAJ);
            this.m_panelHaut.Controls.Add(this.m_cmbURL);
            this.m_panelHaut.Controls.Add(this.label4);
            this.m_panelHaut.Controls.Add(this.m_lblStockerResIn);
            this.m_panelHaut.Controls.Add(this.m_cmbProcess);
            this.m_panelHaut.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHaut.Location = new System.Drawing.Point(0, 32);
            this.m_panelHaut.LockEdition = false;
            this.m_panelHaut.Name = "m_panelHaut";
            this.m_panelHaut.Size = new System.Drawing.Size(701, 72);
            this.m_panelHaut.TabIndex = 8;
            this.m_panelHaut.Controls.SetChildIndex(this.m_cmbProcess, 0);
            this.m_panelHaut.Controls.SetChildIndex(this.m_lblStockerResIn, 0);
            this.m_panelHaut.Controls.SetChildIndex(this.label4, 0);
            this.m_panelHaut.Controls.SetChildIndex(this.m_cmbURL, 0);
            this.m_panelHaut.Controls.SetChildIndex(this.m_lnkMAJ, 0);
            // 
            // m_lnkMAJ
            // 
            this.m_lnkMAJ.Location = new System.Drawing.Point(575, 16);
            this.m_lnkMAJ.Name = "m_lnkMAJ";
            this.m_lnkMAJ.Size = new System.Drawing.Size(89, 23);
            this.m_lnkMAJ.TabIndex = 6;
            this.m_lnkMAJ.TabStop = true;
            this.m_lnkMAJ.Text = "Update|139";
            this.m_lnkMAJ.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkMAJ_LinkClicked);
            // 
            // m_cmbURL
            // 
            this.m_cmbURL.Location = new System.Drawing.Point(80, 16);
            this.m_cmbURL.Name = "m_cmbURL";
            this.m_cmbURL.Size = new System.Drawing.Size(498, 21);
            this.m_cmbURL.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "URL|138";
            // 
            // m_splitContainer1
            // 
            this.m_splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer1.Location = new System.Drawing.Point(0, 104);
            this.m_splitContainer1.Name = "m_splitContainer1";
            // 
            // m_splitContainer1.Panel1
            // 
            this.m_splitContainer1.Panel1.Controls.Add(this.m_panelCentre);
            // 
            // m_splitContainer1.Panel2
            // 
            this.m_splitContainer1.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer1.Size = new System.Drawing.Size(701, 320);
            this.m_splitContainer1.SplitterDistance = 482;
            this.m_splitContainer1.TabIndex = 9;
            // 
            // CFormEditActionLancerProcessDistant
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(701, 472);
            this.Controls.Add(this.m_splitContainer1);
            this.Controls.Add(this.m_panelHaut);
            this.Name = "CFormEditActionLancerProcessDistant";
            this.Text = "Start an action|133";
            this.Load += new System.EventHandler(this.CFormEditActionLancerProcessDistant_Load);
            this.Controls.SetChildIndex(this.m_panelHaut, 0);
            this.Controls.SetChildIndex(this.m_splitContainer1, 0);
            this.m_panelCentre.ResumeLayout(false);
            this.m_panelCentre.PerformLayout();
            this.m_splitContainer2.Panel1.ResumeLayout(false);
            this.m_splitContainer2.Panel2.ResumeLayout(false);
            this.m_splitContainer2.ResumeLayout(false);
            this.m_panelHaut.ResumeLayout(false);
            this.m_splitContainer1.Panel1.ResumeLayout(false);
            this.m_splitContainer1.Panel2.ResumeLayout(false);
            this.m_splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();

			OnChangeVariable();

			if ( !(m_cmbProcess.SelectedValue is CActionLancerProcessDistant.CInfoProcessDistant ) )
			{
				result.EmpileErreur("Select an action|30014");
			}
			
			CActionLancerProcessDistant.CInfoProcessDistant process = (CActionLancerProcessDistant.CInfoProcessDistant )m_cmbProcess.SelectedValue;
			ActionLancerProcessDistant.IdProcess = process.IdProcess;
			ActionLancerProcessDistant.URL = process.URL;
			ActionLancerProcessDistant.NomProcess = process.NomProcess;
			ActionLancerProcessDistant.TypeRetourProcess = process.TypeRetour;
			ActionLancerProcessDistant.ClearExpressionsVariables();
			ActionLancerProcessDistant.VariablesDistantes = process.Variables;
			foreach ( int nIdVariable in m_tableIdVariableToFormule.Keys )
			{
				C2iExpression exp = (C2iExpression)m_tableIdVariableToFormule[nIdVariable];
				if ( exp != null )
					ActionLancerProcessDistant.SetExpressionForVariableProcess ( nIdVariable, exp );
			}


			return result;
		}

		

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			m_cmbURL.Items.Clear();

			string[] strUrls = new CSc2iProcessRegistre().LastURLsActionsDistantes;
			foreach ( string strUrl in strUrls )
				m_cmbURL.Items.Add ( strUrl );

			m_cmbURL.Text = ActionLancerProcessDistant.URL;

			m_txtFormuleVariable.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			RefreshFromURL();
			m_cmbProcess.SelectedValue = null;
			foreach ( CActionLancerProcessDistant.CInfoProcessDistant info in m_cmbProcess.ListDonnees )
			{
				if ( info.IdProcess == ActionLancerProcessDistant.IdProcess )
					m_cmbProcess.SelectedValue = info;
			}

			UpdateListeVariables();


		}

		/// //////////////////////////////////////////
		private bool m_bListeVariablesRemplie = false;
		private void UpdateListeVariables()
		{
			m_bListeVariablesRemplie = false;
			m_variableEnCoursEdition = null;
			m_txtFormuleVariable.Visible = false;
			if ( !(m_cmbProcess.SelectedValue is CActionLancerProcessDistant.CInfoProcessDistant ))
			{
				m_wndListeVariables.Enabled = false;
				m_txtFormuleVariable.Visible = false;
				return;
			}
			m_wndListeVariables.Enabled = true;
			m_txtFormuleVariable.Visible = true;

			CActionLancerProcessDistant.CInfoProcessDistant process = (CActionLancerProcessDistant.CInfoProcessDistant)m_cmbProcess.SelectedValue;
			if ( process == null )
			{
				m_wndListeVariables.Enabled = false;
				m_txtFormuleVariable.Visible = false;
				return;
			}
			ArrayList lst = new ArrayList (process.Variables);
			m_wndListeVariables.Remplir ( lst, false );
			foreach ( IVariableDynamique variable in process.Variables )
				m_tableIdVariableToFormule[variable.Id] = ActionLancerProcessDistant.GetExpressionForVariableProcess ( variable.Id );
			m_bListeVariablesRemplie = true;
		}

		/// ////////////////////////////////////////////////////////////////
		private CResultAErreur GetFormule ( sc2i.win32.expression.CControleEditeFormule textBox )
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			result = analyseur.AnalyseChaine ( textBox.Text );
			return result;
		}

		/// //////////////////////////////////////////
		private void OnChangeVariable()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_variableEnCoursEdition != null )
			{
				if ( m_txtFormuleVariable.Text.Trim() != "" )
				{
					result = GetFormule ( m_txtFormuleVariable );
					if ( !result )
					{
						CFormAlerte.Afficher( result);
						return ;
					}
					m_tableIdVariableToFormule[m_variableEnCoursEdition.Id] = result.Data;
				}
				else
					m_tableIdVariableToFormule[m_variableEnCoursEdition.Id] = null;

			}
			if ( m_wndListeVariables.SelectedItems.Count != 1 )
			{
				m_variableEnCoursEdition = null;
				m_txtFormuleVariable.Visible = true;
				return ;
			}
			
			m_variableEnCoursEdition = (CVariableDynamiqueSaisie)m_wndListeVariables.SelectedItems[0].Tag;
			m_lblNomVariable.Text = m_variableEnCoursEdition.Nom;
			C2iExpression expression = (C2iExpression)m_tableIdVariableToFormule[m_variableEnCoursEdition.Id];
			m_txtFormuleVariable.Text = expression==null?"":expression.GetString();
		}

		/// //////////////////////////////////////////
		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_txtFormuleEnCours != null )
				m_wndAideFormule.InsereInTextBox ( m_txtFormuleEnCours, nPosCurseur, strCommande );
		}
	

		/// //////////////////////////////////////////
		private void OnEnterTextBoxFormule(object sender, System.EventArgs e)
		{
			if ( sender is sc2i.win32.expression.CControleEditeFormule )
			{
				if ( m_txtFormuleEnCours != null )
					m_txtFormuleEnCours.BackColor = Color.White;
				m_txtFormuleEnCours = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_txtFormuleEnCours.BackColor = Color.LightGreen;
			}
			
		}

		/// //////////////////////////////////////////
		private void CFormEditActionLancerProcessDistant_Load(object sender, System.EventArgs e)
		{
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            OnEnterTextBoxFormule(m_txtFormuleVariable, new EventArgs());
		}

		/// //////////////////////////////////////////
		private void m_wndListeVariables_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			if ( m_bListeVariablesRemplie )
				OnChangeVariable();
		}

		/// //////////////////////////////////////////
		private void m_cmbProcess_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( !m_bIsFillingActions )
				UpdateListeVariables();
		}

		
		private string m_strLastURLEnter = "";
		/// //////////////////////////////////////////
		private void m_txtURL_Enter(object sender, System.EventArgs e)
		{
			m_strLastURLEnter = m_cmbURL.Text;
		}

		/// //////////////////////////////////////////
		private void m_txtURL_Leave(object sender, System.EventArgs e)
		{
			if ( m_strLastURLEnter != m_cmbURL.Text )
				RefreshFromURL();
		}

		/// //////////////////////////////////////////
		private void SaveUrls()
		{
			ArrayList lst = new ArrayList();
			foreach ( string strUrl in m_cmbURL.Items )
				lst.Add ( strUrl );
			new CSc2iProcessRegistre().LastURLsActionsDistantes = (string[])lst.ToArray(typeof(string));
		}

		/// //////////////////////////////////////////
		private bool m_bIsFillingActions = false;
		private void RefreshFromURL()
		{
			CActionLancerProcessDistant.CInfoProcessDistant[] infos =
				CActionLancerProcessDistant.GetProcessDispo ( m_cmbURL.Text );
			CActionLancerProcessDistant.CInfoProcessDistant m_lastInfo = (CActionLancerProcessDistant.CInfoProcessDistant)m_cmbProcess.SelectedValue;
			if ( infos == null )
			{
				if ( ActionLancerProcessDistant.IdProcess >= 0 )
				{
					CActionLancerProcessDistant.CInfoProcessDistant info =
						new CActionLancerProcessDistant.CInfoProcessDistant ( 
						ActionLancerProcessDistant.URL,
						ActionLancerProcessDistant.IdProcess,
						ActionLancerProcessDistant.NomProcess,
						ActionLancerProcessDistant.TypeRetourProcess,
						ActionLancerProcessDistant.VariablesDistantes );


					ArrayList lst =  new ArrayList();
					lst.Add ( info );
					m_cmbProcess.ListDonnees = lst;
					m_cmbProcess.SelectedValue = info;
				}
				else
					m_cmbProcess.ListDonnees = new ArrayList();
			}
			else
			{
				bool bExist = false;
				foreach ( string strVal in m_cmbURL.Items )
				{
					if ( strVal.ToUpper() == m_cmbURL.Text.ToUpper() )
					{
						bExist = true;
						break;
					}
				}
				if ( !bExist )
				{
					m_cmbURL.Items.Insert (0, m_cmbURL.Text );
					SaveUrls();
				}
				m_bIsFillingActions = true;
				m_cmbProcess.ListDonnees = infos;
				m_cmbProcess.ProprieteAffichee = "NomProcess";
				m_cmbProcess.SelectedValue = m_lastInfo;
				m_bIsFillingActions = false;
				UpdateListeVariables();
			}
			
		}

		private void m_lnkMAJ_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			RefreshFromURL();
		}

		

		




	}
}

*/