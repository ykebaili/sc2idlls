using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using sc2i.win32.common;

namespace sc2i.win32.common
{
    public partial class CControlPanorama : UserControl, IControlALockEdition
    {
        List<object> m_listeObjets = new List<object>();
        private Color m_btnColor = Color.LightGreen;
        private int m_nLargeurBoutons = 100;
        private int m_nHauteurBoutons = 70;
        private int m_nMargeHorizontale = 3;
        private int m_nMargeVerticale = 3;
        private int m_nNbLignesMax = 1;
        private int m_nRayonBouton = 5;

        private int m_nPageEnCours = 0;
        private int m_nNbcolonnes;
        private int m_nNblignes;

        private Dictionary<object, Color> m_dicCouleursSpecifiques = new Dictionary<object,Color>();
        private HashSet<object> m_setBoutonsInvalides = new HashSet<object>();

        //---------------------------------------------------
        public CControlPanorama()
        {
            InitializeComponent();
        }

        /// ///////////////////////////////////////////
        public int ButtonWidth
        {
            get
            {
                return m_nLargeurBoutons;
            }
            set
            {
                m_nLargeurBoutons = value;
            }
        }

        /// ///////////////////////////////////////////
        public int ButtonHeight
        {
            get
            {
                return m_nHauteurBoutons;
            }
            set
            {
                m_nHauteurBoutons = value;
            }
        }

        /// ///////////////////////////////////////////
        public int ButtonHorizontalMargin
        {
            get
            {
                return m_nMargeHorizontale;
            }
            set
            {
                m_nMargeHorizontale = value;
            }
        }

        /// ///////////////////////////////////////////
        public int ButtonVerticalMargin
        {
            get
            {
                return m_nMargeVerticale;
            }
            set
            {
                m_nMargeVerticale = value;
            }
        }

        /// ///////////////////////////////////////////
        public Color ButtonColor
        {
            get
            {
                return m_btnColor;
            }
            set
            {
                m_btnColor = value;
                foreach (Control ctrl in m_panelGlobal.Controls)
                {
                    CRoundButton button = ctrl as CRoundButton;
                    if (button != null)
                        button.BackColor = value;
                }
            }
        }

        /// ///////////////////////////////////////////
        public int ButtonRadius
        {
            get
            {
                return m_nRayonBouton;
            }
            set
            {
                m_nRayonBouton = value;
            }
        }

        /// ///////////////////////////////////////////
        public void ClearSpecificColors()
        {
            m_dicCouleursSpecifiques.Clear();
            ButtonColor = ButtonColor;
        }

        /// ///////////////////////////////////////////
        public void ClearInvalidateValues()
        {
            m_setBoutonsInvalides.Clear();
            foreach (Control ctrl in m_panelGlobal.Controls)
                ctrl.Enabled = true;
        }

        /// ///////////////////////////////////////////
        public void SetSpecificColor(object valeur, Color couleur)
        {
            if (couleur != ButtonColor && valeur != null)
            {
                m_dicCouleursSpecifiques[valeur] = couleur;
                foreach (Control ctrl in m_panelGlobal.Controls)
                {
                    if (valeur.Equals(ctrl.Tag) )
                    {
                        ctrl.BackColor = couleur;
                        break;
                    }
                }
            }
        }

        /// ///////////////////////////////////////////
        public void InvalideValue(object valeur, bool bInvalidate)
        {
            if (valeur != null)
            {
                if (bInvalidate)
                    m_setBoutonsInvalides.Add(valeur);
                else
                    m_setBoutonsInvalides.Remove(valeur);
                foreach (Control ctrl in m_panelGlobal.Controls)
                {
                    if (valeur.Equals(ctrl.Tag))
                        ctrl.Enabled = !bInvalidate;
                }
            }
        }

