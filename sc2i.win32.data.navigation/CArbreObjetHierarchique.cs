using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.data;

using sc2i.common;

namespace sc2i.win32.data.navigation
{
    public partial class CArbreObjetHierarchique : UserControl, IControlALockEdition
    {
        private CObjetHierarchique m_feuille = null;
        public CArbreObjetHierarchique()
        {
            InitializeComponent();
        }

        public void AfficheHierarchie(CObjetHierarchique elementFeuille)
        {
            m_arbre.Nodes.Clear();
            m_feuille = elementFeuille;
            TreeNode node = FillNodesParent(elementFeuille);
        }

        //--------------------------------------------
        private TreeNode FillNodesParent(IObjetHierarchiqueACodeHierarchique element)
        {
            if (element.ObjetParent == null)
            {
                return null;
            }
            else
            {
                TreeNode node = new TreeNode(element.ObjetParent.Libelle);
                node.Tag = element.ObjetParent;
				TreeNode nodeParent = FillNodesParent(element.ObjetParent);
				if (nodeParent != null)
					nodeParent.Nodes.Add(node);
				else
					m_arbre.Nodes.Add(node);
                if (node.Parent != null)
                    node.Parent.Expand();
                m_arbre.SelectedNode = node;
                return node;
            }
        }

        private void m_btnEdit_Click(object sender, EventArgs e)
        {
            AfficheElement();
        }

        private void AfficheElement()
        {
            TreeNode node = m_arbre.SelectedNode;
            if (node == null)
                return;
			CObjetHierarchique objet = (CObjetHierarchique)node.Tag;
			AfficheElement(objet);
		}


		private void AfficheElement ( IObjetHierarchiqueACodeHierarchique objet )
		{
            //Type typeForm = CFormFinder.GetTypeFormToEdit(objet.GetType());
            //if (typeForm == null || !typeForm.IsSubclassOf(typeof(CFormEditionStandard)))
            //    return;
            //CFormEditionStandard form = (CFormEditionStandard)Activator.CreateInstance(typeForm, new object[] { objet });
            //CTimosApp.Navigateur.AffichePage(form);
            CReferenceTypeForm refTypeForm = CFormFinder.GetRefFormToEdit(objet.GetType());
            if (refTypeForm != null)
            {
                CFormEditionStandard form = refTypeForm.GetForm((CObjetDonneeAIdNumeriqueAuto)objet) as CFormEditionStandard;
                if (form != null)
                    CSc2iWin32DataNavigation.Navigateur.AffichePage(form);
            }

        }

        private void m_arbre_DoubleClick(object sender, EventArgs e)
        {
            AfficheElement();
        }

        //----------------------------------------------------------------------------
        public bool AutoriseReaffectation
        {
            get
            {
                return m_btnReaffecter.Visible;
            }
            set
            {
                m_btnReaffecter.Visible = value;
            }
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

		public event EventHandler OnChangeObjetParent;

        private void m_btnReaffecter_Click(object sender, EventArgs e)
        {
            if (CFormReaffecteObjetHierarchique.ReaffecteObjet(m_feuille))
            {
                AfficheHierarchie(m_feuille);
				if ( OnChangeObjetParent != null )
					OnChangeObjetParent ( this, new EventArgs() );

            }
        }

        private void m_btnUp_Click(object sender, EventArgs e)
        {
            if (m_feuille != null &&
                m_feuille.ObjetParent != null)
            {
                IObjetHierarchiqueACodeHierarchique eltParent = m_feuille.ObjetParent;
				AfficheElement(eltParent);
            }
        }
    }
}
