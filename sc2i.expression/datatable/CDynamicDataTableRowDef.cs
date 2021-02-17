using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.expression.datatable
{
    [Serializable]
    public class CDynamicDataTableRowDef : I2iSerializable, IElementAVariableInstance
    {
        private List<CDefinitionProprieteDynamique> m_listeChamps = new List<CDefinitionProprieteDynamique>();
        
        //------------------------------------------------------------
        public CDynamicDataTableRowDef(CDynamicDataTableDef table)
        {
            m_listeChamps.AddRange(table.Colonnes);
        }

        //------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            lst.AddRange((m_listeChamps));
            return lst.ToArray();
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
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            result = serializer.TraiteListe<CDefinitionProprieteDynamique>(m_listeChamps);
            if ( !result )
                return result;
            return result;
        }

    }
}
