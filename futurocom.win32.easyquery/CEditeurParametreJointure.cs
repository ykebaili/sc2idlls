using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.common;

namespace futurocom.win32.easyquery
{
    public partial class CEditeurParametreJointure : UserControl
    {
        private CParametreJointure m_parametre = null;

        //-------------------------------------------------
        public CEditeurParametreJointure()
        {
            InitializeComponent();
        }

        //-------------------------------------------------
        public void Init(CODEQJointure objetJointure, CParametreJointure parametre)
        {
            Init(objetJointure.Table1, objetJointure.Table2, parametre);
            /*m_parametre = CCloner2iSerializable.Clone(parametre) as CParametreJointure;
            m_cmbOperateur.Items.Clear();

            if (objetJointure.Table1 != null)
                m_txtFormule1.Init(objetJointure.Table1, objetJointure.Table1.GetType());
            if (objetJointure.Table2 != null)
                m_txtFormule2.Init(objetJointure.Table2, objetJointure.Table2.GetType());

            foreach (EOperateurJointure operateur in Enum.GetValues(typeof(EOperateurJointure)))
            {
                switch (operateur)
                {
                    case EOperateurJointure.Egal:
                        m_cmbOperateur.Items.Add("=");
                        break;
                    case EOperateurJointure.Superieur:
                        m_cmbOperateur.Items.Add(">");
                        break;
                    case EOperateurJointure.SuperieurEgal:
                        m_cmbOperateur.Items.Add(">=");
                        break;
                    case EOperateurJointure.Inferieur:
                        m_cmbOperateur.Items.Add("<");
                        break;
                    case EOperateurJointure.InferieurEgal:
                        m_cmbOperateur.Items.Add("<=");
                        break;
                    case EOperateurJointure.Different:
                        m_cmbOperateur.Items.Add("<>");
                        break;
                    default:
                        break;
                }
                m_txtFormule1.Formule = m_parametre.FormuleTable1;
                m_txtFormule2.Formule = m_parametre.FormuleTable2;
                m_cmbOperateur.SelectedIndex = (int)m_parametre.Operateur;
            }*/
        }

        //-------------------------------------------------
        public void Init(IObjetDeEasyQuery table1, IObjetDeEasyQuery table2, CParametreJointure parametre)
        {
            if (table1 == null || table2 == null)
            {
                Visible = false;
                return;
            }
            Visible = true;
            m_parametre = CCloner2iSerializable.Clone(parametre) as CParametreJointure;
            m_cmbOperateur.Items.Clear();
            
            if ( table1 != null )
                m_txtFormule1.Init(table1, table1.GetType());
            if ( table2 != null )
                m_txtFormule2.Init(table2, table2.GetType());

            foreach (EOperateurJointure operateur in Enum.GetValues(typeof(EOperateurJointure)))
            {
                switch (operateur)
                {
                    case EOperateurJointure.Egal:
                        m_cmbOperateur.Items.Add("=");
                        break;
                    case EOperateurJointure.Superieur:
                        m_cmbOperateur.Items.Add(">");
                        break;
                    case EOperateurJointure.SuperieurEgal:
                        m_cmbOperateur.Items.Add(">=");
                        break;
                    case EOperateurJointure.Inferieur:
                        m_cmbOperateur.Items.Add("<");
                        break;
                    case EOperateurJointure.InferieurEgal:
                        m_cmbOperateur.Items.Add("<=");
                        break;
                    case EOperateurJointure.Different:
                        m_cmbOperateur.Items.Add("<>");
                        break;
                    default:
                        break;
                }
                m_txtFormule1.Formule = m_parametre.FormuleTable1;
                m_txtFormule2.Formule = m_parametre.FormuleTable2;
                m_cmbOperateur.SelectedIndex = (int)m_parametre.Operateur;
            }
        }

        //-------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_txtFormule1.Formule == null)
            {
                if (!m_txtFormule1.ResultAnalyse)
                    result += m_txtFormule1.ResultAnalyse;
                result.EmpileErreur(I.T("Error in formula 1|20012"));
            }
            if (m_txtFormule2.Formule == null)
            {
                if (!m_txtFormule2.ResultAnalyse)
                    result += m_txtFormule2.ResultAnalyse;
                result.EmpileErreur(I.T("Error in formula 2|20013"));
            }
            if (!result)
                return result;
            m_parametre.FormuleTable1 = m_txtFormule1.Formule;
            m_parametre.FormuleTable2 = m_txtFormule2.Formule;
            m_parametre.Operateur = (EOperateurJointure)m_cmbOperateur.SelectedIndex;
            return result;
        }

        //-------------------------------------
        private void m_lnkDelete_LinkClicked(object sender, EventArgs e)
        {
            CFormEditeProprietesJointure frm = FindForm() as CFormEditeProprietesJointure;
            if (frm != null)
                frm.RemoveParametre(this);
        }

        //-------------------------------------
        public CParametreJointure Parametre
        {
            get
            {
                return m_parametre;
            }
        }
    }
}
