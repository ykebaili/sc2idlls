using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMajuscule : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMajuscule()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "UpperCase", typeof(string), I.TT(GetType(), "UpperCase(Text)\nReturn the text in capital letters|287"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;

			if ( valeursParametres[0] == null )
				result.Data = "";
			else
				result.Data = valeursParametres[0].ToString().ToUpper();
			return result;
		}

		
	}
}
