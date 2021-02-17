using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionAnneeEtSemaine : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionAnneeEtSemaine()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"YearAndWeek", 
				typeof(string),
				I.TT(GetType(), "YearAndWeek(Date)\nReturn the year and week of the date separated by a slash|191"),
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
				result.Data	= CUtilDate.GetYearOfWeek( dt )+"/"+
					CUtilDate.GetWeekNum(dt).ToString().PadLeft(2,'0');
			}
			catch
			{
				result.EmpileErreur(I.T("The paramters of the function 'YearAndWeek' are incorrect|192"));
			}
			return result;
		}

		
	}
}
