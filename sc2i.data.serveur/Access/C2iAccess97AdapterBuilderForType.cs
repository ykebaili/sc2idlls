using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data.serveur.Access;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Fabrique automatiquement les commandes pour un SqlDataAdapter
	/// </summary>
    public class CAccess97AdapterBuilderForType : IAdapterBuilder
	{

		protected Type m_type;
		protected COleDbDatabaseConnexion m_connexion;
        protected bool m_bAvecSynchro = false;
		protected Hashtable m_tblExclusions = new Hashtable();
		protected DataRowState m_etatsAPrendreEnCompte;
		/// <summary>
		/// </summary>
        public CAccess97AdapterBuilderForType(Type tp, COleDbDatabaseConnexion connexion)
		{
			m_type = tp;
			m_connexion = connexion;
            m_bAvecSynchro = connexion is IDatabaseConnexionSynchronisable;
            if (m_bAvecSynchro)
                m_bAvecSynchro &= ((IDatabaseConnexionSynchronisable)connexion).EnableLogSynchronisation;
		}

		////////////////////////////////////////////////////
		public virtual IDataAdapter GetNewAdapter ( DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[]champsExclus )
		{
			m_etatsAPrendreEnCompte = etatsAPrendreEnCompte;
			CStructureTable structure = CStructureTable.GetStructure(m_type);
			m_tblExclusions = new Hashtable();
			foreach ( string strChamp in champsExclus )
				m_tblExclusions[strChamp] = strChamp;
			IDbDataAdapter adapter = new OleDbDataAdapter();
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
				adapter.InsertCommand = new OleDbCommand ( "select 1", (OleDbConnection)m_connexion.GetConnexion(), (OleDbTransaction)m_connexion.Transaction  );
			}

			if ( (etatsAPrendreEnCompte & DataRowState.Deleted) != 0 )
				adapter.DeleteCommand = GetDeleteCommand ( structure );
			else
			{
				adapter.DeleteCommand = new OleDbCommand ( "select 1", (OleDbConnection)(OleDbConnection)m_connexion.GetConnexion(), (OleDbTransaction)m_connexion.Transaction   );
			}
			

			if ( (etatsAPrendreEnCompte & DataRowState.Modified) != 0 )
				adapter.UpdateCommand = GetUpdateCommand ( structure );
			else
			{
				adapter.UpdateCommand = new OleDbCommand ( "select 1", (OleDbConnection)m_connexion.GetConnexion(), (OleDbTransaction)m_connexion.Transaction   );
			}

            if (adapter is OleDbDataAdapter)
            {
			    ((OleDbDataAdapter)adapter).RowUpdating += new OleDbRowUpdatingEventHandler ( OnRowUpdating );
                ((OleDbDataAdapter)adapter).RowUpdated += new OleDbRowUpdatedEventHandler(OnRowUpdated);
            }
		
			return adapter;
		}

		////////////////////////////////////////////////////
		private void OnRowUpdating ( object sender, OleDbRowUpdatingEventArgs args )
		{
			if ( args.StatementType == StatementType.Delete && 
				(m_etatsAPrendreEnCompte & DataRowState.Deleted) == 0 )
				args.Status = UpdateStatus.SkipCurrentRow;
			
			if ( args.StatementType == StatementType.Insert && 
				(m_etatsAPrendreEnCompte & DataRowState.Added) == 0 )
				args.Status = UpdateStatus.SkipCurrentRow;

			if ( args.StatementType == StatementType.Update && 
				(m_etatsAPrendreEnCompte & DataRowState.Modified) == 0 )
				args.Status = UpdateStatus.SkipCurrentRow;
		}

        ////////////////////////////////////////////////////
        private void OnRowUpdated(object sender, OleDbRowUpdatedEventArgs args)
        {
            if (args.StatementType == StatementType.Delete || args.StatementType == StatementType.Insert)
            {
                CAccess97DataBaseConnexionSynchronisable cnx = m_connexion as CAccess97DataBaseConnexionSynchronisable;
                if (cnx != null)
                    cnx.RenseignerPourSynchro(args.Row, args.StatementType);
            }
        }

		////////////////////////////////////////////////////
		public virtual OleDbCommand GetSelectCommand ( CStructureTable structure )
		{
			string strReq = "";
			bool bHasBlob = false;
			foreach ( CInfoChampTable info in structure.Champs )
			{
				if ( info.TypeDonnee==typeof(CDonneeBinaireInRow) || info.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					bHasBlob = true;
				else
					strReq += "["+info.NomChamp+"],";
			}
			if ( bHasBlob && strReq.Length > 0 )
				strReq = "select "+strReq.Substring(0, strReq.Length-1)+" from ["+structure.NomTableInDb+"]";
			else
				strReq = "select * from ["+structure.NomTableInDb+"]";
			return new OleDbCommand ( strReq, (OleDbConnection)m_connexion.GetConnexion(), (OleDbTransaction)m_connexion.Transaction );
		}

		////////////////////////////////////////////////////
		public virtual OleDbCommand GetDeleteCommand ( CStructureTable structure )
		{
			string strReq = "delete from ["+structure.NomTableInDb+"]";
			OleDbCommand command = new OleDbCommand();
			command.Connection = (OleDbConnection)m_connexion.GetConnexion();
			command.Transaction = (OleDbTransaction)m_connexion.Transaction;
			AddWhereClauseToDeleteAndUpdate ( ref strReq, command, structure.Champs );
			command.CommandText = strReq;
			return command;
		}

		////////////////////////////////////////////////////
		public virtual OleDbCommand GetInsertCommand ( CStructureTable structure, bool bDisableIdAuto, IDataAdapter adapter )
		{
			string strReq = "insert into ["+structure.NomTableInDb+"](";
			string strValues = "(";
			foreach ( CInfoChampTable champ in structure.Champs )
			{
				if ( (!champ.IsAutoId || bDisableIdAuto) && m_tblExclusions[champ.NomChamp] == null)
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					{
						strReq += "["+champ.NomChamp+"],";
						strValues += "?,";//GetNomParametreFor(champ, DataRowVersion.Current)+",";
					}
			}
			strReq = strReq.Substring(0, strReq.Length-1)+")";
			strValues = strValues.Substring(0, strValues.Length-1)+")";
			strReq += " values "+strValues;
			
			if ( structure.HasChampIdAuto && !bDisableIdAuto)
			{
                C2iDataAdapterForClasseAutoReferencee adapteurAutoRef = adapter as C2iDataAdapterForClasseAutoReferencee;
                if (adapteurAutoRef != null)
                {
                    adapteurAutoRef.RowInserted += new C2iDataAdapterForClasseAutoReferencee.RowInsertedEventHandler(OnRowAIdAutoInsertedGeneric);
                    if (m_bAvecSynchro)
                        adapteurAutoRef.RowInserted += new C2iDataAdapterForClasseAutoReferencee.RowInsertedEventHandler(OnRowAIdAutoInserted);
                }
                else
                    ((OleDbDataAdapter)adapter).RowUpdated += new OleDbRowUpdatedEventHandler(OnRowAIdAutoUpdated);
			}
			OleDbCommand command = new OleDbCommand(strReq, (OleDbConnection)m_connexion.GetConnexion(), (OleDbTransaction)m_connexion.Transaction );
			command.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
			//Ajoute les paramètres
			foreach ( CInfoChampTable champ in structure.Champs )
			{
				if ( (!champ.IsAutoId || bDisableIdAuto) && m_tblExclusions[champ.NomChamp] == null)
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					{
						command.Parameters.Add ( GetParametreFor ( champ, DataRowVersion.Current, ParameterDirection.Input ) );
					}
			}
			return command;
		}

		////////////////////////////////////////////////////
		public OleDbCommand GetUpdateCommand( CStructureTable structure )
		{
			string strReq = "update ["+structure.NomTableInDb+"] set ";
			ArrayList listeChampsUpdate = new ArrayList();
			foreach ( CInfoChampTable champ in structure.Champs )
			{
				if ( !champ.IsId && m_tblExclusions[champ.NomChamp] == null && !champ.ExclureFormStandardUpdate )
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
					{
						strReq += "["+champ.NomChamp+"]="+GetNomParametreFor(champ, DataRowVersion.Current)+",";
						listeChampsUpdate.Add ( champ );
					}
			}
			strReq = strReq.Substring(0, strReq.Length-1)+" ";
			OleDbCommand command = new OleDbCommand();
			command.Connection = (OleDbConnection)m_connexion.GetConnexion();
			command.Transaction = (OleDbTransaction)m_connexion.Transaction;
			//Ajoute les paramètres nouvelle version
			foreach ( CInfoChampTable champ in listeChampsUpdate )
			{
				if ( m_tblExclusions[champ.NomChamp] == null )
				{
					if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) )
						command.Parameters.Add ( GetParametreFor ( champ, DataRowVersion.Current, ParameterDirection.Input ) );
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
		protected virtual OleDbParameter GetParametreFor ( CInfoChampTable champ, DataRowVersion version, ParameterDirection direction )
		{
			OleDbParameter parametre = new OleDbParameter ( GetNomParametreFor(champ, version), COleDbTypeConvertor.GetTypeOleDbFromType(champ.TypeDonnee));
			if ( (parametre.DbType == DbType.String || parametre.DbType == DbType.AnsiString)&& !champ.IsLongString )
				parametre.Size = champ.Longueur;
			parametre.Direction = direction;
			parametre.SourceColumn = champ.NomChamp;
			parametre.SourceVersion = version;
			return parametre;
		}

		////////////////////////////////////////////////////
		protected virtual string GetNomParametreFor ( CInfoChampTable champ, DataRowVersion version )
		{
			string strNom = "@"+champ.NomChamp;
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
		protected virtual void AddWhereClauseToDeleteAndUpdate ( ref string strReq, OleDbCommand command, CInfoChampTable[] champs )
		{
			strReq += " where ";
			foreach ( CInfoChampTable champ in champs )
			{
				if ( champ.TypeDonnee!=typeof(CDonneeBinaireInRow) && 
					!champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) &&
					!champ.IsLongString )
				{
					//if ( champ.NullAuthorized || champ.TypeDonnee==typeof(object))
					strReq += "((["+champ.NomChamp+"] is null and "+
						GetNomParametreFor(champ,DataRowVersion.Original)+" is null) or ["+
						champ.NomChamp+"]="+GetNomParametreFor(champ, DataRowVersion.Original)+") and ";
					//else
					//	strReq += champ.NomChamp+"="+GetNomParametreFor(champ, DataRowVersion.Original)+" and ";
					OleDbParameter parametre = GetParametreFor ( champ, DataRowVersion.Original, ParameterDirection.Input );
					command.Parameters.Add ( parametre );
				}
			}
			if ( champs.Length > 0 )
				strReq = strReq.Substring(0, strReq.Length-" and ".Length);
		}

        /// ////////////////////////////////////////////////////
        private void OnRowAIdAutoInserted(object sender, DataRow row)
        {
            if (m_bAvecSynchro)
                ((CAccess97DataBaseConnexionSynchronisable)m_connexion).RenseignerPourSynchro(row, StatementType.Insert);
        }

		/// ////////////////////////////////////////////////////
		private void OnRowAIdAutoUpdated(object sender, OleDbRowUpdatedEventArgs e)
		{
            if (e.StatementType == StatementType.Insert)
            {
                OnRowAIdAutoInsertedGeneric(sender, e.Row);
                if (m_bAvecSynchro)
                    ((CAccess97DataBaseConnexionSynchronisable)m_connexion).RenseignerPourSynchro(e.Row, e.StatementType);
            }
		}

        /// ////////////////////////////////////////////////////
        private void OnRowAIdAutoInsertedGeneric(object sender, DataRow row)
        {
            string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(row.Table.TableName);
            string strChampId = row.Table.PrimaryKey[0].ColumnName;
            CResultAErreur result = m_connexion.ExecuteScalar("select @@IDENTITY");
            /*"select max([" + strChampId + "]) from " +
                "[" + strNomTableInDb + "]");*/
            if (!result)
            {
                result.EmpileErreur(I.T("Access97 Recovery IdAuto Error|101"));
                throw new CExceptionErreur(result.Erreur);
            }
            row[strChampId] = (int)result.Data;
        }

	}
}
