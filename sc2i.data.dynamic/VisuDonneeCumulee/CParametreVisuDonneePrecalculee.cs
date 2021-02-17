using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.data.dynamic;
using sc2i.data;
using System.Data;
using sc2i.common;
using sc2i.expression;
using System.Drawing;
using System.IO;
using sc2i.formulaire;

namespace sc2i.data.dynamic
{
    public class CParametreVisuDonneePrecalculee : I2iSerializable
    {
        private const string c_idFichier = "VISU_PRECALC"; 
        private CTableauCroise m_tableauCroise = new CTableauCroise();

        private int? m_nIdTypeDonneeCumulee = null;

        private List<CParametreVisuChampTableauCroise> m_listeParametresChamps = new List<CParametreVisuChampTableauCroise>();

        //filtres de base appliqués au tableau (sans interface)
        private List<CFiltreDonneePrecalculee> m_listeFiltresDeBase = new List<CFiltreDonneePrecalculee>();

        //filtres utilisateurs appliqués au tableau (avec interface)
        private List<CFiltreDonneePrecalculee> m_listeFiltresUser = new List<CFiltreDonneePrecalculee>();

        private CFormatChampTableauCroise m_formatHeader = null;
        private CFormatChampTableauCroise m_formatRows = null;

        private OperationsAgregation m_operationCumul = OperationsAgregation.None;
        private string m_strLibelleTotal = I.T("Total|10009");

        private bool m_bShowExportButton = true;
        private bool m_bShowHeader = true;

        private class CCacheElements
        {
            private Dictionary<Type, Dictionary<int, CObjetDonneeAIdNumerique>> m_dic = new Dictionary<Type, Dictionary<int, CObjetDonneeAIdNumerique>>();

            public CCacheElements()
            {
            }

            public void AddElement(CObjetDonneeAIdNumerique objet)
            {
                Type tp = objet.GetType();
                Dictionary<int, CObjetDonneeAIdNumerique> dic = null;
                if ( !m_dic.TryGetValue(tp, out dic ))
                {
                    dic = new Dictionary<int,CObjetDonneeAIdNumerique>();
                    m_dic[tp] = dic;
                }
                dic[objet.Id] = objet;
            }

            public CObjetDonneeAIdNumerique GetElement(Type type, int nId)
            {
                Dictionary<int, CObjetDonneeAIdNumerique> dic = null;
                if (m_dic.TryGetValue(type, out dic))
                {
                    CObjetDonneeAIdNumerique objet = null;
                    dic.TryGetValue(nId, out objet);
                    return objet;
                }
                return null;
            }
        }

        private CCacheElements m_cache = new CCacheElements();

        public CParametreVisuDonneePrecalculee()
        {
            InitFormatDefaut();
        }

        //---------------------------------------
        public void InitFormatDefaut()
        {
            m_formatHeader = new CFormatChampTableauCroise();
            m_formatHeader.BackColor = Color.Navy;
            m_formatHeader.ForeColor = Color.White;
            m_formatRows = new CFormatChampTableauCroise();
        }

        //---------------------------------------
        /// <summary>
        /// format des entêtes de colonnes
        /// </summary>
        public CFormatChampTableauCroise FormatHeader
        {
            get
            {
                return m_formatHeader;
            }
            set
            {
                m_formatHeader = value;
            }
        }

        //---------------------------------------
        /// <summary>
        /// format des lignes
        /// </summary>
        public CFormatChampTableauCroise FormatRows
        {
            get
            {
                return m_formatRows;
            }
            set
            {
                m_formatRows = value;
            }
        }

        //---------------------------------------
        public bool ShowExportButton
        {
            get
            {
                return m_bShowExportButton;
            }
            set
            {
                m_bShowExportButton = value;
            }
        }

        //---------------------------------------
        public bool ShowHeader
        {
            get
            {
                return m_bShowHeader;
            }
            set
            {
                m_bShowHeader = value;
            }
        }

        //---------------------------------------
        public string LibelleTotal
        {
            get
            {
                return m_strLibelleTotal;
            }
            set
            {
                m_strLibelleTotal = value;
            }
        }

        //---------------------------------------
        public CFiltreDonneePrecalculee[] FiltresDeBase
        {
            get
            {
                InitFiltres();
                return m_listeFiltresDeBase.ToArray();
            }
        }

        //---------------------------------------
        public CFiltreDonneePrecalculee[] FiltresUtilisateur
        {
            get
            {
                InitFiltres();
                return m_listeFiltresUser.ToArray();
            }
        }

