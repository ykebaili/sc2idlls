using futurocom.win32.chart.series;
using futurocom.win32.chart.Areas;
using futurocom.win32.chart.legends;
using sc2i.win32.expression.variablesDynamiques;
namespace futurocom.win32.chart
{
    partial class CFormEditChartSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.c2iPanel1 = new sc2i.win32.common.C2iPanel(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_btnApply3D = new System.Windows.Forms.Button();
            this.m_btnRefreshPreview = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage4 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage5 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelVariables = new sc2i.win32.expression.variablesDynamiques.CControleVariablesDynamiques();
            this.tabPage7 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelFormulaireFiltreSimple = new sc2i.formulaire.win32.editor.CPanelEditionFullFormulaire();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_chkAllow3DSetup = new System.Windows.Forms.CheckBox();
            this.m_chkAllowZoom = new System.Windows.Forms.CheckBox();
            this.m_cmbSelectSeriesAlignment = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage6 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelFormulaireFiltreAvance = new sc2i.formulaire.win32.editor.CPanelEditionFullFormulaire();
            this.m_wndListeVariables = new sc2i.win32.common.ListViewAutoFilled();
            this.colNomChamp = ((sc2i.win32.common.ListViewAutoFilledColumn)(new sc2i.win32.common.ListViewAutoFilledColumn()));
            this.m_colType = ((sc2i.win32.common.ListViewAutoFilledColumn)(new sc2i.win32.common.ListViewAutoFilledColumn()));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_preview = new futurocom.win32.chart.CControlChart();
            this.m_panelSeries = new futurocom.win32.chart.series.CPanelEditeSeries();
            this.m_panelData = new futurocom.win32.chart.CPanelEditSourcesChart();
            this.m_panelAreas = new futurocom.win32.chart.Areas.CPanelEditeAreas();
            this.m_panelLegends = new futurocom.win32.chart.legends.CPanelEditeLegends();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.c2iPanelOmbre1.SuspendLayout();
            this.c2iPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.c2iTabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 402);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 42);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 1;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Image = global::futurocom.win32.chart.Resource1.cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(514, 5);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.Image = global::futurocom.win32.chart.Resource1.check;
            this.m_btnOk.Location = new System.Drawing.Point(410, 5);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.c2iPanelOmbre1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(569, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 402);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 1;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.c2iPanel1);
            this.c2iPanelOmbre1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(436, 402);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 0;
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanel1.Controls.Add(this.m_preview);
            this.c2iPanel1.Controls.Add(this.panel3);
            this.c2iPanel1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel1.LockEdition = false;
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(420, 399);
            this.m_extStyle.SetStyleBackColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.m_btnApply3D);
            this.panel3.Controls.Add(this.m_btnRefreshPreview);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(420, 22);
            this.m_extStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 0;
            // 
            // m_btnApply3D
            // 
            this.m_btnApply3D.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnApply3D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnApply3D.ForeColor = System.Drawing.Color.White;
            this.m_btnApply3D.Image = global::futurocom.win32.chart.Resource1._3D_tool;
            this.m_btnApply3D.Location = new System.Drawing.Point(362, 0);
            this.m_btnApply3D.Name = "m_btnApply3D";
            this.m_btnApply3D.Size = new System.Drawing.Size(29, 22);
            this.m_extStyle.SetStyleBackColor(this.m_btnApply3D, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnApply3D, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnApply3D.TabIndex = 2;
            this.m_tooltip.SetToolTip(this.m_btnApply3D, "Apply 3D to setup|20024");
            this.m_btnApply3D.UseVisualStyleBackColor = true;
            this.m_btnApply3D.Click += new System.EventHandler(this.m_btnApply3D_Click);
            // 
            // m_btnRefreshPreview
            // 
            this.m_btnRefreshPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnRefreshPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRefreshPreview.ForeColor = System.Drawing.Color.White;
            this.m_btnRefreshPreview.Image = global::futurocom.win32.chart.Resource1.Refresh;
            this.m_btnRefreshPreview.Location = new System.Drawing.Point(391, 0);
            this.m_btnRefreshPreview.Name = "m_btnRefreshPreview";
            this.m_btnRefreshPreview.Size = new System.Drawing.Size(29, 22);
            this.m_extStyle.SetStyleBackColor(this.m_btnRefreshPreview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnRefreshPreview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnRefreshPreview.TabIndex = 1;
            this.m_btnRefreshPreview.UseVisualStyleBackColor = true;
            this.m_btnRefreshPreview.Click += new System.EventHandler(this.m_btnRefreshPreview_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 22);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Preview|20012";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(566, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 402);
            this.m_extStyle.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.ControlBottomOffset = 16;
            this.c2iTabControl1.ControlRightOffset = 16;
            this.c2iTabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.c2iTabControl1.ForeColor = System.Drawing.Color.Black;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 0);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = true;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.SelectedIndex = 0;
            this.c2iTabControl1.SelectedTab = this.tabPage1;
            this.c2iTabControl1.Size = new System.Drawing.Size(566, 402);
            this.m_extStyle.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iTabControl1.TabIndex = 0;
            this.c2iTabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage3,
            this.tabPage4,
            this.tabPage2,
            this.tabPage5,
            this.tabPage7,
            this.tabPage6});
            this.c2iTabControl1.TextColor = System.Drawing.Color.Black;
            this.c2iTabControl1.SelectionChanged += new System.EventHandler(this.c2iTabControl1_SelectionChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_panelAreas);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "Areas|20014";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelData);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Chart data|20004";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.m_panelLegends);
            this.tabPage4.Location = new System.Drawing.Point(0, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Selected = false;
            this.tabPage4.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage4.TabIndex = 13;
            this.tabPage4.Title = "Legends|20026";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_panelSeries);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Series|20005";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.m_panelVariables);
            this.tabPage5.Location = new System.Drawing.Point(0, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Selected = false;
            this.tabPage5.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage5.TabIndex = 14;
            this.tabPage5.Title = "Chart variables|20027";
            // 
            // m_panelVariables
            // 
            this.m_panelVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelVariables.Location = new System.Drawing.Point(0, 0);
            this.m_panelVariables.LockEdition = false;
            this.m_panelVariables.Name = "m_panelVariables";
            this.m_panelVariables.ShortMode = false;
            this.m_panelVariables.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.m_panelVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelVariables.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.m_panelFormulaireFiltreSimple);
            this.tabPage7.Controls.Add(this.panel5);
            this.tabPage7.Controls.Add(this.panel4);
            this.tabPage7.Location = new System.Drawing.Point(0, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Selected = false;
            this.tabPage7.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage7.TabIndex = 16;
            this.tabPage7.Title = "Viewer setup|20040";
            // 
            // m_panelFormulaireFiltreSimple
            // 
            this.m_panelFormulaireFiltreSimple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormulaireFiltreSimple.EntiteEditee = null;
            this.m_panelFormulaireFiltreSimple.FournisseurProprietes = null;
            this.m_panelFormulaireFiltreSimple.Location = new System.Drawing.Point(0, 89);
            this.m_panelFormulaireFiltreSimple.LockEdition = false;
            this.m_panelFormulaireFiltreSimple.Name = "m_panelFormulaireFiltreSimple";
            this.m_panelFormulaireFiltreSimple.Size = new System.Drawing.Size(550, 272);
            this.m_extStyle.SetStyleBackColor(this.m_panelFormulaireFiltreSimple, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelFormulaireFiltreSimple, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormulaireFiltreSimple.TabIndex = 2;
            this.m_panelFormulaireFiltreSimple.TypeEdite = null;
            this.m_panelFormulaireFiltreSimple.WndEditee = null;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 68);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(550, 21);
            this.m_extStyle.SetStyleBackColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel5.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 21);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 0;
            this.label3.Text = "Simple filter form|20050";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_chkAllow3DSetup);
            this.panel4.Controls.Add(this.m_chkAllowZoom);
            this.panel4.Controls.Add(this.m_cmbSelectSeriesAlignment);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(550, 68);
            this.m_extStyle.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 3;
            // 
            // m_chkAllow3DSetup
            // 
            this.m_chkAllow3DSetup.AutoSize = true;
            this.m_chkAllow3DSetup.Location = new System.Drawing.Point(204, 42);
            this.m_chkAllow3DSetup.Name = "m_chkAllow3DSetup";
            this.m_chkAllow3DSetup.Size = new System.Drawing.Size(138, 19);
            this.m_extStyle.SetStyleBackColor(this.m_chkAllow3DSetup, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkAllow3DSetup, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkAllow3DSetup.TabIndex = 2;
            this.m_chkAllow3DSetup.Text = "Allow 3D setup|20049";
            this.m_chkAllow3DSetup.UseVisualStyleBackColor = true;
            // 
            // m_chkAllowZoom
            // 
            this.m_chkAllowZoom.AutoSize = true;
            this.m_chkAllowZoom.Location = new System.Drawing.Point(204, 25);
            this.m_chkAllowZoom.Name = "m_chkAllowZoom";
            this.m_chkAllowZoom.Size = new System.Drawing.Size(122, 19);
            this.m_extStyle.SetStyleBackColor(this.m_chkAllowZoom, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkAllowZoom, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkAllowZoom.TabIndex = 2;
            this.m_chkAllowZoom.Text = "Allow zoom|20048";
            this.m_chkAllowZoom.UseVisualStyleBackColor = true;
            // 
            // m_cmbSelectSeriesAlignment
            // 
            this.m_cmbSelectSeriesAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSelectSeriesAlignment.FormattingEnabled = true;
            this.m_cmbSelectSeriesAlignment.Location = new System.Drawing.Point(204, 0);
            this.m_cmbSelectSeriesAlignment.Name = "m_cmbSelectSeriesAlignment";
            this.m_cmbSelectSeriesAlignment.Size = new System.Drawing.Size(153, 23);
            this.m_extStyle.SetStyleBackColor(this.m_cmbSelectSeriesAlignment, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbSelectSeriesAlignment, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbSelectSeriesAlignment.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 26);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select series alignment :|20042";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.m_panelFormulaireFiltreAvance);
            this.tabPage6.Location = new System.Drawing.Point(0, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Selected = false;
            this.tabPage6.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.tabPage6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.tabPage6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage6.TabIndex = 15;
            this.tabPage6.Title = "Advanced Filter|20028";
            // 
            // m_panelFormulaireFiltreAvance
            // 
            this.m_panelFormulaireFiltreAvance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormulaireFiltreAvance.EntiteEditee = null;
            this.m_panelFormulaireFiltreAvance.FournisseurProprietes = null;
            this.m_panelFormulaireFiltreAvance.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormulaireFiltreAvance.LockEdition = false;
            this.m_panelFormulaireFiltreAvance.Name = "m_panelFormulaireFiltreAvance";
            this.m_panelFormulaireFiltreAvance.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.m_panelFormulaireFiltreAvance, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelFormulaireFiltreAvance, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormulaireFiltreAvance.TabIndex = 1;
            this.m_panelFormulaireFiltreAvance.TypeEdite = null;
            this.m_panelFormulaireFiltreAvance.WndEditee = null;
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNomChamp,
            this.m_colType});
            this.m_wndListeVariables.EnableCustomisation = true;
            this.m_wndListeVariables.FullRowSelect = true;
            this.m_wndListeVariables.Location = new System.Drawing.Point(3, 33);
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(558, 325);
            this.m_extStyle.SetStyleBackColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeVariables.TabIndex = 12;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            // 
            // colNomChamp
            // 
            this.colNomChamp.Field = "Nom";
            this.colNomChamp.PrecisionWidth = 0D;
            this.colNomChamp.ProportionnalSize = false;
            this.colNomChamp.Text = "Name|253";
            this.colNomChamp.Visible = true;
            this.colNomChamp.Width = 250;
            // 
            // m_colType
            // 
            this.m_colType.Field = "LibelleType";
            this.m_colType.PrecisionWidth = 0D;
            this.m_colType.ProportionnalSize = false;
            this.m_colType.Text = "Type|254";
            this.m_colType.Visible = true;
            this.m_colType.Width = 120;
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuVariableCalculée,
            this.m_menuVariableSelection});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Entered|20029";
            // 
            // m_menuVariableCalculée
            // 
            this.m_menuVariableCalculée.Index = 1;
            this.m_menuVariableCalculée.Text = "Computed|20030";
            // 
            // m_menuVariableSelection
            // 
            this.m_menuVariableSelection.Index = 2;
            this.m_menuVariableSelection.Text = "Element selection|20031";
            // 
            // m_preview
            // 
            this.m_preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_preview.Location = new System.Drawing.Point(0, 22);
            this.m_preview.LockEdition = false;
            this.m_preview.ModeSouris = futurocom.win32.chart.CControlChart.EModeMouseChart.SimpleMouse;
            this.m_preview.Name = "m_preview";
            this.m_preview.Size = new System.Drawing.Size(420, 377);
            this.m_extStyle.SetStyleBackColor(this.m_preview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_preview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_preview.TabIndex = 1;
            // 
            // m_panelSeries
            // 
            this.m_panelSeries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelSeries.ForeColor = System.Drawing.Color.Black;
            this.m_panelSeries.Location = new System.Drawing.Point(0, 0);
            this.m_panelSeries.Name = "m_panelSeries";
            this.m_panelSeries.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.m_panelSeries, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelSeries, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelSeries.TabIndex = 0;
            // 
            // m_panelData
            // 
            this.m_panelData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelData.ForeColor = System.Drawing.Color.Black;
            this.m_panelData.Location = new System.Drawing.Point(0, 0);
            this.m_panelData.Name = "m_panelData";
            this.m_panelData.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.m_panelData, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelData, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelData.TabIndex = 0;
            this.m_panelData.Load += new System.EventHandler(this.m_panelData_Load);
            // 
            // m_panelAreas
            // 
            this.m_panelAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelAreas.ForeColor = System.Drawing.Color.Black;
            this.m_panelAreas.Location = new System.Drawing.Point(0, 0);
            this.m_panelAreas.Name = "m_panelAreas";
            this.m_panelAreas.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.m_panelAreas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelAreas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelAreas.TabIndex = 1;
            this.m_panelAreas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_panelAreas_MouseUp);
            // 
            // m_panelLegends
            // 
            this.m_panelLegends.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelLegends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelLegends.ForeColor = System.Drawing.Color.Black;
            this.m_panelLegends.Location = new System.Drawing.Point(0, 0);
            this.m_panelLegends.Name = "m_panelLegends";
            this.m_panelLegends.Size = new System.Drawing.Size(550, 361);
            this.m_extStyle.SetStyleBackColor(this.m_panelLegends, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelLegends, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelLegends.TabIndex = 2;
            // 
            // CFormEditChartSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 444);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.c2iTabControl1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditChartSetup";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Chart setup|20003";
            this.Load += new System.EventHandler(this.CFormEditChartSetup_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.c2iPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.c2iTabControl1.ResumeLayout(false);
            this.c2iTabControl1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.common.C2iTabControl c2iTabControl1;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private CPanelEditSourcesChart m_panelData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private CPanelEditeSeries m_panelSeries;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private sc2i.win32.common.C2iPanel c2iPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private CControlChart m_preview;
        private System.Windows.Forms.Button m_btnRefreshPreview;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private CPanelEditeAreas m_panelAreas;
        private System.Windows.Forms.Button m_btnApply3D;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;
        private Crownwood.Magic.Controls.TabPage tabPage4;
        private CPanelEditeLegends m_panelLegends;
        private sc2i.win32.common.ListViewAutoFilled m_wndListeVariables;
        private sc2i.win32.common.ListViewAutoFilledColumn colNomChamp;
        private sc2i.win32.common.ListViewAutoFilledColumn m_colType;
        private System.Windows.Forms.ContextMenu m_menuNewVariable;
        private System.Windows.Forms.MenuItem m_menuVariableSaisie;
        private System.Windows.Forms.MenuItem m_menuVariableCalculée;
        private System.Windows.Forms.MenuItem m_menuVariableSelection;
        private Crownwood.Magic.Controls.TabPage tabPage5;
        private CControleVariablesDynamiques m_panelVariables;
        private Crownwood.Magic.Controls.TabPage tabPage6;
        private sc2i.formulaire.win32.editor.CPanelEditionFullFormulaire m_panelFormulaireFiltreAvance;
        private Crownwood.Magic.Controls.TabPage tabPage7;
        private sc2i.formulaire.win32.editor.CPanelEditionFullFormulaire m_panelFormulaireFiltreSimple;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox m_chkAllowZoom;
        private System.Windows.Forms.ComboBox m_cmbSelectSeriesAlignment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox m_chkAllow3DSetup;
    }
}