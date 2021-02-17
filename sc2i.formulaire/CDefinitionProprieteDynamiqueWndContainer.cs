using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire
{
	//---------------------------------------------------------------------------
	/// <summary>
	/// Définition représentant un sous contrôle d'un contrôle
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueWndContainer : CDefinitionProprieteDynamiqueInstance
	{
		private static string c_strCleType = "WNDCTER";


		public CDefinitionProprieteDynamiqueWndContainer()
			:base()
		{
		}

		public CDefinitionProprieteDynamiqueWndContainer(IWndAContainer controle, string strName)
			: base(
			strName,
			strName.ToUpper(),
			controle,
			"" )
		{
		}

		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueWndContainer));
		}

		public override string CleType
		{
			get
			{
				return c_strCleType;
			}
		}
	}

	//---------------------------------------------------------------------------
	public class CInterpreteurProprieteDynamiqueWndContainer : IInterpreteurProprieteDynamique
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
			IWndAContainer wnd = objet as IWndAContainer;
			if (wnd == null)
				return result;
			result.Data = wnd.WndContainer;
			return result;
		}

		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20003"));
			return result;
		}

        public class COptimiseurProprieteDynamiqueWndContainer : IOptimiseurGetValueDynamic
        {
            public object GetValue(object objet)
            {
                IWndAContainer wnd = objet as IWndAContainer;
                if (wnd == null)
                    return null;
                return wnd.WndContainer;
            }

            public Type GetTypeRetourne()
            {
                return typeof(IWndAContainer);
            }
        }

        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return new COptimiseurProprieteDynamiqueWndContainer();
        }
    }
		
}
