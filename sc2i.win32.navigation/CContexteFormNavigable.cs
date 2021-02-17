using System;
using System.Collections;

namespace sc2i.win32.navigation
{
	/// <summary>
	/// Contexte contenant les informations relatives à la fenêtre
	/// </summary>
	public class CContexteFormNavigable 
	{
		private Type m_typeForm;
		private Hashtable m_hashTable = new Hashtable();
		//---------------------------------------------------------------------------
		public CContexteFormNavigable(Type leType)
		{
			m_typeForm = leType;
		}

        public static event ContexteFormEventHandler AfterInitPageFromContexte;
		//---------------------------------------------------------------------------
		/// <summary>
		/// Crée une nouvelle instance de CFormNavigable selon le type du contexte
		/// </summary>
		public IFormNavigable AllouePage()
		{
			IFormNavigable formNavigable = (IFormNavigable)(Activator.CreateInstance(m_typeForm));
			
			formNavigable.InitFromContexte(this);

            if (AfterInitPageFromContexte != null)
                AfterInitPageFromContexte(formNavigable, this);

			return formNavigable;
		}

		//---------------------------------------------------------------------------
		public object this[object cle]
		{
			get
			{
				return m_hashTable[cle];
			}
			set
			{
				m_hashTable[cle] = value;
			}
		}
		//---------------------------------------------------------------------------
		public Type TypeFormNavigable
		{
			get
			{
				return m_typeForm;
			}
			set
			{
				m_typeForm = value;
			}
		}
		//---------------------------------------------------------------------------
	}
}
