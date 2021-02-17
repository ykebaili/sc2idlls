using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionForEach : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionForEach()
		{
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
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "ForEach", new CTypeResultatExpression(typeof(string), false),
				I.TT(GetType(), ".ForEach(Action)\nExecute action foreach element in the list|312"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Looped action|20087"),
                new CTypeResultatExpression(typeof(object), false), true));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if (Parametres.Count > 0)
					return Parametres2i[0].TypeDonnee;
				return new CTypeResultatExpression ( typeof(object), false );
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
				result.EmpileErreur(I.T("The function ListInText cannot apply to the @1 type|237",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			ArrayList lst = new ArrayList();

			foreach ( object obj in (IEnumerable) ctx.ObjetSource )
			{
				try
				{
					ctx.PushObjetSource ( obj, true );
					result = Parametres2i[0].Eval ( ctx );
					ctx.PopObjetSource(true);
					if ( !result )
						return result;
				}
				catch {}
			}
			return result;
		}

        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
		{
            return objetSource.GetObjetAnalyseElements();
		}


				
	}
}
