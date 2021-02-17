using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionRetourLigne : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionRetourLigne()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "LineFeed", typeof(string), I.TT(GetType(), "LineFeed()\nReturn a character of return line|270"), CInfo2iExpression.c_categorieTexte);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new Type[0]));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = "\r\n";
			return result;
		}

		
	}
}
