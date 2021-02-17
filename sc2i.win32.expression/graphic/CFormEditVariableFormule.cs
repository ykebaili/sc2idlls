using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.win32.expression
{
    public partial class CFormEditVariableFormule : Form
    {
        CDefinitionProprieteDynamiqueVariableFormule m_variable = null;
        CParametreFonctionDynamique m_parametre = null;

        public CFormEditVariableFormule()
        {
            InitializeComponent();
        }

        private void CFormEditVariableFormule_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            List<string> lstTypes = new List<string>();
            foreach (CInfoClasseDynamique info in DynamicClassAttribute.GetAllDynamicClass())
            {
                lstTypes.Add(info.Nom);
            }
            lstTypes.Add(typeof(Int32).Name);
            lstTypes.Add(typeof(string).Name);
            lstTypes.Add(typeof(DateTime).Name);
            lstTypes.Add(typeof(double).Name);
            lstTypes.Add(typeof(bool).Name);
            m_txtType.AutoCompleteSource = AutoCompleteSource.CustomSource;
            m_txtType.AutoCompleteMode = AutoCompleteMode.Suggest;
            m_txtType.AutoCompleteCustomSource.Clear();
            m_txtType.AutoCompleteCustomSource.AddRange(lstTypes.ToArray());
        }

        public static CParametreFonctionDynamique EditeParametreFonction(CParametreFonctionDynamique parametre)
        {
            if (parametre == null)
                return null;
            CFormEditVariableFormule form = new CFormEditVariableFormule();
            if (parametre != null)
            {
                form.m_txtNom.Text = parametre.Nom;
                form.m_txtType.Text = DynamicClassAttribute.GetNomConvivial(parametre.TypeResultatExpression.TypeDotNetNatif);
                form.m_chkArray.Checked = parametre.TypeResultatExpression.IsArrayOfTypeNatif;
            }
            else
                parametre = new CParametreFonctionDynamique();
            form.m_parametre = parametre;
            CParametreFonctionDynamique retour = null;
            if (form.ShowDialog() == DialogResult.OK)
                retour = form.m_parametre;
            form.Dispose();
            return retour;
        }


        public static CDefinitionProprieteDynamiqueVariableFormule EditeVariable(CDefinitionProprieteDynamiqueVariableFormule variable)
        {
            CFormEditVariableFormule form = new CFormEditVariableFormule();
            if (variable != null)
            {
                form.m_txtNom.Text = variable.Nom;
                form.m_txtType.Text = DynamicClassAttribute.GetNomConvivial(variable.TypeDonnee.TypeDotNetNatif);
                form.m_chkArray.Checked = variable.TypeDonnee.IsArrayOfTypeNatif;
            }
            else
                variable = new CDefinitionProprieteDynamiqueVariableFormule();
            form.m_variable = variable;
            CDefinitionProprieteDynamiqueVariableFormule retour = null;
            if (form.ShowDialog() == DialogResult.OK)
                retour = form.m_variable;
            form.Dispose();
            return retour;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNom.Text == "")
            {
                MessageBox.Show("Name ?");
                return;
            }
            Type tp = null;
            if (m_variable != null && m_variable.TypeDonnee != null)
                tp = m_variable.TypeDonnee.TypeDotNetNatif;
            if (m_parametre != null && m_parametre.TypeResultatExpression != null)
                tp = m_parametre.TypeResultatExpression.TypeDotNetNatif;
            if (tp == null || m_txtType.Text != DynamicClassAttribute.GetNomConvivial(tp))
            {
                tp = CActivatorSurChaine.GetType(m_txtType.Text, true);
                if (tp == null)
                {
                    MessageBox.Show(I.T("Invalid type|20023"));
                    return;
                }
            }
            if (m_variable != null)
            {
                m_variable = new CDefinitionProprieteDynamiqueVariableFormule(
                    m_txtNom.Text, new CTypeResultatExpression(tp, m_chkArray.Checked), true);
            }
            if (m_parametre != null)
            {
                m_parametre.Nom = m_txtNom.Text;
                m_parametre.TypeResultatExpression = new CTypeResultatExpression(tp, m_chkArray.Checked);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
