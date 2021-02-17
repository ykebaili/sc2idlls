using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionEstNull : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionEstNull()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "IsNull", typeof(bool), I.TT(GetType(), "IsNull function : Return true if the element is null|155"), CInfo2iExpression.c_categorieComparaison);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;

			result.Data = valeursParametres[0] == null || valeursParametres[0] == DBNull.Value;
			return result;
		}

		
	}

}