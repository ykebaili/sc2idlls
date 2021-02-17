using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Collections;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Une sous session client est une session indépendante,
	/// avec son user, ses droits, ... qui travaille dans les mêmes
	/// transaction que sa session principale
	/// </summary>
	public class CSousSessionClient : CSessionClient, IObjetAttacheASession
	{
		private int m_nIdSessionPrincipale = -1;

		//---------------------------------------------------------
		protected CSousSessionClient(int nIdSessionPrincipale)
			: base()
		{
			m_nIdSessionPrincipale = nIdSessionPrincipale;
			CGestionnaireObjetsAttachesASession.AttacheObjet(nIdSessionPrincipale, this);
			CSessionClient session = RootSession;
			//Copie les propriétés de la session principale
			foreach (string strProp in session.GetProprietes())
				SetPropriete(strProp, session.GetPropriete(strProp));
		}

		//---------------------------------------------------------
		public override void Dispose()
		{
			CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSessionPrincipale, this);
			base.Dispose();
		}

		//---------------------------------------------------------
		public static CSousSessionClient GetNewSousSession(int nIdSession)
		{
			return new CSousSessionClient(nIdSession);
		}

		//---------------------------------------------------------
		public CSessionClient RootSession
		{
			get
			{
				CSessionClient session = CSessionClient.GetSessionForIdSession(m_nIdSessionPrincipale);
				if (session != null)
				{
					CSousSessionClient sousSession = session as CSousSessionClient;
					if (sousSession != null)
						return sousSession.RootSession;
					return session;
				}
				return null;
			}
		}

		//---------------------------------------------------------
		public override CResultAErreur BeginTrans()
		{
			return RootSession.BeginTrans();
		}

		//---------------------------------------------------------
		public override CResultAErreur BeginTrans(System.Data.IsolationLevel isolationLevel)
		{
			return RootSession.BeginTrans(isolationLevel);
		}

		//---------------------------------------------------------
		public override CResultAErreur CommitTrans()
		{
			return RootSession.CommitTrans();
		}

		//---------------------------------------------------------
		public override CResultAErreur RollbackTrans()
		{
			return RootSession.RollbackTrans();
		}


		//---------------------------------------------------------
		public string DescriptifObjetAttacheASession
		{
			get { return "SubSession"; }
		}

		//---------------------------------------------------------
		public override void CloseSession()
		{
			CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSessionPrincipale, this);
			base.CloseSession();
		}

		//---------------------------------------------------------
		void IObjetAttacheASession.OnCloseSession()
		{
			CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSessionPrincipale, this);
			CloseSession();
		}

		//---------------------------------------------------------
		public override IServicePourSessionClient GetService(string strService)
		{
			return RootSession.GetService(strService);
		}

		//---------------------------------------------------------
		public override IServicePourSessionClient GetService(string strService, int nIdSession)
		{
			return RootSession.GetService(strService, nIdSession);
		}

		//---------------------------------------------------------
		public override IFournisseurServicePourSessionClient GetFournisseur(string strService)
		{
			return RootSession.GetFournisseur(strService);
		}

		//---------------------------------------------------------
		///////////////////////////////////////////////
		public override CServiceSurClient GetServiceSurClient(string strService)
		{
			return RootSession.GetServiceSurClient(strService);
		}

	}

}
