using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using System.Data;
using sc2i.common;
using System.Text;
using sc2i.expression;

namespace sc2i.data
{

	public delegate CFiltreData GetFiltreAffichageDelegate ( Type typeElements, CContexteDonnee contexte );

	/// <summary>
	/// Description résumée de CListeObjetsDonnees.
	/// </summary>
	public class CListeObjetsDonnees : 
        I2iSerializable, 
        IEnuerableBiSens, 
        IList, 
        IObjetAContexteDonnee,
        IPreloadableFromArbreProprietesDynamiques
	{

		public class CStockeurDonneesListe
		{
				
			private CListeObjetsDonnees m_listeObjetsDonnees = null;
			private ArrayList m_listeObjets = null;
			private DataRow[] m_rows = null;
            private DataTable m_tableForRows;
            private string m_strFiltreRows = "";
            private string m_strSortRows = "";
            private DataViewRowState m_stateRows = DataViewRowState.None;
            
            //Set des ids interessants, remplace un ID in (...) qui est très lent
            //si m_setIds est null, on ne filtre pas sur les ids
            private HashSet<int> m_setIds = null;

			public CStockeurDonneesListe ( CListeObjetsDonnees listeObjetsDonnees, ArrayList listeObjets )
			{
				m_listeObjetsDonnees = listeObjetsDonnees;
				m_listeObjets = listeObjets;
			}

			public CStockeurDonneesListe ( 
                CListeObjetsDonnees listeObjetsDonnees, 
                DataTable table,
                IEnumerable<int> idsInteressant,
                string strFiltre,
                string strSort,
                DataViewRowState rowState )
			{
				m_listeObjetsDonnees = listeObjetsDonnees;
                m_tableForRows = table;
                m_strFiltreRows = strFiltre;
                m_strSortRows = strSort;
                m_stateRows = rowState;
                if (idsInteressant != null)
                {
                    m_setIds = new HashSet<int>();
                    foreach (int nId in idsInteressant)
                        m_setIds.Add(nId);
                }
                else
                    m_setIds = null;
                RecalculeRows();
			}

            private void RecalculeRows()
            {
                DataRow[] rows = m_tableForRows.Select(m_strFiltreRows, m_strSortRows, m_stateRows);
                if ( m_setIds != null )
                {
                    List<DataRow> lstRows = new List<DataRow>();
                    string strPrimKey = m_tableForRows.PrimaryKey[0].ColumnName;
                    foreach ( DataRow row in rows )
                        if ( m_setIds.Contains((int)row[strPrimKey] ))
                            lstRows.Add ( row );
                    rows = lstRows.ToArray();
                }
                m_rows = rows;
            }

			public object this[int nIndex]
			{
				get
				{
					if ( m_listeObjets != null )
					{
						if ( nIndex >= 0 && nIndex < m_listeObjets.Count )
							return Activator.CreateInstance ( m_listeObjetsDonnees.TypeObjets, new object[]{m_listeObjets[nIndex]} );
					}
					else if ( m_rows != null )
					{
						if ( nIndex <0 || nIndex>= m_rows.Length )
							return null;
						return Activator.CreateInstance ( m_listeObjetsDonnees.TypeObjets, new object[]{m_rows[nIndex]} );
					}
					return null;
				}
				set
				{
					throw new NotSupportedException(I.T("CStockeurDonneesListe doesn't support the function 'this[] set'|135"));
				}
			}

			public string Sort
			{
				get
				{
                    return m_strSortRows;
				}
				set
				{
                    m_strSortRows = value;
                    if ( m_tableForRows != null )
                        RecalculeRows();
				}
			}

			public int Count
			{
				get
				{
					if ( m_rows != null )
						return m_rows.Length;
					if ( m_listeObjets != null )
						return m_listeObjets.Count;
					return 0;
				}
			}

			public object GetValeurChamp ( int nIndex, string strChamp )
			{
				if ( m_rows != null )
					return m_rows[nIndex][strChamp];
				else if (m_listeObjets != null )
					return ((DataRow)m_listeObjets[nIndex])[strChamp];
				return null;
			}

			

			public DataRow GetRow ( int nIndex )
			{
				if ( m_rows != null )
					return m_rows[nIndex];
				if ( m_listeObjets != null )
					return (DataRow)m_listeObjets[nIndex];
				return null;
			}
		}


		public static int c_nNbLectureParLotFils = 1000;

		#region CListeObjetDonneesEnumerator
		private class CListeObjetDonneesEnumerator : IEnumeratorBiSens
		{
			private CListeObjetsDonnees m_liste;
			private int m_nIndex = -1;

			/// ////////////////////////////////////////////////////////////
			public CListeObjetDonneesEnumerator( CListeObjetsDonnees liste)
			{
				m_liste = liste;
			}

			/// ////////////////////////////////////////////////////////////
			public object Current
			{
				get
				{
					return m_liste[m_nIndex];
				}
			}

			/// ////////////////////////////////////////////////////////////
			public int CurrentIndex
			{
				get
				{
					return m_nIndex;
				}

				set
				{
					m_nIndex = value;
				}
			}

			/// ////////////////////////////////////////////////////////////
			public bool MoveNext()
			{
				if ( m_nIndex < m_liste.Count-1 )
				{
					m_nIndex++;
					return true;
				}
				return false;
			}

			/// ////////////////////////////////////////////////////////////
			public bool MovePrev()
			{
				if ( m_nIndex > 0 )
				{
					m_nIndex--;
					return true;
				}
				return false;
			}

			/// ////////////////////////////////////////////////////////////
			public void Reset()
			{
				m_nIndex = -1;
			}

		}
		#endregion

		public bool m_bModeSansTri = false;
        private bool m_bPreventVerifNoIsToRead = false;


		protected DataViewRowState m_filterState = DataViewRowState.CurrentRows;

		protected CContexteDonnee m_contexte = null;
		protected string m_strNomTable = "";
		protected Type m_typeObjet;
		private CFiltreData m_filtre = null;
		
		protected CStockeurDonneesListe	m_view = null;
		
		protected CFiltreData m_filtrePrincipal = null;
		protected bool	m_bIsToRead = true;
		protected bool	m_bInterditLecture = false;
		protected string m_strTri = "";
		protected bool m_bAppliquerFiltreParDefaut = true;

		//Liste si remplissage progressif par page
		/// <summary>
		/// Lorsque la liste est en mode progressif, m_listeProgressive contient
		/// au fil du remplissage les DataRow chargés
		/// </summary>
		private ArrayList m_listeProgressive = null;
		private int m_nTaillePage = 100;
		private bool m_bRemplissageProgressif = false;
		
		//Indique que la liste est lue par page.
		//Dés qu'une page est chargée, le reste de la table (et des tables dépendantes) est vidé.
		private bool m_bLectureParPage = false;

		/// <summary>
		/// Indique si la liste préserve les données de son contexte ou si elle les remplace
		/// par celles de la base de données !
		/// </summary>
		private bool m_bPreserveChanges = false;

		public int m_nStartZone = -1;
		public int m_nEndZone = -1;

		//Si vrai, complete le filtre en cours par un filtre d'affichage,
		//Fourni par les delegués enregistrés GetFiltreAffichageDelegate 
		private bool m_bAppliquerFiltreAffichage = false;

		private static List<GetFiltreAffichageDelegate> m_listeDeleguesAffichages = new List<GetFiltreAffichageDelegate>();



		////////////////////////////////////////////////////////////
		public CListeObjetsDonnees(CContexteDonnee ctx)
		{
			m_contexte = ctx;
			m_strTri  = "";
		}

		////////////////////////////////////////////////////////////
		public CListeObjetsDonnees( CContexteDonnee ctx, Type typeObjet )
		{
			Init ( ctx, typeObjet, true );
			m_strTri  = "";
		}

		////////////////////////////////////////////////////////////
		public CListeObjetsDonnees ( CContexteDonnee ctx, Type typeObjet, CFiltreData filtrePrincipal )
		{
			Init ( ctx, typeObjet, true );
			m_strTri = "";
			m_filtrePrincipal = filtrePrincipal;
		}
		
		////////////////////////////////////////////////////////////
		public CListeObjetsDonnees ( CContexteDonnee ctx, Type typeObjet, bool bAppliquerFiltreParDefaut )
		{
			Init ( ctx, typeObjet, bAppliquerFiltreParDefaut );
		}

		////////////////////////////////////////////////////////////
		public static void RegisterDelegueFiltreAffichage ( GetFiltreAffichageDelegate delegue )
		{
			m_listeDeleguesAffichages.Add ( delegue );
		}

		////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 6;
			//6 ajout de m_bAppliquerFiltreAffichage
		}

