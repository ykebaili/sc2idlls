using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionCalcule : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionCalcule()
		{
		}

	

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Calc", new CTypeResultatExpression(typeof(object), true), I.TT(GetType(), ".Calc(formula)\nCalculate for each element of a list the formula and return the list of the result|243"),
				CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new CTypeResultatExpression(typeof(object), false)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count > 0 )
					return Parametres2i[0].TypeDonnee.GetTypeArray();
				return new CTypeResultatExpression ( typeof(object), true );
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
				result.EmpileErreur(I.T("The Calc function cannot apply to the @1 type|244",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			ArrayList lst = new ArrayList();

			IEnumerable enumerable = null;
			if ( ctx.ObjetSource is IEnumerable )
				enumerable = (IEnumerable)ctx.ObjetSource;
			else
			{
				ArrayList lstTmp = new ArrayList();
				lstTmp.Add ( ctx.ObjetSource );
				enumerable = lstTmp;
			}

			//Calcule
			foreach ( object obj in (IEnumerable) ctx.ObjetSource )
			{
				try
				{
					ctx.PushObjetSource ( obj, true );
					result = Parametres2i[0].Eval ( ctx );
					ctx.PopObjetSource(true);
					if ( !result )
						return result;
					lst.Add ( result.Data );
				}
				catch {}
			}
			result.Data = lst;
			return result;
		}

		public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource ( CObjetPourSousProprietes objetSource )
		{
			return objetSource.GetObjetAnalyseElements();
		}


				
	}
}
