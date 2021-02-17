using System;
using System.Data;
using System.Collections;
using System.Reflection;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un SqlDataAdapter
	/// </summary>
    public class C2iDbAdapterBuilderForType : IAdapterBuilder
	{

		protected Type m_type;
		protected C2iDbDatabaseConnexion m_connexion;
		protected Hashtable m_tblExclusions = new Hashtable();
		protected DataRowState m_etatsAPrendreEnCompte;
		/// <summary>
		/// </summary>
		public C2iDbAdapterBuilderForType( Type tp, C2iDbDatabaseConnexion connexion )
		{
			m_type = tp;
			m_connexion = connexion;
		}

		////////////////////////////////////////////////////
		public DataRowState EtatsAPrendreEnCompte
		{
			get
			{
				return m_etatsAPrendreEnCompte;
			}
		}

		////////////////////////////////////////////////////
		public virtual IDataAdapter GetNewAdapter ( DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[]champsExclus )
		{
			CStructureTable structure = CStructureTable.GetStructure(m_type);
			m_etatsAPrendreEnCompte = etatsAPrendreEnCompte;
			m_tblExclusions = new Hashtable();
			foreach ( string strChamp in champsExclus )
				m_tblExclusions[strChamp] = strChamp;
			IDbDataAdapter adapter = (IDbDataAdapter)m_connexion.GetTableAdapter(structure.NomTableInDb);

			if ( typeof(IObjetDonneeAutoReference).IsAssignableFrom(m_type) )
			{
				adapter = new C2iDataAdapterForClasseAutoReferencee ( m_type, adapter );
			}
			adapter.TableMappings.Add("Table", structure.NomTable );
			adapter.SelectCommand = GetSelectCommand ( structure );

			if ( (etatsAPrendreEnCompte & DataRowState.Added) != 0 )
				adapter.InsertCommand = GetInsertCommand ( structure, bDisableIdAuto, adapter );
			else
			{
				adapter.InsertCommand = m_connexion.GetConnexion().CreateCommand();
				adapter.InsertCommand.CommandText = GetCommandQuiFaitRien();
				adapter.InsertCommand.Transaction = m_connexion.Transaction;
			}

			if ( (etatsAPrendreEnCompte & DataRowState.Deleted) != 0 )
				adapter.DeleteCommand = GetDeleteCommand ( structure );
			else
			{
				adapter.DeleteCommand = m_connexion.GetConnexion().CreateCommand();
				adapter.DeleteCommand.CommandText = GetCommandQuiFaitRien();
				adapter.DeleteCommand.Transaction = m_connexion.Transaction;
			}
			
			if ( (etatsAPrendreEnCompte & DataRowState.Modified) != 0 )
				adapter.UpdateCommand = GetUpdateCommand ( structure );
			else
			{
				adapter.UpdateCommand = m_connexion.GetConnexion().CreateCommand();
				adapter.UpdateCommand.CommandText = GetCommandQuiFaitRien();
				adapter.UpdateCommand.Transaction = m_connexion.Transaction;
			}
		
			return adapter;
		}

		////////////////////////////////////////////////////
		protected virtual string GetCommandQuiFaitRien ( )
		{
			return "Select 1";
		}


		////////////////////////////////////////////////////
		public virtual IDbCommand GetSelectCommand ( CStructureTable structure )
		{
			string strReq = "";
			bool bHasBlob = false;
			foreach ( CInfoChampTable info in structure.Champs )
			{
				if ( info.TypeDonnee==typeof(CDonneeBinaireInRow) || info.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					bHasBlob = true;
				else
					strReq += info.NomChamp+",";
			}
			if ( bHasBlob && strReq.Length > 0 )
				strReq = "select "+strReq.Substring(0, strReq.Length-1)+" from "+structure.NomTableInDb;
			else
				strReq = "select * from "+structure.NomTableInDb;
			IDbCommand command = m_connexion.GetConnexion().CreateCommand();
			command.CommandText = strReq;
			command.Transaction = m_connexion.Transaction;
			return command;

		}

		////////////////////////////////////////////////////
		public virtual IDbCommand GetDeleteCommand ( CStructureTable structure )
		{
			string strReq = "delete from "+structure.NomTableInDb;
			IDbCommand command = m_connexion.GetConnexion().CreateCommand();
			command.Transaction = m_connexion.Transaction;
			AddWhereClauseToDeleteAndUpdate ( ref strReq, command, structure.Champs );
			command.CommandText = strReq;
			return command;
		}

		////////////////////////////////////////////////////
		public virtual IDbCommand GetInsertCommand ( CStructureTable structure, bool bDisableIdAuto, IDbDataAdapter adapter )
		{
			//Stef 2804 : N'exclue pas les champs exclus de l'insertion !
			string strReq = "insert into "+structure.NomTableInDb+"(";
			string strValues = "(";
			foreach ( CInfoChampTable champ in structure.Champs )
			{
				if ( (!champ.IsAutoId || bDisableIdAuto) )
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					{
						strReq += champ.NomChamp+",";
						strValues += GetNomParametreFor(champ, DataRowVersion.Current)+",";
					}
			}
			strReq = strReq.Substring(0, strReq.Length-1)+")";
			strValues = strValues.Substring(0, strValues.Length-1)+")";
			strReq += " values "+strValues;
			
			if ( structure.HasChampIdAuto )
			{
				//Récupère l'identité
				strReq += ";SELECT "+structure.ChampsId[0].NomChamp+
					" FROM "+structure.NomTableInDb+" WHERE ("+structure.ChampsId[0].NomChamp+" = @@IDENTITY)";
			}
			IDbCommand command = m_connexion.GetConnexion().CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandType = CommandType.Text;
			command.CommandText = strReq;
			command.Transaction = m_connexion.Transaction;
			command.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
			//Ajoute les paramètres
			foreach ( CInfoChampTable champ in structure.Champs )
			{
				if ( (!champ.IsAutoId || bDisableIdAuto) )
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					{
						command.Parameters.Add ( GetParametreFor ( command, champ, DataRowVersion.Current, ParameterDirection.Input ) );
					}
			}
			return command;
		}

		////////////////////////////////////////////////////
		public virtual IDbCommand GetUpdateCommand( CStructureTable structure )
		{
			string strReq = "update "+structure.NomTableInDb+" set ";
			ArrayList listeChampsUpdate = new ArrayList();
			foreach ( CInfoChampTable champ in structure.Champs )
			{
				if ( !champ.IsId && m_tblExclusions[champ.NomChamp] == null && !champ.ExclureFormStandardUpdate )
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					{
						strReq += champ.NomChamp+"="+GetNomParametreFor(champ, DataRowVersion.Current)+",";
						listeChampsUpdate.Add ( champ );
					}
			}
			strReq = strReq.Substring(0, strReq.Length-1)+" ";
			IDbCommand command = m_connexion.GetConnexion().CreateCommand();
			command.Transaction = m_connexion.Transaction;
			//Ajoute les paramètres nouvelle version
			foreach ( CInfoChampTable champ in listeChampsUpdate )
			{
				if ( m_tblExclusions[champ.NomChamp] == null )
				{
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
						command.Parameters.Add ( GetParametreFor ( command, champ, DataRowVersion.Current, ParameterDirection.Input ) );
				}
			}
			
			//La mise à jour doit tenir compte des id !!!!
			foreach ( CInfoChampTable champ in structure.ChampsId )
				listeChampsUpdate.Add ( champ );
			AddWhereClauseToDeleteAndUpdate ( ref strReq, command, (CInfoChampTable[])listeChampsUpdate.ToArray(typeof(CInfoChampTable)));
			command.CommandText = strReq;
			return command;
		}

		////////////////////////////////////////////////////
		protected virtual IDbDataParameter GetParametreFor ( IDbCommand commande, CInfoChampTable champ, DataRowVersion version, ParameterDirection direction )
		{
			DbType dbType = m_connexion.GetDbType(champ.TypeDonnee);
			IDbDataParameter parametre = commande.CreateParameter();
			parametre.ParameterName = GetNomParametreFor(champ, version);
			parametre.DbType = m_connexion.GetDbType ( champ.TypeDonnee );
			if ( champ.TypeDonnee == typeof(string) )
				parametre.Size = champ.Longueur;
			parametre.Direction = direction;
			parametre.SourceColumn = champ.NomChamp;
			parametre.SourceVersion = version;
			return parametre;
		}

		////////////////////////////////////////////////////
		protected virtual string GetNomParametreFor ( CInfoChampTable champ, DataRowVersion version )
		{
			string strNom = m_connexion.GetNomParametre(champ.NomChamp);
			switch ( version )
			{
				case DataRowVersion.Current :
					strNom+="_NEW";
					break;
				case DataRowVersion.Original : 
					strNom += "_OLD";
					break;
				default :
					strNom += (int)version;
					break;
			}
			return strNom;
		}

		////////////////////////////////////////////////////
		protected virtual void AddWhereClauseToDeleteAndUpdate ( ref string strReq, IDbCommand command, CInfoChampTable[] champs )
		{
			strReq += " where ";
			foreach ( CInfoChampTable champ in champs )
			{
				if ( m_tblExclusions[champ.NomChamp] == null )
				{
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && 
						!champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) &&
						!champ.IsLongString )
					{
						if ( champ.NullAuthorized || champ.TypeDonnee==typeof(object))
							strReq += "(("+champ.NomChamp+" is null and "+
								GetNomParametreFor(champ,DataRowVersion.Original)+" is null) or "+
								champ.NomChamp+"="+GetNomParametreFor(champ, DataRowVersion.Original)+") and ";
						else
							strReq += champ.NomChamp+"="+GetNomParametreFor(champ, DataRowVersion.Original)+" and ";
						IDbDataParameter parametre = GetParametreFor ( command, champ, DataRowVersion.Original, ParameterDirection.Input );
						command.Parameters.Add ( parametre );
					}
				}
			}
			if ( champs.Length > 0 )
				strReq = strReq.Substring(0, strReq.Length-" and ".Length);
		}



	}
}
