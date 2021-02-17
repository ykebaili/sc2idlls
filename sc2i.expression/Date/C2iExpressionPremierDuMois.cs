using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionPremierDuMois : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionPremierDuMois()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"FirstOfMonth", 
				typeof(DateTime),
				I.TT(GetType(), "FirstOfMonth(Month, Year)\nFirstOfMonth(Date)\nReturn the first day of the month given|217"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Month|20059"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Year|20060"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)));
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
					//PAramètre date
					DateTime dtParam = (DateTime)valeursParametres[0];
					dt = new DateTime ( dtParam.Year, dtParam.Month, 1 );
				}
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the date conversion : '@1' is not convertible in date|196", valeursParametres[0].ToString()));
			}
			return result;
		}

		
	}
}
