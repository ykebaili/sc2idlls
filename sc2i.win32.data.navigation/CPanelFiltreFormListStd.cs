using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.win32.data;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.win32.navigation;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CPanelFiltreFormListStd.
	/// </summary>
	public class CPanelFiltreFormListStd : System.Windows.Forms.UserControl, IControlDefinitionFiltre
	{
		private bool m_bAfficheInitialise = false;
		private CFiltreData m_filtreData = null;
		private CFiltreDynamique m_filtre = null;
		private sc2i.formulaire.win32.editor.CPanelFormulaireSurElement m_panelFiltre;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelFiltreFormListStd()
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
			this.m_panelFiltre = new sc2i.formulaire.win32.editor.CPanelFormulaireSurElement();
			this.SuspendLayout();
			// 
			// m_panelFiltre
			// 
			this.m_panelFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_panelFiltre.AutoScroll = true;
			this.m_panelFiltre.ElementEdite = null;
			this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
			this.m_panelFiltre.LockEdition = false;
			this.m_panelFiltre.Name = "m_panelFiltre";
			this.m_panelFiltre.Size = new System.Drawing.Size(584, 312);
			this.m_panelFiltre.TabIndex = 0;
			// 
			// CPanelFiltreFormListStd
			// 
			this.Controls.Add(this.m_panelFiltre);
			this.Name = "CPanelFiltreFormListStd";
			this.Size = new System.Drawing.Size(584, 312);
			this.VisibleChanged += new System.EventHandler(this.CPanelFiltreFormListStd_VisibleChanged);
			this.ResumeLayout(false);

		}
		#endregion


		/*//Retourne faux si le panel ne contient pas de filtres
		public bool InitPanel(Type typeFiltre)
		{
			InitMenuFiltres();
			if ( m_listeFiltresPossibles.Count == 0 )
				return false;
			m_filtre = ((CFiltreDynamiqueInDb)m_listeFiltresPossibles[0]).Filtre;
			AfficheFiltreCourant();
			return true;
		}*/

		/// ////////////////////////////////////////////////////////////////////
		public bool ShouldShow()
		{
			return true;
		}

		/// ////////////////////////////////////////////////////////////////////
		public void SetFiltreDynamique ( CFiltreDynamique filtre )
		{
			m_filtre = filtre;
			if (Visible)
				AfficheFiltreCourant();
			else
				m_bAfficheInitialise = false;
		}


		/// ////////////////////////////////////////////////////////////////////
		public CFiltreDynamique GetFiltreDynamique()
		{
			return m_filtre;
		}



		/// ////////////////////////////////////////////////////////////////////
		private void AfficheFiltreCourant()
		{
			m_bAfficheInitialise = true;
            if (m_filtre != null)
            {
                m_panelFiltre.InitPanel(m_filtre.FormulaireEdition, m_filtre);
                if (m_filtre.FormulaireEdition != null)
                {
                    if (OnChangeDesiredHeight != null)
                        OnChangeDesiredHeight(m_filtre.FormulaireEdition.Size.Height);
                }
            }
		}

        /// ////////////////////////////////////////////////////////////////////
        public delegate void OnChangeDesiredHeightDelegate(int nHeight);

        public event OnChangeDesiredHeightDelegate OnChangeDesiredHeight;


		/// ////////////////////////////////////////////////////////////////////
		public event EventHandler OnAppliqueFiltre;

		/// ////////////////////////////////////////////////////////////////////
		public CFiltreData	Filtre
		{
			get
			{
				return m_filtreData;
			}
			set
			{
                m_filtreData = value;				
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public int MinHeight
		{
			get
			{
				return 80;
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public void FillContexte ( CContexteFormNavigable ctx )
		{
		}

		/// ////////////////////////////////////////////////////////////////////
		public void InitFromContexte ( CContexteFormNavigable ctx )
		{
		}


		/// ////////////////////////////////////////////////////////////////////
		public void AppliquerFiltre( )
		{
			if ( m_filtre == null )
				return;
			CResultAErreur result = m_panelFiltre.AffecteValeursToElement();
			if (result)
			{
				try
				{
					result = m_filtre.GetFiltreData();
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
				}
			}
			if ( !result )
			{
				CFormAlerte.Afficher( result);
				return;
			}
			m_filtreData = (CFiltreData)result.Data;
			if ( OnAppliqueFiltre != null )
			{
				OnAppliqueFiltre ( this, new EventArgs() );
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public void AffecteValeursToNewObjet ( CObjetDonnee objet )
		{
			//Ne peut rien faire !
		}


		/// ////////////////////////////////////////////////////////////////////
		public CResultAErreur SerializeFiltre ( C2iSerializer serializer )
		{
			CResultAErreur result = CResultAErreur.True;
			I2iSerializable filtre = m_filtre;
			result = serializer.TraiteObject ( ref filtre, CSc2iWin32DataClient.ContexteCourant );
			SetFiltreDynamique ( (CFiltreDynamique)filtre );
			return result;

		}

		private void CPanelFiltreFormListStd_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible && !m_bAfficheInitialise)
				AfficheFiltreCourant();
		}


	}
}
