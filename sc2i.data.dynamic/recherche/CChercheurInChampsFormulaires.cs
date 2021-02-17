using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using sc2i.common.recherche;
using sc2i.expression;
using sc2i.expression.recherche;
using sc2i.formulaire;

namespace sc2i.data.dynamic.recherche
{
    
    [AutoExec("Autoexec")]
    public class CChercheurInFormulaires : IChercheurObjetsCherchables
    {
        //----------------------------------------------
        public static void Autoexec()
        {
            CMoteurRechercheObjetCherchable.RegisterChercheur(typeof(CChercheurInFormulaires));
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
                CListeObjetDonneeGenerique<CFormulaire> lstFormulaires = new CListeObjetDonneeGenerique<CFormulaire>(contexte);
                foreach (CFormulaire form in lstFormulaires)
                {
                    C2iWnd wnd = form.Formulaire;
                    try
                    {
                        if (wnd != null)
                        {
                            resultat.PushChemin(new CNoeudRechercheObjet_ObjetDonnee(form));
                            wnd.ChercheObjet(objetCherche, resultat);
                            resultat.PopChemin();
                        }
                    }
                    catch 
                    {
                        System.Console.WriteLine("Erreur recherche formulaire " + form.Libelle);
                    }
                }
            }
        }

    }
}
