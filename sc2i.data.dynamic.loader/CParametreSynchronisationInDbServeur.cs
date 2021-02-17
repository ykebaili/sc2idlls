using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;


#if !PDA_DATA
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CParametreSynchronisationInDbServeur.
	/// </summary>
	public class CParametreSynchronisationInDbServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CParametreSynchronisationInDbServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CParametreSynchronisationInDbServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CParametreSynchronisationInDb.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CParametreSynchronisationInDb);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CParametreSynchronisationInDb parametre = (CParametreSynchronisationInDb)objet;
				if ( parametre.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("The parameter name cannot be empty|129"));
				if ( parametre.Code.Trim() == "" )
					result.EmpileErreur(I.T("The parameter code cannot be empty|130"));
				if (!CObjetDonneeAIdNumerique.IsUnique(parametre, CParametreSynchronisationInDb.c_champCode, parametre.Code))
					result.EmpileErreur(I.T("Another parameter with this code already exist|131"));
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