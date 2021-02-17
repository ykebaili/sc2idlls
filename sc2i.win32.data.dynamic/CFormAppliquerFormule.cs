using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data;
using sc2i.expression;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CFormAppliquerFormule : Form
    {
        private List<CObjetDonnee> m_listeObjetsAAppliquer = new List<CObjetDonnee>();
        private CContexteDonnee m_contexteModification = null;
        
        public CFormAppliquerFormule()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------------
        public static bool ApplyFormula(CListeObjetsDonnees lst)
        {
            return ApplyFormula(lst.ToArray<CObjetDonnee>());
        }

        //-------------------------------------------------------------
        public static bool ApplyFormula(CObjetDonnee[] objets)
        {
            if (objets.Length == 0)
                return false;
            CFormAppliquerFormule form = new CFormAppliquerFormule();
            List<CObjetDonnee> lstSource = new List<CObjetDonnee>(objets);
            form.m_listeObjetsAAppliquer.AddRange(lstSource);
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
                bResult = true;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------------
        private void CFormRunFormula_Load(object sender, EventArgs e)
        {
            Type typeObjets = m_listeObjetsAAppliquer[0].GetType();
            m_txtFormule.Init(new CFournisseurGeneriqueProprietesDynamiques(),
                typeObjets);
            m_lblApplyTo.Text = I.T("Apply formula to @1 @2|20092",
                m_listeObjetsAAppliquer.Count().ToString(),
                DynamicClassAttribute.GetNomConvivial(typeObjets));
        }

        //-------------------------------------------------------------
        private CResultAErreur AppliqueFormule()
        {
            CResultAErreur result = CResultAErreur.True;
            C2iExpression formule = m_txtFormule.Formule;
            if (formule == null)
            {
                result = m_txtFormule.ResultAnalyse;
                return result;
            }
            CContexteDonnee ctxOriginal = m_listeObjetsAAppliquer[0].ContexteDonnee;
            try
            {
                if (m_contexteModification != null)
                {
                    m_contexteModification.Dispose();
                    m_contexteModification = null;
                }
                m_contexteModification = ctxOriginal.GetContexteEdition();
                foreach (CObjetDonnee objet in m_listeObjetsAAppliquer)
                {
                    if (objet != null)
                    {
                        CObjetDonnee objetModif = objet.GetObjetInContexte(m_contexteModification);
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(objetModif);
                        result = formule.Eval(ctx);
                        if (!result)
                        {
                            result.EmpileErreur(I.T("Error while applying formula on @1|20093", objetModif.DescriptionElement));
                            return result;
                        }
                        else
                        {
                            m_txtResult.Text += "-----------------------------------";
                            m_txtResult.Text += Environment.NewLine;
                            m_txtResult.Text += objetModif.DescriptionElement + "->";
                            m_txtResult.Text += result.Data != null ? result.Data.ToString() : "NULL";
                            m_txtResult.Text += Environment.NewLine;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

        //----------------------------------------------------------------
        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //----------------------------------------------------------------
        private void m_btnAppliquer_Click(object sender, EventArgs e)
        {
            m_btnOk.Enabled = false;
            CResultAErreur result = AppliqueFormule();
            if (!result)
                CFormAlerte.Afficher(result.Erreur);
            m_btnOk.Enabled = true;
        }

        //----------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_contexteModification != null)
            {
                CResultAErreur result = m_contexteModification.CommitEdit();
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
