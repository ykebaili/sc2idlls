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
	public class C2iExpressionSetModificationContextId : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
        public C2iExpressionSetModificationContextId()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionSetModificationContextId().IdExpression,
                typeof(C2iExpressionSetModificationContextId));
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "SetModificationContextId", 
				new CTypeResultatExpression(typeof(string), true),
				I.TT(GetType(), "SetModificationContext(id)\nSets the current modification context ID(string)|"),
				CInfo2iExpression.c_categorieDivers);
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 1;
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
                if (contexteDonnee != null)
                {
                    contexteDonnee.IdModificationContextuelle = listeParametres[0].ToString();
                    result.Data = contexteDonnee.IdModificationContextuelle;
                }
                else
                    result.Data = "";
			return result;
		}


	}
}
