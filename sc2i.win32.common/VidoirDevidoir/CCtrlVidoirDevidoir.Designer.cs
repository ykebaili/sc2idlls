namespace sc2i.win32.common
{
	partial class CCtrlVidoirDevidoir
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
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_glNonSelec = new sc2i.win32.common.GlacialList();
            this.m_lblUnSelected = new System.Windows.Forms.Label();
            this.m_glSelec = new sc2i.win32.common.GlacialList();
            this.m_lblSelected = new System.Windows.Forms.Label();
            this.m_btnSelectAll = new System.Windows.Forms.Button();
            this.m_btnUnselectAll = new System.Windows.Forms.Button();
            this.m_btnSelect = new System.Windows.Forms.Button();
            this.m_btnUnselect = new System.Windows.Forms.Button();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BackColor = System.Drawing.Color.DimGray;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_glNonSelec);
            this.m_splitContainer.Panel1.Controls.Add(this.m_lblUnSelected);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_glSelec);
            this.m_splitContainer.Panel2.Controls.Add(this.m_lblSelected);
            this.m_splitContainer.Size = new System.Drawing.Size(426, 285);
            this.m_splitContainer.SplitterDistance = 210;
            this.m_splitContainer.SplitterWidth = 36;
            this.m_splitContainer.TabIndex = 0;
            this.m_splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.m_splitContainer_SplitterMoved);
            // 
            // m_glNonSelec
            // 
            this.m_glNonSelec.AllowColumnResize = true;
            this.m_glNonSelec.AllowMultiselect = true;
            this.m_glNonSelec.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_glNonSelec.AlternatingColors = false;
            this.m_glNonSelec.AutoHeight = true;
            this.m_glNonSelec.AutoSort = true;
            this.m_glNonSelec.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_glNonSelec.CanChangeActivationCheckBoxes = false;
            this.m_glNonSelec.CheckBoxes = false;
            this.m_glNonSelec.ContexteUtilisation = "";
            this.m_glNonSelec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_glNonSelec.EnableCustomisation = true;
            this.m_glNonSelec.FocusedItem = null;
            this.m_glNonSelec.FullRowSelect = true;
            this.m_glNonSelec.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_glNonSelec.GridColor = System.Drawing.SystemColors.ControlLight;
            this.m_glNonSelec.HeaderHeight = 22;
            this.m_glNonSelec.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_glNonSelec.HeaderTextColor = System.Drawing.Color.Black;
            this.m_glNonSelec.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_glNonSelec.HeaderVisible = true;
            this.m_glNonSelec.HeaderWordWrap = false;
            this.m_glNonSelec.HotColumnIndex = -1;
            this.m_glNonSelec.HotItemIndex = -1;
            this.m_glNonSelec.HotTracking = false;
            this.m_glNonSelec.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_glNonSelec.ImageList = null;
            this.m_glNonSelec.ItemHeight = 18;
            this.m_glNonSelec.ItemWordWrap = false;
            this.m_glNonSelec.ListeSource = null;
            this.m_glNonSelec.Location = new System.Drawing.Point(0, 20);
            this.m_glNonSelec.MaxHeight = 0;
            this.m_glNonSelec.Name = "m_glNonSelec";
            this.m_glNonSelec.SelectedTextColor = System.Drawing.Color.White;
            this.m_glNonSelec.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_glNonSelec.ShowBorder = true;
            this.m_glNonSelec.ShowFocusRect = true;
            this.m_glNonSelec.Size = new System.Drawing.Size(210, 265);
            this.m_glNonSelec.SortIndex = 0;
            this.m_glNonSelec.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_glNonSelec.TabIndex = 1;
            this.m_glNonSelec.Text = "Unselected Elements|10014";
            this.m_glNonSelec.TrierAuClicSurEnteteColonne = true;
            this.m_glNonSelec.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectionDoubleClic);
            this.m_glNonSelec.OnChangeSelection += new System.EventHandler(this.selectionChanged);
            // 
            // m_lblUnSelected
            // 
            this.m_lblUnSelected.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblUnSelected.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblUnSelected.ForeColor = System.Drawing.Color.White;
            this.m_lblUnSelected.Location = new System.Drawing.Point(0, 0);
            this.m_lblUnSelected.Name = "m_lblUnSelected";
            this.m_lblUnSelected.Size = new System.Drawing.Size(210, 20);
            this.m_lblUnSelected.TabIndex = 2;
            this.m_lblUnSelected.Text = "Unselected|121";
            this.m_lblUnSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_glSelec
            // 
            this.m_glSelec.AllowColumnResize = true;
            this.m_glSelec.AllowMultiselect = true;
            this.m_glSelec.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_glSelec.AlternatingColors = false;
            this.m_glSelec.AutoHeight = true;
            this.m_glSelec.AutoSort = true;
            this.m_glSelec.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_glSelec.CanChangeActivationCheckBoxes = false;
            this.m_glSelec.CheckBoxes = false;
            this.m_glSelec.ContexteUtilisation = "";
            this.m_glSelec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_glSelec.EnableCustomisation = true;
            this.m_glSelec.FocusedItem = null;
            this.m_glSelec.FullRowSelect = true;
            this.m_glSelec.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_glSelec.GridColor = System.Drawing.SystemColors.ControlLight;
            this.m_glSelec.HeaderHeight = 22;
            this.m_glSelec.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_glSelec.HeaderTextColor = System.Drawing.Color.Black;
            this.m_glSelec.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_glSelec.HeaderVisible = true;
            this.m_glSelec.HeaderWordWrap = false;
            this.m_glSelec.HotColumnIndex = -1;
            this.m_glSelec.HotItemIndex = -1;
            this.m_glSelec.HotTracking = false;
            this.m_glSelec.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_glSelec.ImageList = null;
            this.m_glSelec.ItemHeight = 18;
            this.m_glSelec.ItemWordWrap = false;
            this.m_glSelec.ListeSource = null;
            this.m_glSelec.Location = new System.Drawing.Point(0, 20);
            this.m_glSelec.MaxHeight = 0;
            this.m_glSelec.Name = "m_glSelec";
            this.m_glSelec.SelectedTextColor = System.Drawing.Color.White;
            this.m_glSelec.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_glSelec.ShowBorder = true;
            this.m_glSelec.ShowFocusRect = true;
            this.m_glSelec.Size = new System.Drawing.Size(180, 265);
            this.m_glSelec.SortIndex = 0;
            this.m_glSelec.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_glSelec.TabIndex = 1;
            this.m_glSelec.Text = "Selected Elements|10015";
            this.m_glSelec.TrierAuClicSurEnteteColonne = true;
            this.m_glSelec.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectionDoubleClic);
            this.m_glSelec.OnChangeSelection += new System.EventHandler(this.selectionChanged);
            // 
            // m_lblSelected
            // 
            this.m_lblSelected.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblSelected.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblSelected.ForeColor = System.Drawing.Color.White;
            this.m_lblSelected.Location = new System.Drawing.Point(0, 0);
            this.m_lblSelected.Name = "m_lblSelected";
            this.m_lblSelected.Size = new System.Drawing.Size(180, 20);
            this.m_lblSelected.TabIndex = 2;
            this.m_lblSelected.Text = "Selected|122";
            this.m_lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btnSelectAll
            // 
            this.m_btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnSelectAll.Location = new System.Drawing.Point(210, 23);
            this.m_btnSelectAll.Name = "m_btnSelectAll";
            this.m_btnSelectAll.Size = new System.Drawing.Size(36, 20);
            this.m_btnSelectAll.TabIndex = 2;
            this.m_btnSelectAll.Text = ">>";
            this.m_btnSelectAll.UseVisualStyleBackColor = true;
            this.m_btnSelectAll.Click += new System.EventHandler(this.m_btnSelectAll_Click);
            // 
            // m_btnUnselectAll
            // 
            this.m_btnUnselectAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnUnselectAll.Location = new System.Drawing.Point(210, 86);
            this.m_btnUnselectAll.Name = "m_btnUnselectAll";
            this.m_btnUnselectAll.Size = new System.Drawing.Size(36, 20);
            this.m_btnUnselectAll.TabIndex = 2;
            this.m_btnUnselectAll.Text = "<<";
            this.m_btnUnselectAll.UseVisualStyleBackColor = true;
            this.m_btnUnselectAll.Click += new System.EventHandler(this.m_btnUnselectAll_Click);
            // 
            // m_btnSelect
            // 
            this.m_btnSelect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnSelect.Location = new System.Drawing.Point(210, 43);
            this.m_btnSelect.Name = "m_btnSelect";
            this.m_btnSelect.Size = new System.Drawing.Size(36, 20);
            this.m_btnSelect.TabIndex = 2;
            this.m_btnSelect.Text = ">";
            this.m_btnSelect.UseVisualStyleBackColor = true;
            this.m_btnSelect.Click += new System.EventHandler(this.m_btnSelect_Click);
            // 
            // m_btnUnselect
            // 
            this.m_btnUnselect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnUnselect.Location = new System.Drawing.Point(210, 66);
            this.m_btnUnselect.Name = "m_btnUnselect";
            this.m_btnUnselect.Size = new System.Drawing.Size(36, 20);
            this.m_btnUnselect.TabIndex = 2;
            this.m_btnUnselect.Text = "<";
            this.m_btnUnselect.UseVisualStyleBackColor = true;
            this.m_btnUnselect.Click += new System.EventHandler(this.m_btnUnselect_Click);
            // 
            // CCtrlVidoirDevidoir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_btnUnselectAll);
            this.Controls.Add(this.m_btnSelect);
            this.Controls.Add(this.m_btnUnselect);
            this.Controls.Add(this.m_btnSelectAll);
            this.Controls.Add(this.m_splitContainer);
            this.Name = "CCtrlVidoirDevidoir";
            this.Size = new System.Drawing.Size(426, 285);
            this.SizeChanged += new System.EventHandler(this.CCtrlVidoirDevidoir_SizeChanged);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer m_splitContainer;
		private sc2i.win32.common.GlacialList m_glSelec;
		private sc2i.win32.common.GlacialList m_glNonSelec;
		private System.Windows.Forms.Button m_btnSelectAll;
		private System.Windows.Forms.Button m_btnUnselectAll;
		private System.Windows.Forms.Button m_btnSelect;
		private System.Windows.Forms.Button m_btnUnselect;
		private System.Windows.Forms.Label m_lblUnSelected;
		private System.Windows.Forms.Label m_lblSelected;
	}
}
