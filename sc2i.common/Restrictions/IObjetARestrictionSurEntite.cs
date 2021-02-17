using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.common
{
	/// ///////////////////////////////////////////
	public interface IObjetARestrictionSurEntite
	{
		void CompleteRestriction(CRestrictionUtilisateurSurType restriction);
	}

    /// ///////////////////////////////////////////
    public interface IConteneurObjetARestriction
    {
        Type TypePourRestriction { get; }
    }

    
}
