using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionArrondi : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionArrondi()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Round", 
				typeof(double),
				I.TT(GetType(), "Round(Value, precision)\nReturn the value with the float 'precision'|257"),
				CInfo2iExpression.c_categorieMathematiques );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(int)),
                new CInfoUnParametreExpression ( I.T("Decimals number|20061"), typeof(int) ));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Decimals number|20061"), typeof(int)));
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
				if ( val is int )
					result.Data = Sc2iMath.RoundUp ( (int)val, nPrecision );
				else
					result.Data = Sc2iMath.RoundUp ( Convert.ToDouble ( val ), nPrecision );
			}
			catch
			{
				result.EmpileErreur(I.T("The paramters of the 'Round' function are incorrect|258"));
			}
			return result;
		}
	}
}
