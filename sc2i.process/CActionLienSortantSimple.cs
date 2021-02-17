using System;

using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionLienSortantSimple.
	/// </summary>
	public abstract class CActionLienSortantSimple : CAction
	{
		/// //////////////////////////////////////////////////
		public CActionLienSortantSimple( CProcess process )
			:base( process )
		{
		}

		/// //////////////////////////////////////////////////
		protected abstract CResultAErreur MyExecute ( CContexteExecutionAction contexte );

		/// //////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction ( CContexteExecutionAction contexte )
		{
			CResultAErreur result = MyExecute ( contexte );
			if ( !result )
			{
				result.EmpileErreur(I.T("@1 action execution error|203",Libelle));
				return result;
			}
			CLienAction[] liens = GetLiensSortantHorsErreur();
			if ( liens.Length < 1 )
				result.Data = null;
			else if ( liens.Length == 1 )
				result.Data = liens[0];
			else 
				result.Data = liens;
			return result;
		}
	}
}
