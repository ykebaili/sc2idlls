using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using System.Data;

namespace futurocom.easyquery
{
    /// <summary>
    /// Permet d'utiliser une EasyQuery sans modifier sa source
    /// </summary>
    [Serializable]
    public class CEasyQueryFromEasyQueryASourcesSpecifique : IRunnableEasyQuery
    {
        private CListeQuerySource m_listeSources = new CListeQuerySource();
        private CEasyQuery m_queryPointee = null;

        //-----------------------------------------------
        public CEasyQueryFromEasyQueryASourcesSpecifique(
            CEasyQuery queryPointee,
            CEasyQuerySource source)
        {
            m_queryPointee = queryPointee;
            m_listeSources.AddSource(source);
        }

        //-----------------------------------------------
        public IEnumerable<CEasyQuerySource> Sources
        {
            get
            {
                return m_listeSources.Sources;
            }
        }

        //-----------------------------------------------
        public string Libelle
        {
            get
            {
                return m_queryPointee.Libelle;
            }
            set
            {
                m_queryPointee.Libelle = value;
            }
        }

        //-------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            return m_queryPointee.GetProprietesInstance();
        }

        //-------------------------------------------------------------
        public DataTable GetTable(string strNomTable)
        {
            return m_queryPointee.GetTable(strNomTable, m_listeSources);
        }
    }
}
