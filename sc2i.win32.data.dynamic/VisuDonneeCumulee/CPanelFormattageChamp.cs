using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using sc2i.common;
using sc2i.formulaire;
using sc2i.data.dynamic;
using sc2i.win32.expression;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CPanelFormattageChamp : UserControl
    {
        private CFormatChampTableauCroise m_format = null;
        private CElementAVariablesDynamiques m_elementAVariables = null;
        public CPanelFormattageChamp()
        {
            InitializeComponent();
        }

        private void m_chkForeColor_CheckedChanged(object sender, EventArgs e)
        {
            m_colorFore.Visible = m_chkForeColor.Checked;
        }

        private void m_chkBackColor_CheckedChanged(object sender, EventArgs e)
        {
            m_colorBack.Visible = m_chkBackColor.Checked;
        }

        public void Init ( 
            CFormatChampTableauCroise format, 
            CElementAVariablesDynamiques eltAVariables,
            bool bForHeader)
        {
            m_elementAVariables = eltAVariables;
            m_format = format;
            if (format.FontSize == 0)
                m_txtFontSize.IntValue = null;
            else
                m_txtFontSize.IntValue = format.FontSize;
            m_txtColumnWidth.IntValue = format.Width;
            m_chkBackColor.Checked = format.BackColor.A != 0;
            m_chkForeColor.Checked = format.ForeColor.A != 0;
            m_chkSelectedColor.Checked = format.SelectionBackcolor.A != 0;
            m_colorBack.SelectedColor = format.BackColor;
            m_colorFore.SelectedColor = format.ForeColor;
            m_colorSelected.SelectedColor = format.SelectionBackcolor;
            m_colorFore.Visible = m_chkForeColor.Checked;
            m_colorBack.Visible = m_chkBackColor.Checked;
            m_colorSelected.Visible = m_chkSelectedColor.Checked;
            m_cmbFont.Text = format.FontName;
            if (format.Bold != null)
                m_chkBold.Checked = format.Bold.Value;
            else
                m_chkBold.CheckState = CheckState.Indeterminate;

            m_cmbTextAlign.Items.Clear();
            m_cmbTextAlign.Items.Add("");
            foreach (C2iWndTextBox.TypeAlignement align in Enum.GetValues(typeof(C2iWndTextBox.TypeAlignement))) 
                m_cmbTextAlign.Items.Add ( align );
            if (format.Alignement != null)
                m_cmbTextAlign.SelectedItem = format.Alignement;
            else
                m_cmbTextAlign.SelectedItem = "";
            m_btnFormuleBack.Visible =
                m_btnFormuleFore.Visible =
                m_btnFormuleBold.Visible =
                m_btnFormuleSize.Visible = m_elementAVariables != null;
            m_panelWidth.Visible = bForHeader;
            UpdateBoutonsFormules();
        }

        private void UpdateBoutonsFormules()
        {
            m_btnFormuleSize.BackColor = m_format.FormuleFontSize == null ? BackColor : Color.LightGreen;
            m_btnFormuleBold.BackColor = m_format.FormuleBold == null ? BackColor : Color.LightGreen;
            m_btnFormuleFore.BackColor = m_format.FormuleForeColor == null ? BackColor : Color.LightGreen;
            m_btnFormuleBack.BackColor = m_format.FormuleBackColor == null ? BackColor : Color.LightGreen;
        }

        public void MajChamps()
        {
            m_format.FontName = m_cmbFont.Text;
            if (m_txtFontSize.IntValue != null)
                m_format.FontSize = m_txtFontSize.IntValue.Value;
            else
                m_format.FontSize = 0;
            if (m_chkBackColor.Checked)
                m_format.BackColor = m_colorBack.SelectedColor;
            else
                m_format.BackColor = Color.FromArgb(0, 255, 255, 255);
            if (m_chkSelectedColor.Checked)
                m_format.SelectionBackcolor = m_colorSelected.SelectedColor;
            else
                m_format.SelectionBackcolor = Color.FromArgb(0, 255, 255, 255);
            if (m_chkForeColor.Checked)
                m_format.ForeColor = m_colorFore.SelectedColor;
            else
                m_format.ForeColor = Color.FromArgb(0, 0, 0, 0);
            if (m_cmbTextAlign.SelectedItem != null && m_cmbTextAlign.SelectedItem.ToString() != "")
                m_format.Alignement = (C2iWndTextBox.TypeAlignement)m_cmbTextAlign.SelectedItem;
            else
                m_format.Alignement = null;
            switch (m_chkBold.CheckState)
            {
                case CheckState.Checked:
                    m_format.Bold = true;
                    break;
                case CheckState.Indeterminate:
                    m_format.Bold = null;
                    break;
                case CheckState.Unchecked:
                    m_format.Bold = false;
                    break;
                default:
                    break;
            }
            m_format.Width = m_txtColumnWidth.IntValue;


        }

        private void CPanelFormattageChamp_Load(object sender, EventArgs e)
        {
            m_cmbFont.Items.Add("");
            foreach (FontFamily f in FontFamily.Families)
                m_cmbFont.Items.Add ( f.Name );
        }

        private void m_btnFormuleSize_Click(object sender, EventArgs e)
        {
            C2iExpression formule = m_format.FormuleFontSize;
            if (CFormStdEditeFormule.EditeFormule(
                ref formule,
                m_elementAVariables,
                new CObjetPourSousProprietes(m_elementAVariables),
                true))
                m_format.FormuleFontSize = formule;
            UpdateBoutonsFormules();
        }

        private void m_btnFormuleFore_Click(object sender, EventArgs e)
        {
            C2iExpression formule = m_format.FormuleForeColor;
            if (CFormStdEditeFormule.EditeFormule(
                ref formule,
                m_elementAVariables,
                new CObjetPourSousProprietes(m_elementAVariables),
                true))
                m_format.FormuleForeColor = formule;
            UpdateBoutonsFormules();
        }

        private void m_btnFormuleBack_Click(object sender, EventArgs e)
        {
            C2iExpression formule = m_format.FormuleBackColor;
            if (CFormStdEditeFormule.EditeFormule(
                ref formule,
                m_elementAVariables,
                new CObjetPourSousProprietes(m_elementAVariables),
                true))
                m_format.FormuleBackColor = formule;
            UpdateBoutonsFormules();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            C2iExpression formule = m_format.FormuleBold;
            if (CFormStdEditeFormule.EditeFormule(
                ref formule,
                m_elementAVariables,
                new CObjetPourSousProprietes(m_elementAVariables),
                true))
                m_format.FormuleBold = formule;
            UpdateBoutonsFormules();
        }

        private void m_colorSelected_Click(object sender, EventArgs e)
        {

        }

        private void m_chkSelectedColor_CheckedChanged(object sender, EventArgs e)
        {
            m_colorSelected.Visible = m_chkSelectedColor.Checked;
        }




       
                
    }
}
