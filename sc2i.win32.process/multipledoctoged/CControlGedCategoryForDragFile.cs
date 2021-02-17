using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.documents;
using System.IO;
using sc2i.common;

namespace sc2i.win32.process.multipledoctoged
{
    public partial class CControlGedCategoryForDragFile : UserControl
    {
        private CCategorieGED m_categorie = null;
        private List<string> m_listeFichiers = new List<string>();
        private int m_nNiveau = 0;


        public CControlGedCategoryForDragFile()
        {
            InitializeComponent();
        }

        public CCategorieGED CategorieGed
        {
            get
            {
                return m_categorie;
            }
        }

        public IEnumerable<string> SelectedFiles
        {
            get
            {
                return m_listeFichiers.AsReadOnly();
            }
        }

        public void Init(CCategorieGED categorie, int nNiveau)
        {
            m_categorie = categorie;
            CalculeLabel();
            if (nNiveau == 0)
                m_panelMarge.Visible = false;
            else
            {
                m_panelMarge.Visible = true;
                m_panelMarge.Width = 22 * nNiveau;
            }
        }

        private void CalculeLabel()
        {
            string strLabel = m_categorie != null ? m_categorie.Libelle : "";
            strLabel += " (" + m_listeFichiers.Count + " ";
            strLabel += I.T("file(s)|20142") + ")";
            m_lblCategorie.Text = strLabel;
        }

        private void m_lblCategorie_DragEnter(object sender, DragEventArgs e)
        {
            string[] strFiles = e.Data.GetData(typeof(string[])) as string[];
            if (strFiles != null && strFiles.Length > 0)
            {
                if (File.Exists(strFiles[0]))
                {
                    e.Effect = DragDropEffects.Link;
                    return;
                }
            }
                e.Effect = DragDropEffects.None;
        }

        private void m_lblCategorie_DragDrop(object sender, DragEventArgs e)
        {
            string[] strFiles = e.Data.GetData(typeof(string[])) as string[];
            if (strFiles != null)
            {
                AddFiles(strFiles);
            }
        }



        private void m_lblCategorie_MouseUp(object sender, MouseEventArgs e)
        {
             ContextMenuStrip menu = new ContextMenuStrip();
            foreach ( string strFile in m_listeFichiers )
            {
                ToolStripMenuItem itemFile = new ToolStripMenuItem(Path.GetFileName(strFile));
                itemFile.Image = sc2i.win32.process.Properties.Resources.delete;
                menu.Items.Add ( itemFile );
                itemFile.Tag = strFile;
                itemFile.Click += new EventHandler(itemFile_Click);
            }
            if ( menu.Items.Count > 0 )
            {
                menu.Show ( m_lblCategorie, new Point ( 0, m_lblCategorie.Height) );
            }
        }

        //----------------------------------------------------------
        void itemFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string strFile = item != null ? item.Tag as string : null;
            if (strFile != null)
            {
                m_listeFichiers.Remove(strFile);
                CalculeLabel();
            }
        }

        public event EventHandler OnClickAddButton;

        private void m_btnAddFile_Click(object sender, EventArgs e)
        {
            if (OnClickAddButton != null)
                OnClickAddButton(this, null);
        }

        //-------------------------------------------------
        public void AddFiles(IEnumerable<string> strFiles)
        {
            foreach (string strFile in strFiles)
            {
                if (File.Exists(strFile))
                {
                    if (!m_listeFichiers.Contains(strFile))
                    {
                        m_listeFichiers.Add(strFile);
                    }
                }
            }
            CalculeLabel();
        }
    }
}
