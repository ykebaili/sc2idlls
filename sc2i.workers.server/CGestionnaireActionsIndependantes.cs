using System;
using System.Collections;

using sc2i.common;
using sc2i.multitiers.server;
using sc2i.workers;

namespace sc2i.workers.server
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CGestionnaireActionsIndependantes : C2iObjetServeur, IGestionnaireActionsIndependantes
	{
		private static Hashtable m_tableDecriptionAction = new Hashtable();

		/// ////////////////////////////////////////////////////////
		public CGestionnaireActionsIndependantes( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////////////
		public static void RegisterAction ( string strId, Type typeObjetAction )
		{
			m_tableDecriptionAction[strId] = typeObjetAction;
		}

		/// ////////////////////////////////////////////////////////
		public CResultAErreur LanceAction ( string strIdAction, object parametre )
		{
			CResultAErreur result = CResultAErreur.True;
			Type tp = (Type)m_tableDecriptionAction[strIdAction];
			if ( tp == null )
			{
				result.EmpileErreur(I.T("Unknown type of action @1|101", strIdAction));
				return result;
			}
			try
			{
				IActionIndependante action = (IActionIndependante)Activator.CreateInstance ( tp, new object[]{IdSession} );
				if ( action == null )
				{
					result.EmpileErreur(I.T("Error during the allocation of a @1 object|102",tp.ToString()));
					return result;
				}
				result = action.RunAction ( parametre );
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}
	}
}
