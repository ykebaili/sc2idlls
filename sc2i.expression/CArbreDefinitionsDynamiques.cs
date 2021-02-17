using System;
using System.Collections;
using System.Collections.Generic;

namespace sc2i.expression
{
	/// <summary>
	/// Permet de connaitre toutes les définitions utilisées par un expression
	/// </summary>
	[Serializable]
	public class CArbreDefinitionsDynamiques
	{
		private CDefinitionProprieteDynamique m_definition;
		private ArrayList m_listeSousArbres = new ArrayList();

		[NonSerialized]
		private Hashtable m_extendedProperties = new Hashtable();

		/// <summary>
		/// Liste des propriétés sous forme de string,
		/// car l'élément qui les a ajouté n'avait pas assez d'info pour faire
		/// des CDEfinitionProprietesDynamiques
		/// </summary>
		private ArrayList m_listeAutresSousProprietesString = new ArrayList();
		
		/// /////////////////////////////////////////////////////////////////////
		public CArbreDefinitionsDynamiques( CDefinitionProprieteDynamique def)
		{
			m_definition = def;
		}

		/// /////////////////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique DefinitionPropriete
		{
			get
			{
				return m_definition;
			}
		}

		/// /////////////////////////////////////////////////////////////////////
		public void AddSousArbre ( CArbreDefinitionsDynamiques arbre )
		{
			m_listeSousArbres.Add ( arbre );
		}

		/// /////////////////////////////////////////////////////////////////////
		public void RemoveSousArbre(CArbreDefinitionsDynamiques arbre)
		{
			m_listeSousArbres.Remove(arbre);
		}

		/// /////////////////////////////////////////////////////////////////////
		public void AddSousProprieteString ( string strPropriete )
		{
			m_listeAutresSousProprietesString.Add ( strPropriete );
		}

		/// /////////////////////////////////////////////////////////////////////
		public string[] AutresSousProprietesString
		{
			get
			{
				return (string[])m_listeAutresSousProprietesString.ToArray(typeof(string));
			}
		}

		

		/// /////////////////////////////////////////////////////////////////////
		public CArbreDefinitionsDynamiques GetArbre ( CDefinitionProprieteDynamique def )
		{
			foreach ( CArbreDefinitionsDynamiques arbre in m_listeSousArbres )
				if ( arbre.DefinitionPropriete.CompareTo ( def ) == 0 )
					return arbre;
			return null;
		}

		/// /////////////////////////////////////////////////////////////////////
		public CArbreDefinitionsDynamiques[] SousArbres
		{
			get
			{
				return (CArbreDefinitionsDynamiques[])m_listeSousArbres.ToArray ( typeof(CArbreDefinitionsDynamiques));
			}
		}

		/// /////////////////////////////////////////////////////////////////////
		/// Retourne la liste des propriétés séparés par des points
		/// accedées par cet arbre
		public string[] GetListeProprietesAccedees()
		{
			ArrayList lst = new ArrayList();
			AddProprietesToListe ( lst, "" );
			return (string[])lst.ToArray(typeof(string));
		}

		/// /////////////////////////////////////////////////////////////////////
		private void AddProprietesToListe ( ArrayList lst, string strChemin )
		{
			if ( strChemin != "" )
				strChemin += ".";
			if ( DefinitionPropriete != null )
			{
				lst.Add ( strChemin+DefinitionPropriete.NomPropriete );
				strChemin += DefinitionPropriete.NomPropriete;
			}
			foreach ( string strProp in m_listeAutresSousProprietesString )
				lst.Add ( strChemin + strProp );
			
			foreach ( CArbreDefinitionsDynamiques sousArbre in m_listeSousArbres )
				sousArbre.AddProprietesToListe ( lst, strChemin );
		}


		/// /////////////////////////////////////////////////////////////////////
		///Permet de véhiculer des objets dans l'arbre
		///par exemple, il peut y avoir un contextedonnee sous la clé typeof(CContexteDonnee)
		public Hashtable ExtendedProperties
		{
			get
			{
				return m_extendedProperties;
			}
		}

        
	}
}
