using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.client;

namespace sc2i.data
{

	/// <summary>
	/// Une requête au format SC2I.
	/// </summary>
	[Serializable]
	public class C2iRequeteAvancee : I2iSerializable
	{
		//Liste de C2iChampExportCumule
		private ArrayList m_listeChamps = new ArrayList();

		private string m_strTableInterrogee = "";

		private int? m_nIdVersion = null;

		/// <summary>
		/// Filtre à appliquer aux elements pour qu'ils entrent dans la table
		/// </summary>
		private CFiltreData m_filtreAAppliquer = null;

		/// //////////////////////////////////////////////////////////////
		public C2iRequeteAvancee()
		{
			m_nIdVersion = null;
		}

		/// //////////////////////////////////////////////////////////////
		public C2iRequeteAvancee( int ? nIdVersion )
		{
			m_nIdVersion = nIdVersion;
		}

		/// //////////////////////////////////////////////////////////////
		public string TableInterrogee
		{
			get
			{
				return m_strTableInterrogee;
			}
			set
			{
				m_strTableInterrogee = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des C2iChampExportCumule contenus dans la table
		/// </summary>
		public ArrayList ListeChamps
		{
			get
			{
				return m_listeChamps;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public C2iChampDeRequete[] Champs
		{
			get
			{
				return (C2iChampDeRequete[])m_listeChamps.ToArray(typeof(C2iChampDeRequete));
			}
		}
	
		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//1 : ajout de l'id de version
		}

		/// //////////////////////////////////////////////////////////////
		public CFiltreData FiltreAAppliquer
		{
			get
			{
				return m_filtreAAppliquer;
			}
			set
			{
				m_filtreAAppliquer = value;
			}
		}
		/// /////////////////////////////////////////////
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			
			Hashtable tableNoms = new Hashtable();
			foreach(C2iChampDeRequete champ in this.ListeChamps)
			{
				CResultAErreur tempResult = CResultAErreur.True;
				tempResult = champ.VerifieDonnees();
				if (!tempResult)
				{
					result.Erreur.EmpileErreurs(tempResult.Erreur);
					result.SetFalse();
					return result;
				}

				if ( !tableNoms.ContainsKey( champ.NomChamp ))
					tableNoms[champ.NomChamp] = true;
				else 
				{
					result.EmpileErreur(I.T("Several fields have the name \"@1\"|105", champ.NomChamp));
					return result;
				}
			}

			//Vérifie que l'on arrive a créer l'arbre
			result = CalculeArbre();
			if ( !result )
				return result;
			return result;
		}
		
		//----------------------------------------------------------------------------------
		public CResultAErreur Serialize ( C2iSerializer serialiser )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serialiser.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serialiser.TraiteString ( ref m_strTableInterrogee );
			
			if ( result )
				result = serialiser.TraiteArrayListOf2iSerializable ( m_listeChamps );

			I2iSerializable obj = m_filtreAAppliquer;
			result = serialiser.TraiteObject ( ref obj );
			m_filtreAAppliquer = (CFiltreData)obj;

			if ( nVersion >= 1 )
			{
				bool bHasVersion = m_nIdVersion != null;
				serialiser.TraiteBool(ref bHasVersion );
				if ( bHasVersion )
				{
					int nVersionDeBase = m_nIdVersion == null?0:(int)m_nIdVersion;
					serialiser.TraiteInt(ref nVersionDeBase);
					m_nIdVersion = nVersionDeBase;
				}
			}

			return result;
		}

		public CResultAErreur CalculeArbre() 
		{
			CArbreTableParente arbre = new CArbreTableParente ( m_strTableInterrogee );
			return CalculeArbre ( arbre );
		}

		/// <summary>
		/// Le data du result contient un CArbreTable
		/// </summary>
		/// <returns></returns>
		public CResultAErreur CalculeArbre ( CArbreTable arbre )
		{
			CResultAErreur result = CResultAErreur.True;
			//Identifie les relations à mettre en jeu
			ArrayList lstChamps = new ArrayList();
			if ( m_strTableInterrogee == "")
			{
				result.EmpileErreur(I.T("Request table is not defined|106"));
				return result;
			}
			if ( arbre == null )
				arbre = new CArbreTableParente ( m_strTableInterrogee );
			foreach ( C2iChampDeRequete champ in ListeChamps )
			{
				try
				{
					foreach ( CSourceDeChampDeRequete source in champ.Sources )
					{
						CComposantFiltreChamp composant = new CComposantFiltreChamp ( source.Source, m_strTableInterrogee );
						CArbreTable arbreEnCours = arbre;
						foreach ( CInfoRelationComposantFiltre relation in composant.Relations )
						{
                            //Stef 08/08/2013 : toutes les relations sont integrées
                            //en leftouter : en effet, on doit prendre toutes les valeurs
                            //de la table source, même si elles n'ont pas de valeur liées dans la
                            //table fille
                            arbreEnCours = arbreEnCours.IntegreRelation(relation, true, composant.IdChampCustom);
							if ( arbreEnCours == null )
							{
								result.EmpileErreur(I.T("Itegration error of the relation @1|107",relation.RelationKey));
								return result;
							}
						}
						source.ChampDeTable = composant.NomChamp;
						source.Alias = arbreEnCours.Alias;
					}
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException(e));
					result.EmpileErreur(I.T("Error in field @1|108",champ.NomChamp));
					return result;
				}
			}
			result.Data = arbre;
			return result;			
		}

		//-----------------------------------------------------
		public int? IdVersionDeTravail
		{
			get
			{
				return m_nIdVersion;
			}
		}

		/// <summary>
		/// Le data du result contient un datatable avec le résulat de la requête
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		public CResultAErreur ExecuteRequete ( int nIdSession )
		{
			I2iRequeteAvanceeServeur objetServeur = (I2iRequeteAvanceeServeur)C2iFactory.GetNewObjetForSession ( "C2iRequeteAvanceeServeur", typeof(I2iRequeteAvanceeServeur), nIdSession );
			CResultAErreur result = objetServeur.ExecuteRequete ( this );
			if (!result)
				return result;
			result.Data = ((CDataTableFastSerialize)result.Data).DataTableObject;
			return result;
		}
	}

	/// /////////////////////////////////////////////////
	public interface I2iRequeteAvanceeServeur
	{
		/// ////////////////////////////////
		CResultAErreur ExecuteRequete(C2iRequeteAvancee requete);
	}
}
