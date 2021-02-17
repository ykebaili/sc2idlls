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
	public class CDefinitionProprieteDynamiqueWndFils : CDefinitionProprieteDynamiqueInstance
	{
		private static string c_strCleType = "WNDFLS";


		public CDefinitionProprieteDynamiqueWndFils()
			:base()
		{
		}

		public CDefinitionProprieteDynamiqueWndFils(IWndAChildNomme controle, string strName)
			: base(
			strName,
			strName.ToUpper(),
			controle,
			"" )
		{
		}

		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueWndFils));
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
	public class CInterpreteurProprieteDynamiqueWndFils : IInterpreteurProprieteDynamique
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
			IWndAChildNomme wnd = objet as IWndAChildNomme;
			if (wnd == null)
				return result;
			result.Data = wnd.GetChildFromName(strPropriete);
			return result;
		}

		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20003"));
			return result;
		}

        //----------------------------------------------------------
        public class COptimiseurProprieteDynamiqueWndFils : IOptimiseurGetValueDynamic
        {
            private string m_strNomElement;

            public COptimiseurProprieteDynamiqueWndFils ( string strNomElement )
            {
                m_strNomElement = strNomElement;
            }

            public object GetValue(object objet)
            {
                IWndAChildNomme wnd = objet as IWndAChildNomme;
			    if (wnd == null)
				    return null;
                return wnd.GetChildFromName ( m_strNomElement );
            }

            public Type GetTypeRetourne()
            {
                return typeof(IWndAChildNomme);
            }

        }

        //----------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return new COptimiseurProprieteDynamiqueWndFils ( strPropriete );
        }

    }
		
}
