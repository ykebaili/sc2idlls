using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.unites;

namespace sc2i.data
{
    /// <summary>
    /// Classe se chargeant de stocker des valeurs avec unité
    /// dans la base de données.
    /// </summary>
    public static class CUtilUniteObjetDonnee
    {

        /// <summary>
        /// Stocke une valeur unité
        /// </summary>
        /// <param name="valeur"></param>
        /// <param name="objet"></param>
        /// <param name="strChampValeur"></param>
        /// <param name="strChampUnite"></param>
        public static void SetValeur(CValeurUnite valeur,
            CObjetDonnee objet, 
            string strChampValeur,
            string strChampUnite)
        {
            if (valeur == null)
            {
                objet.Row[strChampValeur] = DBNull.Value;
                objet.Row[strChampUnite] = "";
            }
            else
            {
                objet.Row[strChampValeur] = valeur.ConvertToBase();
                objet.Row[strChampUnite] = valeur.Unite;
            }
        }

        /// <summary>
        /// Récupère une valeur unité
        /// </summary>
        /// <param name="objet"></param>
        /// <param name="strChampValeur"></param>
        /// <param name="strChampUnite"></param>
        /// <returns></returns>
        public static CValeurUnite GetValeur(CObjetDonnee objet,
            string strChampValeur,
            string strChampUnite)
        {
            if (objet == null)
                return null;
            double? fValeur = objet.Row.Get<double?>(strChampValeur);
            if (fValeur == null)
                return null;
            string strUnite = objet.Row.Get<string>(strChampUnite);
            if (strUnite != null)
            {
                CValeurUnite v = CValeurUnite.GetValeurFromValeurBase(fValeur.Value, strUnite);
                return v;
            }
            return new CValeurUnite(fValeur.Value, "");

        }
    }
}
