using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data;
using sc2i.win32.common;
using sc2i.win32.data;
using sc2i.win32.data.dynamic;


namespace sc2i.win32.data.navigation
{

	/// <summary>
	/// Description résumée de C2iTextBoxSelectionneOld.
	/// </summary>
	public class C2iTextBoxSelectionneOld : System.Windows.Forms.UserControl, IControlALockEdition, ISelectionneurElementListeObjetsDonnees
	{
		
		private bool m_bHasLink = true;
		private sc2i.win32.common.C2iTextBox m_textBox;
		private System.Windows.Forms.Button m_btn;

        private Type m_typeObjets = null;

		private string m_strTextNull = "";
		private string m_strOldText = "";
		private CObjetDonnee m_selectedObject = null;
		private string m_strPropriete = "";
		private Type m_typeFormList = null;
		private Type m_typeFormEdition = null;
		private CFiltreData m_filtre = null;
        private bool m_bAppliquerFiltreStandard = true;
		private bool m_bLockEdition = true;
		private System.Windows.Forms.Button m_btnFlush;
		private System.Windows.Forms.LinkLabel m_linkControl;
		private FonctionRetournantTexteNull m_fctTextNull = null;

		public FonctionRetournantTexteNull FonctionTextNull
		{
			get
			{
				return m_fctTextNull;
			}
			set
			{
				m_fctTextNull = value;
			}
		}

