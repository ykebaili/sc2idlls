using System;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CContexteDonnee.
	/// </summary>

    public delegate void OnInvalideCachesRelations(DataRow row, DataRelation relation);
    [Serializable]
	public class CContexteDonnee : 
        DataSet, 
        IObjetAttacheASession, 
        ISerializable, 
        IObjetAContexteDonnee,
        IContexteDonnee
	{
		private int m_nIdSession = -1;

		#region CONSTANTES
		/// <summary>
		/// Champ auto incrément local qui permet de conserver une clé
		/// fixe indépendante de la base de données
		/// </summary>
		[NonSerializedAttribute]
		public const string c_colLocalKey = "__LOCAL_KEY";

        [NonSerialized]
        public const string c_colIdContexteCreation = "__CTX_CREATE_ID";

        [NonSerialized]
        ///Extended property de table qui contient la liste des colonnes
        ///qui ne sont pas dans la base, mais utilisées en cache local
        public const string c_extPropTableColNotInDb = "_EXT_NOT_IN_DB_COLS";

        [NonSerialized]
        private Dictionary<string, List<CInfoRelation>> m_dicTableToRelations = new Dictionary<string, List<CInfoRelation>>();

        

		public const bool c_bTrace = true;

		/// <summary>
		/// true si la ligne doit être lue, false sinon
		/// la colonne est masqué car elle ne doit pas être modifiée directement
		/// sinon, le rowStatus en tient compte et il ne faut pas !
		/// </summary>
		public const string c_colIsToRead = "__ISTOREAD";

		/// <summary>
		/// True si la ligne est hors de la version en cours,
		/// false sinon
		/// </summary>
		[NonSerializedAttribute]
		public const string c_colIsHorsVersion = "__ISOUTVERS";

		/// <summary>
		/// true si la ligne vient de la base de données
		/// </summary>
		[NonSerializedAttribute]
		public const string c_colIsFromDb = "__ISFROMDB";

		/// <summary>
		/// Propriété étendue d'une relation qui indique si c'est une composition
		/// </summary>
		[NonSerialized]
		public const string c_extPropRelationComposition = "__IsComposition";


        private static Dictionary<string, List<CInfoRelation>> m_dicTablesAvecCascadeManuelle = new Dictionary<string, List<CInfoRelation>>();


		/// <summary>
		/// Id de la version pour laquelle les données sont lues.
		/// Si null, les données sont lues normallement dans les tables de la base de données,
		/// sinon, les données représentent leur valeur dans la version demandée<BR></BR>
		/// si Négatif, aucune version de travail, le contexte travaille dans toutes les
		/// versions. La sauvegarde est alors impossible !
		/// Un id négatif est utilisé par exemple lors d'un travail sur les CVersionDonnee,
		/// où l'on souhaite ignorer toute notion de version
		/// </summary>
		private int? m_nIdVersionDeTravail = null;

        private bool m_bDisableHistorisation = false;

		private Hashtable m_tableIdsVersionValides = new Hashtable();

		#endregion
		//Astuce pour que le Clone() ne recopie pas la structure
		//Lors de la création d'un Contexte de données, il faut le mettre à true
		//Par le biais du constructeur
		private bool m_bEnableAutoStructure = false;

		//Paramètrages pour communication
		[NonSerializedAttribute]
		protected static CMappeurTableToObjectClass m_mappeurTablesToClass = new CMappeurTableToObjectClass();


		[NonSerialized]
		protected static Dictionary<string, string> m_nomTablesToNomTableInDb = new Dictionary<string, string>();
		[NonSerialized]
		protected static Dictionary<string, List<string>> m_nomTablesInDbToNomTable = new Dictionary<string, List<string>>();
		[NonSerialized]
		///Indique que la gestion des tables completes est activé
		private bool m_bGestionParTablesCompletes = false;



		/// <summary>
		/// Stocke toutes les relations CInfoRelation connues
		/// </summary>
		[NonSerializedAttribute]
		private static ArrayList m_listeRelations = new ArrayList();

		/// <summary>
		/// Stocke toutes les relations de type TypeId (dynamiques)
		/// connues par le système. (RelationTypeIdAttribute)
		/// </summary>
		[NonSerialized]
		private static ArrayList m_listeRelationsTypeId = new ArrayList();

        [NonSerialized]
        private static List<string> m_listeTablesAInsererApresRelationTypeId = new List<string>();

		[NonSerialized]
		private bool m_bModeDeconnecte = false;

		[NonSerialized]
		protected CRecepteurNotification m_recepteurNotificationsModifs;
		[NonSerialized]
		protected CRecepteurNotification  m_recepteurNotificationsAjout;

		protected bool m_bCanReceiveNotifications = false;

		private bool m_bEnableTraitementAvantSauvegarde = true;
        private bool m_bEnableTraitementsExternes = true;

		/// <summary>
		/// Si true, un VerifieDonnee à la sauvegarde qui genère un avertissement n'est pas
		/// bloquant pour la sauvegarde
		/// </summary>
		private bool m_bIgnorerAvertissementALaSauvegarde = false;

		[NonSerialized]
		private CContexteDonnee m_contextePrincipal = null;

		private DateTime m_dateHeureInitialisation = DateTime.Now;

		//Pour identifier les datasets en déboguage
		private static int m_numeroteurContexteDonnee = 0;

		private int m_nNumeroContexteDonnee = 0;

		private bool m_bAlwaysPreserveChange = false;

        private HashSet<string> m_tablesPourLesquellesDesactiverIdAuto = new HashSet<string>();


        /// <summary>
        /// Stef 23/01/2011 : suppression du système
        /// d'incrément qui augment
        /// Maintenant, on sait si un élément a été
        /// créé dans un contexte, parce qu'il a une colonne
        /// qui indique l'id du contexte dans lequel il a été créé
        /// </summary>
		/*private int m_nStartIncrement = -1;

		/// <summary>
		/// Lorsqu'un contexte d'édition est créé, les incréments auto de ce contexte
		/// commencent à m_nStartIncrement. Quand un contexte crée un autre contexte,
		/// il indique que l'incrément commence à son incrément + c_nPasIncrementParContexte
		/// </summary>
		private static int c_nPasIncrementParContexte = -10000;*/

		
		private static string c_cleIsColIndicateurChargementForeignKey = "IS_FOREIGN_KEY";

        /// <summary>
        /// Identifiant texte du contexte (pas du contexte de donnée)
        /// dans lequel sont inscrites les modifications. Par exemple,
        /// un process peut changer le contexte de modifications pour
        /// outrepasser des droits.
        /// </summary>
        private string m_strIdModificationContextuelle = "";

		/// <summary>
		/// Nom de table à gestion par table complete qu'il faut recharger->true
		/// </summary>
		[NonSerialized]
		private Hashtable m_tablesCompletesInvalides = new Hashtable();

		/// <summary>
		/// Permet de stocker les notifications reçues durant les opérations de mise à jour
		/// </summary>
		[NonSerialized]
		internal List<IDonneeNotification> m_pileNotificationsEnAttente = new List<IDonneeNotification>();
		internal bool m_bBloqueNotifications = false;

        //Si non vide, chaque fois qu'une ligne est inserée dans ce contexte,
        //son contexte de modification est égale à cette valeur;
        private string m_strContexteModification = "";

		private class CBloqueurNotifications : IDisposable
		{
			private CContexteDonnee m_contexte = null;

            /// <summary>
            /// si ce bloqueur a créé un autre bloqueur (
            /// par exemple, le bloqueur sur un contexte d'édition  crée un bloqueur sur
            /// le contexte principal
            /// </summary>
            private CBloqueurNotifications m_bloqueurFils = null;

            /// <summary>
            /// Stef le 27/03/2013
            /// Indique que ce bloqueur est celui qui est actif sur le contexte
            /// Il peut exister des bloqueurs non bloquants quand le contexte
            /// était déjà bloqué lorsque le bloqueur a été créé.
            /// </summary>
            private bool m_bIsBloquant = true;


			public CBloqueurNotifications ( CContexteDonnee contexte )
			{
				m_contexte = contexte;
                if (!m_contexte.m_bBloqueNotifications)
                {
                    m_contexte.m_bBloqueNotifications = true;
                    if (contexte.ContextePrincipal != null)
                    {
                        m_bloqueurFils = new CBloqueurNotifications(contexte.ContextePrincipal);
                    }
                    m_bIsBloquant = true;
                }
                else
                    m_bIsBloquant = false;
			}

			public void  Dispose()
			{
                if (m_bIsBloquant)
                {
                    if (m_bloqueurFils != null)
                    {
                        m_bloqueurFils.Dispose();
                        m_bloqueurFils = null;
                    }
                    List<IDonneeNotification> copie = new List<IDonneeNotification>(m_contexte.m_pileNotificationsEnAttente);
                    m_contexte.m_pileNotificationsEnAttente.Clear();
                    m_contexte.m_bBloqueNotifications = false;
                    while (copie.Count != 0)
                    {
                        IDonneeNotification donnee = copie[0];
                        if (donnee is CDonneeNotificationAjoutEnregistrement)
                            m_contexte.OnReceiveNotificationAjout(donnee);
                        if (donnee is CDonneeNotificationModificationContexteDonnee)
                            m_contexte.OnReceiveNotificationModif(donnee);
                        copie.RemoveAt(0);
                    }
                }
					
			}
		}

        public CContexteDonnee GetNewContexteDonneeInSameThread(int nIdSession, bool bAvecNotifications)
        {
            CContexteDonnee ctx = new CContexteDonnee(nIdSession, true, bAvecNotifications);
            return ctx;
        }

        public string ContexteModification
        {
            get
            {
                return m_strContexteModification;
            }
            set
            {
                m_strContexteModification = value;
            }
        }
	
		

		#region Construction et initialisation

#if PDA
#else

        ////////////////////////////////////////////////////////////////////////////
        public CContexteDonnee( System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext contexte)
			//:base ( info, contexte)
		{
			AdoNetHelper.DeserializeDataSet(this, (byte[])info.GetValue("DATASET_DATA", typeof(byte[])));
            m_nIdSession = info.GetInt32("m_nIdSession");
			m_nIdVersionDeTravail = (int?)info.GetValue("m_nIdVersionDeTravail", typeof(int?));
			m_tableIdsVersionValides = new Hashtable();
			int nNbVersion = (int)info.GetValue("m_tableIdsVersionValides", typeof(int));
			for ( int nIndex = 0; nIndex < nNbVersion; nIndex++ )
			{
				int nVal = (int)info.GetValue ( "m_tableIdsVersionValides"+nIndex, typeof(int) );
				m_tableIdsVersionValides[nVal] = true;
			}
			m_tableIdsVersionValides[DBNull.Value] = true;
            m_bDisableHistorisation = (bool)info.GetValue("m_bDisableHistorisation", typeof(bool));
            m_bEnableTraitementsExternes = (bool)info.GetValue("m_bEnableTraitementsExternes", typeof(bool));
			CGestionnaireObjetsAttachesASession.AttacheObjet(m_nIdSession, this);
            m_strContexteModification = info.GetString("m_strContexteModification");
			Init( false);
		}

        ////////////////////////////////////////////////////////////////////////////
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //base.GetObjectData(info, context);
			info.AddValue("DATASET_DATA", AdoNetHelper.SerializeDataSet(this));
            info.AddValue( "m_nIdSession", m_nIdSession);
			info.AddValue("m_nIdVersionDeTravail", m_nIdVersionDeTravail);
            info.AddValue("m_bDisableHistorisation", m_bDisableHistorisation);
            info.AddValue("m_bEnableTraitementsExternes", m_bEnableTraitementsExternes);
			info.AddValue("m_tableIdsVersionValides", m_tableIdsVersionValides.Count - (m_tableIdsVersionValides.Contains(DBNull.Value)?1:0));
			int nVal = 0;
			foreach (object valeur in m_tableIdsVersionValides.Keys)
				if ( valeur is int )
					info.AddValue("m_tableIdsVersionValides" + nVal.ToString(), valeur);
            info.AddValue("m_strContexteModification", m_strContexteModification);
          /* info.AddValue("m_bEnableAutoStructure", m_bEnableAutoStructure);
            info.AddValue("m_bCanReceiveNotifications", m_bCanReceiveNotifications);
            info.AddValue("m_bEnableTraitementAvantSauvegarde", m_bEnableTraitementAvantSauvegarde);
            info.AddValue("m_nNumeroContexteDonnee", m_nNumeroContexteDonnee);
            info.AddValue("m_dateHeureInitialisation", m_dateHeureInitialisation);
            info.AddValue("m_bAlwaysPreserveChange", m_bAlwaysPreserveChange);*/
            

        }



