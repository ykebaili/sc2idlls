using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMaintenant : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMaintenant()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Now", 
				typeof(DateTime),
				I.TT(GetType(), "Now()\nReturn the current date|205"),
				CInfo2iExpression.c_categorieDate );
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new Type[]{}));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				result.Data = DateTime.Now;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the date conversion : '@1' is not convertible to a date|196",valeursParametres[0].ToString()));
			}
			return result;
		}

		
	}
}
