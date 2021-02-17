using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionLongueur : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionLongueur()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Length", typeof(int), I.TT(GetType(), "Length(Text)\nReturn the number of characters in the text|288"), CInfo2iExpression.c_categorieTexte);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;

			if ( valeursParametres.Length == 0 || valeursParametres[0] == null )
				result.Data = 0;
			else
				result.Data = valeursParametres[0].ToString().Length;
			return result;
		}

		
	}
}
