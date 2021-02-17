using sc2i.win32.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.data.Package;
using sc2i.common;

namespace sc2i.win32.data.Package
{
    public partial class CFormParametresRecherche : Form
    {
        private const string c_cleFichier = "FUT_CONF_DEP_SEARCH";
        private sc2i.data.Package.CConfigurationRechercheEntites m_configuration = new CConfigurationRechercheEntites();
        public CFormParametresRecherche()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //---------------------------------------------------------------------------
        private void CFormParametresRecherche_Load(object sender, EventArgs e)
        {
            m_panelOptions.Init(m_configuration);
        }

        //---------------------------------------------------------------------------
        public static CConfigurationRechercheEntites EditeConfiguration ( CConfigurationRechercheEntites configuration )
        {
            using ( CFormParametresRecherche frm = new CFormParametresRecherche())
            {
                frm.m_configuration = CCloner2iSerializable.CloneGeneric<CConfigurationRechercheEntites>(configuration);
                if ( frm.ShowDialog() == DialogResult.OK )
                {
                    return frm.m_configuration;
                }
            }
            return null;
        }

        //---------------------------------------------------------------------------
        private void m_btnValider_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        //---------------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //---------------------------------------------------------------------------
        private void m_btnLoadConfig_Click(object sender, EventArgs e)
        {
            using ( OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = I.T("Dependancies search configuration|*.depConf|All files|*.*|20011");
                if ( dlg.ShowDialog() == DialogResult.OK )
                {
                    CConfigurationRechercheEntites configuration = new CConfigurationRechercheEntites();
                    CResultAErreur result = CSerializerObjetInFile.ReadFromFile(configuration, c_cleFichier, dlg.FileName);
                    if (!result)
                        CFormAlerte.Afficher(result.Erreur);
                    else
                    {
                        m_configuration = configuration;
                        m_panelOptions.Init(configuration);
                    }

                }
            }
        }

        //---------------------------------------------------------------------------
        private void m_btnSaveConfig_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = I.T("Dependancies search configuration|*.depConf|All files|*.*|20011");
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    CConfigurationRechercheEntites configuration = m_configuration;
                    CResultAErreur result = CSerializerObjetInFile.SaveToFile(configuration, c_cleFichier, dlg.FileName);
                    if (!result)
                        CFormAlerte.Afficher(result.Erreur);
                }
            }
        }

        //---------------------------------------------------------------------------

    }
}
