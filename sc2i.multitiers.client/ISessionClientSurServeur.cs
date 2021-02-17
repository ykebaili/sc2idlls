using System;
using System.Collections.Generic;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public interface ISessionClientSurServeur : 
		IServicesClientManager, 
		IInfoSession,
		IServiceTransactions
	{
		string GetVersionServeur();
		void CloseSession();
		void ChangeUtilisateur(CDbKey keyUtilisateur);
		bool Ping();

        DateTime DateHeureLastTestSessionClientSuccess { get; set; }

        void RegisterSousSession(ISessionClientSurServeur sousSession);

        void RemoveSousSession(ISessionClientSurServeur sousSession);

        void FillListeSousSessions(List<ISessionClientSurServeur> lstSousSessions);

        ISessionClientSurServeur GetSessionPrincipale();

        IEnumerable<ISessionClientSurServeur> GetSousSessions();
        
        IEnumerable<ISessionClientSurServeur> GetAllSousSession();


        bool IsInTransaction();
    }
}
