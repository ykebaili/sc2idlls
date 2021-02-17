using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace sc2i.formulaire
{
	public class CTextRenderer
	{
		public static void DrawText ( 
			Graphics g, 
			string strText, 
			Font ft, 
			Rectangle clipRect, 
			Brush brush,
			ContentAlignment alignement )
		{
			if (ft == null)
				return;
			if (strText == "")
				return;
			SizeF sz = g.MeasureString(strText, ft);
			Rectangle rect = new Rectangle(0, 0, (int)sz.Width, (int)sz.Height);
			if (rect.Width > clipRect.Width)
			{
				//Word wrap
				int nNbLignes = clipRect.Width != 0 ? (int)(rect.Width / clipRect.Width) + 1 : 0;
				rect = new Rectangle(0, 0, clipRect.Width, nNbLignes * (int)sz.Height);
			}
			switch (alignement)
			{
				case ContentAlignment.BottomCenter:
				case ContentAlignment.BottomLeft:
				case ContentAlignment.BottomRight:
					rect.Offset(0, clipRect.Height - rect.Height);
					break;
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.MiddleLeft:
				case ContentAlignment.MiddleRight:
					rect.Offset(0, clipRect.Height / 2 - rect.Height / 2);
					break;
			}
			switch (alignement)
			{
				case ContentAlignment.BottomCenter:
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.TopCenter:
					rect.Offset(clipRect.Width / 2 - rect.Width / 2, 0);
					break;
				case ContentAlignment.BottomRight:
				case ContentAlignment.MiddleRight:
				case ContentAlignment.TopRight:
					rect.Offset(clipRect.Width - rect.Width, 0);
					break;
			}
			rect.Offset(clipRect.Location);
			g.DrawString(strText, ft, brush, rect.Left, rect.Top);
		}
	}
}
