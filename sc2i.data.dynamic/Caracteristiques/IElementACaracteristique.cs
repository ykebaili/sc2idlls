using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data.dynamic
{
	public interface IElementACaracteristique : IObjetDonneeAChamps
	{
		CListeObjetsDonnees Caracteristiques { get;}
	}
}
