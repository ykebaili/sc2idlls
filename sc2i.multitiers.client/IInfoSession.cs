using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de IInfoSession.
	/// </summary>
	public interface IInfoSession
	{
		int IdSession{get;}
		string DescriptionApplicationCliente{get;}
		ETypeApplicationCliente TypeApplicationCliente{get;}
		DateTime DateHeureConnexion {get;}
        DateTime DateHeureDerniereActivite { get; set; }
		IInfoUtilisateur GetInfoUtilisateur();
	}
}
