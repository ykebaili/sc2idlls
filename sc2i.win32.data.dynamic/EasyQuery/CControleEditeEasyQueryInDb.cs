using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.common;
using sc2i.data.dynamic;
using futurocom.easyquery;

namespace sc2i.win32.data.dynamic.EasyQuery
{
    public partial class CControleEditeEasyQueryInDb : UserControl, IControlALockEdition
    {
        private CEasyQueryInDb m_queryInDb = null;


        public CControleEditeEasyQueryInDb()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(typeof(CControleEditeEasyQueryInDb), this);
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        //-----------------------------------------------
        public CResultAErreur InitChamps(CEasyQueryInDb queryInDb)
        {
            CResultAErreur result = CResultAErreur.True;
            m_queryInDb = queryInDb;
            CEasyQuery query = queryInDb.EasyQueryAvecSources;
            m_panelQuery.Init(query);

            result = m_extLinkField.FillDialogFromObjet(queryInDb);
            return result;
        }


        //-----------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if ( !LockEdition && m_queryInDb != null )
            {
                result = m_extLinkField.FillObjetFromDialog ( m_queryInDb );
                if (result)
                    m_queryInDb.EasyQueryAvecSources = m_panelQuery.Query;
            }
            return result;
        }
    }
}