        ////////////////////////////////////////////////////////////
        public bool PreventVerifIsToRead
        {
            get
            {
                return m_bPreventVerifNoIsToRead;
            }
            set
            {
                m_bPreventVerifNoIsToRead = value;
            }
        }

		////////////////////////////////////////////////////////////
		public CFiltreData FiltrePrincipal
		{
			get
			{
				return m_filtrePrincipal;
			}
			set
			{
				m_filtrePrincipal = value;
				m_view = null;
				m_listeProgressive = null;
				//Si on change le filter, il faut forcement recharger la liste  !!
				m_bIsToRead = true;
			}
		}


		////////////////////////////////////////////////////////////
		public int StartAt
		{
			get
			{
				return m_nStartZone;
			}
			set
			{
				m_nStartZone = value;
			}
		}

		////////////////////////////////////////////////////////////
		public int EndAt
		{
			get
			{
				return m_nEndZone;
			}
			set
			{
				m_nEndZone = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique que la liste doit appliquer les éventuels filtres
		/// d'affichage des entités
		/// </summary>
		public bool AppliquerFiltreAffichage
		{
			get
			{
				return m_bAppliquerFiltreAffichage;
			}
			set
			{
				if ( value != m_bAppliquerFiltreAffichage )
					Refresh();
				m_bAppliquerFiltreAffichage = value;
			}
		}

		////////////////////////////////////////////////////////////
		public bool RemplissageProgressif
		{
			get
			{
				return m_bRemplissageProgressif;
			}
			set
			{
				m_bRemplissageProgressif = value;
				if ( m_bInterditLecture || typeof(IObjetALectureTableComplete).IsAssignableFrom(m_typeObjet))
					m_bRemplissageProgressif = false;
			}
		}

		////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Indique que la liste est lu par pages.
		/// </summary>
		/// <remarks>
		/// Attention, lorsqu'une page est chargée, tout le reste de la table est vidé !!!<BR>
		/// </BR>
		/// Cette fonction provoque la perte de toute modification faite dans le dataset
		/// sur les tables qui sont ainsi vidées.
		/// elles est utile dans le cas d'une lecture uniquement.
		/// </remarks>
		public bool LectureParPages
		{
			get
			{
				return m_bLectureParPage;
			}
			set
			{
				m_bLectureParPage = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// L'utilisation du mode sans tri permet d'optimiser considérablement les 
		/// Liste avec des filtredataavances
		/// </summary>
		public bool ModeSansTri
		{
			get
			{
				return m_bModeSansTri;
			}
			set
			{
				m_bModeSansTri = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique si lors de la lecture de la base, les données du contexte sont
		/// remplacées (false) ou conservées (true)
		/// </summary>
		public bool PreserveChanges
		{
			get
			{
				return m_bPreserveChanges;
			}
			set
			{
				m_bPreserveChanges = value;
			}
		}

		////////////////////////////////////////////////////////////
		public void ResetStartAndEnd()
		{
			m_nStartZone = -1;
			m_nEndZone = -1;
		}

		////////////////////////////////////////////////////////////
		public virtual CResultAErreur Serialize ( C2iSerializer serializer )
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomTable );
			serializer.TraiteType ( ref m_typeObjet );
			
			I2iSerializable objet = (I2iSerializable)m_filtre;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_filtre = (CFiltreData)objet;

			objet = (I2iSerializable)m_filtrePrincipal;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_filtrePrincipal = (CFiltreData)objet;

			serializer.TraiteBool ( ref m_bInterditLecture );
			serializer.TraiteString ( ref m_strTri );
			serializer.TraiteBool ( ref m_bAppliquerFiltreParDefaut );
			
			if ( serializer.Mode == ModeSerialisation.Lecture )
				Init ( m_contexte, m_typeObjet, m_bAppliquerFiltreParDefaut );
			
			if ( nVersion > 1 )
				serializer.TraiteBool ( ref m_bRemplissageProgressif );
			else
				m_bRemplissageProgressif = false;

			if ( nVersion >= 3 )
			{
				int nVal = (int)m_filterState;
				serializer.TraiteInt ( ref nVal );
				m_filterState = (DataViewRowState)nVal;
			}
			else
				m_filterState = DataViewRowState.CurrentRows;

			if ( nVersion >= 4 )
				serializer.TraiteBool ( ref m_bLectureParPage );
			else
				m_bLectureParPage = false;

			if ( nVersion >= 5 )
				serializer.TraiteBool ( ref m_bPreserveChanges );
			else
				m_bPreserveChanges = false;

			if (nVersion >= 6)
				serializer.TraiteBool(ref m_bAppliquerFiltreAffichage);
			else
				m_bAppliquerFiltreAffichage = false;

			return result;
		}

		/// ////////////////////////////////////////////////////////////
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexte;
			}
			set
			{
				m_contexte = value;
			}
		}

        /// ////////////////////////////////////////////////////////////
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return ContexteDonnee;
            }
        }
       
		////////////////////////////////////////////////////////////
		/// <summary>
		/// Si vrai, la liste se contente de la table chargée dans le contexte
		/// et ne se relit jamais depuis la base de données
		/// </summary>
		public bool InterditLectureInDB
		{
			get
			{
				return m_bInterditLecture;
			}
			set
			{
				m_bInterditLecture = value;
			}
		}

		////////////////////////////////////////////////////////////
		protected void Init ( CContexteDonnee ctx, Type typeObjet, bool bAppliquerFiltreParDefaut )
		{
			m_bAppliquerFiltreParDefaut = bAppliquerFiltreParDefaut;
            //stef 11/01/2011 : si le contexte est en édition,
            //met PreserveChanges à TRUE
            m_bPreserveChanges = ctx.IsEnEdition;
			object[] attribs = typeObjet.GetCustomAttributes(typeof(TableAttribute), true);
			if ( attribs.Length == 0 )
				throw new Exception(I.T("Cannot create list because @1 type has no linked table|136",
					DynamicClassAttribute.GetNomConvivial(typeObjet)));
			m_contexte = ctx;
			m_typeObjet = typeObjet;
			m_strNomTable = ((TableAttribute)attribs[0]).NomTable;
#if PDA
			CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(typeObjet);
			objet.ContexteDonnee = ctx;
#else
			CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(typeObjet, new object[]{ctx});
#endif
			CFiltreData filtre = bAppliquerFiltreParDefaut?objet.FiltreStandard:null;
			if ( filtre != null )
				m_filtrePrincipal = filtre;
			m_view = null;
			m_listeProgressive =  null;
		}

