using System;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSplit : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSplit()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Split", typeof(string[]), I.TT(GetType(), "Split(String, Separator) or Split(String, Length)\nReturn a table of text, by separating each element after the separator, or on the number of characters given|266"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Separator|20076"), typeof(string)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Length|20070"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion du second en int
			try
			{
				string str1 = valeursParametres[0].ToString();
				if (valeursParametres[1] is string)
				{
					string str2 = valeursParametres[1].ToString();
					if (str2.Length > 0)
						result.Data = str1.Split(str2[0]);
					else
						result.EmpileErreur(I.T("The separator of the split operation is incorrect|267"));
				}
				else
					if ( typeof(int).IsAssignableFrom ( valeursParametres[1].GetType() ))
					{
						//Coupe sur un nombre de caractères donnés
						int nNb = Convert.ToInt32 ( valeursParametres[1] );
						if ( nNb > 0 )
						{
							List<string> lst = new List<String>();
							int nPos = 0;
							while ( nPos < str1.Length )
							{
								string strPart = str1.Substring ( nPos, Math.Min ( nNb, str1.Length-nPos ) );
								lst.Add ( strPart );
								nPos += nNb;
							}
							result.Data = lst.ToArray();
						}
						else
							result.EmpileErreur(I.T("The number of characters to split is incorrect|268"));
					}
			}
			catch (Exception ex )
			{
                result.EmpileErreur(new CErreurException(ex));
                result.EmpileErreur(I.T("Error at the time of Split|314"));
				return result;
			}
			return result;
		}

		
	}
}
