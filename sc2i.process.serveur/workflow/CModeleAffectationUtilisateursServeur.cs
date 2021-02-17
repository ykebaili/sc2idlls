using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.workflow;
using sc2i.common;

namespace sc2i.process.serveur.workflow
{
    public class CModeleAffectationUtilisateursServeur : CObjetServeurAvecBlob
    {

        /// //////////////////////////////////////////////////
        public CModeleAffectationUtilisateursServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CModeleAffectationUtilisateurs.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CModeleAffectationUtilisateurs modeleAffectationUtilisateurs = objet as CModeleAffectationUtilisateurs;
            if ( modeleAffectationUtilisateurs != null )
            {
                if ( modeleAffectationUtilisateurs.Libelle.Length == 0 )
                {
                    result.EmpileErreur(I.T("Assignment template must have a label|20010"));
                }
            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CModeleAffectationUtilisateurs);
        }
    }
}