        //---------------------------------------
        public OperationsAgregation OperationCumul
        {
            get
            {
                return m_operationCumul;
            }
            set
            {
                m_operationCumul = value;
            }
        }

        //---------------------------------------
        private void InitFiltres()
        {
            CTypeDonneeCumulee typeDonnee = GetTypeDonneeCumulee(CContexteDonneeSysteme.GetInstance());
            if (typeDonnee == null)
            {
                m_listeFiltresDeBase.Clear();
                m_listeFiltresUser.Clear();
                return;
            }
            CParametreDonneeCumulee parametre = typeDonnee.Parametre;
            Dictionary<string, bool> dicToDelete = new Dictionary<string, bool>();
            foreach (CFiltreDonneePrecalculee filtre in m_listeFiltresDeBase)
                dicToDelete[filtre.ChampAssocie] = true;
            foreach (CFiltreDonneePrecalculee filtre in m_listeFiltresUser)
                dicToDelete[filtre.ChampAssocie] = true;
            foreach (CCleDonneeCumulee cle in parametre.ChampsCle)
            {
                if (cle.TypeLie != null)
                {
                    CFiltreDonneePrecalculee filtre = m_listeFiltresDeBase.FirstOrDefault(f => f.ChampAssocie == cle.Champ);
                    if ( filtre != null )
                        dicToDelete[filtre.ChampAssocie] = false;
                    if (m_listeFiltresDeBase.Count(f => f.ChampAssocie== cle.Champ) == 0)
                    {
                        CFiltreDynamique filtreTmp = new CFiltreDynamique();
                        filtreTmp.TypeElements = cle.TypeLie;
                        m_listeFiltresDeBase.Add ( new CFiltreDonneePrecalculee ( cle.Champ, cle.Champ, filtreTmp ));
                    }
                    if (m_listeFiltresUser.Count(f => f.ChampAssocie== cle.Champ) == 0)
                    {
                        CFiltreDynamique filtreTmp = new CFiltreDynamique();
                        filtreTmp.TypeElements = cle.TypeLie;
                        m_listeFiltresUser.Add ( new CFiltreDonneePrecalculee ( cle.Champ, cle.Champ, filtreTmp ));
                    }
                }
            }
            foreach (KeyValuePair<string, bool> kv in dicToDelete)
            {
                if (kv.Value)
                {
                    foreach (CFiltreDonneePrecalculee filtre in m_listeFiltresUser.FindAll(f => f.ChampAssocie == kv.Key).ToArray())
                        m_listeFiltresUser.Remove(filtre);
                    foreach (CFiltreDonneePrecalculee filtre in m_listeFiltresDeBase.FindAll(f => f.ChampAssocie == kv.Key).ToArray())
                        m_listeFiltresDeBase.Remove(filtre);
                }
            }
        }


        //---------------------------------------
        public CTableauCroise TableauCroise
        {
            get
            {
                return m_tableauCroise;
            }
            set
            {
                m_tableauCroise = value;
            }
        }

        //---------------------------------------
        public int? IdTypeDonneeCumulee
        {
            get
            {
                return m_nIdTypeDonneeCumulee;
            }
            set
            {
                m_nIdTypeDonneeCumulee = value;
                InitFiltres();
            }
        }

        //---------------------------------------
        public DataTable GetDataTableModelePourParametrage(CContexteDonnee contexte)
        {
            if (m_nIdTypeDonneeCumulee == null)
                return null;
            CTypeDonneeCumulee typeDonnee = GetTypeDonneeCumulee(contexte);
            if (typeDonnee == null)
                return null;
            DataTable table = new DataTable();
            CParametreDonneeCumulee parametreDonnee = typeDonnee.Parametre;
            if (parametreDonnee == null)
                return null;
            foreach (CCleDonneeCumulee cle in parametreDonnee.ChampsCle)
            {
                if(  cle.Champ != "" )
                {
                    DataColumn col = new DataColumn(cle.Champ, typeof(string));
                    col.AllowDBNull = true;
                    table.Columns.Add(col);
                }
            }
            foreach (CParametreDonneeCumulee.CNomChampCumule champ in parametreDonnee.NomChampsDecimaux)
            {
                string strChamp = parametreDonnee.GetValueField(champ.NumeroChamp);
                if (strChamp != null && strChamp != "")
                {
                    DataColumn col = new DataColumn(champ.NomChamp, typeof(double));
                    col.AllowDBNull = true;
                    table.Columns.Add(col);
                }
            }
            foreach (CParametreDonneeCumulee.CNomChampCumule champ in parametreDonnee.NomChampsDates)
            {
                string strChamp = parametreDonnee.GetDateField(champ.NumeroChamp);
                if (strChamp != null && strChamp != "")
                {
                    DataColumn col = new DataColumn(champ.NomChamp, typeof(DateTime));
                    col.AllowDBNull = true;
                    table.Columns.Add(col);
                }
            }
            foreach (CParametreDonneeCumulee.CNomChampCumule champ in parametreDonnee.NomChampsTextes)
            {
                string strChamp = parametreDonnee.GetTextField(champ.NumeroChamp);
                if (strChamp != null && strChamp != "")
                {
                    DataColumn col = new DataColumn(champ.NomChamp, typeof(string));
                    col.AllowDBNull = true;
                    table.Columns.Add(col);
                }
            }
            return table;
        }

