using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CExtModeEdition.
	/// </summary>
	public enum TypeModeEdition
	{
		Autonome,
		EnableSurEdition,
		DisableSurEdition
	}

	[ProvideProperty("ModeEdition", typeof(Control))]
	public class CExtModeEdition : Component, IExtenderProvider
	{
		private bool m_bModeEdition = false;

		private Hashtable m_tableValeurs = new Hashtable();

		public CExtModeEdition()
		{
		}

		/////////////////////////////////////////
		public bool CanExtend ( object extendee )
		{
			if ( extendee is Control )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/////////////////////////////////////////
		public void SetModeEdition ( object extendee, TypeModeEdition type )
		{
			m_tableValeurs[extendee] = type;
		}

		/////////////////////////////////////////
		public TypeModeEdition GetModeEdition ( object extendee ) 
		{
			object obj = m_tableValeurs[extendee];
			if ( obj != null && obj is TypeModeEdition )
				return (TypeModeEdition)obj;
			return TypeModeEdition.Autonome;
		}


        /////////////////////////////////////////
		public bool ShouldSerializeModeEdition()
		{
			return true;
		}


		public void ResetModeEdition()
		{}

		/////////////////////////////////////////
		public event EventHandler ModeEditionChanged;

		/////////////////////////////////////////
		[DefaultValue(false)]
		public bool ModeEdition 
		{
			get
			{
				return m_bModeEdition;
			}
			set
			{
				m_bModeEdition = value;
				ArrayList lst = new ArrayList(m_tableValeurs.Keys);
				foreach ( object obj in lst )
				{
					if ( obj is Control )
					{
						TypeModeEdition mode = GetModeEdition ( obj );
						switch ( mode )
						{
							case TypeModeEdition.DisableSurEdition :
								if ( obj is IControlALockEdition )
									((IControlALockEdition)obj).LockEdition = m_bModeEdition;
								else
									((Control)obj).Enabled = !m_bModeEdition;
								break;
							case TypeModeEdition.EnableSurEdition :
								if ( obj is IControlALockEdition )
									((IControlALockEdition)obj).LockEdition = !m_bModeEdition;
								else
									((Control)obj).Enabled = m_bModeEdition;
								break;
						}
					}
				}
				if (ModeEditionChanged != null)
					ModeEditionChanged(this, null);
			}
		}
	}
}
