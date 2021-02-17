using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;



namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CEasyQueryInDbServeur.
	/// </summary>
	public class CEasyQueryInDbServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CEasyQueryInDbServeur()
			:base()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CEasyQueryInDbServeur(int nIdSession)
			:base(nIdSession)
		{
		}


		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CEasyQueryInDb.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CEasyQueryInDb);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CEasyQueryInDb easyQueryInDb = (CEasyQueryInDb)objet;
				if ( easyQueryInDb.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The Stored query name cannot be empty|20009"));
				if (!CObjetDonneeAIdNumerique.IsUnique(easyQueryInDb, CEasyQueryInDb.c_champLibelle, easyQueryInDb.Libelle))
					result.EmpileErreur(I.T("The stored query name '@1' is already used|20008",easyQueryInDb.Libelle));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
		
	}
}
