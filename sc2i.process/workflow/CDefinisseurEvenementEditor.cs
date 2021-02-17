using System;
using System.Drawing.Design;

using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.process.workflow
{
	public interface IDefinisseurEvenementsEditor
	{
        void EditeDefinisseur(IDefinisseurEvenements definisseur);
	}

    //car le IDefinisseurEvenement n'est pas éditable, je ne sais pas pourquoi,
    //il faut passer par cette classe pour que çà marche
    public class CDefinisseurEvenementsEditable
    {
        public readonly IDefinisseurEvenements Definisseur;

        public CDefinisseurEvenementsEditable(IDefinisseurEvenements def)
        {
            Definisseur = def;
        }

        public override string ToString()
        {
            return "Setup";
        }
    }

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
	public class CDefinisseurEvenementEditor : UITypeEditor
	{
		private static Type m_typeEditeur = null;

		/// ///////////////////////////////////////////
        public CDefinisseurEvenementEditor()
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
            IDefinisseurEvenementsEditor editeur = (IDefinisseurEvenementsEditor)Activator.CreateInstance(m_typeEditeur);
            CDefinisseurEvenementsEditable def = value as CDefinisseurEvenementsEditable;
            if (def == null)
                return value;
            IDefinisseurEvenements definisseur = def.Definisseur;
            editeur.EditeDefinisseur(definisseur);
			if (editeur is IDisposable)
				((IDisposable)editeur).Dispose();
			return value;
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
