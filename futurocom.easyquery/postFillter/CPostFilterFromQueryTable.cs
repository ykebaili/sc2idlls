using sc2i.common;
using sc2i.drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery.postFillter
{
    [AutoExec("Autoexec")]
    public class CPostFilterFromQueryTable : IPostFilter
    {
        public static string c_strIdType = "FILTER_FROM_QUERY_TABLE";
        private string m_strTableId = null;

        private CParametreJointure m_parametreJointure = new CParametreJointure();

        //--------------------------------------------------------------------------------------
        public CPostFilterFromQueryTable()
        {

        }

        //--------------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CAllocateurPostFilter.RegisterType(new CDefPostFilter(c_strIdType,
                I.T("Filter from a table|20016"), typeof(CPostFilterFromQueryTable)));
        }

        //--------------------------------------------------------------------------------------
        public string SourceTableId
        {
            get
            {
                return m_strTableId;
            }
            set
            {
                m_strTableId = value;
            }
        }

        //--------------------------------------------------------------------------------------
        public CParametreJointure ParametreJointure
        {
            get
            {
                return m_parametreJointure;
            }
            set
            {
                m_parametreJointure = value;
            }
        }

        //--------------------------------------------------------------------------------------
        public CResultAErreurType<DataTable> FiltreData(DataTable tableSource, CEasyQuery query, CListeQuerySource sources)
        {
            CResultAErreurType<DataTable> resTable = new CResultAErreurType<DataTable>();

            //Trouve la table source
            IObjetDeEasyQuery objet = query.GetObjet(m_strTableId);
            if ( objet == null )
            {
                resTable.EmpileErreur(I.T("Can not find table @1|20017", m_strTableId));
                return resTable;
            }

            //Récupére les données de la table source
            CResultAErreur result = objet.GetDatas ( sources );
            if ( !result )
            {
                resTable.EmpileErreur ( result.Erreur );
                return resTable;
            }

            DataTable tableResult = tableSource.Clone();
            

            List<DataRow> lstRows1 = new List<DataRow>();
            foreach ( DataRow row in tableSource.Rows )
                lstRows1.Add ( row );

            List<DataRow> lstRows2 = new List<DataRow>();
            DataTable table = result.Data as DataTable;
            if ( table != null )
            {
                foreach ( DataRow row in table.Rows )
                    lstRows2.Add ( row );
            }

            Dictionary<object,  List<DataRow>> keys1 = null;
            CParametreJointure.GetDicValeurs ( lstRows1, ParametreJointure.FormuleTable1, ref keys1 );

            Dictionary<object, List<DataRow>> keys2 = null;
            CParametreJointure.GetDicValeurs ( lstRows2, ParametreJointure.FormuleTable2, ref keys2 );

            HashSet<DataRow> rowsToKeep = new HashSet<DataRow>();
            foreach (KeyValuePair<object, List<DataRow>> kv in keys1)
            {
                if (ParametreJointure.Operateur == EOperateurJointure.Egal)
                {
                    List<DataRow> rows2 = null;
                    if (keys2.TryGetValue(kv.Key, out rows2))
                    {
                        if (rows2.Count() > 0)
                            foreach (DataRow row in kv.Value)
                                rowsToKeep.Add(row);
                    }
                }
                else
                {
                    foreach (KeyValuePair<object, List<DataRow>> kv2 in keys2)
                    {
                        if (CParametreJointure.Compare(kv.Key, kv2.Key, ParametreJointure.Operateur))
                            foreach (DataRow row in kv.Value)
                                rowsToKeep.Add(row);
                    }
                }

            }
            tableResult.BeginLoadData();
            foreach ( DataRow row in tableSource.Rows )
            {
                if ( rowsToKeep.Contains ( row ) )
                    tableResult.ImportRow ( row );
            }
            tableResult.EndLoadData();
            resTable.DataType = tableResult;
            return resTable;
        }
    

        //--------------------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strTableId);
            result = serializer.TraiteObject<CParametreJointure>(ref m_parametreJointure);
            if (!result)
                return result;
            return result;
        }


        

        //----------------------------------------------------------------------------------------
        public void Draw(IObjetDeEasyQuery objetPossedant, CContextDessinObjetGraphique ctxDessin)
        {
            if ( objetPossedant != null && objetPossedant.Query != null )
            {
                IObjetDeEasyQuery objet = objetPossedant.Query.GetObjet(SourceTableId);
                if ( objet != null )
                {
                    Color c = Color.FromArgb(128, 0, 0, 255);
                    Pen pen = new Pen(c, 1);
                    AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
                    pen.CustomEndCap = cap;
                    CLienTracable lien = CTraceurLienDroit.GetLienPourLier(objet.RectangleAbsolu, objetPossedant.RectangleAbsolu, EModeSortieLien.Automatic);
                    lien.RendVisibleAvecLesAutres(ctxDessin.Liens);
                    ctxDessin.AddLien(lien);
                    lien.Draw(ctxDessin.Graphic, pen);
                    pen.Dispose();
                    cap.Dispose();
                }
            }
        }
    }
}
