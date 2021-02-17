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
    public class CBlocWorkflowEt : CBlocWorkflow
    {

        //-----------------------------------------------------
        public CBlocWorkflowEt()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowEt(CTypeEtapeWorkflow typeEtape)
            : base(typeEtape)
        {
        }

        //---------------------------------------------------
        public override Size DefaultSize
        {
            get
            {
                return new Size(50, 40);
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
            Rectangle rct = donneesDessin.RectangleAbsolu;
            return new Point[]{
                new Point(rct.Left, rct.Top),
                new Point(rct.Right, rct.Top ),
                new Point ( rct.Right - 10, rct.Top+rct.Height/2),
                new Point ( rct.Right, rct.Bottom ),
                new Point ( rct.Left, rct.Bottom ),
                new Point ( rct.Left+10, rct.Top + rct.Height/2),
                new Point ( rct.Left, rct.Top ) };
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            Brush br = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            rct.Size = new Size(rct.Width - 4, rct.Height - 4);
            Point[] pts = GetPolygoneDessin(donneesDessin);
            
            Matrix mat = contexte.Graphic.Transform;
            mat.Translate(4, 4);
            contexte.Graphic.Transform = mat;
            contexte.Graphic.FillPolygon(br, pts);
            mat.Translate(-4, -4);
            contexte.Graphic.Transform = mat;
            
            br.Dispose();
            br = new SolidBrush(donneesDessin.BackColor);
            Pen pen = new Pen(donneesDessin.ForeColor);
            Font ft = donneesDessin.Font;
            if (ft == null)
                ft = new Font("Arial", 8);

            contexte.Graphic.FillPolygon(br, pts);
            contexte.Graphic.DrawPolygon(pen, pts);
            if (rct.Size.Width > BlocImage.Size.Width && 
                rct.Size.Height > BlocImage.Size.Height )
            {
                contexte.Graphic.DrawImage(BlocImage, new Point(rct.Left+10, rct.Top));
                Brush brTrans = new SolidBrush(Color.FromArgb(128, donneesDessin.BackColor));
                contexte.Graphic.FillRectangle(brTrans, rct.Left+10, rct.Top,
                    BlocImage.Width,
                    BlocImage.Height);
                brTrans.Dispose();
            }

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
            get { return I.T("And|20069"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "AND"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1._1345561540_alliance; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get { return false; }
        }

        //---------------------------------------------------
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            foreach ( CLienEtapesWorkflow lien in etape.TypeEtape.LiensEntrants )
            {
                CTypeEtapeWorkflow typeEtapePrecedente = lien.EtapeSource;
                if ( typeEtapePrecedente != null )
                {
                    CEtapeWorkflow etapePrecedente = etape.Workflow.GetEtapeForType(typeEtapePrecedente);
                    if ( etapePrecedente == null //Pas encore créée 
                        || etapePrecedente.DateFin == null//Pas encore executée
                        || etapePrecedente.RunGeneration != etape.RunGeneration//Pas la même génération d'execution
                        )
                        return etape.ContexteDonnee.SaveAll(true);
                    }
            }
            return EndAndSaveIfOk(etape);
        }
    }
}
