using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using sc2i.common;
using System.Collections;
using System.Data;
using CarlosAg.ExcelXmlWriter;

namespace sc2i.data.Excel
{
    enum ETypeFichierExcel
    {
        Inconnu = -1,
        Xml = 0,
        OleDb
    }

    /// <summary>
    /// Cette classe sait lire duex types de fichier Excel
    /// Excel XML en utilisant la librairie CarlosAg.ExcelXmlWriter
    /// Excel natif via une commande OleDb
    /// Pour le moment au 04/12/2009 seul la lecture de l'Excel XML est implémentée
    /// </summary>
    public class CLecteurFichierExcel
    {
        private ETypeFichierExcel m_typeFichier = ETypeFichierExcel.Inconnu;

        private string m_strSheetName = "";
        private const string m_strPremiereCelluleHG = "A1"; // Cellule en haute à gauche de la plage à importer
        private Regex m_expCol = new Regex("[A-Z]");
        private Regex m_expLine = new Regex("[0-9]");


        private string[] m_strEntetes;
        // 
        Workbook m_fichierExcelXml = new Workbook();
        CFichierExcelOleDb m_fichierExcelOleDb = new CFichierExcelOleDb();

        public CLecteurFichierExcel()
        {
            m_typeFichier = ETypeFichierExcel.Inconnu;
        }

        public CLecteurFichierExcel(string strFileName, string strSheetName )
        {
            OpenFile(strFileName, strSheetName, "");
        }

        public CLecteurFichierExcel(string strFileName, string strSheetName, string strDataRange)
        {
            OpenFile(strFileName, strSheetName, strDataRange);
        }

        public void OpenFile ( string strFileName, string strSheetName, string strDataRange )
        {
            m_strSheetName = strSheetName;
            if (m_typeFichier == ETypeFichierExcel.Inconnu)
            {
                //Essaie de lire le fichier XML
                try
                {
                    m_fichierExcelXml.Load(strFileName);
                    m_typeFichier = ETypeFichierExcel.Xml;
                }
                catch
                {
                    //Ne marche pas en xml, essai en OleDB
                    m_fichierExcelOleDb.ExcelFilename = strFileName;
                    m_fichierExcelOleDb.SheetName = strSheetName;
                    if(strDataRange != string.Empty)
                        m_fichierExcelOleDb.SheetRange = strDataRange;
                    m_fichierExcelOleDb.KeepConnectionOpen = true;
                    m_typeFichier = ETypeFichierExcel.OleDb;
                }
            }
            else
            {
                if (m_typeFichier == ETypeFichierExcel.Xml)
                {
                    m_fichierExcelXml.Load(strFileName);
                }
                else if (m_typeFichier == ETypeFichierExcel.OleDb)
                {
                    m_fichierExcelOleDb.ExcelFilename = strFileName;
                    m_fichierExcelOleDb.SheetName = strSheetName;
                    if (strDataRange != string.Empty)
                        m_fichierExcelOleDb.SheetRange = strDataRange;
                    m_fichierExcelOleDb.KeepConnectionOpen = true;
                }
            }
        }


        public string FirstCellTopLeft
        {
            get
            {
                return m_strPremiereCelluleHG;
            }
        }

        /// <summary>
        /// Recherche la référence de la première cellule trouvée contenant la valeur indiquée
        /// Se limite dans la recherhce aux nMaxLine premières lignes et nMaxCol premières colonnes
        /// </summary>
        /// <param name="strValeurAttendue"></param>
        /// <returns></returns>
        public string GetFirstCellForValue(string strValeurAttendue, int nMaxCol, int nMaxLine)
        {
            if (m_typeFichier == ETypeFichierExcel.OleDb)
            {
                for (int l = 1; l < nMaxLine; l++)
                {
                    // Commence en A1
                    string cColonne = "A";

                    for (int c = 0; c < nMaxCol; c++)
                    {
                        string strCell = cColonne + l.ToString();
                        object valeur = m_fichierExcelOleDb.GetValue(strCell);

                        if (valeur != null && valeur.ToString() == strValeurAttendue)
                            return strCell;

                        cColonne = CUtilExcel.GetNextColumnHeader(cColonne);
                    }

                }
            }

            return "";
        }



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

            if (m_fichierExcelOleDb == null)
            {
                result.EmpileErreur("");
                return result;
            }

            string strExcelCol = m_expCol.Match(m_strPremiereCelluleHG).ToString();
            string strExcelLine = m_expLine.Match(m_strPremiereCelluleHG).ToString();

            string strTitreColonne = "";

