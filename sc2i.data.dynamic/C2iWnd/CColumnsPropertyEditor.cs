using System;
using System.Drawing.Design;
using System.Collections.Generic;

using sc2i.expression;

namespace sc2i.data.dynamic
{
	public interface IEditeurColonnesListeSpeedStd
	{
		List<C2iWndListeSpeedStandard.CColonneListeSpeedStd> EditeColonnes (  );
		C2iWndListeSpeedStandard ListeEditee { get;set;}
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CColumnsPropertyEditor : UITypeEditor
	{
		private static IEditeurColonnesListeSpeedStd m_editeur = null;
		private static Type m_typeEditeur = null;

		private static C2iWndListeSpeedStandard m_listeEditee = null;

		/// ///////////////////////////////////////////
        public CColumnsPropertyEditor()
		{
		}

		/// ///////////////////////////////////////////
        public static void SetEditeur(IEditeurColonnesListeSpeedStd editeur)
		{
			m_editeur = editeur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}

		/// ///////////////////////////////////////////
		public static C2iWndListeSpeedStandard ListeEditee
		{
			get
			{
				return m_listeEditee;
			}
			set
			{
				m_listeEditee = value;
			}
		}

		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
			IEditeurColonnesListeSpeedStd editeur = m_editeur;
			if (editeur == null && m_typeEditeur != null)
			{
                editeur = (IEditeurColonnesListeSpeedStd)Activator.CreateInstance(m_typeEditeur);
				editeur.ListeEditee = m_listeEditee;
			}
			object retour = editeur.EditeColonnes();
			if (m_editeur == null && editeur is IDisposable)
				((IDisposable)editeur).Dispose();
			return retour;
		}

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if ( m_editeur == null && m_typeEditeur == null )
				return UITypeEditorEditStyle.None;
			return UITypeEditorEditStyle.Modal;
		}

		
	}
}
