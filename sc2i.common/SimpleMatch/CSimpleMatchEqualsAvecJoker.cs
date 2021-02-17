using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace sc2i.common.SimpleMatch
{
    //Egalité avec jokers % pour tout caractère, ? pour un caractère.
    //utiliser \ pour l'échappement
    public class CSimpleMatchEqualsAvecJoker : ISimpleMatch
    {
        private string m_strFormat = "";
        private Regex m_expressionRegulière = null;

        public CSimpleMatchEqualsAvecJoker()
        {
        }

        public CSimpleMatchEqualsAvecJoker(string strFormat)
        {
            m_strFormat = strFormat;
        }

        //-------------------------------------
        public string Format
        {
            get
            {
                return m_strFormat;
            }
            set
            {
                m_strFormat = value;
            }
        }

        //-------------------------------------
        private Regex GetRegEx ()
        {
            if (m_expressionRegulière == null)
            {
                StringBuilder bl = new StringBuilder();
                if (m_strFormat.Length > 0 && m_strFormat[0] != '%')
                    bl.Append("^");
                int nIndex = 0;
                bool bEscape = false;
                while (nIndex < m_strFormat.Length)
                {
                    char c = m_strFormat[nIndex];
                    if (bEscape)
                    {
                        bl.Append(c);
                        bEscape = false;
                    }
                    else
                    {
                        if (c == '%')
                            bl.Append(".*");
                        else if (c == '?')
                            bl.Append(".+");
                        else if (c == '\\')
                            bEscape = true;
                        else if ("^.$|()-[][^]+*{}".IndexOf(c) >= 0)
                        {
                            bl.Append('\\');
                            bl.Append(c);
                        }
                        else
                            bl.Append(c);
                    }
                    nIndex++;
                }
                if (m_strFormat.Length > 0 && m_strFormat[m_strFormat.Length - 1] != '%')
                    bl.Append('$');
                try
                {
                    m_expressionRegulière = new Regex(bl.ToString(), RegexOptions.IgnoreCase);
                }
                catch { }
            }
            return m_expressionRegulière;
        }


        //-------------------------------------
        public bool IsValide()
        {
            return GetRegEx() != null;
        }


        //-------------------------------------
        public bool Match(string strChaine)
        {
            Regex ex = GetRegEx();
            if (ex != null)
                return ex.IsMatch(strChaine);
            return false;
        }

        //-------------------------------------
        public string GetString()
        {
            return m_strFormat;    
        }
    }
}
