using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using futurocom.chart;
using futurocom.easyquery;
using sc2i.drawing;
using sc2i.expression.datatable;
using sc2i.expression;

namespace futurocom.win32.chart.series
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeFournisseurValeursSerieChampDeTable : UserControl, IEditeurFournisseurValeursSerieDeTypeConnu
    {
        private CFournisseurValeursSerieChampDeTable m_fournisseur = null;
        private CChartSetup m_chartSetup = null;

        //-----------------------------------------------------------------
        public CControleEditeFournisseurValeursSerieChampDeTable()
        {
            InitializeComponent();
            sc2i.win32.common.CWin32Traducteur.Translate(this);
        }

        //-----------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireEditeursFournisseursValeurs.RegisterEditeur(typeof(CFournisseurValeursSerieChampDeTable),
                typeof(CControleEditeFournisseurValeursSerieChampDeTable));
        }

        //-----------------------------------------------------------------
        public void InitChamps(CChartSetup chartSetup, IFournisseurValeursSerie fournisseur)
        {
            m_chartSetup = chartSetup;
            m_fournisseur = fournisseur as CFournisseurValeursSerieChampDeTable;

            FillTables();
            FillChamps();
            m_txtFormule.Formule = m_fournisseur.Formule;
            m_rbtnField.Checked = m_fournisseur.Formule == null;
            m_rbtnFormule.Checked = !m_rbtnField.Checked;
            UpdateAspect();
        }

        //-----------------------------------------------------------------
        private void UpdateAspect()
        {
            m_panelFormule.Visible = m_rbtnFormule.Checked;
            m_panelColonne.Visible = !m_rbtnFormule.Checked;
        }

        //-----------------------------------------------------------------
        private bool m_bIsFillingTables = false;
        private void FillTables()
        {
            m_bIsFillingTables = true;
            m_comboTable.BeginUpdate();
            m_comboTable.Items.Clear();
            CParametreSourceChart p = m_chartSetup.ParametresDonnees.GetSourceFV(m_fournisseur.SourceId);
            if (p != null)
            {
                CEasyQuery query = p.GetSource(m_chartSetup)as CEasyQuery;
                if (query != null)
                {
                    foreach (C2iObjetGraphique obj in query.Childs)
                    {
                        IObjetDeEasyQuery oe = obj as IObjetDeEasyQuery;
                        if (oe != null)
                        {
                            m_comboTable.Items.Add(oe.NomFinal);
                        }
                    }
                }
            }
            m_comboTable.EndUpdate();
            m_comboTable.SelectedItem = m_fournisseur.TableName;
            m_bIsFillingTables = false;
        }

        //-----------------------------------------------------------------
        private void FillChamps()
        {
            m_comboChamp.BeginUpdate();
            m_comboChamp.Items.Clear();
            if (m_comboTable.Text.Length > 0)
            {
                CParametreSourceChart p = m_chartSetup.ParametresDonnees.GetSourceFV(m_fournisseur.SourceId);
                if (p != null)
                {
                    CEasyQuery query = p.GetSource(m_chartSetup) as CEasyQuery;
                    if (query != null)
                    {
                        foreach (C2iObjetGraphique obj in query.Childs)
                        {
                            IObjetDeEasyQuery oe = obj as IObjetDeEasyQuery;
                            if (oe != null)
                            {
                                if (oe.NomFinal == m_comboTable.Text)
                                {
                                    DataTable table = query.GetTable(oe.NomFinal);
                                    if (table != null)
                                    {
                                        foreach (DataColumn col in table.Columns)
                                        {
                                            m_comboChamp.Items.Add(col.ColumnName);
                                        }
                                    }
                                    CDynamicDataTableDef tableDef = new CDynamicDataTableDef(table);
                                    CDynamicDataTableRowDef rowDef = new CDynamicDataTableRowDef(tableDef);
                                    m_txtFormule.Init(new CFournisseurGeneriqueProprietesDynamiques(),
                                        new CObjetPourSousProprietes(rowDef));

                                }
                            }
                        }
                    }
                }
            }
            m_comboChamp.EndUpdate();
            m_comboChamp.SelectedItem = m_fournisseur.ColumnName;
        }


        //-----------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_comboTable.Text.Length == 0 )
            {
                result.EmpileErreur(I.T("Select a table|20020"));
                return result;
            }
            if (m_rbtnFormule.Checked &&  m_txtFormule.Formule == null)
            {
                result.EmpileErreur(I.T("Select a field or a formula|20034"));
                return result;
            }

            //Vérifie que la sélection est valide
            CParametreSourceChart p = m_chartSetup.ParametresDonnees.GetSourceFV(m_fournisseur.SourceId);
            CEasyQuery q = p != null ? p.GetSource (m_chartSetup) as CEasyQuery : null;
            if ( q == null )
            {
                result.EmpileErreur(I.T("Invalid source|20021"));
                return result;
            }
            //Trouve la table
            IObjetDeEasyQuery oqSel = null;
            foreach ( C2iObjetGraphique objet in q.Childs )
            {
                IObjetDeEasyQuery o = objet as IObjetDeEasyQuery;
                if ( o != null && o.NomFinal == m_comboTable.Text )
                {
                    oqSel = o;
                    break;
                }
            }
            if ( oqSel == null )
            {
                result.EmpileErreur(I.T("Invalid table name (@1)|20022", m_comboTable.Text));
                return result;
            }
            DataColumn colSel = null;
            DataTable table = q.GetTable(oqSel.NomFinal);
            if ( m_rbtnField.Checked )
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (col.ColumnName == m_comboChamp.Text)
                    {
                        colSel = col;
                        break;
                    }
                }
                if (colSel == null)
                {
                    result.EmpileErreur(I.T("Invalid column name @1|20023", m_comboChamp.Text));
                    return result;
                }
            }
            if (m_rbtnFormule.Checked)
            {
                if (!m_txtFormule.ResultAnalyse && colSel == null)
                {
                    result.EmpileErreur(m_txtFormule.ResultAnalyse.Erreur);
                }
                else
                {
                    m_fournisseur.Formule = m_txtFormule.Formule;
                    m_fournisseur.ColumnName = "";
                }
            }

            if (colSel != null)
            {
                m_fournisseur.ColumnName = colSel.ColumnName;
                m_fournisseur.Formule = null;
            }
            m_fournisseur.TableName = oqSel.NomFinal;
            return result;

        }

        //-----------------------------------------------------------------
        private void m_comboTable_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillChamps();
        }

        private void m_comboChamp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_rbtnField_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAspect();
        }

        private void m_rbtnFormule_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAspect();
        }

        
    }
}
