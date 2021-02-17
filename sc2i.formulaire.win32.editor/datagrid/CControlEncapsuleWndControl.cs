using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire.datagrid;
using sc2i.expression;
using System.Collections;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire.win32.controles2iWnd.datagrid;

namespace sc2i.formulaire.win32.datagrid
{
    //--------------------------------------------------------------
    public partial class CControlEncapsuleWndControl : UserControl, IDataGridViewEditingControl
    {
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IWndIncluableDansDataGrid m_wndSource = null;
        private CGridDataCache m_datas = null;
        private IControlIncluableDansDataGrid m_wndEdition = null;
        private IFournisseurProprietesDynamiques m_fournisseur;
        private DataGridView m_dataGridView = null;
        private int m_nRowIndex = -1;
        private int m_nColumnIndex = 0;
        private CCreateur2iFormulaireV2 m_createurFormulaires = null;

        private bool m_bValueChanged = false;

        public CControlEncapsuleWndControl()
        {
        }

        //--------------------------------------------------------------
        public void InitRestrictions(CListeRestrictionsUtilisateurSurType lstRestrictions)
        {
            if (lstRestrictions != null)
                m_listeRestrictions = lstRestrictions.Clone() as CListeRestrictionsUtilisateurSurType;
            else
                m_listeRestrictions = null;
        }

        //--------------------------------------------------------------
        public void Initialiser( 
            CGridDataCache datas,
            int nColumnIndex,
            IWndIncluableDansDataGrid wndIncluable,
            IFournisseurProprietesDynamiques fournisseur )
        {
            m_datas = datas;
            m_nColumnIndex = nColumnIndex;
            m_wndSource = wndIncluable;
            m_fournisseur = fournisseur;
            if (m_wndEdition != null && m_wndEdition.Control != null)
            {
                m_wndEdition.Control.Visible = false;
                Controls.Remove(m_wndEdition.Control);
                m_wndEdition.Control.Dispose();
                m_wndEdition = null;
            }
                

            InitializeComponent();


            m_createurFormulaires = new CCreateur2iFormulaireV2();
            m_wndEdition = m_createurFormulaires.CreateControle((C2iWnd)m_wndSource, this, m_fournisseur) as IControlIncluableDansDataGrid;
            if (m_wndEdition != null && m_wndEdition.Control != null)
            {
                m_wndEdition.Control.Dock = DockStyle.Fill;
            }
            IControlIncluableDansDataGrid ctrlDg = m_wndEdition as IControlIncluableDansDataGrid;
            if (ctrlDg != null)
                ctrlDg.DataGrid = m_dataGridView;
            
        }

        //--------------------------------------------------------------
        public void ApplyModifs()
        {

        }

        #region IDataGridViewEditingControl Membres

        //--------------------------------------------------------------
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            
        }

        //--------------------------------------------------------------
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return m_dataGridView;
            }
            set
            {
                m_dataGridView = value;
                if ( m_wndEdition != null )
                    m_wndEdition.DataGrid = value;
            }
        }

        //--------------------------------------------------------------
        public object EditingControlFormattedValue
        {
            get
            {
                return "";
            }
            set
            {
                
            }
        }

        //--------------------------------------------------------------
        public int EditingControlRowIndex
        {
            get
            {
                return m_nRowIndex;
            }
            set
            {
                m_nRowIndex = value;
            }
        }

        //--------------------------------------------------------------
        public bool EditingControlValueChanged
        {
            get
            {
                return m_bValueChanged;
            }
            set
            {
                m_bValueChanged = value;
            }
        }

        //--------------------------------------------------------------
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if ( m_wndEdition != null )
                return m_wndEdition.WantsInputKey(keyData, dataGridViewWantsInputKey);
            return true;
        }

        //--------------------------------------------------------------
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        //--------------------------------------------------------------
        public void MajChamps()
        {
            ((IControlIncluableDansDataGrid)m_wndEdition).MajChamps(true);
        }

        //--------------------------------------------------------------
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            //return ((IControlIncluableDansDataGrid)m_wndEdition).GetValue();
            return "";//Apparement ne sert jamais
        }

        //--------------------------------------------------------------
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            try
            {
                m_wndEdition.SetElementEdite(ElementEdite);
                if (m_createurFormulaires != null && m_wndEdition != null && m_wndEdition.Control != null)
                    m_createurFormulaires.UpdateVisibilityEtEnable(m_wndEdition, ElementEdite);
                if (m_listeRestrictions != null && ElementEdite != null)
                {
                    CRestrictionUtilisateurSurType rest = m_listeRestrictions.GetRestriction(ElementEdite.GetType());
                    if (rest != null)
                    {
                        rest.ApplyToObjet(ElementEdite);
                        m_wndEdition.AppliqueRestriction(rest, m_listeRestrictions, new CGestionnaireReadOnlySysteme());
                    }
                }
                m_wndEdition.SelectAll();
            }
            catch { }
        }

        //--------------------------------------------------------------
        private object ElementEdite
        {
            get
            {
                IList lst = m_dataGridView.DataSource as IList;
                if (lst != null)
                {
                    object element = lst[m_nRowIndex];
                    if ( m_datas != null )
                        element = m_datas.GetElementEdite (element, m_nColumnIndex );
                    return element;
                }
                return null;
            }
        }

        //--------------------------------------------------------------
        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        #endregion
    }
}
