using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionConcatList : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionConcatList()
		{
		}

        /// //////////////////////////////////////////
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = base.GetInfos();
            if (Parametres.Count == 2)
            {
                CTypeResultatExpression tpParametre0 = Parametres2i[0].TypeDonnee;
                CTypeResultatExpression tpParametre1 = Parametres2i[1].TypeDonnee;

                if (tpParametre0 != null && tpParametre1 != null)
                {
                    Type tp1, tp2;
                    tp1 = tpParametre0.TypeDotNetNatif;
                    tp2 = tpParametre1.TypeDotNetNatif;
                    if (tp1 == tp2)
                        info.TypeDonnee = new CTypeResultatExpression(tp1, true);
                    else
                        info.TypeDonnee = new CTypeResultatExpression(typeof(object), true);
                }
            }
            return info;
        }

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "ConcatList", typeof(int), I.TT(GetType(), "ConcatList(List1,List2)\nAdd a list (or an element) to an other|20007"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("List 1|20064"), typeof(object)),
                new CInfoUnParametreExpression(I.T("List 2|20065"), typeof(object)));
			//info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(object), typeof(object)) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
            object v1 = valeursParametres[0];
            object v2 = valeursParametres[1];
            ArrayList lst = new ArrayList();
            if (v1 is ICollection)
                lst.AddRange((ICollection)v1);
            else if ( v1!=null)
                lst.Add(v1);
            if (v2 is ICollection)
                lst.AddRange((ICollection)v2);
            else if ( v2 != null )
                lst.Add(v2);
            result.Data = lst;
			return result;
		}

	
	}
}
