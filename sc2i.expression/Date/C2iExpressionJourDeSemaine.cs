using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionJourDeSemaine : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionJourDeSemaine()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"WeekDay", 
				typeof(int),
				I.TT(GetType(), "WeekDay(Date)\nReturn the day of the week (Monday = 0)|201"),
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
				result.Data =  ((int)dt.DayOfWeek + 6)%7 ;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'WeekDay' is incorrect|202"));
			}
			return result;
		}

		
	}
}
