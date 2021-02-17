using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
   

    //Contexte d'import de donnée
    public class CContexteImportDonnee
    {
        private class CFiltreImportToEntite
        {
            //Pour chaque "clé" de filtre : indique les entités correspondantes
            private class CCacheEntitesDeType : Dictionary<string, CObjetDonnee>
            {
                public CCacheEntitesDeType()
                { }
            }

            private Dictionary<Type, CCacheEntitesDeType> m_dicEntitesParType = new Dictionary<Type, CCacheEntitesDeType>();

            public void SetEntite(string strCleFiltre, CObjetDonnee objet)
            {
                if (objet == null)
                    return;
                CCacheEntitesDeType cache = null;
                if (!m_dicEntitesParType.TryGetValue(objet.GetType(), out cache))
                {
                    cache = new CCacheEntitesDeType();
                    m_dicEntitesParType[objet.GetType()] = cache;
                }
                cache[strCleFiltre] = objet;
            }

            public CObjetDonnee GetEntite(Type typeEntite, string strCleFiltre)
            {
                if (typeEntite == null)
                    return null;
                CCacheEntitesDeType cache = null;
                if (m_dicEntitesParType.TryGetValue(typeEntite, out cache))
                {
                    if (cache != null)
                    {
                        CObjetDonnee objet = null;
                        if (cache.TryGetValue(strCleFiltre, out objet))
                            return objet;
                    }
                }
                return null;
            }
        }

        private DataTable m_tableSource = null;

        private CContexteDonnee m_contexteDestination = null;
        private CFiltreImportToEntite m_entitesFromFiltre = new CFiltreImportToEntite();

        private Dictionary<string, CObjetDonnee> m_dicIdMappageToRefImportee = new Dictionary<string, CObjetDonnee>();

        private List<CLigneLogImport> m_listeLogs = new List<CLigneLogImport>();

        private Stack<CDefinitionProprieteDynamique> m_pileChampsEnCours = new Stack<CDefinitionProprieteDynamique>();

        private Dictionary<DataRow, DataRow> m_dicRowSourceToTemoinDest = new Dictionary<DataRow, DataRow>();

        private Stack<string> m_stackIdMappages = new Stack<string>();

        //Stocke les informations au fur et à mesure de l'import
        private DataRow m_currentRowTemoin = null;

        private int m_nIndexCurrentRow = 0;

        private CElementsAjoutesPendantImport m_elementsAjoutes = new CElementsAjoutesPendantImport();

        private List<CInfoImportLigne> m_listeInfosImport = new List<CInfoImportLigne>();

        //Options d'import
        private int? m_nStartLine = null;
        private int? m_nEndLine = null;
        private bool m_bBestEffort = true;



        //---------------------------------------------------
        public CContexteImportDonnee()
        {

        }



        //---------------------------------------------------
        public CContexteImportDonnee(CContexteDonnee contexteDestination)
        {
            m_contexteDestination = contexteDestination;
        }

        //---------------------------------------------------
        public int? StartLine
        {
            get
            {
                return m_nStartLine;
            }
            set
            {
                m_nStartLine = value;
            }
        }

        //---------------------------------------------------
        public int? EndLine
        {
            get
            {
                return m_nEndLine;
            }
            set
            {
                m_nEndLine = value;
            }
        }

        //---------------------------------------------------
        public bool BestEffort
        {
            get
            {
                return m_bBestEffort;
            }
            set
            {
                m_bBestEffort = value;
            }
        }

        //---------------------------------------------------
        public DataTable TableSource
        {
            get
            {
                return m_tableSource;
            }
            set
            {
                m_tableSource = value;
            }
        }

        //---------------------------------------------------
        public void StartImportRow ( DataRow row, int nIndexRow)
        {
            if ( row != null )
            {
                m_currentRowTemoin = row.Table.NewRow();
                m_dicRowSourceToTemoinDest[row] = m_currentRowTemoin;
                m_nIndexCurrentRow = nIndexRow;
            }
        }

        //---------------------------------------------------
        public void AddElementQuiAEteAjoute ( CObjetDonnee objet )
        {
            m_elementsAjoutes.SetElementAjoute(objet);
        }

        //---------------------------------------------------
        public CElementsAjoutesPendantImport ElementsAjoutes
        {
            get
            {
                return m_elementsAjoutes;
            }
        }


        //---------------------------------------------------
        public void SetElementPourLigne(int nLigne, CObjetDonnee objet)
        {
            m_listeInfosImport.Add(new CInfoImportLigne(nLigne, objet));
        }

        //---------------------------------------------------
        public IEnumerable<CInfoImportLigne> ElementsRootDeLignes
        {
            get
            {
                return m_listeInfosImport.AsReadOnly();
            }
        }

        //---------------------------------------------------
        public int CurrentRowIndex
        {
            get
            {
                return m_nIndexCurrentRow;
            }
        }


        //---------------------------------------------------
        public void SetValeurTemoin ( string strColonne, object valeur )
        {
            if (m_currentRowTemoin != null )
            {
                try
                {
                    if (valeur != null && valeur != DBNull.Value)
                        m_currentRowTemoin[strColonne] = Convert.ChangeType(valeur, m_currentRowTemoin.Table.Columns[strColonne].DataType);
                    else
                        m_currentRowTemoin[strColonne] = DBNull.Value;  
                }
                catch { }
            }
        }

        //---------------------------------------------------
        public DataRow GetRowTemoin ( DataRow row )
        {
            DataRow rowTemoin = null;
            m_dicRowSourceToTemoinDest.TryGetValue(row, out rowTemoin);
            return rowTemoin;
        }

        //---------------------------------------------------
        public void PushChamp(CDefinitionProprieteDynamique def)
        {
            m_pileChampsEnCours.Push(def);
        }

        //---------------------------------------------------
        public void PopChamp()
        {
            m_pileChampsEnCours.Pop();
        }

        //---------------------------------------------------
        public void PushIdConfigMappage ( string strId )
        {
            m_stackIdMappages.Push(strId);
        }

        //---------------------------------------------------
        public void PopIdConfigMappage()
        {
            m_stackIdMappages.Pop();
        }

        //---------------------------------------------------
        public string IdsConfigsEnCours
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                foreach ( string strId in m_stackIdMappages)
                {
                    bl.Append(strId);
                    bl.Append(".");
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                return bl.ToString();
            }
        }

        //---------------------------------------------------
        public string ChampEnCours
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                foreach (CDefinitionProprieteDynamique def in m_pileChampsEnCours.ToArray())
                {
                    if (def != null)
                        bl.Append(def.Nom);
                    bl.Append(".");
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                return bl.ToString();
            }
        }

        //---------------------------------------------------
        public CContexteDonnee ContexteDonnee
        {
            get
            {
                return m_contexteDestination;
            }
        }

        //---------------------------------------------------
        public IEnumerable<CLigneLogImport> Logs
        {
            get
            {
                return m_listeLogs.AsReadOnly();
            }
        }

        //---------------------------------------------------
        public void AddLog(CLigneLogImport log)
        { 
            m_listeLogs.Add(log); 
        }

        //---------------------------------------------------
        public void SetEntiteForFiltre(string strCleFiltre, CObjetDonnee objet)
        {
            m_entitesFromFiltre.SetEntite(strCleFiltre, objet);
        }

        //---------------------------------------------------
        public CObjetDonnee GetEntiteForFiltre(Type typeEntite, string strCleFiltre)
        {
            return m_entitesFromFiltre.GetEntite(typeEntite, strCleFiltre);
        }

        //---------------------------------------------------
        public void SetObjetImporteForIdMappage(string strIdMappage, CObjetDonnee objetImporte)
        {
            m_dicIdMappageToRefImportee[strIdMappage] = objetImporte;
        }

        //---------------------------------------------------
        public CObjetDonnee GetObjetImporteForIdMappage(string strIdMappage)
        {
            CObjetDonnee objet = null;
            m_dicIdMappageToRefImportee.TryGetValue(strIdMappage, out objet);
            return objet;
        }
    }
}
