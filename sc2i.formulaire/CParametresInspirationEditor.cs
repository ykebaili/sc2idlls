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
using sc2i.formulaire.inspiration;

namespace sc2i.formulaire
{
    public interface IEditeurParametresInspiration
    {
        CListeParametresInspiration EditeParametres(CListeParametresInspiration parametres);
    }

	[Serializable]
    public class CParametresInspriationEditor : UITypeEditor
    {
      
        private static Type m_typeEditeur;
		/// ///////////////////////////////////////////
        public CParametresInspriationEditor()
		{
		}

    

        /// ///////////////////////////////////////////
        public static void SetTypeEditeur(Type typeEditeur)
        {
            m_typeEditeur = typeEditeur;
           
        }

        
        /// ///////////////////////////////////////////
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
            IEditeurParametresInspiration ed = (IEditeurParametresInspiration)Activator.CreateInstance(m_typeEditeur);
            return ed.EditeParametres(value as CListeParametresInspiration);
        }


        /// ///////////////////////////////////////////
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_typeEditeur != null)
                return UITypeEditorEditStyle.Modal;
            else
                return UITypeEditorEditStyle.None;
            
        }

    }
}
