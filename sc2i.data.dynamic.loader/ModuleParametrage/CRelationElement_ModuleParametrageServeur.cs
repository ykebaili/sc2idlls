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
	public class CRelationElement_ModuleParametrageServeur : CObjetServeur
	{
		
		///////////////////////////////////////////////////
        public CRelationElement_ModuleParametrageServeur(int nIdSession)
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
            return CRelationElement_ModuleParametrage.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
                CRelationElement_ModuleParametrage relation = (CRelationElement_ModuleParametrage)objet;
			
            }
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
            return typeof(CRelationElement_ModuleParametrage);
		}
	}
}
