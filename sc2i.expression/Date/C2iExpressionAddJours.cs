using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionAddJours : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionAddJours()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"AddDays", 
				typeof(DateTime),
				I.TT(GetType(), "AddDays(Date, Nb)\nAdd a number of days on a date|183"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Days|20054"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Days|20054"), typeof(double)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DateTime dt = Convert.ToDateTime(valeursParametres[0]);
				double fVal = Convert.ToDouble(valeursParametres[1]);
				dt = dt.AddDays( fVal );
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'AddDays' are incorrect|184"));
			}
			return result;
		}

		
	}
}
