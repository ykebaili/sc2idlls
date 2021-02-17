using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSi : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSi()
		{
		}

        /// //////////////////////////////////////////
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = base.GetInfos();
            if (Parametres.Count > 1 && Parametres2i[1] != null)
                info.TypeDonnee = Parametres2i[1].TypeDonnee;
            Type tp = info.TypeDonnee==null?typeof(object):info.TypeDonnee.TypeDotNetNatif;
            if (tp == null || tp == typeof(object))
            {
                if (Parametres.Count > 2 && Parametres2i[2] != null)
                    info.TypeDonnee = Parametres2i[2].TypeDonnee;
            }
            


            return info;
        }

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "If", typeof(object), I.TT(GetType(), "If(condition;then;if not)\nConditional connection IF|245"), CInfo2iExpression.c_categorieLogique);
			info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Condition|20067"), typeof(bool)),
                new CInfoUnParametreExpression(I.T("Action if true|20068"), typeof(object), true),
                new CInfoUnParametreExpression(I.T("Action if false|20069"), typeof(object), true));
			return info;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval(CContexteEvaluationExpression ctx)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( GetNbParametresNecessaires() >=0 &&  Parametres.Count != GetNbParametresNecessaires() )
			{
				result.EmpileErreur(I.T("The number of parameters isn't correct (@1 expected)|246",GetNbParametresNecessaires().ToString()));
				return result;
			}
			try
			{
				result = Parametres2i[0].Eval ( ctx );
				if ( !result )
					return result;
				bool b1 = Convert.ToBoolean(result.Data);
				if ( b1 )
					return Parametres2i[1].Eval ( ctx );
				else
					return Parametres2i[2].Eval ( ctx );
			}
			catch
			{
			}
			result.EmpileErreur(I.T("No overload of the function 'IF' accept the indicated parameters|247"));
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("Impossible Error|248"));
			return result;
		}

		
	}
}
