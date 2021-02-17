using System;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description r�sum�e de Class1.
	/// </summary>
	public class CRelationTypeCaracteristiqueEntite_ChampCustomServeur : CObjetServeur
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CRelationTypeCaracteristiqueEntite_ChampCustomServeur()
			:base ()
		{
			
		}
#endif
		/// //////////////////////////////////////////////////
		public CRelationTypeCaracteristiqueEntite_ChampCustomServeur( int nIdSession )
			:base ( nIdSession )
		{
			
		}

		//////////////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CRelationTypeCaracteristiqueEntite_ChampCustom.c_nomTable;
		}

		//////////////////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CRelationTypeCaracteristiqueEntite_ChampCustom);
		}

		//////////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
				
		}


	}
}
