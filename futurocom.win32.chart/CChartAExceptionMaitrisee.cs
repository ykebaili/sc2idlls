using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using sc2i.common;
using System.Windows.Forms;

namespace futurocom.win32.chart
{
    public class CChartAExceptionMaitrisee : Chart
    {
        private string m_strMessageErreurForce = "";

        public CChartAExceptionMaitrisee()
            : base()
        {
        }

        public void ResetErreur()
        {
            m_strMessageErreurForce = "";
        }

        public void SetErreur(string strMessage)
        {
            m_strMessageErreurForce = strMessage;
        }



        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if ( m_strMessageErreurForce.Length == 0 )
                    base.OnPaint(e);
            }
            catch (Exception ex)
            {
                m_strMessageErreurForce = ex.Message;
            }
            if ( m_strMessageErreurForce.Length > 0 )
            {
                Brush br = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(br, ClientRectangle);
                br.Dispose();
                br = new SolidBrush(Color.Red);
                StringBuilder blErreur = new StringBuilder();
                blErreur.Append(I.T("Some stetting prevent chart to draw normally|20025"));
                blErreur.Append(Environment.NewLine);
                blErreur.Append(m_strMessageErreurForce);
                Font ft = new Font(FontFamily.GenericSansSerif, 10);
                e.Graphics.DrawString(blErreur.ToString(),
                    ft, br, ClientRectangle);
                ft.Dispose();
                br.Dispose();
            }
        }
    }
}
