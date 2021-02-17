namespace futurocom.win32.chart
{
    partial class CControlChart
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControlChart));
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnFiltrer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelModeSouris = new System.Windows.Forms.Panel();
            this.m_rbtn3D = new System.Windows.Forms.RadioButton();
            this.m_rbtnLoupe = new System.Windows.Forms.RadioButton();
            this.m_rbtnMouse = new System.Windows.Forms.RadioButton();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelFiltreSimple = new System.Windows.Forms.Panel();
            this.m_panelFormulairefiltreSimple = new sc2i.formulaire.win32.editor.CPanelFormulaireSurElement();
            this.m_panelOutilsFiltre = new System.Windows.Forms.Panel();
            this.m_btnApplyFilter = new System.Windows.Forms.Button();
            this.m_panelFiltreSeries = new System.Windows.Forms.Panel();
            this.m_wndListeSeries = new System.Windows.Forms.ListView();
            this.m_lblSelectSeriesTitle = new System.Windows.Forms.Label();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_chartTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.m_chartControl = new futurocom.win32.chart.CChartAExceptionMaitrisee();
            this.m_imagesData = new System.Windows.Forms.ImageList(this.components);
            this.m_panelTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelModeSouris.SuspendLayout();
            this.m_panelFiltreSimple.SuspendLayout();
            this.m_panelOutilsFiltre.SuspendLayout();
            this.m_panelFiltreSeries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chartControl)).BeginInit();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.panel1);
            this.m_panelTop.Controls.Add(this.m_panelModeSouris);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelTop, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(439, 28);
            this.m_panelTop.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnFiltrer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(335, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 28);
            this.panel1.TabIndex = 3;
            // 
            // m_btnFiltrer
            // 
            this.m_btnFiltrer.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnFiltrer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnFiltrer.Image = global::futurocom.win32.chart.Resource1.Filtrer;
            this.m_btnFiltrer.Location = new System.Drawing.Point(66, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnFiltrer, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFiltrer.Name = "m_btnFiltrer";
            this.m_btnFiltrer.Size = new System.Drawing.Size(28, 28);
            this.m_btnFiltrer.TabIndex = 1;
            this.m_tooltip.SetToolTip(this.m_btnFiltrer, "Filter graph|20036");
            this.m_btnFiltrer.UseVisualStyleBackColor = true;
            this.m_btnFiltrer.Click += new System.EventHandler(this.m_btnFiltrer_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(94, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 28);
            this.label1.TabIndex = 2;
            // 
            // m_panelModeSouris
            // 
            this.m_panelModeSouris.Controls.Add(this.m_rbtn3D);
            this.m_panelModeSouris.Controls.Add(this.m_rbtnLoupe);
            this.m_panelModeSouris.Controls.Add(this.m_rbtnMouse);
            this.m_panelModeSouris.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelModeSouris.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelModeSouris, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelModeSouris.Name = "m_panelModeSouris";
            this.m_panelModeSouris.Size = new System.Drawing.Size(136, 28);
            this.m_panelModeSouris.TabIndex = 2;
            // 
            // m_rbtn3D
            // 
            this.m_rbtn3D.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtn3D.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtn3D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_rbtn3D.Image = global::futurocom.win32.chart.Resource1._3D_tool;
            this.m_rbtn3D.Location = new System.Drawing.Point(56, 0);
            this.m_extModeEdition.SetModeEdition(this.m_rbtn3D, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_rbtn3D.Name = "m_rbtn3D";
            this.m_rbtn3D.Size = new System.Drawing.Size(28, 28);
            this.m_rbtn3D.TabIndex = 0;
            this.m_rbtn3D.TabStop = true;
            this.m_tooltip.SetToolTip(this.m_rbtn3D, "Setup 3D (right click for 2D)|20037");
            this.m_rbtn3D.UseVisualStyleBackColor = true;
            this.m_rbtn3D.CheckedChanged += new System.EventHandler(this.m_rbtn3D_CheckedChanged);
            this.m_rbtn3D.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_rbtn3D_MouseClick);
            this.m_rbtn3D.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_rbtn3D_MouseUp);
            // 
            // m_rbtnLoupe
            // 
            this.m_rbtnLoupe.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnLoupe.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnLoupe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_rbtnLoupe.Image = global::futurocom.win32.chart.Resource1.search1;
            this.m_rbtnLoupe.Location = new System.Drawing.Point(28, 0);
            this.m_extModeEdition.SetModeEdition(this.m_rbtnLoupe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_rbtnLoupe.Name = "m_rbtnLoupe";
            this.m_rbtnLoupe.Size = new System.Drawing.Size(28, 28);
            this.m_rbtnLoupe.TabIndex = 2;
            this.m_rbtnLoupe.TabStop = true;
            this.m_tooltip.SetToolTip(this.m_rbtnLoupe, "Zoom|20038");
            this.m_rbtnLoupe.UseVisualStyleBackColor = true;
            this.m_rbtnLoupe.CheckedChanged += new System.EventHandler(this.m_rbtnLoupe_CheckedChanged);
            // 
            // m_rbtnMouse
            // 
            this.m_rbtnMouse.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnMouse.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_rbtnMouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_rbtnMouse.Image = global::futurocom.win32.chart.Resource1.Mouse_cursor;
            this.m_rbtnMouse.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_rbtnMouse, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_rbtnMouse.Name = "m_rbtnMouse";
            this.m_rbtnMouse.Size = new System.Drawing.Size(28, 28);
            this.m_rbtnMouse.TabIndex = 1;
            this.m_rbtnMouse.TabStop = true;
            this.m_tooltip.SetToolTip(this.m_rbtnMouse, "Selection mode|20039");
            this.m_rbtnMouse.UseVisualStyleBackColor = true;
            this.m_rbtnMouse.CheckedChanged += new System.EventHandler(this.m_rbtnMouse_CheckedChanged);
            // 
            // m_panelFiltreSimple
            // 
            this.m_panelFiltreSimple.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelFiltreSimple.Controls.Add(this.m_panelFormulairefiltreSimple);
            this.m_panelFiltreSimple.Controls.Add(this.m_panelOutilsFiltre);
            this.m_panelFiltreSimple.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFiltreSimple.Location = new System.Drawing.Point(0, 28);
            this.m_extModeEdition.SetModeEdition(this.m_panelFiltreSimple, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltreSimple.Name = "m_panelFiltreSimple";
            this.m_panelFiltreSimple.Size = new System.Drawing.Size(439, 45);
            this.m_panelFiltreSimple.TabIndex = 2;
            // 
            // m_panelFormulairefiltreSimple
            // 
            this.m_panelFormulairefiltreSimple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormulairefiltreSimple.ElementEdite = null;
            this.m_panelFormulairefiltreSimple.Location = new System.Drawing.Point(28, 0);
            this.m_panelFormulairefiltreSimple.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelFormulairefiltreSimple, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFormulairefiltreSimple.Name = "m_panelFormulairefiltreSimple";
            this.m_panelFormulairefiltreSimple.Size = new System.Drawing.Size(407, 41);
            this.m_panelFormulairefiltreSimple.TabIndex = 1;
            // 
            // m_panelOutilsFiltre
            // 
            this.m_panelOutilsFiltre.Controls.Add(this.m_btnApplyFilter);
            this.m_panelOutilsFiltre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelOutilsFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelOutilsFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelOutilsFiltre.Name = "m_panelOutilsFiltre";
            this.m_panelOutilsFiltre.Size = new System.Drawing.Size(28, 41);
            this.m_panelOutilsFiltre.TabIndex = 0;
            // 
            // m_btnApplyFilter
            // 
            this.m_btnApplyFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnApplyFilter.Image = global::futurocom.win32.chart.Resource1.Appliquer_2;
            this.m_btnApplyFilter.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnApplyFilter, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnApplyFilter.Name = "m_btnApplyFilter";
            this.m_btnApplyFilter.Size = new System.Drawing.Size(28, 28);
            this.m_btnApplyFilter.TabIndex = 0;
            this.m_tooltip.SetToolTip(this.m_btnApplyFilter, "Apply filter");
            this.m_btnApplyFilter.UseVisualStyleBackColor = true;
            this.m_btnApplyFilter.Click += new System.EventHandler(this.m_btnApplyFilter_Click);
            // 
            // m_panelFiltreSeries
            // 
            this.m_panelFiltreSeries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelFiltreSeries.Controls.Add(this.m_wndListeSeries);
            this.m_panelFiltreSeries.Controls.Add(this.m_lblSelectSeriesTitle);
            this.m_panelFiltreSeries.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFiltreSeries.Location = new System.Drawing.Point(0, 73);
            this.m_extModeEdition.SetModeEdition(this.m_panelFiltreSeries, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltreSeries.Name = "m_panelFiltreSeries";
            this.m_panelFiltreSeries.Size = new System.Drawing.Size(439, 40);
            this.m_panelFiltreSeries.TabIndex = 3;
            // 
            // m_wndListeSeries
            // 
            this.m_wndListeSeries.CheckBoxes = true;
            this.m_wndListeSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeSeries.Location = new System.Drawing.Point(109, 0);
            this.m_extModeEdition.SetModeEdition(this.m_wndListeSeries, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeSeries.Name = "m_wndListeSeries";
            this.m_wndListeSeries.Size = new System.Drawing.Size(328, 38);
            this.m_wndListeSeries.SmallImageList = this.m_imagesData;
            this.m_wndListeSeries.TabIndex = 0;
            this.m_wndListeSeries.UseCompatibleStateImageBehavior = false;
            this.m_wndListeSeries.View = System.Windows.Forms.View.List;
            this.m_wndListeSeries.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.m_wndListeSeries_ItemChecked);
            // 
            // m_lblSelectSeriesTitle
            // 
            this.m_lblSelectSeriesTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblSelectSeriesTitle.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblSelectSeriesTitle, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblSelectSeriesTitle.Name = "m_lblSelectSeriesTitle";
            this.m_lblSelectSeriesTitle.Size = new System.Drawing.Size(109, 38);
            this.m_lblSelectSeriesTitle.TabIndex = 0;
            this.m_lblSelectSeriesTitle.Text = "Series to display :|20041";
            this.m_lblSelectSeriesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_chartTooltip
            // 
            this.m_chartTooltip.AutomaticDelay = 0;
            // 
            // m_chartControl
            // 
            this.m_chartControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            chartArea1.Name = "ChartArea1";
            this.m_chartControl.ChartAreas.Add(chartArea1);
            this.m_chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.m_chartControl.Legends.Add(legend1);
            this.m_chartControl.Location = new System.Drawing.Point(0, 113);
            this.m_extModeEdition.SetModeEdition(this.m_chartControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chartControl.Name = "m_chartControl";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.m_chartControl.Series.Add(series1);
            this.m_chartControl.Size = new System.Drawing.Size(439, 163);
            this.m_chartControl.TabIndex = 1;
            this.m_chartControl.Text = "chart1";
            this.m_chartControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_chartControl_MouseDown);
            this.m_chartControl.MouseLeave += new System.EventHandler(this.m_chartControl_MouseLeave);
            this.m_chartControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_chartControl_MouseMove);
            this.m_chartControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_chartControl_MouseUp);
            // 
            // m_imagesData
            // 
            this.m_imagesData.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesData.ImageStream")));
            this.m_imagesData.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesData.Images.SetKeyName(0, "data_good.png");
            this.m_imagesData.Images.SetKeyName(1, "data_error.png");
            // 
            // CControlChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_chartControl);
            this.Controls.Add(this.m_panelFiltreSeries);
            this.Controls.Add(this.m_panelFiltreSimple);
            this.Controls.Add(this.m_panelTop);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlChart";
            this.Size = new System.Drawing.Size(439, 276);
            this.m_panelTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_panelModeSouris.ResumeLayout(false);
            this.m_panelFiltreSimple.ResumeLayout(false);
            this.m_panelOutilsFiltre.ResumeLayout(false);
            this.m_panelFiltreSeries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_chartControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelTop;
        private CChartAExceptionMaitrisee m_chartControl;
        private System.Windows.Forms.RadioButton m_rbtn3D;
        private System.Windows.Forms.RadioButton m_rbtnMouse;
        private System.Windows.Forms.Panel m_panelModeSouris;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnFiltrer;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.RadioButton m_rbtnLoupe;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;
        private System.Windows.Forms.Panel m_panelFiltreSimple;
        private System.Windows.Forms.Panel m_panelOutilsFiltre;
        private System.Windows.Forms.Button m_btnApplyFilter;
        private sc2i.formulaire.win32.editor.CPanelFormulaireSurElement m_panelFormulairefiltreSimple;
        private System.Windows.Forms.Panel m_panelFiltreSeries;
        private System.Windows.Forms.Label m_lblSelectSeriesTitle;
        private System.Windows.Forms.ListView m_wndListeSeries;
        private System.Windows.Forms.ToolTip m_chartTooltip;
        private System.Windows.Forms.ImageList m_imagesData;
    }
}
