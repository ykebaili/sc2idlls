using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Remoting;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using System.Text;
using sc2i.data.synchronisation;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CObjetLoader.
	/// </summary>

	public abstract class CObjetServeur : C2iObjetServeur, IObjetServeur
	{
        /// <summary>
        /// Gestion du cache des requêtes
        /// </summary>
        private class CCacheLecture
        {
            //Nom de la table->Date d'invalidité reçue
            private static Dictionary<string, DateTime> m_dicTableToDateInvalidite = new Dictionary<string, DateTime>();

            private static int c_nTailleCacheParSession = 30;
            private static Dictionary<int, CCacheLecturePourSession> m_cacheLecture = new Dictionary<int, CCacheLecturePourSession>();
            #region class CCacheLecturePourSession
            private class CCacheLecturePourSession : IDisposable, IObjetAttacheASession
            {
                private int m_nIdSession;
                //Key->Cache des lectures de la session
                private Dictionary<string, CCacheLecturePourTableEtFiltre> m_dicCache = new Dictionary<string, CCacheLecturePourTableEtFiltre>();

                public CCacheLecturePourSession(int nIdSession)
                {
                    m_nIdSession = nIdSession;
                    CGestionnaireObjetsAttachesASession.AttacheObjet(nIdSession, this);
                }



                public void Dispose()
                {
                    CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSession, this);
                    foreach (CCacheLecturePourTableEtFiltre cache in m_dicCache.Values)
                    {
                        cache.Dispose();
                    }
                    m_dicCache.Clear();
                }

                private class CCacheLecturePourTableEtFiltre : IDisposable, IComparable<CCacheLecturePourTableEtFiltre>
                {
                    private string m_strKey;
                    private DataTable m_tableResult;
                    private DateTime m_lastAcces = DateTime.Now;
                    private DateTime m_dateDonnees = DateTime.Now;

                    public CCacheLecturePourTableEtFiltre(string strKey, DataTable tableResult)
                    {
                        m_strKey = strKey;
                        m_tableResult = tableResult;
                    }

                    public void Dispose()
                    {
                    }

                    /// <summary>
                    /// date à laquelle les données ont été prises dans la base
                    /// </summary>
                    public DateTime DateDonnees
                    {
                        get
                        {
                            return m_dateDonnees;
                        }
                    }

                    public DateTime LastAcces
                    {
                        get
                        {
                            return m_lastAcces;
                        }
                        set
                        {
                            m_lastAcces = value;
                        }
                    }


                    public string Key
                    {
                        get
                        {
                            return m_strKey;
                        }
                    }
                    public DataTable TableResult
                    {
                        get
                        {
                            return m_tableResult;
                        }
                    }

                    //--------------------------------------------------------
                    public int CompareTo(CCacheLecturePourTableEtFiltre other)
                    {
                        return m_lastAcces.CompareTo(other.LastAcces);
                    }
                }

                //--------------------------------------------------------
                private string GetKey(
                    int? nIdVersion,
                    CFiltreData filtre,
                    bool bCount,
                    string strNomTable)
                {
                    if (filtre == null)
                        return "";
                    StringBuilder bl = new StringBuilder();
                    bl.Append(strNomTable);
                    if (nIdVersion == null)
                        bl.Append("/NO/");
                    else
                        bl.Append("/" + nIdVersion.ToString() + "/");
                    if (bCount)
                        bl.Append("/COUNT/");
                    else
                        bl.Append("/DATA/");
                    bl.Append(filtre.IntergerParentsHierarchiques ? "1/" : "0/");
                    bl.Append(filtre.IntegrerFilsHierarchiques ? "1/" : "0/");
                    bl.Append(filtre.NeConserverQueLesRacines ? "1/" : "0/");

                    bl.Append(filtre.Filtre);
                    foreach (object obj in filtre.Parametres)
                    {
                        if (obj != null)
                        {
                            bl.Append("/");
                            IList lst = obj as IList;
                            if (lst != null)
                            {
                                foreach (object val in lst)
                                {
                                    if (val == null)
                                        bl.Append("NULL");
                                    else
                                        bl.Append(val.ToString());
                                    bl.Append("#");
                                }
                            }
                            else
                                bl.Append(obj.ToString());
                        }
                    }
                    bl.Append('/');
                    bl.Append(filtre.SortOrder.Replace(strNomTable + ".", ""));
                    return bl.ToString();
                }

                //--------------------------------------------------------
                public void StockeResult(
                    string strNomTable,
                    int? nIdVersion,
                    CFiltreData filtre,
                    bool bCount,
                    DataTable tableResultat)
                {
                    string strKey = GetKey(nIdVersion, filtre, bCount ,strNomTable);
                    if (strKey.Length > 50)
                        return;
                    CCacheLecturePourTableEtFiltre cache = new CCacheLecturePourTableEtFiltre(strKey, tableResultat);
                    lock (m_dicCache)
                    {
                        m_dicCache[strKey] = cache;
                    }
                    NettoieCache();
                }

                //-----------------------------------------
                private void NettoieCache()
                {
                    lock (m_dicCache)
                    {
                        while (m_dicCache.Count > c_nTailleCacheParSession)
                        {
                            List<CCacheLecturePourTableEtFiltre> lst = new List<CCacheLecturePourTableEtFiltre>();
                            foreach (CCacheLecturePourTableEtFiltre cache in m_dicCache.Values)
                                lst.Add(cache);
                            lst.Sort();
                            m_dicCache.Remove(lst[0].Key);
                        }
                    }
                }

                //-----------------------------------------
                public DataTable GetCache(
                    string strNomTable,
                    int? nIdVersion,
                    CFiltreData filtre,
                    bool bCount)
                {
                    string strKey = GetKey(nIdVersion, filtre, bCount, strNomTable);
                    if (strKey.Length > 50)
                        return null;
                    CCacheLecturePourTableEtFiltre cache = null;
                    if (m_dicCache.TryGetValue(strKey, out cache))
                    {
                        //Regarde la validité du cache, si aucune table accedée n'a bougé
                        List<string> lstTablesAccedees = new List<string>();
                        lstTablesAccedees.Add(strNomTable);
                        if (filtre is CFiltreDataAvance)
                        {
                            CFiltreDataAvance filtreAv = filtre as CFiltreDataAvance;
                            CResultAErreur result = filtreAv.GetArbreTables();
                            if (result)
                            {
                                CArbreTable arbre = result.Data as CArbreTable;
                                if (arbre != null)
                                    lstTablesAccedees.AddRange(arbre.TablesAccedees);
                            }
                        }

                        foreach (string strTable in lstTablesAccedees)
                        {
                            DateTime dt;
                            if (CCacheLecture.m_dicTableToDateInvalidite.TryGetValue(strTable, out dt))
                                if (cache.DateDonnees < dt)
                                {
                                    m_dicCache.Remove(strKey);
                                    return null;
                                }
                        }
                        cache.LastAcces = DateTime.Now;
                        return cache.TableResult;
                    }
                    return null;
                }

                #region IObjetAttacheASession Membres

                public string DescriptifObjetAttacheASession
                {
                    get { return "Data cache"; }
                }

                public void OnCloseSession()
                {
                    CCacheLecture.OnCloseSession(m_nIdSession);
                }

                #endregion

                internal void ClearCache(string strTable)
                {
                    m_dicCache.Clear();
                }
            }
            #endregion

            public static void CacheResult(int nIdSession,
                string strTable,
                int? nIdVersion,
                CFiltreData filtre,
                bool bCount,
                DataTable tableResultat)
            {
                CCacheLecturePourSession cache = null;
                if (!m_cacheLecture.TryGetValue(nIdSession, out cache))
                {
                    cache = new CCacheLecturePourSession(nIdSession);
                    m_cacheLecture[nIdSession] = cache;
                }
                cache.StockeResult(strTable, nIdVersion, filtre, bCount, tableResultat);
            }

            public static DataTable GetCache(int nIdSession,
                string strTable,
                int? nIdVersion,
                bool bCount,
                CFiltreData filtre)
            {
                CCacheLecturePourSession cache = null;
                if (m_cacheLecture.TryGetValue(nIdSession, out cache))
                    return cache.GetCache(strTable, nIdVersion, filtre, bCount);
                return null;
            }

            public static void ClearCache(int nIdSession, string strTable)
            {
                CCacheLecturePourSession cache = null;
                if (m_cacheLecture.TryGetValue(nIdSession, out cache))
                    cache.ClearCache(strTable);
            }


            internal static void OnCloseSession(int nIdSession)
            {
                if (m_cacheLecture.ContainsKey(nIdSession))
                    m_cacheLecture.Remove(nIdSession);
            }

            internal static void InvalideTable(string strNomTable)
            {
                m_dicTableToDateInvalidite[strNomTable] = DateTime.Now;
            }
        }


		private bool m_bDesactiverIdAuto = false;
		private bool m_bDesactiverContraintes = false;
		private Hashtable m_tableDependancesChargees = new Hashtable();
		private int? m_nIdVersionTravail = null;
		//private IDataAdapter m_adapter = null;

		//Gestion du cache des tables en cache complet (non pas des requêtes)
		private static Dictionary<string, DataTable> m_cacheTables = new Dictionary<string, DataTable>();
		private static CRecepteurNotification m_recepteurNotificationsAjout = null;
		private static CRecepteurNotification m_recepteurNotificationsModif = null;
		private class CLockerCacheObjetServeur { }

#if PDA
		//////////////////////////////////////////////////
		public CObjetServeur ()
			:base(-1)
		{
		}
