using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using sc2i.common;
using sc2i.process;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CFormEditLienAction.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CFormEditActionForEach : CFormEditObjetDeProcess
	{
		private CVariableProcessTypeComplexe m_variableNew = null;
		private sc2i.win32.common.CExtLinkField m_extLinkField;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.CComboboxAutoFilled m_comboVariableListe;
		private sc2i.win32.common.CComboboxAutoFilled m_comboVariable;
		private System.Windows.Forms.LinkLabel m_lnkNouvelleVariable;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormEditActionForEach()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

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

		public static void Autoexec()
		{
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionForEach), typeof(CFormEditActionForEach));
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditActionForEach));
            this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_comboVariableListe = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_comboVariable = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_lnkNouvelleVariable = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.m_extLinkField.SetLinkField(this.label1, "");
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "List|129";
            // 
            // label2
            // 
            this.m_extLinkField.SetLinkField(this.label2, "");
            this.label2.Location = new System.Drawing.Point(8, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Iteration variable|206";
            // 
            // m_comboVariableListe
            // 
            this.m_comboVariableListe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboVariableListe.IsLink = false;
            this.m_extLinkField.SetLinkField(this.m_comboVariableListe, "");
            this.m_comboVariableListe.ListDonnees = null;
            this.m_comboVariableListe.Location = new System.Drawing.Point(120, 8);
            this.m_comboVariableListe.LockEdition = false;
            this.m_comboVariableListe.Name = "m_comboVariableListe";
            this.m_comboVariableListe.NullAutorise = false;
            this.m_comboVariableListe.ProprieteAffichee = null;
            this.m_comboVariableListe.Size = new System.Drawing.Size(288, 21);
            this.m_comboVariableListe.TabIndex = 4;
            this.m_comboVariableListe.TextNull = I.T("(to affect)|30017");
            this.m_comboVariableListe.Tri = true;
            this.m_comboVariableListe.SelectedValueChanged += new System.EventHandler(this.m_comboVariableListe_SelectedValueChanged);
            // 
            // m_comboVariable
            // 
            this.m_comboVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboVariable.IsLink = false;
            this.m_extLinkField.SetLinkField(this.m_comboVariable, "");
            this.m_comboVariable.ListDonnees = null;
            this.m_comboVariable.Location = new System.Drawing.Point(120, 32);
            this.m_comboVariable.LockEdition = false;
            this.m_comboVariable.Name = "m_comboVariable";
            this.m_comboVariable.NullAutorise = false;
            this.m_comboVariable.ProprieteAffichee = null;
            this.m_comboVariable.Size = new System.Drawing.Size(288, 21);
            this.m_comboVariable.TabIndex = 5;
            this.m_comboVariable.TextNull = I.T("(to affect)|30017");
            this.m_comboVariable.Tri = true;
            // 
            // m_lnkNouvelleVariable
            // 
            this.m_extLinkField.SetLinkField(this.m_lnkNouvelleVariable, "");
            this.m_lnkNouvelleVariable.Location = new System.Drawing.Point(416, 34);
            this.m_lnkNouvelleVariable.Name = "m_lnkNouvelleVariable";
            this.m_lnkNouvelleVariable.Size = new System.Drawing.Size(120, 16);
            this.m_lnkNouvelleVariable.TabIndex = 6;
            this.m_lnkNouvelleVariable.TabStop = true;
            this.m_lnkNouvelleVariable.Text = "New variable|127";
            this.m_lnkNouvelleVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkNouvelleVariable_LinkClicked);
            // 
            // CFormEditActionForEach
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(536, 110);
            this.Controls.Add(this.m_lnkNouvelleVariable);
            this.Controls.Add(this.m_comboVariable);
            this.Controls.Add(this.m_comboVariableListe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.m_extLinkField.SetLinkField(this, "");
            this.Name = "CFormEditActionForEach";
            this.Text = "List iterating properties|128";
            this.Load += new System.EventHandler(this.CFormEditLienAction_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_comboVariableListe, 0);
            this.Controls.SetChildIndex(this.m_comboVariable, 0);
            this.Controls.SetChildIndex(this.m_lnkNouvelleVariable, 0);
            this.ResumeLayout(false);

		}
		#endregion

		private void CFormEditLienAction_Load(object sender, System.EventArgs e)
		{
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

            if ( DesignMode)
				return;
			InitChamps();
			
		}

		/// ////////////////////////////////////////
		public CActionForEach ActionForEach
		{
			get
			{
				return (CActionForEach)ObjetEdite;
			}
		}
		/// ////////////////////////////////////////
		protected override void InitChamps()
		{
			FillListeVariablesListe();
			FillListeVariables();
			m_comboVariableListe.SelectedValue = ActionForEach.VariableListe;
			m_comboVariable.SelectedValue = ActionForEach.VariableElementEnCours;
		}

		/// ////////////////////////////////////////
        private void m_lnkNouvelleVariable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string strNom = "";
            if (!(m_comboVariableListe.SelectedValue is CVariableDynamique))
            {
                CFormAlerte.Afficher(I.T("Select list variable first|30055"), EFormAlerteType.Exclamation);
                return;
            }
            CVariableDynamique variable = (CVariableDynamique)m_comboVariableListe.SelectedValue;
            CTypeResultatExpression type = variable.TypeDonnee.GetTypeElements();
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
                m_comboVariable.SelectedValue = m_variableNew;
            }
        }
	

		/// //////////////////////////////////////////
		protected void FillListeVariablesListe()
		{
			ArrayList lstVariables = new ArrayList();
			foreach ( CVariableDynamique variable in ActionForEach.Process.ListeVariables )
			{
				if ( variable.TypeDonnee.IsArrayOfTypeNatif || typeof(IEnumerable).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif))
					lstVariables.Add ( variable );
			}
			if ( m_variableNew != null )
				lstVariables.Add ( m_variableNew );
			lstVariables.Sort();
			m_comboVariableListe.ProprieteAffichee = "Nom";
			m_comboVariableListe.ListDonnees = lstVariables;
		}

		/// //////////////////////////////////////////
		protected void FillListeVariables()
		{
			if ( !(m_comboVariableListe.SelectedValue is CVariableDynamique ) )
			{
				m_comboVariable.Enabled = false;
				return;
			}
			m_comboVariable.Enabled = true;
			CVariableDynamique varListe = (CVariableDynamique)m_comboVariableListe.SelectedValue;
			ArrayList lstVariables = new ArrayList();
			foreach ( CVariableDynamique variable in ObjetEdite.Process.ListeVariables )
			{
				if ( variable.TypeDonnee.Equals	( varListe.TypeDonnee.GetTypeElements()) )
					lstVariables.Add ( variable );
			}
			if ( m_variableNew != null )
				lstVariables.Add ( m_variableNew );
			lstVariables.Sort();
			m_comboVariable.ProprieteAffichee = "Nom";
			m_comboVariable.ListDonnees = lstVariables;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			if ( !result )
				return result;
			if ( !(m_comboVariableListe.SelectedValue is CVariableDynamique) )
			{
				result.EmpileErreur(I.T("Select the list to browse|30042"));
				return result;
			}
			ActionForEach.VariableListe = (CVariableDynamique)m_comboVariableListe.SelectedValue;
			if ( !(m_comboVariable.SelectedValue is CVariableDynamique) )
			{
				result.EmpileErreur(I.T("Select the browsing variable|30043"));
				return result;
			}
			CVariableDynamique variable = (CVariableDynamique)m_comboVariable.SelectedValue;
            if (variable.IdVariable == "")
            {
                m_variableNew = new CVariableProcessTypeComplexe(ActionForEach.Process);
                m_variableNew.Nom = variable.Nom;
                m_variableNew.SetTypeDonnee(ActionForEach.VariableListe.TypeDonnee.GetTypeElements());
                ActionForEach.Process.AddVariable(m_variableNew);
                variable = m_variableNew;
            }
			ActionForEach.VariableElementEnCours = variable;
			return result;
		}

		/// //////////////////////////////////////////
		private void m_comboVariableListe_SelectedValueChanged(object sender, System.EventArgs e)
		{
			FillListeVariables();
		}

		

		
	}
}
