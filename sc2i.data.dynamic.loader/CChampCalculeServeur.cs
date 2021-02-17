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
	public class CChampCalculeServeur : CObjetDonneeServeurAvecCache, IObjetServeur
	{
#if PDA
		///////////////////////////////////////////////////
		public CChampCalculeServeur (  )
			:base (  )
		{
		}
#endif
		
		///////////////////////////////////////////////////
		public CChampCalculeServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CChampCalcule.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CChampCalcule champ = (CChampCalcule)objet;
			
				if (champ.Nom == "")
					result.EmpileErreur(I.T("The calculated field name cannot be empty|105"));
				if (!CObjetDonneeAIdNumerique.IsUnique(champ, CChampCalcule.c_champNom, champ.Nom))
					result.EmpileErreur(I.T("A calculated field with this name already exist|106"));

				if ( champ.TypeObjets == null )
					result.EmpileErreur(I.T("The associated object type with the calculated field is false|107"));

			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CChampCalcule);
		}
	}
}
