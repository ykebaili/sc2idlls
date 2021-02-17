using System;
using System.Collections;

using sc2i.common;
using System.Threading;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionBackground : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionBackground()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Background", 
				typeof(object), 
				"Background ( Exp1; exp2; exp3; ... )\r\nExecute séquentiellement un groupe de formules. retoure par défaut le résultat de la dernière formule",
				CInfo2iExpression.c_categorieDivers );
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres( true, new CInfoUnParametreExpression ( I.T("Statement|20037"),  typeof(object))));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			//Variable
			return -1;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{

				if ( Parametres.Count > 0 && Parametres[Parametres.Count-1] != null)
				{
					return Parametres2i[Parametres.Count-1].TypeDonnee;
				}
				return new CTypeResultatExpression ( typeof(object), false);
			}
		}


		/// //////////////////////////////////////////
        /// //////////////////////////////////////////
        protected override CResultAErreur ProtectedEval(CContexteEvaluationExpression ctx)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                if (ctx.SynchronizationContext == null)//Pas déjà en background
                {
                    CContexteEvaluationExpression ctxCopie = ctx.GetCopie();
                    ctxCopie.SynchronizationContext = SynchronizationContext.Current;
                    Thread th = new Thread(delegate()
                    {
                        try
                        {
                            foreach (C2iExpression parametre in Parametres2i)
                                parametre.Eval(ctxCopie);
                        }
                        catch (Exception ex)
                        {
                        }
                    });
                    th.Start();
                }
                else
                {
                    foreach (C2iExpression parametre in Parametres2i)
                    {
                        result = parametre.Eval(ctx);
                        if (!result)
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;

        }

        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
        {
            CResultAErreur result = CResultAErreur.False;
            result.EmpileErreur("MyEval of Background formula should never be called");
            return result;
        }
		
	}
}
