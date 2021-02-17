using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDans : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDans()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(3, "in", typeof(bool), I.TT(GetType(), "operator IN: return true if the left element is in the right list|154"), CInfo2iExpression.c_categorieComparaison);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Tested value|20044"), typeof(IComparable)),
                new CInfoUnParametreExpression(I.T("List|20045"), new CTypeResultatExpression(typeof(IComparable), true)));
			//info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new CTypeResultatExpression(typeof(IComparable), false), new CTypeResultatExpression(typeof(IComparable), true)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion en ICOomparable
			try
			{
				foreach ( IComparable comparable in (IEnumerable)valeursParametres[1] )
					if ( ((IComparable)valeursParametres[0]).CompareTo(comparable)==0 )
					{
						result.Data = true;
						return result;
					}
				result.Data = false;
				return result;
			}
			catch
			{
			}
			result.EmpileErreur(I.T("No overload of the 'in' function accepts the parameters indicated|153"));
			return result;
		}

		
	}

}