using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSetHeure : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSetHeure()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SetHour", 
				typeof(DateTime),
				I.TT(GetType(), "SetHour(Date, Hour)\nRedefines the hour of a Date|219"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Hour|20063"), typeof(int)));
		
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
				dt = new DateTime (dt.Year, dt.Month, dt.Day, nVal, dt.Minute, dt.Second); 
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'SetHour' are incorrect|212"));
			}
			return result;
		}

		
	}
}
