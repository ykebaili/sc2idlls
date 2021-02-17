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
	public class C2iExpressionDecimal : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDecimal()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Decimal", 
				typeof(double),
				I.TT(GetType(), "Decimal(value)\nConvert the argument in 'floating numerical' type. The value is 0 if conversion fails. If the value isn't 'numerical', the value is 0|167"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)));
            return info;
		}

		/// //////////////////////////////////////////
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
        {
            CResultAErreur result = CResultAErreur.True;
            double fVal = 0;
            if (valeursParametres[0] is CValeurUnite)
            {
                result.Data = ((CValeurUnite)valeursParametres[0]).Valeur;
                return result;
            }
            try
            {
                fVal = Convert.ToDouble(valeursParametres[0]);
                if (double.IsNaN(fVal))
                    fVal = 0;
            }
            catch
            {
                try
                {
                    string strTexte = valeursParametres[0].ToString();
                    fVal = CUtilDouble.DoubleFromString(strTexte);
                    if (double.IsNaN(fVal))
                        fVal = 0;
                }
                catch { }
            }
            result.Data = fVal;
            return result;
        }		
	}
}
