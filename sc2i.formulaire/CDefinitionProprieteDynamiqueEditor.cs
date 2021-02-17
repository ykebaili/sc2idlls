using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using System.Data;

using sc2i.common;
using sc2i.expression;
using sc2i.drawing;

namespace sc2i.formulaire
{
    public interface IEditeurProprieteDynamique
    {
        CDefinitionProprieteDynamique EditePropriete(CDefinitionProprieteDynamique propriete,
            CObjetPourSousProprietes objetPourSousProprietes,
            IFournisseurProprietesDynamiques fournisseur,
            bool bAffectable,
            Type[]typesAutorises);
    }

	[Serializable]
    public class CDefinitionProprieteDynamiqueEditor : UITypeEditor
    {
      
        private static Type m_typeEditeur;
        private static CObjetPourSousProprietes m_objetPourSousProprietes;
        private static IFournisseurProprietesDynamiques m_fournisseur;
        private static bool m_bAffectable;
        private static Type[] m_typesAutorises;
		/// ///////////////////////////////////////////
		public CDefinitionProprieteDynamiqueEditor()
		{
		}

    

        /// ///////////////////////////////////////////
        public static void SetTypeEditeur(Type typeEditeur)
        {
            m_typeEditeur = typeEditeur;
           
        }

        public static void SetObjetPourSousProprietes(CObjetPourSousProprietes objet)
        {
            m_objetPourSousProprietes = objet;
           
        }


        public static void SetFournisseur(IFournisseurProprietesDynamiques fournisseur)
        {
            m_fournisseur = fournisseur;
          
        }

        public static void SetBAffectable(bool bAffectable)
        {
            m_bAffectable = bAffectable;
            
        }

        public static void SetTypeAutorises(Type[] typeAutorises)
        {

            m_typesAutorises = typeAutorises;
        }
        /// ///////////////////////////////////////////
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
            IEditeurProprieteDynamique ed=(IEditeurProprieteDynamique)Activator.CreateInstance(m_typeEditeur);
            return ed.EditePropriete((CDefinitionProprieteDynamique)value, m_objetPourSousProprietes, m_fournisseur, m_bAffectable, m_typesAutorises);
        }



        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_typeEditeur != null)
                return UITypeEditorEditStyle.Modal;
            else
                return UITypeEditorEditStyle.None;
            
        }

    }
}
