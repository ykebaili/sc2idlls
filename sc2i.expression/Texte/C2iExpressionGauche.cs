using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionGauche : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionGauche()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Left", typeof(string), I.TT(GetType(), "Left(Text, length)\nReturn the left part of the text over the length indicated|296"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Length|20070"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion du second en int
			string str1;
			int n2;
				
			try
			{
				str1 = valeursParametres[0].ToString();
				n2 = (int)valeursParametres[1];
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the 'Left' function accepts the parameters indicated|299"));
				return result;
			}
			try
			{			
				if ( n2 >= str1.Length )
					result.Data = str1;
				else
					result.Data = str1.Substring(0, n2);
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the 'Left' function|300"));
			}
			return result;
		}

		
	}
}
