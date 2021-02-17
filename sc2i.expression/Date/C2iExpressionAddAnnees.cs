using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionAddAnnees : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionAddAnnees()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"AddYears", 
				typeof(DateTime),
				I.TT(GetType(), "AddYears(Date, Nb)\nAdd a number of years on a date|181"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Date|20050"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Years|20053"), typeof(int)));
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
				dt = dt.AddYears( nVal );
				result.Data = dt;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameters of function 'AddYears' are incorrect|182"));
			}
			return result;
		}

		
	}
}
