using System;

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de CMotRecherche.
	/// </summary>
	public class CMotRecherche
	{
		public readonly string Mot;
		public readonly int Niveau;

		public CMotRecherche( string strMot, int nNiveau)
		{
			Mot =  strMot;
			Niveau = nNiveau;
		}
	}
}
