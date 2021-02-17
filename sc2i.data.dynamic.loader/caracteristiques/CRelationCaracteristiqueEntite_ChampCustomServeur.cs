using System;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;
using sc2i.data.dynamic.loader;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CRelationCaracteristiqueEntite_ChampCustomServeur : CRelationElementAChamp_ChampCustomServeur
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CRelationCaracteristiqueEntite_ChampCustomServeur()
			:base ()
		{
			
		}
#endif
		/// //////////////////////////////////////////////////
		public CRelationCaracteristiqueEntite_ChampCustomServeur( int nIdSession )
			:base ( nIdSession )
		{
			
		}

		//////////////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CRelationCaracteristiqueEntite_ChampCustom.c_nomTable;
		}

		//////////////////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CRelationCaracteristiqueEntite_ChampCustom);
		}

	}
}
