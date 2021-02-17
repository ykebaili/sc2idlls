using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CDefinitionProprieteDynamique.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueThis : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "TS";
		public CDefinitionProprieteDynamiqueThis()
		{			
		}

		public CDefinitionProprieteDynamiqueThis
			(
			CTypeResultatExpression type, 
			bool bHasSubProprietes,
			bool bIsReadOnly
			)
			:base ( "", "",
			type, 
			bHasSubProprietes,
			bIsReadOnly )
		{
		}

		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueThis));
		}

		public override string CleType
		{
			get { return c_strCleType; }
		}

		
			
	}

	public class CInterpreteurProprieteDynamiqueThis : IInterpreteurProprieteDynamique
	{
		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = objet;
			return result;
		}

		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20004"));
			return result;
		}

        public class COptimiseurProprieteThis : IOptimiseurGetValueDynamic
        {
            Type m_typeRetourne = null;

            public COptimiseurProprieteThis(Type typeRetourne)
            {
                m_typeRetourne = typeRetourne;
            }

            public object GetValue(object objet)
            {
                return objet;
            }

            public Type GetTypeRetourne()
            {
                return m_typeRetourne;
            }
        }


        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return new COptimiseurProprieteThis(tp);
        }

    }


}
