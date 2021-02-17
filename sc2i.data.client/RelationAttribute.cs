using System;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de RelationAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class RelationAttribute : Attribute
	{
		public RelationAttribute(string strTableParente, string strChampParent, string strChampFils, bool bObligatoire, bool bComposition)
		{
			TableMere = strTableParente;
			ChampsParent = new string[]{strChampParent};
			ChampsFils = new string[] {strChampFils};
			Obligatoire = bObligatoire;
			Composition = bComposition;
			Index = false;
			FillListeChamps ( ref m_strListeChampsFils, ChampsFils );
			FillListeChamps ( ref m_strListeChampsParents, ChampsParent );
		}

		public RelationAttribute(string strTableParente, string[] strChampsParent, string[] strChampsFils, bool bObligatoire, bool bComposition)
		{
			TableMere = strTableParente;
			ChampsParent = strChampsParent;
			ChampsFils = strChampsFils;
			Obligatoire = bObligatoire;
			Composition = bComposition;
			Index = false;
			FillListeChamps ( ref m_strListeChampsFils, ChampsFils );
			FillListeChamps ( ref m_strListeChampsParents, ChampsParent );
		}

		public RelationAttribute(string strTableParente, string strChampParent, string strChampFils, bool bObligatoire, bool bComposition, bool bIndex)
		{
			TableMere = strTableParente;
			ChampsParent = new string[]{strChampParent};
			ChampsFils = new string[] {strChampFils};
			Obligatoire = bObligatoire;
			Composition = bComposition;
			Index = bIndex;
			FillListeChamps ( ref m_strListeChampsFils, ChampsFils );
			FillListeChamps ( ref m_strListeChampsParents, ChampsParent );
		}

		public RelationAttribute(string strTableParente, string[] strChampsParent, string[] strChampsFils, bool bObligatoire, bool bComposition, bool bIndex)
		{
			TableMere = strTableParente;
			ChampsParent = strChampsParent;
			ChampsFils = strChampsFils;
			Obligatoire = bObligatoire;
			Composition = bComposition;
			Index = bIndex;
			FillListeChamps ( ref m_strListeChampsFils, ChampsFils );
			FillListeChamps ( ref m_strListeChampsParents, ChampsParent );
		}

		private void FillListeChamps( ref string strListe, string[] lstSource )
		{
			strListe = "";
			foreach ( string strChamp in lstSource )
				strListe+=strChamp+",";
			if ( strListe.Length != 0 )
				strListe = strListe.Substring(0, strListe.Length-1);
		}

		public readonly string TableMere;
		public readonly string[] ChampsFils;
		public readonly string[] ChampsParent;
		public readonly bool Obligatoire;
		public readonly bool Composition;
		public readonly bool Index;
		protected bool m_PasserLesFilsANullLorsDeLaSuppression = false;
        public bool NePasClonerLesFils = false;

        /// <summary>
        /// Si true, le delete en cascade manuel est gerée par le framework SC2I et non
        /// pas par le .Net
        /// </summary>
        public bool DeleteEnCascadeManuel = false;
        
        /// <summary>
        /// Indique que la relation est dans la base de données.
        /// Si non, la relation ne sera pas créée dans la base de données, par contre,
        /// les champs fils qui la compose y seront
        /// </summary>
        public bool IsInDb = true;

        /// <summary>
        /// Indique que l'index à créer est un index cluster 
        /// </summary>
        public bool IsCluster = false;

		//nécéssaires pour la documentation
		public string m_strListeChampsFils;
		public string m_strListeChampsParents;

		public CInfoRelation GetInfoRelation ( string strTableFille )
		{
			return new CInfoRelation (
				TableMere,
				strTableFille,
				ChampsParent,
				ChampsFils,
				Obligatoire,
				Composition,
				Index,
				PasserLesFilsANullLorsDeLaSuppression,
                DeleteEnCascadeManuel );
		}

		public bool PasserLesFilsANullLorsDeLaSuppression
		{
			get
			{
				return m_PasserLesFilsANullLorsDeLaSuppression;
			}
			set
			{
				m_PasserLesFilsANullLorsDeLaSuppression = value;
			}
		}

	}
}
