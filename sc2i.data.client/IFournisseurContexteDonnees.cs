using System;
using sc2i.data;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de IFournisseurContexteDonnee.
	/// </summary>
	public interface IFournisseurContexteDonnee
	{
		CContexteDonnee ContexteCourant { get;}

        void PushContexteCourant(CContexteDonnee contexte);
        void PopContexteCourant();
	}
}
