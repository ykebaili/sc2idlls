using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using System.Data;
using sc2i.common;

namespace sc2i.expression.datatable
{
    [Serializable]
    public class CDynamicDataTableDef : I2iSerializable, IElementAVariableInstance
    {
        private List<CDefinitionProprieteDynamique> m_listeColonnes = new List<CDefinitionProprieteDynamique>();
        //------------------------------------------------------------
        public CDynamicDataTableDef(DataTable table)
        {
            if ( table != null )
            {
                foreach ( DataColumn col in table.Columns )
                {
                    m_listeColonnes.Add ( new CDefinitionProprieteDynamicDataTableCell ( 
                        col ));
                }
            }
        }


        //------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();//m_listeChamps.ConvertAll(c => c as CDefinitionProprieteDynamique));
            lst.Add ( new CDefinitionProprieteDynamiqueDataTableRow(this));
            return lst.ToArray();
        }

        //------------------------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamique> Colonnes
        {
            get
            {
                return m_listeColonnes.AsReadOnly();
            }
        }

        //------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteListe<CDefinitionProprieteDynamique>(m_listeColonnes);
            if (!result)
                return result;
            return result;
        }
            




    }


    
}
