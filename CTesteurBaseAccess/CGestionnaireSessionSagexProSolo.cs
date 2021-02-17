using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.multitiers.server;
using sc2i.multitiers.client;
using sc2i.common;

namespace CTesteurBaseAccess
{
    public class CGestionnaireSessionSagexProSolo : CGestionnaireSessions
    {
        //Id de session pour le gestionnaire de session
		//Le gestionnaire de sessions accédant n'importe quand à la base, il 
		//doit avoir son propre numéro de session
		private static CSessionClient m_session = null;

		/// ///////////////////////////////////////////////////
        public CGestionnaireSessionSagexProSolo()
		{
		}

		/// ///////////////////////////////////////////////////
		/// Le data du result doit être passé à la fonction GetNewSessionSurServeur
        public const string c_dateLimte = "SYSTEM";
		protected override CResultAErreur CanOpenSession ( CSessionClient sessionSurClient )
		{
            
            if ( m_session == null )
			{
				m_session = CSessionClient.CreateInstance();
				m_session.OpenSession ( new CAuthentificationSessionServer(), "Seesion manager", ETypeApplicationCliente.Service );
			}
			CResultAErreur result = CResultAErreur.False;
			System.Console.WriteLine(I.T("Waiting for openSession availiability|30001"));
			System.Console.WriteLine(I.T("Beginning Seesion opening|30002"));

            try
            {
                if (sessionSurClient.Authentification is CAuthentificationSessionServer)
                    result = CanOpenSessionServeur(sessionSurClient);
                if (sessionSurClient.Authentification is CAuthentificationSessionSousSession)
                    result = CanOpenSessionSousSession(sessionSurClient);
                if (sessionSurClient.Authentification is sc2i.process.CAuthentificationSessionProcess)
                    result = CanOpenSessionProcess(sessionSurClient);
            }
            catch(Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }

			System.Console.WriteLine("Ending Session opening|30003");
			return result;
		}

		/// ///////////////////////////////////////////////////
		protected override CSessionClientSurServeur GetNewSessionSurServeur ( CSessionClient session, object data )
		{
			return new CSessionClientSurServeurSagexProSolo ( session );
		}

        protected CResultAErreur CanOpenSessionServeur(CSessionClient session)
        {
            return CResultAErreur.True;
        }
        protected CResultAErreur CanOpenSessionServiceClient(CSessionClient session)
        {
            return CResultAErreur.True;
        }

        protected CResultAErreur CanOpenSessionProcess(CSessionClient session)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }
        protected CResultAErreur CanOpenSessionSousSession(CSessionClient session)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CAuthentificationSessionSousSession auth = (CAuthentificationSessionSousSession)session.Authentification;
                CSessionClient sessionMain = GetSessionClient(auth.IdSessionPrincipale);
                if (sessionMain == null)
                {
                    result.EmpileErreur(I.T("The master session doesn't exist|16"));
                    return result;
                }
                //Effectue un appel pour provoquer une exception en cas d'erreursessionMain.IdSession;
                int nDummy = sessionMain.IdSession;
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error while accessing the master session|17"));
            }
            return result;
        }

        public override string GetNomUtilisateurFromIdUtilisateur(int nIdUtilisateur)
        {
            return "";
        }
    }
}
