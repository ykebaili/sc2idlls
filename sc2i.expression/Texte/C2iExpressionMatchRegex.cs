using System;
using System.Collections;
using System.Text.RegularExpressions;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMatchRegex : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMatchRegex()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"MatchRegEx", 
				typeof(Boolean),
				I.TT(GetType(), "MatchRegEx(Text, ExpReg)\nReturn true if the text passed in paramter checks the regular expression ExpReg|284"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Regular expression|20071"), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			string strChaine, strRegEx;
			strChaine = valeursParametres[0].ToString();
			strRegEx = valeursParametres[1].ToString();
			try
			{
				Regex ex = new Regex(strRegEx, RegexOptions.IgnoreCase);
				result.Data = ex.IsMatch(strChaine);
			}
			catch 
			{
				result.EmpileErreur(I.T("Error in the regular expression '@1'|285",strRegEx));
			}
			return result;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = base.VerifieParametres();
			if ( result )
			{
				if ( Parametres[1].GetType() == typeof(C2iExpressionConstante) )
				{
					string strVal = ((C2iExpressionConstante)Parametres[1]).Valeur.ToString();
					try
					{
						Regex ex = new Regex(strVal);
					}
					catch
					{
						result.EmpileErreur(I.T("The regular expression '@1' is incorrect|286",strVal));
					}
				}
			}
			return result;

		}
	}
}
