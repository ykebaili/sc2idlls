using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionNumSemaine : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionNumSemaine()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"WeekNum", 
				typeof(int),
				I.TT(GetType(), "WeekNum(Date)\nReturn the week number of the date|216"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DateTime dt = Convert.ToDateTime(valeursParametres[0]);
				result.Data	= CUtilDate.GetWeekNum ( dt );
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'WeekNum' is incorrect|210"));
			}
			return result;
		}

		
	}
}
