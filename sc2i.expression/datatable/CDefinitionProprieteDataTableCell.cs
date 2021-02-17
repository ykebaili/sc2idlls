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
    public class CDefinitionProprieteDynamicDataTableCell : CDefinitionProprieteDynamique
    {
        public const string c_cleType = "DTCELL";

        //-------------------------------------------------
        public CDefinitionProprieteDynamicDataTableCell()
            :base()
        {
        }

        //-------------------------------------------------
        public CDefinitionProprieteDynamicDataTableCell(DataColumn col)
            : base(col.ColumnName, col.ColumnName, new CTypeResultatExpression(col.DataType, false), false, true)
        { 
        }

        
        //-------------------------------------------------
        public override string CleType
        {
            get
            {
                return c_cleType;
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CInterpreteurProprieteDynamiqueDataTableCell: IInterpreteurProprieteDynamique
    {
        //-----------------------------------------------------------------
        public static void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(
                CDefinitionProprieteDynamicDataTableCell.c_cleType,
                typeof(CInterpreteurProprieteDynamiqueDataTableCell));
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
            CDynamicDataTableRow row = objet as CDynamicDataTableRow;
            if (row != null)
            {
                result.Data =  row.GetValue(strPropriete);//.Replace('_','.'));
            }
            return result;

        }

        //---------------------------------------------------------------
        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            CResultAErreur result = CResultAErreur.True;
            result.Data = null;
            CDynamicDataTableRow row = objet as CDynamicDataTableRow;
            if (row != null)
            {
                row.SetValue(strPropriete/*.Replace('_', '.')*/, valeur);
                result.Data = valeur;
            }
            return result;
        }

        //---------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return false;
        }

    }
}
