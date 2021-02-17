using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionToUtc : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionToUtc()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"ToUtcDateTime", 
				typeof(DateTime),
                I.TT(GetType(), "ToUtcDateTime(Date)\nConverts local date time to UTC date time|20139"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DateTime dt = Convert.ToDateTime(valeursParametres[0]);
				result.Data = dt.ToUniversalTime();
			}
			catch
			{
                result.EmpileErreur(I.T("The parameter of the function 'ToUtcDateTime' is incorrect|20141"));
			}
			return result;
		}

		
	}
}
