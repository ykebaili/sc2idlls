using System;
using System.Data;

using sc2i.common;
using sc2i.data.serveur;
using sc2i.data.dynamic;
using sc2i.multitiers.server;
using System.Collections;


namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de C2iRequeteAvanceeServeur.
	/// </summary>
	public class C2iRequeteAvanceeServeur : C2iObjetServeur, I2iRequeteAvanceeServeur
	{
		/// ////////////////////////////////
		public C2iRequeteAvanceeServeur()
		{
		}

		/// ////////////////////////////////
		public C2iRequeteAvanceeServeur( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////
		public CResultAErreur ExecuteRequete(C2iRequeteAvancee requete)
		{
			CResultAErreur result = CResultAErreur.True;
			string strTable = requete.TableInterrogee;
			//S'il y a un filtre sur la requête avancée,
			//On prépare d'abord la sous requête de sélection des ids qui nous
			//Interessent

			Type typeReference = CContexteDonnee.GetTypeForTable(strTable);
			if ( typeReference == null )
			{
				result.EmpileErreur (I.T("Impossible to define the associated type with @1|121",strTable));
				return result;
			}				

			IDatabaseConnexion con;
			if ( typeReference == null )
				con = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, "");
			else
				con = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, typeReference);


			CFiltreData filtre = requete.FiltreAAppliquer;
			CArbreTable arbre = null;
			bool bHasVersionSurTablePrincipale = false;
			Type tpObjet = CContexteDonnee.GetTypeForTable(requete.TableInterrogee);
			if (tpObjet != null && !typeof(IObjetSansVersion).IsAssignableFrom(tpObjet) &&
				typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tpObjet))
				bHasVersionSurTablePrincipale = true;
			if (bHasVersionSurTablePrincipale)
			{
				CFiltreDataAvance filtreVersion = new CFiltreDataAvance(strTable,"");
				//Lecture dans le référentiel
				if (requete.IdVersionDeTravail == null && (filtre == null || !filtre.IgnorerVersionDeContexte))
				{
					filtreVersion.Filtre = "HasNo("+CSc2iDataConst.c_champIdVersion+")";
				}
				//Ignorer les suppressions
				if (filtre != null && !filtre.IntegrerLesElementsSupprimes)
				{
					if (filtreVersion.Filtre != "")
						filtreVersion.Filtre += " and ";
					filtreVersion.Filtre += CSc2iDataConst.c_champIsDeleted + "=0";
				}


				//Lecture dans une version
				if (requete.IdVersionDeTravail != null && (filtre == null || !filtre.IgnorerVersionDeContexte) && requete.IdVersionDeTravail >= 0)
				{
					if (filtre == null)
						filtre = new CFiltreData();
					filtre.IdsDeVersionsALire = CVersionDonnees.GetVersionsToRead(IdSession, (int)requete.IdVersionDeTravail);
				}

				if (filtreVersion.Filtre != "")
					filtre = CFiltreData.GetAndFiltre(filtre, filtreVersion);
			}
			if ( filtre != null && filtre is CFiltreDataAvance )
			{
				result = ((CFiltreDataAvance)filtre).GetArbreTables();
				if (!result )
					return result;
				arbre = (CArbreTable)result.Data;
			}
			
			result = requete.CalculeArbre(arbre);
			if ( !result )
				return result;
			arbre = (CArbreTable)result.Data;
			
			int nOldTimeOut = con.CommandTimeOut;
			con.CommandTimeOut = 60*2;
			result = con.ExecuteRequeteComplexe ( requete.Champs, arbre, filtre );
			con.CommandTimeOut = nOldTimeOut;
			return result;
		}
	}
}