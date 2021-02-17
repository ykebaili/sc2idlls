using System;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using sc2i.common;
using sc2i.expression;
using System.Drawing;
using System.IO;


namespace sc2i.data
{
    public delegate CResultAErreur VerifieDonneesDelegate(CObjetDonnee objet);
    /// <summary>
    /// Description résumée de CObjetFromDB.
    /// </summary>
    [Serializable]
    //[DynamicClassAttribute("Objet donnée générique")]
    public abstract class CObjetDonnee :
        IDisposable,
        IObjetDonnee,
        IAttacheurObjetsAContexteEvaluationExpression
    {
        /// <summary>
        /// Colonne sur chaque objet indiquant s'il faut appeler "VerifieDonnee"
        /// au moment de la sauvegarde de l'objet
        /// </summary>
        public const string c_champVerifierDonneesALaSauvegarde = "_2I_VERIF_DATA";

        /// //////////////////////////////////////////////////////////////
        public const string c_champIdUniversel = "UNIVERSAL_ID";

        /// <summary>
        /// Colonne indiquant dans quel contexte a été modifié l'objet
        /// Par défaut, vaut la chaine vide, mais certains process
        /// peuvent mettre une autre chaine dans cette colonne
        /// et l'exploiter pour des vérifications par exemple
        /// </summary>
        public const string c_champContexteModification = "__CTX_MODIF";

        public const string c_categorieChampSystème = "System";
        [NonSerializedAttribute]
        protected DataRow m_row = null;
        [NonSerializedAttribute]
        private CContexteDonnee m_contexte = null;

        private DataRowVersion m_rowVersionToReturn = DataRowVersion.Default;

#if PDA
		////////////////////////////////////////////////////////////
		public CObjetDonnee()
		{
			m_row = null;
			m_contexte = null;
		}
#endif
        ////////////////////////////////////////////////////////////
        public CObjetDonnee(CContexteDonnee contexte)
        {
            m_row = null;
            m_contexte = contexte;
        }

        ////////////////////////////////////////////////////////////
        public CObjetDonnee(DataRow row)
        {
            m_contexte = (CContexteDonnee)row.Table.DataSet;
            SetRow(row);
            AfterRead();
        }

        ////////////////////////////////////////////////////////////
        ~CObjetDonnee()
        {
            Dispose();
        }

        ////////////////////////////////////////////////////////////
        public DataRowVersion VersionToReturn
        {
            get
            {
                return m_rowVersionToReturn;
            }
            set
            {
                m_rowVersionToReturn = value;
            }
        }

        

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne true si l'objet est valide (existe et n'est pas supprimé)
        /// </summary>
        /// <returns></returns>
        public bool IsValide()
        {
            if (m_row == null)
                return false;

            if (m_row.RowState == DataRowState.Deleted || m_row.RowState == DataRowState.Detached)
                return false;
            if (ContexteDonnee.IsHorsVersion(m_row))
                return false;
            return true;
        }

        ////////////////////////////////////////////////////////////
        //Stef 25/07/08 mise en cache des champsId
        private static Dictionary<Type, string[]> m_cacheChampsId = new Dictionary<Type, string[]>();
        /// <summary>
        /// Retourne les champs id de l'élément
        /// </summary>
        /// <returns></returns>
        public virtual string[] GetChampsId()
        {
            return GetChampsId(GetType());
        }

