using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.common;
using System.Drawing;
using sc2i.process.workflow.dessin;
using sc2i.drawing;
using System.Drawing.Drawing2D;

namespace sc2i.process.workflow.blocs
{
    public class CBlocWorkflowPoint : CBlocWorkflow
    {
        //-----------------------------------------------------
        public CBlocWorkflowPoint()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowPoint(CTypeEtapeWorkflow typeEtape)
            : base(typeEtape)
        {
        }

        //---------------------------------------------------
        public override Size DefaultSize
        {
            get
            {
                return new Size(110, 40);
            }
        }

        //-----------------------------------------------------
        public override EModeGestionErreurEtapeWorkflow ModeGestionErreur
        {
            get
            {
                return EModeGestionErreurEtapeWorkflow.SetError;
            }
        }
        
        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------
        public override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            
            return result;
        }




        //---------------------------------------------------
        public override Point[] GetPolygoneDessin(CWorkflowEtapeDessin donneesDessin)
        {
            NormalizeSize(donneesDessin);
            Rectangle rct = donneesDessin.RectangleAbsolu;
            return new Point[]{
                new Point(rct.Left+rct.Height/2, rct.Top),
                new Point(rct.Right - rct.Height/2, rct.Top ),
                new Point(rct.Right, rct.Top + rct.Height/2),
                new Point(rct.Right - rct.Height/2, rct.Bottom ),
                new Point(rct.Left+rct.Height/2, rct.Bottom),
                new Point(rct.Left, rct.Top + rct.Height/2),
                new Point(rct.Left+rct.Height/2, rct.Top)
            };
        }

        //---------------------------------------------------
        private static void NormalizeSize(CWorkflowEtapeDessin donneesDessin)
        {
            if (donneesDessin.Size.Height > donneesDessin.Size.Width)
                donneesDessin.Size = new Size(donneesDessin.Size.Height, donneesDessin.Size.Height);
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin donneesDessin)
        {
            NormalizeSize(donneesDessin);
            Rectangle rct = donneesDessin.RectangleAbsolu;
            Brush br = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            rct.Size = new Size(rct.Width - 4, rct.Height - 4);
            Region rgn = new Region();
            GraphicsPath path = new GraphicsPath();

            

            path.AddLine(rct.Left + rct.Height / 2, rct.Top,
                rct.Right - rct.Height / 2, rct.Top);
            path.AddArc(rct.Right - rct.Height, rct.Top, rct.Height, rct.Height,
                -90, 180);
            path.AddLine(rct.Right - rct.Height / 2, rct.Bottom,
                rct.Left + rct.Height / 2, rct.Bottom);
            path.AddArc(rct.Left, rct.Top, rct.Height, rct.Height,
                90, 180);

            Matrix mat = new Matrix();
            mat.Translate(4, 4);
            path.Transform(mat);
            mat.Dispose();
            contexte.Graphic.FillPath(br, path);
            br.Dispose();

            mat = new Matrix();
            mat.Translate(-4, -4);
            path.Transform(mat);
            mat.Dispose();
            
            br = new SolidBrush(donneesDessin.BackColor);
            Pen pen = new Pen(donneesDessin.ForeColor);
            Font ft = donneesDessin.Font;
            if (ft == null)
                ft = new Font("Arial", 8);

            contexte.Graphic.FillPath(br, path);
            contexte.Graphic.DrawPath(pen, path);
            /*if (rct.Size.Width > BlocImage.Size.Width && 
                rct.Size.Height > BlocImage.Size.Height )
            {
                contexte.Graphic.DrawImage(BlocImage, new Point(rct.Left, rct.Top));
                Brush brTrans = new SolidBrush(Color.FromArgb(128, donneesDessin.BackColor));
                contexte.Graphic.FillRectangle(brTrans, rct.Left, rct.Top,
                    BlocImage.Width,
                    BlocImage.Height);
                brTrans.Dispose();
            }*/
            path.Dispose();

            if (TypeEtape != null && ft != null)
            {
                br.Dispose();
                br = new SolidBrush(donneesDessin.ForeColor);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                contexte.Graphic.DrawString(TypeEtape.Libelle, ft, br, rct, sf);
                sf.Dispose();
            }
            if (donneesDessin.Font == null)
                ft.Dispose();
            pen.Dispose();
            br.Dispose();
        }

        //---------------------------------------------------
        public override string BlocName
        {
            get { return I.T("Free point|20071"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "FREE POINT"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1._1345618093_start; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get { return false; }
        }

        //---------------------------------------------------
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            return EndAndSaveIfOk(etape);
        }
    }
}
