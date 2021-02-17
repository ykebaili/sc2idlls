using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionAnneeDeSemaine : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionAnneeDeSemaine()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"YearOfWeek", 
				typeof(int),
				I.TT(GetType(), "YearOfWeek(Date)\nRetrn the year of the week of the date|189"),
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
				result.Data	= CUtilDate.GetYearOfWeek( dt );
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'YearOfWeek' are incorrect|190"));
			}
			return result;
		}

		
	}
}
