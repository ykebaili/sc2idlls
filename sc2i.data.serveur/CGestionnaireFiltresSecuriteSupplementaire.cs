using System;
using System.Collections;

using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description r�sum�e de CGestionnaireFiltresSecuriteSupplementaire.
	/// </summary>
	public delegate void CompleteFiltreSecurite ( int nIdSession, ref CFiltreData filtre );
	public class CGestionnaireFiltresSecuriteSupplementaire
	{
		//Permet d'enregistrer des fonctions de compl�ment de filtre de s�curit�
		//Permet � un objet d'un assembly descendant de g�rer la s�curit� d'un
		//Objet d'un assembly pr�c�dent.
		//Lors de l'appel de la fonction CObjetDonneeServeur::GetFiltreSecurite,
		//Tous les completeurs de filtre de la classe sont appel�s
		
		//Classe objet (CObjetDonnee)->ArrayList de CompleteFiltreSecurite
		private static Hashtable m_tableClasseToCompleteurSecurite = new Hashtable();

		/// ///////////////////////////////////////////////////////
		public static void AddCompleteurSecurite ( Type typeObjet, CompleteFiltreSecurite fonction )
		{
			ArrayList lst = (ArrayList)m_tableClasseToCompleteurSecurite[typeObjet];
			if ( lst == null )
			{
				lst = new ArrayList();
				m_tableClasseToCompleteurSecurite[typeObjet] = lst;
			}
			lst.Add ( fonction );
		}

		/// ///////////////////////////////////////////////////////
		public static void CompleteFiltre ( int nIdSession, Type typeObjet, ref CFiltreData filtre )
		{
			ArrayList lst = (ArrayList)m_tableClasseToCompleteurSecurite[typeObjet];
			if ( lst != null )
			{
				foreach ( CompleteFiltreSecurite fonction in lst )
					fonction ( nIdSession, ref filtre );
			}
		}

	}
}
