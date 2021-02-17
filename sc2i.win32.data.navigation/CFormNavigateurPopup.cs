using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;

using sc2i.win32.data.navigation;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.common;


namespace sc2i.win32.data.navigation
{
	public class CFormNavigateurPopup : sc2i.win32.navigation.CFormNavigateur
	{
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button m_btnSuivant;
		private System.Windows.Forms.Button m_btnPrecedent;
		private System.Windows.Forms.Button m_btnHome;
		private System.Windows.Forms.ImageList m_imageListButtons;
        protected sc2i.win32.common.CExtStyle m_ExtStyle1;
		private Panel m_panelVersion;
		private Label m_lblVersion;
		private Timer m_timerClignote;
		private System.ComponentModel.IContainer components = null;

        private CContexteDonnee m_contexteDonneePushedAsCurrent = null;


        //Ne pas appeler en direct
		internal CFormNavigateurPopup()
			:base()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
		}

		public CFormNavigateurPopup ( IFormNavigable pageAccueil )
			:base(pageAccueil)
		{
			InitializeComponent();
			CContexteDonnee contexte = null;
			if (pageAccueil is IFormAContexteDonnee)
			{
				contexte = ((IFormAContexteDonnee)pageAccueil).ContexteDonnee;
				contexte.OnChangeVersionDeTravail += new EventHandler(contexte_OnChangeVersionDeTravail);
				SetVersionEnCours(contexte);
                m_contexteDonneePushedAsCurrent = contexte;
			}
		}

		void contexte_OnChangeVersionDeTravail(object sender, EventArgs e)
		{
			if ( sender is CContexteDonnee )
			{
				SetVersionEnCours ( (CContexteDonnee)sender );
			}
		}