#endif
		////////////////////////////////////////////////////////////////////////////
		public CContexteDonnee()
			:base()
		{
			Init( false );
		}

		public event EventHandler OnChangeVersionDeTravail;


        ////////////////////////////////////////////////////////////////////////////
        public int IdContexteDonnee
        {
            get
            {
                return m_nNumeroContexteDonnee;
            }
        }
		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Permet de définir la version avec laquelle ce contexte de données
		/// travaille
		/// </summary>
		/// <param name="nVersion"></param>
		public CResultAErreur SetVersionDeTravail(int? nVersion, bool bDeclencheEvenements)
		{
			CResultAErreur result = CResultAErreur.True;
			using (CBloqueurNotifications bloquer = new CBloqueurNotifications(this))
			{
				if (nVersion != m_nIdVersionDeTravail)
				{
					if (IsEnEdition)
						throw new Exception(I.T("Changing version in an edition context is forbidden|184"));
					RejectChanges();
					m_tableIdsVersionValides = new Hashtable();
					m_tableIdsVersionValides.Add(DBNull.Value, true);
					CVersionDonnees version = null;
					if (nVersion != null)
					{
						version = new CVersionDonnees(this);
						if (!version.ReadIfExists((int)nVersion))
							version = null;
						if (version != null && bDeclencheEvenements)
							result = version.CanUse();
						if (!result)
							return result;
					}
					StringBuilder blIdsVersions = new StringBuilder();
					while (version != null)
					{
						blIdsVersions.Append ( version.Id );
						blIdsVersions.Append(',');
						m_tableIdsVersionValides.Add(version.Id, true);
						version = version.VersionParente;
					}
					
					//Stocke les suppressions
					//Type.ToString()+"/"+Id.toString();
					Hashtable tableSuppressions = new Hashtable();
					if (blIdsVersions.Length > 0)
					{
						blIdsVersions.Remove(blIdsVersions.Length - 1, 1);
						CListeObjetsDonnees listeSuppressions = new CListeObjetsDonnees(this, typeof(CVersionDonneesObjet));
						listeSuppressions.Filtre = new CFiltreData(
							CVersionDonnees.c_champId + " in (" + blIdsVersions + ") and " +
							CVersionDonneesObjet.c_champTypeOperation + "=@1",
							(int)CTypeOperationSurObjet.TypeOperation.Suppression);
						foreach (CVersionDonneesObjet data in listeSuppressions)
							tableSuppressions[data.StringTypeElement + "/" + data.IdElement] = true;
					}
					foreach (DataTable table in Tables)
					{
						if (table.PrimaryKey.Length > 0)
						{
							bool bHasVersion = table.Columns.Contains(CSc2iDataConst.c_champIdVersion);
							List<DataColumn> colsBlob = new List<DataColumn>();
							if (bHasVersion)
							{
								foreach (DataColumn col in table.Columns)
									if (col.DataType == typeof(CDonneeBinaireInRow))
										colsBlob.Add(col);
								Type tp = GetTypeForTable(table.TableName);
								if (tp != null && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp))
								{
									string strType = tp.ToString();
									if (typeof(IObjetALectureTableComplete).IsAssignableFrom(tp))
										m_tablesCompletesInvalides[table.TableName] = true;
									string strPrimKey = table.PrimaryKey[0].ColumnName;
									foreach (DataRow row in new ArrayList(table.Rows))
									{
										if (row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
										{
											//On ne garde pas les éléments qui ne sont pas dans le référentiel
											if (table.Columns.Contains(CSc2iDataConst.c_champIdVersion))
											{
												SetIsToRead(row, true);
												foreach (DataColumn col in colsBlob)
												{
													ChangeRowSansDetectionModification(row, col.ColumnName, DBNull.Value);
												}
												if (!m_tableIdsVersionValides.Contains(row[CSc2iDataConst.c_champIdVersion]))
												{
													//La ligne n'est pas dans la version
													SetIsHorsVersion(row, true);
												}
												else
												{
													if (nVersion >= 0 && tableSuppressions.Contains(strType + "/" + row[strPrimKey].ToString()))
														SetIsHorsVersion(row, true);
													else
														SetIsHorsVersion(row, false);

												}
											}
										}
									}
								}
							}
						}
						//Supprime les colonnes de dépendances
						foreach (DataRelation relation in table.ChildRelations)
						{
							string strForeignKey = GetForeignKeyName(relation);
							if (table.Columns.Contains(strForeignKey))
							{
                                foreach (DataRow row in new ArrayList(table.Rows))
                                    //ChangeRowSansDetectionModification(row, strForeignKey, false);
                                    InvalideCacheDependance(row, relation);
							}
						}
                        foreach ( RelationTypeIdAttribute relTypeId in RelationsTypeIds )
                        {
                            string strNomCol = RelationTypeIdAttribute.GetNomColDepLue ( relTypeId.TableFille );
                            if ( table.Columns.Contains ( strNomCol ))
                            {
                                foreach ( DataRow row in new ArrayList(table.Rows) )
                                    InvalideCacheDependance ( row, strNomCol );
                            }
                        }
					}
					m_nIdVersionDeTravail = nVersion;
					if (OnChangeVersionDeTravail != null)
						OnChangeVersionDeTravail(this, null);
				}
			}
			return result;
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retire une row du contexte et toutes ces filles.
		/// Cette fonction appelle Remove sur la DataRowCollection et non pas
		/// delete, les modifs ne seront donc pas sauvegardées.
		/// Cette fonction doit être utilisée uniquement pour nettoyer le dataset
		/// </summary>
		/// <param name="row"></param>
		private void RemoveElementDuContexteAvecFilles(DataRow row)
		{
			foreach (DataRelation relation in row.Table.ChildRelations)
			{
				foreach (DataRow rowFille in row.GetChildRows(relation))
				{
					RemoveElementDuContexteAvecFilles(rowFille);
				}
			}
			row.Delete();
			row.AcceptChanges();
		}

		////////////////////////////////////////////////////////////////////////////
		public int? IdVersionDeTravail
		{
			get
			{
				return m_nIdVersionDeTravail;
			}
		}

        ////////////////////////////////////////////////////////////////////////////
        public bool DisableHistorisation
        {
            get
            {
                return m_bDisableHistorisation;
            }
            set
            {
                m_bDisableHistorisation = value;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        public bool EnableTraitementsExternes
        {
            get
            {
                return m_bEnableTraitementsExternes;
            }
            set
            {
                m_bEnableTraitementsExternes = value;
            }
        }




		////////////////////////////////////////////////////////////////////////////
		public CContexteDonnee ( int nIdSession, bool bEnableAutoStructure, bool bReceiveNotifications )
			:base()
		{
			m_nIdSession = nIdSession;
			Init(  bReceiveNotifications );
			m_bEnableAutoStructure = bEnableAutoStructure;
			CGestionnaireObjetsAttachesASession.AttacheObjet ( nIdSession, this );
		}

		/// //////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////
		///Se produit lorsqu'on ajoute ou supprime une table de la liste des tables
		protected void OnChangeCollectionTablesParent ( object sender, System.ComponentModel.CollectionChangeEventArgs args )
		{
			if ( args.Action == System.ComponentModel.CollectionChangeAction.Add )
				GetTableSafe ( ((DataTable)args.Element).TableName );
		}

		public CContexteDonnee GetContexteEdition()
		{
			CContexteDonnee ctx = (CContexteDonnee)Clone();
			ctx.m_nIdVersionDeTravail = m_nIdVersionDeTravail;
            ctx.IdModificationContextuelle = IdModificationContextuelle;
			ctx.m_bGestionParTablesCompletes = false;
            if ( m_tableIdsVersionValides != null )
                ctx.m_tableIdsVersionValides = m_tableIdsVersionValides.Clone() as Hashtable;

			/*3/3/2008 : Le incrementSeed de chaque table doit
			 * est augmenté pour le contexte d'édition, pour éviter des
			 * chevauchements entre le contexte d'édition et le contexte principal
			 * ainsi, un nouvel élément contenu dans le contexte d'édition ne peut pas avoir le 
			 * même id qu'un nouvel élément du contexte principal*/

			/*ctx.m_nStartIncrement = m_nStartIncrement + c_nPasIncrementParContexte;

			foreach (DataTable tbl in ctx.Tables)
			{
				foreach (DataColumn col in tbl.PrimaryKey)
					if (col.AutoIncrement == true)
						col.AutoIncrementSeed = ctx.m_nStartIncrement;
			}*/

			/*foreach ( DataTable tbl in ctx.Tables )
			{
				Type tp = GetTypeForTable ( tbl.TableName );
				if ( typeof(IObjetALectureTableComplete).IsAssignableFrom(tp) )
					ctx.m_tablesCompletesInvalides[tbl.TableName] = true;
			}*/
			ctx.m_contextePrincipal = this;
			ctx.m_contextePrincipal.Tables.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler ( ctx.OnChangeCollectionTablesParent );
			ctx.FreeRecepteurNotifications();
			//Comme ça, le contexte d'édition est en mode déconnecté.
			//Quand on appelle 'CommitEdit', les modifs ne sont pas
			//inscrites dans la base, mais bien dans le contexte appelant.
			//le 1/10, idem dans 
			if ( IsModeDeconnecte || IsEnEdition)
				ctx.BeginModeDeconnecte();
			//objet.ContexteDonnee.CopieStructureTo ( ctx );
			ctx.SetEnableAutoStructure ( true );
			return ctx;
		}


        /// //////////////////////////////////////////////////////////////////////////////
        public void ReloadTableComplete(string strNomTable)
        {
            if (m_tablesCompletesInvalides[strNomTable] == null)
            {
                m_tablesCompletesInvalides[strNomTable] = true;
                if (ContextePrincipal != null)
                    ContextePrincipal.ReloadTableComplete(strNomTable);
            }
        }


		/// //////////////////////////////////////////////////////////////////////////////
		public static CContexteDonnee GetContexteEditionFor ( CObjetDonnee objet )
		{
			CContexteDonnee ctx = objet.ContexteDonnee.GetContexteEdition();
			objet.ContexteDonnee.CopieRowTo ( objet.Row, ctx, true, true, true );
			return ctx;
		}
		////////////////////////////////////////////////////////////////////////////
		public bool IsEnEdition
		{
			get
			{
				return m_contextePrincipal != null;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public CContexteDonnee ContextePrincipal
		{
			get
			{
				return m_contextePrincipal;
			}
		}

		/// ///////////////////////////////////////////////////////
		private void InitializeComponent()
		{
#if PDA
#else
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
#endif
		}

		protected override void Dispose(bool disposing)
		{
			if ( true )
			{
				try
				{
                    CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSession, this);
                    if ( m_contextePrincipal != null )
                        m_contextePrincipal.Tables.CollectionChanged -= new System.ComponentModel.CollectionChangeEventHandler(OnChangeCollectionTablesParent);
					
				}
				catch
				{
				}
				FreeRecepteurNotifications();
			}
		}


		

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'objet demandé dans le ce contexte
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>

		public CObjetDonnee GetObjetInThisContexte(CObjetDonnee objet)
		{
			if (objet.ContexteDonnee.Equals(this))
				return objet;
			DataTable table = GetTableSafe(objet.GetNomTable());
			CObjetDonnee nouvelObjet = GetNewObjetForTable(table);
			nouvelObjet.PointeSurLigne(objet.GetValeursCles());
			return nouvelObjet;
		}
        
		////////////////////////////////////////////////////////////////////////////
		private void Init( bool bAvecNotifications )
		{
			m_tableIdsVersionValides[DBNull.Value] = true;
            RemotingFormat = SerializationFormat.Binary;
			m_nNumeroContexteDonnee = m_numeroteurContexteDonnee++;
			InitializeComponent();
			m_bCanReceiveNotifications = bAvecNotifications;
			CanReceiveNotifications = bAvecNotifications;
			Tables.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler ( OnChangeCollectionTables );
            foreach (DataTable table in Tables)
            {
                table.RowChanging += new DataRowChangeEventHandler(OnChangingRow);
                table.RowDeleting += new DataRowChangeEventHandler(OnDeletingRow);
            }
		}
               

		////////////////////////////////////////////////////////////////////////////
		protected void StartRecepteurNotifications()
		{
			if ( m_recepteurNotificationsModifs == null && m_bCanReceiveNotifications )
			{
				m_recepteurNotificationsModifs = new CRecepteurNotification(IdSession, typeof(CDonneeNotificationModificationContexteDonnee));
				m_recepteurNotificationsModifs.OnReceiveNotification += new NotificationEventHandler(OnReceiveNotificationModif);
			}
			if (m_recepteurNotificationsAjout == null && m_bCanReceiveNotifications)
			{
				m_recepteurNotificationsAjout = new CRecepteurNotification(IdSession, typeof(CDonneeNotificationAjoutEnregistrement));
				m_recepteurNotificationsAjout.OnReceiveNotification += new NotificationEventHandler(OnReceiveNotificationAjout);
			}
		}

		////////////////////////////////////////////////////////////////////////////
		protected void FreeRecepteurNotifications()
		{
			if ( m_recepteurNotificationsModifs != null )
			{
				m_recepteurNotificationsModifs.Dispose();
				m_recepteurNotificationsModifs = null;
			}
			if ( m_recepteurNotificationsAjout != null )
			{
				m_recepteurNotificationsAjout.Dispose();
				m_recepteurNotificationsAjout = null;
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public bool CanReceiveNotifications
		{
			get
			{
				return m_bCanReceiveNotifications;
			}
			set
			{
				m_bCanReceiveNotifications = value;
				if ( m_bCanReceiveNotifications )
					StartRecepteurNotifications();
				else
					FreeRecepteurNotifications();
			}
		}



		////////////////////////////////////////////////////////////////////////////
		protected virtual void OnReceiveNotificationModif ( IDonneeNotification donnee )
		{
			if (m_bBloqueNotifications)
			{
				m_pileNotificationsEnAttente.Add(donnee);
				return;
			}
			try
			{
				if ( !(donnee is CDonneeNotificationModificationContexteDonnee))
					return;
				//Marque tous les éléments comme étant à relire depuis la base de données
				CDonneeNotificationModificationContexteDonnee donneeModif = (CDonneeNotificationModificationContexteDonnee)donnee;

                
				//Liste des InfoNotification de suppression par table
				Hashtable tableSuppressions = new Hashtable();
				
				//Fait les modifications
				foreach ( CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in donneeModif.ListeModifications )
				{
                    foreach (string strNomTableInContexte in GetTablesImpacteesParModification ( info.NomTable ))
                    {
                        if (info.Deleted)
                        {
                            ArrayList lst = (ArrayList)tableSuppressions[strNomTableInContexte];
                            if (lst == null)
                            {
                                lst = new ArrayList();
                                tableSuppressions[strNomTableInContexte] = lst;
                            }
                            lst.Add(info);
                        }
                        else
                        {
                            DataTable table = Tables[strNomTableInContexte];
                            if (table != null &&
                                table.PrimaryKey.Length == info.ValeursCle.Length &&
                                table.PrimaryKey.Length != 0 &&
                                table.Columns[c_colIsToRead] != null)
                            {
                                DataRow row = table.Rows.Find(info.ValeursCle);
                                if (row != null)
                                {
                                    DoActionSurRowNotifieeModifiee(row);
                                }
                            }
                        }
                    }
				}
				//Suppressions
				ArrayList lstTables = GetTablesOrderDelete();
				foreach ( DataTable table in lstTables )
				{
					ArrayList lstDelete = (ArrayList)tableSuppressions[table.TableName];
					if ( lstDelete != null )
					{
						foreach ( CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in lstDelete )
						{
							if ( table.PrimaryKey.Length!=info.ValeursCle.Length || 
								table.PrimaryKey.Length==0 )
								return;
							DataRow rowTrouvee= table.Rows.Find(info.ValeursCle);
							if (rowTrouvee != null )
								table.Rows.Remove ( rowTrouvee );
						}
						InvalideCachesTablesParentes ( table.TableName );
					}
				}
			}
			catch
			{
			}
		}

		////////////////////////////////////////////////////////////////////////////
		protected virtual void OnReceiveNotificationAjout ( IDonneeNotification donnee )
		{
			if (m_bBloqueNotifications)
			{
				m_pileNotificationsEnAttente.Add(donnee);
				return;
			}
			try
			{
				if ( !(donnee is CDonneeNotificationAjoutEnregistrement))
					return;
				//Marque tous les éléments comme étant à relire depuis la base de données
				CDonneeNotificationAjoutEnregistrement donneeAjout = (CDonneeNotificationAjoutEnregistrement)donnee;
                foreach (string strNomTable in GetTablesImpacteesParModification(donneeAjout.NomTable))
                {
                    //Recharge la table complete si table a gestion par table complete
                    if (m_bGestionParTablesCompletes && typeof(IObjetALectureTableComplete).IsAssignableFrom(GetTypeForTable(strNomTable)))
                        //Recharge la table complete
                        m_tablesCompletesInvalides[strNomTable] = true;

                    InvalideCachesTablesParentes(strNomTable);
                }
				
			}
			catch
			{
			}
		}

        /// <summary>
        /// Lorsqu'une table est modifiée, elle peut potentiellement impacté toutes les
        /// tables qui stockent leurs données dans la même table de la base de données
        /// </summary>
        /// <param name="strNomTableInContexte"></param>
        /// <returns></returns>
        private List<string> GetTablesImpacteesParModification(string strNomTableInContexte)
        {
            List<string> lstTablesInContexte = new List<string>();
            string strNomTableInDb = GetNomTableInDbForNomTable(strNomTableInContexte);
            if (strNomTableInDb != null)
            {
                foreach (string strTableImpactee in GetNomTableForNomTableInDb(strNomTableInDb))
                    lstTablesInContexte.Add(strTableImpactee);
            }
            if (!lstTablesInContexte.Contains(strNomTableInContexte))
                lstTablesInContexte.Add(strNomTableInContexte);
            return lstTablesInContexte;
        }

        public static event OnInvalideCachesRelations OnInvalideCacheRelation;
		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique à toutes les tables parentes les dépendances sur cette table ne sont plus bonnes
		/// </summary>
		/// <param name="table"></param>
		private void InvalideCachesTablesParentes ( string strNomTable )
		{
			DataTable table = Tables[strNomTable];
			if ( table == null )
				return;
			foreach ( DataRelation rel in table.ParentRelations )
			{
				DataTable tableParente = rel.ParentTable;
				string strKey = GetForeignKeyName ( rel );
				DataColumn col = tableParente.Columns[strKey];
				if ( col != null )
				{
					DataRow[] rows = tableParente.Select ( strKey+"=1" );
					foreach ( DataRow row in rows )
					{
                        InvalideCacheDependance(row, rel);
					}
				}
			}
            foreach ( RelationTypeIdAttribute relTypeId in RelationsTypeIds )
            {
                if ( relTypeId.TableFille == strNomTable )
                {
                    string strNomColDep = relTypeId.GetNomColDepLue();
                    foreach ( DataTable tableTmp in Tables )
                    {
                        if ( tableTmp.Columns.Contains ( strNomColDep ) )
                            tableTmp.Columns.Remove ( strNomColDep );
                    }
                }
            }
		}


		////////////////////////////////////////////////////////////////////////////
		protected virtual void DoActionSurRowNotifieeModifiee ( DataRow row )
		{
			SetIsToRead ( row, true );
		}


		////////////////////////////////////////////////////////////////////////////
		public static void ResetAssembliesRegistres ( )
		{
			m_mappeurTablesToClass = new CMappeurTableToObjectClass();
		}

		////////////////////////////////////////////////////////////////////////////
		public static void AddAssembly ( System.Reflection.Assembly assembly )
		{
			foreach ( Type type in assembly.GetTypes() )
			{
				AddTypeTable ( type );
			}
		}

		public static void AddTypeTable ( Type type )
		{
			object[] attribs = type.GetCustomAttributes(typeof(ObjetServeurURIAttribute), false);
			if ( attribs.Length == 1 && !type.IsAbstract )
			{
				//Optim : enregistre le type dans ActivatorSurChaine
				CActivatorSurChaine.RegisterType(type);

				string strLoaderURI = ((ObjetServeurURIAttribute)attribs[0]).ObjetServeurURI;
				if ( strLoaderURI.IndexOf('.') < 0 )
				{
					//Ajoute l'assembly de l'objet devant l'objet serveur
					int nIndex = type.ToString().LastIndexOf('.');
					if ( nIndex >= 0 )
						strLoaderURI = type.ToString().Substring(0, nIndex)+"."+strLoaderURI;
				}
				attribs = type.GetCustomAttributes(typeof(TableAttribute), false);
				if ( attribs.Length == 1 )
				{
					string strTable = ((TableAttribute)attribs[0]).NomTable;
					bool bSynchronisable = ((TableAttribute)attribs[0]).Synchronizable;
					m_mappeurTablesToClass.SetTableMapping ( strTable, type, strLoaderURI, bSynchronisable );
					m_nomTablesToNomTableInDb[strTable] = ((TableAttribute)attribs[0]).NomTableInDb;
					List<String> lstTables = null;
					if ( !m_nomTablesInDbToNomTable.TryGetValue ( ((TableAttribute)attribs[0]).NomTableInDb, out lstTables ))
					{
						lstTables = new List<string>();
						m_nomTablesInDbToNomTable[((TableAttribute)attribs[0]).NomTableInDb] = lstTables;
					}
					lstTables.Add ( ((TableAttribute)attribs[0]).NomTable );

					///Stef 09/04/08 : la bonne idée serait de remplacer
					///ce parcours de propriétés par un CStructureTable.RelationsParentes,
					///mais hélas, le CStructureTable a besoin que les types soient
					///enregistrés dans le contextededonnées pour fonctionner,
					///donc idée bonne, mais pas réalisable
					foreach ( PropertyInfo property in type.GetProperties() )
					{
						string strNomConvivial = property.Name;
						attribs = property.GetCustomAttributes(typeof(DynamicFieldAttribute), false);
						if ( attribs.Length > 0 )
							strNomConvivial = ((DynamicFieldAttribute)attribs[0]).NomConvivial;
						attribs = property.GetCustomAttributes(typeof(RelationAttribute), false);
						if ( attribs.Length > 0 )
						{
							RelationAttribute rel = (RelationAttribute)attribs[0];
							CInfoRelation infoRelation = new CInfoRelation(rel.TableMere, strTable, rel.ChampsParent, rel.ChampsFils, rel.Obligatoire, rel.Composition, rel.Index, rel.PasserLesFilsANullLorsDeLaSuppression, rel.DeleteEnCascadeManuel) ;
                            infoRelation.NePasClonerLesFils = rel.NePasClonerLesFils;
							m_listeRelations.Add ( infoRelation );
						}
					}

					//Stef 29082008 : suppression de la relation aux version
					/*if (!typeof(IObjetSansVersion).IsAssignableFrom(type))
					{
						//Relation vers la table de versions
						m_listeRelations.Add(new CInfoRelation(
							CVersionDonnees.c_nomTable,
							strTable,
							new string[] { CVersionDonnees.c_champId },
							new string[] { CSc2iDataConst.c_champIdVersion },
							false,
							true,
							true,
							false));
					}*/


					//Relation Type/ID
					attribs = type.GetCustomAttributes(true);
					foreach ( Attribute attrib in attribs )
					{
						if ( attrib is RelationTypeIdAttribute )
							m_listeRelationsTypeId.Add ( attrib );
                        if ( attrib is InsertAfterRelationTypeIdAttribute )
                            m_listeTablesAInsererApresRelationTypeId.Add ( strTable );
					}
				}
					
			}
			//Vide le cache des relations par tables
			m_relationsParTable.Clear();
		}

		////////////////////////////////////////////////////////////////////////////
		private static Dictionary<string, CInfoRelation[]> m_relationsParTable = new Dictionary<string, CInfoRelation[]>();
		public static CInfoRelation[] GetListeRelationsTable ( string strNomTable )
		{
			CInfoRelation[] infos = null;
			if (m_relationsParTable.TryGetValue(strNomTable, out infos))
				return infos;

			ArrayList lst = new ArrayList();
			foreach ( CInfoRelation rel in m_listeRelations )
			{
				if ( rel.TableFille == strNomTable || rel.TableParente == strNomTable )
					lst.Add ( rel );
			}
			infos = (CInfoRelation[])lst.ToArray(typeof(CInfoRelation));
			m_relationsParTable[strNomTable] = infos;
			return infos;
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne toutes les relations TypeId connues
		/// </summary>
		public static RelationTypeIdAttribute[] RelationsTypeIds
		{
			get
			{
				return (RelationTypeIdAttribute[]) m_listeRelationsTypeId.ToArray(typeof(RelationTypeIdAttribute));
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public bool EnableAutoStructure
		{
			get
			{
				return m_bEnableAutoStructure;
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public void SetEnableAutoStructure ( bool bValue )
		{
			m_bEnableAutoStructure = bValue;
		}

		////////////////////////////////////////////////////////////////////////////
		public int IdSession
		{
			get
			{
				return m_nIdSession;
			}
			set
			{
				m_nIdSession = value;
			}
		}
			

		#endregion

		///////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////
		///MAPPAGE DES TYPES DE DONNEES, obtention de loaders et de nouveaux ...
		///////////////////////////////////////////////////////////////////////////
		#region Mappage de types

		////////////////////////////////////////////////////////////////////////////
		public static string GetNomTableInDbForNomTable ( string strNomTable )
		{
			string strNomTableInDb = strNomTable;
			m_nomTablesToNomTableInDb.TryGetValue(strNomTable, out strNomTableInDb);
			return strNomTableInDb;
		}

		////////////////////////////////////////////////////////////////////////////
		public static string[] GetNomTableForNomTableInDb(string strNomTableInDb)
		{
			List<string> lstTables = null;
			if (m_nomTablesInDbToNomTable.TryGetValue(strNomTableInDb, out lstTables))
				return lstTables.ToArray();
			return new string[0];
		}

		////////////////////////////////////////////////////////////////////////////
		public static IObjetServeur GetTableLoader(string strNomTable, int? nIdVersionDeTravail, int nIdSession)
		{
			string strLoader = m_mappeurTablesToClass.GetLoaderURIForTable(strNomTable);
			IObjetServeur serveur = (IObjetServeur)C2iFactory.GetNewObjetForSession(strLoader, typeof(IObjetServeur), nIdSession);
			if (serveur != null)
				serveur.IdVersionDeTravail = nIdVersionDeTravail;
			return serveur;
		}
		
		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le loader associé à une table
		/// </summary>
		public IObjetServeur GetTableLoader ( string strNomTable )
		{
			return GetTableLoader ( strNomTable, m_nIdVersionDeTravail, m_nIdSession );
		}

		////////////////////////////////////////////////////////////////////////////
		public static Type GetTypeForTable ( string strNomTable )
		{
			return m_mappeurTablesToClass.GetObjetTypeForTable(strNomTable);
		}

        ////////////////////////////////////////////////////////////////////////////
        public static Type[] GetAllTypes()
        {
            return m_mappeurTablesToClass.GetListeTypes();
        }

		////////////////////////////////////////////////////////////////////////////
		public static string GetNomTableForType ( Type tp )
		{
			string strNom = m_mappeurTablesToClass.GetNomTableForType ( tp );
			if (strNom == null && tp.BaseType != null)
				strNom = GetNomTableForType(tp.BaseType);
			return strNom;
				
		}


		////////////////////////////////////////////////////////////////////////////
		public CObjetDonnee GetNewObjetForRow ( DataRow row )
		{
			Type leType = m_mappeurTablesToClass.GetObjetTypeForTable(row.Table.TableName);
#if PDA
			CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(leType);
			objet.SetRow ( row );
			return objet;
#else
			return (CObjetDonnee)Activator.CreateInstance(leType, new Object[]{row});
#endif
		}

		////////////////////////////////////////////////////////////////////////////
		public CObjetDonnee GetNewObjetForTable ( DataTable table )
		{
			Type leType = m_mappeurTablesToClass.GetObjetTypeForTable(table.TableName);
#if PDA
			CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(leType);
			objet.ContexteDonnee = this;
			return objet;
#else
			return (CObjetDonnee)Activator.CreateInstance(leType, new object[]{this});
#endif
		}

		////////////////////////////////////////////////////////////////////////////
		private Type GetObjectTypeForTable ( string strNomTable )
		{
			return m_mappeurTablesToClass.GetObjetTypeForTable(strNomTable);
		}

		////////////////////////////////////////////////////////////////////////////
		public static CMappeurTableToObjectClass MappeurTableToClass
		{
			get
			{
				return m_mappeurTablesToClass;
			}
		}


		#endregion


		////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////
		///GESTION DES MODIFICATIONS. SUIVI DE LA LECTURE D'UNE LIGNE
		////////////////////////////////////////////////////////////////////////////
		#region Gestion de modifications
		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// ATTENTION : retourne false si le contexte était déjà en mode déconnecté
		/// </summary>
		/// <returns></returns>
        public bool BeginModeDeconnecte()
		{
            if (m_bModeDeconnecte)
                return false;
			m_bModeDeconnecte = true;
            return true;
		}

        ////////////////////////////////////////////////////////////////////////////
        public string IdModificationContextuelle
        {
            get
            {
                return m_strIdModificationContextuelle;
            }
            set
            {
                m_strIdModificationContextuelle = value;
            }
        }

		////////////////////////////////////////////////////////////////////////////
		public void EndModeDeconnecteSansSauvegardeEtSansReject()
		{
			m_bModeDeconnecte = false;
		}

		////////////////////////////////////////////////////////////////////////////
		public void CancelModifsEtEndModeDeconnecte()
		{
			RejectChanges();
			m_bModeDeconnecte = false;
		}

		////////////////////////////////////////////////////////////////////////////
		public CResultAErreur CommitModifsDeconnecte()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_bModeDeconnecte )
			{
				m_bModeDeconnecte = false;
				result = SaveAll(true);
				if ( !result )
					RejectChanges();
			}
			return result;
		}


		

		////////////////////////////////////////////////////////////////////////////
		public void RollbackModifsDeconnecte()
		{
			if (m_bModeDeconnecte )
			{
				m_bModeDeconnecte = false;
				RejectChanges();
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public bool IsModeDeconnecte
		{
			get
			{
				return m_bModeDeconnecte;
			}
		}
		
		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Change la valeur d'un champ d'une row sans modifier le flag de modification
		/// </summary>
		/// <param name="strColonne"></param>
		/// <param name="valeur"></param>
		public static void ChangeRowSansDetectionModification ( DataRow row, string strColonne, object valeur )
		{
            if (row.RowState == DataRowState.Deleted)
                return;
			DataRowState oldState = row.RowState;
			row[strColonne] = valeur;
			try
			{
				if ( oldState == DataRowState.Unchanged )
					row.AcceptChanges();
			}
			catch
			{
				//l'erreur peut être normale car
				//la ligne a pu être supprimée par cette
				//opération ( cht du nombre de références )
			}
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne l'état de lecture de la colonne depuis la base
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool IsToRead ( DataRow row )
		{
            if (row.RowState == DataRowState.Deleted)
                return false;
			return (bool)row[c_colIsToRead];
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne vrai si la ligne n'est pas dans la version du contexte
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool IsHorsVersion(DataRow row)
		{
			return (bool)row[c_colIsHorsVersion];
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Change l'état de lecture de la colonne
		/// </summary>
		/// <param name="row"></param>
		/// <param name="bValue"></param>
		public void SetIsToRead ( DataRow row, bool bValue )
		{
            if (row.RowState != DataRowState.Modified || !bValue)
            {
                ChangeRowSansDetectionModification(row, c_colIsToRead, bValue);
                if (row.Table != null)
                {
                    string[] lstColsNotInDb = row.Table.ExtendedProperties[c_extPropTableColNotInDb] as string[];
                    if (lstColsNotInDb != null)
                        foreach (string strCol in lstColsNotInDb)
                            ChangeRowSansDetectionModification(row, strCol, DBNull.Value);
                }
            }
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Change l'indicateur d'appartenance à la version en cours
		/// </summary>
		/// <param name="row"></param>
		/// <param name="bValue"></param>
		public void SetIsHorsVersion(DataRow row, bool bValue)
		{
			ChangeRowSansDetectionModification(row, c_colIsHorsVersion, bValue );
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne true si la ligne vient de la bdd
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool IsFromDb ( DataRow row )
		{
			return (bool)row[c_colIsFromDb];
		}

		////////////////////////////////////////////////////////////////////////////
		public object GetNewValeurCle ( DataRow row, string strChamp )
		{
			DataColumn col = row.Table.Columns[strChamp];
			if ( col.AutoIncrement )
				return row[strChamp];
			if ( col.DataType == typeof(Decimal) ||
				col.DataType == typeof(Double) ||
				col.DataType == typeof(Int16) ||
				col.DataType == typeof(Int32) ||
				col.DataType == typeof(Int64) ||
				col.DataType == typeof(SByte) ||
				col.DataType == typeof(Single) ||
				col.DataType == typeof(UInt16) ||
				col.DataType == typeof(UInt32) ||
				col.DataType == typeof(Byte) ||
				col.DataType == typeof(UInt64) )
				return row[c_colLocalKey];
			if ( col.DataType == typeof(string) )
				return row[c_colLocalKey].ToString();
			throw new Exception(I.T("The framework sc2i.data can't give value to the key @1 of the table @2|119",strChamp,row.Table.TableName));
		}
		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne true si la ligne vient de la bdd
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public void SetIsFromDb ( DataRow row, bool b )
		{
			ChangeRowSansDetectionModification ( row, c_colIsFromDb, b );
		}


		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Remplit une ligne avec les données correspondantes
		/// </summary>
		/// <param name="row"></param>
		public virtual DataRow ReadRow ( DataRow row, CObjetDonnee obj )
		{
			if ( m_contextePrincipal != null )
			{
				return ReadRowInContextePrincipal ( row, obj);
			}
			else
			{
				return ReadRowInDb ( row, obj );
			}
		}
		protected DataRow ReadRowInContextePrincipal ( DataRow row, CObjetDonnee obj )
		{
			//Va chercher dans le contexte principal si l'élément a été chargé
			DataTable tablePrincipale = m_contextePrincipal.GetTableSafe(row.Table.TableName);
			DataRow rowTrouvee = tablePrincipale.Rows.Find ( obj.GetValeursCles() );
			DataRow newRow;
			if ( rowTrouvee != null )
			{
				//L'élément existe dans le contexte principal 
				newRow = rowTrouvee;
				if ( IsToRead ( rowTrouvee ) )
					//Par contre, il faut le lire
					newRow = m_contextePrincipal.ReadRow(newRow, obj);

				//Copie la ligne du contexte de base vers ce contexte. Comme la ligne existe
				//Déjà, elle est simplement remplie
				row = CopieRowTo ( newRow, this, true, true, false );

				row.AcceptChanges();
				AssureParents(row);
				return row;
			}
			//L'élement n'existe pas dans le contexte principal, on demande on contexte
			//principal de le lire
			//Crée la ligne dans le contexte principal
			DataRow newRowInPrincipal = tablePrincipale.NewRow();
			SetIsToRead ( newRowInPrincipal, true );
			foreach ( DataColumn col in tablePrincipale.PrimaryKey )
				newRowInPrincipal[col] = row[col.ColumnName];
			tablePrincipale.Rows.Add ( newRowInPrincipal );
			newRowInPrincipal.AcceptChanges();
			newRow =  m_contextePrincipal.ReadRow(newRowInPrincipal, obj);
			//Et on le copie dans le contexte en cours.
			CopyRow ( newRow, row );
			//row.ItemArray = newRow.ItemArray;
			row.AcceptChanges();
			AssureParents(row);
			return row;
		}

		////////////////////////////////////////////////////////////////////////////
		protected virtual DataRow ReadRowInDb ( DataRow row, CObjetDonnee obj )
		{
			return Read ( row.Table.TableName, obj.GetChampsId(), obj.GetValeursCles(), false );
		}

		////////////////////////////////////////////////////////////////////////////
		public virtual DataRow ReadRow ( DataRow row )
		{
			string[] strChampsId = new string[row.Table.PrimaryKey.Length];
			object[] valeursCles = new Object[row.Table.PrimaryKey.Length];
			for ( int nChamp = 0; nChamp < row.Table.PrimaryKey.Length; nChamp++ )
			{
				DataColumn col = row.Table.PrimaryKey[nChamp];
				strChampsId[nChamp] = col.ColumnName;
				valeursCles[nChamp] = row[col];
			}
			return Read ( row.Table.TableName, strChampsId, valeursCles, false );

		}

		/// //////////////////////////////////////////////////////////////////////////////
		public CResultAErreur CommitEdit()
		{
			using (CBloqueurNotifications bloquer = new CBloqueurNotifications(this))
			{
				CResultAErreur result = CResultAErreur.True;
				/*if ( !HasChanges() )
					return result;*/
				if (!IsEnEdition)
				{
					result.EmpileErreur(I.T("CommitEdit Call on a edition context which is not on edition|120"));
				}
				else
				{
					//Stef 28/8/07
					//SI on ne sauve pas dans la base, il faut que les lignes
					//du contexte principal conservent le rowstate des lignes
					//du contexte, sinon, elles ne seront pas sauvées
					bool bAcceptChangesSurContextePrincipal = false;
					if (m_contextePrincipal != null && m_contextePrincipal.ContextePrincipal == null)//c'est un contexte de dernier niveau !
					{
						result = SaveAll(false);
						bAcceptChangesSurContextePrincipal = true;//C'est un contexte de dernier niveau,
						//donc on accepte les changements sur le contexte principal
					}

					if (result)
					{
						//S'assure que toutes les tables sont dans le contexte principal
						foreach (DataTable table in Tables)
							m_contextePrincipal.GetTableSafe(table.TableName);
						//répercute les modifications sur le contexte principal et les contextes du dessus
						//Le 01/10, en fait, on ne remonte pas sur tous les contexte,
						//Mais uniquement sur le contexte du dessus
						CContexteDonnee ctxDuDessus = m_contextePrincipal;
						bool bOldEnforce = ctxDuDessus.EnforceConstraints;
						ctxDuDessus.EnforceConstraints = false;
						using (CContexteDonnee ds = (CContexteDonnee)GetCompleteChanges(DataRowState.Deleted))
						{
							if (ds != null)
							{
								ds.SetEnableAutoStructure(true);
								//Effectue les suppressions
								ArrayList lstTables = ds.GetTablesOrderDelete();
								foreach (DataTable table in lstTables)
								{
									if (table.Rows.Count != 0)
									{
										DataColumn[] colCles = table.PrimaryKey;
										DataTable tableDest = ctxDuDessus.Tables[table.TableName];
										foreach (DataRow row in new ArrayList(table.Rows))
										{
											if (row.RowState == DataRowState.Deleted)
											{
												object[] cles = new object[colCles.Length];
												int nCle = 0;
												foreach (DataColumn col in colCles)
													cles[nCle++] = row[col, DataRowVersion.Original];
												DataRow rowTrouve = tableDest.Rows.Find(cles);
												if (rowTrouve != null)
												{
													//Si mode déconnecté, il faut noter la ligne comme supprimée
													if (IsModeDeconnecte || !bAcceptChangesSurContextePrincipal)
														rowTrouve.Delete();
													else
														//Sinon, on la supprime, rien de plus ne sera à faire
														tableDest.Rows.Remove(rowTrouve);
												}
											}
										}
									}
								}
							}
						}
						ctxDuDessus.EnforceConstraints = bOldEnforce;
						using (CContexteDonnee ds = (CContexteDonnee)GetCompleteChanges(DataRowState.Modified | DataRowState.Added))
						{
							if (ds != null)
							{
								ds.SetEnableAutoStructure(true);
								ArrayList lstTables = ds.GetTablesOrderInsert();
								foreach (DataTable table in lstTables)
								{
									if (table.Rows.Count != 0)
									{
										DataColumn[] colCles = table.PrimaryKey;
										DataTable tableDest = ctxDuDessus.GetTableSafe(table.TableName);
										foreach (DataRow row in new ArrayList(table.Rows))
										{
											if (
												(row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
												&& !ds.IsToRead(row))
											{
												object[] cles = new object[colCles.Length];
												int nCle = 0;
												foreach (DataColumn col in colCles)
													cles[nCle++] = row[col];
												DataRow rowTrouve = tableDest.Rows.Find(cles);
												
												if (rowTrouve != null)
												{
													CopyRow(row, rowTrouve, bAcceptChangesSurContextePrincipal && !IsModeDeconnecte);
													if (!IsModeDeconnecte && bAcceptChangesSurContextePrincipal)
														rowTrouve.AcceptChanges();
												}
												else
												{
													bool bNePasVoirCetObjet = false;
													///Stef 03102008
													///Si l'objet est nouveau, qu'il est dans cette version, et
													///que son id original est déjà dans la base, il ne faut
													///pas le voir : il s'agit de l'objet dans la version que
													///le contexte client n'a pas à connaitre !
													if (table.Columns[CSc2iDataConst.c_champIdVersion] != null &&
														table.Columns[CSc2iDataConst.c_champOriginalId] != null)
													{
														if (row[CSc2iDataConst.c_champIdVersion] is int)
														{
															if ((int)row[CSc2iDataConst.c_champIdVersion] == IdVersionDeTravail &&
																row[CSc2iDataConst.c_champOriginalId] is int)
															{
																int nIdOriginal = (int)row[CSc2iDataConst.c_champOriginalId];
																if (table.Rows.Find(nIdOriginal) != null)
																	bNePasVoirCetObjet = true;
															}
														}
													}
													if (table.Columns[CSc2iDataConst.c_champIsDeleted] != null &&
														row[CSc2iDataConst.c_champIsDeleted] is bool &&
														(bool)row[CSc2iDataConst.c_champIsDeleted])
														bNePasVoirCetObjet = true;
													if (!bNePasVoirCetObjet)
													{
														DataRow newRow = tableDest.NewRow();
														CopyRow(row, newRow, bAcceptChangesSurContextePrincipal && !IsModeDeconnecte);
														try
														{
															tableDest.Rows.Add(newRow);
														}
														catch
														{
															AssureParents(newRow);
															tableDest.Rows.Add(newRow);
														}
														if (!IsModeDeconnecte && bAcceptChangesSurContextePrincipal)
															//Si mode déconnecté, les changements seront acceptés en fin de mode déconnecté
															newRow.AcceptChanges();


														//Stef le 28/8/07 :
														//Si on n'a pas fait de accept changes,
														//Mais que la nouvelle ligne est considerée comme new
														//alors qu'elle ne l'est pas,
														//la repasse en modified.
														if (newRow.RowState == DataRowState.Added &&
															 row.RowState != DataRowState.Added)
														{
															if (row.RowState == DataRowState.Added)
															{
																newRow.AcceptChanges();
																object val = newRow[0];
																newRow[0] = DBNull.Value;
																newRow[0] = val;

															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						//AcceptChanges();
						ctxDuDessus = (ctxDuDessus).m_contextePrincipal;
					}
				}
				return result;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public void CancelEdit()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !IsEnEdition )
			{
				result.EmpileErreur(I.T("CancelEdit Call on a edition context which is not on edition|194"));
			}
			RejectChanges();
		}

		////////////////////////////////////////////////////////////////////////////
		public void AssureTableCompleteSiObjetATableComplete ( string strTable )
		{
			lock ( m_tablesCompletesInvalides )
			{
				GetTableSafe ( strTable );
				if ( m_tablesCompletesInvalides[strTable] != null )
				{
					DataTable tableSrc = IntegreTableComplete(strTable);
					/*IObjetServeur loader = GetTableLoader(strTable);
					DataTable tableSrc = loader.Read ( null );*/
					//IntegreTable ( tableSrc, false );
					m_tablesCompletesInvalides.Remove ( strTable );
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////
		//Optim  SC, 24/07/2008 : on peut maintenant aller lire dans le contexte principal
		//et plus seulement dans la base de données
		public virtual DataRow Read ( string strTable, string[] strChampsId, object[] valeursCles, bool bChercherDansContextePrincipal )
		{
			if (bChercherDansContextePrincipal)
			{
				if (m_contextePrincipal != null)
				{
					DataTable table = m_contextePrincipal.Tables[strTable];
					if (table != null)
					{
						DataRow rowExistante = null;
						if (strChampsId.Length == 1)
							rowExistante = table.Rows.Find(valeursCles[0]);
						else
							rowExistante = table.Rows.Find(valeursCles);
						if ( rowExistante != null )
						{
							//L'élément existe dans le contexte principal 
							if (IsToRead(rowExistante))
								//Par contre, il faut le lire
								rowExistante = m_contextePrincipal.ReadRow(rowExistante);
							//Copie la ligne du contexte de base vers ce contexte. Comme la ligne existe
							//Déjà, elle est simplement remplie
							DataRow laRow = CopieRowTo(rowExistante, this, false, true, false);
							laRow.AcceptChanges();
							AssureParents(laRow);
							return laRow;
						}
					}
				}
			}
			bool bIsGestionParTableComplete = m_bGestionParTablesCompletes && typeof(IObjetALectureTableComplete).IsAssignableFrom(GetTypeForTable(strTable));
			if ( bIsGestionParTableComplete && m_tablesCompletesInvalides[strTable] != null )
				AssureTableCompleteSiObjetATableComplete(strTable);
			else
			{
				//Si pas gestion par table complete, où que la table n'est pas globalement
				//invalide, on ne relit que la ligne.
				IObjetServeur loader = GetTableLoader(strTable);
				CFiltreData filtre = CFiltreData.CreateFiltreAndSurValeurs(strChampsId, valeursCles);
				//filtre.IgnorerVersionDeContexte = true;
                using (DataTable tableSrc = loader.Read(filtre))
                {
                    if (tableSrc.Rows.Find(valeursCles) == null)
                        return null;
                    IntegreTable(tableSrc, false);
                }
			}
			DataRow newRow = Tables[strTable].Rows.Find(valeursCles);
			
			//SC, 24/07/2008 : newRow peut être null, si la ligne a été supprimée
			if ( newRow != null )
				newRow.AcceptChanges();
			return newRow;
		}

		#endregion

		////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////
//		///GESTION DES TABLES, COLONNES INTERNES ET DEPENDANCES
		#region GESTION DES TABLES et des dépendances
		
		////////////////////////////////////////////////////////////////////////////
		public bool AlwaysPreserveChange
		{
			get
			{
				if ( IsEnEdition )
					return true;
				return m_bAlwaysPreserveChange;
			}
			set
			{
				m_bAlwaysPreserveChange = value;
			}
		}


        ////////////////////////////////////////////////////////////////////////////
        public void DesactiverIdAutoALaSauvegardeSur(string strTable)
        {
            m_tablesPourLesquellesDesactiverIdAuto.Add(strTable);
        }

        ////////////////////////////////////////////////////////////////////////////
        public void ReActiverIdAutoALaSauvegardeSur(string strTable)
        {
            m_tablesPourLesquellesDesactiverIdAuto.Remove(strTable);
        }

        ////////////////////////////////////////////////////////////////////////////
        public bool ShouldDesactiveIdAutoOnTable(string strTable)
        {
            return m_tablesPourLesquellesDesactiverIdAuto.Contains(strTable);
        }

		////////////////////////////////////////////////////////////////////////////
		public bool IgnoreAvertissementsALaSauvegarde
		{
			get
			{
				return m_bIgnorerAvertissementALaSauvegarde;
			}
			set
			{
				m_bIgnorerAvertissementALaSauvegarde = value;
			}
		}

		
		//--------------------------------------------------
		/// <summary>
		/// Indique que l'on travaille par tables completes pour les éléments
		/// IObjetALectureTableComplete
		/// </summary>
		public bool GestionParTablesCompletes
		{
			get
			{
				return (this.m_bGestionParTablesCompletes);
			}
			set
			{
				this.m_bGestionParTablesCompletes = value;
			}
		}

		
		////////////////////////////////////////////////////////////////////////////
		///Intègre une table dans le dataset (merge)
		public DataTable IntegreTable( DataTable table, bool bPreserveChanges )
		{
			if ( m_bAlwaysPreserveChange )
				bPreserveChanges = true;
			if (table.Columns[c_colIsHorsVersion] == null)
			{
				DataColumn col = new DataColumn(c_colIsHorsVersion, typeof(bool));
				col.DefaultValue = false;
				table.Columns.Add(col);
			}
			if (table.Columns[c_colIsToRead] == null)
			{
				DataColumn col = new DataColumn(c_colIsToRead, typeof(bool));
				col.DefaultValue = false;
				table.Columns.Add ( col );
			}
			
			if ( table.Columns[c_colIsFromDb] == null )
			{
				DataColumn col = new DataColumn ( c_colIsFromDb, typeof(bool));
				col.DefaultValue = true;
				table.Columns.Add ( col );
			}
			try
			{
				bool bOldEnforce = EnforceConstraints;
                //stef  06/04/2013 : enforceConstraint à true dés le début, ça permet de gagner quelques
                //millisecondes en moyenne. Du coup, masque TryCatch
                EnforceConstraints = false;
				//Si preserve les changements, passe par la fonction SC2I
				//Pour lire les lignes avec ColisToRead à true
				if(  bPreserveChanges && Tables[table.TableName] != null)
					CUtilDataSet.Merge ( table, this, true );
				else
					Merge ( table, false, MissingSchemaAction.Add );
				//S'assure que tous les parents sont biens chargés
				ArrayList lst = new ArrayList(Tables[table.TableName].Rows);
				bool bHasVersion = Tables[table.TableName].Columns.Contains(CSc2iDataConst.c_champIdVersion);
				foreach ( DataRow row in lst )
				{
					AssureParents ( row );
                    if (IdVersionDeTravail >= 0 && bHasVersion && !m_tableIdsVersionValides.ContainsKey(row[CSc2iDataConst.c_champIdVersion]))
						SetIsHorsVersion(row, true );
				}
				if ( EnforceConstraints != bOldEnforce )
					EnforceConstraints = bOldEnforce ;
			}
			catch ( Exception e )
			{
				System.Console.WriteLine("Erreur "+e.ToString());
			}
           
			return Tables[table.TableName];
		}

#if PDA
		/// ////////////////////////////////////////////////////////////////////////
		public void Merge ( DataTable tableSource, bool bPreseveChanges, MissingSchemaAction missingAction )
		{
			CUtilDataSet.Merge ( tableSource, this, bPreseveChanges );
		}

#endif

		////////////////////////////////////////////////////////////////////////////
		public void SupprimeTableEtContraintes ( string strNomTable )
		{
			SupprimeTableEtContraintes(Tables[strNomTable]);
		}
		////////////////////////////////////////////////////////////////////////////
		public void SupprimeTableEtContraintes( DataTable table )
		{
			if ( table == null )
				return;
			int nNbRelations = table.ChildRelations.Count;
			for ( int n = nNbRelations-1; n >= 0; n-- )
				table.ChildRelations.RemoveAt ( n );
			nNbRelations = table.ParentRelations.Count;
			for ( int n = nNbRelations-1; n >= 0; n-- )
			{
				//Supprime la colonne de relation
				DataTable tableFille = table.ParentRelations[n].ChildTable;
				string strKey = GetForeignKeyName ( table.ParentRelations[n] );
				if ( tableFille.Columns[strKey] != null )
					tableFille.Columns.Remove ( strKey );
				table.ParentRelations.RemoveAt ( n );
			}

			foreach ( DataTable tableAutre in Tables )
			{
				ArrayList lstContraintes = new ArrayList();
				//Supprime les contraintes de clé étrangère
				foreach ( Constraint contrainte in tableAutre.Constraints )
				{
					if ( contrainte is ForeignKeyConstraint && 
						(((ForeignKeyConstraint)contrainte).RelatedTable.TableName == table.TableName ||
						tableAutre.TableName == table.TableName))
						lstContraintes.Add ( contrainte );
				}
				for ( int nContrainte = lstContraintes.Count-1; nContrainte >= 0; nContrainte-- )
				{
					Constraint contrainte = (Constraint)lstContraintes[nContrainte];
					tableAutre.Constraints.Remove ( contrainte );
				}
			}
			table.PrimaryKey = null;

			Tables.Remove ( table );
		}

		
		////////////////////////////////////////////////////////////////////////////
		public override DataSet Clone()
		{
			m_bEnableAutoStructure = false;
			CContexteDonnee contexteCopie = (CContexteDonnee) base.Clone();
			foreach ( DataTable tbl in contexteCopie.Tables )
			{
				Type tp = GetTypeForTable ( tbl.TableName );
				if ( typeof(IObjetALectureTableComplete).IsAssignableFrom(tp) )
					contexteCopie.m_tablesCompletesInvalides[tbl.TableName] = true;
			}
			m_bEnableAutoStructure = true;
			contexteCopie.IdSession = m_nIdSession;
			contexteCopie.CanReceiveNotifications = m_bCanReceiveNotifications;
			return contexteCopie;
		}

		////////////////////////////////////////////////////////////////////////////
		///Se produit lorsqu'on ajoute ou supprime un e table de la liste des tables
		protected void OnChangeCollectionTables ( object sender, System.ComponentModel.CollectionChangeEventArgs args )
		{
			if ( args.Action == System.ComponentModel.CollectionChangeAction.Add )
				OnAddTable ( (DataTable)args.Element );
		}


		////////////////////////////////////////////////////////////////////////////
		///Retourne une table et la crée si elle n'existe pas
        private Dictionary<string, DataTable> m_dicTables = new Dictionary<string, DataTable>();
        private static TimeSpan spTotal = new TimeSpan(0);
        private static Dictionary<string, DataTable> m_cacheSchema = new Dictionary<string, DataTable>();
		public DataTable GetTableSafe ( string strNomTable )
		{
            DataTable table = null;
            if (!m_dicTables.TryGetValue(strNomTable, out table))
            {
                table = Tables[strNomTable];
                if (table != null)
                    m_dicTables[strNomTable] = table;
            }
			if ( table == null || m_bGestionParTablesCompletes && m_tablesCompletesInvalides[strNomTable] != null)
			{
				if ( m_bGestionParTablesCompletes && typeof(IObjetALectureTableComplete).IsAssignableFrom(GetTypeForTable(strNomTable) ) ) 
					table = IntegreTableComplete(strNomTable);
				else
				{
                    DataTable tableCache;
                    if (m_cacheSchema.TryGetValue(strNomTable, out tableCache))
                    {
                        table = IntegreTable(tableCache, false);
                    }
                    else
                    {
                        IObjetServeur loader = GetTableLoader(strNomTable);
                        if (loader != null)
                        {
                            try
                            {
                                using (DataTable tableTmp = loader.FillSchema())
                                {
                                    table = IntegreTable(tableTmp, false);
                                    m_cacheSchema[strNomTable] = tableTmp.Clone() as DataTable;
                                }
                            }
                            catch (Exception e)//Pour débug
                            {
                                throw e;
                            }
                        }
                    }
				}
				m_tablesCompletesInvalides.Remove(strNomTable);
			}
			
			return table;
		}

        ////////////////////////////////////////////////////////////////////////////
        public static void ClearCacheSchemas()
        {
            m_cacheSchema = new Dictionary<string, DataTable>();
        }

		////////////////////////////////////////////////////////////////////////////
		public DataTable IntegreTableComplete(String strNomTable)
		{
			DataTable table = null;
			if ( ContextePrincipal != null && ContextePrincipal.GestionParTablesCompletes)
			{
				table = ContextePrincipal.GetTableSafe(strNomTable);
                if (table == null)//force l'intégration
                //de la table dans le contexte principal
                {
                    table = ContextePrincipal.IntegreTableComplete(strNomTable);
                }
				if ( table !=  null )
				{
                    bool bOldEnforce = EnforceConstraints;
                    EnforceConstraints = false;
					//table = ContextePrincipal.IntegreTableComplete ( strNomTable );
					foreach ( DataRow row in new ArrayList(table.Rows) )
						CopieRowTo ( row, this, false, true, false );
                    EnforceConstraints = bOldEnforce;
					//Integre table ne marche pas car on risque de 
					//copier un parent qui croit que ses fils sont chargés,
					//mais sans avoir copié ses fils.
					//return IntegreTable ( table, false );

                    //Stef 28/05/2012 : la fonction ne retournait rien
                    //donc, continuait en faisant un GetTableLoader.Read, qui
                    //ne sert normallement à rien, puisque la table est déjà lue!
                    table = Tables[strNomTable];
                    if (table != null )
                    {
                        m_tablesCompletesInvalides.Remove(strNomTable);
                        return table;
                    }
				}
			}
			if ( m_tablesCompletesInvalides[strNomTable]!=null || Tables[strNomTable] == null)
			{
				IObjetServeur loader = GetTableLoader ( strNomTable );
				using (DataTable tableTmp = loader.Read(null) )
				    return IntegreTable ( tableTmp, true );
			}
			return Tables[strNomTable];
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique combien de contextes parents possède ce contexte
		/// </summary>
		private int ProfondeurContexte
		{
			get
			{
				if (ContextePrincipal != null)
					return ContextePrincipal.ProfondeurContexte + 1;
				return 0;
			}
		}


		////////////////////////////////////////////////////////////////////////////
		///Prépare une table à être utilisable avec les spécificités du CContexteDonnees
		private void OnAddTable ( DataTable table )
		{
			table.RowChanging += new DataRowChangeEventHandler ( OnChangingRow );
            table.RowDeleting += new DataRowChangeEventHandler(OnDeletingRow);

            

			if ( !m_bEnableAutoStructure )
				return;
            List<string> lstColsNotInDb = new List<string>();
			//Ajoute les champs de internes
			DataColumn col;
			//Colonne LocalKey
			if ( table.Columns[c_colLocalKey] == null )
			{
				col = new DataColumn( c_colLocalKey, typeof(int) );
				col.AutoIncrement = true;
				col.ReadOnly = true;
				table.Columns.Add ( col );
			}

            if (table.Columns[c_colIdContexteCreation] == null)
            {
                col = new DataColumn(c_colIdContexteCreation, typeof(int));
                table.Columns.Add(col);
            }

            if (table.Columns[CObjetDonnee.c_champContexteModification] == null)
            {
                col = new DataColumn(CObjetDonnee.c_champContexteModification, typeof(string));
                col.AllowDBNull = false;
                col.DefaultValue = "";
                table.Columns.Add(col);
            }

			if ( table.Columns[CObjetDonnee.c_champVerifierDonneesALaSauvegarde] == null )
			{
				col = new DataColumn ( CObjetDonnee.c_champVerifierDonneesALaSauvegarde, typeof(bool));
				col.DefaultValue = false;
				table.Columns.Add ( col );
			}


			//Colonne c_colIsHorsVersion
			if (table.Columns[c_colIsHorsVersion] == null)
			{
				col = new DataColumn(c_colIsHorsVersion, typeof(bool));
				col.DefaultValue = false;
				table.Columns.Add(col);
			}

			//Colonne IsToRead
			if (table.Columns[c_colIsToRead] == null)
			{
				col = new DataColumn(c_colIsToRead, typeof(bool));
				col.DefaultValue = true;
				table.Columns.Add(col);
			}


			//colonne IsFromDB
			if ( table.Columns[c_colIsFromDb] == null )
			{
				col = new DataColumn ( c_colIsFromDb, typeof(bool));
				col.DefaultValue = false;
				table.Columns.Add ( col );
			}

			//Si le type est un objectDonne à IdAuto, s'assure que 
			//La colonne du dataset est bien à increment auto
			//cas du pda, on gère nous même l'autoincrement
			Type typeDesObjets = GetTypeForTable ( table.TableName );
			if ( typeDesObjets != null && typeDesObjets.IsSubclassOf ( typeof(CObjetDonneeAIdNumeriqueAuto ) ) )
			{
				if ( table.PrimaryKey.Length == 1 && !table.PrimaryKey[0].AutoIncrement)
					table.PrimaryKey[0].AutoIncrement = true;
			}


			//Convertit toutes les colonnes autoincrément en négatif
			bool bHasBlobs = false;
			foreach ( DataColumn colonne in table.Columns )
			{
                
				if ( colonne.AutoIncrement == true )
				{
					//colonne.AutoIncrementSeed = m_nStartIncrement;
					colonne.AutoIncrementStep = -1;
					colonne.ReadOnly = false;
					DataRow row = table.NewRow();
					//force le prochain numéro à être négatif
                    while (((int)row[colonne.ColumnName]) >= 0)
						row = table.NewRow();
				}
                 
				colonne.AllowDBNull = true;
				if ( colonne.DataType == typeof(CDonneeBinaireInRow))
					bHasBlobs = true;
			}
			if ( bHasBlobs )
				table.ColumnChanging += new DataColumnChangeEventHandler(OnColumnBlobChanging);

			//On est obligé de stocker la liste des contraintes car OnDetectePrimaryKey
			//Peut en ajouter et dans ce cas, un foreach échoue car la collection est
			//modifiée
			Constraint[] contraintes = new Constraint[table.Constraints.Count];
			table.Constraints.CopyTo(contraintes, 0);
			foreach ( Constraint contrainte in contraintes )
			{
				if ( contrainte is UniqueConstraint )
				{
					UniqueConstraint key = (UniqueConstraint)contrainte;
					OnDetectePrimaryKey ( key );
				}
			}
			AddParentRelations ( table );

			//Ajoute les champs qui ne sont pas dans la base, mais servent en mémoire
			if ( typeDesObjets != null )
			{
				//Propriétés qui ne sont pas dans la base
				foreach ( PropertyInfo propInfo in typeDesObjets.GetProperties() )
				{
					object[] attribs = propInfo.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
					if ( attribs.Length != 0 )
					{
						TableFieldPropertyAttribute tableField = (TableFieldPropertyAttribute)attribs[0];
						bool bNotInTable = tableField.IsInDb;
						if ( !tableField.IsInDb && table.Columns[tableField.NomChamp] == null )
						{
							col = new DataColumn ( tableField.NomChamp, propInfo.PropertyType );
							col.AllowDBNull = true;
							table.Columns.Add ( col );
						}
                        if (!tableField.IsInDb)
                            lstColsNotInDb.Add(tableField.NomChamp);
					}
				}
				//Evenements
				object[] attribsEvt = typeDesObjets.GetCustomAttributes(typeof(EvenementAttribute), true);
				if (attribsEvt.Length > 0 && !table.Columns.Contains ( EvenementAttribute.c_champEvenements))
				{
					//Création du champ 2I_EVENT
					col = new DataColumn(EvenementAttribute.c_champEvenements, typeof(string));
					col.MaxLength = 1000;
					col.AllowDBNull = true;
					table.Columns.Add(col);
				}
			}

            //Met toutes les dates en undefined
            foreach (DataColumn colDate in table.Columns)
            {
                if (colDate.DataType == typeof(DateTime) || 
                    colDate.DataType == typeof(DateTime?))
                    colDate.DateTimeMode = DataSetDateTime.Unspecified;
            }

			if (typeof(IObjetDonneeAValeurParDefautOptim).IsAssignableFrom(typeDesObjets))
			{
				CObjetDonnee objet = GetNewObjetForTable(table);
				((IObjetDonneeAValeurParDefautOptim)objet).DefineValeursParDefaut(table);
			}
            table.ExtendedProperties[c_extPropTableColNotInDb] = lstColsNotInDb.ToArray();
		}

        public void OnDeletingRow(object sender, DataRowChangeEventArgs args)
        {
            DataRow row = args.Row;
            if (row.RowState != DataRowState.Deleted)
            {
                try
                {
                    if ((string)row[CObjetDonnee.c_champContexteModification] == "")
                    {
                        if ((string)row[CObjetDonnee.c_champContexteModification] != IdModificationContextuelle)
                        {
                            row[CObjetDonnee.c_champContexteModification] = IdModificationContextuelle;
                            row.AcceptChanges();
                        }
                    }
                        
                }
                catch
                {
                }
            }
            List<CInfoRelation> lst = null;
            if (!m_dicTablesAvecCascadeManuelle.TryGetValue(args.Row.Table.TableName, out lst))
            {
                //S'il y a des delete en cascade manuelles à faire, c'est le moment
                Type tp = GetTypeForTable(args.Row.Table.TableName);
                if (tp != null)
                {
                    CStructureTable structure = CStructureTable.GetStructure(tp);
                    List<CInfoRelation> relations = new List<CInfoRelation>();
                    foreach (CInfoRelation relation in structure.RelationsParentes)
                        relations.Add(relation);
                    foreach (CInfoRelation relation in structure.RelationsFilles)
                        relations.Add(relation);
                    foreach (CInfoRelation relation in relations)
                    {
                        if (relation.DeleteCascadeManuel && relation.TableParente == args.Row.Table.TableName)
                        {
                            if (lst == null)
                                lst = new List<CInfoRelation>();
                            if ( lst.FirstOrDefault ( r=>r.RelationKey == relation.RelationKey ) == null )
                                lst.Add(relation);
                        }
                    }
                }
                m_dicTablesAvecCascadeManuelle[args.Row.Table.TableName] = lst;
            }
            if (lst != null)
            {
                foreach (CInfoRelation relation in lst)
                    DoCascadeManuelle(args.Row, relation);
            }
        }


		/////////////////////////////////////////////////////////////////////////
		protected void OnChangingRow ( object sender, DataRowChangeEventArgs args )
		{
			if ( !m_bEnableAutoStructure )
				return;
				
			if ( args.Action == DataRowAction.Add && EnforceConstraints )
            {
				AssureParents ( args.Row );
            }
            
		}

        //////////////////////////////////////////////////
        private void DoCascadeManuelle(DataRow row, CInfoRelation relationFille)
        {
            DataTable tableFille = Tables[relationFille.TableFille];
            if (tableFille == null)
                return;
            string strNomRelation = GetForeignKeyName(relationFille.TableParente, relationFille.TableFille, relationFille.ChampsFille);
            if (row.HasVersion(DataRowVersion.Original))
            {
                DataRow[] rows = row.GetChildRows(strNomRelation, DataRowVersion.Original);
                foreach (DataRow rowToDelete in rows)
                {
                    if (rowToDelete.RowState != DataRowState.Deleted && rowToDelete.RowState != DataRowState.Detached)
                    {
                        foreach (string strColFille in relationFille.ChampsFille)
                            rowToDelete[strColFille] = DBNull.Value;
                        rowToDelete.Delete();
                    }
                }
            }
        }

		


		/// //////////////////////////////////////////////////////////////////////////////
		protected void OnColumnBlobChanging ( object sender, DataColumnChangeEventArgs args )
		{
			if ( args.Column.DataType == typeof(CDonneeBinaireInRow))
			{
				if ( args.ProposedValue is CDonneeBinaireInRow )
				{
					CDonneeBinaireInRow donnee = (CDonneeBinaireInRow)args.ProposedValue;
					if ( donnee.Row != args.Row )
					{
						args.ProposedValue = donnee.GetCloneForRow ( args.Row );
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le nom de la clé étrangère utilisée par la propriété indiquée
		/// </summary>
		/// <param name="typeFils"></param>
		/// <param name="strProprieteFils"></param>
		/// <returns></returns>
		public string GetForeignKeyName(Type typeFils, string strProprieteFils)
		{
			string strTableFille = GetNomTableForType ( typeFils );
			if ( strTableFille == null || strTableFille == "" )
				return null;
			PropertyInfo info = typeFils.GetProperty(strProprieteFils);
			if (info == null)
				return null;
			//Il doit y avoir un attribut Relation
			object[] attrs = info.GetCustomAttributes(typeof(RelationAttribute), true);
			if (attrs == null || attrs.Length == 0)
				return null;
			RelationAttribute att = (RelationAttribute)attrs[0];
			return GetForeignKeyName(att.TableMere, strTableFille, att.ChampsFils);
		}
		
		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le nom que doit avoir une foreign key entre deux tables
		/// </summary>
		/// <param name="tableSource"></param>
		/// <param name="tableDest"></param>
		/// <param name="strNomChamp"></param>
		/// <returns></returns>
		public string GetForeignKeyName ( string strTableParente, string strTableFille, string[] strNomsChampsFille )
		{
			CInfoRelation info = new CInfoRelation(strTableParente, strTableFille, new string[0], strNomsChampsFille, false, false );
			return info.RelationKey;
		}

		/////////////////////////////////////////////////////////////////////////
		public string GetForeignKeyName ( DataRelation relation )
		{
			ArrayList lst = new ArrayList();
			foreach ( DataColumn col in relation.ChildColumns )
				lst.Add ( col.ColumnName );
			return GetForeignKeyName ( relation.ParentTable.TableName, relation.ChildTable.TableName,
				(string[])lst.ToArray(typeof(string)));
		}

		/////////////////////////////////////////////////////////////////////////
		///Pour tout clé primaire d'une nouvelle table,
		///S'assure que les relations clé étrangères sur cette clé
		///existent
		private void OnDetectePrimaryKey ( UniqueConstraint key )
		{
			//recherche la clé dans toutes les tables
			DataTable tableKey = key.Table;
			foreach ( DataTable table in Tables )
			{
				AddRelationToForeignKeySiExiste( key, table );
			}
		}

		/////////////////////////////////////////////////////////////////////////
		///Crée automatiquement une relation entre une table mère (clé primaire)
		///et un table fille si elle existe ( si la fille possède
		///un champ ayant le même nom que la clé primaire
		private void AddRelationToForeignKeySiExiste ( UniqueConstraint key, DataTable tableFille )
		{
			if (  !m_bEnableAutoStructure)
				return;
			foreach ( CInfoRelation info in GetListeRelationsTable ( tableFille.TableName ))
			{
				if ( info.TableParente == key.Table.TableName &&
					info.TableFille == tableFille.TableName &&
					info.MatchParentsCols(key.Columns) )
				{
					bool  bProblemeChampFille = false;
					ArrayList lstColsFille = new ArrayList();
					ArrayList lstColsParente = new ArrayList();
					foreach ( string strChamp in info.ChampsFille )
					{
						if ( tableFille.Columns[strChamp] == null )
						{
							bProblemeChampFille = true;
							break;
						}
						else
							lstColsFille.Add ( tableFille.Columns[strChamp] );
					}
					foreach ( string strChamp in info.ChampsParent )
						lstColsParente.Add ( key.Table.Columns[strChamp] );

					if ( !bProblemeChampFille )
					{
						string strNomRelation = GetForeignKeyName ( key.Table.TableName, tableFille.TableName, info.ChampsFille );
						if ( Relations[strNomRelation] == null )
						{
							
							CreateParentElements ( key.Columns, (DataColumn[])lstColsFille.ToArray(typeof(DataColumn)) );
							
							//SC 16/10 : La contrainte est systèmatiquement créée
							DataRelation rel = Relations.Add ( strNomRelation,  (DataColumn[])lstColsParente.ToArray(typeof(DataColumn)),
								(DataColumn[])lstColsFille.ToArray(typeof(DataColumn)), /*info.Composition | info.Obligatoire*/ true );
							
							rel.ExtendedProperties[c_extPropRelationComposition] = info.Composition;

							ForeignKeyConstraint contrainte = (ForeignKeyConstraint)tableFille.Constraints[strNomRelation];
							
							if ( contrainte != null )
							{
								contrainte.UpdateRule = Rule.Cascade;
								if (!info.Composition || info.DeleteCascadeManuel)
									contrainte.DeleteRule = Rule.None;
								else
									contrainte.DeleteRule = Rule.Cascade;
							}
							
							
							//Ajoute dans la table parente une colonne indiquant si les
							//données de la relation fille sont chargées
							DataColumn col = key.Table.Columns[strNomRelation];
							if ( col == null )
							{
								col = new DataColumn( strNomRelation, typeof(bool));
								col.ExtendedProperties[c_cleIsColIndicateurChargementForeignKey] = true;
								col.DefaultValue = false;
								key.Table.Columns.Add ( col );
							}
						}
					}
				}
			}
		}

		
		/////////////////////////////////////////////////////////////////////////
		//Remplit une ligne à partir de la base de données
		protected void FillRowFromDB ( DataRow row )
		{
			if ( !IsToRead(row) )
				return;
			IObjetServeur loader = GetTableLoader(row.Table.TableName);
			CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow ( row.Table.PrimaryKey, row );
            using (DataTable table = loader.Read(filtre))
            {
                if (table.Rows.Count > 0)
                {
                    SetIsToRead(row, false);
                    CopyRow(table.Rows[0], row);
                }
            }
		}

		/////////////////////////////////////////////////////////////////////////
		///Crée automatiquement les enregistrements de la table mère
		///pour tout enregistrement de la table fille
		///Les enregistrements de la table mère sont créés
		///mais ne sont pas lus
		private void CreateParentElements ( DataColumn[] colonnesParents, DataColumn[] colonnesFilles )
		{
			foreach ( DataRow row in colonnesFilles[0].Table.Rows )
			{
				CreateParentElementSiBesoin ( row, colonnesFilles, colonnesParents );
			}
		}

		/////////////////////////////////////////////////////////////////////////
		//Ajoute une ligne à une table avec sureté d'intégrité référentielle
		public void AddRow ( DataRow row )
		{
			if ( m_bEnableAutoStructure )
				AssureParents(row);
			row.Table.Rows.Add (row);
		}

		/////////////////////////////////////////////////////////////////////////
		///si la colonneParent ne contient pas 'valeurFille', celle ci
		///est créée et prête à lire.
		///Cette fonction est utilisée pour créer automatiquement les 
		///enregistrements parents d'un enregistrement enfant.
		private void CreateParentElementSiBesoin ( DataRow rowFille, DataColumn[] colonnesFilles, DataColumn[] colonnesParents )
		{
			if ( rowFille.RowState == DataRowState.Deleted )
				return;
			if ( !m_bEnableAutoStructure )
				return;
			ArrayList lstValeurs = new ArrayList();
			bool bAllNuls = false;
			foreach ( DataColumn col in colonnesFilles )
			{
				object val = rowFille[col];
				bAllNuls |= (val==null || val == DBNull.Value) ;
			}
			if ( bAllNuls )
				return;
            string strFiltre = "";
            if (colonnesParents.Length > 1 || colonnesParents[0].DataType != typeof(int))
            {
                CFiltreData filtre = new CFiltreData();
                int nIndex = 1;
                foreach (DataColumn col in colonnesParents)
                {
                    strFiltre += col.ColumnName + "=@" + nIndex + " and ";
                    filtre.Parametres.Add(rowFille[colonnesFilles[nIndex - 1]]);
                    nIndex++;

                }
                strFiltre = strFiltre.Substring(0, strFiltre.Length - 5);//Suppression du dernier AND
                filtre.Filtre = strFiltre;

                strFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(filtre);
            }
            else//Optim pour les ids entiers
            {
                strFiltre = colonnesParents[0].ColumnName + "=" + (int)rowFille[colonnesFilles[0]];
            }
			DataRow[] rows = colonnesParents[0].Table.Select(strFiltre);
			if ( rows.Length == 0 )
			{
				DataRow newRow = colonnesParents[0].Table.NewRow();
				int nIndex = 0;
				foreach ( DataColumn col in colonnesParents )
				{
					newRow[col] = rowFille[colonnesFilles[nIndex]];
					nIndex++;
				}
				newRow[c_colIsToRead] = true;
				//Stef : le 0610 : Enleve le FillRowFromDb, la colonne
				//Peut très bien être à lire !!!
				//FillRowFromDB ( newRow );
				AddRow ( newRow );
				//colonnesParents[0].Table.Rows.Add ( newRow );
				newRow.AcceptChanges();
				newRow = null;
			}
		}

		/////////////////////////////////////////////////////////////////////////
		///S'assure que les enregistrements parents d'une ligne existent.
		public void AssureParents ( DataRow row )
		{
			foreach ( DataRelation relation in row.Table.ParentRelations )
			{
				bool bParentExiste = false;
				try
				{
					bParentExiste = row.GetParentRow ( relation ) != null;
				}
				catch
				{
				}
				if ( !bParentExiste )
					CreateParentElementSiBesoin( row, relation.ChildColumns, relation.ParentColumns );
			}
		}

		/////////////////////////////////////////////////////////////////////////
		///Crée les relations parentes d'une table fille.
		private void AddParentRelations ( DataTable tableFille )
		{
			foreach ( DataTable tableParente in Tables )
			{
				foreach ( Constraint cst in tableParente.Constraints )
				{
					if ( cst is UniqueConstraint )
						AddRelationToForeignKeySiExiste ( (UniqueConstraint)cst, tableFille )  ;
				}
			}
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Place le flag de lecture d'une ligne et de toutes ces compositions à faux
		/// </summary>
		/// <param name="row"></param>
		public void InvalideRowEtCompositions ( DataRow row )
		{
			SetIsToRead ( row, true );
			foreach ( DataRelation childRelation in row.Table.ChildRelations )
			{
				object extProp = childRelation.ExtendedProperties[c_extPropRelationComposition];
				bool bComposition = false;
				//Erreur .Net Framework, lors de la copie du paramètre, il est 
				//copié sous forme de chaine !!!
				if ( extProp is bool )
					bComposition = (bool)extProp;
				else
					bComposition = extProp.ToString()==true.ToString();
				if ( bComposition )
					foreach ( DataRow child in row.GetChildRows(childRelation) )
						InvalideRowEtCompositions ( child );
			}
		}
		#endregion

		//////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////
		///Sauvegarde des données, communications avec les loaders
		//////////////////////////////////////////////////////////////////////////////
		///
		#region Sauvegarde des données, communication avec les loaders

		////////////////////////////////////////////////////////////////////////////
		public bool EnableTraitementsAvantSauvegarde
		{
			get
			{
				return m_bEnableTraitementAvantSauvegarde;
			}
			set
			{
				m_bEnableTraitementAvantSauvegarde = value;
			}
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Trie les tables dans l'ordre pour l'update et l'insert
		/// </summary>
		/// <returns></returns>
		public ArrayList GetTablesOrderInsert()
		{
			return GetTablesOrderInsert( this );
		}


		////////////////////////////////////////////////////////////////////////////
		private class CRelationTypeIdComparer : IComparer
		{
			#region Membres de IComparer

			public int Compare(object x, object y)
			{
				if ( ! ( x is RelationTypeIdAttribute ) || 
					!( y is RelationTypeIdAttribute ) )
					return 0;
				//Compare y avec x pour ordre décroissant
				return ((RelationTypeIdAttribute)y).Priorite.CompareTo ( ((RelationTypeIdAttribute)x).Priorite );
			}

			#endregion

		}


		////////////////////////////////////////////////////////////////////////////
		public static ArrayList GetTablesOrderInsert ( DataSet ds )
		{
			ArrayList liste = new ArrayList();
			Hashtable tableTraitees = new Hashtable();
			
			//S'assure que les relationTypeId sont à la fin et dans l'ordre décroissant de priorité
			/* pb des relations Type id
			 * il se peut qu'une table relation type id puisse créer des relations
			 * sur une autre table relation type id
			 * PAr exemple, chez Cafel, on a des Controles et des dossiers.
			 * Il devrait être possible de faire des contrôles sur des dossiers et des
			 * dossiers sur des contrôles. Le pb est que si on a les deux possiblités,
			 * il est impossible de déterminer un ordre de suppression pour les tables.
			 * Il faut donc empecher l'une des possibilités, c'est geré par les propriétés
			 * de RelationTypeId. Une relation typeId de priorité forte accepte des
			 * relation typeId de priorité plus faible. Par exemple, on affecte
			 * a dossier la priorité 1200 et à contrôle la priorité 1000,
			 * ce qui fait qu'on ne pourra pas faire de dossiers sur un contrôle,
			 * par contre le contraire sera possible.
			 * Dans l'ordre d'insertion des données, il faut créer le dossier avant le contrôle.
			 * Le relations type Id sont toujours inserées en dernier (dans l'ordre de priorité).
			 * Il faut cependant que les dépendances filles sur les relationsTypeId soient inserées
			 * après, c'est pourquoi il faut une gestion particulières des relation type id dans 
			 * l'ordre des tables. On va retrouver en fin, toutes les relations typeid suivies de
			 * leurs relations filles (collées au plus près de la relation typeid de laquelle ils dépendent.
			 * */

			/*Première phase : insérer les Relation type id dans l'ordre décroissant des priorités*/

			//Pour connaitre les relations typeId par la suite(nomtable->True)
			Hashtable tablesRelationTypeID = new Hashtable();
			
			//Liste des attributs relationTypeIdConnus
			ArrayList lstTypeId = new ArrayList(RelationsTypeIds);
			lstTypeId.Sort ( new CRelationTypeIdComparer() );
			ArrayList listePourFin = new ArrayList();
			foreach ( RelationTypeIdAttribute attr in lstTypeId )
			{
				DataTable laTable = ds.Tables[attr.TableFille];
				if ( laTable != null && tableTraitees[attr.TableFille]==null)
				{
					InsereTableEtFillesOrderInsert ( laTable, listePourFin.Count, listePourFin, tableTraitees );
					
				}
			}

            //Insere a la fin les tables à mettre à la fin
            foreach (string strNomTable in m_listeTablesAInsererApresRelationTypeId)
            {
                DataTable table = ds.Tables[strNomTable];
                if (table != null)
                {
                    InsereTableEtFillesOrderInsert(table, listePourFin.Count, listePourFin, tableTraitees);
                }
            }
			
			//Toutes les autres tables doivent être inserées avant les tables RelationTypeId
			//Sauf les filles de celles-ci
			foreach ( DataTable table in ds.Tables )
			{
                Type tp = GetTypeForTable(table.TableName);
                if (tp != null && tp.GetCustomAttributes(typeof(InsertEnFinAttribute), true).Length > 0)
                    listePourFin.Add(table);
                else
				    InsertTableIntoListeOrderInsert ( table, liste, tableTraitees );
			}
			liste.AddRange ( listePourFin );
			return liste;
		}

		////////////////////////////////////////////////////////////////////////////
		private static void InsereTableEtFillesOrderInsert ( DataTable table,
			int nPosition,
			ArrayList listeTables,
			Hashtable tablesTraitees )
		{
			if ( tablesTraitees[table.TableName] != null )
				return;
			tablesTraitees[table.TableName] = true;
			int nMyPosInsert = nPosition;
			ArrayList lstFillesToInsert = new ArrayList();
			foreach ( DataRelation relation in table.ChildRelations )
			{
				DataTable tableFille = relation.ChildTable;
				if ( tablesTraitees[tableFille] != null )//Si une table fille est déjà rentré dedans
					//il faut glisser la table parent avant !!!
					nMyPosInsert = Math.Min ( nMyPosInsert, listeTables.IndexOf ( tableFille.TableName ));
				else 
					lstFillesToInsert.Add ( tableFille );
			}
			//Insere la table là ou on l'a demandé
			if ( nMyPosInsert >= listeTables.Count )
				listeTables.Add ( table );
			else
				listeTables.Insert ( nMyPosInsert, table );
			foreach ( DataTable tableFille in lstFillesToInsert )
			{
				nMyPosInsert++;
				InsereTableEtFillesOrderInsert (tableFille, nMyPosInsert, listeTables, tablesTraitees );
			}
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Trie les tables dans l'ordre pour le delete
		/// </summary>
		/// <returns></returns>
		public ArrayList GetTablesOrderDelete()
		{
			return GetTableOrderDelete(this);
		}

		////////////////////////////////////////////////////////////////////////////
		public static ArrayList GetTableOrderDelete ( DataSet ds )
		{
			ArrayList lst = GetTablesOrderInsert ( ds );
			lst.Reverse();
			return lst;
		}

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Ajoute les tables dans une liste par ordre pour insert et update
		/// </summary>
		/// <param name="table"></param>
		/// <param name="liste"></param>
		/// <param name="tableTraitees"></param>
		private static void InsertTableIntoListeOrderInsert ( 
			DataTable table, 
			ArrayList liste, 
			Hashtable tableTraitees )
		{
			if ( tableTraitees.ContainsKey(table.TableName) )
				return;
			tableTraitees[table.TableName] = 1;
			foreach ( DataRelation relation in table.ParentRelations )
				InsertTableIntoListeOrderInsert ( relation.ParentTable, liste, tableTraitees );

            //Stef le 17/05/2011, insert en premier les tables qui n'ont aucun parent
            //nécéssaire pour que les champs custom par exemple soit enregistrés en premier
            if (table.ParentRelations.Count == 0 && liste.Count > 0)
                liste.Insert(0, table);
            else
			    liste.Add ( table );
		}

		////////////////////////////////////////////////////////////////////////////
		public static string[] GetFastListNomTablesOrderInsert()
		{
			Hashtable tableTraitees = new Hashtable();
			ArrayList liste = new ArrayList();
			foreach ( string strNomTable in m_mappeurTablesToClass.GetListeTables() )
				FastInsertNomTableIntoListeOrderInsert ( strNomTable, liste, tableTraitees );
			return (string[])liste.ToArray ( typeof(string));
		}

		////////////////////////////////////////////////////////////////////////////
		public static string[] GetFastListNomTablesOrderDelete()
		{
			ArrayList lst = new ArrayList(GetFastListNomTablesOrderInsert());
			lst.Reverse();
			return (string[])lst.ToArray ( typeof(string) );
		}

		////////////////////////////////////////////////////////////////////////////
		private static void FastInsertNomTableIntoListeOrderInsert ( 
			string strNomTable, 
			ArrayList liste, 
			Hashtable tableTraitees )
		{
			if ( tableTraitees.ContainsKey(strNomTable) )
				return;
			tableTraitees[strNomTable] = 1;
			foreach ( CInfoRelation relation in m_listeRelations )
				if ( relation.TableFille == strNomTable )
					FastInsertNomTableIntoListeOrderInsert ( relation.TableParente, liste, tableTraitees );
			liste.Add ( strNomTable );
		}

		

		/////////////////////////////////////////////////////////////////////////
		public void MergeOnLocalKey ( DataTable tableSource, bool bAddNewRows )
		{
			try
			{
				PrivateMergeOnLocalKey ( tableSource, bAddNewRows );
			}
			catch
			{
				bool bOldEnforce = EnforceConstraints;
				try
				{
					if ( bOldEnforce )
					{
						EnforceConstraints = false;
						PrivateMergeOnLocalKey ( tableSource, bAddNewRows );
					}
				}
				catch ( Exception e )
				{
					CResultAErreur result = CResultAErreur.True;
					result.EmpileErreur(new CErreurException(e));
					result.EmpileErreur(I.T("Error while merging table @1|121",tableSource.TableName));
					throw new CExceptionErreur(result.Erreur);
				}
				finally
				{
					try
					{
						if ( !EnforceConstraints )
						{
							ArrayList lst = new ArrayList ( Tables[tableSource.TableName].Rows );
							foreach ( DataRow row in lst )
								AssureParents ( row );
						}
						if ( EnforceConstraints != bOldEnforce )
							EnforceConstraints = bOldEnforce;
					}
					catch ( Exception e )
					{
						CResultAErreur result = CResultAErreur.True;
						result.EmpileErreur(new CErreurException(e));
						result.EmpileErreur(I.T("Error while merging table @1|121",tableSource.TableName));
						throw new CExceptionErreur(result.Erreur);
					}
				}
			}
		}
		/////////////////////////////////////////////////////////////////////////
		///Fusionne deux tables en se basant sur la clé locale (champ
		///à utilisation interne.
		private void PrivateMergeOnLocalKey ( DataTable tableSource, bool bAddNewRows )
		{
			DateTime dt = DateTime.Now;
			try
			{
				if ( tableSource.Rows.Count == 0 )
					return;
				DataTable tableDest = Tables[tableSource.TableName];
				if ( tableDest == null )
					return;
				
				Hashtable tblLocalKey = new Hashtable ( tableDest.Rows.Count, 0.4f );
				foreach ( DataRow row in tableDest.Rows )
				{
					if ( row.RowState == DataRowState.Deleted )
						tblLocalKey[row[c_colLocalKey,DataRowVersion.Original]] = row;
					else
						tblLocalKey[row[c_colLocalKey]] = row;
				}
				CFiltreData filtreLocalKey = new CFiltreData();
				filtreLocalKey.Filtre = c_colLocalKey+"=@1 ";
				filtreLocalKey.Parametres.Add(null);
				DataColumn[] keys = tableSource.PrimaryKey;

				tableDest.BeginLoadData();
                //Commence par changer les clés pour éviter les pbs d'autoréférence
                //On travaille dans la table dest, car la table source a eu un acceptChanges,
                //dont les lignes n'ont plus un état de modificatino correct
                foreach (DataRow rowDest in tableDest.Rows)
                {
                    if (rowDest.RowState == DataRowState.Added)
                    {
                        object[] valsKey = new object[keys.Length];
                        for (int n = 0; n < keys.Length; n++)
                            valsKey[n] = rowDest[keys[n].ColumnName];

                        //Tente d'abord le lien sur la clé primaire
                        DataRow rowSource = tableSource.Rows.Find(valsKey);
                        if (rowSource == null)
                        {
                            rowSource = tblLocalKey[rowDest[c_colLocalKey]] as DataRow;
                            if (rowSource != null)
                            {
                                bool bOldEnforce = tableDest.DataSet.EnforceConstraints;
                                tableDest.DataSet.EnforceConstraints = false;
                                foreach (DataColumn col in tableDest.PrimaryKey)
                                    rowDest[col.ColumnName] = rowSource[col.ColumnName];
                                tableDest.DataSet.EnforceConstraints = bOldEnforce;
                            }
                        }
                    }
                }

				foreach ( DataRow row in tableSource.Rows )
				{
					DataRowVersion version = DataRowVersion.Default;
					if ( row.RowState == DataRowState.Deleted )
						version = DataRowVersion.Original;
					filtreLocalKey.Parametres[0] = row[c_colLocalKey,version];
					object[] valsKey = new object[keys.Length];
					for ( int n = 0; n < keys.Length; n++ )
						valsKey[n] = row[keys[n], version];

					//Tente d'abord le lien sur la clé primaire
					DataRow rowDest = tableDest.Rows.Find(valsKey);
                    DataRow rowDestFromPrimaryKey = rowDest;
                    //Si la rowDest est trouvée sur la clé primaire et qu'elle est 
                    //en is to read, il s'agit donc d'un élément autoreferencé
                    //qui a été créé lors de l'ajout d'un élément fils, 
                    //Il faut donc le supprimer et travailler sur celui avec localKey
					if ( rowDest == null || IsToRead ( rowDest ))
					{
						//si pas sur clé primaire, tente sur localkey
						/*DataRow[] rows = tableDest.Select ( new CFormatteurFiltreDataToStringDataTable().GetString(filtreLocalKey));
						if ( rows.Length != 0 )
							rowDest = rows[0];*/
                        if (row.RowState == DataRowState.Deleted)
                            rowDest = null;
                        else
                        {
                            rowDest = (DataRow)tblLocalKey[row[c_colLocalKey]];
                            //Stef 17/09/2012 : la ligne existait avec ce numéro de localKey,
                            //mais elle a été supprimé par une ligne précédente
                            if (rowDest != null && rowDest.RowState == DataRowState.Detached)
                                rowDest = null;
                        }

						DataRowVersion versionDest = DataRowVersion.Default;
						if (rowDest != null && rowDest.RowState == DataRowState.Deleted)
							versionDest = DataRowVersion.Original;
							
						//OK on en a trouvé une avec la même LocalKey. Mais que se passe-t-il si la local Key est 
						//la même mais que la clé primaire n'est pas la même alors que l'objet vient de la
						//base : ça veut dire que ce n'est pas le même élément !!!!
						if ( rowDest != null && rowDest.RowState != DataRowState.Added && rowDest[c_colIsFromDb, versionDest].Equals(true)) 
						{
							foreach ( DataColumn col in tableDest.PrimaryKey )
								if ( !rowDest[col, versionDest].Equals(row[col.ColumnName]) )
								{
									//Ce n'est pas la même ligne !!!
									rowDest = null;
									break;
								}
						}
					}
					if ( rowDest != null )
					{
						if (row.RowState == DataRowState.Deleted && rowDest.RowState != DataRowState.Deleted)
							rowDest.Delete();
						else if (row.RowState != DataRowState.Deleted && rowDest.RowState == DataRowState.Deleted)
						{
                            if (row.Table.Columns[CSc2iDataConst.c_champIsDeleted] != null &&
                                row[CSc2iDataConst.c_champIsDeleted].Equals(false))
                            {
                                rowDest.RejectChanges();
                                if (!IsToRead(row))
                                    CopyRow(row, rowDest);
                            }
						}
						else
                            if (!IsToRead(row))//Si la ligne originale est à lire, pas de maj!
                            {
                                bool bOldEnforce = EnforceConstraints;
                                if (rowDestFromPrimaryKey != null &&  rowDestFromPrimaryKey != rowDest)
                                    EnforceConstraints = false;
                                CopyRow(row, rowDest);
                                if (rowDestFromPrimaryKey != null &&  rowDestFromPrimaryKey != rowDest)
                                {
                                    //la rowDest from primary key avait été créée par un fils
                                    //il faut donc la supprimer
                                    //ON a désactivé les contraintes car rowDest et RowDestFromPrimaryKey ont les mêmes
                                    //clés primaires
                                    tableDest.Rows.Remove(rowDestFromPrimaryKey);
                                    tblLocalKey[rowDestFromPrimaryKey[c_colLocalKey]] = null;
                                    EnforceConstraints = bOldEnforce;
                                }
                            }
					}
                    else if (bAddNewRows && row.RowState != DataRowState.Deleted)
                    {
                        if (rowDestFromPrimaryKey != null)
                        {
                            //STEF 17/09/2012 : si on avait déjà une ligne avec cette clé, supprime la ligne
                            tblLocalKey[rowDestFromPrimaryKey[c_colLocalKey]] = null;
                            tableDest.Rows.Remove(rowDestFromPrimaryKey);
                        }
                        tableDest.ImportRow(row);
                    }
				}
				tableDest.EndLoadData();
			}
			catch ( Exception e )
			{
				CResultAErreur result = CResultAErreur.True;
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error while merging table @1|121",tableSource.TableName));
				throw new CExceptionErreur(result.Erreur);
			}
			if ( tableSource.Rows.Count > 1000 )
			{
				TimeSpan sp = DateTime.Now - dt;
			}
		}

		/////////////////////////////////////////////////////////////////////////
		public virtual string GetContexteDonneeServeurURI
		{
			get
			{
				return "CContexteDonneeServeur";
			}
		}

		/*[NonSerialized]
		//Fonction appellée durant la sauvegarde
		private TimerCallback m_foncCallBackSave = null;
		
		//Objet passé à la fonction callback
		[NonSerialized]
		private object m_paramCallBackSave = null;

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Fonction callback appellée durant la sauvegarde
		/// </summary>
		public TimerCallback CallbackSave
		{
			get
			{
				return m_foncCallBackSave;
			}
			set
			{
				m_foncCallBackSave = value;
			}
		}

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Parametre passé au callback de sauvegarde
		/// </summary>
		public object ParamCallBackSave
		{
			get
			{
				return m_paramCallBackSave;
			}
			set
			{
				m_paramCallBackSave = value;
			}
		}*/

		/////////////////////////////////////////////////////////////////////////
		///Sauvegarde tout le dataset
		public CResultAErreur SaveAll( bool bAcceptChangeIfOk)
		{
			//Stef le 12/11/03 : si on est en mode déconnecté, il faut quand même faire monter les
			//notification, le test sur le mode déconnecté doit donc être différent,
			//il faut faire tout le process de sauvegarde sans toucher à la base
			/*if ( m_bModeDeconnecte )
				return CResultAErreur.True;*/
			CResultAErreur result = CResultAErreur.True;

			//Sauvegarde les modifs
			if ( HasChanges() )
			{
				#region Ouverture de la transaction
				CSessionClient session = CSessionClient.GetSessionForIdSession ( IdSession );
				if ( session == null )
				{
					result.EmpileErreur(I.T("Invalid Session|122"));
					return result;
				}
				result = session.BeginTrans();
				if ( !result )
				{
					result.EmpileErreur(I.T("Cannot open transaction|123"));
					return result;
				}
				#endregion
				CContexteDonnee contexteApresSauvegarde = null;
				using (CContexteDonnee contexteModifs = GetCompleteChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted))
				{
					try
					{

						contexteModifs.m_nIdVersionDeTravail = m_nIdVersionDeTravail;
                        contexteModifs.m_tableIdsVersionValides = (Hashtable)m_tableIdsVersionValides.Clone();
                        contexteModifs.IdModificationContextuelle = IdModificationContextuelle;
						contexteModifs.IgnoreAvertissementsALaSauvegarde = true;
                        contexteModifs.EnableTraitementsExternes = EnableTraitementsExternes;
                        contexteModifs.DisableHistorisation = DisableHistorisation;
                        contexteModifs.m_tablesPourLesquellesDesactiverIdAuto = m_tablesPourLesquellesDesactiverIdAuto;
						if ( !m_bModeDeconnecte )
						{
							IContexteDonneeServeur contexteServeur = (IContexteDonneeServeur)C2iFactory.GetNewObjetForSession(GetContexteDonneeServeurURI, typeof(IContexteDonneeServeur), IdSession );
							using (C2iSponsor sponsor = new C2iSponsor())
							{
                                CResultAErreur defaultResult = CResultAErreur.True;
                                defaultResult.EmpileErreur(I.T("Asynchronous call error @1|20001", "SaveAll"));
								sponsor.Register(contexteServeur);
								CAppelleurFonctionAsynchrone appelleur = new CAppelleurFonctionAsynchrone();
								result = (CResultAErreur)appelleur.StartFonctionAndWaitAvecCallback(
									typeof(IContexteDonneeServeur),
									contexteServeur,
									"SaveAll",
                                    "",
                                    defaultResult,
									contexteModifs,
									m_bEnableTraitementAvantSauvegarde);
								if (!result)
									result.Data = null;

								contexteApresSauvegarde = (CContexteDonnee)result.Data;

								if (result && !m_bModeDeconnecte)
								{
									result = SaveBlobs(contexteApresSauvegarde, contexteServeur.IdVersionArchiveAssociee);
								}
							}
						}
					}
					catch(Exception e)
					{
						result.EmpileErreur(new CErreurException(e));
						result.EmpileErreur(I.T("Server Exception|124"));
					}
					/*}
						catch ( Exception e )
						{
							result.EmpileErreur(new CErreurException(e));
						}*/

                    //SC 27/03/2013 : bloque l'arrivée des notifications,
                    //Pour éviter un multithread sur ce contexte pendant la recopie de données
                    using ( CBloqueurNotifications bloqueur = new CBloqueurNotifications(this) )
                    {
                        {
                            if (result)
                                result = session.CommitTrans();
                            else
                                session.RollbackTrans();


                            if (m_bModeDeconnecte)
                                return result;
                            if (result)
                            {
                                DateTime dt = DateTime.Now;

                                //Intègre les modifs faites dans le contexte après sauvegarde
                                //Pas la peine sur PDA car c'est le même dataset 
                                ArrayList listeToUpdate = contexteApresSauvegarde.GetTablesOrderInsert();
                                foreach (DataTable table in listeToUpdate)
                                {
                                    CResultAErreur defaultResult = CResultAErreur.True;
                                    defaultResult.EmpileErreur(I.T("Asynchronous call error @1|20001", "MergeOnLocalKey"));
                                    if (table.Rows.Count > 1000)//Traitement asychrone pour ne pas tout bloquer
                                        new CAppelleurFonctionAsynchrone().StartFonctionAndWaitAvecCallback(
                                        typeof(CContexteDonnee),
                                        this,
                                        "MergeOnLocalKey",
                                        "",
                                        defaultResult,
                                        table,
                                        true);
                                    else
                                        MergeOnLocalKey(table, true);
                                }
                                //Toutes les dataRow added ou modified qui sont dans le contexte
                                //envoyé au serveur et qui ne sont pas
                                //dans le contexte apres sauvegarde on forcement été supprimées
                                //du contexte après sauvegarde ( probablement par un traitement avant
                                //sauvegarde. 
                                //Stef 09/04/2008 de même les lignes qui étaient supprimées
                                //et qui ne le sont plus ne doivent plus être supprimées !
                                ArrayList lstTableDelete = contexteModifs.GetTablesOrderDelete();
                                CFiltreData filtreLocalKey = new CFiltreData();
                                filtreLocalKey.Filtre = c_colLocalKey + "=@1 ";
                                filtreLocalKey.Parametres.Add(null);
                                foreach (DataTable tableEnvoyee in lstTableDelete)
                                {
                                    DataTable tableFinale = Tables[tableEnvoyee.TableName];
                                    DataTable tableModif = contexteApresSauvegarde.Tables[tableEnvoyee.TableName];
                                    if (tableModif != null)
                                    {
                                        ArrayList lstCopie = new ArrayList(tableEnvoyee.Rows);
                                        foreach (DataRow row in lstCopie)
                                        {
                                            if ((row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified) &&
                                                tableModif.Select(c_colLocalKey + "=" + row[c_colLocalKey].ToString()).Length == 0)
                                            {
                                                DataRow[] rows = tableFinale.Select(c_colLocalKey + "=" + row[c_colLocalKey].ToString());
                                                if (rows.Length > 0)
                                                    rows[0].Delete();
                                            }
                                            /*if (row.RowState == DataRowState.Deleted &&
                                                tableModif.Select(c_colLocalKey + "=" + row[c_colLocalKey, DataRowVersion.Original].ToString()).Length != 0)
                                            {
                                                DataRow[] rows = tableFinale.Select(c_colLocalKey + "=" + row[c_colLocalKey, DataRowVersion.Original].ToString(),"", DataViewRowState.Deleted);
                                                DataRow[] rowsModif = tableModif.Select(c_colLocalKey + "=" + row[c_colLocalKey, DataRowVersion.Original].ToString());
                                                if (rows.Length > 0)
                                                {
                                                    rows[0].RejectChanges();
                                                    rows[0].ItemArray = rowsModif[0].ItemArray;
                                                }
                                            }*/
                                        }
                                    }
                                }

                                if (bAcceptChangeIfOk)
                                {
                                    bool bOldEnforce = EnforceConstraints;
                                    EnforceConstraints = false;
                                    foreach (DataTable table in Tables)
                                        table.BeginLoadData();
                                    AcceptChanges();
                                    foreach (DataTable table in Tables)
                                        table.EndLoadData();
                                    EnforceConstraints = bOldEnforce;
                                }


                            }

                            if (contexteApresSauvegarde != null)
                                contexteApresSauvegarde.Dispose();
                        }
                    }
				}
			}
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Sauvegarde les blos
		/// </summary>
		/// <param name="contexteQuiVientDetreSauve">
		/// Le contexte qui vient d'être sauvé contient les nouvelles clés et autres modifs 
		/// faites dans la base par la sauvegarde
		/// </param>
		/// 
		/// <returns></returns>
		//Nom de table->bool qui indique si la table contient des blobs
		private static Hashtable m_tableTableToHasBlob = new Hashtable();
		private CResultAErreur SaveBlobs(CContexteDonnee contexteQuiVientDetreSauve, int? nIdVersionArchive)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				foreach ( DataTable table in Tables )
				{
					bool bHasBlobs = false;
					object val = m_tableTableToHasBlob[table.TableName];
					if (val is bool)
						bHasBlobs = (bool)val;
					else
					{
						IObjetServeur serveur = (IObjetServeur)GetTableLoader(table.TableName);
						bHasBlobs = serveur.HasBlobs();
						m_tableTableToHasBlob[table.TableName] = bHasBlobs;
					}
					if ( bHasBlobs )
					{
                        Type tp = GetTypeForTable ( table.TableName );
                        string strChampIdAuto = null;
                        if ( typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(tp) && table.PrimaryKey.Length == 1 )
                            strChampIdAuto = table.PrimaryKey[0].ColumnName;

						foreach ( DataColumn col in table.Columns )
						{
							if ( col.DataType == typeof(CDonneeBinaireInRow))
							{
								foreach ( DataRow row in table.Rows )
								{
									if ( row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached )
									{
										if ( row[col] != DBNull.Value )
										{
											CDonneeBinaireInRow donnee = (CDonneeBinaireInRow)row[col];
											if ( donnee != null && donnee.HasChange() )
											{
												//Force la colonne a être notés comme modifiée
												if ( row.RowState == DataRowState.Unchanged )
													row[col] = row[col];

                                                DataRow[] rows = null;

                                                if (strChampIdAuto != null && (int)row[strChampIdAuto] >= 0 && row.RowState != DataRowState.Added)
                                                {
                                                    DataRow rowSauvee = contexteQuiVientDetreSauve.Tables[table.TableName].Rows.Find(row[strChampIdAuto]);
                                                    if (rowSauvee != null)
                                                        rows = new DataRow[] { rowSauvee };
                                                    else
                                                        rows = null;
                                                }
                                                else
                                                {
                                                    //Trouve la ligne dans le contexte qui vient d'être sauvé
                                                    CFiltreData filtre = new CFiltreData(c_colLocalKey + "=@1", row[c_colLocalKey]);
                                                    rows = contexteQuiVientDetreSauve.Tables[table.TableName].Select(new CFormatteurFiltreDataToStringDataTable().GetString(filtre));
                                                }
												if ( rows != null && rows.Length > 0 )
												{
                                                    CDonneeBinaireInRow donneeFromServeur = rows[0][col.ColumnName] as CDonneeBinaireInRow;
                                                    if (donneeFromServeur != null && donneeFromServeur.DateLastModification != null &&
                                                        donnee.DateLastModification.Value < donneeFromServeur.DateLastModification.Value)
                                                    {
                                                        donneeFromServeur = donneeFromServeur.GetCloneForRow(rows[0]);
                                                        result = donneeFromServeur.SaveData(nIdVersionArchive);
                                                        if (!result)
                                                            return result;
                                                        donnee = donneeFromServeur.GetCloneForRow(row);
                                                        donnee.ForceHasChangeToFalse();
                                                    }
                                                    else
                                                    {
                                                        CDonneeBinaireInRow newDonnee = donnee.GetCloneForRow(rows[0]);
                                                        result = newDonnee.SaveData(nIdVersionArchive);
                                                        if (!result)
                                                            return result;
                                                        donnee.ForceHasChangeToFalse();
                                                    }
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
				result.EmpileErreur(I.T("Error while saving blobs|126"));
			}
			return result;
		}

		/*
				IServiceTransactions serviceTransactions = (IServiceTransactions)CSessionClient.GetInstance().GetService(CSc2iDataConst.c_ServiceTransaction);
				if ( serviceTransactions != null )
					serviceTransactions.BeginTrans();

				ArrayList lstTablesToMergeIfTransOK = new ArrayList();

				//Sauvegarde les modifs ( ajouts et update )
				if ( result )
				{
					result = MySaveAll( serviceTransactions != null,  lstTablesToMergeIfTransOK );
				}

				if ( result && serviceTransactions != null )
				{
					result = serviceTransactions.CommitTrans();
					if ( result )
					{
						foreach ( DataTable table in lstTablesToMergeIfTransOK )
							MergeOnLocalKey( table );
					}
				}
				if ( result && bAcceptChangeIfOk )
                    AcceptChanges();
				if ( !result )
				{
					if ( serviceTransactions != null )
						serviceTransactions.RollbackTrans();
					result.EmpileErreur("Erreur lors de la sauvegarde des données");
				}
			}
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		///A surcharger si l'ordre des modifs doit être différent
		protected virtual CResultAErreur MySaveAll( bool bExecutionEnTransaction, ArrayList lstTablesToMergeIfTransactionOk )
		{
			CResultAErreur result = CResultAErreur.True;
			result = DoAddAndUpdateInDB(bExecutionEnTransaction, lstTablesToMergeIfTransactionOk);
			if ( result )
				result = DoDeleteInDB();
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		protected CResultAErreur DoAddAndUpdateInDB ( bool bExecutionEnTransaction, ArrayList lstTablesToMergeIfTransOk )
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteDonnee ctx = (CContexteDonnee)GetCompleteChanges(DataRowState.Added | DataRowState.Modified );
			if ( ctx != null )
			{
                
				ArrayList listeTables = ctx.GetTablesOrderInsert();
				foreach ( DataTable table in listeTables )
				{
					if ( table.Rows.Count != 0  )
					{
						IObjetServeur loader = GetTableLoader(table.TableName);
						PrepareLoader ( loader );
						CContexteDonnee contexteLimite = ctx.GetDataSetForTableUnique(table.TableName, DataRowState.Added | DataRowState.Modified);
						if ( contexteLimite.Tables[table.TableName].Rows.Count > 0 )
						{
							result = loader.SaveAll(contexteLimite);
							if ( !result )
								break;
							if ( result.Data is DataTable )//Un loader qui ne sauve pas renvoie null
							{
								DataTable newTable = (DataTable)result.Data;
								foreach ( DataRow row in newTable.Rows )
									row[c_colIsFromDb] = true;
					
								//Stef 0605 : LEs modifs ne sont passées dans la base définitive que
								//lorsque tout est passé
								if ( !bExecutionEnTransaction )
									MergeOnLocalKey ( newTable );
								else
									lstTablesToMergeIfTransOk.Add ( newTable );
								ctx.MergeOnLocalKey( newTable );
							}
						}
					}
				}
			}
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		protected CResultAErreur DoDeleteInDB()
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteDonnee ctx = (CContexteDonnee)GetCompleteChanges(DataRowState.Deleted);
			if ( ctx != null )
			{
				ArrayList listeTables = ctx.GetTablesOrderDelete();
				foreach ( DataTable table in listeTables )
				{
					if ( table.Rows.Count != 0 )
					{
						IObjetServeur loader = GetTableLoader(table.TableName);
						PrepareLoader(loader);
						CContexteDonnee contexteLimite = ctx.GetDataSetForTableUnique(table.TableName, DataRowState.Deleted);
						if ( contexteLimite.Tables[table.TableName].Rows.Count !=  0)
						{
							result = loader.SaveAll(contexteLimite);
							if ( !result )
								break;
						}
					}
				}
			}
			return result;
		}*/

		/////////////////////////////////////////////////////////////////////////
		public 
#if PDA
#else
			new 
#endif
			DataSet GetChanges ( DataRowState state )
		{
			return GetCompleteChanges ( state );
		}

		/////////////////////////////////////////////////////////////////////////
		public virtual CContexteDonnee GetCompleteChanges ( DataRowState state )
		{
			CContexteDonnee contexte = (CContexteDonnee)Clone();
			contexte.m_nIdVersionDeTravail = m_nIdVersionDeTravail;
            contexte.IdModificationContextuelle = IdModificationContextuelle;
			contexte.EnforceConstraints = false;
			contexte.m_bEnableAutoStructure= true;
			contexte.CanReceiveNotifications = false;
            contexte.ContexteModification = ContexteModification;
			//CopieStructureTo ( contexte );
			foreach ( DataTable tableSource in GetTablesOrderInsert() )//Pour qu'il y ait le moins de parents possibles à créer
			{
				DataTable tableDest = contexte.Tables[tableSource.TableName];

				DataViewRowState rowViewState = DataViewRowState.None;
				if ( (state & DataRowState.Added) == DataRowState.Added )
					rowViewState |= DataViewRowState.Added;
				if ( (state & DataRowState.Deleted) == DataRowState.Deleted )
					rowViewState |= DataViewRowState.Deleted;
				if ( (state & DataRowState.Modified) == DataRowState.Modified )
					rowViewState |= DataViewRowState.ModifiedCurrent;

				//Stef 4/12/2005 : Utilise la fonction select car plus rapide que de balayer toutes les
				//rows
				
				DataRow[] rows = tableSource.Select ( null, null, rowViewState );
				
                List<DataRow> lstNewRows = new List<DataRow>();
				foreach ( DataRow row in rows )
				{
					if ( ((int)row.RowState & (int)state) != 0 )
					{
						if ( row.RowState == DataRowState.Deleted || !IsToRead(row))
							//Si la ligne 
							//doit être lue, elle n'a pas pu être changée
						{
							//Stef 1811 : Pourquoi copier les fils ? S'ils sont modifiés, ils seront copiés
							//Et pourquoi avec les parents, même remarque
							//remplacement de la ligne
							//DataRow newRow = CopieRowTo ( row, contexte, row.RowState!=DataRowState.Deleted, row.RowState!=DataRowState.Deleted, false );
							DataRow newRow = CopieRowTo ( row, contexte, false, false, false );
                            
							/*if ( row.RowState != DataRowState.Deleted )
								AssureParents ( newRow );*/
                            lstNewRows.Add ( newRow );

                            //Remarque : lors de la copie d'une table autoréférencée,
                            //un fils peut créer son parent du fait de ses dépendances.
                            //Or, lorsque le parent est ensuite ajouté,
                            //l'ancienne DataRow du parent est supprimée (donc Detached)
                            //de fait, lstNewRows peut contenir des DataRow à l'état Detached
                            //et c'est normal
						}
					}
				}
                foreach ( DataRow row in lstNewRows )
                {
                    if ( row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
                        AssureParents ( row );
                }
				/*if ( tableDest.Rows.Count == 0 )
				{
					try
					{
						contexte.Tables.Remove(tableDest);
					}
					catch
					{
					}
				}*/
			}
			contexte.EnforceConstraints = true;
			return contexte;
		}


		/////////////////////////////////////////////////////////////////////////
		//Retourne un CContexteDonnee contenant uniquement la table spécifiée et les
		//lignes avec l'état donné
		public CContexteDonnee GetDataSetForTableUnique ( string strTable, DataRowState state )
		{
			CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false);
			contexte.m_nIdVersionDeTravail = m_nIdVersionDeTravail;
            contexte.IdModificationContextuelle = IdModificationContextuelle;
			DataTable tableDest = CUtilDataSet.AddTableCopie ( Tables[strTable], contexte );
			DataTable tableSource = Tables[strTable];
			contexte.EnforceConstraints = false;
			foreach ( DataRow rowSource in tableSource.Rows )
			{
				if ( ((int)rowSource.RowState & (int)state) != 0 )
				{
					tableDest.ImportRow(rowSource);
				}
			}
			//contexte.EnforceConstraints = false;
			return contexte;
		}

		/*////////////////////////////////////////////////////////////////////////////
		public DataTable AddTableCopie ( DataTable tableSource )
		{
			DataTable table = new DataTable();
			table.CaseSensitive = tableSource.CaseSensitive;
			foreach ( DataColumn colSource in tableSource.Columns )
			{
				DataColumn col = new DataColumn();
				col.AllowDBNull = colSource.AllowDBNull;
				col.AutoIncrement = colSource.AutoIncrement;
				col.AutoIncrementSeed = colSource.AutoIncrementSeed;
				col.AutoIncrementStep = colSource.AutoIncrementStep;
				col.Caption = colSource.Caption;
				col.ColumnMapping = colSource.ColumnMapping;
				col.ColumnName = colSource.ColumnName;
				col.DataType = colSource.DataType;
				col.DefaultValue = colSource.DefaultValue;
				col.Expression = colSource.Expression;
				foreach ( object obj in colSource.ExtendedProperties.Keys )
					col.ExtendedProperties[obj] = colSource.ExtendedProperties[obj];
				col.MaxLength = colSource.MaxLength;
				col.Prefix = colSource.Prefix;
				col.ReadOnly = colSource.ReadOnly;
				col.Unique = colSource.Unique;
				table.Columns.Add ( col );
			}
			table.DisplayExpression = tableSource.DisplayExpression;
			foreach ( object obj in tableSource.ExtendedProperties.Keys )
				table.ExtendedProperties[obj] = tableSource.ExtendedProperties[obj];
			table.MinimumCapacity = tableSource.MinimumCapacity;
			table.Namespace = tableSource.Namespace;
			table.Prefix = tableSource.Prefix;
			ArrayList lstKeys = new ArrayList();
			foreach(  DataColumn col in tableSource.PrimaryKey )
				lstKeys.Add ( table.Columns[col.ColumnName] );
			table.PrimaryKey = (DataColumn[])lstKeys.ToArray(typeof(DataColumn));
			table.TableName = tableSource.TableName;
			Tables.Add ( table );
			return table;
		}*/


		/*////////////////////////////////////////////////////////////////////////////
		private void IntegreRows ( DataTable tableSource, DataTable tableDest )
		{
			//string strPrim = tableSource.PrimaryKey[0].ColumnName;
			string strFiltre = "";
			for ( int nCol = 0; nCol < tableSource.PrimaryKey.Length; nCol++ )
				strFiltre += tableSource.PrimaryKey[nCol].ColumnName+"=@"+nCol+",";
			if ( strFiltre.Length > 0 )
				strFiltre = strFiltre.Substring(0, strFiltre.Length-1);
			CFiltreData filtre = new CFiltreData();
			filtre.Filtre = strFiltre;

				
		    foreach ( DataRow rowSource in tableSource.Rows )
			{
				filtre.Parametres.Clear();
				foreach ( DataColumn col in tableSource.PrimaryKey )
					filtre.Parametres.Add ( rowSource[col.ColumnName] );
				DataRow[] rows = tableDest.Select(new CFormatteurFiltreDataToStringDataTable().GetString(filtre));
				DataRow newRow;
				if ( rows.Length > 0 )
					newRow = rows[0];
				else
					newRow = tableDest.NewRow();
				CopyRow ( rowSource, newRow );
				if ( newRow.RowState == DataRowState.Detached )
					tableDest.Rows.Add ( newRow );
			}
		}*/



		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Attention utilisation dangereuse !!! Supprime tous les enregistrements fille.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="bOnlyIfNotInDB"></param>
		/// <returns></returns>
		public bool DeleteFillesEnCascade( DataRow row, bool bOnlyIfNotInDB )
		{
			DataTable table = row.Table;
			DataRelationCollection relations = table.ChildRelations;
			bool bResult = true;
			foreach ( DataRelation relation in relations )
			{
				DataRow[] rows = row.GetChildRows( relation );
				foreach ( DataRow rowFille in rows )
				{
                    if (rowFille.RowState != DataRowState.Deleted && rowFille.RowState != DataRowState.Detached)
                        //Stef 30/10/2012 : si la ligne est déjà supprimée ou détachée, on ne
                        //fait rien. Ca arrive par exemple lors de la suppression sur
                        //des objets autoreferencés
                    {
                        if (!bOnlyIfNotInDB || rowFille.RowState == DataRowState.Added)
                        {
                            bResult = DeleteFillesEnCascade(rowFille, bOnlyIfNotInDB);
                            if (bResult)
                                rowFille.Delete();
                        }
                        else
                            bResult = false;
                    }
					if ( !bResult )
						return false;
				}
				if ( !bResult )
					return false;
			}
			return bResult;
		}

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Vide une table et toutes ses dépendances
		/// </summary>
		/// <param name="strNomTable"></param>
		public void ResetTableEtDependances ( string strNomTable )
		{
			ResetTableEtDependances ( Tables[strNomTable], new Hashtable() );
		}

		////////////////////////////////////////////////////////////////////////////
		private void ResetTableEtDependances ( DataTable table, Hashtable listeTraitees )
		{
			if ( table == null )
				return;
			foreach ( DataRelation relation in table.ChildRelations )
			{
				if ( listeTraitees[relation.ChildTable.TableName] == null )
				{
					listeTraitees[relation.ChildTable.TableName] = true;
					ResetTableEtDependances ( relation.ChildTable, listeTraitees );
				}
			}
			table.Clear();
			table.AcceptChanges();
		}

		////////////////////////////////////////////////////////////////////////////
		public CContexteDonnee GetMiniDataSetFor( CObjetDonnee objet )
		{
			CContexteDonnee ctx = new CContexteDonnee(IdSession, true, false);
			ctx.m_nIdVersionDeTravail = m_nIdVersionDeTravail;
            ctx.IdModificationContextuelle = IdModificationContextuelle;
			ctx.CanReceiveNotifications = false;
			CopieStructureTo ( ctx );
			CopieRowTo ( objet.Row, ctx, true, true, false );
			return ctx;
		}

		////////////////////////////////////////////////////////////////////////////
		internal void CopieStructureTo ( DataSet dsDest )
		{
			MemoryStream stream = new MemoryStream();
#if PDA
			XmlTextWriter writer = new XmlTextWriter ( stream, System.Text.Encoding.Unicode );
			this.WriteXmlSchema(writer);
#else
			this.WriteXmlSchema(stream);
#endif
			bool bOldAutoStructure = false;
			if ( dsDest is CContexteDonnee )
			{
				bOldAutoStructure = ((CContexteDonnee)dsDest).EnableAutoStructure;
				((CContexteDonnee)dsDest).SetEnableAutoStructure( false );
			}
#if PDA
			MemoryStream readerStream = new MemoryStream ( stream.GetBuffer() );
			XmlTextReader reader = new XmlTextReader(readerStream );
			dsDest.ReadXmlSchema ( reader );
#else
			MemoryStream strNew = new MemoryStream(stream.GetBuffer() );
			dsDest.ReadXmlSchema ( strNew );
#endif
			if ( dsDest is CContexteDonnee )
			{
				((CContexteDonnee)dsDest).SetEnableAutoStructure( bOldAutoStructure );
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public DataRow CopieRowTo (
			DataRow row,
			DataSet ctxDest,
			bool bAvecFils,
			bool bAvecParents,
			bool bEcarserData )
		{
			return CopieRowTo ( row, ctxDest, bAvecFils, bAvecParents, bEcarserData, null );
		}

		////////////////////////////////////////////////////////////////////////////
		public object[] GetValeursCles ( DataRow row, DataRowVersion version )
		{
			DataColumn[] keys = row.Table.PrimaryKey;
			object[] valKeys = new object[keys.Length];
			for ( int n = 0; n < keys.Length; n++ )
				valKeys[n] = row[keys[n], version];
			return valKeys;
		}

		////////////////////////////////////////////////////////////////////////////
		public DataRow CopieRowTo ( 
			DataRow row, 
			DataSet ctxDest, 
			bool bAvecFils, 
			bool bAvecParents, 
			bool bEcraserData, 
			Hashtable tableRowsDejaCopiees)
		{
			if ( row == null )
				return null;
			if ( tableRowsDejaCopiees == null )
				tableRowsDejaCopiees = new Hashtable();
			if ( tableRowsDejaCopiees[row] != null )
				return (DataRow)tableRowsDejaCopiees[row];


			DataRowVersion versionToCopy = DataRowVersion.Default;
			if ( row.RowState == DataRowState.Deleted )
				versionToCopy = DataRowVersion.Original;

			DataTable tableDest = ctxDest.Tables[row.Table.TableName];
			if ( tableDest == null )
				return null;
			
			DataRow newRow=null;
			//Si l'élément n'existe pas, il est créé
			object[] cles = GetValeursCles ( row, versionToCopy );
			DataRow rowExistante = tableDest.Rows.Find ( cles );
            if (row.RowState == DataRowState.Deleted)
            {
                //cherche dans les supprimés si elle existe
                CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(row.Table.PrimaryKey,
                    row,
                    DataRowVersion.Original);
                string strFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(filtre);
                DataRow[] rowsDeleted = tableDest.Select(strFiltre, "", DataViewRowState.Deleted);
                if (rowsDeleted.Length > 0)
                    rowExistante = rowsDeleted[0];
            }

			//Greffon pour les tables autoréférencées : la ligne nouvelle 
			//a été ajoutée par un fils
			if ( rowExistante != null && row.RowState == DataRowState.Added && IsToRead(rowExistante))
			{
				try
				{
					tableDest.Rows.Remove(rowExistante);
					rowExistante = null;
				}
				catch//S'il y a erreur, c'est que la ligne ne peut pas être supprimée
					//Donc que ce n'est pas juste un ajout de parent par objet autoreference
				{ }
			}
				
			bool bRowDejaDedans = rowExistante != null;

			if ( !bEcraserData && bRowDejaDedans && !bAvecFils && !IsToRead(rowExistante) )
				return rowExistante;

			
			

			//Copie les parents
			if ( bAvecParents )
			{
				foreach ( DataRelation parentRelation in row.Table.ParentRelations )
				{
					DataRow rowParente = row.GetParentRow(parentRelation, versionToCopy);

					if ( rowParente != null && tableRowsDejaCopiees[rowParente] == null)
					{
						tableRowsDejaCopiees[rowParente] = CopieRowTo ( rowParente, ctxDest, false, true, false, tableRowsDejaCopiees );
					}
				}
			}

			//Il se peut que la copie des parents ai provoqué la copie du fils (si composition)
			//Dans ce cas, maintenait la row est peut être déjà copiée
			if ( tableRowsDejaCopiees[row] != null )
				return (DataRow)tableRowsDejaCopiees[row];

			
			if ( rowExistante == null )
			{
				//Si le contexte dans lequel on copie contient des tables parentes que le contexte
				//parent ne contient pas, il peut y avoir un pb avec les parents.
				//c'est pourquoi, il faut s'assurer que les parents existent !

				bool bOldEnforceTableDest = tableDest.DataSet.EnforceConstraints;
				if (bOldEnforceTableDest)
					tableDest.DataSet.EnforceConstraints = false;
				bool bOldEnforceTableSource = row.Table.DataSet.EnforceConstraints;
				if (row.RowState == DataRowState.Deleted)
				{
					//Copie toutes les valeurs originales
					newRow = tableDest.NewRow();
					foreach (DataColumn col in row.Table.Columns)
					{
						DataColumn colDest = tableDest.Columns[col.ColumnName];
						if (colDest != null)
							newRow[colDest] = row[col, DataRowVersion.Original];
					}
					tableDest.Rows.Add(newRow);
					newRow.AcceptChanges();
					newRow.Delete();
				}
				else
				{
					tableDest.ImportRow(row);
					newRow = tableDest.Rows.Find(cles);
				}
				if ( newRow == null )//C'est possible qu'on ne trouve pas la ligne si elle est dans l'état "Supprimée"
				{
					CFiltreData filtrePrimKey = CFiltreData.CreateFiltreAndSurRow(tableDest.PrimaryKey, row, versionToCopy);
					string strFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(filtrePrimKey);
					newRow = tableDest.Select(strFiltre,"", DataViewRowState.CurrentRows | DataViewRowState.Deleted)[0];
				}
				if (newRow != null && tableDest.DataSet is CContexteDonnee && bOldEnforceTableDest)
				{
					try
					{
						tableDest.DataSet.EnforceConstraints = bOldEnforceTableDest;
					}
					catch
					{
						((CContexteDonnee)tableDest.DataSet).AssureParents(newRow);
						tableDest.DataSet.EnforceConstraints = bOldEnforceTableDest;
					}
				}
				if (tableDest.DataSet.EnforceConstraints != bOldEnforceTableDest)
					tableDest.DataSet.EnforceConstraints = bOldEnforceTableDest;
			}
			else
			{
				newRow = rowExistante;
				bRowDejaDedans = !IsToRead(newRow);
				if ( bEcraserData || IsToRead(newRow)) 
					CopyRow(row, newRow, versionToCopy);
			}
			tableRowsDejaCopiees[row] = newRow;
			if ( row.RowState == DataRowState.Unchanged )
				newRow.AcceptChanges();
		
			foreach ( DataRelation childRelation in row.Table.ChildRelations )
			{
				object extProp = childRelation.ExtendedProperties[c_extPropRelationComposition];
				bool bComposition = false;
				//Erreur .Net Framework, lors de la copie du paramètre, il est 
				//copié sous forme de chaine !!!
				if ( extProp is bool )
					bComposition = (bool)extProp;
				else
					bComposition = extProp.ToString()==true.ToString();
				
				bool bEcraserDataFils = true;
				bool bCopierLesFils = bAvecFils && bComposition;

				DataRelation relationInNewContexte = ctxDest.Relations[childRelation.RelationName];
				if ( relationInNewContexte != null )
				{
					if ( bComposition && !bAvecFils && newRow.GetChildRows(relationInNewContexte, versionToCopy).Length > 0 )
					{
						//AHAHA on ne copie pas les fils, mais il y en a qui sont dedans, il faut
						//Copier quand même, sinon, il y a un risque que les fils soient faux, mais sans écraser de donnée (il se peut que l'un des
						//fils soit en édition) !
						bCopierLesFils = true;
						bEcraserDataFils = false;
					}
				}

				if ( bCopierLesFils ) 
				{
					foreach ( DataRow rowFils in row.GetChildRows(childRelation,versionToCopy) )
						//06/05 : Lors de la copie des fils, il faut aussi copier les
						//parents, sinon, pb d'intégrité
					{
						if ( tableRowsDejaCopiees[rowFils] == null )
						{
							tableRowsDejaCopiees[rowFils] = CopieRowTo ( rowFils, ctxDest, true, true, bEcraserDataFils, tableRowsDejaCopiees );
						}
					}
				}
				else if ( !bRowDejaDedans &&  newRow != null && newRow.RowState != DataRowState.Deleted )
				{
                    if (newRow.Table.Columns[childRelation.RelationName] != null)
                        //ChangeRowSansDetectionModification ( newRow, childRelation.RelationName,false );
                        InvalideCacheDependance(newRow, childRelation);
				}
			}
            foreach ( RelationTypeIdAttribute relationTypeId in RelationsTypeIds )
                InvalideCacheDependance ( newRow, relationTypeId.GetNomColDepLue() );
			return newRow;
		}

		////////////////////////////////////////////////////////////////////////////
		public void CopyRow ( DataRow rowSource, DataRow rowDest, DataRowVersion versionToCopy, params string[] strExclusions )
		{
			CopyRow ( rowSource, rowDest, versionToCopy, true, strExclusions );
		}

		////////////////////////////////////////////////////////////////////////////
		public void CopyRow ( DataRow rowSource, DataRow rowDest, DataRowVersion versionToCopy, bool bKeepUnchanged,params string[] strExclusions )
		{
			Hashtable tbl = new Hashtable();
			foreach ( string strExclusion in strExclusions )
				tbl.Add(strExclusion,"");
			//Exclue systématiquement la colonne localkey des copies
			tbl[c_colLocalKey] = "";
			bool bWasUnchanged = rowDest.RowState == DataRowState.Unchanged;
			bool bOldEnforceConstraints = rowDest.Table.DataSet.EnforceConstraints;
			try
			{
                if (rowDest.RowState != DataRowState.Deleted)
                {
                    foreach (DataColumn col in rowSource.Table.Columns)
                    {
                        if (tbl[col.ColumnName] == null &&
                            rowDest.Table.Columns[col.ColumnName] != null &&
                            col.ExtendedProperties[c_cleIsColIndicateurChargementForeignKey] == null //Ce n'est pas un indicateur de chargement de dépendances
                            )
                        {
                            if (col.DataType != typeof(CDonneeBinaireInRow) ||
                                rowSource[col.ColumnName, versionToCopy] is CDonneeBinaireInRow)
                                try
                                {
                                    rowDest[col.ColumnName] = rowSource[col.ColumnName, versionToCopy];
                                }
                                catch
                                {
                                    if (rowDest.Table.DataSet.EnforceConstraints)
                                    {
                                        rowDest.Table.DataSet.EnforceConstraints = false;
                                        rowDest[col.ColumnName] = rowSource[col.ColumnName, versionToCopy];
                                    }
                                    else
                                        throw new Exception(I.T("Error|199"));
                                }
                        }
                    }
                }
				try
				{
					if ( rowDest.Table.DataSet.EnforceConstraints != bOldEnforceConstraints )
						rowDest.Table.DataSet.EnforceConstraints = bOldEnforceConstraints;
				}
				catch
				{
					//Lorsque l'on essaie de remettre les contraintes, on a une erreur, elle est
					//due à 95% sûr du fait qu'un parent n'est pas là !!!
					AssureParents ( rowDest );
				}

				//Si les contraintes ne sont pas activées, il faut s'assurer ici que les parents
				//de la ligne existent, sinon on risque de planter quand on remettra les contraintes
				if ( !bOldEnforceConstraints )
					AssureParents ( rowDest );
			}

			catch {}
			finally
			{
				if ( rowDest.Table.DataSet.EnforceConstraints != bOldEnforceConstraints )
					rowDest.Table.DataSet.EnforceConstraints = bOldEnforceConstraints;
			}
			if ( bWasUnchanged && bKeepUnchanged )
				rowDest.AcceptChanges();
		}
		

		////////////////////////////////////////////////////////////////////////////
		public void CopyRow ( DataRow rowSource, DataRow rowDest, params string[] strExclusions )
		{
			CopyRow ( rowSource, rowDest, DataRowVersion.Default, strExclusions );
		}

		////////////////////////////////////////////////////////////////////////////
		public void CopyRow ( DataRow rowSource, DataRow rowDest, bool bKeepUnchanged ,params string[] strExclusions )
		{
			CopyRow ( rowSource, rowDest, DataRowVersion.Default, bKeepUnchanged, strExclusions );
		}

		
		#endregion

		////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Recherche un objet dans le contexte. S'il n'est pas trouvé, il peut être
		/// recherché dans la base
		/// </summary>
		/// <param name="strNomTable">Nom de la table dans laquelle faire la recherche</param>
		/// <param name="filtre">Filtre de recherche</param>
		/// <param name="bNoRead">Indique si la lecture dans la base de données est autorisée</param>
		/// <returns></returns>
		public CObjetDonnee FindObjetUnique ( string strNomTable, CFiltreData filtre, bool bNoRead )
		{
			CListeObjetsDonnees liste = new CListeObjetsDonnees ( this, GetTypeForTable ( strNomTable ));
			liste.Filtre = filtre;
			liste.InterditLectureInDB = true;
			if ( liste.Count != 0 )//Il y a au moins un élément dans le contexte en cours
				return (CObjetDonnee)liste[0];
			//L'élément n'a pas été trouvé dans le contexte en cours,
			//cherche dans la base
			if ( !bNoRead )
			{
				liste = new CListeObjetsDonnees ( this, GetTypeForTable ( strNomTable ));
				liste.Filtre = filtre;
				if ( liste.Count != 0 )
					return (CObjetDonnee)liste[0];
			}
			return null;
		}
		#region Membres de IObjetAttacheASession

		public void OnCloseSession()
		{
			Dispose();
		}

		public string DescriptifObjetAttacheASession
		{
			get
			{
				return I.T("Data Context @1|127",m_nNumeroContexteDonnee.ToString());
			}
		}
		#endregion



		public void RefreshDependances(DataRow dataRow)
		{
			foreach ( DataRelation relation in dataRow.Table.ChildRelations )
                InvalideCacheDependance(dataRow, relation);
            foreach ( RelationTypeIdAttribute relTypeId in RelationsTypeIds )
                InvalideCacheDependance ( dataRow, relTypeId.GetNomColDepLue() );
		}

        public void InvalideCacheDependance(DataRow row, DataRelation relation)
        {
            string strNomColonne = GetForeignKeyName(relation);
            InvalideCacheDependance(row, strNomColonne);
            if (OnInvalideCacheRelation != null)
                OnInvalideCacheRelation(row, relation);
        }

        public void InvalideCacheDependance ( DataRow row, string strNomColonne )
        {
            if(row.Table.Columns.Contains(strNomColonne))
                ChangeRowSansDetectionModification(row, strNomColonne, false);
        }


        #region IObjetAContexteDonnee Membres

        public CContexteDonnee ContexteDonnee
        {
            get { return this; }
        }

        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return ContexteDonnee;
            }
        }

        #endregion

        /// <summary>
        /// S'assure qu'il n'y a pas dans la table d'élément à IsToRead
        /// </summary>
        /// <param name="typeObjets"></param>
        internal void AssureAucunAIsToRead(Type typeObjets)
        {
            string strNomTable = GetNomTableForType(typeObjets);
            bool bLectureUnParUn = false;
            if (m_bGestionParTablesCompletes && typeof(IObjetALectureTableComplete).IsAssignableFrom(typeObjets))
            {
                if ( m_tablesCompletesInvalides[strNomTable] != null )
                {
                    GetTableSafe ( strNomTable );
                    return;
                }
                bLectureUnParUn = true;
            }
            DataTable table = Tables[strNomTable];
            if (table != null && table.Columns.Contains(c_colIsToRead))
            {
                DataRow[] rows = table.Select(c_colIsToRead + "=1");
                if (rows.Length == 0)
                    return;
                if (typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(typeObjets) && !bLectureUnParUn)
                {
                    string strCle = table.PrimaryKey[0].ColumnName;
                    StringBuilder bl = new StringBuilder();
                    foreach (DataRow row in rows)
                    {
                        bl.Append(row[strCle]);
                        bl.Append(',');
                    }
                    bl.Remove(bl.Length - 1, 1);
                    CListeObjetsDonnees lst = new CListeObjetsDonnees(this, typeObjets);
                    lst.PreventVerifIsToRead = true;
                    lst.Filtre = new CFiltreData(strCle + " in (" + bl.ToString() + ")");
                    lst.AssureLectureFaite();
                }
                else
                {
                    foreach (DataRow row in rows)
                    {
                        ReadRow(row);
                    }
                }
            }
        }

        //------------------------------------------------------------
        public CDbKey GetKeyFromId<T>(int nId)
        {
            CObjetDonneeAIdNumerique obj = Activator.CreateInstance(typeof(T), new object[] { this }) as CObjetDonneeAIdNumerique;
            if (obj != null && obj.ReadIfExists ( nId ))
                return obj.DbKey;
            return null;
        }

        //------------------------------------------------------------
        public int? GetIdFromKey<T>(CDbKey key)
        {
            CObjetDonneeAIdNumerique obj = Activator.CreateInstance(typeof(T), new object[] { this }) as CObjetDonneeAIdNumerique;
            if (obj != null && obj.ReadIfExists(key) )
                return obj.Id;
            return null;
        }

    }

 
}
