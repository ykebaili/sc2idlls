using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire.subform;
using sc2i.common;
using sc2i.win32.common;
using sc2i.drawing;

namespace sc2i.formulaire.win32
{
    public partial class CFormSelect2iWndReference : Form
    {
        private C2iWndReference m_wndRef = null;
        //----------------------------------------------
        public CFormSelect2iWndReference()
        {
            InitializeComponent();
        }

        //----------------------------------------------
        public static C2iWndReference SelectForm(C2iWndReference wndRef)
        {
            using (CFormSelect2iWndReference frm = new CFormSelect2iWndReference())
            {
                frm.m_wndRef = wndRef;
                if (frm.ShowDialog() == DialogResult.OK)
                    return frm.m_wndRef;
                return null;
            }
        }

        //----------------------------------------------
        public void InitForm ( bool bInitSearchWords)
        {
            m_timerSearch.Stop();
            using (CWaitCursor waiter = new CWaitCursor())
            {
                IEnumerable<C2iWndReference> lst = C2iWndProvider.GetAvailable2iWnd();
                m_wndListeForms.BeginUpdate();
                m_wndListeForms.Items.Clear();
                AutoCompleteStringCollection lstMots = new AutoCompleteStringCollection();
                foreach (C2iWndReference reference in lst)
                {
                    if (m_txtSearch.Text.Length > 0 &&
                        !reference.WndLabel.ToUpper().Contains(m_txtSearch.Text.ToUpper()))
                        continue;
                    ListViewItem item = new ListViewItem(reference.WndLabel);
                    item.Tag = reference;
                    m_wndListeForms.Items.Add(item);
                    if (reference == m_wndRef)
                        item.Selected = true;
                    if (bInitSearchWords)
                        FillMots(reference.WndLabel, lstMots);
                }
                if (bInitSearchWords)
                {
                    m_txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    m_txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
                    m_txtSearch.AutoCompleteCustomSource = lstMots;
                }
                m_wndListeForms.EndUpdate();
            }
        }

        //----------------------------------------------
        private void FillMots(string strPhrase, AutoCompleteStringCollection lstMots)
        {
            lstMots.Add(strPhrase);
            string strSeps = " _,.-";
            int? nIndex = null;
            foreach ( char strSep in strSeps )
            {
                int nTmp = strPhrase.IndexOf ( strSep);
                if ( nTmp >0 && (nIndex == null || nTmp < nIndex.Value ))
                    nIndex = nTmp;
            }
            if ( nIndex != null )
                FillMots ( strPhrase.Substring(nIndex.Value+1), lstMots);
        }


        //----------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_wndListeForms.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeForms.SelectedItems[0];
                m_wndRef = item.Tag as C2iWndReference;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        //----------------------------------------------
        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            m_wndRef = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //----------------------------------------------
        private void CFormSelect2iWndReference_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            InitForm(true);
        }

        //----------------------------------------------
        private void ShowPreview()
        {
            m_timerPreview.Stop();
            using (CWaitCursor waiter = new CWaitCursor())
            {
                if (m_wndListeForms.SelectedItems.Count == 1)
                {
                    ListViewItem item = m_wndListeForms.SelectedItems[0];
                    C2iWndReference reference = item.Tag as C2iWndReference;
                    if (reference != null)
                    {
                        if (m_wndPreview.Image != null)
                        {
                            m_wndPreview.Image.Dispose();
                            m_wndPreview.Image = null;
                        }
                        m_lblForm.Text = reference.WndLabel;
                        C2iWnd wnd = C2iWndProvider.GetForm(reference);
                        if (wnd != null)
                        {
                            Bitmap bmp = new Bitmap(wnd.Size.Width + 2, wnd.Size.Height + 2);
                            Graphics g = Graphics.FromImage(bmp);
                            CContextDessinObjetGraphique ctx = new CContextDessinObjetGraphique(g);
                            wnd.Draw(ctx);
                            g.Dispose();
                            m_wndPreview.Image = bmp;
                        }
                    }
                }
            }
        }

        //----------------------------------------------
        private void m_wndListeForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_timerPreview.Stop();
            m_timerPreview.Start();
        }

        //----------------------------------------------
        private void m_txtSearch_TextChanged(object sender, EventArgs e)
        {
            m_timerSearch.Stop();
            m_timerSearch.Start();
        }

        //----------------------------------------------
        private void m_timerSearch_Tick(object sender, EventArgs e)
        {
            InitForm(false);
        }

        //----------------------------------------------
        private void m_timerPreview_Tick(object sender, EventArgs e)
        {
            ShowPreview();
        }


        
    }

    //----------------------------------------------
    [AutoExec("Autoexec")]
    public class CSelecteurWndReference : I2iWndReferenceSelector
    {
        public static void Autoexec()
        {
            C2iWndReferenceEditor.SetEditeur(new CSelecteurWndReference());
        }

        public C2iWndReference Select2iWndReference(C2iWndReference wndRef)
        {
            C2iWndReference retour = CFormSelect2iWndReference.SelectForm(wndRef);
            if (retour == null)
                return wndRef;
            return retour;
        }

    }

}
