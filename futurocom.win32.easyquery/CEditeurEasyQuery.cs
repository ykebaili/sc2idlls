using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.expression;
using sc2i.win32.common;
using sc2i.drawing;

namespace futurocom.win32.easyquery
{
    public partial class CEditeurEasyQuery : UserControl, IControlALockEdition
    {
        private CEasyQuery m_query = null;
        private bool m_bSaveWithSource = false;

        public CEditeurEasyQuery()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public void Init(CEasyQuery query)
        {
            m_query = query;
            Size sz = new Size(Math.Max(m_query.Size.Width, 3000), Math.Max(m_query.Size.Height, 3000));
            m_query.Size = sz;
            m_arbre.Init(query.ListeSources);
            m_editeur.ObjetEdite = query;
            m_panelVariables.Init(query);
        }

        //---------------------------------------------------------------
        public CEasyQuery Query
        {
            get
            {
                return m_query;
            }
        }

        //---------------------------------------------------------------
        private void m_btnSave_Click(object sender, EventArgs e)
        {
           
        }

        

        //---------------------------------------------------------------
        private void m_btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Easy query|*.esq|All files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CEasyQuery query = m_editeur.ObjetEdite as CEasyQuery;
                CResultAErreur res = CSerializerObjetInFile.ReadFromFile(query, "QUERY", dlg.FileName);
                if (!res)
                {
                    CEasyQueryAvecSource qas = new CEasyQueryAvecSource();
                    CResultAErreur res2 = CSerializerObjetInFile.ReadFromFile(qas, "QUERYWITHSOURCE", dlg.FileName);
                    if (res2)
                    {
                        m_bSaveWithSource = true;
                        query = qas.GetEasyQuerySansSource();
                        res = CResultAErreur.True;
                    }
                }
                else
                    m_bSaveWithSource = false;
                if (!res)
                    CFormAlerte.Afficher(res.Erreur);
                Init(query);
                /*m_editeur.ObjetEdite = query;*/
                m_editeur.Refresh();
            }
        }


        //---------------------------------------------------------------
        private void m_chkCurseur_Click(object sender, EventArgs e)
        {
            m_chkCurseur.Checked = true;
            m_editeur.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
        }

        //---------------------------------------------------------------
        private void m_chkZoom_Click(object sender, EventArgs e)
        {
            m_chkZoom.Checked = true;
            m_editeur.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Zoom;
        }

        //---------------------------------------------------------------
        private void m_chkJointure_Click(object sender, EventArgs e)
        {
            m_chkJointure.Checked = true;
            m_editeur.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Custom;
        }

        //---------------------------------------------------------------------
        private void m_editeur_ModeSourisChanged(object sender, EventArgs e)
        {
            UpdateBoutonsFromModeSouris();
        }

        //---------------------------------------------------------------------
        private void UpdateBoutonsFromModeSouris()
        {
            switch (m_editeur.ModeSouris)
            {
                case sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection:
                    m_chkCurseur.Checked = true;
                    break;
                case sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Zoom:
                    m_chkZoom.Checked = true;
                    break;
                case sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Custom:
                    switch (m_editeur.ModeSourisCustom)
                    {
                        case CEditeurObjetsEasyQuery.EModeSourisCustom.Join:
                            m_chkJointure.Checked = true;
                            break;
                    }
                    break;
            }
        }

        //---------------------------------------------------------------
        private void m_btnCopy_Click(object sender, EventArgs e)
        {

        }

        



        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        #region IControlALockEdition Membres

        bool IControlALockEdition.LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        event EventHandler IControlALockEdition.OnChangeLockEdition
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion

        
        //------------------------------------------------------------
        public event EventHandler AfterRemoveElement;
        private void m_editeur_AfterRemoveObjetGraphique(object sender, EventArgs e)
        {
            if (AfterRemoveElement != null)
                AfterRemoveElement(this, null);
        }

        //------------------------------------------------------------
        public event EventHandler AfterAddElements;
        private bool m_editeur_AfterAddElements(List<I2iObjetGraphique> objs)
        {
            if (AfterAddElements != null)
                AfterAddElements(this, null);
            return true;
        }

        public event EventHandler ElementPropertiesChanged;
        private void m_editeur_ElementPropertiesChanged(object sender, EventArgs e)
        {
            if (ElementPropertiesChanged != null)
                ElementPropertiesChanged(sender, null);
        }

        //------------------------------------------------------------
        private void m_btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
            {
                if (m_bSaveWithSource)
                    SaveWithSources();
                else
                    SaveWithoutSources();
            }
            else
                m_menuSaveOptions.Show(m_btnSave, new Point(e.X, e.Y));
        }

        //------------------------------------------------------------
        private void SaveWithoutSources()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Easy query|*.esq|All files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CEasyQuery query = m_editeur.ObjetEdite as CEasyQuery;
                CSerializerObjetInFile.SaveToFile(query, "QUERY", dlg.FileName);
                m_bSaveWithSource = false;
            }
        }

        //------------------------------------------------------------
        private void m_menuSaveWithSources_Click(object sender, EventArgs e)
        {
            SaveWithSources();
        }

        //------------------------------------------------------------
        private void SaveWithSources()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Easy query|*.esq|All files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CEasyQuery query = m_editeur.ObjetEdite as CEasyQuery;
                CEasyQueryAvecSource qas = CEasyQueryAvecSource.FromQuery(query);
                CSerializerObjetInFile.SaveToFile(qas, "QUERYWITHSOURCE", dlg.FileName);
                m_bSaveWithSource = true;
            }
        }

        private void m_menuSaveWithoutSources_Click(object sender, EventArgs e)
        {

        }

        private bool m_editeur_AfterAddElements()
        {
            return default(bool);
        }

        private void m_btnOptionsQuery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_query != null)
                CFormOptionsDeQuery.EditeOptions(m_query);
        }
    }
}
