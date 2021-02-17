using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSetJours : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSetJours()
		{
		}

	

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SetDay", 
				typeof(DateTime),
				I.TT(GetType(), "SetDay(Date, Days)\nRedefines the days of a date|220"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Day|20064"), typeof(int)));			
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
				dt = new DateTime (dt.Year, dt.Month, nVal, dt.Hour, dt.Minute, dt.Second); 
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'SetDay' are incorrect|213"));
			}
			return result;
		}

		
	}
}
