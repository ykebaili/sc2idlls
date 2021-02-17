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
	/// Interface généralisant l'allocation de controles Win32 pour les C2iWndACreationDynamique
	/// </summary>
	public interface IControleWndFor2iWnd : IRuntimeFor2iWnd, IConvertibleEnIElementAProprietesDynamiquesDeportees
	{
		/// <summary>
		/// Crée le contrôle
		/// </summary>
		/// <param name="wnd"></param>
		/// <param name="parent"></param>
		void CreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes);


       

		/// <summary>
		/// Récupère le contrôle créé. Un IControleWndFor2iWnd ne doit correspondre qu'à un seul contrôle
		/// </summary>
		Control Control { get;}

        ToolTip Tooltip { get; set; }

	}

    
}
