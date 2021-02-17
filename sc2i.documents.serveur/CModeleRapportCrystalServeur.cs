using System;
using System.IO;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;

/*
namespace sc2i.documents.serveur
{
	/// <summary>
	/// Description r�sum�e de CModeleRapportCrystalServeur.
	/// </summary>
	public class CModeleRapportCrystalServeur : CObjetServeurAvecBlob
	{
		public CModeleRapportCrystalServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CModeleRapportCrystal.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CModeleRapportCrystal rapport = (CModeleRapportCrystal)objet;

				if (rapport.Libelle == "")
					result.EmpileErreur("Le libell� du mod�le de rapport ne peut �tre vide");

				if (!IsUnique(rapport,  CModeleRapportCrystal.c_champLibelle, rapport.Libelle))
					result.EmpileErreur("Un mod�le de rapport ayant ce libell� existe d�j�.");
					
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
			return typeof(CModeleRapportCrystal);
		}
		//-------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = base.TraitementAvantSauvegarde( contexte );

			return result;
		}
		//-------------------------------------------------------------------
	}
}
*/