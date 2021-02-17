using System;
using System.Collections;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Important : Créer une nouvelle instance de CTrieurListView
	/// à chaque
	/// </summary>
	//-------------------------------------------------------------------------
	public class CTrieurListView : IComparer
	{
		int m_nNumColumn;
		bool m_bAscending = true;
		Type m_typeComparaison = null;
		Hashtable m_mapStringToDate = new Hashtable();
		Hashtable m_mapStringToInt = new Hashtable();
		Hashtable m_mapStringToDouble = new Hashtable();
		Hashtable m_mapStringToString = new Hashtable();
		//-------------------------------------------------------------------------
		public CTrieurListView ( int nNumColumn, bool bAsc )
		{
			m_nNumColumn = nNumColumn;
			m_bAscending = bAsc;
		}
		//-------------------------------------------------------------------------
		public int Compare( object x, object y)
		{
			if ( !(x is ListViewItem) || !(y is ListViewItem) )
				return 0;

			ListViewItem item1, item2;
			item1 = (ListViewItem)x;
			item2 = (ListViewItem)y;

			if ( m_nNumColumn < 0 || m_nNumColumn > item1.SubItems.Count ||
				 m_nNumColumn > item2.SubItems.Count )
				return 0;

			int nResult;
			bool bResult;
			string str1 = item1.SubItems[m_nNumColumn].Text;
			string str2 = item2.SubItems[m_nNumColumn].Text;
			
			if (m_typeComparaison == typeof(string) )
			{
				nResult = str1.CompareTo(str2);
			}
			else if (m_typeComparaison == typeof(DateTime) )
			{
				DateTime date1 = DateTime.Now, date2 = DateTime.Now;
				TestDate(str1, ref date1);
				TestDate(str2, ref date2);
				nResult = date1.CompareTo(date2);
			}
			else if (m_typeComparaison == typeof(double) )
			{
				double dbl1 = 0, dbl2 = 0;
				TestReel(str1, ref dbl1);
				TestReel(str2, ref dbl2);
				nResult = dbl1.CompareTo(dbl2);
			}
			else if (m_typeComparaison == typeof(int) )
			{
				int i1 = 0, i2 = 0;
				TestEntier(str1, ref i1);
				TestEntier(str2, ref i2);
				nResult = i1.CompareTo(i2);
			}
			else
			{
				if ( m_mapStringToString.Contains(str1) )
				{
					nResult = str1.CompareTo(str2);
					if ( !m_bAscending )
						nResult = - nResult;
					return nResult;
				}

				double dble1 = 0, dble2 = 0;
				bResult = ( TestReel(str1,ref dble1) ) && ( TestReel(str2,ref dble2) );
				if ( bResult )
				{
					nResult = dble1.CompareTo(dble2);
					if ( !m_bAscending )
						nResult = - nResult;
					return nResult;
				}

				int n1 = 0, n2 = 0;
				bResult = ( TestEntier(str1,ref n1) ) && ( TestEntier(str2,ref n2) );
				if ( bResult )
				{
					nResult = n1.CompareTo(n2);
					if ( !m_bAscending )
						nResult = - nResult;
					return nResult;
				}

				DateTime dt1 = DateTime.Now, dt2 = DateTime.Now;
				bResult = ( TestDate(str1,ref dt1) ) && ( TestDate(str2,ref dt2) );
				if ( bResult )
				{
					nResult = dt1.CompareTo(dt2);
					if ( !m_bAscending )
						nResult = - nResult;
					return nResult;
				}
			
				string s1 = "", s2 = "";
				bResult = ( TestString(str1,ref s1) ) && ( TestString(str2,ref s2) );
			
				nResult = s1.CompareTo(s2);
			}

			if ( !m_bAscending )
				nResult = - nResult;

			return nResult;

		}
		//-------------------------------------------------------------------------
		public bool TestReel(string chaine, ref double dbl)
		{
			try
			{
				if (m_mapStringToDouble.ContainsKey(chaine))
				{
					dbl = (double) m_mapStringToDouble[chaine];
					return true;
				}
				dbl = Double.Parse(chaine);
				if (!m_mapStringToDouble.ContainsKey(chaine))
					m_mapStringToDouble.Add(chaine,dbl);
				
				m_typeComparaison = typeof(double);
				return true;
			}
			catch
			{
				return false;
			}
		}
		//-------------------------------------------------------------------------
		public bool TestEntier(string chaine, ref int n)
		{
			try
			{
				if (m_mapStringToInt.ContainsKey(chaine))
				{
					n = (int) m_mapStringToInt[chaine];
					return true;
				}
				n = Int32.Parse(chaine);
				if (!m_mapStringToInt.ContainsKey(chaine))
					m_mapStringToInt.Add(chaine,n);
				
				m_typeComparaison = typeof(int);
				return true;
			}
			catch
			{
				return false;
			}
		}
		//-------------------------------------------------------------------------
		public bool TestDate(string chaine, ref DateTime dt)
		{
			try
			{
				if (m_mapStringToDate.ContainsKey(chaine))
				{
					dt = (DateTime) m_mapStringToDate[chaine];
					return true;
				}
				dt = DateTime.Parse(chaine);
				if (!m_mapStringToDate.ContainsKey(chaine))
					m_mapStringToDate.Add(chaine,dt);
				
				m_typeComparaison = typeof(DateTime);
				return true;
			}
			catch
			{
				return false;
			}
		}
		//-------------------------------------------------------------------------
		public bool TestString(string chaine, ref string chaineSortie)
		{
			if (m_mapStringToString.ContainsKey(chaine))
			{
				chaineSortie = (string) m_mapStringToString[chaine];
				return true;
			}
			chaineSortie = chaine;
			if (!m_mapStringToString.ContainsKey(chaine))
				m_mapStringToString.Add(chaine,chaine);

			m_typeComparaison = typeof(string);
			return true;
		}
		//-------------------------------------------------------------------------
		public int NumColonne
		{
			get { return m_nNumColumn;}
			set { m_nNumColumn = value;}
		}
		//-------------------------------------------------------------------------
		public bool Ascending
		{
			get { return m_bAscending;}
			set { m_bAscending  =value;}
		}
		//-------------------------------------------------------------------------
	}
}
