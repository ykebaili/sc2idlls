using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	/// <summary>
	/// Objet source pour une analyse de formule. Il peut s'agir
	/// soit d'une type, soit d'un IElementAVariableInstance,
	/// soit d'un CTypeResultatExpression
	/// </summary>
    [Serializable]
	public class CObjetPourSousProprietes
	{
		object m_objet;
        private bool m_bIsArrayOfObject = false;
		
		//------------------------------------
		public CObjetPourSousProprietes(object obj)
		{
			m_objet = obj;
            CTypeResultatExpression tp = obj as CTypeResultatExpression;
            if (tp != null)
                m_bIsArrayOfObject = tp.IsArrayOfTypeNatif;
		}

        //------------------------------------
        public CObjetPourSousProprietes(object obj, bool bIsArrayOfObject)
        {
            m_objet = obj;
            m_bIsArrayOfObject = bIsArrayOfObject;
        }

        //------------------------------------
        public bool IsArrayOfObject
        {
            get
            {
                return m_bIsArrayOfObject;
            }
            set
            {
                m_bIsArrayOfObject = value;
            }
        }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() + (m_objet != null ? m_objet.GetHashCode() : 0);
		}

		//------------------------------------
		public Type TypeAnalyse
		{
			get
			{
				if (m_objet == null)
					return null;
				if (m_objet is Type)
					return m_objet as Type;
				if (m_objet is CTypeResultatExpression)
					return ((CTypeResultatExpression)m_objet).TypeDotNetNatif;
                if (m_objet is CDefinitionMultiSourceForExpression)
                    return ((CDefinitionMultiSourceForExpression)m_objet).DefinitionObjetPrincipal.TypeAnalyse;
				return m_objet.GetType();
			}
		}

		//------------------------------------
		public CTypeResultatExpression TypeResultatExpression
		{
			get
			{
				if (m_objet == null)
					return null;
				CTypeResultatExpression typeResultat = m_objet as CTypeResultatExpression;
				if (typeResultat != null)
					return typeResultat;
				return new CTypeResultatExpression(TypeAnalyse, m_bIsArrayOfObject);
			}
		}

		//------------------------------------
		public IElementAVariableInstance ElementAVariableInstance
		{
			get
			{
				IElementAVariableInstance elt = m_objet as IElementAVariableInstance;
				return elt;
			}
		}

        //------------------------------------
        public object ObjetAnalyse
        {
            get
            {
                return m_objet;
            }
        }

		//------------------------------------
		public static implicit operator CObjetPourSousProprietes(Type tp)
		{
			return new CObjetPourSousProprietes(tp);
		}

		public override string ToString()
		{
			CTypeResultatExpression typeResultat = TypeResultatExpression;
			if (typeResultat == null)
				return "null";
			return typeResultat.ToString();
		}


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CObjetPourSousProprietes GetObjetAnalyseElements()
        {
            if (m_objet == null)
                return null;
            if (m_objet is Type)
            {
                return new CObjetPourSousProprietes(m_objet);
            }
            if (m_objet is CTypeResultatExpression)
                return new CObjetPourSousProprietes(((CTypeResultatExpression)m_objet).TypeDotNetNatif, false);
            return new CObjetPourSousProprietes(m_objet, false);
        }
    }
}
