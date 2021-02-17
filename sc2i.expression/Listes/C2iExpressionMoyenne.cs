using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionMoyenne : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMoyenne()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Average", typeof(double), I.TT(GetType(), "Average(List)\nReturn the elements average of the list|231"), CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("List|20066"), new CTypeResultatExpression(typeof(object), true)));
			//info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(object)) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			double fMoyenne = 0;
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
					fMoyenne += Convert.ToDouble ( valeur );
				}
				catch
				{
				}
			}
			if ( ((IList)valeursParametres[0]).Count != 0 )
				fMoyenne = fMoyenne / ((IList)valeursParametres[0]).Count;
			result.Data = fMoyenne;
			return result;
		}

		/// //////////////////////////////////////////
		private double GetValeur ( object obj )
		{
			double fMoyenne = 0;
			if ( obj == null )
				return 0;
			if ( typeof(IList).IsAssignableFrom(obj.GetType()) )
			{
				foreach ( object objFils in (IEnumerable)obj)
					fMoyenne += GetValeur(objFils);
			}
			try
			{
				fMoyenne += Convert.ToDouble(obj);
			}
			catch
			{
					
			}
			return fMoyenne;
		}


		
	}
}
