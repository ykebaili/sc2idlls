using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.win32;
using sc2i.data;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Permet de gérer l'édition d'un sous objet d'édition principal
	/// Le composant est lié à une listViewautoFilled remplie à partir d'une liste passée
	/// par ObjetEdite. Lorsque l'utilisateur séléctionne un élément de la liste,
	/// InitChamp et MAJChamps sont appelés.
	/// </summary>
	public class CGestionnaireEditionSousObjetDonnee : System.ComponentModel.Component, I2iControlEditObject
	{
		private int m_nLastIndexDeListe = -1;
		private ListViewAutoFilled m_wndListe;
		private ListViewItem m_itemEnCours;

		private IEnumerable m_sourceDonnees = null;
		private CObjetDonnee m_objetEnCoursEdition = null;

		private EventHandler m_handlerPasseALaListe;


		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CGestionnaireEditionSousObjetDonnee(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			container.Add(this);
			InitializeComponent();

			m_handlerPasseALaListe = new EventHandler(OnSelectionListeChange);

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		public CGestionnaireEditionSousObjetDonnee()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		//Remplit les champs avec l'objet en cours d'édition
		public event ObjetDonneeResultEventHandler InitChamp;

		//Remplit l'objet en cours d'édition avec les champs
		public event ObjetDonneeResultEventHandler MAJ_Champs;



		private bool m_bIsChangingSelection = false;
		/// ////////////////////////////////////////////////////
		public void ShowErreurValidation( CResultAErreur result )
		{
			Cursor.Current = Cursors.Arrow;
			if ( m_nLastIndexDeListe >= 0 && m_nLastIndexDeListe < m_wndListe.Items.Count )
				m_wndListe.Items[m_nLastIndexDeListe].Selected = true;
			CFormAlerte.Afficher( result);
			
			m_bIsChangingSelection = false;
		}
		/// ////////////////////////////////////////////////////
		private void OnSelectionListeChange ( object sender, EventArgs args )
		{
			if (DesignMode)
				return;
			if ( m_bIsChangingSelection )
				return;
			m_bIsChangingSelection = true;
			if ( m_objetEnCoursEdition != null &&
				m_objetEnCoursEdition.ContexteDonnee.IsEnEdition  
				&& m_objetEnCoursEdition.IsValide()  
				&& MAJ_Champs != null )
			{
				CObjetDonneeResultEventArgs argsMAJ = new CObjetDonneeResultEventArgs ( m_objetEnCoursEdition );
				MAJ_Champs ( this, argsMAJ );
				if ( !argsMAJ.Result )
				{
					Cursor.Current = Cursors.WaitCursor;
					CAppeleurFonctionAvecDelai.CallFonctionAvecDelai ( this, "ShowErreurValidation", 1000, new Object[]{argsMAJ.Result} );
					return;
				}
				if (m_itemEnCours != null && m_itemEnCours.ListView!=null/*supprimé*/)
					m_wndListe.UpdateItemWithObject(m_itemEnCours, m_objetEnCoursEdition );
			}
			m_bIsChangingSelection = false;
			m_objetEnCoursEdition = null;
			if ( m_wndListe.SelectedItems.Count == 1 )
			{
				m_nLastIndexDeListe = m_wndListe.SelectedItems[0].Index;
				m_objetEnCoursEdition = (CObjetDonnee)m_wndListe.SelectedItems[0].Tag;
			}
			if ( InitChamp != null )
				InitChamp ( this, new CObjetDonneeResultEventArgs(m_objetEnCoursEdition));

			if ( m_wndListe.SelectedItems.Count == 1)
				m_itemEnCours = m_wndListe.SelectedItems[0];
			else
				m_itemEnCours = null;
		}

		/// ////////////////////////////////////////////////////
		public bool ShouldSerializeListeAssociee()
		{
			return ListeAssociee != null;
		}

		/// <summary>
		/// ////////////////////////////////////////////////////
		/// </summary>
		public ListViewAutoFilled ListeAssociee
		{
			get
			{
				return m_wndListe;
			}
			set
			{
				if ( m_wndListe != value && m_wndListe != null)
					m_wndListe.SelectedIndexChanged -= m_handlerPasseALaListe;
				if ( m_wndListe != value && value != null )
					value.SelectedIndexChanged += m_handlerPasseALaListe;
				m_wndListe = value;
				m_wndListe.MultiSelect = false;
				m_wndListe.HideSelection = false;
			}
		}

		public void SetObjetEnCoursToNull()
		{
			m_objetEnCoursEdition = null;
		}

		/// ////////////////////////////////////////////////////
		public CObjetDonnee ObjetEnCours
		{
			get
			{
				return m_objetEnCoursEdition;
			}
		}
		/// ////////////////////////////////////////////////////
		public object ObjetEdite
		{
			get
			{
				return m_sourceDonnees;
			}
			set
			{
				if (DesignMode)
					return;
				m_sourceDonnees = (IEnumerable)value;
				if ( m_wndListe != null )
				{
					int nNumSel = m_wndListe.SelectedIndices.Count>0?m_wndListe.SelectedIndices[0]:-1;
					m_wndListe.Remplir ((IList)m_sourceDonnees);
					if ( nNumSel != -1 )
					{
						m_wndListe.SelectedItems.Clear();
						if ( nNumSel < m_wndListe.Items.Count )
							m_wndListe.Items[nNumSel].Selected = true;
						else
							//Rien de sélectionné, initialise la fenêtre avec rien
                            if(InitChamp != null)
							    InitChamp ( this, new CObjetDonneeResultEventArgs(null));
					}
				}
			}
		}

		/// ////////////////////////////////////////////////////
		public CResultAErreur ValideModifs()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_objetEnCoursEdition != null && MAJ_Champs != null )
			{
				CObjetDonneeResultEventArgs args = new CObjetDonneeResultEventArgs ( m_objetEnCoursEdition );
				MAJ_Champs ( this, args);
				result = args.Result;
			}
			return result;
		}



	}
}
