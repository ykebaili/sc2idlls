using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSelectFirst : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionSelectFirst()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "SelectFirst", 
				new CTypeResultatExpression(typeof(object), false),
				I.TT(GetType(), ".SelectFirst(Condition)\nReturn the first element of the list satisfying the condition|224"),
				CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new CTypeResultatExpression(typeof(bool), false)));
			return info;
		}

		/// //////////////////////////////////////////
		public override bool AgitSurListe
		{
			get
			{
				return true;
			}
		}


		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( ObjetPourAnalyseSourceConnu != null )
					return ObjetPourAnalyseSourceConnu.TypeResultatExpression.GetTypeElements();
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

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !typeof(IEnumerable).IsAssignableFrom(ctx.ObjetSource.GetType()) )
			{
				result.EmpileErreur(I.T("The function SelectFirst cannot apply to the @1 type|225",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			//Evaluation sur condition
			int nIndex = 0;
			object resultat = null;
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
					{
						resultat = obj;
						break;
					}
					nIndex++;
				}
				catch {}
			}
			result.Data = resultat;
			return result;
		}

        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
        {
            return objetSource.GetObjetAnalyseElements();
        }


				
	}
}
