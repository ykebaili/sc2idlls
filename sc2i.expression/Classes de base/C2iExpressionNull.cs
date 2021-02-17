using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionNull : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionNull()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Null", 
				typeof(object),
				I.TT(GetType(), "Null()\nReturn null value|125"),
				CInfo2iExpression.c_categorieConstantes);
			info.AddDefinitionParametres ( new CInfo2iDefinitionParametres(new Type[0]));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = null;
			return result;
		}

		
	}
	
}
