using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;
using sc2i.expression.datatable;
using futurocom.easyquery;
using System.ComponentModel;
using sc2i.expression;

namespace futurocom.chart
{
    [AutoExec("Autoexec")]
    public class CFournisseurValeursSerieChampDeTable : IFournisseurValeursSerie
    {
        private string m_strIdSource = "";
        private string m_strTableName = "";
        private string m_strColumnName = "";
        private C2iExpression m_formule = null;


        //-------------------------------------------------------------------
        public CFournisseurValeursSerieChampDeTable()
        {
        }

        //-------------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireFournisseursValeursSerie.RegisterFournisseur(typeof(CFournisseurValeursSerieChampDeTable));
        }

        //-------------------------------------------------------------------
        [Browsable(false)]
        public string LabelType
        {
            get
            {
                return I.T("Table field|20001");
            }
        }

        //-------------------------------------------------------------------
        [Browsable(false)]
        public string SourceId
        {
            get
            {
                return m_strIdSource;
            }
            set
            {
                m_strIdSource = value;
            }
        }

        //-------------------------------------------------------------------
        public C2iExpression Formule
        {
            get
            {
                return m_formule;
            }
            set
            {
                m_formule = value;
            }
        }

        //-------------------------------------------------------------------
        public string GetLabel(CDonneesDeChart donnees)
        {
            if (donnees == null)
                return LabelType;
            CParametreSourceChart p = donnees.GetSourceFV(SourceId);
            
            StringBuilder bl = new StringBuilder();
            if (p != null)
                bl.Append(p.SourceName);
            else
                bl.Append("?");
            bl.Append(".");
            if (TableName.Length > 0)
                bl.Append(TableName);
            else
                bl.Append("?");
            bl.Append(".");
            if (ColumnName.Length > 0)
                bl.Append(ColumnName);
            else
                bl.Append("?");
            return bl.ToString();
        }


        //-------------------------------------------------------------------
        public string TableName
        {
            get{
                return m_strTableName;
            }
            set{
                m_strTableName = value;
            }
        }

        //-------------------------------------------------------------------
        public string ColumnName
        {
            get{
                return m_strColumnName;
            }
            set{
                m_strColumnName = value;
            }
        }

        //-------------------------------------------------------------------
        public object[] GetValues(CChartSetup chart)
        {
            List<object> lstValeurs = new List<object>();
            CParametreSourceChart p = chart.ParametresDonnees.GetSourceFV(SourceId);
            if (p != null)
            {
                CEasyQuery query = p.GetSource(chart) as CEasyQuery;
                if (query != null)
                {
                    //trouve la table
                    DataTable table = query.GetTable(TableName);
                    if (table != null)
                    {
                        DataColumn col = table.Columns[ColumnName];
                        if (col != null)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                object val = row[col];
                                if (val == DBNull.Value)
                                    lstValeurs.Add(null);
                                else
                                    lstValeurs.Add(val);
                            }
                        }
                        else if (Formule != null)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                CDynamicDataTableRow dr = new CDynamicDataTableRow(row);
                                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(dr);
                                CResultAErreur result = Formule.Eval(ctx);
                                if (result)
                                    lstValeurs.Add(result.Data);
                                else
                                    lstValeurs.Add(null);
                            }
                        }
                    }
                }
            }
            return lstValeurs.ToArray();
        }

        //-------------------------------------------------------------------
        public bool IsApplicableToSource(CParametreSourceChart parametre)
        {
            if (parametre.TypeSource.TypeDotNetNatif == typeof(CEasyQuery))
                return true;
            return false;
        }             


        //-------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : ajout de la formule
        }

        //-------------------------------------------------------------------
        public CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString(ref m_strIdSource);
            serializer.TraiteString ( ref m_strTableName );
            serializer.TraiteString ( ref m_strColumnName );
            if (nVersion >= 1)
                serializer.TraiteObject<C2iExpression>(ref m_formule);
            return result;
        }


    }
}
