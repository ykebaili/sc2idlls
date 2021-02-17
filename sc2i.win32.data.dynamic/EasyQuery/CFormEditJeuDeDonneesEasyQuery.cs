using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic;
using futurocom.easyquery;
using sc2i.data.dynamic.EasyQuery;
using sc2i.drawing;
using sc2i.common;
using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.data.dynamic.EasyQuery
{
    public partial class CFormEditJeuDeDonneesEasyQuery : Form
    {
        private CElementMultiStructureExport m_elementDeMultiStructure = null;
        private CEasyQuery m_query = null;
        private HashSet<string> m_setTablesARetourner = new HashSet<string>();

        //--------------------------------------------------------------------------------
        public CFormEditJeuDeDonneesEasyQuery()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------
        private void m_wndListeTablesAExporter_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeTablesAExporter.Columns[0].Width = m_wndListeTablesAExporter.Size.Width - 20;
        }

        //--------------------------------------------------------------------------------
        public static bool EditeElementQuery(CElementMultiStructureExport elt, bool bWithHeader)
        {
            CFormEditJeuDeDonneesEasyQuery form = new CFormEditJeuDeDonneesEasyQuery();
            form.m_panelHeader.Visible = bWithHeader;
            form.m_elementDeMultiStructure = elt;
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                bResult = true;
            }
            form.Dispose();
            return bResult;
        }

        //--------------------------------------------------------------------------------
        public static bool EditeElementQuery(CElementMultiStructureExport elt)
        {
            return EditeElementQuery(elt, true);
        }

        //--------------------------------------------------------------------------------
        public CDefinitionJeuDonneesEasyQuery JeuQuery
        {
            get
            {
                return m_elementDeMultiStructure.DefinitionJeu as CDefinitionJeuDonneesEasyQuery;
            }
        }

        //--------------------------------------------------------------------------------
        private void InitChamps()
        {
            m_txtLibelleElement.Text = m_elementDeMultiStructure.Libelle;
            m_txtPrefix.Text = m_elementDeMultiStructure.Prefixe;
            m_query = CCloner2iSerializable.Clone(JeuQuery.EasyQueryAvecSource) as CEasyQuery;
            m_query.ElementAVariablesExternes = JeuQuery.EasyQueryAvecSource.ElementAVariablesExternes;
            m_query.IContexteDonnee = JeuQuery.EasyQueryAvecSource.IContexteDonnee;
            foreach (CEasyQuerySource source in JeuQuery.EasyQueryAvecSource.Sources)
                m_query.ListeSources.AddSource(source);
            foreach (string strIdTable in JeuQuery.IdTablesARetourner)
                m_setTablesARetourner.Add(strIdTable);
            m_panelQuery.Init(m_query);
            InitListeTables(false);
        }

        //--------------------------------------------------------------------------------
        private void InitListeTables(bool bKeepChecked)
        {
            //Stocke les items cochés
            HashSet<string> strCoches = new HashSet<string>();
            if (bKeepChecked)
            {
                foreach (ListViewItem item in m_wndListeTablesAExporter.CheckedItems)
                {
                    IObjetDeEasyQuery objet = item.Tag as IObjetDeEasyQuery;
                    if (objet != null)
                        strCoches.Add(objet.Id);
                }
            }
            else
                strCoches = m_setTablesARetourner;
            m_wndListeTablesAExporter.BeginUpdate();
            m_wndListeTablesAExporter.Items.Clear();
            foreach (I2iObjetGraphique objet in m_query.Childs)
            {
                IObjetDeEasyQuery objE = objet as IObjetDeEasyQuery;
                if (objE != null)
                {
                    ListViewItem item = new ListViewItem(objE.NomFinal);
                    item.Tag = objE;
                    item.Checked = strCoches.Contains(objE.Id);
                    m_wndListeTablesAExporter.Items.Add(item);
                }
            }
            m_wndListeTablesAExporter.EndUpdate();
        }

        //--------------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            m_elementDeMultiStructure.Libelle = m_txtLibelleElement.Text;
            m_elementDeMultiStructure.Prefixe = m_txtPrefix.Text;
            JeuQuery.EasyQueryAvecSource = m_panelQuery.Query;
            List<string> lstIdChecked = new List<string>();
            foreach (ListViewItem item in m_wndListeTablesAExporter.CheckedItems)
            {
                IObjetDeEasyQuery obj = item.Tag as IObjetDeEasyQuery;
                if (obj != null)
                    lstIdChecked.Add(obj.Id);
            }
            JeuQuery.IdTablesARetourner = lstIdChecked;
            return CResultAErreur.True;
        }

        //--------------------------------------------------------------------------------
        private void m_panelQuery_AfterAddElements(object sender, EventArgs e)
        {
            InitListeTables(true);
        }

        private void m_panelQuery_AfterRemoveElement(object sender, EventArgs e)
        {
            InitListeTables(true);
        }

        private void m_panelQuery_ElementPropertiesChanged(object sender, EventArgs e)
        {
            InitListeTables(true);
        }

        //-----------------------------------------------------------------------------
        private void CFormEditJeuDeDonneesEasyQuery_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            InitChamps();
        }

        //-----------------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //-----------------------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            MajChamps();
            
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
