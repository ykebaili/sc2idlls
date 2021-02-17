using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.win32.common.customizableList
{
    //----------------------------------------------
    public class CCustomizableListItem
    {
        private object m_tag = null;

        private object m_controlTag = null;

        private int? m_nHeight = null;

        private bool m_bIsSelected = false;

        private bool m_bIsMasque = false;

        private int m_nIndex = -1;

        private CDonneesSpecifiquesControleDansCustomList m_donneesControles = null;




        //-----------------------------------------
        public CCustomizableListItem()
        {
        }

        //-----------------------------------------
        public virtual void OnRemoveFromList()
        {
        }

        //-----------------------------------------
        //Hauteur de l'item
        public virtual int? Height
        {
            get
            {
                return m_nHeight;
            }
            set
            {
                m_nHeight = value;
            }
        }

        //-----------------------------------------
        public CDonneesSpecifiquesControleDansCustomList DonneesControles
        {
            get
            {
                return m_donneesControles;
            }
            set
            {
                m_donneesControles = value;
            }
        }

        public virtual Color? ColorInactive
        {
            get
            {
                return null;
            }
        }

        //-----------------------------------------
        public bool IsMasque
        {
            get
            {
                return m_bIsMasque;
            }
            set
            {
                m_bIsMasque = value;
            }
        }

        //-----------------------------------------
        public virtual int Index
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
       
        //-----------------------------------------
        public bool IsSelected
        {
            get
            {
                return m_bIsSelected;
            }
            set
            {
                m_bIsSelected = value;
            }
        }

        //-----------------------------------------
        /// <summary>
        /// Stocke les paramétres du contrôle
        /// </summary>
        public object ControlTag
        {
            get
            {
                return m_controlTag;
            }
            set
            {
                m_controlTag = value;
            }
        }

        //-----------------------------------------
        public object Tag
        {
            get
            {
                return m_tag;
            }
            set
            {
                m_tag = value;
            }
        }
    }

    //----------------------------------------------
    public abstract class CCustomizableListItemANiveau : CCustomizableListItem
    {
        private List<CCustomizableListItemANiveau> m_childsItems = new List<CCustomizableListItemANiveau>();
        private CCustomizableListItemANiveau m_itemParent = null;

        public CCustomizableListItemANiveau(CCustomizableListItemANiveau itemParentInitial)
        {
            m_itemParent = itemParentInitial;
            if (m_itemParent != null)
                m_itemParent.m_childsItems.Add(this);
        }


        public abstract bool IsCollapse { get; }

        public int Niveau
        {
            get
            {
                if (ItemParent == null)
                    return 0;
                return ItemParent.Niveau + 1;
            }
        }

        //--------------------------------------------------------
        public CCustomizableListItemANiveau ItemParent
        {
            get{
                return m_itemParent;
            }
            set
            {
                CCustomizableListItemANiveau oldParent = ItemParent;
                m_itemParent = value;
                if (!OnChangeParent(value))
                {
                    m_itemParent = oldParent;
                    return;
                }
                if (oldParent != null)
                    oldParent.m_childsItems.Remove(this);
                if (value != null && !value.m_childsItems.Contains(this))
                    value.m_childsItems.Add(this);
            }
        }

        //-----------------------------------------
        public IEnumerable<CCustomizableListItemANiveau> ChildItems
        {
            get{
                return m_childsItems.AsReadOnly();
            }
        }

        //-----------------------------------------
        public override void OnRemoveFromList()
        {
            if (ItemParent != null)
                ItemParent.m_childsItems.Remove(this);
        }

        //Retourne true si le setParent a fonctionné
        public abstract bool OnChangeParent(CCustomizableListItemANiveau item);

    }
 
}
