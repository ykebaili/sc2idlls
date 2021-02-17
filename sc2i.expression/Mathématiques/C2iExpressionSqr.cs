using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSQR : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSQR()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SQR", 
				typeof(double),
				I.TT(GetType(), "SQR(Value)\nReturn the Square of the value|20007"),
				CInfo2iExpression.c_categorieMathematiques );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres2i.Length > 0 )
				{
                	return new CTypeResultatExpression ( typeof ( double ), false );
				}
				return base.TypeDonnee;
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				object val = valeursParametres[0];
                result.Data = ((double)val)*((double)val);
			}
			catch
			{
				result.EmpileErreur(I.T("Invalid parameter for SQR function|20106"));
			}
			return result;
		}
	}
}
