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
using futurocom.easyquery.CAML;
using sc2i.data.dynamic.easyquery;
using futurocom.win32.easyquery;
using sc2i.expression;
using sc2i.data;

namespace sc2i.win32.data.dynamic.easyquery
{
    public partial class CFormEditeProprietesTableFromFramework : Form
    {
        private CODEQTableFromTableFramework m_tableFromFramework = null;
        private IControlOptionTableDefinition m_controleOptions = null;

        public CFormEditeProprietesTableFromFramework()
        {
            InitializeComponent();
            m_panelFiltre.MasquerFormulaire(true);
        }

        //--------------------------------------------------------
        public void Init(CODEQTableFromTableFramework obj)
        {
            m_tableFromFramework = obj;
            if (m_tableFromFramework.TableDefinition != null)
                m_lblSource.Text = m_tableFromFramework.TableDefinition.TableName;
            else
                m_lblSource.Text = "?";
            m_txtNomTable.Text = m_tableFromFramework.NomFinal;
            m_chkUseCache.Checked = m_tableFromFramework.UseCache;
            FillListeColonnes();
            FillListeFormulesNommees();
            if (m_controleOptions != null)
            {
                m_pageOptions.Control = null;
                m_controleOptions.Dispose();
            }
            m_controleOptions = null;

            m_controleOptions = CAllocateurControleOptionTableDefinition.GetControleOptions(obj.TableDefinition);
            if (m_controleOptions == null)
            {
                if (m_tabControl.TabPages.Contains(m_pageOptions))
                    m_tabControl.TabPages.Remove(m_pageOptions);
            }
            else
            {
                if (!m_tabControl.TabPages.Contains(m_pageOptions))
                {
                    m_tabControl.TabPages.Add(m_pageOptions);
                }
                m_pageOptions.Control = (Control)m_controleOptions;
                m_controleOptions.FillFromTable(m_tableFromFramework.TableDefinition);
            }

            m_panelFiltre.InitSansVariables(m_tableFromFramework.FiltreDynamique);
            m_panelPostFilter.Init(obj);
        }

        //--------------------------------------------------------
        private void FillListeFormulesNommees()
        {
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_tableFromFramework.ColonnesCalculees.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_tableFromFramework);
        }

        //--------------------------------------------------------
        private void FillListeColonnes()
        {
            if (m_tableFromFramework != null)
            {
                foreach (IColumnDeEasyQuery col in m_tableFromFramework.ColonnesOrCalculees)
                {
                    ListViewItem item = new ListViewItem(col.ColumnName);
                    item.Tag = col;
                    m_wndListeColonnes.Items.Add(item);
                }
            }
        }

