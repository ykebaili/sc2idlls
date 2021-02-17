using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.data.dynamic;
using sc2i.formulaire.win32;


namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Pour compatibilité, classe obsolète
	/// </summary>
	/// 
	public class CFormEditProprieteExpression : sc2i.formulaire.win32.CFormEditProprieteExpression
	{
		public CFormEditProprieteExpression()
			: base()
		{
			Fournisseur = new CFournisseurPropDynStd(true);
        }
    }
}

