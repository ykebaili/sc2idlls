using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;


#if !PDA_DATA
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CGroupeUtilisateursSynchronisationServeur.
	/// </summary>
	public class CGroupeUtilisateursSynchronisationServeur : CObjetServeur
	{
#if PDA
		public CGroupeUtilisateursSynchronisationServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CGroupeUtilisateursSynchronisationServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CGroupeUtilisateursSynchronisation.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CGroupeUtilisateursSynchronisation);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CGroupeUtilisateursSynchronisation groupe = (CGroupeUtilisateursSynchronisation)objet;
				if ( groupe.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The group name cannot be empty|124"));
				if ( groupe.Code.Trim() == "" )
					result.EmpileErreur(I.T("The group code cannot be empty|123"));
				if ( !CObjetDonneeAIdNumerique.IsUnique ( groupe, CGroupeUtilisateursSynchronisation.c_champCode, groupe.Code ))
					result.EmpileErreur(I.T("Another group with this code already exist|122"));
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