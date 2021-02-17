using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites
{
    //-----------------------------------------
    [Serializable]
    public class CClasseUniteCustom : IClasseUnite
    {
        private string m_strLibelle;
        private string m_strId;
        private string m_strUniteBase;

        //-----------------------------------------
        public CClasseUniteCustom()
        {
            
        }

        //-----------------------------------------
        public string Libelle
        {
            get
            {
                return m_strLibelle;
            }
            set
            {
                m_strLibelle = value;
            }
        }

        //-----------------------------------------
        public string UniteBase
        {
            get
            {
                return m_strUniteBase;
            }
            set
            {
                m_strUniteBase = value;
            }
        }

        //-----------------------------------------
        public string GlobalId
        {
            get
            {
                return m_strId;
            }
            set
            {
                m_strId = value;
            }
        }
    }
}
