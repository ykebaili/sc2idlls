namespace sc2i.win32.data
{
	partial class CFormMappageStringsStrings
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_wndListeA = new System.Windows.Forms.ListView();
            this.columnHeaderA = new System.Windows.Forms.ColumnHeader();
            this.m_wndListeB = new System.Windows.Forms.ListView();
            this.columnHeaderB = new System.Windows.Forms.ColumnHeader();
            this.m_imgFleche = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_ctrlMonterDescendreObjetDansListeA = new sc2i.win32.common.CCtrlUpDownListView();
            this.m_ctrlMonterDescendreObjetDansListeB = new sc2i.win32.common.CCtrlUpDownListView();
            this.m_panelTotal = new System.Windows.Forms.Panel();
            this.m_wndLigneB = new System.Windows.Forms.PictureBox();
            this.m_wndLigneA = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_imgFleche)).BeginInit();
            this.panel1.SuspendLayout();
            this.m_panelTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_wndLigneB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_wndLigneA)).BeginInit();
            this.SuspendLayout();
            // 
            // m_wndListeA
            // 
            this.m_wndListeA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderA});
            this.m_wndListeA.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndListeA.FullRowSelect = true;
            this.m_wndListeA.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeA.HideSelection = false;
            this.m_wndListeA.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeA.Name = "m_wndListeA";
            this.m_wndListeA.Size = new System.Drawing.Size(242, 284);
            this.m_wndListeA.TabIndex = 0;
            this.m_wndListeA.UseCompatibleStateImageBehavior = false;
            this.m_wndListeA.View = System.Windows.Forms.View.Details;
            this.m_wndListeA.SelectedIndexChanged += new System.EventHandler(this.m_wndListe1_SelectedIndexChanged);
            // 
            // columnHeaderA
            // 
            this.columnHeaderA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderA.Width = 220;
            // 
            // m_wndListeB
            // 
            this.m_wndListeB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderB});
            this.m_wndListeB.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndListeB.FullRowSelect = true;
            this.m_wndListeB.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeB.HideSelection = false;
            this.m_wndListeB.Location = new System.Drawing.Point(334, 0);
            this.m_wndListeB.Name = "m_wndListeB";
            this.m_wndListeB.Size = new System.Drawing.Size(242, 284);
            this.m_wndListeB.TabIndex = 1;
            this.m_wndListeB.UseCompatibleStateImageBehavior = false;
            this.m_wndListeB.View = System.Windows.Forms.View.Details;
            this.m_wndListeB.SelectedIndexChanged += new System.EventHandler(this.m_wndListe2_SelectedIndexChanged);
            // 
            // columnHeaderB
            // 
            this.columnHeaderB.Width = 220;
            // 
            // m_imgFleche
            // 
            this.m_imgFleche.BackColor = System.Drawing.Color.Black;
            this.m_imgFleche.Location = new System.Drawing.Point(1, 3);
            this.m_imgFleche.Name = "m_imgFleche";
            this.m_imgFleche.Size = new System.Drawing.Size(63, 10);
            this.m_imgFleche.TabIndex = 2;
            this.m_imgFleche.TabStop = false;
            this.m_imgFleche.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_imgFleche);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(242, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(92, 284);
            this.panel1.TabIndex = 3;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(200, 317);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Location = new System.Drawing.Point(303, 317);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_ctrlMonterDescendreObjetDansListeA
            // 
            this.m_ctrlMonterDescendreObjetDansListeA.ListeGeree = this.m_wndListeA;
            this.m_ctrlMonterDescendreObjetDansListeA.Location = new System.Drawing.Point(96, 291);
            this.m_ctrlMonterDescendreObjetDansListeA.LockEdition = false;
            this.m_ctrlMonterDescendreObjetDansListeA.Name = "m_ctrlMonterDescendreObjetDansListeA";
            this.m_ctrlMonterDescendreObjetDansListeA.ProprieteNumero = "";
            this.m_ctrlMonterDescendreObjetDansListeA.Size = new System.Drawing.Size(51, 20);
            this.m_ctrlMonterDescendreObjetDansListeA.TabIndex = 6;
            // 
            // m_ctrlMonterDescendreObjetDansListeB
            // 
            this.m_ctrlMonterDescendreObjetDansListeB.ListeGeree = this.m_wndListeB;
            this.m_ctrlMonterDescendreObjetDansListeB.Location = new System.Drawing.Point(430, 291);
            this.m_ctrlMonterDescendreObjetDansListeB.LockEdition = false;
            this.m_ctrlMonterDescendreObjetDansListeB.Name = "m_ctrlMonterDescendreObjetDansListeB";
            this.m_ctrlMonterDescendreObjetDansListeB.ProprieteNumero = "";
            this.m_ctrlMonterDescendreObjetDansListeB.Size = new System.Drawing.Size(51, 20);
            this.m_ctrlMonterDescendreObjetDansListeB.TabIndex = 7;
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.Controls.Add(this.m_wndLigneB);
            this.m_panelTotal.Controls.Add(this.m_wndLigneA);
            this.m_panelTotal.Controls.Add(this.panel1);
            this.m_panelTotal.Controls.Add(this.m_wndListeB);
            this.m_panelTotal.Controls.Add(this.m_wndListeA);
            this.m_panelTotal.Location = new System.Drawing.Point(5, 7);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(576, 284);
            this.m_panelTotal.TabIndex = 8;
            // 
            // m_wndLigneB
            // 
            this.m_wndLigneB.BackColor = System.Drawing.Color.Black;
            this.m_wndLigneB.Location = new System.Drawing.Point(218, 142);
            this.m_wndLigneB.Name = "m_wndLigneB";
            this.m_wndLigneB.Size = new System.Drawing.Size(100, 1);
            this.m_wndLigneB.TabIndex = 4;
            this.m_wndLigneB.TabStop = false;
            this.m_wndLigneB.Visible = false;
            // 
            // m_wndLigneA
            // 
            this.m_wndLigneA.BackColor = System.Drawing.Color.Black;
            this.m_wndLigneA.Location = new System.Drawing.Point(42, 64);
            this.m_wndLigneA.Name = "m_wndLigneA";
            this.m_wndLigneA.Size = new System.Drawing.Size(100, 1);
            this.m_wndLigneA.TabIndex = 3;
            this.m_wndLigneA.TabStop = false;
            this.m_wndLigneA.Visible = false;
            // 
            // CFormMappageStringsStrings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(593, 361);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelTotal);
            this.Controls.Add(this.m_ctrlMonterDescendreObjetDansListeB);
            this.Controls.Add(this.m_ctrlMonterDescendreObjetDansListeA);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormMappageStringsStrings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object mapping|140";
            this.Load += new System.EventHandler(this.CFormMappage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_imgFleche)).EndInit();
            this.panel1.ResumeLayout(false);
            this.m_panelTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_wndLigneB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_wndLigneA)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView m_wndListeA;
		private System.Windows.Forms.ColumnHeader columnHeaderA;
		private System.Windows.Forms.ListView m_wndListeB;
		private System.Windows.Forms.ColumnHeader columnHeaderB;
		private System.Windows.Forms.PictureBox m_imgFleche;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private sc2i.win32.common.CCtrlUpDownListView m_ctrlMonterDescendreObjetDansListeA;
		private sc2i.win32.common.CCtrlUpDownListView m_ctrlMonterDescendreObjetDansListeB;
		private System.Windows.Forms.Panel m_panelTotal;
		private System.Windows.Forms.PictureBox m_wndLigneB;
		private System.Windows.Forms.PictureBox m_wndLigneA;
	}
}