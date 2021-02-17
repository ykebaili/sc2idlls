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

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesTableFromBase : Form
    {
        private CODEQTableFromBase m_tableFromBase = null;
        private IControlOptionTableDefinition m_controleOptions = null;

        public CFormEditeProprietesTableFromBase()
        {
            InitializeComponent();
        }

        public void Init(CODEQTableFromBase obj)
        {
            m_tableFromBase = obj;
            if (m_tableFromBase.TableDefinition != null)
                m_lblSource.Text = m_tableFromBase.TableDefinition.TableName;
            else
                m_lblSource.Text = "?";
            m_txtNomTable.Text = m_tableFromBase.NomFinal;
            m_chkUseCache.Checked = m_tableFromBase.UseCache;
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
                m_controleOptions.FillFromTable(m_tableFromBase.TableDefinition);
            }
            CCAMLQuery query = obj.CAMLQuery;
            if (query == null)
                query = new CCAMLQuery();
            IEnumerable<CCAMLItemField> fields = null;
            ITableDefinitionRequetableCAML tableCAML = obj.TableDefinition as ITableDefinitionRequetableCAML;
            if (tableCAML != null)
                fields = tableCAML.CAMLFields;
            else
            {
                if (obj.TableDefinition != null)
                    fields = obj.CAMLFields;
                else
                    fields = new List<CCAMLItemField>();
            }
            m_panelCAML.Init(m_tableFromBase.Query, query, fields);

            m_panelPostFilter.Init ( obj );
        }

        private void FillListeFormulesNommees()
        {
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_tableFromBase.ColonnesCalculees.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_tableFromBase);
        }

        private void FillListeColonnes()
        {
            if (m_tableFromBase.TableDefinition != null)
            {
                foreach (IColumnDefinition col in m_tableFromBase.TableDefinition.Columns)
                {
                    ListViewItem item = new ListViewItem(col.ColumnName);
                    IColumnDeEasyQuery colFromBase = m_tableFromBase.GetColonneFor(col);
                    if (colFromBase != null)
                    {
                        item.Text = colFromBase.ColumnName;
                        item.Checked = true;
                    }
                    item.SubItems.Add(col.ColumnName);
                    item.Tag = col;
                    m_wndListeColonnes.Items.Add(item);
                }
            }
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }

            
            m_tableFromBase.NomFinal = m_txtNomTable.Text;
            m_tableFromBase.UseCache = m_chkUseCache.Checked;
            List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
            foreach ( ListViewItem item in m_wndListeColonnes.Items )
                if (item.Checked)
                {
                    IColumnDefinition colFromSource = item.Tag as IColumnDefinition;
                    IColumnDeEasyQuery newCol = m_tableFromBase.GetColonneFor(colFromSource);
                    if ( newCol == null )
                        newCol = new CColumnEQFromSource(colFromSource);
                    newCol.ColumnName = item.Text;
                    lst.Add(newCol);
                }
            m_tableFromBase.SetColonnesOrCalculees(lst);

            List<CColonneEQCalculee> colsCalc = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                colsCalc.Add(col);

            m_tableFromBase.ColonnesCalculees = colsCalc;

            CResultAErreur result = CResultAErreur.True;
            if (m_controleOptions != null)
                result = m_controleOptions.MajChamps();

            CResultAErreurType<CCAMLQuery> resCAML = m_panelCAML.MajChamps();
            if (!resCAML)
            {
                result.EmpileErreur(resCAML.Erreur);
            }
            else
                m_tableFromBase.CAMLQuery = resCAML.DataType;

            if ( result )
                result = m_panelPostFilter.MajChamps();

            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            

            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CFormEditeProprietesTableFromBase_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        
    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableFromBase : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableFromBase), typeof(CEditeurProprietesTableFromBase));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableFromBase tableFromBase = objet as CODEQTableFromBase;
            if ( tableFromBase == null )
                return false;
            CFormEditeProprietesTableFromBase form = new CFormEditeProprietesTableFromBase();
            form.Init(tableFromBase);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }
            
        

        #endregion
    }

    


}
