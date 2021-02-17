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
		//Définit la variable associée au controle
		void SetVariable ( IVariableDynamique variable );

		//Récupère les infos d'initialisation à partir d'un C2iWnd
		void FillFrom2iWnd(C2iWnd wnd);

		//Définit l'élément édité par le contrôle
		void SetElementEdite(IElementAVariables element);

        //Redéfinit l'élément édité, mais ne change pas les valeurs affichées
        void SetElementEditeSansChangerLesValeursAffichees(IElementAVariables element);

		//Met à jour l'élément édité par le contrôle
		CResultAErreur MajChamps( bool bControlerValeur );

		//Retourne le control alloué par ce composant
		Control Control { get;}

        //Retourne la valeur du controle 
        object Value { get; set;}

        event EventHandler ValueChanged;

        CWndFor2iWndVariable WndFor2iVariable { get;set;}

        bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey);

        void SelectAll();

	}
}
