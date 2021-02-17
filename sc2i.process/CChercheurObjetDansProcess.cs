using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common.recherche;
using sc2i.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.process
{
    /// <summary>
    /// Sait trouver dans des process :
    /// -des CDefinitionProprieteDynamique
    /// -Des processIndb ( référence à des process )
    /// </summary>
    [AutoExec("Autoexec")]
    public class CChercheurObjetDansProcess : IChercheurObjetsCherchables
    {
        //----------------------------------------------------
        public static void Autoexec()
        {
            CMoteurRechercheObjetCherchable.RegisterChercheur ( typeof(CChercheurObjetDansProcess) );
        }

        //----------------------------------------------------
        public bool CanFind(object objetCherche)
        {
            if (objetCherche == null)
                return false;
            Type tp = objetCherche.GetType();
            if (typeof(CDefinitionProprieteDynamique).IsAssignableFrom(tp) ||
                typeof(CProcessInDb).IsAssignableFrom(tp))
                return true;
            return false;
        }

        //----------------------------------------------------
        public void  ChercheObjet(object objetCherche, CResultatRequeteRechercheObjet resultat)
        {
            CContexteDonnee ctx = CContexteDonneeSysteme.GetInstance();
            CListeObjetDonneeGenerique<CProcessInDb> lstProcess = new CListeObjetDonneeGenerique<CProcessInDb>(ctx);
            foreach (CProcessInDb processInDb in lstProcess)
            {
                try
                {
                    processInDb.RechercheObjet(objetCherche, resultat);
                }
                catch { }
            }
            CListeObjetDonneeGenerique<CEvenement> lstEvents = new CListeObjetDonneeGenerique<CEvenement>(ctx);
            foreach (CEvenement evt in lstEvents)
            {
                try
                {
                    evt.RechercheObjet(objetCherche, resultat);
                }
                catch
                {
                }
            }

        }

    }
}
