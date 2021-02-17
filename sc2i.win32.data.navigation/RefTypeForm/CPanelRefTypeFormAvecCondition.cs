using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using sc2i.win32.common.customizableList;

namespace sc2i.win32.data.navigation.RefTypeForm
{
    public partial class CPanelRefTypeFormAvecCondition : CCustomizableListControl
    {
        public CPanelRefTypeFormAvecCondition()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------
        public override bool AspectDifferentEnInactif
        {
            get
            {
                return true;
            }
        }

        //-----------------------------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
 	        CResultAErreur result = base.MyInitChamps(item);
            CRefTypeFormAvecConditionItem i = item as CRefTypeFormAvecConditionItem;
            if (i != null)
            {
                m_comboDefaultForm.Init(i.TypeObjetPourForm);
                if (IsCreatingImage)
                {
                    m_txtCondition.Visible = false;
                    m_lblFormule.Visible = true;
                    m_lblFormule.Dock = DockStyle.Fill;
                    m_lblFormule.Text = i.Parametre.Formule != null ? i.Parametre.Formule.GetString() : "";
                }
                else
                {
                    m_txtCondition.Visible = true;
                    m_txtCondition.Dock = DockStyle.Fill;
                    m_lblFormule.Visible = false;
                    m_txtCondition.Init(new CFournisseurGeneriqueProprietesDynamiques(), i.TypeObjetPourForm);
                    m_txtCondition.Formule = i.Parametre.Formule;
                }
                m_lblIndex.Text = (i.Parametre.Index+1).ToString();
                m_comboDefaultForm.TypeSelectionne = i.Parametre.ReferenceTypeForm;

            }
            return result;
        }

        //-----------------------------------------------------------
        public override bool HasChange
        {
            get
            {
                return true;
            }
            set
            {
                base.HasChange = value;
            }
        }

        //-----------------------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CResultAErreur result = base.MyMajChamps();
            CRefTypeFormAvecConditionItem i = CurrentItem as CRefTypeFormAvecConditionItem;
            if (i != null)
            {
                if (m_comboDefaultForm.TypeSelectionne == null)
                    result.EmpileErreur(I.T("You have to select a form for conditionnal @1|20027"));
                else
                    i.Parametre.ReferenceTypeForm = m_comboDefaultForm.TypeSelectionne;
                if (m_txtCondition.Formule == null &&
                    !m_txtCondition.ResultAnalyse)
                    result.EmpileErreur(m_txtCondition.ResultAnalyse.Erreur);
                else
                    i.Parametre.Formule = m_txtCondition.Formule;
            }
            return result;
        }

        public event EventHandler AskForDelete;
        public event EventHandler AskForUp;
        public event EventHandler AskForDown;

        //----------------------------------------------------------------
        private void m_btnDelete_Click(object sender, EventArgs e)
        {
            if (AskForDelete != null)
                AskForDelete(this, null);
        }

        //----------------------------------------------------------------
        private void m_btnUp_Click(object sender, EventArgs e)
        {
            if (AskForUp != null)
                AskForUp(this, null);

        }

        //----------------------------------------------------------------
        private void m_btnDown_Click(object sender, EventArgs e)
        {
            if (AskForDown != null)
                AskForDown(this, null);
        }

        //----------------------------------------------------------------
        private Point m_ptStartDrag;
        private void m_picDragDrop_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                m_ptStartDrag = new Point(e.X, e.Y);
            }
        }

        //----------------------------------------------------------------
        private void m_picDragDrop_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left &&
                Math.Abs(m_ptStartDrag.X - e.X) > 3 ||
                Math.Abs(m_ptStartDrag.Y - e.Y) > 3)
            {
                this.StartDragDrop(m_ptStartDrag, DragDropEffects.Move);
            }
        }
        
    }
}