		private void SetVersionEnCours(CContexteDonnee contexte)
		{
			if (contexte == null || contexte.IdVersionDeTravail == null)
			{
				m_timerClignote.Stop();
				m_panelVersion.Visible = false;
			}
			else
			{
				CVersionDonnees version = new CVersionDonnees(contexte);
				if (version.ReadIfExists((int)contexte.IdVersionDeTravail))
				{
					m_lblVersion.Text = version.Libelle;
					m_panelVersion.Visible = true;
					m_timerClignote.Start();
				}
			}
            
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

		#region Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormNavigateurPopup));
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_panelVersion = new System.Windows.Forms.Panel();
            this.m_lblVersion = new System.Windows.Forms.Label();
            this.m_btnSuivant = new System.Windows.Forms.Button();
            this.m_imageListButtons = new System.Windows.Forms.ImageList(this.components);
            this.m_btnPrecedent = new System.Windows.Forms.Button();
            this.m_btnHome = new System.Windows.Forms.Button();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_timerClignote = new System.Windows.Forms.Timer(this.components);
            this.panel3.SuspendLayout();
            this.m_panelVersion.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelForMainTabs
            // 
            this.m_panelForMainTabs.Location = new System.Drawing.Point(0, 40);
            this.m_panelForMainTabs.Size = new System.Drawing.Size(658, 408);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelForMainTabs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelForMainTabs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.m_panelVersion);
            this.panel3.Controls.Add(this.m_btnSuivant);
            this.panel3.Controls.Add(this.m_btnPrecedent);
            this.panel3.Controls.Add(this.m_btnHome);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(658, 40);
            this.m_ExtStyle1.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 6;
            // 
            // m_panelVersion
            // 
            this.m_panelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelVersion.BackColor = System.Drawing.Color.Red;
            this.m_panelVersion.Controls.Add(this.m_lblVersion);
            this.m_panelVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_panelVersion.Location = new System.Drawing.Point(184, 9);
            this.m_panelVersion.Name = "m_panelVersion";
            this.m_panelVersion.Size = new System.Drawing.Size(290, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelVersion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelVersion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelVersion.TabIndex = 8;
            this.m_panelVersion.Visible = false;
            // 
            // m_lblVersion
            // 
            this.m_lblVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblVersion.ForeColor = System.Drawing.Color.Black;
            this.m_lblVersion.Location = new System.Drawing.Point(0, 0);
            this.m_lblVersion.Name = "m_lblVersion";
            this.m_lblVersion.Size = new System.Drawing.Size(290, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblVersion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblVersion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblVersion.TabIndex = 6;
            this.m_lblVersion.Text = "VERSION V1";
            this.m_lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblVersion.Visible = false;
            // 
            // m_btnSuivant
            // 
            this.m_btnSuivant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSuivant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSuivant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(108)))));
            this.m_btnSuivant.ImageIndex = 1;
            this.m_btnSuivant.ImageList = this.m_imageListButtons;
            this.m_btnSuivant.Location = new System.Drawing.Point(48, 2);
            this.m_btnSuivant.Name = "m_btnSuivant";
            this.m_btnSuivant.Size = new System.Drawing.Size(34, 28);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnSuivant, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnSuivant, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSuivant.TabIndex = 1;
            this.m_btnSuivant.Click += new System.EventHandler(this.m_btnSuivant_Click);
            // 
            // m_imageListButtons
            // 
            this.m_imageListButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListButtons.ImageStream")));
            this.m_imageListButtons.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.m_imageListButtons.Images.SetKeyName(0, "");
            this.m_imageListButtons.Images.SetKeyName(1, "");
            this.m_imageListButtons.Images.SetKeyName(2, "");
            this.m_imageListButtons.Images.SetKeyName(3, "");
            this.m_imageListButtons.Images.SetKeyName(4, "");
            this.m_imageListButtons.Images.SetKeyName(5, "");
            // 
            // m_btnPrecedent
            // 
            this.m_btnPrecedent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnPrecedent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPrecedent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(108)))));
            this.m_btnPrecedent.ImageIndex = 0;
            this.m_btnPrecedent.ImageList = this.m_imageListButtons;
            this.m_btnPrecedent.Location = new System.Drawing.Point(8, 2);
            this.m_btnPrecedent.Name = "m_btnPrecedent";
            this.m_btnPrecedent.Size = new System.Drawing.Size(34, 28);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnPrecedent, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnPrecedent, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPrecedent.TabIndex = 0;
            this.m_btnPrecedent.Click += new System.EventHandler(this.m_btnPrecedent_Click);
            // 
            // m_btnHome
            // 
            this.m_btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(108)))));
            this.m_btnHome.ImageIndex = 2;
            this.m_btnHome.ImageList = this.m_imageListButtons;
            this.m_btnHome.Location = new System.Drawing.Point(96, 2);
            this.m_btnHome.Name = "m_btnHome";
            this.m_btnHome.Size = new System.Drawing.Size(34, 28);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnHome, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnHome, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnHome.TabIndex = 2;
            this.m_btnHome.Click += new System.EventHandler(this.m_btnHome_Click);
            // 
            // m_timerClignote
            // 
            this.m_timerClignote.Interval = 500;
            this.m_timerClignote.Tick += new System.EventHandler(this.m_timerClignote_Tick);
            // 
            // CFormNavigateurPopup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(658, 448);
            this.Controls.Add(this.panel3);
            this.Name = "CFormNavigateurPopup";
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CFormNavigateurPopup_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CFormNavigateurPopup_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFormNavigateurPopup_FormClosing);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.m_panelForMainTabs, 0);
            this.panel3.ResumeLayout(false);
            this.m_panelVersion.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		//---------------------------------------------------------------------------
		private void m_btnPrecedent_Click(object sender, System.EventArgs e)
		{
			AffichePagePrecedente();
		}
		//---------------------------------------------------------------------------
		private void m_btnSuivant_Click(object sender, System.EventArgs e)
		{
			AffichePageSuivante();
		}
		//---------------------------------------------------------------------------
		private void m_btnHome_Click(object sender, System.EventArgs e)
		{
			AffichePageAccueil();
		}
		
		//---------------------------------------------------------------------------
		public static void Show( IFormNavigable frm )
		{
			Show ( frm, typeof(CFormNavigateurPopup), System.Windows.Forms.FormWindowState.Normal  );
		}

        //---------------------------------------------------------------------------
        public static void Show(IFormNavigable frm, IWin32Window owner)
        {
            Show(frm, typeof(CFormNavigateurPopup), System.Windows.Forms.FormWindowState.Normal, owner);
        }
		//---------------------------------------------------------------------------
		public static void Show( IFormNavigable frm, System.Windows.Forms.FormWindowState windowState )
		{
			Show ( frm, typeof(CFormNavigateurPopup), windowState );
		}
		
		//---------------------------------------------------------------------------
		public static void Show ( IFormNavigable frm, Type typeNavigateur )
		{
			Show ( frm, typeNavigateur, System.Windows.Forms.FormWindowState.Normal );
		}
		//---------------------------------------------------------------------------
		public static void Show ( IFormNavigable frm, Type typeNavigateur, System.Windows.Forms.FormWindowState windowState )
        {
            Show ( frm, typeNavigateur, windowState, null );
        }

        //---------------------------------------------------------------------------
        public static void Show(IFormNavigable frm, Type typeNavigateur, System.Windows.Forms.FormWindowState windowState, IWin32Window owner)
		{
			CFormNavigateur oldNavigateur = CSc2iWin32DataNavigation.Navigateur;
			
			CFormNavigateurPopup navigateur = (CFormNavigateurPopup) Activator.CreateInstance(typeNavigateur, new object[] {frm} );
			CSc2iWin32DataNavigation.PushNavigateur( navigateur );
            try
            {
                navigateur.WindowState = windowState;
                //LE 5/10/2007, stef
                //Il y avait ici : navigateur.TopMost = true, mais ça pose des problèmes,
                //parce que quand un navigateur popup ouvre un navigateur popup, ils se 
                //passent les uns sous les autres.
                //tentative avec bringtofront
                navigateur.BringToFront();
                CFormEditionStandard formEdition = frm as CFormEditionStandard;
                if (formEdition != null)
                {
                    if (formEdition.IsEnEditionPourNouvelElement)
                    {
                        formEdition.AfterAnnulationModification += new ObjetDonneeEventHandler(CloseOnAnnuleOuValideModification);
                        formEdition.AfterValideModification += new ObjetDonneeEventHandler(CloseOnAnnuleOuValideModification);
                    }
                }
                if (navigateur.m_contexteDonneePushedAsCurrent != null)
                    CSc2iWin32DataClient.PushContexteCourant(navigateur.m_contexteDonneePushedAsCurrent);
                try
                {
                    DialogResult result = navigateur.ShowDialog(owner);
                }
                finally
                {
                    if (navigateur.m_contexteDonneePushedAsCurrent != null)
                        CSc2iWin32DataClient.PopContexteCourant(navigateur.m_contexteDonneePushedAsCurrent);
                }

            }
            catch { }
            finally
            {
                CSc2iWin32DataNavigation.PopNavigateur();
            }
		}

        //--------------------------------------------------------------------------------------------------------------------
        public static void ShowNonModale(IFormNavigable frm, FormWindowState windowState)
        {
            CFormNavigateurPopup navigateur = 
                (CFormNavigateurPopup)Activator.CreateInstance(typeof(CFormNavigateurPopup), new object[] { frm });
            try
            {
                navigateur.WindowState = windowState;
                CFormEditionStandard formEdition = frm as CFormEditionStandard;
                
                if (formEdition != null)
                {
                    if (formEdition.IsEnEditionPourNouvelElement)
                    {
                        formEdition.AfterAnnulationModification += new ObjetDonneeEventHandler(CloseOnAnnuleOuValideModification);
                        formEdition.AfterValideModification += new ObjetDonneeEventHandler(CloseOnAnnuleOuValideModification);
                    }
                    formEdition.HideBtnExtractList();
                    formEdition.BoutonAjouterVisible = false;
                    formEdition.BoutonSupprimerVisible = false;
                }
                navigateur.m_contexteDonneePushedAsCurrent = CSc2iWin32DataClient.ContexteCourant;
                navigateur.Show();
            }
            catch { }

        }

        static void CloseOnAnnuleOuValideModification(object sender, CObjetDonneeEventArgs args)
        {
            CFormEditionStandard frmEdition = sender as CFormEditionStandard;
            if (frmEdition.IsEnEditionPourNouvelElement)
            {
                try
                {
                    frmEdition.Navigateur.Close();
                }
                catch
                {
                }
            }
        }

        public delegate CResultAErreur EditeElementDelegate(CObjetDonnee objet, bool bInNewOnglet, string strCodeForm);
        private EditeElementDelegate m_funcEditeElement = null;

        private CResultAErreur EditeElementInterne(CObjetDonnee objet, bool bInNewOnglet, string strCodeFormEdition)
        {
            CResultAErreur result = CResultAErreur.True;

            CReferenceTypeForm refTypeForm = null;
            if (strCodeFormEdition != string.Empty)
                refTypeForm = sc2i.win32.data.navigation.CFormFinder.GetRefFormToEdit(objet.GetType(), strCodeFormEdition);
            else
                refTypeForm = sc2i.win32.data.navigation.CFormFinder.GetRefFormToEdit(objet.GetType());

            if (refTypeForm == null)
            {
                result.EmpileErreur(I.T("The system is not able to edit elements of type '@1'|1076", objet.GetType().ToString()));
                return result;
            }
            try
            {
                CFormEditionStandard form = refTypeForm.GetForm((CObjetDonneeAIdNumeriqueAuto)objet) as CFormEditionStandard;
                if (bInNewOnglet)
                    AffichePageDansNouvelOnglet(form);
                else
                    AffichePage(form);
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

        //---------------------------------------------------------------------------
        public override CResultAErreur EditeElement(object objet, bool bNewOnglet, string strCodeFormulaire)
        {
            CResultAErreur result = CResultAErreur.True;
            if (objet is CObjetDonnee)
            {
                if ( m_funcEditeElement == null )
                    m_funcEditeElement = new EditeElementDelegate(EditeElementInterne);
                IAsyncResult asRes = BeginInvoke(m_funcEditeElement, objet, bNewOnglet, strCodeFormulaire);
                while (!asRes.IsCompleted)
                    System.Threading.Thread.Sleep(100);
            }
            return CResultAErreur.True;
        }
       
		//---------------------------------------------------------------------------
		private void CFormNavigateurPopup_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            if (PageAccueil!=null && PageEnCours==null)
				AffichePage ( PageAccueil );
            
		}

		//------------------------------------------------------------------------------------
		public override string TitreFenetreEnCours
		{
			get
			{
				return Text;
			}
			set
			{
				Text = value;
			}
		}

		private void m_timerClignote_Tick(object sender, EventArgs e)
		{
			m_lblVersion.Visible = !m_lblVersion.Visible;
		}

        private void CFormNavigateurPopup_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void CFormNavigateurPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
		
	}
}

