using System;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.web
{
	/// <summary>
	/// Description résumée de C2iSessionWeb.
	/// </summary>
	public abstract class C2iSessionWeb
	{
		private DateTime m_datePeremption = DateTime.Now.AddMinutes(10);


		private CSessionClient m_session;

		public readonly int IdSessionWeb;

		private static int m_IdLastSessionWeb = 1;

		public C2iSessionWeb()
		{
			IdSessionWeb = m_IdLastSessionWeb++;	
		}

		//------------------------------------------------------------
		public DateTime DatePeremption
		{
			get
			{
				return m_datePeremption;
			}
			set
			{
				m_datePeremption = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public CResultAErreur Close()
		{
			CResultAErreur result = CResultAErreur.True;
			result = MyClose();
			try
			{		
				m_session.CloseSession();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////
		public abstract CResultAErreur MyClose();

		/// /////////////////////////////////////////////////////////
		protected virtual void BeforeAuthentification ( CSessionClient session )
		{
		}

		/// /////////////////////////////////////////////////////////
		public virtual CResultAErreur OpenSession ()
		{

			m_session = CSessionClient.CreateInstance();
            m_session.UseSessionUtilisateurData = false;
			BeforeAuthentification ( m_session );
			CResultAErreur result = CResultAErreur.True;
            result = MyOpenSession(m_session);
			return result;
		}

        /// /////////////////////////////////////////////////////////
        protected virtual CResultAErreur MyOpenSession(CSessionClient session)
        {
            return session.OpenSession(new CAuthentificationSessionSousSession(0), "Session Web " + DynamicClassAttribute.GetNomConvivial(GetType()), ETypeApplicationCliente.Service);
        }


		/// /////////////////////////////////////////////////////////
		public CSessionClient Session
		{
			get
			{
				return m_session;
			}
		}
	}
}
