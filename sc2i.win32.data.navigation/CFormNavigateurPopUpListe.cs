using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using sc2i.win32.data.navigation;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.data.dynamic;
using sc2i.win32.common;
using sc2i.common;
using sc2i.win32.data.dynamic;
using System.Threading;

namespace sc2i.win32.data.navigation
{
    [AutoExec("Autoexec")]
	public class CFormNavigateurPopupListe : sc2i.win32.data.navigation.CFormNavigateurPopup
	{
		private CObjetDonnee m_objetSelectionne = null;
		private System.ComponentModel.IContainer components = null;

		public CFormNavigateurPopupListe()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}

		public CFormNavigateurPopupListe ( IFormNavigable pageAccueil )
			:base(pageAccueil)
		{
			InitializeComponent();
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

		#region Code généré par le concepteur
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // m_panelForMainTabs
            // 
            this.m_panelForMainTabs.Location = new System.Drawing.Point(0, 40);
            this.m_panelForMainTabs.Size = new System.Drawing.Size(632, 406);
            // 
            // CFormNavigateurPopupListe
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Name = "CFormNavigateurPopupListe";
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CFormNavigateurPopupListe_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CFormNavigateurPopupListe_Closing);
            this.ResumeLayout(false);

		}
		#endregion
		/// //////////////////////////////////////////
		private void OnDoubleClickFormList ( object sender, EventArgs args )
		{
			DialogResult = DialogResult.OK;
			if ( PageAccueil is CFormListeStandard)
				m_objetSelectionne = ((CFormListeStandard)PageAccueil).ObjetDoubleClicked;
			Close();
		}

		/// //////////////////////////////////////////
		private void AfterValideCreationObjet ( object sender, CObjetDonneeEventArgs data )
		{
			DialogResult = DialogResult.OK;
			m_objetSelectionne = data.Objet;
			Close();
		}

		//---------------------------------------------------------------------------
		public static new void Show( IFormNavigable frm )
		{
			Show ( frm, typeof(CFormNavigateurPopupListe) );
		}

        public static void Autoexec()
        {
            C2iTextBoxFiltreRapide.SetGlobalSelectObjectDelegate(new SelectObjectFiltreRapideDelegate(MySelectionneurObjetRapide));
        }


        private static CFormListeStandard m_formForThread = null;
        private static string m_strTextRechercheRapideForThread = "";
        private static CObjetDonnee m_objetSelectedByThread = null;
        private static void SelectOnSTAThread()
        {
            m_objetSelectedByThread = SelectObjectQuickSearch(
                m_formForThread,
                null,
                m_strTextRechercheRapideForThread,
                "QUICKSEARCH");
        }



        //---------------------------------------------------------------------------
        /// <summary>
        /// Remplace la fonction de sélection dans C2iTextBoxFiltreRapide
        /// </summary>
        /// <param name="typeObjets"></param>
        /// <param name="filtreRapide"></param>
        /// <param name="strRechercheRapide"></param>
        /// <param name="bSelectionPriseEnCharge"></param>
        /// <returns></returns>
        public static CObjetDonnee MySelectionneurObjetRapide(
            Type typeObjets,
            CFiltreData filtreDeBase,
            string strRechercheRapide,
            ref bool bSelectionPriseEnCharge)
        {
            if (bSelectionPriseEnCharge)
                return null;
            Type typeForm = CFormFinder.GetTypeFormToList(typeObjets);
            bSelectionPriseEnCharge = typeForm != null && typeof(CFormListeStandard).IsAssignableFrom(typeForm);
            if (!bSelectionPriseEnCharge)
                return null;
            CFormListeStandard frmListe = (CFormListeStandard)Activator.CreateInstance(typeForm, null);
            frmListe.FiltreDeBase = filtreDeBase;
            frmListe.FiltreRapide = CFournisseurFiltreRapide.GetFiltreRapideForType(typeObjets);
            if (frmListe.ListeObjets != null)
                frmListe.ListeObjets.FiltrePrincipal = filtreDeBase;

            Thread th = new Thread(new ThreadStart(SelectOnSTAThread));
            th.SetApartmentState(ApartmentState.STA);
            m_formForThread = frmListe;
            m_strTextRechercheRapideForThread = strRechercheRapide;
            m_objetSelectedByThread = null;
            th.Start();
            th.Join();
            return m_objetSelectedByThread;
        }

		public static CObjetDonnee SelectObject ( 
			CFormListeStandard formListe, 
			CObjetDonnee objetPreSelectionnee,
			string strContexteUtilisation)
		{
			return SelectObjectQuickSearch ( formListe, objetPreSelectionnee, null, strContexteUtilisation );
		}

		public static CObjetDonnee SelectObjectQuickSearch ( CFormListeStandard formListe, CObjetDonnee objetPreSelectionnee, string strTextQuick, string strContexteUtilisation )
		{
			return SelectObjectQuickSearch (
				formListe,
				objetPreSelectionnee,
				strTextQuick,
				strContexteUtilisation,
				null );
		}
		public static CObjetDonnee SelectObjectQuickSearch ( 
			CFormListeStandard formListe, 
			CObjetDonnee objetPreSelectionnee, 
			string strTextQuick, 
			string strContexteUtilisation,
			OnNewObjetDonneeEventHandler onNewObjetDonnee )
		{
			if ( strTextQuick != null )
			{
				formListe.ModeQuickSearch = true;
				formListe.QuickSearchText = strTextQuick;
			}

            CObjetDonnee objetUnique = formListe.GetObjetQuickSearchSiUnique();

			/*CObjetDonnee objetUnique = CPanelFiltreRapide.GetObjetSiUnique(
				formListe.ListeObjets,
				formListe.FiltreRapide,
				strTextQuick);*/
			if (objetUnique != null)
			{
				formListe.Dispose();
				return objetUnique;
			}
			
			formListe.ContexteUtilisation = strContexteUtilisation;
			
            CFormNavigateur oldNavigateur = CSc2iWin32DataNavigation.Navigateur;
			
            CFormNavigateurPopupListe navigateur = new CFormNavigateurPopupListe( formListe);
            CSc2iWin32DataNavigation.PushNavigateur(navigateur);
            
            formListe.ModeSelection = true;
			formListe.OnObjetDoubleClicked += new EventHandler ( navigateur.OnDoubleClickFormList );
			formListe.AfterValideCreationObjet += new ObjetDonneeEventHandler ( navigateur.AfterValideCreationObjet );
			formListe.OnNewObjetDonnee += onNewObjetDonnee;
            
            DialogResult result = navigateur.ShowDialog();
            
		    
            CObjetDonnee objetSelectionne = null;
		    if ( result == DialogResult.OK )
		    {
			    objetSelectionne = navigateur.m_objetSelectionne;
		    }
            CSc2iWin32DataNavigation.PopNavigateur();
		    return objetSelectionne;
		}

		private void CFormNavigateurPopupListe_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
		}

		/// <summary>
		/// Calcule un contexte d'utilisation à partir du type de formulaire et du nom du controle
		/// passé en paramètre
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		public static string CalculeContexteUtilisation ( Control ctrl )
		{
			if ( ctrl == null )
				return "";
			string strContexte = "";
			Form frm = ctrl.FindForm ( );
			if ( frm != null )
				strContexte = frm.GetType().ToString()+"_";
			strContexte += ctrl.Name;
			return strContexte;
		}

		private void CFormNavigateurPopupListe_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				if ( PageAccueil != null && PageAccueil is Form)
					((Form)PageAccueil).Close();
			}
			catch{}
		}
	}
}