        public void SetParametreChamp(CParametreVisuChampTableauCroise parametre)
        {
            CParametreVisuChampTableauCroise old = GetParametreForchamp(parametre.ChampFinal);
            m_listeParametresChamps.Remove(old);
            m_listeParametresChamps.Add(parametre);
        }

        public CParametreVisuChampTableauCroise GetParametreForchamp(CChampFinalDeTableauCroise champ)
        {
            CParametreVisuChampTableauCroise parametre = m_listeParametresChamps.FirstOrDefault(p => p.ChampFinal.NomChamp == champ.NomChamp);
            if (parametre == null)
            {
                parametre = new CParametreVisuChampTableauCroise(champ);
                m_listeParametresChamps.Add(parametre);
            }
            return parametre;
        }

        public CParametreVisuChampTableauCroise[] ParametresChamps
        {
            get
            {
                return m_listeParametresChamps.ToArray();
            }
        }


        #region I2iSerializable Membres
        private int GetNumVersion()
        {
            return 3;
            //2 : ajout de ShowExportbutton
            //3 : ajout de "Show header"
        }

        //-----------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            if (m_tableauCroise == null)
                m_tableauCroise = new CTableauCroise();
            result = serializer.TraiteObject<CTableauCroise>(ref m_tableauCroise);

            int nValSafe = m_nIdTypeDonneeCumulee == null ? -1 : m_nIdTypeDonneeCumulee.Value;
            serializer.TraiteInt(ref nValSafe);
            if (nValSafe < 0)
                m_nIdTypeDonneeCumulee = null;
            else
                m_nIdTypeDonneeCumulee = nValSafe;

            if (nVersion > 0)
            {
                int nVal = (int)OperationCumul;
                serializer.TraiteInt(ref nVal);
                OperationCumul = (OperationsAgregation)nVal;
                serializer.TraiteString(ref m_strLibelleTotal);
            }

            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bShowExportButton);
            else
                m_bShowExportButton = true;

            if (nVersion >= 3)
                serializer.TraiteBool(ref m_bShowHeader);
            else
                m_bShowHeader = true;


            if (result)
                result = serializer.TraiteListe<CParametreVisuChampTableauCroise>(m_listeParametresChamps);

            if (result)
                result = serializer.TraiteObject<CFormatChampTableauCroise>(ref m_formatRows);
            if (result)
                result = serializer.TraiteObject<CFormatChampTableauCroise>(ref m_formatHeader);
            if ( result )
                result = serializer.TraiteListe<CFiltreDonneePrecalculee>(m_listeFiltresDeBase);
            if (result)
                result = serializer.TraiteListe<CFiltreDonneePrecalculee>(m_listeFiltresUser);

            return result;
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// Le data du result contient le datatable correspondant à ce paramètre de visu
        /// </summary>
        /// <returns></returns>
        public CResultAErreur GetDataTable(CContexteDonnee contexteDonnee)
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeDonneeCumulee typeDonnee = GetTypeDonneeCumulee(contexteDonnee);
            if (typeDonnee == null)
            {
                result.EmpileErreur(I.T("Unable to load Precalculated data type|20041"));
                return result;
            }
            CListeObjetsDonnees lstDatas = new CListeObjetsDonnees(
                contexteDonnee, 
                typeof(CDonneeCumulee), 
                new CFiltreData(CTypeDonneeCumulee.c_champId + "=@1",m_nIdTypeDonneeCumulee) );

