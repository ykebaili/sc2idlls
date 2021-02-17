using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMax : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMax()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Max", typeof(object), I.TT(GetType(), "Max(list)\nReturn the bigger element of the list|234"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("List|20066"), new CTypeResultatExpression(typeof(object), true)));

			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count == 0 || Parametres[0] == null )
					return new CTypeResultatExpression(typeof(object), false);
				return Parametres2i[0].TypeDonnee.GetTypeElements();
			}
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
			try
			{
				object max = null;
				IEnumerable lst = (IEnumerable)valeursParametres[0];
				foreach ( object obj in lst )
				{
                    IComparable elt = obj as IComparable;
                    if (elt == null)
                    {
                        if (max == null)
                            max = obj;
                    }
                    else if (max == null || !(max is IComparable) || ((IComparable)max).CompareTo(elt) < 0)
						max = elt;
				}
				result.Data = max;
			}
			catch
			{
				result.EmpileErreur(I.T("The paramters of Max formula cannot be compared|235"));
			}
			return result;
		}

	
	}
}
