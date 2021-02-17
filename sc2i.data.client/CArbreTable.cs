using System;
using System.Collections;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Arbre des tables nécéssaires à une requête
	/// </summary>
	[Serializable]
	public abstract class CArbreTable 
	{
		private string m_strAlias;
		
		//si une table doit être présente sous deux alias différents dans une
		//requete, IdGroupeRelation est différent pour les deux instances de la table.
		private int m_nIdGroupeRelation = -1;
		private ArrayList m_listeTablesLiees = new ArrayList();


        //////////////////////////////////////////////////////////
        public string[] TablesAccedees
        {
            get
            {
                List<string> lst = new List<string>();
                FillTablesAccedees(lst);
                return lst.ToArray();
            }
        }

        //////////////////////////////////////////////////////////
        protected virtual void FillTablesAccedees(List<string> lst)
        {
            lst.Add(NomTable);
            foreach (CArbreTable arbre in TablesLiees)
                arbre.FillTablesAccedees(lst);
        }


		//////////////////////////////////////////////////////////
		///<summary>
		///Tableau de CArbreTable
		///</summary>
		public CArbreTableFille[] TablesLiees
		{
			get
			{
				return (CArbreTableFille[])m_listeTablesLiees.ToArray (typeof(CArbreTableFille));
			}
		}

		//////////////////////////////////////////////////////////
		public int IdGroupeRelation
		{
			get
			{
				return m_nIdGroupeRelation;
			}
			set
			{
				m_nIdGroupeRelation = value;
			}
		}

		

		//////////////////////////////////////////////////////////
		public abstract string NomTable{get;}

		//////////////////////////////////////////////////////////
		public string Alias
		{
			get
			{
				return m_strAlias;
			}
			set
			{
				m_strAlias = value;
			}
		}

		//////////////////////////////////////////////////////////
		public abstract string RacineAliasFils{get;}

		

		//////////////////////////////////////////////////////////
		public CArbreTable IntegreRelation ( CInfoRelationComposantFiltre relation, bool bIsLeftOuter, int nIdGroupeRelation )
		{
			foreach ( CArbreTableFille arbre in m_listeTablesLiees )
			{
				if ( arbre.Relation.RelationKey == relation.RelationKey &&
					arbre.Relation.IsRelationFille == relation.IsRelationFille &&
					arbre.IdGroupeRelation == nIdGroupeRelation )
				{
					if ( bIsLeftOuter && !arbre.IsLeftOuter )
						arbre.IsLeftOuter = bIsLeftOuter;
					return arbre;
				}
			}
			//la table dépendante n'existe pas
			string strIdAlias = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			string strNumAlias = strIdAlias[m_listeTablesLiees.Count]+"";
			CArbreTableFille arbreNew = new CArbreTableFille ( this, relation, RacineAliasFils+strNumAlias);
			arbreNew.IsLeftOuter = bIsLeftOuter;
			arbreNew.IdGroupeRelation = nIdGroupeRelation;
			m_listeTablesLiees.Add ( arbreNew );
			return arbreNew;

		}

        private class CSorterTableLiee : IComparer
        {
            public int Compare(object x, object y)
            {
                CArbreTableFille arbre1 = x as CArbreTableFille;
                CArbreTableFille arbre2 = y as CArbreTableFille;
                if (arbre1 == null || arbre2 == null)
                    return 0;
                return arbre1.Relation.IsRelationFille.CompareTo(arbre2.Relation.IsRelationFille);
            }
        }

        //Trie la liste des tables liées pour optimisation du join
        public void SortTablesLiees()
        {
            m_listeTablesLiees.Sort(new CSorterTableLiee());
        }
    }

	/// ///////////////////////////////////////////////////////
	[Serializable]
	public class CArbreTableParente : CArbreTable
	{
		private string m_strNomTable;

		public CArbreTableParente ( string strNomTable )
		{
			m_strNomTable = strNomTable;
			Alias = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable);
		}

		/// ///////////////////////////////////////////////////////
		public override string NomTable
		{
			get
			{
				return m_strNomTable;
			}
		}

		/// ///////////////////////////////////////////////////////
		public override string RacineAliasFils
		{
			get
			{
				return "AL_";
			}
		}

	}

	/// ///////////////////////////////////////////////////////
	[Serializable]
	public class CArbreTableFille : CArbreTable
	{
		private CInfoRelationComposantFiltre m_infoRelation;
		private bool m_bIsLeftOuter = false;
		CArbreTable m_parent = null;

		/// ///////////////////////////////////////////////////////
		public CArbreTableFille (  CArbreTable parent, CInfoRelationComposantFiltre relation, string strAlias )
		{
			m_infoRelation = relation;
			Alias = strAlias;
			m_parent = parent;
		}

		/// ///////////////////////////////////////////////////////
		public override string NomTable
		{
			get
			{
				if ( m_infoRelation.IsRelationFille )
					return m_infoRelation.TableFille;
				else
					return m_infoRelation.TableParente;
			}
		}

		//////////////////////////////////////////////////////////
		public CInfoRelationComposantFiltre Relation
		{
			get
			{
				return m_infoRelation;
			
			}
		}

		//////////////////////////////////////////////////////////
		public bool IsLeftOuter
		{
			get
			{
				return m_bIsLeftOuter;
			}
			set
			{
				m_bIsLeftOuter = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public CInfoRelationComposantFiltre[] CheminRelations
		{
			get
			{
				ArrayList lst = new ArrayList();
				InsertCheminRelations ( lst );
				return (CInfoRelationComposantFiltre[])lst.ToArray(typeof(CInfoRelationComposantFiltre));
			}
		}

		/// ///////////////////////////////////////////////////////
		protected void InsertCheminRelations ( ArrayList lst )
		{
			if ( m_parent != null && m_parent is CArbreTableFille )
				((CArbreTableFille)m_parent).InsertCheminRelations ( lst );
			lst.Add ( m_infoRelation );
		}

		/// ///////////////////////////////////////////////////////
		public override string RacineAliasFils
		{
			get
			{
				return Alias;
			}
		}

	}

}