            CParametreDonneeCumulee parametre = typeDonnee.Parametre;

            #region Filtrage des données
            CFiltreData filtreDonnees = null;
            int nCle = 0;
            foreach (CCleDonneeCumulee cle in parametre.ChampsCle)
            {
                if (cle.Champ != null && cle.Champ != "")
                {
                    CFiltreDonneePrecalculee filtreBase = m_listeFiltresDeBase.FirstOrDefault(f => f.ChampAssocie == cle.Champ);
                    CFiltreDonneePrecalculee filtreUser = m_listeFiltresUser.FirstOrDefault(f => f.ChampAssocie == cle.Champ);
                    CFiltreDynamique filtreDynBase = filtreBase != null ? filtreBase.Filtre : null;
                    CFiltreDynamique filtreDynUser = filtreUser != null ? filtreUser.Filtre : null; 
                    CFiltreData filtre = null;
                    if (filtreDynUser != null && filtreDynUser.ComposantPrincipal != null)
                    {
                        result = filtreDynUser.GetFiltreData();
                        if (result)
                            filtre = result.Data as CFiltreData;
                    }
                    if (filtreDynBase != null &&  filtreDynBase.ComposantPrincipal != null)
                    {
                        result = filtreDynBase.GetFiltreData();
                        if (result)
                            filtre = CFiltreData.GetAndFiltre(filtre, result.Data as CFiltreData);
                    }
                    if (filtre != null && filtre.HasFiltre)
                    {
                        //Crée une liste d'objets correspondant au filtre
                        CListeObjetsDonnees lst = new CListeObjetsDonnees(contexteDonnee, cle.TypeLie);
                        lst.Filtre = filtre;
                        StringBuilder bl = new StringBuilder();
                        foreach (CObjetDonneeAIdNumerique obj in lst)
                        {
                            bl.Append(obj.Id);
                            bl.Append(',');
                        }
                        filtre = null;
                        if (bl.Length == 0)
                        {
                            filtre = new CFiltreDataImpossible();
                            filtreDonnees = new CFiltreDataImpossible();
                        }
                        else
                        {
                            bl.Remove(bl.Length - 1, 1);
                            filtre = new CFiltreData(CDonneeCumulee.GetNomChampCle(nCle) + " in (" + bl.ToString() + ")");
                            filtreDonnees = CFiltreData.GetAndFiltre(filtreDonnees, filtre);
                        }
                    }
                }
                nCle++;
            }
            #endregion
            if (filtreDonnees != null)
                lstDatas.Filtre = filtreDonnees;


            //Crée le datatable de base
            DataTable table = new DataTable();
            
            Dictionary<string, string> dicChampsACopier = new Dictionary<string, string>();
            nCle = 0;
            foreach ( CCleDonneeCumulee cle in parametre.ChampsCle )
            {
                if ( cle.Champ != "" )
                {
                    DataColumn col = new DataColumn(cle.Champ, typeof(string));
                    table.Columns.Add ( col );
                    dicChampsACopier[CDonneeCumulee.GetNomChampCle(nCle)] = col.ColumnName;
                }
                nCle++;
            }
            foreach ( CParametreDonneeCumulee.CNomChampCumule nom in parametre.NomChampsDecimaux )
            {
                if ( nom.NomChamp != "" )
                {
                    DataColumn col = new DataColumn ( nom.NomChamp, typeof(double));
                    table.Columns.Add ( col );
                    dicChampsACopier[CDonneeCumulee.GetNomChampValeur(nom.NumeroChamp)] = nom.NomChamp;
                }
            }
            foreach (CParametreDonneeCumulee.CNomChampCumule nom in parametre.NomChampsDates)
            {
                if (nom.NomChamp != "")
                {
                    DataColumn col = new DataColumn(nom.NomChamp, typeof(DateTime));
                    table.Columns.Add(col);
                    dicChampsACopier[CDonneeCumulee.GetNomChampDate(nom.NumeroChamp)] = nom.NomChamp;
                }
            }
            foreach (CParametreDonneeCumulee.CNomChampCumule nom in parametre.NomChampsTextes)
            {
                if (nom.NomChamp != "")
                {
                    DataColumn col = new DataColumn(nom.NomChamp, typeof(string));
                    table.Columns.Add(col);
                    dicChampsACopier[CDonneeCumulee.GetNomChampTexte(nom.NumeroChamp)] = nom.NomChamp;
                }
            }

