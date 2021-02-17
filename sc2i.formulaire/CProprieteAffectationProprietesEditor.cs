using System;
using System.Drawing.Design;

using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.formulaire
{
	public interface IEditeurAffectationsProprietes
	{
		List<CAffectationsProprietes> EditeAffectationsProprietes(List<CAffectationsProprietes> affectations);
		CObjetPourSousProprietes ObjetSource { get;set;}
		Type TypeElementAffecte { get;set;}
		IFournisseurProprietesDynamiques FournisseurProprietes { get;set;}
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CProprieteAffectationsProprietesEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;
		//private static Type m_typeSource = null;
        private static CObjetPourSousProprietes m_objetSource = null;
		private static Type m_typeElementAffecte = null;
		private static IFournisseurProprietesDynamiques m_fournisseurProprietes;

		/// ///////////////////////////////////////////
		public CProprieteAffectationsProprietesEditor()
		{
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeSource(Type tp)
		{
            m_objetSource = new CObjetPourSousProprietes(tp);
		}

        /// ///////////////////////////////////////////
        public static void SetObjetSource(CObjetPourSousProprietes objetSource)
        {
            m_objetSource = objetSource;
        }

        /// ///////////////////////////////////////////
		public static void SetTypeElementAffecte(Type tp)
		{
			m_typeElementAffecte = tp;
		}

		/// ///////////////////////////////////////////
		public static IFournisseurProprietesDynamiques FournisseurProprietes
		{
			get
			{
				return m_fournisseurProprietes;
			}
			set
			{
				m_fournisseurProprietes = value;
			}
		}


		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
			IEditeurAffectationsProprietes editeur = (IEditeurAffectationsProprietes)Activator.CreateInstance(m_typeEditeur);
			editeur.ObjetSource = m_objetSource;
			editeur.TypeElementAffecte = m_typeElementAffecte;
			editeur.FournisseurProprietes = m_fournisseurProprietes;
			object valeur = editeur.EditeAffectationsProprietes(value as List<CAffectationsProprietes>);
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
