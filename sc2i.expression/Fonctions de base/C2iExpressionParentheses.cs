using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionParentheses : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionParentheses()
		{
		}


        /// //////////////////////////////////////////
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = base.GetInfos();
            if (Parametres.Count > 0)
                info.TypeDonnee = Parametres2i[0].TypeDonnee;
            return info;
        }

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "", typeof(object),"" ,"" );
			info.Selectionnable = false;
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(object)));
			return info;
		}

        /// //////////////////////////////////////////
        public override CObjetPourSousProprietes GetObjetPourSousProprietes()
        {
            if (Parametres.Count == 1)
                return Parametres2i[0].GetObjetPourSousProprietes();
            return base.GetObjetPourSousProprietes();
        }

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = valeursParametres[0];
			return result;
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		public override CArbreDefinitionsDynamiques GetArbreProprietesAccedees(CArbreDefinitionsDynamiques arbreEnCours)
		{
			if (Parametres.Count == 1)
				return Parametres2i[0].GetArbreProprietesAccedees(arbreEnCours);
			return arbreEnCours;
		}

		
	}
}
