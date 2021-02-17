using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionPadLeft : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionPadLeft()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "PadLeft", typeof(string), I.TT(GetType(), "PadLeft(String, Character, length)\nSupplements the string on the left by the character indicated|279"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Character|20073"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Length|20070"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion du second en int
			string str1,str2;
			int n2;
				
			try
			{
				str1 = valeursParametres[0].ToString();
				str2 = valeursParametres[1].ToString()+" ";
				n2 = (int)valeursParametres[2];
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the function 'PadLeft' accepts the paramters indicated|278"));
				return result;
			}
			try
			{			
				result.Data = str1.PadLeft ( n2, str2[0] );
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the function 'PadLeft'|277"));
			}
			return result;
		}

		
	}
}
