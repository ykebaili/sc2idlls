using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionLog : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionLog()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"log", 
				typeof(double),
				I.TT(GetType(), "log(Value)\nReturn the natural logarithm for the value|20151"),
				CInfo2iExpression.c_categorieMathematiques );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)),
                new CInfoUnParametreExpression ( I.T("Decimals number|20061"), typeof(double) ));
			return info;
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				object val = valeursParametres[0];
				int nPrecision = (int)valeursParametres[1];
                result.Data = Math.Log ( Convert.ToDouble ( val ), nPrecision );
			}
			catch
			{
				result.EmpileErreur(I.T("The paramters of the 'log' function are incorrect|20152"));
			}
			return result;
		}
	}
}