        /// ///////////////////////////////////////////
        public int MaxLineCount
        {
            get
            {
                return m_nNbLignesMax;
            }
            set
            {
                m_nNbLignesMax = value;
            }
        }

        
        /// ///////////////////////////////////////////
        private void RecalcSize()
        {
            if (m_listeObjets == null )
                return;
            m_nNbcolonnes = ClientSize.Width / (ButtonWidth);
            int nMaxItems = m_nNbcolonnes * MaxLineCount;
            if (m_listeObjets.Count < nMaxItems)
            {
                m_nNblignes = m_listeObjets.Count / m_nNbcolonnes+1;
                if (m_listeObjets.Count % m_nNbcolonnes == 0)
                    m_nNblignes--;
            }
            else
                m_nNblignes = MaxLineCount;
            Height = m_nNblignes * ButtonHeight + (m_lblTitreGroupe.Text.Length > 0? m_panelTop.Height : 0);
        }


        /// ///////////////////////////////////////////
        public void Init(List<object> lstObjets, string strTitre)
        {
            m_listeObjets = lstObjets;

            m_lblTitreGroupe.Font = Font;
            m_lblTitreGroupe.ForeColor = ForeColor;

            if (strTitre.Length > 0)
                m_lblTitreGroupe.Text = strTitre;
            else
                m_panelTop.Visible = false;
            RecalcSize();
            m_nPageEnCours = 0;
            m_panelGlobal.BackColor = BackColor;
            AffichePage(m_nPageEnCours, false);
        }

        /// ///////////////////////////////////////////
        private int GetNbSurPageDebutEtFin()
        {
            if (m_listeObjets == null)
                return 1;
            int nNbParPage = m_nNbcolonnes * m_nNblignes;
            if (m_listeObjets.Count <= nNbParPage)
                return nNbParPage;
            return nNbParPage - 1;
        }

        /// ///////////////////////////////////////////
        private int GetNbSurPageMilieu()
        {
            return m_nNblignes * m_nNbcolonnes - 2;
        }



        /// ///////////////////////////////////////////
        private int GetNbPages()
        {
            if (m_listeObjets == null)
                return 1;
            int nNb = m_listeObjets.Count;
            int nNbPages = 1;//Page de début
            nNb -= GetNbSurPageDebutEtFin();
            if (nNb < 0)
                return nNbPages;
            nNbPages += 1;//Page de fin
            nNb -= GetNbSurPageDebutEtFin();
            if (nNb < 0)
                return nNbPages;
            return (int)(nNb/GetNbSurPageMilieu())+1;
        }
       

        /// ///////////////////////////////////////////
        private int GetIndexStartPage(int nPage)
        {
            if (nPage == 0 || m_listeObjets == null)
                return 0;
            int nNbParPage = Math.Max(m_nNblignes * m_nNbcolonnes, 1);
            int nStart = GetNbSurPageDebutEtFin() + (nPage - 1) * GetNbSurPageMilieu();

            return nStart;
        }

        /// ///////////////////////////////////////////
        public event OnCalcButtonTextEventHandler OnCalcButtonText;

