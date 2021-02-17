using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using sc2i.common;
using System.Collections;
using System.Data;

namespace sc2i.data.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public class CPlageDonneeARecherchePremiereCelluleHautGauche : IPlageDonnees
    {

        private string m_strPremièreCelluleHG; // Cellule en haute à gauche de la plage à importer
        
        private string[] m_strEntetes;
        private CFichierExcelOleDb m_fichierExcel = new CFichierExcelOleDb();


        public CPlageDonneeARecherchePremiereCelluleHautGauche()
        {

        }


        public string FirstCellTopLeft
        {
            get
            {
                return m_strPremièreCelluleHG;
            }
        }

        /// <summary>
        /// Recherche la référence de la première cellule ctrouvée contenant la valeur indiquée
        /// Se limite dans la recherhce aux nMaxLine premières lignes et nMaxCol premières colonnes
        /// </summary>
        /// <param name="strValeurAttendue"></param>
        /// <returns></returns>
        public string GetFirstCellForValue(string strValeurAttendue, int nMaxCol, int nMaxLine)
        {
            for (int l = 1; l < nMaxLine; l++)
            {
                // Commence en A1
                string cColonne = "A";

                for (int c = 0; c < nMaxCol; c++)
                {
                    string strCell = cColonne + l.ToString();
                    object valeur = m_fichierExcel.GetValue(strCell);

                    if (valeur != null && valeur.ToString() == strValeurAttendue)
                        return strCell;

                    cColonne = ((char)(((int)cColonne[0]) + 1)).ToString();
                }


            }

            return "";
        }


        #region IPlageDonnees Membres

        private Dictionary<string, string> m_dicColHeaderColName = new Dictionary<string, string>();

        public string[] Entetes
        {
            get
            {
                return new List<string>(m_dicColHeaderColName.Keys).ToArray();
            }
        }


        public CResultAErreur InitEntetes()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_fichierExcel == null)
            {
                result.EmpileErreur("");
                return result;
            }

            Regex expCol = new Regex("[A-Z]");
            Regex expLine = new Regex("[0-9]");

            string strExcelCol = expCol.Match(m_strPremièreCelluleHG).ToString();
            string strExcelLine = expLine.Match(m_strPremièreCelluleHG).ToString();

            string strTitreColonne = "";

            // Récupèration des colonnes et construit le dictionnaire NomDeColonne / EntetExcel
            // Util pour les focntions GetFirstLine et GetNextLine
            do
            {
                strTitreColonne = string.Empty;
                object cellValue = m_fichierExcel.GetValue(strExcelCol, strExcelLine);
                if (cellValue != null)
                    strTitreColonne = cellValue.ToString();

                if (strTitreColonne != string.Empty)
                {
                    m_dicColHeaderColName.Add(strTitreColonne, strExcelCol);
                }
                strExcelCol = CUtilExcel.GetNextColumnHeader(strExcelCol);

            } while (strTitreColonne != string.Empty);

            return result;
        }

        private int m_nCurrentLineIndex = 1; 
        public object[] GetFirstLine()
        {
            m_nCurrentLineIndex = 1; // Pour la première ligne
            ArrayList listeValeurs = new ArrayList();

            Regex expCol = new Regex("[A-Z]");
            Regex expLine = new Regex("[0-9]");

            string strExcelCol = expCol.Match(m_strPremièreCelluleHG).ToString();
            string strExcelLine = expLine.Match(m_strPremièreCelluleHG).ToString();

            strExcelLine = (Int32.Parse(strExcelLine) + m_nCurrentLineIndex).ToString();

            foreach (string strTitreColonne in m_dicColHeaderColName.Keys)
            {
                string colHeader = "";
                if (m_dicColHeaderColName.TryGetValue(strTitreColonne, out colHeader))
                {
                    listeValeurs.Add(m_fichierExcel.GetValue(colHeader, strExcelLine));
                }

            }

            m_nCurrentLineIndex = 2;
            return listeValeurs.ToArray();

        }

        public object[] GetNextLine()
        {
            ArrayList listeValeurs = new ArrayList();

            Regex expCol = new Regex("[A-Z]");
            Regex expLine = new Regex("[0-9]");

            string strExcelCol = expCol.Match(m_strPremièreCelluleHG).ToString();
            string strExcelLine = expLine.Match(m_strPremièreCelluleHG).ToString();

            strExcelLine = (Int32.Parse(strExcelLine) + m_nCurrentLineIndex).ToString();

            foreach (string strTitreColonne in m_dicColHeaderColName.Keys)
            {
                string colHeader = "";
                if (m_dicColHeaderColName.TryGetValue(strTitreColonne, out colHeader))
                {
                    listeValeurs.Add(m_fichierExcel.GetValue(colHeader, strExcelLine));
                }

            }

            return listeValeurs.ToArray();
        }

        #endregion
    }
}
