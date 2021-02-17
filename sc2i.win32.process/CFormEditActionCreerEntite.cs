using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using sc2i.expression;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.process;
using sc2i.data;
using sc2i.win32.common;
using sc2i.win32.expression;

namespace sc2i.win32.process
{
	[AutoExec("Autoexec")]
	public class CFormEditActionCreerEntite : sc2i.win32.process.CFormEditActionFonction
	{
		private CControleEditeFormule m_txtFormule = null;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListView m_wndListeVariables;
		private sc2i.win32.expression.CControlAideFormule m_wndAideFormule;
		private System.Windows.Forms.Label label2;
		private sc2i.win32.common.C2iComboBox m_cmbTypeEntite;
		private System.Windows.Forms.Panel m_panelFormules;
        private SplitContainer m_splitContainer;
		private System.ComponentModel.IContainer components = null;

		public CFormEditActionCreerEntite()
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
			CEditeurActionsEtLiens.RegisterEditeur ( typeof(CActionCreerEntite), typeof(CFormEditActionCreerEntite));
		}


		public CActionCreerEntite ActionCreerEntite
		{
			get
			{
				return (CActionCreerEntite)ObjetEdite;
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
            this.m_panelFormules = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbTypeEntite = new sc2i.win32.common.C2iComboBox();
            this.m_wndAideFormule = new sc2i.win32.expression.CControlAideFormule();
            this.m_wndListeVariables = new System.Windows.Forms.ListView();
            this.panel2.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblStockerResIn
            // 
            this.m_lblStockerResIn.Text = "Store result in :|112";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_splitContainer);
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(736, 288);
            this.panel2.TabIndex = 2;
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
            this.m_splitContainer.Panel1.Controls.Add(this.m_panelFormules);
            this.m_splitContainer.Panel1.Controls.Add(this.label2);
            this.m_splitContainer.Panel1.Controls.Add(this.m_cmbTypeEntite);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_wndAideFormule);
            this.m_splitContainer.Size = new System.Drawing.Size(736, 288);
            this.m_splitContainer.SplitterDistance = 536;
            this.m_splitContainer.TabIndex = 6;
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Location = new System.Drawing.Point(4, 33);
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(525, 248);
            this.m_panelFormules.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Entity type|113";
            // 
            // m_cmbTypeEntite
            // 
            this.m_cmbTypeEntite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTypeEntite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeEntite.IsLink = false;
            this.m_cmbTypeEntite.Location = new System.Drawing.Point(133, 6);
            this.m_cmbTypeEntite.LockEdition = false;
            this.m_cmbTypeEntite.Name = "m_cmbTypeEntite";
            this.m_cmbTypeEntite.Size = new System.Drawing.Size(396, 21);
            this.m_cmbTypeEntite.TabIndex = 4;
            this.m_cmbTypeEntite.SelectedValueChanged += new System.EventHandler(this.m_cmbTypeEntite_SelectedValueChanged);
            // 
            // m_wndAideFormule
            // 
            this.m_wndAideFormule.BackColor = System.Drawing.Color.White;
            this.m_wndAideFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndAideFormule.FournisseurProprietes = null;
            this.m_wndAideFormule.Location = new System.Drawing.Point(0, 0);
            this.m_wndAideFormule.Name = "m_wndAideFormule";
            this.m_wndAideFormule.SendIdChamps = false;
            this.m_wndAideFormule.Size = new System.Drawing.Size(192, 284);
            this.m_wndAideFormule.TabIndex = 0;
            this.m_wndAideFormule.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAideFormule_OnSendCommande);
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Location = new System.Drawing.Point(8, 8);
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(544, 256);
            this.m_wndListeVariables.TabIndex = 0;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            // 
            // CFormEditActionCreerEntite
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(736, 366);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditActionCreerEntite";
            this.Text = "Create|111";
            this.Load += new System.EventHandler(this.CFormEditActionCreerEntite_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// //////////////////////////////////////////
		private bool m_bInitialisationEnCours =  false;
		protected override void InitChamps()
		{
			m_bInitialisationEnCours = true;
			base.InitChamps ();
			m_wndAideFormule.FournisseurProprietes = ObjetEdite.Process;
			m_wndAideFormule.ObjetInterroge = typeof(CProcess);


			//Remplit la liste des types
			CInfoClasseDynamique[] classes = DynamicClassAttribute.GetAllDynamicClass();
			ArrayList classesAIdAuto = new ArrayList();
			foreach ( CInfoClasseDynamique classe in classes )
			{
				if ( typeof ( CObjetDonneeAIdNumeriqueAuto ).IsAssignableFrom ( classe.Classe ) )
					classesAIdAuto.Add ( classe );
			}

			classesAIdAuto.Insert(0, new CInfoClasseDynamique(typeof(DBNull), I.T("None|19")));

			m_cmbTypeEntite.DataSource = null;
			m_cmbTypeEntite.DataSource = classesAIdAuto;
			m_cmbTypeEntite.DisplayMember = "Nom";
			m_cmbTypeEntite.ValueMember = "Classe";

			if ( ActionCreerEntite.TypeEntiteACreer != null )
				m_cmbTypeEntite.SelectedValue = ActionCreerEntite.TypeEntiteACreer;

			FillListeChamps();
			m_bInitialisationEnCours = false;
		}

		private class CFormuleForPropriete : IComparable
		{
			public readonly string NomPropriete = "";
			private C2iExpression m_expression;
			private string m_strNomConvivial = "";
			private CEditeurFormuleNommee m_editeur;

			public CFormuleForPropriete ( string strNomPropriete, string strNomConvivial )
			{
				m_strNomConvivial = strNomConvivial;
				NomPropriete = strNomPropriete;
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
				CResultAErreur result =  m_editeur.ResultAnalyse;
				if ( result )
					Formule = m_editeur.Formule;
				return result;
			}
			#region Membres de IComparable

			public int CompareTo(object obj)
			{
				try
				{
					return NomConvivial.CompareTo ( ((CFormuleForPropriete)obj).NomConvivial );
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
			foreach ( Control ctrl in m_panelFormules.Controls )
			{
				if ( ctrl is CEditeurFormuleNommee )
				{
					ctrl.Visible =  false;
					m_reserveEditeurs.Add ( ctrl );
				}
			}
			ArrayList lst = new ArrayList();
			if ( m_cmbTypeEntite.SelectedValue is Type && m_cmbTypeEntite.SelectedValue != typeof(DBNull) )
			{
				Type tp = (Type)m_cmbTypeEntite.SelectedValue;
				foreach ( PropertyInfo info in tp.GetProperties() )
				{
					if ( info.GetSetMethod() != null )
					{
						object[] attrs = info.GetCustomAttributes ( typeof( DynamicFieldAttribute ), true );
						if ( attrs.Length > 0 )
						{
							DynamicFieldAttribute attr = (DynamicFieldAttribute)attrs[0];
							CFormuleForPropriete formule = new CFormuleForPropriete ( info.Name, attr.NomConvivial );
							formule.Formule = ActionCreerEntite.GetFormuleForPropriete ( info.Name );
							lst.Add ( formule );
						}
					}
				}
			}
			lst.Sort();
			m_listeExpressions = lst;
			int nY = 0;
			
			foreach ( CFormuleForPropriete formule in lst )
			{
				CEditeurFormuleNommee editeur = null;
				if ( m_reserveEditeurs.Count > 0 )
				{
					editeur = (CEditeurFormuleNommee)m_reserveEditeurs[0];
					m_reserveEditeurs.Remove ( editeur );
				}
				else
				{
					editeur = new CEditeurFormuleNommee();
					editeur.Parent = m_panelFormules;
				}
				editeur.Visible = true;
				editeur.Width = m_panelFormules.ClientRectangle.Width;
				editeur.Location = new Point ( 0, nY );
				formule.Editeur = editeur;
				editeur.TextFormule.Enter += new EventHandler ( OnEnterZoneFormule );
				editeur.Init ( m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge );
				editeur.Libelle = formule.NomConvivial;
				editeur.TabIndex = nY;
				nY += editeur.Size.Height+1;
				editeur.Formule = formule.Formule;
			}
			m_panelFormules.ResumeDrawing();
		}

		/// //////////////////////////////////////////
		private void OnEnterZoneFormule ( object sender, EventArgs args )
		{
			if ( sender is CControleEditeFormule )
			{
				if ( m_txtFormule != null )
					m_txtFormule.BackColor = Color.White;
				m_txtFormule = (CControleEditeFormule)sender;
				m_txtFormule.BackColor = Color.LightGreen;
			}
		}

		/// //////////////////////////////////////////
		private string GetStringExpression ( object elementInterroge, object objet )
		{
			if ( objet == null )
				return "";
			return ((C2iExpression)objet).GetString();
		}
	

		/// //////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = base.MAJ_Champs();
			
			ActionCreerEntite.TypeEntiteACreer = (Type)m_cmbTypeEntite.SelectedValue;
			if (ActionCreerEntite.TypeEntiteACreer == typeof(DBNull))
				ActionCreerEntite.TypeEntiteACreer = null;
			ActionCreerEntite.ResetFormules();

			foreach ( CFormuleForPropriete formule in m_listeExpressions )
			{
				result =  formule.UpdateFromEditeur();
				if ( result )
				{
					C2iExpression expression = formule.Formule;
					if ( expression != null )
						ActionCreerEntite.SetFormuleForPropriete ( formule.NomPropriete, formule.Formule );
				}
				else
				{
					result.EmpileErreur (I.T("Error in  the formula of @1|30041",formule.NomConvivial));
					return result;
				}
			}

			
			return result;
		}

		private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_txtFormule != null )
				m_wndAideFormule.InsereInTextBox ( m_txtFormule, nPosCurseur, strCommande );
		}

		private void m_cmbTypeEntite_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if ( !m_bInitialisationEnCours )
			{
				FillListeChamps();
				ActionCreerEntite.TypeEntiteACreer = (Type)m_cmbTypeEntite.SelectedValue;
				if (ActionCreerEntite.TypeEntiteACreer == typeof(DBNull))
					ActionCreerEntite.TypeEntiteACreer = null;
				OnChangeTypeRetour();
			}
		}

        private void CFormEditActionCreerEntite_Load(object sender, EventArgs e)
        {
            // Traduit le formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

		
		

		




	}
}

