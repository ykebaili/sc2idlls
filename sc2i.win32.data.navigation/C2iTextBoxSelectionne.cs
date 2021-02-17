using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.win32.data.dynamic;
using sc2i.common;

namespace sc2i.win32.data.navigation
{

    public partial class C2iTextBoxSelectionne : UserControl, IControlALockEdition,
        ISelectionneurElementListeObjetsDonnees
    {
        private bool m_bLockEdition = false;
        private FonctionRetournantTexteNull m_fctTextNull = null;
        private bool m_bHasLink = false;
        private Dictionary<Type, Type> m_dicTypeObjetToTypeFormList = new Dictionary<Type,Type>();
        private Dictionary<Type, CReferenceTypeForm> m_dicTypeObjetToTypeFormEdition = new Dictionary<Type,CReferenceTypeForm>();
        private bool m_bAppliquerFiltreStandard = true;


        //-----------------------------------
        public C2iTextBoxSelectionne()
        {
            InitializeComponent();
        }

        #region IControlALockEdition Membres

        //-----------------------------------
        public bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_panelLink.Visible = value && m_bHasLink ;
                m_txtSelect.Visible = !value || ! m_bHasLink;
                m_txtSelect.LockEdition = value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        //-----------------------------------
        public FonctionRetournantTexteNull FonctionTextNull
        {
            get
            {
                return m_txtSelect.FonctionTextNull;
            }
            set
            {
                m_txtSelect.FonctionTextNull = value;
            }
        }

        //-----------------------------------
        public string TextNull
        {
            get
            {
                return m_txtSelect.TextNull;
            }
            set
            {
                m_txtSelect.TextNull = value ;
            }
        }

        //------------------------------------------------------------------
        public override string Text
        {
            get
            {
                return m_txtSelect.Text;
            }
            set
            {
                m_txtSelect.Text = value;
                m_link.Text = value;
            }
        }

        //------------------------------------------------------
        private void UpdateImage()
        {
            Image img = null;
            if (m_txtSelect.SpecificImage != null)
                m_picType.Image = m_txtSelect.SpecificImage;
            else
            {
                if (m_txtSelect.Configs.Length == 1)
                {
                    img = DynamicClassAttribute.GetImage(m_txtSelect.Configs[0].TypeObjets);
                }
                else
                {
                    if (SelectedObject != null)
                        img = DynamicClassAttribute.GetImage(SelectedObject.GetType());
                    else
                        img = null;
                }
            }
            m_picType.Image = img;
            switch (m_txtSelect.ImageDisplayMode)
            {
                case EModeAffichageImageTextBoxRapide.Never:
                    m_picType.Visible = false;
                    break;
                case EModeAffichageImageTextBoxRapide.Always:
                    m_picType.Visible = img != null;
                    break;
                case EModeAffichageImageTextBoxRapide.OnSelection:
                    m_picType.Visible = img != null && SelectedObject != null;
                    break;
                default:
                    break;
            }
        }

        //------------------------------------------------------------------
        public Image SpecificImage
        {
            get
            {
                return m_txtSelect.SpecificImage;
            }
            set
            {
                m_txtSelect.SpecificImage = value;
            }
        }

        //------------------------------------------------------------------
        public EModeAffichageImageTextBoxRapide ImageDisplayMode
        {
            get
            {
                return m_txtSelect.ImageDisplayMode;
            }
            set
            {
                m_txtSelect.ImageDisplayMode = value;
            }
        }

        //------------------------------------------------------------------
        private void SynchronizeTexteEtObjet()
        {
            m_link.Text = Text;
            Image img = null;
            if (SelectedObject != null)
            {
                img = DynamicClassAttribute.GetImage(SelectedObject.GetType());
            }
            UpdateImage();
        }


        //-----------------------------------
        private CReferenceTypeForm GetTypeFormToEdit ( Type typeObjet )
        {
            CReferenceTypeForm refTypeForm = null;
            if ( !m_dicTypeObjetToTypeFormEdition.TryGetValue ( typeObjet, out refTypeForm ) )
            {
                refTypeForm = CFormFinder.GetRefFormToEdit ( typeObjet );
                m_dicTypeObjetToTypeFormEdition[typeObjet] = refTypeForm;
            }
            return refTypeForm;
        }

        

        //------------------------------------------------------------------
        public CObjetDonnee SelectedObject
        {
            get
            {
                //Nécéssaire pour les grilles: SI le contrôle est dans une grille,
                //on peut interroger sa valeur SelectedObject avec que le lostFocus
                //ne soit détecté par le contrôle
                if (!LockEdition && m_txtSelect.Focused && m_txtSelect.LastValidatedText != Text)
                    m_txtSelect.SelectObject();
                return m_txtSelect.SelectedObject;
            }
            set
            {
                m_txtSelect.ElementSelectionne = value;
                SynchronizeTexteEtObjet();
                if (OnSelectedObjectChanged != null)
                    OnSelectedObjectChanged(this, new EventArgs());
                if (ElementSelectionneChanged != null)
                    ElementSelectionneChanged(this, new EventArgs());
                if (value != null && ElementSelectionneChangedNotNull != null)
                    ElementSelectionneChangedNotNull(this, new EventArgs());
            }
        }

        //------------------------------------------------------------------
        public void InitForSelectAvecFiltreDeBase(
            Type typeObjets, 
            string strProprieteAffichee, 
            CFiltreData filtreDeBase, 
            bool bForceInit)
        {
            m_txtSelect.InitAvecFiltreDeBase(typeObjets, strProprieteAffichee, filtreDeBase, bForceInit);
        }

        //------------------------------------------------------------------
        public void InitForSelect(
            Type typeObjets, 
            string strProprieteAffichee, 
            bool bForceInit)
        {
            m_txtSelect.InitAvecFiltreDeBase(typeObjets, strProprieteAffichee, null, bForceInit);
        }

        //------------------------------------------------------------------
        public void InitAvecFiltreDeBase<T>(
            string strProprieteAffichee, 
            CFiltreData filtreDeBase, 
            bool bForceInit)
            where T : CObjetDonnee
        {
            InitForSelectAvecFiltreDeBase(typeof(T), strProprieteAffichee, filtreDeBase, bForceInit);
        }

        //------------------------------------------------------------------
        public void Init<T>(
            string strProprieteAffichee, 
            bool bForceInit)
            where T : CObjetDonnee
        {
            InitForSelect(typeof(T), strProprieteAffichee, bForceInit);
        }

        //------------------------------------------------------------------
        public void InitMultiple(CConfigTextBoxFiltreRapide[] configs, bool bForceInit)
        {
            m_txtSelect.InitMultiple(configs, bForceInit);
        }

		/*//------------------------------------------------------------------
		public void Init(Type typeFormListe, string strProprieteAffichee, CFiltreData filtre)
		{
			Init(typeFormListe, strProprieteAffichee, filtre, false);
		}
		//------------------------------------------------------------------
		public void Init(Type typeFormListe, string strProprieteAffichee, CFiltreData filtre, bool bForceInit)
		{
			Init ( typeFormListe, null, strProprieteAffichee, filtre, bForceInit );
		}

        //------------------------------------------------------------------
        public void Init(Type typeFormListe, string strProprieteAffichee, CFiltreData filtre, bool bForceInit, bool bAppliquerFiltreStd)
        {
            Init(typeFormListe, null, strProprieteAffichee, filtre, bForceInit, bAppliquerFiltreStd);
        }

        //------------------------------------------------------------------
        public void Init(Type typeFormListe, Type typeFormEdition, string strProprieteAffichee, CFiltreData filtre, bool bForceInit)
        {
            Init(typeFormListe, null, strProprieteAffichee, filtre, bForceInit, true);
        }

        //------------------------------------------------------------------
		public void Init(Type typeFormListe, Type typeFormEdition, string strProprieteAffichee, CFiltreData filtre, bool bForceInit, bool bAppliquerFiltreStd)
        {
            Init ( null, typeFormListe, typeFormEdition, strProprieteAffichee, filtre, bForceInit, bAppliquerFiltreStd );
        }

        //------------------------------------------------------------------
		public void Init(Type typeObjets, Type typeFormListe, Type typeFormEdition, string strProprieteAffichee, CFiltreData filtre, bool bForceInit, bool bAppliquerFiltreStd)
		{
			if (!bForceInit && m_bIsInit)
				return;
            if ( typeObjets == null )
            {
                if (typeFormListe != null)
                {
                    object[] attribs = typeFormListe.GetCustomAttributes(typeof(ObjectListeur), true);
                    if (attribs.Length > 0)
                        typeObjets = ((ObjectListeur)attribs[0]).TypeListe;
                }
                if (typeObjets == null &&  typeFormEdition != null)
                {
                    object[] attribs = typeFormEdition.GetCustomAttributes(typeof(ObjectEditeur), true);
                    if (attribs.Length > 0)
                        typeObjets = ((ObjectEditeur)attribs[0]).TypeEdite;
                }
            }
            m_dicTypeObjetToTypeFormList = new Dictionary<Type,Type>();
            m_dicTypeObjetToTypeFormEdition = new Dictionary<Type,CReferenceTypeForm>();
            if ( typeFormEdition != null )
                m_dicTypeObjetToTypeFormEdition[typeObjets] = new CReferenceTypeFormBuiltIn(typeFormEdition);
            if ( typeFormListe != null )
                m_dicTypeObjetToTypeFormList[typeObjets] = typeFormListe;
            m_txtSelect.Init ( typeObjets, strProprieteAffichee, filtre, true);
            m_bAppliquerFiltreStandard = bAppliquerFiltreStd;
			m_bIsInit = true;
            
		}*/
		public OnNewObjetDonneeEventHandler OnNewObjetDonnee;
        public event EventHandler OnSelectedObjectChanged; 

		//------------------------------------------------------------------
        /*private void SelectObject(
            Type typeObjets, 
            CFiltreData filtre, 
            string strRechercheRapide, 
            ref bool bPrisEnCharge)
        {
            Type typeFormList = GetTypeFormList(typeObjets);

            if (typeFormList == null)
                return;
            if (!typeFormList.IsSubclassOf(typeof(CFormListeStandard)))
                return;

            CFormListeStandard frmListe = (CFormListeStandard)Activator.CreateInstance(typeFormList, null);
            if (filtre != null)
                frmListe.FiltreRapide = filtre;
            if (m_bAppliquerFiltreStandard && frmListe.ListeObjets != null)
            {
                Type typeObjet = frmListe.ListeObjets.TypeObjets;
                CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(typeObjet, new object[] { frmListe.ListeObjets.ContexteDonnee });
                CFiltreData filtrePrincipal = objet.FiltreStandard;
                if (filtrePrincipal != null)
                    frmListe.ListeObjets.FiltrePrincipal = filtrePrincipal;
            }
            string strText = Text == TextNull ? "" : Text;
            CObjetDonnee obj = CFormNavigateurPopupListe.SelectObjectQuickSearch(
                frmListe,
                null,
                strText,
                CFormNavigateurPopupListe.CalculeContexteUtilisation(this),
                new OnNewObjetDonneeEventHandler(OnNewObjetDonneeFunc));
            SelectedObject = obj;
        }*/

		public void OnNewObjetDonneeFunc ( object sender, CObjetDonnee nouvelObjet, bool bCancel )
		{
			if ( OnNewObjetDonnee != null )
				OnNewObjetDonnee ( sender, nouvelObjet, ref bCancel );
		}

		//------------------------------------------------------------------
		private void m_linkControl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CObjetDonneeAIdNumeriqueAuto obj = this.SelectedObject as CObjetDonneeAIdNumeriqueAuto;
            if (obj == null)
                return;
            CReferenceTypeForm refTypeForm = GetTypeFormToEdit(obj.GetType());
            if ( refTypeForm != null )
            {
                IFormNavigable frm = refTypeForm.GetForm ( obj ) as IFormNavigable;
                if ( frm != null )
                    CSc2iWin32DataNavigation.Navigateur.AffichePage(frm);
            }
		}

