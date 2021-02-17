using System;
using sc2i.data;
using sc2i.common;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CEntreeLogSynchronisationLoader.
	/// </summary>
	public class CEntreeLogSynchronisationServer : CObjetServeur
	{
#if PDA
		/// ///////////////////////////////////////////////////////////
		public CEntreeLogSynchronisationServer(  )
			:base()
		{
		}
#endif

		/// ///////////////////////////////////////////////////////////
		public CEntreeLogSynchronisationServer( int nIdSession )
			:base(nIdSession)
		{
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CEntreeLogSynchronisation.c_nomTable;
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CEntreeLogSynchronisation);
		}
	}
}
