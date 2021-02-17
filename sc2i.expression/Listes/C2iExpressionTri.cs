using System;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionTri : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionTri()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, 
                "Sort", 
                new CTypeResultatExpression(typeof(object), true),
				I.TT(GetType(), ".Sort(value[,bool])\nSort list according to selected value, ascending or descending according to last parameter (is set)|20013"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(
                new CInfoUnParametreExpression(I.T("sort value|20119"), new CTypeResultatExpression(typeof(object), false))));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(
                new CInfoUnParametreExpression(I.T("sort value|20119"), new CTypeResultatExpression(typeof(object), false)),
                new CInfoUnParametreExpression(I.T("Ascending(true) or descending(false)|20120"), new CTypeResultatExpression(typeof(bool), false))));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( ObjetPourAnalyseSourceConnu != null )
					return ObjetPourAnalyseSourceConnu.TypeResultatExpression;
				return base.TypeDonnee;
			}
		}

			/// //////////////////////////////////////////
			public override CTypeResultatExpression[] TypesObjetSourceAttendu
		{
			get
			{
				return new CTypeResultatExpression[]
					{
						new CTypeResultatExpression(typeof(object), true)
					};
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			return CResultAErreur.False;
		}

        private class CSorterListe  : IComparer<KeyValuePair<object, object>>
        {
            #region IComparer<object> Membres

            public int Compare(KeyValuePair<object,object> x, KeyValuePair<object, object> y)
            {
                object a = x.Value;
                object b = y.Value;
                if (a is IComparable && b is IComparable)
                    return ((IComparable)a).CompareTo((IComparable)b);
                if (a == null)
                    return 1;
                if (b == null)
                    return -1;
                return a.ToString().CompareTo(b.ToString());
            }

            #endregion
        }

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !typeof(IEnumerable).IsAssignableFrom(ctx.ObjetSource.GetType()) )
			{
				result.EmpileErreur(I.T("The function select cannot apply to the @1 type|227",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			ArrayList lst = new ArrayList();

           
            List<KeyValuePair<object, object>> lstObjValeurs = new List<KeyValuePair<object, object>>();
			foreach ( object obj in (IEnumerable) ctx.ObjetSource )
			{
				try
				{
					ctx.PushObjetSource ( obj, true );
					result = Parametres2i[0].Eval ( ctx );
					ctx.PopObjetSource(true);
                    object val = null;
                    if (result)
                        val = result.Data;
                    lstObjValeurs.Add ( new KeyValuePair<object,object>(obj, val ));
				}
				catch {}
			}
            lstObjValeurs.Sort ( new CSorterListe ());
            foreach ( KeyValuePair<object, object> kv in lstObjValeurs ) 
                lst.Add ( kv.Key );
            if ( Parametres2i.Length > 1 )
            {
                result = Parametres2i[1].Eval ( ctx);
                if ( result && result.Data is bool && !(bool)result.Data ) //descending
                lst.Reverse();
            }
			result.Data = lst;
			return result;
		}

        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
        {
            return objetSource.GetObjetAnalyseElements();
        }


				
	}
}
