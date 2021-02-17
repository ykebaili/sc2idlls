using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionPadRight : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionPadRight()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "PadRight", typeof(string), I.TT(GetType(), "PadRight(String, Character, length)\nSupplements the string on the right by the character indicated|274"), CInfo2iExpression.c_categorieTexte);
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
				result.EmpileErreur(I.T("No overload of the function 'PadRight' accepts the paramters indicated|275"));
				return result;
			}
			try
			{			
				result.Data = str1.PadRight ( n2, str2[0] );
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the function 'PadRight'|276"));
			}
			return result;
		}

		
	}
}
