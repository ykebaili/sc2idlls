using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionIndexs : C2iExpressionMethodeAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionIndexs()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Indexs", new CTypeResultatExpression(typeof(int), true), I.TT(GetType(), "Indexs(Condition)\nReturn the index list of elements satisfying the condition|239"), CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new CTypeResultatExpression(typeof(bool), false)));
			return info;
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

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !typeof(IEnumerable).IsAssignableFrom(ctx.ObjetSource.GetType()) )
			{
				result.EmpileErreur(I.T("The function indexs cannot apply to the @1 type|240",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			ArrayList lst = new ArrayList();

			int nIndex = 0;
			foreach ( object obj in (IEnumerable) ctx.ObjetSource )
			{
				try
				{
					ctx.PushObjetSource ( obj, true );
					result = Parametres2i[0].Eval ( ctx );
					ctx.PopObjetSource(true);
					if ( !result )
						return result;
					bool bResult = Convert.ToBoolean ( result.Data );
					if ( bResult )
						lst.Add ( nIndex );
					nIndex++;
				}
				catch {}
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
