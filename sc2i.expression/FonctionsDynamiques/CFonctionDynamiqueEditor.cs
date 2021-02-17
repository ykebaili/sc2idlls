using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;

namespace sc2i.expression.FonctionsDynamiques
{
    public interface IEditeurFonctionsDynamiques
    {
        IEnumerable<CFonctionDynamique> EditeFonctions(IEnumerable<CFonctionDynamique> fonctions,
            CObjetPourSousProprietes objet);
    }

    /// <summary>
    /// Description résumée de CProprieteChampCustomEditor.
    /// </summary>
    public class CFonctionDynamiqueEditor : UITypeEditor
    {
        private static Type m_typeEditeur = null;
        private static CObjetPourSousProprietes m_objetPourSousProprietes = null;

        /// ///////////////////////////////////////////
        public CFonctionDynamiqueEditor()
        {
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
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
            if (m_typeEditeur == null)
                return null;
            IEditeurFonctionsDynamiques editeur = null;
            editeur = (IEditeurFonctionsDynamiques)Activator.CreateInstance(m_typeEditeur);
            object retour = editeur.EditeFonctions((IEnumerable<CFonctionDynamique>)value, m_objetPourSousProprietes).ToArray();
            if (editeur is IDisposable)
                ((IDisposable)editeur).Dispose();
            return retour;
        }

        /// ///////////////////////////////////////////
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_typeEditeur == null)
                return UITypeEditorEditStyle.None;
            return UITypeEditorEditStyle.Modal;
        }




    }
}
