using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Permet d'obtenir un filtredynamique pour chaque table
	/// </summary>
	////////////////////////////////////////////////////////////////////////
	public class CFiltresDynamiquesForTables
	{
		//Nom de table->filtres de synchronisation pour cette table
		private Hashtable m_tableTableToFiltreSynchronisation = new Hashtable();

		public CFiltresDynamiquesForTables()
		{
		}

		////////////////////////////////////////////////////////////////////////
		public void Reset()
		{
			m_tableTableToFiltreSynchronisation.Clear();
		}

		////////////////////////////////////////////////////////////////////////
		public void AddFiltreSynchronisation( CFiltreSynchronisation filtreSynchro)
		{
			PrivateAddFiltre ( filtreSynchro );
		}

		////////////////////////////////////////////////////////////////////////
		private void PrivateAddFiltre ( CFiltreSynchronisation filtre )
		{
			ArrayList lst = (ArrayList)m_tableTableToFiltreSynchronisation[filtre.NomTable];
			if ( lst == null )
			{
				lst = new ArrayList();
				m_tableTableToFiltreSynchronisation[filtre.NomTable] = lst;
			}
			CInfoRelation relation = filtre.RelationToParent;
			if ( relation != null )
			{
				//Regarde s'il y a un filtre sur la table complète avant d'aller plus loin !
				foreach ( CFiltreSynchronisation filtreExistant in lst )
				{
					if ( filtreExistant.IsLienToFullTableParente && 
						filtreExistant.RelationToParent.Equals ( relation ) )
						//Il y a un filtre sur toute la table liée, on n'ajoute pas celui-ci
						return;
				}
				if ( filtre.IsLienToFullTableParente )
				{
					//Supprime tous les autres filtres pour la même relation
					//puisqu'on ajoute un filtre sur toute la table liée
					for ( int nFiltre = lst.Count-1; nFiltre >= 0; nFiltre-- )
					{
						CFiltreSynchronisation filtreExistant = (CFiltreSynchronisation)lst[nFiltre];
						if ( relation.Equals ( filtreExistant.RelationToParent ) )
							lst.RemoveAt ( nFiltre );
					}
				}
			}
			lst.Add ( filtre );
			foreach ( CFiltreSynchronisation filtreFils in filtre.FiltresFils )
				PrivateAddFiltre ( filtreFils );
		}

		////////////////////////////////////////////////////////////////////////
		public CFiltreDynamique GetFiltreDynamiqueForTable ( string strNomTable, ref bool bShouldSynchroniseTable )
		{
			ArrayList lst = (ArrayList)m_tableTableToFiltreSynchronisation[strNomTable];
			Type tp = CContexteDonnee.GetTypeForTable ( strNomTable );
			
			if ( tp != null && tp.GetCustomAttributes(typeof(FullTableSyncAttribute), true).Length > 0 )
			{
				bShouldSynchroniseTable = true;
				return null;
			}
			if ( lst == null || lst.Count == 0 )
			{
				bShouldSynchroniseTable = false;
				return null;
			}
			bShouldSynchroniseTable = true;
			CFiltreDynamique filtreDynamique = CFiltreSynchronisation.GetFiltreDynamiqueSynchro ( CContexteDonnee.GetTypeForTable ( strNomTable ) );

			CComposantFiltreDynamiqueOu composantOu = new CComposantFiltreDynamiqueOu();
			foreach ( CFiltreSynchronisation filtreSynchro in lst )
			{
				if ( filtreSynchro.TouteLaTable || filtreSynchro.FiltreDynamique == null )
					return null;
				CFiltreDynamique filtreLocal = filtreSynchro.GetFiltreToElementPrincipal();
				if ( filtreLocal != null && filtreLocal.ComposantPrincipal != null )
					composantOu.AddComposantFils ( filtreLocal.ComposantPrincipal );
				if ( filtreLocal == null )
					return null;
			}
			if ( composantOu.GetNbComposantsFils() == 0 )
				return null;
			if ( composantOu.GetNbComposantsFils() == 1 )
				filtreDynamique.ComposantPrincipal = composantOu.ComposantsFils[0];
			else
				filtreDynamique.ComposantPrincipal = composantOu;
			return filtreDynamique;
		}

		////////////////////////////////////////////////////////////////////////
		public string[] GetListeTables()
		{
			string[] strTables = new string[m_tableTableToFiltreSynchronisation.Count];
			int nIndex = 0;
			foreach ( string strTable in m_tableTableToFiltreSynchronisation.Keys )
				strTables[nIndex++] = strTable;
			return strTables;
		}

	}
}
