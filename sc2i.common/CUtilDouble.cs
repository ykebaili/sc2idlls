using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace sc2i.common
{
    /// <summary>
    /// Description résumée de CUtilDouble.
    /// </summary>
    public sealed class CUtilDouble
    {
        private static bool m_bTravailleAvecVirgules = false;
        private CUtilDouble() { }

        public static double DoubleFromString(string strChaine)
        {
            if (strChaine.Trim().Length == 0)
                throw new Exception("Double parse error");
            if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." && strChaine.Contains(","))
                strChaine = strChaine.Replace(",", ".");
            else if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "," && strChaine.Contains("."))
                strChaine = strChaine.Replace(".", ",");

            double dVal = 0;
            if (!Double.TryParse(strChaine, out dVal))
            {
                //Ancienne méthode, conservée pour compatiblité, mais elle ne
                //doit pas faire grand chose. L'interêt est qu'elle lance
                //une exception si besoin
                try
                {
                    if (m_bTravailleAvecVirgules && strChaine.Contains("."))
                        strChaine = strChaine.Replace(".", ",");
                    if (!m_bTravailleAvecVirgules && strChaine.Contains(","))
                        strChaine = strChaine.Replace(",", ".");

                    dVal = Double.Parse(strChaine);
                }
                catch
                {
                    //Remplace le . par un virgule et visversa
                    if (strChaine.Contains("."))
                    {
                        strChaine = strChaine.Replace(".", ",");
                        m_bTravailleAvecVirgules = true;
                    }
                    else
                    {
                        strChaine = strChaine.Replace(",", ".");
                        m_bTravailleAvecVirgules = false;
                    }
                    //On retente, ça peut planter !!!
                    if (!Double.TryParse(strChaine, out dVal))
                    {
                        // Extraire les caratères numériques
                        string strPattern = "[0123456789.,+-]+";
                        Regex exp = new Regex(strPattern);
                        Match chaineTrouvee = exp.Match(strChaine);
                        if (chaineTrouvee != null)
                        {
                            if(chaineTrouvee.ToString() != strChaine)
                                dVal = DoubleFromString(chaineTrouvee.ToString());
                        }

                    }

                }
            }
            return dVal;
        }

    }
}
