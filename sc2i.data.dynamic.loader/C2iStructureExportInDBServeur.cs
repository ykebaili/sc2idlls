using System;
using System.Collections;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.data.dynamic;

#if !PDA_DATA

namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de C2iStructureExportInDBServeur.
	/// </summary>
	public class C2iStructureExportInDBServeur : CObjetServeurAvecBlob
	{
		public C2iStructureExportInDBServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return C2iStructureExportInDB.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				C2iStructureExportInDB structure = (C2iStructureExportInDB)objet;

				if (structure.Libelle == "")
					result.EmpileErreur(I.T("The structure label cannot be empty|103"));

				if (!CObjetDonneeAIdNumerique.IsUnique(structure, C2iStructureExportInDB.c_champLibelle, structure.Libelle))
					result.EmpileErreur(I.T("A structure with this label already exist|104"));
					
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(C2iStructureExportInDB);
		}
		//-------------------------------------------------------------------
	}
}
#endif