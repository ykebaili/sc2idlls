using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire.win32
{
    public partial class CFormEditConfigMultiSelect : Form
    {
        private C2iWndMultiSelect.CConfigMultiSelect m_config = null;
        private Type m_typeElements = typeof(string);
        public CFormEditConfigMultiSelect()
        {
            InitializeComponent();
        }

        public static C2iWndMultiSelect.CConfigMultiSelect EditeConfig(
            Type typeElements,
            C2iWndMultiSelect.CConfigMultiSelect config)
        {
            CFormEditConfigMultiSelect form = new CFormEditConfigMultiSelect();
            if (config == null)
                config = new C2iWndMultiSelect.CConfigMultiSelect();
            form.m_config = config;
            form.m_typeElements = typeElements;
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                config = form.m_config;
            }
            form.Dispose();
            return config;
        }

        private void CFormEditConfigMultiSelect_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            Init();
        }

        private void Init()
        {
            m_panelColonnes.ClearAndDisposeControls();

            m_txtZoomFormule.Init(new CFournisseurGeneriqueProprietesDynamiques(),
                m_typeElements);
            m_txtZoomFormule.Formule = m_config.FormuleSelectedValue;

            m_panelColonnes.SuspendDrawing();
            foreach (C2iWndMultiSelect.CColonneMultiSelect col in m_config.Colonnes)
            {
                CPanelEditColonneMultiSelect panel = GetNewPanel(col);
                m_panelColonnes.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
            }
            m_panelColonnes.ResumeDrawing();
        }

        private CPanelEditColonneMultiSelect GetNewPanel(C2iWndMultiSelect.CColonneMultiSelect col)
        {
            CPanelEditColonneMultiSelect panel = new CPanelEditColonneMultiSelect();
            panel.Init(m_panelColonnes.Controls.Count,col, m_typeElements);
            panel.OnDeleteClick += new EventHandler(panel_OnDeleteClick);
            return panel;
        }

        private void RenumerottePanels()
        {
            int nIndex = 0;
            foreach ( CPanelEditColonneMultiSelect panel in PanelsColonne )
                panel.Index = nIndex++;
        }

        private IEnumerable<CPanelEditColonneMultiSelect> PanelsColonne
        {
            get
            {
                List<CPanelEditColonneMultiSelect> panels = new List<CPanelEditColonneMultiSelect>();
                foreach (CPanelEditColonneMultiSelect panel in m_panelColonnes.Controls)
                    panels.Add(panel);
                panels.Sort((x, y) => x.Index.CompareTo(y.Index));

                return panels.AsReadOnly();
            }
        }



        void panel_OnDeleteClick(object sender, EventArgs e)
        {
            CPanelEditColonneMultiSelect panel = sender as CPanelEditColonneMultiSelect;
            if (panel != null)
            {
                m_panelColonnes.Controls.Remove(panel);
                panel.Dispose();
            }
            RenumerottePanels();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            C2iWndMultiSelect.CConfigMultiSelect config = new C2iWndMultiSelect.CConfigMultiSelect();
            config.FormuleSelectedValue = m_txtZoomFormule.Formule;
            foreach (CPanelEditColonneMultiSelect panel in PanelsColonne)
            {
                panel.MajChamps();
                config.AddColonne(panel.Colonne);
            }
            m_config = config;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void m_btnAdd_LinkClicked(object sender, EventArgs e)
        {
            C2iWndMultiSelect.CColonneMultiSelect col = new C2iWndMultiSelect.CColonneMultiSelect();
            CPanelEditColonneMultiSelect panel = GetNewPanel(col);
            m_panelColonnes.Controls.Add(panel);
            panel.Dock = DockStyle.Top;
            panel.BringToFront();
            panel.Focus();
            RenumerottePanels();
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurConfigMultiSelect : IEditeurConfigMultiSelect
    {
        public static void Autoexec()
        {
            CConfigMultiSelectEditor.SetTypeEditeur(typeof(CEditeurConfigMultiSelect));
        }

        public C2iWndMultiSelect.CConfigMultiSelect EditeConfig(
            Type typeElements,
            C2iWndMultiSelect.CConfigMultiSelect config)
        {
            return CFormEditConfigMultiSelect.EditeConfig(typeElements, config);
        }

    }
}
