using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSomme : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSomme()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Sum", typeof(double), I.TT(GetType(), "Sum(list)\nReturn the sum of elements of the list|223"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("List|20066"), new CTypeResultatExpression(typeof(object), true)));
			//info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(object)) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			double fSomme = 0;
			if ( valeursParametres[0] == null || !typeof(IEnumerable).IsAssignableFrom(valeursParametres[0].GetType()) )
			{
				result.Data = 0;
				return result;
			}
			foreach ( object obj in (IEnumerable) valeursParametres[0] )
			{
				object valeur = GetValeur ( obj );
				try
				{
					fSomme += Convert.ToDouble ( valeur );
				}
				catch
				{
				}
			}
			result.Data = fSomme;
			return result;
		}

		/// //////////////////////////////////////////
		private double GetValeur ( object obj )
		{
			double fSomme = 0;
			if ( obj == null )
				return 0;
			if ( typeof(IList).IsAssignableFrom(obj.GetType()) )
			{
				foreach ( object objFils in (IEnumerable)obj)
					fSomme += GetValeur(objFils);
			}
			try
			{
				fSomme += Convert.ToDouble(obj);
			}
			catch
			{
					
			}
			return fSomme;
		}


		
	}
}
