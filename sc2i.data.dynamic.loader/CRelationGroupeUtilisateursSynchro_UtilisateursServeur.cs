using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;


#if !PDA_DATA
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CRelationGroupeUtilisateursSynchro_UtilisateursServeur.
	/// </summary>
	public class CRelationGroupeUtilisateursSynchro_UtilisateursServeur : CObjetServeur
	{
#if PDA
		public CRelationGroupeUtilisateursSynchro_UtilisateursServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CRelationGroupeUtilisateursSynchro_UtilisateursServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CRelationGroupeUtilisateursSynchro_Utilisateurs.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CRelationGroupeUtilisateursSynchro_Utilisateurs);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CRelationGroupeUtilisateursSynchro_Utilisateurs rel = (CRelationGroupeUtilisateursSynchro_Utilisateurs)objet;
				if ( rel.IdUtilisateur < 0 )
					result.EmpileErreur(I.T("The user ID in relation is false|140"));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
		
	}
}
#endif