using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSafeValue : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSafeValue()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SafeValue", 
				typeof(string),
				I.TT(GetType(), "SafeValue(value,defaultValue)\nTry to return the value of 'value'. If an error occurs or that the value isn't 'numerical', return the default value|168"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Value if error|20048"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres2i.Length != 0 && Parametres2i[0] != null )
					return Parametres2i[0].TypeDonnee;
				return new CTypeResultatExpression(typeof(string), false);
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = base.VerifieParametres();
			if(  !result )
				return result;
			if ( Parametres.Count != 2 )
			{
				result.EmpileErreur(I.T("SafeValue wait two parameters|169"));
				return result;
			}
			if ( Parametres[0] == null || Parametres[1] == null )
			{
				result.EmpileErreur(I.T("SafeValue wait two parameters not null|170"));
				return result;
			}
			if ( !(Parametres2i[0] is C2iExpressionNull) && !(Parametres2i[1] is C2iExpressionNull)
                && Parametres2i[0].TypeDonnee != null 
                && !Parametres2i[0].TypeDonnee.Equals ( Parametres2i[1].TypeDonnee ) )
			{
				result.EmpileErreur(I.T("The two parameters of SafeValue must be of the same type|171"));
				return result;
			}
			return result;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval(CContexteEvaluationExpression ctx)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				bool bErreur = false;
				if ( GetNbParametresNecessaires() >=0 &&  Parametres.Count != GetNbParametresNecessaires() )
				{
					result.EmpileErreur(I.T("The expected number of parameters isn't correct (@1 expected)|172", GetNbParametresNecessaires().ToString()));
					return result;
				}
				try
				{
					result = Parametres2i[0].Eval ( ctx );
					bErreur = !result.Result;
					if ( result.Data is Double && Double.IsNaN ( (double)result.Data ) )
						result = Parametres2i[1].Eval ( ctx );
				}
				catch
				{
					bErreur = true;
				}
				if ( bErreur )
				{
					result = Parametres2i[1].Eval ( ctx );
				}

			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
			
		}

		public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
		{
			 CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("MyEval of C2iExpresisonSafeValue should never be called|173"));
			return result;
		}

	}
}
