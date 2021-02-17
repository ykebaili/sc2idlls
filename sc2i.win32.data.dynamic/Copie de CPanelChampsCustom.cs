using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;


using sc2i.data.dynamic;
using sc2i.common;
using Crownwood.Magic.Controls;
using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.multitiers.client;
using System.Text;
using sc2i.data;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelChampsCustom.
	/// </summary>
	public class CPanelChampsCustom : sc2i.win32.common.C2iTabControl, IControlALockEdition
	{
		//Liste des pages initialisées avec l'élément en cours
		private Dictionary<Crownwood.Magic.Controls.TabPage, bool> m_tablePagesInit = new Dictionary<Crownwood.Magic.Controls.TabPage, bool>();

		//Liste des restrictions pour chaque page
		private Dictionary<Crownwood.Magic.Controls.TabPage, ERestriction> m_tablePageToRestriction = new Dictionary<Crownwood.Magic.Controls.TabPage, ERestriction>();

		//Liste des C2iWnd pour chaque page
		private Dictionary<Crownwood.Magic.Controls.TabPage, C2iWnd> m_tablePageTo2iWnd = new Dictionary<Crownwood.Magic.Controls.TabPage, C2iWnd>();
		
		private const string c_strNomTableChampsValeurs = "TableChampsValeurs";
		private const string c_strColChamp = "Champ";
		private const string c_strColChampNom = "Nom";
		private const string c_strColChampValeur = "Valeur";

		
		//Id de formulaire->TabPage
		private Hashtable m_tableIdFormulaireToTabPage = new Hashtable();

		//tabPage->CCreateur2iFormulaire
		private Hashtable m_tableCreateurs = new Hashtable();
		private bool m_bIsLock = false;
		private IElementAChamps m_elementEdite;
		private System.Windows.Forms.ToolTip m_champTooltip;
		private System.ComponentModel.IContainer components;

		private Hashtable m_tableTabsInitialises = new Hashtable();

		public CPanelChampsCustom()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm

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
			this.components = new System.ComponentModel.Container();
			this.m_champTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// CPanelChampsCustom
			// 
			this.Name = "CPanelChampsCustom";
			this.Size = new System.Drawing.Size(720, 408);
			this.SizeChanged += new System.EventHandler(this.CPanelChampsCustom_SizeChanged);
			this.SelectionChanged += new System.EventHandler(this.CPanelChampsCustom_SelectionChanged);
			this.BackColorChanged += new System.EventHandler(this.CPanelChampsCustom_BackColorChanged);
			this.Controls.SetChildIndex(this._rightArrow, 0);
			this.Controls.SetChildIndex(this._leftArrow, 0);
			this.Controls.SetChildIndex(this._closeButton, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// ///////////////////////////////////////////////////
		public IElementAChamps ElementEdite
		{
			get
			{
				return m_elementEdite;
			}
			set
			{
				m_elementEdite = value;
				UpdateOnglets();
			}
		}

		public event EventHandler OnChangeLockEdition; 
		/// ///////////////////////////////////////////////////
		public bool LockEdition
		{
			get
			{
				return m_bIsLock;
			}
			set
			{
				m_bIsLock = value;
				OnMyChangeLockEdition();
			}
		}

		/// ///////////////////////////////////////////////////
		public bool IsEmpty()
		{
			return TabPages.Count == 0;
		}

		/// ///////////////////////////////////////////////////
		public string Titre
		{
			get
			{
				if ( TabPages.Count == 1 )
					return TabPages[0].Title;
				return "Infos complémentaires";
			}
		}
		

		/// ///////////////////////////////////////////////////
		private void OnMyChangeLockEdition()
		{
			foreach ( Crownwood.Magic.Controls.TabPage page in TabPages )
			{
				CCreateur2iFormulaire createur = (CCreateur2iFormulaire)m_tableCreateurs[page];
				if ( createur != null )
					createur.LockEdition = LockEdition;
			}
			if ( OnChangeLockEdition != null )
				OnChangeLockEdition ( this, new EventArgs() );
		}

		/// ///////////////////////////////////////////////////
		public CResultAErreur MAJ_Champs()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_bIsLock )
				return result;

			foreach ( DictionaryEntry entry in m_tableCreateurs )
			{
				CCreateur2iFormulaire createur = (CCreateur2iFormulaire)entry.Value;
				Crownwood.Magic.Controls.TabPage page = (Crownwood.Magic.Controls.TabPage)entry.Key;
				if (m_tablePagesInit.ContainsKey(page) &&
					m_tablePagesInit[page])
				{
					result = createur.MAJ_Champs();
					if (!result)
						return result;
				}
			}

			return result;
		}

		private class CSorterFormulaires : IComparer
		{
			public int Compare(object x, object y)
			{
				if ( x is CFormulaire && y is CFormulaire )
				{
					return ((CFormulaire)x).NumeroOrdre.CompareTo(((CFormulaire)y).NumeroOrdre);
				}
				return 0;
			}

		}
				
		/// ///////////////////////////////////////////////////
		private bool m_bIsInitializing = false;
		public void UpdateOnglets()
		{
			if ( !LockEdition )
			{
				//valide les modifs car on va tout supprimer
				MAJ_Champs();
			}
			m_bIsInitializing = true;
			Hashtable tablePagesToHide = new Hashtable();
			foreach (Crownwood.Magic.Controls.TabPage page in TabPages)
			{
				m_tablePagesInit[page] = false;
				tablePagesToHide[page] = true;
			}

			if ( m_elementEdite == null )
				return;
            
            /* YK : On obtient la session de l'objet en cours d'édition au lieur de la session par défaut */
            CSessionClient session = null;
            IObjetDonneeAIdNumeriqueAuto objetDonnee = m_elementEdite as IObjetDonneeAIdNumeriqueAuto;
            if(objetDonnee != null)
                session = CSessionClient.GetSessionForIdSession(objetDonnee.ContexteDonnee.IdSession);
            else
                session = CSessionClient.GetSessionUnique();

			ArrayList lstFormulaires = new ArrayList(m_elementEdite.GetFormulaires());
			lstFormulaires.Sort(new CSorterFormulaires());
			//Lit les champs liés et leurs valeurs
			StringBuilder bl = new StringBuilder();
			foreach (CFormulaire formulaire in lstFormulaires)
			{
				bl.Append(formulaire.Id);
				bl.Append(",");
			}
			if (bl.Length > 0)
			{
				bl.Remove(bl.Length-1,1);
				CListeObjetsDonnees lst = new CListeObjetsDonnees(((CFormulaire)lstFormulaires[0]).ContexteDonnee, typeof(CFormulaire));
				lst.Filtre = new CFiltreData(CFormulaire.c_champId + " in (" + bl.ToString() + ")");
				lst.AssureLectureFaite();
				lst.ReadDependances("RelationsChamps", "RelationsChamps.Champ", "RelationsChamps.Champ.ListeValeurs");
			}


			foreach ( CFormulaire formulaire in lstFormulaires )
			{
				CRestrictionUtilisateurSurType restriction = new CRestrictionUtilisateurSurType(m_elementEdite.GetType());
				if (session.GetInfoUtilisateur() != null)
				{
					int? nIdVersion = null;
					IObjetAContexteDonnee objetAContexte = m_elementEdite as IObjetAContexteDonnee;
					if (objetAContexte != null)
						nIdVersion = objetAContexte.ContexteDonnee.IdVersionDeTravail;
					restriction = session.GetInfoUtilisateur().GetRestrictionsSurObjet(m_elementEdite, nIdVersion);
				}

				ERestriction restrictionFormulaire = restriction.GetRestriction(formulaire.CleRestriction);

				if ( (restrictionFormulaire & ERestriction.Hide) != ERestriction.Hide )
				{
					Crownwood.Magic.Controls.TabPage page = (Crownwood.Magic.Controls.TabPage)m_tableIdFormulaireToTabPage[formulaire.Id];
					CCreateur2iFormulaire createur =  null;
					
					if (  page == null )
					{
						C2iWndFenetre wnd = formulaire.Formulaire;
						if (wnd != null)
						{
							page = new Crownwood.Magic.Controls.TabPage(wnd.Text);
							page.SuspendLayout();
							if (wnd.Text == "")
								page.Title = formulaire.Libelle;
							TabPages.Add(page);
							m_tableCreateurs[page] = null;
							m_tablePageTo2iWnd[page] = wnd;
							m_tableIdFormulaireToTabPage[formulaire.Id] = page;
							
						}
					}
					else
						createur = (CCreateur2iFormulaire)m_tableCreateurs[page];
			
					if ( page != null  )
					{
						m_tablePageToRestriction[page] = restrictionFormulaire;
						if ( !TabPages.Contains(page))
							TabPages.Add(page);
						tablePagesToHide.Remove(page);
					}
				}
			}

			/*CChampCustom[] champs = ElementEdite.GetChampsHorsFormulaire();
			if ( champs.Length != 0 )
			{
				UpdatePageChamps( nRest );
				if ( m_pageGrid != null )
					tablePagesToHide.Remove(m_pageGrid);
			}*/
			foreach ( Crownwood.Magic.Controls.TabPage pageToDel in tablePagesToHide.Keys )
				TabPages.Remove ( pageToDel );

			if ( TabPages.Count == 1 )
			{
				this.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.HideAlways;
			}
			else
				this.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
			if (SelectedTab != null)
				InitPageWithObjetCourant(SelectedTab);
			m_bIsInitializing = false;
		}

		/// ///////////////////////////////////////////////////
		private void CreateControles(Crownwood.Magic.Controls.TabPage page)
		{
			if (m_tableCreateurs[page] != null || ElementEdite == null)
				return;
			C2iWnd wnd= m_tablePageTo2iWnd[page];
			Panel panel = new Panel();
			panel.BackColor = wnd.BackColor;
			panel.ForeColor = wnd.ForeColor;
			panel.Font = wnd.Font;
			panel.Location = new Point(0, 0);
			panel.Size = wnd.Size;
			panel.AutoScroll = true;
			page.Controls.Add(panel);
			panel.SuspendLayout();
			CCreateur2iFormulaire createur;

            IObjetDonneeAIdNumeriqueAuto objetDonnee = ElementEdite as IObjetDonneeAIdNumeriqueAuto;
            if(objetDonnee != null)
                createur = new CCreateur2iFormulaire(objetDonnee.ContexteDonnee.IdSession, ElementEdite, page);
            else
                createur = new CCreateur2iFormulaire(CSessionClient.GetSessionUnique().IdSession, ElementEdite, page);

			createur.CreateChilds(panel, wnd, m_champTooltip);
			panel.ResumeLayout();
			panel.Size = page.ClientSize;
			m_tableCreateurs[page] = createur;
			page.ResumeLayout();
			panel.Dock = DockStyle.Fill;
		}

		/// ///////////////////////////////////////////////////
		private void InitPageWithObjetCourant(Crownwood.Magic.Controls.TabPage page)
		{
			if (m_tablePagesInit.ContainsKey(page) &&
				m_tablePagesInit[page])
				return;
			if (m_tableCreateurs[page] == null)
				CreateControles(page);
			ERestriction restrictionFormulaire = m_tablePageToRestriction[page];
			CCreateur2iFormulaire createur = (CCreateur2iFormulaire)m_tableCreateurs[page];
			createur.ReadOnly = (restrictionFormulaire & ERestriction.ReadOnly) == ERestriction.ReadOnly;
			createur.LockEdition = m_bIsLock || (restrictionFormulaire & ERestriction.ReadOnly) == ERestriction.ReadOnly;
			if (createur != null)
				createur.ElementEdite = m_elementEdite;
			m_tablePagesInit[page] = true;

		}


		
		

		/// ///////////////////////////////////////////////////
		private void CPanelChampsCustom_BackColorChanged(object sender, System.EventArgs e)
		{
			
		}

		private void CPanelChampsCustom_SizeChanged(object sender, System.EventArgs e)
		{
		
		}

		/// ///////////////////////////////////////////////////
		private void CPanelChampsCustom_SelectionChanged(object sender, EventArgs e)
		{
			Crownwood.Magic.Controls.TabPage page = SelectedTab;
			if (!m_bIsInitializing && page != null)
				InitPageWithObjetCourant(page);
		}

		
		
	
#region IComparer Membres

public int  Compare(object x, object y)
{
 	throw new Exception("The method or operation is not implemented.");
}

#endregion
}
}
