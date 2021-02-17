using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;



namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CFormulaireServeur.
	/// </summary>
	public class CFormulaireServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CFormulaireServeur()
			:base()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CFormulaireServeur(int nIdSession)
			:base(nIdSession)
		{
		}


		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CFormulaire.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CFormulaire);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CFormulaire formulaire = (CFormulaire)objet;
				if ( formulaire.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The form name cannot be empty|119"));
				if (!CObjetDonneeAIdNumerique.IsUnique(formulaire, CFormulaire.c_champLibelle, formulaire.Libelle))
					result.EmpileErreur(I.T("The form name '@1' is already used|120",formulaire.Libelle));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
		
	}
}
