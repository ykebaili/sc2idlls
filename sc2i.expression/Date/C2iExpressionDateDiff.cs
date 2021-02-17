using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDateDiff : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDateDiff()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"DateDiff", 
				typeof(double),
				I.TT(GetType(), "DateDiff(Date1, Date2, ReturnType)\nSubstract the date2 from date1 and return the difference.\nThe return value depends on ReturnType parameter:\n0 - Weeks\n1 - Days\n2 - Hours\n3 - Minutes\n4 - Seconds|193"),
				CInfo2iExpression.c_categorieDate );

            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date 1|20056"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Date 2|20057"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Return type|20058"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DateTime dt1 = Convert.ToDateTime(valeursParametres[0]);
				DateTime dt2 = Convert.ToDateTime(valeursParametres[1]);
				TimeSpan sp = dt1-dt2;
				double fVal = 0;
				switch ( Convert.ToInt32(valeursParametres[2] ))
				{
					case 0 :
						fVal = sp.TotalDays/7;
						break;
					case 1 :
						fVal = sp.TotalDays;
						break;
					case 2 :
						fVal = sp.TotalHours;
						break;
					case 3 :
						fVal = sp.TotalMinutes;
						break;
					case 4 :
						fVal = sp.TotalSeconds;
						break;
					default :
						fVal = sp.TotalDays;
						break;
				}
				result.Data = fVal;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'DateDiff' are incorrect|194"));
			}
			return result;
		}

		
	}
}
