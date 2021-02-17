using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.drawing
{
    public static class CUtilRect
    {
        public static Rectangle Normalise(Rectangle r)
        {
            return new Rectangle(
                Math.Min(r.Left, r.Left + r.Width),
                Math.Min(r.Top, r.Top + r.Height),
                Math.Abs(r.Width),
                Math.Abs(r.Height));
        }
    }
}
