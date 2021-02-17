using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.common.Restrictions;
using System.Drawing;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec ( "Autoexec")]
	public class CWndFor2iImage : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
		private C2iPictureBox m_pictureBox = new C2iPictureBox();
		//--------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndImage), typeof(CWndFor2iImage));
		}

        //--------------------------------------------
        public CWndFor2iImage()
            : base()
        {
            m_pictureBox.MouseClick += new MouseEventHandler(m_pictureBox_MouseClick);
            m_pictureBox.EnabledChanged += new EventHandler(m_pictureBox_EnabledChanged);
        
        }

        //--------------------------------------------
        void m_pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            C2iWndImage wndImage = WndAssociee as C2iWndImage;
            if (wndImage != null && wndImage.Action != null)
            {
                CResultAErreur result = CExecuteurActionSur2iLink.ExecuteAction(
                    Control, 
                    wndImage.Action, 
                    CUtilControlesWnd.GetObjetForEvalFormuleParametrage(this, EditedElement));
                if (!result)
                    CFormAlerte.Afficher(result.Erreur);
            }
            CUtilControlesWnd.DeclencheEvenement(C2iWndImage.c_strIdEvenementClick, this);
            
        }

        void m_pictureBox_EnabledChanged(object sender, EventArgs e)
        {
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            if (m_pictureBox.Enabled)
            {
                C2iWndImage wndImage = WndAssociee as C2iWndImage;
                if (wndImage != null && wndImage.Action != null ||
                    wndImage.GetHandler(C2iWndImage.c_strIdEvenementClick) != null)
                {
                    m_pictureBox.Cursor = Cursors.Hand;
                    return;
                }
            }
            m_pictureBox.Cursor = Cursors.Arrow;
        }



        protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndImage image = wnd as C2iWndImage;
			if (image == null)
				return;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(image, m_pictureBox);
			m_pictureBox.Image = image.GetImageToDisplay(null);
			parent.Controls.Add(m_pictureBox);
            UpdateCursor();
		}

		
		//--------------------------------------------
		public override Control Control
		{
			get
			{
				return m_pictureBox;
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
			C2iWndImage wndImage = WndAssociee as C2iWndImage;
			if (wndImage == null | m_pictureBox == null)
				return;
			CContexteEvaluationExpression contexte = CUtilControlesWnd.GetContexteEval(this,EditedElement);
			try
			{
                m_pictureBox.SuspendDrawing();
                Image img = m_pictureBox.Image;
                m_pictureBox.Image = null;
                if ( img != null )
                    img.Dispose();
				m_pictureBox.Image = wndImage.GetImageToDisplay(contexte);
                m_pictureBox.ResumeDrawing();
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
