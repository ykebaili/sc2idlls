using System;
using System.Data;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un IDbDataAdapter
	/// </summary>
    public abstract class C2iDbAdapterBuilder : IAdapterBuilder
	{

		protected string m_strNomTableInDb;
		protected C2iDbDatabaseConnexion m_connexion;
		/// <summary>
		/// </summary>
		public C2iDbAdapterBuilder( string strNomTableInDb, C2iDbDatabaseConnexion connexion )
		{
			m_strNomTableInDb = strNomTableInDb;
			m_connexion = connexion;
		}

		////////////////////////////////////////////////////
		protected virtual string GetCommandQuiFaitRien ( )
		{
			return "Select 1";
		}

		////////////////////////////////////////////////////
		public virtual IDataAdapter GetNewAdapter( DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[] strExclusions )
		{
			IDbDataAdapter adapter = (IDbDataAdapter)m_connexion.GetTableAdapter(m_strNomTableInDb);
			if ( m_connexion.IsInTrans() )
				adapter.SelectCommand.Transaction = m_connexion.Transaction;
			string strNomTableInContexte = m_connexion.GetNomTableInContexteFromNomTableInDb(m_strNomTableInDb);
			if (strNomTableInContexte == null)
				strNomTableInContexte = m_strNomTableInDb;
			adapter.TableMappings.Add("Table", strNomTableInContexte);
			DataSet ds = new DataSet();
			adapter.FillSchema ( ds, SchemaType.Mapped );
			DataTable table = ds.Tables[strNomTableInContexte];
			if (table == null)
			{
				table = ds.Tables[m_strNomTableInDb];
				if (table != null)
					table.TableName = strNomTableInContexte;
			}

			if ( (etatsAPrendreEnCompte & DataRowState.Added) != 0 )
				adapter.InsertCommand = GetInsertCommand ( table, adapter, bDisableIdAuto );
			else
			{
				adapter.InsertCommand = m_connexion.GetConnexion().CreateCommand();
				adapter.InsertCommand.CommandType = CommandType.Text;
				adapter.InsertCommand.CommandText = GetCommandQuiFaitRien();
				adapter.InsertCommand.Transaction = m_connexion.Transaction;
			}

			if ( (etatsAPrendreEnCompte & DataRowState.Deleted) != 0 )
				adapter.DeleteCommand = GetDeleteCommand ( table, adapter );
			else
			{
				adapter.DeleteCommand = m_connexion.GetConnexion().CreateCommand();
				adapter.DeleteCommand.CommandType = CommandType.Text;
				adapter.DeleteCommand.CommandText = GetCommandQuiFaitRien();
				adapter.DeleteCommand.Transaction = m_connexion.Transaction;
			}
			
			if ( (etatsAPrendreEnCompte & DataRowState.Modified) != 0 )
				adapter.UpdateCommand = GetUpdateCommand ( table, adapter );
			else
			{
				adapter.UpdateCommand = m_connexion.GetConnexion().CreateCommand();
				adapter.UpdateCommand.CommandType = CommandType.Text;
				adapter.UpdateCommand.CommandText = GetCommandQuiFaitRien();
				adapter.UpdateCommand.Transaction = m_connexion.Transaction;
			}
			return adapter;
		}

		////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la commande pour inserer dans une table
		/// </summary>
		/// <param name="table">le nom de la table est le nom dans le contexte et non les
		/// nom dans la base de données
		/// </param>
		/// <param name="adapter"></param>
		/// <param name="bDisableIdAuto"></param>
		/// <returns></returns>
		public abstract IDbCommand GetInsertCommand ( DataTable table, IDbDataAdapter adapter, bool bDisableIdAuto );


		////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le nom pour mettre à jour la table
		/// </summary>
		/// <param name="table">Le nom de la table est le nom de la table dans le contexte
		/// de données et non le nom dans la base de données</param>
		/// <param name="adapter"></param>
		/// <returns></returns>
		public abstract IDbCommand GetUpdateCommand(DataTable table, IDbDataAdapter adapter);

		////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la commande permettant de supprimer dans la table
		/// </summary>
		/// <param name="table">Le nom de la table est le nom dans le contexte de données
		/// et non le nom dans la base de données</param>
		/// <param name="adapter"></param>
		/// <returns></returns>
		public abstract IDbCommand GetDeleteCommand(DataTable table, IDbDataAdapter adapter);
	}
}