#endif
		//////////////////////////////////////////////////
		public CObjetServeur( int nIdSession )
			:base ( nIdSession )
		{
			if ( nIdSession == -1 )
				throw new Exception(I.T("Sever object with -1 session|178"));
		}

		//////////////////////////////////////////////////
		public virtual int? IdVersionDeTravail
		{
			get
			{
				return m_nIdVersionTravail;
			}
			set
			{
				m_nIdVersionTravail = value;
			}
		}

		//////////////////////////////////////////////////
		/// <summary>
		/// Indique que les données de ce type d'élément peuvent être mises en cache
		/// pour éviter des lectures repetées dans la base de données
		/// </summary>
		protected virtual bool UseCache
		{
			get
			{
				return false;
			}
		}

		//////////////////////////////////////////////////
		public abstract string GetNomTable();

		//////////////////////////////////////////////////
		/// <summary>
		/// Retourne le nom de la table dans la base de données
		/// </summary>
		/// <returns></returns>
		public virtual string GetNomTableInDb()
		{
			return CContexteDonnee.GetNomTableInDbForNomTable(GetNomTable());
		}

		//////////////////////////////////////////////////
		public abstract CResultAErreur VerifieDonnees(CObjetDonnee objet);

        //////////////////////////////////////////////////
        public CResultAErreur VerifieDonneesAvecFilles(CObjetDonnee objet)
        {
            CResultAErreur result = VerifieDonnees(objet);
            if (result)
                result = VerifieDonneesFilles(objet.Row.Row);
            return result;
        }

		//////////////////////////////////////////////////
		public abstract Type GetTypeObjets();

		/// ///////////////////////////////////////////////////////
		public virtual bool HasBlobs()
		{
			return false;
		}

		/////////////////////////////////////////////////////////
		//Permet d'ajouter un filtre de sécurité au filtre en cours
		public CFiltreData GetFiltreSecurite ()
		{
			CFiltreData filtre = GetMyFiltreSysteme();
			CGestionnaireFiltresSecuriteSupplementaire.CompleteFiltre ( IdSession, GetTypeObjets(), ref filtre );
			return filtre;
		}

		/////////////////////////////////////////////////////////
		protected virtual CFiltreData GetMyFiltreSysteme()
		{
			return null;
		}


		/////////////////////////////////////////////////////////
		public CDataTableFastSerialize Read ( CFiltreData filtre, params string[] strChampsARetourner )
		{
			return ReadAndReturnDataTable ( filtre, strChampsARetourner );
		}

        /////////////////////////////////////////////////////////
        public DataTable ReadAndReturnDataTable(CFiltreData filtre, params string[] strChampsARetourner)
        {
            return ReadAndReturnDataTable(filtre, -1, -1, strChampsARetourner);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Indique si le cache de requête est activé pour cette table
        /// </summary>
        public virtual bool ActivateQueryCache
        {
            get
            {
                return true;
            }
        }

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la version la plus récente de l'objet pour la version
		/// </summary>
		/// <param name="row"></param>
		/// <param name="connexion"></param>
		/// <returns></returns>
		private DataRow GetRowLaPlusRecentePourRow(DataRow row, IDatabaseConnexion connexion, Dictionary<int, int> dicoOriginalToIdInVer)
		{
			string strPrimKey = row.Table.PrimaryKey[0].ColumnName;
			int nIdElement = (int)row[strPrimKey];
			int nIdInVers;
			if (dicoOriginalToIdInVer.TryGetValue(nIdElement, out nIdInVers))
			{
				DataRow rowFinale = null;
				//il faut trouver l'élement dans la base
				CFiltreData filtre = new CFiltreData(
					strPrimKey + "=@1",
					nIdInVers);
				filtre.SortOrder = CSc2iDataConst.c_champIdVersion + " desc";
				string strReq = GetRequeteSelectForRead(connexion);
				IDataAdapter adapter = connexion.GetSimpleReadAdapter(strReq, filtre);
                DataSet dsTmp = new DataSet();
                adapter.TableMappings.Add(new DataTableMapping("Table", row.Table.TableName));
                dsTmp.Tables.Add(row.Table.Clone());
                connexion.FillAdapter(adapter, dsTmp);
                CUtilDataAdapter.DisposeAdapter(adapter);
                DataTable tableTmp = dsTmp.Tables[0];
				if (tableTmp.Rows.Count != 0)
				{
					rowFinale = tableTmp.Rows[0];
					nIdElement = (int)rowFinale[strPrimKey];
				}
				return rowFinale;
			}
			return null;
		}

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Supprime les éléments supprimés depuis la version
        /// </summary>
        /// <param name="table"></param>
        /// <param name="contexteDeTravail">doit contenir toutes les modifications depuis le référentiel jusqu'à la version et 
        /// uniquement celles ci(utilisation de InterditLectureInDb</param>
        private void RemoveElementsSupprimes(DataTable table, CContexteDonnee contexte)
        {
            ArrayList lstRows = new ArrayList(table.Rows);
            string strPrimKey = table.PrimaryKey[0].ColumnName;
            DataTable tableVersion = contexte.Tables[CVersionDonneesObjet.c_nomTable];
            foreach (DataRow row in lstRows)
            {
                string strFilter = String.Format("{0}={1} and {2}={3}",
                    CVersionDonneesObjet.c_champIdElement,
                    row[strPrimKey].ToString(),
                    CVersionDonneesObjet.c_champTypeOperation,
                    (int)CTypeOperationSurObjet.TypeOperation.Suppression);
                if (tableVersion.Select(strFilter).Length != 0)
                {
                    //L'objet a été supprime
                    table.Rows.Remove(row);
                }
            }
        }

		/////////////////////////////////////////////////////////
		//Crée un dicitionnaire des Ids version les ids originaux des éléments
		private Dictionary<int, int> GetMapIdToIdOriginal(DataTable table)
		{
			Hashtable tableOriginauxALire = new Hashtable();
			string strPrimKey = table.PrimaryKey[0].ColumnName;
			List<int> listeConnus = new List<int>();

			///Map chaque id avec son original
			Dictionary<int, int> mapIdToOriginalId = new Dictionary<int, int>();

			foreach (DataRow row in table.Rows)
			{
				if (row[CSc2iDataConst.c_champOriginalId] != DBNull.Value)
				{
					tableOriginauxALire[row[CSc2iDataConst.c_champOriginalId]] = true;
					mapIdToOriginalId[(int)row[strPrimKey]] = (int)row[CSc2iDataConst.c_champOriginalId];
				}
				listeConnus.Add((int)row[strPrimKey]);
			}
			

			//Crée la liste des originaux à lire
			//Supprime de la liste des originaux recherchés ceux pour lesquels on connait
			//déjà l'id de l'original
			foreach (int nId in listeConnus)
				tableOriginauxALire.Remove(nId);
			while (tableOriginauxALire.Count > 0)
			{
				StringBuilder bl = new StringBuilder();
				foreach (int nId in tableOriginauxALire.Keys)
				{
					bl.Append(nId.ToString());
					bl.Append(',');
				}
				bl.Remove(bl.Length - 1, 1);


				C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
				requete.TableInterrogee = GetNomTable();
				requete.ListeChamps.Add(new C2iChampDeRequete(strPrimKey,
					new CSourceDeChampDeRequete(strPrimKey),
					typeof(int),
					OperationsAgregation.None,
					true));
				requete.ListeChamps.Add(new C2iChampDeRequete(CSc2iDataConst.c_champOriginalId,
				new CSourceDeChampDeRequete(CSc2iDataConst.c_champOriginalId),
				typeof(int),
				OperationsAgregation.None,
				true));
				requete.FiltreAAppliquer = new CFiltreData(strPrimKey + " in (" + bl.ToString() + ")");
				requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
				CResultAErreur result = requete.ExecuteRequete(IdSession);
				if (!result)
					return null;
				if (((DataTable)result.Data).Rows.Count == 0)//On n'a rien trouvé,
					//ce n'est pas normal, mais il ne faut pas faire une boucle infinie pour autant
					return mapIdToOriginalId;
				foreach (DataRow rowMap in ((DataTable)result.Data).Rows)
				{
					listeConnus.Add((int)rowMap[strPrimKey]);
					if (rowMap[CSc2iDataConst.c_champOriginalId] != DBNull.Value)
					{
						tableOriginauxALire[rowMap[CSc2iDataConst.c_champOriginalId]] = true;
						mapIdToOriginalId[(int)rowMap[strPrimKey]] = (int)rowMap[CSc2iDataConst.c_champOriginalId];
					}
				}
				foreach (int nIdOriginal in listeConnus)
				{
					tableOriginauxALire.Remove(nIdOriginal);
				}
			}
			return mapIdToOriginalId;
		}

		/// <summary>
		/// Retourne un dictionnaire, qui, pour chaque id original indique l'élément
		/// à utiliser pour la version (ou les versions successives)
		/// </summary>
		/// <param name="strIdsVersions"></param>
		/// <param name="table"></param>
		/// <returns></returns>
		private Dictionary<int, int> GetDicoOriginalToElementDeVersion(
			string strIdsSuccessives,
			DataTable table)
		{
			string strPrimKey = table.PrimaryKey[0].ColumnName;
			Dictionary<int, int> dico = new Dictionary<int, int>();
			for (int nPaquet = 0; nPaquet < table.Rows.Count; nPaquet += 100)
			{
				int nMax = Math.Min(nPaquet + 100, table.Rows.Count);
				StringBuilder bl = new StringBuilder();
				for (int nRow = nPaquet; nRow < nMax; nRow++)
				{
					bl.Append(table.Rows[nRow][strPrimKey].ToString());
					bl.Append(",");
				}
				string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(table.TableName);
				if (bl.Length > 0)
				{
					bl.Remove(bl.Length - 1, 1);

					string strRequete = "select " +
						strPrimKey + "," +
						CSc2iDataConst.c_champOriginalId +
						" from " +
						strNomTableInDb + " T1 where " +
						CSc2iDataConst.c_champOriginalId + " in (" + bl.ToString() + ") and " +
						CSc2iDataConst.c_champIdVersion + " in (" + strIdsSuccessives + ") and " +
						CSc2iDataConst.c_champIdVersion + "=(select max(" + CSc2iDataConst.c_champIdVersion + ") from " +
						table.TableName + " T2 where " +
						"T1." + CSc2iDataConst.c_champOriginalId + "=" +
						"T2." + CSc2iDataConst.c_champOriginalId + " and " +
						CSc2iDataConst.c_champIdVersion + " in (" + strIdsSuccessives + "))";
					IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType());
					IDataAdapter adapter = connexion.GetAdapterForRequete(strRequete);
					DataSet ds = new DataSet();
                    connexion.FillAdapter(adapter, ds);
                    DataTable tableTmp = ds.Tables[0];
					//tableTmp.Columns[0].DataType = tableTmp.Columns[1].DataType = typeof(int);
					foreach (DataRow row in tableTmp.Rows)
						dico.Add(Convert.ToInt32(row[CSc2iDataConst.c_champOriginalId]), Convert.ToInt32(row[strPrimKey]));
				}
			}
			return dico;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Modifie les données de la table pour qu'elle
        /// contienne les informations conformes à une version, à savoir :
        /// -Les données existantes du référentiel
        ///		-Données non supprimées dans la version
        ///		-Avec les modifs de la version appliquées
        ///	-Les données nouvelles dans la version et dans les versions précédentes
        /// </summary>
        /// <param name="table"></param>
        private CResultAErreur AdapteDatatableSurVersion(DataTable table, bool bIsLectureParId)
        {
            CResultAErreur result = CResultAErreur.True;
            //Travail brut sur la base, sans gestion de version
            if (m_nIdVersionTravail != null && m_nIdVersionTravail < 0)
                return result;


            IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType());

            if (!typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(GetTypeObjets()) ||
                typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()))
                return result;
            if (IdVersionDeTravail == null)
                return result;
            List<int> listeElementsDuReferentielALire = new List<int>();
            if (table.Rows.Count == 0)
                return result;

            //Liste des ids de version pour aller jusqu'à celle ci
            string strIdsSuccessives = "";
            using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
            {
                #region Lecture des modifications effectuées dans la version
                int[] ids = CVersionDonnees.GetVersionsToRead(IdSession, (int)IdVersionDeTravail);
                StringBuilder bl = new StringBuilder();
                foreach (int nId in ids)
                {
                    bl.Append(nId.ToString());
                    bl.Append(",");
                }
                bl.Remove(bl.Length - 1, 1);
                strIdsSuccessives = bl.ToString();

                Dictionary<int, int> dicoIdToIdDansVersion = GetDicoOriginalToElementDeVersion(strIdsSuccessives, table);

                #endregion


                ///Suppression des éléments supprimés dans cette version
                ///
                //S'assure que les modifications de suppression sont lues
                CListeObjetsDonnees lstModifs = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjet));
                lstModifs.Filtre = new CFiltreData(CVersionDonnees.c_champId + " in (" + strIdsSuccessives + ") and " +
                    CVersionDonneesObjet.c_champTypeElement + "=@1 and " +
                    CVersionDonneesObjet.c_champTypeOperation + "=@2",
                    GetTypeObjets().ToString(),
                    (int)CTypeOperationSurObjet.TypeOperation.Suppression);
                lstModifs.AssureLectureFaite();

                RemoveElementsSupprimes(table, contexte);


                /* A ce stade, on a dans la table :
                 * Les données du référentiel 
                 * Les données des versions successives jusqu'à la version en recherchée
                 * En parcourant la table, par id de version, on trouve d'abord les données
                 * de la version, puis celles de la version parente et de la version parente...
                 * jusqu'au référentiel.
                 * Il suffit de convertir tout ça sur les ids du référentiel, ou de la version
                 * la plus proche du référentiel pour les éléments nouveaux
                 */

                string strPrimKey = table.PrimaryKey[0].ColumnName;
                table.Columns[strPrimKey].ReadOnly = false;

                //Création dun mappage entre les ids des éléments et leurs ids originaux
                Dictionary<int, int> mapIdToOriginalId = GetMapIdToIdOriginal(table);

                //Travail par ordre de version croissant pour ne garder au final que la dernière version
                table.DefaultView.Sort = CSc2iDataConst.c_champIdVersion + " asc";
                ArrayList lstRows = new ArrayList();
                lstRows = new ArrayList();
                foreach (DataRowView tmp in table.DefaultView)
                    lstRows.Add(tmp.Row);

                DataTable tableVersionObjet = contexte.Tables[CVersionDonneesObjet.c_nomTable];

                foreach (DataRow row in lstRows)
                {
                    if (row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted)
                    {
                        //Si la ligne est dans la version ou une version précedente, trouve l'id originale
                        if (row[CSc2iDataConst.c_champIdVersion] != DBNull.Value)
                        {
                            int nId = (int)row[strPrimKey];
                            while (mapIdToOriginalId.ContainsKey(nId))
                            {
                                nId = mapIdToOriginalId[nId];
                                //supprime la ligne originale de la table pour qu'on ne garde
                                //que la ligne de la version
                                DataRow rowOriginal = table.Rows.Find(nId);
                                if (rowOriginal != null)
                                {
                                    table.Rows.Remove(rowOriginal);
                                }
                            }
                            //Il ne sera pas nécéssaire de rechercher l'élément
                            //pour cet objet si c'est lui même !
                            int nIdInVer;
                            if (dicoIdToIdDansVersion.TryGetValue(nId, out nIdInVer) &&
                                nIdInVer == (int)row[strPrimKey])
                                dicoIdToIdDansVersion.Remove(nId);
                            row[strPrimKey] = nId;
                            //Fait croire au système que l'élément est dans le référentiel !
                            //Sauf si l'élément ne vient pas du référentiel, mais d'une version
                            if (row[CSc2iDataConst.c_champOriginalId] != DBNull.Value)
                            {
                                row[CSc2iDataConst.c_champIdVersion] = DBNull.Value;
                                row[CSc2iDataConst.c_champOriginalId] = DBNull.Value;
                            }
                        }
                        else // c'est une ligne du référentiel. si elle existe dans la version
                        //Et qu'on ne la voit pas c'est qu'elle ne doit pas faire partie du
                        //jeu de données retourné
                        {
                            string strFiltre = String.Format("{0}={1}",
                                CVersionDonneesObjet.c_champIdElement,
                                row[strPrimKey]);
                            if (tableVersionObjet.Select(strFiltre).Length != 0)
                            /*CListeObjetsDonnees lst = new CListeObjetsDonnees ( contexte, typeof(CVersionDonneesObjet) );
                            lst.Filtre = new CFiltreData ( CVersionDonneesObjet.c_champIdElement+"=@1",
                                row[strPrimKey]);
                            lst.InterditLectureInDB = true;
                            if ( lst.Count > 0 )*/
                            {
                                //Il y a des modifs sur cette ligne, mais on ne la voit pas,
                                //Donc elle ne doit pas être dans le jeu de données
                                //Sauf si on est en train de lire à partir des Ids
                                if (!bIsLectureParId)
                                    table.Rows.Remove(row);
                            }
                        }
                        //Cherche la version la plus récente de la ligne pour la version en cours
                        if (row.RowState != DataRowState.Detached && row.RowState != DataRowState.Deleted)
                        {
                            DataRow rowFinale = GetRowLaPlusRecentePourRow(row, connexion, dicoIdToIdDansVersion);
                            if (rowFinale != null)
                            {
                                foreach (DataColumn col in table.Columns)
                                {
                                    if (col.ColumnName != strPrimKey &&
                                        col.ColumnName != CSc2iDataConst.c_champOriginalId &&
                                        col.ColumnName != CSc2iDataConst.c_champIdVersion)
                                        row[col.ColumnName] = rowFinale[col.ColumnName];
                                }
                            }
                            row.AcceptChanges();
                        }
                    }
                }
            }
            return result;
        }

		/////////////////////////////////////////////////////////
		public virtual string GetRequeteSelectForRead( IDatabaseConnexion connexion, params string[] strChampsARetourner )
		{
			string strTableForRequete = connexion.GetNomTableForRequete(GetNomTableInDb());
            StringBuilder bl = new StringBuilder();
            bl.Append("select ");
            if (strChampsARetourner == null || strChampsARetourner.Length == 0)
            {
                bl.Append(strTableForRequete);
                bl.Append(".* ");
            }
            else
            {
                foreach (string strChamp in strChampsARetourner)
                {
                    bl.Append(strTableForRequete);
                    bl.Append(".");
                    bl.Append(strChamp);
                    bl.Append(" ");
                }
            }
            bl.Append(" from " + strTableForRequete);
            return bl.ToString();
		}

		/////////////////////////////////////////////////////////
		//Fait les traitements après remplissage de la table
		//Utilisé par CObjetServeurAvecBlob
		public virtual void AfterFill(DataTable table)
		{
		}

        /////////////////////////////////////////////////////////
        public void ClearCache()
        {
            CCacheLecture.ClearCache(IdSession, GetNomTable());
        }

		/////////////////////////////////////////////////////////
		public virtual CDataTableFastSerialize Read(CFiltreData filtre, int nStart, int nEnd, params string[] strChampsARetourner)
		{
            return ReadAndReturnDataTable(filtre, nStart, nEnd, strChampsARetourner);
		}

        public virtual DataTable ReadAndReturnDataTable(CFiltreData filtre, int nStart, int nEnd, params string[] strChampsARetourner)
        {
            if (UseCache)
                return ReadAvecCache(filtre, nStart, nEnd, strChampsARetourner);
            else
                return ReadSansCache(filtre, nStart, nEnd, strChampsARetourner);
        }


		/////////////////////////////////////////////////////////
		protected virtual DataTable ReadSansCache(CFiltreData filtre, int nStart, int nEnd, params string[] strChampsARetourner)
		{
            
            AssureRecepteurs();
			string strTri = filtre==null?"":filtre.SortOrder;
			//Préparation de la requête
			IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType() );
            if (filtre != null && (filtre.IntergerParentsHierarchiques || filtre.IntegrerFilsHierarchiques))
                strChampsARetourner = null;
			string strReq = GetRequeteSelectForRead(connexion, strChampsARetourner);			
			
			//Application du filtre sécurité
			CFiltreData filtreSecurite = GetFiltreSecurite();
			if (filtreSecurite != null )
				filtre = CFiltreData.GetAndFiltre ( filtre, filtreSecurite );
			
			//Gestion des versions à lire
			if (!typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()))
			{
				CFiltreData filtreVersion = new CFiltreData();
				//Lecture dans le référentiel
				if (m_nIdVersionTravail == null && (filtre == null || !filtre.IgnorerVersionDeContexte))
					filtreVersion.Filtre = CSc2iDataConst.c_champIdVersion + " is null";

				//Ignorer les suppressions
				if (filtre != null && !filtre.IntegrerLesElementsSupprimes)
				{
					if (filtreVersion.Filtre != "")
						filtreVersion.Filtre += " and ";
					filtreVersion.Filtre += CSc2iDataConst.c_champIsDeleted + "=@1";
					filtreVersion.Parametres.Add(false);
				}


				//Lecture dans une version
				if (m_nIdVersionTravail != null && (filtre == null || !filtre.IgnorerVersionDeContexte) && m_nIdVersionTravail >= 0)
				{
					if (filtre == null)
						filtre = new CFiltreData();
					filtre.IdsDeVersionsALire = CVersionDonnees.GetVersionsToRead(IdSession, (int)m_nIdVersionTravail);
				}

				if (filtreVersion.Filtre != "")
					filtre = CFiltreData.GetAndFiltre(filtre, filtreVersion);
			}

			DataSet ds = new DataSet();
			if ( strChampsARetourner == null || strChampsARetourner.Length == 0 )
			{
				using (DataTable table = FillSchema())
				    ds.Tables.Add((DataTable)table.Clone());
			}

			//Adaptation du filtre à la gestion de version
			if ( !typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()) && IdVersionDeTravail != null && (filtre == null || !filtre.IgnorerVersionDeContexte) && IdVersionDeTravail >= 0 )
			{
				//Lit les éléments du référentiel, de la version et des versions parentes
				int[]idsVersion = CVersionDonnees.GetVersionsToRead(IdSession,(int)IdVersionDeTravail);
				string strSepInListe = filtre is CFiltreDataAvance?";":",";
				StringBuilder bl = new StringBuilder();
				foreach ( int nId  in idsVersion )
				{
					bl.Append ( nId.ToString());
					bl.Append ( strSepInListe );
				}
				bl.Remove ( bl.Length-1,1);
				string strListeVersions = bl.ToString();

					
				if ( filtre is CFiltreDataAvance )
					filtre = CFiltreData.GetAndFiltre ( filtre,
						new CFiltreDataAvance ( GetNomTable(),
						"Hasno("+CSc2iDataConst.c_champIdVersion+") or "+
						CSc2iDataConst.c_champIdVersion+" in ("+strListeVersions+")"));
				else
					filtre = CFiltreData.GetAndFiltre ( filtre,
						new CFiltreData ( CSc2iDataConst.c_champIdVersion+" is null or "+
						CSc2iDataConst.c_champIdVersion+" in ("+strListeVersions+")"));
			}

			if ( filtre != null )
				filtre.SortOrder = strTri;

            //Stef 20/08/2013 : il faut conserver le tri en cas de lecture partielle
            if (strChampsARetourner != null && strChampsARetourner.Length > 0 &&
                nStart < 0 && nEnd < 0 )
                filtre.SortOrder = "";

            
			DataTable tbl = null;

            ///Indique qu'on n'a pas lu tout la table, mais uniquement entre nStart et nEnd
            ///(ce n'est pas forcement le cas car toutes les bdd ne peuvent pas le faire)
            bool bRemplissagePartiel = false;

            IDataAdapter adapter = connexion.GetSimpleReadAdapter(strReq, filtre);
            try
            {
                bool bUseCache = ActivateQueryCache && !bRemplissagePartiel && strChampsARetourner == null || strChampsARetourner.Length == 0;
                if (bUseCache)
                    bUseCache &=
                        (!(adapter is DbDataAdapter) && !(adapter is IDataAdapterARemplissagePartiel)) &&
                        (nStart < 0 && nEnd < 0);

                if (bUseCache)
                {
                    tbl = CCacheLecture.GetCache(IdSession, GetNomTable(), IdVersionDeTravail, false, filtre);
                }
                if (tbl == null)
                {

                    adapter.TableMappings.Add("Table", GetNomTableInDb());
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    try
                    {
                        if (nStart >= 0 &&
                            nEnd >= 0 &&
                            IdVersionDeTravail == null &&
                            !filtre.IntergerParentsHierarchiques &&
                            !filtre.NeConserverQueLesRacines &&
                            !filtre.IntegrerFilsHierarchiques)
                        //La lecture partielle ne peut pas fonctionner en mode version
                        {
                            if (adapter is DbDataAdapter)
                            {
                                //((DbDataAdapter)adapter).Fill(ds, nStart, nEnd - nStart + 1, GetNomTable());
                                connexion.FillAdapter((DbDataAdapter)adapter, ds, nStart, nEnd - nStart + 1, GetNomTable());
                                bRemplissagePartiel = true;
                            }
                            else if (adapter is IDataAdapterARemplissagePartiel)
                            {
                                //((IDataAdapterARemplissagePartiel)adapter).Fill(ds, nStart, nEnd - nStart + 1, GetNomTable());
                                connexion.FillAdapter((IDataAdapterARemplissagePartiel)adapter, ds, nStart, nEnd - nStart + 1, GetNomTable());
                                bRemplissagePartiel = true;
                            }
                        }
                        if (!bRemplissagePartiel)
                        {
                            if (ds.Tables.Count == 1 && GetNomTable() != GetNomTableInDb())
                                ds.Tables[0].TableName = GetNomTableInDb();
                            connexion.FillAdapter(adapter, ds);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(I.T("Reading error in database (filter : @1)|179",
                            (filtre == null ? I.T("Nothing|20") : filtre.Filtre)));
                        Console.WriteLine(e.ToString());
                        C2iEventLog.WriteErreur("DB reading error " + GetNomTable() + ":(" +
                            filtre.Filtre + ")\r\n" + e.ToString());
                        throw e;
                    }

                    tbl = ds.Tables[GetNomTable()];
                    if (tbl == null)
                    {
                        tbl = ds.Tables[GetNomTableInDb()];
                        if (tbl == null)
                            tbl = ds.Tables[0];
                        if (tbl != null)
                            tbl.TableName = GetNomTable();
                    }
                    if (tbl == null)
                    {
                        System.Console.WriteLine(I.T("Error : table @1 null \r\nfilter : @2|180", GetNomTable(), filtre.Filtre));
                    }
                    if (strChampsARetourner == null || strChampsARetourner.Length == 0)
                    {
                        AfterFill(tbl);
                        if (filtre != null && filtre.IntergerParentsHierarchiques)
                            IntegrerParentsHierarchiques(tbl);
                        if (filtre != null && filtre.IntegrerFilsHierarchiques)
                            IntegrerFilsHierarchiques(tbl);
                        if (filtre != null && filtre.NeConserverQueLesRacines)
                            NeConserverQueLesRacines(tbl);
                        if (m_nIdVersionTravail != null && (filtre == null || !filtre.IgnorerVersionDeContexte) && m_nIdVersionTravail >= 0)
                        {
                            CResultAErreur result = AdapteDatatableSurVersion(tbl, filtre.Filtre.Contains(tbl.PrimaryKey[0].ColumnName));
                            if (!result)
                            {
                                throw new CExceptionErreur(result.Erreur);
                            }
                        }
                        if (!bRemplissagePartiel && ActivateQueryCache && bUseCache)
                            CCacheLecture.CacheResult(IdSession, GetNomTable(), IdVersionDeTravail, filtre, false, tbl);
                    }
                }
            }
            finally
            {
                CUtilDataAdapter.DisposeAdapter(adapter);
            }


			nEnd = Math.Min ( nEnd, tbl.Rows.Count-1 );
			if ( nStart >= 0 && nEnd >= 0 && !bRemplissagePartiel)
			{
				DataTable tblNew = tbl.Clone();

				for ( int n = nStart; n <= nEnd; n++ )
					tblNew.ImportRow ( tbl.Rows[n] );
				tbl = tblNew;
			}
			return tbl;
		}

        protected void IntegrerParentsHierarchiques(DataTable table)
        {
            if (table.Rows.Count == 0)
                return;

            IObjetDonneeAutoReference objet = null;
            using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
            {
                objet = Activator.CreateInstance(GetTypeObjets(), new object[] { contexte }) as IObjetDonneeAutoReference;
                if (objet == null)
                    return;
            }
            string strChampCodeParent = objet.ChampParent;
            string strChampId = objet.GetChampId();
            if (!table.Columns.Contains(strChampCodeParent))
                return ;
            HashSet<int> codesConnus = new HashSet<int>();
            foreach (DataRow row in table.Rows)
                codesConnus.Add((int)row[strChampId]);
            

            if (table.PrimaryKey.Length == 0)
                table.PrimaryKey = new DataColumn[] { table.Columns[strChampId] };

            while (true)
            {
                HashSet<int> codesToLearn = new HashSet<int>();

                foreach (DataRow row in table.Rows)
                {
                    if (row[strChampCodeParent] != DBNull.Value)
                    {
                        int nIdParent = (int)row[strChampCodeParent];
                        if (!codesConnus.Contains(nIdParent))
                            codesToLearn.Add(nIdParent);
                    }
                }
                if (codesToLearn.Count == 0)
                    break;

                int nTaillePaquet = 100;
                int nIndex = 0;
                List<string> lstPaquets = new List<string>();
                StringBuilder blPaquet = new StringBuilder();
                foreach (int nCodeToLearn in codesToLearn)
                {
                    nIndex++;
                    if (nIndex >= nTaillePaquet)
                    {
                        blPaquet.Remove(blPaquet.Length - 1, 1);
                        lstPaquets.Add(blPaquet.ToString());
                        blPaquet = new StringBuilder();
                    }
                    blPaquet.Append(nCodeToLearn);
                    blPaquet.Append(',');
                }
                if (blPaquet.Length != 0)
                {
                    blPaquet.Remove(blPaquet.Length - 1, 1);
                    lstPaquets.Add(blPaquet.ToString());
                }
                foreach (string strPaquet in lstPaquets)
                {
                    CFiltreData filtre = new CFiltreData(strChampId + " in (" + strPaquet + ")");
                    using (DataTable tableMore = Read(filtre))
                    {
                        if (tableMore.PrimaryKey.Length == 0)
                            tableMore.PrimaryKey = new DataColumn[] { tableMore.Columns[strChampId] };
                        table.Merge(tableMore);
                    }
                }
                foreach (int nCodeToLearn in codesToLearn)
                    codesConnus.Add(nCodeToLearn);
            }
        }

        protected void IntegrerFilsHierarchiques(DataTable table)
        {
            if (table.Rows.Count == 0)
                return;

            IObjetHierarchiqueACodeHierarchique objet = null;
            using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
            {
                objet = Activator.CreateInstance(GetTypeObjets(), new object[] { contexte }) as IObjetHierarchiqueACodeHierarchique;
                if (objet == null)
                    return;
            }
            string strChampCodeSysteme = objet.ChampCodeSystemeComplet;
            if (!table.Columns.Contains(strChampCodeSysteme))
                return;
            HashSet<string> codesConnus = new HashSet<string>();

            int nTaillePaquet = 25;
            ArrayList lstRows = new ArrayList ( table.Rows );
            for ( int nIndex = 0; nIndex < lstRows.Count; nIndex += nTaillePaquet )
            {
                StringBuilder bl = new StringBuilder();
                int nMax = Math.Min ( nIndex+nTaillePaquet, lstRows.Count );
                List<string> lst = new List<string>();
                int nVariable = 1;
                for ( int nRow = nIndex; nRow < nMax; nRow++ )
                {
                    DataRow row = lstRows[nRow] as DataRow;
                    bl.Append ( strChampCodeSysteme );
                    bl.Append ( " like @");
                    bl.Append ( nVariable );
                    bl.Append(" or ");
                    lst.Add ( (string)row[strChampCodeSysteme]+"%" );
                    nVariable++;
                }
                if ( bl.Length > 0 )
                {
                    bl.Remove ( bl.Length - 4, 4);
                    CFiltreData filtre = new CFiltreData ( bl.ToString(), lst.ToArray() );
                    using (DataTable tableMore = Read(filtre))
                        table.Merge(tableMore);
                }
            }
        }

        protected void NeConserverQueLesRacines(DataTable tbl)
        {
            if (tbl.Rows.Count == 0)
                return;
            HashSet<string> codesConnus = new HashSet<string>();
            string strChampCodeSysteme = "";
            int nTailleParNiveau = 2;
            using (CContexteDonnee ctx = new CContexteDonnee(IdSession, true, false))
            {
                IObjetHierarchiqueACodeHierarchique objet = Activator.CreateInstance(GetTypeObjets(), new object[] { ctx }) as IObjetHierarchiqueACodeHierarchique;
                if (objet == null)
                    return;
                strChampCodeSysteme = objet.ChampCodeSystemeComplet;
                nTailleParNiveau = objet.NbCarsParNiveau;
            }
            foreach (DataRow row in tbl.Rows)
            {
                codesConnus.Add((string)row[strChampCodeSysteme]);
            }
            List<DataRow> lst = new List<DataRow>();
            foreach ( DataRow row in tbl.Rows )
                lst.Add ( row );

            foreach (DataRow row in lst)
            {
                string strCode = (string)row[strChampCodeSysteme];
                bool bDelete = false;
                while (strCode.Length > nTailleParNiveau)
                {
                    strCode = strCode.Substring(0, strCode.Length - nTailleParNiveau);
                    if (codesConnus.Contains(strCode))
                    {
                        bDelete = true;
                        break;
                    }
                }
                if (bDelete)
                    tbl.Rows.Remove(row);
            }
        }
            


		/////////////////////////////////////////////////////////
		public CResultAErreur VerifieDonnees ( CValiseObjetDonnee valise )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CObjetDonnee objet = valise.GetObjet();
                result = VerifieDonneesAvecFilles(objet);
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

		protected CResultAErreur VerifieDonneesFilles(DataRow row)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach ( CInfoRelation relation in CContexteDonnee.GetListeRelationsTable ( row.Table.TableName ))
			{
				if ( relation.Composition && relation.TableParente == GetNomTable() )
				{
					if ( row.Table.DataSet.Relations[relation.RelationKey] != null )
					{
						CObjetServeur objServeurFille = null;
						DataRow[] filles = row.GetChildRows(relation.RelationKey);
						foreach (DataRow fille in filles)
						{
							if (fille.RowState != DataRowState.Deleted)
							{
								CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(CContexteDonnee.GetTypeForTable(fille.Table.TableName), new object[] { fille });
								if (objServeurFille == null)
									objServeurFille = (CObjetServeur)objet.GetLoader();
								if ( objet.Row.RowState == DataRowState.Added || objet.Row.RowState == DataRowState.Modified )
									result = objServeurFille.VerifieDonnees(objet);
								if (!result)
									result.EmpileErreur(new CErreurValidation (I.T("Error on @1|20004", objet.DescriptionElement), false));
								if (result)
									result = objServeurFille.VerifieDonneesFilles(objet.Row.Row);
								if ( !result )
									return result;
							}
						}
					}
				}
			}
			return result;
		}


		
		//////////////////////////////////////////////////
		public bool IsUnique(CObjetDonnee objet, string[] tableStrChamp, string[] tableStrValeurChamp)
		{
			CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(objet.GetChampsId(), objet.Row);
			filtre.Filtre = "NOT(" + filtre.Filtre + ") ";
			int i=1;
			foreach (string strChamp in tableStrChamp)
			{
				i++;
				filtre.Filtre += " AND (" +strChamp+" like @" + i.ToString() + ")";
			}
			foreach (string strValeurChamp in tableStrValeurChamp)
			{
				filtre.Parametres.Add ( strValeurChamp );
			}
            DataTable table = objet.ContexteDonnee.GetTableSafe(CContexteDonnee.GetNomTableForType (objet.GetType()));
            if ( table.Columns.Contains ( CSc2iDataConst.c_champIsDeleted ) )
                filtre = CFiltreData.GetAndFiltre ( filtre,
                    new CFiltreData ( CSc2iDataConst.c_champIsDeleted+"=@1", false));
			IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion (IdSession, GetType());
			IDataAdapter adapter = connexion.GetSimpleReadAdapter( "select 1 from "+objet.GetNomTableInDb(), filtre );
			DataSet ds = new DataSet();
            connexion.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			return (ds.Tables["Table"].Rows.Count == 0);
		}		

		//////////////////////////////////////////////////
		public int CountRecords ( string strNomTableInContexte, CFiltreData filtre )
		{
			if (IdVersionDeTravail == null && !filtre.IntergerParentsHierarchiques && !filtre.NeConserverQueLesRacines && !filtre.IntegrerFilsHierarchiques)
			{
				CFiltreData filtreSecurite = GetFiltreSecurite();
				if (filtreSecurite != null)
					filtre = CFiltreData.GetAndFiltre(filtre, filtreSecurite);
				if ((filtre == null || !filtre.IgnorerVersionDeContexte)
				&& !typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()))
					filtre = CFiltreData.GetAndFiltre(filtre, new CFiltreData(CSc2iDataConst.c_champIdVersion + " is null"));
                filtre.SortOrder = "";//Pour le cache
				string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTableInContexte);
                DataTable cache = CCacheLecture.GetCache(IdSession, strNomTableInContexte, null, true, filtre);
                if (cache != null)
                    return (int)cache.Rows[0][0];
                else
                {
                    int nVal = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType()).CountRecords(strNomTableInDb, filtre);
                    DataTable tableTmp = new DataTable();
                    tableTmp.Columns.Add("COUNT", typeof(int));
                    DataRow row = tableTmp.NewRow();
                    row[0] = nVal;
                    tableTmp.Rows.Add(row);
                    CCacheLecture.CacheResult(IdSession, strNomTableInContexte, null, filtre, true, tableTmp);
                    return nVal;
                }
			}
			else
			{
				//Quand on est dans une version, on est obligé de passer par une liste
				//car c'est le seul objet qui garantit qu'on n'aura que les éléments 
				//voulus.
				/*par exemple, IsUnique de CObjetDonneeAIdNumeriqueAuto demande les
				 * éléments qui n'ont pas un champ pareil, et un id différent
				 * si des éléments de version on un champ commun, ils sont retournés,
				 * mais comme AdapteDataTableSurVersion change les ids, on se retrouve
				 * avec un count sur des objets qui ont l'id qu'on ne veut pas
				 * et le count retourne donc 1 !
				 * filtre = ChampId<>32 and libelle="TOTO"
				 * s'il existe dans la version en cours un objet dérivé du 32,
				 * avec comme libellé TOTO, cet objet va être retourné par le read
				 * avec comme id 32, et il sera dans le résultat du read
				 * or, on n'en veut pas. 
				 * En passant par CListeObjetDonnees, cet élément sera supprimé
				 * */
				using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
				{
					contexte.SetVersionDeTravail(IdVersionDeTravail, false);
                    //On n'applique pas le filtre de base, car s'il doit être appliqué, il est déjà dans le filtre passé à cette fonction
					CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, CContexteDonnee.GetTypeForTable(strNomTableInContexte), false);
					liste.Filtre = filtre;
					return liste.Count;
				}
			}
					
		}

		//////////////////////////////////////////////////
		public CResultAErreur ExecuteScalar ( string strSelectClause, CFiltreData filtre )
		{
			CFiltreData filtreSecurite = GetFiltreSecurite();
			if ( filtreSecurite != null )
				filtre = CFiltreData.GetAndFiltre ( filtre, filtreSecurite );
			return CSc2iDataServer.GetInstance().GetDatabaseConnexion ( IdSession, GetType() ).ExecuteScalar ( strSelectClause, GetNomTableInDb(), filtre );
		}

		//////////////////////////////////////////////////
		public bool DesactiverIdentifiantAutomatique
		{
			get
			{
				return m_bDesactiverIdAuto;
			}
			set
			{
				m_bDesactiverIdAuto = value;
			}
		}

		//////////////////////////////////////////////////
		public bool DesactiverContraintes
		{
			get
			{
				return m_bDesactiverContraintes;
			}
			set
			{
				m_bDesactiverContraintes = value;
			}
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Réalise les fonctions de base du traitement avant sauvegarde, et 
		/// lance le traitement avant sauvegarde standard
		/// 
		/// </summary>
		/// <param name="contexte"></param>
		/// <returns></returns>
		internal virtual CResultAErreur TraitementTotalAvantSauvegarde ( CContexteDonnee contexte )
		{
			CResultAErreur result = UniqueAttribute.VerifieUnicite(contexte, GetTypeObjets(), DataRowState.Modified | DataRowState.Added);

			//Lance les verifies données au moment de la sauvegarde
			DataTable table = contexte.Tables[GetNomTable()];
			if (table == null)
				return result;
			if (!table.Columns.Contains(CObjetDonnee.c_champVerifierDonneesALaSauvegarde))
				return result;
			DataRow[] rows = table.Select(CObjetDonnee.c_champVerifierDonneesALaSauvegarde + "=1");
			foreach (DataRow row in rows)
			{
				if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
				{
					CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(GetTypeObjets(), row);
					result = VerifieDonneesAvecFilles(objet);
					if (!result)
					{
						if (contexte.IgnoreAvertissementsALaSauvegarde)//Ignorer les avertissements
						{
							bool bGrave = false;
							foreach (IErreur erreur in result.Erreur)
							{
								if (!(erreur is CErreurValidation) || !((CErreurValidation)erreur).IsAvertissement)
								{
									bGrave = true;
									break;
								}
							}
							if (!bGrave)//Rien de grave
								result = CResultAErreur.True;
						}
						if (!result)
							return result;
					}
					objet.Row[CObjetDonnee.c_champVerifierDonneesALaSauvegarde] = false;
				}
			}
			if (result)
				result = TraitementAvantSauvegarde(contexte);
			return result;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Execute le traitement de base apressauvegarde, et lance le traitement après sauvegarde
		/// </summary>
		/// <param name="contexte"></param>
		/// <param name="bOperationReussie"></param>
		/// <returns></returns>
		internal virtual CResultAErreur TraitementTotalApresSauvegarde(CContexteDonnee contexte, bool bOperationReussie)
		{
			CResultAErreur result = CResultAErreur.True;
			return TraitementApresSauvegarde(contexte, bOperationReussie);
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Permet de faire un traitement sur le contexte avant la sauvegarde des données
		/// Tous les objets serveurs concernés par une sauvegarde sont appelés
		/// dans l'ordre de dépendance allant des composants vers les composés (les composants d'abord)
		/// </summary>
		/// <param name="contexte"></param>
		/// <returns></returns>
		public virtual CResultAErreur TraitementAvantSauvegarde ( CContexteDonnee contexte )
		{
			return CResultAErreur.True;
		}


		

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Permet de purger des choses qui auraient été créées pour la sauvegarde
		/// </summary>
		/// <param name="contexte"></param>
		/// <param name="bOperationReussie">Indique que l'opération de sauvegarde est un succès</param>
		/// <returns></returns>
		public virtual CResultAErreur TraitementApresSauvegarde ( CContexteDonnee contexte, bool bOperationReussie )
		{
			return CResultAErreur.True;
		}

		/// <summary>
		/// Retourne le journaliseur de champs capable de journaliser les données des objets de ce type
		/// </summary>
		public virtual IJournaliseurDonneesObjet JournaliseurChamps
		{
			get
			{
				if ( typeof(IObjetSansVersion).IsAssignableFrom ( GetTypeObjets() ))
					return null;
				return new CJournaliseurVersionObjetChampsDb();
			}
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Copie les modifications faites sur les version filles de la version en cours
		/// </summary>
		/// <param name="contexteSauvegarde"></param>
		/// <returns></returns>
		private CResultAErreur RepercuteModifsSurVersionsPrevisionnelles(CContexteSauvegardeObjetsDonnees contexteSauvegarde)
		{
			CContexteDonnee contexte = contexteSauvegarde.ContexteDonnee;	
			CResultAErreur result = CResultAErreur.True;
			if ( !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom ( GetTypeObjets() ) )
				return result;
			
			if (contexte.IdVersionDeTravail != null && contexte.IdVersionDeTravail < 0)
				return result;

			IJournaliseurDonneesObjet journaliseur = JournaliseurChamps;
			if ( journaliseur == null )
				return result;

			//Versions pour laquelle on modifie les objets dérivés
			StringBuilder blVersionsEnCours = new StringBuilder();
			if (contexte.IdVersionDeTravail != null)
				blVersionsEnCours.Append(contexte.IdVersionDeTravail.ToString());

			//Versions dérivées des versions en cours
			StringBuilder blIdsVersionsDerivees = new StringBuilder();

			//Liste des objets pour lesquels on cherche les dérivés
			List<int> listeIdsObjets = new List<int>();
//			StringBuilder blIdsObjets = new StringBuilder();
			DataTable table = contexte.Tables[GetNomTable()];
			if (table == null)
				return result;
			string strPrimKey = table.PrimaryKey[0].ColumnName;
			
			List<DataRow> rowsModifiees = new List<DataRow>();
			foreach (DataRow row in table.Rows)
			{
				if (row.RowState == DataRowState.Modified)
				{
					if (row[CSc2iDataConst.c_champOriginalId] == DBNull.Value)
						listeIdsObjets.Add((int)row[strPrimKey]);
					//blIdsObjets.Append(row[strPrimKey]);
					else
						listeIdsObjets.Add((int)row[CSc2iDataConst.c_champOriginalId]);
						//blIdsObjets.Append(row[CSc2iDataConst.c_champOriginalId]);
					rowsModifiees.Add(row);
				}
			}
			if (listeIdsObjets.Count == 0)
				return result;
			//blIdsObjets.Remove(blIdsObjets.Length - 1,1);

			Dictionary<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> lastDicoModifs = null;
			while (rowsModifiees.Count > 0)
			{
				//Recherche toutes les versions dérivées des versions en cours
				CListeObjetsDonnees listeVersionsPrevisionnelles = new CListeObjetsDonnees(contexte, typeof(CVersionDonnees));

				CFiltreData filtre;
				if (blVersionsEnCours.Length == 0)
				{
					filtre = new CFiltreData(
				   CVersionDonnees.c_champTypeVersion + "=@1 and " +
				   CVersionDonnees.c_champIdVersionParente + " is null",
				   (int)CTypeVersion.TypeVersion.Previsionnelle);
				}
				else
					filtre = new CFiltreData(
						CVersionDonnees.c_champTypeVersion + "=@1 and " +
						CVersionDonnees.c_champIdVersionParente + " in ("+blVersionsEnCours+")",
						(int)CTypeVersion.TypeVersion.Previsionnelle);
				filtre.IgnorerVersionDeContexte = true;
				listeVersionsPrevisionnelles.Filtre = filtre;
				
				//IdVersion->IdVersionParente
				Dictionary<int, List<int>> dicoVersionParenteToFilles = new Dictionary<int, List<int>>();
				foreach (CVersionDonnees version in listeVersionsPrevisionnelles)
				{
					blIdsVersionsDerivees.Append(version.Id);
					blIdsVersionsDerivees.Append(",");
					if ( version.Row[CVersionDonnees.c_champIdVersionParente] != DBNull.Value )
					{
						List<int> lstFilles = null;
						int nIdParent = (int)version.Row[CVersionDonnees.c_champIdVersionParente];
						if ( !dicoVersionParenteToFilles.TryGetValue(nIdParent, out lstFilles ))
						{
							lstFilles = new List<int>();
							dicoVersionParenteToFilles[nIdParent] = lstFilles;
						}
						lstFilles.Add ( version.Id );
					}
				}

				if (blIdsVersionsDerivees.Length == 0)
					return result;//Pas de version dérivée

				blIdsVersionsDerivees.Remove(blIdsVersionsDerivees.Length - 1, 1);

				//CRée le dictionnaire des modifications pour les versions traitées
				//IdVersion->Id objet->Champs
				Dictionary<int,Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> dicoChampsModifies = journaliseur.GetDictionnaireChampsModifies(
					IdSession,
					blIdsVersionsDerivees.ToString(),
					GetTypeObjets(),
					rowsModifiees.ToArray());

				if (lastDicoModifs != null)
				{
					//Ajoute les modifs des versions précédentes dans les versions dérivées
					//Parcours les modifs parente
					foreach (KeyValuePair<int, Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>> modifVersionParent in lastDicoModifs)
					{
						//Pour chaque modif parente, ajoute les modifications dans les versions filles
						List<int> listeFilles = null;
						dicoVersionParenteToFilles.TryGetValue ( modifVersionParent.Key, out listeFilles );
						if ( listeFilles != null )
						{
							foreach ( int nIdFille in listeFilles )
							{
								//Cherche les modifs pour la version fille
								Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoVersionFille = null;
								if (dicoChampsModifies.TryGetValue(nIdFille, out dicoVersionFille))
								{
									//Il y a des modifs pour la version fille
									//Parcours les modifs d'élément de la version parente
									foreach ( KeyValuePair<int, Dictionary<CReferenceChampPourVersion,bool>> modifElementVParent in modifVersionParent.Value )
									{
										//Cherche les modifs de cet élément dans la version fille
										Dictionary<CReferenceChampPourVersion, bool> modifElementVFille = null;
										if ( dicoVersionFille.TryGetValue ( modifElementVParent.Key, out modifElementVFille ) )
										{
											//Il y a des modifs pour cet élément, ajoute les modifs
											//de la version parente
											foreach (KeyValuePair<CReferenceChampPourVersion,bool> modifChamp in modifElementVParent.Value )
												modifElementVFille[modifChamp.Key] = true;
										}
										else
											//La version fille n'a pas modifié l'élément,
											//Ajoute simplement les modifs de l'élément par la version parente
											dicoVersionFille.Add ( modifElementVParent.Key, modifElementVParent.Value );
									}									
								}
								else
									//S'il n'y a pas de modif pour la version fille, ajoute simplement
									//les modifs de la version parente comme modifs de la version fille
									dicoChampsModifies.Add ( nIdFille, modifVersionParent.Value );
							}
						}
					}
				}
				lastDicoModifs = dicoChampsModifies;


				rowsModifiees.Clear();
				//Recherche les éléments dérivés des éléments modifiés
				//Traitement par paquets de 500 éléments
				for (int nIndexLot = 0; nIndexLot < listeIdsObjets.Count; nIndexLot += 500) 
				{
					StringBuilder blIdsObjets = new StringBuilder();
					int nMin = Math.Min ( listeIdsObjets.Count, nIndexLot+500);
					for ( int nIndex = nIndexLot; nIndex < nMin; nIndex++ )
					{
						blIdsObjets.Append(listeIdsObjets[nIndex]);
						blIdsObjets.Append(",");
					}
					if ( blIdsObjets.Length > 0 )
					{
						blIdsObjets.Remove ( blIdsObjets.Length-1, 1);
						CListeObjetsDonnees listeObjets = new CListeObjetsDonnees(contexte, GetTypeObjets());
						listeObjets.Filtre = new CFiltreData(CSc2iDataConst.c_champIdVersion + " in (" + blIdsVersionsDerivees.ToString() + ") and " +
							CSc2iDataConst.c_champOriginalId + " in (" + blIdsObjets.ToString() + ")");
						listeObjets.Filtre.IgnorerVersionDeContexte = true;

						
						foreach (CObjetDonneeAIdNumerique objetBis in listeObjets.ToArrayList())
						{
							Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>> dicoDeVersion = null;
							if (objetBis.IdVersionDatabase != null)
							{
								dicoChampsModifies.TryGetValue((int)objetBis.IdVersionDatabase, out dicoDeVersion);
							}
							if (dicoDeVersion == null)
								dicoDeVersion = new Dictionary<int, Dictionary<CReferenceChampPourVersion, bool>>();
							DataRow rowReference = table.Rows.Find(objetBis.Row[CSc2iDataConst.c_champOriginalId]);
							DataRow rowCopie = objetBis.Row.Row;
							if (rowReference != null)
							{
								journaliseur.RepercuteModifsSurVersionFuture(rowReference, rowCopie, dicoDeVersion);
								//Stocke la nouvelle ligne modifiée pour la passe suivante
								rowsModifiees.Add(rowCopie);
							}
						}
					}
				}
				

				//On traite maintenant les versions dérivées des versions dérivées
				blVersionsEnCours = blIdsVersionsDerivees;
				blIdsVersionsDerivees = new StringBuilder();

			}
			return result;
		}

        public delegate void BeforeOrAfterSaveExterneDelegate(
            DataTable table, 
            Hashtable tableParametres,
            ref CResultAErreur result);

        public static BeforeOrAfterSaveExterneDelegate BeforeSaveExterne;
        public static BeforeOrAfterSaveExterneDelegate AfterSaveExterne;

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Code executé juste avant le adapter.Update
		/// </summary>
		/// <param name="adapter"></param>
		/// <param name="contexte"></param>
		/// <param name="etatsAPrendreEnCompte"></param>
		/// <returns></returns>
		public virtual CResultAErreur BeforeSave(CContexteSauvegardeObjetsDonnees contexte, IDataAdapter adapter, DataRowState etatsAPrendreEnCompte)
		{
			return CResultAErreur.True;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Code executé juste après le adapter.Update
		/// </summary>
		/// <param name="adapter"></param>
		/// <param name="contexte"></param>
		/// <param name="etatsAPrendreEnCompte"></param>
		/// <returns></returns>
		public virtual CResultAErreur AfterSave(CContexteSauvegardeObjetsDonnees contexte, IDataAdapter adapter, DataRowState etatsAPrendreEnCompte)
		{
			return CResultAErreur.True;
		}


		/////////////////////////////////////////////////////////
		public virtual CResultAErreur SaveAll ( CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataRowState etatsAPrendreEnCompte )
		{
			CRestrictionUtilisateurSurType restrictionSurType = new CRestrictionUtilisateurSurType(GetTypeObjets() );

			CResultAErreur result = CResultAErreur.True;
			IDatabaseConnexion db = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType());
			IDataAdapter adapter = null;
			try
			{
				DataTable table = contexteSauvegarde.ContexteDonnee.Tables[GetNomTable()];

				/*SC 07/03/2008 : ne pas sauver les tables nulles ou vides !*/
				if (table == null || table.Rows.Count == 0)
					return result;
                bool bHasRows = false;
                foreach (DataRow row in table.Rows)
                {
                    if ((row.RowState & etatsAPrendreEnCompte) == row.RowState)
                    {
                        bHasRows = true;
                        break;
                    }
                }
                if (!bHasRows)
                    return result;
				
				//Vérifie la présence des champs
				restrictionSurType = contexteSauvegarde.Restrictions.GetRestriction(GetTypeObjets() );
				CStructureTable structure = CStructureTable.GetStructure(GetTypeObjets());
                
                //Stef 17/11/2009 : on ne complète plus les restrictions sur la structure,
                //car sinon, on ne tient pas compte du AppliqueSurObjet
                Dictionary<string, List<string>> dicNomPropToChamps = structure.GetDicProprieteToChampsLies();

				//Stef 13/11/2008 : gestion des restrictions sur objets				
				CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
				IInfoUtilisateur user = session.GetInfoUtilisateur();
				bool bHasRestrictionsParObjets = restrictionSurType.ContextesException.Length != 0;
				if (user != null && !bHasRestrictionsParObjets)
				{
					bHasRestrictionsParObjets = user.GetTypesARestrictionsSurObjets(contexteSauvegarde.ContexteDonnee.IdVersionDeTravail).Contains(GetTypeObjets());
					if (contexteSauvegarde.ContexteDonnee.IdVersionDeTravail < 0 && !bHasRestrictionsParObjets)//On ne sait pas si on est dans le référentiel ou pas
					{
						//On ajoute donc les types à restriction pour le référentiel
						bHasRestrictionsParObjets |= user.GetTypesARestrictionsSurObjets(null).Contains(GetTypeObjets());
					}
				}

				ArrayList lstChampsNotToUpdate = new ArrayList();
				foreach ( CInfoChampTable champ in structure.Champs )
				{
					//Stef 190504 : enleve le test sur readonly :  sinon, le champ id par exemple
					//Qui est en readonly n'est plus utilisé dans la clause where !!
					/*if ( table.Columns[champ.NomChamp] == null || champ.ReadOnly)
						lstChampsNotToUpdate.Add ( champ.NomChamp );*/
					if ( table.Columns[champ.NomChamp] == null)
						lstChampsNotToUpdate.Add ( champ.NomChamp );

				}
				
				
				
				db.PrepareTableToWriteDatabase ( contexteSauvegarde.ContexteDonnee.Tables[GetNomTable()] );
				if ( m_bDesactiverIdAuto )
					db.DesactiverIdAuto ( GetNomTableInDb(), true );
				if ( m_bDesactiverContraintes )
					db.DesactiverContraintes ( true );

				//Crée la table des colonnes en readonly
				//Nom de champ->True
				Hashtable tableReadOnlySansRestSurObjet = new Hashtable();
                foreach (string strProp in restrictionSurType.GetProprietesReadOnly())
                {
                    List<string> lst = null;
                    if (dicNomPropToChamps.TryGetValue(strProp, out lst))
                        foreach (string strChamp in lst )
                            tableReadOnlySansRestSurObjet[strChamp] = true;
                }
               
				
#if !PDA
				//Stef le 19/11/04 : LEs notifications sont maintenant faites ici, comme ça,
				//on est sur que tout ce qui entre dans la base envoie des notifications !!
				bool bHasAdds = false;
				DataColumn[] primKeys = table.PrimaryKey;


				#region vérification des droits
				foreach ( DataRow row in new ArrayList(table.Rows) )
				{
                    Hashtable tableReadOnly = tableReadOnlySansRestSurObjet;
					CRestrictionUtilisateurSurType restrictionSurObjet = (CRestrictionUtilisateurSurType)restrictionSurType.Clone();
					if (bHasRestrictionsParObjets && row.RowState != DataRowState.Unchanged)
					{
						//Crée l'objet
						try
						{
							CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(GetTypeObjets(), row);
							restrictionSurObjet.ApplyToObjet(objet);
                            //Calcule les restrictions sur champs
                            tableReadOnly = new Hashtable();
                            foreach (string strProp in restrictionSurObjet.GetProprietesReadOnly())
                            {
                                List<string> lst = null;
                                if (dicNomPropToChamps.TryGetValue(strProp, out lst))
                                    foreach (string strChamp in lst)
                                        tableReadOnly[strChamp] = true;
                            }
						}
						catch
						{
						}
					}
					bHasAdds |= row.RowState == DataRowState.Added;
					if ( row.RowState == DataRowState.Modified || row.RowState == DataRowState.Deleted )
					{
						object[] keys = new object[primKeys.Length];
						int nIndex = 0;
						DataRowVersion version = DataRowVersion.Current;
						if ( row.RowState == DataRowState.Deleted )
							version = DataRowVersion.Original;
						foreach ( DataColumn col in primKeys )
							keys[nIndex++] = row[col, version];
						contexteSauvegarde.DonneeNotification.AddModifiedRecord ( table.TableName, row.RowState == DataRowState.Deleted, keys );
						if (row.RowState == DataRowState.Modified && restrictionSurObjet.HasRestrictions)
						{
							if (!restrictionSurObjet.CanModifyType())
							{
								result.EmpileErreur(I.T("You aren't authorized to modify the @1 type elements|181",
									DynamicClassAttribute.GetNomConvivial ( GetTypeObjets() )));
								return result;
							}
							bool bOldContraintes = contexteSauvegarde.ContexteDonnee.EnforceConstraints;
							contexteSauvegarde.ContexteDonnee.EnforceConstraints = false;
							foreach ( string strChamp in tableReadOnly.Keys )
								row[strChamp] = row[strChamp, DataRowVersion.Original];
							try
							{
								contexteSauvegarde.ContexteDonnee.EnforceConstraints = bOldContraintes;
							}
							catch
							{
								//On a un pb, à tous les coups, c'est un parent qui n'est pas là
								if ( contexteSauvegarde.ContexteDonnee is CContexteDonnee )
								{
									CContexteDonnee contexteDonnee = (CContexteDonnee)contexteSauvegarde.ContexteDonnee;
									contexteDonnee.AssureParents(row);
								}
								contexteSauvegarde.ContexteDonnee.EnforceConstraints = bOldContraintes;
							}
						}
						if ( row.RowState == DataRowState.Deleted )
						{
							if (!restrictionSurObjet.CanDeleteType())
							{
								result.EmpileErreur(I.T("You aren't authorized to modify the @1 type elements|181",
									DynamicClassAttribute.GetNomConvivial ( GetTypeObjets() )));
								return result;
							}
						}
					}
					if (row.RowState == DataRowState.Added &&
						!restrictionSurObjet.CanCreateType())
					{
						result.EmpileErreur(I.T("You are not allowed to create elements of the type @1|30111",DynamicClassAttribute.GetNomConvivial(GetTypeObjets())));
						return result;
					}

				}
				if ( bHasAdds )
				{
					CDonneeNotificationAjoutEnregistrement notif = new CDonneeNotificationAjoutEnregistrement(IdSession, GetNomTable() );
					new CGestionnaireNotification(IdSession).EnvoieNotifications(new IDonneeNotification[] { notif });
				}
				#endregion
#endif
				if ( !result )
					return result;

				#region Prépare la mise à jour des relations typeId
				//Pour mise à jour des Ids des relations typeId
				//Stocke les relationTypeId pour lesquels il faut mettre
				//à jour les ids pour les lignes ajoutées
				Dictionary<RelationTypeIdAttribute, Dictionary<DataRow, DataRow>> lstTypeId = new Dictionary<RelationTypeIdAttribute,Dictionary<DataRow,DataRow>>();
				string strChampId = "";
				RelationVersionDonneesObjetAttribute attributRelIdVersion = null;
				if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(GetTypeObjets()) &&
					(IdVersionDeTravail >= 0 ||
					GetTypeObjets().GetCustomAttributes(typeof(NoRelationTypeIdAttribute),true).Length== 0 ))
					/*Stef, 040908 : l'objet peut ne pas avoir de RelationTypeId, mais
					 * si on est dans une version, il utilise quand même un relationTypeId pour
					 * le lien au CVersionDonneeObjet, donc on ne teste par le RelationTypeId
					 * en mode version*/
					 
				{
					foreach (DataRow row in new ArrayList(table.Rows))
					{
						if (row.RowState == DataRowState.Added)
						{
							foreach (RelationTypeIdAttribute attrib in CContexteDonnee.RelationsTypeIds)
							{
								if (attrib is RelationVersionDonneesObjetAttribute)
									attributRelIdVersion = (RelationVersionDonneesObjetAttribute)attrib;
                                //Si la table n'existe pas, on ignore (c'est qu'il n'y a pas de données dedans !)
                                if (contexteSauvegarde.ContexteDonnee.Tables.Contains(attrib.TableFille))
                                {
                                    CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(GetTypeObjets(), row);
                                    strChampId = objet.GetChampId();
                                    CListeObjetsDonnees listeRelsIds = objet.GetDependancesRelationTypeId(
                                        attrib.TableFille,
                                        attrib.ChampType,
                                        attrib.ChampId,
                                        false,
                                        true);
                                    foreach (CObjetDonnee rel in listeRelsIds)
                                    {
                                        Dictionary<DataRow, DataRow> lstRows = null;
                                        if (lstTypeId.ContainsKey(attrib))
                                            lstRows = lstTypeId[attrib];
                                        else
                                        {
                                            lstRows = new Dictionary<DataRow, DataRow>();
                                            lstTypeId[attrib] = lstRows;
                                        }
                                        //Map les rows liées sur le row principal
                                        lstRows[rel.Row] = row;
                                    }
                                }
							}
						}
					}
				}

				#endregion

                #region Prépare le mappage d'ids de synchro
                /*CODE OBSOLETE AVEC CDBKey
                Dictionary<DataRow, DataRow> dicMappageIdsSynchroAMettreAJour = new Dictionary<DataRow,DataRow>();
                if ( typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(GetTypeObjets() ) &&
                    contexteSauvegarde.ContexteDonnee.Tables.Contains ( CMapIdMainToIdSecInDb.c_nomTable))
                {
                    CListeObjetDonneeGenerique<CMapIdMainToIdSecInDb> lstMaps = new CListeObjetDonneeGenerique<CMapIdMainToIdSecInDb>(contexteSauvegarde.ContexteDonnee);
                    lstMaps.Filtre = new CFiltreData ( CMapIdMainToIdSecInDb.c_champNomTable+"=@1",
                        table.TableName );
                    lstMaps.InterditLectureInDB = true;
                    foreach ( CMapIdMainToIdSecInDb map in lstMaps )
                    {
                        if ( map.IdInSecondary < 0 )//Nouvel élément
                        {
                            DataRow row = table.Rows.Find ( map.IdInSecondary );
                            if (row != null)
                                dicMappageIdsSynchroAMettreAJour[row] = map.Row;
                            else
                            {
                                result.EmpileErreur("System error : missing row for synchronisation " + map.IdInSecondary);
                                return result;
                            }
                        }
                    }
                }
                 * */
                #endregion

                #region Journalisation des données
                //Journalisation des données
				if (
                    !contexteSauvegarde.ContexteDonnee.DisableHistorisation && 
                    CVersionDonneesObjet.EnableJournalisation && 
                    contexteSauvegarde.ContexteDonnee.IdVersionDeTravail == null)
				{
					IJournaliseurDonneesObjet journaliseur = JournaliseurChamps;
					if (journaliseur != null)
					{
						foreach (DataRow row in table.Rows)
						{
							if ((row.RowState & etatsAPrendreEnCompte) == row.RowState &&
								!(bool)row[CSc2iDataConst.c_champIsDeleted])//Pas d'archivage de supp, car il est fait par CContexteDonneeServeur
							{
								CVersionDonneesObjet version = journaliseur.JournaliseDonnees(row, contexteSauvegarde.VersionDonneesAssociee);
								if (version != null && contexteSauvegarde.VersionDonneesAssociee != null)
								{
									if (attributRelIdVersion != null && row.RowState == DataRowState.Added &&
										version.IdElement == (int)row[row.Table.PrimaryKey[0].ColumnName])
									{
										Dictionary<DataRow, DataRow> lstRows = null;
										if (lstTypeId.ContainsKey(attributRelIdVersion))
											lstRows = lstTypeId[attributRelIdVersion];
										else
										{
											lstRows = new Dictionary<DataRow, DataRow>();
											lstTypeId[attributRelIdVersion] = lstRows;
										}
										lstRows[version.Row.Row] = row;
									}
								}
							}
						}
					}
				}
				#endregion

				#region Répercution des modifications sur les versions prévisionnelles
				result = RepercuteModifsSurVersionsPrevisionnelles(contexteSauvegarde);
				if ( !result )
					return result;
				#endregion

				adapter = GetDataAdapter(etatsAPrendreEnCompte, (string[])lstChampsNotToUpdate.ToArray(typeof(string)));
                try
                {
                    result = BeforeSave(contexteSauvegarde, adapter, etatsAPrendreEnCompte);
                    if (!result)
                        return result;
                    Hashtable tableBeforeOrAfterExterne = new Hashtable();
                    if (BeforeSaveExterne != null)
                        BeforeSaveExterne(table, tableBeforeOrAfterExterne, ref result);
                    if (!result)
                        return result;
                    //Correction notamment pour access, parfois,
                    //la connexion échoue et il faut simplement
                    //vider le garbage collector
                    int nNb = 0;
                    int nNbRetry = 3;
                    while (nNbRetry > 0)
                    {
                        try
                        {
                            nNb = adapter.Update(contexteSauvegarde.ContexteDonnee);
                            break;
                        }
                        catch (Exception ex)
                        {
                            GC.Collect();
                            nNbRetry--;
                            if (nNbRetry == 0)
                                throw ex;
                        }
                    }
                    if (AfterSaveExterne != null)
                        AfterSaveExterne(table, tableBeforeOrAfterExterne, ref result);
                    if (!result)
                        return result;
                    result = AfterSave(contexteSauvegarde, adapter, etatsAPrendreEnCompte);
                    if (!result)
                        return result;
                }
                finally
                {
                    CUtilDataAdapter.DisposeAdapter(adapter);
                }
                
				

				//Change les ids des relationsTypeId
				if ( lstTypeId.Count >  0 && strChampId != "")
				{
					foreach ( KeyValuePair<RelationTypeIdAttribute,Dictionary<DataRow, DataRow>> listeRels in lstTypeId )
					{
						foreach ( KeyValuePair<DataRow, DataRow> map in listeRels.Value )
						{
							map.Key[listeRels.Key.ChampId] = map.Value[strChampId];
						}
					}
				}

                /*Code Obsolete avec CDbKey
                //Change les ids des mappage de synchro
                foreach ( KeyValuePair<DataRow, DataRow> mapIds in dicMappageIdsSynchroAMettreAJour )
                {
                    mapIds.Value[CMapIdMainToIdSecInDb.c_champSecId] = mapIds.Key[table.PrimaryKey[0]];
                }
                 * */


                ClearCache();
				
				result.Data = table;
				
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException (e ) );
				result.EmpileErreur("Error while saving @1|30012",GetNomTable());
			}
			finally
			{
				if ( m_bDesactiverContraintes )
					db.DesactiverContraintes ( false );
				if ( m_bDesactiverIdAuto )
					db.DesactiverIdAuto ( GetNomTableInDb(), false );
			}
			return result;
		}

        //////////////////////////////////////////////////
        public static void ClearCacheSchemas()
        {
            if (m_tableSchema != null)
            {
                m_tableSchema.Clear();
                CContexteDonnee.ClearCacheSchemas();
                C2iOracleDataAdapter.ClearCacheSchemas();
            }
        }

		//////////////////////////////////////////////////
		protected static Hashtable m_tableSchema = new Hashtable();
		public virtual CDataTableFastSerialize FillSchema()
		{
			DataTable tbl = (DataTable)m_tableSchema[GetNomTable()];
			if ( tbl != null )
				return tbl.Clone();
			IDataAdapter adapter = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType()).GetTableAdapter(GetNomTableInDb());
			adapter.TableMappings.Add ("Table", GetNomTable() );
			DataSet ds = new DataSet();
			adapter.FillSchema(ds, SchemaType.Mapped);
            CUtilDataAdapter.DisposeAdapter(adapter);
			tbl = ds.Tables[GetNomTable()];
			if ( tbl == null )
			{
				tbl = ds.Tables[GetNomTableInDb()];
				if ( tbl != null )
					tbl.TableName = GetNomTable();
			}
            FaitLesCorrectionsSurLeSchema(tbl);
			m_tableSchema[GetNomTable()] = tbl.Clone();
			return tbl;
		}

        /////////////////////////////////////////////////////////
        protected virtual void FaitLesCorrectionsSurLeSchema ( DataTable table )
        {
            CStructureTable structure = CStructureTable.GetStructure(GetTypeObjets());
            HashSet<string> lstChamps = new HashSet<string>();
			foreach ( CInfoChampTable info in structure.Champs )
            {
                DataColumn col = table.Columns[ info.NomChamp ];
                if ( col != null && !info.NullAuthorized && col.AllowDBNull )
                    col.AllowDBNull = false;
                lstChamps.Add(info.NomChamp);
            }
            //Si le champ est dans la base, mais pas dans la structure objet,
            //Ajoute allowdbnull pour ne pas empecher la lecture de 
            //la table
            foreach (DataColumn col in table.Columns)
            {
                if (!lstChamps.Contains(col.ColumnName))
                    col.AllowDBNull = true;
            }
        }

		/////////////////////////////////////////////////////////
		public virtual IDataAdapter GetDataAdapter( DataRowState rowsPriseEnCharge, params string[] champsExclus)
		{
			return CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType()).GetNewAdapterForType(GetTypeObjets(), rowsPriseEnCharge, m_bDesactiverIdAuto, champsExclus );
		}

		/////////////////////////////////////////////////////////
		public virtual CResultAErreur ReadBlob(string strChamp, object[] primaryKeys)
		{
			CResultAErreur result = CResultAErreur.True;
			CFiltreData filtre = new CFiltreData();
			bool bGestionParDifferences = false;
			int[] nIdsVersions = new int[0];
			StringBuilder blIDsVersionsToRead = new StringBuilder();
			using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
			{
                contexte.GestionParTablesCompletes = false;
				DataTable table = contexte.GetTableSafe(CContexteDonnee.GetNomTableForType(GetTypeObjets()));
				if (IdVersionDeTravail != null && (int)IdVersionDeTravail >= 0 &&
					typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(GetTypeObjets()) &&
					table.Columns.Contains(CSc2iDataConst.c_champOriginalId))
				{

					if (typeof(IObjetABlobAVersionPartielle).IsAssignableFrom(GetTypeObjets()))
					{
						IObjetABlobAVersionPartielle objet = (IObjetABlobAVersionPartielle)Activator.CreateInstance(GetTypeObjets(), new object[] { contexte });
						bGestionParDifferences = objet.IsBlobParDifference(strChamp);
					}
					nIdsVersions = CVersionDonnees.GetVersionsToRead(IdSession,(int)IdVersionDeTravail);
					
					foreach (int nIdVersion in nIdsVersions)
					{
						blIDsVersionsToRead.Append(nIdVersion);
						blIDsVersionsToRead.Append(",");
					}
					if ( blIDsVersionsToRead.Length > 0 )
						blIDsVersionsToRead.Remove ( blIDsVersionsToRead.Length-1, 1);

					if (!bGestionParDifferences)
					{
						//Il faut trouver l'élément sur lequel lire le blob

						
						if (blIDsVersionsToRead.Length > 0)
						{
							//Si par de gestion par différence, lit simplement dans 
							//l'élement le plus récent

							C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
							requete.TableInterrogee = table.TableName;
							requete.ListeChamps.Add(new C2iChampDeRequete(
								table.PrimaryKey[0].ColumnName,
								new CSourceDeChampDeRequete(table.PrimaryKey[0].ColumnName),
								typeof(int),
								OperationsAgregation.None,
								true));
							requete.ListeChamps.Add(new C2iChampDeRequete(
								CSc2iDataConst.c_champIdVersion,
								new CSourceDeChampDeRequete(CSc2iDataConst.c_champIdVersion),
								typeof(int),
								OperationsAgregation.None,
								true));
							requete.FiltreAAppliquer = new CFiltreData(
							CSc2iDataConst.c_champOriginalId + "=@1 and " +
							CSc2iDataConst.c_champIdVersion + " in (" + blIDsVersionsToRead.ToString() + ")",
							primaryKeys[0]);
							requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
							result = requete.ExecuteRequete(IdSession);
							if (result)
							{
								DataTable tableTmp = (DataTable)result.Data;
								if (tableTmp.Rows.Count > 0)
								{
                                    tableTmp.DefaultView.Sort = CSc2iDataConst.c_champIdVersion + " desc";
									DataRow row = tableTmp.DefaultView[0].Row;
									primaryKeys = new object[] { row[table.PrimaryKey[0].ColumnName] };
								}
							}
							else
								return result;
						} 
					}
				}
				filtre = CFiltreData.CreateFiltreAndSurValeurs(table.PrimaryKey, primaryKeys);
				result = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType()).ReadBlob(GetNomTableInDb(), strChamp, filtre);

				if (bGestionParDifferences && result && nIdsVersions.Length > 0)
				{
					byte[] data = (byte[])result.Data;
					//Lit les modifs de toutes les version antérieures
					CListeObjetsDonnees listeVersions = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjet));
					listeVersions.Filtre = new CFiltreData(
						CVersionDonnees.c_champId + " in (" + blIDsVersionsToRead + ") and " +
						CVersionDonneesObjet.c_champIdElement + "=@1 and " +
						CVersionDonneesObjet.c_champTypeElement + "=@2",
						primaryKeys[0],
						GetTypeObjets().ToString());
					listeVersions.Filtre.IgnorerVersionDeContexte = true;
					listeVersions.Tri = CVersionDonnees.c_champId;
					IObjetABlobAVersionPartielle objet = (IObjetABlobAVersionPartielle)Activator.CreateInstance(GetTypeObjets(), new object[] { contexte });
					foreach (CVersionDonneesObjet versionObjet in listeVersions)
					{
						CListeObjetsDonnees listeData = versionObjet.Modifications;
						listeData.Filtre = new CFiltreData(
							CVersionDonneesObjetOperation.c_champTypeChamp + "=@1 and " +
							CVersionDonneesObjetOperation.c_champChamp + "=@2",
							CChampPourVersionInDb.c_TypeChamp,
							strChamp);
						foreach (CVersionDonneesObjetOperation versionData in listeData)
						{
							object valeur = versionData.GetValeurStd();
							if (valeur is IDifferencesBlob)
							{
								data = ((IObjetABlobAVersionPartielle)objet).RedoModifs((IDifferencesBlob)valeur, data);
							}
						}
					}
					result.Data = data;
				}
			}


			return result;
			//Si gestion par différences, on a lu la valeur originale, mais il faut lui réappliquer toutes les 
			//Modifs faites


		}

        /////////////////////////////////////////////////////////
        public CResultAErreur ReadBlobs(string strChamp, List<object[]> primaryKeys)
        {
            CResultAErreur result = CResultAErreur.True;
            List<byte[]> datas = new List<byte[]>();
            foreach (object[] keys in primaryKeys)
            {
                result = ReadBlob(strChamp, keys);
                if (!result)
                    return result;
                byte[] data = result.Data as byte[];
                datas.Add(data);
            }
            result.Data = datas;
            return result;
        }

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Sauvegarde un blob
		/// </summary>
		/// <param name="strChamp"></param>
		/// <param name="filtre"></param>
		/// <param name="data"></param>
		/// <param name="dataOriginal">Contient les données originales du blob (avant modif)</param>
		/// <returns></returns>
		public virtual CResultAErreur SaveBlob ( string strChamp, object[] primaryKeys, byte[] data, int ?nIdVersionArchive, byte[] dataOriginal )
		{
			using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
			{
				bool bGestionParDifference = false;
				if ( typeof(IObjetABlobAVersionPartielle).IsAssignableFrom ( GetTypeObjets() ))
				{
					IObjetABlobAVersionPartielle objetTmp = (IObjetABlobAVersionPartielle)Activator.CreateInstance ( GetTypeObjets(), contexte );
					bGestionParDifference = objetTmp.IsBlobParDifference ( strChamp );
				}

				DataTable table = contexte.GetTableSafe(GetNomTable());
				CVersionDonnees version = null;
				CResultAErreur result = CResultAErreur.True;
				IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType());

				//Archivage
				if (nIdVersionArchive != null && CVersionDonneesObjet.EnableJournalisation &&
					typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(GetTypeObjets()) &&
					!typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()))
				{
					//Regarde si le blob a bougé
					bool bHasChange = false;
					if ((data == null) != (dataOriginal == null))
						bHasChange = true;
					if (data != null && dataOriginal != null)
						bHasChange = !data.Equals(dataOriginal);
					if (bHasChange)
					{
						version = new CVersionDonnees(contexte);
						if (version.ReadIfExists((int)nIdVersionArchive))
						{
							//Cherche une modification de l'objet dans la version
							CFiltreData filtre = new CFiltreData(
								CVersionDonnees.c_champId + "=@1 and " +
								CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
								CVersionDonneesObjet.c_champIdElement + "=@3",
								version.Id,
								GetTypeObjets().ToString(),
								(int)primaryKeys[0]);
							CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(contexte);
							if (!versionObjet.ReadIfExists(filtre))
							{
								versionObjet = new CVersionDonneesObjet(contexte);
								versionObjet.CreateNewInCurrentContexte();
								versionObjet.TypeElement = GetTypeObjets();
								versionObjet.IdElement = (int)primaryKeys[0];
								versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
								versionObjet.VersionDonnees = version;
							}

							CJournaliseurChampDb journaliseur = new CJournaliseurChampDb();
							journaliseur.JournaliseValeur(versionObjet, strChamp, dataOriginal);
							result = contexte.SaveAll(true);
							if (!result)
								return result;
						}
					}
				}
				CFiltreData filtrePourSave = CFiltreData.CreateFiltreAndSurValeurs(table.PrimaryKey, primaryKeys);

				//Version prévisionnelles
				if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(GetTypeObjets()) &&
					!typeof(IObjetSansVersion).IsAssignableFrom(GetTypeObjets()))
				{
					//Gestion de version prévisionnelle
					if (IdVersionDeTravail != null &&
						IdVersionDeTravail >= 0)
					{
						//Va chercher la valeur du blob dans la version précédente
						ArrayList lstVersions = new ArrayList(CVersionDonnees.GetVersionsToRead(IdSession, (int)IdVersionDeTravail));
						lstVersions.Remove(IdVersionDeTravail);
						StringBuilder blIdsVersions = new StringBuilder();
						foreach (int nVersionTmp in lstVersions)
						{
							blIdsVersions.Append(nVersionTmp);
							blIdsVersions.Append(',');
						}
						CListeObjetsDonnees listeElements = new CListeObjetsDonnees(contexte, GetTypeObjets());
						if (blIdsVersions.Length > 0)
						{
							blIdsVersions.Remove(blIdsVersions.Length - 1, 1);
							listeElements.Filtre = new CFiltreData(
								"(" + CSc2iDataConst.c_champIdVersion + " in (" + blIdsVersions.ToString() + ") and " +
								CSc2iDataConst.c_champOriginalId + "=@1) or " +
								"(" + CSc2iDataConst.c_champIdVersion + " is null and " +
								table.PrimaryKey[0].ColumnName + "=@1)",
								primaryKeys[0]);
						}
						else
						{
							listeElements.Filtre = new CFiltreData(
								"(" + CSc2iDataConst.c_champIdVersion + " is null and " +
								table.PrimaryKey[0].ColumnName + "=@1)",
								primaryKeys[0]);
						}
						listeElements.Filtre.IgnorerVersionDeContexte = true;
						listeElements.Tri = CSc2iDataConst.c_champIdVersion + " desc";
						if (listeElements.Count > 0)
						{
							CObjetServeur newServeur = (CObjetServeur)Activator.CreateInstance(GetType(), new object[] { IdSession });
							newServeur.IdVersionDeTravail = ((CObjetDonneeAIdNumerique)listeElements[0]).IdVersionDatabase;
							//Lit le blob dans la version de l'objet
							result = newServeur.ReadBlob(strChamp, primaryKeys);
							if (!result)
								return result;
							dataOriginal = (byte[])result.Data;
						}
						else
							dataOriginal = null;

						//Regarde si le blob a bougé
						bool bHasChange = false;
						IDifferencesBlob differencesBlob = null;
						if (bGestionParDifference)
						{
							IObjetABlobAVersionPartielle objet = (IObjetABlobAVersionPartielle)Activator.CreateInstance(GetTypeObjets(), new object[] { contexte });
							CFiltreData filtre = new CFiltreData(
								table.PrimaryKey[0] + "=@1",
								primaryKeys[0]);
							filtre.IgnorerVersionDeContexte = true;
							if (!objet.ReadIfExists(filtre))
							{
								result.EmpileErreur(I.T("Object @1 doesn't exist|225", primaryKeys[0].ToString()));
								return result;
							}
							differencesBlob = objet.GetDifferencesBlob(strChamp, data, dataOriginal);
							if (differencesBlob != null)
								bHasChange = true;
						}
						if (differencesBlob == null)
						{
							if ((data == null) != (dataOriginal == null))
								bHasChange = true;
							if (data != null && dataOriginal != null)
								bHasChange = !data.Equals(dataOriginal);
						}
						if (bHasChange)
						{
							version = new CVersionDonnees(contexte);
							if (version.ReadIfExists((int)IdVersionDeTravail))
							{
								//Cherche une modification de l'objet dans la version
								CFiltreData filtre = new CFiltreData(
									CVersionDonnees.c_champId + "=@1 and " +
									CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
									CVersionDonneesObjet.c_champIdElement + "=@3",
									version.Id,
									GetTypeObjets().ToString(),
									(int)primaryKeys[0]);
								CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(contexte);
								if (!versionObjet.ReadIfExists(filtre))
								{
									versionObjet = new CVersionDonneesObjet(contexte);
									versionObjet.CreateNewInCurrentContexte();
									versionObjet.TypeElement = GetTypeObjets();
									versionObjet.IdElement = (int)primaryKeys[0];
									versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
									versionObjet.VersionDonnees = version;
								}

								CJournaliseurChampDb journaliseur = new CJournaliseurChampDb();
								if (differencesBlob == null)
									journaliseur.JournaliseValeur(versionObjet, strChamp, data);
								else
									journaliseur.JournaliseValeur(versionObjet, strChamp, differencesBlob);

								result = contexte.SaveAll(true);
								if (!result)
									return result;
							}
						}
					}


					//Répercute la modif sur les prévisionnelles dépendantes

					StringBuilder blIdsVersionDerivees = new StringBuilder();

					if (IdVersionDeTravail != null &&
						IdVersionDeTravail >= 0)
					{
						blIdsVersionDerivees.Append(IdVersionDeTravail);
						blIdsVersionDerivees.Append(',');
					}
					StringBuilder bl = new StringBuilder(blIdsVersionDerivees.ToString());
					if (bl.Length > 0)
						bl.Remove(bl.Length - 1, 1);

					//Si l'objet est un objet à blob à version partielle,
					//On ne doit pas répercuter sur les version
					//suivantes, car on travaille par les différences à la lecture,
					//On stocke uniquement la valeur dans la version
					while (true && !bGestionParDifference)
					{
						//Cherche les versions dérivées
						C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
						requete.TableInterrogee = CVersionDonnees.c_nomTable;
						requete.ListeChamps.Add(new C2iChampDeRequete(
							CVersionDonnees.c_champId,
							new CSourceDeChampDeRequete(CVersionDonnees.c_champId),
							typeof(int),
							OperationsAgregation.None,
							true));


						if (bl.Length > 0)
						{
							requete.FiltreAAppliquer = new CFiltreData(
								CVersionDonnees.c_champIdVersionParente + " in (" + bl.ToString() + ") and " +
							CVersionDonnees.c_champTypeVersion + "=@1",
							CTypeVersion.TypeVersion.Previsionnelle);
						}
						else
							requete.FiltreAAppliquer = new CFiltreData(
								CVersionDonnees.c_champIdVersionParente + " is null and " +
							CVersionDonnees.c_champTypeVersion + "=@1",
							CTypeVersion.TypeVersion.Previsionnelle);
						requete.FiltreAAppliquer.IgnorerVersionDeContexte = true;
						result = requete.ExecuteRequete(IdSession);
						if (!result)
							return result;
						bl = new StringBuilder();
						Hashtable tableIdsVersionsSuivantes = new Hashtable();
						if (((DataTable)result.Data).Rows.Count == 0)
							break;//Pas de versions suivantes
						foreach (DataRow row in ((DataTable)result.Data).Rows)
						{
							tableIdsVersionsSuivantes[row[0]] = true;
							bl.Append(row[0]);
							bl.Append(",");
						}
						bl.Remove(bl.Length - 1, 1);

						//TableIdsVersionSuivantes contient toutes le version suivantes.
						//On filtre pour ne prendre que les version qui n'ont pas modifié le champ
						//Sélectionne celles qui ont modifié le champ
						requete.FiltreAAppliquer = new CFiltreDataAvance(
							CVersionDonnees.c_nomTable,
							CVersionDonnees.c_champId + " in (" + bl.ToString() + ") and " +
							CVersionDonneesObjet.c_nomTable + "." +
							CVersionDonneesObjet.c_champIdElement + "=@1 and " +
							CVersionDonneesObjet.c_nomTable + "." +
							CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
							CVersionDonneesObjet.c_nomTable + "." +
							CVersionDonneesObjetOperation.c_nomTable + "." +
							CVersionDonneesObjetOperation.c_champTypeChamp + "=@3 and " +
							CVersionDonneesObjet.c_nomTable + "." +
							CVersionDonneesObjetOperation.c_nomTable + "." +
							CVersionDonneesObjetOperation.c_champChamp + "=@4",
							primaryKeys[0],
							GetTypeObjets().ToString(),
							CChampPourVersionInDb.c_TypeChamp,
							strChamp);
						result = requete.ExecuteRequete(IdSession);
						if (!result)
							return result;
						foreach (DataRow row in ((DataTable)result.Data).Rows)
							tableIdsVersionsSuivantes.Remove(row[0]);

						if (tableIdsVersionsSuivantes.Count == 0)
							break;//Plus de version suivante

						bl = new StringBuilder();
						foreach (object obj in tableIdsVersionsSuivantes.Keys)
						{
							blIdsVersionDerivees.Append(obj);
							blIdsVersionDerivees.Append(",");
							bl.Append(obj);
							bl.Append(",");
						}
						if (bl.Length == 0)
							break;
						bl.Remove(bl.Length - 1, 1);
					}
					if (blIdsVersionDerivees.Length > 0)
					{
						blIdsVersionDerivees.Remove(blIdsVersionDerivees.Length - 1, 1);
						//blIdsVersionDerivees contient les ids de toutes les versions qui n'ont pas modifié ce champ
						CListeObjetsDonnees listeObjets = new CListeObjetsDonnees(contexte, GetTypeObjets());
						listeObjets.Filtre = new CFiltreData(
							CSc2iDataConst.c_champIdVersion + " in (" + blIdsVersionDerivees + ") and " +
							"("+CSc2iDataConst.c_champOriginalId + "=@1 or "+
							table.PrimaryKey[0].ColumnName+"=@1)",
							primaryKeys[0]);
						listeObjets.Filtre.IgnorerVersionDeContexte = true;
						List<int> idsElements = new List<int>();
						StringBuilder blIdsToChange = new StringBuilder();
						if (IdVersionDeTravail == null || IdVersionDeTravail<0)
						{
							blIdsToChange.Append(primaryKeys[0].ToString());
							blIdsToChange.Append(",");
						}
						foreach (CObjetDonneeAIdNumerique objet in listeObjets)
						{
							blIdsToChange.Append(objet.Id);
							blIdsToChange.Append(",");
						}
						blIdsToChange.Remove(blIdsToChange.Length - 1, 1);
						filtrePourSave = new CFiltreData(table.PrimaryKey[0].ColumnName + " in (" + blIdsToChange.ToString() + ")");
					}
				}
				return CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType()).SaveBlob(GetNomTableInDb(), strChamp, filtrePourSave, data);
			}
		}

		/////GESTION AVEC CACHE
		//---------------------------------------------------------------------------
		private static void AssureRecepteurs()
		{
			lock (typeof(CLockerCacheObjetServeur))
			{
				if (m_recepteurNotificationsAjout == null)
				{
					m_recepteurNotificationsAjout = new CRecepteurNotification(0, typeof(CDonneeNotificationAjoutEnregistrement));
					m_recepteurNotificationsAjout.OnReceiveNotification += new NotificationEventHandler(m_recepteurNotificationsAjout_OnReceiveNotification);
				}
				if (m_recepteurNotificationsModif == null)
				{
					m_recepteurNotificationsModif = new CRecepteurNotification(0, typeof(CDonneeNotificationModificationContexteDonnee));
					m_recepteurNotificationsModif.OnReceiveNotification += new NotificationEventHandler(m_recepteurNotificationsModif_OnReceiveNotification);
				}

			}
		}

		//---------------------------------------------------------------------------
		static void m_recepteurNotificationsAjout_OnReceiveNotification(IDonneeNotification donnee)
		{
			if (donnee.GetType() != typeof(CDonneeNotificationAjoutEnregistrement))
				return;
			lock (typeof(CLockerCacheObjetServeur))
			{
				CDonneeNotificationAjoutEnregistrement donneeAjout = (CDonneeNotificationAjoutEnregistrement)donnee;
				if (m_cacheTables.ContainsKey(donneeAjout.NomTable))
					m_cacheTables.Remove(donneeAjout.NomTable);
                CCacheLecture.InvalideTable(donneeAjout.NomTable);
			}
		}

		//--------------------------------------------------------------------------------
		public static void m_recepteurNotificationsModif_OnReceiveNotification(IDonneeNotification donnee)
		{
			if (donnee.GetType() != typeof(CDonneeNotificationModificationContexteDonnee))
				return;
			lock (typeof(CLockerCacheObjetServeur))
			{
				CDonneeNotificationModificationContexteDonnee donneeModif = (CDonneeNotificationModificationContexteDonnee)donnee;
				Dictionary<string, bool> tablesFaites = new Dictionary<string, bool>();
				foreach (CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in donneeModif.ListeModifications)
				{
					if (!tablesFaites.ContainsKey(info.NomTable))
					{
						tablesFaites[info.NomTable] = true;
						if (m_cacheTables.ContainsKey(info.NomTable))
							m_cacheTables.Remove(info.NomTable);
					}
                    CCacheLecture.InvalideTable(info.NomTable);
				}
			}
		}

		//---------------------------------------------------------------------------
		public virtual DataTable ReadAvecCache(CFiltreData filtre, int nStart, int nEnd, params string[] strChampsARetourner)
		{
			AssureRecepteurs();
			//Ne peut pas travailler avec le cache
			if (filtre is CFiltreDataAvance || IdVersionDeTravail != null || GetFiltreSecurite() != null ||
				(filtre != null && (filtre.IgnorerVersionDeContexte || filtre.IntegrerLesElementsSupprimes)))
				return ReadSansCache(filtre, nStart, nEnd, strChampsARetourner);

			//Récupère la table du cache
			DataTable table = null;
			lock (typeof(CLockerCacheObjetServeur))
			{
				if (!m_cacheTables.TryGetValue(GetNomTable(), out table))
				{
					//Charge les données
					table = ReadSansCache(null, -1, -1);
					m_cacheTables[GetNomTable()] = table;
				}
			}

			DataView view = new DataView(table);
			if (filtre != null && filtre.SortOrder.Trim() != "")
				view.Sort = filtre.SortOrder;
			if (filtre != null && filtre.HasFiltre)
				view.RowFilter = new CFormatteurFiltreDataToStringDataTable().GetString(filtre);
			nEnd = Math.Min(nEnd, view.Count);

			DataTable tbl = table;
			if (nStart < 0)
				nStart = 0;
			if (nEnd < 0)
				nEnd = view.Count;

			DataTable tblNew = tbl.Clone();
			for (int n = nStart; n < nEnd; n++)
				tblNew.ImportRow(view[n].Row);
			tbl = tblNew;
			return tbl;



		}

	}
}
