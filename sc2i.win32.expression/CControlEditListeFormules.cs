using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using System.Collections;
using sc2i.win32.common;

namespace sc2i.win32.expression
{
    public partial class CControlEditListeFormules : UserControl
    {
        CTextBoxZoomFormule m_textBoxSel = null;
        CObjetPourSousProprietes m_objetAnalyse = null;
        IFournisseurProprietesDynamiques m_fournisseurProps = null;
        public CControlEditListeFormules()
        {
            InitializeComponent();
        }

        public void Init(C2iExpression[] formules, CObjetPourSousProprietes objetAnalyse, IFournisseurProprietesDynamiques fournisseurProps)
        {
            this.SuspendDrawing();
            if ( fournisseurProps == null )
                fournisseurProps = new CFournisseurGeneriqueProprietesDynamiques();
            m_fournisseurProps = fournisseurProps;
            m_objetAnalyse = objetAnalyse;
            foreach (Control ctrl in new ArrayList(m_panelFormules.Controls))
            {
                CTextBoxZoomFormule textBox = ctrl as CTextBoxZoomFormule;
                if (textBox != null)
                {
                    textBox.Visible = false;
                    m_panelFormules.Controls.Remove(textBox);
                    textBox.Dispose();
                }
            }
            m_textBoxSel = null;
            foreach (C2iExpression formule in formules)
            {
                CTextBoxZoomFormule textBox = CreateTextBoxFormule();
                textBox.Formule = formule;
            }
            this.ResumeDrawing();
        }

        private CTextBoxZoomFormule CreateTextBoxFormule()
        {
            CTextBoxZoomFormule textBox = new CTextBoxZoomFormule();
            m_panelFormules.Controls.Add(textBox);
            textBox.Dock = DockStyle.Top;
            textBox.Height = 44;
            textBox.BringToFront();
            textBox.Init(m_fournisseurProps, m_objetAnalyse);
            textBox.Enter += new EventHandler(textBox_Enter);
            return textBox;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            if ( m_textBoxSel != null )
                m_textBoxSel.BackColor = Color.White;
            m_textBoxSel = sender as CTextBoxZoomFormule;
            if (m_textBoxSel != null)
                m_textBoxSel.BackColor = Color.LightGreen;
        }

        public C2iExpression[] GetFormules()
        {
            List<C2iExpression> lstFormules = new List<C2iExpression>();
            foreach (Control ctrl in m_panelFormules.Controls)
            {
                CTextBoxZoomFormule textBox = ctrl as CTextBoxZoomFormule;
                if (textBox != null)
                {
                    if (textBox.Formule != null)
                        lstFormules.Add(textBox.Formule);
                }
            }
            return lstFormules.ToArray();
        }

        private void m_lnkAjouter_LinkClicked(object sender, EventArgs e)
        {
            CTextBoxZoomFormule newTextBox = CreateTextBoxFormule();
            newTextBox.Focus();
        }

        private void m_lnkSupprimer_LinkClicked(object sender, EventArgs e)
        {
            if (m_textBoxSel != null)
            {
                m_panelFormules.Controls.Remove(m_textBoxSel);
                m_textBoxSel.Visible = false;
                m_textBoxSel.Dispose();
            }
            m_textBoxSel = null;
        }
    }
}
