using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;

namespace sc2i.common.memorydb
{
    [Serializable]
    public class CMemoryDb : DataSet, I2iSerializable, ISerializable
    {
        [NonSerialized]
        private Dictionary<Type, List<IFournisseurElementsManquantsPourMemoryDb>> m_fournisseursSupplementairesElements = new Dictionary<Type, List<IFournisseurElementsManquantsPourMemoryDb>>();

        private const string c_ExtPropRelationNomProprieteFils="__CHILD_PROPERTY";
        
        /// <summary>
        /// champ indiquant que la lecture d'un objet n'a pas été faite
        /// </summary>
        public const string c_champIsToRead = "__IS_TO_READ";

        /// <summary>
        /// Indique qu'on est en train de faire une lecture complète, 
        /// la serialisation n'a donc pas besoin de gérer
        /// des ReadIfExist et autres.
        /// </summary>
        private bool m_bIsFullReadEnCours = false;

        private Dictionary<string, Type> m_dicPrivateNomTableToType = new Dictionary<string,Type>();
        private Dictionary<Type, string> m_dicPrivateTypeToNomTable = new Dictionary<Type,string>();

        ////////////////////////////////////////////////////////////////////////////
        public CMemoryDb()
            :base()
        {
        }


        ////////////////////////////////////////////////////////////////////////////
        public CMemoryDb( System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext contexte)
        {
            AdoNetHelper.DeserializeDataSet(this, (byte[])info.GetValue("DATASET_DATA", typeof(byte[])));
            m_dicPrivateNomTableToType = info.GetValue("DIC_TABLE_TO_TYPE", typeof(Dictionary<string, Type>)) as Dictionary<string, Type>;
            m_dicPrivateTypeToNomTable = info.GetValue("DIC_TYPE_TO_TABLE", typeof(Dictionary<Type, string>)) as Dictionary<Type, string>;
        }

        //------------------------------------------------------------------------
        public static CMemoryDb FromDataSet(DataSet ds)
        {
            MemoryStream stream = new MemoryStream();
            ds.WriteXml(stream, XmlWriteMode.WriteSchema);

            stream.Seek(0, SeekOrigin.Begin);
            CMemoryDb db = new CMemoryDb();
            db.ReadXml(stream, XmlReadMode.ReadSchema);

            db.FindAllTablesTypes();
            return db;
        }

