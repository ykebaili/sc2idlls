using System;
using System.Collections;

using sc2i.common;
using System.Text;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionRemoveCharsExcept : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionRemoveCharsExcept()
		{
		}

	

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "RemoveCharsExcept", typeof(string), I.TT(GetType(), "RemoveCharsExcept(String, Valid characters)\nReturns the string where all characters not in 'Valid characters' are removed|20147"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Valid characters|20148"), typeof(string)));
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
                result.EmpileErreur(I.T("No overload of the 'RemoveCharsExcept' function accepts the parameters indicated|20149"));
				return result;
			}
			try
			{
                StringBuilder bl = new StringBuilder();
                foreach (char c in str1)
                    if (str2.IndexOf ( c )>=0)
                        bl.Append(c);
                result.Data = bl.ToString();
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the RemoveCharsExcept Function|20150"));
			}
			return result;
		}

		
	}
}