		//------------------------------------------------------------------
		/// <summary>
		/// Indique si le controle permet de faire un lien
		/// </summary>
		public bool HasLink
		{
			get
			{
				return m_bHasLink;
			}
			set
			{
				m_bHasLink = value;
			}
		}
	
		#region Membres de ISelectionneurElementListeObjetsDonnees

		public CObjetDonnee ElementSelectionne
		{
			get
			{
				return SelectedObject;
			}
			set
			{
				SelectedObject = value;
			}
		}

        public event System.EventHandler ElementSelectionneChanged;
        public event System.EventHandler ElementSelectionneChangedNotNull;

		public bool IsUpdating()
		{
			// TODO : ajoutez l'implémentation de C2iTextBoxSelectionneOld.IsUpdating
			return false;
		}

		#endregion
        public void SelectAll()
        {
            m_txtSelect.SelectAll();
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData)
            {
                case Keys.Shift:
                    return true;
                case Keys.Right:
                case Keys.End:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_txtSelect.TextBox != null && m_txtSelect.TextBox.SelectionStart == m_txtSelect.TextBox.Text.Length &&
                        m_txtSelect.TextBox.SelectionLength == 0)
                        return false;
                    return true;
                case Keys.Left:
                case Keys.Home:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_txtSelect.TextBox != null && m_txtSelect.TextBox.SelectionStart == 0 && m_txtSelect.TextBox.SelectionLength == 0)
                        return false;
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        private void m_txtSelect_ElementSelectionneChanged(object sender, EventArgs e)
        {
            if (ElementSelectionneChanged != null)
                ElementSelectionneChanged(this, new EventArgs());
            if ( m_txtSelect.ElementSelectionne != null && ElementSelectionneChangedNotNull != null )
                ElementSelectionneChangedNotNull ( this, new EventArgs() );
            if ( OnSelectedObjectChanged != null )
                OnSelectedObjectChanged ( this, new EventArgs() );
            SynchronizeTexteEtObjet();
            
        }

        private Point? m_ptStartDrag = null;
        private void m_picType_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_ptStartDrag = new Point(e.X, e.Y);
                m_picType.Capture = true;
            }
        }

        private void m_picType_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && m_ptStartDrag != null && ElementSelectionne != null)
            {
                if (Math.Abs(e.X - m_ptStartDrag.Value.X) > 3 ||
                    Math.Abs(e.Y - m_ptStartDrag.Value.Y) > 3)
                {
                    CReferenceObjetDonneeDragDropData data = new CReferenceObjetDonneeDragDropData(ElementSelectionne);
                    m_picType.Capture = false;
                    DoDragDrop(data, DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Link);
                }
            }

        }

        private void m_picType_MouseUp(object sender, MouseEventArgs e)
        {
            m_ptStartDrag = null;
            m_picType.Capture = false;
        }
	}

 
}
