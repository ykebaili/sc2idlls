using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using System.Data;


namespace sc2i.expression.datatable
{
    public interface IElementADataTableDynamique
    {
        DataTable GetTable(string strLibelle);
    }
    [Serializable]
    public class CDefinitionProprieteDynamiqueDataTable : CDefinitionProprieteDynamiqueInstance
    {
        public const string c_cleType = "DTABLE";

        //-------------------------------------------
        public CDefinitionProprieteDynamiqueDataTable()
            : base()
        {
        }

        //-------------------------------------------
        public CDefinitionProprieteDynamiqueDataTable(String strName, DataTable table)
            :base ( strName, strName,
            new CDynamicDataTableDef(table), "")

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
    public class CInterpreteurProprieteDynamicDataTable : IInterpreteurProprieteDynamique
    {

        //-----------------------------------------------------------------
        public static void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(
                CDefinitionProprieteDynamiqueDataTable.c_cleType,
                typeof(CInterpreteurProprieteDynamicDataTable));
        }

        //-----------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return null;
        }

        //-----------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            IElementADataTableDynamique eltATable = objet as IElementADataTableDynamique;
            if (eltATable != null)
            {
                DataTable table = eltATable.GetTable(strPropriete);
                if (table != null)
                    result.Data = new CDynamicDataTable(table);
            }
            return result;
        }

        //-----------------------------------------------------------------
        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            return CResultAErreur.True;
        }

        //-----------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return true;
        }
    }
}
