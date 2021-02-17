using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using sc2i.drawing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Collections;

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class CODEQFiltre : CODEQFromObjetsSource, IObjetDeEasyQuery
    {
        private C2iExpression m_formule = null;
        
        //Contient des colonn es from source et des colonnes calculées (à partir de la source)
        private List<CColonneEQCalculee> m_listeColonnesCalculeesFromSource = new List<CColonneEQCalculee>();
        private List<CColumnEQFromSource> m_listeColonnesFromSource = new List<CColumnEQFromSource>();

        //Si true, on ne s'occupe pas de la m_listeColonnesFromSource
        private bool m_bInclureToutesLesColonnesSource = true;
       
        
        //---------------------------------------------------
        public CODEQFiltre() : base()
        {
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(CODEQBase), typeof(CODEQFiltre));
        }

        //---------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Formula filter|20003"); }
        }

        //---------------------------------------------------
        public bool InclureToutesLesColonnesSource
        {
            get
            {
                return m_bInclureToutesLesColonnesSource;
            }
            set
            {
                m_bInclureToutesLesColonnesSource = value;
            }
        }

        //---------------------------------------------------
        public IEnumerable<CColumnEQFromSource> ColonnesFromSource
        {
            get{
                return m_listeColonnesFromSource.AsReadOnly();
            }
            set{
                m_listeColonnesFromSource = new List<CColumnEQFromSource>();
                if ( value != null )
                    m_listeColonnesFromSource.AddRange ( value );
            }
        }

        //---------------------------------------------------
        public IEnumerable<CColonneEQCalculee> ColonnesCalculeesFromSource
        {
            get{
                return m_listeColonnesCalculeesFromSource.AsReadOnly();
            }
            set{
                m_listeColonnesCalculeesFromSource = new List<CColonneEQCalculee>();
                if ( value != null )
                    m_listeColonnesCalculeesFromSource.AddRange ( value );
            }
        }

        //---------------------------------------------------
        public C2iExpression Formule
        {
            get
            {
                if (m_formule == null)
                    return new C2iExpressionVrai();
                return m_formule;
            }
            set
            {
                m_formule = value;
            }
        }

        //---------------------------------------------------
        public override int NbSourceRequired
        {
            get { return 1;}
        }

        //---------------------------------------------------
        public IObjetDeEasyQuery TableSource
        {
            get
            {
                IObjetDeEasyQuery[] sources = ElementsSource;
                if ( sources.Length == 1 )
                    return sources[0];
                return null;
            }
            set
            {
                if (value == null)
                    ElementsSource = null;
                else
                    ElementsSource = new IObjetDeEasyQuery[] { value };
            }
        }

        //---------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
            if (m_bInclureToutesLesColonnesSource)
            {
                IObjetDeEasyQuery source = TableSource;
                if (source != null)
                    lst.AddRange(source.Columns);
            }
            else
            {
                foreach ( IColumnDeEasyQuery col in ColonnesFromSource )
                    lst.Add ( col );
            }
            foreach ( IColumnDeEasyQuery col in ColonnesCalculeesFromSource )
                lst.Add ( col );
            return lst.AsReadOnly();
        }

        //---------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CResultAErreur result = CResultAErreur.True;
            if (TableSource != null)
            {
                result = TableSource.GetDatas(sources);
                if (!result)
                    return result;
                DataTable table = result.Data as DataTable;
                if (table != null)
                {

                    DataTable tableFiltre = null;
                    Dictionary<IColumnDeEasyQuery, DataColumn> mapColonnes = new Dictionary<IColumnDeEasyQuery, DataColumn>();
                    Dictionary<string, string> mapIdSourceToNomSource = new Dictionary<string, string>();
                    foreach (IColumnDeEasyQuery col in TableSource.Columns)
                        mapIdSourceToNomSource[col.Id] = col.ColumnName;

                    if (InclureToutesLesColonnesSource)
                    {
                        tableFiltre = table.Clone() as DataTable;
                    }
                    else
                    {
                        tableFiltre = new DataTable(NomFinal);


                        foreach (CColumnEQFromSource colFromSource in ColonnesFromSource)
                        {

                            Type tp = colFromSource.DataType;
                            if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                                tp = tp.GetGenericArguments()[0];
                            DataColumn colToAdd = new DataColumn(colFromSource.ColumnName, tp);
                            tableFiltre.Columns.Add(colToAdd);
                            mapColonnes[colFromSource] = colToAdd;

                        }
                    }
                    foreach (CColonneEQCalculee colCalc in ColonnesCalculeesFromSource)
                    {
                        Type tp = colCalc.DataType;
                        if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                            tp = tp.GetGenericArguments()[0];
                        DataColumn colToAdd = new DataColumn(colCalc.ColumnName, tp);
                        tableFiltre.Columns.Add(colToAdd);
                        mapColonnes[colCalc] = colToAdd;
                    }
                    HashSet<IColumnDeEasyQuery> colsSourceCopiables = new HashSet<IColumnDeEasyQuery>();
                    foreach (IColumnDeEasyQuery col in TableSource.Columns)
                        if (tableFiltre.Columns[col.ColumnName] != null)
                            colsSourceCopiables.Add(col);

                    DataRow previousRow = null;
                    foreach (DataRow row in table.Rows)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(new CDataRowForChampCalculeODEQ(row, previousRow, Query));
                        if (Formule is C2iExpressionVrai)
                            result.Data = true;
                        else
                            result = Formule.Eval(ctx);
                        previousRow = row;
                        if (result && result.Data is bool && (bool)result.Data)
                        {
                            DataRow rowDest = tableFiltre.NewRow();
                            if (InclureToutesLesColonnesSource)
                            {
                                foreach (IColumnDeEasyQuery col in colsSourceCopiables)
                                {
                                    rowDest[col.ColumnName] = row[col.ColumnName];
                                }
                            }
                            else foreach (CColumnEQFromSource colFromSource in ColonnesFromSource)
                                {
                                    rowDest[mapColonnes[colFromSource]] = row[mapIdSourceToNomSource[colFromSource.IdColumnSource]];
                                }
                            //Ajout des données calculées
                            foreach (CColonneEQCalculee colCalc in ColonnesCalculeesFromSource)
                            {
                                if (colCalc != null && colCalc.Formule != null)
                                {
                                    result = colCalc.Formule.Eval(ctx);
                                    if (result)
                                    {
                                        try
                                        {
                                            if (result.Data == null)
                                                rowDest[mapColonnes[colCalc]] = DBNull.Value;
                                            else
                                                rowDest[mapColonnes[colCalc]] = result.Data;
                                        }
                                        catch { }
                                    }
                                }
                            }
                            tableFiltre.Rows.Add(rowDest);
                        }
                    }
                    result.Data = tableFiltre;
                }
            }
            if (!(result.Data is DataTable))
            {
                result.EmpileErreur(I.T("Error in table @1|20002", NomFinal));
            }
            return result;
        }

        //-------------------------------------------------------
        /// <summary>
        /// Retourne la colonne finale associé à une colonne de la base
        /// </summary>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public IColumnDeEasyQuery GetColonneFinaleFor(IColumnDeEasyQuery colonne)
        {
            return ColonnesFromSource.FirstOrDefault(c => c.IdColumnSource == colonne.Id);
        }


        //---------------------------------------------------
        private int GetNumVersion()
        {
            /*1 : ajout de la liste des colonnes calculées from source et colonnes from source
                Avant, on prenait toutes les colonnes de la table source
             * */
            return 1;
        }

        //---------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.MySerialize(serializer);
            if (!result)
                return result;
            result = serializer.TraiteObject<C2iExpression>(ref m_formule);
            if ( !result ) 
                return result;
            if (nVersion >= 1)
            {
                serializer.TraiteBool(ref m_bInclureToutesLesColonnesSource);
                result = serializer.TraiteListe<CColumnEQFromSource>(m_listeColonnesFromSource);
                if (result)
                    result = serializer.TraiteListe<CColonneEQCalculee>(m_listeColonnesCalculeesFromSource);
                if (!result)
                    return result;
            }
            else
                InclureToutesLesColonnesSource = true;
            return result;
        }

        

        

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
            IObjetDeEasyQuery source = TableSource;
            if (source != null)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                CLienTracable lien = CTraceurLienDroit.GetLienPourLier(source.RectangleAbsolu, RectangleAbsolu, EModeSortieLien.Automatic);
                lien.RendVisibleAvecLesAutres(ctx.Liens);
                ctx.AddLien(lien);
                AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
                pen.CustomEndCap = cap;
                lien.Draw(ctx.Graphic, pen);
                pen.Dispose();
                cap.Dispose();
                
            }

        }

        //---------------------------------------------------
        protected override void DrawHeader(CContextDessinObjetGraphique ctx, Rectangle rctHeader)
        {
            Image img = Resource1.filtre;
            ctx.Graphic.DrawImage(img, rctHeader.Left, rctHeader.Top + (rctHeader.Height - img.Height) / 2);
            Rectangle reste = new Rectangle(rctHeader.Left + img.Width, rctHeader.Top,
                rctHeader.Width - img.Width, rctHeader.Height);
            base.DrawHeader(ctx, reste);
        }

        

        
    }
}
