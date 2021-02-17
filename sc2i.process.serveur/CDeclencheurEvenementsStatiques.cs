using System;
using System.Reflection;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using sc2i.data.serveur;
using sc2i.process;
using sc2i.expression;
using sc2i.multitiers.server;

namespace sc2i.process.serveur
{
    /// <summary>
    /// Outils permettant de déclencher les evements 'statiques' d'un objet
    /// Une évenement statique est un évenement sur création, ou un évenement
    /// sur modification sans valeur initiale et executable une seule fois sur l'élement
    /// </summary>
    public class CDeclencheurEvenementsStatiques : C2iObjetServeur, IDeclencheurEvenementsStatiques
    {
        /// //////////////////////////////////////////////////////////////////////////
        public CDeclencheurEvenementsStatiques(int nIdSession)
            : base(nIdSession)
        {
        }

        /// //////////////////////////////////////////////////////////////////////////
        public CResultAErreur DeclencheEvenementStatiques(Type typeObjet, CDbKey dbKeyObjet)
        {
            CResultAErreur result = CResultAErreur.True;
            using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
            {
                CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(typeObjet, new object[] { contexte });
                if (!objet.ReadIfExists(dbKeyObjet))
                {
                    result.EmpileErreur(I.T("The @1 object with id @2 doesn't exist|106", DynamicClassAttribute.GetNomConvivial(typeObjet), dbKeyObjet.StringValue));
                    return result;
                }
                IDeclencheurAction[] declencheurs = CRecuperateurDeclencheursActions.GetDeclencheursAssocies(objet);
                foreach (IDeclencheurAction declencheur in declencheurs)
                {
                    if (declencheur is CEvenement)
                    {
                        CEvenement evt = (CEvenement)declencheur;
                        CInfoDeclencheurProcess infoDeclencheur = new CInfoDeclencheurProcess();
                        bool bShouldDeclenche = false;
                        if (!evt.DejaDeclenchePourEntite(objet))
                            bShouldDeclenche = evt.ParametreDeclencheur.ShouldDeclenche(
                                    objet,
                                    false,
                                    true,
                                    ref infoDeclencheur);
                        if (bShouldDeclenche)
                            evt.EnregistreDeclenchementEvenement(objet, infoDeclencheur);
                        if (!result)
                            return result;
                    }
                }
            }
            return result;
        }
    }
}
