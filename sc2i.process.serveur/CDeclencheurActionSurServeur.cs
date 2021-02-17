using System;
using System.Collections;

using sc2i.common;
using sc2i.process;
using sc2i.multitiers.server;

namespace sc2i.process.serveur
{
	/// <summary>
	/// Description résumée de CDeclencheurActionSurServeur.
	/// </summary>
	public class CDeclencheurActionSurServeur : C2iObjetServeur, IDeclencheurActionSurServeur
	{
		private static Hashtable m_tableCodeActionToType = new Hashtable();
		private static ArrayList m_listeInfosActions = new ArrayList();

		public CDeclencheurActionSurServeur( int nIdSession )
			:base ( nIdSession )
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		/// ////////////////////////////////////////////
		public CResultAErreur ExecuteAction(string strCodeAction, Hashtable valeursParametres)
		{
			//Alloue l'action
			Type tp = (Type)m_tableCodeActionToType[strCodeAction];
			CResultAErreur result = CResultAErreur.True;
			if ( tp == null )
			{
				result.EmpileErreur(I.T("No action with the code '@1' exists|104",strCodeAction));
				return result;
			}
			IActionSurServeur action = null;
			try
			{
				action = (IActionSurServeur)Activator.CreateInstance(tp );
			}
			catch
			{
				result.EmpileErreur(I.T("Impossible to allocate an action of the @1 type|105",strCodeAction));
				return result;
			}
			return action.Execute ( IdSession, valeursParametres );
		}

		/// ////////////////////////////////////////////
		public static void RegisterAction ( IActionSurServeur action  )
		{
			CInfoActionServeur info = new CInfoActionServeur ( action.CodeType, action.Libelle, action.Description, action.NomsParametres );
			m_listeInfosActions.Add ( info );
			m_tableCodeActionToType[action.CodeType] = action.GetType();
			m_listeInfosActions.Sort ( new InfoActionComparer() );
		}

		/// ////////////////////////////////////////////
		private class InfoActionComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				if ( x is CInfoActionServeur && y is CInfoActionServeur )
				{
					return ((CInfoActionServeur)x).Libelle.CompareTo (((CInfoActionServeur)y).Libelle );
				}
				return 0;
			}
		}


		/// ////////////////////////////////////////////
		public CInfoActionServeur[] ActionsDisponibles
		{
			get
			{
				return (CInfoActionServeur[])m_listeInfosActions.ToArray ( typeof(CInfoActionServeur) );
			}
		}

	}
}
