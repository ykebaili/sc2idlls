using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
using sc2i.data;
using sc2i.process;



namespace sc2i.process.serveur
{
	/// <summary>
	/// Description résumée de CFormulaireServeur.
	/// </summary>
	public class CGroupeParametrageServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CGroupeParametrageServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CGroupeParametrageServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CGroupeParametrage.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CGroupeParametrage);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CGroupeParametrage groupe = (CGroupeParametrage)objet;
				if ( groupe.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The label of the group of parameter setting should not be empty|111"));
				if (!CObjetDonneeAIdNumerique.IsUnique(groupe, CGroupeParametrage.c_champLibelle, groupe.Libelle))
					result.EmpileErreur(I.T("The @1 group already exists|112", groupe.Libelle));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
		
	}
}
