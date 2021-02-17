using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation.RefTypeForm
{
    public partial class CPanelEditRefTypeFormAvecCondition : UserControl, IControlALockEdition
    {
        private CReferenceTypeFormAvecCondition m_refTypeFormAvecCondition = new CReferenceTypeFormAvecCondition();
        private Type m_typeObjetPourForm = null;
        public CPanelEditRefTypeFormAvecCondition()
        {
            InitializeComponent();
            m_wndListeForms.ItemControl = new CPanelRefTypeFormAvecCondition();

        }

        //-----------------------------------------------------------------------
        public void Init(Type typeObjetPourForm, CReferenceTypeForm refTypeForm)
        {
            m_typeObjetPourForm = typeObjetPourForm;
            m_refTypeFormAvecCondition = new CReferenceTypeFormAvecCondition();
            if (refTypeForm is CReferenceTypeFormBuiltIn)
            {
                m_refTypeFormAvecCondition.DefaultTypeForm = refTypeForm;
            }
            if (refTypeForm is CReferenceTypeFormDynamic)
            {
                m_refTypeFormAvecCondition.DefaultTypeForm = refTypeForm;
            }
            if (refTypeForm is CReferenceTypeFormAvecCondition)
            {
                m_refTypeFormAvecCondition = CCloner2iSerializable.Clone ( refTypeForm ) as CReferenceTypeFormAvecCondition;
            }
            m_comboDefaultForm.Init(typeObjetPourForm);
            m_comboDefaultForm.TypeSelectionne = m_refTypeFormAvecCondition.DefaultTypeForm;

            List<CRefTypeFormAvecConditionItem> lst = new List<CRefTypeFormAvecConditionItem>();
            foreach (CReferenceTypeFormAvecCondition.CParametreTypeForm p in m_refTypeFormAvecCondition.Parametres)
            {
                lst.Add ( new CRefTypeFormAvecConditionItem(m_typeObjetPourForm, p ));
            }
            m_wndListeForms.Items = lst.ToArray();
        }
        

        //---------------------------------------------------------------
        private void m_lnkAdd_LinkClicked(object sender, EventArgs e)
        {
            CReferenceTypeFormAvecCondition.CParametreTypeForm p = new CReferenceTypeFormAvecCondition.CParametreTypeForm();
            CRefTypeFormAvecConditionItem i = new CRefTypeFormAvecConditionItem(m_typeObjetPourForm, p);
            m_wndListeForms.AddItem(i, true);
        }

        //---------------------------------------------------------------
        void panel_AskForDelete(object sender, EventArgs e)
        {
            CPanelRefTypeFormAvecCondition panel = sender as CPanelRefTypeFormAvecCondition;
            if (panel != null)
            {
                Color old = panel.BackColor;
                if (panel != null)
                    panel.BackColor = Color.Red;
                if (MessageBox.Show(I.T("Remove this condition ?|20028"),
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    m_wndListeForms.Controls.Remove(panel);
                }
                else
                    panel.BackColor = old;
            }
        }

        //---------------------------------------------------------------
        private void m_lnkRemove_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeForms.CurrentItemIndex != null)
            {
                if (MessageBox.Show(I.T("Delete item @1 ?|20028",
                    (m_wndListeForms.CurrentItemIndex.Value + 1).ToString()),
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question )== DialogResult.Yes)
                {
                    m_wndListeForms.RemoveItem(m_wndListeForms.CurrentItemIndex.Value, true);
                }
            }
        }

        //-----------------------------------------------------------------------
        public CReferenceTypeForm GetReferenceTypeForm()
        {
            if (m_wndListeForms.Items.Count() == 0)
            {
                if (m_comboDefaultForm.TypeSelectionne != null)
                    return m_comboDefaultForm.TypeSelectionne;
            }
            else
            {
                m_refTypeFormAvecCondition.DefaultTypeForm = m_comboDefaultForm.TypeSelectionne;
                CResultAErreur result = m_wndListeForms.MajChamps();
                if (result)
                {
                    List<CReferenceTypeFormAvecCondition.CParametreTypeForm> lst = new List<CReferenceTypeFormAvecCondition.CParametreTypeForm>();
                    foreach (CRefTypeFormAvecConditionItem i in m_wndListeForms.Items)
                    {
                        lst.Add(i.Parametre);
                    }
                    m_refTypeFormAvecCondition.Parametres = lst.ToArray();
                }
                CReferenceTypeFormAvecCondition rt = CCloner2iSerializable.Clone(m_refTypeFormAvecCondition) as CReferenceTypeFormAvecCondition;
                return rt;

            }
            return null;
        }

        #region IControlALockEdition Membres

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

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
