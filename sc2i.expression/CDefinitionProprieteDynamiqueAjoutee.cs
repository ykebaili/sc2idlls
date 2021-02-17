using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{
	[AutoExec("Autoexec")]
	[Serializable]
	public class CDefinitionProprieteDynamiqueAjoutee : CDefinitionProprieteDynamique
	{
		private static string c_strCleType = "DYNADD";

		public CDefinitionProprieteDynamiqueAjoutee()
			: base()
		{ }

		public CDefinitionProprieteDynamiqueAjoutee(
			Type tpSource,
			string strField,
			CInfoDynamicFieldAjoute info,
			bool bHasSubProprietes,
			string strRubrique)
			:
			base(
			strField,
			strField,
			info.TypePropriete,
			bHasSubProprietes,
			info.SetDynamicValue == null,
			strRubrique)
		{
		}

		public override string CleType
		{
			get
			{
				return c_strCleType;
			}
		}

		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueAjoutee));
		}

	}

	public class CInterpreteurProprieteDynamiqueAjoutee : IInterpreteurProprieteDynamique
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
			if ( objet == null )
				return result;
			GetDynamicValueDelegate getValue = CGestionnaireProprietesAjoutees.GetGetDelegate ( objet.GetType(), strPropriete );
			try
			{
			if ( getValue != null )
				result.Data = getValue ( objet );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e));
			}
			return result;
		}

        //------------------------------------------------------------
		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( objet == null )
				return result;
			SetDynamicValueDelegate setValue = CGestionnaireProprietesAjoutees.GetSetDelegate ( objet.GetType(), strPropriete );
			if ( setValue != null )
			{
				try
				{
					result = setValue ( objet, valeur );
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException (e));
				}
			}
			return result;
		}

        //------------------------------------------------------------------
        public class COptimiseurProprieteAjoutee : IOptimiseurGetValueDynamic
        {
            private GetDynamicValueDelegate m_delegate = null;

            public COptimiseurProprieteAjoutee ( GetDynamicValueDelegate delegue )
            {
                m_delegate = delegue;
            }
            
            public object GetValue(object objet)
            {
                try
                {
                    if ( m_delegate != null )
                        return m_delegate(objet);
                }
                catch { }
                return null;
            }

            public Type GetTypeRetourne()
            {
                if (m_delegate != null)
                    return m_delegate.Method.ReturnType;
                return null;
            }
        }

        //------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return new COptimiseurProprieteAjoutee ( CGestionnaireProprietesAjoutees.GetGetDelegate ( tp, strPropriete ));
        }
    }
}
