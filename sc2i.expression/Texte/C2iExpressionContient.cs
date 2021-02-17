using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionContient : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionContient()
		{
		}

	

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Contains", typeof(bool), I.TT(GetType(), "Contains(String, Search string)\nReturns true if String contains Search string|20008"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Value|20039"), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			string str1;
			string str2;
			try
			{
				str1 = valeursParametres[0].ToString().ToUpper();
				str2 = valeursParametres[1].ToString().ToUpper();
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the 'Contains' function accepts the parameters indicated|20009"));
				return result;
			}
			try
			{			
				result.Data = str1.Contains ( str2 );
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the Contains Function|20010"));
			}
			return result;
		}

		
	}
}
