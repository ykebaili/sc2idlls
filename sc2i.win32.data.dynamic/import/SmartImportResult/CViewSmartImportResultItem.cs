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
    public class CViewSmartImportResultItem :  CCustomizableListItemANiveau
    {
        private CDefinitionProprieteDynamique m_propriete = null;
        private object m_valeur = null;
        private object m_valeurOriginale = null;
        private bool m_bIsCollapse = true;
        private int m_nColorIndex = 0;
        
        //--------------------------------------------------
        public CViewSmartImportResultItem(
            CViewSmartImportResultItem parent, 
            CDefinitionProprieteDynamique def,
            object valeur,
            object valeurOriginale,
            int nColorIndex)
            :base ( parent )
        {
            m_propriete = def;
            m_valeur = valeur;
            m_valeurOriginale = valeurOriginale;
            m_nColorIndex = nColorIndex;
        }

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

        //--------------------------------------------------
        public CDefinitionProprieteDynamique Propriete
        {
            get
            {
                return m_propriete;
            }
            set { m_propriete = value; }
        }

        //--------------------------------------------------
        public object Valeur
        {
            get
            {
                return m_valeur;
            }
            set { m_valeur = value; }
        }

        //-------------------------------------------------------------------
        public object ValeurOriginale
        {
            get
            {
                return m_valeurOriginale;
            }
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
                item.IsMasque = false;
            m_bIsCollapse = false;
        }

        //-------------------------------------------------------------------
        public void Collapse()
        {
            foreach (CViewSmartImportResultItem item in ChildItems)
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






    }
}
