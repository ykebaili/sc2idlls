using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using futurocom.easyquery.postFillter;
using futurocom.easyquery;
using sc2i.drawing;
using sc2i.common;

namespace futurocom.win32.easyquery.postFilter
{
    [AutoExec("Autoexec")]
    public partial class CEditeurPostFilterFromQueryTable : UserControl, IEditeurPostFilter
    {
        private IObjetDeEasyQuery m_tableFiltree = null;
        private CPostFilterFromQueryTable m_postFilter = new CPostFilterFromQueryTable();
        private CEasyQuery m_query = null;


        public CEditeurPostFilterFromQueryTable()
        {
            InitializeComponent();
        }

        public static void Autoexec()
        {
            CEditeurPostFilter.RegisterEditeur(typeof(CPostFilterFromQueryTable), typeof(CEditeurPostFilterFromQueryTable));
        }

        public void Init ( IObjetDeEasyQuery tableFiltree, CPostFilterFromQueryTable postFilter, CEasyQuery query )
        {
            m_tableFiltree = tableFiltree;
            m_postFilter = postFilter;
            m_query = query;
            //Remplit la liste des tables;
            List<IObjetDeEasyQuery> lstObjets = new List<IObjetDeEasyQuery>();
            if (query != null)
            {
                foreach (I2iObjetGraphique objet in query.Childs)
                    if (objet is IObjetDeEasyQuery)
                        lstObjets.Add(objet as IObjetDeEasyQuery);
                m_cmbTableSource.DataSource = lstObjets;
                IObjetDeEasyQuery objetSource = query.GetObjet(postFilter.SourceTableId);
                m_cmbTableSource.SelectedItem = objetSource;
            }
            UpdatePanelJointure();            

        }

        //--------------------------------------------------------------------------------------
        public void UpdatePanelJointure()
        {
            IObjetDeEasyQuery table2 = m_cmbTableSource.SelectedValue as IObjetDeEasyQuery;
            m_editeurParametre.Init(m_tableFiltree, table2, m_postFilter.ParametreJointure);
        }

        //--------------------------------------------------------------------------------------
        private void m_cmbTableSource_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_editeurParametre.MajChamps();
            UpdatePanelJointure();
        }

        //--------------------------------------------------------------------------------------
        public void Init(CODEQBase odeqBase, IPostFilter postFilter)
        {
            CPostFilterFromQueryTable postFromQuery = postFilter as CPostFilterFromQueryTable;
            if (postFromQuery == null)
                postFromQuery = new CPostFilterFromQueryTable();
            Init(odeqBase, postFromQuery, odeqBase.Query);
        }

        //--------------------------------------------------------------------------------------
        public CResultAErreurType<IPostFilter> MajChamps()
        {
            CResultAErreurType<IPostFilter> resPost = new CResultAErreurType<IPostFilter>();
            CResultAErreur res = m_editeurParametre.MajChamps();
            if (!res)
            {
                resPost.EmpileErreur(res.Erreur);
                return resPost;
            }
            m_postFilter.ParametreJointure = m_editeurParametre.Parametre;
            if ( m_cmbTableSource.SelectedValue is IObjetDeEasyQuery )
                m_postFilter.SourceTableId = ((IObjetDeEasyQuery)m_cmbTableSource.SelectedValue).Id;
            resPost.DataType = m_postFilter;
            return resPost;
        }
    }
}
