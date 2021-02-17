using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;

namespace data.hotel.easyquery.win32.entitysource
{
    [AutoExec("Autoexec")]
    public partial class CEditeurSourceFormule : UserControl, IEditeurSourceEntite
    {
        private CSourceEntitesPourTableDataHotelFormule m_source = null;
        //---------------------------------------------------------------------
        public CEditeurSourceFormule()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------
        public static void Autoexec()
        {
            CPanelEditSourceEntites.RegisterTypeEditeurSource ( typeof(CSourceEntitesPourTableDataHotelFormule),
                typeof(CEditeurSourceFormule),
                I.T("Formula|20017"));
        }
        //---------------------------------------------------------------------
        public void Init(ISourceEntitesPourTableDataHotel source, CODEQTableFromDataHotel table)
        {
            m_source = source as CSourceEntitesPourTableDataHotelFormule;
            if (m_source == null)
                m_source = new CSourceEntitesPourTableDataHotelFormule();
            m_txtFormule.Init(new CFournisseurGeneriqueProprietesDynamiques() , new CObjetPourSousProprietes(table.Query));
            m_txtFormule.Formule = m_source.FormuleEntites;
        }

        public CResultAErreurType<ISourceEntitesPourTableDataHotel> MajChamps()
        {
            CResultAErreurType<ISourceEntitesPourTableDataHotel> result = new CResultAErreurType<ISourceEntitesPourTableDataHotel>();
            if (m_txtFormule.Formule == null || !m_txtFormule.ResultAnalyse)
            {
                result.EmpileErreur(m_txtFormule.ResultAnalyse.Erreur);
            }
            else
            {
                m_source.FormuleEntites = m_txtFormule.Formule;
                result.DataType = m_source;
            }
            return result;
        }
        
    }
}
