using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace sc2i.common
{
	public abstract class CEnumALibelle<TmonTypeEnum> : IEnumALibelle, IComparable where TmonTypeEnum : struct, IComparable 
	{
		/// //////////////////////////////////////////////////////
		private TmonTypeEnum m_valeurEnum;

		/// //////////////////////////////////////////////////////
		protected CEnumALibelle(TmonTypeEnum statut)
		{
			m_valeurEnum = statut;
		}

		/// //////////////////////////////////////////////////////
		public override string ToString()
		{
			return Libelle;
		}

		public abstract string Libelle { get;}

		/// //////////////////////////////////////////////////////
		[DynamicField("Code")]
		public int CodeInt
		{
			get
			{
				return Convert.ToInt32(m_valeurEnum, CultureInfo.InvariantCulture);
			}
			set
			{
				m_valeurEnum = (TmonTypeEnum)(object)value;// Convert.ChangeType(value, m_statut.GetType());
			}
		}

		/// //////////////////////////////////////////////////////
		public TmonTypeEnum Code
		{
			get
			{
				return m_valeurEnum;
			}
			set
			{
				m_valeurEnum = value;
			}
		}

		/// //////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            return m_valeurEnum.GetHashCode();
        }

		public override bool Equals ( object obj )
		{
			if (object.ReferenceEquals(obj, null))
				return false;
			if (obj != null && obj.GetType() == GetType())
				return ((IEnumALibelle)obj).CodeInt.Equals(CodeInt);
			return false;
		}

		
		public static TmonTypeEnum[] ValeursEnumPossibles
		{
			get
			{
				return (TmonTypeEnum[]) Enum.GetValues(typeof(TmonTypeEnum));
				//return (TmonTypeEnum[])lstVals.ToArray(typeof(TmonTypeEnum));
			}
		}
		public static CEnumALibelle<TmonTypeEnum>[] GetValeursEnumPossibleInEnumALibelle(Type tp)
		{
			List<CEnumALibelle<TmonTypeEnum>> lstVals = new List<CEnumALibelle<TmonTypeEnum>>();
			foreach (TmonTypeEnum val in ValeursEnumPossibles)
				lstVals.Add((CEnumALibelle<TmonTypeEnum>)Activator.CreateInstance(tp, new object[] { val }));	
			return lstVals.ToArray();
		}

		public static implicit operator TmonTypeEnum ( CEnumALibelle<TmonTypeEnum> valeur)
		{
			return valeur.Code;
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
				return -1;
			if (obj.GetType() != GetType())
				return -1;
			return ((IEnumALibelle)obj).CodeInt.CompareTo(CodeInt);
		}

        public static bool operator == (CEnumALibelle<TmonTypeEnum> eal1, CEnumALibelle<TmonTypeEnum> eal2)
        {
			if (object.ReferenceEquals(eal1, null))
				return object.ReferenceEquals(eal2, null);
            return eal1.Equals(eal2);
        }

        public static bool operator !=(CEnumALibelle<TmonTypeEnum> eal1, CEnumALibelle<TmonTypeEnum> eal2)
        {
            return !(eal1 == eal2);
        }

        public static bool operator <(CEnumALibelle<TmonTypeEnum> eal1, CEnumALibelle<TmonTypeEnum> eal2)
        {
            return eal1.CompareTo(eal2) < 0;
        }

        public static bool operator >(CEnumALibelle<TmonTypeEnum> eal1, CEnumALibelle<TmonTypeEnum> eal2)
        {
            return eal1.CompareTo(eal2) > 0;
        }
	}
}
