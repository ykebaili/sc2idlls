using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.drawing
{
    //Couleur Hue, saturation, luminance
    [Serializable]
    public class ColorHSL
    {
        private double m_fH = 0;
        private double m_fS = 0;
        private double m_fL = 0;

        //-----------------------------------------------
        public ColorHSL()
        {}

        //-----------------------------------------------
        public ColorHSL(double fH, double fS, double fL)
        {
            m_fH = fH;
            m_fS = fS;
            m_fL = fL;
        }

        //-----------------------------------------------
        public double H
        {
            get
            {
                return m_fH;
            }
            set
            {
                m_fH = Math.Max(0, Math.Min(1.0, value)); ;
            }
        }

        //-----------------------------------------------
        public double S
        {
            get
            {
                return m_fS;
            }
            set
            {
                m_fS = Math.Max(0, Math.Min(1.0, value)); ;
            }
        }

        //-----------------------------------------------
        public double L
        {
            get
            {
                return m_fL;
            }
            set
            {
                m_fL = Math.Max(0,Math.Min(1.0,value));
            }
        }

        //-----------------------------------------------
        public static ColorHSL FromRGB( Color rgb )
        {
            return new ColorHSL(rgb.GetHue()/360.0, rgb.GetSaturation(), rgb.GetBrightness());
            /*double r = rgb.R/255.0;
            double g = rgb.G/255.0;
            double b = rgb.B/255.0;
            double v;
            double m;
            double vm;
            double r2, g2, b2;
 
            double h = 0; // default to black
            double s = 0;
            double l = 0;
            v = Math.Max(r,g);
            v = Math.Max(v,b);
            m = Math.Min(r,g);
            m = Math.Min(m,b);
            l = (m + v) / 2.0;
            if (l <= 0.0)
            {
                return new ColorHSL(h, s, l); ;
            }
            vm = v - m;
            s = vm;
            if (s > 0.0)
            {
                  s /= (l <= 0.5) ? (v + m ) : (2.0 - v - m) ;
            }
            else
            {
                return new ColorHSL(h, s, l); ;
            }
            r2 = (v - r) / vm;
            g2 = (v - g) / vm;
            b2 = (v - b) / vm;
            if (r == v)
            {
                  h = (g == m ? 5.0 + b2 : 1.0 - g2);
            }
            else if (g == v)
            {
                  h = (b == m ? 1.0 + r2 : 3.0 - b2);
            }
            else
            {
                  h = (r == m ? 3.0 + g2 : 5.0 - r2);
            }
            h /= 6.0;
            return new ColorHSL(h, s, l);*/
        }

        //-----------------------------------------------
        public Color ToRGB()
        {
            double v;
            double r, g, b;
            double fH, fL, fS;
            fH = H;
            fL = L;
            fS = S;

            r =  fL;   // default to gray
            g = fL;
            b = fL;
            v = (fL <= 0.5) ? (fL * (1.0 + fS)) : (fL + fS - fL * fS);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = fL + fL - v;
                sv = (v - m) / v;
                fH *= 6.0;
                sextant = (int)fH;
                fract = fH - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            Color rgb = Color.FromArgb(Convert.ToByte(r * 255.0f), Convert.ToByte(g * 255.0f), Convert.ToByte(b * 255.0f));
            return rgb;
        }
    }


    public static class CUtilCouleur
    {
        public static Color ChangeLuminance ( Color c, int nVariationSur255)
        {
            ColorHSL cHSL = ColorHSL.FromRGB(c);
            cHSL.L += (double)nVariationSur255/255.0;
            return cHSL.ToRGB();
        }

        public static Color Eclaircir(Color c, int nVariationSur255)
        {
            return ChangeLuminance(c, nVariationSur255);
        }

        public static Color Assombrir(Color c, int nVariationSur255)
        {
            return ChangeLuminance(c, -nVariationSur255);
        }


        public static Color GetCouleurAlternative(Color c, int nVariationSur255)
        {
            if (c.GetBrightness() > 0.5)
                return ChangeLuminance(c, -nVariationSur255);
            return ChangeLuminance(c, nVariationSur255);
        }

        

        public static Color GetCouleurAlternative(Color c)
        {
            return GetCouleurAlternative(c, 20);
        }

        public static Color GetCouleurVisibleSur(Color c)
        {
            ColorHSL col = ColorHSL.FromRGB(c);
            if (col.L > 0.4)
                return Color.Black;
            return Color.White;
        }


    }
}
