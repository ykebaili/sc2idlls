using System;
using System.Collections.Generic;
using System.Text;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.formulaire;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.multitiers.client;
using sc2i.win32.data.dynamic.controlesFor2iWnd;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	public interface IControleForVariable : IControlALockEdition
	{
		//D�finit la variable associ�e au controle
		void SetVariable ( IVariableDynamique variable );

		//R�cup�re les infos d'initialisation � partir d'un C2iWnd
		void FillFrom2iWnd(C2iWnd wnd);

		//D�finit l'�l�ment �dit� par le contr�le
		void SetElementEdite(IElementAVariables element);

        //Red�finit l'�l�ment �dit�, mais ne change pas les valeurs affich�es
        void SetElementEditeSansChangerLesValeursAffichees(IElementAVariables element);

		//Met � jour l'�l�ment �dit� par le contr�le
		CResultAErreur MajChamps( bool bControlerValeur );

		//Retourne le control allou� par ce composant
		Control Control { get;}

        //Retourne la valeur du controle 
        object Value { get; set;}

        event EventHandler ValueChanged;

        CWndFor2iWndVariable WndFor2iVariable { get;set;}

        bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey);

        void SelectAll();

	}
}
