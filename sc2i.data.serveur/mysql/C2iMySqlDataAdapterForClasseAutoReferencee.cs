using System;
using System.Data;
using System.Data.Common;
using System.Data.MySqlClient;
using System.Collections;
using System.Reflection;

using sc2i.data;


namespace sc2i.data.serveur
{

	/// <summary>
	/// Obtient le schéma de la base
	/// </summary>
	public class C2iMySqlDataAdapterForClasseAutoReferencee : C2iDataAdapterForClasseAutoReferencee
	{

		/// /////////////////////////////////////////////////////////////////
		public C2iMySqlDataAdapterForClasseAutoReferencee(Type tp, IDbDataAdapter adapter)
			: base(tp, adapter)
		{
		}

		/// /////////////////////////////////////////////////////////////////
		protected override int ExecuteAdd(IDbCommand commande, DataRow row)
		{
			int nVal = 0; 
			if (DataAdapterUtilise is C2iMySqlDataAdapter)
				return ((C2iMySqlDataAdapter)DataAdapterUtilise).ExecuteCommande(commande, row);
			return nVal;
		}

		protected override int ExecuteDelete(IDbCommand commande, DataRow row)
		{
			int nVal = 0; 
			if (DataAdapterUtilise is C2iMySqlDataAdapter)
				return ((C2iMySqlDataAdapter)DataAdapterUtilise).ExecuteCommande(commande, row);
			return nVal;
		}

		protected override int ExecuteUpdate(IDbCommand commande, DataRow row)
		{
			int nVal = 0; 
			if (DataAdapterUtilise is C2iMySqlDataAdapter)
				return ((C2iMySqlDataAdapter)DataAdapterUtilise).ExecuteCommande(commande, row);
			return nVal;
		}
	}
}
