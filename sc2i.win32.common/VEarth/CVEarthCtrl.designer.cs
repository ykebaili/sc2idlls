namespace sc2i.win32.common
{
    partial class CVEarthCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CVEarthCtrl));
            this.m_gMap = new GMap.NET.WindowsForms.GMapControl();
            this.m_toolbar = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnHybrid = new System.Windows.Forms.RadioButton();
            this.m_btnSat = new System.Windows.Forms.RadioButton();
            this.m_btnMap = new System.Windows.Forms.RadioButton();
            this.m_trackZoom = new System.Windows.Forms.TrackBar();
            this.m_selectedTimer = new System.Windows.Forms.Timer(this.components);
            this.m_toolbar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // m_gMap
            // 
            this.m_gMap.Bearing = 0F;
            this.m_gMap.CanDragMap = true;
            this.m_gMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.m_gMap.GrayScaleMode = false;
            this.m_gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.m_gMap.LevelsKeepInMemmory = 5;
            this.m_gMap.Location = new System.Drawing.Point(21, 29);
            this.m_gMap.MarkersEnabled = true;
            this.m_gMap.MaxZoom = 22;
            this.m_gMap.MinZoom = 2;
            this.m_gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.m_gMap.Name = "m_gMap";
            this.m_gMap.NegativeMode = false;
            this.m_gMap.PolygonsEnabled = true;
            this.m_gMap.RetryLoadTile = 0;
            this.m_gMap.RoutesEnabled = true;
            this.m_gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.m_gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.m_gMap.ShowTileGridLines = false;
            this.m_gMap.Size = new System.Drawing.Size(342, 261);
            this.m_gMap.TabIndex = 0;
            this.m_gMap.Zoom = 5D;
            this.m_gMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.m_gMap_OnMarkerClick);
            this.m_gMap.OnRouteClick += new GMap.NET.WindowsForms.RouteClick(this.m_gMap_OnRouteClick);
            this.m_gMap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.m_gMap_OnMapZoomChanged);
            this.m_gMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_gMap_MouseDown);
            this.m_gMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_gMap_MouseMove);
            this.m_gMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_gMap_MouseUp);
            // 
            // m_toolbar
            // 
            this.m_toolbar.Controls.Add(this.panel1);
            this.m_toolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_toolbar.Location = new System.Drawing.Point(0, 0);
            this.m_toolbar.Name = "m_toolbar";
            this.m_toolbar.Size = new System.Drawing.Size(363, 29);
            this.m_toolbar.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnHybrid);
            this.panel1.Controls.Add(this.m_btnSat);
            this.panel1.Controls.Add(this.m_btnMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(105, 29);
            this.panel1.TabIndex = 0;
            // 
            // m_btnHybrid
            // 
            this.m_btnHybrid.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnHybrid.AutoSize = true;
            this.m_btnHybrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnHybrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnHybrid.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHybrid.Image")));
            this.m_btnHybrid.Location = new System.Drawing.Point(64, 0);
            this.m_btnHybrid.Name = "m_btnHybrid";
            this.m_btnHybrid.Size = new System.Drawing.Size(32, 29);
            this.m_btnHybrid.TabIndex = 2;
            this.m_btnHybrid.TabStop = true;
            this.m_btnHybrid.UseVisualStyleBackColor = true;
            this.m_btnHybrid.CheckedChanged += new System.EventHandler(this.m_btnHybrid_CheckedChanged);
            // 
            // m_btnSat
            // 
            this.m_btnSat.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnSat.AutoSize = true;
            this.m_btnSat.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnSat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSat.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSat.Image")));
            this.m_btnSat.Location = new System.Drawing.Point(32, 0);
            this.m_btnSat.Name = "m_btnSat";
            this.m_btnSat.Size = new System.Drawing.Size(32, 29);
            this.m_btnSat.TabIndex = 1;
            this.m_btnSat.TabStop = true;
            this.m_btnSat.UseVisualStyleBackColor = true;
            this.m_btnSat.CheckedChanged += new System.EventHandler(this.m_btnSat_CheckedChanged);
            // 
            // m_btnMap
            // 
            this.m_btnMap.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnMap.AutoSize = true;
            this.m_btnMap.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnMap.Image = ((System.Drawing.Image)(resources.GetObject("m_btnMap.Image")));
            this.m_btnMap.Location = new System.Drawing.Point(0, 0);
            this.m_btnMap.Name = "m_btnMap";
            this.m_btnMap.Size = new System.Drawing.Size(32, 29);
            this.m_btnMap.TabIndex = 0;
            this.m_btnMap.TabStop = true;
            this.m_btnMap.UseVisualStyleBackColor = true;
            this.m_btnMap.CheckedChanged += new System.EventHandler(this.m_btnMap_CheckedChanged);
            // 
            // m_trackZoom
            // 
            this.m_trackZoom.AutoSize = false;
            this.m_trackZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_trackZoom.Location = new System.Drawing.Point(0, 29);
            this.m_trackZoom.Maximum = 22;
            this.m_trackZoom.Minimum = 2;
            this.m_trackZoom.Name = "m_trackZoom";
            this.m_trackZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_trackZoom.Size = new System.Drawing.Size(21, 261);
            this.m_trackZoom.TabIndex = 2;
            this.m_trackZoom.Value = 2;
            this.m_trackZoom.ValueChanged += new System.EventHandler(this.m_trackZoom_ValueChanged);
            // 
            // CVEarthCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gMap);
            this.Controls.Add(this.m_trackZoom);
            this.Controls.Add(this.m_toolbar);
            this.Name = "CVEarthCtrl";
            this.Size = new System.Drawing.Size(363, 290);
            this.m_toolbar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_toolbar;
        private System.Windows.Forms.TrackBar m_trackZoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton m_btnMap;
        private System.Windows.Forms.RadioButton m_btnHybrid;
        private System.Windows.Forms.RadioButton m_btnSat;
        private System.Windows.Forms.Timer m_selectedTimer;
        protected GMap.NET.WindowsForms.GMapControl m_gMap;
    }
}
