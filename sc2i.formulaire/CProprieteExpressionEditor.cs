using System;
using System.Drawing.Design;

using sc2i.expression;
using System.ComponentModel;
using sc2i.common;

namespace sc2i.formulaire
{
	public interface IEditeurExpression
	{
		C2iExpression EditeExpression ( C2iExpression expression );
        CObjetPourSousProprietes ObjetPourSousProprietes { get; set; }
		IFournisseurProprietesDynamiques FournisseurProprietes { get;set;}
		
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CProprieteExpressionEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;
		private static IEditeurExpression m_editeur = null;
		private static IFournisseurProprietesDynamiques m_fournisseurProprietes = null;
        private static CObjetPourSousProprietes m_objetPourSousProprietes = null;
		/// ///////////////////////////////////////////
		public CProprieteExpressionEditor()
		{
		}

		/// ///////////////////////////////////////////
		public static void SetEditeur ( IEditeurExpression editeur)
		{
			m_editeur = editeur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type typeEditeur)
		{
            m_typeEditeur = typeEditeur;
		}

		/// ///////////////////////////////////////////
		public static CObjetPourSousProprietes ObjetPourSousProprietes
		{
			get
			{
				return m_objetPourSousProprietes;
			}
			set
			{
				m_objetPourSousProprietes = value;
			}
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
			IEditeurExpression editeur = m_editeur;
			if (editeur == null && m_typeEditeur != null)
			{
				editeur = (IEditeurExpression)Activator.CreateInstance(m_typeEditeur);
				editeur.ObjetPourSousProprietes = m_objetPourSousProprietes;
				editeur.FournisseurProprietes = m_fournisseurProprietes;
			}
			object retour = editeur.EditeExpression((C2iExpression)value);
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


    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    public class CExpressionOptionsConverter : CGenericObjectConverter<C2iExpression>
    {
        public override string GetString(C2iExpression value)
        {
            if (value == null)
                return "";
            return value.GetString();
        }
    }
}
