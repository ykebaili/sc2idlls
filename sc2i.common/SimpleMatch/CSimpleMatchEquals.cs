using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.SimpleMatch
{
    //Egalité simple
    public class CSimpleMatchEquals : ISimpleMatch
    {
        private string m_strChaine = "";

        //--------------------------------------
        public CSimpleMatchEquals()
        {
        }

        //--------------------------------------
        public CSimpleMatchEquals(string strChaine)
        {
            m_strChaine = strChaine;
        }

        //--------------------------------------
        public string Valeur
        {
            get
            {
                return m_strChaine;
            }
            set
            {
                m_strChaine = value;
            }
        }

        //--------------------------------------
        public bool Match(string strChaine)
        {
            return strChaine == m_strChaine;
        }

        //--------------------------------------
        public string GetString()
        {
            return m_strChaine;
        }

        //--------------------------------------
        public bool IsValide()
        {
            return true;
        }
    }
}
