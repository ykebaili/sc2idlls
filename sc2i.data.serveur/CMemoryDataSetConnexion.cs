using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;
using sc2i.multitiers.server;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CMemoryDataSetConnexion.
	/// </summary>
	public abstract class CMemoryDataSetConnexion : C2iObjetServeur, IDatabaseConnexion
	{
        private string m_strPrefixeTables = "";
		private ArrayList m_listeDataSetTransaction = new ArrayList();
        private object m_lockerAdapter = new object();

		/// //////////////////////////////////////////////////
		public CMemoryDataSetConnexion( int nIdSession )
			:base ( nIdSession )
		{
		}

		public IDataBaseCreator GetDataBaseCreator()
		{
			return null;
		}

        /// //////////////////////////////////////////////////
        public bool IsInTrans()
        {
            return m_listeDataSetTransaction.Count > 0;
        }

        /// //////////////////////////////////////////////////
        public string PrefixeTables
        {
            get
            {
                return m_strPrefixeTables;
            }
            set
            {
                m_strPrefixeTables = value;
            }
        }

		/// //////////////////////////////////////////////////
		public void Dispose()
		{
		}

		/// //////////////////////////////////////////////////
		public abstract string ConnexionString {get;set;}
		
		protected abstract DataSet DataSetProtected{get;set;}

		/// //////////////////////////////////////////////////
		public virtual DataSet DataSet
		{
			get
			{
				return DataSetProtected;
			}
		}

		/// <summary>
		/// Retourne un IDataAdapter pour la lecture seulement
		/// </summary>
		/// <param name="strRequete">
		/// Requete SQL
		/// </param>
		/// <returns></returns>
		public IDataAdapter GetSimpleReadAdapter ( string strRequete )
		{
			return new CMemoryDataSetAdapter(this, strRequete,null);
		}

		/// <summary>
		/// Retourne un IDataAdapter pour la lecture seulement avec un filtre
		/// </summary>
		/// <param name="strRequete">Requête SQL</param>
		/// <param name="filtre">Filtre à appliquer</param>
		/// <returns></returns>
		public IDataAdapter GetSimpleReadAdapter ( string strRequete, CFiltreData filtre )
		{
			return new CMemoryDataSetAdapter(this, strRequete, filtre);
		}

		/// <summary>
		/// Retourne un IDataAdapter pour lire une table complète
		/// </summary>
		/// <param name="strTable">Nom de la table à lire</param>
		/// <returns></returns>
		public IDataAdapter GetTableAdapter ( string strNomTableInDb )
		{
			return new CMemoryDataSetAdapter(this, "select * from "+strNomTableInDb,null);
		}

		/// <summary>
		/// Retourne un IDataAdapter pour mettre à jour une table
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		public IDataAdapter GetNewAdapterForUpdate ( string strNomTableInDb )
		{
			return null;
		}

		/// <summary>
		/// Retourne un IDataAdapter pour un type possédant les attributs [Table][Relation] et [Champ]
		/// </summary>
		/// <param name="strTable"></param>
		/// <returns></returns>
		public IDataAdapter GetNewAdapterForType ( Type tp, DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[] champsExclus )
		{
			return null;
		}

		/// <summary>
		/// Execute une requete ne renvoyant pas de ligne
		/// A utiliser uniquement pour modifier la structure de la base
		/// </summary>
		public CResultAErreur RunStatement ( String strStatement)
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("'RunStatement' function not supported by @1|173",GetType().ToString()));
			return result;
		}

		/// <summary>
		/// Execute une requete ne renvoyant pas de ligne
		/// A utiliser uniquement pour modifier la structure de la base
		/// </summary>
		public CResultAErreur ExecuteScalar ( String strStatement)
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("'ExecuteScalar' function not supported by @1|174",GetType().ToString()));
			return result;
		}

		/// <summary>
		/// Execute une requete ne renvoyant pas de ligne
		/// A utiliser uniquement pour modifier la structure de la base
		/// </summary>
		public CResultAErreur ExecuteScalar ( string strSql, string strTable, CFiltreData filtre)
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("'ExecuteScalar' function not supported by @1|174", GetType().ToString()));
			return result;
		}

		/// <summary>
		/// Modifie une table avant l'écriture (modif des num synchro par exemple)
		/// </summary>
		/// <param name="table"></param>
		public void PrepareTableToWriteDatabase ( DataTable table )
		{
		}

		/// <summary>
		/// Active ou désactive les identifiants automatiques
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="bDesactiver"></param>
		public void DesactiverIdAuto ( string strNomTable, bool bDesactiver )
		{
		}

		/// <summary>
		/// Active ou désactive les contraintes d'intégrité référentielle
		/// A UTILISER AVEC PRECAUTION
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <param name="bDesactiver"></param>
		public void DesactiverContraintes ( bool bDesactiver )
		{
			DataSet.EnforceConstraints = bDesactiver;
		}


		/// <summary>
		/// Retourne true si le système arrive à se connecter à la base demandée
		/// </summary>
		/// <returns></returns>
		public CResultAErreur IsConnexionValide()
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( DataSetProtected != null )
				{
					return result;
				}
				result.EmpileErreur(I.T("Erreur in the dataset|30007"));
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				return result;
			}
		}


		/////////////////////////////////////////////////////////
		/// <summary>
		/// Charge un blob à partir d'une ligne d'une table. Le resultat est 
		/// placé dans le Data du CResultAErreur
		/// </summary>
		public CResultAErreur ReadBlob ( string strTable, string strChamp, CFiltreData filtre )
		{
			CResultAErreur result = CResultAErreur.False;
			return result;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Sauve un blob dans une ligne d'une base de données
		/// </summary>
		public CResultAErreur SaveBlob ( string strTable, string strChamp, CFiltreData filtre, byte[] data )
		{
			CResultAErreur result = CResultAErreur.False;
			return result;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne les noms des tables de la base de données
		/// </summary>
		public string[] TablesNames
		{
			get
			{
				if ( DataSet == null )
					return new String[0];
				string[] strTables = new string[DataSet.Tables.Count];
				for ( int n = 0; n < DataSet.Tables.Count; n++ )
					strTables[n] = DataSet.Tables[n].TableName;
				return strTables;
			}
		}
		/////////////////////////////////////////////////////////
		public string[] GetColonnesDeTable(string strTable)
		{
			DataTable table = DataSet.Tables[strTable];
			List<string> lst = new List<string>();
			if (table != null)
			{
				foreach (DataColumn col in table.Columns)
					lst.Add(col.ColumnName);
			}
			return lst.ToArray();
		}

		/////////////////////////////////////////////////////////
		public CResultAErreur BeginTrans()
		{
			return CResultAErreur.True;
			/*
			m_listeDataSetTransaction.Add ( DataSetProtected.Clone() );
			return CResultAErreur.True;*/
		}

		/////////////////////////////////////////////////////////
		public CResultAErreur BeginTrans( System.Data.IsolationLevel isolationLevel )
		{
			return CResultAErreur.True;
			//return BeginTrans();
		}
		
		/// //////////////////////////////////////////////////
		public event OnCommitTransEventHandler OnCommitTrans;
		/////////////////////////////////////////////////////////
		public CResultAErreur CommitTrans()
		{
			CResultAErreur result =  CResultAErreur.True;
			if ( OnCommitTrans != null )
				OnCommitTrans();
			return result;//Suite à des pbs, la fonction de transaction est désactivés
			/*
			if ( m_listeDataSetTransaction.Count == 0 )
				result.EmpileErreur("Appel de commit trans sur une connexion qui n'est pas en transaction");
			else
				m_listeDataSetTransaction.RemoveAt ( m_listeDataSetTransaction.Count-1 );
			if ( result && OnCommitTrans != null )
				result = OnCommitTrans();
			return result;*/
		}

		/////////////////////////////////////////////////////////
		public CResultAErreur RollbackTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
			/*
			if ( m_listeDataSetTransaction.Count == 0 )
				result.EmpileErreur("Appel de Rollbacktrans sur une connexion qui n'est pas en transaction");
			else
			{
				DataSetProtected = (DataSet)m_listeDataSetTransaction[m_listeDataSetTransaction.Count-1];
				m_listeDataSetTransaction.RemoveAt(m_listeDataSetTransaction.Count-1);
			}
			return result;*/
		}

		/////////////////////////////////////////////////////////
		public bool AccepteTransactionsImbriquees
		{
			get
			{
				return false;
			}
		}

		/////////////////////////////////////////////////////////
		public CResultAErreur SetValeurChamp ( string strNomTableInDb, string[] strChamps, object[] valeurs, CFiltreData filtre )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(filtre);
				DataRow[] rows = DataSet.Tables[strNomTableInDb].Select(strFiltre);
				foreach ( DataRow row in rows )
				{
					for ( int n = 0; n < strChamps.Length; n++ )
						row[strChamps[n]] = valeurs[n];
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error SetValeurChamp(@1)|175",strNomTableInDb));
			}
			return result;
		}

		/////////////////////////////////////////////////////////
		public string GetStringForRequete ( object valeur )
		{
			return CFormatteurFiltreDataToStringDataTable.GetStringFor ( valeur );
		}


		/////////////////////////////////////////////////////////
		public int CountRecords ( string strNomTableInDb, CFiltreData filtre )
		{
			throw new Exception(I.T("Count record not implemented|176"));
		}

		/////////////////////////////////////////////////////////
		public string GetNomParametre ( string strNom )
		{
			return "@"+strNom;
		}

		/////////////////////////////////////////////////////////
		public string GetNomTableForRequete ( string strNom )
		{
			return strNom;
		}

		/////////////////////////////////////////////////////////
		public IDataAdapter GetAdapterForRequete ( string strRequete, object[] parametres )
		{
			return null;
			//Fonction non supportée
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public CResultAErreur ExecuteRequeteComplexe ( C2iChampDeRequete[] champs, CArbreTable arbreTables, CFiltreData filtre )
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Function not implemented|177"));
			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public int CommandTimeOut

		{
			get
			{
				return 0;
			}
			set
			{
			}
		}


        //-------------------------------------------------------------------------
        /// <summary>
        /// Permet de vérouiller l'appel d'un seul DataAdapter à la fois par connexion
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public void FillAdapter(IDataAdapter adapter, DataSet ds)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(ds);
            }
        }
        //--------------------------------------------------------------------------
        public void FillAdapter(DbDataAdapter adapter, DataTable dt)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(dt);
            }
        }
        //--------------------------------------------------------------------------
        public void FillAdapter(DbDataAdapter adapter, DataSet ds, int startRecord, int maxRecords, string srcTable)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(ds, startRecord, maxRecords, srcTable);
            }
        }

        //--------------------------------------------------------------------------
        public void FillAdapter(IDataAdapterARemplissagePartiel adapter, DataSet ds, int startRecord, int maxRecords, string srcTable)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(ds, startRecord, maxRecords, srcTable);
            }
        }

    }
}
