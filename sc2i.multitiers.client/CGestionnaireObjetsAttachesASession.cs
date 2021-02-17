using System;
using System.Collections;

namespace sc2i.multitiers.client
{
	public delegate void LinkObjectEventHandler ( int nIdSession, IObjetAttacheASession objetAttache );
	/// <summary>
	/// Description résumée de CGestionnaireObjetsAttachesASession.
	/// </summary>
	public class CGestionnaireObjetsAttachesASession
	{
		//Table de ArrayList contenant pour chaque id de session tous les objets attachés
		private static Hashtable m_tableObjetsAttaches = new Hashtable();

		/// ///////////////////////////////////////////////////////////////////////////////
		public static event LinkObjectEventHandler OnAttacheObjet;
		/// ///////////////////////////////////////////////////////////////////////////////
		public static void AttacheObjet ( int nIdSession, IObjetAttacheASession objet )
		{
			ArrayList lst = (ArrayList)m_tableObjetsAttaches[nIdSession];
			if ( lst == null )
			{
				lst = new ArrayList();
				m_tableObjetsAttaches[nIdSession] = lst;
			}
			if (!lst.Contains(objet))
			{
				lst.Add(objet);
				if (OnAttacheObjet != null)
				{
					try
					{
						OnAttacheObjet(nIdSession, objet);
					}
					catch
					{
					}
				}
			}
		}

		public static event LinkObjectEventHandler OnDetacheObjet;
		/// ///////////////////////////////////////////////////////////////////////////////
		public static void DetacheObjet ( int nIdSession, IObjetAttacheASession objet )
		{
			ArrayList lst = (ArrayList)m_tableObjetsAttaches[nIdSession];
			if ( lst != null && lst.Contains(objet))
			{
				lst.Remove(objet);
				if (OnDetacheObjet != null)
				{
					try
					{
						OnDetacheObjet(nIdSession, objet);
					}
					catch
					{
					}
				}
				if ( objet != null && typeof(IDisposable).IsAssignableFrom(objet.GetType() ))
				{
					((IDisposable)objet).Dispose();
				}
			}
		}

		/// ///////////////////////////////////////////////////////////////////////////////
		public static void OnCloseSession ( int nIdSession )
		{
			ArrayList lst = (ArrayList)m_tableObjetsAttaches[nIdSession];
			if(  lst == null )
				return;
			//Crée une copie de la liste, comme ça OnCloseSession peut désinscrire l'élément si on veut
			IObjetAttacheASession[] listeObjetsCopie = (IObjetAttacheASession[])lst.ToArray(typeof(IObjetAttacheASession));
			
			foreach ( IObjetAttacheASession objet in listeObjetsCopie )
			{
				objet.OnCloseSession();
			}
			m_tableObjetsAttaches.Remove(nIdSession);
		}
	}
}
