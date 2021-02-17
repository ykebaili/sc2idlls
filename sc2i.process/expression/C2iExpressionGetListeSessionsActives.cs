using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data;
using sc2i.multitiers.client;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	[AutoExec("RegisterAction")]
	public class C2iExpressionGetListeSessionsActives : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
        public C2iExpressionGetListeSessionsActives()
		{
		}

		/// //////////////////////////////////////////
		public static void RegisterAction()
		{
			CAllocateur2iExpression.Register2iExpression(new C2iExpressionGetListeSessionsActives().IdExpression, typeof(C2iExpressionGetListeSessionsActives));
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"GetActiveSessions",
                new CTypeResultatExpression ( typeof(CInfoSessionAsDynamicClass), true ),
				I.TT(GetType(), "GetActiveSessions()\nreturns a list of active user sessions|20055"),
				CInfo2iExpression.c_categorieDivers );
            info.AddDefinitionParametres();
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
                return new CTypeResultatExpression(typeof(CInfoSessionAsDynamicClass), true);
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
                IGestionnaireSessions gestionnaire = (IGestionnaireSessions)C2iFactory.GetNewObject(typeof(IGestionnaireSessions));
                CInfoSessionAsDynamicClass[] infos = gestionnaire.GetInfosSessionsActives();
                result.Data = infos;
                return result;
			}
			catch
			{
                result.EmpileErreur(I.T("Error while retrieving active sessions|20056"));
			}
			return result;
		}
	}
}
