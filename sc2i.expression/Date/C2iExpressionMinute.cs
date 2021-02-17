using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMinute : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMinute()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Minute", 
				typeof(int),
				I.TT(GetType(), "Minute(Date)\nReturn the minutes of the data Date/Hour|204"),
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
				result.Data = dt.Minute;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'Minute' is incorrect|206"));
			}
			return result;
		}

		
	}
}
