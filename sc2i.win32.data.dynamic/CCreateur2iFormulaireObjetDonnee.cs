using System;
using System.Collections.Generic;
using System.Text;
using sc2i.formulaire.win32;
using sc2i.data;
using sc2i.multitiers.client;
using sc2i.common;
using sc2i.formulaire.win32.editor;

namespace sc2i.win32.data.dynamic
{
    [AutoExec("Autoexec")]
	public class CCreateur2iFormulaireObjetDonnee : CCreateur2iFormulaireV2
	{
		private int m_nIdSession = -1;

		//-----------------------------------------------------------
		public CCreateur2iFormulaireObjetDonnee(int nIdSession)
			:base ( )
		{
			m_nIdSession = nIdSession;
		}

        //-----------------------------------------------------------
        private static bool m_bReferenceAupresDuPanelFormulaireSurElement = false;
        public static void Autoexec()
        {
            if (!m_bReferenceAupresDuPanelFormulaireSurElement)
            {
                m_bReferenceAupresDuPanelFormulaireSurElement = true;
                CPanelFormulaireSurElement.AddGetCreateur(new CPanelFormulaireSurElement.GetCreateurForObjectDelegate(GetCreateurForObject));
            }
        }

        //-----------------------------------------------------------
        private static void GetCreateurForObject(object obj, ref CCreateur2iFormulaireV2 createurCree)
        {
            IObjetDonneeAIdNumerique oai = obj as IObjetDonneeAIdNumerique;
            if (oai != null)
            {
                createurCree = new CCreateur2iFormulaireObjetDonnee(oai.ContexteDonnee.IdSession);
            }
        }

		//-----------------------------------------------------------
		protected override void  UpdateVisibilityEtEnable(object elementEdite)
		{
			 base.UpdateVisibilityEtEnable(elementEdite);
			
			/*//Récupère la restriction sur le type édité
			 CSessionClient session = CSessionClient.GetSessionForIdSession(m_nIdSession);
			 if (session != null && ElementEdite != null)
			 {
				 IInfoUtilisateur user = session.GetInfoUtilisateur();
				 if (user != null)
				 {
					 int? nIdVersion = null;
					 IObjetAContexteDonnee objetAContexte = ElementEdite as IObjetAContexteDonnee;
					 if (objetAContexte != null)
						 nIdVersion = objetAContexte.ContexteDonnee.IdVersionDeTravail;
					 CListeRestrictionsUtilisateurSurType restrictions = user.GetListeRestrictions(nIdVersion);
					 foreach (IControleWndFor2iWnd ctrl in ControlesPrincipaux)
						 ctrl.AppliqueRestriction(restrictions);
				 }
			 }*/
		}

        //-----------------------------------------------------------
        public override void UpdateVisibilityEtEnable(IControleWndFor2iWnd ctrl, object elementEdite)
        {
            base.UpdateVisibilityEtEnable(ctrl, elementEdite);
            //Récupère la restriction sur le type édité
            /*CSessionClient session = CSessionClient.GetSessionForIdSession(m_nIdSession);
            if (session != null && ElementEdite != null)
            {
                IInfoUtilisateur user = session.GetInfoUtilisateur();
                if (user != null)
                {
                    int? nIdVersion = null;
                    IObjetAContexteDonnee objetAContexte = ElementEdite as IObjetAContexteDonnee;
                    if (objetAContexte != null)
                        nIdVersion = objetAContexte.ContexteDonnee.IdVersionDeTravail;
                    CListeRestrictionsUtilisateurSurType restrictions = user.GetListeRestrictions(nIdVersion);
                    ctrl.AppliqueRestriction(restrictions);
                }
            }*/
        }
	}
}
