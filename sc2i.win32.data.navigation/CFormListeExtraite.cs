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
using System.Runtime.InteropServices;

namespace sc2i.win32.data.navigation
{
    public partial class CFormListeExtraite : CFormToolPopup
    {
        private CListeObjetsDonnees m_listeObjets = null;
        private Image m_imageDeListe = null;
        
        public CFormListeExtraite()
            :base()
        {
            InitializeComponent();
            m_panelListe.ContexteUtilisation = "SYSTEM_POPUP";
            m_panelListe.TraiterModificationElement = new CPanelListeSpeedStandard.ModifierElementDelegate(OnDetailDemande);
        }

        //----------------------------------------------------
        public static void ShowListeFromPanelListStd(string strTitre,
            CPanelListeSpeedStandard panelList)
        {
            ShowListe(strTitre,
                panelList.ListeObjets,
                panelList.Columns,
                panelList.ContexteUtilisation);
        }


        //----------------------------------------------------
        public static void ShowListe(
            string strTitre,
            CListeObjetsDonnees listeAVoir,
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


            CFormListeExtraite frm = new CFormListeExtraite();
            frm.Text = strTitre;
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

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        //--------------------------------------------------------------------
        private void CFormListeExtraite_Load(object sender, EventArgs e)
        {
            AutoArrangeSize();
            if (m_listeObjets != null)
            {
                Bitmap bmp = CFormListeStandard.CalculeNewImage(m_listeObjets.TypeObjets) as Bitmap;
                if (bmp != null)
                {
                    IntPtr hIcon = bmp.GetHicon();
                    Icon newIcon = Icon.FromHandle(hIcon);
                    Icon = newIcon;
                    bmp.Dispose();
                    try{
                    newIcon.Dispose();
                    DestroyIcon(hIcon);
                    }
                    catch{}
                  
                }
            }
            m_panelListe.InitFromListeObjets(m_listeObjets,
                m_listeObjets.TypeObjets,
                null,
                "");
        }

        //--------------------------------------------------------------------
        public CResultAErreur OnDetailDemande(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            if (NavigateurAssocie != null)
                return NavigateurAssocie.EditeElement(objet, false, "");
            return result;
        }


    }
}
