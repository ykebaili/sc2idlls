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
using futurocom.easyquery;
using data.hotel.easyquery.filtre;
using data.hotel.client.query;
using System.Drawing.Drawing2D;


namespace data.hotel.easyquery
{
    [Serializable]
    public class CODEQTableFromDataHotel :
        CODEQBase,
        IObjetDeEasyQuery
    {
        //Liste des colonnes utilisées dans la table
        private List<IColumnDeEasyQuery> m_listeColonnes = new List<IColumnDeEasyQuery>();

        private ITableDefinition m_definitionTable;

        private ISourceEntitesPourTableDataHotel m_sourceEntites = new CSourceEntitesPourTableDataHotelFormule();

        private C2iExpression m_formuleDateDebut = null;
        private C2iExpression m_formuleDateFin = null;

        private IDHFiltre m_filtre = null;

        //-------------------------------------------------------
        public CODEQTableFromDataHotel()
            : base()
        {
            Size = new Size(80, 150);
        }

        //-------------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Data hotel table|20067"); }
        }

        //-------------------------------------------------------
        public CODEQTableFromDataHotel(ITableDefinition definition)
            : base()
        {
            NomFinal = definition.TableName;
            Size = new Size(80, 150);
            m_definitionTable = definition;
            if (m_definitionTable != null)
                foreach (IColumnDefinition def in m_definitionTable.Columns)
                {
                    CColumnEQFromSource col = new CColumnEQFromSource(def);
                    m_listeColonnes.Add(col);
                }
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
        public ISourceEntitesPourTableDataHotel SourceEntites
        {
            get
            {
                return m_sourceEntites;
            }
            set
            {
                m_sourceEntites = value;
            }
        }

        //-------------------------------------------------------
        public IDHFiltre Filtre
        {
            get
            {
                return m_filtre;
            }
            set
            {
                m_filtre = value;
            }
        }

        //-------------------------------------------------------
        public C2iExpression FormuleDateDebut
        {
            get
            {
                return m_formuleDateDebut;
            }
            set
            {
                m_formuleDateDebut = value;
            }
        }

        //-------------------------------------------------------
        public C2iExpression FormuleDateFin
        {
            get
            {
                return m_formuleDateFin;
            }
            set
            {
                m_formuleDateFin = value;
            }
        }

        //-------------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CEasyQuery query = Query;
            CResultAErreur result = CResultAErreur.True;



            if (query == null || sources == null)
            {
                result.EmpileErreur(I.T("Table @1 needs a source to provide datas|20001", NomFinal));
                return result;
            }

            CEasyQuerySource source = sources.GetSourceFromId(TableDefinition.SourceId);
            CDataHotelConnexion hotelConnexion = source != null ? source.Connexion as CDataHotelConnexion : null;
            if (hotelConnexion == null)
            {
                result.EmpileErreur(I.T("No connection for table @1|20006", NomFinal));
                return result;
            }

            CTableDefinitionDataHotel tableHotel = m_definitionTable as CTableDefinitionDataHotel;
            if (tableHotel == null)
            {
                result.EmpileErreur(I.T("Table @1 can not be calculated. A DataHotel table should be used as source|20002", NomFinal));
                return result;
            }

            DateTime? dateDebut = null;
            DateTime? dateFin = null;
            CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(Query);
            if (m_formuleDateDebut != null)
            {
                result = m_formuleDateDebut.Eval(ctxEval);
                if (!result)
                    result.EmpileErreur(I.T("Error on start date formula in table @1|20003", NomFinal));
                else
                    dateDebut = result.Data as DateTime?;
            }
            if (m_formuleDateFin != null)
            {
                result = m_formuleDateFin.Eval(ctxEval);
                if (!result)
                    result.EmpileErreur(I.T("Error on end date formula in table @1|20004", NomFinal));
                else
                    dateFin = result.Data as DateTime?;
            }
            if (dateDebut == null || dateFin == null)
            {
                result.EmpileErreur(I.T("Both start date and end date must be set for table  @1|20005", NomFinal));
            }
            if (!result)
                return result;

            List<string> lstIdsEntites = new List<string>();
            if (SourceEntites != null)
                lstIdsEntites.AddRange(SourceEntites.GetListeIdsEntites(Query));

            ITestDataHotel test = null;
            //Calcule le filtre
            if (m_filtre != null)
            {
                test = m_filtre.GetTestFinal(Query);

            }

            CDataHotelQuery hotelQuery = new CDataHotelQuery();
            hotelQuery.TableId = tableHotel.Id;
            hotelQuery.DateDebut = dateDebut.Value;
            hotelQuery.DateFin = dateFin.Value;
            hotelQuery.EntitiesId.AddRange(lstIdsEntites);
            hotelQuery.Filtre = test;
            List<string> lstIdsColonnes = new List<string>();
            List<IChampHotelCalcule> lstCalcs = new List<IChampHotelCalcule>();
            foreach (IColumnDeEasyQuery col in m_listeColonnes)
            {
                CColumnEQFromSource colFromSource = col as CColumnEQFromSource;
                if (colFromSource != null)
                    lstIdsColonnes.Add(colFromSource.IdColumnSource);
                CColonneCalculeeDataHotel colCalc = col as CColonneCalculeeDataHotel;
                if ( colCalc != null )
                {
                    if ( colCalc.Calcul != null )
                    {
                        IChampHotelCalcule champHotelCalc = colCalc.Calcul.GetChampHotel ( Query );
                        if (champHotelCalc != null)
                        {
                            champHotelCalc.NomChampFinal = colCalc.ColumnName;
                            lstCalcs.Add(champHotelCalc);
                        }
                    }

                }
            }
            hotelQuery.ChampsId = lstIdsColonnes;
            hotelQuery.ChampsCalcules = lstCalcs;


            DataTable tableResult = hotelConnexion.GetData(tableHotel, hotelQuery);

            if ( tableResult != null )
            {
                Dictionary<string, IColumnDeEasyQuery> colNameSourceToDestCol = new Dictionary<string, IColumnDeEasyQuery>();
                foreach ( IColumnDeEasyQuery col in m_listeColonnes )
                {
                    CColumnEQFromSource cs = col as CColumnEQFromSource;
                    if (cs != null)
                    {
                        IColumnDefinition def = tableHotel.GetColumn(cs.IdColumnSource);
                        if (def != null)
                            colNameSourceToDestCol[def.ColumnName] = col;
                    }
                }

                foreach ( DataColumn col in tableResult.Columns )
                {
                    IColumnDeEasyQuery colThis = null;
                    if (colNameSourceToDestCol.TryGetValue(col.ColumnName, out colThis))
                        col.ColumnName = colThis.ColumnName;
                    
                }
            }

            result.Data = tableResult;



           
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


        //---------------------------------------------------
        public IEnumerable<IColumnDeEasyQuery> ColonnesOrCalculees
        {
            get
            {
                return m_listeColonnes.AsReadOnly();
            }
        }

        //-------------------------------------------------------
        /// <summary>
        /// Retourne la colonne finale associé à une colonne de la base
        /// </summary>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public IColumnDeEasyQuery GetColonneFor(IColumnDefinition colonne)
        {
            if ( colonne != null )
                return m_listeColonnes.FirstOrDefault(c => c is CColumnEQFromSource && ((CColumnEQFromSource)c).IdColumnSource == colonne.Id);
            return null;
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
            return 0;
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
            result = serializer.TraiteListe<IColumnDeEasyQuery>(m_listeColonnes);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleDateDebut);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleDateFin);
            if (result)
                result = serializer.TraiteObject<ISourceEntitesPourTableDataHotel>(ref m_sourceEntites);
            if (result)
                result = serializer.TraiteObject<IDHFiltre>(ref m_filtre);
            return result;
        }

        //-------------------------------------------------------
        public override void Draw(CContextDessinObjetGraphique ctx)
        {
            base.Draw(ctx);
            if (SourceEntites is CSourceEntitesPourTableDataChampDeTable)
            {
                string strIdTable = ((CSourceEntitesPourTableDataChampDeTable)SourceEntites).IdTable;
                IObjetDeEasyQuery tableSource = null;
                foreach (IObjetDeEasyQuery objet in Query.Childs)
                {
                    CODEQBase o = objet as CODEQBase;
                    if (o != null && o.Id == strIdTable)
                    {
                        tableSource = o;
                        break;
                    }
                }
                if (tableSource != null)
                {
                    Pen pen = new Pen(Brushes.Black, 2);
                    CLienTracable lien = CTraceurLienDroit.GetLienPourLier(tableSource.RectangleAbsolu, RectangleAbsolu, EModeSortieLien.Automatic);
                    lien.RendVisibleAvecLesAutres(ctx.Liens);
                    ctx.AddLien(lien);
                    pen.DashStyle = DashStyle.Dot;
                    AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
                    pen.CustomEndCap = cap;
                    lien.Draw(ctx.Graphic, pen);
                    pen.Dispose();
                    cap.Dispose();
                }
            }
        }
    }
}
  