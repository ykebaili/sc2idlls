using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace sc2i.common.memorydb
{
    [Serializable]
    public abstract class CEntiteDeMemoryDb : I2iSerializable, ISerializable, IEquatable<CEntiteDeMemoryDb>, 
        IEntiteDeMemoryDb
    {
        private CMemoryDb m_database = null;
        private CMemoryDataRow m_row;

        //-----------------------------------------------------------
        public CEntiteDeMemoryDb(CMemoryDb database)
        {
            m_database = database;
        }

        //-----------------------------------------------------------
        public CEntiteDeMemoryDb(DataRow row)
        {
            m_row = new CMemoryDataRow(row);
            m_database = (CMemoryDb)m_row.Table.DataSet;
        }

        //-----------------------------------------------------------
        public CEntiteDeMemoryDb(SerializationInfo info, StreamingContext context)
        {
        }

        //-----------------------------------------------------------
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }

        //-----------------------------------------------------------
        public CMemoryDb Database
        {
            get
            {
                return m_database;
            }
        }

        //-----------------------------------------------------------
        public virtual string GetChampTriParDefaut()
        {
            return "";
        }

        //-----------------------------------------------------------
        public string ObjectKey
        {
            get
            {
                return GetType() + "_" + Id;
            }
        }

        //-----------------------------------------------------------
        public CMemoryDataRow Row
        {
            get
            {
                return m_row;
            }
        }

        //-----------------------------------------------------------
        private static Dictionary<Type, string> m_dicNomsTables = new Dictionary<Type, string>();
        public virtual string GetNomTable()
        {
            string strNomTable = null;
            if (!m_dicNomsTables.TryGetValue(GetType(), out strNomTable))
            {
                MemoryTableAttribute attr = GetType().GetCustomAttributes(typeof(MemoryTableAttribute), true)[0] as MemoryTableAttribute;
                strNomTable = attr.NomTable;
                m_dicNomsTables[GetType()] = strNomTable;
            }
            return strNomTable;
        }

        //-----------------------------------------------------------
        private static Dictionary<Type, string> m_dicChampsId = new Dictionary<Type, string>();
        public virtual string GetChampId()
        {
            string strChampId = null;
            if (!m_dicChampsId.TryGetValue(GetType(), out strChampId))
            {
                MemoryTableAttribute attr = GetType().GetCustomAttributes(typeof(MemoryTableAttribute), true)[0] as MemoryTableAttribute;
                strChampId = attr.ChampId;
                m_dicChampsId[GetType()] = strChampId;
            }
            return strChampId;
        }

        //-----------------------------------------------------------
        [DynamicField("Id")]
        public string Id
        {
            get
            {
                return (string)Row[GetChampId()];
            }
            set
            {
                Row[GetChampId()] = value;
            }
        }

        //-----------------------------------------------------------
        protected void InitValeursParDefaut()
        {
            MyInitValeursParDefaut();
        }

        //-----------------------------------------------------------
        public abstract void MyInitValeursParDefaut();

        //-----------------------------------------------------------
        public void CreateNew(string strId)
        {
            DataTable table = m_database.GetTable(GetType());
            m_row = new CMemoryDataRow(table.NewRow());
            m_row[GetChampId()] = strId;
            InitValeursParDefaut();
            table.Rows.Add(m_row.Row);
        }

        //-----------------------------------------------------------
        public bool ReadIfExist(string strId)
        {
            return ReadIfExist(strId, true);
        }

        //-----------------------------------------------------------
        public bool ReadIfExist(string strId, bool bUtiliserLesSourcesExternes)
        {
            DataTable table = m_database.GetTable(GetType());
            DataRow row = table.Rows.Find(strId);
            if (row != null)
            {
                m_row = new CMemoryDataRow(row);
                return true;
            }
            else if ( bUtiliserLesSourcesExternes )
            {
                CEntiteDeMemoryDb entite = Database.GetElementFromFournisseurSupplementaire(GetType(), strId);
                if (entite != null)
                {
                    m_row = new CMemoryDataRow(entite.Row.Row);
                    return true;
                }
            }
            return false;
        }

        //-----------------------------------------------------------
        public bool ReadIfExist(CFiltreMemoryDb filtre)
        {
            DataTable table = m_database.GetTable(GetType());
            DataRow[] rows = table.Select(filtre.GetFiltreDataTable());
            if (rows.Length > 0)
            {
                m_row = new CMemoryDataRow(rows[0]);
                return true;
            }
            return false;
        }

        //-----------------------------------------------------------
        public virtual CResultAErreur Delete()
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                m_row.Row.Delete();
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

        //-----------------------------------------------------------
        public T GetParent<T>()
            where T : CEntiteDeMemoryDb
        {
            CEntiteDeMemoryDb entite = Activator.CreateInstance(typeof(T), new object[] { m_database }) as CEntiteDeMemoryDb;
            string strChampId = entite.GetChampId();
            return GetParent<T>(strChampId);
        }

        //-----------------------------------------------------------
        public CEntiteDeMemoryDb GetParent(Type type, string strChampFils)
        {
            string strKey = (string)Row[strChampFils];
            if (strKey == null)
                return null;

            DataTable table = m_database.GetTable(type);
            DataRow row = table.Rows.Find(strKey);
            if (row != null)
                return Activator.CreateInstance(type, new object[] { row }) as CEntiteDeMemoryDb;
            return null;
        }

        //-----------------------------------------------------------
        public T GetParent<T>(string strChampFils)
            where T : CEntiteDeMemoryDb
        {
            return GetParent(typeof(T), strChampFils) as T;
        }

        //-----------------------------------------------------------
        public CListeEntitesDeMemoryDb<T> GetFils<T>()
            where T : CEntiteDeMemoryDb
        {
            return GetFils<T>(GetChampId());
        }

        //-----------------------------------------------------------
        public CListeEntitesDeMemoryDb<T> GetFils<T>(string strChampId)
            where T : CEntiteDeMemoryDb
        {
            CListeEntitesDeMemoryDb<T> liste = new CListeEntitesDeMemoryDb<T>(m_database,
                new CFiltreMemoryDb(strChampId + "=@1", Id));
            return liste;
        }


        //-----------------------------------------------------------
        public T GetObjetInContexte<T>(T valeur)
            where T : CEntiteDeMemoryDb
        {
            if (valeur.Database == m_database)
                return valeur;
            return m_database.ImporteObjet(valeur, false, false) as T;
        }

        //-----------------------------------------------------------
        public void SetParent<T>(T valeur)
            where T : CEntiteDeMemoryDb
        {
            DataTable table = m_database.GetTable<T>();
            string strChampFils = table.PrimaryKey[0].ColumnName;
            SetParent<T>(valeur, strChampFils);
        }

        //-----------------------------------------------------------
        public void SetParent<T>(T valeur, string strChampFils)
            where T : CEntiteDeMemoryDb
        {
            if (valeur == null)
                Row[strChampFils] = null;
            else
            {
                valeur = GetObjetInContexte<T>(valeur);
                Row[strChampFils] = valeur.Id;
            }
        }

        //------------------------------------------------------------------------------
        public CResultAErreur SerializeChilds<T>(C2iSerializer serializer)
            where T : CEntiteDeMemoryDb
        {
            return SerializeChilds<T>(serializer, GetChampId());
        }

        //------------------------------------------------------------------------------
        protected CResultAErreur SerializeChilds<T>(C2iSerializer serializer, string strChampIdDansChild)
            where T : CEntiteDeMemoryDb
        {
            CResultAErreur result = CResultAErreur.True;
            CListeEntitesDeMemoryDb<T> listeChilds = GetFils<T>(strChampIdDansChild);
            int nNb = listeChilds.Count();
            if (serializer.Mode == ModeSerialisation.Ecriture && Database.IsFullReadEnCours)
                nNb = 0;
            serializer.TraiteInt(ref nNb);
            if (nNb > 0)
            {
                switch (serializer.Mode)
                {
                    case ModeSerialisation.Ecriture:
                        foreach (T child in listeChilds)
                        {
                            T childRef = child;
                            result = serializer.TraiteObject<T>(ref childRef);
                            if (!result)
                                return result;
                        }
                        break;
                    case ModeSerialisation.Lecture:
                        for (int nChild = 0; nChild < nNb; nChild++)
                        {
                            T child = null;
                            result = serializer.TraiteObject<T>(ref child, Database);
                            if (!result)
                                return result;
                        }
                        break;
                }
            }
            return result;
        }

        //------------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------------------------------------
        protected abstract CResultAErreur MySerialize ( C2iSerializer serializer );

        //------------------------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            string strId = serializer.Mode == ModeSerialisation.Lecture?"":Id;
            serializer.TraiteString(ref strId);
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                if (Database.IsFullReadEnCours)
                {
                    CreateNew(strId);
                }
                else
                {
                    if (Row == null)
                    {
                        if (!ReadIfExist(strId))
                            CreateNew(strId);
                    }
                    else
                    {
                        ReadIfExist(strId);
                    }
                }
            }
            return MySerialize(serializer);
        }

        private enum ETypeValeur
        {
            Null,
            I2iSerializable,
            Serializable
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// Serialize la valeur d'un champ
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="strChamp"></param>
        /// <returns></returns>
        protected CResultAErreur SerializeChamp(C2iSerializer serializer, string strChamp)
        {
            CResultAErreur result = CResultAErreur.True;
            object valeur = Row[strChamp];
            
            ETypeValeur typeValeur = ETypeValeur.Null;
            if (valeur is I2iSerializable)
                typeValeur = ETypeValeur.I2iSerializable;
            else if (valeur != null && valeur.GetType().GetCustomAttributes(typeof(SerializableAttribute), true).Length > 0)
                typeValeur = ETypeValeur.Serializable;
            else if ( valeur != null )
                result.EmpileErreur(I.T("Can not serialize value @1|20008", strChamp));
            else
                typeValeur = ETypeValeur.Null;
            
            int nTypeValeur = (int)typeValeur;
            serializer.TraiteInt(ref nTypeValeur);
            typeValeur = (ETypeValeur)nTypeValeur;
            switch ( typeValeur )
            {
                case ETypeValeur.I2iSerializable :
                I2iSerializable objSer = valeur as I2iSerializable;
                result = serializer.TraiteObject(ref objSer);
                if (!result)
                    return result;
                if (serializer.Mode == ModeSerialisation.Lecture)
                    Row[strChamp] = objSer;
                    break;
                case ETypeValeur.Serializable :
                result = serializer.TraiteSerializable(ref valeur);
                if (!result)
                    return result;
                if (serializer.Mode == ModeSerialisation.Lecture)
                    Row[strChamp] = valeur;
                    break;
            }

            return result;
        }

        //------------------------------------------------------------------------------
        public CResultAErreur SerializeParent<T>(C2iSerializer serializer)
            where T : CEntiteDeMemoryDb
        {
            //Trouve le champ parent
            DataTable table = Database.GetTable<T>();
            return SerializeParent<T>(serializer, table.PrimaryKey[0].ColumnName);
        }

        //------------------------------------------------------------------------------
        public CResultAErreur SerializeParent<T>(C2iSerializer serializer, string strChamp)
            where T : CEntiteDeMemoryDb
        {
            CResultAErreur result = CResultAErreur.True;
            string strVal = Row == null ? "" : Row.Get<string>(strChamp);
            if (strVal == null)
                strVal = "";
            serializer.TraiteString(ref strVal);
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                if (strVal == "")
                    Row[strChamp] = null;
                else
                {
                    if (!Database.IsFullReadEnCours)
                    {
                        DataTable table = Database.GetTable<T>();
                        if (table != null)
                        {
                            CEntiteDeMemoryDb parent = Activator.CreateInstance(typeof(T), Database) as CEntiteDeMemoryDb;
                            if (!parent.ReadIfExist(strVal))
                            {
                                parent.CreateNew(strVal);
                                parent.Row.Row[CMemoryDb.c_champIsToRead] = true;
                            }
                        }
                    }
                    Row[strChamp] = strVal;
                }
            }
            return result;
        }

        //------------------------------------------------------------------------------
        public CResultAErreur SerializeChamps(C2iSerializer serializer, params string[] strChamps)
        {
            CResultAErreur result = CResultAErreur.True;
            foreach (string strChamp in strChamps)
            {
                result = SerializeChamp(serializer, strChamp);
                if (!result)
                    return result;
            }
            return result;
        }

        //------------------------------------------------------------------------------
        public bool IsToRead()
        {
            return Row.Get<bool>(CMemoryDb.c_champIsToRead);
        }



        #region IEquatable<CEntiteDeMemoryDb> Membres

        bool IEquatable<CEntiteDeMemoryDb>.Equals(CEntiteDeMemoryDb other)
        {
            return other != null && other.GetType() == GetType() && other.Id == Id;
        }

        #endregion

        //------------------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            CEntiteDeMemoryDb ett = obj as CEntiteDeMemoryDb;
            return ett != null && ett.GetType() == GetType() && ett.Id == Id;
        }

        //------------------------------------------------------------------------------
        public static bool operator == (CEntiteDeMemoryDb obj1, CEntiteDeMemoryDb obj2)
        {

            if (((object)obj1) == null && ((object)obj2) == null)
                return true;
            if (((object)obj1) == null || ((object)obj2) == null)
                return false;
            return obj1.Equals(obj2);
        }

        //------------------------------------------------------------------------------
        public static bool operator !=(CEntiteDeMemoryDb obj1, CEntiteDeMemoryDb obj2)
        {
            return !(obj1 == obj2);
        }

        //------------------------------------------------------------------------------
        public override int GetHashCode()
        {
            return (GetType() + "_" + Id).GetHashCode();
        }

        
    }
    
}
