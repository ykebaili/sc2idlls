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
    /// Récupère une données cumulée précalculée à partir d'un
    /// <see cref="CTypeDonneeCumulee">Type de donnée cumulé</see>
    /// </summary>
    [Serializable]
    [AutoExec("Autoexec")]
    public class C2iExpressionGetDonneeCumuleeTexte : C2iExpressionAnalysable
    {
        /// //////////////////////////////////////////
        public C2iExpressionGetDonneeCumuleeTexte()
        {
        }


        /// //////////////////////////////////////////
        public static void Autoexec()
        {
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionGetDonneeCumuleeTexte().IdExpression,
                typeof(C2iExpressionGetDonneeCumuleeTexte));
        }

        /// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
        {
            CInfo2iExpression info = new CInfo2iExpression(0, "GetCumulatedTextValue",
                new CTypeResultatExpression(typeof(double), true),
                I.TT(GetType(), "GetCumulatedTextValue([NoReadInDb], Code, nValue, key1, key2, ...) returns the Text Value nValue (indexed on 0) of cumulated data according to the CODE type, for specified keys values. If NoReadInDb is set to true(), data will be read in local context only|10013"),
                CInfo2iExpression.c_categorieGroupe);
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(string), typeof(int)));
            info.AddDefinitionParametres(new CInfo2iDefinitionParametres(typeof(bool), typeof(string), typeof(int)));
            return info;
        }

        /// //////////////////////////////////////////
        public override int GetNbParametresNecessaires()
        {
            return -1;//Nombre variable
        }


        /// //////////////////////////////////////////
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] listeParametres)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CContexteDonnee contexteDonnee = (CContexteDonnee)ctx.GetObjetAttache(typeof(CContexteDonnee));
                if (contexteDonnee == null)
                {
                    if (ctx.ObjetSource != null && ctx.ObjetSource is IObjetAContexteDonnee)
                        contexteDonnee = ((IObjetAContexteDonnee)ctx.ObjetSource).ContexteDonnee;
                    else
                        contexteDonnee = new CContexteDonnee(CSessionClient.GetSessionUnique().IdSession, true, false);
                    ctx.AttacheObjet(typeof(CContexteDonnee), contexteDonnee);
                }
                /*//Cherche un objet donnée dans le contexte d'évaluation pour pouvoir
                //avoir un contexte de donnee;
                if ( !(ctx.ObjetSource is CObjetDonnee) )
                {
                    result.EmpileErreur("Impossible de récupérer une donnée de production si la source de la formule n'est pas un objet donnee");
                    return result;
                }
                CContexteDonnee contexte = ((CObjetDonnee)ctx.ObjetSource).ContexteDonnee;*/
                bool bHasNoReadInDb = listeParametres[0] is bool;
                bool bNePasLireEnBase = bHasNoReadInDb ? (bool)listeParametres[0] : false;
                int nStartParam = bHasNoReadInDb ? 1 : 0;
                CTypeDonneeCumulee typeDonnee = new CTypeDonneeCumulee(contexteDonnee);
                if (!typeDonnee.ReadIfExists(new CFiltreData(CTypeDonneeCumulee.c_champCode + "=@1",
                    listeParametres[0 + nStartParam].ToString())))
                {
                    result.EmpileErreur(I.T("The cumulated data type @1 does not exist|218", listeParametres[0 + nStartParam].ToString()));
                    return result;
                }
                CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteDonnee, typeof(CDonneeCumulee));
                CFiltreData filtre = new CFiltreData(
                    CTypeDonneeCumulee.c_champId + "=@1", typeDonnee.Id);
                //Indice de la clé
                int nCle = 0;
                CParametreDonneeCumulee parametre = typeDonnee.Parametre;
                for (int nParam = nStartParam + 2; nParam < listeParametres.Length; nParam++)
                {
                    if (listeParametres[nParam] != null)
                    {
                        CCleDonneeCumulee cle = parametre.GetChampCle(nCle);
                        while ((cle == null || cle.Champ == "") && nCle < CParametreDonneeCumulee.c_nbChampsCle)
                        {
                            nCle++;
                            cle = parametre.GetChampCle(nCle);
                        }
                        if (nCle > CParametreDonneeCumulee.c_nbChampsCle)
                            break;
                        else
                        {
                            if (cle.Champ != "")
                            {
                                filtre.Filtre += " and " + CDonneeCumulee.c_baseChampCle + nCle.ToString() + "=@" +
                                    (filtre.Parametres.Count + 1).ToString();
                                filtre.Parametres.Add(listeParametres[nParam].ToString());
                            }
                            nCle++;
                        }
                    }
                }
                liste.Filtre = filtre;
                liste.InterditLectureInDB = bNePasLireEnBase;
                if (liste.Count > 0)
                {
                    CDonneeCumulee donnee = (CDonneeCumulee)liste[0];
                    int nParam = Convert.ToInt32(listeParametres[nStartParam + 1]);
                    if (nParam < 0 || nParam > CParametreDonneeCumulee.c_nbChampsTexte)
                    {
                        result.EmpileErreur(I.T("The cumulated data has no value @1|219", nParam.ToString()));
                        return result;
                    }
                    if (donnee.Row[CDonneeCumulee.c_baseChampTexte + nParam.ToString()] == DBNull.Value)
                        result.Data = "";
                    else
                        result.Data = (string)donnee.Row[CDonneeCumulee.c_baseChampTexte + nParam.ToString()];
                }
                else
                    result.Data = "";
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                return result;
            }
            return result;
        }
    }
}
