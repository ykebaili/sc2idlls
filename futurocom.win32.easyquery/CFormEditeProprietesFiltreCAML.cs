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
using futurocom.win32.easyquery.CAML;

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesFiltreCAML : Form
    {
        private CODEQFiltreCAML m_objetFiltre = new CODEQFiltreCAML();
        private CCAMLQuery m_query = new CCAMLQuery();
        
        public CFormEditeProprietesFiltreCAML()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------
        public void Init(CODEQFiltreCAML filtre)
        {
            m_objetFiltre = filtre;
            m_txtNomTable.Text = filtre.NomFinal;
            m_chkUseCache.Checked = filtre.UseCache;
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_objetFiltre.ColonnesCalculees.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_objetFiltre );
            if (filtre.CAMLQuery != null)
            {
                m_query = CCloner2iSerializable.Clone(filtre.CAMLQuery) as CCAMLQuery;
            }
            else
                m_query = new CCAMLQuery();
            m_panelCAML.Init(m_objetFiltre.Query, m_query, filtre.TableSource.CAMLFields);
            m_panelPostFilter.Init(filtre);
        }

        

        //--------------------------------------------
        private void CFormEditeProprietesFiltreCAML_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

                


        //--------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }
            
            m_objetFiltre.NomFinal = m_txtNomTable.Text;
            m_objetFiltre.UseCache = m_chkUseCache.Checked;
            
            CResultAErreurType<CCAMLQuery> resCAML = m_panelCAML.MajChamps();
            if (!resCAML)
            {
                CFormAlerte.Afficher(resCAML.Erreur);
                return;
            }
            CResultAErreur result = m_panelPostFilter.MajChamps();
            if ( !result )
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            m_objetFiltre.CAMLQuery = resCAML.DataType;

            List<CColonneEQCalculee> lst = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                lst.Add(col);
            m_objetFiltre.ColonnesCalculees = lst;
            DialogResult = DialogResult.OK;
            Close();
        }

        //--------------------------------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        
    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesFiltreCAML : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQFiltreCAML), typeof(CEditeurProprietesFiltreCAML));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQFiltreCAML filtre = objet as CODEQFiltreCAML;
            if (filtre == null)
                return false;
            CFormEditeProprietesFiltreCAML form = new CFormEditeProprietesFiltreCAML();
            form.Init(filtre);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }
}
