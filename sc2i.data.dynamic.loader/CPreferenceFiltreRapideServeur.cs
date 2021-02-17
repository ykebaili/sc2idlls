using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CPreferenceFiltreRapideServeur : CObjetServeurAvecBlob, IObjetServeur
	{
		///////////////////////////////////////////////////
		public CPreferenceFiltreRapideServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CPreferenceFiltreRapide.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CPreferenceFiltreRapide filtre = (CPreferenceFiltreRapide)objet;

                if (filtre.TypeObjets == null)
                {
                    result.EmpileErreur(I.T("Easy filter setup should have a data type|20000"));
                    return result;
                }
                if (!IsUnique(filtre, new string[] { CPreferenceFiltreRapide.c_champTypeObjets },
                    new string[] { filtre.TypeObjetsString }))
                {
                    result.EmpileErreur(I.T("Another Easy filter setup exists for that data type|20001"));
                    return result;
                }

			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CPreferenceFiltreRapide);
		}
	}
}
