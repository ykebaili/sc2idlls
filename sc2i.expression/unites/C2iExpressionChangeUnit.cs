using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionChangeUnit : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionChangeUnit()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"ChangeUnit", 
				typeof(string),
				I.TT(GetType(), "ChangeUnit (value , unit) \nConvert the unit value argument into another unit|20114"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(CValeurUnite)),
                new CInfoUnParametreExpression(I.T("Unit(string)|20111"), typeof(string)));
            return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
            CValeurUnite valeur = valeursParametres[0] as CValeurUnite;
            string strUnite = valeursParametres[1] as string;
            try{
                result.Data = valeur.ConvertTo ( strUnite );
            }
            catch ( Exception e )
            {
                result.EmpileErreur ( new CErreurException(e));
                result.EmpileErreur(I.T("Error during unit conversion|20115"));
            }
            return result;
		}

		
	}
}
