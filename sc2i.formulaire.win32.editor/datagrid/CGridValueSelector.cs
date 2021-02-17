using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.datagrid.Filters;

namespace sc2i.formulaire.win32.datagrid
{
    public partial class CGridValueSelector : UserControl
    {
        private string[] m_strValues = new string[0];
        private HashSet<string> m_checkedValues = new HashSet<string>();

        public CGridValueSelector()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        public event EventHandler OnOkClicked;

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (OnOkClicked != null)
                OnOkClicked(this, null);
        }

        public void FillWithValues(CCoupleValeurEtValeurDisplay[] values, CGridFilterListeValeurs filtre)
        {
            HashSet<string> setValues = new HashSet<string>();
            foreach (CCoupleValeurEtValeurDisplay cpl in values)
            {
                setValues.Add(cpl.StringValue);
            }
            List<string> lst = new List<string>(setValues.ToArray());
            lst.Sort();
            m_strValues = lst.ToArray();
            m_checkedValues = new HashSet<string>();
            if (filtre != null)
                foreach (string strVal in filtre.ListeValeurs)
                    m_checkedValues.Add(strVal);
            else
                foreach (string strVal in lst)
                    m_checkedValues.Add(strVal);
            m_txtSearch.Text = "";
            RefillListe();
        }

        private void RefillListe()
        {
            m_bIsFilling = true;
            m_wndListeItems.BeginUpdate();
            m_wndListeItems.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            ListViewItem item = new ListViewItem(I.T("(Select all)|20031"));
            item.Tag = DBNull.Value;
            items.Add(item);
            string strSearch = m_txtSearch.Text;
            foreach (string strVal in m_strValues)
            {
                if (strSearch.Length == 0 || strVal.Contains(strSearch))
                {
                    item = new ListViewItem(strVal);
                    item.Tag = strVal;
                    items.Add(item);
                    item.Checked = m_checkedValues.Contains(strVal);
                }
            }
            m_wndListeItems.Items.AddRange(items.ToArray());
            m_wndListeItems.EndUpdate();
            m_bIsFilling = false;
        }


        private bool m_bIsFilling = false;
        private void m_wndListeItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!m_bIsFilling )
            {
                try
                {
                    if (e.Item.Tag == DBNull.Value)
                    {
                        CheckAll(e.Item.Checked);
                    }
                    else
                    {
                        if (e.Item.Checked && e.Item.Tag is string)
                            m_checkedValues.Add((string)e.Item.Tag);
                        else
                            m_checkedValues.Remove((string)e.Item.Tag);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void CheckAll(bool bCheck)
        {
            m_bIsFilling = true;
            //m_wndListeItems.BeginUpdate();
            foreach (ListViewItem item in m_wndListeItems.Items)
            {
                if (item != null)
                {
                    item.Checked = bCheck;
                    if (item.Tag is string)
                    {
                        if (bCheck)
                            m_checkedValues.Add((string)item.Tag);
                        else
                            m_checkedValues.Remove((string)item.Tag);
                    }
                }
            }
            //m_wndListeItems.EndUpdate();
            m_bIsFilling = false;
        }

        private void m_txtSearch_TextChanged(object sender, EventArgs e)
        {
            RefillListe();
        }

        public CGridFilterForWndDataGrid GetFiltre()
        {
            if (m_checkedValues.Count() == m_strValues.Length)
                return null;
            CGridFilterListeValeurs filtre = new CGridFilterListeValeurs();
            filtre.ListeValeurs = m_checkedValues;
            return filtre;
        }

    }
}
