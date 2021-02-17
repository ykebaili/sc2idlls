using System;
using System.IO;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;

#if !PDA_DATA

namespace sc2i.documents.serveur
{
	/// <summary>
	/// Description résumée de C2iCategorieRapportCrystalServeur.
	/// </summary>
	public class C2iCategorieRapportCrystalServeur : CObjetServeur
	{
		public C2iCategorieRapportCrystalServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return C2iCategorieRapportCrystal.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				C2iCategorieRapportCrystal cat = (C2iCategorieRapportCrystal)objet;

				if (cat.Libelle == "")
					result.EmpileErreur(I.T("The Crystal Report Category name cannot be empty|101"));

				if (!CObjetDonneeAIdNumerique.IsUnique(cat, C2iCategorieRapportCrystal.c_champLibelle, cat.Libelle))
					result.EmpileErreur(I.T("Another Crystal Report Category exist with this name|102"));
					
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
			return typeof(C2iCategorieRapportCrystal);
		}
		//-------------------------------------------------------------------
	}
}
#endif