using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CFormOptionsFiltreDynamique : Form
    {
        private CFiltreDynamique m_filtre;


        public CFormOptionsFiltreDynamique()
        {
            InitializeComponent();
        }

        private void CFormOptionsFiltreDynamique_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_txtFormuleIncludeChilds.Init(m_filtre, typeof(CFiltreDynamique));
            m_txtFormuleIncludeParents.Init(m_filtre, typeof(CFiltreDynamique));
            m_txtFormuleRootOnly.Init(m_filtre, typeof(CFiltreDynamique));

            m_txtFormuleIncludeChilds.Formule = m_filtre.FormuleIntegrerFilsHierarchiques;
            m_txtFormuleIncludeParents.Formule = m_filtre.FormuleIntegrerParentsHierarchiques;
            m_txtFormuleRootOnly.Formule = m_filtre.FormuleNeConserverQueLesRacines;
        }

        public static void EditeOptions(CFiltreDynamique filtre)
        {
            CFormOptionsFiltreDynamique form = new CFormOptionsFiltreDynamique();
            form.m_filtre = filtre;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_txtFormuleRootOnly.Formule == null || m_txtFormuleRootOnly.Formule.TypeDonnee.TypeDotNetNatif != typeof( bool ))
            {
                result.EmpileErreur(I.T("Root elements only formula is incorrect|20055"));
            }
            if (m_txtFormuleIncludeParents.Formule == null || m_txtFormuleIncludeParents.Formule.TypeDonnee.TypeDotNetNatif != typeof(bool))
                result.EmpileErreur(I.T("Include parent elements is incorrect|20056"));
            if (m_txtFormuleIncludeChilds.Formule == null || m_txtFormuleIncludeChilds.Formule.TypeDonnee.TypeDotNetNatif != typeof(bool))
                result.EmpileErreur(I.T("Include child elements is incorrect|20057"));
            if (!result)
            {
                CFormAfficheErreur.Show(result.Erreur);
                return;
            }
            m_filtre.FormuleNeConserverQueLesRacines = m_txtFormuleRootOnly.Formule;
            m_filtre.FormuleIntegrerParentsHierarchiques = m_txtFormuleIncludeParents.Formule;
            m_filtre.FormuleIntegrerFilsHierarchiques = m_txtFormuleIncludeChilds.Formule;
            DialogResult = DialogResult.OK;
            Close();
        }
            
    }
}
