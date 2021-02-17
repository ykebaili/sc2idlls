using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionNombre : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionNombre()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Number", typeof(int), I.TT(GetType(), "Number(List)\nReturn the number of elements in list|230"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("List|20066"), new CTypeResultatExpression(typeof(object), true)));
			//info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(object)) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( valeursParametres[0] == null || !typeof(IEnumerable).IsAssignableFrom(valeursParametres[0].GetType() ) )
			{
				result.Data = 0;
				return result;
			}
            IList lst = valeursParametres[0] as IList;
            if (lst != null)
                result.Data = lst.Count;
            else
            {
                IEnumerable en = valeursParametres[0] as IEnumerable;
                int nCount = 0;
                foreach (object obj in en)
                    nCount++;
                result.Data = nCount;
            }
			return result;
		}

	
	}
}
