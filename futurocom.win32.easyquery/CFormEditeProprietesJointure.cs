using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.common;
using sc2i.win32.common;
using System.Collections;

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesJointure : Form
    {
        private CODEQJointure m_objetJointure = new CODEQJointure();

        //-----------------------------------
        public CFormEditeProprietesJointure()
        {
            InitializeComponent();
        }

        //-----------------------------------
        public void Init(CODEQJointure jointure)
        {
            m_objetJointure = jointure;
            FillParametres();
            m_cmbTypeJointure.Items.Clear();
            m_txtNomTable.Text = jointure.NomFinal;
            m_chkUseCache.Checked = jointure.UseCache;
            foreach (CODEQJointure.EModeJointure mode in Enum.GetValues(typeof(CODEQJointure.EModeJointure)))
            {
                switch (mode)
                {
                    case CODEQJointure.EModeJointure.Stricte:
                        m_cmbTypeJointure.Items.Add(I.T("Only matching elements|20016"));
                        break;
                    case CODEQJointure.EModeJointure.Left:
                        m_cmbTypeJointure.Items.Add(I.T("All table1 elements|20017"));
                        break;
                    case CODEQJointure.EModeJointure.Right:
                        m_cmbTypeJointure.Items.Add(I.T("All table2 elements|20018"));
                        break;
                    case CODEQJointure.EModeJointure.Outer :
                        m_cmbTypeJointure.Items.Add(I.T("All elements from both tables|20020"));
                        break;
                    default:
                        break;
                }
            }
            m_cmbTypeJointure.SelectedIndex = (int)m_objetJointure.ModeJointure;
            if (m_objetJointure.Table1 != null)
                m_lblTable1.Text = m_objetJointure.Table1.NomFinal;
            if (m_objetJointure.Table2 != null)
                m_lblTable2.Text = m_objetJointure.Table2.NomFinal;

            FillListeColonnes(m_wndListeColonnes1, m_objetJointure.Table1);
            FillListeColonnes(m_wndListeColonnes2, m_objetJointure.Table2);
        }

        //-----------------------------------
        private void FillListeColonnes(ListView wndListe, IObjetDeEasyQuery table)
        {
            wndListe.Items.Clear();
            if (table == null)
                return;
            foreach (IColumnDeEasyQuery col in table.Columns)
            {
                ListViewItem item = new ListViewItem(col.ColumnName);
                item.SubItems.Add(col.ColumnName);
                IColumnDeEasyQuery colFinale = m_objetJointure.GetColonneFinaleFor(col);
                if (colFinale != null)
                {
                    item.Text = colFinale.ColumnName;
                    item.Checked = true;
                    colFinale.DataType = col.DataType;
                }
                item.Tag = col;
                wndListe.Items.Add(item);
            }
        }


        //-----------------------------------
        private void FillParametres()
        {
            m_panelJointure.SuspendDrawing();
            foreach (Control ctrl in new ArrayList(m_panelJointure.Controls))
            {
                ctrl.Visible = false;
                m_panelJointure.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            foreach (CParametreJointure parametre in m_objetJointure.ParametresJointure)
            {
                CEditeurParametreJointure editeur = new CEditeurParametreJointure();
                editeur.Init(m_objetJointure, parametre);
                m_panelJointure.Controls.Add(editeur);
                editeur.Dock = DockStyle.Top;
                editeur.BringToFront();
            }
            m_panelJointure.ResumeDrawing();
        }
       
        //-----------------------------------
        private void CFormEditeProprietesJointure_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //-----------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }
            List<CParametreJointure> lstParametres = new List<CParametreJointure>();
            ArrayList lst = new ArrayList(m_panelJointure.Controls);
            lst.Reverse();
            foreach (Control ctrl in lst)
            {
                CEditeurParametreJointure editeur = ctrl as CEditeurParametreJointure;
                if (editeur != null)
                {
                    
                    CResultAErreur result = editeur.MajChamps();
                    if (!result)
                    {
                        editeur.BackColor = Color.Red;
                        CFormAfficheErreur.Show(result.Erreur);
                        return;
                    }
                    editeur.BackColor = BackColor;
                    lstParametres.Add(editeur.Parametre);
                    
                }
            }

            List<IColumnDeEasyQuery> lstColonnes = new List<IColumnDeEasyQuery>();
            foreach (ListViewItem item in m_wndListeColonnes1.Items)
            {
                if (item.Checked)
                {
                    IColumnDeEasyQuery col = item.Tag as IColumnDeEasyQuery;
                    if (col != null)
                    {
                        bool bColExiste = false;
                        foreach (IColumnDeEasyQuery colEx in m_objetJointure.ColonnesSource)
                        {
                            CColumnEQFromSource colExSrc = colEx as CColumnEQFromSource;
                            if (colExSrc != null && colExSrc.IdColumnSource == col.Id)
                            {
                                colEx.ColumnName = item.Text;
                                lstColonnes.Add(colEx);
                                bColExiste = true;
                                break;
                            }
                        }
                        if (!bColExiste)
                        {
                            IColumnDeEasyQuery newCol = new CColumnEQFromSource(col);
                            newCol.ColumnName = item.Text;
                            lstColonnes.Add(newCol);
                        }
                    }
                }
            }

            foreach (ListViewItem item in m_wndListeColonnes2.Items)
            {
                if (item.Checked)
                {
                    IColumnDeEasyQuery col = item.Tag as IColumnDeEasyQuery;
                    if (col != null)
                    {
                        bool bColExiste = false;
                        foreach (IColumnDeEasyQuery colEx in m_objetJointure.ColonnesSource)
                        {
                            CColumnEQFromSource colExSrc = colEx as CColumnEQFromSource;
                            if (colExSrc != null && colExSrc.IdColumnSource == col.Id)
                            {
                                colEx.ColumnName = item.Text;
                                lstColonnes.Add(colEx);
                                bColExiste = true;
                                break;
                            }
                        }
                        if (!bColExiste)
                        {
                            IColumnDeEasyQuery newCol = new CColumnEQFromSource(col);
                            newCol.ColumnName = item.Text;
                            lstColonnes.Add(newCol);
                        }
                    }
                }
            }
            m_objetJointure.UseCache = m_chkUseCache.Checked;
            m_objetJointure.ModeJointure = (CODEQJointure.EModeJointure)m_cmbTypeJointure.SelectedIndex;
            m_objetJointure.ColonnesSource = lstColonnes;
            m_objetJointure.ParametresJointure = lstParametres;
            m_objetJointure.NomFinal = m_txtNomTable.Text;
            
            DialogResult = DialogResult.OK;
            Close();
        }

        //-----------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //-----------------------------------
        public void RemoveParametre(CEditeurParametreJointure editeur)
        {
            editeur.Visible = false;
            m_panelJointure.Controls.Remove(editeur);
            editeur.Dispose();
        }

        //-----------------------------------
        private void m_lnkAddParametre_LinkClicked(object sender, EventArgs e)
        {
            CParametreJointure parametre = new CParametreJointure();
            CEditeurParametreJointure editeur = new CEditeurParametreJointure();
            editeur.Init(m_objetJointure, parametre);
            editeur.Dock = DockStyle.Top;
            m_panelJointure.Controls.Add(editeur);
            editeur.BringToFront();
            editeur.Focus();
        }

    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesJointure : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQJointure), typeof(CEditeurProprietesJointure));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQJointure jointure = objet as CODEQJointure;
            if (jointure == null)
                return false;
            CFormEditeProprietesJointure form = new CFormEditeProprietesJointure();
            form.Init(jointure);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }
}
