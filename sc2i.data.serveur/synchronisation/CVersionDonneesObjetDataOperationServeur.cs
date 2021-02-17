using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.data;
using sc2i.data.synchronisation;


namespace sc2i.data.serveur.synchronisation
{
    /*OBSOLETE AVEC CDBKey
	public class CMapIdMainToIdSecInDbServeur : CObjetServeur
	{
		public CMapIdMainToIdSecInDbServeur(int nIdSession)
			: base(nIdSession)
		{

		}

		//------------------------------------------------
		/// <summary>
		/// Pas de journalisation de ces données
		/// </summary>
		public override IJournaliseurDonneesObjet JournaliseurChamps
		{
			get
			{
				return null;
			}
		}

		//------------------------------------------------
		/// <summary>
		/// Travail toujours directement dans la base !
		/// </summary>
		public override int? IdVersionDeTravail
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public override string GetNomTable()
		{
			return CMapIdMainToIdSecInDb.c_nomTable;
		}

		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			return CResultAErreur.True;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CMapIdMainToIdSecInDb);
		}
	}
     * */
}
