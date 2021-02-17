using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace sc2i.win32.common
{
	/// <summary>
	/// Pensez à overrider le Equals sur les objets que vous lui passez pour que 
	/// le control puisse identifier les éléments non selectionnés dans la liste
	/// des éléments possibles
	/// </summary>
	[ToolboxBitmap("..\\Icones\\vidoirDevidoir.PNG")]
	public partial class CCtrlVidoirDevidoir : UserControl, IControlALockEdition
	{

		/// <summary>
		/// Retourne une Liste d'objet contenant les elements selectionnés
		/// </summary>
		public event EventHandler ChangementSelection;


		public CCtrlVidoirDevidoir()
		{
			InitializeComponent();
		}

		public void Initialiser(IEnumerable elementsSelec, IEnumerable elementsPossibles, params string[] strPropsAffichees)
		{
			
			if (elementsPossibles == null)
				return;
			List<object> elementsSelecFinaux = new List<object>();
			if(elementsSelec != null)
				foreach (object o in elementsSelec)
					elementsSelecFinaux.Add(o);

			List<object> elementsPossiblesFinaux = new List<object>();
			foreach (object o in elementsPossibles)
				elementsPossiblesFinaux.Add(o);

			Initialiser(elementsSelecFinaux, elementsPossiblesFinaux, strPropsAffichees);
		}

		public void Initialiser(List<object> elementsSelec, List<object> elementsPossibles, params string[] strPropsAffichees)
		{
			if(elementsPossibles == null)
				return;

			List<object> elementsNonSelec = new List<object>();
			foreach (object elePoss in elementsPossibles)
			{
				bool bFind = false;
				foreach (object eleSelec in elementsSelec)
					if(elePoss.Equals(eleSelec))
					{
						bFind = true;
						break;
					}

				if(!bFind)
					elementsNonSelec.Add(elePoss);
			}


			InitialiserColonnes(m_glSelec, strPropsAffichees);
			InitialiserColonnes(m_glNonSelec, strPropsAffichees);

			m_glSelec.ListeSource = elementsSelec;
			m_glNonSelec.ListeSource = elementsNonSelec;
		}

		private void InitialiserColonnes(GlacialList gl, params string[] strProps)
		{
			gl.Columns.Clear();
			foreach (string str in strProps)
			{
				GLColumn col = new GLColumn();
				col.Propriete = str;
				col.Text = str;
				gl.Columns.Add(col);
			}
		}

	
		public List<object> ElementsSelectionnes
		{
			get
			{
				List<object> elements = new List<object>();
				foreach (object ele in m_glSelec.ListeSource)
					elements.Add(ele);
				return elements;
			}
		}

		#region IControlALockEdition Membres
		private bool m_bLockEdition;
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				m_btnSelectAll.Visible = !m_bLockEdition;
				m_btnSelect.Visible = !m_bLockEdition;
				m_btnUnselect.Visible = !m_bLockEdition;
				m_btnUnselectAll.Visible = !m_bLockEdition;

				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion

		private void selectionChanged(object sender, EventArgs e)
		{
			if (m_bBlocSelectItem)
				return;
			GlacialList glConcerne = sender == m_glSelec ? m_glSelec : m_glNonSelec;

			if (glConcerne.SelectedItems.Count > 0)
			{
				GlacialList glNonConcerne = sender != m_glSelec ? m_glSelec : m_glNonSelec;
				glNonConcerne.ClearSelection();
				glNonConcerne.Invalidate();
			}

			if (ChangementSelection != null)
			{
				List<object> eles = GetElementsSelectionnes(glConcerne);
				ChangementSelection(eles, new EventArgs());
			}
			
		}
		private void Transvaser()
		{
			GlacialList glConcerne = m_glSelec.SelectedItems.Count > 0 ? m_glSelec : m_glNonSelec;

			if (glConcerne.SelectedItems.Count > 0)
			{
				GlacialList glNonConcerne = glConcerne != m_glSelec ? m_glSelec : m_glNonSelec;
				int nStartIndex = glNonConcerne.ListeSource.Count;
				List<object> eles = GetElementsSelectionnes(glConcerne);
				foreach (object ele in eles)
				{
					glConcerne.ListeSource.Remove(ele);
					glNonConcerne.ListeSource.Add(ele);
				}

				glConcerne.ListeSource = glConcerne.ListeSource;
				glNonConcerne.ListeSource = glNonConcerne.ListeSource;
				glConcerne.Invalidate();
				glNonConcerne.Invalidate();
				glConcerne.ClearSelection();

				m_bBlocSelectItem = true;
				for(int n = nStartIndex; n < glNonConcerne.ListeSource.Count; n++)
					glNonConcerne.SelectItem(n);
				m_bBlocSelectItem = false;
				selectionChanged(glNonConcerne, new EventArgs());
			}
		}
		private void selectionDoubleClic(object sender, MouseEventArgs e)
		{
			if (LockEdition)
				return;
			Transvaser();
		
		}

		private List<object> GetElementsSelectionnes(GlacialList gl)
		{
			List<object> eles = new List<object>();
			foreach (object ele in gl.SelectedItems)
				eles.Add(ele);
			return eles;
		}
		private int m_nMargeBoutons = 10;
		private void ActualPositionBoutons()
		{
			m_btnSelectAll.Location = new Point(m_splitContainer.SplitterDistance, m_lblSelected.Height + m_nMargeBoutons);
			m_btnSelectAll.Refresh();
			m_btnSelect.Location = new Point(m_splitContainer.SplitterDistance, m_lblSelected.Height + (m_nMargeBoutons * 2) + m_btnSelectAll.Height);
			m_btnSelect.Refresh();
			m_btnUnselect.Location = new Point(m_splitContainer.SplitterDistance, Height - ((m_nMargeBoutons * 2) + m_btnUnselectAll.Height + m_btnUnselect.Height));
			m_btnUnselect.Refresh();
			m_btnUnselectAll.Location = new Point(m_splitContainer.SplitterDistance, Height - (m_nMargeBoutons + m_btnUnselectAll.Height));
			m_btnUnselectAll.Refresh();
		}

		private void CCtrlVidoirDevidoir_SizeChanged(object sender, EventArgs e)
		{
			ActualPositionBoutons();
		}

		private void m_splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			ActualPositionBoutons();
		}

		private bool m_bBlocSelectItem = false;
		private void m_btnSelectAll_Click(object sender, EventArgs e)
		{
			if (m_glNonSelec.ListeSource.Count == 0)
				return;
			m_bBlocSelectItem = true;
			m_glSelec.ClearSelection();
			for (int n = 0; n < m_glNonSelec.ListeSource.Count; n++)
				m_glNonSelec.SelectItem(n);
			m_bBlocSelectItem = false;

			Transvaser();
		}

		private void m_btnSelect_Click(object sender, EventArgs e)
		{
			if (m_glNonSelec.SelectedItems.Count == 0)
				return;
			Transvaser();
		}

		private void m_btnUnselect_Click(object sender, EventArgs e)
		{
			if (m_glSelec.SelectedItems.Count == 0)
				return;
			Transvaser();
		}

		private void m_btnUnselectAll_Click(object sender, EventArgs e)
		{
			if (m_glSelec.ListeSource.Count == 0)
				return;
			m_bBlocSelectItem = true;
			m_glNonSelec.ClearSelection();
			for (int n = 0; n < m_glSelec.ListeSource.Count; n++)
				m_glSelec.SelectItem(n);
			m_bBlocSelectItem = false;

			Transvaser();
		}
	}
}
