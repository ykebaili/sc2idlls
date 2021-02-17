using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDroite : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDroite()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Right", typeof(string), I.TT(GetType(), "Right(Text, length)\nReturn the right part of the text over the length indicated|297"), CInfo2iExpression.c_categorieTexte);
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
				result.EmpileErreur(I.T("No overload of the 'Right' function accepts the parameters indicated|298"));
				return result;
			}
			try
			{			
				if ( n2 >= str1.Length )
					result.Data = str1;
				else
					result.Data = str1.Substring(str1.Length-n2);
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the 'Right' function|301"));
			}
			return result;
		}

		
	}
}
