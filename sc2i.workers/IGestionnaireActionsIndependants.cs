using System;

using sc2i.common;

namespace sc2i.workers
{
	/// <summary>
	/// Description r�sum�e de IGestionnaireActionsIndependances.
	/// </summary>
	public interface IGestionnaireActionsIndependantes
	{
		CResultAErreur LanceAction ( string strIdAction, object parametre );
	}
}
