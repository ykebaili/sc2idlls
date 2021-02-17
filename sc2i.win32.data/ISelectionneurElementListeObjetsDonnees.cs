using System;
using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de ISelectionneurElementListeObjetsDonnees.
	/// </summary>
	public interface ISelectionneurElementListeObjetsDonnees :  IControlALockEdition
	{
		event EventHandler ElementSelectionneChanged;
		//Indique que le sélectionneur est en cours de mise à jour
		bool IsUpdating();
		CObjetDonnee ElementSelectionne {get; set;}

        void SelectAll();

        bool WantsInputKey(System.Windows.Forms.Keys keyData, bool dataGridViewWantsInputKey);
    }
}
