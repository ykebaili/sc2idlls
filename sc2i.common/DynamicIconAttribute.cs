using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace sc2i.common
{
    public enum ETypeIconeDynamique
    {
        EType = 0 //Indique un icone de type d'élément
    }
    //Permet de créer un icone dynamiquement à partir d'un autre type
    [AttributeUsage(AttributeTargets.Class)]
    public class DynamicIconAttribute : Attribute
    {
        

        public readonly Type TypeDeBase;
        public readonly ETypeIconeDynamique TypeIcone;

        public DynamicIconAttribute(Type typeDeBase,
            ETypeIconeDynamique typeIcone)
        {
            TypeDeBase = typeDeBase;
            TypeIcone = typeIcone;
        }

        public Image GetImage()
        {
            Image img = DynamicClassAttribute.GetImage(TypeDeBase);
            if (img != null)
            {
                Bitmap bmp = new Bitmap(img);
                Graphics g = Graphics.FromImage(bmp);
                bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                img = null;
                switch (TypeIcone)
                {
                    case ETypeIconeDynamique.EType:
                        img = Resources.ImagePourIconeDeType;
                        break;
                }
                if (img != null)
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(img, bmp.Width / 2, bmp.Height / 2, bmp.Width / 2, bmp.Height / 2);
                }
                g.Dispose();
                return bmp;
            }
            return null;
        }
    }
}
