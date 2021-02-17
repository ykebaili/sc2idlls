using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionIndexOf : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionIndexOf()
		{
		}

	

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "IndexOf", typeof(int), I.TT(GetType(), "IndexOf(Text, required text)\nReturn the index of the first required text occurence find in the text|295"), CInfo2iExpression.c_categorieTexte);
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
				str1 = valeursParametres[0] == null?"":valeursParametres[0].ToString();
                str2 = valeursParametres[1] == null ? "" : valeursParametres[1].ToString();
                if ( str2.Length == 0 )
                {
                    result.Data = -1;
                    return result;
                }
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the 'IndexOf' function accepts the parameters indicated|293"));
				return result;
			}
			try
			{			
				result.Data = str1.IndexOf ( str2 );
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the 'IndexOf' function|294"));
			}
			return result;
		}

		
	}
}
