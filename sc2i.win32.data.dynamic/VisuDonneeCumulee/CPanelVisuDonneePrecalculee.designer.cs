namespace sc2i.win32.data.dynamic
{
    partial class CPanelVisuDonneePrecalculee
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelVisuDonneePrecalculee));
            this.m_grid = new System.Windows.Forms.DataGridView();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_lblDetailFiltre = new System.Windows.Forms.Label();
            this.m_imageHasFiltreRef = new System.Windows.Forms.PictureBox();
            this.m_menuFiltres = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_lnkExport = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
            this.m_panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageHasFiltreRef)).BeginInit();
            this.SuspendLayout();
            // 
            // m_grid
            // 
            this.m_grid.AllowUserToAddRows = false;
            this.m_grid.AllowUserToDeleteRows = false;
            this.m_grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.m_grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.m_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grid.Location = new System.Drawing.Point(0, 28);
            this.m_grid.MultiSelect = false;
            this.m_grid.Name = "m_grid";
            this.m_grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.m_grid.RowHeadersVisible = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.m_grid.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.m_grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_grid.ShowEditingIcon = false;
            this.m_grid.ShowRowErrors = false;
            this.m_grid.Size = new System.Drawing.Size(400, 300);
            this.m_grid.TabIndex = 0;
            this.m_grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.m_grid_CellFormatting);
            this.m_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_grid_CellContentClick);
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_lblDetailFiltre);
            this.m_panelTop.Controls.Add(this.m_lnkExport);
            this.m_panelTop.Controls.Add(this.m_imageHasFiltreRef);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(400, 28);
            this.m_panelTop.TabIndex = 1;
            // 
            // m_lblDetailFiltre
            // 
            this.m_lblDetailFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblDetailFiltre.Location = new System.Drawing.Point(23, 0);
            this.m_lblDetailFiltre.Name = "m_lblDetailFiltre";
            this.m_lblDetailFiltre.Size = new System.Drawing.Size(321, 28);
            this.m_lblDetailFiltre.TabIndex = 5;
            this.m_lblDetailFiltre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_imageHasFiltreRef
            // 
            this.m_imageHasFiltreRef.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageHasFiltreRef.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_imageHasFiltreRef.Image = ((System.Drawing.Image)(resources.GetObject("m_imageHasFiltreRef.Image")));
            this.m_imageHasFiltreRef.Location = new System.Drawing.Point(0, 0);
            this.m_imageHasFiltreRef.Name = "m_imageHasFiltreRef";
            this.m_imageHasFiltreRef.Size = new System.Drawing.Size(23, 28);
            this.m_imageHasFiltreRef.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_imageHasFiltreRef.TabIndex = 4;
            this.m_imageHasFiltreRef.TabStop = false;
            this.m_imageHasFiltreRef.Click += new System.EventHandler(this.m_imageHasFiltreRef_Click);
            // 
            // m_menuFiltres
            // 
            this.m_menuFiltres.Name = "m_menuFiltres";
            this.m_menuFiltres.Size = new System.Drawing.Size(61, 4);
            this.m_menuFiltres.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuFiltres_Opening);
            // 
            // m_lnkExport
            // 
            this.m_lnkExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lnkExport.Location = new System.Drawing.Point(344, 0);
            this.m_lnkExport.Name = "m_lnkExport";
            this.m_lnkExport.Size = new System.Drawing.Size(56, 28);
            this.m_lnkExport.TabIndex = 6;
            this.m_lnkExport.TabStop = true;
            this.m_lnkExport.Text = "Export|26";
            this.m_lnkExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lnkExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkExport_LinkClicked);
            // 
            // CPanelVisuDonneePrecalculee
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_grid);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CPanelVisuDonneePrecalculee";
            this.Size = new System.Drawing.Size(400, 328);
            this.Load += new System.EventHandler(this.CPanelVisuDonneePrecalculee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
            this.m_panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageHasFiltreRef)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_grid;
        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Label m_lblDetailFiltre;
        private System.Windows.Forms.PictureBox m_imageHasFiltreRef;
        private System.Windows.Forms.ContextMenuStrip m_menuFiltres;
        private System.Windows.Forms.LinkLabel m_lnkExport;
    }
}
