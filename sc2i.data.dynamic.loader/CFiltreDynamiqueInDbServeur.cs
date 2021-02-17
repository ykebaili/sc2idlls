using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
using System.Data;



namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CFormulaireServeur.
	/// </summary>
	public class CFiltreDynamiqueInDbServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CFiltreDynamiqueInDbServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CFiltreDynamiqueInDbServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CFiltreDynamiqueInDb.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CFiltreDynamiqueInDb);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CFiltreDynamiqueInDb filtre = (CFiltreDynamiqueInDb)objet;
				if ( filtre.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The filter name cannot be empty|117"));
				CFiltreData filtreData = new CFiltreData(
					CFiltreDynamiqueInDb.c_champLibelle+"=@1 and "+
					CFiltreDynamiqueInDb.c_champTypeElements+"=@2 and "+
					CFiltreDynamiqueInDb.c_champId+"<>@3",
					filtre.Libelle,
					filtre.StringTypeElements,
					filtre.Id);
				if ( CountRecords(CFiltreDynamiqueInDb.c_nomTable, filtreData) != 0 )
					result.EmpileErreur(I.T("This name is already used|118"));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
	}
}
