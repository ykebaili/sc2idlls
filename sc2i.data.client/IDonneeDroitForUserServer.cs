using System;

using sc2i.data;

#if !PDA_DATA

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de CDonneeDroitForUser.
	/// </summary>
	public interface IDonneeDroitForUserServer
	{
		CDonneeDroitForUser GetDonneeDroit ( int nIdUtilisateur, string strCode );
	}

}
#endif