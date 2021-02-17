using System;
using System.Data;
using System.Xml;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.server;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Impl�mente les fonction d'un IObjetServeur pour une source de donn�es
	/// identifi�e dans un dataset
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
		/// Retourne la table contenant le sch�ma de la base de donn�es
		/// </summary>
		public DataTable FillSchema()
		{
			return null;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// V�rifie que l'objet est correct. S'il ne l'est pas,
		/// retourne une erreur avec les probl�mes trouv�s dans l'objet
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
		//le data du resultAErreur contient les donn�es (byte[])
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
		/// retourne true si une requ�te sur le filtre demand�
		/// a des r�sultats. Les r�sultats ne sont pas charg�s
		/// </summary>
		/// <param name="filtre"></param>
		/// <returns></returns>
		int CountRecords ( string strNomTable, CFiltreData filtre )
		{
		}
	}

		
	}*/
}
