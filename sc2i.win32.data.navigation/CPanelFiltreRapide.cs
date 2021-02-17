using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.win32.data;
using sc2i.win32.data.navigation;
using sc2i.win32.navigation;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CPanelFiltreRechercheArticle.
	/// </summary>
	public class CPanelFiltreRapide : System.Windows.Forms.UserControl, IControlDefinitionFiltre
	{
		private CFiltreData m_filtre;
		private System.Windows.Forms.TextBox m_txtRecherche;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_btnOK;

		private CFiltreData m_filtreRapide = null;


		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public event EventHandler OnAppliqueFiltre;
		//-------------------------------------------------------------------
		public CPanelFiltreRapide( CFiltreData filtreRapide, string strQuickSearchText )
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm
			m_filtreRapide = filtreRapide;
			QuickSearchText = strQuickSearchText;

		}
		//-------------------------------------------------------------------
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
		//-------------------------------------------------------------------
		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_txtRecherche = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.m_btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_txtRecherche
			// 
			this.m_txtRecherche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtRecherche.Location = new System.Drawing.Point(88, 4);
			this.m_txtRecherche.Name = "m_txtRecherche";
			this.m_txtRecherche.Size = new System.Drawing.Size(320, 20);
			this.m_txtRecherche.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Search |27";
			// 
			// m_btnOK
			// 
			this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnOK.Location = new System.Drawing.Point(412, 4);
			this.m_btnOK.Name = "m_btnOK";
			this.m_btnOK.Size = new System.Drawing.Size(88, 20);
			this.m_btnOK.TabIndex = 2;
			this.m_btnOK.Text = "OK|10";
			this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
			// 
			// CPanelFiltreRapide
			// 
			this.Controls.Add(this.m_btnOK);
			this.Controls.Add(this.m_txtRecherche);
			this.Controls.Add(this.label1);
			this.Name = "CPanelFiltreRapide";
			this.Size = new System.Drawing.Size(512, 32);
			this.Load += new System.EventHandler(this.CPanelFiltreRechercheArticle_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		//-------------------------------------------------------------------
		public CFiltreData Filtre
		{
			get
			{
				return m_filtre;
			}
			set
			{
				m_filtre = value;
			}
		}
		//-------------------------------------------------------------------
		public int MinHeight
		{
			get
			{
				return 32;
			}
		}
		//-------------------------------------------------------------------
		private void CPanelFiltreRechercheArticle_Load(object sender, System.EventArgs e)
		{
			sc2i.win32.common.CWin32Traducteur.Translate(this);
		}

		//-------------------------------------------------------------------
		private void m_btnOK_Click(object sender, System.EventArgs e)
		{
			AppliqueRecherche(m_txtRecherche.Text);
		}

		//-------------------------------------------------------------------
		private static void UpdateFiltre(CFiltreData filtre, string strRecherche)
		{
			strRecherche = "%" + strRecherche + "%";
			if (filtre.Parametres.Count < 1)
				filtre.Parametres.Add(strRecherche);
			else
				filtre.Parametres[0] = strRecherche;
		}

		//-------------------------------------------------------------------
		private void UpdateFiltre ( string strRecherche )
		{
			UpdateFiltre(m_filtreRapide, strRecherche );
			m_filtre = m_filtreRapide;
		}

		//-------------------------------------------------------------------
		public void AppliqueRecherche(string strRecherche)
		{
			UpdateFiltre(strRecherche);
			if (OnAppliqueFiltre != null)
				OnAppliqueFiltre(new object(), null);
		}
		//-------------------------------------------------------------------
		public bool ShouldShow()
		{
			return true;
		}

		//-------------------------------------------------------------------
		//---------------------------------------------------------------------------
		public void FillContexte ( CContexteFormNavigable ctx )
		{
			ctx ["FR_TEXTE"] = m_txtRecherche.Text;
		}

		//---------------------------------------------------------------------------
		public void InitFromContexte ( CContexteFormNavigable ctx )
		{
			m_txtRecherche.Text = (string)ctx ["FR_TEXTE"];
		}

		//---------------------------------------------------------------------------
		public CFiltreData FiltreRapide
		{
			get
			{
				return m_filtreRapide;
			}
			set
			{
				m_filtreRapide = value;
			}
		}

		public string QuickSearchText
		{
			get
			{
				return m_txtRecherche.Text;
			}
			set
			{
				m_txtRecherche.Text = value;
				UpdateFiltre(value);
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public void AffecteValeursToNewObjet ( CObjetDonnee objet )
		{
			//
		}

		/// ////////////////////////////////////////////////////////////////////
		public void AppliquerFiltre()
		{
			AppliqueRecherche(m_txtRecherche.Text);
		}

		/// ////////////////////////////////////////////////////////////////////
		public CResultAErreur SerializeFiltre ( C2iSerializer serializer )
		{
			string strText = m_txtRecherche.Text;
			serializer.TraiteString ( ref strText );
			m_txtRecherche.Text = strText;
			return CResultAErreur.True;
		}

	}
}
