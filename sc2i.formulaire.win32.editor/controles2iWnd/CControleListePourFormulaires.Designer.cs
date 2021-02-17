namespace sc2i.formulaire.win32
{
	partial class CControleListePourFormulaires
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
			this.m_lnkRemplir = new System.Windows.Forms.LinkLabel();
			this.m_lblERREUR = new System.Windows.Forms.Label();
			this.m_linkForGrid = new System.Windows.Forms.LinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.m_labelForGrid = new System.Windows.Forms.Label();
			this.m_grid = new sc2i.win32.common.C2iDataGrid(this.components);
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
			this.SuspendLayout();
			// 
			// m_lnkRemplir
			// 
			this.m_lnkRemplir.BackColor = System.Drawing.Color.White;
			this.m_lnkRemplir.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_lnkRemplir.Location = new System.Drawing.Point(0, 0);
			this.m_lnkRemplir.Name = "m_lnkRemplir";
			this.m_lnkRemplir.Size = new System.Drawing.Size(503, 17);
			this.m_lnkRemplir.TabIndex = 1;
			this.m_lnkRemplir.TabStop = true;
			this.m_lnkRemplir.Text = "<<CLIQUEZ POUR AFFICHER LE CONTENU>>";
			this.m_lnkRemplir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.m_lnkRemplir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkRemplir_LinkClicked);
			// 
			// m_lblERREUR
			// 
			this.m_lblERREUR.AutoSize = true;
			this.m_lblERREUR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.m_lblERREUR.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_lblERREUR.ForeColor = System.Drawing.Color.White;
			this.m_lblERREUR.Location = new System.Drawing.Point(38, 122);
			this.m_lblERREUR.Name = "m_lblERREUR";
			this.m_lblERREUR.Size = new System.Drawing.Size(426, 24);
			this.m_lblERREUR.TabIndex = 2;
			this.m_lblERREUR.Text = "ERREUR LORS DU REMPLISSAGE DE LA LISTE";
			this.m_lblERREUR.Visible = false;
			// 
			// m_linkForGrid
			// 
			this.m_linkForGrid.BackColor = System.Drawing.Color.White;
			this.m_linkForGrid.Location = new System.Drawing.Point(42, 19);
			this.m_linkForGrid.Name = "m_linkForGrid";
			this.m_linkForGrid.Size = new System.Drawing.Size(31, 13);
			this.m_linkForGrid.TabIndex = 4;
			this.m_linkForGrid.TabStop = true;
			this.m_linkForGrid.Text = "LINK";
			this.m_linkForGrid.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.m_linkForGrid.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_linkForGrid_LinkClicked);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.m_labelForGrid);
			this.panel1.Controls.Add(this.m_linkForGrid);
			this.panel1.Location = new System.Drawing.Point(284, 46);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(147, 90);
			this.panel1.TabIndex = 5;
			this.panel1.Visible = false;
			// 
			// m_labelForGrid
			// 
			this.m_labelForGrid.Location = new System.Drawing.Point(20, 60);
			this.m_labelForGrid.Name = "m_labelForGrid";
			this.m_labelForGrid.Size = new System.Drawing.Size(35, 13);
			this.m_labelForGrid.TabIndex = 5;
			this.m_labelForGrid.Text = "label1";
			this.m_labelForGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_grid
			// 
			this.m_grid.AllowNavigation = false;
			this.m_grid.BackgroundColor = System.Drawing.Color.White;
			this.m_grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_grid.CaptionVisible = false;
			this.m_grid.CurrentElement = null;
			this.m_grid.DataMember = "";
			this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.m_grid.Location = new System.Drawing.Point(0, 17);
			this.m_grid.LockEdition = false;
			this.m_grid.Name = "m_grid";
			this.m_grid.ParentRowsVisible = false;
			this.m_grid.PreferredRowHeight = 24;
			this.m_grid.RowHeadersVisible = false;
			this.m_grid.Size = new System.Drawing.Size(503, 251);
			this.m_grid.TabIndex = 3;
			// 
			// CControlListeForFormulaire
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.m_lblERREUR);
			this.Controls.Add(this.m_grid);
			this.Controls.Add(this.m_lnkRemplir);
			this.Name = "CControlListeForFormulaire";
			this.Size = new System.Drawing.Size(503, 268);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel m_lnkRemplir;
		private System.Windows.Forms.Label m_lblERREUR;
		private sc2i.win32.common.C2iDataGrid m_grid;
		private System.Windows.Forms.LinkLabel m_linkForGrid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label m_labelForGrid;
	}
}
