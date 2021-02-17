using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Data;
using System.IO;
using System.Collections;
using CarlosAg.ExcelXmlWriter;

namespace sc2i.data.Excel
{
    public class CParametreLectureExcel : I2iSerializable, IParametreLectureFichier
    {
        public class CColonneExcel : I2iSerializable
        {
            public CColonneExcel()
            {
            }

            private string m_strNom = "";
            private Type m_type = null;
            private string m_strNull;
            private bool m_bNullCaseSensitive;
            private bool m_bNullMapping = false;

            public bool HasNullMapping
            {
                get
                {
                    return m_bNullMapping;
                }
                set
                {
                    m_bNullMapping = value;
                }
            }
            public string NullMapping
            {
                get
                {
                    return m_strNull;
                }
                set
                {
                    m_strNull = value;
                }
            }
            public bool NullCaseSensitive
            {
                get
                {
                    return m_bNullCaseSensitive;
                }
                set
                {
                    m_bNullCaseSensitive = value;
                }
            }
            public string Nom
            {
                get
                {
                    return m_strNom;
                }
                set
                {
                    m_strNom = value;
                }
            }
            public Type DataType
            {
                get
                {
                    return m_type;
                }
                set
                {
                    m_type = value;
                }
            }

            //I2iSerializable
            private int GetNumVersion()
            {
                return 0;
            }
            
            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;

                serializer.TraiteString(ref m_strNom);

                bool bHasType = m_type != null;
                bool bHasStrNull = m_strNull != null;
                serializer.TraiteBool(ref bHasStrNull);
                serializer.TraiteBool(ref bHasType);
                serializer.TraiteBool(ref m_bNullMapping);
                serializer.TraiteBool(ref m_bNullCaseSensitive);
                if (bHasStrNull)
                    serializer.TraiteString(ref m_strNull);
                else
                    m_strNull = null;
                if (bHasType)
                    serializer.TraiteType(ref m_type);
                else
                    m_type = null;

                return result;
            }
        }


        private CLecteurFichierExcel m_reader = null;
        private CMappageStringsStrings m_mappage = new CMappageStringsStrings();
        private bool m_bNomChampsSurPremiereLigne = true;
        private bool m_bValeurNulleSiErreur = true;
        private string m_strSheetName = "";
        private string m_strPlageDonnees = "";
        
        //Numéro de colonne->CColonneCSV
        private Hashtable m_tableColonnes = new Hashtable();

        //----------------------------------------------------
        public CMappageStringsStrings Mappage
        {
            get
            {
                return m_mappage;
            }
            set
            {
                if (m_mappage != null)
                    m_mappage = value;
            }
        }
        //----------------------------------------------------
        public bool ValeurNullSiErreur
        {
            get
            {
                return m_bValeurNulleSiErreur;
            }
            set
            {
                m_bValeurNulleSiErreur = value;
            }
        }

        //----------------------------------------------------
        public bool NomChampsSurPremiereLigne
        {
            get
            {
                return m_bNomChampsSurPremiereLigne;
            }
            set
            {
                m_bNomChampsSurPremiereLigne = value;
            }
        }

        //----------------------------------------------------
        public string SheetName
        {
            get
            {
                return m_strSheetName;
            }
            set
            {
                m_strSheetName = value;
            }
        }

        public string PlageDonnees
        {
            get
            {
                return m_strPlageDonnees;
            }
            set
            {
                m_strPlageDonnees = value;
            }
        }

        //----------------------------------------------------
        public CParametreLectureExcel()
        {

        }

        //----------------------------------------------------
        public bool ValideNomPourColonne(int nNumColonne, string strNom)
        {
            for (int nCol = 0; nCol < m_tableColonnes.Count; nCol++)
                if (nCol != nNumColonne && ((CColonneExcel)m_tableColonnes[nCol]).Nom == strNom)
                    return false;
            return true;
        }

        //----------------------------------------------------
        public void SetColonne(int nNumColonne, CColonneExcel colonne)
        {
            if (colonne != null)
            {
                m_tableColonnes[nNumColonne] = colonne;
            }
            else
                m_tableColonnes.Remove(nNumColonne);
        }
        
        //----------------------------------------------------
        public CColonneExcel GetColonne(int nNumColonne)
        {
            object result = m_tableColonnes[nNumColonne];
            if (result == null)
                return null;
            return (CColonneExcel)result;
        }

        //----------------------------------------------------
        public CResultAErreur LectureFichier(string strFileName)
        {
            CResultAErreur result = CResultAErreur.True;

            // Ouvrir le fichier Excel ici
            m_reader = new CLecteurFichierExcel(strFileName, m_strSheetName, m_strPlageDonnees);

            if (m_reader == null)
            {
                result.EmpileErreur(I.T("The Excel data reader is not defined|10000"));
                return result;
            }

            try
            {
                DataTable dt = m_reader.GetTable(m_bNomChampsSurPremiereLigne);
                result.Data = dt;
            }
            catch (Exception ex)
            {
                result.EmpileErreur(ex.Message);
            }
            finally
            {
                try
                {
                    m_reader.Close();
                }
                catch { }
            }

          
            return result;
        }


        public CLecteurFichierExcel Lecteur
        {
            get
            {
                return m_reader;
            }
            set
            {
                m_reader = value;
            }
        }

        public DataColumn[] GetColonnes()
        {
            List<DataColumn> lstCols = new List<DataColumn>();
            foreach (CColonneExcel colonne in m_tableColonnes.Values)
            {
                lstCols.Add(new DataColumn(colonne.Nom, colonne.DataType));
            }
            return lstCols.ToArray();
        }




        #region I2iSerializable Membres
        private int GetNumVersion()
        {
            //return 1;
            return 2; // Ajout de la plage de données (exemple A1:H77)
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            serializer.TraiteBool(ref m_bNomChampsSurPremiereLigne);

            int nNbTypes = m_tableColonnes.Count;
            serializer.TraiteInt(ref nNbTypes);

            //serializer.TraiteObject(ref m_mappage, new object[] { });
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (DictionaryEntry entry in m_tableColonnes)
                    {
                        int nNum = (int)entry.Key;
                        CColonneExcel col = (CColonneExcel)entry.Value;
                        serializer.TraiteInt(ref nNum);
                        result = col.Serialize(serializer);
                        if (!result)
                            return result;
                    }

                    break;
                case ModeSerialisation.Lecture:
                    m_tableColonnes.Clear();
                    for (int nEntry = 0; nEntry < nNbTypes; nEntry++)
                    {
                        int nNum = 0;
                        CColonneExcel col = new CColonneExcel();
                        serializer.TraiteInt(ref nNum);
                        result = col.Serialize(serializer);
                        if (!result)
                            return result;
                        m_tableColonnes[nNum] = col;
                    }
                    break;
            }

            result = m_mappage.Serialize(serializer);

            if (nVersion >= 1)
            {
                serializer.TraiteString(ref m_strSheetName);
                serializer.TraiteBool(ref m_bValeurNulleSiErreur);
            }

            if (nVersion >= 2)
                serializer.TraiteString(ref m_strPlageDonnees);

            if (!result)
                return result;
            
            return result;
        }

        #endregion
    }
}