		////////////////////////////////////////////////////////////
		public CFiltreData Filtre 
		{
			get
			{
				return m_filtre;
			}
			set
			{
				m_filtre = value;
				m_view = null;
				m_listeProgressive = null;
				//Si on change le filter, il faut forcement recharger la liste  !!
				m_bIsToRead = true;
                if (m_filtre != null && m_filtre.SortOrder != null)
                    Tri = m_filtre.SortOrder;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Combinaison des RowState à retourner.
		/// </summary>
		public DataViewRowState RowStateFilter
		{
			get
			{
				return m_filterState;
			}
			set
			{
				m_filterState = value;
			}
		}

		////////////////////////////////////////////////////////////
		private CFiltreData GetFiltreAffichage ( )
		{
			CFiltreData filtre = null;
			foreach ( GetFiltreAffichageDelegate func in m_listeDeleguesAffichages )
				filtre = CFiltreData.GetAndFiltre ( filtre, func (m_typeObjet, m_contexte ) );
			return filtre;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le filtre complet appliqué à la liste
		/// </summary>
		/// <returns></returns>
		public CFiltreData GetFiltreForRead()
		{
			CFiltreData filtre = m_filtre!=null?m_filtre.GetClone():null;
			if ( m_filtrePrincipal != null )
			{
				if ( filtre == null )
					filtre = m_filtrePrincipal;
				else
				{
					filtre = CFiltreData.GetAndFiltre ( filtre, m_filtrePrincipal );
				}
			}
			if ( AppliquerFiltreAffichage )
			{
				CFiltreData filtreAffichage = GetFiltreAffichage();
				if ( filtreAffichage != null && filtreAffichage.HasFiltre )
					filtre =CFiltreData.GetAndFiltre ( filtre, filtreAffichage );
			}
			if ( Tri == "" )
			{
				CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(m_typeObjet, new object[]{ContexteDonnee});
				Tri = objet.GetChampsTriParDefautSeparesVirgule();
			}
			if ( filtre != null )
				filtre.SortOrder = Tri;
			else if ( Tri.Trim() != "" )
			{
				filtre = new CFiltreData();
				filtre.SortOrder = Tri;
			}

			return filtre;
		}

		////////////////////////////////////////////////////////////
		public string Tri
		{
			get
			{
				if ( m_strTri == null )
					return "";
				else
					return m_strTri;
			}
			set
			{
				if ( value == null )
					m_strTri = "";
				else if( value != m_strTri )
				{
					m_strTri = value;
					if ( m_bRemplissageProgressif )
						Refresh();
					else if (m_view != null )
					{
						try
						{
							m_view.Sort = value;
						}
						catch{}
					}
				}
			}
		}

		////////////////////////////////////////////////////////////
		private DataRowChangeEventHandler m_lastHandler = null;
		private CResultAErreur AssureViewModeProgressif()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_listeProgressive != null )
				return result;
			int nNb = GetCountNoLoadFromBase();
			m_listeProgressive = new ArrayList();
			for ( int n = 0; n< nNb; n++ )
			{
				m_listeProgressive.Add ( null );
			}

			if ( m_lastHandler != null && m_contexte.Tables[m_strNomTable]!= null)
			{
				m_contexte.Tables[m_strNomTable].RowChanged -= m_lastHandler;
				m_contexte.Tables[m_strNomTable].RowDeleted -= m_lastHandler;
				m_lastHandler = null;
			}
			if ( m_contexte.Tables[m_strNomTable] != null )
			{
				m_lastHandler = new DataRowChangeEventHandler(OnChangeTable);
				m_contexte.Tables[m_strNomTable].RowChanged += m_lastHandler;
				m_contexte.Tables[m_strNomTable].RowDeleted += m_lastHandler;
			}

			return CResultAErreur.True;
		}

		////////////////////////////////////////////////////////////
		private bool m_bDisableTableEventHandler = false;
		public void OnChangeTable ( object sender, DataRowChangeEventArgs args )
		{
			if ( !m_bDisableTableEventHandler )
				if ( args.Action == DataRowAction.Add || args.Action == DataRowAction.Delete )
					m_listeProgressive = null;
		}

		////////////////////////////////////////////////////////////
		private CResultAErreur AssureView()
		{
			if ( m_bRemplissageProgressif && !m_bInterditLecture)
				return AssureViewModeProgressif();
			m_bRemplissageProgressif = false;
			if ( m_view != null )
				return CResultAErreur.False;
			DataTable table = null;
			ArrayList listeCles = new ArrayList();
			if ( m_filtre !=  null && m_filtre is CFiltreDataImpossible )
				m_bIsToRead = false;
			if ( m_filtre is CFiltreDataAvance || m_filtrePrincipal is CFiltreDataAvance )
				m_bIsToRead = true;
            bool bGestionParTableComplete = typeof(IObjetALectureTableComplete).IsAssignableFrom(m_typeObjet) && ContexteDonnee.GestionParTablesCompletes;
            if (!m_bPreventVerifNoIsToRead && ( m_bInterditLecture || bGestionParTableComplete) )
            {
                //Si interdiction de lecture, on doit s'assurer que tous les éléments 
                //de la table sont bien lus, sinon, le filtre peut porter
                //sur des éléments qui ne sont pas à jour !
                m_contexte.AssureAucunAIsToRead(TypeObjets);
            }

			if ( m_bIsToRead && (!m_bInterditLecture || m_filtre is CFiltreDataAvance || m_filtrePrincipal is CFiltreDataAvance) )
			{
				CFiltreData filtre = GetFiltreForRead();

				//S'assure que la structure de la table est renseignée
				DataTable tableConcernee = m_contexte.GetTableSafe( m_strNomTable );

                HashSet<int> nIdsInteressantes = null;

				DataTable tableLue = null;
                try
                {

                    //SI gestion par table complete, la table est lue entierement
                    //Si filtre par avancé->Il suffit d'appliquer le filtre à la table
                    //Sinon, on a besoin des ids des éléments pour filtrer.
                    if (!bGestionParTableComplete || filtre is CFiltreDataAvance)
                    {
                        IObjetServeur loader = m_contexte.GetTableLoader(m_strNomTable);
                        if (!filtre.AreParametresValides())
                            filtre = new CFiltreDataImpossible();

                        //STEF 06/40/2013 : optimisation par lecture des ids concernés et lecture uniquement des 
                        //éléments qu'on ne connait pas encore
                        if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(TypeObjets) && tableConcernee.Rows.Count > 0 &&
                            tableConcernee.PrimaryKey.Length > 0)
                        {
                            string strKey = tableConcernee.PrimaryKey[0].ColumnName;
                            DataTable tableIds = null;
                            if (m_nStartZone != -1 && m_nEndZone != -1)
                                tableIds = loader.Read(filtre, m_nStartZone, m_nEndZone, strKey);
                            else
                                tableIds = loader.Read(filtre, strKey);
                            nIdsInteressantes = new HashSet<int>();
                            StringBuilder bl = new StringBuilder();
                            foreach (DataRow row in tableIds.Rows)
                            {
                                int nId = (int)row[0];
                                nIdsInteressantes.Add(nId);
                            }
                            if (tableIds != null)
                                tableIds.Dispose();
                            tableIds = null;
                            HashSet<int> setIdsALire = new HashSet<int>(nIdsInteressantes);
                            if (nIdsInteressantes.Count == 0)
                            {
                                filtre = new CFiltreDataImpossible();
                                nIdsInteressantes = null;
                            }
                            else
                            {
                                int nNbFound = 0;
                                foreach (int nId in nIdsInteressantes)
                                {
                                    DataRow row = tableConcernee.Rows.Find(nId);
                                    if (row != null && !ContexteDonnee.IsToRead(row))
                                    {
                                        nNbFound++;
                                        setIdsALire.Remove(nId);
                                    }
                                }
                                if (nNbFound > nIdsInteressantes.Count / 4)
                                {

                                    //Lecture des éléments interessants
                                    List<int> lstALire = new List<int>(setIdsALire);
                                    for (int n = 0; n < lstALire.Count; n += 500)
                                    {
                                        bl = new StringBuilder();
                                        int nMin = Math.Min(n + 500, lstALire.Count);
                                        for (int n2 = n; n2 < nMin; n2++)
                                        {
                                            bl.Append(lstALire[n2]);
                                            bl.Append(",");
                                        }
                                        if (bl.Length > 0)
                                        {
                                            bl.Remove(bl.Length - 1, 1);
                                            using (DataTable tableProv = loader.Read(new CFiltreData(strKey + " in (" + bl.ToString() + ")")))
                                                m_contexte.IntegreTable(tableProv, m_bPreserveChanges);
                                        }
                                    }
                                }
                                else
                                    nIdsInteressantes = null;

                            }
                        }
                        if (nIdsInteressantes == null)
                        {
                            if (m_nStartZone != -1 && m_nEndZone != -1)
                                tableLue = loader.Read(filtre, m_nStartZone, m_nEndZone);
                            else
                                tableLue = loader.Read(filtre);

                        }
                    }
                    if (!bGestionParTableComplete && nIdsInteressantes == null/*sinon, déjà integré*/)
                        table = m_contexte.IntegreTable(tableLue, m_bPreserveChanges);
                    else
                        table = m_contexte.Tables[m_strNomTable];
                    m_bIsToRead = false;
                    if (filtre is CFiltreDataAvance || (m_bModeSansTri && !bGestionParTableComplete))
                    {
                        string strPrimaryKey = "";
                        bool bIdNumerique = table.PrimaryKey.Length == 1 &&
                            table.PrimaryKey[0].DataType == typeof(int);
                        if (bIdNumerique)
                            strPrimaryKey = table.PrimaryKey[0].ColumnName;

                        ArrayList listeObjets = new ArrayList();
                        if (nIdsInteressantes != null)
                        {
                            foreach (int nId in nIdsInteressantes)
                            {
                                listeCles.Add(nId);
                                if (ModeSansTri)
                                {
                                    DataRow rowTmp = table.Rows.Find(nId);
                                    if (rowTmp != null)
                                        listeObjets.Add(rowTmp);
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow row in tableLue.Rows)
                            {
                                string strCle = "";

                                if (bIdNumerique)
                                {
                                    listeCles.Add(row[strPrimaryKey]);
                                    if (ModeSansTri)
                                    {
                                        DataRow rowTmp = table.Rows.Find(row[strPrimaryKey]);
                                        if (rowTmp != null)
                                            listeObjets.Add(rowTmp);
                                    }
                                }
                                else
                                {
                                    foreach (DataColumn col in table.PrimaryKey)
                                        strCle += row[col.ColumnName].ToString() + "_";
                                    listeCles.Add(strCle);
                                }
                            }
                        }
                        if (ModeSansTri)
                        {
                            m_view = new CStockeurDonneesListe(this, listeObjets);
                            return CResultAErreur.True;
                        }

                    }
                }
                finally
                {
                    if (tableLue != null)
                        tableLue.Dispose();
                    tableLue = null;
                }
			}
			else
				table = m_contexte.GetTableSafe(m_strNomTable);

			CFiltreData filtreDeBase = null;
			string strFiltre = null;
			if (m_filtrePrincipal != null )
				filtreDeBase = m_filtrePrincipal;

			if (AppliquerFiltreAffichage)
			{
				CFiltreData filtreTmp = GetFiltreAffichage();
				if (filtreTmp != null)
					filtreDeBase = CFiltreData.GetAndFiltre(filtreDeBase, filtreTmp);
			}

			if ( filtreDeBase != null && !(filtreDeBase is CFiltreDataAvance ) )
				strFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(filtreDeBase);
			
			//Ne lit que les éléments du référentiel
			if (ContexteDonnee.IdVersionDeTravail == null && !typeof(IObjetSansVersion).IsAssignableFrom(m_typeObjet) &&
				(Filtre == null || !Filtre.IgnorerVersionDeContexte))
			{
				string strTmp = CSc2iDataConst.c_champIdVersion + " is null and "+
					CSc2iDataConst.c_champIsDeleted+"=0";
				if ( strFiltre != null && strFiltre.Trim() != "" )
					strFiltre = "("+strFiltre+") and ("+strTmp+")";
				else
					strFiltre = strTmp;
			}

            if (table.Columns.Contains(CContexteDonnee.c_colIsToRead) && !InterditLectureInDB)
            {
                string strNotIsToRead = CContexteDonnee.c_colIsToRead + "<>1";
                if (strFiltre != null && strFiltre.Trim() != "")
                    strFiltre = "(" + strFiltre + ") and (" + strNotIsToRead + ")";
                else
                    strFiltre = strNotIsToRead;
            }


			//Ne lit que les éléments qui font partie de la version
			if (Filtre == null || !Filtre.IgnorerVersionDeContexte &&
				table.Columns.Contains(CContexteDonnee.c_colIsHorsVersion))
			{
				string strTmp = CContexteDonnee.c_colIsHorsVersion + "=0";
				if (strFiltre != null && strFiltre.Trim() != "")
					strFiltre = "("+strFiltre+") and ("+strTmp+")";
				else
					strFiltre = strTmp;
			}

            List<int> lstIdsPourStockeurDonneesListe = null;
			if ( m_filtre != null || filtreDeBase != null )
			{
				string strFiltreSecondaire = "";
				if (m_filtre is CFiltreDataAvance || filtreDeBase is CFiltreDataAvance)
				{
					if ( listeCles.Count == 0 )
						strFiltreSecondaire +="0=1";
					else
					{
						bool bIdNumerique = table.PrimaryKey.Length == 1 && 
							table.PrimaryKey[0].DataType == typeof(int);
						if ( bIdNumerique )
						{
							/*strFiltreSecondaire = table.PrimaryKey[0].ColumnName+" in (";
                            StringBuilder bl = new StringBuilder();*/
                            lstIdsPourStockeurDonneesListe = new List<int>();
                            foreach (int nCle in listeCles)
                            {
                                /*bl.Append(nCle);
                                bl.Append(',');*/
                                lstIdsPourStockeurDonneesListe.Add(nCle);
                            }
                            /*bl.Remove(bl.Length - 1, 1);
                            strFiltreSecondaire += bl.ToString();
							strFiltreSecondaire += ")";*/
						}
						else
						{
							foreach ( DataColumn col in table.PrimaryKey )
								strFiltreSecondaire += col.ColumnName+"+'_'+";
							strFiltreSecondaire = strFiltreSecondaire.Substring(0, strFiltreSecondaire.Length-1);
							strFiltreSecondaire += " in (";
                            StringBuilder bl = new StringBuilder();
                            foreach (string strCle in listeCles)
                            {
                                bl.Append("'");
                                bl.Append(strCle);
                                bl.Append("',");
                            }
                            bl.Remove(bl.Length - 1, 1);
                            strFiltreSecondaire += bl.ToString();
						}
					}
				}
				else if ( m_filtre != null )
					strFiltreSecondaire += new CFormatteurFiltreDataToStringDataTable().GetString(m_filtre);
//				if (filtreDeBase != null && !(filtreDeBase is CFiltreDataAvance) && strFiltre.Trim() != "")
				if (strFiltre != null && strFiltre.Trim() != "")
				{
					if (strFiltreSecondaire.Trim() != "")
						strFiltre = "(" + strFiltre + ") and (" + strFiltreSecondaire + ")";
				}
				else
					strFiltre = strFiltreSecondaire;
			}
			string strSort = Tri;
			if ( strSort == "" )
			{
				CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance ( m_typeObjet, new object[]{ContexteDonnee});
				strSort = objet.GetChampsTriParDefautSeparesVirgule();
				m_strTri = strSort;
			}
			m_view = new CStockeurDonneesListe ( 
                this,
                table,
                lstIdsPourStockeurDonneesListe,
                strFiltre,
                strSort,
                m_filterState );
            object[] atts = m_typeObjet.GetCustomAttributes(typeof(RelationsToReadAlwaysAttribute), true);
            if (atts.Length > 0)
            {
                List<string> lstRelations = new List<string>();
                foreach (RelationsToReadAlwaysAttribute rel in atts)
                {
                    lstRelations.AddRange(rel.RelationsToRead);
                }
                ReadDependances(lstRelations.ToArray());
            }
                    
			return CResultAErreur.True;
		}


        ////////////////////////////////////////////////////////////
        public CResultAErreur ReadBlobs(string strChamp)
        {
            IObjetServeur loader = m_contexte.GetTableLoader(m_strNomTable);
            List<object[]> keys = new List<object[]>();
            DataColumn[] cols = ContexteDonnee.Tables[m_strNomTable].PrimaryKey;
            List<CObjetDonnee> lstObjets = new List<CObjetDonnee>();
            foreach (CObjetDonnee objet in this)
            {
                List<object> lst = new List<object>();
                CDonneeBinaireInRow data = objet.Row[strChamp] as CDonneeBinaireInRow;
                if (data == null)
                {
                    foreach (DataColumn col in cols)
                        lst.Add(objet.Row[col]);
                    keys.Add(lst.ToArray());
                    lstObjets.Add ( objet );
                }
            }
            CResultAErreur result = loader.ReadBlobs(strChamp, keys);
            if (!result)
                return result;
            List<byte[]> lstDatas = result.Data as List<byte[]>;
            for ( int nIndex = 0; nIndex < lstObjets.Count; nIndex++ )
            {
                byte[] data = lstDatas[nIndex];
                CObjetDonnee objet = lstObjets[nIndex];
                CDonneeBinaireInRow donneeBinaire = new CDonneeBinaireInRow ( ContexteDonnee.IdSession, objet.Row.Row, strChamp, data );
                CContexteDonnee.ChangeRowSansDetectionModification ( objet.Row, strChamp, donneeBinaire );
            }
            result.Data = null;
            return result;
        }

		////////////////////////////////////////////////////////////
		[DynamicMethod("Ensure that data are read for database")]
		public void ForceRead()
		{
			AssureLectureFaite();
		}
		
		////////////////////////////////////////////////////////////
		public void AssureLectureFaite()
		{
			AssureView();
		}

		////////////////////////////////////////////////////////////
		private void AssureItemCharge ( int nIndex )
		{
			if ( !m_bRemplissageProgressif )
				return;
			if ( nIndex < 0 || nIndex >= m_listeProgressive.Count )
				return;
			if ( m_listeProgressive[nIndex] == null )
				LoadPage ( (int) nIndex / m_nTaillePage );
		}

        //////////////////////////////////////////////////////////////
        public delegate void AfterLoadPageEventHandler(List<DataRow> rowsLues);

        public event AfterLoadPageEventHandler AfterLoadPage;

        public bool HasAfterLoadPageEventHandler
        {
            get
            {
                return AfterLoadPage != null;
            }
        }

		////////////////////////////////////////////////////////////
		private void LoadPage ( int nPage )
		{
			AssureView();
			m_bDisableTableEventHandler = true;
			CFiltreData filtre = GetFiltreForRead();
			//S'assure que la structure de la table est renseignée
			m_contexte.GetTableSafe( m_strNomTable );
			IObjetServeur loader = m_contexte.GetTableLoader ( m_strNomTable );
			DataTable tableLue;
			int nStart = nPage*m_nTaillePage;
			int nEnd = Math.Min(nStart + m_nTaillePage-1, m_listeProgressive.Count );
			
			//Vide la table
			if ( LectureParPages )
			{
				m_contexte.ResetTableEtDependances ( m_strNomTable );
				int nSize = m_listeProgressive.Count;
				m_listeProgressive.Clear();
				m_listeProgressive.AddRange ( new object[nSize] );
			}
			
			tableLue = loader.Read ( filtre, nStart, nEnd );
            DataTable table = m_contexte.Tables[m_strNomTable];
            if ( table == null )
                table = m_contexte.IntegreTable(tableLue, m_bPreserveChanges);

            ArrayList listeCles = new ArrayList();
            string strPrimKeyIdNumerique = table.PrimaryKey.Length == 1 && table.PrimaryKey[0].DataType == typeof(int)? table.PrimaryKey[0].ColumnName : null;
			foreach ( DataRow row in tableLue.Rows )
			{
                if (strPrimKeyIdNumerique == null)
                {
                    string strCle = "";
                    foreach (DataColumn col in table.PrimaryKey)
                        strCle += row[col.ColumnName].ToString() + "_";
                    listeCles.Add(strCle);
                }
                else
                    listeCles.Add(row[strPrimKeyIdNumerique]);
			}
			string strFiltre = "";
			if ( listeCles.Count == 0 )
					strFiltre +="0=1";
			else
			{
                StringBuilder bl = new StringBuilder();
                if (strPrimKeyIdNumerique != null)
                    bl.Append(strPrimKeyIdNumerique);
                else
                {
                    foreach (DataColumn col in table.PrimaryKey)
                    {
                        bl.Append(col.ColumnName);
                        bl.Append("+'_'+");
                    }
                    bl.Remove(bl.Length - 1, 1);
                }
                bl.Append(" in(");
                if ( strPrimKeyIdNumerique != null )
                    foreach ( int nCle in listeCles )
                    {
                        bl.Append ( nCle );
                        bl.Append(',');
                    }
                else
                    foreach (string strCle in listeCles)
                    {
                        bl.Append("'");
                        bl.Append(strCle);
                        bl.Append("',");
                    }
                bl.Remove(bl.Length - 1,1);
                bl.Append(')');
                strFiltre = bl.ToString();
			}
			DataView view = new DataView ( table );
			view.RowStateFilter = m_filterState;
			if(  strFiltre != null && strFiltre != "")
				view.RowFilter = strFiltre;
			if ( Tri != "" )
				view.Sort = Tri;

            if ( view.Count != tableLue.Rows.Count )
                table = m_contexte.IntegreTable(tableLue, m_bPreserveChanges);
			
			
			int nRow = 0;
            List<DataRow> rowsLues = new List<DataRow>();
			foreach ( DataRowView row in view )
			{
				m_listeProgressive[nStart+nRow] = row.Row;
                rowsLues.Add ( row.Row );
				nRow++;
			}

            if (AfterLoadPage != null)
            {
                AfterLoadPage(rowsLues);
            }
                
			m_bDisableTableEventHandler = false;

            if (tableLue != null)
                tableLue.Dispose();
		}

		////////////////////////////////////////////////////////////
		public object this [ int nIndex ]
		{
			get
			{
				AssureView();
				if ( m_bRemplissageProgressif )
				{
					AssureItemCharge ( nIndex );
					try
					{
						return Activator.CreateInstance ( m_typeObjet, new object[]{(DataRow)m_listeProgressive[nIndex]} );
					}
					catch
					{
						return null;
					}
					//return m_contexte.GetNewObjetForRow((DataRow)m_listeProgressive[nIndex]);
				}
				if ( m_view == null )
					return null;
				return m_view[nIndex];
			}
			set
			{
				throw new NotSupportedException(I.T("CListeObjetDonnee doen't support the function this[] set|137"));
			}
		}

		public int GetIndex(CObjetDonnee obj)
		{
			if(  obj == null )
				return -1;
			int nIndex = -1;
			if ( m_bRemplissageProgressif  )
			{
				if( m_listeProgressive != null )
				{
					DataRow row = obj.Row.Row;
					return m_listeProgressive.IndexOf ( row );
				}
				else
					return -1;
			}
			foreach(CObjetDonnee objet in this)
			{
				nIndex++;
				if (objet==obj)
					return nIndex;
			}
			return -1;

		}

		////////////////////////////////////////////////////////////
		[DynamicField("Number")]
		public int Count
		{
			get
			{
				AssureView();
				if ( m_bRemplissageProgressif )
					return m_listeProgressive.Count;
				else
					return m_view.Count;
			}
		}

        ////////////////////////////////////////////////////////////
        private int GetCountNoLoadFromBase()
        {
            IObjetServeur loader = m_contexte.GetTableLoader(m_strNomTable);
            CFiltreData filtre = GetFiltreForRead();
            return loader.CountRecords(m_strNomTable, filtre);
        }


		////////////////////////////////////////////////////////////
		public int CountNoLoad
		{
			get
			{
				if ( m_view != null || m_listeProgressive != null)
					return Count;
                if (m_bRemplissageProgressif && !m_bInterditLecture)
                {
                    //Le AssureViewModeProgressif ne fait qu'un countNoLoad,
                    //autant stocker le résultat
                    AssureViewModeProgressif();
                    return Count;
                }
                return GetCountNoLoadFromBase();				
			}
		}

		////////////////////////////////////////////////////////////
		public string NomTable
		{
			get
			{
				return m_strNomTable;
			}
		}


		////////////////////////////////////////////////////////////
		public virtual IEnumeratorBiSens GetEnumeratorBiSens()
		{
			return new CListeObjetDonneesEnumerator(this);
		}

		////////////////////////////////////////////////////////////
		public virtual System.Collections.IEnumerator GetEnumerator()
		{
			return GetEnumeratorBiSens();
		}

		/// ////////////////////////////////////////////////////////////
		public Type TypeObjets
		{
			get
			{
				return m_typeObjet;
			}
		}

		/// ////////////////////////////////////////////////////////////
		public void CopyTo ( System.Array array, int nStart )
		{
			int nTotal = Count;
			for ( int n = 0; n < nTotal; n++ )
				array.SetValue ( this[n], nStart+n );
		}

		/// ////////////////////////////////////////////////////////////
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// ////////////////////////////////////////////////////////////
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// ////////////////////////////////////////////////////////////
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// ////////////////////////////////////////////////////////////
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// ////////////////////////////////////////////////////////////
		public void Remove ( object valeur )
		{
			throw new NotSupportedException(I.T("CListeObjetDonnee doesn't support the function Remove|138"));
		}

		/// ////////////////////////////////////////////////////////////
		public void RemoveAt ( int nPosition )
		{
			throw new NotSupportedException(I.T("CListeObjetDonnee doesn't support the function RemoveAt|139"));
		}


		/// ////////////////////////////////////////////////////////////
		public int Add ( object valeur )
		{
			throw new NotSupportedException(I.T("CListeObjetDonnee doesn't support the function Add|140"));
		}

		/// ////////////////////////////////////////////////////////////
		public void Clear()
		{
			throw new NotSupportedException(I.T("CListeObjetDonnee doesn't support the function Clear|141"));
		}

		/// ////////////////////////////////////////////////////////////
		public bool Contains ( object value )
		{
			return IndexOf ( value ) >= 0;
		}

		/// ////////////////////////////////////////////////////////////
		public int IndexOf ( object obj )
		{
			AssureView();
			int nVal = 0;
			foreach ( CObjetDonnee objet in this )
			{
				if ( objet.Equals(obj) )
					return nVal;
				nVal++;
			}
			return -1;
		}
		
		/// ////////////////////////////////////////////////////////////
		public void Insert ( int nIndex, object valeur )
		{
			throw new NotSupportedException(I.T("CListeObjetDonnee doesn't support the function Remove|142"));
		}

		/// ////////////////////////////////////////////////////////////
		public void Refresh()
		{
			m_bIsToRead = true;
			m_view = null;
			m_listeProgressive = null;
            if (m_typeObjet != null)
            {
                IObjetServeur objet = CContexteDonnee.GetTableLoader(CContexteDonnee.GetNomTableForType(m_typeObjet), null, m_contexte.IdSession);
                if ( objet != null )
                    objet.ClearCache();
            }
		}

		//Permet de stocker un arbre de propriétés à lire
		/// ////////////////////////////////////////////////////////
		public class CArbreProps 
		{
			private string m_strProprietePrincipale;
            private string m_strFiltre = "";
			private ArrayList m_listeProprietesFillesALire = new ArrayList();
            private CArbreProps m_arbreParent = null;
			
			/// ////////////////////////////////////////////////////////
			public CArbreProps ( CArbreProps arbreParent, string strPropPrincipale, string strFiltre )
			{
				m_strProprietePrincipale = strPropPrincipale;
                m_strFiltre = strFiltre;
                m_arbreParent = arbreParent;
			}

            /// ////////////////////////////////////////////////////////
            public CArbreProps ArbreParent
            {
                get
                {
                    return m_arbreParent;
                }
            }

            


            /// ////////////////////////////////////////////////////////
            public string Filtre
            {
                get
                {
                    return m_strFiltre;
                }
                set
                {
                    m_strFiltre = value;
                }
            }


			/// ////////////////////////////////////////////////////////
            ///Le nom de la propriété peut contenir / suivi d'un filtre
			public CArbreProps GetArbreSousProp ( string strProp, bool bCreateIfNecessaire )
			{
				string strDebut, strFin;
				int nPos = strProp.IndexOf('.');
				if ( nPos >= 0 )
				{
					strDebut = strProp.Substring(0, nPos);
					strFin = strProp.Substring(nPos+1);
				}
				else
				{
					strDebut = strProp;
					strFin = "";
				}
                string strFiltre = "";
                if (strProp.IndexOf('/') >= 1)
                {
                    strFiltre = strProp.Split('/')[1];
                    strProp = strProp.Split('/')[0];
                }
				CArbreProps arbreCorrespondant = null;
				foreach ( CArbreProps arbre in m_listeProprietesFillesALire )
				{
					if ( arbre.ProprietePrincipale == strDebut )
					{
                        if (strFiltre == "")
                            arbre.Filtre = "";
                        else if ( arbre.Filtre.Length > 0 )
                            arbre.Filtre = "("+arbre.Filtre+") or ("+strFiltre+")";
						arbreCorrespondant = arbre;
						break;
					}
				}

				if ( bCreateIfNecessaire && arbreCorrespondant == null )
				{
					arbreCorrespondant = new CArbreProps(this, strDebut, strFiltre);
					m_listeProprietesFillesALire.Add ( arbreCorrespondant );
				}
				if ( strFin != "" )
					return arbreCorrespondant.GetArbreSousProp(strFin, bCreateIfNecessaire);
				return arbreCorrespondant;
			}

			/// ////////////////////////////////////////////////////////
			public ArrayList SousArbres
			{
				get
				{
					return m_listeProprietesFillesALire;
				}
			}

			
			///////////////////////////////////////////////////////////
			public string ProprietePrincipale
			{
				get
				{
					return m_strProprietePrincipale;
				}
				set
				{
					m_strProprietePrincipale = value;
				}
			}

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Transforme la liste en un table d'objet donnée
		/// </summary>
		/// <returns></returns>
		public Array ToArray ( Type typeElements)
		{
			if ( RemplissageProgressif )
			{
				RemplissageProgressif = false;
				AssureView();
			}
			Array liste = Array.CreateInstance ( typeElements, Count );
			int nIndex = 0;			
			foreach ( CObjetDonnee objet in this )
				liste.SetValue ( objet, nIndex++ );
			return liste;
		}

        /////////////////////////////////////////////////////////////////////////////////////////////////
        public T[] ToArray<T>()
        {
            return (T[])ToArray(typeof(T));
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Transforme la liste en un arrayList
		/// </summary>
		/// <returns></returns>
		public ArrayList ToArrayList()
		{
			if ( RemplissageProgressif )
			{
				RemplissageProgressif = false;
				AssureView();
			}
			ArrayList lst = new ArrayList();
			foreach ( object obj in this )
				lst.Add ( obj );
			return lst;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////
		public List<typeElements> ToList<typeElements>()
		{
			List<typeElements> lstRetour = new List<typeElements>();
			foreach (typeElements obj in this)
				lstRetour.Add(obj);
			return lstRetour;
		}


		public CListeObjetsDonnees GetDependances(string strProprieteDependances)
		{
			return GetDependances(strProprieteDependances, false);
		}


		/// ////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne une liste de tous les éléments liés par la propriété demandée
		/// <BR></BR>
		/// ATTENTION : ne fonctionne que pour les relations ne mettant en cause qu'UN SEUL CHAMP numérique
		/// </summary>
		/// <param name="strProprieteDependances"></param>
		/// <returns></returns>
		public CListeObjetsDonnees GetDependances ( string strProprieteDependances, bool bAvecSupprimes )
		{
			ReadDependances ( strProprieteDependances );
			string[] strProps = strProprieteDependances.Split('.');
			CListeObjetsDonnees liste = this;

			foreach ( string strProp in strProps )
			{
				CStructureTable structure = CStructureTable.GetStructure(liste.TypeObjets);
				CListeObjetsDonnees listeDep = null;
				
				foreach ( CInfoRelation relation in structure.RelationsFilles )
					if ( relation.Propriete == strProp )
						listeDep = liste.GetDependancesFilles ( relation, bAvecSupprimes );
				if ( listeDep == null )
				{
					foreach ( CInfoRelation relation in structure.RelationsParentes )
						if ( relation.Propriete == strProp )
							listeDep = liste.GetDependancesParentes ( relation, bAvecSupprimes );
				}
				if ( listeDep == null )
					return null;
				liste = listeDep;
			}
			return liste;
		}

		/// ////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDependances(CInfoRelation relation)
		{
			return GetDependances(relation, false);
		}

		/// ////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDependances ( CInfoRelation relation, bool bAvecSupprimes )
		{
			if ( relation.TableFille == CContexteDonnee.GetNomTableForType(m_typeObjet) )
				return GetDependancesParentes ( relation, bAvecSupprimes );
			return GetDependancesFilles ( relation, bAvecSupprimes );
		}

		/// ////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDependancesFilles(CInfoRelation relation)
		{
			return GetDependancesFilles(relation, false);
		}

		/// ////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDependancesFilles ( CInfoRelation relation, bool bAvecSupprimes )
		{
			string strChampInTableParente = relation.ChampsParent[0];
			string strChampInTableFille = relation.ChampsFille[0];
			CFiltreData filtre = null;
			if ( Count == 0 )
				filtre = new CFiltreDataImpossible();
			else
			{
				string strListe = "";
				AssureLectureFaite();
                foreach (CObjetDonnee objet in this)
                {
                    if (objet.Row.RowState == DataRowState.Deleted)
                        strListe += objet.Row[strChampInTableParente, DataRowVersion.Original].ToString() + ",";
                    else
                        strListe += objet.Row[strChampInTableParente].ToString() + ",";
                }
				strListe = strListe.Substring(0, strListe.Length-1);
				filtre = new CFiltreData ( strChampInTableFille+ " in ("+
					strListe+")");
				if (bAvecSupprimes)
				{
					filtre.IntegrerLesElementsSupprimes = true;
					filtre.IgnorerVersionDeContexte = true;
				}
			}


			CListeObjetsDonnees listeDependances = new CListeObjetsDonnees ( 
				ContexteDonnee, 
				CContexteDonnee.GetTypeForTable(relation.TableFille),
				filtre );
			listeDependances.PreserveChanges = true;
			listeDependances.ModeSansTri = ModeSansTri;
            listeDependances.ModeSansTri = true;
			return listeDependances;
		}

		/// ////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDependancesParentes(CInfoRelation relation)
		{
			return GetDependancesParentes(relation, false);
		}

		/// ////////////////////////////////////////////////////////////
		public CListeObjetsDonnees GetDependancesParentes ( CInfoRelation relation, bool bAvecSupprimes )
		{
			string strChampInTableParente = relation.ChampsParent[0];
			string strChampInTableFille = relation.ChampsFille[0];
			CFiltreData filtre = null;
			if ( Count == 0 )
				filtre = new CFiltreDataImpossible();
			else
			{
				Hashtable tableParentsCherches = new Hashtable();
				string strListe = "";
				AssureLectureFaite();
				foreach ( CObjetDonnee objet in this )
				{
					object val = objet.Row[strChampInTableFille];
					if ( tableParentsCherches[val] == null )
					{
						tableParentsCherches[val] = true;
						if ( val != DBNull.Value )
							strListe += val.ToString()+",";
					}
				}
				if ( strListe.Length == 0 )
					filtre = new CFiltreDataImpossible();
				else
				{
					strListe = strListe.Substring(0, strListe.Length-1 );
					filtre = new CFiltreData ( strChampInTableParente+" in ("+strListe+")");
					if ( bAvecSupprimes )
					{
						filtre.IgnorerVersionDeContexte = true;
						filtre.IntegrerLesElementsSupprimes = true;
					}
				}
				
			}
			CListeObjetsDonnees listeDependances = new CListeObjetsDonnees ( 
				ContexteDonnee, 
				CContexteDonnee.GetTypeForTable(relation.TableParente),
				filtre );
			listeDependances.PreserveChanges = true;
			listeDependances.ModeSansTri = ModeSansTri;
			return listeDependances;
		}
		
		[DynamicMethod("Reads the dependences from a child property name","")]
		public CResultAErreur LireDependances ( string strDependance )
		{
			return ReadDependances ( strDependance );
		}

		/// ////////////////////////////////////////////////////////////
		public CStockeurDonneesListe View
		{
			get
			{
				if ( m_bRemplissageProgressif )
					m_bRemplissageProgressif = false;
				AssureView();
				return m_view;
			}
		}


		/// ////////////////////////////////////////////////////////////
		/// <summary>
		/// Charge les dépendances sur une propriété
		/// La propriété peut appeler la propriété d'un fils en utilisant le point
		/// Par exemple, si une classe A a un lien fils ou parent sur une classe
		/// B via une propriété ElementB, on passera ElementB comme paramètre.
		/// Si B a une référence sur C via une propriété ElementC,
		/// On passera ElementB.ElementC pour charger tous les éléments C
		/// à partir de la liste des A.<BR></BR>
		/// ATTENTION : ne fonctionne que si la relation ne se fait que sur 1 et un seul champ numérique
		/// </summary>
		/// <param name="strProprieteDependances"></param>
		/// <returns></returns>
		public CResultAErreur ReadDependances ( params string[] strProprietesDependances )
		{
			CResultAErreur result = CResultAErreur.True;
			bool bOldEnforceConstraint = ContexteDonnee.EnforceConstraints;
			ContexteDonnee.EnforceConstraints = false;
			try
			{
				//Charge toutes les dépendances relatives à la propriété donnée en paramètre
				CStructureTable structure = CStructureTable.GetStructure(m_typeObjet);

				ArrayList lstProps = new ArrayList();
				foreach ( string strProp in strProprietesDependances )
					lstProps.Add ( strProp );
				//Tri les propriétés par ordre alpha, comme ça, on traitera 
				//ElementB, puis ElementB.ElementC !!
				lstProps.Sort();
				CArbreProps arbrePrincipal = new CArbreProps(null, "MAIN","");
				foreach ( string strProp in lstProps )
				{
					arbrePrincipal.GetArbreSousProp ( strProp, true );
				}
				return ReadDependances ( arbrePrincipal );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
			}
			finally
			{
				ContexteDonnee.EnforceConstraints = bOldEnforceConstraint;
			}
			return result;

		}

        /// <summary>
        /// Crée une liste de paquets d'ids pour lecture d'id.
        /// Chaque paquet d'id est une chaine exploitable directement dans 
        /// un in
        /// </summary>
        /// <returns></returns>
        public List<string> GetPaquetsPourLectureFils( string strChampId, DataColumn colIndiquantDependanceLue)
        {
            List<string> listePaquets = new List<string>();

            //Lit par paquets 
            //Liste paquets contient une liste d'identifiants sous la forme (1,2,3) qui
            //peut être passée à un filtre IN
            int nNbTotal = Count;
            for (int nStartPaquet = 0; nStartPaquet < nNbTotal; nStartPaquet += c_nNbLectureParLotFils)
            {
                StringBuilder bl = new StringBuilder();
                int nMax = Math.Min(nNbTotal, nStartPaquet + c_nNbLectureParLotFils);
                for (int nRow = nStartPaquet; nRow < nMax; nRow++)
                {
                    if (colIndiquantDependanceLue == null ||
                        !(bool)View.GetValeurChamp(nRow, colIndiquantDependanceLue.ColumnName))
                        bl.Append(View.GetValeurChamp(nRow, strChampId).ToString() + ",");
                }
                if (bl.Length > 0)
                {
                    bl.Remove(bl.Length - 1, 1);
                    bl.Insert(0, '(');
                    bl.Append(')');
                }
                listePaquets.Add(bl.ToString());
            }
            return listePaquets;
        }


		/// ////////////////////////////////////////////////////////////
		public CResultAErreur ReadDependances ( CArbreProps arbre )
		{
			CResultAErreur result = CResultAErreur.True;
			CStructureTable structure = CStructureTable.GetStructure(m_typeObjet);
			
			//Array de string contenant la liste des ids à lire 
			//Par paquet de c_nNbLectureParLotFils. La chaine étant dans
			//le format du IN ()			
            List<string> listePaquetsForFilleALire = null;
			Hashtable sousArbresToRemove = new Hashtable();
			foreach ( CArbreProps arbreFils in arbre.SousArbres )
			{
                IDependanceListeObjetsDonneesReader reader = null;
                if (arbreFils.ProprietePrincipale.StartsWith("#"))//C'est un champ complexe
                {
                    reader = CGestionnaireDependanceListeObjetsDonneesReader.GetReader(arbreFils.ProprietePrincipale);
                }
                else
                    reader = new CReaderDependancesListeObjetsDonneesPropriete();
                if (reader != null)
                {
                    reader.ReadArbre (
                        this,
                        arbreFils, 
                        listePaquetsForFilleALire);
                }
            }
            return result;
        }
        /*


		/////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Lit les dépendances filles du type de donné
		/// </summary>
		/// <returns></returns>
		private CResultAErreur ReadDependanceFille ( CArbreProps arbre , CInfoRelation relation, List<string> listePaquets)
		{
			CResultAErreur result = CResultAErreur.True;
			Type tpFille = null;
			PropertyInfo info = m_typeObjet.GetProperty(arbre.ProprietePrincipale);
			if ( info == null )
			{
				result.EmpileErreur(I.T("The property @1 doesn't exist|143",arbre.ProprietePrincipale));
				return result;
			}
			
			object[] attrs= info.GetCustomAttributes(typeof(RelationFilleAttribute), true);
			if (attrs == null || attrs.Length < 0 )
			{
				result.EmpileErreur(I.T("The property @1 has no 'ChildRelation' attribute|144",arbre.ProprietePrincipale));
				return result;
			}
			tpFille = ( (RelationFilleAttribute)attrs[0]).TypeFille;

            DataTable table = View.Table;
            string strKey = relation.RelationKey;
            DataColumn colDependance = table.Columns[strKey];

			if ( listePaquets == null )
			{
                listePaquets = GetPaquetsPourLectureFils(relation.ChampsParent[0], colDependance);
			}
			int nNbPaquets = listePaquets.Count;

			
			
						
			//Lit les relations par paquet
			for ( int nPaquet = 0; nPaquet < nNbPaquets; nPaquet++ )
			{
				string strPaquet = (string)listePaquets[nPaquet];
				if ( strPaquet.Length > 0 )
				{
					CListeObjetsDonnees listeFille = new CListeObjetsDonnees ( m_contexte, tpFille );
					listeFille.Filtre = new CFiltreData ( relation.ChampsFille[0]+" in "+strPaquet );
                    if (arbre.Filtre.Length > 0)
                        listeFille.Filtre.Filtre += " and " + arbre.Filtre;
					listeFille.ModeSansTri = true;
					listeFille.PreserveChanges = true;
					listeFille.AssureLectureFaite();
					int nMax = Math.Min(Count, (nPaquet+1)*c_nNbLectureParLotFils);
					
					
					if ( colDependance != null && arbre.Filtre.Length == 0)
					{
						//Indique que les lignes ont été lues
						for ( int nRow = nPaquet*c_nNbLectureParLotFils; nRow < nMax; nRow++ )
						{
							DataRow row = m_view.GetRow(nRow);
							DataRowState oldState = row.RowState;
							row[colDependance] = true;
							if ( oldState == DataRowState.Unchanged )
								row.AcceptChanges();
						}
						
					}
					listeFille.ReadDependances ( arbre );

				}
			}
			return result;
		}
        */
		
		/////////////////////////////////////////////////////////////////////////////////////////////////
		public static CResultAErreur ReadObjetsAId ( 
			CContexteDonnee contexte, 
			Type typeObjets, 
			int[] listeIds,
			bool bPreserveChanges)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom ( typeObjets ) )
			{
				result.EmpileErreur(I.T("The type @1 cannot be read by the function 'ReadObjetsAId'|146",typeObjets.ToString() ));
				return result;
			}
			
			object[] attribs = typeObjets.GetCustomAttributes ( typeof ( TableAttribute ), true );
			if ( attribs.Length == 0 )
			{
				result.EmpileErreur(I.T("No key for type @1|145",typeObjets.ToString()));
				return result;
			}
			TableAttribute attrib = (TableAttribute)attribs[0];
			string strCle = attrib.ChampsId[0];
			
			for ( int nPack = 0; nPack < Math.Min(listeIds.Length, nPack+c_nNbLectureParLotFils); nPack += c_nNbLectureParLotFils )
			{
				string strIds = "";
				for ( int nObjet = nPack; nObjet < Math.Min(listeIds.Length, nPack+c_nNbLectureParLotFils); nObjet++ )
				{
					strIds += listeIds[nObjet]+",";
				}
				if ( strIds.Length > 0 )
				{
					strIds = strIds.Substring(0, strIds.Length-1 );
					CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte, typeObjets );
					liste.Filtre = new CFiltreData ( strCle+" in ("+strIds+")");
                    liste.ModeSansTri = true;
					liste.PreserveChanges = bPreserveChanges;
					liste.AssureLectureFaite();
				}
			}
			return result;
		}
				


		/*

		/////////////////////////////////////////////////////////////////////////////////////////////////
		private CResultAErreur ReadDependanceParente ( CArbreProps arbre , CInfoRelation relation) 
		{
			CResultAErreur result = CResultAErreur.True;
			int nNbTotal = Count;
			string strKey = relation.RelationKey;
			Type tp = null;
			PropertyInfo info = m_typeObjet.GetProperty(arbre.ProprietePrincipale);
			if ( info == null )
			{
				result.EmpileErreur(I.T("The property '@1' doesn't exist|147",arbre.ProprietePrincipale));
				return result;
			}
			tp = info.PropertyType;
			if ( tp != null && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp))
			{
				//Crée les paquets
				ArrayList lstPaquets = new ArrayList();
				Hashtable tableIdsParentTraites = new Hashtable();
				int nPaquet = 0;
				string strChampFille = relation.ChampsFille[0];
				string strPaquetEnCours = "";
				int nNbInPaquet = 0;
				for ( int n = 0; n < View.Count; n++ )
				{
					DataRow row = View.GetRow ( n );
					string strCle = row[strChampFille].ToString();
					if ( strCle != "" )
					{
						if ( tableIdsParentTraites[strCle] == null )
						{
							tableIdsParentTraites[strCle] = true;
							strPaquetEnCours += strCle+",";
							nNbInPaquet++;
							if ( nNbInPaquet >= c_nNbLectureParLotFils )
							{
								strPaquetEnCours = "("+strPaquetEnCours.Substring(0, strPaquetEnCours.Length-1)+")";
								lstPaquets.Add ( strPaquetEnCours );
								strPaquetEnCours = "";
								nNbInPaquet = 0;
							}
						}
					}
				}
				if( strPaquetEnCours.Length > 0 )
				{
					strPaquetEnCours = "("+strPaquetEnCours.Substring(0, strPaquetEnCours.Length-1)+")";
					lstPaquets.Add ( strPaquetEnCours );
				}

				//Lit les relations par paquet
				int nNbPaquets = lstPaquets.Count;
				for ( nPaquet = 0; nPaquet < nNbPaquets; nPaquet++ )
				{
					string strPaquet = (string)lstPaquets[nPaquet];
					if ( strPaquet !="()" )
					{
						CListeObjetsDonnees listeParent = new CListeObjetsDonnees ( m_contexte, tp );
						listeParent.ModeSansTri = true;
						listeParent.PreserveChanges = true;
						listeParent.Filtre = new CFiltreData ( relation.ChampsParent[0]+" in "+strPaquet );
						listeParent.AssureLectureFaite();
						//Indique que les lignes ont été lues
						if ( View.Table.Columns[strKey] != null )
						{
							for ( int nRow = 0; nRow < View.Count; nRow++ )
							{
								CContexteDonnee.ChangeRowSansDetectionModification ( View.GetRow ( nRow ), strKey, true );
							}
						}
						listeParent.ReadDependances(arbre);
					}
				}
			}
				return result;
		}*/

		public static CListeObjetsDonnees GetListeFromIds(CContexteDonnee contexte,
			Type typeObjets,
			int[] nIdsObjets)
		{
			CFiltreData filtre = new CFiltreDataImpossible();
			if (nIdsObjets != null && nIdsObjets.Length > 0)
			{
				string strIds = "";
				foreach (int nId in nIdsObjets)
				{
					strIds += nId + ",";
				}
				strIds = strIds.Substring(0, strIds.Length - 1);
				filtre = new CFiltreData(
					contexte.GetTableSafe(CContexteDonnee.GetNomTableForType(typeObjets)).PrimaryKey[0]
					+ " in (" + strIds + ")");
			}
			return new CListeObjetsDonnees(contexte, typeObjets, filtre);
		}

        public void Preload(CArbreDefinitionsDynamiques arbre)
        {
            CArbreProps arbreProps = new CArbreProps(null, "MAIN", "");
            CGestionnaireDependanceListeObjetsDonneesReader.PrepareArbre(arbre, ContexteDonnee);
            FillArbreProps(arbre, arbreProps);
            ReadDependances(arbreProps);
        }

        private void FillArbreProps(CArbreDefinitionsDynamiques arbre, CArbreProps arbreProps)
        {
            foreach (CArbreDefinitionsDynamiques sousArbre in arbre.SousArbres)
            {
                if (sousArbre.DefinitionPropriete != null)
                {
                    CArbreProps sousProp = arbreProps.GetArbreSousProp(sousArbre.DefinitionPropriete.NomPropriete, true);
                    if (sousProp != null)
                    {
                        FillArbreProps(sousArbre, sousProp);
                    }
                }
            }
        }

        
        /// <summary>
        /// Tente de convertir un IEnumerable en CListeObjetDonnee;
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static CListeObjetsDonnees CreateListFrom(CContexteDonnee ctx, IEnumerable list)
        {
            if (list == null)
                return null;
            CListeObjetsDonnees listeRetour = list as CListeObjetsDonnees;
            if (listeRetour != null)
                return listeRetour;
            Type tp = null;
            List<int> lstIds = new List<int>();
            foreach (object obj in list)
            {
                if (obj != null)
                {
                    CObjetDonneeAIdNumerique objAId = obj as CObjetDonneeAIdNumerique;
                    if (objAId == null)
                        return null;
                    if (tp == null)
                        tp = obj.GetType();
                    if (obj.GetType() != tp)
                        return null;
                    lstIds.Add(objAId.Id);
                }
            }
            if (tp != null)
            {
                return GetListeFromIds(ctx, tp, lstIds.ToArray());
            }
            return null;
        }

    }

}
