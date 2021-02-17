using System;
using System.Drawing.Design;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	public interface ISelectionneurVariableFiltreDynamique
	{
		CVariableDynamique SelectVariable ( CVariableDynamique variableToSel );
		IElementAVariablesDynamiquesBase ElementEdite { get;set;}
	}
	/// <summary>
	/// Description résumée de CProprieteVariableFiltreDynamiqueEditor.
	/// </summary>
	public class CProprieteVariableFiltreDynamiqueEditor : UITypeEditor
	{
		private static ISelectionneurVariableFiltreDynamique m_selectionneur = null;
		private static Type m_typeEditeur = null;
		private static IElementAVariablesDynamiquesBase m_elementEdite = null;
		
		// ///////////////////////////////////////////
		public CProprieteVariableFiltreDynamiqueEditor()
		{
		}

		// ///////////////////////////////////////////
		public static void SetEditeur ( ISelectionneurVariableFiltreDynamique selectionneur)
		{
			m_selectionneur = selectionneur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}

		/// ///////////////////////////////////////////
		public static void SetElementEdite(IElementAVariablesDynamiquesBase element)
		{
			m_elementEdite = element;
		}

		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
			ISelectionneurVariableFiltreDynamique selectionneur = m_selectionneur;
			if (selectionneur == null)
			{
				selectionneur = (ISelectionneurVariableFiltreDynamique)Activator.CreateInstance(m_typeEditeur);
				selectionneur.ElementEdite = m_elementEdite;
			}
			object retour = selectionneur.SelectVariable ((CVariableDynamique)value);
			if (m_selectionneur == null && selectionneur is IDisposable)
				((IDisposable)selectionneur).Dispose();
			return retour;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_selectionneur == null && m_typeEditeur == null)
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.Modal;
		}
	}
}
