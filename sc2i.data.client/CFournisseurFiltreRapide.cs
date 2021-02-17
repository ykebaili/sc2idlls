using System;
using System.Collections;
using System.Reflection;

namespace sc2i.data
{

	[AttributeUsage ( AttributeTargets.Property | AttributeTargets.Class, AllowMultiple=true)]
	public class RechercheRapideAttribute : Attribute
	{
		public string ChampFiltre;

		public RechercheRapideAttribute()
		{
			ChampFiltre = "";
		}

		public RechercheRapideAttribute ( string strChamp )
		{
			ChampFiltre = strChamp;
		}
	}

	/// <summary>
	/// Fournit un filtre de recherche rapide sur un élément
	/// Un filtre de recherche rapide cherche dans le champ
	/// "Libelle" de l'élément ou dans le champ "Nom";
	/// Si certaines propriétés de l'élément ont l'attribut [RechercheRapide],
	/// le filtre cherchera également dans ces champs
	/// Le seul paramètre du filtre est la chaine de recherche. Penser à y mettre
	/// les % nécéssaires
	/// </summary>
    /// 
    public delegate void GetFiltreRapideDelegate ( Type typeElements, ref CFiltreData filtreRapide );
	public class CFournisseurFiltreRapide
	{
        private static GetFiltreRapideDelegate m_filtreRapideDelegate;

        public static void SetFiltreDelegate(GetFiltreRapideDelegate delegue)
        {
            m_filtreRapideDelegate = delegue;
        }
		/// <summary>
		/// Retourne un filtre de recherche rapide pour le type demandé
		/// 
		/// </summary>
		/// <param name="tp"></param>
		/// <param name="bContient">Indique que le filtre est un filtre 'Contient' et non 'Commence par'</param>
		/// <returns></returns>
		public static CFiltreData GetFiltreRapideForType ( Type tp )
		{
			if( tp == null )
				return null;
            CFiltreData filtreRetour = null;
            if (m_filtreRapideDelegate != null)
                m_filtreRapideDelegate.Invoke(tp, ref filtreRetour);
            if (filtreRetour != null)
                return filtreRetour;
			bool bAvance = false;
			ArrayList lstChamps = new ArrayList();
			foreach ( PropertyInfo prop in tp.GetProperties() )
			{
				if ( prop.PropertyType == typeof(string) )
				{
					bool bPrendre = false;
					if ( prop.Name.ToUpper() == "LIBELLE" || prop.Name.ToUpper() =="NOM" )
						bPrendre = true;
					else
					{
						object[] attribs = prop.GetCustomAttributes(typeof(RechercheRapideAttribute), true);
						bPrendre = attribs.Length >0;
					}
					if ( bPrendre )
					{
						//Cherche le nom du champ correspondant
						object[] attribs = prop.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
						if ( attribs.Length > 0)
						{
							TableFieldPropertyAttribute attr = (TableFieldPropertyAttribute)attribs[0];
							lstChamps.Add ( attr.NomChamp );
						}
					}
				}
			}
			object[] attribsClasse = tp.GetCustomAttributes ( typeof( RechercheRapideAttribute ), true );
			foreach ( RechercheRapideAttribute attr in attribsClasse )
			{
				if ( attr.ChampFiltre != "" )
					lstChamps.Add ( attr.ChampFiltre );
				if ( attr.ChampFiltre.IndexOf('.') >= 0 )
					bAvance = true;
			}
			if ( lstChamps.Count == 0 )
				return  null;
			string strFiltre = "";
			foreach ( string strChamp in lstChamps )
				strFiltre += strChamp+" like @1 or ";
			strFiltre = strFiltre.Substring(0, strFiltre.Length-4);
			if ( bAvance )
				return new CFiltreDataAvance ( 
					CContexteDonnee.GetNomTableForType ( tp ),
					strFiltre, "" );
			return new CFiltreData ( strFiltre, "" );
		}

	}
}
