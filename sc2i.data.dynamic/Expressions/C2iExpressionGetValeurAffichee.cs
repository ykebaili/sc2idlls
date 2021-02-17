using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.multitiers.client;


namespace sc2i.data.dynamic
{
    /// <summary>
    /// Retourne l'entité du type demandé ayant l'identifiant demandé
    /// </summary>
    /// <remarks>
    /// <LI>
    /// Le premier paramètre représente le type de l'objet
    /// </LI>
    /// <LI>
    /// Le second paramètre représente l'identifiant de l'objet
    /// </LI>
    /// </remarks>
    [Serializable]
    [AutoExec("Autoexec")]
    public class C2iExpressionGetValeurAffichee : C2iExpressionAnalysable
    {
        /// //////////////////////////////////////////
        public C2iExpressionGetValeurAffichee()
        {
        }


        /// //////////////////////////////////////////
        public static void Autoexec()
        {
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionGetValeurAffichee().IdExpression,
                typeof(C2iExpressionGetValeurAffichee));
        }

        /// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
        {
            CInfo2iExpression info = new CInfo2iExpression(0, "GetDisplayedValue",
                new CTypeResultatExpression(typeof(string), false),
                I.TT(GetType(), "GetDisplayedValue([IdChamp,] returned value)\n Returns the displayed field value from a returned value (for multiple choice fields)|223"),
                CInfo2iExpression.c_categorieDivers);
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(int), typeof(object)));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(string), typeof(object)));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(object)));

            return info;
        }

        /// //////////////////////////////////////////
        public override int GetNbParametresNecessaires()
        {
            return -1;
        }


        /// //////////////////////////////////////////
        public override CTypeResultatExpression TypeDonnee
        {
            get
            {
                return new CTypeResultatExpression(typeof(string), false);
            }
        }

        /// //////////////////////////////////////////
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] listeParametres)
        {
            CResultAErreur result = CResultAErreur.True;

            try
            {
                CDbKey dbKeyIdChamp = null;
                object valeurRetournee = null;
                //TESTDBKEYOK : le premier paramètre peut être un Id ou un UniversalId de champ
                if (listeParametres.Length == 2)
                {
                    if (listeParametres[0] is int)
                    {
                        int nIdChamp = (int)listeParametres[0];
                        dbKeyIdChamp = CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nIdChamp);
                    }
                    else
                        dbKeyIdChamp = CDbKey.CreateFromStringValue((string)listeParametres[0]);

                    valeurRetournee = listeParametres[1];
                }

                if (listeParametres.Length == 1)
                {
                    //1 seul paramètre, ce doit être une expression variable avec une variable champ
                    C2iExpressionChamp exChamp = Parametres2i[0] as C2iExpressionChamp;
                    if (exChamp != null)
                    {
                        CDefinitionProprieteDynamiqueChampCustom def = exChamp.DefinitionPropriete as CDefinitionProprieteDynamiqueChampCustom;
                        if (def != null)
                            dbKeyIdChamp = def.DbKeyChamp;
                    }
                    valeurRetournee = listeParametres[0];
                }
                if (valeurRetournee == null || dbKeyIdChamp == null)
                {
                    result.Data = "";
                    return result;
                }

                CContexteDonnee contexteDonnee = (CContexteDonnee)ctx.GetObjetAttache(typeof(CContexteDonnee));
                if (contexteDonnee == null)
                {
                    contexteDonnee = new CContexteDonnee(CSessionClient.GetSessionUnique().IdSession, true, false);
                    ctx.AttacheObjet(typeof(CContexteDonnee), contexteDonnee);
                }
                CChampCustom champ = new CChampCustom(contexteDonnee);
                if (champ.ReadIfExists(dbKeyIdChamp))
                {
                    if (!champ.IsChoixParmis())
                        result.Data = valeurRetournee.ToString();
                    else
                    {
                        foreach (CValeurChampCustom valeur in champ.ListeValeurs)
                        {
                            if (valeur.Value.Equals(valeurRetournee))
                            {
                                result.Data = valeur.Display;
                                return result;
                            }
                        }
                    }
                }
                result.Data = "";
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

    }
}
