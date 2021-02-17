using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSetAnnee : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSetAnnee()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SetYear", 
				typeof(DateTime),
				I.TT(GetType(), "SetYear(Date, Year)\nRedefines the year of a Date|218"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Year|20060"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				DateTime dt = Convert.ToDateTime(valeursParametres[0]);
				int nVal = Convert.ToInt32(valeursParametres[1]);
				dt = new DateTime ( nVal, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second );
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'SetYear' are incorrect|211"));
			}
			return result;
		}

		
	}
}
