using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using sc2i.common.recherche;
using sc2i.expression;
using sc2i.expression.recherche;

namespace sc2i.data.dynamic.recherche
{
    
    [AutoExec("Autoexec")]
    public class CChercheurInChampsCalcules : IChercheurObjetsCherchables
    {
        //----------------------------------------------
        public static void Autoexec()
        {
            CMoteurRechercheObjetCherchable.RegisterChercheur(typeof(CChercheurInChampsCalcules));
        }

        //----------------------------------------------
        public bool CanFind(object objetCherche)
        {
            return typeof(CDefinitionProprieteDynamique).IsAssignableFrom ( objetCherche.GetType() );
        }

        //----------------------------------------------
        public void ChercheObjet(object objetCherche, CResultatRequeteRechercheObjet resultat)
        {
            if (objetCherche is CDefinitionProprieteDynamique)
            {
                CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
                CListeObjetDonneeGenerique<CChampCalcule> lstChamps = new CListeObjetDonneeGenerique<CChampCalcule>(contexte);
                foreach (CChampCalcule champ in lstChamps)
                {
                    C2iExpression formule = champ.Formule;
                    if (formule != null)
                    {
                        if (CTesteurUtilisationDefinitionChampInExpression.GetInstance().DoesUse(formule, objetCherche))
                        {
                            resultat.AddResultat(new CNoeudRechercheObjet_ObjetDonnee(champ));
                        }
                    }
                }
            }
        }

    }
}
