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
    public interface IEditeurProprieteFiltreDynamique
    {
        // Retourne l'ID d'un CFiltreDynamiqueInDb
        C2iWndListeSpeedStandard.CPointeurFiltreDynamiqueInDb EditeFiltre(Type typeElement, IServiceProvider provider, object value);
    }

    ///////////////////////////////////////////////////////////////////////////////
    public class CProprieteFiltreDynamiqueEditor :UITypeEditor
    {
        private static Type m_typeEditeur;
        
        private static Type m_typeElement;
        
     
        public static void SetTypeElement(Type typeElement)
        {
            m_typeElement = typeElement;
        }

        
        public static void SetTypeEditeur(Type typeEditeur)
        {
            m_typeEditeur = typeEditeur;

        }


        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IEditeurProprieteFiltreDynamique editeur = (IEditeurProprieteFiltreDynamique)Activator.CreateInstance(m_typeEditeur);

            return editeur.EditeFiltre(m_typeElement, provider, value);
           
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_typeEditeur != null)
                return UITypeEditorEditStyle.DropDown;
            else
                return UITypeEditorEditStyle.None;
        }

        //public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        //{
        //    return true;
        //}

        //public override void PaintValue(PaintValueEventArgs e)
        //{
        //    //base.PaintValue(e);
        //    if (e.Value is int)
        //    {
        //        Graphics g = e.Graphics;
        //        Font ft = new Font("Microsoft Sans Serif", 8);
        //        Brush br = Brushes.Black;
        //        int nId = (int)e.Value;
        //        g.DrawString(nId == -1?"None":"Filter", ft, br, new PointF( e.Bounds.Right + 3, 0));

        //        ft.Dispose();
                
        //    }
            
        //}

        
    }
}
