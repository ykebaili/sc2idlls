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
using sc2i.data.dynamic.EasyQuery;
using sc2i.data.dynamic;
using sc2i.formulaire.win32;
using System.Reflection;

namespace sc2i.win32.data.dynamic.easyquery
{
    public partial class CFormEditeProprietesTableFilleFromFramework : Form
    {
        private CODEQTableFilleFromFramework m_tableFromFramework = null;
        private IControlOptionTableDefinition m_controleOptions = null;
        private Type m_typeSource = null;
        private CDefinitionProprieteDynamique m_champ = null;

        public CFormEditeProprietesTableFilleFromFramework()
        {
            InitializeComponent();
            m_panelFiltre.MasquerFormulaire(true);
        }

        //--------------------------------------------------------
        public void Init(CODEQTableFilleFromFramework obj)
        {
            m_tableFromFramework = obj;
            if (m_tableFromFramework.ElementsSource.Length > 0)
            {
                m_lblSource.Text = m_tableFromFramework.ElementsSource[0].NomFinal;
                IODEQTableFromFramework t = m_tableFromFramework.ElementsSource[0] as IODEQTableFromFramework;
                if (t != null)
                    m_typeSource = t.TypeElements;
                else
                    m_typeSource = typeof(DBNull);
            }
            else
                m_lblSource.Text = "?";
            m_txtNomTable.Text = m_tableFromFramework.NomFinal;
            m_chkUseCache.Checked = m_tableFromFramework.UseCache;

            CResultAErreur result = m_tableFromFramework.GetErreurIncompatibilitéTableParente();
            if (!result)
            {
                m_panelSourceIncompatible.Visible = true;
                m_lblImpossible.Text = result.Erreur.ToString();
            }
            else
                m_panelSourceIncompatible.Visible = false;

            FillListeColonnes();
            FillListeFormulesNommees();

            m_champ = m_tableFromFramework.ChampSource;
            m_labelChamp.Text = m_champ == null ? I.T("[UNDEFINED]|30013") : m_champ.Nom;


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
                Dictionary<string, CColumnEQFromSource> dicIdSourceToCol = new Dictionary<string, CColumnEQFromSource>();
                foreach (IColumnDeEasyQuery col in m_tableFromFramework.ColonnesOrCalculees)
                {
                    CColumnEQFromSource colFromSource = col as CColumnEQFromSource;
                    if (colFromSource == null)
                    {
                        ListViewItem item = new ListViewItem(col.ColumnName);
                        item.Tag = col;
                        m_wndListeColonnes.Items.Add(item);
                    }
                    else
                        dicIdSourceToCol[colFromSource.IdColumnSource] = colFromSource;
                }
                IObjetDeEasyQuery tableParent = m_tableFromFramework.ElementsSource.Length > 0 ? m_tableFromFramework.ElementsSource[0] : null;
                if ( tableParent != null )
                {
                    foreach ( IColumnDeEasyQuery col in tableParent.Columns )
                    {
                        ListViewItem item = new ListViewItem(col.ColumnName);
                        item.SubItems.Add(col.ColumnName);
                        CColumnEQFromSource colFromSource = null;
                        if ( dicIdSourceToCol.TryGetValue(col.Id, out colFromSource ))
                        {
                            item.Text = colFromSource.ColumnName;
                            item.Checked = true;
                            colFromSource.DataType = col.DataType;
                        }
                        item.Tag = col;
                        m_wndListeColonnesFromParent.Items.Add(item);
                    }
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
            m_tableFromFramework.ChampSource = m_champ;

            m_tableFromFramework.NomFinal = m_txtNomTable.Text;
            m_tableFromFramework.UseCache = m_chkUseCache.Checked;
            List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
            foreach (ListViewItem item in m_wndListeColonnes.Items)
            {
                CColumnDeEasyQueryChampDeRequete col = item.Tag as CColumnDeEasyQueryChampDeRequete;
                if (col != null)
                    lst.Add(col);
            }


            foreach (ListViewItem item in m_wndListeColonnesFromParent.Items)
            {
                if (item.Checked)
                {
                    IColumnDeEasyQuery col = item.Tag as IColumnDeEasyQuery;
                    if (col != null)
                    {
                        bool bColExiste = false;
                        foreach (IColumnDeEasyQuery colEx in m_tableFromFramework.Columns)
                        {
                            CColumnEQFromSource colExSrc = colEx as CColumnEQFromSource;
                            if (colExSrc != null && colExSrc.IdColumnSource == col.Id)
                            {
                                colEx.ColumnName = item.Text;
                                lst.Add(colEx);
                                bColExiste = true;
                                break;
                            }
                        }
                        if (!bColExiste)
                        {
                            IColumnDeEasyQuery newCol = new CColumnEQFromSource(col);
                            newCol.ColumnName = item.Text;
                            lst.Add(newCol);
                        }
                    }
                }
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
        private void CFormEditeProprietesTableFilleFromFramework_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }


        //--------------------------------------------------------
        private void m_wndAddColumn_LinkClicked(object sender, EventArgs e)
        {
            CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes
                (
                m_tableFromFramework.TypeElements,
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
                col, m_tableFromFramework.TypeElements, null))
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
                bl.Remove(bl.Length - 1, 1);
                if (CFormAlerte.Afficher(I.T("Delete column(s) @1 ?|20102", bl.ToString()),
                    EFormAlerteBoutons.OuiNon,
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    System.Collections.ArrayList lst = new System.Collections.ArrayList(m_wndListeColonnes.SelectedItems);
                    foreach ( ListViewItem item in lst )
                        m_wndListeColonnes.Items.Remove(item);
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

        private void m_boutonDropList_Click(object sender, EventArgs e)
        {
            if (m_typeSource != null)
            {
                Rectangle rect = m_panelComboChamp.RectangleToScreen(new Rectangle(0, m_panelComboChamp.Height, m_panelComboChamp.Width, 230));
                bool bCancel = false;
                CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp(
                    rect,
                    m_typeSource,
                    new CFournisseurProprietesForFiltreDynamique(),
                    ref bCancel,
                    new BeforeIntegrerChampEventHandler(FiltreChamp),
                    null);
                if (!bCancel)
                {
                    if (m_champ == null || champ == null || m_champ.NomPropriete != champ.NomPropriete)
                    {
                        m_champ = champ;
                        m_labelChamp.Text = m_champ == null ? I.T("[UNDEFINED]|30013") : m_champ.Nom;
                        Type tp = m_champ.TypeDonnee.TypeDotNetNatif;
                        if (tp != m_panelFiltre.FiltreDynamique.TypeElements)
                        {
                            m_wndListeColonnes.Items.Clear();
                            m_panelFiltre.FiltreDynamique.TypeElements = m_champ.TypeDonnee.TypeDotNetNatif;
                        }
                        m_tableFromFramework.ChampSource = champ;
                    }
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////
        private bool FiltreChamp(CDefinitionProprieteDynamique def)
        {
            if (def is CDefinitionProprieteDynamiqueRelation)
            {
                CDefinitionProprieteDynamiqueRelation rel = def as CDefinitionProprieteDynamiqueRelation;
                string strTableSource = CContexteDonnee.GetNomTableForType(m_typeSource);
                if (rel.Relation.TableParente == strTableSource)
                    return true;
            }
            /*if (def is CDefinitionProprieteDynamiqueChampCustomFils)
                return true;*/
            if (def is CDefinitionProprieteDynamiqueRelationTypeId)
                return true;
            
            return false;
        }

        //--------------------------------------------------------------
        private void m_btnAddFieldId_Click(object sender, EventArgs e)
        {
            IODEQTableFromFramework source = m_tableFromFramework.ElementsSource[0] as IODEQTableFromFramework;
            if (source != null)
            {
                CStructureTable structure = CStructureTable.GetStructure ( source.TypeElements );
                CColumnDeEasyQueryChampDeRequete col = new CColumnDeEasyQueryChampDeRequete("ID",
                    new CSourceDeChampDeRequete(structure.ChampsId[0].NomChamp),
                    typeof(int),
                    OperationsAgregation.None,
                    true);
                source.AddColonneDeRequete(col);
                CResultAErreur result = m_tableFromFramework.GetErreurIncompatibilitéTableParente();
                if (!result)
                {
                    m_panelSourceIncompatible.Visible = true;
                    m_lblImpossible.Text = result.Erreur.ToString();
                }
                else
                    m_panelSourceIncompatible.Visible = false;
            }
        }

    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableFillFromFramework : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableFilleFromFramework), typeof(CEditeurProprietesTableFillFromFramework));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableFilleFromFramework tableFromFramework = objet as CODEQTableFilleFromFramework;
            if (tableFromFramework == null)
                return false;
            CFormEditeProprietesTableFilleFromFramework form = new CFormEditeProprietesTableFilleFromFramework();
            form.Init(tableFromFramework);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }
            
        

        #endregion
    }

    


}
