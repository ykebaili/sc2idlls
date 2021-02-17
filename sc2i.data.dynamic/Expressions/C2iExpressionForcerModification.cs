using System;
using System.Collections;


using sc2i.common;

using sc2i.expression;
using sc2i.multitiers.client;
using sc2i.data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionForcerModification: C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionForcerModification()
		{
		}

		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionForcerModification().IdExpression,
				typeof(C2iExpressionForcerModification) );
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SetModified", 
				typeof(bool),
				I.TT(GetType(),"SetModified (object) \n forces the system to considerer that the object has been modified. The modification process will be executed when saving|216"),
				CInfo2iExpression.c_categorieDivers);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new Type[]{typeof(CObjetDonnee)}));
			return info
				;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if (valeursParametres[0] is CObjetDonnee)
					((CObjetDonnee)valeursParametres[0]).ForceChangementSyncSession();
				result.Data = true;
			}
			catch
			{
				result.Data = -1;
			}
			return result;
		}

		
	}
}
