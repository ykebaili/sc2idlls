using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using System.Drawing;
using System.Drawing.Drawing2D;
using sc2i.drawing;

namespace sc2i.expression
{
    [Serializable]
    public class CDessineurExpressionGraphique
    {
        private static CDessineurExpressionGraphique m_instance = null;
        private static Dictionary<Type, CDessineurExpressionGraphique> m_dicDessineurs = new Dictionary<Type, CDessineurExpressionGraphique>();

        protected CDessineurExpressionGraphique()
        {
        }

        private static CDessineurExpressionGraphique DefaultInstance
        {
            get
            {
                if ( m_instance == null )
                    m_instance = new CDessineurExpressionGraphique();
                return m_instance;
            }
        }

        public static void RegisteurDessineur(Type typeExpression, CDessineurExpressionGraphique dessineur)
        {
            m_dicDessineurs[typeExpression] = dessineur;
        }

        public static CDessineurExpressionGraphique GetDessineur(CRepresentationExpressionGraphique expression)
        {
            C2iExpression formule = expression.Formule;
            CDessineurExpressionGraphique dessineur = null;
            if (formule != null)
            {
                Type tp = formule.GetType();
                while (tp != null)
                {
                    if (m_dicDessineurs.TryGetValue(tp, out dessineur))
                        return dessineur;
                    tp = tp.BaseType;
                }
            }
            return DefaultInstance;
        }


        //------------------------------------------------------------------------
        public virtual void DrawExpression(CContextDessinObjetGraphique ctx, CRepresentationExpressionGraphique expressionGraphique)
        {
            Rectangle rct = expressionGraphique.RectangleAbsolu;
            if ( expressionGraphique.LastErreur != null && expressionGraphique.LastErreur != "" )
                ctx.Graphic.FillRectangle(Brushes.Red, rct);
            else
                ctx.Graphic.FillRectangle(Brushes.White, rct);
            ctx.Graphic.DrawRectangle(Pens.Black, rct);

            C2iExpressionAnalysable expAn = expressionGraphique.Formule as C2iExpressionAnalysable;
            string strText = "";
            /*if (expAn != null)
                strText = expAn.GetInfos().Texte;
            else */if ( expressionGraphique.Formule != null )
                strText = expressionGraphique.Formule.GetString();
            StringFormat f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;

            Font ft = new Font(FontFamily.GenericSansSerif, 8);
            ctx.Graphic.DrawString(strText, ft, Brushes.Black, expressionGraphique.RectangleAbsolu, f);

            AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
            C2iExpressionGraphique rep = expressionGraphique.RepresentationRacine;
            if (rep != null)
            {
                Pen pen = new Pen(Brushes.Black, 1);
                pen.DashStyle = DashStyle.Dot;
                pen.EndCap = LineCap.Custom;
                
                pen.CustomEndCap = cap;
                foreach (string strLien in expressionGraphique.IdElementsUtilises)
                {
                    CRepresentationExpressionGraphique exp = rep.GetFormule(strLien);
                    if (exp != null)
                    {
                        DrawLien(ctx, pen, ft, exp, expressionGraphique, "");
                    }
                }
                pen.Dispose();
                
            }

            CRepresentationExpressionGraphique next = expressionGraphique.Next;
            if (next != null)
            {
                Pen pen = new Pen(Brushes.Black, 3);
                pen.CustomEndCap = cap;
                DrawLien(ctx, pen, ft, expressionGraphique, next, "");
                pen.Dispose();
            }
            cap.Dispose();
            ft.Dispose();
        }

        protected void DrawLien(
            CContextDessinObjetGraphique ctx, 
            Pen pen, 
            Font ft,
            CRepresentationExpressionGraphique de, 
            CRepresentationExpressionGraphique vers,
            string strText )
        {
            CLienTracable lien = CTraceurLienDroit.GetLienPourLier(de.RectangleAbsolu, vers.RectangleAbsolu, EModeSortieLien.Automatic);
            lien.RendVisibleAvecLesAutres(ctx.Liens);
            ctx.AddLien(lien);
            ctx.Graphic.DrawLines(pen, lien.Points.ToArray());
            if (strText != "" && lien.Points.Count() > 0)
            {
                CSegmentDroite segment = lien.Segments[0];
                Point ptMilieu = segment.Milieu;
                Brush br = new SolidBrush(pen.Color);
                ctx.Graphic.DrawString(strText, ft, br, ptMilieu);
                br.Dispose();
            }
        }

        
    }
}
