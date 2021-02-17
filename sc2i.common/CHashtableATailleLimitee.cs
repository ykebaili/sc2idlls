using System;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Hashtable avec un nombre limité d'éléments
	/// Lors de l'ajout d'un nouvel élément, 
	/// si la taille dépasse la limite,
	/// l'élément le plus ancien est supprimé
	/// </summary>
	public class CHashtableATailleLimitee
	{
		private int m_nMaxSize = 30;
		private Hashtable m_table = new Hashtable();
		private ArrayList m_lstCles = new ArrayList();
		
		/// //////////////////////////////////////////
		public CHashtableATailleLimitee( )
		{
			
		}

		/// //////////////////////////////////////////
		public CHashtableATailleLimitee( int nMaxSize )
		{
			m_nMaxSize = nMaxSize;
		}

		/// //////////////////////////////////////////
		public void AddElement ( object cle, object valeur )
		{
			if ( cle == null )
				return;
			if ( m_table.Contains(cle) )
				return;
			m_table[cle] = valeur;
			m_lstCles.Add ( cle );
			if ( m_lstCles.Count > m_nMaxSize )
			{
				object oldKey = m_lstCles[0];
				m_table.Remove(oldKey);
				m_lstCles.RemoveAt(0);
			}
		}

		/// //////////////////////////////////////////
		public object this[object cle]
		{
			get
			{
				if ( cle == null )
					return null;
				return m_table[cle];
			}
		}
	}
}
