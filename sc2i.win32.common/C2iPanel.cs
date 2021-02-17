using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iPanel.
	/// </summary>
	public class C2iPanel : System.Windows.Forms.Panel, IControlALockEdition
	{
		private bool m_bLockEdition = false;

		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public C2iPanel(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();

		}

		public C2iPanel()
		{
			InitializeComponent();
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
		public event EventHandler OnChangeLockEdition; 

		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				RecursiveLockEditionChilds ( m_bLockEdition, this );
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		//////////////////////////////////////////////////////////////
		private void RecursiveLockEditionChilds ( bool b, Control ctrl )
		{
			foreach ( Control fils in ctrl.Controls )
			{
				try
				{
					if ( fils is IControlALockEdition )
						((IControlALockEdition)fils).LockEdition = b;
					else
						fils.Enabled = !b;
				}
				catch {}
			}
		}
	}
}
