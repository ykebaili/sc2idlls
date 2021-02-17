using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionGetGUID : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionGetGUID()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"GetGUID", 
				typeof(string),
				I.TT(GetType(), "GetGUID()\nReturn an unique value|112"),
				CInfo2iExpression.c_categorieDivers);
			info.AddDefinitionParametres ( new CInfo2iDefinitionParametres(new Type[0]));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = Guid.NewGuid().ToString();
			return result;
		}

		
	}
	
}
