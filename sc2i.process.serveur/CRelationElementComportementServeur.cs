using System;
using System.Collections;

using sc2i.data;
using sc2i.data.serveur;

using sc2i.common;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CRelationElementComportementServeur.
	/// </summary>
	public class CRelationElementComportementServeur : CObjetServeur
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationElementComportementServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationElementComportementServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationElementComportement.c_nomTable;
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
			return typeof(CRelationElementComportement);
		}
		//-------------------------------------------------------------------
	}
}
