using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDistinct : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDistinct()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Distinct", typeof(object), I.TT(GetType(), "Distinct(list)\nReturn the list without double|242"), CInfo2iExpression.c_categorieGroupe);
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
				return Parametres2i[0].TypeDonnee;
			}
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( valeursParametres[0] == null || !typeof(IEnumerable).IsAssignableFrom(valeursParametres[0].GetType() ) )
			{
				result.Data = new object[]{valeursParametres[0]};
				return result;
			}
			try
			{
				ArrayList newListe = new ArrayList();
				IEnumerable lst = (IEnumerable)valeursParametres[0];
				foreach ( object elt in lst )
				{
					if ( !newListe.Contains(elt) )
						newListe.Add ( elt ); 
				}
				result.Data = newListe;
				return result;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the evaluation of 'distinct'|241"));
			}
			return result;
		}

	
	}
}
