using System;
using System.Collections.Generic;
using System.Text;
using sc2i.formulaire.win32;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.formulaire;

namespace sc2i.win32.data.dynamic.controlesFor2iWnd
{
	/// <summary>
	/// Pour compatibilité
	/// </summary>
	[AutoExec("Autoexec")]
	public class C2iWndFor2iWndExpression
	{
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndFormule), typeof(CWndFor2iFormule));
		}
	}
}