        /// ///////////////////////////////////////////
        /// Methode d'affichage d'une page
        /// ///////////////////////////////////////////
        private bool m_bActivePagePasCalculee = false;
        private void AffichePage(int nNumPage, bool bImmediate)
        {
            m_nPageEnCours = nNumPage;
            if (!bImmediate)
            {
                m_bActivePagePasCalculee = true;
                return;
            }
            m_bActivePagePasCalculee = false;
            
            m_nPageEnCours = nNumPage;
            RecalcSize();
            if ( m_listeObjets == null)
                return ;

            m_panelGlobal.SuspendDrawing();
            foreach (Control ctrl in new ArrayList(m_panelGlobal.Controls))
            {
                ctrl.Dispose();
            }
            m_panelGlobal.Controls.Clear();

            int nStart = GetIndexStartPage(nNumPage);

            int nIndexElement = nStart;
            for (int nLigne = 0; nLigne < m_nNblignes; nLigne++)
            {
                for (int nCol = 0; nCol < m_nNbcolonnes; nCol++)
                {
                    Color buttonColor = ButtonColor;
                    CRoundButton bouton = new CRoundButton();
                    bouton.Rayon = m_nRayonBouton;
                    bouton.Size = new Size ( ButtonWidth, ButtonHeight );
                    if (nLigne == 0 && nCol == 0 && nNumPage != 0)
                    {
                        //Bouton précédent
                        bouton.Text = "<<";
                        bouton.Click += new EventHandler(BoutonPagePrecedente_Click);
                    }
                    else if (nLigne == m_nNblignes - 1 && nCol == m_nNbcolonnes - 1 && nIndexElement+1 < m_listeObjets.Count)
                    {
                        bouton.Text = ">>";
                        bouton.Click += new EventHandler(BoutonPageSuivante_Click);
                    }
                    else if (nIndexElement < m_listeObjets.Count)
                    {
                        object obj = m_listeObjets[nIndexElement];
                        CalcButtonTextEventArgs args = new CalcButtonTextEventArgs(obj);
                        if (OnCalcButtonText != null)
                            OnCalcButtonText(this, args);
                        bouton.Tag = obj;

                        if (obj != null && m_dicCouleursSpecifiques.ContainsKey(obj))
                            buttonColor = m_dicCouleursSpecifiques[obj];
                        if (obj != null && m_setBoutonsInvalides.Contains(obj))
                            bouton.Enabled = false;
                        else
                            bouton.Enabled = true;

                        bouton.Text = args.Libelle;
                        bouton.Click += new EventHandler(ButtonSelection_OnClickBoutonEntite);
                        nIndexElement++;
                    }
                    else
                        bouton = null;
                    if (bouton != null)
                    {
                        bouton.MargeHorizontale = ButtonHorizontalMargin;
                        bouton.MargeVerticale = ButtonVerticalMargin;
                        bouton.BackColor = buttonColor;
                        bouton.Font = Font;
                        m_panelGlobal.Controls.Add(bouton);
                        Point pt = new Point(nCol * ButtonWidth,
                            nLigne * ButtonHeight);
                        bouton.Location = pt;
                        bouton.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
                    }
                }
            }
            m_panelGlobal.ResumeDrawing();
        }

        //-------------------------------------------------------------------------------
        public event OnSelectObjectEventHandler OnSelectObject;

        //-------------------------------------------------------------------------------
        void ButtonSelection_OnClickBoutonEntite(object sender, EventArgs e)
        {
            if (!m_gestionnaireModeEdition.ModeEdition)
                return;

            CRoundButton btnClicked = sender as CRoundButton;
            if (btnClicked != null)
            {
                if (OnSelectObject != null)
                    OnSelectObject(this, new OnSelectObjectEventArgs(btnClicked.Tag));
            }
        }

        //-------------------------------------------------------------------------------
        void BoutonPagePrecedente_Click(object sender, EventArgs e)
        {
            if ( m_nPageEnCours > 0 )
                AffichePage(m_nPageEnCours - 1, true);
        }

        //-------------------------------------------------------------------------------
        void BoutonPageSuivante_Click(object sender, EventArgs e)
        {
            AffichePage(m_nPageEnCours + 1, true);
        }


        //-------------------------------------------------------------------------------
        private void UpdateListeObjetAfficherFromListeSource()
        {
            
        }

        //-------------------------------------------------------------------------------
        private void CControlPanorama_SizeChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                int nbLigneTemp = m_nNblignes;
                int nbColonnesTemp = m_nNbcolonnes;

                RecalcSize();
                AffichePage(0,false);
            }

            
        }

        //-------------------------------------------------------------------------------
        void m_panelGlobal_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_bActivePagePasCalculee)
            {
                AffichePage(m_nPageEnCours, true);
            }
        }

        
        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }
        public event EventHandler OnChangeLockEdition;

        #endregion
    }
    public delegate void OnSelectObjectEventHandler(object sender, OnSelectObjectEventArgs args);
    public class OnSelectObjectEventArgs : EventArgs
    {
        public object ObjectSelectionne { get; set; }
        public OnSelectObjectEventArgs(object objet)
            : base()
        {
            ObjectSelectionne = objet;
        }
    }

    public delegate void OnCalcButtonTextEventHandler(object sender, CalcButtonTextEventArgs args);

    public class CalcButtonTextEventArgs
    {
        public object Objet { get; set; }
        public string Libelle { get; set; }

        public CalcButtonTextEventArgs(Object objet)
        {
            Objet = objet;
            Libelle = objet != null ? objet.ToString() : "";
        }
    }


}
