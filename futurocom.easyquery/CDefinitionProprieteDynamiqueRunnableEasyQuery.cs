using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace futurocom.easyquery
{
    public interface IElementARunnableEasyQueryDynamique
    {
        IRunnableEasyQuery GetQuery ( string strLibelle );
    }

    [Serializable]
    public class CDefinitionProprieteDynamiqueRunnableEasyQuery : CDefinitionProprieteDynamiqueInstance
    {
        public const string c_cleType = "EASYQUERY";
        public CDefinitionProprieteDynamiqueRunnableEasyQuery()
        {
        }

        public CDefinitionProprieteDynamiqueRunnableEasyQuery(IRunnableEasyQuery query)
            : base(query.Libelle.Replace(' ','_'), query.Libelle, query, "")
        {
        }

        public CDefinitionProprieteDynamiqueRunnableEasyQuery(string strNomQuery, IRunnableEasyQuery query)
            : base(strNomQuery.Replace(' ', '_'), strNomQuery, query, "")
        {
        }

        public override string CleType
        {
            get
            {
                return c_cleType;
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CInterpreteurProprieteDynamiqueEasyQuery : IInterpreteurProprieteDynamique
    {
        //----------------------------------------------------------------
        public static void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(CDefinitionProprieteDynamiqueRunnableEasyQuery.c_cleType,
                typeof(CInterpreteurProprieteDynamiqueEasyQuery));
        }

        //----------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            IElementARunnableEasyQueryDynamique elt = objet as IElementARunnableEasyQueryDynamique;
            if ( elt == null )
                return result;
            result.Data = elt.GetQuery ( strPropriete );
            return result;
        }

        //----------------------------------------------------------------
        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            return CResultAErreur.True;
        }

        //----------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return true;
        }

        //----------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return null;
        }
    }
}
