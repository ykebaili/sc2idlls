using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common.customizableList;
using sc2i.data.dynamic;
using sc2i.common;

namespace sc2i.win32.data.navigation
{
    public partial class CControlPourListeDeListePanelSpeed : CCustomizableListControl
    {
        public class CItemListe : CCustomizableListItem
        {
            public readonly CListeEntites ListeEntites = null;
            public readonly int NbElements = 0;

            private bool m_bIsSelected = false;

            //-----------------------------------------------------
            public CItemListe ( CListeEntites lst, int nNbElements )
            {
                ListeEntites = lst;
                NbElements = nNbElements;
            }

            //-----------------------------------------------------
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
        }

        //-----------------------------------------------------
        public CControlPourListeDeListePanelSpeed()
        {
            InitializeComponent();
        }


        //-----------------------------------------------------
        public override bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        //-----------------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result = base.MyInitChamps(item);
            CItemListe itemList = item as CItemListe;
            if (itemList != null)
            {
                m_lnkListe.Text = itemList.ListeEntites.Libelle + " (" +
                    itemList.NbElements + ")";
                m_picSelected.Visible = itemList.IsSelected;
            }
            return result;
            
        }

        //-----------------------------------------------------
        public delegate void SelectListEventHandler ( CListeEntites liste );

        public event SelectListEventHandler OnSelectList;

        //-----------------------------------------------------
        private void m_lnkListe_Click(object sender, EventArgs e)
        {
            CItemListe item = CurrentItem as CItemListe;
            if (item != null && OnSelectList != null)
                OnSelectList(item.ListeEntites);
        }
    }
}
