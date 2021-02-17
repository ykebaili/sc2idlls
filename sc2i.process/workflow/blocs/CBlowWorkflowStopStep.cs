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
    public class CBlocWorkflowStopStep : CBlocWorkflow
    {
        private string m_strCleStepToStop = "";
        private ETypeActionExterneOnWorkflowStep m_typeAction = ETypeActionExterneOnWorkflowStep.End;
        //-----------------------------------------------------
        public CBlocWorkflowStopStep()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowStopStep(CTypeEtapeWorkflow typeEtape)
            : base(typeEtape)
        {
        }

        //---------------------------------------------------
        public override Size DefaultSize
        {
            get
            {
                return new Size(100, 100);
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

        //-----------------------------------------------------
        public string CleStepToStop
        {
            get
            {
                return m_strCleStepToStop;
            }
            set
            {
                m_strCleStepToStop = value;
            }
        }

        //---------------------------------------------------
        public ETypeActionExterneOnWorkflowStep TypeAction
        {
            get
            {
                return m_typeAction;
            }
            set
            {
                m_typeAction = value;
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
            serializer.TraiteString(ref m_strCleStepToStop);

            int nAction = (int)TypeAction;
            serializer.TraiteInt ( ref nAction);
            TypeAction = (ETypeActionExterneOnWorkflowStep)nAction;
            
            return result;
        }


        //---------------------------------------------------
        public override Point[] GetPolygoneDessin(CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            int nWidth = Math.Max ( rct.Width/3,2);
            int nHeight = Math.Max ( rct.Height/3,2);
            return new Point[]{
                new Point(rct.Left + nWidth, rct.Top ),
                new Point(rct.Left + nWidth*2, rct.Top),
                new Point(rct.Right, rct.Top + nHeight),
                new Point(rct.Right, rct.Top + nHeight*2),
                new Point(rct.Left + nWidth*2, rct.Bottom),
                new Point(rct.Left + nWidth, rct.Bottom),
                new Point(rct.Left, rct.Top + 2*nHeight),
                new Point(rct.Left, rct.Top + nHeight ),
                new Point(rct.Left + nWidth, rct.Top )};
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
                contexte.Graphic.DrawImage(BlocImage, new Point(rct.Left + (rct.Width-BlocImage.Width)/2, rct.Top));
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
            get { return I.T("Stop|20098"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "STOP STEP"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1.stop_sign; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get { return false; }
        }

        //---------------------------------------------------
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            if (etape != null && etape.Workflow != null)
            {
                CEtapeWorkflow etapeToStop = etape.Workflow.GetStepForStepType(m_strCleStepToStop);
                if (etapeToStop != null)
                {
                    if (TypeAction == ETypeActionExterneOnWorkflowStep.End)
                        etapeToStop.EndEtapeNoSave();
                    else
                        etapeToStop.CancelStep();
                }
            }
            return EndAndSaveIfOk(etape);
        }
    }
}
