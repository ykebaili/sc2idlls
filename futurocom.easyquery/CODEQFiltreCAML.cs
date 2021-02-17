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
using futurocom.easyquery.CAML;

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class CODEQFiltreCAML : CODEQFromObjetsSource, IObjetDeEasyQuery
    {
        private CCAMLQuery m_queryCAML = new CCAMLQuery();
       
        
        //---------------------------------------------------
        public CODEQFiltreCAML() : base()
        {
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(CODEQBase), typeof(CODEQFiltreCAML));
        }

        //---------------------------------------------------
        public override string TypeName
        {
            get { return I.T("CAML query|20003"); }
        }

        //---------------------------------------------------
        public CCAMLQuery CAMLQuery
        {
            get
            {
                return m_queryCAML;
            }
            set
            {
                m_queryCAML = value;
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
            IObjetDeEasyQuery source = TableSource;
            if (source != null)
                return source.Columns;
            return new List<IColumnDeEasyQuery>();
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
                    DataTable tableFiltre = table.Clone() as DataTable;
                    List<DataRow> lstRows = new List<DataRow>();
                    string strFiltre = m_queryCAML != null ? m_queryCAML.GetRowFilter(Parent as CEasyQuery) : "";
                    if (strFiltre.Length > 0)
                    {
                        foreach (DataRow row in table.Select(strFiltre))
                            lstRows.Add(row);
                    }
                    else
                        foreach (DataRow row in table.Rows)
                            lstRows.Add(row);

                    foreach (DataRow row in lstRows)
                    {
                        tableFiltre.ImportRow(row);
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
            result = serializer.TraiteObject<CCAMLQuery>(ref m_queryCAML);

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
