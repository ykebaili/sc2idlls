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
using data.hotel.easyquery;
using data.hotel.easyquery.win32.calcul;

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesTableEntitiesFromDataHotel : Form
    {
        private CODEQTableEntitiesFromDataHotel m_tableFromDataHotel = null;

        public CFormEditeProprietesTableEntitiesFromDataHotel()
        {
            InitializeComponent();
        }

        public void Init(CODEQTableEntitiesFromDataHotel obj)
        {
            m_tableFromDataHotel = obj;
            if (m_tableFromDataHotel.TableDefinition != null)
                m_lblSource.Text = m_tableFromDataHotel.TableDefinition.TableName;
            else
                m_lblSource.Text = "?";
            m_txtNomTable.Text = m_tableFromDataHotel.NomFinal;
            m_chkUseCache.Checked = m_tableFromDataHotel.UseCache;
            FillListeColonnes();
            FillListeFormulesNommees();
            m_panelOptions.Init(m_tableFromDataHotel);
        }

        private void FillListeFormulesNommees()
        {
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_tableFromDataHotel.ColonnesCalculees.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_tableFromDataHotel);
        }

        private void FillListeColonnes()
        {
            if (m_tableFromDataHotel.TableDefinition != null)
            {
                foreach (IColumnDefinition col in m_tableFromDataHotel.TableDefinition.Columns)
                {
                    ListViewItem item = new ListViewItem(col.ColumnName);
                    IColumnDeEasyQuery colFromDataHotel = m_tableFromDataHotel.GetColonneFor(col);
                    if (colFromDataHotel != null)
                    {
                        item.Text = colFromDataHotel.ColumnName;
                        item.Checked = true;
                    }
                    item.SubItems.Add(col.ColumnName);
                    item.Tag = col;
                    m_wndListeColonnes.Items.Add(item);
                }
            }
            
        }

       

        //--------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }



            m_tableFromDataHotel.NomFinal = m_txtNomTable.Text;
            m_tableFromDataHotel.UseCache = m_chkUseCache.Checked;
            List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
            foreach (ListViewItem item in m_wndListeColonnes.Items)
                if (item.Checked)
                {
                    IColumnDefinition colFromSource = item.Tag as IColumnDefinition;
                    IColumnDeEasyQuery newCol = m_tableFromDataHotel.GetColonneFor(colFromSource);
                    if (newCol == null)
                        newCol = new CColumnEQFromSource(colFromSource);
                    newCol.ColumnName = item.Text;
                    lst.Add(newCol);
                }
            m_tableFromDataHotel.SetColonnesOrCalculees(lst);

            List<CColonneEQCalculee> colsCalc = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                colsCalc.Add(col);

            m_tableFromDataHotel.ColonnesCalculees = colsCalc;

            CResultAErreur result = m_panelOptions.MajChamps(m_tableFromDataHotel);
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

        private void CFormEditeProprietesTableEntitiesFromDataHotel_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        


    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableEntitiesFromDataHotel : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableEntitiesFromDataHotel), typeof(CEditeurProprietesTableEntitiesFromDataHotel));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableEntitiesFromDataHotel tableFromDataHotel = objet as CODEQTableEntitiesFromDataHotel;
            if (tableFromDataHotel == null)
                return false;
            CFormEditeProprietesTableEntitiesFromDataHotel form = new CFormEditeProprietesTableEntitiesFromDataHotel();
            form.Init(tableFromDataHotel);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }






}
