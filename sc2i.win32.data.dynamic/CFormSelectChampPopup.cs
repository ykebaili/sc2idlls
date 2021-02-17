using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	public class CFormSelectChampPopup : sc2i.formulaire.win32.CFormSelectChampPopupBase
	{
		/// /////////////////////////////////////////////////////////////////
        public override void Init(CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur, CDefinitionProprieteDynamique definitionRacineDeChamps)
		{
			if (fournisseur == null)
				fournisseur = new CFournisseurPropDynStd(false);
            base.Init(objetPourSousProprietes, fournisseur, definitionRacineDeChamps);
		}
	}
}
