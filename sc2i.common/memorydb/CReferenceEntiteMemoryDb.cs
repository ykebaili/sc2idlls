using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    public class CReferenceEntiteMemoryDb
    {
        private Type m_typeObjet;
        private object[] m_cles = null;


        public CReferenceEntiteMemoryDb()
        {
            m_typeObjet = null;
            m_cles = new object[] { };
        }

        public CReferenceEntiteMemoryDb(CEntiteDeMemoryDb entite)
        {
            m_typeObjet = entite.GetType();
            m_cles = new object[] { entite.Id };
        }

        public CReferenceEntiteMemoryDb(Type type, params object[] cles)
        {
            m_typeObjet = type;
            m_cles = cles;
        }

        //----------------------------------------------------------------
        public Type TypeObjet
        {
            get
            {
                return m_typeObjet;
            }

        }

        //----------------------------------------------------------------
        public object[] ClesObjet
        {
            get
            {
                return m_cles;
            }
        }

        //----------------------------------------------------------------
        public CEntiteDeMemoryDb GetEntity(CMemoryDb contexteMemoire)
        {
            CEntiteDeMemoryDb entite = (CEntiteDeMemoryDb)Activator.CreateInstance(m_typeObjet, new object[] { contexteMemoire });

            foreach (object cle in m_cles)
            {
                try
                {
                    string strId = (string)cle;
                    if (entite.ReadIfExist(strId))
                        return entite;
                }
                catch
                {
                    continue;
                }
            }

            return null;
        }
    }
}
