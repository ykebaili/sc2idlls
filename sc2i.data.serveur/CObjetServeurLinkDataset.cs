using System;
using System.Data;
using System.Xml;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.server;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Implémente les fonction d'un IObjetServeur pour une source de données
	/// identifiée dans un dataset
	/// </summary>
	/*public class CObjetServeurLinkDataset : C2iObjetServeur, IObjetServeur
	{
		/// /////////////////////////////////////////////////
		public CDroitUtilisateurServeur( int nIdSession )
			:base ( nIdSession )
		{
		}

		/////////////////////////////////////////////////////////
		public DataTable Read ( CFiltreData filtre )
		{
			return null;
		}

		public int GetMaxIdentity()
		{
			return 0;
		}

		/////////////////////////////////////////////////////////////////
		public bool DesactiverIdentifiantAutomatique
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/////////////////////////////////////////////////////////////////
		public bool DesactiverContraintes
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/////////////////////////////////////////////////////////////////
		public CResultAErreur SaveAll ( DataSet ds )
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur("Impossible de sauvegarder les droits utilisateur");
			return result;
		}

		/////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la table contenant le schéma de la base de données
		/// </summary>
		public DataTable FillSchema()
		{
			return null;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Vérifie que l'objet est correct. S'il ne l'est pas,
		/// retourne une erreur avec les problèmes trouvés dans l'objet
		/// </summary>
		public CResultAErreur VerifieDonnees( CValiseObjetDonnee valise )
		{
			return CResultAErreur.True;
		}

		/////////////////////////////////////////////////////////
		public bool HasBlobs()
		{
			return false;
		}

		/////////////////////////////////////////////////////////
		//le data du resultAErreur contient les données (byte[])
		public CResultAErreur ReadBlob ( string strChamp, string strFiltreLigne )
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur("Fonction 'ReadBlob' non disponible pour le CDroitUtilisateurServer");
			return result;
		}

		/////////////////////////////////////////////////////////
		public CResultAErreur SaveBlob ( string strChamp, string strFiltreLigne, byte[] data )
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur("Fonction 'SaveBlob' non disponible pour le CDroitUtilisateurServer");
			return result;
		}


		/////////////////////////////////////////////////////////
		/// <summary>
		/// retourne true si une requête sur le filtre demandé
		/// a des résultats. Les résultats ne sont pas chargés
		/// </summary>
		/// <param name="filtre"></param>
		/// <returns></returns>
		int CountRecords ( string strNomTable, CFiltreData filtre )
		{
		}
	}

		
	}*/
}
