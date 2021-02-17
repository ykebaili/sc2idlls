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
using sc2i.data;
using System.Data;

namespace sc2i.process.workflow.blocs
{
    public class CBlocWorkflowAttente : CBlocWorkflow
    {

        //-----------------------------------------------------
        private CParametreDeclencheurEvenement m_parametreDeclencheur = new CParametreDeclencheurEvenement();
        private C2iExpression m_formuleElementDeclencheur = null;

        //-----------------------------------------------------
        public CBlocWorkflowAttente()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowAttente(CTypeEtapeWorkflow typeEtape)
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

        //---------------------------------------------------
        public CParametreDeclencheurEvenement ParametreDeclencheur
        {
            get
            {
                return m_parametreDeclencheur;
            }
            set
            {
                m_parametreDeclencheur = value;
            }
        }

        //---------------------------------------------------
        public C2iExpression FormuleElementDeclencheur
        {
            get
            {
                return m_formuleElementDeclencheur;
            }
            set
            {
                m_formuleElementDeclencheur = value;
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
            result = serializer.TraiteObject<CParametreDeclencheurEvenement>(ref m_parametreDeclencheur);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleElementDeclencheur);
            return result;
        }


        //---------------------------------------------------
        public override Point[] GetPolygoneDessin(CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            return new Point[]{
                new Point(rct.Left, rct.Top),
                new Point(rct.Right, rct.Top ),
                new Point ( rct.Right, rct.Bottom ),
                new Point ( rct.Left, rct.Bottom )};
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            Brush br = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            rct.Size = new Size(rct.Width - 4, rct.Height - 4);
            contexte.Graphic.FillRectangle(br,
                rct.Left + 4,
                rct.Top + 4,
                rct.Width,
                rct.Height);

            br.Dispose();
            br = new SolidBrush(donneesDessin.BackColor);
            Pen pen = new Pen(donneesDessin.ForeColor);
            Font ft = donneesDessin.Font;
            if (ft == null)
                ft = new Font("Arial", 8);

            contexte.Graphic.FillRectangle(br, rct);
            contexte.Graphic.DrawRectangle(pen, rct);
            if (rct.Size.Width > BlocImage.Size.Width &&
                rct.Size.Height > BlocImage.Size.Height)
            {
                contexte.Graphic.DrawImage(BlocImage, new Rectangle(rct.Left, rct.Top, BlocImage.Size.Width, BlocImage.Size.Height));
                Brush brTrans = new SolidBrush(Color.FromArgb(128, donneesDessin.BackColor));
                contexte.Graphic.FillRectangle(brTrans, rct.Left, rct.Top,
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
            get { return I.T("Wait event|20090"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "WAIT EVENT"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1.BlocAttente; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get { return false; }
        }

        //---------------------------------------------------
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(etape);
            CResultAErreur result = CResultAErreur.True;
            if (m_formuleElementDeclencheur == null)
            {
                result.EmpileErreur(I.T("Invalid wait paramter on step @1|20091", TypeEtape.Libelle));
                return result;
            }
            result = m_formuleElementDeclencheur.Eval(ctxEval);
            if (!result)
            {
                result.EmpileErreur(I.T("Invalid wait parameter on step @1|20091", TypeEtape.Libelle));
                return result;
            }
            if (result.Data == null)//Pas de cible, ok, on dégage
                return EndAndSaveIfOk(etape);
            CObjetDonneeAIdNumerique objetCible = result.Data as CObjetDonneeAIdNumerique;
            if (objetCible == null)
            {
                result.EmpileErreur(I.T("Can not run wait step @1 on type @2|20092",
                    TypeEtape.Libelle, DynamicClassAttribute.GetNomConvivial(result.Data.GetType())));
                return result;
            }

            CResultAErreurType<CHandlerEvenement> resH = CHandlerEvenement.CreateHandlerOnObject(
                etape.ContexteDonnee,
                objetCible,
                ParametreDeclencheur,
                "Workflow step " + TypeEtape.Id + "/" + etape.UniversalId,
                etape.UniversalId);
            if (!resH)
            {
                result.EmpileErreur(resH.Erreur);
                return result;
            }


            
            if (resH)
                resH.DataType.EtapeWorkflowATerminer = etape;
            
            return etape.ContexteDonnee.SaveAll(true);
        }
    }
}