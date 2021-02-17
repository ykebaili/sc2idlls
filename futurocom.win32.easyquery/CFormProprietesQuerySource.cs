using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.win32.common.customizableList;
using sc2i.win32.common;

namespace futurocom.win32.easyquery
{
    public partial class CFormProprietesQuerySource : Form
    {
        CEasyQuerySource m_source = null;
        public CFormProprietesQuerySource()
        {
            InitializeComponent();
            m_propertyGrid.ItemControl = new CEditeurParametreConnexion();
            CWin32Traducteur.Translate(this);
        }

        public static bool EditeParametres(CEasyQuerySource source)
        {
            CFormProprietesQuerySource frm = new CFormProprietesQuerySource();
            frm.m_source = source;
            DialogResult res = frm.ShowDialog();
            frm.Dispose();
            return res == DialogResult.OK;
        }

        private void CFormProprietesConnexion_Load(object sender, EventArgs e)
        {
            CEasyQuerySource source = m_source;
            IEasyQueryConnexion cnx = source.Connexion;
            IEnumerable<CEasyQueryConnexionProperty> props = cnx.ConnexionProperties;
            List<CCustomizableListItem> lst = new List<CCustomizableListItem>();
            m_txtNomSource.Text = source.SourceName;

            foreach (CEasyQueryConnexionProperty prop in props)
            {
                CCustomizableListItem item = new CCustomizableListItem();
                item.Tag = prop;
                lst.Add(item);
            }
            m_propertyGrid.Items = lst.ToArray();
            m_propertyGrid.Refill();
        }

        //----------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            List<CEasyQueryConnexionProperty> props = new List<CEasyQueryConnexionProperty>();
            m_propertyGrid.MajChamps();
            foreach ( CCustomizableListItem item in m_propertyGrid.Items )
            {
                CEasyQueryConnexionProperty prop = item.Tag as CEasyQueryConnexionProperty;
                if ( prop != null )
                    props.Add ( prop );
            }
            m_source.Connexion.ConnexionProperties = props;
            m_source.SourceName = m_txtNomSource.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        //----------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        

    }
}
