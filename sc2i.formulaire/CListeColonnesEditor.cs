using System;
using System.Drawing.Design;
using System.Collections.Generic;

using sc2i.expression;

namespace sc2i.formulaire
{
	public interface IEditeurColonnes
	{
		List<C2iWndListe.CColonne> EditeColonnes (  );
		C2iWndListe ListeEditee { get;set;}
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CListeColonnesEditor : UITypeEditor
	{
		private static IEditeurColonnes m_editeur = null;
		private static Type m_typeEditeur = null;

		private static C2iWndListe m_listeEditee = null;

		/// ///////////////////////////////////////////
		public CListeColonnesEditor()
		{
		}

		/// ///////////////////////////////////////////
		public static void SetEditeur(IEditeurColonnes editeur)
		{
			m_editeur = editeur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}

		/// ///////////////////////////////////////////
		public static C2iWndListe ListeEditee
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
			IEditeurColonnes editeur = m_editeur;
			if (editeur == null && m_typeEditeur != null)
			{
				editeur = (IEditeurColonnes)Activator.CreateInstance(m_typeEditeur);
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
