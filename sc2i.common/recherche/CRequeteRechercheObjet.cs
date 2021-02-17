using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.recherche
{
    public class CRequeteRechercheObjet
    {
        private object m_objectToSearch = null;
        private string m_strLibelleRequete = "";

        public CRequeteRechercheObjet(
            string strLibelleRequete,
            object objectToSearch
            )
        {
            m_objectToSearch = objectToSearch;
            m_strLibelleRequete = strLibelleRequete;
        }

        //------------------------------------
        public object ObjetCherché
        {
            get
            {
                return m_objectToSearch;
            }
            set
            {
                m_objectToSearch = value;
            }
        }

        //------------------------------------
        public string Libelle
        {
            get
            {
                return m_strLibelleRequete;
            }
            set
            {
                m_strLibelleRequete = value;
            }
        }
    }
}
