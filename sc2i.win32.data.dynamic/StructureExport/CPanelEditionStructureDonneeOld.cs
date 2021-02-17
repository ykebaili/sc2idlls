using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.win32.data.dynamic;
using sc2i.win32.expression;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelEditionStructureDonneeOld.
	/// </summary>
	public class CPanelEditionStructureDonneeOld : System.Windows.Forms.UserControl, IControlALockEdition
	{

		#region CDataArbre
		public class CDataArbre
		{
			public readonly ITableExport TableExport;
			
			public CDataArbre ( ITableExport table )
			{
				TableExport = table;
			}
		}
		#endregion

		public CPanelEditionStructureDonneeOld()
		{
			InitializeComponent();
		}


		private IElementAVariablesDynamiques m_elementAVariablesPourFiltre = null;

		private ITableExport m_tableEnEdition = null;

		private Hashtable m_tableTableToNode = new Hashtable();

		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleAutonome = null;

		private C2iStructureExport m_structureExport = new C2iStructureExport();


		#region Code généré par le Concepteur de composants

		private System.Windows.Forms.Panel m_panelStructure;
		private System.Windows.Forms.TreeView m_arbreTables;
		private System.Windows.Forms.Panel m_panelListeTables;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel m_panelDetailTable;
		private System.Windows.Forms.ContextMenu m_menuArbreTables;
		private System.Windows.Forms.MenuItem m_menuSupprimerTable;
		private sc2i.win32.common.C2iComboBox m_cmbType;
		private System.Windows.Forms.RadioButton m_btnStructureSimple;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton m_btnStructureComplexe;
		private System.Windows.Forms.Panel m_panelTypeDonnee;
		private System.Windows.Forms.Panel m_panelTypeStructure;
		private sc2i.win32.common.C2iTextBox m_txtNomTable;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private sc2i.win32.common.CWndLinkStd m_btnAjouter;
		private sc2i.win32.common.CWndLinkStd m_btnDetail;
		private sc2i.win32.common.CWndLinkStd m_btnSupprimer;
		private System.Windows.Forms.ImageList m_imagesChamps;
		private System.Windows.Forms.ListView m_wndListeChamps;
		private System.Windows.Forms.ContextMenu m_menuAjouterChamp;
		private System.Windows.Forms.MenuItem m_menuAjouterChampDonnee;
		private System.Windows.Forms.MenuItem m_menuAjouterChampCalcule;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.Windows.Forms.Panel m_panelTableExport;
		private System.Windows.Forms.Panel m_panelTableCalculee;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox m_imageFiltre;
		private System.Windows.Forms.ImageList m_imagesFiltre;
		private System.Windows.Forms.MenuItem m_menuAjouterTableCalculee;
		private System.Windows.Forms.MenuItem m_menuAjouterTable;
		private System.Windows.Forms.ImageList m_imagesArbre;
		private System.Windows.Forms.MenuItem m_menuAjouterTableCumulee;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private System.Windows.Forms.Panel m_panelDetailTableCalculee;
		private System.Windows.Forms.Splitter splitter2;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleValeurAutonome;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel m_lnkTableauCroise;
		private System.Windows.Forms.Panel m_panelTableauCroise;
		private System.Windows.Forms.CheckBox m_chkCroiser;
		private sc2i.win32.expression.CControleEditeFormule m_txtFormuleNbRecords;
		private System.Windows.Forms.Button m_btnOpen;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnPaste;
		private System.Windows.Forms.Button m_btnCopy;
		private System.Windows.Forms.PictureBox m_btnHaut;
		private System.Windows.Forms.PictureBox m_btnBas;
		private MenuItem m_menuTableAFormule;
		private MenuItem menuItem2;
		private Panel m_panelFiltreOuFormule;
		private PictureBox m_imageFormuleTable;
		private MenuItem m_menuAjouterChampsPersonnalisés;
        private MenuItem m_menuAddTableFusion;
        private Panel m_panelTableFusion;
        private SplitContainer m_splitContainer1;
        private Panel m_panelNomTableEtFiltre;
        private Label m_lblListeTables;
        private GlacialList m_wndListeTablesAFussionner;
		private System.ComponentModel.IContainer components;

	
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

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditionStructureDonneeOld));
            sc2i.win32.common.GLColumn glColumn1 = new sc2i.win32.common.GLColumn();
            this.m_panelStructure = new System.Windows.Forms.Panel();
            this.m_splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_panelListeTables = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_arbreTables = new System.Windows.Forms.TreeView();
            this.m_imagesArbre = new System.Windows.Forms.ImageList(this.components);
            this.m_panelDetailTable = new System.Windows.Forms.Panel();
            this.m_panelTableFusion = new System.Windows.Forms.Panel();
            this.m_wndListeTablesAFussionner = new sc2i.win32.common.GlacialList();
            this.m_lblListeTables = new System.Windows.Forms.Label();
            this.m_panelTableCalculee = new System.Windows.Forms.Panel();
            this.m_panelDetailTableCalculee = new System.Windows.Forms.Panel();
            this.m_txtFormuleNbRecords = new sc2i.win32.expression.CControleEditeFormule();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtFormuleValeurAutonome = new sc2i.win32.expression.CControleEditeFormule();
            this.label5 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.m_panelTableExport = new System.Windows.Forms.Panel();
            this.m_btnBas = new System.Windows.Forms.PictureBox();
            this.m_btnHaut = new System.Windows.Forms.PictureBox();
            this.m_panelTableauCroise = new System.Windows.Forms.Panel();
            this.m_chkCroiser = new System.Windows.Forms.CheckBox();
            this.m_lnkTableauCroise = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.m_wndListeChamps = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_imagesChamps = new System.Windows.Forms.ImageList(this.components);
            this.m_btnSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_btnDetail = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_panelNomTableEtFiltre = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.m_panelFiltreOuFormule = new System.Windows.Forms.Panel();
            this.m_imageFormuleTable = new System.Windows.Forms.PictureBox();
            this.m_imageFiltre = new System.Windows.Forms.PictureBox();
            this.m_txtNomTable = new sc2i.win32.common.C2iTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_menuArbreTables = new System.Windows.Forms.ContextMenu();
            this.m_menuAjouterTable = new System.Windows.Forms.MenuItem();
            this.m_menuTableAFormule = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterTableCumulee = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterTableCalculee = new System.Windows.Forms.MenuItem();
            this.m_menuAddTableFusion = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.m_menuSupprimerTable = new System.Windows.Forms.MenuItem();
            this.m_cmbType = new sc2i.win32.common.C2iComboBox();
            this.m_btnStructureSimple = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnStructureComplexe = new System.Windows.Forms.RadioButton();
            this.m_panelTypeDonnee = new System.Windows.Forms.Panel();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_panelTypeStructure = new System.Windows.Forms.Panel();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_menuAjouterChamp = new System.Windows.Forms.ContextMenu();
            this.m_menuAjouterChampDonnee = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterChampCalcule = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterChampsPersonnalisés = new System.Windows.Forms.MenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_imagesFiltre = new System.Windows.Forms.ImageList(this.components);
            this.m_panelStructure.SuspendLayout();
            this.m_splitContainer1.Panel1.SuspendLayout();
            this.m_splitContainer1.Panel2.SuspendLayout();
            this.m_splitContainer1.SuspendLayout();
            this.m_panelListeTables.SuspendLayout();
            this.m_panelDetailTable.SuspendLayout();
            this.m_panelTableFusion.SuspendLayout();
            this.m_panelTableCalculee.SuspendLayout();
            this.m_panelDetailTableCalculee.SuspendLayout();
            this.m_panelTableExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnBas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnHaut)).BeginInit();
            this.m_panelTableauCroise.SuspendLayout();
            this.m_panelNomTableEtFiltre.SuspendLayout();
            this.m_panelFiltreOuFormule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFormuleTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFiltre)).BeginInit();
            this.m_panelTypeDonnee.SuspendLayout();
            this.m_panelTypeStructure.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelStructure
            // 
            this.m_panelStructure.Controls.Add(this.m_splitContainer1);
            this.m_panelStructure.Controls.Add(this.splitter1);
            this.m_panelStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelStructure.Location = new System.Drawing.Point(0, 40);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelStructure, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelStructure.Name = "m_panelStructure";
            this.m_panelStructure.Size = new System.Drawing.Size(704, 457);
            this.m_panelStructure.TabIndex = 0;
            this.m_panelStructure.Paint += new System.Windows.Forms.PaintEventHandler(this.m_panelStructure_Paint);
            // 
            // m_splitContainer1
            // 
            this.m_splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer1.Location = new System.Drawing.Point(1, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer1.Name = "m_splitContainer1";
            // 
            // m_splitContainer1.Panel1
            // 
            this.m_splitContainer1.Panel1.Controls.Add(this.m_panelListeTables);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer1.Panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            // 
            // m_splitContainer1.Panel2
            // 
            this.m_splitContainer1.Panel2.Controls.Add(this.m_panelDetailTable);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitContainer1.Panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitContainer1.Size = new System.Drawing.Size(703, 457);
            this.m_splitContainer1.SplitterDistance = 200;
            this.m_splitContainer1.TabIndex = 4;
            // 
            // m_panelListeTables
            // 
            this.m_panelListeTables.Controls.Add(this.label1);
            this.m_panelListeTables.Controls.Add(this.m_arbreTables);
            this.m_panelListeTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListeTables.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelListeTables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelListeTables.Name = "m_panelListeTables";
            this.m_panelListeTables.Size = new System.Drawing.Size(198, 455);
            this.m_panelListeTables.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tables|182";
            // 
            // m_arbreTables
            // 
            this.m_arbreTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbreTables.ImageIndex = 0;
            this.m_arbreTables.ImageList = this.m_imagesArbre;
            this.m_arbreTables.Location = new System.Drawing.Point(0, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_arbreTables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbreTables.Name = "m_arbreTables";
            this.m_arbreTables.SelectedImageIndex = 0;
            this.m_arbreTables.Size = new System.Drawing.Size(198, 431);
            this.m_arbreTables.TabIndex = 0;
            this.m_arbreTables.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_arbreTables_MouseUp);
            this.m_arbreTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreTables_AfterSelect);
            // 
            // m_imagesArbre
            // 
            this.m_imagesArbre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesArbre.ImageStream")));
            this.m_imagesArbre.TransparentColor = System.Drawing.Color.White;
            this.m_imagesArbre.Images.SetKeyName(0, "");
            this.m_imagesArbre.Images.SetKeyName(1, "");
            this.m_imagesArbre.Images.SetKeyName(2, "");
            this.m_imagesArbre.Images.SetKeyName(3, "");
            this.m_imagesArbre.Images.SetKeyName(4, "");
            this.m_imagesArbre.Images.SetKeyName(5, "");
            this.m_imagesArbre.Images.SetKeyName(6, "");
            // 
            // m_panelDetailTable
            // 
            this.m_panelDetailTable.Controls.Add(this.m_panelTableFusion);
            this.m_panelDetailTable.Controls.Add(this.m_panelTableCalculee);
            this.m_panelDetailTable.Controls.Add(this.m_panelTableExport);
            this.m_panelDetailTable.Controls.Add(this.m_panelNomTableEtFiltre);
            this.m_panelDetailTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDetailTable.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDetailTable, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDetailTable.Name = "m_panelDetailTable";
            this.m_panelDetailTable.Size = new System.Drawing.Size(497, 455);
            this.m_panelDetailTable.TabIndex = 3;
            // 
            // m_panelTableFusion
            // 
            this.m_panelTableFusion.Controls.Add(this.m_wndListeTablesAFussionner);
            this.m_panelTableFusion.Controls.Add(this.m_lblListeTables);
            this.m_panelTableFusion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelTableFusion.Location = new System.Drawing.Point(0, 302);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableFusion, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableFusion.Name = "m_panelTableFusion";
            this.m_panelTableFusion.Size = new System.Drawing.Size(497, 153);
            this.m_panelTableFusion.TabIndex = 5;
            // 
            // m_wndListeTablesAFussionner
            // 
            this.m_wndListeTablesAFussionner.AllowColumnResize = true;
            this.m_wndListeTablesAFussionner.AllowMultiselect = false;
            this.m_wndListeTablesAFussionner.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeTablesAFussionner.AlternatingColors = false;
            this.m_wndListeTablesAFussionner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeTablesAFussionner.AutoHeight = true;
            this.m_wndListeTablesAFussionner.AutoSort = true;
            this.m_wndListeTablesAFussionner.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeTablesAFussionner.CanChangeActivationCheckBoxes = false;
            this.m_wndListeTablesAFussionner.CheckBoxes = false;
            glColumn1.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn1.ActiveControlItems")));
            glColumn1.BackColor = System.Drawing.Color.Transparent;
            glColumn1.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn1.ForColor = System.Drawing.Color.Black;
            glColumn1.ImageIndex = -1;
            glColumn1.IsCheckColumn = false;
            glColumn1.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn1.Name = "Table Name";
            glColumn1.Propriete = "NomTable";
            glColumn1.Text = "Table Name";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 300;
            this.m_wndListeTablesAFussionner.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn1});
            this.m_wndListeTablesAFussionner.ContexteUtilisation = "";
            this.m_wndListeTablesAFussionner.EnableCustomisation = true;
            this.m_wndListeTablesAFussionner.FocusedItem = null;
            this.m_wndListeTablesAFussionner.FullRowSelect = true;
            this.m_wndListeTablesAFussionner.GLGridLines = sc2i.win32.common.GLGridStyles.gridNone;
            this.m_wndListeTablesAFussionner.GridColor = System.Drawing.SystemColors.ControlLight;
            this.m_wndListeTablesAFussionner.HeaderHeight = 22;
            this.m_wndListeTablesAFussionner.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeTablesAFussionner.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeTablesAFussionner.HeaderVisible = true;
            this.m_wndListeTablesAFussionner.HeaderWordWrap = false;
            this.m_wndListeTablesAFussionner.HotColumnIndex = -1;
            this.m_wndListeTablesAFussionner.HotItemIndex = -1;
            this.m_wndListeTablesAFussionner.HotTracking = false;
            this.m_wndListeTablesAFussionner.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeTablesAFussionner.ImageList = null;
            this.m_wndListeTablesAFussionner.ItemHeight = 17;
            this.m_wndListeTablesAFussionner.ItemWordWrap = false;
            this.m_wndListeTablesAFussionner.ListeSource = null;
            this.m_wndListeTablesAFussionner.Location = new System.Drawing.Point(6, 22);
            this.m_wndListeTablesAFussionner.MaxHeight = 17;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeTablesAFussionner, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeTablesAFussionner.Name = "m_wndListeTablesAFussionner";
            this.m_wndListeTablesAFussionner.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeTablesAFussionner.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeTablesAFussionner.ShowBorder = true;
            this.m_wndListeTablesAFussionner.ShowFocusRect = true;
            this.m_wndListeTablesAFussionner.Size = new System.Drawing.Size(484, 116);
            this.m_wndListeTablesAFussionner.SortIndex = 0;
            this.m_wndListeTablesAFussionner.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeTablesAFussionner.TabIndex = 8;
            this.m_wndListeTablesAFussionner.Text = "glacialList1";
            this.m_wndListeTablesAFussionner.TrierAuClicSurEnteteColonne = true;
            // 
            // m_lblListeTables
            // 
            this.m_lblListeTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblListeTables.Location = new System.Drawing.Point(8, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblListeTables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblListeTables.Name = "m_lblListeTables";
            this.m_lblListeTables.Size = new System.Drawing.Size(136, 16);
            this.m_lblListeTables.TabIndex = 4;
            this.m_lblListeTables.Text = "Tables to merge|238";
            // 
            // m_panelTableCalculee
            // 
            this.m_panelTableCalculee.Controls.Add(this.m_panelDetailTableCalculee);
            this.m_panelTableCalculee.Controls.Add(this.splitter2);
            this.m_panelTableCalculee.Controls.Add(this.m_wndAide);
            this.m_panelTableCalculee.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTableCalculee.Location = new System.Drawing.Point(0, 172);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableCalculee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableCalculee.Name = "m_panelTableCalculee";
            this.m_panelTableCalculee.Size = new System.Drawing.Size(497, 130);
            this.m_panelTableCalculee.TabIndex = 3;
            // 
            // m_panelDetailTableCalculee
            // 
            this.m_panelDetailTableCalculee.Controls.Add(this.m_txtFormuleNbRecords);
            this.m_panelDetailTableCalculee.Controls.Add(this.label6);
            this.m_panelDetailTableCalculee.Controls.Add(this.m_txtFormuleValeurAutonome);
            this.m_panelDetailTableCalculee.Controls.Add(this.label5);
            this.m_panelDetailTableCalculee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDetailTableCalculee.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDetailTableCalculee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDetailTableCalculee.Name = "m_panelDetailTableCalculee";
            this.m_panelDetailTableCalculee.Size = new System.Drawing.Size(275, 130);
            this.m_panelDetailTableCalculee.TabIndex = 21;
            // 
            // m_txtFormuleNbRecords
            // 
            this.m_txtFormuleNbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleNbRecords.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleNbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleNbRecords.Formule = null;
            this.m_txtFormuleNbRecords.Location = new System.Drawing.Point(0, 16);
            this.m_txtFormuleNbRecords.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFormuleNbRecords, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormuleNbRecords.Name = "m_txtFormuleNbRecords";
            this.m_txtFormuleNbRecords.Size = new System.Drawing.Size(278, 101);
            this.m_txtFormuleNbRecords.TabIndex = 4;
            this.m_txtFormuleNbRecords.Enter += new System.EventHandler(this.OnEnterFormuleTableAutonome);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 120);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Valeur";
            // 
            // m_txtFormuleValeurAutonome
            // 
            this.m_txtFormuleValeurAutonome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleValeurAutonome.BackColor = System.Drawing.Color.White;
            this.m_txtFormuleValeurAutonome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtFormuleValeurAutonome.Formule = null;
            this.m_txtFormuleValeurAutonome.Location = new System.Drawing.Point(0, 136);
            this.m_txtFormuleValeurAutonome.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFormuleValeurAutonome, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormuleValeurAutonome.Name = "m_txtFormuleValeurAutonome";
            this.m_txtFormuleValeurAutonome.Size = new System.Drawing.Size(278, 112);
            this.m_txtFormuleValeurAutonome.TabIndex = 2;
            this.m_txtFormuleValeurAutonome.Enter += new System.EventHandler(this.OnEnterFormuleTableAutonome);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nombre d\'enregistrements";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(275, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 130);
            this.splitter2.TabIndex = 22;
            this.splitter2.TabStop = false;
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(278, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndAide, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(219, 130);
            this.m_wndAide.TabIndex = 20;
            this.m_wndAide.TypeInterroge = null;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // m_panelTableExport
            // 
            this.m_panelTableExport.Controls.Add(this.m_btnBas);
            this.m_panelTableExport.Controls.Add(this.m_btnHaut);
            this.m_panelTableExport.Controls.Add(this.m_panelTableauCroise);
            this.m_panelTableExport.Controls.Add(this.label4);
            this.m_panelTableExport.Controls.Add(this.m_wndListeChamps);
            this.m_panelTableExport.Controls.Add(this.m_btnSupprimer);
            this.m_panelTableExport.Controls.Add(this.m_btnDetail);
            this.m_panelTableExport.Controls.Add(this.m_btnAjouter);
            this.m_panelTableExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTableExport.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableExport, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableExport.Name = "m_panelTableExport";
            this.m_panelTableExport.Size = new System.Drawing.Size(497, 147);
            this.m_panelTableExport.TabIndex = 2;
            // 
            // m_btnBas
            // 
            this.m_btnBas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnBas.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBas.Image")));
            this.m_btnBas.Location = new System.Drawing.Point(473, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnBas, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnBas.Name = "m_btnBas";
            this.m_btnBas.Size = new System.Drawing.Size(15, 15);
            this.m_btnBas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_btnBas.TabIndex = 10;
            this.m_btnBas.TabStop = false;
            this.m_btnBas.Click += new System.EventHandler(this.m_btnBas_Click);
            // 
            // m_btnHaut
            // 
            this.m_btnHaut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnHaut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnHaut.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHaut.Image")));
            this.m_btnHaut.Location = new System.Drawing.Point(457, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnHaut, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnHaut.Name = "m_btnHaut";
            this.m_btnHaut.Size = new System.Drawing.Size(15, 15);
            this.m_btnHaut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_btnHaut.TabIndex = 9;
            this.m_btnHaut.TabStop = false;
            this.m_btnHaut.Click += new System.EventHandler(this.m_btnHaut_Click);
            // 
            // m_panelTableauCroise
            // 
            this.m_panelTableauCroise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelTableauCroise.Controls.Add(this.m_chkCroiser);
            this.m_panelTableauCroise.Controls.Add(this.m_lnkTableauCroise);
            this.m_panelTableauCroise.Location = new System.Drawing.Point(379, 123);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableauCroise, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableauCroise.Name = "m_panelTableauCroise";
            this.m_panelTableauCroise.Size = new System.Drawing.Size(118, 24);
            this.m_panelTableauCroise.TabIndex = 8;
            // 
            // m_chkCroiser
            // 
            this.m_chkCroiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkCroiser.Location = new System.Drawing.Point(3, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkCroiser, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkCroiser.Name = "m_chkCroiser";
            this.m_chkCroiser.Size = new System.Drawing.Size(16, 24);
            this.m_chkCroiser.TabIndex = 8;
            this.m_chkCroiser.CheckedChanged += new System.EventHandler(this.m_chkCroiser_CheckedChanged);
            // 
            // m_lnkTableauCroise
            // 
            this.m_lnkTableauCroise.Location = new System.Drawing.Point(17, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkTableauCroise, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkTableauCroise.Name = "m_lnkTableauCroise";
            this.m_lnkTableauCroise.Size = new System.Drawing.Size(98, 23);
            this.m_lnkTableauCroise.TabIndex = 7;
            this.m_lnkTableauCroise.TabStop = true;
            this.m_lnkTableauCroise.Text = "Tableau croisé|187";
            this.m_lnkTableauCroise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lnkTableauCroise.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkTableauCroise_LinkClicked);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Champs de la table|186";
            // 
            // m_wndListeChamps
            // 
            this.m_wndListeChamps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeChamps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeChamps.FullRowSelect = true;
            this.m_wndListeChamps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeChamps.HideSelection = false;
            this.m_wndListeChamps.LabelEdit = true;
            this.m_wndListeChamps.Location = new System.Drawing.Point(6, 16);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeChamps, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeChamps.MultiSelect = false;
            this.m_wndListeChamps.Name = "m_wndListeChamps";
            this.m_wndListeChamps.Size = new System.Drawing.Size(481, 103);
            this.m_wndListeChamps.SmallImageList = this.m_imagesChamps;
            this.m_wndListeChamps.TabIndex = 2;
            this.m_wndListeChamps.UseCompatibleStateImageBehavior = false;
            this.m_wndListeChamps.View = System.Windows.Forms.View.Details;
            this.m_wndListeChamps.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.m_wndListeChamps_AfterLabelEdit);
            this.m_wndListeChamps.SelectedIndexChanged += new System.EventHandler(this.m_wndListeChamps_SelectedIndexChanged);
            this.m_wndListeChamps.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_wndListeChamps_MouseMove);
            this.m_wndListeChamps.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.m_wndListeChamps_BeforeLabelEdit);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 500;
            // 
            // m_imagesChamps
            // 
            this.m_imagesChamps.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesChamps.ImageStream")));
            this.m_imagesChamps.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesChamps.Images.SetKeyName(0, "");
            this.m_imagesChamps.Images.SetKeyName(1, "");
            this.m_imagesChamps.Images.SetKeyName(2, "");
            this.m_imagesChamps.Images.SetKeyName(3, "");
            // 
            // m_btnSupprimer
            // 
            this.m_btnSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnSupprimer.Location = new System.Drawing.Point(136, 125);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnSupprimer.Name = "m_btnSupprimer";
            this.m_btnSupprimer.Size = new System.Drawing.Size(80, 16);
            this.m_btnSupprimer.TabIndex = 6;
            this.m_btnSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnSupprimer.LinkClicked += new System.EventHandler(this.m_btnSupprimer_LinkClicked);
            // 
            // m_btnDetail
            // 
            this.m_btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnDetail.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnDetail.Location = new System.Drawing.Point(72, 125);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDetail, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDetail.Name = "m_btnDetail";
            this.m_btnDetail.Size = new System.Drawing.Size(64, 16);
            this.m_btnDetail.TabIndex = 5;
            this.m_btnDetail.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnDetail.LinkClicked += new System.EventHandler(this.m_btnDetail_LinkClicked);
            // 
            // m_btnAjouter
            // 
            this.m_btnAjouter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAjouter.Location = new System.Drawing.Point(8, 125);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjouter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAjouter.Name = "m_btnAjouter";
            this.m_btnAjouter.Size = new System.Drawing.Size(64, 16);
            this.m_btnAjouter.TabIndex = 4;
            this.m_btnAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAjouter.LinkClicked += new System.EventHandler(this.m_btnAjouter_LinkClicked);
            // 
            // m_panelNomTableEtFiltre
            // 
            this.m_panelNomTableEtFiltre.Controls.Add(this.label3);
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_panelFiltreOuFormule);
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_txtNomTable);
            this.m_panelNomTableEtFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelNomTableEtFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelNomTableEtFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelNomTableEtFiltre.Name = "m_panelNomTableEtFiltre";
            this.m_panelNomTableEtFiltre.Size = new System.Drawing.Size(497, 25);
            this.m_panelNomTableEtFiltre.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Table|165";
            // 
            // m_panelFiltreOuFormule
            // 
            this.m_panelFiltreOuFormule.Controls.Add(this.m_imageFormuleTable);
            this.m_panelFiltreOuFormule.Controls.Add(this.m_imageFiltre);
            this.m_panelFiltreOuFormule.Location = new System.Drawing.Point(90, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltreOuFormule, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltreOuFormule.Name = "m_panelFiltreOuFormule";
            this.m_panelFiltreOuFormule.Size = new System.Drawing.Size(24, 17);
            this.m_panelFiltreOuFormule.TabIndex = 11;
            // 
            // m_imageFormuleTable
            // 
            this.m_imageFormuleTable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageFormuleTable.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_imageFormuleTable.Image = ((System.Drawing.Image)(resources.GetObject("m_imageFormuleTable.Image")));
            this.m_imageFormuleTable.Location = new System.Drawing.Point(-8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_imageFormuleTable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_imageFormuleTable.Name = "m_imageFormuleTable";
            this.m_imageFormuleTable.Size = new System.Drawing.Size(16, 17);
            this.m_imageFormuleTable.TabIndex = 9;
            this.m_imageFormuleTable.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_imageFormuleTable, "Click gauche pour filtrer, click droit pour annuler le filtre|163");
            this.m_imageFormuleTable.Click += new System.EventHandler(this.m_imageFormuleTable_Click);
            // 
            // m_imageFiltre
            // 
            this.m_imageFiltre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageFiltre.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_imageFiltre.Image = ((System.Drawing.Image)(resources.GetObject("m_imageFiltre.Image")));
            this.m_imageFiltre.Location = new System.Drawing.Point(8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_imageFiltre, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_imageFiltre.Name = "m_imageFiltre";
            this.m_imageFiltre.Size = new System.Drawing.Size(16, 17);
            this.m_imageFiltre.TabIndex = 8;
            this.m_imageFiltre.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_imageFiltre, "Click gauche pour filtrer, click droit pour annuler le filtre|163");
            this.m_imageFiltre.Click += new System.EventHandler(this.m_imageFiltre_Click);
            this.m_imageFiltre.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_imageFiltre_MouseUp);
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtNomTable.Location = new System.Drawing.Point(120, 0);
            this.m_txtNomTable.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtNomTable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(374, 22);
            this.m_txtNomTable.TabIndex = 0;
            this.m_txtNomTable.Text = "c2iTextBox1";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Black;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 457);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_menuArbreTables
            // 
            this.m_menuArbreTables.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouterTable,
            this.m_menuTableAFormule,
            this.m_menuAjouterTableCumulee,
            this.m_menuAjouterTableCalculee,
            this.m_menuAddTableFusion,
            this.menuItem2,
            this.m_menuSupprimerTable});
            this.m_menuArbreTables.Popup += new System.EventHandler(this.m_menuArbreTables_Popup);
            // 
            // m_menuAjouterTable
            // 
            this.m_menuAjouterTable.Index = 0;
            this.m_menuAjouterTable.Text = "Ajouter une table";
            this.m_menuAjouterTable.Click += new System.EventHandler(this.m_menuAjouterTable_Click);
            // 
            // m_menuTableAFormule
            // 
            this.m_menuTableAFormule.Index = 1;
            this.m_menuTableAFormule.Text = "Ajouter une table à partir d\'une formule";
            this.m_menuTableAFormule.Click += new System.EventHandler(this.m_menuTableAFormule_Click);
            // 
            // m_menuAjouterTableCumulee
            // 
            this.m_menuAjouterTableCumulee.Index = 2;
            this.m_menuAjouterTableCumulee.Text = "Ajouter une table cumulée";
            this.m_menuAjouterTableCumulee.Click += new System.EventHandler(this.m_menuAddTableCumulee_Click);
            // 
            // m_menuAjouterTableCalculee
            // 
            this.m_menuAjouterTableCalculee.Index = 3;
            this.m_menuAjouterTableCalculee.Text = "Ajouter une table calculée";
            this.m_menuAjouterTableCalculee.Click += new System.EventHandler(this.m_menuTableIndep_Click);
            // 
            // m_menuAddTableFusion
            // 
            this.m_menuAddTableFusion.Index = 4;
            this.m_menuAddTableFusion.Text = "Ajouter une table fusionnée";
            this.m_menuAddTableFusion.Click += new System.EventHandler(this.m_menuAddTableFusion_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.Text = "-";
            // 
            // m_menuSupprimerTable
            // 
            this.m_menuSupprimerTable.Index = 6;
            this.m_menuSupprimerTable.Text = "Supprimer la table";
            this.m_menuSupprimerTable.Click += new System.EventHandler(this.menuSupprimer_Click);
            // 
            // m_cmbType
            // 
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(59, 0);
            this.m_cmbType.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbType, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(319, 21);
            this.m_cmbType.TabIndex = 11;
            this.m_cmbType.SelectedIndexChanged += new System.EventHandler(this.m_cmbType_SelectedIndexChanged);
            // 
            // m_btnStructureSimple
            // 
            this.m_btnStructureSimple.Location = new System.Drawing.Point(8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnStructureSimple, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnStructureSimple.Name = "m_btnStructureSimple";
            this.m_btnStructureSimple.Size = new System.Drawing.Size(136, 16);
            this.m_btnStructureSimple.TabIndex = 13;
            this.m_btnStructureSimple.Text = "Structure simple|184";
            this.m_btnStructureSimple.CheckedChanged += new System.EventHandler(this.m_btnTypeStructure_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Type :|183";
            // 
            // m_btnStructureComplexe
            // 
            this.m_btnStructureComplexe.Location = new System.Drawing.Point(146, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnStructureComplexe, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnStructureComplexe.Name = "m_btnStructureComplexe";
            this.m_btnStructureComplexe.Size = new System.Drawing.Size(136, 16);
            this.m_btnStructureComplexe.TabIndex = 14;
            this.m_btnStructureComplexe.Text = "Structure complexe|185";
            this.m_btnStructureComplexe.CheckedChanged += new System.EventHandler(this.m_btnTypeStructure_CheckedChanged);
            // 
            // m_panelTypeDonnee
            // 
            this.m_panelTypeDonnee.Controls.Add(this.m_btnOpen);
            this.m_panelTypeDonnee.Controls.Add(this.m_btnSave);
            this.m_panelTypeDonnee.Controls.Add(this.m_btnPaste);
            this.m_panelTypeDonnee.Controls.Add(this.m_btnCopy);
            this.m_panelTypeDonnee.Controls.Add(this.m_cmbType);
            this.m_panelTypeDonnee.Controls.Add(this.label2);
            this.m_panelTypeDonnee.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTypeDonnee.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTypeDonnee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTypeDonnee.Name = "m_panelTypeDonnee";
            this.m_panelTypeDonnee.Size = new System.Drawing.Size(704, 24);
            this.m_panelTypeDonnee.TabIndex = 15;
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(672, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.m_btnOpen.TabIndex = 28;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(648, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.m_btnSave.TabIndex = 27;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(616, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(24, 23);
            this.m_btnPaste.TabIndex = 26;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(592, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.m_btnCopy.TabIndex = 25;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_panelTypeStructure
            // 
            this.m_panelTypeStructure.Controls.Add(this.m_btnStructureSimple);
            this.m_panelTypeStructure.Controls.Add(this.m_btnStructureComplexe);
            this.m_panelTypeStructure.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTypeStructure.Location = new System.Drawing.Point(0, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTypeStructure, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTypeStructure.Name = "m_panelTypeStructure";
            this.m_panelTypeStructure.Size = new System.Drawing.Size(704, 16);
            this.m_panelTypeStructure.TabIndex = 16;
            // 
            // m_menuAjouterChamp
            // 
            this.m_menuAjouterChamp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouterChampDonnee,
            this.m_menuAjouterChampCalcule,
            this.m_menuAjouterChampsPersonnalisés});
            this.m_menuAjouterChamp.Popup += new System.EventHandler(this.m_menuAjouterChamp_Popup);
            // 
            // m_menuAjouterChampDonnee
            // 
            this.m_menuAjouterChampDonnee.Index = 0;
            this.m_menuAjouterChampDonnee.Text = "Champ de donnée";
            this.m_menuAjouterChampDonnee.Popup += new System.EventHandler(this.m_menuAjouterChampDonnee_Popup);
            this.m_menuAjouterChampDonnee.Click += new System.EventHandler(this.m_menuAjouterChampDonnee_Click);
            // 
            // m_menuAjouterChampCalcule
            // 
            this.m_menuAjouterChampCalcule.Index = 1;
            this.m_menuAjouterChampCalcule.Text = "Champ calculé";
            this.m_menuAjouterChampCalcule.Click += new System.EventHandler(this.m_menuAjouterChampCalcule_Click);
            // 
            // m_menuAjouterChampsPersonnalisés
            // 
            this.m_menuAjouterChampsPersonnalisés.Index = 2;
            this.m_menuAjouterChampsPersonnalisés.Text = "Champs personnalisés";
            this.m_menuAjouterChampsPersonnalisés.Click += new System.EventHandler(this.m_menuAjouterChampsPersonnalisés_Click);
            // 
            // m_imagesFiltre
            // 
            this.m_imagesFiltre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesFiltre.ImageStream")));
            this.m_imagesFiltre.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesFiltre.Images.SetKeyName(0, "");
            this.m_imagesFiltre.Images.SetKeyName(1, "");
            // 
            // CPanelEditionStructureDonneeOld
            // 
            this.Controls.Add(this.m_panelStructure);
            this.Controls.Add(this.m_panelTypeStructure);
            this.Controls.Add(this.m_panelTypeDonnee);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditionStructureDonneeOld";
            this.Size = new System.Drawing.Size(704, 497);
            this.Load += new System.EventHandler(this.CPanelEditionStructureDonneeOld_Load);
            this.m_panelStructure.ResumeLayout(false);
            this.m_splitContainer1.Panel1.ResumeLayout(false);
            this.m_splitContainer1.Panel2.ResumeLayout(false);
            this.m_splitContainer1.ResumeLayout(false);
            this.m_panelListeTables.ResumeLayout(false);
            this.m_panelDetailTable.ResumeLayout(false);
            this.m_panelTableFusion.ResumeLayout(false);
            this.m_panelTableCalculee.ResumeLayout(false);
            this.m_panelDetailTableCalculee.ResumeLayout(false);
            this.m_panelTableExport.ResumeLayout(false);
            this.m_panelTableExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnBas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnHaut)).EndInit();
            this.m_panelTableauCroise.ResumeLayout(false);
            this.m_panelNomTableEtFiltre.ResumeLayout(false);
            this.m_panelNomTableEtFiltre.PerformLayout();
            this.m_panelFiltreOuFormule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFormuleTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFiltre)).EndInit();
            this.m_panelTypeDonnee.ResumeLayout(false);
            this.m_panelTypeDonnee.PerformLayout();
            this.m_panelTypeStructure.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_panelStructure_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void m_arbreTables_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!LockEdition && (e.Button & MouseButtons.Right) == MouseButtons.Right )
				ShowMenuArbre ( new Point ( e.X, e.Y ) );
		}

		//-------------------------------------------------------------------------
		public C2iStructureExport StructureExport
		{
			get
			{
				MAJ_Champs();
				return m_structureExport;
			}
			set
			{	
				if ( value == null )
					m_structureExport = new C2iStructureExport();
				else
					m_structureExport = (C2iStructureExport)CCloner2iSerializable.Clone ( value );	
				InitChamps();
			}
		}

		//-------------------------------------------------------------------------
		public IElementAVariablesDynamiques ElementAVariablesPourFiltre
		{
			get
			{
				return m_elementAVariablesPourFiltre;
			}
			set
			{
				m_elementAVariablesPourFiltre = value;
				C2iTableExportCalculee.CTypeForFormule leType = new C2iTableExportCalculee.CTypeForFormule (m_elementAVariablesPourFiltre);

				m_wndAide.TypeInterroge = typeof ( C2iTableExportCalculee.CTypeForFormule );
				m_wndAide.FournisseurProprietes = leType;

				m_txtFormuleValeurAutonome.Init ( m_wndAide.FournisseurProprietes, m_wndAide.TypeInterroge );
				m_txtFormuleNbRecords.Init ( m_wndAide.FournisseurProprietes, m_wndAide.TypeInterroge );
			}

		}


		//-------------------------------------------------------------------------
		private bool m_bComboTypeInitialized = false;
		private CResultAErreur InitComboBoxType()
		{
			CResultAErreur result = CResultAErreur.True;
		
			if (m_bComboTypeInitialized)
				return result;

			//CInfoClasseDynamique[] infosClasses = DynamicClassAttribute.GetAllDynamicClass();
			ArrayList infosClasses = new ArrayList(DynamicClassAttribute.GetAllDynamicClass());
			infosClasses.Insert(0, new CInfoClasseDynamique(typeof(DBNull), "Aucun"));
			m_cmbType.DataSource = null;
			m_cmbType.DataSource = infosClasses;
			m_cmbType.ValueMember = "Classe";
			m_cmbType.DisplayMember = "Nom";

			m_cmbType.SelectedIndex = 0;

			m_bComboTypeInitialized = true;
			return result;
		}

		//-------------------------------------------------------------------------
		private void MAJ_Champs()
		{
			if ( m_gestionnaireModeEdition.ModeEdition )
			{
				ValideTableEnCours();
				m_structureExport.IsStructureComplexe = m_btnStructureComplexe.Checked;
			}
		}

		//-------------------------------------------------------------------------
		private bool m_bIsInitialising = false;
		private  CResultAErreur InitChamps()
		{
			CResultAErreur result = CResultAErreur.True;

			m_bIsInitialising = true;

			AfficheInfosNoeud ( null );

			result = InitComboBoxType();
			if ( m_structureExport == null )
				return result;

			if ( m_structureExport.TypeSource == null && m_cmbType.SelectedValue != typeof(DBNull) )
			{
				m_structureExport.TypeSource = (Type)m_cmbType.SelectedValue;
			}

			m_btnStructureComplexe.Checked = m_structureExport.IsStructureComplexe;
			m_btnStructureSimple.Checked = !m_structureExport.IsStructureComplexe;
			ReloadTree();

			UpdateAspect();
			if (m_structureExport.TypeSource!=null)
			{
				m_cmbType.SelectedValue = m_structureExport.TypeSource;
			}

			bool bHasChamps = m_structureExport.Table.TablesFilles.Length > 0  ||
				m_structureExport.Table.Champs.Length > 0;

			m_btnStructureComplexe.Enabled = !bHasChamps && m_gestionnaireModeEdition.ModeEdition;
			m_btnStructureSimple.Enabled = !bHasChamps && m_gestionnaireModeEdition.ModeEdition;

			m_bIsInitialising = false;

			C2iTableExportCalculee.CTypeForFormule leType = new C2iTableExportCalculee.CTypeForFormule (m_elementAVariablesPourFiltre);

			m_wndAide.TypeInterroge = typeof ( C2iTableExportCalculee.CTypeForFormule );
			m_wndAide.FournisseurProprietes = leType;

            m_wndListeTablesAFussionner.CheckBoxes = true;
			return result;
		}

		//-------------------------------------------------------------------------
		private void UpdateAspect()
		{
			m_btnDetail.Visible = m_btnStructureComplexe.Checked;
			m_btnSupprimer.Visible = m_btnStructureComplexe.Checked;
		}

		//-------------------------------------------------------------------------
		private void ReloadTree()
		{
			m_arbreTables.Nodes.Clear();
			if ( m_structureExport == null )
				return;
			ITableExport table = m_structureExport.Table;
			m_tableTableToNode = new Hashtable();
			TreeNode node = new TreeNode ( );
			m_arbreTables.Nodes.Add ( node );
			FillNodeForTable ( node, table );
			m_arbreTables.SelectedNode = node ;

			foreach ( C2iTableExportCalculee tableCalc in m_structureExport.TablesCalculees )
			{
				node = new TreeNode ( );
				FillNodeForTable ( node, tableCalc );
				m_arbreTables.Nodes.Add ( node );
			}
		}

		//-------------------------------------------------------------------------
		private void FillNodeForTable ( TreeNode node, ITableExport table )
		{
			node.Text = table.NomTable;
			node.Tag = new CDataArbre ( table );
			if ( table is C2iTableExport )
			{
				C2iTableExport tableExport = (C2iTableExport)table;
				if ( node.Nodes.Count == 0 )
				{
					foreach ( ITableExport sousTable in tableExport.TablesFilles )
					{
						TreeNode nodeFils = new TreeNode();
						node.Nodes.Add ( nodeFils );
						FillNodeForTable ( nodeFils, sousTable );
					}
				}
				if ( tableExport.ChampOrigine == null )
					node.ImageIndex = 2;
				else
					node.ImageIndex = tableExport.ChampOrigine.TypeDonnee.IsArrayOfTypeNatif?3:2;
				if ( tableExport.FiltreAAppliquer != null )
					node.ImageIndex = 4;
				node.SelectedImageIndex = node.ImageIndex;
				
			}
            if (table is C2iTableExportCalculee || table is C2iTableExportUnion)
			{
				node.ImageIndex = 5;
				node.SelectedImageIndex = node.ImageIndex;
			}
			if ( table is C2iTableExportCumulee )
			{
				node.ImageIndex = 6;
				node.SelectedImageIndex = node.ImageIndex;
			}
			m_tableTableToNode[table] = node;
		}

		//-------------------------------------------------------------------------
		private Type m_oldTypeInCombo;
		private void m_cmbType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!m_bComboTypeInitialized || !m_gestionnaireModeEdition.ModeEdition)
				return;
			if (m_cmbType.SelectedValue == null || m_cmbType.SelectedValue == typeof(DBNull) )
				return;
			if (m_oldTypeInCombo != (Type)m_cmbType.SelectedValue)
			{
				Type tp = (Type) m_cmbType.SelectedValue;
				if ( tp != m_structureExport.TypeSource )
				{
					m_tableEnEdition = null;
					AfficheInfosNoeud ( null );
					m_structureExport.TypeSource = (Type) m_cmbType.SelectedValue;
					m_structureExport.Table = new C2iTableExport();
					m_structureExport.Table.NomTable = DynamicClassAttribute.GetNomConvivial ( m_structureExport.TypeSource );
					ReloadTree(  );
				}
				m_oldTypeInCombo = (Type) m_cmbType.SelectedValue;
				
			}
		}

		//-------------------------------------------------------------------------
		private void AfficheInfosNoeud ( TreeNode node )
		{
			ValideTableEnCours();
			if ( node == null || !(node.Tag is CDataArbre ))
			{
				m_tableEnEdition = null;
				m_panelDetailTable.Visible = false;
				return;
			}
			CDataArbre data = (CDataArbre)node.Tag;
			if ( data.TableExport != null )
				AfficheInfosTableExport ( data.TableExport );
		}

		//-------------------------------------------------------------------------
		private void AfficheInfosTableExport ( ITableExport tableExport )
		{
			if ( m_tableEnEdition != null && 
				!m_tableEnEdition.Equals ( tableExport ) )
				ValideTableEnCours();

			if ( tableExport == null )
			{
				m_panelDetailTable.Visible = false;
				return;
			}
            m_txtNomTable.Enabled = m_btnStructureComplexe.Checked;

            // Table normale
			if ( tableExport is C2iTableExport )
			{
                m_panelTableExport.Visible = true;
                m_panelTableExport.Dock = DockStyle.Fill;
                m_panelTableauCroise.Visible = false;
				m_panelTableCalculee.Visible = false;
                m_panelTableFusion.Visible = false;
				C2iTableExport table = (C2iTableExport)tableExport;
				m_panelDetailTable.Visible = true;
				m_wndListeChamps.Items.Clear();
				m_wndListeChamps.LabelEdit = true;
				m_wndListeChamps.BeginUpdate();
				m_imageFiltre.Image = m_imagesFiltre.Images[table.FiltreAAppliquer == null?1:0];
				if (tableExport.ChampOrigine is CDefinitionProprieteDynamiqueFormule)
					m_imageFormuleTable.Visible = true;
				else
					m_imageFormuleTable.Visible = false;
				m_imageFiltre.Visible = m_imageFiltre.Visible && !m_imageFormuleTable.Visible;

				foreach ( C2iChampExport champ in table.Champs )
				{
					ListViewItem item = new ListViewItem();
					FillItemForChamp ( item, champ );
					m_wndListeChamps.Items.Add ( item );
				}
				m_wndListeChamps.EndUpdate();
				//Peut-on appliquer un filtre sur cette table ?
				TreeNode node = (TreeNode)m_tableTableToNode[table];
				m_imageFiltre.Visible = false;
				if ( m_btnStructureComplexe.Checked && table.ChampOrigine != null && table.ChampOrigine is CDefinitionProprieteDynamiqueDonneeCumulee )
					m_imageFiltre.Visible = true;
				if ( !m_imageFiltre.Visible && node != null && node.Parent != null )
				{
					Type typeParent = m_structureExport.TypeSource;
					if ( node.Parent.Tag is CDataArbre && 
						((CDataArbre)node.Parent.Tag).TableExport is C2iTableExport )
					{
						CDataArbre data = ((CDataArbre)node.Parent.Tag);
						C2iTableExport tableParente = (C2iTableExport)data.TableExport;
						if ( tableParente.ChampOrigine != null )
							typeParent = tableParente.ChampOrigine.TypeDonnee.TypeDotNetNatif;
					}
					string strProp = table.ChampOrigine.NomPropriete;
					string[] strProps = strProp.Split('.');
					PropertyInfo info = null;
					foreach ( string strSousProp in strProps )
					{
						info = typeParent.GetProperty ( strSousProp );
						if ( info != null )
							typeParent = info.PropertyType;
						else
							break;
					}
					if ( info != null )
						m_imageFiltre.Visible = typeof ( CListeObjetsDonnees ).IsAssignableFrom ( info.PropertyType );
				}
				if ( m_btnStructureSimple.Checked )
				{
					Type tp = m_structureExport.TypeSource;
					if ( table.ChampOrigine != null )
						tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
					CStructureTable structure = CStructureTable.GetStructure(tp);
					m_wndListeChamps.BeginUpdate();
					foreach( CInfoChampTable info in structure.Champs )
					{
						ListViewItem item = new ListViewItem  ( info.NomChamp );
						item.Tag = info;
						m_wndListeChamps.Items.Add ( item );
					}
					m_wndListeChamps.EndUpdate();
				}
			}

            // Table calculée indépendante
			if ( tableExport is C2iTableExportCalculee )
			{
                m_panelTableCalculee.Visible = true;
                m_panelTableCalculee.Dock = DockStyle.Fill;
				m_txtNomTable.Enabled = true;
				m_panelTableExport.Visible = false;
                m_panelTableFusion.Visible = false;
                m_imageFiltre.Visible = false;
				C2iTableExportCalculee tableCalc = (C2iTableExportCalculee)tableExport;
				if ( tableCalc.FormuleValeur != null )
					m_txtFormuleValeurAutonome.Text = tableCalc.FormuleValeur.GetString();
				else
					m_txtFormuleValeurAutonome.Text = "";
				if ( tableCalc.FormuleNbRecords != null )
					m_txtFormuleNbRecords.Text = tableCalc.FormuleNbRecords.GetString();
				else
					m_txtFormuleNbRecords.Text = "0";
				
			}

            // Table cumulée
			if ( tableExport is C2iTableExportCumulee )
			{
                m_panelDetailTable.Visible = true;
                m_panelTableExport.Visible = true;
                m_panelTableExport.Dock = DockStyle.Fill;
				m_panelTableCalculee.Visible = false;
                m_panelTableFusion.Visible = false;
                C2iTableExportCumulee table = (C2iTableExportCumulee)tableExport;
                m_wndListeChamps.Items.Clear();
				m_wndListeChamps.LabelEdit = true;
				m_wndListeChamps.BeginUpdate();
				m_imageFiltre.Image = m_imagesFiltre.Images[table.FiltreAAppliquer == null?1:0];
				foreach ( C2iChampDeRequete champ in table.Champs )
				{
					ListViewItem item = new ListViewItem();
					FillItemForChamp ( item, champ );
					m_wndListeChamps.Items.Add ( item );
				}
				m_wndListeChamps.EndUpdate();
				m_imageFiltre.Visible = true;
				m_panelTableauCroise.Visible = true;
				m_chkCroiser.Checked = table.TableauCroise != null;
                m_lnkTableauCroise.Enabled = m_chkCroiser.Checked && m_gestionnaireModeEdition.ModeEdition;
			}

            // Si c'est une Table Fusionnée
            if(tableExport is C2iTableExportUnion)
            {
                m_panelTableFusion.Visible = true;
                m_panelTableFusion.Dock = DockStyle.Fill;
                m_panelTableExport.Visible = false;
                m_panelTableCalculee.Visible = false;
                m_panelDetailTable.Visible = true;
                m_imageFiltre.Visible = false;

                // Initialiste la liste des tables fusionnables
                C2iTableExportUnion tableFusion =  (C2iTableExportUnion) tableExport;
                /*if(tableFusion.TableParente != null)
                {
                    // Construit la liste des tables fusionnables
                    ArrayList lstTablesFilles = new ArrayList();
                    foreach (ITableExport tabFille in tableFusion.TableParente.Tables)
                    {
                        if (tabFille.NomTable.ToUpper() != tableFusion.NomTable.ToUpper())
                            lstTablesFilles.Add(tabFille);
                    }
                    m_wndListeTablesAFussionner.ListeSource = lstTablesFilles;
                    // Initialise les items cochés
                    List<string> listeNomsTablesCheckes = new List<string>();                    
                    foreach (ITableExport tabCheck in tableFusion.TablesSource)
                    {
                        listeNomsTablesCheckes.Add(tabCheck.NomTable);
                    }
                    for (int i = 0; i < m_wndListeTablesAFussionner.Count; i++)
			        {
                        if(listeNomsTablesCheckes.Contains(((ITableExport)m_wndListeTablesAFussionner.ListeSource[i]).NomTable))
                            m_wndListeTablesAFussionner.CheckItem(i);

			        }

                }*/

            }
				
			m_txtNomTable.Text = tableExport.NomTable;
			m_tableEnEdition = tableExport;

		}

		//-------------------------------------------------------------------------
		private void ValideTableEnCours()
		{
			if ( m_tableEnEdition == null )
				return;
			if ( !m_gestionnaireModeEdition.ModeEdition )
				return;
			m_tableEnEdition.NomTable = m_txtNomTable.Text;

            // Table normale
			if ( m_tableEnEdition is C2iTableExport )
			{
				C2iTableExport tableExport = (C2iTableExport)m_tableEnEdition;
				tableExport.ListeChamps.Clear();
				foreach ( ListViewItem item in m_wndListeChamps.Items )
				{
					if ( item.Tag is C2iChampExport )
					{
						C2iChampExport champ = (C2iChampExport )item.Tag;
						tableExport.ListeChamps.Add ( champ );
					}
				}
			}

            // Table calculée indépendante
			if ( m_tableEnEdition is C2iTableExportCalculee )
			{
				C2iTableExportCalculee tableCalc = (C2iTableExportCalculee)m_tableEnEdition;
				CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression ( new CContexteAnalyse2iExpression ( m_wndAide.FournisseurProprietes, typeof(C2iTableExportCalculee.CTypeForFormule) ));
				CResultAErreur result = CResultAErreur.True;
				if ( m_txtFormuleValeurAutonome.Text.Trim() != "" )
				{
					result = analyseur.AnalyseChaine ( m_txtFormuleValeurAutonome.Text );
					if ( !result )
					{
						result.EmpileErreur("Formule de valeur incorrecte");
						CFormAlerte.Afficher ( result);
						return;
					}
					tableCalc.FormuleValeur = (C2iExpression)result.Data;
				}
				else
					tableCalc.FormuleValeur = null;
				result = analyseur.AnalyseChaine( m_txtFormuleNbRecords.Text);
				if ( !result )
				{
					result.EmpileErreur("Erreur dans la formule du nomre d'enregistrements");
					CFormAlerte.Afficher ( result);
					return;
				}
				tableCalc.FormuleNbRecords = (C2iExpression)result.Data;
			}

            // Table cumulée
			if ( m_tableEnEdition is C2iTableExportCumulee )
			{
				C2iTableExportCumulee tableExport = (C2iTableExportCumulee)m_tableEnEdition;
				tableExport.Requete.ListeChamps.Clear();
				foreach ( ListViewItem item in m_wndListeChamps.Items )
				{
					if ( item.Tag is C2iChampDeRequete )
					{
						C2iChampDeRequete champ = (C2iChampDeRequete )item.Tag;
						tableExport.Requete.ListeChamps.Add ( champ );
					}
				}
				if ( !m_chkCroiser.Checked )
					tableExport.TableauCroise = null;
			}

            // Table Fusionnée
            if (m_tableEnEdition is C2iTableExportUnion)
            {
                C2iTableExportUnion tableFusion = (C2iTableExportUnion)m_tableEnEdition;
                /*tableFusion.TablesSource = 
                    (ITableExport[]) m_wndListeTablesAFussionner.CheckedItems.ToArray(typeof(ITableExport));*/
            }

			TreeNode node = (TreeNode)m_tableTableToNode[m_tableEnEdition];
			if ( node != null )
				FillNodeForTable ( node, m_tableEnEdition );
		}

		//-------------------------------------------------------------------------
		private void FillItemForChamp ( ListViewItem item, C2iChampExport champ )
		{
			item.Text = champ.NomChamp;
			item.ImageIndex = (champ.Origine is C2iOrigineChampExportExpression)?1:0;
			item.Tag = champ;
		}

		//-------------------------------------------------------------------------
		private void FillItemForChamp ( ListViewItem item, C2iChampDeRequete champ )
		{
			item.Text = champ.NomChamp;
			item.ImageIndex = (champ.GroupBy?3:2);
			item.Tag = champ;
		}

		
		//-------------------------------------------------------------------------
		private class CMenuItemADefProp : MenuItem
		{
			public readonly CDefinitionProprieteDynamique DefinitionPropriete;

			public CMenuItemADefProp ( CDefinitionProprieteDynamique defProp )
			{
				DefinitionPropriete = defProp;
				Text = defProp.Nom;
			}
		}

		//-------------------------------------------------------------------------
		private void ShowMenuArbre( Point pt)
		{
			TreeNode node = m_arbreTables.SelectedNode;
			m_menuAjouterTable.Visible = node != null;
			m_menuSupprimerTable.Visible = node != null;
			m_menuAjouterTableCumulee.Visible = node != null;
            m_menuAddTableFusion.Visible = node != null;

			if ( node.Tag != null && node.Tag is CDataArbre )
			{
				if ( !(((CDataArbre)node.Tag).TableExport is C2iTableExport ))
				{
					m_menuAjouterTable.Visible = false;
					m_menuAjouterTableCumulee.Visible = false;
                    m_menuAddTableFusion.Visible = false;
				}

			}

			m_menuArbreTables.Show ( m_arbreTables, pt );
		}

		/*/// ////////////////////////////////////
		private void AddTable ( object sender, EventArgs args )
		{
			if ( sender is CMenuItemADefProp && m_tableEnEdition != null  )
			{
				TreeNode nodeParent = (TreeNode)m_tableTableToNode[m_tableEnEdition];
				if ( nodeParent == null || !(nodeParent.Tag is CDataArbre ))
					return;
				CDataArbre data = (CDataArbre)nodeParent.Tag;
				if ( data.TableExport is C2iTableExport )
				{
					CDefinitionProprieteDynamique defProp = ((CMenuItemADefProp)sender).DefinitionPropriete;
					C2iTableExport nouvelleTable = new C2iTableExport (defProp );
					if ( nouvelleTable.NomTable == "" )
						nouvelleTable.NomTable = defProp.Nom;
					TreeNode node = new TreeNode();
					m_arbreTables.SelectedNode.Nodes.Add ( node );
					FillNodeForTable ( node, nouvelleTable ) ;
					nodeParent.Expand();
					((C2iTableExport)data.TableExport).ListeTables.Add ( nouvelleTable );
				}

			}
		}*/

		

		private void AddTable ( CDefinitionProprieteDynamique defProp )
		{
			if ( m_arbreTables.SelectedNode == null || !(m_arbreTables.SelectedNode.Tag is CDataArbre ) )
				return;
			CDataArbre data = (CDataArbre)m_arbreTables.SelectedNode.Tag;
			if ( !(data.TableExport is C2iTableExport ))
				return;
			C2iTableExport tableParente = (C2iTableExport)data.TableExport;
			C2iTableExport nouvelleTable = new C2iTableExport (defProp );
			if ( nouvelleTable.NomTable == "" )
			{
				if ( m_btnStructureComplexe.Checked )
					nouvelleTable.NomTable = defProp.Nom;
				else
					nouvelleTable.NomTable = CContexteDonnee.GetNomTableForType ( defProp.TypeDonnee.TypeDotNetNatif );
			}
			TreeNode node = new TreeNode();
			m_arbreTables.SelectedNode.Nodes.Add ( node );
			FillNodeForTable ( node, nouvelleTable ) ;
			m_arbreTables.SelectedNode.Expand();
			tableParente.ListeTables.Add ( nouvelleTable );
		}

		//---------------------------------------------------------------------------------
        private void AddTableCumulee ( CDefinitionProprieteDynamique defProp )
		{
			if ( m_arbreTables.SelectedNode == null || !(m_arbreTables.SelectedNode.Tag is CDataArbre ) )
				return;
			CDataArbre data = (CDataArbre)m_arbreTables.SelectedNode.Tag;
			if ( !(data.TableExport is C2iTableExport ))
				return;
			C2iTableExport tableParente = (C2iTableExport)data.TableExport;
			C2iTableExportCumulee nouvelleTable = new C2iTableExportCumulee (defProp );
			if ( nouvelleTable.NomTable == "" )
				nouvelleTable.NomTable = defProp.Nom;
			TreeNode node = new TreeNode();
			m_arbreTables.SelectedNode.Nodes.Add ( node );
			FillNodeForTable ( node, nouvelleTable ) ;
			m_arbreTables.SelectedNode.Expand();
			tableParente.AddTableFille ( nouvelleTable );
		}

		/// ////////////////////////////////////
		private void CPanelEditionStructureDonneeOld_Load(object sender, System.EventArgs e)
		{
			InitChamps();
		}

		/// ////////////////////////////////////
		private void m_arbreTables_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			AfficheInfosNoeud ( m_arbreTables.SelectedNode );
		}


		//-------------------------------------------------------------------------
		private void AddChampStandard(object sender, EventArgs e)
		{
			if ( sender is CMenuItemADefProp )
			{
				C2iChampExport champ = new C2iChampExport();
				CDefinitionProprieteDynamique prop = ((CMenuItemADefProp)sender).DefinitionPropriete;
				champ.Origine = new C2iOrigineChampExportChamp ( prop );
				champ.NomChamp = prop.Nom;
				ListViewItem item = new ListViewItem();
				FillItemForChamp ( item, champ );
				m_wndListeChamps.Items.Add ( item );
			}
		}

		//-------------------------------------------------------------------------
		private void m_menuAjouterChampCalcule_Click(object sender, System.EventArgs e)
		{
			C2iChampExport champ = new C2iChampExport ( );
			champ.Origine = new C2iOrigineChampExportExpression();
			champ.NomChamp = "Champ "+m_wndListeChamps.Items.Count;
			if ( EditChamp ( champ ) )
			{
				ListViewItem item = new ListViewItem ( );
				FillItemForChamp ( item, champ );
				m_wndListeChamps.Items.Add ( item );
			}
		}

		//-------------------------------------------------------------------------
		private bool EditChamp ( C2iChampExport champ )
		{
			if ( champ.Origine is C2iOrigineChampExportExpression ||
				champ.Origine is C2iOrigineChampExportChampCustom)
			{
				Type tp = m_structureExport.TypeSource;
				if ( m_tableEnEdition is C2iTableExport )
				{
					C2iTableExport table = (C2iTableExport)m_tableEnEdition;
					if ( table.ChampOrigine != null )
						tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
					if ( champ.Origine is C2iOrigineChampExportExpression )
						return CFormEditChampCalcule.EditeChamp ( champ, tp, m_elementAVariablesPourFiltre!=null?(IFournisseurProprietesDynamiques)m_elementAVariablesPourFiltre:new CFournisseurPropDynStd(true) );
					if (champ.Origine is C2iOrigineChampExportChampCustom)
						return CFormEditOrigineChampCustom.EditeChamp(champ, tp);
				}
			}
			return false;
		}

		//-------------------------------------------------------------------------
		private bool EditChamp ( C2iChampDeRequete champ )
		{
			return CFormEditChampDeRequete.EditeChamp ( champ, m_tableEnEdition.ChampOrigine.TypeDonnee.TypeDotNetNatif, m_tableEnEdition.ChampOrigine );
		}

		//-------------------------------------------------------------------------
		private void m_btnAjouter_LinkClicked(object sender, System.EventArgs e)
		{
			if ( m_tableEnEdition is C2iTableExport )
			{
				/*if(  m_btnStructureSimple.Checked )
					m_menuAjouterChampCalcule_Click ( m_menuAjouterChampCalcule, new EventArgs() );
				else*/
					m_menuAjouterChamp.Show ( m_btnAjouter, new Point ( 0, m_btnAjouter.Height ) );
			}
			else
				OnAddChampCumule ( );
		}

		//-------------------------------------------------------------------------
		private void m_menuAjouterChampDonnee_Click(object sender, System.EventArgs e)
		{
			Type tp = m_structureExport.TypeSource;
			if ( m_tableEnEdition == null || !(m_tableEnEdition is C2iTableExport ) )
				return;
			C2iTableExport table = (C2iTableExport)m_tableEnEdition;
			if ( m_tableEnEdition != null && table.ChampOrigine != null)
				tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes ( tp, CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent, table.ChampOrigine);
			foreach ( CDefinitionProprieteDynamique def in defs )
			{
				C2iChampExport champ = new C2iChampExport();
				champ.Origine = new C2iOrigineChampExportChamp ( def );
				champ.NomChamp = def.Nom;
				ListViewItem item = new ListViewItem();
				FillItemForChamp ( item, champ );
				m_wndListeChamps.Items.Add ( item );
			}
		}

		//-------------------------------------------------------------------------
		private ListViewItem m_lastItemTooltip = null;
		private void m_wndListeChamps_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem item = m_wndListeChamps.GetItemAt ( e.X, e.Y );
			if ( item != null && item != m_lastItemTooltip )
			{
				if ( item.Tag is C2iChampExport )
				{
					C2iChampExport champ = (C2iChampExport)item.Tag;
					if ( champ.Origine is C2iOrigineChampExportChamp )
					{
						m_tooltip.SetToolTip ( m_wndListeChamps, ((C2iOrigineChampExportChamp)champ.Origine).ChampOrigine.NomPropriete );
						return;
					}
					if(  champ.Origine is C2iOrigineChampExportExpression )
					{
						C2iExpression formule = ((C2iOrigineChampExportExpression)champ.Origine).Expression;
						if ( formule != null )
						{
							m_tooltip.SetToolTip ( m_wndListeChamps, formule.GetString() );
							return;
						}
					}
				}
				else if ( item.Tag is CInfoChampTable )
				{
					m_tooltip.SetToolTip ( m_wndListeChamps, ((CInfoChampTable)item.Tag).NomConvivial );
					return;
				}
				else if ( item.Tag is C2iChampDeRequete )
				{
					C2iChampDeRequete champ = (C2iChampDeRequete)item.Tag;
					string strInfo = "Origin : "+champ.GetStringSql()+"\r\n"+
						"Operation : "+champ.OperationAgregation.ToString()+"\r\n"+
						"Group by : "+(champ.GroupBy?"Yes":"No");
					m_tooltip.SetToolTip ( m_wndListeChamps, strInfo );
					return;
				}
				m_lastItemTooltip = item;
			}
			m_tooltip.SetToolTip ( m_wndListeChamps, "" );
		}

		//-------------------------------------------------------------------------
		private void m_wndListeChamps_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			ListViewItem item = m_wndListeChamps.Items[e.Item];
            if (e.Label == null || item.Tag == null || !(item.Tag is C2iChampExport))
            {
                e.CancelEdit = true;
                return;
            }
			((C2iChampExport)item.Tag).NomChamp = e.Label;
		}

		private void m_btnOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Structure de données (*.2IStruct)|*.2iStruct|Tous fichier (*.*)|*.*";
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				if ( CFormAlerte.Afficher("Les données de la structure actuelle seront remplacée, continuer ?",
					EFormAlerteType.Question) == DialogResult.No )
					return;
				C2iStructureExport newStruct = new C2iStructureExport();
				CResultAErreur result = newStruct.ReadFromFile ( dlg.FileName );
				if ( !result )
					CFormAlerte.Afficher(result);
				else
				{
					m_structureExport = newStruct;
					InitChamps();
				}
			}
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog ();
			dlg.Filter = "Structure de données (*.2IStruct)|*.2iStruct|Tous fichier (*.*)|*.*";
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				MAJ_Champs();
				string strNomFichier = dlg.FileName;
				CResultAErreur result = StructureExport.SaveToFile ( strNomFichier );
				if ( !result )
					CFormAlerte.Afficher ( result);
				else
					CFormAlerte.Afficher ("Enregistrement réussi");
			}
		}

		//---------------------------------------------------------------------------
		private void m_btnDetail_LinkClicked(object sender, System.EventArgs e)
		{
			if ( m_wndListeChamps.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				if ( item.Tag is C2iChampExport )
				{
					if ( EditChamp ( (C2iChampExport)item.Tag ) )
						FillItemForChamp ( item, ((C2iChampExport)item.Tag ));
				}
				if ( item.Tag is C2iChampDeRequete )
				{
					if ( EditChamp ( (C2iChampDeRequete)item.Tag ) )
						FillItemForChamp ( item, ((C2iChampDeRequete)item.Tag ) );
				}
			}
		}

		//---------------------------------------------------------------------------
		private void m_btnSupprimer_LinkClicked(object sender, System.EventArgs e)
		{
			if ( m_wndListeChamps.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				if ( CFormAlerte.Afficher( "Supprimer le champ ?", 
					EFormAlerteType.Question ) == DialogResult.Yes )
					m_wndListeChamps.Items.Remove ( item );
			}
		}

		//---------------------------------------------------------------------------
		private void menuSupprimer_Click(object sender, EventArgs e)
		{
			if ( m_arbreTables.SelectedNode != null )
			{
				CDataArbre data = (CDataArbre)m_arbreTables.SelectedNode.Tag;
				if ( data != null  && data.TableExport != null )
				{
					if ( m_arbreTables.SelectedNode.Parent == null && data.TableExport is C2iTableExport)
					{
						CFormAlerte.Afficher("Impossible de supprimer la table principale", EFormAlerteType.Erreur);
						return;
					}
					if ( CFormAlerte.Afficher("Supprimer la table '"+data.TableExport.NomTable+" et ses dépendances ?",
						EFormAlerteType.Question ) == DialogResult.Yes )
					{
						if (data.TableExport is C2iTableExportCalculee)
						{
							m_arbreTables.Nodes.Remove(m_arbreTables.SelectedNode);
							m_structureExport.RemoveTableCalculee(((C2iTableExportCalculee)data.TableExport));
						}
						else
						{
							CDataArbre dataParent = (CDataArbre)m_arbreTables.SelectedNode.Parent.Tag;
							((C2iTableExport)dataParent.TableExport).RemoveTableFille(data.TableExport);

							if (m_arbreTables.SelectedNode.Parent != null)
								m_arbreTables.SelectedNode.Parent.Nodes.Remove(m_arbreTables.SelectedNode);
							else
								m_arbreTables.Nodes.Remove(m_arbreTables.SelectedNode);
						}
						
					}
				}
			}
		}
        
        //-----------------------------------------------------------------------------------
		private void m_menuTableIndep_Click(object sender, EventArgs e)
		{
			C2iTableExportCalculee newTable = new C2iTableExportCalculee();
			newTable.NomTable = I.T("Independant Table|234");
			m_structureExport.AddTableCalculee ( newTable );
			TreeNode node = new TreeNode ( );
			FillNodeForTable ( node, newTable );
			m_arbreTables.Nodes.Add ( node );
			m_arbreTables.SelectedNode = node;

		}

        //-----------------------------------------------------------------------------------
        private void m_btnTypeStructure_CheckedChanged(object sender, System.EventArgs e)
		{
            if (m_cmbType.SelectedValue == null || m_cmbType.SelectedValue == DBNull.Value)
                return;

            if (!m_bIsInitialising && sender is RadioButton && ((RadioButton)sender).Checked)
			{
				m_tableEnEdition = null;
				AfficheInfosNoeud ( null );
				C2iTableExport table = new C2iTableExport();
				table.NomTable = DynamicClassAttribute.GetNomConvivial ( m_structureExport.TypeSource );
				m_structureExport.IsStructureComplexe = m_btnStructureComplexe.Checked;
				m_structureExport.Table = table;
				InitChamps();
			}
		}

		private void FiltrerTable()
		{
			if ( m_tableEnEdition == null || 
				( !(m_tableEnEdition is C2iTableExport ) && 
				!(m_tableEnEdition is C2iTableExportCumulee) ) )
			{
				return;
			}
			ITableExport tableExport = (ITableExport)m_tableEnEdition;
			if ( tableExport.ChampOrigine == null )
				return;
			CFiltreDynamique filtre = ((ITableExport)m_tableEnEdition).FiltreAAppliquer;
			if ( filtre == null )
			{
				filtre = new CFiltreDynamique ( );
				filtre.TypeElements = tableExport.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			}
			filtre.ElementAVariablesExterne = m_elementAVariablesPourFiltre;
			if ( CFormEditFiltreDynamique.EditeFiltre (filtre, true, true, tableExport.ChampOrigine) )
			{
				tableExport.FiltreAAppliquer = filtre;
				AfficheInfosTableExport ( tableExport );
			}
		}

        //---------------------------------------------------------------------------------------------
		private void NePasFiltrer()
		{
			if ( m_tableEnEdition != null &&
			    ((m_tableEnEdition is C2iTableExport ||
                m_tableEnEdition is C2iTableExportCumulee)))
			{
				if ( m_tableEnEdition.FiltreAAppliquer != null )
				{
					if ( CFormAlerte.Afficher("Annuler le filtre ?", 
						EFormAlerteType.Question ) == DialogResult.Yes )
					{
						((C2iTableExport)m_tableEnEdition).FiltreAAppliquer = null;
						AfficheInfosTableExport(m_tableEnEdition);
					}
				}
			}
		}

        //---------------------------------------------------------------------------------------------
        private void m_wndListeChamps_BeforeLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			ListViewItem item = m_wndListeChamps.Items[e.Item];
			if ( item.Tag == null || !(item.Tag is C2iChampExport) )
				e.CancelEdit = true;
		}

		private void m_menuAjouterTable_Click(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbreTables.SelectedNode;
			if ( node == null || !(node.Tag is CDataArbre) )
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			if ( data.TableExport is C2iTableExport )
			{
				C2iTableExport table = (C2iTableExport)data.TableExport;
				Type tp = m_structureExport.TypeSource;
				if ( table.ChampOrigine != null )
					tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes (
					tp,
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableParente,
					table.ChampOrigine);
				foreach ( CDefinitionProprieteDynamique def in defs )
				{
					AddTable ( def );
				}
			}
		}

		private void m_imageFiltre_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( (e.Button & (MouseButtons.Left ))>0 )
				FiltrerTable();
			else if ( (e.Button & (MouseButtons.Right ))>0)
			{
				NePasFiltrer();
			}
		}

		private void m_menuArbreTables_Popup(object sender, System.EventArgs e)
		{
		}

        //-------------------------------------------------------------------------------
        private void m_menuAddTableCumulee_Click(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbreTables.SelectedNode;
			if ( node == null || !(node.Tag is CDataArbre) )
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			if ( data.TableExport is C2iTableExport )
			{
				C2iTableExport table = (C2iTableExport)data.TableExport;
				Type tp = m_structureExport.TypeSource;
				if ( table.ChampOrigine != null )
					tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes (
					tp,
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableParente |
					CFormSelectChampPourStructure.TypeSelectionAttendue.UniquementElementDeBaseDeDonnees,
					table.ChampOrigine );
				foreach ( CDefinitionProprieteDynamique def in defs )
				{
					AddTableCumulee ( def );
				}
			}
		}

		//-------------------------------------------------------------------------
		private void OnAddChampCumule()
		{
			if ( !(m_tableEnEdition is C2iTableExportCumulee ) )
				return;

			Type tp = m_structureExport.TypeSource;
			C2iTableExportCumulee table = (C2iTableExportCumulee)m_tableEnEdition;
			if ( m_tableEnEdition != null && table.ChampOrigine != null)
				tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes
				( 
				tp, 
				CFormSelectChampPourStructure.TypeSelectionAttendue.ChampParent | 
				CFormSelectChampPourStructure.TypeSelectionAttendue.ChampFille | 
				CFormSelectChampPourStructure.TypeSelectionAttendue.UniquementElementDeBaseDeDonnees |
                CFormSelectChampPourStructure.TypeSelectionAttendue.InclureChampsCustom,
				table.ChampOrigine);

			C2iChampDeRequete champUnique = null;
			foreach ( CDefinitionProprieteDynamique def in defs )
			{
				C2iChampDeRequete champ = new C2iChampDeRequete();
				champ.NomChamp = def.Nom;
				champ.TypeDonneeAvantAgregation = def.TypeDonnee.TypeDotNetNatif;
				CSourceDeChampDeRequete source = new CSourceDeChampDeRequete ( def.NomChampCompatibleCComposantFiltreChamp );
				champ.Sources = new CSourceDeChampDeRequete[]{source};
				bool bContinue = true;
				if ( champUnique != null )
					bContinue = EditChamp ( champ );
				if ( bContinue )
				{
					ListViewItem item = new ListViewItem();
					FillItemForChamp ( item, champ );
					m_wndListeChamps.Items.Add ( item );
				}
				
			}
			
		}

        //-------------------------------------------------------------------------------
        private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_txtFormuleAutonome != null)
				m_txtFormuleAutonome = m_txtFormuleValeurAutonome;
			m_wndAide.InsereInTextBox ( m_txtFormuleAutonome, nPosCurseur, strCommande );
		}

        //-------------------------------------------------------------------------------
        private void m_chkCroiser_CheckedChanged(object sender, System.EventArgs e)
		{
			m_lnkTableauCroise.Enabled = m_chkCroiser.Checked && m_gestionnaireModeEdition.ModeEdition;
		}

        //-------------------------------------------------------------------------------
        private void m_lnkTableauCroise_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( !(m_tableEnEdition is C2iTableExportCumulee) )
				return;
			ValideTableEnCours();
			C2iTableExportCumulee tableCumulee = (C2iTableExportCumulee)m_tableEnEdition;
			CTableauCroise tableau = tableCumulee.TableauCroise;
			if ( tableau == null )
			{
				tableau = new CTableauCroise();
			}
			DataTable table = new DataTable();
			tableCumulee.TableauCroise = null;
			m_tableEnEdition.InsertColonnesInTable ( table );
			tableCumulee.TableauCroise = tableau;
			CFormEditTableauCroise.EditeTableau ( tableau, table );
		}

        //-------------------------------------------------------------------------------
        private void OnEnterFormuleTableAutonome(object sender, System.EventArgs e)
		{
			if ( sender is sc2i.win32.expression.CControleEditeFormule)
			{
				if ( m_txtFormuleAutonome != null )
					m_txtFormuleAutonome.BackColor = Color.White;
				m_txtFormuleAutonome = (sc2i.win32.expression.CControleEditeFormule)sender;
				m_txtFormuleAutonome.BackColor = Color.LightGreen;
			}
		}

        //-------------------------------------------------------------------------------
        private void m_btnCopy_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CSerializerObjetInClipBoard.Copy ( StructureExport, C2iStructureExport.c_idFichier );
			if ( !result) 
			{
				CFormAlerte.Afficher ( result);
			}
		}

        //-------------------------------------------------------------------------------
        private void m_btnPaste_Click(object sender, System.EventArgs e)
		{
			I2iSerializable objet = null;
			CResultAErreur result = CSerializerObjetInClipBoard.Paste ( ref objet, C2iStructureExport.c_idFichier );
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return;
			}
			if ( CFormAlerte.Afficher("Les données de la structure actuelle seront remplacées, continuer ?",
				EFormAlerteType.Question) == DialogResult.No )
				return;
			m_structureExport = (C2iStructureExport)objet;
			InitChamps();
		}

		private void m_btnHaut_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeChamps.SelectedItems.Count == 1  )
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				int nIndex = item.Index;
				if (nIndex > 0 )
				{
					m_wndListeChamps.Items.RemoveAt ( nIndex );
					m_wndListeChamps.Items.Insert ( nIndex-1, item );
				}
			}
		}

		private void m_btnBas_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeChamps.SelectedItems.Count == 1 )
			{
				ListViewItem item = m_wndListeChamps.SelectedItems[0];
				int nIndex = item.Index;
				if ( nIndex < m_wndListeChamps.Items.Count-1 )
				{
					m_wndListeChamps.Items.RemoveAt ( nIndex );
					if ( nIndex+1 >= m_wndListeChamps.Items.Count-1 )
						m_wndListeChamps.Items.Add ( item );
					else
						m_wndListeChamps.Items.Insert ( nIndex+1, item );
				}
			}

		}

		//--------------------------------------------------------------------------
		public bool ComboTypeLockEdition
		{
			get
			{
				return m_cmbType.LockEdition;
			}
			set
			{
				m_cmbType.LockEdition = value;
			}
		}

		//-------------------------------------------------------------------------
		[DefaultValue(true)]
		public bool ComboTypeVisible
		{
			get
			{
				return m_panelTypeDonnee.Visible;
			}
			set
			{
				if (value==ComboTypeVisible)
					return;

				m_panelTypeDonnee.Visible = ComboTypeVisible;
			}
		}
		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

		private void m_menuTableAFormule_Click(object sender, EventArgs e)
		{
			TreeNode node = m_arbreTables.SelectedNode;
			if ( node == null || !(node.Tag is CDataArbre) )
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			if (data.TableExport is C2iTableExport)
			{
				C2iTableExport table = (C2iTableExport)data.TableExport;
				Type tp = m_structureExport.TypeSource;
				if (table.ChampOrigine != null)
					tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				C2iExpression formule = sc2i.win32.expression.CFormStdEditeFormule.EditeFormule("", new CFournisseurPropDynStd(true), tp);
				if (formule != null)
				{
					CDefinitionProprieteDynamiqueFormule def = new CDefinitionProprieteDynamiqueFormule(formule);
					AddTable(def);
				}
			}
		}

		private void m_imageFormuleTable_Click(object sender, EventArgs e)
		{

		}

		private void m_imageFiltre_Click(object sender, EventArgs e)
		{
			if (m_tableEnEdition == null ||
				(!(m_tableEnEdition is C2iTableExport)))
			{
				return;
			}
			ITableExport tableExport = (ITableExport)m_tableEnEdition;
			TreeNode node = m_arbreTables.SelectedNode;
			if ( tableExport != node.Tag )
				return;
			if ( node.Parent == null )
				return;
			try
			{
				CDataArbre data = (CDataArbre)node.Parent.Tag;
				Type tpParent = data.TableExport.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				if (!(tableExport.ChampOrigine is CDefinitionProprieteDynamiqueFormule))
					return;
				C2iExpression formule = ((CDefinitionProprieteDynamiqueFormule)tableExport.ChampOrigine).Formule;
				C2iExpression exTmp = CFormStdEditeFormule.EditeFormule(formule.GetString(), new CFournisseurPropDynStd(true), tpParent);
				if (exTmp != null)
				{
					if (!exTmp.TypeDonnee.Equals(formule.TypeDonnee))
					{
						CFormAlerte.Afficher("Impossible de modifier le type de valeur retournée par la formule", EFormAlerteType.Erreur);
						return;
					}
					((C2iTableExport)tableExport).ChampOrigine = new CDefinitionProprieteDynamiqueFormule(exTmp);
					AfficheInfosTableExport(tableExport);
				}
			}
			catch
			{ }
		}

		private void m_menuAjouterChamp_Popup(object sender, EventArgs e)
		{
			if (m_btnStructureComplexe.Checked)
				m_menuAjouterChampsPersonnalisés.Visible = false;
			else
			{
				m_menuAjouterChampsPersonnalisés.Visible = true;
				m_menuAjouterChampDonnee.Visible = false;
			}
		}

		private void m_menuAjouterChampDonnee_Popup(object sender, EventArgs e)
		{
			
		}

		private void m_menuAjouterChampsPersonnalisés_Click(object sender, EventArgs e)
		{
			C2iChampExport champ = new C2iChampExport();
			champ.Origine = new C2iOrigineChampExportChampCustom();
			champ.NomChamp = "CUSTOM_FIELDS";
			if (EditChamp(champ))
			{
				ListViewItem item = new ListViewItem();
				FillItemForChamp(item, champ);
				m_wndListeChamps.Items.Add(item);
			}
		}

		private void m_wndListeChamps_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_btnStructureSimple.Checked)
			{
				bool bCanEdit = false;
				if (m_wndListeChamps.SelectedItems.Count == 1)
				{
					ListViewItem item = m_wndListeChamps.SelectedItems[0];
					if (item != null)
					{

						if (item.Tag is C2iChampExport &&
							((C2iChampExport)item.Tag).Origine is C2iOrigineChampExportChampCustom)
							bCanEdit = true;
					}
				}
				m_btnDetail.Visible = bCanEdit;
				m_btnSupprimer.Visible = bCanEdit;
			}
		}

        //-------------------------------------------------------------------------------
        private void m_menuAddTableFusion_Click(object sender, EventArgs e)
        {
            // Ajoute une table de Fusion. Permet de fusionner plusieurs champs de tables différentes
            TreeNode node = m_arbreTables.SelectedNode;
            if (node == null || !(node.Tag is CDataArbre))
                return;
            CDataArbre data = (CDataArbre)node.Tag;
            C2iTableExport tableParente = data.TableExport as C2iTableExport;
            if (tableParente != null)
            {
                // Créé une nouvelle table sout la table parente
                C2iTableExportUnion nouvelleTableFusion = new C2iTableExportUnion();
                nouvelleTableFusion.NomTable = I.T("Merge Table|235");
                // Ajoute la nouvelle table à l'arbre des tables
                TreeNode newNode = new TreeNode();
                m_arbreTables.SelectedNode.Nodes.Add(newNode);
                FillNodeForTable(newNode, nouvelleTableFusion);
                m_arbreTables.SelectedNode = newNode;
                tableParente.AddTableFille(nouvelleTableFusion);
            }

        }

	}
}
