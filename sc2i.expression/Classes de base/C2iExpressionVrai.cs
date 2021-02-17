using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionVrai : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionVrai()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"True", 
				typeof(bool),
				I.TT(GetType(), "True()\nReturn 'true' value|139"),
				CInfo2iExpression.c_categorieConstantes);
			info.AddDefinitionParametres ( new CInfo2iDefinitionParametres(new Type[0]));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = true;
			return result;
		}

		
	}
	
}
