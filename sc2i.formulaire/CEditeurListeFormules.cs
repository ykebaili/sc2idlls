using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using System.Drawing.Design;

namespace sc2i.formulaire
{
    public interface IEditeurListeFormules
    {
        C2iExpression[] EditeFormules(C2iExpression[] formules);
        CObjetPourSousProprietes ObjetPourSousProprietes { get; set; }
    }

    /// <summary>
    /// Description résumée de CProprieteChampCustomEditor.
    /// </summary>
    public class CListeFormulesEditor : UITypeEditor
    {
        private static Type m_typeEditeur = null;
        private static CObjetPourSousProprietes m_objetPourSousProprietes = null;

        /// ///////////////////////////////////////////
        public CListeFormulesEditor()
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
            IEditeurListeFormules editeur = null;
            editeur = (IEditeurListeFormules)Activator.CreateInstance(m_typeEditeur);
            editeur.ObjetPourSousProprietes = m_objetPourSousProprietes;
            object retour = editeur.EditeFormules((C2iExpression[])value);
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
