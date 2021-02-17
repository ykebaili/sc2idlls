using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.win32.process
{
	public class CFormEditActionFonction : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private CVariableProcessTypeComplexe m_variableNew = null;
		private System.Windows.Forms.Panel panel2;
		protected System.Windows.Forms.Label m_lblStockerResIn;
		private sc2i.win32.common.CComboboxAutoFilled m_comboBoxVariables;
		private System.Windows.Forms.LinkLabel m_lnkNouvelleVariable;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionFonction()
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

		public CActionFonction ActionFonction
		{
			get
			{
				return (CActionFonction)ObjetEdite;
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
            this.m_lnkNouvelleVariable = new System.Windows.Forms.LinkLabel();
            this.m_comboBoxVariables = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_lblStockerResIn = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lnkNouvelleVariable);
            this.panel2.Controls.Add(this.m_comboBoxVariables);
            this.panel2.Controls.Add(this.m_lblStockerResIn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 32);
            this.panel2.TabIndex = 2;
            // 
            // m_lnkNouvelleVariable
            // 
            this.m_lnkNouvelleVariable.Location = new System.Drawing.Point(392, 9);
            this.m_lnkNouvelleVariable.Name = "m_lnkNouvelleVariable";
            this.m_lnkNouvelleVariable.Size = new System.Drawing.Size(112, 16);
            this.m_lnkNouvelleVariable.TabIndex = 2;
            this.m_lnkNouvelleVariable.TabStop = true;
            this.m_lnkNouvelleVariable.Text = "New variable|127";
            this.m_lnkNouvelleVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkNouvelleVariable_LinkClicked);
            // 
            // m_comboBoxVariables
            // 
            this.m_comboBoxVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboBoxVariables.IsLink = false;
            this.m_comboBoxVariables.ListDonnees = null;
            this.m_comboBoxVariables.Location = new System.Drawing.Point(144, 7);
            this.m_comboBoxVariables.LockEdition = false;
            this.m_comboBoxVariables.Name = "m_comboBoxVariables";
            this.m_comboBoxVariables.NullAutorise = true;
            this.m_comboBoxVariables.ProprieteAffichee = null;
            this.m_comboBoxVariables.Size = new System.Drawing.Size(248, 21);
            this.m_comboBoxVariables.TabIndex = 1;
            this.m_comboBoxVariables.TextNull = I.T("(to affect)|30017");
            this.m_comboBoxVariables.Tri = true;
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Location = new System.Drawing.Point(3, 9);
            this.m_lblStockerResIn.Name = "m_lblStockerResIn";
            this.m_lblStockerResIn.Size = new System.Drawing.Size(141, 16);
            this.m_lblStockerResIn.TabIndex = 0;
            this.m_lblStockerResIn.Text = "Store the result in|125";
            // 
            // CFormEditActionFonction
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(520, 266);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionFonction";
            this.Text = "Function|126";
            this.Load += new System.EventHandler(this.CFormEditActionFonction_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	
		/// //////////////////////////////////////////
		protected void OnChangeTypeRetour()
		{
			FillListeVariables();
		}

		/// //////////////////////////////////////////
		protected void FillListeVariables()
		{
			ArrayList lstVariables = new ArrayList();
			if ( ActionFonction.TypeResultat != null )
			{
				foreach ( IVariableDynamique variable in ActionFonction.Process.ListeVariables )
				{
					if ( variable.TypeDonnee.Equals(ActionFonction.TypeResultat) )
						lstVariables.Add ( variable );
				}
			}
			if ( m_variableNew != null )
				lstVariables.Add ( m_variableNew );
			lstVariables.Sort();
			m_comboBoxVariables.ProprieteAffichee = "Nom";
			m_comboBoxVariables.ListDonnees = lstVariables;
		}

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			if ( !result )
				return result;
			if ( !(m_comboBoxVariables.SelectedValue is CVariableDynamique) && !ActionFonction.VariableRetourCanBeNull )
			{
				result.EmpileErreur(I.T("Select a variable to store the action value|30012"));
				return result;
			}
			if ( m_comboBoxVariables.SelectedValue is CVariableDynamique )
			{
				CVariableDynamique variable = (CVariableDynamique)m_comboBoxVariables.SelectedValue;
                if (variable.IdVariable == "")
                {
                    m_variableNew = new CVariableProcessTypeComplexe(ActionFonction.Process);
                    m_variableNew.Nom = variable.Nom;
                    m_variableNew.SetTypeDonnee(ActionFonction.TypeResultat);
                    ActionFonction.Process.AddVariable(m_variableNew);
                    variable = m_variableNew;
                    FillListeVariables();
                    m_comboBoxVariables.SelectedValue = m_variableNew;
                }
				ActionFonction.VariableResultat = variable;
			}
			else
				ActionFonction.VariableResultat = null;
			return result;
		}

		private void m_lnkNouvelleVariable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string strNom = "";
			CTypeResultatExpression type = ActionFonction.TypeResultat;
            CDbKey dbKey = null;
			if ( CFormEditNomVariable.EditeNomVariable ( ref strNom, ref type, ref dbKey, false) )
			{
				if ( m_variableNew == null )
				{
					m_variableNew = new CVariableProcessTypeComplexe( );
					m_variableNew.SetTypeDonnee ( type );
				}
				m_variableNew.Nom = strNom;
				FillListeVariables();
				m_comboBoxVariables.SelectedValue = m_variableNew;
			}
		}

		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			FillListeVariables();
			m_comboBoxVariables.SelectedValue = ActionFonction.VariableResultat;
		}

        private void CFormEditActionFonction_Load(object sender, EventArgs e)
        {
			this.Text = I.TT(typeof(CFormEditActionFonction), this.Text);
			m_lblStockerResIn.Text = I.TT(typeof(CFormEditActionFonction), m_lblStockerResIn.Text);
			m_lnkNouvelleVariable.Text = I.TT(typeof(CFormEditActionFonction), m_lnkNouvelleVariable.Text);
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

		




	}
}

