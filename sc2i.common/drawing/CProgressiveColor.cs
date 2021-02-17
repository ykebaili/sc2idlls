using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.drawing
{
    public class CProgressiveColor 
    {
        private Color m_color1;
        private Color m_color2;

        private int m_nNbDegrades = 50;
        private int m_nCurrentIndex = 0;

        //---------------------------------------------------
        public CProgressiveColor(Color couleur1, Color couleur2,
            int nNbDegradesTotal)
        {
            m_color1 = couleur1;
            m_color2 = couleur2;
            m_nNbDegrades = nNbDegradesTotal;
        }

        //---------------------------------------------------
        public void Increment()
        {
            m_nCurrentIndex++;
            if (m_nCurrentIndex / m_nNbDegrades == 2)
                m_nCurrentIndex = 0;
        }


        //---------------------------------------------------
        public Color GetCurrentColor()
        {
            int ndA = m_color2.A - m_color1.A;
            int ndR = m_color2.R - m_color1.R;
            int ndG = m_color2.G - m_color1.G;
            int ndB = m_color2.B - m_color1.B;

            double fdA = (double)ndA / (double)m_nNbDegrades;
            double fdR = (double)ndR / (double)m_nNbDegrades;
            double fdG = (double)ndG / (double)m_nNbDegrades;
            double fdB = (double)ndB / (double)m_nNbDegrades;

            int nSens = m_nCurrentIndex;
            if (nSens > m_nNbDegrades)
                nSens = m_nNbDegrades + m_nNbDegrades - nSens;
            int nA = m_color1.A + (int)(fdA * nSens);
            int nR = m_color1.R + (int)(fdR * nSens);
            int nG = m_color1.G + (int)(fdG * nSens);
            int nB = m_color1.B + (int)(fdB * nSens);
            nA = Math.Min(255, Math.Max(nA, 0 ));
            nR = Math.Min(255, Math.Max(nR, 0));
            nG = Math.Min(255, Math.Max(nG, 0));
            nB = Math.Min(255, Math.Max(nB, 0)); 
            
            return Color.FromArgb(nA, nR, nG, nB);
        }


        //---------------------------------------------------
        public static CProgressiveColor operator ++(CProgressiveColor p)
        {
            p.Increment();
            return p;
        }

        //---------------------------------------------------
        public static implicit operator Color(CProgressiveColor p)
        {
            return p.GetCurrentColor();
        }
        
    }
}
