using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using data.hotel.easyquery.calcul;
using data.hotel.client;
using futurocom.easyquery;
using sc2i.common;
using data.hotel.easyquery.filtre;

namespace data.hotel.easyquery.win32.calcul
{
    [AutoExec("AutoExec")]
    public partial class CEditeurCalculHotelDuration : UserControl, IEditeurCalculHotel
    {
        private CDataHotelCalculDuration m_calcul = null;

        //-------------------------------------------------
        public CEditeurCalculHotelDuration()
        {
            InitializeComponent();
        }

        //-------------------------------------------------
        public static void AutoExec()
        {
            CAllocateurEditeurCalculDataHotel.RegisterEditeur(
                typeof(CDataHotelCalculDuration), typeof (CEditeurCalculHotelDuration), 
            I.T("Duration|20047"));
        }

        //-------------------------------------------------
        public void Init ( 
            IDataHotelCalcul calcul, 
            CEasyQuery query,
            CODEQTableFromDataHotel table )
        {
            m_calcul = calcul as CDataHotelCalculDuration;
            if (m_calcul == null)
                m_calcul = new CDataHotelCalculDuration();
            List<CColumnEQFromSource> cols = new List<CColumnEQFromSource>();
            CColumnEQFromSource colSel = null;
            foreach ( IColumnDeEasyQuery colTest in table.Columns )
            {
                CColumnEQFromSource cs = colTest as CColumnEQFromSource;
                if (cs != null && colTest != null && colTest.DataType == typeof(double))
                {
                    cols.Add(cs);
                    if (cs.IdColumnSource == m_calcul.IdChampSource)
                        colSel = cs;
                }

            }
            m_cmbChampSource.SelectedValue = colSel;
            if ( table != null )
            {
                    m_cmbChampSource.ListDonnees = cols;
                    m_cmbChampSource.ProprieteAffichee = "ColumnName";
                    m_cmbChampSource.AssureRemplissage();
                    m_cmbChampSource.SelectedItem = colSel;
                    
                }
            m_panelFiltre.Init(query, m_calcul.Filtre, table);

        }

        //-------------------------------------------------
        public CResultAErreurType<IDataHotelCalcul> GetCalcul()
        {
            CResultAErreurType<IDataHotelCalcul> res = new CResultAErreurType<IDataHotelCalcul>();
            CColumnEQFromSource col = m_cmbChampSource.SelectedValue as CColumnEQFromSource;
            if ( col == null )
            {
                res.EmpileErreur(I.T("Select a source field|20033"));
                return res;
            }
            IDHFiltre filtre = m_panelFiltre.MajAndGetFiltre();
            m_calcul.IdChampSource = col.IdColumnSource;
            m_calcul.Filtre = filtre;
            res.DataType = m_calcul;
            return res;
            
        }
    }
}
