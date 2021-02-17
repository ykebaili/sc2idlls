using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionAscii : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionAscii()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Ascii", 
				typeof(int),
				I.TT(GetType(), "Ascii(text)\nReturn the ASCII code of the first character of the string|156"), 
		CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Text|20046"), typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			string strVal = valeursParametres[0].ToString();
			if(  strVal.Length >= 1 )
				result.Data = (int)(char)strVal[0];
			else
				result.Data = (int)0;
			return result;
		}

	}
}
