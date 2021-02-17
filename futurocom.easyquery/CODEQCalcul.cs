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

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class CODEQCalculated : CODEQFromObjetsSource, IObjetDeEasyQuery
    {
        List<IColumnDeEasyQuery> m_listeColonnesCalculeesFromSource = new List<IColumnDeEasyQuery>();
       
        
        //---------------------------------------------------
        public CODEQCalculated() : base()
        {
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(CODEQBase), typeof(CODEQCalculated));
        }

        //---------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Calculated table|20012"); }
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
            return m_listeColonnesCalculeesFromSource.AsReadOnly();
        }

       

        //---------------------------------------------------
        public void SetColonnesCalculeesFromSource(IEnumerable<CColonneEQCalculee> listColonnes)
        {
            m_listeColonnesCalculeesFromSource.Clear();
            if (listColonnes != null)
            {
                foreach (IColumnDeEasyQuery col in listColonnes)
                    m_listeColonnesCalculeesFromSource.Add(col);
            }
        }

        //---------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CResultAErreur result = CResultAErreur.True;
            if ( TableSource != null )
            {
                result = TableSource.GetDatas(sources);
                if ( !result )
                    return result;
                DataTable source = result.Data as DataTable;
                DataTable table = new DataTable();
                Dictionary<IColumnDeEasyQuery, DataColumn> mapCols = new Dictionary<IColumnDeEasyQuery,DataColumn>();
                foreach (IColumnDeEasyQuery colonne in m_listeColonnesCalculeesFromSource)
                {
                    Type tp = colonne.DataType;
                    if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                        tp = tp.GetGenericArguments()[0];
                    DataColumn col = new DataColumn(colonne.ColumnName, tp);
                    table.Columns.Add(col);
                    mapCols[colonne] = col;
                }
                if ( source != null )
                {
                    foreach ( DataRow row in source.Rows )
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression (row );
                        DataRow rowDest = table.NewRow();
                        foreach (IColumnDeEasyQuery col in m_listeColonnesCalculeesFromSource)
                        {
                            CColonneEQCalculee colCalc = col as CColonneEQCalculee;
                            if (colCalc != null && colCalc.Formule != null)
                            {
                                result = colCalc.Formule.Eval(ctx);
                                if (result)
                                {
                                    try
                                    {
                                        rowDest[mapCols[col]] = result.Data;
                                    }
                                    catch { }
                                }
                            }
                        }
                        table.Rows.Add(rowDest);
                    }
                    result.Data = table;
                }
            }
            if ( !(result.Data is DataTable) )
            {
                result.EmpileErreur(I.T("Error in table @1|20002", NomFinal ) );
            }
            return result;
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
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
            result = serializer.TraiteListe<IColumnDeEasyQuery>(m_listeColonnesCalculeesFromSource);

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
            Image img = Resource1.TableCalculee;
            ctx.Graphic.DrawImage(img, rctHeader.Left, rctHeader.Top + (rctHeader.Height - img.Height) / 2);
            Rectangle reste = new Rectangle(rctHeader.Left + img.Width, rctHeader.Top,
                rctHeader.Width - img.Width, rctHeader.Height);
            base.DrawHeader(ctx, reste);
        }

        

        
    }
}
