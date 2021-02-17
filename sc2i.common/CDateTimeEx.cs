using System;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// se substite au type dateTime pour contenir une date de valeur nulle.
	/// </summary>
	[Serializable]
	public class CDateTimeEx : I2iSerializable, IComparable
	{
		DateTime m_dtVal = DateTime.Now;

		public CDateTimeEx()
		{
		}

		/// /////////////////////////////////////////
		/// <param name="dt"></param>
		public CDateTimeEx ( DateTime dtVal )
		{
			DateTimeValue = dtVal;
		}

		/// /////////////////////////////////////////
		public DateTime DateTimeValue
		{
			get
			{
				return m_dtVal;
			}
			set
			{
				m_dtVal = value;
			}
		}

		/// /////////////////////////////////////////
		public static implicit operator DateTime ( CDateTimeEx dt )
		{
			return dt.DateTimeValue;
		}

		/// /////////////////////////////////////////
		public static implicit operator DateTime? (CDateTimeEx dt)
		{
			if (dt == null)
				return null;
			return dt.DateTimeValue;
		}

		/// /////////////////////////////////////////
		public static implicit operator CDateTimeEx ( DateTime dtVal )
		{
			return new CDateTimeEx ( dtVal );
		}
		/// /////////////////////////////////////////
		public static implicit operator CDateTimeEx(string strDate)
		{
			try
			{
				return new CDateTimeEx(DateTime.Parse(strDate));
			}
			catch
			{
				if (strDate.ToLower().Trim() == "now")
					return new CDateTimeEx(DateTime.Now);
				return null;
			}
		}
		/// /////////////////////////////////////////
		public override string ToString()
		{
			return DateTimeValue.ToString(CultureInfo.CurrentCulture);
		}

		/// /////////////////////////////////////////
		public string ToString(string strFormat)
		{
			return DateTimeValue.ToString(strFormat, CultureInfo.CurrentCulture);
		}
		#region Membres de I2iSerializable

		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteDate ( ref m_dtVal );
			return result;
		}

		#endregion

		#region Membres de IComparable

		public int CompareTo(object obj)
		{
			if (obj is DateTime)
				return DateTimeValue.CompareTo ( (DateTime)obj );
            CDateTimeEx dtex = ( obj as CDateTimeEx );
			if (dtex != null)
			{
				return DateTimeValue.CompareTo ( dtex.DateTimeValue );
			}
			return -1;
		}

        public override int GetHashCode()
        {
            return m_dtVal.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CDateTimeEx dte = (obj as CDateTimeEx);
            if (!(obj is DateTime) && (dte == null))
                return false;
			if (obj == null)
				return false;
            if (dte != null)
                return DateTimeValue.Equals(dte.DateTimeValue);
            else
                return DateTimeValue.Equals(obj);
        }

       public static bool operator == (CDateTimeEx dt1, CDateTimeEx dt2)
        {
			if ((object)dt1 == null)
				return (object)dt2 == null;
			return dt1.Equals(dt2);
        }

        public static bool operator != (CDateTimeEx dt1, CDateTimeEx dt2)
        {
            return !(dt1 == dt2);
        }

        public static bool operator < (CDateTimeEx dt1, CDateTimeEx dt2)
        {
			if ( (object)dt1 != null )
				return dt1.CompareTo(dt2) < 0;
			return false;
        }

        public static bool operator > (CDateTimeEx dt1, CDateTimeEx dt2)
        {
			if ( (object)dt1 != null )
				return dt1.CompareTo(dt2) > 0;
			return false;
        }

		#endregion
	}
}

