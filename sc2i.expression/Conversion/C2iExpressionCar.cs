using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionCar : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionCar()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Char", 
				typeof(string),
				I.TT(GetType(), "Char(value)\nReturn the char whose code corresponds to the value|158"), 
		CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres ( new CInfoUnParametreExpression ( I.T("Ascci index|20047"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				int nVal = Convert.ToInt32 ( valeursParametres[0] );
				result.Data = ((char)(nVal))+"";
				return result;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the evaluation of the function 'car'|159"));
				return result;
			}
		}
	}
}
