using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionLundiDeSemaine : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionLundiDeSemaine()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"WeekMonday", 
				typeof(DateTime),
				I.TT(GetType(), "WeekMonday(week, year) or WeekMonday(Date)\nReturn the date of the monday of the week|203"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres ( new CInfoUnParametreExpression ( I.T("Week|20062"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Year|20060"), typeof(int) ));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return -1;//Nombre de paramètres variables
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( valeursParametres.Length == 1 )
				{
					DateTime dt = (DateTime)valeursParametres[0];
					result.Data = CUtilDate.LundiDeSemaine ( CUtilDate.GetWeekNum(dt), CUtilDate.GetYearOfWeek(dt) );
					return result;
				}
				int nSemaine = Convert.ToInt32(valeursParametres[0]);
				int nAnnee = Convert.ToInt32 (valeursParametres[1]);
				result.Data = CUtilDate.LundiDeSemaine ( nSemaine, nAnnee );
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the date conversion : '@1' is not convertible to a date|196", valeursParametres[0].ToString()));
			}
			return result;
		}

		
	}
}