            foreach (CDonneeCumulee donnee in lstDatas)
            {
                DataRow rowSource = donnee.Row.Row;
                DataRow rowDest = table.NewRow();
                foreach (KeyValuePair<string, string> kv in dicChampsACopier)
                    rowDest[kv.Value] = rowSource[kv.Key];
                table.Rows.Add(rowDest);
            }
            result = m_tableauCroise.CreateTableCroisee(table);

            if (OperationCumul != OperationsAgregation.None)
            {
                DataTable tableFinale = result.Data as DataTable;
                DataRow row = tableFinale.NewRow();
                bool bHeaderFait = false;
                foreach (DataColumn col in tableFinale.Columns)
                {
                    CChampFinalDeTableauCroiseDonnee champDonnee = col.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] as CChampFinalDeTableauCroiseDonnee;
                    if (champDonnee != null)
                    {
                        CDonneeAgregation donnee = CDonneeAgregation.GetNewDonneeForOperation(OperationCumul);
                        donnee.PrepareCalcul();
                        foreach (DataRow rowTmp in tableFinale.Rows)
                        {
                            donnee.IntegreDonnee(rowTmp[col]);
                        }
                        row[col] = donnee.GetValeurFinale();
                    }
                    else if (!bHeaderFait)
                    {
                        row[col] = m_strLibelleTotal;
                        bHeaderFait = true;
                    }
                }
                tableFinale.Rows.Add(row);                
            }
            return result;
        }


        #endregion

        //----------------------------------------------------------------------------------
        public CResultAErreur ReadFromFile(string strNomFichier)
        {
            CResultAErreur result = CResultAErreur.True;
            FileStream stream = null;
            try
            {
                stream = new FileStream(strNomFichier, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error while opening file|109"));
            }
            try
            {
                BinaryReader reader = new BinaryReader(stream);
                string strId = reader.ReadString();
                if (strId != c_idFichier)
                {
                    result.EmpileErreur(I.T("The file doesn't contain a valid vizualisation parameter|20042"));
                    return result;
                }
                CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                result = Serialize(serializer);
                reader.Close();
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error while reading the file|110"));
            }
            finally
            {
                try
                {
                    stream.Close();
                }
                catch { }
            }
            return result;
        }

        //----------------------------------------------------------------------------------
        public CResultAErreur SaveToFile(string strNomFichier)
        {
            CResultAErreur result = CResultAErreur.True;
            FileStream stream = null;
            try
            {
                stream = new FileStream(strNomFichier, FileMode.Create, FileAccess.Write, FileShare.None);
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error while opening the file|109"));
            }
            try
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(c_idFichier);
                CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                result = Serialize(serializer);
                writer.Close();
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error while writing the file|112"));
            }
            finally
            {
                try
                {
                    stream.Close();
                }
                catch { }
            }
            return result;
        }

        public CTypeDonneeCumulee GetTypeDonneeCumulee(CContexteDonnee ctx)
        {
            if (m_nIdTypeDonneeCumulee == null)
                return null;
            CTypeDonneeCumulee typeDonnee = new CTypeDonneeCumulee(ctx);
            if (typeDonnee.ReadIfExists(m_nIdTypeDonneeCumulee.Value))
                return typeDonnee;
            return null;
        }

        /// <summary>
        /// Prépare le paramètre de visu pour afficher les données demandées
        /// fait les chargements à faire, et tout et tout
        /// </summary>
        /// <param name="table"></param>
        public CResultAErreur PrepareAffichageDonnees(
            DataTable table,
            CContexteDonnee contexte)
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeDonneeCumulee typeDonnee = GetTypeDonneeCumulee(contexte);
            if (typeDonnee == null)
                result.EmpileErreur(I.T("Can not find Cumulated data type|20041"));
            CParametreDonneeCumulee parametreDonnee = typeDonnee.Parametre;
            foreach (CParametreVisuChampTableauCroise paramVisu in ParametresChamps)
            {
                result = paramVisu.PrepareAffichageDonnees(table, contexte, parametreDonnee, this);
                if (!result)
                    return result;
            }
            return result;
        }

        public void PutElementInCache(CObjetDonneeAIdNumerique objet)
        {
            if ( objet != null )
                m_cache.AddElement(objet);
        }

        public CObjetDonneeAIdNumerique GetFromCache(Type type, int nId)
        {
            return m_cache.GetElement(type, nId);
        }

       
    }
}
