using System;
using System.Drawing.Design;

using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.process.workflow
{
	public interface IEditeurParametresInitialisationEtape
	{
        CParametresInitialisationEtape EditeParametres(CParametresInitialisationEtape parametres);
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CParametresInitialisationEtapeEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;

		/// ///////////////////////////////////////////
		public CParametresInitialisationEtapeEditor()
		{
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}




		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
			IEditeurParametresInitialisationEtape editeur = (IEditeurParametresInitialisationEtape)Activator.CreateInstance(m_typeEditeur);
            object valeur = editeur.EditeParametres(value as CParametresInitialisationEtape);
			if (editeur is IDisposable)
				((IDisposable)editeur).Dispose();
			return valeur;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_typeEditeur == null )
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.Modal;
		}

		
	}
}
