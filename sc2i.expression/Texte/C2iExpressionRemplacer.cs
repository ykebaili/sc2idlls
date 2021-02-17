using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionRemplacer : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionRemplacer()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Replace", typeof(string), I.TT(GetType(), "Replace(String, required, replacement)\nReplaces the required value by the replacement value in the string|271"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Replaced text|20074"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Replacing text|20075"), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			string str1;
			string str2;
			string str3;
				
			try
			{
				str1 = valeursParametres[0].ToString();
				str2 = valeursParametres[1].ToString();
				str3 = valeursParametres[2].ToString();
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the 'Replace' function accepts the parameters indicated|273"));
				return result;
			}
			try
			{			
				result.Data = str1.Replace ( str2, str3 );
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the Replace Function|272"));
			}
			return result;
		}

		
	}
}
