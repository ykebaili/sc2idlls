using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.common;

namespace sc2i.test.metier
{
    public partial class CFormTestFormule : Form
    {
        public CFormTestFormule()
        {
            InitializeComponent();
        }

        private void CFormTestFormule_Load(object sender, EventArgs e)
        {
            m_txtFormule.Init(new CFournisseurPropDynStd(true), typeof(string));
        }

        private void m_btnEval_Click(object sender, EventArgs e)
        {
            C2iExpression formule = m_txtFormule.Formule;
            CResultAErreur res = CResultAErreur.True;
            if (formule == null)
            {
                res = m_txtFormule.ResultAnalyse;
            }
            else
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression("");
                res = formule.Eval(ctx);
            }
            if (!res)
            {
                m_txtResult.Text = res.Erreur.ToString();
            }
            else
            {
                if (res.Data == null)
                    m_txtResult.Text = "null";
                else
                    m_txtResult.Text = res.Data.ToString();
            }
        }
                
         
    }
}
