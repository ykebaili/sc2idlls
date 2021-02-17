using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	public class CUpdaterDataBase
	{
		private IDatabaseConnexion m_connexion;
		private IStructureDataBase m_structureDB;
		public const string c_strCleVersionBDD = "VERSION_BDD";

		private bool m_bForceMAJ;
		public bool MAJDerniereVersionBase
		{
			get
			{
				return m_bForceMAJ;
			}
			set
			{
				m_bForceMAJ = value;
			}
		}
		public int NbTableInitialisation
		{
			get
			{
				return m_connexion.GetDataBaseCreator().NbTableInitialisation;
			}
		}
		public int GetNbTableUpdate(int n)
		{
			return m_structureDB.GetListeTypeOfVersion(n).Count;
		}

		/// /////////////////////////////////////////////////////////
		static CUpdaterDataBase m_instance;
		public static CUpdaterDataBase GetInstance(IDatabaseConnexion connexion, IStructureDataBase structureDB)
		{
			if (m_instance == null)
				m_instance = new CUpdaterDataBase(connexion, structureDB);
			return m_instance;
		}


		//---------------------------------------------------------------------------		
		public CUpdaterDataBase(IDatabaseConnexion connexion, IStructureDataBase structureDB)
		{
			m_bForceMAJ = false;
			m_structureDB = structureDB;
			m_connexion = connexion;
		}


		public IDatabaseConnexion Connection
		{
			get
			{
				return m_connexion;
			}
		}
		public IStructureDataBase StructureDataBase
		{
			get
			{
				return m_structureDB;
			}
		}
		private IIndicateurProgression m_indicateurProgress;

		//---------------------------------------------------------------------------
		public CResultAErreur UpdateStructureBase(IIndicateurProgression indicateurProgress)
		{
			CResultAErreur result = CResultAErreur.True;

			m_indicateurProgress = indicateurProgress;

			int nVersionDB = (int)new CDatabaseRegistre(Connection).GetValeurLong(c_strCleVersionBDD, -1);
			int nVersionFinale = m_structureDB.GetLastVersion();

			int nOldVersion = nVersionDB;

			if (nVersionDB < 0)
				result = InitialisationBase();

			if (m_bForceMAJ)
				nVersionDB = nVersionDB > 0 ? nVersionDB - 1 : 0;

			if (nVersionDB < 0)
				nVersionDB = 0;

			if (m_indicateurProgress != null)
			{
				m_indicateurProgress.PushLibelle(I.T("Database Update|30005"));
                m_indicateurProgress.SetBornesSegment(nVersionDB, nVersionFinale);
				//m_indicateurProgress.PushSegment(nVersionDB, nVersionFinale);
			}

			result = m_connexion.GetDataBaseCreator().UpdateStructureTableRegistre();

			while (nVersionDB < nVersionFinale && result)
			{
				nVersionDB++;

				result = m_connexion.BeginTrans();

				if (!result)
					return result;

				if (m_indicateurProgress != null)
				{
					m_indicateurProgress.SetInfo("Version " + nVersionDB.ToString());
					m_indicateurProgress.SetValue(nVersionDB-1);
					m_indicateurProgress.PushLibelle("DataBase v" + nVersionDB.ToString());
					m_indicateurProgress.PushSegment(nVersionDB-1, nVersionDB);
				}

				result = UpdateToVersion(nVersionDB);

				//Validation ou annulation des modifications si erreur
				if (!result)
				{
					m_connexion.RollbackTrans();
					result.EmpileErreur(I.T("Database Update Error|30006"));
					throw new CExceptionErreur(result.Erreur);
				}
				else
					result = m_connexion.CommitTrans();


				if (m_indicateurProgress != null)
				{
					m_indicateurProgress.PopSegment();
					m_indicateurProgress.PopLibelle();
				}
			}

			/*if (m_indicateurProgress != null)
				m_indicateurProgress.PopSegment();*/

            //rafraichit les caches de schéma
            CObjetServeur.ClearCacheSchemas();
            C2iOracleDataAdapter.ClearCacheSchemas();
            CStructureTable.ClearCache();

			return result;
		}

		//---------------------------------------------------------------------------
		public int ChangeCommandTimeOut(int nNewValeur)
		{
			int nOld = m_connexion.CommandTimeOut;
			m_connexion.CommandTimeOut = nNewValeur;
			return nOld;
		}


		//---------------------------------------------------------------------------
		//Procédure de mise à jour du numéro de version
		protected CResultAErreur SetVersion(int nVersion)
		{
			return new CDatabaseRegistre(Connection).SetValeur(c_strCleVersionBDD, nVersion.ToString());
		}

		//private void CreerOuMettreAJourTableDansNouveauThread()
		//{
		//}

		//---------------------------------------------------------------------------
		private CResultAErreur InitialisationBase()
		{
			CResultAErreur result = CResultAErreur.True;
			result = m_connexion.GetDataBaseCreator().InitialiserDataBase();
			if (result)
				result = SetVersion(0);
			return result;
		}
		//---------------------------------------------------------------------------
		private CResultAErreur UpdateToVersion(int nVersion)
		{
			CResultAErreur result = CResultAErreur.True;

			C2iDataBaseUpdateOperationList lstOps = m_structureDB.GetListeTypeOfVersion(nVersion);
			int nCptOp = 0;
			if (m_indicateurProgress != null)
				m_indicateurProgress.SetBornesSegment(nCptOp, lstOps.Count);

			bool bMAJVersion = true;
			foreach (C2iDataBaseUpdateOperation op in lstOps)
			{
				if (m_indicateurProgress != null)
				{
					m_indicateurProgress.SetInfo(op.DescriptionOperation);
					m_indicateurProgress.SetValue(nCptOp);
				}

				result = op.ExecuterOperation(Connection, m_indicateurProgress);
				bMAJVersion = (op.GetType() != typeof(C2iDataBaseUpdateOperationNoSetVersionBase))? bMAJVersion:false;
				nCptOp++;

				if(!result)
					break;
			}

			if (result && bMAJVersion && nVersion <= m_structureDB.GetLastVersion())
				result = SetVersion(nVersion);

			return result;
		}
	}
}
