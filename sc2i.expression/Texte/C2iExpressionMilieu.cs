using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMilieu : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMilieu()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Substring", typeof(string), I.TT(GetType(), "Substring(string, start, length)\nReturn the part of the string from the start character over the length indicated|281"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Start|20072"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Length|20070"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion du second en int
			string str1;
			int nStart, nLongueur;
				
			try
			{
				str1 = valeursParametres[0].ToString();
				nStart = (int)valeursParametres[1];
				nLongueur = (int)valeursParametres[2];
			}
			catch
			{
				result.EmpileErreur(I.T("No overload of the function 'SubString' accepts the paramters indicated|283"));
				return result;
			}
			try
			{			
				if ( nStart > str1.Length )
					result.Data = "";
				else if ( nStart+nLongueur >= str1.Length )
					result.Data = str1.Substring(nStart);
				else
					result.Data = str1.Substring(nStart, nLongueur);
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the 'Substring' function|282"));
			}
			return result;
		}

		
	}
}
