using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionNullSiZero : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionNullSiZero()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"NullIfZero", 
				typeof(double),
				I.TT(GetType(), "NullSiZero(valeur)\nReturn null if value equal 0, else, return value|126"),
				CInfo2iExpression.c_categorieConversion);
			info.AddDefinitionParametres ( new CInfoUnParametreExpression ( I.T("Value|20039"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count < 1 || Parametres[0] == null )
					return base.TypeDonnee;
				return Parametres2i[0].TypeDonnee;
			}
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( Convert.ToDouble ( valeursParametres[0] ) == 0 )
					result.Data = null;
				else
					result.Data = valeursParametres[0];
			}
			catch
			{
				result.Data = null;
			}
			return result;
		}

		
	}
	
}
