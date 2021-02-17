using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSetSecondes : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSetSecondes()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SetSeconds", 
				typeof(DateTime),
				I.TT(GetType(), "SetSeconds(Date, Seconds)\nRedefines the seconds of a Date|20122"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Minutes|20052"), typeof(int)));
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
				dt = new DateTime (dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, nVal); 
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'SetSeconds' are incorrect|20123"));
			}
			return result;
		}

		
	}
}
