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
    public class CODEQTableauCroise : CODEQFromObjetsSource, IObjetDeEasyQuery
    {
        private CTableauCroise m_tableauCroise = new CTableauCroise();
       
        
        //---------------------------------------------------
        public CODEQTableauCroise() : base()
        {
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(CODEQBase), typeof(CODEQTableauCroise));
        }

        //---------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Cross table|20008"); }
        }

        //---------------------------------------------------
        public CTableauCroise TableauCroise
        {
            get
            {
                if (m_tableauCroise == null)
                    m_tableauCroise = new CTableauCroise();
                return m_tableauCroise;
            }
            set
            {
                if (value != null)
                    m_tableauCroise = value;
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
            foreach ( CChampFinalDeTableauCroise champFinal in TableauCroise.ChampsFinaux )
            {
                bool bAddAsFinal = true;
                if ( champFinal is CChampFinalDeTableauCroiseDonnee )
                {
                    if ( ((CChampFinalDeTableauCroiseDonnee)champFinal).CumulCroise != null &&
                        !((CChampFinalDeTableauCroiseDonnee)champFinal).CumulCroise.HorsPivot && 
                        !(champFinal is CChampFinalDetableauCroiseDonneeAvecValeur))
                        bAddAsFinal = false;
                }
                if ( bAddAsFinal )
                    lst.Add ( new CColumnEQSimple ( champFinal.NomChamp, champFinal.NomChamp, champFinal.TypeDonnee ));
            }
            return lst.AsReadOnly();
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
                DataTable table = result.Data as DataTable;
                if ( table != null )
                {
                    result = TableauCroise.CreateTableCroisee(table);
                    if (Query != null && !Query.ModeCompatibilteTimos4_0_1_3)
                    {
                        DataTable tableResult = result.Data as DataTable;
                        if (tableResult != null)
                            tableResult.TableName = NomFinal;
                    }
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
            result = serializer.TraiteObject<CTableauCroise>(ref m_tableauCroise);

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
