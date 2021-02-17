using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.expression
{
    public partial class CFormEditionExpressionGraphique : Form
    {
        C2iExpressionGraphique m_expression = new C2iExpressionGraphique();
        private IFournisseurProprietesDynamiques m_fournisseur;
        private CObjetPourSousProprietes m_objetAnalyse;


        public CFormEditionExpressionGraphique()
        {
            InitializeComponent();
        }

        public static C2iExpression EditeFormule(C2iExpression formule, IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes objetAnalyse)
        {
            CFormEditionExpressionGraphique form = new CFormEditionExpressionGraphique();
            C2iExpressionGraphique graf = formule as C2iExpressionGraphique;
            if (graf == null)
            {
                graf = new C2iExpressionGraphique();
                graf.InitFromFormule(formule);
            }
            form.m_expression = graf;
            form.m_fournisseur = fournisseur;
            form.m_objetAnalyse = objetAnalyse;
            form.m_editeur.Init(graf, fournisseur, objetAnalyse);
            C2iExpression retour = formule ;
            if (form.ShowDialog() == DialogResult.OK)
            {
                retour = form.m_expression;
            }
            form.Dispose();
            return retour;
        }

        private void CFormEditionExpressionGraphique_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //-----------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            C2iExpressionGraphique exp = m_editeur.ExpressionGraphique;
            CResultAErreur result = exp.RefreshFormuleFinale();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            C2iExpression formule = exp.FormuleFinale;
            result = formule.VerifieParametres();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            m_expression = exp;
            DialogResult = DialogResult.OK;
            Close();
        }

        //-----------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        

    }
}
