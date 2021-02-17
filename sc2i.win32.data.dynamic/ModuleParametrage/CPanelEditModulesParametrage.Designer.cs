using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.win32.data.dynamic.ModuleParametrage;
namespace sc2i.win32.data.dynamic
{
    partial class CPanelEditModulesParametrage
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
            CFiltreInterfaceExterne.UnregisterFournisseur(this);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditModulesParametrage));
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_ArbreModules = new sc2i.win32.data.dynamic.CArbreModulesParametrage();
            this.m_menuArbreModules = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_nouveauModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_chkAppliquerFiltre = new System.Windows.Forms.CheckBox();
            this.m_treeItems = new sc2i.win32.data.dynamic.ModuleParametrage.CArbreElementsModule();
            this.m_panelProprietes = new System.Windows.Forms.Panel();
            this.m_ctrlEditModuleParam = new sc2i.win32.data.dynamic.CControlEditModuleParametrage();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_panelHaut = new sc2i.win32.common.C2iPanelFondDegradeStd();
            this.m_btnAutoArrange = new System.Windows.Forms.Button();
            this.m_chkAlwaysVisible = new System.Windows.Forms.CheckBox();
            this.m_btnOrientationV = new System.Windows.Forms.RadioButton();
            this.m_btnOrientationH = new System.Windows.Forms.RadioButton();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.m_menuArbreModules.SuspendLayout();
            this.m_panelProprietes.SuspendLayout();
            this.m_panelHaut.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 25);
            this.m_splitContainer.Name = "m_splitContainer";
            this.m_splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_ArbreModules);
            this.m_splitContainer.Panel1.Controls.Add(this.m_chkAppliquerFiltre);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_treeItems);
            this.m_splitContainer.Panel2.Controls.Add(this.m_panelProprietes);
            this.m_splitContainer.Size = new System.Drawing.Size(382, 455);
            this.m_splitContainer.SplitterDistance = 212;
            this.m_splitContainer.TabIndex = 1;
            // 
            // m_ArbreModules
            // 
            this.m_ArbreModules.AllowDrop = true;
            this.m_ArbreModules.ContextMenuStrip = this.m_menuArbreModules;
            this.m_ArbreModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ArbreModules.HideSelection = false;
            this.m_ArbreModules.ImageIndex = 0;
            this.m_ArbreModules.Location = new System.Drawing.Point(0, 0);
            this.m_ArbreModules.Name = "m_ArbreModules";
            this.m_ArbreModules.SelectedImageIndex = 0;
            this.m_ArbreModules.Size = new System.Drawing.Size(378, 191);
            this.m_ArbreModules.TabIndex = 0;
            this.m_ArbreModules.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_ArbreModules_DragDrop);
            this.m_ArbreModules.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_ArbreModules_AfterSelect);
            this.m_ArbreModules.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_ArbreModules_DragEnter);
            this.m_ArbreModules.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_ArbreModules_ItemDrag);
            this.m_ArbreModules.DragOver += new System.Windows.Forms.DragEventHandler(this.m_ArbreModules_DragOver);
            // 
            // m_menuArbreModules
            // 
            this.m_menuArbreModules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_nouveauModuleToolStripMenuItem,
            this.toolStripSeparator1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pastToolStripMenuItem,
            this.toolStripSeparator2,
            this.m_deleteToolStripMenuItem});
            this.m_menuArbreModules.Name = "m_menuArbreModules";
            this.m_menuArbreModules.ShowImageMargin = false;
            this.m_menuArbreModules.Size = new System.Drawing.Size(153, 126);
            // 
            // m_nouveauModuleToolStripMenuItem
            // 
            this.m_nouveauModuleToolStripMenuItem.Name = "m_nouveauModuleToolStripMenuItem";
            this.m_nouveauModuleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_nouveauModuleToolStripMenuItem.Text = "New Module|10005";
            this.m_nouveauModuleToolStripMenuItem.Click += new System.EventHandler(this.m_nouveauModuleToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem.Text = "Cut|283";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy|280";
            // 
            // pastToolStripMenuItem
            // 
            this.pastToolStripMenuItem.Name = "pastToolStripMenuItem";
            this.pastToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pastToolStripMenuItem.Text = "Past|281";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // m_deleteToolStripMenuItem
            // 
            this.m_deleteToolStripMenuItem.Name = "m_deleteToolStripMenuItem";
            this.m_deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_deleteToolStripMenuItem.Text = "Delete|276";
            this.m_deleteToolStripMenuItem.Click += new System.EventHandler(this.m_deleteToolStripMenuItem_Click);
            // 
            // m_chkAppliquerFiltre
            // 
            this.m_chkAppliquerFiltre.AutoSize = true;
            this.m_chkAppliquerFiltre.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_chkAppliquerFiltre.Location = new System.Drawing.Point(0, 191);
            this.m_chkAppliquerFiltre.Name = "m_chkAppliquerFiltre";
            this.m_chkAppliquerFiltre.Size = new System.Drawing.Size(378, 17);
            this.m_chkAppliquerFiltre.TabIndex = 1;
            this.m_chkAppliquerFiltre.Text = "Global filter|20059";
            this.m_chkAppliquerFiltre.UseVisualStyleBackColor = true;
            // 
            // m_treeItems
            // 
            this.m_treeItems.AllowDrop = true;
            this.m_treeItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_treeItems.ImageIndex = 0;
            this.m_treeItems.Location = new System.Drawing.Point(0, 93);
            this.m_treeItems.Name = "m_treeItems";
            this.m_treeItems.SelectedImageIndex = 0;
            this.m_treeItems.Size = new System.Drawing.Size(378, 142);
            this.m_treeItems.TabIndex = 2;
            this.m_treeItems.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_treeItems_NodeMouseDoubleClick);
            // 
            // m_panelProprietes
            // 
            this.m_panelProprietes.Controls.Add(this.m_ctrlEditModuleParam);
            this.m_panelProprietes.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelProprietes.Location = new System.Drawing.Point(0, 0);
            this.m_panelProprietes.Name = "m_panelProprietes";
            this.m_panelProprietes.Size = new System.Drawing.Size(378, 93);
            this.m_panelProprietes.TabIndex = 0;
            // 
            // m_ctrlEditModuleParam
            // 
            this.m_ctrlEditModuleParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_ctrlEditModuleParam.Location = new System.Drawing.Point(0, 0);
            this.m_ctrlEditModuleParam.Name = "m_ctrlEditModuleParam";
            this.m_ctrlEditModuleParam.Size = new System.Drawing.Size(378, 82);
            this.m_ctrlEditModuleParam.TabIndex = 0;
            this.m_ctrlEditModuleParam.Leave += new System.EventHandler(this.m_ctrlEditModuleParam_Leave);
            // 
            // m_panelBas
            // 
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 480);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(382, 16);
            this.m_panelBas.TabIndex = 3;
            // 
            // m_panelHaut
            // 
            this.m_panelHaut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_panelHaut.BackgroundImage")));
            this.m_panelHaut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_panelHaut.Controls.Add(this.m_btnAutoArrange);
            this.m_panelHaut.Controls.Add(this.m_chkAlwaysVisible);
            this.m_panelHaut.Controls.Add(this.m_btnOrientationV);
            this.m_panelHaut.Controls.Add(this.m_btnOrientationH);
            this.m_panelHaut.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHaut.Location = new System.Drawing.Point(0, 0);
            this.m_panelHaut.Name = "m_panelHaut";
            this.m_panelHaut.Size = new System.Drawing.Size(382, 25);
            this.m_panelHaut.TabIndex = 0;
            // 
            // m_btnAutoArrange
            // 
            this.m_btnAutoArrange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAutoArrange.BackColor = System.Drawing.Color.Transparent;
            this.m_btnAutoArrange.FlatAppearance.BorderSize = 0;
            this.m_btnAutoArrange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_btnAutoArrange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_btnAutoArrange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAutoArrange.Image = global::sc2i.win32.data.dynamic.Properties.Resources._1258732295_layers_stack_arrange;
            this.m_btnAutoArrange.Location = new System.Drawing.Point(352, 1);
            this.m_btnAutoArrange.Name = "m_btnAutoArrange";
            this.m_btnAutoArrange.Size = new System.Drawing.Size(24, 24);
            this.m_btnAutoArrange.TabIndex = 2;
            this.m_btnAutoArrange.UseVisualStyleBackColor = false;
            this.m_btnAutoArrange.Click += new System.EventHandler(this.m_btnAutoArrange_Click);
            // 
            // m_chkAlwaysVisible
            // 
            this.m_chkAlwaysVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkAlwaysVisible.AutoSize = true;
            this.m_chkAlwaysVisible.BackColor = System.Drawing.Color.Transparent;
            this.m_chkAlwaysVisible.Location = new System.Drawing.Point(183, 5);
            this.m_chkAlwaysVisible.Name = "m_chkAlwaysVisible";
            this.m_chkAlwaysVisible.Size = new System.Drawing.Size(91, 17);
            this.m_chkAlwaysVisible.TabIndex = 1;
            this.m_chkAlwaysVisible.Text = "Always visible";
            this.m_chkAlwaysVisible.UseVisualStyleBackColor = false;
            this.m_chkAlwaysVisible.CheckedChanged += new System.EventHandler(this.m_chkAlwaysVisible_CheckedChanged);
            // 
            // m_btnOrientationV
            // 
            this.m_btnOrientationV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOrientationV.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnOrientationV.BackColor = System.Drawing.Color.Transparent;
            this.m_btnOrientationV.FlatAppearance.BorderSize = 0;
            this.m_btnOrientationV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_btnOrientationV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_btnOrientationV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOrientationV.Image = global::sc2i.win32.data.dynamic.Properties.Resources._1258730073_application_split;
            this.m_btnOrientationV.Location = new System.Drawing.Point(317, 1);
            this.m_btnOrientationV.Name = "m_btnOrientationV";
            this.m_btnOrientationV.Size = new System.Drawing.Size(24, 24);
            this.m_btnOrientationV.TabIndex = 0;
            this.m_btnOrientationV.UseVisualStyleBackColor = false;
            this.m_btnOrientationV.Click += new System.EventHandler(this.m_btnOrientationV_Click);
            // 
            // m_btnOrientationH
            // 
            this.m_btnOrientationH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOrientationH.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnOrientationH.BackColor = System.Drawing.Color.Transparent;
            this.m_btnOrientationH.FlatAppearance.BorderSize = 0;
            this.m_btnOrientationH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_btnOrientationH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_btnOrientationH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOrientationH.Image = global::sc2i.win32.data.dynamic.Properties.Resources._1258730095_application_split_vertical;
            this.m_btnOrientationH.Location = new System.Drawing.Point(292, 1);
            this.m_btnOrientationH.Name = "m_btnOrientationH";
            this.m_btnOrientationH.Size = new System.Drawing.Size(24, 24);
            this.m_btnOrientationH.TabIndex = 0;
            this.m_btnOrientationH.UseVisualStyleBackColor = false;
            this.m_btnOrientationH.Click += new System.EventHandler(this.m_btnOrientationH_Click);
            // 
            // CPanelEditModulesParametrage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_splitContainer);
            this.Controls.Add(this.m_panelBas);
            this.Controls.Add(this.m_panelHaut);
            this.Name = "CPanelEditModulesParametrage";
            this.Size = new System.Drawing.Size(382, 496);
            this.Load += new System.EventHandler(this.CPanelEditModulesParametrage_Load);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel1.PerformLayout();
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.m_menuArbreModules.ResumeLayout(false);
            this.m_panelProprietes.ResumeLayout(false);
            this.m_panelHaut.ResumeLayout(false);
            this.m_panelHaut.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C2iPanelFondDegradeStd m_panelHaut;
        private System.Windows.Forms.SplitContainer m_splitContainer;
        private CArbreModulesParametrage m_ArbreModules;
        private System.Windows.Forms.Panel m_panelProprietes;
        private System.Windows.Forms.ContextMenuStrip m_menuArbreModules;
        private System.Windows.Forms.ToolStripMenuItem m_nouveauModuleToolStripMenuItem;
        private System.Windows.Forms.Panel m_panelBas;
        private CControlEditModuleParametrage m_ctrlEditModuleParam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem m_deleteToolStripMenuItem;
        private RadioButton m_btnOrientationH;
        private RadioButton m_btnOrientationV;
        private CheckBox m_chkAlwaysVisible;
        private Button m_btnAutoArrange;
        private CheckBox m_chkAppliquerFiltre;
        private CArbreElementsModule m_treeItems;

    }
}
