using System;
using System.Collections;

using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionEntier : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionEntier()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Int", 
				typeof(int),
				I.TT(GetType(), "Int (value) \nConvert the argument into an integer. The value is 0 if conversion fails|175"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres ( new CInfoUnParametreExpression ( I.T("Value[20039"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
            if (valeursParametres[0] is CValeurUnite)
            {
                result.Data = (int)((CValeurUnite)valeursParametres[0]).Valeur;
                return result;
            }
			int nVal = 0;
			try
			{
				nVal = Convert.ToInt32 (  valeursParametres[0] );				
			}
			catch
			{
			}
			result.Data = nVal;
			return result;
		}
	}
}
