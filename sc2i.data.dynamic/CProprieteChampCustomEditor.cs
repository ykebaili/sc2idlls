using System;
using System.Drawing.Design;

namespace sc2i.data.dynamic
{
	public interface ISelectionneurChampCustom
	{
		CChampCustom SelectChamp ( CChampCustom lastSelectionne );
		Type TypeElementsAChamp { get;set;}
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CProprieteChampCustomEditor : UITypeEditor
	{
		private static ISelectionneurChampCustom m_selectionneur = null;
		private static Type m_typeSelectionneur = null;
		private static Type m_typeElementAChamps = null;

		/// ///////////////////////////////////////////
		public CProprieteChampCustomEditor()
		{
		}

		/// ///////////////////////////////////////////
		public static void SetEditeur ( ISelectionneurChampCustom selectionneur)
		{
			m_selectionneur = selectionneur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur ( Type tp )
		{
			m_typeSelectionneur = tp;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeElementAChamp ( Type tp )
		{
			m_typeElementAChamps = tp;
		}



		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
			ISelectionneurChampCustom selectionneur = m_selectionneur;
			if (selectionneur == null)
			{
				selectionneur = (ISelectionneurChampCustom)Activator.CreateInstance(m_typeSelectionneur);
				selectionneur.TypeElementsAChamp = m_typeElementAChamps;
			}
			object retour = selectionneur.SelectChamp((CChampCustom)value);
			if (m_selectionneur == null && selectionneur is IDisposable)
				((IDisposable)selectionneur).Dispose();
			return retour;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_selectionneur == null && m_typeSelectionneur == null )
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.Modal;
		}

		
	}
}
