using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CDonneeCumuleeServeur : CObjetServeurAvecBlob, IObjetServeur
	{
		///////////////////////////////////////////////////
		public CDonneeCumuleeServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CDonneeCumulee.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CDonneeCumulee);
		}
	}
}
