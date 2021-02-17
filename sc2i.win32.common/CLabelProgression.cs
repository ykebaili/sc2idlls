using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.common;

namespace sc2i.win32.common
{
    public partial class CLabelProgression : UserControl, IIndicateurProgression
    {
        private List<string> m_listeLibelles = new List<string>();
        private string m_strSeparateur = "-";
        //-----------------------------------------------
        public CLabelProgression()
        {
            InitializeComponent();
        }

        //-----------------------------------------------
        public void PushSegment(int nMin, int nMax)
        {
        }

        //-----------------------------------------------
        public void SetBornesSegment(int nMin, int nMax)
        {
        }

        //-----------------------------------------------
        public void PopSegment()
        {
        }

        //-----------------------------------------------
        public string LabelSeparator
        {
            get
            {
                return m_strSeparateur;
            }
            set
            {
                m_strSeparateur = value;
            }
        }

        //-----------------------------------------------
        public void SetInfo(string strInfo)
        {
            PushLibelle(strInfo);
            UpdateText();
            PopLibelle();
        }

        //-----------------------------------------------
        public void UpdateText()
        {
            StringBuilder bl = new StringBuilder();
            foreach (string strLibelle in m_listeLibelles)
            {
                if (strLibelle.Length > 0)
                {
                    bl.Append(strLibelle);
                    bl.Append(m_strSeparateur);
                }
            }
            if (bl.Length > 0)
            {
                bl.Remove(bl.Length - m_strSeparateur.Length, m_strSeparateur.Length);
            }
            Invoke((MethodInvoker)delegate
            {
                m_lblProgression.Text = bl.ToString();
            });
        }
    

        //-----------------------------------------------
        public void SetValue(int nValue)
        {
            
        }

        //-----------------------------------------------
        public void PushLibelle(string strInfo)
        {
            m_listeLibelles.Add(strInfo);
            //UpdateText();
        }

        //-----------------------------------------------
        public void PopLibelle()
        {
            if (m_listeLibelles.Count > 0)
                m_listeLibelles.RemoveAt(m_listeLibelles.Count - 1);
            //UpdateText();
        }

        //-----------------------------------------------
        public void Masquer(bool bMasquer)
        {
        }

        //-----------------------------------------------
        public bool CancelRequest
        {
            get { return false; }
        }

        //-----------------------------------------------
        public bool CanCancel
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        //-----------------------------------------------
        public void Init()
        {
            m_listeLibelles.Clear();
            UpdateText();
        }
    }
}
