using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.win32.common.customizableList;
using sc2i.common;
using futurocom.easyquery;

namespace futurocom.win32.easyquery
{
    public partial class CEditeurParametreConnexion : CCustomizableListControl
    {
        public CEditeurParametreConnexion()
        {
            InitializeComponent();
        }

        //--------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result =  base.MyInitChamps(item);
            if (!result)
                return result;
            CEasyQueryConnexionProperty prop = item != null ? item.Tag as CEasyQueryConnexionProperty : null;
            if (prop != null)
            {
                m_lblNomParametre.Text = prop.Property;
                m_txtValeur.Text = prop.Value;
            }
            return result;
        }

        //--------------------------------------------
        public override bool HasChange
        {
            get
            {
                return true;
            }
            set
            {
                base.HasChange = value;
            }
        }

        //--------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CEasyQueryConnexionProperty prop = CurrentItem != null ? CurrentItem.Tag as CEasyQueryConnexionProperty : null;
            if (prop != null)
                prop.Value = m_txtValeur.Text;
            return CResultAErreur.True;
        }


    }
}
