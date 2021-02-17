using System;
using System.Collections.Generic;
using System.Text;
using sc2i.formulaire;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire.win32.controles2iWnd;


namespace sc2i.formulaire.win32
{
	/// <summary>
	/// Interface g�n�ralisant l'allocation de controles Win32 pour les C2iWndACreationDynamique
	/// </summary>
	public interface IControleWndFor2iWnd : IRuntimeFor2iWnd, IConvertibleEnIElementAProprietesDynamiquesDeportees
	{
		/// <summary>
		/// Cr�e le contr�le
		/// </summary>
		/// <param name="wnd"></param>
		/// <param name="parent"></param>
		void CreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes);


       

		/// <summary>
		/// R�cup�re le contr�le cr��. Un IControleWndFor2iWnd ne doit correspondre qu'� un seul contr�le
		/// </summary>
		Control Control { get;}

        ToolTip Tooltip { get; set; }

	}

    
}
