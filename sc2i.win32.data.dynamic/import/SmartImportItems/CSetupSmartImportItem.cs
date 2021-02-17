using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.expression;
using sc2i.win32.common.customizableList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.win32.data.dynamic.import
{
    

    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    public abstract class CSetupSmartImportItem : CCustomizableListItemANiveau
    {
        private CDefinitionProprieteDynamique m_propriete = null;
        private int m_nColorIndex = 0;
        private bool m_bIsCollapse = true;

        //-------------------------------------------------------------------
        public CSetupSmartImportItem(
            CSetupSmartImportItem parentItem,
            CDefinitionProprieteDynamique def,
            int nColorIndex)
            : base(parentItem)
        {
            m_propriete = def;
            m_nColorIndex = nColorIndex;

        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Retourne l'objet exemple associé à l'item si celui ci est importé.
        /// </summary>
        public abstract CObjetDonnee ObjetExempleAssocie { get; }

        //-------------------------------------------------------------------
        public abstract string IdMappage { get; }

        #region Gestion des couleurs

        private static List<Color> m_listeCouleurs = new List<Color>();
        //-------------------------------------------------------------------------------
        public static int NbColors
        {
            get
            {
                AssureCouleurs();
                return m_listeCouleurs.Count;
            }
        }

        //-------------------------------------------------------------------------------
        private static void AssureCouleurs()
        {
            if (m_listeCouleurs.Count == 0)
            {
                m_listeCouleurs.Add(Color.LightBlue);
                m_listeCouleurs.Add(Color.LightGreen);
                m_listeCouleurs.Add(Color.LightCyan);
                m_listeCouleurs.Add(Color.LightCoral);
                m_listeCouleurs.Add(Color.LightSalmon);
                m_listeCouleurs.Add(Color.LightSeaGreen);
                m_listeCouleurs.Add(Color.LightYellow);
            }
        }

        //-------------------------------------------------------------------------------
        public static Color GetCouleur(int nIndex)
        {
            AssureCouleurs();
            return m_listeCouleurs[nIndex % m_listeCouleurs.Count];
        }

        #endregion

        //-------------------------------------------------------------------
        public int ColorIndex
        {
            get
            {
                return m_nColorIndex;
            }
            set { m_nColorIndex = value; }
        }

        //-------------------------------------------------------------------
        public Color BackColor
        {
            get
            {
                return GetCouleur(m_nColorIndex);
            }
        }

        //-------------------------------------------------------------------
        public bool Highlight { get; set; }

        //-------------------------------------------------------------------
        public CDefinitionProprieteDynamique Propriete
        {
            get
            {
                return m_propriete;
            }
            set { m_propriete = value; }
        }

        //-------------------------------------------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamique> Chemin
        {
            get
            {
                List<CDefinitionProprieteDynamique> lstChemins = new List<CDefinitionProprieteDynamique>();
                FillChemins(lstChemins);
                return lstChemins.ToArray();
            }
        }

        //-------------------------------------------------------------------------------
        protected void FillChemins(List<CDefinitionProprieteDynamique> lstChemins)
        {
            CSetupSmartImportItem parent = ItemParent as CSetupSmartImportItem;
            if (parent != null)
                parent.FillChemins(lstChemins);
            if (Propriete != null)
                lstChemins.Add(Propriete);
        }
        //-------------------------------------------------------------------
        public override bool IsCollapse
        {
            get { return m_bIsCollapse; }
        }

        //-------------------------------------------------------------------
        public void Expand()
        {
            foreach (CCustomizableListItemANiveau item in ChildItems)
            {
                item.IsMasque = false;
                item.Height = null;
            }
            m_bIsCollapse = false;
        }

        //-------------------------------------------------------------------
        public void Collapse()
        {
            foreach (CSetupSmartImportItem item in ChildItems)
            {
                item.IsMasque = true;
                item.Collapse();
            }
            m_bIsCollapse = true;
        }

        //-------------------------------------------------------------------
        public override bool OnChangeParent(CCustomizableListItemANiveau item)
        {
            return false;
        }

        //-------------------------------------------------------------------
        public abstract string LibelleValeur { get; }

        //-------------------------------------------------------------------
        public abstract bool UseAsKey { get; set; }
        //-------------------------------------------------------------------
        public abstract bool CanBeUsedAsKey { get; }

        //-------------------------------------------------------------------
        public abstract CSourceSmartImport Source { get; set; }
        //-------------------------------------------------------------------
        public abstract IEnumerable<Type> SourcesPossibles { get; }

        //-------------------------------------------------------------------
        public abstract EOptionCreationElementImport OptionCreation { get; set; }
        //-------------------------------------------------------------------
        public abstract bool CanHaveOptionCreation { get; }

        //-------------------------------------------------------------------
        public abstract bool IsEntite { get; }

        //-------------------------------------------------------------------
        public abstract bool IsExpandable { get; }

        //-------------------------------------------------------------------------------
        public abstract object ValeurParDefaut { get; }

        //-------------------------------------------------------------------------------
        public abstract CResultAErreur MajConfig(CConfigMappagesSmartImport config);

        //-------------------------------------------------------------------------------
        public abstract bool HasConfigData();
    }




}
