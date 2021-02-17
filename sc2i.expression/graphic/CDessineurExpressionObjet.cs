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
    public class CDessineurExpressionObjet : CDessineurExpressionGraphique
    {
        public static void Autoexec()
        {
            RegisteurDessineur(typeof(C2iExpressionObjet), new CDessineurExpressionObjet());
        }

        //------------------------------------------------------------------------
        public override  void DrawExpression(CContextDessinObjetGraphique ctx, CRepresentationExpressionGraphique expressionGraphique)
        {
            C2iExpressionObjet expObjet = expressionGraphique.Formule as C2iExpressionObjet;
            if (expObjet == null)
                return;

            Rectangle rct = expressionGraphique.RectangleAbsolu;
            Rectangle[] rcts= new Rectangle[]{
                new Rectangle(rct.Left, rct.Top, rct.Width, rct.Height / 2),
                new Rectangle(rct.Left, rct.Top+rct.Height/2, rct.Width, rct.Height / 2)};
            //Dessin des deux paramètres
            for (int n = 0; n < 2; n++)
            {
                CRepresentationExpressionGraphique exp = new CRepresentationExpressionGraphique();
                exp.Position = new Point(rcts[n].Left, rcts[n].Top);
                exp.Size = new Size(rcts[n].Width, rcts[n].Height);
                if (expObjet.Parametres.Count > n)
                    exp.Formule = expObjet.Parametres2i[n];
                CDessineurExpressionGraphique dessineur = CDessineurExpressionGraphique.GetDessineur(exp);
                if (dessineur != null)
                    dessineur.DrawExpression(ctx, exp);
            }
            
            

           StringFormat f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;

            Font ft = new Font(FontFamily.GenericSansSerif, 8);
            
            AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
            C2iExpressionGraphique rep = expressionGraphique.RepresentationRacine;
            if (rep != null)
            {
                Pen pen = new Pen(Brushes.DarkGreen, 1);
                pen.DashStyle = DashStyle.DashDotDot;
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

    }
}