        ////////////////////////////////////////////////////////////
        public static string[] GetChampsId(Type typeObjet)
        {
            string[] strChamps = null;
            if (m_cacheChampsId.TryGetValue(typeObjet, out strChamps))
                return strChamps;
            object[] attribs = typeObjet.GetCustomAttributes(typeof(TableAttribute), true);
            if (attribs.Length == 0)
                throw new Exception(I.T("The class @1 has no class attribute|148", typeObjet.ToString()));
            strChamps = ((TableAttribute)attribs[0]).ChampsId;
            m_cacheChampsId[typeObjet] = strChamps;
            return strChamps;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne un tableau contenant les noms des champs de tri par défaut
        /// Les noms de champs peuvent être suivis de ASC ou de DESC
        /// </summary>
        /// <returns></returns>
        public abstract string[] GetChampsTriParDefaut();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne le filtre par défaut à appliquer aux listes
        /// Dés qu'une liste de ce type d'objet est créée, le filtre standard
        /// est appliqué ( dans le filtre de base de la liste )
        /// </summary>
        public virtual CFiltreData FiltreStandard
        {
            get
            {
                return null;
            }
        }


        ////////////////////////////////////////////////////////////
        public int GetInternalKey()
        {
            return (int)Row[CContexteDonnee.c_colLocalKey];
        }

        ////////////////////////////////////////////////////////////
        public bool ReadIfExistInternalKey(int nKey)
        {
            DataRow[] rows = Table.Select(CContexteDonnee.c_colLocalKey + "=" + nKey.ToString());
            if (rows.Length != 0)
            {
                //Stef 2/5/2008  remplacé m_row= par SetRow
                SetRow(rows[0]);
                return true;
            }
            return false;
        }

        ////////////////////////////////////////////////////////////
        public virtual string GetChampsTriParDefautSeparesVirgule()
        {
            string strListe = "";
            foreach (string strChamp in GetChampsTriParDefaut())
                strListe += strChamp + ",";
            if (strListe.Length > 0)
                strListe = strListe.Substring(0, strListe.Length - 1);
            return strListe;
        }

        ////////////////////////////////////////////////////////////
        //Stef 25/07/08 mise en cache des noms de tables
        private static Dictionary<Type, string> m_cacheNomsTables = new Dictionary<Type, string>();
        public virtual string GetNomTable()
        {
            string strNomTable = "";
            if (m_cacheNomsTables.TryGetValue(GetType(), out strNomTable))
                return strNomTable;
            object[] attribs = GetType().GetCustomAttributes(typeof(TableAttribute), true);
            if (attribs.Length == 0)
                throw new Exception(I.T("The class @1 has no class attribute|148", GetType().ToString()));
            strNomTable = ((TableAttribute)attribs[0]).NomTable;
            m_cacheNomsTables[GetType()] = strNomTable;
            return strNomTable;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne le nom de la table dans la base de données
        /// Par défaut, retourne le même nom que la table 
        /// </summary>
        /// <returns></returns>
        private static Dictionary<Type, string> m_cacheNomsTablesInDb = new Dictionary<Type, string>();
        public virtual string GetNomTableInDb()
        {
            string strNomTableInDb = "";
            if (m_cacheNomsTablesInDb.TryGetValue(GetType(), out strNomTableInDb))
                return strNomTableInDb;
            object[] attribs = GetType().GetCustomAttributes(typeof(TableAttribute), true);
            if (attribs.Length == 0)
                throw new Exception(I.T("The class @1 has no class attribute|148", GetType().ToString()));
            strNomTableInDb = ((TableAttribute)attribs[0]).NomTableInDb;
            m_cacheNomsTablesInDb[GetType()] = strNomTableInDb;
            return strNomTableInDb;
        }


        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne les valeurs de clé de l'élément
        /// </summary>
        /// <returns></returns>
        public virtual object[] GetValeursCles()
        {
            AssureRow();
            string[] strCles = GetChampsId();
            object[] lst = new object[strCles.Length];
            int nIndex = 0;
            foreach (string strChamp in strCles)
                lst[nIndex++] = m_row[strChamp, VersionToReturn];
            return lst;
        }

        ////////////////////////////////////////////////////////////
        public void SetValeursMultiples(string[] strChamps, object[] strValeurs)
        {
            bool bOldEnforce = ContexteDonnee.EnforceConstraints;
            ContexteDonnee.EnforceConstraints = false;
            for (int n = 0; n < strChamps.Length; n++)
            {
                Row[strChamps[n]] = strValeurs[n];
            }
            ContexteDonnee.EnforceConstraints = bOldEnforce;
        }

        ////////////////////////////////////////////////////////////
        public CFiltreData GetFiltreCles(object[] valeurs)
        {
            CFiltreData filtre = new CFiltreData();
            int nCle = 0;
            foreach (DataColumn col in Table.PrimaryKey)
            {
                if (filtre.Filtre.Length > 0)
                    filtre.Filtre += " and ";
                filtre.Filtre += col.ColumnName + "=@" + (filtre.Parametres.Count + 1);
                filtre.Parametres.Add(valeurs[nCle]);
                nCle++;
            }
            return filtre;
        }

        ////////////////////////////////////////////////////////////
        public string GetFiltreClesString(object[] valeurs)
        {
            CFiltreData filtre = GetFiltreCles(valeurs);
            string strFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(filtre);
            return strFiltre;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Définit la clé de l'élement, les valeurs doivent être dans l'ordre de définition dans la clé
        /// </summary>
        /// <param name="valeurs"></param>
        public void PointeSurLigne(params object[] valeurs)
        {
            if (m_row != null && (m_row.RowState == DataRowState.Detached ||
                m_row.RowState == DataRowState.Deleted))
                m_row = null;
            if (m_row != null)
            {
                object[] oldValeurs = GetValeursCles();
                bool bTousPareil = true;
                for (int nVal = 0; nVal < oldValeurs.Length; nVal++)
                    if (oldValeurs[nVal] != valeurs[nVal])
                    {
                        bTousPareil = false;
                        break;
                    }
                if (bTousPareil)
                    return;//Pas changé de clé
            }
            DataRow rowTrouvee = Table.Rows.Find(valeurs);
            if (rowTrouvee == null)
            {
                //Vérifie que la ligne n'a pas été supprimée
                string strFiltre = GetFiltreClesString(valeurs);
                DataRow[] rows = Table.Select(strFiltre, "", DataViewRowState.Deleted);
                if (rows.Length > 0)
                    rowTrouvee = rows[0];
            }
            if (rowTrouvee == null)
            {
                DataRow newRow = Table.NewRow();
                int nIndex = 0;
                string[] strIds = GetChampsId();
                foreach (string strChamp in strIds)
                    newRow[strChamp] = valeurs[nIndex++];
                ContexteDonnee.AddRow(newRow);
                m_contexte.SetIsToRead(newRow, true);
                m_contexte.SetIsFromDb(newRow, false);
                newRow.AcceptChanges();
                SetRow(newRow);
            }
            else
            {
                SetRow(rowTrouvee);
            }
        }

        ////////////////////////////////////////////////////////////
        ///<summary>
        ///Initialise l'objet avec ses valeurs par défaut
        ///</summary>
        protected abstract void MyInitValeurDefaut();

        //-------------------------------------------------------------------
        protected virtual void InitValeurDefaut()
        {
            Type tp = this.GetType();
            if ( ManageIdUniversel )
                Row[c_champIdUniversel] = CUniqueIdentifier.GetNew();
            foreach (PropertyInfo prop in tp.GetProperties())
            {
                object[] attribs = prop.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
                bool bNull = false;
                if (attribs != null && attribs.Length > 0)
                {
                    bNull = ((TableFieldPropertyAttribute)attribs[0]).NullAutorise;
                }
                if (!bNull && attribs.Length > 0)
                {
                    if (prop.PropertyType == typeof(string) && prop.Name != "DescriptionElement")
                    {
                        MethodInfo method = prop.GetSetMethod();
                        if (method != null)
                        {
                            object[] objText = { "" };
                            try
                            {
                                method.Invoke(this, objText);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(I.T("Initialization error @1 \r\n @2 \r\n stack : @3|149", prop.Name, e.Message, e.StackTrace.ToString()));
                            }
                        }
                    }
                    else if ((prop.PropertyType == typeof(double)) || (prop.PropertyType == typeof(int) && prop.Name != "Id"))
                    {
                        MethodInfo method = prop.GetSetMethod();
                        if (method != null)
                        {
                            object[] objNum = { 0 };
                            try
                            {
                                method.Invoke(this, objNum);
                            }
                            catch { }
                        }
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        MethodInfo method = prop.GetSetMethod();
                        if (method != null)
                        {
                            object[] objBool = { false };
                            try
                            {
                                method.Invoke(this, objBool);
                            }
                            catch { }
                        }
                    }
                }
            }
            MyInitValeurDefaut();
        }
        ////////////////////////////////////////////////////////////
        public virtual string GetLoaderURI()
        {
            Type tp = this.GetType();
            return tp.ToString() + "Loader";
        }

        ////////////////////////////////////////////////////////////
        public virtual void Dispose()
        {
            try
            {
                SetRow(null);
            }
            catch
            {
            }
        }






        ////////////////////////////////////////////////////////////
        public virtual void AfterRead()
        {
        }

        ////////////////////////////////////////////////////////////
        public virtual IObjetServeur GetLoader()
        {
            AssureRow();
            return m_contexte.GetTableLoader(GetNomTable());

        }

        ////////////////////////////////////////////////////////////
        public DataTable Table
        {
            get
            {
                return m_contexte.GetTableSafe(GetNomTable());
            }
        }

        ////////////////////////////////////////////////////////////
        public C2iDataRow Row
        {
            get
            {
                AssureData();
                return new C2iDataRow(m_row, m_rowVersionToReturn);
            }
        }

        ////////////////////////////////////////////////////////////
        public CContexteDonnee ContexteDonnee
        {
            get
            {
                return m_contexte;
            }
#if PDA
			set
			{
				m_contexte = value;
			}
#endif
        }

        ////////////////////////////////////////////////////////////
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return ContexteDonnee;
            }
        }

        ////////////////////////////////////////////////////////////
        public void SetRow(DataRow row)
        {
            if (row == m_row)
                return;
            m_row = row;
            if (m_row != null)
                m_contexte = (CContexteDonnee)m_row.Table.DataSet;
            OnRowChanged();
        }


        /// <summary>
        /// Permet d'executer du code quand la ligne pointée  a changé.
        /// </summary>
        protected virtual void OnRowChanged()
        {
        }


        /////////////////////////////////////////////////////////////////
        public void AssureRow(params object[] cles)
        {
            if (m_row == null)
            {
                DataRow row = Table.NewRow();
                m_contexte.SetIsToRead(row, true);
                //S'assure que les clés ne sont pas nulles
                int nCle = 0;
                foreach (string strCle in GetChampsId())
                {
                    if (row[strCle] == DBNull.Value)
                    {
                        if (cles != null && cles.Length > nCle)
                            row[strCle] = cles[nCle];
                        else
                            row[strCle] = ContexteDonnee.GetNewValeurCle(row, strCle);
                    }
                    nCle++;
                }
                row[CContexteDonnee.c_colIdContexteCreation] = ContexteDonnee.IdContexteDonnee;
                ContexteDonnee.AddRow(row);
                //Table.Rows.Add ( row );
                SetRow(row);
                InitValeurDefaut();
            }
        }

        /////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initialise l'élément en création et edition
        /// </summary>
        /// l'élement est supprimé du contexte principal et 
        /// déplacé dans un contexte d'édition.
        public void CreateNew(params object[] valeursCles)
        {
            SetRow(Table.NewRow());
            InitValeurDefaut();
            DataRow rowOriginal = m_row;
            if (valeursCles != null && valeursCles.Length != 0)
            {
                int nIndex = 0;
                foreach (string strChamp in GetChampsId())
                    m_row[strChamp] = valeursCles[nIndex++];
            }
            ContexteDonnee.AddRow(m_row);
            //Table.Rows.Add ( m_row );
            m_contexte.SetIsToRead(m_row, false);
            //Crée un contexte d'édition pour l'élément
            BeginEdit();
            Row[CContexteDonnee.c_colIdContexteCreation] = ContexteDonnee.IdContexteDonnee;
            //Supprime la ligne dans le contexte général
            rowOriginal.Table.Rows.Remove(rowOriginal);
        }

        /////////////////////////////////////////////////////////////////
        public virtual bool IsNew()
        {
            if (m_row != null)
                return m_row.RowState == DataRowState.Added;
            return false;
            /*object[] cles = this.GetValeursCles();
            foreach ( object obj in cles )
                if ( obj != null && obj != DBNull.Value )
                    return false;
            return true;*/
        }

        /////////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique que l'objet a été créé dans ce contexte
        /// </summary>
        /// <returns></returns>
        public virtual bool IsNewInThisContexte()
        {
            ///Une ligne est nouvelle dans ce contexte si elle est nouvelle et
            ///que son id auto est supérieure à l'autoincrementSeed de sa colonne de clé primaire
            if (IsNew())
            {
                object val = m_row[CContexteDonnee.c_colIdContexteCreation];
                if (val is int)
                    return (int)val == ContexteDonnee.IdContexteDonnee;

                /*DataColumn[] cols = m_row.Table.PrimaryKey;
                if (cols.Length == 1 && cols[0].AutoIncrement)
                {
                    if (((int)m_row[cols[0]]) > cols[0].AutoIncrementSeed)
                        return false;
                }
                return true;*/
            }
            return false;
        }
        /////////////////////////////////////////////////////////////////
        public void CreateNewInCurrentContexte(params object[] valeursCles)
        {
            AssureRow(valeursCles);
            if (valeursCles != null)
            {
                int nIndex = 0;
                bool bOldEnforce = ContexteDonnee.EnforceConstraints;
                ContexteDonnee.EnforceConstraints = false;
                foreach (string strChamp in GetChampsId())
                    m_row[strChamp] = valeursCles[nIndex++];
                ContexteDonnee.EnforceConstraints = bOldEnforce;
            }
            Row[CContexteDonnee.c_colIdContexteCreation] = ContexteDonnee.IdContexteDonnee;
        }

        /////////////////////////////////////////////////////////////////
        ///<summary>
        ///S'assure que les données sont chargées dans la ligne
        ///</summary>
        public void AssureData()
        {
            AssureRow();
            if (m_row.RowState == DataRowState.Deleted || m_row.RowState == DataRowState.Detached)
                return;
            if (!m_contexte.IsToRead(m_row))
                return;
            if (IsNewInThisContexte())
            {
                m_contexte.SetIsToRead(m_row, false);
                return;
            }

            DataRow newRow = m_contexte.ReadRow(m_row, this);
            if (newRow != null)
            {
                SetRow(newRow);
                AfterRead();
                m_row.AcceptChanges();
            }
            else
            {
                m_contexte.SetIsToRead(m_row, false);
                m_contexte.SetIsFromDb(m_row, false);
            }
        }

        public bool ReadIfExists(params object[] valeursCle)
        {
            return ReadIfExists(valeursCle, true);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique si ce type d'objet utilise les idsUniversels
        /// </summary>
        static Dictionary<Type, bool> m_dicTypeManagesIdUniversel = new Dictionary<Type, bool>();
        public bool ManageIdUniversel
        {
            get
            {
                return TypeManageIdUniversel(GetType());
            }
        }

        ////////////////////////////////////////////////////////////
        public static bool TypeManageIdUniversel(Type tp)
        {
            bool bManage = true;
            if (!m_dicTypeManagesIdUniversel.TryGetValue(tp, out bManage))
            {
                bManage = tp.GetCustomAttributes(typeof(NoIdUniverselAttribute), true).Length == 0;
                m_dicTypeManagesIdUniversel[tp] = bManage;
            }
            return bManage;
        }

        //-----------------------------------------------------------
        public virtual CDbKey DbKey
        {
            get
            {
                if ( ManageIdUniversel )
                    return CDbKeyAddOn.CreateCDbKey(this);
                return null;
            }
        }

        





        //-----------------------------------------------------------
        /// <summary>
        /// Id unique universel quelque soit la machine
        /// </summary>
        [TableFieldProperty(c_champIdUniversel, 64)]
        [DynamicField("Universal id")]
        [IndexField]
        [NonCloneable]
        public string IdUniversel
        {
            get
            {
                if ( ManageIdUniversel )
                    return (string)Row[c_champIdUniversel];
                return "";
            }
        }

        //-----------------------------------------------------------
        public bool ReadIfExistsUniversalId(string strId)
        {
            if (ManageIdUniversel)
            {
                CFiltreData filtre = new CFiltreData(c_champIdUniversel + "=@1",
                    strId);
                return ReadIfExists(filtre);
            }
            return false;
        }

        //-----------------------------------------------------------
        public bool ReadIfExists(CDbKey key)
        {
            if (key != null)
                return key.ReadIfExists(this);
            return false;
        }


        /////////////////////////////////////////////////////////////////
        ///Charge l'élement uniquement s'il existe.
        ///s'il n'existe pas, aucune ligne n'est créée
        public bool ReadIfExists(object[] valeursCle, bool bAutoriseLectureInDb)
        {
            DataRow row = Table.Rows.Find(valeursCle);
            bool bGestionParTableComplete = typeof(IObjetALectureTableComplete).IsAssignableFrom(GetType()) && ContexteDonnee.GestionParTablesCompletes;
            
            if (row != null &&
                (m_contexte.IdVersionDeTravail == null || !m_contexte.IsToRead(row)))
            {
                SetRow(row);
                return true;
            }
            else if (bAutoriseLectureInDb && !bGestionParTableComplete)
            {
                //Cherche la ligne supprimée
                string strFiltre = GetFiltreClesString(valeursCle);
                DataRow[] rows = Table.Select(strFiltre, "", DataViewRowState.Deleted);
                if (rows.Length > 0)
                {
                    SetRow(rows[0]);
                    return true;
                }
                else
                {
                    row = m_contexte.Read(GetNomTable(), GetChampsId(), valeursCle, true);
                    if (row == null)
                        return false;
                    m_contexte.SetIsFromDb(row, true);
                    m_contexte.SetIsToRead(row, false);


                    //SC 1510 :
                    /*On appelle AfterRead et AcceptChanges seulement si on on a lu de la base
                     * car dans le cas contraire, si on n'a pas lu de la base et que la ligne
                     * avait été changée, on appelle accept changes dessus et elle n'est plus 
                     * conforme à ce qu'il y a dans la base
                     * */
                    SetRow(row);

                    AfterRead();
                    m_row.AcceptChanges();
                    return true;
                }

            }
            return false;
        }


        /// <summary>
        /// Remplit l'élément avec les données du premier élément trouvé dans la liste
        /// dont le filtre correspond au filtre passé en paramètre.
        /// Si la fonction retourne FAUX, l'élément n'a pas été chargé.
        /// </summary>
        /// <param name="filtre"></param>
        /// <returns></returns>
        public bool ReadIfExists(CFiltreData filtre, bool bAutoriseLectureDansBase)
        {
            CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonnee, GetType());
            liste.Filtre = filtre;
            bool bGestionParTableComplete = false;
            liste.PreserveChanges = true;
            if (!(filtre is CFiltreDataAvance))//Si c'est un filtre avancé, on ne peut chercher que dans la base !
            //Cherche d'abord sans lire la base
            {
                liste.InterditLectureInDB = true; ;
                bGestionParTableComplete = typeof(IObjetALectureTableComplete).IsAssignableFrom(GetType()) && ContexteDonnee.GestionParTablesCompletes;
            }
            if (liste.Count == 0)
            {
                if (filtre is CFiltreDataAvance)
                    return false;
                if (bAutoriseLectureDansBase && !bGestionParTableComplete)
                {
                    liste.InterditLectureInDB = false;
                    liste.Refresh();
                }
            }
            if (liste.Count > 0)
            {
                //Stef 2/5/2008  remplacé m_row= par SetRow
                m_row = ((CObjetDonnee)liste[0]).Row;
                //SetRow(((CObjetDonnee)liste[0]).Row);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remplit l'élément avec les données du premier élément trouvé dans la liste
        /// dont le filtre correspond au filtre passé en paramètre.
        /// Si la fonction retourne FAUX, l'élément n'a pas été chargé.
        /// </summary>
        /// <param name="filtre"></param>
        /// <returns></returns>
        public bool ReadIfExists(CFiltreData filtre)
        {
            return ReadIfExists(filtre, true);
        }


        /////////////////////////////////////////////////////////////////
        public bool IsFromDb
        {
            get
            {
                AssureRow();
                return m_contexte.IsFromDb(Row);
            }
        }

        /////////////////////////////////////////////
        public bool IsDependanceChargee(string strNomTable, params string[] strChampsFille)
        {
            string strForeignKeyName = ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTable, strChampsFille);
            return IsDependanceChargee(strForeignKeyName);

        }

        /////////////////////////////////////////////
        public bool IsDependanceChargee(string strForeignKeyName)
        {
            DataTable table = ContexteDonnee.GetTableSafe(GetNomTable());
            if (table.Columns.Contains(strForeignKeyName) &&
                Row[strForeignKeyName] != DBNull.Value && (bool)Row[strForeignKeyName])
                return true;//Les filles sont déjà chargées
            return false;
        }

        /////////////////////////////////////////////
        public void RefreshDependances()
        {
            ContexteDonnee.RefreshDependances(Row.Row);
        }

        /////////////////////////////////////////////
        /// <summary>
        /// Retourne vrai s'il existe des dépendances pour l'élément dans la table demandée
        /// </summary>
        /// <param name="strNomTable"></param>
        /// <param name="strChampsFille"></param>
        /// <returns></returns>
        public bool HasDependances(string strNomTable, string[] strChampsFille)
        {
            string strForeignKeyName = ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTable, strChampsFille);
            DataTable tableFille = ContexteDonnee.GetTableSafe(strNomTable);

            //Regarde s'il y a des éléments dans la table fille
            List<DataColumn> lst = new List<DataColumn>();
            foreach (string strCol in strChampsFille)
                lst.Add(tableFille.Columns[strCol]);
            DataRow[] rows = tableFille.Select(new CFormatteurFiltreDataToStringDataTable().GetString(CFiltreData.CreateFiltreAndSurValeurs(lst.ToArray(), GetValeursCles())));
            if (rows.Length > 0)
                return true;
            if (IsDependanceChargee(strNomTable, strChampsFille) && rows.Length == 0)
                return false;

            if (IsDependanceChargee(strForeignKeyName))
                return GetDependancesListe(strNomTable, strChampsFille).Count > 0;
            //La dépendance n'est pas chargée, va lire dans la base
            CListeObjetsDonnees lstTmp = new CListeObjetsDonnees(ContexteDonnee, CContexteDonnee.GetTypeForTable(strNomTable));
            lstTmp.Filtre = CFiltreData.CreateFiltreAndSurValeurs(lst.ToArray(), GetValeursCles());
            return lstTmp.CountNoLoad > 0;
        }

        /////////////////////////////////////////////
        public void AssureDependances(string strNomTable, string[] strChampsFille)
        {
            DataTable table = ContexteDonnee.GetTableSafe(GetNomTable());
            string strForeignKeyName = ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTable, strChampsFille);
            if (IsDependanceChargee(strNomTable, strChampsFille))
                return;
            //Stef 14/08/08 : si l'objet est nouveau, ces dépendances sont forcement chargées!
            if (Row.RowState == DataRowState.Added)
            {
                ContexteDonnee.GetTableSafe(strNomTable);//Les dépendances sont chargées,oui, mais il faut bien s'assurer que la table est là
                CContexteDonnee.ChangeRowSansDetectionModification(Row, ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTable, strChampsFille), true);
                return;
            }
            table = ContexteDonnee.GetTableSafe(strNomTable);
            foreach (DataRelation relation in table.ParentRelations)
            {
                if (relation.RelationName == strForeignKeyName)
                {
                    //Stef 13/09/2005 : Pourquoi ne pas utiliser une liste, ça permet de benéficier tout seul
                    //des optims faites sur les listes !!!
                    //Stef 14/9/2005 : Parce que le chargement d'une liste est plus long !!!
                    if (ContexteDonnee.GestionParTablesCompletes && typeof(IObjetALectureTableComplete).IsAssignableFrom(CContexteDonnee.GetTypeForTable(strNomTable)))
                        ContexteDonnee.AssureTableCompleteSiObjetATableComplete(strNomTable);
                    else
                    {

                        CFiltreData filtre = new CFiltreData();
                        filtre.Filtre = "";
                        int nIndex = 1;
                        foreach (DataColumn col in relation.ChildColumns)
                        {
                            filtre.Filtre += col.ColumnName + "=@" + nIndex + " and ";
                            filtre.Parametres.Add(Row[relation.ParentColumns[nIndex - 1]]);
                            nIndex++;
                        }

                        filtre.Filtre = filtre.Filtre.Substring(0, filtre.Filtre.Length - 5);
                        IObjetServeur loader = ContexteDonnee.GetTableLoader(strNomTable);
                        try
                        {
                            table = loader.Read(filtre);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        ContexteDonnee.IntegreTable(table, true);
                        table.Dispose();
                    }
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTable, strChampsFille), true);
                    //Row[ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTable, strChampsFille)] = true;
                    break;
                }
            }
        }


        /////////////////////////////////////////////
        public DataView GetDependancesView(string strNomTableFille, string[] strChampsFille)
        {
            AssureDependances(strNomTableFille, strChampsFille);
            DataView view = new DataView(ContexteDonnee.Tables[strNomTableFille]);
            string strNomRelation = ContexteDonnee.GetForeignKeyName(GetNomTable(), strNomTableFille, strChampsFille);
            DataRelation rel = Row.Table.ChildRelations[strNomRelation];
            if (rel == null)
                throw new Exception(I.T("The relation @1 doesn't exist (GetDependancesView)|150", strNomRelation));
            object[] valeurs = new object[rel.ParentColumns.Length];
            string[] strChampsFilleDansOrdre = new string[rel.ChildColumns.Length];
            int nIndex = 0;
            foreach (DataColumn col in rel.ParentColumns)
            {
                valeurs[nIndex] = Row[col];
                strChampsFilleDansOrdre[nIndex] = rel.ChildColumns[nIndex].ColumnName;
            }
            CFiltreData filtre = CFiltreData.CreateFiltreAndSurValeurs(strChampsFilleDansOrdre, valeurs);
            view.RowFilter = new CFormatteurFiltreDataToStringDataTable().GetString(filtre);
            return view;
        }

        /////////////////////////////////////////////
        public CListeObjetsDonneesContenus GetDependancesListe(string strNomTableFille, params string[] strChampsFille)
        {
            return GetDependancesListe(strNomTableFille, true, strChampsFille);
        }

        /////////////////////////////////////////////
        public CListeObjetsDonneesContenus GetDependancesListe(string strNomTableFille, bool bAppliquerFiltreStandardFils, params string[] strChampsFille)
        {
            return new CListeObjetsDonneesContenus(this, strNomTableFille, strChampsFille, bAppliquerFiltreStandardFils, false);
        }

        /////////////////////////////////////////////
        public CListeObjetsDonneesContenus GetDependancesListeProgressive(string strNomTableFille, params string[] strChampsFille)
        {
            return new CListeObjetsDonneesContenus(this, strNomTableFille, strChampsFille, true, true);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne le même objet, mais dans un contexte différent
        /// </summary>
        /// <param name="newContexte"></param>
        /// <returns></returns>
        public CObjetDonnee GetObjetInContexte(CContexteDonnee newContexte)
        {
            CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(GetType(), newContexte);
            if (objet.ReadIfExists(GetValeursCles()))
                return objet;
            return null;
        }

        ////////////////////////////////////////////////////////////
        public void AssureExiste(CObjetDonnee objet)
        {
            if (objet == null)
                return;
            if (objet.ContexteDonnee == ContexteDonnee)
                return;
            DataTable table = ContexteDonnee.GetTableSafe(objet.Table.TableName);
            //Surtout ne pas copier la ligne car sinon, les colonnes indiquant si
            //les dépendances sont chargées seraient également recopiées or il ne faut pas !
            CObjetDonnee newObjet = ContexteDonnee.GetNewObjetForTable(table);
            newObjet.Table.RowChanging += new DataRowChangeEventHandler(OnChangeObjetAssure);
            newObjet.PointeSurLigne(objet.GetValeursCles());
        }

        /////////////////////////////////////////////
        private string GetParentKey(string[] strChamps)
        {
            string strRetour = "";
            foreach (string strChamp in strChamps)
                strRetour += strChamp + "_";
            return strRetour;
        }

        /////////////////////////////////////////////
        public CObjetDonnee GetParent(string[] strChamps, Type typeParent)
        {
            CObjetDonnee objet = null;
            object[] cles = new object[strChamps.Length];
            bool bHasNull = false;
            for (int n = 0; n < strChamps.Length; n++)
            {
                cles[n] = Row[strChamps[n]];
                if (cles[n] == DBNull.Value)
                    bHasNull = true;
            }
            if (bHasNull)
                return null;
#if PDA
			objet = (CObjetDonnee)Activator.CreateInstance ( typeParent );
			objet.ContexteDonnee = ContexteDonnee;
#else
            objet = (CObjetDonnee)Activator.CreateInstance(typeParent, new object[] { ContexteDonnee });
#endif
            objet.PointeSurLigne(cles);
            return objet;
        }

        /////////////////////////////////////////////
        public void SetParent(string[] strChamps, CObjetDonnee value)
        {
            if (value == null)
            {
                foreach (string strChamp in strChamps)
                    Row[strChamp] = DBNull.Value;
                return;
            }
            AssureExiste(value);
            object[] cles = value.GetValeursCles();
            if (cles.Length != strChamps.Length)
            {
                throw new Exception(I.T("Attempt to define a parent with a different number of foreign keys from the parent table|151"));
            }
            bool bOldEnforce = ContexteDonnee.EnforceConstraints;
            if (cles.Length > 1)//S'il n'y a qu'une seule clé, pas besoin de désactiver les contraintes
                ContexteDonnee.EnforceConstraints = false;
            for (int n = 0; n < cles.Length; n++)
                Row[strChamps[n]] = cles[n];
            if (cles.Length > 1)
                ContexteDonnee.EnforceConstraints = bOldEnforce;

        }

        ////////////////////////////////////////////////////////////
        public void OnChangeObjetAssure(object sender, DataRowChangeEventArgs args)
        {
        }

        /////////////////////////////////////////////
        ///Annule la création d'un élément
        public void CancelCreate()
        {
            if (m_row == null || m_row.RowState != DataRowState.Added)
                return;
            m_contexte.DeleteFillesEnCascade(m_row, true);
            m_row.Table.Rows.Remove(m_row);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Retourne true si l'élément peut être supprimé
        /// </summary>
        /// <returns></returns>
        public virtual CResultAErreur CanDelete()
        {
            CResultAErreur result = CResultAErreur.True;
            if (Row.RowState == DataRowState.Deleted)
                return result;
            //Vérifie qu'il n'y a pas de dépendances
            foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(GetNomTable()))
            {
                if (relation.TableParente == GetNomTable())
                {
                    if (!relation.Composition)
                    {
                        if (!relation.PasserLesFilsANullLorsDeLaSuppression)//sinon, on s'en fiche !
                        {
                            /*
                             * Si ce n'est pas une composition, il ne doit pas
                             * y avoir de fils dans cette relation pour pouvoir supprimer
                             */
                            IObjetServeur objServeur = ContexteDonnee.GetTableLoader(relation.TableFille);
                            CFiltreData filtre = CFiltreData.CreateFiltreAndSurValeurs(relation.ChampsFille, GetValeursCles());
                            int nNb = objServeur.CountRecords(relation.TableFille, filtre);
                            if (nNb != 0)
                            {
                                string strNomConvivial = "";
                                Type tp = CContexteDonnee.GetTypeForTable(relation.TableFille);
                                if (tp != null)
                                    strNomConvivial = I.T(" of type @1 |155", DynamicClassAttribute.GetNomConvivial(tp));
                                else
                                    strNomConvivial = I.T("in table @1|156", relation.TableFille);

                                string strTexte = I.T("The element @1 contains @2 |152", DynamicClassAttribute.GetNomConvivial(GetType()), nNb.ToString());
                                if (nNb > 1)
                                    strTexte += I.T("bound elements|153");
                                else
                                    strTexte += I.T("bound element|154");
                                strTexte += strNomConvivial;
                                result.EmpileErreur(strTexte);
                            }
                        }
                    }
                    else
                    /*
                     * Si c'est une compositions, les fils devront être supprimés,
                     * il faut que ce soit possible !! */
                    {
                        CListeObjetsDonnees liste = GetDependancesListe(relation.TableFille, relation.ChampsFille);
                        foreach (CObjetDonnee objet in liste)
                        {
                            result = objet.CanDelete();
                            if (!result)
                                return result;
                        }
                    }
                }
            }
            return result;
        }

        public virtual CResultAErreur DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison()
        {
            return DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(false);
        }

        /////////////////////////////////////////////////////////////////
        /// <summary>
        /// Effectue la suppression de l'objet. Cette fonction ne doit pas être
        /// appellée en std, il faut utiliser Delete !!!
        /// </summary>
        /// <returns></returns>
        public virtual CResultAErreur DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(bool bDansContexteCourant)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                foreach (CInfoRelation infoRelation in CContexteDonnee.GetListeRelationsTable(GetNomTable()))
                {
                    if (infoRelation.PasserLesFilsANullLorsDeLaSuppression && infoRelation.TableParente == GetNomTable())
                    {
                        CListeObjetsDonnees lst = GetDependancesListe(infoRelation.TableFille, infoRelation.ChampsFille);
                        foreach (CObjetDonnee objet in lst.ToArrayList())
                        {
                            objet.Row.Row.BeginEdit();
                            foreach (string strChampFille in infoRelation.ChampsFille)
                                objet.Row[strChampFille] = DBNull.Value;
                            objet.Row.Row.EndEdit();
                        }
                    }
                }
                if (m_row != null)
                {
                    if (m_row.RowState != DataRowState.Deleted && m_row.RowState != DataRowState.Detached)
                        m_row.Sc2iDelete();
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

        public virtual CResultAErreur DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi()
        {
            return DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(false);
        }

        /////////////////////////////////////////////////////////////////
        /// <summary>
        /// Supprime l'objet et TOUTES SES DEPENDANCES
        /// </summary>
        /// <returns></returns>
        public virtual CResultAErreur DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(bool bDansContexteCourant)
        {
            CResultAErreur result = CResultAErreur.True;
            //Vérifie qu'il n'y a pas de dépendances
            foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(GetNomTable()))
            {
                if (relation.TableParente == GetNomTable())
                {
                    CListeObjetsDonnees liste = GetDependancesListe(relation.TableFille, relation.ChampsFille);
                    foreach (CObjetDonnee objet in liste)
                    {
                        result = objet.DeleteAvecCascadeSansControleDoncIlFautEtreSurDeSoi(bDansContexteCourant);
                        if (!result)
                            return result;
                    }
                }
            }
            Row.Row.Sc2iDelete();
            return result;
        }

        /////////////////////////////////////////////////////////////////
        public CResultAErreur Delete()
        {
            return Delete(false);
        }

        /////////////////////////////////////////////////////////////////
        public virtual CResultAErreur Delete(bool bDansContexteCourant)
        {
            CResultAErreur result = CanDelete();
            if (!result)
                return result;
            bool bEditAuto = !(ContexteDonnee.IsEnEdition) && !ContexteDonnee.IsModeDeconnecte && !bDansContexteCourant;
            if (bEditAuto)
                BeginEdit();
            try
            {
                if (m_row == null)
                    return result;
                result = DoDeleteInterneACObjetDonneeNePasUtiliserSansBonneRaison(bDansContexteCourant);
                if (result)
                {
                    if (bEditAuto)
                    {
                        result = CommitEdit();
                    }
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Delete error|157"));
            }
            finally
            {
                if (!result && bEditAuto)
                    CancelEdit();
            }
            return result;
        }

        /////////////////////////////////////////////////////////////////
        public static VerifieDonneesDelegate VerifieDonneesSuppl = null;

        /////////////////////////////////////////////////////////////////
        [DynamicMethod("Check if data are ok", "true to test now, false to test during data saving")]
        public CResultAErreur CheckData(bool bNow)
        {
            return VerifieDonnees(!bNow);
        }

        /////////////////////////////////////////////////////////////////
        public virtual CResultAErreur VerifieDonnees(bool bAuMomentDeLaSauvegarde)
        {
            CResultAErreur result = CResultAErreur.True;
            if (bAuMomentDeLaSauvegarde && (Row.RowState == DataRowState.Modified || Row.RowState == DataRowState.Added))
            {
                if (Row.RowState != DataRowState.Deleted &&
                    Row.RowState != DataRowState.Detached)
                    Row[c_champVerifierDonneesALaSauvegarde] = true;
                return result;
            }
            using (CValiseObjetDonnee valise = new CValiseObjetDonnee(this))
            {
                result = GetLoader().VerifieDonnees(valise);
            }
            if (VerifieDonneesSuppl != null)
            {
                CResultAErreur resTmp = VerifieDonneesSuppl(this);
                if (!resTmp)
                {
                    result.Erreur.EmpileErreurs(resTmp.Erreur);
                    result.Result = false;
                }
            }
            return result;
        }

        /////////////////////////////////////////////////////////////////
        public CObjetDonnee GetCloneIsole()
        {
            return new CValiseObjetDonnee(this).GetObjet();
        }

        /////////////////////////////////////////////////////////////////
        public virtual void BeginEdit()
        {
            CContexteDonnee ctxEdition = CContexteDonnee.GetContexteEditionFor(this);
            m_contexte = ctxEdition;
            CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(GetChampsId(), Row);
            DataRow row = ctxEdition.Tables[GetNomTable()].Rows.Find(GetValeursCles());
            SetRow(row);
        }

        /////////////////////////////////////////////////////////////////
        public virtual CResultAErreur CommitEdit()
        {
            if (!m_contexte.IsEnEdition)
                throw new Exception(I.T("CommitEdit call for an element which is not on edition|158"));
            CResultAErreur result = ContexteDonnee.CommitEdit();
            if (result)
            {
                CContexteDonnee ctxEdition = m_contexte;
                m_contexte = ContexteDonnee.ContextePrincipal;
                ctxEdition.Dispose();
                if (m_row != null && m_row.RowState != DataRowState.Deleted && m_row.RowState != DataRowState.Detached)
                {
                    CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(GetChampsId(), Row);
                    DataRow row = m_contexte.Tables[GetNomTable()].Rows.Find(GetValeursCles());
                    SetRow(row);
                }
                else
                    SetRow(null);
            }
            return result;
        }

        /////////////////////////////////////////////////////////////////
        public void CancelEdit()
        {
            if (!m_contexte.IsEnEdition)
                throw new Exception(I.T("CancelEdit call for an element which is not on edition|159"));
            ContexteDonnee.CancelEdit();
            CContexteDonnee ctxEdition = m_contexte;
            m_contexte = ContexteDonnee.ContextePrincipal;
            ctxEdition.Dispose();
            DataRow rowTrouvee = null;

            if ((Row.Row.RowState & (DataRowState.Detached | DataRowState.Deleted)) == 0)
                rowTrouvee = m_contexte.Tables[GetNomTable()].Rows.Find(GetValeursCles());
            SetRow(rowTrouvee);
        }

        /////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            string str = GetType().ToString() + "_" + GetValeursCles().ToString();
            return str.GetHashCode();
        }

        /////////////////////////////////////////////////////////////////
        public override bool Equals(object obj)
        {
            if (!(obj is CObjetDonnee))
                return false;
            return this == (CObjetDonnee)obj;
        }

        /////////////////////////////////////////////////////////////////
        public static bool operator ==(CObjetDonnee obj1, CObjetDonnee obj2)
        {
            if (((object)obj1) == null && ((object)obj2) == null)
                return true;
            if (((object)obj1) == null || ((object)obj2) == null)
                return false;
            if (obj1.GetType() != obj2.GetType())
                return false;
            object[] cles1, cles2;
            cles1 = obj1.GetValeursCles();
            cles2 = obj2.GetValeursCles();
            for (int nCle = 0; nCle < cles1.Length; nCle++)
            {
                if (!cles1[nCle].Equals(cles2[nCle]))
                    return false;
            }
            return true;
        }

        /////////////////////////////////////////////////////////////////
        public static bool operator !=(CObjetDonnee obj1, CObjetDonnee obj2)
        {
            return !(obj1 == obj2);
        }

        /////////////////////////////////////////////////////////////////
        [DynamicField("Element description", c_categorieChampSystème)]
        public string NonVirtualDescription
        {
            get
            {
                return DescriptionElement;
            }
        }

        /////////////////////////////////////////////////////////////////
        [DynamicField("Type", c_categorieChampSystème)]
        public string TypeString
        {
            get
            {
                return GetType().ToString();
            }
        }

        /////////////////////////////////////////////////////////////////
        public abstract string DescriptionElement
        { get; }

        /////////////////////////////////////////////////////////////////
        public virtual void InvalideData()
        {
            ContexteDonnee.InvalideRowEtCompositions(Row);
        }

        /////////////////////////////////////////////////////////////////
        public CObjetDonnee Clone(bool bEnEdition)
        {
            return Clone(ContexteDonnee, bEnEdition);
        }



        /////////////////////////////////////////////////////////////////
        protected virtual CObjetDonnee Clone(CContexteDonnee contexteDestination, bool bEnEdition)
        {

            object[] attributes = GetType().GetCustomAttributes(typeof(NonClonableObjectAttribute), false);
            if (attributes.Length > 0)
                return null;


            CObjetDonnee objetClone = (CObjetDonnee)Activator.CreateInstance(GetType(), new object[] { contexteDestination });
            if (bEnEdition)
                objetClone.CreateNew();
            else
                objetClone.CreateNewInCurrentContexte(null);


            objetClone.AssureRow();
            ArrayList lst = new ArrayList();
            foreach (string strChamp in GetChampsId())
                lst.Add(strChamp);
            lst.Add(CContexteDonnee.c_colLocalKey);

            //Exclue les champs non clonables

            CStructureTable structure = CStructureTable.GetStructure(GetType());

            //Exclue les champs marqués comme non clonables
            foreach (CInfoChampTable info in structure.Champs)
            {
                PropertyInfo prop = GetType().GetProperty(info.Propriete);
                if (prop != null && prop.GetCustomAttributes(typeof(NonCloneableAttribute), true).Length != 0)
                    lst.Add(info.NomChamp);
            }

            foreach (CInfoRelation infoRelation in structure.RelationsParentes)
            {
                PropertyInfo prop = GetType().GetProperty(infoRelation.Propriete);
                if (prop != null && prop.GetCustomAttributes(typeof(NonCloneableAttribute), true).Length != 0)
                {
                    foreach (string strChamp in infoRelation.ChampsFille)
                        lst.Add(strChamp);
                }
            }






            ContexteDonnee.CopyRow(Row, objetClone.Row, (string[])lst.ToArray(typeof(string)));

            objetClone.Row[CContexteDonnee.c_colIdContexteCreation] = objetClone.ContexteDonnee.IdContexteDonnee;

            //Gère les blobs
            foreach (CInfoChampTable info in structure.Champs)
                if (info.TypeDonnee == typeof(CDonneeBinaireInRow))
                {
                    object obj = objetClone.Row[info.NomChamp];
                    if (obj == DBNull.Value)
                    {
                        //Appelle la propriété
                        PropertyInfo propBlob = GetType().GetProperty(info.Propriete);
                        MethodInfo methBlob = propBlob != null ? propBlob.GetGetMethod() : null;
                        if (methBlob != null)
                            obj = methBlob.Invoke(this, new object[0]);
                    }
                    if (obj != DBNull.Value && obj != null)
                    {
                        CDonneeBinaireInRow bin = (CDonneeBinaireInRow)obj;
                        /*CDonneeBinaireInRow newBin = new CDonneeBinaireInRow ( contexteDestination.IdSession,
                                objetClone.Row,
                                info.NomChamp );*/
                        CDonneeBinaireInRow newBin = bin.GetCloneForRow(objetClone.Row);
                        objetClone.Row[info.NomChamp] = newBin;
                    }
                }

            Hashtable tableRelationsFilles = new Hashtable();
            ArrayList lstFilles = new ArrayList();
            foreach (CInfoRelation relation in structure.RelationsFilles)
            {
                lstFilles.Add(relation);
                tableRelationsFilles[relation] = true;
            }

            //Crée une copie de tous les objets composition
            foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(GetNomTable()))
            {
                if (tableRelationsFilles[relation] == null)
                    lstFilles.Add(relation);
            }

            foreach (CInfoRelation relation in lstFilles)
            {
                if (relation.TableParente == GetNomTable() && relation.Composition && !relation.NePasClonerLesFils)
                //C'est une dépendance fille et composition
                {
                    PropertyInfo prop = GetType().GetProperty(relation.Propriete);
                    if (prop == null || prop.GetCustomAttributes(typeof(NonCloneableAttribute), true).Length == 0)
                    {
                        CListeObjetsDonnees listeFils = GetDependancesListe(relation.TableFille, relation.ChampsFille);
                        foreach (CObjetDonnee objet in listeFils)
                        {
                            CObjetDonnee filsClone = objet.Clone(objetClone.ContexteDonnee, false);
                            filsClone.SetValeursMultiples(relation.ChampsFille, objetClone.GetValeursCles());
                        }
                    }
                }
            }
            return objetClone;
        }

        //--------------------------------------------------------------
        /// <summary>
        /// Met à jour les données de l'objet
        /// </summary>
        [DynamicMethod(typeof(bool), "Updates the object from the database")]
        public virtual bool Refresh()
        {
            if (ContexteDonnee.IsToRead(m_row))
                return true;
            ContexteDonnee.SetIsToRead(Row.Row, true);
            IObjetServeur objServeur = GetLoader();
            if (objServeur != null)
                objServeur.ClearCache();
            DataTable table = ContexteDonnee.GetTableSafe(GetNomTable());
            //foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(GetNomTable()))
            foreach (DataRelation relation in table.ChildRelations)
            {
                DataTable tableFille = relation.ChildTable;

                if ((bool)relation.ExtendedProperties[CContexteDonnee.c_extPropRelationComposition])
                {
                    string strRelation = ContexteDonnee.GetForeignKeyName(relation);
                    if (table.Columns[strRelation] != null)
                    {
                        bool bRel = (bool)Row.Row[strRelation];
                        if (bRel)
                        {
                            ContexteDonnee.InvalideCacheDependance(Row.Row, relation);
                            string[] strChampsFille = (from r in relation.ChildColumns select r.ColumnName).ToArray();
                            CListeObjetsDonnees lst = GetDependancesListe(relation.ChildTable.TableName, strChampsFille);
                            lst.Refresh();
                            foreach (CObjetDonnee objet in new ArrayList(lst))
                                objet.Refresh();
                        }
                    }
                }
            }
            foreach (RelationTypeIdAttribute relTypeId in CContexteDonnee.RelationsTypeIds)
            {
                string strNomCol = relTypeId.GetNomColDepLue();
                if (Table.Columns.Contains(strNomCol))
                    ContexteDonnee.InvalideCacheDependance(Row, strNomCol);
            }
            return true;
        }

        /// <summary>
        /// Force le changement d'id de synchro d'un objet
        /// </summary>
        /// <param name="objet"></param>
        public void ForceChangementSyncSession()
        {
            DataColumn col = Row.Table.Columns[CSc2iDataConst.c_champIdSynchro];
            if (col != null && Row.RowState == DataRowState.Unchanged)
            {
                if (Row[col] == DBNull.Value)
                    Row[col] = 1;
                else
                    Row[col] = ((int)Row[col]) + 1;
            }
        }


        /// <summary>
        /// Retourne une valeur bidon correspondant à un champ masqué
        /// </summary>
        public string ValeurMasquee
        {
            get
            {
                return "*****";
            }
        }

        //-----------------------------------------------------------------------
        public CResultAErreur EnregistreEvenement(string strIdEvenement, bool bDeclenchementImmediatSiPossible)
        {
            CResultAErreur result = CResultAErreur.True;
            bool bEnEdition = ContexteDonnee.IsEnEdition;
            CObjetDonnee objet = this;
            if (!bEnEdition && bDeclenchementImmediatSiPossible)
                objet.BeginEdit();
            EvenementAttribute.StockeDeclenchement(objet, strIdEvenement);
            if (!bEnEdition && bDeclenchementImmediatSiPossible)
                result = objet.CommitEdit();
            return result;
        }

        public void AttacheObjetsAContexteEvaluation(CContexteEvaluationExpression ctx)
        {
            ctx.AttacheObjet(typeof(CContexteDonnee), ContexteDonnee);
        }

        //-----------------------------------------------------------------------
        [DynamicMethod("Free unused memory for this object")]
        public void FreeUnusedMemory()
        {
            if (ContexteDonnee.IsToRead(m_row))
                return;
            foreach (DataColumn col in Table.Columns)
            {
                if (col.DataType == typeof(CDonneeBinaireInRow))
                {
                    CDonneeBinaireInRow data = m_row[col] as CDonneeBinaireInRow;
                    if (!data.HasChange())
                    {
                        data.Donnees = null;
                        m_row[col] = DBNull.Value;
                    }
                }
            }
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Indique dans quel contexte l'entité a été modifiée.
        /// </summary>
        /// <remarks>
        /// Cette propriété peut être utilisée par les administrateurs pour stocker sur un objet
        /// un contexte (sous forme de texte) de modification.<BR></BR>
        /// Ce contexte peut être exploité dans les évenements pour conditionner le déclenchement de l'évenement
        /// dans certains contextes seulement.
        /// </remarks>
        [DynamicField("Edition context", CObjetDonnee.c_categorieChampSystème)]
        public string ContexteDeModification
        {
            get
            {
                string strVal = "";
                if (Row.RowState == DataRowState.Deleted)
                    strVal = (string)Row[c_champContexteModification, DataRowVersion.Original];

                strVal = (string)Row[c_champContexteModification];
                if (strVal.Length == 0)
                    return ContexteDonnee.ContexteModification;
                return strVal;
            }
            set
            {
                Row.Row[c_champContexteModification] = value;
            }
        }

        //-----------------------------------------------------------------------
        public static string GetMessageAccesConccurentiel(DataRow row)
        {
            if (row != null)
            {
                try
                {
                    Type tp = CContexteDonnee.GetTypeForTable(row.Table.TableName);
                    if (tp != null)
                    {
                        CObjetDonnee objet = Activator.CreateInstance(tp, new object[] { row }) as CObjetDonnee;
                        if (objet != null)
                            return objet.ProtectedGetMessageAccesConccurentiel();
                    }
                }
                catch { }
            }
            return I.T("Another user has modified the data|20002");
        }

        //-----------------------------------------------------------------------
        protected virtual string ProtectedGetMessageAccesConccurentiel()
        {
            return I.T("Another user has modified the data|20002");
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Stocke un objet serializable dans un blob
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBinaire"></param>
        /// <returns></returns>
        protected bool DecodeBlob<T>( CDonneeBinaireInRow dataBinaire, ref T objet )
            where T : I2iSerializable
        {
            return DecodeBlob<T>(dataBinaire, ref objet, null);
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Stocke un objet serializable dans un blob
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBinaire"></param>
        /// <returns></returns>
        protected bool DecodeBlob<T>( CDonneeBinaireInRow dataBinaire, ref T retour, string strChampCache )
            where T : I2iSerializable
        {
            bool bUseCache = strChampCache != null && strChampCache.Length != 0 && Row.Table.Columns.Contains(strChampCache);
            if ( bUseCache )
            {
                if ( Row[strChampCache] is T)
                retour = (T)Row[strChampCache];
                if (retour != null)
                    return true;
            }
            if (dataBinaire.Donnees != null)
            {
                MemoryStream stream = new MemoryStream(dataBinaire.Donnees);
                BinaryReader reader = new BinaryReader(stream);
                CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                serializer.AttacheObjet(typeof(CContexteDonnee), ContexteDonnee);
                I2iSerializable objet = null;
                CResultAErreur result = serializer.TraiteObject(ref objet);
                reader.Close();
                stream.Close();
                reader.Dispose();
                stream.Dispose();
                if (result)
                {
                    retour = (T)objet;
                }
                if (bUseCache)
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, strChampCache, retour);
                return result.Result;
            }
            return false;
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Stocke un objet serializable dans un blob
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBinaire"></param>
        /// <returns></returns>
        protected bool EncodeBlob<T> ( 
            CDonneeBinaireInRow dataToUpdate,
            T objetToSerialize)
            where T : I2iSerializable
        { 
            return EncodeBlob<T>(dataToUpdate, objetToSerialize, null);
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Stocke un objet serializable dans un blob
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBinaire"></param>
        /// <returns></returns>
        protected bool EncodeBlob<T>(
            CDonneeBinaireInRow dataToUpdate,
            T objetToSerialize, 
            string strChampCache)
            where T : I2iSerializable
        {
            if (strChampCache != null && strChampCache.Length > 0 && Row.Table.Columns.Contains(strChampCache))
                Row[strChampCache] = DBNull.Value;
            if (objetToSerialize == null)
                dataToUpdate.Donnees = null;
            else
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);
				CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
				I2iSerializable objet = objetToSerialize;
				CResultAErreur result = serializer.TraiteObject ( ref objet );
                if (result)
                {
                    dataToUpdate.Donnees = stream.GetBuffer();
                }
                stream.Close();
                writer.Close();
                stream.Dispose();
                writer.Dispose();
                return result.Result;
            }
            return true;
        }

    }
}