            // Récupèration des colonnes et construit le dictionnaire NomDeColonne / EnteteExcel
            // Util pour les fonctions GetFirstLine et GetNextLine
            do
            {
                strTitreColonne = string.Empty;
                object cellValue = m_fichierExcelOleDb.GetValue(strExcelCol, strExcelLine);
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

        private Worksheet ExcelSheet
        {

            get
            {
                return m_fichierExcelXml.Worksheets[m_strSheetName];
            }
        }

        private int m_nCurrentLineIndex = 0; 
        public object[] GetFirstLine()
        {
            // Remise à zéro de l'index de lexture pour lire la première ligne
            m_nCurrentLineIndex = 0; 

            ArrayList listeValeurs = new ArrayList(GetNextLine());

            // Met l'index de lecture à 1 pour lire les lignes suivantes avec GetNextLine
            m_nCurrentLineIndex = 1;
            
            return listeValeurs.ToArray();

        }

        public object[] GetNextLine()
        {
            ArrayList listeValeurs = new ArrayList();

            if (m_typeFichier == ETypeFichierExcel.OleDb)
            {
                string strExcelCol = m_expCol.Match(m_strPremiereCelluleHG).ToString();
                string strExcelLine = m_expLine.Match(m_strPremiereCelluleHG).ToString();

                strExcelLine = (Int32.Parse(strExcelLine) + m_nCurrentLineIndex).ToString();

                foreach (string strTitreColonne in m_dicColHeaderColName.Keys)
                {
                    string colHeader = "";
                    if (m_dicColHeaderColName.TryGetValue(strTitreColonne, out colHeader))
                    {
                        listeValeurs.Add(m_fichierExcelOleDb.GetValue(colHeader, strExcelLine));
                    }

                }
            }
            else if (m_typeFichier == ETypeFichierExcel.Xml)
            {
                if (ExcelSheet != null && m_nCurrentLineIndex < ExcelSheet.Table.Rows.Count)
                {
                    WorksheetRow row = ExcelSheet.Table.Rows[m_nCurrentLineIndex];
                    foreach (WorksheetCell cell in row.Cells)
                    {
                        if (cell.Index != 0)
                        {
                            while (listeValeurs.Count < cell.Index)
                                listeValeurs.Add(new WorksheetCell("").Data);
                            listeValeurs[cell.Index - 1] = cell.Data;
                        }
                        else
                            listeValeurs.Add(cell.Data);
                    }
                }
            }
            
            m_nCurrentLineIndex++;
            return listeValeurs.ToArray();
        }

        //---------------------------------------------------------------------------
        public DataTable GetTable(bool bNomChampsSurPremiereLigne)
        {
            DataTable dt = new DataTable("EXCEL_TABLE");

            if (m_typeFichier == ETypeFichierExcel.OleDb && m_fichierExcelOleDb != null)
            {
                m_fichierExcelOleDb.Headers = bNomChampsSurPremiereLigne;
                return m_fichierExcelOleDb.GetTable();
            }

            // Construction des colonnes
            object[] entetes = GetFirstLine();
            if (bNomChampsSurPremiereLigne)
            {
                foreach (object titreCol in entetes)
                {
                    dt.Columns.Add(titreCol.ToString());
                }
            }
            else
            {
                string titre = "A";
                foreach (object titreCol in entetes)
                {
                    dt.Columns.Add(titre);
                    titre = CUtilExcel.GetNextColumnHeader(titre);
                }
            }

            // Remplissage des lignes

            object[] datas = GetFirstLine();
            if (bNomChampsSurPremiereLigne)
                datas = GetNextLine();

            int index = 0;

            while (datas != null && datas.Length > 0)
            {
                index = 0;
                DataRow firstRow = dt.Rows.Add();
                foreach (DataColumn colonne in dt.Columns)
                {
                    try
                    {
                        object data = datas[index++];
                        if (data != null)
                            firstRow[colonne] = data;
                        else
                            firstRow[colonne] = "";
                    }
                    catch
                    {
                        firstRow[colonne] = "";
                    }
                }

                datas = GetNextLine();
            }


            return dt;
        }

        //---------------------------------------------------------------------------
        public void Close()
        {
            if (m_fichierExcelOleDb != null)
            {
                m_fichierExcelOleDb.Close();
            }
        }


        public string[] GetExcelSheetNames()
        {
            List<string> listeNomFeuilles = new List<string>();

            if (m_typeFichier == ETypeFichierExcel.OleDb && m_fichierExcelOleDb != null)
            {
                return m_fichierExcelOleDb.GetExcelSheetNames();
            }

            else if (m_typeFichier == ETypeFichierExcel.Xml && m_fichierExcelXml != null)
            {
                foreach (Worksheet sheet in m_fichierExcelXml.Worksheets)
                {
                    listeNomFeuilles.Add(sheet.Name);
                }
            }

            return listeNomFeuilles.ToArray();
        }
    }
}
