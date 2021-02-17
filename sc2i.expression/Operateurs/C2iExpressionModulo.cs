using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionModulo : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionModulo()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(2, "%", typeof(int), I.TT(GetType(), "Opérator modulo|262"), CInfo2iExpression.c_categorieMathematiques);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( valeursParametres[0] is int && valeursParametres[1] is int )
			{
				result.Data = (int)valeursParametres[0] % (int)valeursParametres[1];
				return result;
			}
			result.EmpileErreur(I.T("No overload of the function accept the paramters indicated|261"));
			return result;
		}
	}
}
