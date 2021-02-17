using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;



namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CRelationFormulaireChampCustomServeur.
	/// </summary>
	public class CRelationFormulaireChampCustomServeur : CObjetDonneeServeurAvecCache
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CRelationFormulaireChampCustomServeur()
			:base()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CRelationFormulaireChampCustomServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CRelationFormulaireChampCustom.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CRelationFormulaireChampCustom);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CRelationFormulaireChampCustom rel = (CRelationFormulaireChampCustom)objet;
				if ( rel.Formulaire == null )
					result.EmpileErreur(I.T("The form cannot be null|138"));
				if ( rel.Champ == null )
					result.EmpileErreur(I.T("The field cannot be null|139"));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
		
	}
}
