using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un SqlDataAdapter
	/// </summary>
    public class C2iSqlAdapterBuilderForType : C2iDbAdapterBuilderForType
	{
		public C2iSqlAdapterBuilderForType( Type tp, CSqlDatabaseConnexion connexion )
			:base ( tp, connexion )
		{
		}

        protected override IDbDataParameter GetParametreFor(IDbCommand commande, CInfoChampTable champ, DataRowVersion version, ParameterDirection direction)
        {
            IDbDataParameter parametre = base.GetParametreFor(commande, champ, version, direction);
            if (champ.IsLongString)
                parametre.Size = -1;

            return parametre;
        }

	}
}
