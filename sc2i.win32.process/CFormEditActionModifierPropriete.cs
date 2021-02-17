using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionModifierPropriete : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormule;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.CComboboxAutoFilled m_comboVariables;
		private sc2i.win32.data.dynamic.CControlSelectDefinitionPropriete m_comboChamp;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionModifierPropriete()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionModifierPropriete), typeof(CFormEditActionModifierPropriete));
		}


		public CActionModifierPropriete ActionModifier
		{
			get
			{
				return (CActionModifierPropriete)ObjetEdite;
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
            this.m_txtFormule = new sc2i.win32.expression.CControleEditeFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_comboVariables = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_comboChamp = new sc2i.win32.data.dynamic.CControlSelectDefinitionPropriete();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_txtFormule);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 296);
            this.panel2.TabIndex = 2;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormule.BackColor = System.Drawing.Color.White;
            this.m_txtFormule.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(8, 24);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(392, 268);
            this.m_txtFormule.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Value|146";
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(408, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 296);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Object to modify|144";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Property|145";
            // 
            // m_comboVariables
            // 
            this.m_comboVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comboVariables.IsLink = false;
            this.m_comboVariables.ListDonnees = null;
            this.m_comboVariables.Location = new System.Drawing.Point(120, 8);
            this.m_comboVariables.LockEdition = false;
            this.m_comboVariables.Name = "m_comboVariables";
            this.m_comboVariables.NullAutorise = false;
            this.m_comboVariables.ProprieteAffichee = null;
            this.m_comboVariables.Size = new System.Drawing.Size(416, 21);
            this.m_comboVariables.TabIndex = 5;
            this.m_comboVariables.TextNull = I.T("(to affect)|30017");
            this.m_comboVariables.Tri = true;
            this.m_comboVariables.SelectedValueChanged += new System.EventHandler(this.m_comboVariables_SelectedValueChanged);
            // 
            // m_comboChamp
            // 
            this.m_comboChamp.DefinitionSelectionnee = null;
            this.m_comboChamp.Location = new System.Drawing.Point(120, 32);
            this.m_comboChamp.Name = "m_comboChamp";
            this.m_comboChamp.Size = new System.Drawing.Size(416, 21);
            this.m_comboChamp.TabIndex = 6;
            // 
            // CFormEditActionModifierPropriete
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(584, 398);
            this.Controls.Add(this.m_comboChamp);
            this.Controls.Add(this.m_comboVariables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionModifierPropriete";
            this.Text = "Property modification|143";
            this.Load += new System.EventHandler(this.CFormEditActionModifierPropriete_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.m_comboVariables, 0);
            this.Controls.SetChildIndex(this.m_comboChamp, 0);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			CContexteAnalyse2iExpression contexte = new CContexteAnalyse2iExpression ( ObjetEdite.Process, typeof(CProcess));
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(contexte);
			result = analyseur.AnalyseChaine ( m_txtFormule.Text );
			if ( !result )
				return result;
			ActionModifier.ExpressionValeur = (C2iExpression)result.Data;
			if ( m_comboChamp.DefinitionSelectionnee != null )
				ActionModifier.Propriete = m_comboChamp.DefinitionSelectionnee;
			else
				ActionModifier.Propriete = null;
			if ( m_comboVariables.SelectedValue is IVariableDynamique )
				ActionModifier.VariableAModifier = (CVariableDynamique)m_comboVariables.SelectedValue;
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);
			m_txtFormule.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );

			FillListeVariables();
			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = false;
			m_comboChamp.Init ( four, null,	null);
			m_comboVariables.SelectedValue = ActionModifier.VariableAModifier!=null?ActionModifier.VariableAModifier:null;
			m_comboChamp.DefinitionSelectionnee = ActionModifier.Propriete;
			if ( ActionModifier.ExpressionValeur != null )
			{
				m_txtFormule.Text = ActionModifier.ExpressionValeur.GetString();
			}
			m_comboVariables_SelectedValueChanged(this, new EventArgs());
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		/// //////////////////////////////////////////
		protected void FillListeVariables()
		{
			ArrayList lstVariables = new ArrayList();
			foreach ( CVariableDynamique variable in ActionModifier.Process.ListeVariables )
			{
				if ( !variable.TypeDonnee.IsArrayOfTypeNatif )
					lstVariables.Add ( variable );
			}
			lstVariables.Sort();
			m_comboVariables.ProprieteAffichee = "Nom";
			m_comboVariables.ListDonnees = lstVariables;
		}

		private void m_comboVariables_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if ( !(m_comboVariables.SelectedValue is CVariableDynamique) )
			{
				m_comboChamp.Enabled = false;
				return;
			}
			m_comboChamp.Enabled = true;
			CVariableDynamique variable = (CVariableDynamique)m_comboVariables.SelectedValue;
			CFournisseurPropDynStd four = new CFournisseurPropDynStd();
			four.AvecReadOnly = false;
			m_comboChamp.Init ( four, variable.TypeDonnee.TypeDotNetNatif, null );
		}

        private void CFormEditActionModifierPropriete_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }
		

		




	}
}

