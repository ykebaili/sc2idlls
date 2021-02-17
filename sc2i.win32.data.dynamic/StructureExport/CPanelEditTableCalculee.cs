using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.common;
using System.Reflection;
using sc2i.data;
using sc2i.expression;
using sc2i.win32.expression;


namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Editeur pour les tables d'export calculées indépendantes
	/// </summary>
    [EditeurTableExport(typeof(C2iTableExportCalculee))]
	public partial class CPanelEditTableCalculee : UserControl, IControlALockEdition, IPanelEditTableExport
	{
		private C2iTableExportCalculee m_tableCalculee = null;
		private C2iStructureExport m_structureExport = null;
        private IElementAVariables m_elementAVariablesExternes;
        private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesDynamiques;

		//---------------------------------------------------------------------------------
		public CPanelEditTableCalculee()
		{
			InitializeComponent();
		}


        //----------------------------------------------------------------------------------
        public ITableExport TableEditee
        {
            get { return m_tableCalculee; }
        }

		//----------------------------------------------------------------------------------
		public CResultAErreur InitChamps(
            ITableExport table, 
            ITableExport tableParente,
            C2iStructureExport structure,
            IElementAVariablesDynamiquesAvecContexteDonnee eltAVariablesDynamiques)
		{
            CResultAErreur result = CResultAErreur.True;

            m_tableCalculee = table as C2iTableExportCalculee;

            if (m_tableCalculee == null)
                return CResultAErreur.False;

            m_structureExport = structure;

            m_txtNomTable.Text = m_tableCalculee.NomTable;

            if (m_tableCalculee.FormuleValeur != null)
                m_txtFormuleValeur.Text = m_tableCalculee.FormuleValeur.GetString();
            else
                m_txtFormuleValeur.Text = "";
            if (m_tableCalculee.FormuleNbRecords != null)
                m_txtFormuleNbRecords.Text = m_tableCalculee.FormuleNbRecords.GetString();
            else
                m_txtFormuleNbRecords.Text = "0";

            ElementAVariablesDynamiques = eltAVariablesDynamiques;

            C2iTableExportCalculee.CTypeForFormule leType = new C2iTableExportCalculee.CTypeForFormule(m_elementAVariablesDynamiques);

			m_wndAideFormule.ObjetInterroge = typeof(C2iTableExportCalculee.CTypeForFormule);
            m_wndAideFormule.FournisseurProprietes = leType;

			m_txtFormuleValeur.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);
			m_txtFormuleNbRecords.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);

            return result;
		}

        //-------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;

            CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(new CContexteAnalyse2iExpression(m_wndAideFormule.FournisseurProprietes, typeof(C2iTableExportCalculee.CTypeForFormule)));
            if (m_txtFormuleValeur.Text.Trim() != "")
            {
                result = analyseur.AnalyseChaine(m_txtFormuleValeur.Text);
                if (!result)
                {
                    result.EmpileErreur(I.T("Incorrect value formula|30003"));
                    CFormAlerte.Afficher(result);
                    return result;
                }
                m_tableCalculee.FormuleValeur = (C2iExpression)result.Data;
            }
            else
                m_tableCalculee.FormuleValeur = null;
            result = analyseur.AnalyseChaine(m_txtFormuleNbRecords.Text);
            if (!result)
            {
                result.EmpileErreur(I.T("Error in record number formula|30004"));
                CFormAlerte.Afficher(result);
                return result;
            }
            m_tableCalculee.FormuleNbRecords = (C2iExpression)result.Data;

            m_tableCalculee.NomTable = m_txtNomTable.Text;

            return result;
        }

        //-------------------------------------------------------------------------
        public IElementAVariables ElementAVariablesExternes
        {
            get
            {
                return m_elementAVariablesExternes;
            }
            set
            {
                m_elementAVariablesExternes = value;
            }

        }


        //-------------------------------------------------------------------------
        public IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesDynamiques
        {
            get
            {
                return m_elementAVariablesDynamiques;
            }
            set
            {
                m_elementAVariablesDynamiques = value;
            }

        }



		//---------------------------------------------------------------------------
		private void CPanelEditTableCalculee_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
		}

        //---------------------------------------------------------------------------
        private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
        {
            if (m_txtFormule == null)
                m_txtFormule = m_txtFormuleValeur;
            m_wndAideFormule.InsereInTextBox(m_txtFormule, nPosCurseur, strCommande);
        }

        //-------------------------------------------------------------------------------
        private CControleEditeFormule m_txtFormule = null;
        private void OnEnterFormule(object sender, System.EventArgs e)
        {
            if (sender is CControleEditeFormule)
            {
                if (m_txtFormule!= null)
                    m_txtFormule.BackColor = Color.White;
                m_txtFormule = (CControleEditeFormule)sender;
                m_txtFormule.BackColor = Color.LightGreen;
            }
        }

        #region IControlALockEdition Membres

        //-------------------------------------------------------------------------------
        public bool LockEdition
        {
			get
            {
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

 
    }
}
