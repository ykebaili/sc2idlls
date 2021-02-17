using sc2i.win32.common;
namespace sc2i.win32.data.dynamic
{
    partial class CPanelEditParametreVisuDonneePrecalculee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditParametreVisuDonneePrecalculee));
            this.m_cmbTypeDonnee = new sc2i.win32.data.CComboBoxListeObjetsDonnees();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelTableauCroise = new sc2i.win32.common.CPanelEditTableauCroise();
            this.m_panelCumul = new System.Windows.Forms.Panel();
            this.m_panelLibelleTotal = new System.Windows.Forms.Panel();
            this.m_txtLibelleTotal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_cmbOperation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelFormatRows = new sc2i.win32.data.dynamic.CPanelFormattageChamp();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelFormatHeader = new sc2i.win32.data.dynamic.CPanelFormattageChamp();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lblNomChamp = new System.Windows.Forms.Label();
            this.m_chkShowHeader = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelFormatChamp = new sc2i.win32.data.dynamic.CPanelFormatChamp();
            this.m_wndListeChamps = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.tabPage4 = new Crownwood.Magic.Controls.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_panelFiltresDeBase = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.m_panelFiltresUser = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_chkShowExportButton = new System.Windows.Forms.CheckBox();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_panelCumul.SuspendLayout();
            this.m_panelLibelleTotal.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmbTypeDonnee
            // 
            this.m_cmbTypeDonnee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeDonnee.ElementSelectionne = null;
            this.m_cmbTypeDonnee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeDonnee.FormattingEnabled = true;
            this.m_cmbTypeDonnee.IsLink = false;
            this.m_cmbTypeDonnee.ListDonnees = null;
            this.m_cmbTypeDonnee.Location = new System.Drawing.Point(152, 2);
            this.m_cmbTypeDonnee.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeDonnee, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeDonnee.Name = "m_cmbTypeDonnee";
            this.m_cmbTypeDonnee.NullAutorise = false;
            this.m_cmbTypeDonnee.ProprieteAffichee = null;
            this.m_cmbTypeDonnee.ProprieteParentListeObjets = null;
            this.m_cmbTypeDonnee.SelectionneurParent = null;
            this.m_cmbTypeDonnee.Size = new System.Drawing.Size(304, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbTypeDonnee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbTypeDonnee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeDonnee.TabIndex = 0;
            this.m_cmbTypeDonnee.TextNull = "(empty)|20023";
            this.m_cmbTypeDonnee.Tri = true;
            this.m_cmbTypeDonnee.SelectionChangeCommitted += new System.EventHandler(this.m_cmbTypeDonnee_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 18);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "Precalculated data type|20024";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_tabControl
            // 
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ControlBottomOffset = 16;
            this.m_tabControl.ControlRightOffset = 16;
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(0, 29);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tabControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.SelectedTab = this.tabPage1;
            this.m_tabControl.Size = new System.Drawing.Size(816, 444);
            this.cExtStyle1.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 2;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage3,
            this.tabPage2,
            this.tabPage4});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelTableauCroise);
            this.tabPage1.Controls.Add(this.m_panelCumul);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(800, 403);
            this.cExtStyle1.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Cross data|20028";
            // 
            // m_panelTableauCroise
            // 
            this.m_panelTableauCroise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelTableauCroise.Location = new System.Drawing.Point(0, 0);
            this.m_panelTableauCroise.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableauCroise, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelTableauCroise.Name = "m_panelTableauCroise";
            this.m_panelTableauCroise.Size = new System.Drawing.Size(800, 378);
            this.cExtStyle1.SetStyleBackColor(this.m_panelTableauCroise, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelTableauCroise, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelTableauCroise.TabIndex = 0;
            // 
            // m_panelCumul
            // 
            this.m_panelCumul.Controls.Add(this.m_panelLibelleTotal);
            this.m_panelCumul.Controls.Add(this.m_cmbOperation);
            this.m_panelCumul.Controls.Add(this.label4);
            this.m_panelCumul.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelCumul.Location = new System.Drawing.Point(0, 378);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelCumul, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelCumul.Name = "m_panelCumul";
            this.m_panelCumul.Size = new System.Drawing.Size(800, 25);
            this.cExtStyle1.SetStyleBackColor(this.m_panelCumul, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelCumul, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelCumul.TabIndex = 1;
            // 
            // m_panelLibelleTotal
            // 
            this.m_panelLibelleTotal.Controls.Add(this.m_txtLibelleTotal);
            this.m_panelLibelleTotal.Controls.Add(this.label6);
            this.m_panelLibelleTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelLibelleTotal.Location = new System.Drawing.Point(255, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelLibelleTotal, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelLibelleTotal.Name = "m_panelLibelleTotal";
            this.m_panelLibelleTotal.Size = new System.Drawing.Size(200, 25);
            this.cExtStyle1.SetStyleBackColor(this.m_panelLibelleTotal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelLibelleTotal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelLibelleTotal.TabIndex = 2;
            // 
            // m_txtLibelleTotal
            // 
            this.m_txtLibelleTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtLibelleTotal.Location = new System.Drawing.Point(58, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtLibelleTotal, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtLibelleTotal.Name = "m_txtLibelleTotal";
            this.m_txtLibelleTotal.Size = new System.Drawing.Size(100, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_txtLibelleTotal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtLibelleTotal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelleTotal.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 25);
            this.cExtStyle1.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 2;
            this.label6.Text = "Label";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbOperation
            // 
            this.m_cmbOperation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOperation.Location = new System.Drawing.Point(143, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbOperation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbOperation.Name = "m_cmbOperation";
            this.m_cmbOperation.Size = new System.Drawing.Size(112, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbOperation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbOperation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbOperation.TabIndex = 1;
            this.m_cmbOperation.SelectedIndexChanged += new System.EventHandler(this.m_cmbOperation_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 25);
            this.cExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 0;
            this.label4.Text = "Global summary";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_panelFormatRows);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.m_panelFormatHeader);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(800, 403);
            this.cExtStyle1.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "General format|20029";
            // 
            // m_panelFormatRows
            // 
            this.m_panelFormatRows.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFormatRows.Location = new System.Drawing.Point(0, 140);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFormatRows, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFormatRows.Name = "m_panelFormatRows";
            this.m_panelFormatRows.Size = new System.Drawing.Size(800, 94);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFormatRows, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFormatRows, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormatRows.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 117);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(800, 23);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rows|20030";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelFormatHeader
            // 
            this.m_panelFormatHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFormatHeader.Location = new System.Drawing.Point(0, 23);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFormatHeader, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFormatHeader.Name = "m_panelFormatHeader";
            this.m_panelFormatHeader.Size = new System.Drawing.Size(800, 94);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFormatHeader, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFormatHeader, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormatHeader.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.m_lblNomChamp);
            this.panel1.Controls.Add(this.m_chkShowHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 23);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 5;
            // 
            // m_lblNomChamp
            // 
            this.m_lblNomChamp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_lblNomChamp.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblNomChamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNomChamp.ForeColor = System.Drawing.Color.White;
            this.m_lblNomChamp.Location = new System.Drawing.Point(22, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblNomChamp, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblNomChamp.Name = "m_lblNomChamp";
            this.m_lblNomChamp.Size = new System.Drawing.Size(778, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_lblNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lblNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblNomChamp.TabIndex = 1;
            this.m_lblNomChamp.Text = "Headers|20017";
            this.m_lblNomChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_chkShowHeader
            // 
            this.m_chkShowHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_chkShowHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkShowHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_chkShowHeader.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkShowHeader, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkShowHeader.Name = "m_chkShowHeader";
            this.m_chkShowHeader.Size = new System.Drawing.Size(22, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_chkShowHeader, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkShowHeader, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkShowHeader.TabIndex = 4;
            this.m_chkShowHeader.UseVisualStyleBackColor = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_panelFormatChamp);
            this.tabPage2.Controls.Add(this.m_wndListeChamps);
            this.tabPage2.Controls.Add(this.m_lnkUpdate);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(800, 403);
            this.cExtStyle1.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Display format|20031";
            // 
            // m_panelFormatChamp
            // 
            this.m_panelFormatChamp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelFormatChamp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFormatChamp.ForeColor = System.Drawing.Color.Black;
            this.m_panelFormatChamp.Location = new System.Drawing.Point(235, 16);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFormatChamp, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFormatChamp.Name = "m_panelFormatChamp";
            this.m_panelFormatChamp.Size = new System.Drawing.Size(562, 384);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFormatChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFormatChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormatChamp.TabIndex = 3;
            this.m_panelFormatChamp.Visible = false;
            // 
            // m_wndListeChamps
            // 
            this.m_wndListeChamps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_wndListeChamps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeChamps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeChamps.HideSelection = false;
            this.m_wndListeChamps.Location = new System.Drawing.Point(6, 16);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeChamps, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeChamps.Name = "m_wndListeChamps";
            this.m_wndListeChamps.ShowItemToolTips = true;
            this.m_wndListeChamps.Size = new System.Drawing.Size(223, 384);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeChamps.TabIndex = 2;
            this.m_wndListeChamps.UseCompatibleStateImageBehavior = false;
            this.m_wndListeChamps.View = System.Windows.Forms.View.Details;
            this.m_wndListeChamps.SelectedIndexChanged += new System.EventHandler(this.m_wndListeChamps_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 198;
            // 
            // m_lnkUpdate
            // 
            this.m_lnkUpdate.AutoSize = true;
            this.m_lnkUpdate.Location = new System.Drawing.Point(3, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkUpdate, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkUpdate.Name = "m_lnkUpdate";
            this.m_lnkUpdate.Size = new System.Drawing.Size(76, 13);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkUpdate, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkUpdate, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkUpdate.TabIndex = 1;
            this.m_lnkUpdate.TabStop = true;
            this.m_lnkUpdate.Text = "Update|20031";
            this.m_lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkUpdate_LinkClicked);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer1);
            this.tabPage4.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Selected = false;
            this.tabPage4.Size = new System.Drawing.Size(800, 403);
            this.cExtStyle1.SetStyleBackColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage4.TabIndex = 13;
            this.tabPage4.Title = "Filters|20025";
            this.tabPage4.PropertyChanged += new Crownwood.Magic.Controls.TabPage.PropChangeHandler(this.tabPage4_PropertyChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitContainer1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_panelFiltresDeBase);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitContainer1.Panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.cExtStyle1.SetStyleBackColor(this.splitContainer1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitContainer1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_panelFiltresUser);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitContainer1.Panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.cExtStyle1.SetStyleBackColor(this.splitContainer1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitContainer1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitContainer1.Size = new System.Drawing.Size(800, 403);
            this.splitContainer1.SplitterDistance = 180;
            this.cExtStyle1.SetStyleBackColor(this.splitContainer1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitContainer1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitContainer1.TabIndex = 2;
            // 
            // m_panelFiltresDeBase
            // 
            this.m_panelFiltresDeBase.AutoScroll = true;
            this.m_panelFiltresDeBase.AutoSize = true;
            this.m_panelFiltresDeBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltresDeBase.Location = new System.Drawing.Point(0, 23);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltresDeBase, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltresDeBase.Name = "m_panelFiltresDeBase";
            this.m_panelFiltresDeBase.Size = new System.Drawing.Size(800, 157);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFiltresDeBase, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFiltresDeBase, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltresDeBase.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(800, 23);
            this.cExtStyle1.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 7;
            this.label5.Text = "Base filter|20026";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelFiltresUser
            // 
            this.m_panelFiltresUser.AutoScroll = true;
            this.m_panelFiltresUser.AutoSize = true;
            this.m_panelFiltresUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltresUser.Location = new System.Drawing.Point(0, 23);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltresUser, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltresUser.Name = "m_panelFiltresUser";
            this.m_panelFiltresUser.Size = new System.Drawing.Size(800, 196);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFiltresUser, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFiltresUser, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltresUser.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(800, 23);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 8;
            this.label3.Text = "User filter|20027";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_chkShowExportButton);
            this.m_panelTop.Controls.Add(this.m_btnOpen);
            this.m_panelTop.Controls.Add(this.m_btnSave);
            this.m_panelTop.Controls.Add(this.m_cmbTypeDonnee);
            this.m_panelTop.Controls.Add(this.label1);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTop, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(816, 29);
            this.cExtStyle1.SetStyleBackColor(this.m_panelTop, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelTop, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelTop.TabIndex = 3;
            // 
            // m_chkShowExportButton
            // 
            this.m_chkShowExportButton.AutoSize = true;
            this.m_chkShowExportButton.Location = new System.Drawing.Point(463, 5);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkShowExportButton, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkShowExportButton.Name = "m_chkShowExportButton";
            this.m_chkShowExportButton.Size = new System.Drawing.Size(150, 17);
            this.cExtStyle1.SetStyleBackColor(this.m_chkShowExportButton, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkShowExportButton, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkShowExportButton.TabIndex = 31;
            this.m_chkShowExportButton.Text = "Show export button|20090";
            this.m_chkShowExportButton.UseVisualStyleBackColor = true;
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(792, -1);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOpen.TabIndex = 30;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(768, -1);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSave.TabIndex = 29;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // CPanelEditParametreVisuDonneePrecalculee
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.m_panelTop);
            this.ForeColor = System.Drawing.Color.Black;
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditParametreVisuDonneePrecalculee";
            this.Size = new System.Drawing.Size(816, 473);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.m_panelCumul.ResumeLayout(false);
            this.m_panelLibelleTotal.ResumeLayout(false);
            this.m_panelLibelleTotal.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.data.CComboBoxListeObjetsDonnees m_cmbTypeDonnee;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private CPanelEditTableauCroise m_panelTableauCroise;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.LinkLabel m_lnkUpdate;
        private System.Windows.Forms.ListView m_wndListeChamps;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private CPanelFormatChamp m_panelFormatChamp;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private CPanelFormattageChamp m_panelFormatHeader;
        private CPanelFormattageChamp m_panelFormatRows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_lblNomChamp;
        private Crownwood.Magic.Controls.TabPage tabPage4;
        private System.Windows.Forms.Button m_btnOpen;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.Panel m_panelFiltresDeBase;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel m_panelFiltresUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel m_panelCumul;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_cmbOperation;
        private System.Windows.Forms.TextBox m_txtLibelleTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel m_panelLibelleTotal;
        private System.Windows.Forms.CheckBox m_chkShowExportButton;
        private System.Windows.Forms.CheckBox m_chkShowHeader;
        private System.Windows.Forms.Panel panel1;
    }
}
