using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire
{
	public interface IElementAProprietesDynamiquesDeportees
	{
		object GetValeurDynamiqueDeportee(string strPropriete);
		void SetValeurDynamiqueDeportee(string strPropriete, object valeur);
	}

    public interface IConvertibleEnIElementAProprietesDynamiquesDeportees
    {
        IElementAProprietesDynamiquesDeportees ConvertToElementAProprietesDynamiquesDeportees();
    }
        

	/// <summary>
	/// Une définition de propriété qui n'appartient pas
	/// forcement à la classe qu'on interroge. Par exemple,
	/// sur les C2iWnd, il y a des propriétés qui correspondent
	/// aux éléments alloués pour les C2iWnd
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueDeportee : CDefinitionProprieteDynamique
	{
		private static string c_strCleType = "PPDEP";
		//----------------------------------------------
		public CDefinitionProprieteDynamiqueDeportee()
			:base()
		{
		}
		//----------------------------------------------
		public CDefinitionProprieteDynamiqueDeportee(
			string strNom,
			string strPropriete,
			CTypeResultatExpression type,
			bool bHasSubProperties,
			bool bIsReadOnly,
			string strRubrique)
			: base(
			strNom,
			strPropriete,
			type,
			bHasSubProperties,
			bIsReadOnly,
			strRubrique)
		{
		}

		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueDeportee));
		}

		public override string CleType
		{
			get
			{
				return c_strCleType;
			}
		}
	}

	public class CInterpreteurProprieteDynamiqueDeportee : IInterpreteurProprieteDynamique
	{
		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public sc2i.common.CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			IElementAProprietesDynamiquesDeportees elt = objet as IElementAProprietesDynamiquesDeportees;
            if (elt == null)
            {
                IConvertibleEnIElementAProprietesDynamiquesDeportees cv = objet as IConvertibleEnIElementAProprietesDynamiquesDeportees;
                if (cv != null)
                    elt = cv.ConvertToElementAProprietesDynamiquesDeportees();
            }
			if ( elt != null )
			{
				result.Data = elt.GetValeurDynamiqueDeportee ( strPropriete );
			}
			return result;
		}

		//--------------------------------------------------------------------------
		public sc2i.common.CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			IElementAProprietesDynamiquesDeportees elt = objet as IElementAProprietesDynamiquesDeportees;
			if ( elt != null )
				elt.SetValeurDynamiqueDeportee ( strPropriete, valeur );
			return result;
		}

        public class COptimiseurProprieteDynamiqueDeportee : IOptimiseurGetValueDynamic
        {
            private string m_strPropriete = null;

            public COptimiseurProprieteDynamiqueDeportee ( string strPropriete )
            {
                m_strPropriete = strPropriete;
            }

            public object GetValue(object objet)
            {
    			IElementAProprietesDynamiquesDeportees elt = objet as IElementAProprietesDynamiquesDeportees;
                if (elt == null)
                {
                    IConvertibleEnIElementAProprietesDynamiquesDeportees cv = objet as IConvertibleEnIElementAProprietesDynamiquesDeportees;
                    if (cv != null)
                        elt = cv.ConvertToElementAProprietesDynamiquesDeportees();
                }
			    if ( elt != null )
				    return elt.GetValeurDynamiqueDeportee ( m_strPropriete );
			    return null;
            }

            public Type GetTypeRetourne()
            {
                return typeof(object);
            }

        }


        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return new COptimiseurProprieteDynamiqueDeportee(strPropriete);
        }

    }
}
