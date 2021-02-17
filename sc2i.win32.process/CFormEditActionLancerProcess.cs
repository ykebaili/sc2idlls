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

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionLancerProcess : sc2i.win32.process.CFormEditActionFonction
	{
		private Hashtable m_tableIdVariableToFormule = new Hashtable();
		private IVariableDynamique m_variableEnCoursEdition = null;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleEnCours = null;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.common.CComboboxAutoFilled m_cmbProcess;
		private System.Windows.Forms.Label m_lblNomVariable;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleVariable;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeVariables;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeCategories;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn2;
		private System.Windows.Forms.CheckBox m_chkAsynchrone;
		private sc2i.win32.common.C2iPanelOmbre m_panelCentre;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private sc2i.win32.common.C2iPanel m_panelHaut;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label3;
		private CheckBox m_chkContexteSepare;
        private Panel m_panelPointEntree;
        private Label label4;
        private CComboboxAutoFilled m_cmbPointEntree;
        private CheckBox m_chkSansTrace;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionLancerProcess()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionLancerProcess), typeof(CFormEditActionLancerProcess));
		}


		public CActionLancerProcess ActionLancerProcess
		{
			get
			{
				return (CActionLancerProcess)ObjetEdite;
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbProcess = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_lblNomVariable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtFormuleVariable = new sc2i.win32.expression.CControleEditeFormule();
            this.m_wndListeVariables = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_wndListeCategories = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn2 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_chkAsynchrone = new System.Windows.Forms.CheckBox();
            this.m_panelCentre = new sc2i.win32.common.C2iPanelOmbre();
            this.label3 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelHaut = new sc2i.win32.common.C2iPanel(this.components);
            this.m_chkContexteSepare = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_panelPointEntree = new System.Windows.Forms.Panel();
            this.m_cmbPointEntree = new sc2i.win32.common.CComboboxAutoFilled();
            this.label4 = new System.Windows.Forms.Label();
            this.m_chkSansTrace = new System.Windows.Forms.CheckBox();
            this.m_panelCentre.SuspendLayout();
            this.m_panelHaut.SuspendLayout();
            this.m_panelPointEntree.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Text = "Store the result in|30011";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Action|134";
            // 
            // m_cmbProcess
            // 
            this.m_cmbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbProcess.IsLink = false;
            this.m_cmbProcess.ListDonnees = null;
            this.m_cmbProcess.Location = new System.Drawing.Point(80, 8);
            this.m_cmbProcess.LockEdition = false;
            this.m_cmbProcess.Name = "m_cmbProcess";
            this.m_cmbProcess.NullAutorise = false;
            this.m_cmbProcess.ProprieteAffichee = null;
            this.m_cmbProcess.Size = new System.Drawing.Size(288, 21);
            this.m_cmbProcess.TabIndex = 3;
            this.m_cmbProcess.TextNull = "(vide)";
            this.m_cmbProcess.Tri = true;
            this.m_cmbProcess.SelectedIndexChanged += new System.EventHandler(this.m_cmbProcess_SelectedIndexChanged);
            // 
            // m_lblNomVariable
            // 
            this.m_lblNomVariable.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNomVariable.Location = new System.Drawing.Point(216, 8);
            this.m_lblNomVariable.Name = "m_lblNomVariable";
            this.m_lblNomVariable.Size = new System.Drawing.Size(232, 16);
            this.m_lblNomVariable.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(216, 24);
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
            this.m_txtFormuleVariable.Location = new System.Drawing.Point(216, 40);
            this.m_txtFormuleVariable.LockEdition = false;
            this.m_txtFormuleVariable.Name = "m_txtFormuleVariable";
            this.m_txtFormuleVariable.Size = new System.Drawing.Size(240, 179);
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
            this.m_wndListeVariables.Location = new System.Drawing.Point(8, 24);
            this.m_wndListeVariables.MultiSelect = false;
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(192, 195);
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
            // m_chkAsynchrone
            // 
            this.m_chkAsynchrone.Location = new System.Drawing.Point(374, 6);
            this.m_chkAsynchrone.Name = "m_chkAsynchrone";
            this.m_chkAsynchrone.Size = new System.Drawing.Size(174, 18);
            this.m_chkAsynchrone.TabIndex = 5;
            this.m_chkAsynchrone.Text = "Asynchronous mode|135";
            this.m_chkAsynchrone.Visible = false;
            this.m_chkAsynchrone.CheckedChanged += new System.EventHandler(this.m_chkAsynchrone_CheckedChanged);
            // 
            // m_panelCentre
            // 
            this.m_panelCentre.Controls.Add(this.m_lblNomVariable);
            this.m_panelCentre.Controls.Add(this.label2);
            this.m_panelCentre.Controls.Add(this.m_txtFormuleVariable);
            this.m_panelCentre.Controls.Add(this.m_wndListeVariables);
            this.m_panelCentre.Controls.Add(this.label3);
            this.m_panelCentre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCentre.Location = new System.Drawing.Point(0, 101);
            this.m_panelCentre.LockEdition = false;
            this.m_panelCentre.Name = "m_panelCentre";
            this.m_panelCentre.Size = new System.Drawing.Size(485, 240);
            this.m_panelCentre.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Variables|136";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(488, 72);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 269);
            this.m_wndAideFormule.TabIndex = 7;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_panelHaut
            // 
            this.m_panelHaut.Controls.Add(this.m_chkSansTrace);
            this.m_panelHaut.Controls.Add(this.m_chkContexteSepare);
            this.m_panelHaut.Controls.Add(this.label1);
            this.m_panelHaut.Controls.Add(this.m_chkAsynchrone);
            this.m_panelHaut.Controls.Add(this.m_cmbProcess);
            this.m_panelHaut.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHaut.Location = new System.Drawing.Point(0, 32);
            this.m_panelHaut.LockEdition = false;
            this.m_panelHaut.Name = "m_panelHaut";
            this.m_panelHaut.Size = new System.Drawing.Size(664, 40);
            this.m_panelHaut.TabIndex = 8;
            // 
            // m_chkContexteSepare
            // 
            this.m_chkContexteSepare.Location = new System.Drawing.Point(374, 22);
            this.m_chkContexteSepare.Name = "m_chkContexteSepare";
            this.m_chkContexteSepare.Size = new System.Drawing.Size(174, 18);
            this.m_chkContexteSepare.TabIndex = 6;
            this.m_chkContexteSepare.Text = "Separate data context|20009";
            this.m_chkContexteSepare.CheckedChanged += new System.EventHandler(this.m_chkContexteSepare_CheckedChanged);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(485, 72);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 269);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // m_panelPointEntree
            // 
            this.m_panelPointEntree.Controls.Add(this.m_cmbPointEntree);
            this.m_panelPointEntree.Controls.Add(this.label4);
            this.m_panelPointEntree.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelPointEntree.Location = new System.Drawing.Point(0, 72);
            this.m_panelPointEntree.Name = "m_panelPointEntree";
            this.m_panelPointEntree.Size = new System.Drawing.Size(485, 29);
            this.m_panelPointEntree.TabIndex = 5;
            // 
            // m_cmbPointEntree
            // 
            this.m_cmbPointEntree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbPointEntree.IsLink = false;
            this.m_cmbPointEntree.ListDonnees = null;
            this.m_cmbPointEntree.Location = new System.Drawing.Point(144, 4);
            this.m_cmbPointEntree.LockEdition = false;
            this.m_cmbPointEntree.Name = "m_cmbPointEntree";
            this.m_cmbPointEntree.NullAutorise = false;
            this.m_cmbPointEntree.ProprieteAffichee = null;
            this.m_cmbPointEntree.Size = new System.Drawing.Size(335, 21);
            this.m_cmbPointEntree.TabIndex = 4;
            this.m_cmbPointEntree.TextNull = "(vide)";
            this.m_cmbPointEntree.Tri = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Entry point|20011";
            // 
            // m_chkSansTrace
            // 
            this.m_chkSansTrace.Location = new System.Drawing.Point(539, 6);
            this.m_chkSansTrace.Name = "m_chkSansTrace";
            this.m_chkSansTrace.Size = new System.Drawing.Size(122, 18);
            this.m_chkSansTrace.TabIndex = 7;
            this.m_chkSansTrace.Text = "Don\'t trace|20026";
            // 
            // CFormEditActionLancerProcess
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 389);
            this.Controls.Add(this.m_panelCentre);
            this.Controls.Add(this.m_panelPointEntree);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_wndAideFormule);
            this.Controls.Add(this.m_panelHaut);
            this.Name = "CFormEditActionLancerProcess";
            this.Text = "Start an Action|133";
            this.Load += new System.EventHandler(this.CFormEditActionLancerProcess_Load);
            this.Controls.SetChildIndex(this.m_panelHaut, 0);
            this.Controls.SetChildIndex(this.m_wndAideFormule, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.m_panelPointEntree, 0);
            this.Controls.SetChildIndex(this.m_panelCentre, 0);
            this.m_panelCentre.ResumeLayout(false);
            this.m_panelCentre.PerformLayout();
            this.m_panelHaut.ResumeLayout(false);
            this.m_panelPointEntree.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();

			OnChangeVariable();

			if ( !(m_cmbProcess.SelectedValue is CProcessInDb ) )
			{
				result.EmpileErreur(I.T("Select an action|30014"));
			}
			
			CProcessInDb processInDB = (CProcessInDb )m_cmbProcess.SelectedValue;
            ActionLancerProcess.DbKeyProcess = processInDB.DbKey;

			ActionLancerProcess.ClearExpressionsVariables();
            foreach (string strIdVariable in m_tableIdVariableToFormule.Keys)
            {
                C2iExpression exp = (C2iExpression)m_tableIdVariableToFormule[strIdVariable];
                if (exp != null)
                    ActionLancerProcess.SetExpressionForVariableProcess(strIdVariable, exp);
            }

			ActionLancerProcess.ModeAsynchrone = m_chkAsynchrone.Checked;
            ActionLancerProcess.SansTrace = m_chkSansTrace.Checked;
			ActionLancerProcess.LancerDansUnProcessSepare = m_chkContexteSepare.Checked || 
				!processInDB.Process.PeutEtreExecuteSurLePosteClient;

            CActionPointEntree actionEntree = m_cmbPointEntree.SelectedValue as CActionPointEntree;
            if (actionEntree != null)
                ActionLancerProcess.IdPointEntree = actionEntree.IdObjetProcess;
            else
                ActionLancerProcess.IdPointEntree = -1;
			return result;
		}

		

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			m_txtFormuleVariable.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			CListeObjetsDonnees liste = new CListeObjetsDonnees ( CSc2iWin32DataClient.ContexteCourant, typeof(CProcessInDb) );
			m_cmbProcess.ListDonnees = liste;
			m_cmbProcess.ProprieteAffichee = "Libelle";
			CProcessInDb processInDB = new CProcessInDb ( CSc2iWin32DataClient.ContexteCourant );
			m_tableIdVariableToFormule.Clear();
            CProcess processALancer = null;
            if (processInDB.ReadIfExists(ActionLancerProcess.DbKeyProcess))
            {
                m_cmbProcess.SelectedValue = processInDB;
                processALancer = processInDB.Process;
                if (processALancer != null)
                {
                    foreach (IVariableDynamique variable in processALancer.ListeVariables)
                        m_tableIdVariableToFormule[variable.IdVariable] = ActionLancerProcess.GetExpressionForVariableProcess(variable.IdVariable);
                }
            }
            else
                m_cmbProcess.SelectedValue = null;

			UpdateListeVariables();

			m_chkAsynchrone.Checked = ActionLancerProcess.ModeAsynchrone;
            m_chkSansTrace.Checked = ActionLancerProcess.SansTrace;
			m_chkContexteSepare.Checked = ActionLancerProcess.LancerDansUnProcessSepare;

            UpdatePointsEntree();

            
		}

        /// //////////////////////////////////////////
        private void UpdatePointsEntree()
        {
            m_panelPointEntree.Visible = false;
            m_cmbPointEntree.TextNull = I.T("Default|20012");
            m_cmbPointEntree.Fill(new CActionPointEntree[0], "Libelle", true);
            if (!(m_cmbProcess.SelectedValue is CProcessInDb))
            {
                m_cmbPointEntree.SelectedValue = null;
                return;
            }
            CProcessInDb processInDB = (CProcessInDb)m_cmbProcess.SelectedValue;
            CProcess process = processInDB.Process;
            if (process == null)
            {
                return;
            }
            CActionPointEntree[] entrees =  process.PointsEntreeAlternatifs;
            
            m_cmbPointEntree.Fill ( entrees, "Libelle", true );
            if (entrees.Length == 0)
            {
                m_panelPointEntree.Visible = false;
                m_cmbPointEntree.SelectedValue = null;                
            }
            else
            {
                m_panelPointEntree.Visible = true;
                m_cmbPointEntree.SelectedValue = process.GetActionFromId ( ActionLancerProcess.IdPointEntree );
            }
        }


		/// //////////////////////////////////////////
		private bool m_bListeVariablesRemplie = false;
		private void UpdateListeVariables()
		{
			m_bListeVariablesRemplie = false;
			m_variableEnCoursEdition = null;
			m_txtFormuleVariable.Visible = false;
			if ( !(m_cmbProcess.SelectedValue is CProcessInDb ))
			{
				m_wndListeVariables.Enabled = false;
				m_txtFormuleVariable.Visible = false;
				return;
			}
			m_wndListeVariables.Enabled = true;
			m_txtFormuleVariable.Visible = true;

			CProcessInDb processInDB = (CProcessInDb)m_cmbProcess.SelectedValue;
			CProcess process =  processInDB.Process;
			if ( process == null )
			{
				m_wndListeVariables.Enabled = false;
				m_txtFormuleVariable.Visible = false;
				return;
			}
			ArrayList lst = new ArrayList(process.ListeVariables);
			m_wndListeVariables.Remplir ( lst, false );
			m_bListeVariablesRemplie = true;
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
					m_tableIdVariableToFormule[m_variableEnCoursEdition.IdVariable] = result.Data;
				}
				else
					m_tableIdVariableToFormule[m_variableEnCoursEdition.IdVariable] = null;

			}
			if ( m_wndListeVariables.SelectedItems.Count != 1 )
			{
				m_variableEnCoursEdition = null;
				m_txtFormuleVariable.Visible = true;
				return ;
			}
			
			m_variableEnCoursEdition = (IVariableDynamique)m_wndListeVariables.SelectedItems[0].Tag;
			m_lblNomVariable.Text = m_variableEnCoursEdition.Nom;
			C2iExpression expression = (C2iExpression)m_tableIdVariableToFormule[m_variableEnCoursEdition.IdVariable];
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
		private void CFormEditActionLancerProcess_Load(object sender, System.EventArgs e)
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
			UpdateListeVariables();
            UpdatePointsEntree();
		}

        private void m_chkContexteSepare_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void m_chkAsynchrone_CheckedChanged(object sender, EventArgs e)
        {
            m_chkSansTrace.Visible = !m_chkAsynchrone.Checked;
        }

		

		




	}
}

