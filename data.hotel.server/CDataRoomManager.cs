using data.hotel.client;
using data.hotel.client.query;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.server
{
    public class CDataRoomManager
    {
        private static string m_strRepertoireStockage = "";

        private static CDataRoomManager m_instance = null;


        //------------------------------------------------
        private class CPlageDates
        {
            public DateTime? DateMin = null;
            public DateTime? DateMax = null;

            //------------------------------------------------
            public CPlageDates()
            {
            }

            //------------------------------------------------
            public CPlageDates ( DateTime dateMin, DateTime dateMax )
            {
                DateMax = dateMax;
                DateMin = dateMin;
            }
        }

        //------------------------------------------------
        //dates min et max pour chaque table
        private Dictionary<string, CPlageDates> m_dicPlagesDeDates = new Dictionary<string, CPlageDates>();

        //--------------------------------------------------------------------
        private CPlageDates ReCalcDatesMinEtMax(string strIdTable)
        {
            CPlageDates plage = new CPlageDates();
            plage.DateMin = DateTime.Now.Date;
            plage.DateMin = DateTime.Now.Date;
            StringBuilder bl = new StringBuilder();
            AddPathForTable(strIdTable, bl);
            if (Directory.Exists(bl.ToString()))
            {
                int nYear = 0;
                List<int> lstYears = new List<int>(from strDir in Directory.GetDirectories(bl.ToString())
                                                   where Int32.TryParse(Path.GetFileName(strDir), out nYear)
                                                   select nYear);
                lstYears.Sort();
                if (lstYears.Count > 0)
                {
                    bl = new StringBuilder();
                    AddPathForTable(strIdTable, bl);
                    bl.Append("\\");
                    bl.Append(lstYears[0].ToString());
                    int nMonth = 0;
                    List<int> lstMonths = new List<int>(from strDir in Directory.GetDirectories(bl.ToString())
                                                        where Int32.TryParse(Path.GetFileName(strDir), out nMonth) &&
                                                        nMonth >= 1 && nMonth <= 12
                                                        select nMonth);
                    lstMonths.Sort();
                    if (lstMonths.Count > 0)
                        plage.DateMin = new DateTime(nYear, lstMonths[0], 1);
                    if (lstYears.Count == 1)
                        //Même année
                        plage.DateMax = new DateTime(nYear, lstMonths[lstMonths.Count - 1], 1).AddMonths(1).AddSeconds(-0.1);
                    else
                    {
                        bl = new StringBuilder();
                        AddPathForTable(strIdTable, bl);
                        bl.Append("\\");
                        bl.Append(lstYears[lstYears.Count - 1].ToString());
                        nMonth = 0;
                        lstMonths = new List<int>(from strDir in Directory.GetDirectories(bl.ToString())
                                                  where Int32.TryParse(strDir, out nMonth) &&
                                                  nMonth >= 1 && nMonth <= 12
                                                  select nMonth);
                        lstMonths.Sort();
                        if (lstMonths.Count > 0)
                            plage.DateMax = new DateTime(nYear, lstMonths[lstMonths.Count - 1], 1).AddMonths(1).AddSeconds(-0.1);
                    }
                }
            }
            return plage;
        }

        //--------------------------------------------------------------------
        private CPlageDates GetPlageDates ( string strIdTable )
        {
            CPlageDates plage = null;
            if (!m_dicPlagesDeDates.TryGetValue(strIdTable, out plage))
            {
                plage = ReCalcDatesMinEtMax(strIdTable);
                m_dicPlagesDeDates[strIdTable] = plage;
            }
            return plage;
        }
        

        //--------------------------------------------------------------------
        private CDataRoomManager()
        {

        }

        //--------------------------------------------------------------------
        public static string RepertoireStockage
        {
            get
            {
                if ( m_strRepertoireStockage.Length == 0)
                {
                    string strRep = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    if (strRep[strRep.Length - 1] != '\\')
                        strRep += "\\";
                    strRep += "Futurocom\\DataHotelSrv";
                    m_strRepertoireStockage = strRep;
                }
                if (m_strRepertoireStockage.Length > 0 && m_strRepertoireStockage[m_strRepertoireStockage.Length-1] != '\\')
                    m_strRepertoireStockage += '\\';
                return m_strRepertoireStockage;
            }
            set {
                m_strRepertoireStockage = value;
            }
        }

        //--------------------------------------------------------------------
        public static CDataRoomManager Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new CDataRoomManager();
                return m_instance;
            }

        }

        //--------------------------------------------------------------------
        private void AddPathForTable ( string strTableId, StringBuilder bl )
        {
            bl.Append(RepertoireStockage);
            bl.Append(strTableId);
        }


        //--------------------------------------------------------------------
        private string GetPathForData(string strTableId, string strIdEntite, string strIdChamp, DateTime dt)
        {
            StringBuilder blRep = new StringBuilder();
            AddPathForTable(strTableId, blRep);
            blRep.Append("\\");
            blRep.Append(dt.Year);
            blRep.Append("\\");
            blRep.Append(dt.Month);
            blRep.Append("\\");
            blRep.Append(strIdEntite.Substring(0, 1));
            blRep.Append("\\");
            blRep.Append(strIdEntite);
            blRep.Append("\\");
            blRep.Append(strIdChamp);
            blRep.Append("\\");
            blRep.Append(dt.Day);
            blRep.Append(".fdt");
            return blRep.ToString();
        }


        public TimeSpan m_spAssureRep = new TimeSpan(0);
        public TimeSpan m_spWrite = new TimeSpan(0);

        //--------------------------------------------------------------------
        public CResultAErreur SetData(string strTableId, string strIdEntite, string strIdChamp, DateTime dt, double fVal)
        {
            string strFile = GetPathForData(strTableId, strIdEntite, strIdChamp, dt);
            DateTime dtChrono = DateTime.Now;
            m_spAssureRep += DateTime.Now - dtChrono;
            FileStream stream = null;
            dtChrono = DateTime.Now;
            if (!File.Exists(strFile))
            {
                CUtilRepertoire.AssureRepertoirePourFichier(strFile);
                stream = new FileStream(strFile, FileMode.CreateNew, FileAccess.Write);
            }
            else
                stream = new FileStream(strFile, FileMode.Append, FileAccess.Write);

            BinaryWriter writer = new BinaryWriter(stream);
            CDataRoomEntry rec = new CDataRoomEntry(dt, fVal);
            rec.Write(writer);
            writer.Close();
            writer.Dispose();
            stream.Close();
            stream.Dispose();
            m_spWrite += DateTime.Now - dtChrono;
            CPlageDates plage = GetPlageDates(strTableId);
            if (dt > plage.DateMax)
                plage.DateMax = dt;
            if (dt < plage.DateMin)
                plage.DateMin = dt;
            return CResultAErreur.True;
        }

        //-------------------------------------------------------------------------------------------------------------------
        private List<CDataRoomEntry> ReadFile ( DateTime dt, string strFichier )
        {
            List<CDataRoomEntry> lstRecs = new List<CDataRoomEntry>();
            FileStream fs = new FileStream(strFichier, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            long nLength = fs.Length;
            while ( fs.Position < nLength )
            {
                CDataRoomEntry rec = new CDataRoomEntry();
                rec.Read(reader, dt);
                lstRecs.Add(rec);
            }
            reader.Close();
            fs.Close();
            reader.Dispose();
            fs.Dispose();
            return lstRecs;

        }


        //-------------------------------------------------------------------------------------------------------------------
        public List<CDataRoomEntry> GetData(string strTableId, string strIdEntite, string strIdChamp, DateTime dateDebut, DateTime dateFin)
        {
            List<CDataRoomEntry> lstRecs = new List<CDataRoomEntry>();
            DateTime dt = dateDebut;
            while ( dt < dateFin )
            {
                string strFile = GetPathForData(strTableId, strIdEntite, strIdChamp, dt);
                if ( File.Exists (strFile ))
                {
                    List<CDataRoomEntry> lstTmp = ReadFile(dt, strFile);
                        if (dt.Date == dateDebut.Date || dt.Date == dateFin.Date)
                            lstRecs.AddRange(from h in lstTmp where h.Date >= dateDebut && h.Date <= dateFin select h);
                        else
                        {
                            lstRecs.AddRange(lstTmp);
                        }
                    
                }
                dt = dt.Date.AddDays(1);
            }
            return lstRecs;
        }

        //-------------------------------------------------------------------------------------------------------------------
        public DateTime? GetMinDateForEntity(string strIdEntite)
        {
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------
        public DateTime? GetMaxDateForEntity(string strIdEntite)
        {
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------
        public DataTable GetData ( 
            CDataHotelQuery query )
        {
            DataTable table = new DataTable();
            table.Columns.Add(CDataHotelTable.c_nomChampTableEntiteId, typeof(string));
            table.Columns.Add(CDataHotelTable.c_nomChampTableDate, typeof(DateTime));

            //Sélectionne les champs nécéssaires au calcul
            //On utilise un Hashset et une liste pour conserver l'ordre
            HashSet<string> setChampsSource = new HashSet<string>();
            List<string> lstChampsSource = new List<string>();

            foreach (string strIdChamp in query.ChampsId)
            {
                if (!setChampsSource.Contains(strIdChamp))
                {
                    lstChampsSource.Add(strIdChamp);
                    setChampsSource.Add(strIdChamp);
                }
            }
            foreach (IChampHotelCalcule champCalcule in query.ChampsCalcules)
                if (champCalcule.IdsChampsSource.Count() > 0)
                {
                    foreach ( string strIdChampSource in champCalcule.IdsChampsSource )
                    if (!setChampsSource.Contains(strIdChampSource))
                    {

                        lstChampsSource.Add(strIdChampSource);
                        setChampsSource.Add(strIdChampSource);
                    }
                }

            foreach (string strIdChamp in lstChampsSource)
                table.Columns.Add(strIdChamp, typeof(double));

            //Récupère toutes les données
            foreach ( string strIdEntite in query.EntitiesId )
            {
                Dictionary<DateTime, Dictionary<string, double>> dicValues = new Dictionary<DateTime, Dictionary<string, double>>();
                foreach (string strChamp in lstChampsSource)
                {
                    List<CDataRoomEntry> lst = GetData(query.TableId, strIdEntite, strChamp, query.DateDebut.Value, query.DateFin.Value);
                    foreach ( CDataRoomEntry rec in lst )
                    {
                        if (query.Filtre == null || query.Filtre.IsInFilter(strChamp, rec))
                        {
                            Dictionary<string, double> vals = null;
                            if (!dicValues.TryGetValue(rec.Date, out vals))
                            {
                                vals = new Dictionary<string, double>();
                                dicValues[rec.Date] = vals;
                            }
                            vals[strChamp] = rec.Value;
                        }
                    }
                }
                foreach ( KeyValuePair<DateTime, Dictionary<string, double>> kv in dicValues )
                {
                    DataRow row = table.NewRow();
                    row[CDataHotelTable.c_nomChampTableEntiteId] = strIdEntite;
                    row[CDataHotelTable.c_nomChampTableDate] = kv.Key;
                    foreach ( KeyValuePair<string, double> fv in kv.Value )
                        row[fv.Key] = fv.Value;
                    table.Rows.Add ( row );
                }
            }
            return table;
        }

        //-------------------------------------------------------------------------------------------------------------------
        public IDataRoomEntry GetFirstNotInSerie ( 
            string strIdTable, 
            string strIdEntite, 
            string strIdChamp,
            DateTime dateRecherche,
            ITestDataHotel test
            )
        {
            if ( test  == null )
                return null;
            List<CDataRoomEntry> lstData = GetData(strIdTable, strIdEntite, strIdChamp, dateRecherche.Date, dateRecherche);
            lstData.Sort((x, y) => x.Date.CompareTo(y.Date));
            for ( int n = lstData.Count-1; n >= 0; n-- )
            {
                if (!test.IsInFilter(strIdChamp, lstData[n]))
                {
                    return lstData[n];
                }
            }
            CPlageDates plage = GetPlageDates(strIdTable);
            if (dateRecherche > plage.DateMin)
                return GetFirstNotInSerie(strIdTable, strIdEntite, strIdChamp, dateRecherche.Date.AddSeconds(-0.1), test);
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retourne la donnée connue à la date demandée
        /// </summary>
        /// <param name="strIdTable"></param>
        /// <param name="strIdEntite"></param>
        /// <param name="strIdChamp"></param>
        /// <param name="dateRecherche"></param>
        /// <param name="test"></param>
        /// <param name="recordSuivantQuiRépondAuTest"></param>
        /// <returns></returns>
        private IDataRoomEntry GetKnownDataAt(
            string strIdTable,
            string strIdEntite,
            string strIdChamp,
            DateTime dateRecherche
            )
        {
            List<CDataRoomEntry> lstData = GetData(strIdTable, strIdEntite, strIdChamp, dateRecherche.Date, dateRecherche);
            lstData.Sort((x, y) => x.Date.CompareTo(y.Date));
            if (lstData.Count > 0)
                return lstData[lstData.Count - 1];
            CPlageDates plage = GetPlageDates(strIdTable);
            if (dateRecherche > plage.DateMin)
                return GetKnownDataAt(strIdTable, strIdEntite, strIdChamp, dateRecherche.Date.AddSeconds(-0.1));
            return null;
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Dit depuis combien de temps le champ demandé correspond
        /// au filtre à la date donnée
        /// </summary>
        /// <param name="strTableId"></param>
        /// <param name="strEntityId"></param>
        /// <param name="strFieldId"></param>
        /// <param name="dt"></param>
        /// <param name="filtre"></param>
        /// <returns></returns>
        public double GetDepuisCombienDeTempsEnS ( 
            string strTableId,
            string strEntityId,
            string strFieldId,
            DateTime dt,
            ITestDataHotel filtre )
        {
            IDataRoomEntry entry = GetKnownDataAt ( strTableId, strEntityId, strFieldId, dt );
            if ( entry == null )
                return 0;
            if (!filtre.IsInFilter(strFieldId, entry))
                return 0;
            entry = GetFirstNotInSerie(strTableId, strEntityId, strFieldId, dt, filtre);
            if (entry != null)
                return (dt - entry.Date).TotalSeconds;
            return 0;
        }



        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des entités pour une table entre deux dates
        /// </summary>
        /// <param name="strIdTable"></param>
        /// <param name="dataStart"></param>
        /// <param name="dataEnd"></param>
        /// <returns></returns>
        internal IEnumerable<string> GetEntities(string strIdTable, DateTime dataStart, DateTime dataEnd)
        {
            DateTime dt = dataStart.Date;
            List<string>  lstEtts = new List<string>();
            do
            {
                StringBuilder blRep = new StringBuilder();
                AddPathForTable(strIdTable, blRep);
                blRep.Append("\\");
                blRep.Append(dt.Year);
                blRep.Append("\\");
                blRep.Append(dt.Month);
                if (Directory.Exists(blRep.ToString()))
                {
                    foreach (string strDir in Directory.GetDirectories(blRep.ToString()))//rep initial
                    {
                        foreach (string strDirEtt in Directory.GetDirectories(strDir))
                            lstEtts.Add(Path.GetFileName(strDirEtt));
                    }
                }
                dt = dt.AddDays(1);
            }
            while (dt <= dataEnd);
            return lstEtts;
        }
    }

     
}
