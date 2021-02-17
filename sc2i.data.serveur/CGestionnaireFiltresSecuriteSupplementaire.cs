using System;
using System.Collections;

using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CGestionnaireFiltresSecuriteSupplementaire.
	/// </summary>
	public delegate void CompleteFiltreSecurite ( int nIdSession, ref CFiltreData filtre );
	public class CGestionnaireFiltresSecuriteSupplementaire
	{
		//Permet d'enregistrer des fonctions de complément de filtre de sécurité
		//Permet à un objet d'un assembly descendant de gérer la sécurité d'un
		//Objet d'un assembly précédent.
		//Lors de l'appel de la fonction CObjetDonneeServeur::GetFiltreSecurite,
		//Tous les completeurs de filtre de la classe sont appelés
		
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
