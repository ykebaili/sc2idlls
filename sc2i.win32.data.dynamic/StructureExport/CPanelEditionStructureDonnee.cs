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
	/// Description résumée de CPanelEditionStructureDonnee.
	/// </summary>
	public class CPanelEditionStructureDonnee : System.Windows.Forms.UserControl, IControlALockEdition
	{
		private CObjetPourSousProprietes m_objetAdditionnelPourSousProprietes = null;

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

		/// <summary>
		/// Pour chaque type de table, stocke le type d'éditeur associé
		/// </summary>
		private static Dictionary<Type, Type> m_tableTypesEditeursParTypeTable = null;

		/// <summary>
		/// Pour chaque type de table, stocke une instance d'un éditeur.
		/// </summary>
		private Dictionary<Type, IPanelEditTableExport> m_tableEditeursParType = new Dictionary<Type, IPanelEditTableExport>();



		private IElementAVariablesDynamiquesAvecContexteDonnee m_elementAVariablesPourFiltre = null;


		private Hashtable m_tableTableToNode = new Hashtable();

		private C2iStructureExport m_structureExport = new C2iStructureExport();
		private MenuItem m_menuAjouter;
		private MenuItem m_menuAddTableauCroise;
		private MenuItem m_menuAddVue;
		private ContextMenuStrip m_menuArbreTablesSimple;
		private ToolStripMenuItem m_menuSimpleAddTable;
		private ToolStripMenuItem m_menuSimpleFromFormule;
		private ToolStripMenuItem m_menuSimpleTableCumulee;
		private ToolStripMenuItem m_menuSimpleTableCalculee;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem m_menuSimpleSupprimer;
		private MenuItem menuItem1;
		private MenuItem m_menuSupprimerTable;
		private MenuItem m_menuInserer;
		private MenuItem m_menuInsererUnion;
		private MenuItem m_menuInsererTableauCroise;
		private MenuItem m_menuInsererVue;
        private RadioButton m_btnTableCumulee;

		private IPanelEditTableExport m_panelEditionEnCours = null;

		//-------------------------------------------
		public CPanelEditionStructureDonnee()
		{
			InitializeComponent();
			if ( !DesignMode )
				InitTypesEditeurs();
		}

		//-------------------------------------------
		/// <summary>
		/// Recherche les éditeurs connus
		/// ATTENTION : se limite a sc2i.win32.data.dynamic
		/// </summary>
		private static void InitTypesEditeurs()
		{
			if (m_tableTypesEditeursParTypeTable == null)
			{
				m_tableTypesEditeursParTypeTable = new Dictionary<Type, Type>();
				foreach (Type tp in typeof(CPanelEditionStructureDonnee).Assembly.GetTypes())
				{
					if (typeof(IPanelEditTableExport).IsAssignableFrom(tp))
					{
						object[] attributes = tp.GetCustomAttributes(typeof(EditeurTableExportAttribute), true);
						if (attributes.Length != 0)
						{
							EditeurTableExportAttribute attr = attributes[0] as EditeurTableExportAttribute;
							if (attr != null)
							{
								m_tableTypesEditeursParTypeTable[attr.TypeEdite] = tp;
							}
						}
					}
				}
			}
		}



		#region Code généré par le Concepteur de composants

		private System.Windows.Forms.Panel m_panelStructure;
		private System.Windows.Forms.TreeView m_arbreTables;
		private System.Windows.Forms.Panel m_panelListeTables;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel m_panelDetailTable;
		private System.Windows.Forms.ContextMenu m_menuArbreTables;
		private sc2i.win32.common.C2iComboBox m_cmbType;
		private System.Windows.Forms.RadioButton m_btnStructureSimple;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton m_btnStructureComplexe;
		private System.Windows.Forms.Panel m_panelTypeDonnee;
		private System.Windows.Forms.Panel m_panelTypeStructure;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.ImageList m_imagesChamps;
		private System.Windows.Forms.ContextMenu m_menuAjouterChamp;
		private System.Windows.Forms.MenuItem m_menuAjouterChampDonnee;
		private System.Windows.Forms.MenuItem m_menuAjouterChampCalcule;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.Windows.Forms.ImageList m_imagesFiltre;
		private System.Windows.Forms.MenuItem m_menuAjouterTableCalculee;
		private System.Windows.Forms.MenuItem m_menuAjouterTable;
		private System.Windows.Forms.ImageList m_imagesArbre;
		private System.Windows.Forms.MenuItem m_menuAjouterTableCumulee;
		private System.Windows.Forms.Button m_btnOpen;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnPaste;
		private System.Windows.Forms.Button m_btnCopy;
		private MenuItem m_menuTableAFormule;
		private MenuItem menuItem2;
		private MenuItem m_menuAjouterChampsPersonnalisés;
		private MenuItem m_menuAddTableFusion;
		private SplitContainer m_splitContainer1;
		private System.ComponentModel.IContainer components;

	
		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			CFournisseurGeneriqueProprietesDynamiques.DissocieObjetSupplementaire(null, m_objetAdditionnelPourSousProprietes);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditionStructureDonnee));
            this.m_panelStructure = new System.Windows.Forms.Panel();
            this.m_splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_panelListeTables = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_arbreTables = new System.Windows.Forms.TreeView();
            this.m_imagesArbre = new System.Windows.Forms.ImageList(this.components);
            this.m_panelDetailTable = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_imagesChamps = new System.Windows.Forms.ImageList(this.components);
            this.m_menuArbreTables = new System.Windows.Forms.ContextMenu();
            this.m_menuAjouter = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterTable = new System.Windows.Forms.MenuItem();
            this.m_menuTableAFormule = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterTableCumulee = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_menuAddTableFusion = new System.Windows.Forms.MenuItem();
            this.m_menuAddTableauCroise = new System.Windows.Forms.MenuItem();
            this.m_menuAddVue = new System.Windows.Forms.MenuItem();
            this.m_menuInserer = new System.Windows.Forms.MenuItem();
            this.m_menuInsererUnion = new System.Windows.Forms.MenuItem();
            this.m_menuInsererTableauCroise = new System.Windows.Forms.MenuItem();
            this.m_menuInsererVue = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterTableCalculee = new System.Windows.Forms.MenuItem();
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
            this.m_menuArbreTablesSimple = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuSimpleAddTable = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSimpleFromFormule = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSimpleTableCumulee = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSimpleTableCalculee = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuSimpleSupprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuAjouterChamp = new System.Windows.Forms.ContextMenu();
            this.m_menuAjouterChampDonnee = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterChampCalcule = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterChampsPersonnalisés = new System.Windows.Forms.MenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_imagesFiltre = new System.Windows.Forms.ImageList(this.components);
            this.m_btnTableCumulee = new System.Windows.Forms.RadioButton();
            this.m_panelStructure.SuspendLayout();
            this.m_splitContainer1.Panel1.SuspendLayout();
            this.m_splitContainer1.Panel2.SuspendLayout();
            this.m_splitContainer1.SuspendLayout();
            this.m_panelListeTables.SuspendLayout();
            this.m_panelTypeDonnee.SuspendLayout();
            this.m_panelTypeStructure.SuspendLayout();
            this.m_menuArbreTablesSimple.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(116, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tables|182";
            // 
            // m_arbreTables
            // 
            this.m_arbreTables.AllowDrop = true;
            this.m_arbreTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbreTables.CheckBoxes = true;
            this.m_arbreTables.ImageIndex = 0;
            this.m_arbreTables.ImageList = this.m_imagesArbre;
            this.m_arbreTables.Location = new System.Drawing.Point(0, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_arbreTables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbreTables.Name = "m_arbreTables";
            this.m_arbreTables.SelectedImageIndex = 0;
            this.m_arbreTables.Size = new System.Drawing.Size(198, 431);
            this.m_arbreTables.TabIndex = 0;
            this.m_arbreTables.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreTables_AfterCheck);
            this.m_arbreTables.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_arbreTables_MouseUp);
            this.m_arbreTables.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_arbreTables_DragDrop);
            this.m_arbreTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbreTables_AfterSelect);
            this.m_arbreTables.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_arbreTables_DragEnter);
            this.m_arbreTables.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_arbreTables_ItemDrag);
            this.m_arbreTables.DragOver += new System.Windows.Forms.DragEventHandler(this.m_arbreTables_DragOver);
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
            this.m_panelDetailTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDetailTable.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDetailTable, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDetailTable.Name = "m_panelDetailTable";
            this.m_panelDetailTable.Size = new System.Drawing.Size(497, 455);
            this.m_panelDetailTable.TabIndex = 3;
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
            // m_imagesChamps
            // 
            this.m_imagesChamps.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesChamps.ImageStream")));
            this.m_imagesChamps.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesChamps.Images.SetKeyName(0, "");
            this.m_imagesChamps.Images.SetKeyName(1, "");
            this.m_imagesChamps.Images.SetKeyName(2, "");
            this.m_imagesChamps.Images.SetKeyName(3, "");
            // 
            // m_menuArbreTables
            // 
            this.m_menuArbreTables.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouter,
            this.m_menuInserer,
            this.m_menuAjouterTableCalculee,
            this.menuItem2,
            this.m_menuSupprimerTable});
            this.m_menuArbreTables.Popup += new System.EventHandler(this.m_menuArbreTables_Popup);
            // 
            // m_menuAjouter
            // 
            this.m_menuAjouter.Index = 0;
            this.m_menuAjouter.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouterTable,
            this.m_menuTableAFormule,
            this.m_menuAjouterTableCumulee,
            this.menuItem1,
            this.m_menuAddTableFusion,
            this.m_menuAddTableauCroise,
            this.m_menuAddVue});
            this.m_menuAjouter.Text = "Add|10009";
            // 
            // m_menuAjouterTable
            // 
            this.m_menuAjouterTable.Index = 0;
            this.m_menuAjouterTable.Text = "From field|10010";
            this.m_menuAjouterTable.Click += new System.EventHandler(this.m_menuAjouterTable_Click);
            // 
            // m_menuTableAFormule
            // 
            this.m_menuTableAFormule.Index = 1;
            this.m_menuTableAFormule.Text = "From formula|10011";
            this.m_menuTableAFormule.Click += new System.EventHandler(this.m_menuTableAFormule_Click);
            // 
            // m_menuAjouterTableCumulee
            // 
            this.m_menuAjouterTableCumulee.Index = 2;
            this.m_menuAjouterTableCumulee.Text = "Cumulated table|10012";
            this.m_menuAjouterTableCumulee.Click += new System.EventHandler(this.m_menuAddTableCumulee_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 3;
            this.menuItem1.Text = "-";
            // 
            // m_menuAddTableFusion
            // 
            this.m_menuAddTableFusion.Index = 4;
            this.m_menuAddTableFusion.Text = "Union|10013";
            this.m_menuAddTableFusion.Click += new System.EventHandler(this.m_menuAddTableFusion_Click);
            // 
            // m_menuAddTableauCroise
            // 
            this.m_menuAddTableauCroise.Index = 5;
            this.m_menuAddTableauCroise.Text = "Cross Table|10014";
            this.m_menuAddTableauCroise.Click += new System.EventHandler(this.m_menuAddTableauCroise_Click);
            // 
            // m_menuAddVue
            // 
            this.m_menuAddVue.Index = 6;
            this.m_menuAddVue.Text = "View|10015";
            this.m_menuAddVue.Click += new System.EventHandler(this.m_menuAddVue_Click);
            // 
            // m_menuInserer
            // 
            this.m_menuInserer.Index = 1;
            this.m_menuInserer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuInsererUnion,
            this.m_menuInsererTableauCroise,
            this.m_menuInsererVue});
            this.m_menuInserer.Text = "Insert|10016";
            // 
            // m_menuInsererUnion
            // 
            this.m_menuInsererUnion.Index = 0;
            this.m_menuInsererUnion.Text = "Union|10013";
            this.m_menuInsererUnion.Click += new System.EventHandler(this.m_menuInsererUnion_Click);
            // 
            // m_menuInsererTableauCroise
            // 
            this.m_menuInsererTableauCroise.Index = 1;
            this.m_menuInsererTableauCroise.Text = "Cross Table|10014";
            this.m_menuInsererTableauCroise.Click += new System.EventHandler(this.m_menuInsererTableauCroise_Click);
            // 
            // m_menuInsererVue
            // 
            this.m_menuInsererVue.Index = 2;
            this.m_menuInsererVue.Text = "View|10015";
            this.m_menuInsererVue.Click += new System.EventHandler(this.m_menuInsererVue_Click);
            // 
            // m_menuAjouterTableCalculee
            // 
            this.m_menuAjouterTableCalculee.Index = 2;
            this.m_menuAjouterTableCalculee.Text = "Add Independant Calculated Table|10017";
            this.m_menuAjouterTableCalculee.Click += new System.EventHandler(this.m_menuTableIndep_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.Text = "-";
            // 
            // m_menuSupprimerTable
            // 
            this.m_menuSupprimerTable.Index = 4;
            this.m_menuSupprimerTable.Text = "Remove Table|10018";
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
            this.m_btnStructureSimple.Text = "Simple structure|184";
            this.m_btnStructureSimple.CheckedChanged += new System.EventHandler(this.m_btnTypeStructure_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Type|183";
            // 
            // m_btnStructureComplexe
            // 
            this.m_btnStructureComplexe.Location = new System.Drawing.Point(146, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnStructureComplexe, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnStructureComplexe.Name = "m_btnStructureComplexe";
            this.m_btnStructureComplexe.Size = new System.Drawing.Size(136, 16);
            this.m_btnStructureComplexe.TabIndex = 14;
            this.m_btnStructureComplexe.Text = "Complex structure|185";
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
            this.m_panelTypeStructure.Controls.Add(this.m_btnTableCumulee);
            this.m_panelTypeStructure.Controls.Add(this.m_btnStructureSimple);
            this.m_panelTypeStructure.Controls.Add(this.m_btnStructureComplexe);
            this.m_panelTypeStructure.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTypeStructure.Location = new System.Drawing.Point(0, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTypeStructure, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTypeStructure.Name = "m_panelTypeStructure";
            this.m_panelTypeStructure.Size = new System.Drawing.Size(704, 16);
            this.m_panelTypeStructure.TabIndex = 16;
            // 
            // m_menuArbreTablesSimple
            // 
            this.m_menuArbreTablesSimple.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuSimpleAddTable,
            this.m_menuSimpleFromFormule,
            this.m_menuSimpleTableCumulee,
            this.m_menuSimpleTableCalculee,
            this.toolStripMenuItem1,
            this.m_menuSimpleSupprimer});
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_menuArbreTablesSimple, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuArbreTablesSimple.Name = "m_menuArbreTablesSimple";
            this.m_menuArbreTablesSimple.Size = new System.Drawing.Size(228, 120);
            // 
            // m_menuSimpleAddTable
            // 
            this.m_menuSimpleAddTable.Name = "m_menuSimpleAddTable";
            this.m_menuSimpleAddTable.Size = new System.Drawing.Size(227, 22);
            this.m_menuSimpleAddTable.Text = "Add Table|10019";
            this.m_menuSimpleAddTable.Click += new System.EventHandler(this.m_menuAjouterTable_Click);
            // 
            // m_menuSimpleFromFormule
            // 
            this.m_menuSimpleFromFormule.Name = "m_menuSimpleFromFormule";
            this.m_menuSimpleFromFormule.Size = new System.Drawing.Size(227, 22);
            this.m_menuSimpleFromFormule.Text = "Add from a formula|10020";
            this.m_menuSimpleFromFormule.Click += new System.EventHandler(this.m_menuTableAFormule_Click);
            // 
            // m_menuSimpleTableCumulee
            // 
            this.m_menuSimpleTableCumulee.Name = "m_menuSimpleTableCumulee";
            this.m_menuSimpleTableCumulee.Size = new System.Drawing.Size(227, 22);
            this.m_menuSimpleTableCumulee.Text = "Add cumulated Table|10021";
            this.m_menuSimpleTableCumulee.Click += new System.EventHandler(this.m_menuAddTableCumulee_Click);
            // 
            // m_menuSimpleTableCalculee
            // 
            this.m_menuSimpleTableCalculee.Name = "m_menuSimpleTableCalculee";
            this.m_menuSimpleTableCalculee.Size = new System.Drawing.Size(227, 22);
            this.m_menuSimpleTableCalculee.Text = "Add calculated Table|10022";
            this.m_menuSimpleTableCalculee.Click += new System.EventHandler(this.m_menuTableIndep_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 6);
            // 
            // m_menuSimpleSupprimer
            // 
            this.m_menuSimpleSupprimer.Name = "m_menuSimpleSupprimer";
            this.m_menuSimpleSupprimer.Size = new System.Drawing.Size(227, 22);
            this.m_menuSimpleSupprimer.Text = "Remove|10023";
            this.m_menuSimpleSupprimer.Click += new System.EventHandler(this.menuSupprimer_Click);
            // 
            // m_menuAjouterChamp
            // 
            this.m_menuAjouterChamp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouterChampDonnee,
            this.m_menuAjouterChampCalcule,
            this.m_menuAjouterChampsPersonnalisés});
            // 
            // m_menuAjouterChampDonnee
            // 
            this.m_menuAjouterChampDonnee.Index = 0;
            this.m_menuAjouterChampDonnee.Text = "Data field|10024";
            // 
            // m_menuAjouterChampCalcule
            // 
            this.m_menuAjouterChampCalcule.Index = 1;
            this.m_menuAjouterChampCalcule.Text = "Calculated field|10025";
            // 
            // m_menuAjouterChampsPersonnalisés
            // 
            this.m_menuAjouterChampsPersonnalisés.Index = 2;
            this.m_menuAjouterChampsPersonnalisés.Text = "Custom fields|10026";
            // 
            // m_imagesFiltre
            // 
            this.m_imagesFiltre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesFiltre.ImageStream")));
            this.m_imagesFiltre.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesFiltre.Images.SetKeyName(0, "");
            this.m_imagesFiltre.Images.SetKeyName(1, "");
            // 
            // m_btnTableCumulee
            // 
            this.m_btnTableCumulee.Location = new System.Drawing.Point(305, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnTableCumulee, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnTableCumulee.Name = "m_btnTableCumulee";
            this.m_btnTableCumulee.Size = new System.Drawing.Size(136, 16);
            this.m_btnTableCumulee.TabIndex = 15;
            this.m_btnTableCumulee.Text = "Cumulated table|20009";
            this.m_btnTableCumulee.CheckedChanged += new System.EventHandler(this.m_btnTableCumulee_CheckedChanged);
            // 
            // CPanelEditionStructureDonnee
            // 
            this.Controls.Add(this.m_panelStructure);
            this.Controls.Add(this.m_panelTypeStructure);
            this.Controls.Add(this.m_panelTypeDonnee);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditionStructureDonnee";
            this.Size = new System.Drawing.Size(704, 497);
            this.Load += new System.EventHandler(this.CPanelEditionStructureDonnee_Load);
            this.m_panelStructure.ResumeLayout(false);
            this.m_splitContainer1.Panel1.ResumeLayout(false);
            this.m_splitContainer1.Panel2.ResumeLayout(false);
            this.m_splitContainer1.ResumeLayout(false);
            this.m_panelListeTables.ResumeLayout(false);
            this.m_panelTypeDonnee.ResumeLayout(false);
            this.m_panelTypeDonnee.PerformLayout();
            this.m_panelTypeStructure.ResumeLayout(false);
            this.m_menuArbreTablesSimple.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_arbreTables_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!LockEdition && (e.Button & MouseButtons.Right) == MouseButtons.Right )
				ShowMenuArbre ( new Point ( e.X, e.Y ) );
		}

		//-------------------------------------------------------------------------
        [Browsable(false)]
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
        [Browsable(false)]
        public IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesPourFiltre
		{
			get
			{
				return m_elementAVariablesPourFiltre;
			}
			set
			{
				m_elementAVariablesPourFiltre = value;
				AddProprietesToFournisseurPropDyn();
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
			int nNbClasses = infosClasses.Count;
			for (int nClasse = nNbClasses - 1; nClasse >= 0; nClasse--)
			{
				if (((CInfoClasseDynamique)infosClasses[nClasse]).Classe.IsAbstract)
					infosClasses.RemoveAt(nClasse);
			}
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
				m_structureExport.IsStructureComplexe = m_btnStructureComplexe.Checked || m_btnTableCumulee.Checked;
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

            m_btnStructureComplexe.Checked = m_structureExport.IsStructureComplexe && 
                (m_structureExport.Table is C2iTableExport || m_structureExport.Table is C2iTableExportTableauCroise ||
                m_structureExport.Table is C2iTableExportUnion || m_structureExport.Table is C2iTableExportVue );
            m_btnTableCumulee.Checked = m_structureExport.Table is C2iTableExportCumulee;
			m_btnStructureSimple.Checked = !m_structureExport.IsStructureComplexe;
			ReloadTree();

			if (m_structureExport.TypeSource != null)
			{
				m_cmbType.SelectedValue = m_structureExport.TypeSource;
			}
			else
				m_cmbType.SelectedValue = typeof(DBNull);

			AddProprietesToFournisseurPropDyn();

			bool bHasChamps = m_structureExport.Table.TablesFilles.Length > 0  ||
				m_structureExport.Table.Champs.Length > 0;

			m_btnStructureComplexe.Enabled = !bHasChamps && m_gestionnaireModeEdition.ModeEdition;
			m_btnStructureSimple.Enabled = !bHasChamps && m_gestionnaireModeEdition.ModeEdition;
            m_btnTableCumulee.Enabled = !bHasChamps && m_gestionnaireModeEdition.ModeEdition;

			m_bIsInitialising = false;

			return result;
		}

		//-------------------------------------------------------------------------
		private void AddProprietesToFournisseurPropDyn()
		{
			CFournisseurGeneriqueProprietesDynamiques.DissocieObjetSupplementaire(null, m_objetAdditionnelPourSousProprietes);
			m_objetAdditionnelPourSousProprietes = new CObjetPourSousProprietes(m_elementAVariablesPourFiltre);
			if (m_cmbType.SelectedValue is Type)
				CFournisseurGeneriqueProprietesDynamiques.AssocieObjetSupplementaire(null, m_objetAdditionnelPourSousProprietes);
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
		private int GetIndexImageForTable(ITableExport table)
		{
			int nIndex = 0;
			if (table is C2iTableExport)
			{
				if (table.ChampOrigine == null)
					nIndex = 2;
				else
					nIndex = table.ChampOrigine.TypeDonnee.IsArrayOfTypeNatif ? 3 : 2;
				if (((C2iTableExport)table).FiltreAAppliquer != null)
					nIndex = 4;
			}
			else if (table is C2iTableExportCumulee)
				nIndex = 6;
			else
				nIndex = 5;
			return nIndex;
		}

		//-------------------------------------------------------------------------
		private void FillNodeForTable ( TreeNode node, ITableExport table )
		{
			node.Text = table.NomTable;
			node.Tag = new CDataArbre ( table );
			node.ImageIndex = GetIndexImageForTable(table);
			node.SelectedImageIndex = node.ImageIndex;
			node.Checked = !table.NePasCalculer;
			node.BackColor = table.NePasCalculer ? Color.Red : m_arbreTables.BackColor;
			if ( node.Nodes.Count == 0 )
			{
				foreach ( ITableExport sousTable in table.TablesFilles )
				{
					TreeNode nodeFils = new TreeNode();
					node.Nodes.Add ( nodeFils );
					FillNodeForTable ( nodeFils, sousTable );
				}
			}
			m_tableTableToNode[table] = node;
		}

		//-------------------------------------------------------------------------
		private Type m_oldTypeInCombo;
		private void m_cmbType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!m_bComboTypeInitialized || !m_gestionnaireModeEdition.ModeEdition)
				return;
			if (m_oldTypeInCombo != (Type)m_cmbType.SelectedValue)
			{
				Type tp = (Type) m_cmbType.SelectedValue;
				if ( tp != m_structureExport.TypeSource )
				{
					if (tp == typeof(DBNull))
						tp = null;
					if (m_panelEditionEnCours != null)
						((Control)m_panelEditionEnCours).Visible = false;
					m_panelEditionEnCours = null;
					AfficheInfosNoeud ( null );
					m_structureExport.TypeSource = tp;
					m_structureExport.Table = new C2iTableExport();
					if (tp != null)
						m_structureExport.Table.NomTable = DynamicClassAttribute.GetNomConvivial(m_structureExport.TypeSource);
					else
						m_structureExport.Table.NomTable = "Table";
					ReloadTree(  );
				}
				m_oldTypeInCombo = (Type) m_cmbType.SelectedValue;
				
			}
			AddProprietesToFournisseurPropDyn();
		}

		//-------------------------------------------------------------------------
		private void AfficheInfosNoeud ( TreeNode node )
		{
			ValideTableEnCours();
			if ( node == null || !(node.Tag is CDataArbre ))
			{
				if (m_panelEditionEnCours != null)
					((Control)m_panelEditionEnCours).Visible = false;
				m_panelEditionEnCours = null;
				m_panelDetailTable.Visible = false;
				return;
			}
			CDataArbre data = (CDataArbre)node.Tag;
			if ( data.TableExport != null )
				AfficheInfosTableExport ( data.TableExport );
		}

		//-------------------------------------------------------------------------
		private IPanelEditTableExport GetPanelForTable(ITableExport table)
		{
			if (table == null)
				return null;
			IPanelEditTableExport panel = null;
			if (m_tableEditeursParType.TryGetValue(table.GetType(), out panel))
				return panel;
			Type tpEditeur = null;
			if (m_tableTypesEditeursParTypeTable.TryGetValue(table.GetType(), out tpEditeur))
			{
				panel = (IPanelEditTableExport)Activator.CreateInstance(tpEditeur);
				((Control)panel).Parent = m_panelDetailTable;
				((Control)panel).Dock = DockStyle.Fill;
				m_tableEditeursParType[table.GetType()] = panel;
			}
			return panel;
		}

		//-------------------------------------------------------------------------
		private void AfficheInfosTableExport ( ITableExport tableExport )
		{
			if ( m_panelEditionEnCours != null && 
				!m_panelEditionEnCours.TableEditee.Equals ( tableExport ) )
				ValideTableEnCours();

			if ( tableExport == null )
			{
				m_panelDetailTable.Visible = false;
				return;
			}

			if ( m_panelEditionEnCours != null )
				((Control)m_panelEditionEnCours).Visible = false;
			IPanelEditTableExport panel = GetPanelForTable(tableExport);
			if ( panel != null )
			{
				m_panelDetailTable.Visible = true;
				((Control)panel).Visible=  true;
				TreeNode node = (TreeNode)m_tableTableToNode[tableExport];
				ITableExport tableParente = null;
				if ( node != null && node.Parent != null && node.Parent.Tag is CDataArbre )
					tableParente = ((CDataArbre)node.Parent.Tag).TableExport;
				panel.InitChamps ( tableExport, tableParente, m_structureExport, m_elementAVariablesPourFiltre);
			}
			m_panelEditionEnCours = panel;
			if ( m_panelEditionEnCours != null )
				m_panelEditionEnCours.LockEdition = !m_gestionnaireModeEdition.ModeEdition;

		}

		//-------------------------------------------------------------------------
		private void ValideTableEnCours()
		{
			if ( m_panelEditionEnCours == null )
				return;
			if ( !m_gestionnaireModeEdition.ModeEdition )
				return;
			CResultAErreur result = m_panelEditionEnCours.MajChamps();
			

			TreeNode node = (TreeNode)m_tableTableToNode[m_panelEditionEnCours.TableEditee];
			if ( node != null )
				FillNodeForTable(node, m_panelEditionEnCours.TableEditee);
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
			m_menuSimpleAddTable.Visible = node != null;
			m_menuSimpleSupprimer.Visible = node != null;
			m_menuSimpleTableCumulee.Visible = node != null;
			m_menuSimpleFromFormule.Visible = node != null;
			m_menuSimpleTableCalculee.Visible = node == null;

			//m_menuArbreTablesSimple.Show(m_arbreTables, pt);
			if ( node != null && node.Tag != null && node.Tag is CDataArbre )
			{
				m_menuAjouter.Visible = ((CDataArbre)node.Tag).TableExport.AcceptChilds();
				m_menuInserer.Visible = true;
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
			if ( !data.TableExport.AcceptChilds())
				return;
			ITableExport tableParente = data.TableExport;
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
			tableParente.AddTableFille( nouvelleTable );
		}

		//---------------------------------------------------------------------------------
        private void AddTableCumulee ( CDefinitionProprieteDynamique defProp )
		{
			if ( m_arbreTables.SelectedNode == null || !(m_arbreTables.SelectedNode.Tag is CDataArbre ) )
				return;
			CDataArbre data = (CDataArbre)m_arbreTables.SelectedNode.Tag;
			if ( !data.TableExport.AcceptChilds())
				return;
			ITableExport tableParente = data.TableExport;
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
		private void CPanelEditionStructureDonnee_Load(object sender, System.EventArgs e)
		{
			InitChamps();
		}

		/// ////////////////////////////////////
		private void m_arbreTables_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			AfficheInfosNoeud ( m_arbreTables.SelectedNode );
		}


		

		private void m_btnOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Data structure (*.2IStruct)|*.2iStruct|All files (*.*)|*.*|30051");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				if ( CFormAlerte.Afficher(I.T("Current structure data will be replaced.  Continue?|259"),
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
            dlg.Filter = I.T("Data structure (*.2IStruct)|*.2iStruct|All files (*.*)|*.*|30051");
		    if ( dlg.ShowDialog()==DialogResult.OK )
			{
				MAJ_Champs();
				string strNomFichier = dlg.FileName;
				CResultAErreur result = StructureExport.SaveToFile ( strNomFichier );
				if ( !result )
					CFormAlerte.Afficher ( result);
				else
					CFormAlerte.Afficher (I.T("Save successful|260"));
			}
		}

        //-----------------------------------------------------------------------------------
        private void m_btnTypeStructure_CheckedChanged(object sender, System.EventArgs e)
		{
            if (!m_bIsInitialising && sender is RadioButton && ((RadioButton)sender).Checked)
			{
				if (m_panelEditionEnCours != null)
					((Control)m_panelEditionEnCours).Visible = false;
				m_panelEditionEnCours = null;
				AfficheInfosNoeud ( null );
				C2iTableExport table = new C2iTableExport();
				if (m_structureExport.TypeSource != null)
					table.NomTable = DynamicClassAttribute.GetNomConvivial(m_structureExport.TypeSource);
				else
					table.NomTable = "Table";
				m_structureExport.IsStructureComplexe = m_btnStructureComplexe.Checked;
				m_structureExport.Table = table;
				InitChamps();
			}
		}

        private void m_btnTableCumulee_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bIsInitialising && sender is RadioButton && ((RadioButton)sender).Checked)
            {
                if (m_panelEditionEnCours != null)
                    ((Control)m_panelEditionEnCours).Visible = false;
                m_panelEditionEnCours = null;
                AfficheInfosNoeud(null);
                C2iTableExportCumulee table = new C2iTableExportCumulee();
                if (m_structureExport.TypeSource != null)
                    table.NomTable = DynamicClassAttribute.GetNomConvivial(m_structureExport.TypeSource);
                else
                    table.NomTable = "Table";
                m_structureExport.IsStructureComplexe = true;
                m_structureExport.Table = table;
                InitChamps();
            }
        }

		

		private void m_menuAjouterTable_Click(object sender, System.EventArgs e)
		{
			TreeNode node = m_arbreTables.SelectedNode;
			if ( node == null || !(node.Tag is CDataArbre) )
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			if ( data.TableExport.AcceptChilds() )
			{
				ITableExport table = data.TableExport;
				Type tp = m_structureExport.TypeSource;
				if ( table.ChampOrigine != null )
					tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				CFormSelectChampPourStructure.TypeSelectionAttendue tpSelection =
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableParente |
					CFormSelectChampPourStructure.TypeSelectionAttendue.AllowThis;
				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes (
					tp,
					tpSelection,
					table.ChampOrigine);
				foreach ( CDefinitionProprieteDynamique def in defs )
				{
					AddTable ( def );
				}
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
			if ( data.TableExport.AcceptChilds() )
			{
				ITableExport table = data.TableExport;
				Type tp = m_structureExport.TypeSource;
				if ( table.ChampOrigine != null )
					tp = table.ChampOrigine.TypeDonnee.TypeDotNetNatif;
				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes (
					tp,
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableParente |
					CFormSelectChampPourStructure.TypeSelectionAttendue.UniquementElementDeBaseDeDonnees |
					CFormSelectChampPourStructure.TypeSelectionAttendue.AllowThis,
					table.ChampOrigine );
				foreach ( CDefinitionProprieteDynamique def in defs )
				{
					AddTableCumulee ( def );
				}
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
            if (CFormAlerte.Afficher(I.T("Current structure data will be replaced.  Continue?|259"),
				EFormAlerteType.Question) == DialogResult.No )
				return;
			m_structureExport = (C2iStructureExport)objet;
			InitChamps();
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
				C2iExpression formule = sc2i.win32.expression.CFormStdEditeFormule.EditeFormule("", ElementAVariablesPourFiltre, tp);
				if (formule != null)
				{
					CDefinitionProprieteDynamiqueFormule def = new CDefinitionProprieteDynamiqueFormule(formule);
					AddTable(def);
				}
			}
		}

		//-----------------------------------------------------------------------------------
		private void m_menuTableIndep_Click(object sender, EventArgs e)
		{
			C2iTableExportCalculee newTable = new C2iTableExportCalculee();
			newTable.NomTable = I.T("Independant Table|234");
			m_structureExport.AddTableCalculee(newTable);
			TreeNode node = new TreeNode();
			FillNodeForTable(node, newTable);
			m_arbreTables.Nodes.Add(node);
			m_arbreTables.SelectedNode = node;

		}

		//-------------------------------------------------------------------------------
		private void m_menuAddTableFusion_Click(object sender, EventArgs e)
		{
			// Ajoute une table de Fusion. Permet de fusionner plusieurs champs de tables différentes
			TreeNode node = m_arbreTables.SelectedNode;
			if (node == null || !(node.Tag is CDataArbre))
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			C2iTableExportATableFille tableParente = data.TableExport as C2iTableExportATableFille;
			if (tableParente != null)
			{
				Type tpParent = m_structureExport.TypeSource;
				if (tableParente.ChampOrigine != null)
					tpParent = tableParente.ChampOrigine.TypeDonnee.TypeDotNetNatif;

				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes(
					tpParent,
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.AllowThis|
					CFormSelectChampPourStructure.TypeSelectionAttendue.MonoSelection,
					tableParente.ChampOrigine);
				foreach (CDefinitionProprieteDynamique def in defs)
				{
					// Créé une nouvelle table sout la table parente
					C2iTableExportUnion nouvelleTableFusion = new C2iTableExportUnion(def);
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

		//---------------------------------------------------------------------------
		private void menuSupprimer_Click(object sender, EventArgs e)
		{
			if (m_arbreTables.SelectedNode != null)
			{
				CDataArbre data = (CDataArbre)m_arbreTables.SelectedNode.Tag;
				if (data != null && data.TableExport != null)
				{
					if (m_arbreTables.SelectedNode.Parent == null && !(data.TableExport is C2iTableExportCalculee) )
					{
						//Table principale
						//s'il n'y a qu'une seule table fille avec comme
						//Origine : this, alors, on définit la fille comme principale
						C2iTableExportATableFille tableToDelete = data.TableExport as C2iTableExportATableFille;
						if (tableToDelete != null)
						{
							if (tableToDelete.TablesFilles.Length == 1 &&
								tableToDelete.TablesFilles[0].ChampOrigine is CDefinitionProprieteDynamiqueThis)
							{
								TreeNode node = m_arbreTables.SelectedNode.Nodes[0];
								C2iTableExportATableFille tableFille = ((CDataArbre)node.Tag).TableExport as C2iTableExportATableFille;
								if (tableFille != null)
								{							//On peut remonter
									m_arbreTables.Nodes.Remove(m_arbreTables.SelectedNode);
									m_arbreTables.Nodes.Add(node);
									m_structureExport.Table = tableFille;
									tableFille.ChampOrigine = null;
									return;
								}
							}
						}
						CFormAlerte.Afficher(I.T("Cannot remove the main table|261"), EFormAlerteType.Erreur);
						return;
					}
					if (CFormAlerte.Afficher(I.T("Remove the table @1  and its dependancies ?|30055",data.TableExport.NomTable),
						EFormAlerteType.Question) == DialogResult.Yes)
					{
						if (data.TableExport is C2iTableExportCalculee)
						{
							m_arbreTables.Nodes.Remove(m_arbreTables.SelectedNode);
							m_structureExport.RemoveTableCalculee(((C2iTableExportCalculee)data.TableExport));
						}
						else
						{
							CDataArbre dataParent = (CDataArbre)m_arbreTables.SelectedNode.Parent.Tag;
							dataParent.TableExport.RemoveTableFille(data.TableExport);

							if (m_arbreTables.SelectedNode.Parent != null)
								m_arbreTables.SelectedNode.Parent.Nodes.Remove(m_arbreTables.SelectedNode);
							else
								m_arbreTables.Nodes.Remove(m_arbreTables.SelectedNode);
						}

					}
				}
			}
		}

		//-------------------------------------------------------------------
		private void m_arbreTables_DragDrop(object sender, DragEventArgs e)
		{
			TreeNode node = e.Data.GetData(typeof(TreeNode)) as TreeNode;
			e.Effect = DragDropEffects.None;
			if (node != null)
			{
				TreeViewHitTestInfo info = m_arbreTables.HitTest(m_arbreTables.PointToClient(new Point(e.X, e.Y)));
				if (info != null && info.Node != null)
				{
					if (CanMove(node, info.Node) && node.Parent.Tag != null)
					{
						TreeNode newParent = info.Node;
						ITableExport newTableParente = ((CDataArbre)newParent.Tag).TableExport;
						ITableExport oldTableParente = ((CDataArbre)node.Parent.Tag).TableExport;
						ITableExport tableMoved = ((CDataArbre)node.Tag).TableExport;

						CDefinitionProprieteDynamique champOrigine = tableMoved.ChampOrigine;
						//Faut-il changer l'origine
						if (GetTypeDonneesNode(newParent) != GetTypeDonneesNode(node.Parent))
						{

							if (GetTypeDonneesNode(newParent) == GetTypeDonneesNode(node) &&
								tableMoved is ITableExportAOrigineModifiable)
							{
								//ahaha ! le nouveau parent et le fils sont du même type,
								//Allons y pour un conversion en this !
								((ITableExportAOrigineModifiable)tableMoved).ChampOrigine = new CDefinitionProprieteDynamiqueThis(
									new CTypeResultatExpression(GetTypeDonneesNode(newParent), true), true, false);
							}
							else
							{
								if (!(tableMoved is ITableExportAOrigineModifiable) ||
									!(champOrigine is CDefinitionProprieteDynamiqueThis))
								{
									return;
								}
								//Trouve l'origine réelle (avant le this
								TreeNode nodeTmp = node;
								while (nodeTmp != null)
								{
									ITableExport tableParente = ((CDataArbre)nodeTmp.Tag).TableExport;
									champOrigine = tableParente.ChampOrigine;
									if (champOrigine == null)
										return;
									if (!(champOrigine is CDefinitionProprieteDynamiqueThis))
										break;
									nodeTmp = nodeTmp.Parent;
								}
								if (champOrigine == null || champOrigine is CDefinitionProprieteDynamiqueThis)
									return;
								((ITableExportAOrigineModifiable)tableMoved).ChampOrigine = (CDefinitionProprieteDynamique)CCloner2iSerializable.Clone(champOrigine);
							}
						}


						if (newTableParente != null && oldTableParente != null && tableMoved != null)
						{
							oldTableParente.RemoveTableFille(tableMoved);
							newTableParente.AddTableFille(tableMoved);
							node.Parent.Nodes.Remove(node);
							newParent.Nodes.Add(node);
							e.Effect = DragDropEffects.Move;
						}
					}
				}
			}
			AfficheInfosNoeud(m_arbreTables.SelectedNode);
		}

		//-------------------------------------------------------------------
		private void m_arbreTables_DragOver(object sender, DragEventArgs e)
		{
			VerifiePossibiliteDragDrop(e);
		}

		//-------------------------------------------------------------------
		private void m_arbreTables_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeNode node = e.Item as TreeNode;
			if (node != null && !(node.Tag is C2iTableExportCalculee))
			{
				DoDragDrop(node, DragDropEffects.Move | DragDropEffects.None);
			}
		}

		//-------------------------------------------------------------------------------
		private Type GetTypeDonneesNode(TreeNode node)
		{
			if (node == null)
				return null;
			CDataArbre data = node.Tag as CDataArbre;
			if ( data == null || data.TableExport == null)
				return null;
			Type tp = m_structureExport.TypeSource;
			if (data.TableExport.ChampOrigine != null)
				tp = data.TableExport.ChampOrigine.TypeDonnee.TypeDotNetNatif;
			return tp;
		}

		//-------------------------------------------------------------------
		private bool CanMove(TreeNode nodeFils, TreeNode newParent)
		{
			//Vérifie que le nouveau parent n'est pas fils du nodeFils
			TreeNode nodeTmp = newParent;
			while ( nodeTmp != null )
			{
				if ( nodeTmp == nodeFils )
					return false;
				nodeTmp = nodeTmp.Parent;
			}
			CDataArbre dataFils = nodeFils.Tag as CDataArbre;
			CDataArbre dataPere = newParent.Tag as CDataArbre;
			if ( dataFils != null && dataPere != null &&
				dataFils.TableExport is ITableExport && 
				dataPere.TableExport is ITableExport )
			{
				ITableExport tableParente = dataPere.TableExport;
				ITableExport tableFille = dataFils.TableExport;
				
				if (!tableParente.AcceptChilds())
					return false;
				
				//Pour pouvoir être déplacé, l'ancien parent et le nouveau parent
				//Doivent avoir le même type
				if (nodeFils.Parent == null)
					return false;
				Type tpNew = GetTypeDonneesNode(newParent);
				Type tpOld = GetTypeDonneesNode(nodeFils.Parent);
				if (tpNew == tpOld)
					return true;
				//Si la table draggée a le même type que la nouvelle parente,
				//On pourra convertir son origine en this, donc, accepté !
				if (GetTypeDonneesNode(nodeFils) == tpNew)
					return true;
				//Si la table draggée a pour un origine un this, trouve la propriété d'origine réelle, car
				//c'est elle qui compte
				while (tableFille.ChampOrigine is CDefinitionProprieteDynamiqueThis && 
					tableFille is ITableExportAOrigineModifiable)
				{
					nodeFils = nodeFils.Parent;
					if (GetTypeDonneesNode(nodeFils.Parent) == tpNew)
						return true;
					CDataArbre data = nodeFils.Tag as CDataArbre;
					if (data != null)
					{
						tableFille = data.TableExport;
					}
					else
						return false;
					
				}
				return false;
			}
			else
				return false;
		}

		//------------------------------------------------------------------
		private void m_arbreTables_DragEnter(object sender, DragEventArgs e)
		{
			VerifiePossibiliteDragDrop(e);
		}

		//------------------------------------------------------------------
		private void VerifiePossibiliteDragDrop ( DragEventArgs e )
		{
			TreeNode node = e.Data.GetData(typeof(TreeNode)) as TreeNode;
			e.Effect = DragDropEffects.None;
			if (node != null)
			{

				TreeViewHitTestInfo info = m_arbreTables.HitTest(m_arbreTables.PointToClient(new Point(e.X, e.Y)));
				if (info != null && info.Node != null)
				{
					if (CanMove(node, info.Node))
						e.Effect = DragDropEffects.Move;
				}
			}
					
		}

		//------------------------------------------------------------------------
		private void m_menuSupprimerDecalerLesFils_Click(object sender, EventArgs e)
		{

		}

		//------------------------------------------------------------------------
		private void m_menuAddVue_Click(object sender, EventArgs e)
		{
			// Ajoute une table de Fusion. Permet de fusionner plusieurs champs de tables différentes
			TreeNode node = m_arbreTables.SelectedNode;
			if (node == null || !(node.Tag is CDataArbre))
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			C2iTableExportATableFille tableParente = data.TableExport as C2iTableExportATableFille;
			if (tableParente != null)
			{
				Type tpParent = m_structureExport.TypeSource;
				if (tableParente.ChampOrigine != null)
					tpParent = tableParente.ChampOrigine.TypeDonnee.TypeDotNetNatif;

				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes(
					tpParent,
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.MonoSelection |
					CFormSelectChampPourStructure.TypeSelectionAttendue.AllowThis,
					tableParente.ChampOrigine);
				foreach (CDefinitionProprieteDynamique def in defs)
				{
					// Créé une nouvelle table sout la table parente
					C2iTableExportVue nouvelleTableVue = new C2iTableExportVue();
					nouvelleTableVue.ChampOrigine = def;
					// Ajoute la nouvelle table à l'arbre des tables
					TreeNode newNode = new TreeNode();
					m_arbreTables.SelectedNode.Nodes.Add(newNode);
					FillNodeForTable(newNode, nouvelleTableVue);
					m_arbreTables.SelectedNode = newNode;
					tableParente.AddTableFille(nouvelleTableVue);
				}
			}
		}

		//----------------------------------------------------------------------
		private void m_menuAddTableauCroise_Click(object sender, EventArgs e)
		{
			// Ajoute une table de tableau croisé
			TreeNode node = m_arbreTables.SelectedNode;
			if (node == null || !(node.Tag is CDataArbre))
				return;
			CDataArbre data = (CDataArbre)node.Tag;
			C2iTableExportATableFille tableParente = data.TableExport as C2iTableExportATableFille;
			if (tableParente != null)
			{
				Type tpParent = m_structureExport.TypeSource;
				if (tableParente.ChampOrigine != null)
					tpParent = tableParente.ChampOrigine.TypeDonnee.TypeDotNetNatif;

				CDefinitionProprieteDynamique[] defs = CFormSelectChampPourStructure.SelectProprietes(
					tpParent,
					CFormSelectChampPourStructure.TypeSelectionAttendue.TableFille |
					CFormSelectChampPourStructure.TypeSelectionAttendue.MonoSelection |
					CFormSelectChampPourStructure.TypeSelectionAttendue.AllowThis,
					tableParente.ChampOrigine);
				foreach (CDefinitionProprieteDynamique def in defs)
				{
					// Créé une nouvelle table sous la table parente
					C2iTableExportTableauCroise nouvelleTableTableauCroise = new C2iTableExportTableauCroise();
					nouvelleTableTableauCroise.ChampOrigine = def;
					// Ajoute la nouvelle table à l'arbre des tables
					TreeNode newNode = new TreeNode();
					m_arbreTables.SelectedNode.Nodes.Add(newNode);
					FillNodeForTable(newNode, nouvelleTableTableauCroise);
					m_arbreTables.SelectedNode = newNode;
					tableParente.AddTableFille(nouvelleTableTableauCroise);
				}
			}
		}

		//----------------------------------------------------------------------
		private void Insere(TreeNode nodeFils, C2iTableExportATableFille tableToInsert)
		{
			if (nodeFils == null)
				return;
			bool bOk = false;
			TreeNodeCollection nodes = m_arbreTables.Nodes;
			if (nodeFils.Parent == null)
			{
				tableToInsert.ChampOrigine = null;
                ITableExportAOrigineModifiable table = m_structureExport.Table as ITableExportAOrigineModifiable;
				if (table != null)
				{
					table.ChampOrigine = new CDefinitionProprieteDynamiqueThis(
						new CTypeResultatExpression(m_structureExport.TypeSource, false), true, false);
					tableToInsert.AddTableFille(table);
					m_structureExport.Table = tableToInsert;
					bOk = true;
				}
			}
			else
			{
				if (nodeFils.Tag is CDataArbre &&
					nodeFils.Parent.Tag is CDataArbre)
				{
					C2iTableExportATableFille tableParente = ((CDataArbre)nodeFils.Parent.Tag).TableExport as C2iTableExportATableFille;
					ITableExport tableFille = ((CDataArbre)nodeFils.Tag).TableExport;
					if (tableParente != null )
					{
						tableParente.RemoveTableFille(tableFille);
						tableParente.AddTableFille(tableToInsert);
						if (tableFille.ChampOrigine != null && tableFille is C2iTableExportATableFille)
						{
							tableToInsert.ChampOrigine = tableFille.ChampOrigine;
							((C2iTableExportATableFille)tableFille).ChampOrigine = new CDefinitionProprieteDynamiqueThis(
								tableFille.ChampOrigine.TypeDonnee, true, false);
						}
						else
						{
							tableToInsert.ChampOrigine = new CDefinitionProprieteDynamiqueThis(
								new CTypeResultatExpression(GetTypeDonneesNode(nodeFils.Parent), true),

								true, false);
						}
						tableToInsert.AddTableFille(tableFille);
						nodes = nodeFils.Parent.Nodes;
						bOk = true;
					}
					
				}
			}
			if (bOk)
			{
				TreeNode newNode = new TreeNode();
				FillNodeForTable(newNode, tableToInsert);
				nodes.Add(newNode);
				nodes.Remove(nodeFils);
				newNode.Expand();
				m_arbreTables.SelectedNode = newNode;
			}
				
		}

		//----------------------------------------------------------------------
		private void m_menuInsererUnion_Click(object sender, EventArgs e)
		{
			Insere(m_arbreTables.SelectedNode, new C2iTableExportUnion());
			
		}

		//----------------------------------------------------------------------
		private void m_menuInsererTableauCroise_Click(object sender, EventArgs e)
		{
			Insere(m_arbreTables.SelectedNode, new C2iTableExportTableauCroise());
		}

		//----------------------------------------------------------------------
		private void m_menuInsererVue_Click(object sender, EventArgs e)
		{
			Insere(m_arbreTables.SelectedNode, new C2iTableExportVue());
		}

		//----------------------------------------------------------------------
		private void m_arbreTables_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				CDataArbre data = e.Node.Tag as CDataArbre;
				if (data != null)
				{
					if (!e.Node.Checked != data.TableExport.NePasCalculer)
					{
						data.TableExport.NePasCalculer = !e.Node.Checked;
						e.Node.BackColor = data.TableExport.NePasCalculer ? Color.Red : m_arbreTables.BackColor;
					}
				}
			}
		}

       



	}
}
