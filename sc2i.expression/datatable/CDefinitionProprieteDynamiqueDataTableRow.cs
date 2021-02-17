using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using System.Data;

namespace sc2i.expression.datatable
{
    [Serializable]
    public class CDefinitionProprieteDynamiqueDataTableRow : CDefinitionProprieteDynamiqueInstance
    {
        public const string c_cleType = "DTROW";

        //-------------------------------------------
        public CDefinitionProprieteDynamiqueDataTableRow()
            : base()
        {
        }
        //-------------------------------------------
        public CDefinitionProprieteDynamiqueDataTableRow(CDynamicDataTableDef table)
            : base("Rows",
            "ROWS",
            new CDynamicDataTableRowDef(table), 
            true,
            "")
        {
        }

        //-------------------------------------------
        public override string CleType
        {
            get
            {
                return c_cleType;
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CInterpreteurProprieteDynamiqueDataTableRow : IInterpreteurProprieteDynamique
    {
        //-----------------------------------------------------------------
        public static void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(
                CDefinitionProprieteDynamiqueDataTableRow.c_cleType,
                typeof(CInterpreteurProprieteDynamiqueDataTableRow));
        }
        
        //---------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return null;
        }

        //---------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            result.Data = null;
            CDynamicDataTable table = objet as CDynamicDataTable;
            if ( table == null )
                return result;
            DataTable dtTable = table.GetTable();
            if (dtTable != null)
            {
                List<CDynamicDataTableRow> lst = new List<CDynamicDataTableRow>();
                for (int n = 0; n < dtTable.Rows.Count; n++)
                    lst.Add(new CDynamicDataTableRow(dtTable, n));
                result.Data = lst.ToArray();
            }
            return result;

        }

        //---------------------------------------------------------------
        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            return CResultAErreur.True;
        }

        //---------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return true;
        }

    }

}
