using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.data.navigation
{
    public partial class CFormToolPopup : Form
    {
        public static List<CFormToolPopup> m_listeFormesVisibles = new List<CFormToolPopup>();

        private CListeObjetsDonnees m_listeObjets = null;
        private Image m_imageDeListe = null;

        private static CFormNavigateur m_navigateurParDefaut = null;

        //----------------------------------------------------
        public CFormToolPopup()
        {
            InitializeComponent();
            m_listeFormesVisibles.Add(this);
            CWin32Traducteur.Translate( typeof(CFormToolPopup), this);
        }

        //----------------------------------------------------
        public static void InitNavigateurParDefaut(CFormNavigateur navigateur)
        {
            if (navigateur != null)
                m_navigateurParDefaut = navigateur;
        }

        //----------------------------------------------------
        protected static CFormNavigateur NavigateurDefaut
        {
            get
            {
                return m_navigateurParDefaut;
            }
        }


        //--------------------------------------------------------------------
        private void m_chktopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = m_chktopMost.Checked;
        }

        //--------------------------------------------------------------------
        public CResultAErreur OnDetailDemande(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            if (NavigateurAssocie != null)
                return NavigateurAssocie.EditeElement(objet, false, "");
            return result;
        }

        //--------------------------------------------------------------------
        public virtual CFormNavigateur NavigateurAssocie
        {
            get
            {
                return m_navigateurParDefaut;
            }
        }
        
        //--------------------------------------------------------------------
        private bool m_bIsAutoArranging = false;
        public void AutoArrangeSize()
        {
            if (NavigateurAssocie != null)
            {
                m_bIsAutoArranging = true;
                WindowState = FormWindowState.Normal;
                int nWidth = Math.Min(Width, Screen.PrimaryScreen.WorkingArea.Size.Width/2);
                Location = new Point(Screen.PrimaryScreen.WorkingArea.Size.Width-nWidth, 0);
                Size = new Size(nWidth, Screen.PrimaryScreen.WorkingArea.Height);
                NavigateurAssocie.WindowState = FormWindowState.Normal;
                NavigateurAssocie.Location = new Point(0, 0);
                NavigateurAssocie.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - nWidth,
                    Screen.PrimaryScreen.WorkingArea.Height);
                m_bIsAutoArranging = false;
            }
        }

        //------------------------------------------------------------------------
        private void m_chkAutoArrange_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkAutoArrange.Checked)
                AutoArrangeSize();
        }

        //------------------------------------------------------------------------
        private void CFormToolPopup_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bIsAutoArranging && !DesignMode && m_chkAutoArrange.Checked)
                AutoArrangeSize();
        }



    }
}
