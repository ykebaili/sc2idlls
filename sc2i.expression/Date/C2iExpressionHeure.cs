using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionHeure : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionHeure()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Hour", 
				typeof(int),
				I.TT(GetType(), "Hour(Date)\nReturn the hour of the Date/Time|197"),
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
				result.Data = dt.Hour;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of the function 'Hour' are incorrect|198"));
			}
			return result;
		}

		
	}
}
