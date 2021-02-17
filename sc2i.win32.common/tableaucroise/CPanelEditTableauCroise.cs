using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Linq;

using sc2i.common;
using sc2i.win32.common;


namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CPanelEditTableauCroise.
	/// </summary>
	public class CPanelEditTableauCroise : System.Windows.Forms.UserControl, sc2i.win32.common.IControlALockEdition
	{
		private DataTable m_tableSource = new DataTable();
		private CTableauCroise m_tableauCroise = new CTableauCroise();
		private System.Windows.Forms.ListView m_wndListeChampsOrigine;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Panel m_panelOrigine;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListView m_wndListeCles;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView m_wndListeCumuls;
		private System.Windows.Forms.ColumnHeader colChamp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel m_panelDroite;
		private System.Windows.Forms.Panel m_panelCleCol;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView m_wndListeColonnes;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Panel m_panelCles;
		private System.Windows.Forms.Button m_btnAddCle;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel m_panelColonnes;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.Panel m_panelCumuls;
		private System.Windows.Forms.Splitter splitter4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button m_btnAddPivot;
		private System.Windows.Forms.Button m_btnDeleteCle;
		private System.Windows.Forms.Button m_btnDeletePivot;
		private System.Windows.Forms.Button m_btnAddCumul;
		private System.Windows.Forms.Button m_btnDeleteCumul;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelEditTableauCroise()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_wndListeChampsOrigine = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelOrigine = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_wndListeCles = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_wndListeCumuls = new System.Windows.Forms.ListView();
            this.colChamp = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.label4 = new System.Windows.Forms.Label();
            this.m_panelDroite = new System.Windows.Forms.Panel();
            this.m_panelCumuls = new System.Windows.Forms.Panel();
            this.m_btnAddCumul = new System.Windows.Forms.Button();
            this.m_btnDeleteCumul = new System.Windows.Forms.Button();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.m_panelCleCol = new System.Windows.Forms.Panel();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.m_panelColonnes = new System.Windows.Forms.Panel();
            this.m_btnAddPivot = new System.Windows.Forms.Button();
            this.m_wndListeColonnes = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnDeletePivot = new System.Windows.Forms.Button();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_panelCles = new System.Windows.Forms.Panel();
            this.m_btnDeleteCle = new System.Windows.Forms.Button();
            this.m_btnAddCle = new System.Windows.Forms.Button();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelOrigine.SuspendLayout();
            this.m_panelDroite.SuspendLayout();
            this.m_panelCumuls.SuspendLayout();
            this.m_panelCleCol.SuspendLayout();
            this.m_panelColonnes.SuspendLayout();
            this.m_panelCles.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_wndListeChampsOrigine
            // 
            this.m_wndListeChampsOrigine.AllowDrop = true;
            this.m_wndListeChampsOrigine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeChampsOrigine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeChampsOrigine.HideSelection = false;
            this.m_wndListeChampsOrigine.Location = new System.Drawing.Point(0, 16);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeChampsOrigine, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeChampsOrigine.MultiSelect = false;
            this.m_wndListeChampsOrigine.Name = "m_wndListeChampsOrigine";
            this.m_wndListeChampsOrigine.Size = new System.Drawing.Size(151, 248);
            this.m_wndListeChampsOrigine.TabIndex = 0;
            this.m_wndListeChampsOrigine.UseCompatibleStateImageBehavior = false;
            this.m_wndListeChampsOrigine.View = System.Windows.Forms.View.Details;
            this.m_wndListeChampsOrigine.SelectedIndexChanged += new System.EventHandler(this.m_wndListeChampsOrigine_SelectedIndexChanged);
            this.m_wndListeChampsOrigine.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_wndListeChampsOrigine_DragDrop);
            this.m_wndListeChampsOrigine.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_wndListeChampsOrigine_ItemDrag);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field|256";
            this.columnHeader1.Width = 140;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Original fields|197";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(36, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key fields (lines)|198";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelOrigine
            // 
            this.m_panelOrigine.Controls.Add(this.m_wndListeChampsOrigine);
            this.m_panelOrigine.Controls.Add(this.label1);
            this.m_panelOrigine.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelOrigine.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelOrigine, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelOrigine.Name = "m_panelOrigine";
            this.m_panelOrigine.Size = new System.Drawing.Size(151, 264);
            this.m_panelOrigine.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(151, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 264);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // m_wndListeCles
            // 
            this.m_wndListeCles.AllowDrop = true;
            this.m_wndListeCles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeCles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_wndListeCles.HideSelection = false;
            this.m_wndListeCles.Location = new System.Drawing.Point(0, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeCles, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeCles.MultiSelect = false;
            this.m_wndListeCles.Name = "m_wndListeCles";
            this.m_wndListeCles.Size = new System.Drawing.Size(168, 75);
            this.m_wndListeCles.TabIndex = 5;
            this.m_wndListeCles.UseCompatibleStateImageBehavior = false;
            this.m_wndListeCles.View = System.Windows.Forms.View.Details;
            this.m_wndListeCles.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_wndListeCles_DragDrop);
            this.m_wndListeCles.DragOver += new System.Windows.Forms.DragEventHandler(this.m_wndListeCles_DragOver);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Field|256";
            this.columnHeader2.Width = 137;
            // 
            // m_wndListeCumuls
            // 
            this.m_wndListeCumuls.AllowDrop = true;
            this.m_wndListeCumuls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeCumuls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colChamp,
            this.columnHeader4,
            this.columnHeader6});
            this.m_wndListeCumuls.HideSelection = false;
            this.m_wndListeCumuls.Location = new System.Drawing.Point(0, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeCumuls, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeCumuls.MultiSelect = false;
            this.m_wndListeCumuls.Name = "m_wndListeCumuls";
            this.m_wndListeCumuls.Size = new System.Drawing.Size(449, 142);
            this.m_wndListeCumuls.TabIndex = 8;
            this.m_wndListeCumuls.UseCompatibleStateImageBehavior = false;
            this.m_wndListeCumuls.View = System.Windows.Forms.View.Details;
            this.m_wndListeCumuls.DoubleClick += new System.EventHandler(this.m_wndListeCumuls_DoubleClick);
            this.m_wndListeCumuls.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_wndListeCumuls_DragDrop);
            this.m_wndListeCumuls.DragOver += new System.Windows.Forms.DragEventHandler(this.m_wndListeCumuls_DragOver);
            // 
            // colChamp
            // 
            this.colChamp.Text = "Field|256";
            this.colChamp.Width = 192;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Operation|133";
            this.columnHeader4.Width = 87;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Prefix|257";
            this.columnHeader6.Width = 68;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(39, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Cumulated fields|200";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelDroite
            // 
            this.m_panelDroite.Controls.Add(this.m_panelCumuls);
            this.m_panelDroite.Controls.Add(this.splitter4);
            this.m_panelDroite.Controls.Add(this.m_panelCleCol);
            this.m_panelDroite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDroite.Location = new System.Drawing.Point(154, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDroite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDroite.Name = "m_panelDroite";
            this.m_panelDroite.Size = new System.Drawing.Size(446, 264);
            this.m_panelDroite.TabIndex = 10;
            // 
            // m_panelCumuls
            // 
            this.m_panelCumuls.Controls.Add(this.m_btnAddCumul);
            this.m_panelCumuls.Controls.Add(this.m_btnDeleteCumul);
            this.m_panelCumuls.Controls.Add(this.label4);
            this.m_panelCumuls.Controls.Add(this.m_wndListeCumuls);
            this.m_panelCumuls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCumuls.Location = new System.Drawing.Point(0, 100);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelCumuls, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelCumuls.Name = "m_panelCumuls";
            this.m_panelCumuls.Size = new System.Drawing.Size(446, 164);
            this.m_panelCumuls.TabIndex = 13;
            // 
            // m_btnAddCumul
            // 
            this.m_btnAddCumul.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAddCumul.Location = new System.Drawing.Point(2, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAddCumul, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnAddCumul.Name = "m_btnAddCumul";
            this.m_btnAddCumul.Size = new System.Drawing.Size(18, 18);
            this.m_btnAddCumul.TabIndex = 12;
            this.m_btnAddCumul.Text = ">";
            this.m_btnAddCumul.Click += new System.EventHandler(this.m_btnAddCumul_Click);
            // 
            // m_btnDeleteCumul
            // 
            this.m_btnDeleteCumul.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnDeleteCumul.Location = new System.Drawing.Point(19, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDeleteCumul, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnDeleteCumul.Name = "m_btnDeleteCumul";
            this.m_btnDeleteCumul.Size = new System.Drawing.Size(18, 18);
            this.m_btnDeleteCumul.TabIndex = 12;
            this.m_btnDeleteCumul.Text = "X";
            this.m_btnDeleteCumul.Click += new System.EventHandler(this.m_btnDeleteCumul_Click);
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter4.Location = new System.Drawing.Point(0, 97);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(446, 3);
            this.splitter4.TabIndex = 14;
            this.splitter4.TabStop = false;
            // 
            // m_panelCleCol
            // 
            this.m_panelCleCol.Controls.Add(this.splitter3);
            this.m_panelCleCol.Controls.Add(this.m_panelColonnes);
            this.m_panelCleCol.Controls.Add(this.splitter2);
            this.m_panelCleCol.Controls.Add(this.m_panelCles);
            this.m_panelCleCol.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelCleCol.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelCleCol, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelCleCol.Name = "m_panelCleCol";
            this.m_panelCleCol.Size = new System.Drawing.Size(446, 97);
            this.m_panelCleCol.TabIndex = 10;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(171, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 97);
            this.splitter3.TabIndex = 12;
            this.splitter3.TabStop = false;
            // 
            // m_panelColonnes
            // 
            this.m_panelColonnes.Controls.Add(this.m_btnAddPivot);
            this.m_panelColonnes.Controls.Add(this.m_wndListeColonnes);
            this.m_panelColonnes.Controls.Add(this.label3);
            this.m_panelColonnes.Controls.Add(this.m_btnDeletePivot);
            this.m_panelColonnes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelColonnes.Location = new System.Drawing.Point(171, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelColonnes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelColonnes.Name = "m_panelColonnes";
            this.m_panelColonnes.Size = new System.Drawing.Size(275, 97);
            this.m_panelColonnes.TabIndex = 11;
            // 
            // m_btnAddPivot
            // 
            this.m_btnAddPivot.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAddPivot.Location = new System.Drawing.Point(6, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAddPivot, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAddPivot.Name = "m_btnAddPivot";
            this.m_btnAddPivot.Size = new System.Drawing.Size(18, 18);
            this.m_btnAddPivot.TabIndex = 12;
            this.m_btnAddPivot.Text = ">";
            this.m_btnAddPivot.Click += new System.EventHandler(this.m_btnAddPivot_Click);
            // 
            // m_wndListeColonnes
            // 
            this.m_wndListeColonnes.AllowDrop = true;
            this.m_wndListeColonnes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeColonnes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader5});
            this.m_wndListeColonnes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeColonnes.HideSelection = false;
            this.m_wndListeColonnes.Location = new System.Drawing.Point(0, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeColonnes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeColonnes.MultiSelect = false;
            this.m_wndListeColonnes.Name = "m_wndListeColonnes";
            this.m_wndListeColonnes.Size = new System.Drawing.Size(275, 75);
            this.m_wndListeColonnes.TabIndex = 9;
            this.m_wndListeColonnes.UseCompatibleStateImageBehavior = false;
            this.m_wndListeColonnes.View = System.Windows.Forms.View.Details;
            this.m_wndListeColonnes.DoubleClick += new System.EventHandler(this.m_wndListeColonnes_DoubleClick);
            this.m_wndListeColonnes.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_wndListeColonnes_DragDrop);
            this.m_wndListeColonnes.DragOver += new System.Windows.Forms.DragEventHandler(this.m_wndListeColonnes_DragOver);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Field|256";
            this.columnHeader3.Width = 137;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Prefix|257";
            this.columnHeader5.Width = 69;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(47, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Column fields|199";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // m_btnDeletePivot
            // 
            this.m_btnDeletePivot.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnDeletePivot.Location = new System.Drawing.Point(23, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDeletePivot, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDeletePivot.Name = "m_btnDeletePivot";
            this.m_btnDeletePivot.Size = new System.Drawing.Size(18, 18);
            this.m_btnDeletePivot.TabIndex = 12;
            this.m_btnDeletePivot.Text = "X";
            this.m_btnDeletePivot.Click += new System.EventHandler(this.m_btnDeletePivot_Click);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(168, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 97);
            this.splitter2.TabIndex = 11;
            this.splitter2.TabStop = false;
            // 
            // m_panelCles
            // 
            this.m_panelCles.Controls.Add(this.m_btnDeleteCle);
            this.m_panelCles.Controls.Add(this.label2);
            this.m_panelCles.Controls.Add(this.m_wndListeCles);
            this.m_panelCles.Controls.Add(this.m_btnAddCle);
            this.m_panelCles.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelCles.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelCles, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelCles.Name = "m_panelCles";
            this.m_panelCles.Size = new System.Drawing.Size(168, 97);
            this.m_panelCles.TabIndex = 10;
            // 
            // m_btnDeleteCle
            // 
            this.m_btnDeleteCle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnDeleteCle.Location = new System.Drawing.Point(19, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDeleteCle, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDeleteCle.Name = "m_btnDeleteCle";
            this.m_btnDeleteCle.Size = new System.Drawing.Size(18, 18);
            this.m_btnDeleteCle.TabIndex = 12;
            this.m_btnDeleteCle.Text = "X";
            this.m_btnDeleteCle.Click += new System.EventHandler(this.m_btnDeleteCle_Click);
            // 
            // m_btnAddCle
            // 
            this.m_btnAddCle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAddCle.Location = new System.Drawing.Point(2, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAddCle, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAddCle.Name = "m_btnAddCle";
            this.m_btnAddCle.Size = new System.Drawing.Size(18, 18);
            this.m_btnAddCle.TabIndex = 11;
            this.m_btnAddCle.Text = ">";
            this.m_btnAddCle.Click += new System.EventHandler(this.m_btnAddCle_Click);
            // 
            // CPanelEditTableauCroise
            // 
            this.Controls.Add(this.m_panelDroite);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelOrigine);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditTableauCroise";
            this.Size = new System.Drawing.Size(600, 264);
            this.m_panelOrigine.ResumeLayout(false);
            this.m_panelDroite.ResumeLayout(false);
            this.m_panelCumuls.ResumeLayout(false);
            this.m_panelCleCol.ResumeLayout(false);
            this.m_panelColonnes.ResumeLayout(false);
            this.m_panelCles.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void InitChamps( DataTable tableSource, CTableauCroise tableCroisee)
		{
			m_tableauCroise = tableCroisee;
			m_tableSource = tableSource;
			m_wndListeChampsOrigine.Items.Clear();
			if ( m_tableSource != null )
			{
				foreach ( DataColumn col in m_tableSource.Columns )
				{
					if ( m_tableauCroise.ChampsCle.Count(
                        champ => champ.NomChamp == col.ColumnName
                        ) == 0 )
					{
						bool bTrouve = false;
						foreach ( CColonneePivot pivot in m_tableauCroise.ChampsColonne )
						{
							if ( pivot.NomChamp == col.ColumnName )
							{
								bTrouve = true;
								break;
							}
						}
						if ( !bTrouve )
						{
							ListViewItem item = new ListViewItem ( );
							item.Text = col.ColumnName;
							item.Tag = col;
							m_wndListeChampsOrigine.Items.Add ( item );
						}
					}
				}
			}

			m_wndListeCles.Items.Clear();
			foreach ( CCleTableauCroise cle in m_tableauCroise.ChampsCle )
			{
				ListViewItem item = new ListViewItem();
				item.Text = cle.NomChamp;
				item.Tag = cle;
				m_wndListeCles.Items.Add ( item );
			}

			m_wndListeColonnes.Items.Clear();
			foreach ( CColonneePivot pivot in m_tableauCroise.ChampsColonne )
			{
				ListViewItem item = new ListViewItem ();
				item.Text = pivot.NomChamp;
				item.SubItems.Add ( pivot.Prefixe );
				item.Tag = pivot;
				m_wndListeColonnes.Items.Add ( item );
			}

			m_wndListeCumuls.Items.Clear();
			foreach ( CCumulCroise cumul in m_tableauCroise.CumulsCroises )
			{
				ListViewItem item = new ListViewItem();
				item.Text = cumul.NomChamp;
				item.SubItems.Add (  cumul.TypeCumul.ToString() );
				item.SubItems.Add ( cumul.PrefixFinal );
				item.Tag = cumul;
				m_wndListeCumuls.Items.Add ( item );
			}
		}

		public CTableauCroise TableauCroise
		{
			get
			{
				return m_tableauCroise;
			}
		}

		private void m_wndListeChampsOrigine_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
		}

		private void m_wndListeChampsOrigine_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}


		/// //////////////////////////////////////////
		private void m_btnAddCle_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeChampsOrigine.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeChampsOrigine.SelectedItems[0];
                DataColumn col = item.Tag as DataColumn;
                if ( col != null )
                {
				    m_tableauCroise.AddChampCle ( new CCleTableauCroise(col.ColumnName, col.DataType) );
				    InitChamps(m_tableSource, m_tableauCroise);
                }
			}
		}

		/// //////////////////////////////////////////
		private void m_btnDeleteCle_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeCles.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeCles.SelectedItems[0];
				m_tableauCroise.RemoveChampCle ( (CCleTableauCroise)item.Tag );
				InitChamps(m_tableSource, m_tableauCroise);
			}
		}

		/// //////////////////////////////////////////
		private void m_btnAddPivot_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeChampsOrigine.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeChampsOrigine.SelectedItems[0];
				CColonneePivot colonne = new CColonneePivot ( ((DataColumn)item.Tag).ColumnName, "" );
				if ( CFormEditColonnePivot.EditPivot ( colonne ) )
				{
					m_tableauCroise.AddChampColonne ( colonne );
					InitChamps(m_tableSource, m_tableauCroise);
				}
			}
		}

		/// //////////////////////////////////////////
		private void m_btnDeletePivot_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeColonnes.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeColonnes.SelectedItems[0];
				CColonneePivot colonne = (CColonneePivot)item.Tag;
                if (CFormAlerte.Afficher(I.T("Do not use the column @1 as a pivot column|30001", colonne.NomChamp),
					EFormAlerteType.Question) == DialogResult.Yes )
				{
					m_tableauCroise.RemoveChampColonne ( colonne );
					InitChamps(m_tableSource, m_tableauCroise);
				}
			}
		}

		/// //////////////////////////////////////////
		private void m_wndListeColonnes_DoubleClick(object sender, System.EventArgs e)
		{
			if ( m_wndListeColonnes.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeColonnes.SelectedItems[0];
				CColonneePivot colonne = (CColonneePivot)item.Tag;
				if ( CFormEditColonnePivot.EditPivot ( colonne ) )
				{
					item.SubItems[1].Text = colonne.Prefixe;
				}
			}
		}

		/// //////////////////////////////////////////
		private void m_btnAddCumul_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeChampsOrigine.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeChampsOrigine.SelectedItems[0];
                DataColumn col = item.Tag as DataColumn;
                if (col != null)
                {
                    CCumulCroise cumul = new CCumulCroise(col.ColumnName, TypeCumulCroise.Somme, col.DataType);
                    if (CFormEditCumulCroise.EditeCumul(cumul))
                    {
                        m_tableauCroise.AddCumul(cumul);
                        InitChamps(m_tableSource, m_tableauCroise);
                    }
                }
			}
		}

		/// //////////////////////////////////////////
		private void m_btnDeleteCumul_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeCumuls.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeCumuls.SelectedItems[0];
				CCumulCroise cumul = (CCumulCroise)item.Tag;
				if ( CFormAlerte.Afficher(I.T("Remove cumulated data on @1 ?|30002",cumul.NomChamp),
					EFormAlerteType.Question ) == DialogResult.Yes )
				{
					m_tableauCroise.RemoveCumul ( cumul );
					InitChamps(m_tableSource, m_tableauCroise);
				}
			}
		}

		/// //////////////////////////////////////////
		private void m_wndListeCumuls_DoubleClick(object sender, System.EventArgs e)
		{
			if ( m_wndListeCumuls.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeCumuls.SelectedItems[0];
				CCumulCroise cumul = (CCumulCroise)item.Tag;
				if ( CFormEditCumulCroise.EditeCumul( cumul ) )
				{
					item.SubItems[1].Text = cumul.TypeCumul.ToString();
					item.SubItems[2].Text = cumul.PrefixFinal;
				}
			}
		}


		/// //////////////////////////////////////////

		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value ;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void m_wndListeChampsOrigine_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem item = e.Item as ListViewItem;
            if (item == null)
                return;
            DataColumn col = item.Tag as DataColumn;
            if (col != null)
            {
                DoDragDrop(col, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void m_wndListeCles_DragOver(object sender, DragEventArgs e)
        {
            DataColumn colonne = e.Data.GetData(typeof(DataColumn)) as DataColumn;
            if (colonne != null)
            {
                if (m_tableauCroise.ChampsCle.Count(a => a.NomChamp == colonne.ColumnName) == 0)
                {
                    e.Effect = DragDropEffects.Move;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void m_wndListeCles_DragDrop(object sender, DragEventArgs e)
        {
            DataColumn colonne = e.Data.GetData(typeof(DataColumn)) as DataColumn;
            if (colonne != null)
            {
                if (m_tableauCroise.ChampsCle.Count(a => a.NomChamp == colonne.ColumnName) == 0)
                {
                m_tableauCroise.AddChampCle(new CCleTableauCroise(colonne.ColumnName, colonne.DataType));
                InitChamps(m_tableSource, m_tableauCroise);
                e.Effect = DragDropEffects.Move;
                return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void m_wndListeColonnes_DragOver(object sender, DragEventArgs e)
        {
            DataColumn colonne = e.Data.GetData(typeof(DataColumn)) as DataColumn;
            if (colonne != null)
            {
                if (m_tableauCroise.ChampsColonne.Count(a => a.NomChamp == colonne.ColumnName) == 0)
                {
                    e.Effect = DragDropEffects.Move;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void m_wndListeColonnes_DragDrop(object sender, DragEventArgs e)
        {
            DataColumn colonne = e.Data.GetData(typeof(DataColumn)) as DataColumn;
            if (colonne != null)
            {
                if (m_tableauCroise.ChampsColonne.Count(a => a.NomChamp == colonne.ColumnName) == 0)
                {
                    CColonneePivot pivot = new CColonneePivot(colonne.ColumnName, "");
                    if (CFormEditColonnePivot.EditPivot(pivot))
                    {
                        m_tableauCroise.AddChampColonne(pivot);
                        InitChamps(m_tableSource, m_tableauCroise);
                        e.Effect = DragDropEffects.Move;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void m_wndListeCumuls_DragOver(object sender, DragEventArgs e)
        {
            DataColumn colonne = e.Data.GetData(typeof(DataColumn)) as DataColumn;
            if (colonne != null)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void m_wndListeCumuls_DragDrop(object sender, DragEventArgs e)
        {
            DataColumn colonne = e.Data.GetData(typeof(DataColumn)) as DataColumn;
            if (colonne != null)
            {
                CCumulCroise cumul = new CCumulCroise(colonne.ColumnName, TypeCumulCroise.Somme, colonne.DataType);
                if (CFormEditCumulCroise.EditeCumul(cumul))
                {
                    m_tableauCroise.AddCumul(cumul);
                    InitChamps(m_tableSource, m_tableauCroise);
                    e.Effect = DragDropEffects.Copy;
                    return;

                }
            }
                e.Effect = DragDropEffects.None;
        }
	}
}
