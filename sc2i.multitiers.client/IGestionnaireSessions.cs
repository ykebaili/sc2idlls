using System;

using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de IGestionnaireSessions.
	/// </summary>
	public interface IGestionnaireSessions
	{
		//Tente d'ouvrir la session passée en paramètre.
		CResultAErreur OpenSession ( CSessionClient session );

		CResultAErreur ReconnecteSession(CSessionClient session);

		CSessionClient GetSessionClient ( int nIdSession );

		int[] GetListeIdSessionsConnectees();

		bool IsSessionOpen ( int nIdSession );

		ISessionClientSurServeur GetSessionClientSurServeur ( int nIdSession );

		string GetNomUtilisateurFromKeyUtilisateur ( CDbKey keyUtilisateur );

		bool IsConnected(CDbKey keyUtilisateur);
		IInfoUtilisateur[] GetUtilisateursConnectes();

        CInfoSessionAsDynamicClass[] GetInfosSessionsActives();
	}
}