        //--------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20101"));
                return;
            }

            m_tableFromFramework.FiltreDynamique = m_panelFiltre.FiltreDynamique;

            m_tableFromFramework.NomFinal = m_txtNomTable.Text;
            m_tableFromFramework.UseCache = m_chkUseCache.Checked;
            List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
            foreach (ListViewItem item in m_wndListeColonnes.Items)
            {
                CColumnDeEasyQueryChampDeRequete col = item.Tag as CColumnDeEasyQueryChampDeRequete;
                if (col != null)
                    lst.Add(col);
            }
            m_tableFromFramework.SetColonnesOrCalculees(lst);

            List<CColonneEQCalculee> colsCalc = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                colsCalc.Add(col);

            m_tableFromFramework.ColonnesCalculees = colsCalc;

            CResultAErreur result = CResultAErreur.True;
            if (m_controleOptions != null)
                result = m_controleOptions.MajChamps();

            if (result)
                result = m_panelPostFilter.MajChamps();

            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            

            DialogResult = DialogResult.OK;
            Close();
        }

        //--------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //--------------------------------------------------------
        private void CFormEditeProprietesTableFromFramework_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }


        //--------------------------------------------------------
        private void m_wndAddColumn_LinkClicked(object sender, EventArgs e)
        {
            CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes
                (
                m_tableFromFramework.TypeSource,
                CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent |
                CFormSelectChampPourStructure.TypeSelectionAttendue.ChampFille |
                CFormSelectChampPourStructure.TypeSelectionAttendue.UniquementElementDeBaseDeDonnees |
                CFormSelectChampPourStructure.TypeSelectionAttendue.InclureChampsCustom,
                null);
            // Créé le nouveau champ de requete
            foreach ( CDefinitionProprieteDynamique def in defs )
            {
                CColumnDeEasyQueryChampDeRequete champUnique = new CColumnDeEasyQueryChampDeRequete();
                champUnique.NomChamp = def.Nom;
                champUnique.TypeDonneeAvantAgregation = def.TypeDonnee.TypeDotNetNatif;
                champUnique.OperationAgregation = OperationsAgregation.None;
                champUnique.GroupBy = false;
                List<CSourceDeChampDeRequete> listeSources = new List<CSourceDeChampDeRequete>();
                CSourceDeChampDeRequete source = new CSourceDeChampDeRequete(def.NomChampCompatibleCComposantFiltreChamp);
                listeSources.Add(source);
                champUnique.Sources = listeSources.ToArray();
                ListViewItem item = new ListViewItem(champUnique.NomChamp);
                item.Tag = champUnique;
                m_wndListeColonnes.Items.Add(item);
            }

        }

        //--------------------------------------------------------
        private void m_btnEditColumn_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeColonnes.SelectedItems.Count == 1)
                EditeColumn(m_wndListeColonnes.SelectedItems[0]);
        }

        //--------------------------------------------------------
        private void EditeColumn(ListViewItem item)
        {
            if (item == null)
                return;
            CColumnDeEasyQueryChampDeRequete col = item.Tag as CColumnDeEasyQueryChampDeRequete;
            if (col == null)
                return;
            if (CFormEditChampDeRequete.EditeChamp(
                col, m_tableFromFramework.TypeSource, null))
            {
                item.Text = col.ColumnName;
                m_wndListeColonnes.Refresh();
            }
        }

        //--------------------------------------------------------
        private void m_btnRemoveColumn_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeColonnes.SelectedItems.Count >= 1)
            {
                StringBuilder bl = new StringBuilder();
                foreach (ListViewItem item in m_wndListeColonnes.SelectedItems)
                {
                    CColumnDeEasyQueryChampDeRequete col = item.Tag as CColumnDeEasyQueryChampDeRequete;
                    if (col != null)
                    {
                        bl.Append(col.ColumnName);
                        bl.Append(",");
                    }
                }
                if (bl.Length > 0)
                {
                    bl.Remove(bl.Length - 1, 1);
                    if (CFormAlerte.Afficher(I.T("Delete column(s) @1 ?|20102", bl.ToString()),
                        EFormAlerteBoutons.OuiNon,
                        EFormAlerteType.Question) == DialogResult.Yes)
                    {
                        System.Collections.ArrayList lst = new System.Collections.ArrayList(m_wndListeColonnes.SelectedItems);
                        foreach (ListViewItem item in lst)
                            m_wndListeColonnes.Items.Remove(item);
                    }
                }
            }
        }

        //--------------------------------------------------------
        private void m_wndListeColonnes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = m_wndListeColonnes.HitTest(new Point(e.X, e.Y));
            if (info.Item != null)
                EditeColumn(info.Item);
        }

    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableFromFramework : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableFromTableFramework), typeof(CEditeurProprietesTableFromFramework));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableFromTableFramework TableFromFramework = objet as CODEQTableFromTableFramework;
            if ( TableFromFramework == null )
                return false;
            CFormEditeProprietesTableFromFramework form = new CFormEditeProprietesTableFromFramework();
            form.Init(TableFromFramework);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }
            
        

        #endregion
    }

    


}
