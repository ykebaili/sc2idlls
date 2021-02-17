using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using System.Net;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec ( "Autoexec")]
	public class CWndFor2iWebBrowser : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
        private WebBrowser m_browser = new WebBrowser();
		//--------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndWebBrowser), typeof(CWndFor2iWebBrowser));
		}

        //--------------------------------------------
        public CWndFor2iWebBrowser()
            : base()
        {
            m_browser.Navigating += new WebBrowserNavigatingEventHandler(m_browser_Navigating);
        }

        //--------------------------------------------
        void m_browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            C2iWndWebBrowser browser = WndAssociee as C2iWndWebBrowser;
            if (browser != null && browser.PreventDownload)
            {
                try
                {
                    WebRequest req = WebRequest.Create(e.Url);
                    WebResponse res = req.GetResponse();
                    if (!res.ContentType.ToUpper().Contains("OCTET-STREAM"))
                        return;
                    e.Cancel = true;
                }
                catch
                {

                }
            }
        }

        //--------------------------------------------
        protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndWebBrowser wndBrowser = wnd as C2iWndWebBrowser;
			if (wndBrowser == null)
				return;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(wndBrowser, m_browser);
            m_browser.ScriptErrorsSuppressed = wndBrowser.HideErrors;
            m_browser.IsWebBrowserContextMenuEnabled = wndBrowser.BrowserContextMenu;
            m_browser.WebBrowserShortcutsEnabled = wndBrowser.EnableShortCuts;
            m_browser.AllowNavigation = wndBrowser.AllowNavigation;
			parent.Controls.Add(m_browser);
		}

        //--------------------------------------------
        public void Navigate(string strURL)
        {
            if (m_browser != null)
            {
                try
                {
                    m_browser.Navigate(strURL);
                }
                catch { }
            }
        }
		
		//--------------------------------------------
		public override Control Control
		{
			get
			{
				return m_browser;
			}
		}

		//---------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
			UpdateValeursCalculees();
		}

		//---------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//---------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
			C2iWndWebBrowser wndBrowser = WndAssociee as C2iWndWebBrowser;
			if (wndBrowser == null | m_browser == null)
				return;
			CContexteEvaluationExpression contexte = CUtilControlesWnd.GetContexteEval(this, EditedElement);
            try
            {
                if (wndBrowser.UrlFormula != null)
                {
                    CResultAErreur result = wndBrowser.UrlFormula.Eval(contexte);
                    string strUrl = null;
                    if (result)
                        strUrl = result.Data == null ? null : result.Data.ToString();
                    m_browser.Url = new Uri(strUrl);
                }
            }
            catch { }
		}

		//---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}

        

	}
}
