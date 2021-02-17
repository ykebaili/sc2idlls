using System;

using sc2i.common;

using sc2i.expression;
using System.Collections;
using System.Collections.Generic;
using sc2i.multitiers.client;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.data.dynamic
{
	

	[AutoExec("Autoexec")]
    public class CFournisseurFonctionsDynamiquesInDb : IFournisseurFonctionsDynamiquesSupplementaire
	{
        //-----------------------------------------------------------------------------------------------
		public static void Autoexec()
		{
			CFournisseurFonctionsDynamiques.RegisterFournisseurSupplementaire(new CFournisseurFonctionsDynamiquesInDb());
		}

        //-----------------------------------------------------------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamique> GetDefinitionsFonctionsSupplementaires(CObjetPourSousProprietes objet)
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			if ( objet == null )
				return lstProps.ToArray();
			Type tp = objet.TypeAnalyse;
			if ( tp == null )
				return lstProps.ToArray();

            if (!C2iFactory.IsInit())
                return lstProps.ToArray();

			CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
			CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, typeof(CFonctionDynamiqueInDb));
            liste.Filtre = new CFiltreData(CFonctionDynamiqueInDb.c_champTypeObjets + "=@1", tp.ToString());
			foreach (CFonctionDynamiqueInDb fonctionInDb in liste)
			{
                CFonctionDynamique fonction = fonctionInDb.Fonction;
                if (fonction != null)
                {
                    CDefinitionFonctionDynamique def = new CDefinitionFonctionDynamique(fonction);
                    if (fonctionInDb.Categorie.Length == 0)
                        def.Rubrique = I.T("Methods|58");
                    else
                        def.Rubrique = fonctionInDb.Categorie;
                    lstProps.Add(def);
                }

            }
			return lstProps.ToArray();
		}

        public CFonctionDynamique GetFonctionSupplementaire(string strIdFonction)
        {
            CFonctionDynamiqueInDb f = new CFonctionDynamiqueInDb(CContexteDonneeSysteme.GetInstance());
            if (f.ReadIfExistsUniversalId(strIdFonction))
                return f.Fonction;
            return null;
        }

    }


}
