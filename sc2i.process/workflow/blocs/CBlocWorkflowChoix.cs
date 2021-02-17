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
    public class CBlocWorkflowChoix : CBlocWorkflow
    {
        public const string c_codeRetourAutres = "OTHER";

        private List<CFormuleNommee> m_listeFormulesCodesRetour = new List<CFormuleNommee>();
        //-----------------------------------------------------
        public CBlocWorkflowChoix()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowChoix(CTypeEtapeWorkflow typeEtape)
            : base(typeEtape)
        {
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
        public override Size DefaultSize
        {
            get
            {
                return new Size(100, 100);
            }
        }


        //---------------------------------------------------
        public IEnumerable<CFormuleNommee> FormulesCodesRetour
        {
            get
            {
                return m_listeFormulesCodesRetour;
            }
            set
            {
                m_listeFormulesCodesRetour.Clear();
                if (value != null)
                {
                    foreach (CFormuleNommee formule in value)
                    {
                        formule.Libelle = formule.Libelle.ToUpper().Replace("~", "");
                        m_listeFormulesCodesRetour.Add(formule);
                    }

                }
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
            result = serializer.TraiteListe<CFormuleNommee>(m_listeFormulesCodesRetour);
            if (!result)
                return result;
            
            return result;
        }


        //---------------------------------------------------
        public override Point[] GetPolygoneDessin(CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            return new Point[]{
                new Point(rct.Left+rct.Width/2, rct.Top),
                new Point(rct.Right, rct.Top+rct.Height/2 ),
                new Point ( rct.Left+rct.Width/2, rct.Bottom ),
                new Point ( rct.Left, rct.Top + rct.Height/2 ),
                new Point(rct.Left+rct.Width/2, rct.Top)};
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
                rct.Size.Height > BlocImage.Size.Height)
            {
                Rectangle rctImage = new Rectangle(rct.Left + rct.Width / 2 - BlocImage.Width / 2+1, rct.Top,
                    BlocImage.Width,
                    BlocImage.Height);
                contexte.Graphic.DrawImage(BlocImage, rctImage);
                Brush brTrans = new SolidBrush(Color.FromArgb(128, donneesDessin.BackColor));
                contexte.Graphic.FillPolygon(brTrans, pts);
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
            get { return I.T("Choice|20068"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "CHOICE"; }
        }


        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1._1345539572_Gnome_Dialog_Question_64; }
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

        //---------------------------------------------------
        public override CResultAErreur EndEtapeNoSave(CEtapeWorkflow etape)
        {
            HashSet<string> lstCodes = new HashSet<string>();
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression ( etape );
            foreach ( CFormuleNommee formule in FormulesCodesRetour )
            {
                CResultAErreur result = formule.Formule.Eval ( ctx );
                bool? bOk = result && result.Data != null ? CUtilBool.BoolFromString(result.Data.ToString()):null;
                if ( bOk != null && bOk.Value )
                    lstCodes.Add ( formule.Libelle);
            }
            if ( lstCodes.Count == 0 )
                lstCodes.Add ( c_codeRetourAutres );
            etape.CodesRetour = lstCodes.ToArray();
            return base.EndEtapeNoSave(etape);
        }

        //---------------------------------------------------
        public override string[] CodesRetourPossibles
        {
            get
            {
                List<string> lst = new List<string>();
                HashSet<string> lstCodes = new HashSet<string>();
                foreach (CFormuleNommee formule in FormulesCodesRetour)
                {
                    lstCodes.Add(formule.Libelle);
                }
                lst.AddRange(lstCodes);
                lst.Add(c_codeRetourAutres);
                lst.Sort();
                return lst.ToArray();
            }
        }


    }
}
