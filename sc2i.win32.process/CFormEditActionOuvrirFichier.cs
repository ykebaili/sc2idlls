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
using sc2i.win32.expression;
using sc2i.data;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionOuvrirFichier : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private CVariableProcessTypeComplexe m_variableNew = null;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleFileName;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
        private SplitContainer m_splitContainer;
        private SplitContainer m_splitContainer2;
        private sc2i.win32.expression.CControleEditeFormule m_txtFormuleArguments;
        private Label label2;
		private Panel m_panelVariable;
		private LinkLabel m_lnkNouvelleVariable;
		private sc2i.win32.common.CComboboxAutoFilled m_comboBoxVariables;
		protected Label m_lblStockerResIn;
		private CheckBox m_chkWaitForExit;
		private CheckBox m_chkSurServeur;
		private System.ComponentModel.IContainer components = null;
        

		public CFormEditActionOuvrirFichier()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionOuvrirFichier), typeof(CFormEditActionOuvrirFichier));
		}


		public CActionOuvrirFichier ActionOuvrirFichier
		{
			get
			{
				return (CActionOuvrirFichier)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_chkSurServeur = new System.Windows.Forms.CheckBox();
            this.m_chkWaitForExit = new System.Windows.Forms.CheckBox();
            this.m_txtFormuleFileName = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtFormuleArguments = new sc2i.win32.expression.CControleEditeFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelVariable = new System.Windows.Forms.Panel();
            this.m_lnkNouvelleVariable = new System.Windows.Forms.LinkLabel();
            this.m_comboBoxVariables = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_lblStockerResIn = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.m_splitContainer2.Panel1.SuspendLayout();
            this.m_splitContainer2.Panel2.SuspendLayout();
            this.m_splitContainer2.SuspendLayout();
            this.m_panelVariable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_splitContainer);
            this.panel2.Controls.Add(this.m_panelVariable);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(648, 393);
            this.panel2.TabIndex = 2;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 25);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_splitContainer2);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer.Size = new System.Drawing.Size(648, 368);
            this.m_splitContainer.SplitterDistance = 428;
            this.m_splitContainer.TabIndex = 3;
            // 
            // m_splitContainer2
            // 
            this.m_splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer2.Name = "m_splitContainer2";
            this.m_splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainer2.Panel1
            // 
            this.m_splitContainer2.Panel1.Controls.Add(this.m_chkSurServeur);
            this.m_splitContainer2.Panel1.Controls.Add(this.m_chkWaitForExit);
            this.m_splitContainer2.Panel1.Controls.Add(this.m_txtFormuleFileName);
            this.m_splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // m_splitContainer2.Panel2
            // 
            this.m_splitContainer2.Panel2.Controls.Add(this.m_txtFormuleArguments);
            this.m_splitContainer2.Panel2.Controls.Add(this.label2);
            this.m_splitContainer2.Size = new System.Drawing.Size(428, 368);
            this.m_splitContainer2.SplitterDistance = 170;
            this.m_splitContainer2.TabIndex = 3;
            // 
            // m_chkSurServeur
            // 
            this.m_chkSurServeur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkSurServeur.Location = new System.Drawing.Point(234, 150);
            this.m_chkSurServeur.Name = "m_chkSurServeur";
            this.m_chkSurServeur.Size = new System.Drawing.Size(133, 16);
            this.m_chkSurServeur.TabIndex = 3;
            this.m_chkSurServeur.Text = "On server|20007";
            this.m_chkSurServeur.UseVisualStyleBackColor = true;
            // 
            // m_chkWaitForExit
            // 
            this.m_chkWaitForExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkWaitForExit.Location = new System.Drawing.Point(6, 150);
            this.m_chkWaitForExit.Name = "m_chkWaitForExit";
            this.m_chkWaitForExit.Size = new System.Drawing.Size(173, 16);
            this.m_chkWaitForExit.TabIndex = 1;
            this.m_chkWaitForExit.Text = "Wait for exit|20003";
            this.m_chkWaitForExit.UseVisualStyleBackColor = true;
            // 
            // m_txtFormuleFileName
            // 
            this.m_txtFormuleFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleFileName.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleFileName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleFileName.Formule = null;
            this.m_txtFormuleFileName.Location = new System.Drawing.Point(6, 19);
            this.m_txtFormuleFileName.LockEdition = false;
            this.m_txtFormuleFileName.Name = "m_txtFormuleFileName";
            this.m_txtFormuleFileName.Size = new System.Drawing.Size(415, 129);
            this.m_txtFormuleFileName.TabIndex = 2;
            this.m_txtFormuleFileName.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Open a File or an URL|153";
            // 
            // m_txtFormuleArguments
            // 
            this.m_txtFormuleArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleArguments.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleArguments.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleArguments.Formule = null;
            this.m_txtFormuleArguments.Location = new System.Drawing.Point(6, 19);
            this.m_txtFormuleArguments.LockEdition = false;
            this.m_txtFormuleArguments.Name = "m_txtFormuleArguments";
            this.m_txtFormuleArguments.Size = new System.Drawing.Size(415, 152);
            this.m_txtFormuleArguments.TabIndex = 2;
            this.m_txtFormuleArguments.Enter += new System.EventHandler(this.m_txtFormule_Enter);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Arguments|218";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(212, 364);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_panelVariable
            // 
            this.m_panelVariable.Controls.Add(this.m_lnkNouvelleVariable);
            this.m_panelVariable.Controls.Add(this.m_comboBoxVariables);
            this.m_panelVariable.Controls.Add(this.m_lblStockerResIn);
            this.m_panelVariable.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelVariable.Location = new System.Drawing.Point(0, 0);
            this.m_panelVariable.Name = "m_panelVariable";
            this.m_panelVariable.Size = new System.Drawing.Size(648, 25);
            this.m_panelVariable.TabIndex = 3;
            // 
            // m_lnkNouvelleVariable
            // 
            this.m_lnkNouvelleVariable.Location = new System.Drawing.Point(361, 7);
            this.m_lnkNouvelleVariable.Name = "m_lnkNouvelleVariable";
            this.m_lnkNouvelleVariable.Size = new System.Drawing.Size(112, 16);
            this.m_lnkNouvelleVariable.TabIndex = 5;
            this.m_lnkNouvelleVariable.TabStop = true;
            this.m_lnkNouvelleVariable.Text = "New variable|127";
            this.m_lnkNouvelleVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkNouvelleVariable_LinkClicked);
            // 
            // m_comboBoxVariables
            // 
            this.m_comboBoxVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboBoxVariables.IsLink = false;
            this.m_comboBoxVariables.ListDonnees = null;
            this.m_comboBoxVariables.Location = new System.Drawing.Point(146, 4);
            this.m_comboBoxVariables.LockEdition = false;
            this.m_comboBoxVariables.Name = "m_comboBoxVariables";
            this.m_comboBoxVariables.NullAutorise = true;
            this.m_comboBoxVariables.ProprieteAffichee = null;
            this.m_comboBoxVariables.Size = new System.Drawing.Size(209, 21);
            this.m_comboBoxVariables.TabIndex = 4;
            this.m_comboBoxVariables.TextNull = "(to affect)";
            this.m_comboBoxVariables.Tri = true;
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Location = new System.Drawing.Point(5, 6);
            this.m_lblStockerResIn.Name = "m_lblStockerResIn";
            this.m_lblStockerResIn.Size = new System.Drawing.Size(141, 16);
            this.m_lblStockerResIn.TabIndex = 3;
            this.m_lblStockerResIn.Text = "Store the result in|125";
            // 
            // CFormEditActionOuvrirFichier
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(648, 439);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionOuvrirFichier";
            this.Text = "Open a File or an URL|153";
            this.Load += new System.EventHandler(this.CFormEditActionOuvrirFichier_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.m_splitContainer2.Panel1.ResumeLayout(false);
            this.m_splitContainer2.Panel2.ResumeLayout(false);
            this.m_splitContainer2.ResumeLayout(false);
            this.m_panelVariable.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			
            result = analyseur.AnalyseChaine ( m_txtFormuleFileName.Text );
			if ( !result )
				return result;
			ActionOuvrirFichier.FormuleFichier = (C2iExpression)result.Data;

            result = analyseur.AnalyseChaine(m_txtFormuleArguments.Text);
            if (!result)
                return result;
            ActionOuvrirFichier.FormuleArguments = (C2iExpression)result.Data;

			if (m_comboBoxVariables.SelectedValue is CVariableDynamique)
			{
				CVariableDynamique variable = (CVariableDynamique)m_comboBoxVariables.SelectedValue;
				if (variable.IdVariable == "")
				{
					m_variableNew = new CVariableProcessTypeComplexe(ActionOuvrirFichier.Process);
					m_variableNew.Nom = variable.Nom;
					m_variableNew.SetTypeDonnee(new CTypeResultatExpression(typeof(int), false));
                    ActionOuvrirFichier.Process.AddVariable(m_variableNew);
					variable = m_variableNew;
					FillListeVariables();
					m_comboBoxVariables.SelectedValue = m_variableNew;
				}
				ActionOuvrirFichier.VariableResultat = variable;
			}
			else
				ActionOuvrirFichier.VariableResultat = null;

			ActionOuvrirFichier.WaitForExit = m_chkWaitForExit.Checked;
			ActionOuvrirFichier.SurServeur = m_chkSurServeur.Checked;

			return result;
		}


        //--------------------------------------------------------------------------------
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
            m_txtFormuleFileName.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
            m_txtFormuleArguments.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);

			FillListeVariables();
			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = true;

			if ( ActionOuvrirFichier.FormuleFichier != null )
				m_txtFormuleFileName.Text = ActionOuvrirFichier.FormuleFichier.GetString();
			
            if (ActionOuvrirFichier.FormuleArguments != null)
                m_txtFormuleArguments.Text = ActionOuvrirFichier.FormuleArguments.GetString();
			m_chkWaitForExit.Checked = ActionOuvrirFichier.WaitForExit;
			m_chkSurServeur.Checked = ActionOuvrirFichier.SurServeur;
			m_comboBoxVariables.SelectedValue = ActionOuvrirFichier.VariableResultat;
		}

        //--------------------------------------------------------------------------------
        private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
            if (m_txtFormule != null)
    			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

        //--------------------------------------------------------------------------------
        private void CFormEditActionOuvrirFichier_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

        
        //--------------------------------------------------------------------------------
        private CControleEditeFormule m_txtFormule = null;
        void m_txtFormule_Enter(object sender, EventArgs e)
        {
            if (m_txtFormule != null)
                m_txtFormule.BackColor = Color.White;
            if (sender is CControleEditeFormule)
            {
                m_txtFormule = (CControleEditeFormule)sender;
                m_txtFormule.BackColor = Color.LightGreen;
            }
            
        }

		/// //////////////////////////////////////////
		protected void FillListeVariables()
		{
			ArrayList lstVariables = new ArrayList();
			CTypeResultatExpression typeResultat = new CTypeResultatExpression(typeof(int), false);
			foreach (IVariableDynamique variable in ActionOuvrirFichier.Process.ListeVariables)
			{
				if (variable.TypeDonnee.Equals(typeResultat))
					lstVariables.Add(variable);
			}
			if (m_variableNew != null)
				lstVariables.Add(m_variableNew);
			lstVariables.Sort();
			m_comboBoxVariables.ProprieteAffichee = "Nom";
			m_comboBoxVariables.ListDonnees = lstVariables;
		}

        private void m_lnkNouvelleVariable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strNom = "";
            CTypeResultatExpression type = new CTypeResultatExpression(typeof(int), false);
            CDbKey dbKey = null;
            if (CFormEditNomVariable.EditeNomVariable(ref strNom, ref type, ref dbKey, false))
            {
                if (m_variableNew == null)
                {
                    m_variableNew = new CVariableProcessTypeComplexe();
                    m_variableNew.SetTypeDonnee(type);
                }
                m_variableNew.Nom = strNom;
                FillListeVariables();
                m_comboBoxVariables.SelectedValue = m_variableNew;
            }
        }

	}
}

