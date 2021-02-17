using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSleep : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSleep()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Sleep", 
				typeof(int),
				I.TT(GetType(), "Sleep(Duration ms)\nPauses execution for a determined time (in ms)|20143"), 
		CInfo2iExpression.c_categorieDivers );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Duration(ms)|20144"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
            try
            {
                int nTime = Convert.ToInt32(valeursParametres[0]);
                System.Threading.Thread.Sleep(nTime);
            }
            catch { }
			return result;
		}

	}
}
