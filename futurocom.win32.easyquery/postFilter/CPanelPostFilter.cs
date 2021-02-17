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
using futurocom.easyquery.postFillter;
using sc2i.common;
using sc2i.win32.common;

namespace futurocom.win32.easyquery.postFilter
{
    public partial class CPanelPostFilter : UserControl
    {
        private IPostFilter m_postFilter = null;
        
        private CODEQBase m_odeqTable = null;

        private IEditeurPostFilter m_editeurEnCours = null;

        private static CDefPostFilter c_defNull = new CDefPostFilter("NULL", I.T("No filter|20072"), null);
        
        //------------------------------------------------------------------------
        public CPanelPostFilter()
        {
            InitializeComponent();
            
            List<CDefPostFilter> lstDefs = new List<CDefPostFilter>();
            lstDefs.Add(c_defNull);
            lstDefs.AddRange(CAllocateurPostFilter.GetDefs());
            m_cmbTypePostFilter.DataSource = lstDefs;
        }

        //------------------------------------------------------------------------
        public void Init ( CODEQBase odeqBase )
        {
            m_odeqTable = odeqBase;
            m_postFilter = m_odeqTable != null ? m_odeqTable.PostFilter : null;
            CDefPostFilter def = m_postFilter != null?CAllocateurPostFilter.GetDef(m_postFilter.GetType()):c_defNull;
            m_cmbTypePostFilter.SelectedItem = def;   
            UpdatePanelPostFilter();
        }

        //------------------------------------------------------------------------
        private void UpdatePanelPostFilter()
        {
            IEditeurPostFilter editeur = CEditeurPostFilter.GetEditeur(m_postFilter != null ? m_postFilter.GetType() : null);
            m_panelFiltre.ClearAndDisposeControls();
            if (editeur != null)
            {
                Control ctrl = editeur as Control;
                m_extModeEdition.SetModeEdition(ctrl, TypeModeEdition.EnableSurEdition);
                ctrl.Parent = m_panelFiltre;
                ctrl.Dock = DockStyle.Fill;
                editeur.Init(m_odeqTable, m_postFilter);
                m_editeurEnCours = editeur;
            }
            else
                m_editeurEnCours = null;
        }

        //------------------------------------------------------------------------
        private void m_cmbTypePostFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CDefPostFilter def = m_cmbTypePostFilter.SelectedValue as CDefPostFilter;
            if (def != null && def.TypePostFilter != null)
            {
                if (m_postFilter == null || m_postFilter.GetType() != def.TypePostFilter)
                {
                    m_postFilter = Activator.CreateInstance(def.TypePostFilter, new object[0]) as IPostFilter;
                    UpdatePanelPostFilter();
                }
            }
            else
            {
                m_postFilter = null;
                UpdatePanelPostFilter();
            }
            
        }

        //------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            CResultAErreurType<IPostFilter> resPost = new CResultAErreurType<IPostFilter>();
            if (m_editeurEnCours != null)
            {
                resPost = m_editeurEnCours.MajChamps();
                if (!resPost)
                {
                    result.EmpileErreur(resPost.Erreur);
                    return result;
                }
                if (m_odeqTable != null)
                    m_odeqTable.PostFilter = resPost.DataType;
            }
            else
                m_odeqTable.PostFilter = null;
            return result;

        }
    }
}
