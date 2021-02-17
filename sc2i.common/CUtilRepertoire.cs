using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sc2i.common
{
    public static class CUtilRepertoire
    {
        //----------------------------------------------------------
        /// <summary>
        /// s'assure que le répertoire demandé existe
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static CResultAErreur AssureRepertoire(string strPath)
        {
            CResultAErreur result = CResultAErreur.True;
            if (Directory.Exists(strPath))
                return result;
            //Le répertoire n'existe pas, tente de le créer
            strPath.Replace("/", "\\");
            string[] strPaths = strPath.Split('\\');
            string strCurrent = "";
            foreach (string strPart in strPaths)
            {
                if (strCurrent.Length > 0)
                    strCurrent += "\\";
                strCurrent += strPart;
                if (!Directory.Exists(strCurrent))
                {
                    try
                    {
                        Directory.CreateDirectory(strCurrent);
                    }
                    catch (Exception e)
                    {
                        result.EmpileErreur(new CErreurException(e));
                        return result;
                    }
                }
            }
            return result;
        }

        //----------------------------------------------------------
        public static CResultAErreur AssureRepertoirePourFichier(string strFichier)
        {
            string strPath = Path.GetDirectoryName(strFichier);
            return AssureRepertoire(strPath);
        }

        //----------------------------------------------------------
        public static string GetValidFileName ( string strNomFichier, char cReplaceChar )
        {
            char[] chInvalides = System.IO.Path.GetInvalidFileNameChars();
            foreach ( char c in chInvalides )
                if ( strNomFichier.Contains ( c ) )
                    strNomFichier = strNomFichier.Replace ( c, cReplaceChar );
            return strNomFichier;
        }
    }
}
