using System;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CDescripteurObjetAHashCode.
	/// </summary>
	[Serializable]
	public class CDescripteurObjetAHashCode
	{
		[NonSerialized]
		private Type m_typeObjet;

		private string m_strTypeObjet;
		private int m_nHashcodeObjet;
		
		////////////////////////////////////////////////////
		public CDescripteurObjetAHashCode()
		{
		}

		////////////////////////////////////////////////////
		public CDescripteurObjetAHashCode( object obj )
		{
			if ( obj == null )
			{
				m_strTypeObjet ="";
			}
			else
			{
				m_typeObjet = obj.GetType();
				m_strTypeObjet = obj.GetType().ToString();
			}
			m_nHashcodeObjet = obj.GetHashCode();
		}

		////////////////////////////////////////////////////
		public Type TypeObjet
		{
			get
			{
				return m_typeObjet;
			}
		}

		////////////////////////////////////////////////////
		public static bool operator == ( CDescripteurObjetAHashCode obj1, object obj2)
		{
			if ( !(obj2 is CDescripteurObjetAHashCode ) )
				obj2 = new CDescripteurObjetAHashCode ( obj2 );
			CDescripteurObjetAHashCode d2;
			d2 = (CDescripteurObjetAHashCode)obj2;
			if ( obj1.m_strTypeObjet != d2.m_strTypeObjet )
				return false;
			if ( obj1.m_nHashcodeObjet != d2.m_nHashcodeObjet )
				return false;
			return true;
		}

		////////////////////////////////////////////////////
		public static bool operator != (CDescripteurObjetAHashCode obj1, object obj2 )
		{
			return !obj1.Equals(obj2);
		}

		////////////////////////////////////////////////////
		public override bool Equals ( object obj )
		{
			return this == obj;
		}

		////////////////////////////////////////////////////
		public override int GetHashCode ()
		{
			return (m_strTypeObjet+m_nHashcodeObjet.ToString(CultureInfo.InvariantCulture)).GetHashCode();
		}
		

				
	}
}
