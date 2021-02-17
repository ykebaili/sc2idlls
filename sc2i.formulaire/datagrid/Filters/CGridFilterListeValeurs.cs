using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.datagrid.Filters
{
    public class CGridFilterListeValeurs : CGridFilterForWndDataGrid
    {
        private HashSet<string> m_listeValeurs = new HashSet<string>();
        public override string Label
        {
            get { return I.T("Selected values|20052"); }
        }

        public override bool IsValueIn(object valeur)
        {
            if (valeur != null)
            {
                if (valeur is DateTime)
                    valeur = ((DateTime)valeur).ToShortDateString();
                return m_listeValeurs.Contains(valeur.ToString());
            }
            else
                return m_listeValeurs.Contains("");
            return false;
        }

        public IEnumerable<string> ListeValeurs
        {
            get
            {
                return m_listeValeurs;
            }
            set
            {
                m_listeValeurs = new HashSet<string>();
                if (value != null)
                    foreach (string strVal in value)
                        m_listeValeurs.Add(strVal);
            }
        }

       
    }
}
