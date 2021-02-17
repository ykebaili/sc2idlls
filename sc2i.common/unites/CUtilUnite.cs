using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites
{
    public static class CUtilUnite
    {
        //-------------------------------------------------------
        /// <summary>
        /// Retourne un tableau de string avec les unités contenues dans l'unité
        /// si l'unité est en diviseur, sa valeur est /ID
        /// </summary>
        /// <param name="strUnite"></param>
        /// <returns></returns>
        public static string[] Developpe(string strUnite)
        {
            List<string> lstComposants = new List<string>();
            char[] cars = strUnite.Trim().ToUpper().ToCharArray();
            int nLength = strUnite.Trim().Length;
            int nCar = 0;
            StringBuilder blLast = new StringBuilder();
            foreach (char c in cars)
            {
                nCar++;
                if (c == '.' || c == '/' || c == '*' || nCar == nLength)
                {
                    if (nCar == nLength)
                        blLast.Append(c);
                    if (blLast.Length > 0)
                    {
                        int nIndexNum = blLast.Length;
                        while (nIndexNum > 0 && "0123456789²".IndexOf(blLast[nIndexNum - 1]) >= 0)
                            nIndexNum--;
                        int nFois = 1;
                        string strText = blLast.ToString();
                        if (nIndexNum <blLast.Length)
                        {
                            if (blLast.ToString().Substring(nIndexNum) == "²")
                                nFois = 2;
                            else
                                nFois = Int32.Parse(blLast.ToString().Substring(nIndexNum));
                            strText = blLast.ToString().Substring(0, nIndexNum);
                        }
                        for (int n = 0; n < nFois; n++)
                            lstComposants.Add(strText);
                    }
                    blLast = new StringBuilder();
                    if (c == '/')
                        blLast.Append('/');
                }
                else
                    blLast.Append(c);
            }

            //Simplification
            bool bHasChange = true;
            while (bHasChange)
            {
                bHasChange = false;
                for (int nCompo = lstComposants.Count - 1; nCompo > 0; nCompo--)
                {
                    string strComposant = lstComposants[nCompo];
                    if (strComposant[0] != '/')
                    {
                        int nIndex = lstComposants.LastIndexOf('/' + strComposant);
                        if (nIndex >= 0)
                        {
                            bHasChange = true;
                            if (nIndex > nCompo)
                            {
                                lstComposants.RemoveAt(nIndex);
                                lstComposants.RemoveAt(nCompo);
                            }
                            else
                            {
                                lstComposants.RemoveAt(nCompo);
                                lstComposants.RemoveAt(nIndex);
                            }
                            break;
                        }
                    }
                }
            }


            return lstComposants.ToArray();
        }

        //-------------------------------------------------------
        private class CSorterDivEnFin : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                char c1, c2;
                c1 = x[0];
                c2 = y[0];
                if ((c1 == '/' && c2 == '/') || (c1 != '/' && c2 != '/'))
                    return 0;
                if (c1 == '/')
                    return 1;
                if (c2 == '/')
                    return -1;
                return 0;
            }
        }
        
        //-------------------------------------------------------
        public static string Factorise(string[] strComposants)
        {
            List<String> lstComposants = new List<string>(strComposants);
            StringBuilder bl = new StringBuilder();
            //Passe les divisions en fin
            lstComposants.Sort(new CSorterDivEnFin());

            while (lstComposants.Count > 0)
            {
                int nCount = 1;
                string strCompo = lstComposants[0];
                for (int nSuite = lstComposants.Count - 1; nSuite >= 1; nSuite--)
                {
                    if (lstComposants[nSuite] == strCompo)
                    {
                        nCount++;
                        lstComposants.RemoveAt(nSuite);

                    }
                    else if (lstComposants[nSuite] == "/" + strCompo)
                    {
                        nCount--;
                        lstComposants.RemoveAt(nSuite);
                    }
                }
                if (nCount > 0)
                {
                    if (strCompo[0] != '/' && bl.Length > 0)
                        bl.Append('.');
                    bl.Append(strCompo);
                    if (nCount > 1)
                    {
                        if (nCount == 2)
                            bl.Append("²");
                        else
                            bl.Append(nCount);
                    }
                }
                lstComposants.RemoveAt(0);
            }
            

            return bl.ToString();
        }

        

        //------------------------------------
        public static string GetUniteInverse(string strUnite)
        {
            List<string> lstComposants = new List<string>(Developpe(strUnite));
            for (int n = 0; n < lstComposants.Count; n++)
            {
                string strTmp = lstComposants[n];
                if (strTmp[0] == '/')
                    strTmp = strTmp.Substring(1);
                else
                    strTmp = "/" + strTmp;
                lstComposants[n] = strTmp;
            }
            return Factorise(lstComposants.ToArray());
        }
                    

        //------------------------------------
        //Se débrouille pour que chaque classe d'unité ne soit
        //représentée que par une seule et même unité
        public static string Harmonise(string strUnite)
        {
            string[] strComposants = Developpe(strUnite);
            List<string> lstComposants = new List<string>(strComposants);
            HashSet<int> setFaits = new HashSet<int>();
            for (int nComposant = 0; nComposant < lstComposants.Count; nComposant++)
            {
                if (!setFaits.Contains(nComposant))
                {
                    setFaits.Add(nComposant);
                    string strCompo1 = lstComposants[nComposant];
                    if (strCompo1[0] == '/')
                    {
                        strCompo1 = strCompo1.Substring(1);
                    }
                    IUnite u1 = CGestionnaireUnites.GetUnite(strCompo1);
                    if (u1 == null)
                        return strUnite;
                    //Cherche les autres éléments de la même classe
                    for (int n = nComposant + 1; n < lstComposants.Count; n++)
                    {
                        if (!setFaits.Contains(n))
                        {
                            string strCompo2 = lstComposants[n];
                            bool bDiviser = false;
                            if (strCompo2[0] == '/')
                            {
                                bDiviser = true;
                                strCompo2 = strCompo2.Substring(1);
                            }
                            IUnite u2 = CGestionnaireUnites.GetUnite(strCompo2);
                            if (u2 == null)
                                return strUnite;
                            if (u2.Classe.GlobalId == u1.Classe.GlobalId)
                            {
                                setFaits.Add(n);
                                if (strCompo2 != strCompo1)
                                {
                                    string strTmp = strCompo1;
                                    if (bDiviser)
                                        strTmp = "/" + strTmp;
                                    lstComposants[n] = strTmp;
                                }
                            }
                        }
                    }
                }
            }
            StringBuilder bl = new StringBuilder();
            foreach (string strCompo in lstComposants)
            {
                if (bl.Length > 0 && strCompo[0] != '/')
                    bl.Append('.');
                bl.Append(strCompo);
            }
            return bl.ToString();
        }

        
        //------------------------------------
        //Se débrouille pour que les éléments qui ont la même classe d'unité travaillent dans
        //la même unité
        public static string GetUniteHarmonisee(string strUniteReference, string strUniteAHarmoniser)
        {
            string[] strComposantsRef = Developpe(strUniteReference);
            string[] strComposantsAHarmoniser = Developpe(strUniteAHarmoniser);
            List<string> lstAHarmoniser = new List<string>(strComposantsAHarmoniser);
            List<string> lstFinale = new List<string>();
            HashSet<string> classesFaites = new HashSet<string>();
            HashSet<int> setFaits = new HashSet<int>();
            foreach (string strCompoRef in strComposantsRef)
            {
                string strRef = strCompoRef;
                if (strRef[0] == '/')
                    strRef = strRef.Substring(1);
                IUnite u1 = CGestionnaireUnites.GetUnite(strRef);
                if (u1 == null)
                    return strUniteAHarmoniser;
                IClasseUnite classe = u1.Classe;
                if (!classesFaites.Contains(classe.GlobalId))
                {
                    classesFaites.Add(classe.GlobalId);
                    for (int n = 0; n < lstAHarmoniser.Count; n++)
                    {
                        if (!setFaits.Contains(n))
                        {
                            string strHar = lstAHarmoniser[n];
                            bool bDiviser = false;
                            if (strHar[0] == '/')
                            {
                                bDiviser = true;
                                strHar = strHar.Substring(1);
                            }
                            IUnite u2 = CGestionnaireUnites.GetUnite(strHar);
                            if (u2 == null)
                                return strUniteAHarmoniser;
                            if (u2.Classe.GlobalId == u1.Classe.GlobalId)
                            {
                                setFaits.Add(n);
                                if (strHar != strRef)
                                {
                                    string strTmp = strRef;
                                    if (bDiviser)
                                        strTmp = "/" + strRef;
                                    lstAHarmoniser[n] = strTmp;
                                }
                            }
                        }
                    }
                }
            }
            StringBuilder bl = new StringBuilder();
            foreach (string strCompo in lstAHarmoniser)
            {
                if (bl.Length > 0 && strCompo[0] != '/')
                    bl.Append('.');
                bl.Append(strCompo);
            }
            return bl.ToString();

        }



        
        //-------------------------------------------------------
        public static string GetIdClasseUnite(string strUnite)
        {
            string[] strUnits = Developpe(strUnite);
            List<string> lstClasses = new List<string>();
            StringBuilder bl = new StringBuilder();
            foreach (string strCompo in strUnits)
            {
                bool bDiviser = false;
                string strCle = strCompo;
                if (strCompo[0] == '/')
                {
                    bDiviser = true;
                    strCle = strCompo.Substring(1);
                }
                IUnite unite = CGestionnaireUnites.GetUnite(strCle);
                if (unite == null || unite.Classe == null)
                    return null;
                string strClasse = unite.Classe.GlobalId;
                if (bDiviser)
                    strClasse = "/" + strClasse;
                else
                    strClasse = "." + strClasse;
                lstClasses.Add(strClasse);
            }
            lstClasses.Sort();
            foreach (string str in lstClasses)
                bl.Append(str);
            return bl.ToString();
        }
            

    }
}
