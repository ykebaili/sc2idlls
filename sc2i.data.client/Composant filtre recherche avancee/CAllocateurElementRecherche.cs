using System;
using System.Collections;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CAllocateurElementRecherche.
	/// </summary>
	public class CAllocateurElementRecherche
	{
		private static ArrayList m_listeTypesElements = new ArrayList();

		public CAllocateurElementRecherche()
		{
			
		}

		public static void RegisterType ( Type tp )
		{
			if ( !m_listeTypesElements.Contains ( tp ) )
				m_listeTypesElements.Add ( tp );
		}

		public static Type[] TypesElements
		{
			get
			{
				return (Type[])m_listeTypesElements.ToArray ( typeof ( Type ) );
			}
		}
		
	}
}
