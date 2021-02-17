using System;
using System.Data;
using System.Data.Common;
using System.Collections;

using sc2i.common;

namespace sc2i.data.serveur
{
	/// summary
	/// Un data adapter qui se gère lui même toutes ses requêtes en fonction
	/// d'un type (avec CStructureTable)
	/// summary
	public abstract class C2iDataAdapterForType : IDataAdapter
	{
		private Type m_typeObjets;
//		private CStructureTable m_structure;
		protected CStructureTable m_structure;
//		private IDatabaseConnexion m_connexion;
		protected IDatabaseConnexion m_connexion;
		private CFiltreData m_filtre;
		private Hashtable m_tableExclusions = new Hashtable();

		private MissingMappingAction m_missingMappingAction = MissingMappingAction.Ignore;
		private MissingSchemaAction m_missingSchemaAction = MissingSchemaAction.Ignore;
		private DataTableMappingCollection m_tableMapping = new DataTableMappingCollection();


		/// //////////////////////////////////////////////////
		public C2iDataAdapterForType( IDatabaseConnexion connexion, Type typeObjets, CFiltreData filtre, params string[] strExclusions )
		{
			m_connexion = connexion;
			m_typeObjets = typeObjets;
			m_structure = CStructureTable.GetStructure(m_typeObjets);
			m_tableMapping.Add(new DataTableMapping(m_structure.NomTableInDb, m_structure.NomTable));
			m_filtre = filtre;
			foreach ( string strChamp in strExclusions )
				m_tableExclusions[strChamp] = true;
		}

		/// //////////////////////////////////////////////////
		protected bool IsExclu ( string strChamp )
		{
			return m_tableExclusions[strChamp] != null;
		}

		/// //////////////////////////////////////////////////
		///A surcharger : permet d'insérer un objet dans une requête.
		///Tpypiquement, cette fonction, ajoute les guillemets autour des chaines,
		///retourne une date au format attendu par la bdd ou autre.
		public abstract string GetStringForRequete ( object obj );

		/// /////////////////////////////////////////////////////////////////////////////
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return m_missingMappingAction;
			}
			set
			{
				m_missingMappingAction = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return m_missingSchemaAction;
			}
			set
			{
				m_missingSchemaAction = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public ITableMappingCollection TableMappings
		{
			get
			{
				return m_tableMapping;
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////
		/// Modifié le 17 juillet 2003 par Stéphane : ajout du mot clé virtual
		public virtual DataTable[] FillSchema ( DataSet dsDest, SchemaType schema )
		{
			string strReq = "select * from "+m_structure.NomTableInDb;
			IDataAdapter adapter = m_connexion.GetSimpleReadAdapter ( strReq );
			foreach ( DataTableMapping tm in TableMappings )
				adapter.TableMappings.Add ( tm );
			adapter.MissingMappingAction = MissingMappingAction;
			adapter.MissingSchemaAction = MissingSchemaAction;
            DataTable[] tables = adapter.FillSchema(dsDest, schema);
            CUtilDataAdapter.DisposeAdapter(adapter);
            return tables;
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public IDataParameter[] GetFillParameters()
		{
			return null;
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public int Fill ( DataSet dsDest )
		{
			string strReq = "select * from "+m_structure.NomTableInDb;
			IDataAdapter adapter = m_connexion.GetSimpleReadAdapter ( strReq, m_filtre );
            int nFill = adapter.Fill ( dsDest );
            CUtilDataAdapter.DisposeAdapter ( adapter );
            return nFill;
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public int Update ( DataSet dsDest )
		{
			DataTable table = dsDest.Tables[m_structure.NomTable];

			int nNbRows = table.Rows.Count;
			CResultAErreur result = CResultAErreur.True;
			foreach ( DataRow row in table.Rows )
			{
				if ( row.RowState == DataRowState.Added )
					result = DoAdd ( row );
				else if (row.RowState == DataRowState.Modified )
					result = DoUpdate ( row );
				else if (row.RowState == DataRowState.Deleted )
					result = DoDelete ( row );
				else
					nNbRows--;
				if ( !result )
				{
					throw new CExceptionErreur ( result.Erreur );
				}
			}
			return nNbRows;
		}



		// /////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur DoAdd ( DataRow row )
		{
			string strRequete = "insert into "+m_structure.NomTableInDb+"(";
			string strValeurs = "";
			foreach ( CInfoChampTable info in m_structure.Champs )
			{
				if ( !info.IsAutoId && !IsExclu ( info.NomChamp ))
				{
					strRequete += info.NomChamp+",";
					strValeurs += GetStringForRequete ( row[info.NomChamp] )+",";
				}
			}
			if ( strValeurs.Length > 0 )
			{
				//Supprime la virgule en trop
				strRequete = strRequete.Substring(0, strRequete.Length-1);
				strValeurs = strValeurs.Substring(0, strValeurs.Length-1);
			}
			strRequete += ") values("+strValeurs+")";
			return m_connexion.RunStatement ( strRequete );
		}

		// /////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur DoUpdate ( DataRow row )
		{
			string strRequete = "update "+m_structure.NomTableInDb+" set ";
			string strSet = "";
			string strWhere = "";

			foreach ( CInfoChampTable champ in m_structure.Champs )
			{
				if ( !champ.IsId && !IsExclu(champ.NomChamp) )
					strSet+=champ.NomChamp+"="+GetStringForRequete(row[champ.NomChamp, System.Data.DataRowVersion.Current])+",";

				if ( !IsExclu (champ.NomChamp) )
					strWhere += champ.NomChamp+"="+GetStringForRequete(row[champ.NomChamp, DataRowVersion.Original])+" and ";
			}
			if ( strSet.Length > 0 )
			{
				//Supprime la virgule finale
				strSet = strSet.Substring(0, strSet.Length-1);
				//Supprime le and final
				strWhere = strWhere.Substring(0, strWhere.Length-5);
			}
			strRequete += strSet+" where "+strWhere;
			CResultAErreur result = m_connexion.RunStatement ( strRequete );
			if ( result && result.Data is int )
			{
                if ((int)result.Data == 0)//Pas de lignes mises à jour
                    throw new Exception(CObjetDonnee.GetMessageAccesConccurentiel(row));
					//throw new Exception(I.T("Another program has modified the data.|103"));
			}
			return result;
		}

		// /////////////////////////////////////////////////////////////////////////////
		public CResultAErreur DoDelete ( DataRow row )
		{
			string strRequete = "Delete from "+m_structure.NomTableInDb+" where ";
			string strWhere = "";
			foreach ( CInfoChampTable champ in m_structure.Champs )
			{
				strWhere += champ.NomChamp+"="+GetStringForRequete(row[champ.NomChamp, DataRowVersion.Original])+" and ";
			}
			if ( strWhere.Length > 0 )
			{
				//Supprime le and final
				strWhere = strWhere.Substring(0, strWhere.Length-5);
			}
			strRequete += strWhere;
			CResultAErreur result = m_connexion.RunStatement ( strRequete );
			if ( result && result.Data is int )
			{
				if ( (int)result.Data == 0 )//Pas de lignes mises à jour
                    throw new Exception ( CObjetDonnee.GetMessageAccesConccurentiel(row));
					//throw new Exception(I.T("Another program has modified the data.|103"));
			}
			return result;
		}
	
	}
}

