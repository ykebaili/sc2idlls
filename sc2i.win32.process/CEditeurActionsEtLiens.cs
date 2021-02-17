using System;
using System.Collections;
using System.Windows.Forms;

using sc2i.process;

namespace sc2i.win32.process
{
	/// <summary>
	/// Sait comment editer les actions et liens sur action
	/// </summary>
	public class CEditeurActionsEtLiens
	{
		//Table TypeAction ou Type Lien ->Form d'édition
		private static Hashtable m_tableEditeurs = new Hashtable();
		
		public static void RegisterEditeur ( Type typeElementAEditer, Type typeEditeur )
		{
			m_tableEditeurs[typeElementAEditer] = typeEditeur;
		}


		public static bool EditeObjet ( IObjetDeProcess obj )
		{
			Type tp = (Type)m_tableEditeurs[obj.GetType()];
			if ( tp == null )
				return true;
			CFormEditObjetDeProcess form = (CFormEditObjetDeProcess)Activator.CreateInstance ( tp );
			form.ObjetEdite = obj;
			bool bResult = form.ShowDialog()==DialogResult.OK;
			form.Dispose();
			return bResult;
		}

	
		
	}
}
