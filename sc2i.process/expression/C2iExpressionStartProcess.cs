using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	[AutoExec("RegisterAction")]
	public class C2iExpressionStartProcess : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionStartProcess()
		{
		}

		/// //////////////////////////////////////////
		public static void RegisterAction()
		{
			CAllocateur2iExpression.Register2iExpression(new C2iExpressionStartProcess().IdExpression, typeof(C2iExpressionStartProcess));
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"StartProcess", 
				typeof(object),
				I.TT(GetType(), "StartProcess(Id, TargetElement)\nStart a process, Id the process Id, target element (optional) is the process target element|20012"),
				CInfo2iExpression.c_categorieDivers );
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(int)));
			info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(int), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression(typeof(Object), false);
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CContexteDonnee contexteDonnee = null;
                IObjetAContexteDonnee objAContexte = ctx.ObjetSource as IObjetAContexteDonnee;
                if (objAContexte != null)
                    contexteDonnee = objAContexte.ContexteDonnee;
                if (valeursParametres.Length > 1 && valeursParametres[1] is IObjetAContexteDonnee)
                    contexteDonnee = ((IObjetAContexteDonnee)valeursParametres[1]).ContexteDonnee;
                if ( contexteDonnee == null )
                    contexteDonnee = ctx.GetObjetAttache(typeof(CContexteDonnee)) as CContexteDonnee;
                
				if (contexteDonnee == null)
				{
					result.EmpileErreur(I.T("Can not find any context to execute process|20013"));
					return result;
				}
				int? nIdProcess = valeursParametres[0] as int?;
				if (nIdProcess == null)
				{
					result.EmpileErreur(I.T("First parameters of start process should be an integer|20014"));
					return result;
				}
				CProcessInDb process = new CProcessInDb(contexteDonnee);
				if (!process.ReadIfExists(nIdProcess.Value))
				{
					result.EmpileErreur(I.T("Can not find process @1|20015", nIdProcess.Value.ToString()));
					return result;
				}
				object target = null;
				if (valeursParametres.Length > 1)
					target = valeursParametres[1];
				result = CProcessEnExecutionInDb.StartProcessClient(
					process.Process,
					target,
					contexteDonnee,
					null);
			}
			catch
			{
				result.EmpileErreur(I.T("Error in StartProcess|20016"));
			}
			return result;
		}
	}
}
