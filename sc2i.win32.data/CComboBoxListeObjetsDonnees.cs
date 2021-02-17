using System;
using System.Collections;
using System.Reflection;

using sc2i.win32.common;
using sc2i.data;
using sc2i.common;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CComboBoxListeObjetDonnee.
	/// </summary>
	public class CComboBoxListeObjetsDonnees : CComboboxAutoFilled, ISelectionneurElementListeObjetsDonnees
	{
		protected object m_lastValue = -1; //pas null car pb pour comparer si SelectedValue=null
		protected ISelectionneurElementListeObjetsDonnees m_selectionneurParent;
		protected string m_strPropParentListeObjets;
		protected CObjetDonnee m_lastObjetSelectionneDansParent = null;
		//------------------------------------------------------------------------------
		public CComboBoxListeObjetsDonnees()
		{
			InitializeComponent();
		}
		//------------------------------------------------------------------------------
		private void InitializeComponent()
		{
			// 
			// CComboBoxListeObjetsDonnees
			// 
			this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SelectedValueChanged += new System.EventHandler(this.CComboBoxListeObjetsDonnees_SelectedValueChanged);
            this.SelectionChangeCommitted += new EventHandler(CComboBoxListeObjetsDonnees_SelectionChangeCommitted);

		}

		//------------------------------------------------------------------------------
		public ISelectionneurElementListeObjetsDonnees SelectionneurParent
		{
			get
			{
				return m_selectionneurParent;
			}
			set
			{
				m_selectionneurParent = value;
				if (m_selectionneurParent!= null)
				{
					m_selectionneurParent.ElementSelectionneChanged += new EventHandler(RemplirFromParent);
				}
			}
		}

		//------------------------------------------------------------------------------
		public override void AssureRemplissage()
		{
			if ( m_selectionneurParent !=  null )
				RemplirFromParent ( this, new EventArgs() );
			base.AssureRemplissage();
		}

		//------------------------------------------------------------------------------
		private void RemplirFromParent(object sender, EventArgs e)
		{
			if (DesignMode || SelectionneurParent.IsUpdating())
				return;
			CObjetDonnee obj = m_selectionneurParent.ElementSelectionne;
			if (obj == null)
			{
				//this.NullAutorise = true;
				this.SelectedValue = null;
				this.Enabled = false;
				return;
			}
			//this.NullAutorise = false;
			this.Enabled = true;
			if ( m_lastObjetSelectionneDansParent != obj )
			{
				this.ListDonnees = (IEnumerable) CInterpreteurTextePropriete.GetValue(obj, m_strPropParentListeObjets );
				this.SelectedValue = null;
			}
			m_lastObjetSelectionneDansParent = obj;
		}
		//------------------------------------------------------------------------------
		public string ProprieteParentListeObjets
		{
			get
			{
				return m_strPropParentListeObjets;
			}
			set
			{
				m_strPropParentListeObjets = value;
			}
		}
		//------------------------------------------------------------------------------
		public event EventHandler ElementSelectionneChanged;
		//------------------------------------------------------------------------------
		public CObjetDonnee ElementSelectionne
		{
			get
			{
				if (!(this.SelectedValue is CObjetDonnee))
					return null;
				return (CObjetDonnee) this.SelectedValue;
			}
			set
			{
				this.SelectedValue = value;
                if (ElementSelectionneChanged != null)
                    ElementSelectionneChanged(this, new EventArgs());
			}
		}

		//------------------------------------------------------------------------------
		public CResultAErreur Init ( Type typeObjets, string strProprieteAffichee, bool bForceInit )
		{
			return Init(typeObjets, null, strProprieteAffichee, bForceInit);
		}
		//------------------------------------------------------------------------------
		public CResultAErreur Init ( Type typeObjets, CFiltreData filtre, string strProprieteAffichee, bool bForceInit )
		{
			CResultAErreur result = CResultAErreur.True;

			CListeObjetsDonnees liste = new CListeObjetsDonnees ( 
					CSc2iWin32DataClient.ContexteCourant,
					typeObjets );
			liste.Filtre = filtre;

			return Init(liste, strProprieteAffichee, bForceInit);
		}
		//------------------------------------------------------------------------------
		public CResultAErreur Init ( CListeObjetsDonnees liste, string strProprieteAffichee, bool bForceInit )
		{
			if (this.Items.Count != 0 && !bForceInit)
				return CResultAErreur.True;

			ProprieteAffichee = strProprieteAffichee;
			ListDonnees = liste;
			return CResultAErreur.True;
		}
		//------------------------------------------------------------------------------
		private void CComboBoxListeObjetsDonnees_SelectedValueChanged(object sender, System.EventArgs e)
		{
            //if (m_lastValue == SelectedValue && Items.Count == 0)
            //    return;
            //if ( ElementSelectionneChanged != null )
            //    ElementSelectionneChanged(sender, e);
            //m_lastValue = SelectedValue;
		}
		//------------------------------------------------------------------------------
        void CComboBoxListeObjetsDonnees_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_lastValue == SelectedValue && Items.Count == 0)
                return;
            if (ElementSelectionneChanged != null)
                ElementSelectionneChanged(sender, e);
            m_lastValue = SelectedValue;
        }

        //------------------------------------------------------------------------------
        public bool WantsInputKey(System.Windows.Forms.Keys keyData, bool dataGridViewWantsInputKey)
        {
            return true;
        }

	}
}
