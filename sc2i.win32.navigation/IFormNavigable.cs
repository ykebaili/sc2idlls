using System;
using sc2i.common;
using System.Drawing;

namespace sc2i.win32.navigation
{
	/// <summary>
	/// Description résumée de IFormNavigable.
	/// </summary>
    /// 
    public delegate void ContexteFormEventHandler ( IFormNavigable form, CContexteFormNavigable ctx );

	public interface IFormNavigable : IDisposable
	{
		CContexteFormNavigable GetContexte();
		CResultAErreur InitFromContexte(CContexteFormNavigable contexte);
		bool QueryClose();

        CResultAErreur Actualiser();
		//Appellée par le navigateur lorsqu'il affiche une nouvelle page et
		//souhaite masquer celle-ci
		void Masquer();

		CFormNavigateur Navigateur{get;set;}

        event ContexteFormEventHandler AfterGetContexte;
        event ContexteFormEventHandler AfterInitFromContexte;

        string GetTitle();

        Image GetImage();

	}
}
