using System;

using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de IGestionnaireSessions.
	/// </summary>
	public interface IGestionnaireSessions
	{
		//Tente d'ouvrir la session pass�e en param�tre.
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
