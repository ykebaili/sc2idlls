using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.multitiers.client;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Retourne le contexte d'edition en cours du contexte de donnée
	/// </summary>
	/// <remarks>
    /// Le contexte d'édition représente le contexte lié à l'édition en cours.
	/// </remarks>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionGetModificationContextId : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
        public C2iExpressionGetModificationContextId()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionGetModificationContextId().IdExpression,
                typeof(C2iExpressionGetModificationContextId));
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "GetModificationContextId", 
				new CTypeResultatExpression(typeof(string), true),
				I.TT(GetType(), "GetModificationContext()\nReturns the current modification context ID(string)|20046"),
				CInfo2iExpression.c_categorieDivers);
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}


		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression ( typeof(string), false );
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			CResultAErreur result = CResultAErreur.True;
				CContexteDonnee contexteDonnee = (CContexteDonnee)ctx.GetObjetAttache ( typeof (CContexteDonnee) );
            if ( contexteDonnee != null )
                result.Data = contexteDonnee.IdModificationContextuelle;
            else
                result.Data = "";
			return result;
		}


	}
}
