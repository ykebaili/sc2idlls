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
    public partial class CFormListeExtraiteOld : Form
    {
        public static List<CFormListeExtraiteOld> m_listeFormesVisibles = new List<CFormListeExtraiteOld>();

        private CListeObjetsDonnees m_listeObjets = null;
        private Image m_imageDeListe = null;
        private CFormNavigateur m_navigateurAssocié = null;

        private static CFormNavigateur m_navigateurParDefaut = null;

        //----------------------------------------------------
        public CFormListeExtraiteOld()
        {
            InitializeComponent();
            m_listeFormesVisibles.Add(this);
            CWin32Traducteur.Translate(this);
            m_panelListe.ContexteUtilisation = "SYSTEM_POPUP";
            m_panelListe.TraiterModificationElement = new CPanelListeSpeedStandard.ModifierElementDelegate(OnDetailDemande);
        }

        //----------------------------------------------------
        public static void InitNavigateurParDefaut(CFormNavigateur navigateur)
        {
            if (navigateur != null)
                m_navigateurParDefaut = navigateur;
        }

        //----------------------------------------------------
        public static void ShowListeFromPanelListStd(string strTitre,
            CPanelListeSpeedStandard panelList)
        {
            ShowListe(strTitre,
                panelList.ListeObjets,
                null,
                panelList.Columns,
                panelList.ContexteUtilisation);
        }


        //----------------------------------------------------
        public static void ShowListe(
            string strTitre,
            CListeObjetsDonnees listeAVoir,
            CFormNavigateur navigateurAssocié,
            GLColumnCollection columns,
            string strContexteUtilisation)
        {
            if (listeAVoir == null)
                return;

            //Créer une nouvelle liste dans le contexte de base
            CListeObjetsDonnees lst = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, listeAVoir.TypeObjets);
            CFiltreData filtre = CFiltreData.GetAndFiltre(listeAVoir.FiltrePrincipal,
                listeAVoir.Filtre);
            lst.Filtre = filtre;


            CFormListeExtraiteOld frm = new CFormListeExtraiteOld();
            frm.Text = strTitre;
            frm.m_navigateurAssocié = navigateurAssocié != null?navigateurAssocié:m_navigateurParDefaut;
            Image img = CFormListeStandard.CalculeNewImage(lst.TypeObjets);
            
            frm.m_listeObjets = lst;
            if (columns != null)
            {
                frm.m_panelListe.AllowSerializePreferences = false;
                foreach (GLColumn col in columns)
                {
                    GLColumn copie = CCloner2iSerializable.Clone(col) as GLColumn;
                    frm.m_panelListe.Columns.Add(copie);
                }
            }
            else
            {
                frm.m_panelListe.AllowSerializePreferences = true;
                if (strContexteUtilisation != null)
                    frm.m_panelListe.ContexteUtilisation = strContexteUtilisation;
                string strField = DescriptionFieldAttribute.GetDescriptionField(lst.TypeObjets, "DescriptionElement");
                GLColumn col = new GLColumn("");
                col.Propriete = strField;
                col.Width = frm.m_panelListe.ClientSize.Width;
                frm.m_panelListe.Columns.Add(col);
            }

            frm.Show();
        }

        //--------------------------------------------------------------------
        private void CFormListeExtraite_Load(object sender, EventArgs e)
        {
            if (m_listeObjets != null)
            {
                m_imageDeListe = CFormListeStandard.CalculeNewImage(m_listeObjets.TypeObjets);
                m_picListe.Image = m_imageDeListe;
            }
            m_panelListe.InitFromListeObjets(m_listeObjets,
                m_listeObjets.TypeObjets,
                null,
                "");
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
        private CFormNavigateur NavigateurAssocie
        {
            get
            {
                return m_navigateurAssocié == null ? m_navigateurParDefaut : m_navigateurParDefaut;
            }
        }


        //--------------------------------------------------------------------
        private bool m_bIsAutoArranging = false;
        private void AutoArrangeSize()
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

        private void m_chkAutoArrange_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkAutoArrange.Checked)
                AutoArrangeSize();
        }

        private void CFormListeExtraite_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bIsAutoArranging && !DesignMode && m_chkAutoArrange.Checked)
                AutoArrangeSize();
        }

        public static Image GetImageStdExtraction()
        {
            return sc2i.win32.data.navigation.Properties.Resources.Extract_List;
        }




    }
}
