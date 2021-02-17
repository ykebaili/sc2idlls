using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.expression;
using sc2i.win32.common;


namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionLancerActionServeur : sc2i.win32.process.CFormEditObjetDeProcess
	{
		private CControleEditeFormule m_txtFormule = null;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.C2iComboBox m_cmbAction;
		private Panel panel2;
		private Panel m_panelFormules;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionLancerActionServeur()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionLancerActionServeur), typeof(CFormEditActionLancerActionServeur));
		}


		public CActionLancerActionServeur ActionLancer
		{
			get
			{
				return (CActionLancerActionServeur)ObjetEdite;
			}
		}

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbAction = new sc2i.win32.common.C2iComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_panelFormules = new System.Windows.Forms.Panel();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action to start|132";
            // 
            // m_cmbAction
            // 
            this.m_cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAction.IsLink = false;
            this.m_cmbAction.Location = new System.Drawing.Point(120, 8);
            this.m_cmbAction.LockEdition = false;
            this.m_cmbAction.Name = "m_cmbAction";
            this.m_cmbAction.Size = new System.Drawing.Size(456, 21);
            this.m_cmbAction.TabIndex = 4;
            this.m_cmbAction.SelectedValueChanged += new System.EventHandler(this.m_cmbAction_SelectedValueChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_panelFormules);
            this.panel2.Controls.Add(this.m_wndAideFormule);
            this.panel2.Location = new System.Drawing.Point(3, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(636, 361);
            this.panel2.TabIndex = 5;
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Location = new System.Drawing.Point(16, 3);
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(444, 358);
            this.m_panelFormules.TabIndex = 5;
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(460, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(176, 361);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.ObjetInterroge = null;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // CFormEditActionLancerActionServeur
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(642, 448);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_cmbAction);
            this.Controls.Add(this.label2);
            this.Name = "CFormEditActionLancerActionServeur";
            this.Text = "Start an action on the server|131";
            this.Load += new System.EventHandler(this.CFormEditActionLancerActionServeur_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_cmbAction, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			if ( !(m_cmbAction.SelectedValue is string) )
			{
				result.EmpileErreur(I.T("Select an action|30014"));
				return result;
			}
			ActionLancer.CodeAction = (string)m_cmbAction.SelectedValue;

			ActionLancer.ResetFormules();

			foreach (CFormuleForParametre formule in m_listeExpressions)
			{
				result = formule.UpdateFromEditeur();
				if (result)
				{
					C2iExpression expression = formule.Formule;
					if (expression != null)
						ActionLancer.SetFormuleForParametre(formule.NomParametre, formule.Formule);
				}
				else
				{
					result.EmpileErreur(I.T("Error in the formula @1|30013", formule.NomConvivial));
					return result;
				}
			}

			
			return result;
		}

		
		/// //////////////////////////////////////////
		protected override void InitChamps()
		{
			base.InitChamps ();
			m_cmbAction.DataSource = CActionLancerActionServeur.GetListeActionsPossibles(ActionLancer.Process.IdSession);
			m_cmbAction.DisplayMember = "Libelle";
			m_cmbAction.ValueMember = "Code";
			try
			{
				m_cmbAction.SelectedValue = ActionLancer.CodeAction;
			}
			catch{}

			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);

			FillListeChamps();
		}

        private void CFormEditActionLancerActionServeur_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }


		private class CFormuleForParametre : IComparable
		{
			public readonly string NomParametre = "";
			private C2iExpression m_expression;
			private string m_strNomConvivial = "";
			private CEditeurFormuleNommee m_editeur;

			public CFormuleForParametre(string strNomParametre, string strNomConvivial)
			{
				m_strNomConvivial = strNomConvivial;
				NomParametre = strNomParametre;
			}

			public C2iExpression Formule
			{
				get
				{
					return m_expression;
				}
				set
				{
					m_expression = value;
				}
			}

			public string NomConvivial
			{
				get
				{
					return m_strNomConvivial;
				}
			}

			public CEditeurFormuleNommee Editeur
			{
				get
				{
					return m_editeur;
				}
				set
				{
					m_editeur = value;
				}
			}

			public CResultAErreur UpdateFromEditeur()
			{
				CResultAErreur result = m_editeur.ResultAnalyse;
				if (result)
					Formule = m_editeur.Formule;
				return result;
			}
			#region Membres de IComparable

			public int CompareTo(object obj)
			{
				try
				{
					return NomConvivial.CompareTo(((CFormuleForParametre)obj).NomConvivial);
				}
				catch
				{
					return -1;
				}
			}

			#endregion


		}

		/// //////////////////////////////////////////
		private ArrayList m_reserveEditeurs = new ArrayList();

		/// //////////////////////////////////////////
		private ArrayList m_listeExpressions = new ArrayList();
		private void FillListeChamps()
		{
			m_panelFormules.SuspendDrawing();
			foreach (Control ctrl in m_panelFormules.Controls)
			{
				if (ctrl is CEditeurFormuleNommee)
				{
					ctrl.Visible = false;
					m_reserveEditeurs.Add(ctrl);
				}
			}
			ArrayList lst = new ArrayList();

			if (m_cmbAction.SelectedValue is string)
			{
				string strCode = (string)m_cmbAction.SelectedValue;
				CInfoActionServeur info = null;
				foreach ( CInfoActionServeur infoTmp  in CActionLancerActionServeur.GetListeActionsPossibles(ActionLancer.Process.IdSession) )
					if (infoTmp.Code == strCode)
					{
						info = infoTmp;
						break;
					}
				if (info != null)
				{
					foreach (string strNomParametre in info.NomsParametres)
					{
						CFormuleForParametre formule = new CFormuleForParametre(strNomParametre, strNomParametre);
						formule.Formule = ActionLancer.GetFormuleForParametre(strNomParametre);
						lst.Add(formule);
					}
				}
			}
			lst.Sort();
			m_listeExpressions = lst;
			int nY = 0;

			foreach (CFormuleForParametre formule in lst)
			{
				CEditeurFormuleNommee editeur = null;
				if (m_reserveEditeurs.Count > 0)
				{
					editeur = (CEditeurFormuleNommee)m_reserveEditeurs[0];
					m_reserveEditeurs.Remove(editeur);
				}
				else
				{
					editeur = new CEditeurFormuleNommee();
					editeur.Parent = m_panelFormules;
				}
				editeur.Visible = true;
				editeur.Width = m_panelFormules.ClientRectangle.Width;
				editeur.Location = new Point(0, nY);
				formule.Editeur = editeur;
				editeur.TextFormule.Enter += new EventHandler(OnEnterZoneFormule);
				editeur.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
				editeur.Libelle = formule.NomConvivial;
				editeur.TabIndex = nY;
				nY += editeur.Size.Height + 1;
				editeur.Formule = formule.Formule;
			}
			m_panelFormules.ResumeDrawing();
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if (m_txtFormule != null)
				m_wndAideFormule.InsereInTextBox(m_txtFormule, nPosCurseur, strCommande);
		}


		/// //////////////////////////////////////////
		private void OnEnterZoneFormule(object sender, EventArgs args)
		{
			if (sender is CControleEditeFormule)
			{
				if (m_txtFormule != null)
					m_txtFormule.BackColor = Color.White;
				m_txtFormule = (CControleEditeFormule)sender;
				m_txtFormule.BackColor = Color.LightGreen;
			}
		}

		/// //////////////////////////////////////////
		private string GetStringExpression(object elementInterroge, object objet)
		{
			if (objet == null)
				return "";
			return ((C2iExpression)objet).GetString();
		}

		private void m_cmbAction_SelectedValueChanged(object sender, EventArgs e)
		{
			FillListeChamps();
		}
	




	}
}

