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
    public class CDessineurIf : CDessineurExpressionGraphique
    {
        public static void Autoexec()
        {
            RegisteurDessineur(typeof(C2iExpressionSi), new CDessineurIf());
        }

        public override void DrawExpression(CContextDessinObjetGraphique ctx, CRepresentationExpressionGraphique expressionGraphique)
        {
            Rectangle rct = expressionGraphique.RectangleAbsolu;

            List<Point> pts = new List<Point>();
            pts.Add(new Point(rct.Left, rct.Top + rct.Height / 2));
            pts.Add(new Point(rct.Left + rct.Width / 2, rct.Top));
            pts.Add(new Point(rct.Right, rct.Top + rct.Height / 2));
            pts.Add(new Point(rct.Left + rct.Width / 2, rct.Bottom));
            ctx.Graphic.FillPolygon(Brushes.White, pts.ToArray());
            ctx.Graphic.DrawPolygon(Pens.Black, pts.ToArray());

            C2iExpressionAnalysable expAn = expressionGraphique.Formule as C2iExpressionAnalysable;
            
            string strText = "";
            if (expAn.Parametres.Count >0 && expAn.Parametres[0] != null)
            {
                strText = expAn.Parametres2i[0].GetString();
            }
            else
            {

                if (expAn != null)
                    strText = expAn.GetInfos().Texte;
                else if (expressionGraphique.Formule != null)
                    strText = expressionGraphique.Formule.GetString();
            }
            StringFormat f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;

            Font ft = new Font(FontFamily.GenericSansSerif, 8);
            ctx.Graphic.DrawString(strText, ft, Brushes.Black, expressionGraphique.RectangleAbsolu, f);


            C2iExpressionGraphique rep = expressionGraphique.RepresentationRacine;
            AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
            if (rep != null)
            {
                
                Pen pen = new Pen ( Color.Black, 2 );
                pen.CustomEndCap = cap;
                for ( int n = 0; n< 3; n++ )
                {
                    
                    CRepresentationExpressionGraphique ext = expressionGraphique.GetExterne ( n );
                    if ( ext != null )
                    {
                        if ( n == 0 )
                        {
                            pen.Color = Color.Black;
                            pen.Width = 1;
                            pen.DashStyle = DashStyle.Dot;
                            DrawLien ( ctx, pen, ft, ext, expressionGraphique, "?" );
                        }
                        else if (n == 1)
                        {
                            pen.Width = 3;
                            pen.DashStyle = DashStyle.Solid;
                            pen.Color = Color.Green;
                            DrawLien(ctx, pen, ft, expressionGraphique, ext, I.T("Yes|20082"));
                        }
                        else if (n == 2)
                        {
                            pen.Width = 3;
                            pen.DashStyle = DashStyle.Solid;
                            pen.Color = Color.Red;
                            DrawLien(ctx, pen, ft, expressionGraphique, ext, I.T("no|20083"));
                        }
                    }
                }
                pen.Dispose();
            }
            CRepresentationExpressionGraphique next = expressionGraphique.Next;
            if (next != null)
            {
                Pen pen = new Pen(Brushes.Black, 3);
                pen.CustomEndCap = cap;
                DrawLien(ctx, pen, ft, expressionGraphique, next, I.T("End if|20084"));
                pen.Dispose();
            }
            cap.Dispose();
            ft.Dispose();
        }

    }
}

