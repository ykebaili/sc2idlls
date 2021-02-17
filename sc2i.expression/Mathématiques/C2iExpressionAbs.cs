using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionAbs : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionAbs()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Abs", 
				typeof(double),
				I.TT(GetType(), "Abs(Value)\nReturn the absolute value of the value|256"),
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
					C2iExpression exp = Parametres2i[0];
					if ( exp != null && exp.TypeDonnee.TypeDotNetNatif == typeof(int) )
						return new CTypeResultatExpression ( typeof ( int ), false );
					else
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
				if ( val is int )
					result.Data = Math.Abs ( (int)val );
				else
					result.Data = Math.Abs ( Convert.ToDouble ( val ) );
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'abs' is incorrect|255"));
			}
			return result;
		}
	}
}
