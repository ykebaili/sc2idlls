using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.data;
using sc2i.common;
using sc2i.data.Excel;


namespace sc2i.win32.data
{
    public class CAssistantLectureFichier
    {
        enum ETypeFichier
        {
            Excel,
            CSV
        }

        public static IParametreLectureFichier CreateParametreLectureFichier(ref string strFichier)
        {
            int nEtape = 0;
            IParametreLectureFichier parametre = null;
            ETypeFichier typeFichier = ETypeFichier.CSV;
            while (true)
            {
                switch (nEtape)
                {
                    case 0://Sélection du fichier
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = I.T("Text file(txt, csv)|*.txt;*.csv|Excel File|*.xls;*.xlsx|All files|*.*|20003");
                        if (dlg.ShowDialog() == DialogResult.Cancel)
                            return null;
                        strFichier = dlg.FileName;
                        string strExtension = strFichier.Substring(strFichier.Length - 3, 3);
                        if (strExtension.ToUpper() == "XLS")
                        {
                            typeFichier = ETypeFichier.Excel;
                            parametre = new CParametreLectureExcel();
                            
                        }
                        else
                        {
                            typeFichier = ETypeFichier.CSV;
                            parametre = new CParametreLectureCSV();
                        }
                        nEtape++;
                        break;

                    case 1://Paramétrage de la lecture CSV
                        if (typeFichier == ETypeFichier.Excel)
                        {
                            if (!CFormOptionsImportExcel1.FillOptions((CParametreLectureExcel)parametre, strFichier))
                                nEtape--;
                            else
                                nEtape++;
                        }
                        else
                        {
                            if (!CFormOptionsCSV1.FillOptions((CParametreLectureCSV)parametre, strFichier))
                                nEtape--;
                            else
                                nEtape++;
                        }
                        break;
                    case 2:
                        return parametre;
                }
            }
            return null;
        }
    }
}
