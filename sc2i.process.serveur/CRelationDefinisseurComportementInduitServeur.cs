using System;
using System.Collections;

using sc2i.data;
using sc2i.data.serveur;

using sc2i.common;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CRelationDefinisseurComportementInduit.
	/// </summary>
	public class CRelationDefinisseurComportementInduitServeur : CObjetServeur
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationDefinisseurComportementInduit()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationDefinisseurComportementInduitServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationDefinisseurComportementInduit.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			
			
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationDefinisseurComportementInduit);
		}
		//-------------------------------------------------------------------
	}
}
