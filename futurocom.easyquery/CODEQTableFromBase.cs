using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.drawing;
using sc2i.common;
using System.Drawing;
using System.Collections;
using System.Data;
using sc2i.expression;
using futurocom.easyquery.CAML;


namespace futurocom.easyquery
{
    /// <summary>
    /// CObjetDeEasyQueryTableFromBase : un table utilisée dans une requête
    /// </summary>
    [Serializable]
    public class CODEQTableFromBase : 
        CODEQBase, 
        IObjetDeEasyQuery
    {
        //Liste des colonnes utilisées dans la table
        private List<IColumnDeEasyQuery> m_listeColonnes = new List<IColumnDeEasyQuery>();

        private CCAMLQuery m_CAMLQuery = null;

        private ITableDefinition m_definitionTable;

        //-------------------------------------------------------
        public CODEQTableFromBase()
            :base()
        {
            Size = new Size(80, 150);
        }

        //-------------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Table from source|20004"); }
        }

        //-------------------------------------------------------
        public CODEQTableFromBase(ITableDefinition definition)
            :base()
        {
            NomFinal = definition.TableName;
            Size = new Size(80, 150);
            m_definitionTable = definition;
            m_listeColonnes = new List<IColumnDeEasyQuery>(
                from def in definition.Columns
                select ((IColumnDeEasyQuery)new CColumnEQFromSource(def)));
        }


        //-------------------------------------------------------
        public ITableDefinition TableDefinition
        {
            get
            {
                return m_definitionTable;
            }
        }

        //-------------------------------------------------------
        public CCAMLQuery CAMLQuery
        {
            get
            {
                return m_CAMLQuery;
            }
            set
            {
                m_CAMLQuery = value;
            }
        }


        //-------------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CEasyQuery query = Query;
            CResultAErreur result = CResultAErreur.True;
            
            if (result && TableDefinition == null)
            {
                result.EmpileErreur(I.T("Table object must be specified |20001"));
            }
            if (query == null || sources == null)
            {
                result.EmpileErreur(I.T("Query needs a source to provide datas|20000"));
            }
            CEasyQuerySource source = sources.GetSourceFromId(TableDefinition.SourceId);
            if (source == null)
            {
                result.EmpileErreur(I.T("Query needs a source to provide datas|20000"));
            }


            if (result)
            {
                IEnumerable<string> lstCols = new List<string>();
                
                if (m_definitionTable != null && m_listeColonnes != null && m_listeColonnes.Count() != m_definitionTable.Columns.Count())
                {
                     lstCols = from c in m_listeColonnes where c is CColumnEQFromSource select ((CColumnEQFromSource)c).IdColumnSource;
                }
                

                ITableDefinitionRequetableCAML tblCAML = TableDefinition as ITableDefinitionRequetableCAML;
                if (tblCAML != null && m_CAMLQuery != null)
                    result = tblCAML.GetDatasWithCAML(source, Parent as CEasyQuery, m_CAMLQuery, lstCols.ToArray());
                else
                    result = TableDefinition.GetDatas(source, lstCols.ToArray());

                if (result && result.Data is DataTable)
                {
                    DataTable table = result.Data as DataTable;
                    foreach (DataColumn col in new ArrayList(table.Columns))
                    {
                        IColumnDefinition def = TableDefinition.Columns.FirstOrDefault(c => c.ColumnName.ToUpper() == col.ColumnName.ToUpper());
                        IColumnDeEasyQuery laCol = null;
                        if (def == null || (laCol = GetColonneFor(def)) == null)
                        {
                            table.Columns.Remove(col);
                        }
                        else
                        {
                            col.ColumnName = laCol.ColumnName;
                            col.ExtendedProperties[CODEQBase.c_extPropColonneId] = laCol.Id;
                        }
                    }
                    if (tblCAML == null && m_CAMLQuery != null)
                    {
                        string strRowFilter = m_CAMLQuery.GetRowFilter(Parent as CEasyQuery);
                        if (strRowFilter.Length > 0)
                        {
                            HashSet<DataRow> rows = new HashSet<DataRow>();
                            foreach (DataRow row in table.Select(strRowFilter))
                                rows.Add(row);
                            foreach (DataRow row in new ArrayList(table.Rows))
                                if (!rows.Contains(row))
                                    table.Rows.Remove(row);
                            table.AcceptChanges();
                        }
                    }
                    //Vérifie et corrige le type des colonnes
                    foreach (CColumnEQFromSource col in Columns)
                    {
                        DataColumn colDeTable = table.Columns[col.ColumnName];
                        if (colDeTable != null && col.DataType != colDeTable.DataType)
                            col.DataType = colDeTable.DataType;
                    }
                }
            }
            if (!result)
                result.EmpileErreur(I.T("Error on table @1|20002"));
            return result;
        }

        //---------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            return m_listeColonnes.AsReadOnly();
        }
        //---------------------------------------------------
        public void SetColonnesOrCalculees(IEnumerable<IColumnDeEasyQuery> cols)
        {
            m_listeColonnes = new List<IColumnDeEasyQuery>(cols);
        }
      
        //-------------------------------------------------------
        /// <summary>
        /// Retourne la colonne finale associé à une colonne de la base
        /// </summary>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public IColumnDeEasyQuery GetColonneFor(IColumnDefinition colonne)
        {
            return m_listeColonnes.FirstOrDefault(c => c is CColumnEQFromSource && ((CColumnEQFromSource)c).IdColumnSource == colonne.Id);
        }

        //-------------------------------------------------------
        public IColumnDefinition GetColumnDefinitionFor(IColumnDeEasyQuery colonne)
        {

            CColumnEQFromSource colFromSource = colonne as CColumnEQFromSource;
            if (m_definitionTable != null && colFromSource != null)
            {
                return m_definitionTable.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource);
            }
            return null;
        }



        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : ajout requête CAML
        }

        //-------------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.MySerialize(serializer);
            if (!result)
                return result;

            result = serializer.TraiteObject<ITableDefinition>(ref m_definitionTable);
            if (!result)
                return result;
            serializer.TraiteListe<IColumnDeEasyQuery>(m_listeColonnes);
            if (nVersion >= 1)
            {
                serializer.TraiteObject<CCAMLQuery>(ref m_CAMLQuery);
            }
            else
                m_CAMLQuery = new CCAMLQuery();


            return result;
        }

        //-------------------------------------------------------
        public override IEnumerable<CCAMLItemField> CAMLFields
        {
            get
            {
                return base.CAMLFields;
            }
        }
    }
}
