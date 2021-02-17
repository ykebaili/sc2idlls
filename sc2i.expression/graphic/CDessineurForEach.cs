using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression.expressions;
using System.Drawing;
using sc2i.drawing;
using sc2i.expression;
using System.Drawing.Drawing2D;

namespace sc2i.expression
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class CDessineurForEach : CDessineurExpressionGraphique
    {
        public static void Autoexec()
        {
            RegisteurDessineur(typeof(C2iExpressionForEach), new CDessineurForEach());
        }

        //------------------------------------------------------------------------
        public override  void DrawExpression(CContextDessinObjetGraphique ctx, CRepresentationExpressionGraphique expressionGraphique)
        {
            Rectangle rct = expressionGraphique.RectangleAbsolu;
            if (expressionGraphique.LastErreur != null && expressionGraphique.LastErreur != "")
                ctx.Graphic.FillRectangle(Brushes.Red, rct);
            else
                ctx.Graphic.FillRectangle(Brushes.White, rct);
            ctx.Graphic.DrawRectangle(Pens.Black, rct);
            ctx.Graphic.DrawImageUnscaled(Resources.CActionForEach, rct.Left + 1, rct.Top+1);

            C2iExpressionAnalysable expAn = expressionGraphique.Formule as C2iExpressionAnalysable;
            string strText = "ForEach";
            /*if (expAn != null)
                strText = expAn.GetInfos().Texte;
            else *//*if ( expressionGraphique.Formule != null )
                strText = expressionGraphique.Formule.GetString();*/
            StringFormat f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;

            Font ft = new Font(FontFamily.GenericSansSerif, 8);
            ctx.Graphic.DrawString(strText, ft, Brushes.Black, expressionGraphique.RectangleAbsolu, f);

            AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
            C2iExpressionGraphique rep = expressionGraphique.RepresentationRacine;
            if (rep != null)
            {
                Pen pen = new Pen(Brushes.Blue, 2);
                pen.EndCap = LineCap.Custom;
                
                pen.CustomEndCap = cap;
                foreach (string strLien in expressionGraphique.IdElementsUtilises)
                {
                    CRepresentationExpressionGraphique exp = rep.GetFormule(strLien);
                    if (exp != null)
                    {
                        DrawLien(ctx, pen, ft, expressionGraphique, exp, "");
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

    }
}

