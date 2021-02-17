using System;

using sc2i.multitiers.client;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Registre de base pour un serveur SC2I annexe au serveur principal
	/// Par exemple une appli ASP, un service WEB, ...
	/// Ces serveurs sont des clients SC2I
	/// </summary>
	public class C2iMultitiersServeurAnnexeRegistre : C2iMultitiersClientRegistre
	{
		/// //////////////////////////////////////
		public C2iMultitiersServeurAnnexeRegistre()
		{
		}

		/// //////////////////////////////////////
		protected override bool IsLocalMachine()
		{
			return true;
		}

		/// ///////////////////////////////////////////
		protected override string GetClePrincipale()
		{
			return "Software\\sc2i\\ServeurSTD\\";
		}


	}
}
