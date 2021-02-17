using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionTrim : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionTrim()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Trim", typeof(string), I.TT(GetType(), "Trim(Text)\nDelete spaces and returns line in beginning and end of text|289"), CInfo2iExpression.c_categorieTexte);
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
				result.Data = valeursParametres[0].ToString().Trim();
			return result;
		}

		
	}
}
