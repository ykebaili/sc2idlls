using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;

namespace data.hotel.easyquery.win32
{
    public partial class CControleOptionsTableEntitesFromDataHotel : UserControl
    {
        //--------------------------------------------------------
        public CControleOptionsTableEntitesFromDataHotel()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------
        public void Init ( CODEQTableEntitiesFromDataHotel table)
        {
            CFournisseurGeneriqueProprietesDynamiques fourn = new CFournisseurGeneriqueProprietesDynamiques();
            m_txtStartDate.Init(fourn,new CObjetPourSousProprietes(table.Query));
            m_txtEndDate.Init(fourn, new CObjetPourSousProprietes(table.Query));
            m_txtStartDate.Formule = table.FormuleDateDebut;
            m_txtEndDate.Formule = table.FormuleDateFin;
        }

        //--------------------------------------------------------
        public CResultAErreur MajChamps ( CODEQTableEntitiesFromDataHotel table )
        {
            CResultAErreur result = CResultAErreur.True;
            if (!m_txtStartDate.ResultAnalyse || m_txtStartDate.Formule == null)
            {
                result.EmpileErreur(m_txtStartDate.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Start date formula error|20014"));
            }
            else
                table.FormuleDateDebut = m_txtStartDate.Formule;
            if (!m_txtEndDate.ResultAnalyse || m_txtEndDate.Formule == null)
            {
                result.EmpileErreur(m_txtEndDate.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("End date formula error|200015"));
            }
            else
                table.FormuleDateFin = m_txtEndDate.Formule;
            
            return result;
        }
    }
}
