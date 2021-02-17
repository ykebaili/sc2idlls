using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Threading;

using sc2i.common;

namespace sc2i.web
{
	/// <summary>
	/// Description résumée de C2iServiceWeb.
	/// </summary>
	public class C2iServiceWeb : System.Web.Services.WebService
	{
		private static Hashtable m_tableSessionsWeb = new Hashtable();

		private static Timer m_timer = null;		


		public C2iServiceWeb()
		{
			//CODEGEN : Cet appel est requis par le Concepteur des services Web ASP.NET
			InitializeComponent();

			if ( m_timer == null )
				m_timer = new Timer ( new TimerCallback(OnNettoieSessions), null, 60000, 60000 );
		}

		#region Code généré par le Concepteur de composants
		
		//Requis par le Concepteur des services Web 
		private IContainer components = null;
				
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		private static void OnNettoieSessions( object state )
		{
			ArrayList lstToDelete = new ArrayList();
			foreach ( C2iSessionWeb session in m_tableSessionsWeb.Values )
				if ( session.DatePeremption < DateTime.Now )
					lstToDelete.Add ( session );
			foreach ( C2iSessionWeb session in lstToDelete )
				StaticCloseSession ( session.IdSessionWeb );
		}

		protected static C2iSessionWeb GetSessionWeb ( int nIdSession )
		{
			C2iSessionWeb session =( C2iSessionWeb )m_tableSessionsWeb[nIdSession];
			if ( session != null )//Augment la durée de vie de 5 minutes
				session.DatePeremption = DateTime.Now.AddMinutes(10);
			return session;
		}

		private static void StaticCloseSession( int nIdSessionPDA )
		{
			C2iSessionWeb session = GetSessionWeb ( nIdSessionPDA );
			if ( session == null )
				return;
			m_tableSessionsWeb.Remove ( nIdSessionPDA );
			session.Close();
		}

		protected CResultAErreur PrivateOpenSession( C2iSessionWeb session )
		{
			CResultAErreur result = session.OpenSession();
			if ( result )
			{
				m_tableSessionsWeb[session.IdSessionWeb] = session;
				session.DatePeremption = DateTime.Now.AddMinutes(1);
			}
			return result;
		}

		[WebMethod]
		public virtual void CloseSession ( int nIdSession )
		{
			C2iSessionWeb session = GetSessionWeb ( nIdSession );
			if ( session != null )
				session.Close();
		}
			

	}
}
