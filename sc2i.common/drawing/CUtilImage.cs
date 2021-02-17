using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace sc2i.drawing
{
    public class CUtilImage
    {
        public static Bitmap CreateImageImageResizeAvecRatio(Image image, Size newSize, Color backColor)
        {
            if (image == null)
                return null;
            if (newSize.Width < 1 || newSize.Height < 1)
                return null;

            Bitmap imageFinale = new Bitmap ( newSize.Width, newSize.Height );
            imageFinale.SetResolution(image.HorizontalResolution,
                image.VerticalResolution);
            Graphics g = Graphics.FromImage ( imageFinale );
            Rectangle rc = new Rectangle(0, 0, newSize.Width, newSize.Height);
            SolidBrush brush = new SolidBrush(backColor);
            if (backColor.A > 0)
                g.FillRectangle(brush, rc);
            brush.Dispose();
            Size sz = GetSizeAvecRatio(image, newSize);
            Point pt = new Point(newSize.Width / 2 - sz.Width / 2,
                newSize.Height / 2 - sz.Height / 2);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawImage(image, pt.X, pt.Y, sz.Width, sz.Height);
            g.Dispose();
            return imageFinale;
        }

        public static Size GetSizeAvecRatio ( Image img, Size sizeSouhaitee )
        {
            if (img == null || sizeSouhaitee.Width < 1 || sizeSouhaitee.Height < 1 )
            {
                return sizeSouhaitee;
            }
            double fRatio = (double)img.Size.Width / (double)img.Size.Height;
            int nNewWidth = sizeSouhaitee.Width;
            int nNewHeight = sizeSouhaitee.Height;
            if (fRatio > ((double)sizeSouhaitee.Width/(double)sizeSouhaitee.Height))
                nNewHeight = (int)((double)sizeSouhaitee.Width / fRatio);
            else
                nNewWidth = (int)((double)sizeSouhaitee.Height * fRatio);
            
            return new Size(nNewWidth, nNewHeight);
        }

        public static Image GetImageResizedQualite(Image source, Size newSize)
        {
            if (source == null)
                return null;
            Bitmap newBmp = new Bitmap(newSize.Width, newSize.Height);
            newBmp.SetResolution(source.HorizontalResolution,
                source.VerticalResolution);
            Graphics g = Graphics.FromImage(newBmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawImage(source, 0, 0, newBmp.Width, newBmp.Height);
            g.Dispose();
            return newBmp;
        }

    }
}
