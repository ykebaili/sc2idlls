using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using sc2i.win32.common;
using sc2i.formulaire;
using System.Collections;
using sc2i.expression;
using sc2i.drawing;
using sc2i.common;

namespace sc2i.formulaire.win32.editor
{
	public partial class CPanelEditionFullFormulaire : UserControl, IControlALockEdition
	{
		public CPanelEditionFullFormulaire()
		{
			InitializeComponent();
		}


		public void AddAllLoadedAssemblies()
		{
			m_wndListeControles.AddAllLoadedAssemblies();
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

		//---------------------------------
		public void Init(Type typeEdite, object entiteEditee, object formulaireEdite, IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			TypeEdite = typeEdite;
            WndEditee = (C2iWnd) formulaireEdite;
            EntiteEditee = entiteEditee;
			FournisseurProprietes = fournisseurProprietes;
			AddAllLoadedAssemblies();
			m_wndListeControles.SetTypeEdite(typeEdite);
			if ( WndEditee != null )
				WndEditee.SetTypeElementEdite(typeEdite);

		}

		public C2iWnd WndEditee
		{
			get
			{
				return (C2iWnd)m_panelFormulaire.ObjetEdite;
			}
			set
			{
                if (!DesignMode)
                {
                    m_panelFormulaire.ObjetEdite = value;
                    if (value != null)
                        value.SetTypeElementEdite(TypeEdite);
                    m_ctrlStructure.Init(value);
                }
			}
		}

		//---------------------------------
		public object EntiteEditee
		{
			get
			{
				return m_panelFormulaire.EntiteEditee;
			}
			set
			{
				m_panelFormulaire.EntiteEditee = value;
			}
		}


        public CPanelEditionObjetGraphique Editeur
        {
            get
            {
                return m_panelFormulaire;
            }

        }

		//---------------------------------
		public IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_panelFormulaire.FournisseurProprietes;
			}
			set
			{
				m_panelFormulaire.FournisseurProprietes = value;
			}
		}

		//---------------------------------
        
        public Type TypeEdite
		{
			get
			{
				return m_panelFormulaire.TypeEdite;
			}
			set
			{
				m_panelFormulaire.TypeEdite = value;
                UpdateListeControles(value);
				if (WndEditee != null)
					WndEditee.SetTypeElementEdite(value);
			}
		}

        //---------------------------------
        private Type m_lastTypeForControles = null;
        private void UpdateListeControles(Type tp)
        {
            if (tp == null)
                return;
            if (m_lastTypeForControles == null || tp != m_lastTypeForControles)
            {
                m_wndListeControles.SetTypeEdite(tp);
                m_lastTypeForControles = tp;
            }
        }

		//---------------------------------
		private void m_panelFormulaire_SelectionChanged(object sender, EventArgs e)
		{
			if (m_panelFormulaire.Selection.Count == 1)
			{
				m_gridProprietes.SelectedObject = m_panelFormulaire.Selection[0];
				m_panelEvenements.Init(m_gridProprietes.SelectedObject as C2iWnd,
					FournisseurProprietes);
                C2iWnd wnd = m_panelFormulaire.Selection[0] as C2iWnd;
                if (wnd != null)
                    UpdateListeControles ( wnd.GetObjetAnalysePourFils(m_panelFormulaire.TypeEdite).TypeAnalyse );
			}
			else
			{
                UpdateListeControles(m_panelFormulaire.TypeEdite);
				ArrayList lst = new ArrayList();
				foreach (C2iWnd element in m_panelFormulaire.Selection)
					lst.Add(element);
				m_gridProprietes.SelectedObjects = lst.ToArray();
				m_panelEvenements.Init(null, FournisseurProprietes);
			}
            UpdateSelectionStructure();
			
		}

		//---------------------------------
		private void m_gridProprietes_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			m_panelFormulaire.Refresh(); ;
		}

		private void CPanelEditionFullFormulaire_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
            m_ctrlStructure.Editeur = Editeur;
		}

		//-------------------------------------------------------
		/// <summary>
		/// Permet d'ajouter des objets au Serialzer de clonage
		/// </summary>
		/// <param name="type"></param>
		/// <param name="objet"></param>
		public void AddObjetForClonerSerializer(Type type, object objet)
		{
			m_panelFormulaire.AddObjetForClonerSerializer(type, objet);
		}

        private void m_btnModeSelection_CheckedChanged(object sender, EventArgs e)
        {
            SelectModeSouris();
        }

        private void SelectModeSouris()
        {
            if (m_btnModeSelection.Checked)
                m_panelFormulaire.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Selection;
            if (m_btnModeZoom.Checked)
                m_panelFormulaire.ModeSouris = CPanelEditionObjetGraphique.EModeSouris.Zoom;
        }

        private void m_panelFormulaire_ElementMovedOrResized(object sender, EventArgs e)
        {
            if (m_tabControl.SelectedTab == m_pageStructure)
            {
                foreach (C2iWnd wnd in m_panelFormulaire.Selection)
                    m_ctrlStructure.Update(wnd);
            }
            UpdateSelectionStructure();
        }

        private void m_panelFormulaire_AfterRemoveObjetGraphique(object sender, EventArgs args)
        {
            if (m_tabControl.SelectedTab == m_pageStructure)
            {
                m_ctrlStructure.Update((C2iWnd)m_panelFormulaire.ObjetEdite);
            }
            UpdateSelectionStructure();
        }

        private bool m_panelFormulaire_AfterAddElements(List<I2iObjetGraphique> objs)
        {
            if (m_tabControl.SelectedTab == m_pageStructure)
            {
                foreach ( C2iWnd wnd in objs )
                    m_ctrlStructure.Update(wnd);
            }
            UpdateSelectionStructure();
            return true;
        }

        private void m_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tabControl.SelectedTab == m_pageStructure)
            {
                m_ctrlStructure.Update((C2iWnd)Editeur.ObjetEdite);
                UpdateSelectionStructure();
            }
        }

        private void UpdateSelectionStructure()
        {
            if (m_tabControl.SelectedTab == m_pageStructure)
            {
                if (Editeur.Selection.Count >= 1)
                    m_ctrlStructure.SelectWnd((C2iWnd)Editeur.Selection[Editeur.Selection.Count - 1]);
            }
        }

        private void m_panelFormulaire_FrontBackChanged(object sender, EventArgs e)
        {
            if (m_tabControl.SelectedTab == m_pageStructure)
            {
                m_ctrlStructure.Update((C2iWnd)Editeur.ObjetEdite);
            }
        }

        private void m_btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Form|*.form|All files|*.*|20017");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                C2iWndFenetre fenetre = new C2iWndFenetre();
                
                CResultAErreur result = CSerializerObjetInFile.ReadFromFile(fenetre, "FORM", dlg.FileName);
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
                else
                    Init(TypeEdite, EntiteEditee, fenetre, FournisseurProprietes);
            }
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = I.T("Form|*.form|All files|*.*|20017");
            if ( !(WndEditee is C2iWndFenetre) )
            {
                CFormAlerte.Afficher (I.T("Unavailable|20018"));
                return;
            }
            if ( dlg.ShowDialog() == DialogResult.OK )
            {
                CResultAErreur  result = CSerializerObjetInFile.SaveToFile ( WndEditee, "FORM", dlg.FileName );
            }
        }

        
	}
}
