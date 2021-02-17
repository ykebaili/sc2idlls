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

using sc2i.formulaire;

namespace sc2i.data.dynamic
{
    public interface IEditeurFiltreDynamique
    {
       CFiltreDynamique EditeFiltre(CFiltreDynamique filtreDynamique, IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables);
          
    }
    public class CDefinitionFiltreDynamiqueEditor :UITypeEditor
    {
        private static Type m_typeEditeur;

        private static IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesExternes = null;
        
        private static CDefinitionProprieteDynamique m_propriete;


        public static void SetElementAVariablesExternes(IElementAVariablesDynamiquesAvecContexteDonnee elt)
        {
            m_elementAVariablesExternes = elt;
        }

        
        public static void SetTypeEditeur(Type typeEditeur)
        {
            m_typeEditeur = typeEditeur;

        }

        public static void SetPropriete(CDefinitionProprieteDynamique propriete)
        {
            m_propriete = propriete;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
         IEditeurFiltreDynamique ed = (IEditeurFiltreDynamique)Activator.CreateInstance(m_typeEditeur);
            return ed.EditeFiltre((CFiltreDynamique)value,m_elementAVariablesExternes);
           
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
