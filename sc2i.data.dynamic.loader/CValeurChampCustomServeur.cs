using System;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CValeurChampCustomServeur : CObjetDonneeServeurAvecCache
	{
#if PDA
		/// //////////////////////////////////////////////////
		public CValeurChampCustomServeur()
			:base()
	{
	}
#endif
		///////////////////////////////////////////////////
		public CValeurChampCustomServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CValeurChampCustom.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			return CResultAErreur.True;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CValeurChampCustom);
		}
	}
}
