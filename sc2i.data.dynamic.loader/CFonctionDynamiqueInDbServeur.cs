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
	public class CFonctionDynamiqueInDbServeur : CObjetServeurAvecBlob, IObjetServeur
	{
#if PDA
		///////////////////////////////////////////////////
		public CFonctionDynamiqueInDbServeur (  )
			:base (  )
		{
		}
#endif
		
		///////////////////////////////////////////////////
		public CFonctionDynamiqueInDbServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CFonctionDynamiqueInDb.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CFonctionDynamiqueInDb fonction = (CFonctionDynamiqueInDb)objet;
			
				if (fonction.Nom == "")
					result.EmpileErreur(I.T("The dynamic function name cannot be empty|20010"));
				if ( fonction.TypeObjets == null )
					result.EmpileErreur(I.T("The associated object type with the dynamic functino is null|20011"));

			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CFonctionDynamiqueInDb);
		}
	}
}
