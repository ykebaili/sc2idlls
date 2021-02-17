using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionLastIndexOf : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionLastIndexOf()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "LastIndexOf", typeof(string), I.TT(GetType(), "LastIndexOf(Text, required text)\nReturn the indec of the last occurence of the required text in the text|292"), CInfo2iExpression.c_categorieTexte);
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
				str1 = valeursParametres[0].ToString();
				str2 = valeursParametres[1].ToString();
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the 'LastIndexOf' function accepts the parameters indicated|290"));
				return result;
			}
			try
			{			
				result.Data = str1.LastIndexOf ( str2 );
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the 'LastIndexOf' function|291"));
			}
			return result;
		}

		
	}
}
