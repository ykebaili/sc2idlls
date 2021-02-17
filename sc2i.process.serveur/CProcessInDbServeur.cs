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
	public class CProcessInDbServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CProcessInDbServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CProcessInDbServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CProcessInDb.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CProcessInDb);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CProcessInDb process = (CProcessInDb)objet;
				if ( process.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The Action label could not be empty|113"));
				if (!CObjetDonneeAIdNumerique.IsUnique(process, CProcessInDb.c_champLibelle, process.Libelle))
					result.EmpileErreur(I.T("An Action with the same label already exist|115"));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
		
	}
}