        //------------------------------------------------------------------------
        internal bool IsFullReadEnCours
        {
            get
            {
                return m_bIsFullReadEnCours;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        private void FindAllTablesTypes()
        {
            foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
            {
                RegisterAssembly(ass);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        private void RegisterAssembly(Assembly ass)
        {
            foreach (Type tp in ass.GetTypes())
            {
                object[] attrs = tp.GetCustomAttributes(typeof(MemoryTableAttribute), true);
                if (attrs.Length > 0)
                {
                    MemoryTableAttribute att = attrs[0] as MemoryTableAttribute;
                    m_dicPrivateNomTableToType[att.NomTable] = tp;
                    m_dicPrivateTypeToNomTable[tp] = att.NomTable;
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DATASET_DATA", AdoNetHelper.SerializeDataSet(this));
            info.AddValue("DIC_TABLE_TO_TYPE", m_dicPrivateNomTableToType);
            info.AddValue("DIC_TYPE_TO_TABLE", m_dicPrivateTypeToNomTable);
        }



        //---------------------------------------
        public void AddFournisseurElementsManquants(IFournisseurElementsManquantsPourMemoryDb fournisseur)
        {
            foreach (Type tp in fournisseur.TypesFournis)
            {
                List<IFournisseurElementsManquantsPourMemoryDb> lst = null;
                if (!m_fournisseursSupplementairesElements.TryGetValue(tp, out lst))
                {
                    lst = new List<IFournisseurElementsManquantsPourMemoryDb>();
                    m_fournisseursSupplementairesElements[tp] = lst;
                }
                if (!lst.Contains(fournisseur))
                    lst.Add(fournisseur);
            }
        }

        //---------------------------------------
        private string GetNomTable(Type tp)
        {
            CEntiteDeMemoryDb entite = Activator.CreateInstance(tp, new object[] { this }) as CEntiteDeMemoryDb;
            return entite.GetNomTable();
        }

        //---------------------------------------
        public DataTable GetTable ( Type tp )
        {
            string strNomTable = GetNomTable(tp);
            DataTable table = Tables[strNomTable];
            if (table == null)
            {
                if (!CreateTable(tp))
                    return null;
                return Tables[strNomTable];
            }
            return table;
        }

        //---------------------------------------
        public DataTable GetTable<T>()
            where T : CEntiteDeMemoryDb
        {
            return GetTable ( typeof(T));
        }

        //---------------------------------------
        public string GetTableNameForType ( Type t )
        {
            string strNomTable = null;
            m_dicPrivateTypeToNomTable.TryGetValue(t, out strNomTable);
            return strNomTable;
        }

        //---------------------------------------
        public Type GetTypeForTable ( string strNomTable )
        {
            Type tp =null;
            m_dicPrivateNomTableToType.TryGetValue(strNomTable, out tp );
            return tp;
        }

        //---------------------------------------
        public static string GetNomRelation(string strTableFille,
            string strProprieteFilleRetournantLeParent)
        {
            return strTableFille + "_" + strProprieteFilleRetournantLeParent;
        }

        

        //---------------------------------------
        public bool CreateTable(Type type)
        {
            MemoryTableAttribute tableAttr = type.GetCustomAttributes(typeof(MemoryTableAttribute),true)[0] as MemoryTableAttribute;
            string strNomTable = tableAttr.NomTable;
            string strChampId = tableAttr.ChampId;

            m_dicPrivateNomTableToType[strNomTable] = type;
            m_dicPrivateTypeToNomTable[type] = strNomTable;

            DataTable table = Tables[strNomTable];
            if (table != null)
                return true;
            table = new DataTable(strNomTable);
            
            //Création du champ Id
            DataColumn col = new DataColumn(strChampId, typeof(string));
            col.AllowDBNull = true;
            table.Columns.Add(col);
            table.PrimaryKey = new DataColumn[] { col };

            

            //Création des champs de propriété
            PropertyInfo[] proprietes = type.GetProperties();
            foreach (PropertyInfo prop in proprietes)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(MemoryFieldAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    MemoryFieldAttribute field = attrs[0] as MemoryFieldAttribute;
                    string strNomChamp = field.NomChamp;
                    if (strNomChamp == "")
                        strNomChamp = prop.Name;
                    bool bNullable = prop.PropertyType.IsClass;
                    Type tpProp = prop.PropertyType;
                    if (!bNullable)
                    {
                        if (prop.PropertyType.IsGenericType)
                        {
                            if (prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                tpProp = prop.PropertyType.GetGenericArguments()[0];
                        }
                    }
                    if (table.Columns[strNomChamp] == null)
                    {
                        col = new DataColumn(strNomChamp, tpProp);
                        col.AllowDBNull = true;
                        table.Columns.Add(col);
                    }
                }
            }
            Tables.Add(table);

            //Création des relations parentes
            foreach (PropertyInfo prop in proprietes)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(MemoryParentAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    MemoryParentAttribute relation = attrs[0] as MemoryParentAttribute;
                    DataTable tableParente = GetTable(prop.PropertyType);
                    string strNomChamp = relation.NomChampFils;
                    if (strNomChamp == "")
                        strNomChamp = tableParente.PrimaryKey[0].ColumnName;
                    col = new DataColumn(strNomChamp, typeof(string));
                    col.AllowDBNull = true;
                    table.Columns.Add(col);
                    DataRelation dtRelation = new DataRelation ( GetNomRelation(table.TableName,prop.Name) ,
                        tableParente.PrimaryKey[0],
                        col,
                        true );
                    Relations.Add ( dtRelation );
                    dtRelation.ExtendedProperties[c_ExtPropRelationNomProprieteFils]=prop.Name;
                    foreach (Constraint cst in table.Constraints)
                    {
                        ForeignKeyConstraint fk = cst as ForeignKeyConstraint;
                        if (fk != null && fk.ConstraintName == dtRelation.RelationName)
                        {
                            fk.DeleteRule = relation.IsComposition?Rule.Cascade:Rule.None;
                        }
                    }
                }
            }

            ///Création des tables filles
            foreach (PropertyInfo prop in proprietes)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(MemoryChildAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    Type tpFils = prop.PropertyType.GetGenericArguments()[0];
                    GetTable(tpFils);
                }
            }

            if (!table.Columns.Contains(c_champIsToRead))
            {
                DataColumn colIsToRead = new DataColumn(c_champIsToRead, typeof(bool));
                colIsToRead.DefaultValue = false;
                colIsToRead.AllowDBNull = false;
                table.Columns.Add(colIsToRead);
            }

            
            return true;
        }


        //----------------------------------------------------
        public CEntiteDeMemoryDb ImporteObjet(
            CEntiteDeMemoryDb valeur, 
            bool bAvecFils,
            bool bMiseAJourElementsExistants)
        {
            HashSet<string> elementsDejaImportés = new HashSet<string>();
            return ImporteObjet(valeur, bAvecFils, bMiseAJourElementsExistants, elementsDejaImportés);
        }

        //----------------------------------------------------
        public CEntiteDeMemoryDb ImporteObjet(
            CEntiteDeMemoryDb valeur, 
            bool bAvecFils,
            bool bMiseAJourElementsExistants,
            HashSet<string> elementsDejaImportés)
        {
            bool bOldEnforceConstrainte = EnforceConstraints;
            try
            {
                EnforceConstraints = false;

                DataTable table = GetTable(valeur.GetType());
                if (elementsDejaImportés.Contains(valeur.ObjectKey))
                {
                    DataRow row = table.Rows.Find(valeur.Id);
                    if (row == null)
                        return null;
                    return Activator.CreateInstance(valeur.GetType(), new object[] { row }) as CEntiteDeMemoryDb;
                }
                DataRow rowExistante = table.Rows.Find(valeur.Id);
                if (rowExistante != null && !(bool)rowExistante[c_champIsToRead] && !bMiseAJourElementsExistants)
                {
                    return Activator.CreateInstance(valeur.GetType(), new object[] { rowExistante }) as CEntiteDeMemoryDb;
                }

                elementsDejaImportés.Add(valeur.ObjectKey);
                foreach (PropertyInfo info in valeur.GetType().GetProperties())
                {
                    object[] attrs = info.GetCustomAttributes(typeof(MemoryParentAttribute), true);
                    if (attrs != null && attrs.Length > 0)
                    {
                        MemoryParentAttribute parentAtt = attrs[0] as MemoryParentAttribute;
                        string strChampFils = parentAtt.NomChampFils;
                        if (strChampFils == "")
                        {
                            CEntiteDeMemoryDb entite = Activator.CreateInstance(info.PropertyType, new object[] { this }) as CEntiteDeMemoryDb;
                            strChampFils = entite.GetChampId();
                        }
                        CEntiteDeMemoryDb parent = valeur.GetParent(info.PropertyType, strChampFils);
                        if (parent != null)
                            ImporteObjet(parent, true, bMiseAJourElementsExistants, elementsDejaImportés);
                    }
                }

                if (rowExistante == null)
                    table.ImportRow(valeur.Row.Row);
                else
                {
                    if ((bool)valeur.Row.Row[c_champIsToRead] || bMiseAJourElementsExistants)
                    {
                        foreach (DataColumn col in valeur.Row.Row.Table.Columns)
                        {
                            if (table.Columns.Contains(col.ColumnName))
                                rowExistante[col.ColumnName] = valeur.Row.Row[col.ColumnName];
                        }
                    }
                }

                if (bAvecFils)
                {
                    foreach (DataRelation dr in valeur.Database.Relations)
                    {
                        if (dr.ParentTable.TableName == table.TableName)
                        {
                            Type tpFils = valeur.Database.GetTypeForTable(dr.ChildTable.TableName);
                            if (tpFils != null)
                            {
                                PropertyInfo prop = tpFils.GetProperty(dr.ExtendedProperties[c_ExtPropRelationNomProprieteFils].ToString());
                                if (prop != null)
                                {
                                    object[] attrs = prop.GetCustomAttributes(typeof(MemoryParentAttribute), true);
                                    if (attrs != null && attrs.Length > 0)
                                    {
                                        MemoryParentAttribute parentAtt = attrs[0] as MemoryParentAttribute;
                                        if (parentAtt.IsComposition)
                                        {
                                            Type tpListe = typeof(CListeEntitesDeMemoryDb<>);
                                            Type tpListeType = tpListe.MakeGenericType(tpFils);
                                            IEnumerable lst = Activator.CreateInstance(tpListeType, new object[] { valeur.Database, new CFiltreMemoryDb(dr.ChildColumns[0].ColumnName + "=@1", valeur.Id) }) as IEnumerable;
                                            foreach (CEntiteDeMemoryDb entite in lst)
                                                ImporteObjet(entite, bAvecFils, bMiseAJourElementsExistants, elementsDejaImportés);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                return Activator.CreateInstance(valeur.GetType(), new object[] { table.Rows.Find(valeur.Id) }) as CEntiteDeMemoryDb;
            }
            finally
            {
                EnforceConstraints = bOldEnforceConstrainte;
            }
        }


        //----------------------------------------------------------
        public T GetEntite<T>(string strId)
            where T : CEntiteDeMemoryDb
        {
            T entite = Activator.CreateInstance(typeof(T), new object[] { this }) as T;
            if (entite != null && entite.ReadIfExist(strId))
                return entite;

            return null;
        }

        //----------------------------------------------------------
        public CResultAErreur RemoveEntite(Type tp, string strId)
        {
            CResultAErreur result = CResultAErreur.True;
            CEntiteDeMemoryDb entite = Activator.CreateInstance(tp, new object[] { this }) as CEntiteDeMemoryDb;
            if (entite != null && entite.ReadIfExist(strId))
            {
                result = entite.Delete();
            }
            return result;
        }

        //----------------------------------------------------------
        public CResultAErreur UpdateEntite(CEntiteDeMemoryDb entite)
        {
            CResultAErreur result = CResultAErreur.True;
            if (entite.Database != this)
            {
                try
                {
                    ImporteObjet(entite, true, true);
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                }
            }
            return result;
        }

        //----------------------------------------------------------
        public CListeEntitesDeMemoryDb<T> GetEntites<T>()
            where T : CEntiteDeMemoryDb
        {
            return new CListeEntitesDeMemoryDb<T>(this);
        }



        //----------------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
        }

        //----------------------------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            CResultAErreur result = CResultAErreur.True;
            int nVersion = GetNumVersion();
            result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            bool bOldEnforce = false;
            EnforceConstraints = false;
            int nNbTables = Tables.Count;
            serializer.TraiteInt(ref nNbTables);
            try
            {
                m_bIsFullReadEnCours = true;
                for (int n = 0; n < nNbTables; n++)
                {
                    DataTable table = null;
                    if (serializer.Mode == ModeSerialisation.Ecriture)
                        table = Tables[n];
                    result = SerializeTable(serializer, nVersion, ref table);
                    if (!result)
                        return result;
                }
                try
                {
                    EnforceConstraints = bOldEnforce;
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                }
            }
            finally
            {
                m_bIsFullReadEnCours = false;
            }
            return result;            
        }

        //----------------------------------------------------------
        private CResultAErreur SerializeTable(C2iSerializer serializer, int nVersion, ref DataTable table)
        {
            Type tp = null;
            int nNb = 0;
            CResultAErreur result = CResultAErreur.True;
            DateTime dt = DateTime.Now;
            switch ( serializer.Mode )
            {
                case ModeSerialisation.Ecriture :
                    tp = GetTypeForTable ( table.TableName );
                    serializer.TraiteType ( ref tp );
                    nNb = table.Rows.Count;
                    serializer.TraiteInt ( ref nNb );
                    foreach ( DataRow row in table.Rows )
                    {
                        I2iSerializable entite = Activator.CreateInstance ( tp, new object[]{row}) as I2iSerializable;
                        if ( nVersion == 0 )
                            result = serializer.TraiteObject( ref entite );
                        else
                            result = entite.Serialize(serializer);
                        
                        if ( !result )
                            return result;
                    }
                    break;
                case ModeSerialisation.Lecture :
                    tp = null;
                    serializer.TraiteType ( ref tp );
                    table = GetTable(tp);
                    if ( table == null )
                    {
                        result.EmpileErreur ( I.T("Can not allocate table for type @1|20009", tp.ToString())) ;
                        return result;
                    }
                    nNb = 0;
                    serializer.TraiteInt ( ref nNb );
                    I2iSerializable ser = (I2iSerializable)Activator.CreateInstance(tp, new object[] { this });
                    for ( int n = 0; n <nNb ; n++ )
                    {
                        if (nVersion == 0)
                            result = serializer.TraiteObject(ref ser, this);
                        else
                        {
                            if ( !IsFullReadEnCours )
                                ser = (I2iSerializable)Activator.CreateInstance(tp, new object[] { this });
                            result = ser.Serialize(serializer);
                        }
                        if ( !result )
                            return result;
                    }
                    break;
            }
            /*TimeSpan sp = DateTime.Now - dt;
            Console.WriteLine("Serialize " + table.TableName + " : " + sp.TotalMilliseconds);*/
           
            return result;
        }

        //----------------------------------------------------------
        public CEntiteDeMemoryDb GetElementFromFournisseurSupplementaire(Type tp, string strId)
        {
            List<IFournisseurElementsManquantsPourMemoryDb> lst = null;
            if (m_fournisseursSupplementairesElements.TryGetValue(tp, out lst))
            {
                foreach (IFournisseurElementsManquantsPourMemoryDb fournisseur in lst)
                {
                    CEntiteDeMemoryDb entite = fournisseur.GetEntite(tp, strId, this);
                    if (entite != null)
                        return entite;
                }
            }
            return null;
        }

        //----------------------------------------------------------
        public T GetElementFromFournisseurSupplementaire<T>(string strId)
            where T : CEntiteDeMemoryDb
        {
            return GetElementFromFournisseurSupplementaire(typeof(T), strId) as T;
        }

        //----------------------------------------------------------
        public CResultAErreur UpdateFrom(CMemoryDb sourceDb)
        {
            return MiseAjour(sourceDb, true);
        }

        //----------------------------------------------------------
        public CResultAErreur Importe(CMemoryDb sourceDb)
        {
            return MiseAjour(sourceDb, false);
        }

        //----------------------------------------------------------
        private CResultAErreur MiseAjour(CMemoryDb sourceDB, bool bUpdateOnly)
        {
            foreach (DataTable table in sourceDB.Tables)
            {
                if (!bUpdateOnly || Tables[table.TableName] != null)
                {
                    Type tp = sourceDB.GetTypeForTable(table.TableName);
                    if (tp != null)
                        GetTable(tp);
                }
            }
            EnforceConstraints = false;
            foreach (DataTable table in sourceDB.Tables)
            {
                if (Tables.Contains(table.TableName))
                {
                    table.BeginLoadData();
                    Type tp = sourceDB.GetTypeForTable(table.TableName);
                    foreach (DataRow row in table.Rows)
                    {
                        if (!bUpdateOnly || Tables[table.TableName].Rows.Find(row[table.PrimaryKey[0]]) != null)
                        {
                            CEntiteDeMemoryDb entite = Activator.CreateInstance(tp, new object[] { row }) as CEntiteDeMemoryDb;
                            ImporteObjet(entite, false, false);
                        }
                    }
                    table.EndLoadData();
                }
            }
            EnforceConstraints = true;
            return CResultAErreur.True;
        }
    }
}