		public string TextNull
		{
			get
			{
				if(m_fctTextNull == null)
					return m_strTextNull;
				return m_fctTextNull();
			}
			set
			{
				m_strTextNull = value;
			}
		}

        

		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public C2iTextBoxSelectionneOld()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
		}

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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_textBox = new sc2i.win32.common.C2iTextBox();
            this.m_btn = new System.Windows.Forms.Button();
            this.m_btnFlush = new System.Windows.Forms.Button();
            this.m_linkControl = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // m_textBox
            // 
            this.m_textBox.AllowDrop = true;
            this.m_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBox.Location = new System.Drawing.Point(0, 0);
            this.m_textBox.LockEdition = false;
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(184, 20);
            this.m_textBox.TabIndex = 0;
            this.m_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_textBox_DragDrop);
            this.m_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyDown);
            this.m_textBox.Leave += new System.EventHandler(this.m_textBox_Leave);
            this.m_textBox.Enter += new System.EventHandler(this.m_textBox_Enter);
            this.m_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_textBox_DragEnter);
            this.m_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.m_textBox_DragOver);
            // 
            // m_btn
            // 
            this.m_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btn.Location = new System.Drawing.Point(184, 0);
            this.m_btn.Name = "m_btn";
            this.m_btn.Size = new System.Drawing.Size(24, 20);
            this.m_btn.TabIndex = 1;
            this.m_btn.Text = "...";
            this.m_btn.Click += new System.EventHandler(this.m_btn_Click);
            // 
            // m_btnFlush
            // 
            this.m_btnFlush.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFlush.Location = new System.Drawing.Point(208, 0);
            this.m_btnFlush.Name = "m_btnFlush";
            this.m_btnFlush.Size = new System.Drawing.Size(24, 20);
            this.m_btnFlush.TabIndex = 2;
            this.m_btnFlush.Text = "X";
            this.m_btnFlush.Click += new System.EventHandler(this.m_btnFlush_Click);
            // 
            // m_linkControl
            // 
            this.m_linkControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_linkControl.BackColor = System.Drawing.Color.White;
            this.m_linkControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_linkControl.Location = new System.Drawing.Point(0, 0);
            this.m_linkControl.Name = "m_linkControl";
            this.m_linkControl.Size = new System.Drawing.Size(184, 23);
            this.m_linkControl.TabIndex = 3;
            this.m_linkControl.TabStop = true;
            this.m_linkControl.Text = "Lien|100";
            this.m_linkControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_linkControl.Visible = false;
            this.m_linkControl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_linkControl_LinkClicked);
            // 
            // C2iTextBoxSelectionneOld
            // 
            this.Controls.Add(this.m_linkControl);
            this.Controls.Add(this.m_btnFlush);
            this.Controls.Add(this.m_btn);
            this.Controls.Add(this.m_textBox);
            this.Name = "C2iTextBoxSelectionneOld";
            this.Size = new System.Drawing.Size(232, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		public event EventHandler OnChangeLockEdition; 
		public event EventHandler OnSelectedObjectChanged; 
		//------------------------------------------------------------------
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				m_textBox.LockEdition = value;
				if ( m_bHasLink )
				{
					m_linkControl.Visible = value;
					m_linkControl.BringToFront();
				}

				m_btn.Enabled = !value;
				m_btnFlush.Enabled = !value;
				m_btn.Visible = !value;
				m_btnFlush.Visible = !value;

				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}
		
        //------------------------------------------------------------------
		public override string Text
		{
			get
			{
				return m_textBox.Text;
			}
			set
			{
				m_textBox.Text = value;
				m_linkControl.Text = value;
			}
		}

		//------------------------------------------------------------------
		private void SynchroniseTextEtObjet()
		{
			if (m_selectedObject == null)
				Text = TextNull;
			else
				Text = CInterpreteurTextePropriete.GetStringValue( m_selectedObject, m_strPropriete, "");
			m_strOldText = Text;
		}
		//------------------------------------------------------------------
		public CObjetDonnee SelectedObject
		{
			get
			{
				//Nécéssaire pour les grilles: SI le contrôle est dans une grille,
				//on peut interroger sa valeur SelectedObject avec que le lostFocus
				//ne soit détecté par le contrôle
				if ( m_textBox.Focused && m_strOldText != Text )
					SelectObject();
				return m_selectedObject;
			}
			set
			{
				m_selectedObject = value;
				SynchroniseTextEtObjet();
				if (OnSelectedObjectChanged!=null)
					OnSelectedObjectChanged(this, new EventArgs());
				if ( ElementSelectionneChanged != null )
					ElementSelectionneChanged(this, new EventArgs());
                if (value != null && ElementSelectionneChangedNotNull != null)
                    ElementSelectionneChangedNotNull(this, new EventArgs());
            }
		}
		//------------------------------------------------------------------
		private void m_textBox_Enter(object sender, System.EventArgs e)
		{
			m_strOldText = Text;
		}
		//------------------------------------------------------------------
		private void m_textBox_Leave(object sender, System.EventArgs e)
		{
			if (Text!=m_strOldText)
				SelectObject();
		}
		//------------------------------------------------------------------
		private void m_btn_Click(object sender, System.EventArgs e)
		{
			SelectObject();
		}
		//------------------------------------------------------------------
		private void m_btnFlush_Click(object sender, System.EventArgs e)
		{
			SelectedObject = null;
		}

		//------------------------------------------------------------------
		public CFiltreData Filtre
		{
			get
			{
				return m_filtre;
			}
		}

        private bool m_bIsInit = false;

		//------------------------------------------------------------------
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
			if (!bForceInit && m_bIsInit)
				return;
			m_typeFormList = typeFormListe;
			m_typeFormEdition = typeFormEdition;
			//HasLink = m_typeFormEdition != null;
			m_strPropriete = strProprieteAffichee;
			m_filtre = filtre;
            m_bAppliquerFiltreStandard = bAppliquerFiltreStd;
			m_bIsInit = true;
            m_typeObjets = null;
            if (m_typeFormList != null)
            {
                object[] attribs = m_typeFormList.GetCustomAttributes(typeof(ObjectListeur), true);
                if (attribs.Length > 0)
                    m_typeObjets = ((ObjectListeur)attribs[0]).TypeListe;
            }
            if (m_typeObjets == null &&  m_typeFormEdition != null)
            {
                object[] attribs = m_typeFormEdition.GetCustomAttributes(typeof(ObjectEditeur), true);
                if (attribs.Length > 0)
                    m_typeObjets = ((ObjectEditeur)attribs[0]).TypeEdite;
            }
		}
		public OnNewObjetDonneeEventHandler OnNewObjetDonnee;
		//------------------------------------------------------------------
		private void SelectObject()
		{	
			if (m_typeFormList==null)
				return;
			if ( !m_typeFormList.IsSubclassOf( typeof(CFormListeStandard) ) )
				return;
            
			CFormListeStandard frmListe = (CFormListeStandard) Activator.CreateInstance( m_typeFormList, null );
			if (m_filtre!=null)
				frmListe.FiltreRapide = m_filtre;
            if (m_bAppliquerFiltreStandard && frmListe.ListeObjets != null)
            {
                Type typeObjet = frmListe.ListeObjets.TypeObjets;
                CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(typeObjet, new object[] { frmListe.ListeObjets.ContexteDonnee });
                CFiltreData filtre = objet.FiltreStandard;
                if (filtre != null)
                    frmListe.ListeObjets.FiltrePrincipal = filtre;
            }
			string strText = Text == TextNull ? "" : Text;
			CObjetDonnee obj = CFormNavigateurPopupListe.SelectObjectQuickSearch( 
				frmListe, 
				null, 
				strText, 
				CFormNavigateurPopupListe.CalculeContexteUtilisation ( this ),
				new OnNewObjetDonneeEventHandler ( OnNewObjetDonneeFunc ));
			if (obj!=null)
				SelectedObject = obj;
			else
				SynchroniseTextEtObjet();
		}

		public void OnNewObjetDonneeFunc ( object sender, CObjetDonnee nouvelObjet )
		{
			if ( OnNewObjetDonnee != null )
				OnNewObjetDonnee ( sender, nouvelObjet );
		}

		//------------------------------------------------------------------
		private void m_linkControl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CObjetDonneeAIdNumeriqueAuto obj = this.ElementSelectionne as CObjetDonneeAIdNumeriqueAuto;
            if (obj == null)
                return;
			Type typeForm = m_typeFormEdition;
            //if ( typeForm == null && ElementSelectionne != null )
            //    typeForm = CFormFinder.GetRefFormToEdit ( ElementSelectionne.GetType() );
            if (ElementSelectionne != null)
            {
                CFormEditionStandard form;
                if (typeForm != null)
                {
                    form = (CFormEditionStandard)Activator.CreateInstance(
                        typeForm,
                        new object[] { obj });
                    CSc2iWin32DataNavigation.Navigateur.AffichePage(form);
                }
                else
                {
                    CReferenceTypeForm refTypeForm = CFormFinder.GetRefFormToEdit(ElementSelectionne.GetType());
                    if (refTypeForm != null)
                    {
                        form = refTypeForm.GetForm(obj) as CFormEditionStandard;
                        if(form != null)
                            CSc2iWin32DataNavigation.Navigateur.AffichePage(form);
                    }
                }
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

		private void m_textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				SelectObject();
			}
		}

        //--------------------------------------------------
        private void TesteDragDrop(DragEventArgs e)
        {
            if ( m_typeObjets == null )
                return;
            CReferenceObjetDonnee refObj = e.Data.GetData(typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
            if (refObj != null)
            {
                if (m_typeObjets != null && m_typeObjets.IsAssignableFrom(refObj.TypeObjet))
                {
                    e.Effect = DragDropEffects.Link;
                    return;
                }
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void m_textBox_DragDrop(object sender, DragEventArgs e)
        {
            CReferenceObjetDonnee refObj = e.Data.GetData(typeof(CReferenceObjetDonnee)) as CReferenceObjetDonnee;
            if (m_typeObjets != null && m_typeObjets.IsAssignableFrom(refObj.TypeObjet))
            {

                CObjetDonnee objet = refObj.GetObjet(CSc2iWin32DataClient.ContexteCourant);
                if (objet != null)
                {
                    if (m_filtre == null)
                        ElementSelectionne = objet;
                    else
                    {
                        CFiltreData filtre = CFiltreData.GetAndFiltre(m_filtre,
                            objet.GetFiltreCles(objet.GetValeursCles()));
                        CListeObjetsDonnees lst = new CListeObjetsDonnees(objet.ContexteDonnee, m_typeObjets, filtre);
                        if (lst.Count == 1)
                            ElementSelectionne = objet;
                    }
                }
            }
        }

        private void m_textBox_DragEnter(object sender, DragEventArgs e)
        {
            TesteDragDrop(e);
        }

        private void m_textBox_DragOver(object sender, DragEventArgs e)
        {
            TesteDragDrop(e);
        }

        public void SelectAll()
        {
            m_textBox.SelectAll();
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
                    if (m_textBox != null && m_textBox.SelectionStart == m_textBox.Text.Length &&
                        m_textBox.SelectionLength == 0)
                        return false;
                    return true;
                case Keys.Left:
                case Keys.Home:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_textBox != null && m_textBox.SelectionStart == 0 && m_textBox.SelectionLength == 0)
                        return false;
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }
	}
}
