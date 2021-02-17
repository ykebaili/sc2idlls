using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.expression.variablesDynamiques
{
    public partial class CControleVariablesDynamiques : UserControl, IControlALockEdition
    {
        private IElementAVariablesDynamiquesBase m_elementAVariables = null;

        private bool m_bShortMode = false;

        //-------------------------------------------------------------------------
        public CControleVariablesDynamiques()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------
        private void m_wndListeVariables_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeVariables.Columns[0].Width = m_wndListeVariables.ClientSize.Width - 20;

        }

        //-------------------------------------------------------------------------
        public bool ShortMode
        {
            get
            {
                return m_bShortMode;
            }
            set
            {
                m_bShortMode = value;
                m_btnAddVariable.ShortMode = value;
                m_btnDeleteVariable.ShortMode = value;
                m_btnDetailVariable.ShortMode = value;
            }
        }

        //-------------------------------------------------------------------------
        public void Init(IElementAVariablesDynamiquesBase elt)
        {
            m_elementAVariables = elt;
            FillListeVariables();
        }

        //-------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        //-------------------------------------------------------------------------
        private void FillListeVariables()
        {
            m_wndListeVariables.BeginUpdate();
            m_wndListeVariables.Items.Clear();
            foreach (IVariableDynamique variable in m_elementAVariables.ListeVariables)
            {
                ListViewItem item = new ListViewItem();
                FillItem(item, variable);
                m_wndListeVariables.Items.Add(item);
            }
            m_wndListeVariables.EndUpdate();
        }

        //-------------------------------------------------------------------------
        private void FillItem(ListViewItem item, IVariableDynamique variable)
        {
            item.Text = variable.Nom;
            item.Tag = variable;
        }

        //-------------------------------------------------------------------------
        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        //-------------------------------------------------------------------------
        public event EventHandler OnChangeLockEdition;

        //-------------------------------------------------------------------------
        private void m_btnAddVariable_LinkClicked(object sender, EventArgs e)
        {
            AssureMenuAddVariable();
            m_menuAddVariable.Show(m_btnAddVariable, new Point(0, m_btnAddVariable.Height));
        }

        //-------------------------------------------------------------------------
        private void AssureMenuAddVariable()
        {
            if (m_menuAddVariable.Items.Count > 0)
                return;
            foreach (CDescripteurTypeVariableDynamique desc in CGestionnaireVariablesDynamiques.GetTypesVariablesConnus())
            {
                ToolStripMenuItem itemNewVariable = new ToolStripMenuItem(desc.LibelleTypeVariable);
                itemNewVariable.Tag = desc.TypeVariable;
                itemNewVariable.Click += new EventHandler(itemNewVariable_Click);
                m_menuAddVariable.Items.Add(itemNewVariable);
            }
        }

        //-------------------------------------------------------------------------
        void itemNewVariable_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Type tp = item != null ? item.Tag as Type : null;
            if (tp != null)
            {
                IVariableDynamique variable = null;
                try
                {
                    variable = Activator.CreateInstance(tp, new object[] { m_elementAVariables }) as IVariableDynamique;
                }
                catch (Exception ex)
                {
                    CResultAErreur result = CResultAErreur.True;
                    result.EmpileErreur(new CErreurException(ex));
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
                if (variable != null && EditeVariable(variable))
                {
                    ListViewItem lvItem = new ListViewItem();
                    FillItem(lvItem, variable);
                    m_wndListeVariables.Items.Add(lvItem);
                    m_elementAVariables.AddVariable(variable);
                }
            }
        }

        //-------------------------------------------------------------------------
        private ListViewItem GetItemForVariable ( IVariableDynamique variable )
        {
            foreach ( ListViewItem  item in m_wndListeVariables.Items )
                if ( item.Tag == variable )
                    return item;
            return null;
        }


        //-------------------------------------------------------------------------
        private bool EditeVariable(IVariableDynamique variable)
        {
            if (variable == null)
                return false;
            Type typeEditeur = CGestionnaireEditeursVariablesDynamiques.GetTypeEditeur(variable.GetType());
            if (typeEditeur == null)
                return false;
            IFormEditVariableDynamique frm = Activator.CreateInstance(typeEditeur) as IFormEditVariableDynamique;
            bool bResult = frm.EditeLaVariable(variable, m_elementAVariables);
            if (bResult)
            {
                ListViewItem item = GetItemForVariable(variable);
                if (item != null)
                    item.Text = variable.Nom;
            }
            return bResult;
        }

        //------------------------------------------------------------------------------
        private void m_btnDetailVariable_LinkClicked(object sender, EventArgs e)
        {
            EditeSelection();
            
        }

        //------------------------------------------------------------------------------
        private void EditeSelection()
        {
            if (m_wndListeVariables.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeVariables.SelectedItems[0];
                IVariableDynamique variable = item.Tag as IVariableDynamique;
                if (variable != null)
                    EditeVariable(variable);
            }
        }


        //------------------------------------------------------------------------------
        private void m_wndListeVariables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditeSelection();
        }

        //------------------------------------------------------------------------------
        private void m_btnDeleteVariable_LinkClicked(object sender, EventArgs e)
        {
            List<ListViewItem> lstItems = new List<ListViewItem>();
            foreach ( ListViewItem item in m_wndListeVariables.SelectedItems )
                lstItems.Add(item); ;
            if (lstItems.Count > 0)
            {
                StringBuilder bl = new StringBuilder();
                foreach (ListViewItem item in lstItems)
                {
                    bl.Append(item.Text);
                    bl.Append(",");
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);

                if (CFormAlerte.Afficher(
                    I.T("Delete variable(s) @1|20039", bl.ToString()),
                    EFormAlerteBoutons.OuiNon, EFormAlerteType.Question) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lstItems)
                    {
                        m_elementAVariables.RemoveVariable(item.Tag as IVariableDynamique);
                        m_wndListeVariables.Items.Remove(item);
                    }
                }
            }
        }

        
    }
}
