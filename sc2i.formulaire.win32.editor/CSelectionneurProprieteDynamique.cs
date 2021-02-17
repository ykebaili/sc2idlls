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

namespace sc2i.formulaire.win32
{
    [AutoExec("Autoexec")]
    public class CSelectionneurProprieteDynamique : IEditeurProprieteDynamique
    {
        private Type[] m_typesAutorises;

        private bool AccepteChamp(CDefinitionProprieteDynamique definition)
        {
               
			if (definition.IsReadOnly == false)
			{
				foreach (Type type in m_typesAutorises)
				{
					if ( type.IsAssignableFrom(definition.TypeDonnee.TypeDotNetNatif))
						return true;
				}
			}
            return false;
        }

       public  CDefinitionProprieteDynamique EditePropriete(CDefinitionProprieteDynamique propriete,
           CObjetPourSousProprietes objetPourSousProprietes,
           IFournisseurProprietesDynamiques fournisseur,
           bool bAffectable,
           Type[] typesAutorises)
        {
            m_typesAutorises = typesAutorises;
            int nPosLargeur = (((int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width/2))-100);
            int nPosHauteur = (((int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height/2))-150);
            Rectangle rectPos = new Rectangle(nPosLargeur,nPosHauteur,200,300);
            bool bCancel = false;
          
           
            propriete=CFormSelectChampPopupBase.SelectDefinitionChamp(
                rectPos, 
                objetPourSousProprietes, 
                fournisseur,
                ref bCancel, 
                new BeforeIntegrerChampEventHandler ( AccepteChamp ),
                propriete);
           
            return propriete;
        }
        public static void Autoexec()
        {

            CDefinitionProprieteDynamiqueEditor.SetTypeEditeur(typeof(CSelectionneurProprieteDynamique));
            

        }
    }
}
