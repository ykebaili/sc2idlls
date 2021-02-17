using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionToLocal : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionToLocal()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"ToLocalDateTime", 
				typeof(DateTime),
				I.TT(GetType(), "ToLocalDateTime(Date)\nConverts UTC date time to local date time|20140"),
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
				result.Data = dt.ToLocalTime();
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'ToLocalDateTime' is incorrect|20142"));
			}
			return result;
		}

		
	}
}
