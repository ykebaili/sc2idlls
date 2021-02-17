using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionNon : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionNon()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Not", typeof(Boolean), I.TT(GetType(), "Not(value)\nLogical NOT Function|253"), CInfo2iExpression.c_categorieLogique);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(bool)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				bool bVal = Convert.ToBoolean(valeursParametres[0]);
				result.Data = !bVal;
			}
			catch
			{
				result.EmpileErreur(I.T("Impossible to convert the parameter into Boolean for the 'Not' function|252"));
			}
			return result;
		}

		
	}
}
