using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.data;
using sc2i.win32.common;
using sc2i.common;


namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CPanelComboFiltrable.
	/// </summary>
	public class CPanelComboLinkListeObjetDonneeFiltrable : System.Windows.Forms.UserControl, IControlALockEdition
	{
		public event EventHandler ElementSelectionneChanged;

		private CFiltreData m_filtrePrincipal;
		private CListeObjetsDonnees m_listeObjets = null;
		private string m_strProprieteAffichee="";
		private Type m_typeFormEdition = null;
		private string m_strChampFiltre = "";
		private System.Windows.Forms.TextBox m_txtFiltre;
		private sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees m_combo;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelComboLinkListeObjetDonneeFiltrable()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm
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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_txtFiltre = new System.Windows.Forms.TextBox();
			this.m_combo = new sc2i.win32.data.navigation.CComboBoxLinkListeObjetsDonnees();
			this.SuspendLayout();
			// 
			// m_txtFiltre
			// 
			this.m_txtFiltre.Name = "m_txtFiltre";
			this.m_txtFiltre.Size = new System.Drawing.Size(80, 20);
			this.m_txtFiltre.TabIndex = 0;
			this.m_txtFiltre.Text = "";
			this.m_txtFiltre.TextChanged += new System.EventHandler(this.m_txtFiltre_TextChanged);
			// 
			// m_combo
			// 
			this.m_combo.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.m_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_combo.ElementSelectionne = null;
			this.m_combo.IsLink = true;
			this.m_combo.ListDonnees = null;
			this.m_combo.Location = new System.Drawing.Point(80, 0);
			this.m_combo.LockEdition = false;
			this.m_combo.Name = "m_combo";
			this.m_combo.NullAutorise = false;
			this.m_combo.ProprieteAffichee = null;
			this.m_combo.ProprieteParentListeObjets = null;
			this.m_combo.SelectionneurParent = null;
			this.m_combo.Size = new System.Drawing.Size(280, 21);
			this.m_combo.TabIndex = 1;
            this.m_combo.TextNull = I.T("(empty)|30018");
			this.m_combo.TypeFormEdition = null;
			this.m_combo.SelectedValueChanged += new System.EventHandler(this.m_combo_SelectedValueChanged);
			// 
			// CPanelComboLinkListeObjetDonneeFiltrable
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_combo,
																		  this.m_txtFiltre});
			this.Name = "CPanelComboLinkListeObjetDonneeFiltrable";
			this.Size = new System.Drawing.Size(360, 21);
			this.Load += new System.EventHandler(this.CPanelComboLinkListeObjetDonneeFiltrable_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// //////////////////////////////
		public void ResetFiltre ( )
		{
			m_txtFiltre.Text = "";
		}

		/// //////////////////////////////
		public event EventHandler OnChangeLockEdition; 
		public bool LockEdition
		{
			get
			{
				return m_combo.LockEdition;
			}
			set
			{
				m_combo.LockEdition = value;
				if ( m_combo.LockEdition )
				{
					m_txtFiltre.Visible = false;
					m_combo.Left = 0;
					m_combo.Width = ClientSize.Width;
				}
				else
				{
					m_txtFiltre.Visible = true;
					m_combo.Left = m_txtFiltre.Right;
					m_combo.Width = ClientSize.Width-m_txtFiltre.Width;
				}
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		/// //////////////////////////////
		public bool NullAutorise
		{
			get
			{
				return m_combo.NullAutorise;
			}
			set
			{
				m_combo.NullAutorise = value;
			}
		}

		/// //////////////////////////////
		public int LargeurZoneFiltre
		{
			get
			{
				return m_txtFiltre.Width;
			}
			set
			{
				m_txtFiltre.Width = value;
				m_combo.Left = m_txtFiltre.Right;
				m_combo.Width = ClientSize.Width-m_txtFiltre.Width;
			}
		}

		/// //////////////////////////////
		public string TexteNull
		{
			get
			{
				return m_combo.TextNull;
			}
			set
			{
				m_combo.TextNull = value;
			}
		}	
	
		/// //////////////////////////////
		public bool IsLink
		{
			get
			{
				return m_combo.IsLink;
			}
			set
			{
				m_combo.IsLink = value;
			}
		}

			/// //////////////////////////////
			public CFiltreData FiltrePrincipal
		{
			get
			{
				return m_filtrePrincipal;
			}
			set
			{
				m_filtrePrincipal = value;
			}
		}

		/// //////////////////////////////
		///
		public CResultAErreur Init ( 
			CListeObjetsDonnees liste, 
			string strProprieteAffiche, 
			string strChampFiltre,
			Type typeFormEditionStandard
			 )
		{
			m_filtrePrincipal = liste.Filtre;
			m_listeObjets = liste;
			m_listeObjets.AssureLectureFaite();
			m_listeObjets.InterditLectureInDB = true;
			m_strProprieteAffichee = strProprieteAffiche;
			m_strChampFiltre = strChampFiltre;
			m_typeFormEdition = typeFormEditionStandard;
			
			return m_combo.Init ( liste, strProprieteAffiche, typeFormEditionStandard, true );
		}

		/// //////////////////////////////
		public CObjetDonnee ElementSelectionne
		{
			get
			{
				return m_combo.ElementSelectionne;
			}
			set
			{
				m_combo.ElementSelectionne = value;
			}
		}


		/// //////////////////////////////
		public void RefillListe()
		{
			if ( DesignMode )
				return;
			CObjetDonnee objSel = m_combo.ElementSelectionne;
			CFiltreData filtre;
			if ( m_filtrePrincipal != null )
				filtre = m_filtrePrincipal.GetClone();
			else
				filtre = new CFiltreData();
			if ( m_txtFiltre.Text.Trim() != "" )
			{
				if ( filtre.HasFiltre )
					filtre.Filtre = "("+filtre.Filtre+") and ";
				filtre.Filtre +=m_strChampFiltre+" like @"+(filtre.Parametres.Count+1);
				filtre.Parametres.Add ( m_txtFiltre.Text.Trim()+"%" );
			}
			m_listeObjets.Filtre = filtre;
			m_combo.Init (
				m_listeObjets,
				m_strProprieteAffichee,
				m_typeFormEdition, true );
			m_combo.SelectedValue = objSel;
			m_combo.AssureRemplissage();
			if ( m_combo.SelectedValue == null && (m_listeObjets.Count != 0 || NullAutorise))
			{
				if ( NullAutorise && m_combo.Items.Count > 1 )
					m_combo.SelectedIndex = 1;
				else
					m_combo.SelectedIndex = 0;
			}
		}

		/// //////////////////////////////
		private void m_txtFiltre_TextChanged(object sender, System.EventArgs e)
		{
			if ( !DesignMode )
				RefillListe();
		}

		private void CPanelComboLinkListeObjetDonneeFiltrable_Load(object sender, System.EventArgs e)
		{
		
		}

		
		private void m_combo_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if ( ElementSelectionneChanged != null )
				ElementSelectionneChanged ( this, e );
		}

	}
}
