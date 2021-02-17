using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description r�sum�e de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDernierDuMois : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDernierDuMois()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"LastOfMonth", 
				typeof(DateTime),
				I.TT(GetType(), "LastOfMonth(Month, Year) or LastOfMonth(Date)\nLastOfMonth(Date)\nReturn the last day of the month|195"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres ( new CInfoUnParametreExpression(I.T("Month|20059"), typeof(int) ),
                new CInfoUnParametreExpression ( I.T("Year|20060"), typeof(int)));
            info.AddDefinitionParametres ( new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime) ) );
			return info;
		}
		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DateTime dt = DateTime.Now;
				if ( valeursParametres.Length == 2 )
				{
					int nMois = Convert.ToInt32(valeursParametres[0]);
					int nAnnee = Convert.ToInt32 (valeursParametres[1]);
					dt = new DateTime ( nAnnee, nMois, 1 );
				}
				else
				{
					//PAram�tre date
					DateTime dtParam = (DateTime)valeursParametres[0];
					dt = new DateTime ( dtParam.Year, dtParam.Month, 1 );
				}
				dt = dt.AddMonths ( 1 );
				dt = dt.AddDays(-1);
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during  date conversion : '@1' is not convertible to a date|196",valeursParametres[0].ToString()));
			}
			return result;
		}

		
	}
}
