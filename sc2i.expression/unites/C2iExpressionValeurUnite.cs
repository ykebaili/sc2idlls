using System;
using System.Collections;

using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionValeurUnite : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
        public C2iExpressionValeurUnite()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"U", 
				typeof(CValeurUnite),
				I.TT(GetType(), "U(value;string)\ncreate a value with unity (string)|20109"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(
                new CInfoUnParametreExpression(I.T("Value|20110"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Unit(string)|20111"), typeof(string)));
            info.AddDefinitionParametres(
                new CInfoUnParametreExpression(I.T("Value|20110"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Unit(string)|20111"), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
                double? fVal = null;
                if (valeursParametres[0] is double)
                    fVal = (double)valeursParametres[0];
                if (fVal == null)
                {
                    if (valeursParametres[0] is string)
                    {
                        try
                        {
                            fVal = CUtilDouble.DoubleFromString((string)valeursParametres[0]);
                        }
                        catch { }
                    }
                }
                if (fVal == null)
                {
                    try
                    {
                        fVal = Convert.ToDouble(valeursParametres[0]);
                    }
                    catch { }
                }
                if (fVal == null)
                {
                    result.EmpileErreur("Bad parameter for U, first parameter should be a real number|20112");
                    return result;
                }
                result.Data = new CValeurUnite(fVal.Value, valeursParametres[1].ToString());
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'U' are incorrect|20113"));
			}
			return result;
		}

		
	}
}
