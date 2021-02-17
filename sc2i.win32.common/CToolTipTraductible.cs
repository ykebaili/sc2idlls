using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using sc2i.common;

namespace sc2i.win32.common
{

     
    
    public class CToolTipTraductible : ToolTip
    {

        public CToolTipTraductible(IContainer cont)
            : base(cont)
        { 
            // Ajoute un traitement sur l'évenement: apparition de l'infos bulle
            this.Popup += new PopupEventHandler(CToolTipTraductible_Popup);
        }

        void CToolTipTraductible_Popup(object sender, PopupEventArgs e)
        {
            string strChaineTraduite = "";
            Form FormulaireParent = null;

            // Récupère le texte infos bulle
            string strChaineATraduire = this.GetToolTip(e.AssociatedControl);
                        
            // Recherche du formulaire parent du controle à traduire
            if(e.AssociatedControl != null)
                FormulaireParent = e.AssociatedControl.FindForm();
            
            // Si non null et si chaine non traduite
            if ((FormulaireParent != null) && (strChaineATraduire.IndexOf('|') >= 0)) 
            {
                // Traduit le texte
                strChaineTraduite = I.T(strChaineATraduire); 
                // Met à jour le texte infos bulle dans une nouvelle langue
                SetToolTip(e.AssociatedControl, strChaineTraduite);
            }
 
        }

 
    }
}
