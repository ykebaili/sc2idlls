using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CComboboxAutoFilled.
	/// </summary>
	public class CComboboxAutoFilled : C2iComboBox
	{
		private class CBinome : IComparable
		{
			private object m_returnValue;
			private string m_strDisplayValue;

			public CBinome ( object ret, string dis )
			{
				m_returnValue = ret;
				m_strDisplayValue = dis;
			}

			public string DisplayValue
			{
				get
				{
					return m_strDisplayValue;
				}
				set
				{
					m_strDisplayValue = value;
				}
			}

			public object ReturnValue
			{
				get
				{
					return m_returnValue;
				}
				set
				{
					m_returnValue = value;
				}
			}

			public int CompareTo ( object obj )
			{
				if ( !(obj is CBinome ) )
					return -1;
				return DisplayValue.CompareTo(((CBinome)obj).DisplayValue);
			}
		}

		private const string c_colReturnValue = "RETURN";
		private const string c_colDisplayValue = "DISPLAY";

		private string m_strProprieteAffichee;
		private bool m_bCanBeNull;
		private string m_strTexteNull = "(empty)";
		private IEnumerable m_listeDonnees;

		private bool m_bTrier = true;
		
		//Indique qu'on est en cours de remplissage
		private bool m_bIsFilling = false;

		//Indique si la liste a été remplie ou non
		private bool m_bIsRemplie = false;

		//Conserve le dernier objet valide sélectionné explicitement par un appel à SelectedValue
		private object m_lastObjetSelectionne = null;

		/// //////////////////////////////////////////////
		public CComboboxAutoFilled()
			:base()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();
		}

		/// //////////////////////////////////////////////
		public string ProprieteAffichee
		{
			get
			{
				return m_strProprieteAffichee;
			}
			set
			{
				m_strProprieteAffichee = value;
			}
		}

		

		/// //////////////////////////////////////////////
		public bool NullAutorise
		{
			get
			{
				return m_bCanBeNull;
			}
			set
			{
				m_bCanBeNull = value;
			}
		}

		/// //////////////////////////////////////////////
		public string TextNull
		{
			get
			{
				return m_strTexteNull;
			}
			set
			{
				m_strTexteNull = value;
				if ( DataSource is ArrayList )
				{
					try
					{
						foreach ( CBinome binome in (ArrayList)DataSource )
						{
							if ( binome.ReturnValue == DBNull.Value )
								binome.DisplayValue = value;
						}
					}
					catch{}
				}
				if ( SelectedValue == DBNull.Value || SelectedValue == null )
					Text = value;
			}
		}

		/// //////////////////////////////////////////////
		public IEnumerable ListDonnees
		{
			get
			{
				return m_listeDonnees;
			}
			set
			{
				m_listeDonnees = value;
				m_bIsRemplie = false;
			}
		}

		/// //////////////////////////////////////////////
		public void Fill ( IEnumerable liste, string m_strProprieteAffichee, bool bNullAutorise )
		{
			ProprieteAffichee = m_strProprieteAffichee;
			NullAutorise = bNullAutorise;
			ListDonnees = liste;
		}

		/// //////////////////////////////////////////////
		public virtual void AssureRemplissage()
		{
			if ( !m_bIsRemplie )
			{
				m_bIsRemplie = true;
				Refill(m_listeDonnees);
			}
		}


		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // CComboboxAutoFilled
            // 
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CComboboxAutoFilled_KeyDown);
            this.DropDown += new System.EventHandler(this.CComboboxAutoFilled_DropDown);
            this.ResumeLayout(false);

		}

		/// //////////////////////////////////////////////
		public bool Tri
		{
			get
			{
				return m_bTrier;
			}
			set
			{
				m_bTrier = value;
			}
		}



		/// //////////////////////////////////////////////
		public override object SelectedValue
		{
			get
			{
				if ( SelectedIndex < 0 || base.SelectedValue == DBNull.Value)
					return null;
				return base.SelectedValue;
			}
			set
			{
				bool bHasChange = value != m_lastObjetSelectionne;
				if (value == null)
				{
					if (SelectedValue != DBNull.Value)
					{
						bHasChange = true;
						base.SelectedValue = DBNull.Value;
					}
					else
						bHasChange = false;
				}
				else
				{
					if (m_bIsRemplie)
					{
						base.SelectedValue = DBNull.Value;
						base.SelectedValue = value;
					}
					else
					{
						if (value != m_lastObjetSelectionne)
						{
							m_lastObjetSelectionne = value;
							//Crée l'objet dans la liste
							ArrayList lst = new ArrayList();
							if (value != null)
								lst.Add(value);
							Refill(lst);
							Refresh();
							base.SelectedValue = DBNull.Value;
							base.SelectedValue = value;
						}
					}
				}
				if ( !m_bIsFilling && bHasChange )
					base.OnSelectedValueChanged(new EventArgs());
				m_lastObjetSelectionne = value;
			}
		}

		/// //////////////////////////////////////////////
		private void Refill( IEnumerable liste )
		{
			if ( m_bIsFilling )
				return;

			m_bIsFilling = true;
			try
			{
				this.SuspendDrawing();
				BeginUpdate();
				object oldValue = SelectedValue;
				DataSource = null;
			
				if ( liste == null )
				{
					EndUpdate();
					m_bIsFilling = false;
					return;
				}
			
				ArrayList lst = new ArrayList();
				if ( NullAutorise )
					lst.Add ( new CBinome(DBNull.Value, TextNull) );
				foreach ( object obj in liste )
				{
					object objRetour = obj;
                    object valeurProp = CInterpreteurTextePropriete.GetValue(obj, ProprieteAffichee);
					lst.Add ( new CBinome(objRetour, valeurProp!= null? valeurProp.ToString():""));
				}
				if ( m_bTrier )
					lst.Sort();
				ValueMember = "ReturnValue";
				DisplayMember = "DisplayValue";
				DataSource = lst;
				ValueMember = "ReturnValue";
				DisplayMember = "DisplayValue";
				EndUpdate();

				object lastSelection = m_lastObjetSelectionne;
				SelectedValue = oldValue;
				if ( SelectedValue == null )
					SelectedValue = lastSelection;
			}
			finally
			{
				m_bIsFilling = false;
				this.ResumeDrawing();
			}
		}

		private void CComboboxAutoFilled_DropDown(object sender, System.EventArgs e)
		{
			if ( !m_bIsRemplie )
			{
				m_bIsRemplie = true;
				Refill ( m_listeDonnees );
			}
			
		}

        private void CComboboxAutoFilled_KeyDown(object sender, KeyEventArgs e)
        {
            if (!m_bIsRemplie && (
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.End ||
                e.KeyCode == Keys.Home ||
                e.KeyCode == Keys.PageUp ||
                e.KeyCode == Keys.PageDown))
            {
                m_bIsRemplie = true;
                Refill(m_listeDonnees);
            }
        }

	}
}
