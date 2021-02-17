using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
using sc2i.data.dynamic.NommageEntite;

namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CNommageEntiteServeur : CObjetServeurAvecBlob, IObjetServeur
	{

		///////////////////////////////////////////////////
        public CNommageEntiteServeur(int nIdSession)
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
            return CNommageEntite.c_nomTable;
		}


		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
                CNommageEntite nomEntite = (CNommageEntite)objet;
                if (nomEntite.TypeEntityString == String.Empty)
                    result.EmpileErreur(I.T("Entity Type should not be empty|10002"));
                if (nomEntite.CleEntite == null)
                    result.EmpileErreur(I.T("Entity Id not valid|10003"));
                if (nomEntite.NomFort == String.Empty)
                    result.EmpileErreur(I.T("Empty string is not allowed for Strong Name of entity: @1 Id: @2|10001", nomEntite.TypeEntityString, nomEntite.CleEntiteString));

			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

        //--------------------------------------------------------------
		public override Type GetTypeObjets()
		{
            return typeof(CNommageEntite);
		}
	}
}
