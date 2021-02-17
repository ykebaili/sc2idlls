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
using sc2i.formulaire.win32;

using sc2i.data;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
    [AutoExec("Autoexec")]
    public class CSelectionneurFiltreDynamique : IEditeurFiltreDynamique
    {
        //private CDefinitionProprieteDynamique m_propriete;


        public CFiltreDynamique EditeFiltre(CFiltreDynamique filtreDynamique,IElementAVariablesDynamiquesAvecContexteDonnee eltAVariablesExternes)

        {
            if (filtreDynamique == null)
                filtreDynamique = new CFiltreDynamique();
            filtreDynamique.ElementAVariablesExterne = eltAVariablesExternes;
            CFormEditFiltreDynamique.EditeFiltre(filtreDynamique,
                false,
                true,
                null);
            
            return filtreDynamique;
        }
        

        public static void Autoexec()
        {
                        
            CDefinitionFiltreDynamiqueEditor.SetTypeEditeur(typeof(CSelectionneurFiltreDynamique));
          
        }

        

    }
}
