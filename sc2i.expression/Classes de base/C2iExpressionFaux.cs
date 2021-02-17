using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionFaux : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionFaux()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"False", 
				typeof(bool),
				I.TT(GetType(), "False()\nReturn false value|111"),
				CInfo2iExpression.c_categorieConstantes);
			info.AddDefinitionParametres ( new CInfo2iDefinitionParametres(new Type[0]));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = false;
			return result;
		}

		
	}
	
}
