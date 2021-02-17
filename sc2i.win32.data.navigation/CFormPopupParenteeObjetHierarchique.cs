using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.data.navigation
{
    public partial class CFormPopupParenteeObjetHierarchique : CFloatingFormBase
    {
        public CFormPopupParenteeObjetHierarchique()
        {
            InitializeComponent();
        }


        //-----------------------------------------------------------------
        public void Show(IObjetDonneeAutoReferenceNavigable objet)
        {
            if (objet == null)
                return;
            m_wndListe.Items.Clear();
            m_wndListe.BeginUpdate();
            IObjetDonneeAutoReferenceNavigable parent = objet.ObjetAutoRefParent as IObjetDonneeAutoReferenceNavigable;
            int nHeight = 0;

            while (parent != null)
            {
                ListViewItem item = new ListViewItem(parent.Libelle);
                item.Tag = parent;
                m_wndListe.Items.Insert(0, item);
                parent = parent.ObjetAutoRefParent as IObjetDonneeAutoReferenceNavigable;
                nHeight += m_wndListe.GetItemRect(0).Height+1;
            }
            Height = nHeight + 4;
            m_wndListe.EndUpdate();
            AutoPlacement = false;
            Show();
        }

        //-----------------------------------------------------------------
        private void m_wndListe_ItemClick(ListViewItem item)
        {
            CObjetHierarchique obj = item != null ? item.Tag as CObjetHierarchique : null;
            if (obj != null)
            {
                CSc2iWin32DataNavigation.Navigateur.EditeElement(obj, false, "");
                Close();
            }
        }
    }
}
