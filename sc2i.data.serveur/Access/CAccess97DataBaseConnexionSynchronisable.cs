﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data.serveur.Access
{
    public class CAccess97DataBaseConnexionSynchronisable : CAccess97DatabaseConnexion, IDatabaseConnexionSynchronisable
    {
        private const string c_cleSyncSession = "SC2I_SYNC_SESSION";
		private const string c_cleLastVersionVueDeBasePrincipale = "LAST_SYNCID_FROM_MAIN";
		private const string c_cleLastVersionInBasePrincipale = "LAST_SYNCID_PUT_IN_MAIN";

		private int m_nIdSyncSession = 0;
		private bool m_bIsSyncSessionVerouillee = false;
		private int m_nIdLastVersionVueDeBasePrincipale = 0;
		private int m_nIdLastVersionPutInBasePrincipale = 0;

		/// <summary>
		/// Si faux, se comporte comme une connexion non synchronisable
		/// </summary>
		private bool m_bEnableLogSyncrhonisation = true;

		/// /////////////////////////////////////////////////////
        public CAccess97DataBaseConnexionSynchronisable(int nIdSession)
			: base(nIdSession)
		{
		}

		public override IDataBaseCreator GetDataBaseCreator()
		{
			return (IDataBaseCreator)new CAccessDataBaseCreatorSynchronisable(this);
		}


		////////////////////////////////////////////////////////
		/// <summary>
		/// Active ou désactive l'enregistrement des informations pour synchronisation
		/// </summary>
		public bool EnableLogSynchronisation
		{
			get
			{
				return m_bEnableLogSyncrhonisation;
			}
			set
			{
				m_bEnableLogSyncrhonisation = value;
			}
		}

		////////////////////////////////////////////////////////
		public int IdSyncSession
		{
			get
			{
				if (!m_bIsSyncSessionVerouillee)
					m_nIdSyncSession = (int)new CDatabaseRegistre(this).GetValeurLong(c_cleSyncSession, -1);

				return m_nIdSyncSession;
			}
		}

		////////////////////////////////////////////////////////
		///garantie que l'id de synchro de cet objet ne sera pas modifié
		///Il peut cependant être modifié dans la base par un autre process de
		///synchronisation !
		public void LockSyncSessionLocalTo(int nIdSyncSession)
		{
			m_nIdSyncSession = nIdSyncSession;
			m_bIsSyncSessionVerouillee = true;
		}

		////////////////////////////////////////////////////////
		///Dévérouille l'id de synchro
		public void UnlockSyncSessionLocal()
		{
			m_bIsSyncSessionVerouillee = false;
		}

		////////////////////////////////////////////////////////
		public void IncrementeSyncSession()
		{
			lock (this)
			{
				int nIdSyncSession = IdSyncSession;
				nIdSyncSession++;
				new CDatabaseRegistre(this).SetValeur(c_cleSyncSession, nIdSyncSession.ToString());
				m_nIdSyncSession = nIdSyncSession;
			}
		}

		////////////////////////////////////////////////////////
		public void SetSyncSession(int nVersion)
		{
			int nIdSyncSession = IdSyncSession;
			if (nIdSyncSession < nVersion)
			{
				new CDatabaseRegistre(this).SetValeur(c_cleSyncSession, nVersion.ToString());
				m_nIdSyncSession = nVersion;
			}
		}

        ////////////////////////////////////////////////////////
        private void RefreshValeursSyncSession()
        {
            m_nIdLastVersionVueDeBasePrincipale = (int)new CDatabaseRegistre(this).GetValeurLong(c_cleLastVersionVueDeBasePrincipale, -1);
            m_nIdLastVersionPutInBasePrincipale = (int)new CDatabaseRegistre(this).GetValeurLong(c_cleLastVersionInBasePrincipale, -1);
            m_nIdSyncSession = (int)new CDatabaseRegistre(this).GetValeurLong(c_cleSyncSession, -1);
        }

        public override CResultAErreur RollbackTrans()
        {
            CResultAErreur result =  base.RollbackTrans();
            RefreshValeursSyncSession();
            return result;
        }

		////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'id de dernière synchro depuis la base principale
		/// </summary>
		public int LastSyncIdVueDeBasePrincipale
		{
			get
			{
				if (!IsInTrans())
					m_nIdLastVersionVueDeBasePrincipale = (int)new CDatabaseRegistre(this).GetValeurLong(c_cleLastVersionVueDeBasePrincipale, -1);

				return m_nIdLastVersionVueDeBasePrincipale;
			}
			set
			{
				m_nIdLastVersionVueDeBasePrincipale = value;
				new CDatabaseRegistre(this).SetValeur(c_cleLastVersionVueDeBasePrincipale, m_nIdLastVersionVueDeBasePrincipale.ToString());
			}
		}

		////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'id de dernière synchro depuis la base principale
		/// </summary>
		public int LastSyncIdPutInBasePrincipale
		{
			get
			{
				if (!IsInTrans())
					m_nIdLastVersionPutInBasePrincipale = (int)new CDatabaseRegistre(this).GetValeurLong(c_cleLastVersionInBasePrincipale, -1);
				return m_nIdLastVersionPutInBasePrincipale;
			}
			set
			{
				m_nIdLastVersionPutInBasePrincipale = value;
				new CDatabaseRegistre(this).SetValeur(c_cleLastVersionInBasePrincipale, m_nIdLastVersionPutInBasePrincipale.ToString());
			}
		}

		////////////////////////////////////////////////////////
		public override CResultAErreur BeginTrans(IsolationLevel isolationLevel)
		{
			m_nIdSyncSession = IdSyncSession;
			return base.BeginTrans(isolationLevel);
		}

		////////////////////////////////////////////////////////
		public override CResultAErreur BeginTrans()
		{
			m_nIdSyncSession = IdSyncSession;
			return base.BeginTrans();
		}


		/// /////////////////////////////////////////////////////
		public void SetIdSyncSessionNonPersistant(int nId)
		{
			m_nIdSyncSession = nId;
			m_bIsSyncSessionVerouillee = true;
		}

		/////////////////////////////////////////////////////////
		public override string ConnexionString
		{
			get
			{
				return base.ConnexionString;
			}
			set
			{
				if (value != base.ConnexionString)
				{
					base.ConnexionString = value;
					if (IsConnexionValide())
					{
						//Récupère le numéro de session de synchronisation
                        RefreshValeursSyncSession();
						//if ( m_nIdSyncSession == -1 )
						//	throw new Exception("Impossible d'initialiser le numéro de SyncSession");
					}
				}
			}
		}

		/// /////////////////////////////////////////////////////
		public override void PrepareTableToWriteDatabase(DataTable table)
		{
			if (!m_bEnableLogSyncrhonisation)
				base.PrepareTableToWriteDatabase(table);
			//Modifie le numéro de session pour tous les enregistrements qui
			//partent dans la base
			bool bIsModif;
			if (table.Columns[CSc2iDataConst.c_champIdSynchro] != null)
			{
				foreach (DataRow row in table.Rows)
				{
					bIsModif = row.RowState == DataRowState.Added;
					if (row.RowState == DataRowState.Modified)
					{
						//Vérifie qu'il y a réellement eu modif
						foreach (DataColumn col in row.Table.Columns)
						{
							if (row[col.ColumnName, DataRowVersion.Original] != row[col.ColumnName, DataRowVersion.Current])
							{
								bIsModif = true;
								break;
							}
						}
					}
					if (bIsModif)
						row[CSc2iDataConst.c_champIdSynchro] = m_nIdSyncSession;
				}
			}
		}

		///// /////////////////////////////////////////////////////
		//protected override IAdapterBuilder GetBuilder(Type leType)
		//{
		//    if (!m_bEnableLogSyncrhonisation)
		//        return base.GetBuilder(leType);
		//    return new C2iOracleAdapterBuilderForTypeSynchronisable(leType, this);
		//}


		/////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Initialise une clause set d'un update pour les champs demandés
		/// Retour contient les paramètres créés dans l'ordre de strChamps
		/// </summary>
		/// <param name="command"></param>
		/// <param name="strChamps"></param>
		/// <param name="valeurs"></param>
		/// <returns></returns>
		protected override IDbDataParameter[] InitUpdateSetClause(IDbCommand command, ref string strUpdateClause, string[] strChamps, object[] valeurs)
		{
			IDbDataParameter[] retour = base.InitUpdateSetClause(command, ref strUpdateClause, strChamps, valeurs);
			strUpdateClause += "," + CSc2iDataConst.c_champIdSynchro + "=" + IdSyncSession;
			return retour;
		}

        /// /////////////////////////////////////////////////////////////////
        public void RenseignerPourSynchro(DataRow row, StatementType typeOperation)
        {
            if (!m_bEnableLogSyncrhonisation)
                return;
            string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(row.Table.TableName);
            if (strNomTableInDb == null)
                strNomTableInDb = row.Table.TableName;
            //Si la synchro est active et que la table est synchronisable...
            if (row.Table.Columns[CSc2iDataConst.c_champIdSynchro] != null
            && (typeOperation == StatementType.Insert || typeOperation == StatementType.Delete))
            {
                //Recupération de l'ID de l'élément
                string strID;
                DataColumn[] colsID = row.Table.PrimaryKey;
                if (colsID.Length == 1 && colsID[0].AutoIncrement)
                {
                    if (typeOperation == StatementType.Delete)
                        strID = row[colsID[0], DataRowVersion.Original].ToString();
                    else
                        strID = row[colsID[0]].ToString();
                }
                else
                    return;

                //Creation de la requete
                int nIdSync = IdSyncSession;
                string rqtMAJTableSynchro = "Insert into " +
                    CEntreeLogSynchronisation.c_nomTable + " (" +
                    CEntreeLogSynchronisation.c_champTable + "," +
                    CEntreeLogSynchronisation.c_champIdElement + "," +
                    CEntreeLogSynchronisation.c_champType + "," +
                    CSc2iDataConst.c_champIdSynchro + ") values (" +
                    "'" + strNomTableInDb + "'," +
                    strID + ",";

                if (typeOperation == StatementType.Insert)
                    rqtMAJTableSynchro += ((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd).ToString();
                else
                    rqtMAJTableSynchro += ((int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete).ToString();

                rqtMAJTableSynchro += "," + nIdSync.ToString() + ")";

                RunStatement(rqtMAJTableSynchro);
            }
        }
    }
}
