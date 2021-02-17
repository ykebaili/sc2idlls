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
	public class CVariableSurObjetServeur : CObjetServeur, IObjetServeur
	{
#if PDA
		///////////////////////////////////////////////////
		public CVariableSurObjetServeur (  )
			:base (  )
		{
		}
#endif
		
		///////////////////////////////////////////////////
		public CVariableSurObjetServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CVariableSurObjet.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CVariableSurObjet champ = (CVariableSurObjet)objet;
			
				if (champ.Nom == "")
					result.EmpileErreur(I.T("The variable name cannot be empty|155"));
				if (!CObjetDonneeAIdNumerique.IsUnique(champ, CVariableSurObjet.c_champNom, champ.Nom))
					result.EmpileErreur(I.T("A variable with this name already exist|157"));
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CVariableSurObjet);
		}
	}
}
