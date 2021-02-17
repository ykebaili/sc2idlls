using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionET : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionET()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(4, "and", typeof(bool), I.TT(GetType(), "Logical operator AND|254"), CInfo2iExpression.c_categorieLogique);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(bool)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(bool)));
			return info;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval(CContexteEvaluationExpression ctx)
		{
			CResultAErreur  result = CResultAErreur.True;
			try
			{
				foreach (C2iExpression parametre in Parametres2i)
				{
					result = parametre.Eval( ctx );
					if (!result)
					{
						result.EmpileErreur(I.T("Error during the evaluation of the @1 expression|250", GetString()));
						return result;
					}
					bool bVal = Convert.ToBoolean(result.Data);
					if (!bVal)
					{
						result.Data = false;
						return result;
					}
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("MyEval of C2iExpresisonOu should never be called|251"));
			return result;
		} 
	}
}
