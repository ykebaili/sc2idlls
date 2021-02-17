using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression.FonctionsDynamiques;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.expression
{
    public partial class CFormEditionFonctionDynamique : Form
    {
        public CFormEditionFonctionDynamique()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //------------------------------------------------------------------------------------------------------
        public static bool EditeFonction(
            ref CFonctionDynamique fonction, 
            CObjetPourSousProprietes objetAnalyse,
            params KeyValuePair<string, Type>[] variablesSpecifiques)
        {
            if (fonction == null)
                return false;
            CFormEditionFonctionDynamique form = new CFormEditionFonctionDynamique();
            CFonctionDynamique fTmp = CCloner2iSerializable.Clone(fonction) as CFonctionDynamique;
            form.m_panelFonction.Init(fTmp, objetAnalyse, true);
            foreach (KeyValuePair<string, Type> kv in variablesSpecifiques)
                form.m_panelFonction.AddSpecificVariableTypes(kv.Key, kv.Value);
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                fonction = fTmp;
                bResult = true;
            }
            form.Dispose();
            return bResult;
        }


        //------------------------------------------------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = m_panelFonction.MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        //------------------------------------------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
