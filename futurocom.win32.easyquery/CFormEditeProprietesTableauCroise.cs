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

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesTableauCroise : Form
    {
        private CODEQTableauCroise m_objetTableauCroise = null;
        private DataTable m_tableSource = null;
        
        public CFormEditeProprietesTableauCroise()
        {
            InitializeComponent();
        }

        public void Init(CODEQTableauCroise objetTableauCroise)
        {
            m_txtNomTable.Text = objetTableauCroise.NomFinal;
            m_chkUseCache.Checked = objetTableauCroise.UseCache;
            m_objetTableauCroise = objetTableauCroise;
            m_tableSource = new DataTable();
            IObjetDeEasyQuery tableDef = m_objetTableauCroise.TableSource;
            if (tableDef != null)
            {
                foreach (IColumnDeEasyQuery col in tableDef.Columns)
                {
                    Type tp = col.DataType;
                    if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                        tp = tp.GetGenericArguments()[0];
                    m_tableSource.Columns.Add(new DataColumn(col.ColumnName, tp));
                }
            }
            CTableauCroise tableauCroise = CCloner2iSerializable.Clone(m_objetTableauCroise.TableauCroise) as CTableauCroise;
            if (tableauCroise == null)
                tableauCroise = new CTableauCroise();
            m_panelTableauCroise.InitChamps(m_tableSource, tableauCroise);
        }

       

        private void CFormEditeProprietesTableauCroise_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }
            CResultAErreur result = m_panelTableauCroise.TableauCroise.VerifieDonnees(m_tableSource);
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            m_objetTableauCroise.UseCache = m_chkUseCache.Checked;
            m_objetTableauCroise.NomFinal = m_txtNomTable.Text;
            m_objetTableauCroise.TableauCroise = m_panelTableauCroise.TableauCroise;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableauCroise : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableauCroise), typeof(CEditeurProprietesTableauCroise));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableauCroise objetDeTableau = objet as CODEQTableauCroise;
            if (objetDeTableau == null)
                return false;
            CFormEditeProprietesTableauCroise form = new CFormEditeProprietesTableauCroise();
            form.Init(objetDeTableau);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }
}
