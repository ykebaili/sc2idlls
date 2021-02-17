namespace data.hotel.eastquery.win32.filtre
{
    partial class CPanelFiltreDataHotel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_arbreFiltre = new System.Windows.Forms.TreeView();
            this.m_menuArbre = new System.Windows.Forms.ContextMenu();
            this.m_menuAjouter = new System.Windows.Forms.MenuItem();
            this.m_menuAddEt = new System.Windows.Forms.MenuItem();
            this.m_menuAddOu = new System.Windows.Forms.MenuItem();
            this.m_menuAddCondition = new System.Windows.Forms.MenuItem();
            this.m_menuInsert = new System.Windows.Forms.MenuItem();
            this.m_menuInsererEt = new System.Windows.Forms.MenuItem();
            this.m_menuInsererOu = new System.Windows.Forms.MenuItem();
            this.m_menuSupprimer = new System.Windows.Forms.MenuItem();
            this.m_menuSupprimerElementEtFils = new System.Windows.Forms.MenuItem();
            this.m_menuDecalerFilsVersLeHaut = new System.Windows.Forms.MenuItem();
            this.m_menuProprietes = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // m_arbreFiltre
            // 
            this.m_arbreFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreFiltre.HideSelection = false;
            this.m_arbreFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_arbreFiltre.Name = "m_arbreFiltre";
            this.m_arbreFiltre.Size = new System.Drawing.Size(595, 398);
            this.m_exStyle.SetStyleBackColor(this.m_arbreFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_arbreFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_arbreFiltre.TabIndex = 1;
            this.m_arbreFiltre.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_arbreFiltre_NodeMouseClick);
            this.m_arbreFiltre.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_arbreFiltre_MouseUp);
            // 
            // m_menuArbre
            // 
            this.m_menuArbre.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouter,
            this.m_menuInsert,
            this.m_menuSupprimer,
            this.m_menuProprietes});
            this.m_menuArbre.Popup += new System.EventHandler(this.m_menuArbre_Popup);
            // 
            // m_menuAjouter
            // 
            this.m_menuAjouter.Index = 0;
            this.m_menuAjouter.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAddEt,
            this.m_menuAddOu,
            this.m_menuAddCondition});
            this.m_menuAjouter.Text = "Add|20018";
            // 
            // m_menuAddEt
            // 
            this.m_menuAddEt.Index = 0;
            this.m_menuAddEt.Text = "AND group|20022";
            this.m_menuAddEt.Click += new System.EventHandler(this.m_menuAddEt_Click);
            // 
            // m_menuAddOu
            // 
            this.m_menuAddOu.Index = 1;
            this.m_menuAddOu.Text = "OR group|20023";
            this.m_menuAddOu.Click += new System.EventHandler(this.m_menuAddOu_Click);
            // 
            // m_menuAddCondition
            // 
            this.m_menuAddCondition.Index = 2;
            this.m_menuAddCondition.Text = "Simple condition|20024";
            this.m_menuAddCondition.Click += new System.EventHandler(this.m_menuAddCondition_Click);
            // 
            // m_menuInsert
            // 
            this.m_menuInsert.Index = 1;
            this.m_menuInsert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuInsererEt,
            this.m_menuInsererOu});
            this.m_menuInsert.Text = "Insert|20019";
            // 
            // m_menuInsererEt
            // 
            this.m_menuInsererEt.Index = 0;
            this.m_menuInsererEt.Text = "AND group|20022";
            this.m_menuInsererEt.Click += new System.EventHandler(this.m_menuInsererEt_Click);
            // 
            // m_menuInsererOu
            // 
            this.m_menuInsererOu.Index = 1;
            this.m_menuInsererOu.Text = "OR group|20023";
            this.m_menuInsererOu.Click += new System.EventHandler(this.m_menuInsererOu_Click);
            // 
            // m_menuSupprimer
            // 
            this.m_menuSupprimer.Index = 2;
            this.m_menuSupprimer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuSupprimerElementEtFils,
            this.m_menuDecalerFilsVersLeHaut});
            this.m_menuSupprimer.Text = "Delete|20020";
            // 
            // m_menuSupprimerElementEtFils
            // 
            this.m_menuSupprimerElementEtFils.Index = 0;
            this.m_menuSupprimerElementEtFils.Text = "The element and its children|20025";
            this.m_menuSupprimerElementEtFils.Click += new System.EventHandler(this.m_menuSupprimerElementEtFils_Click);
            // 
            // m_menuDecalerFilsVersLeHaut
            // 
            this.m_menuDecalerFilsVersLeHaut.Index = 1;
            this.m_menuDecalerFilsVersLeHaut.Text = "Shift the children upwards|20026";
            this.m_menuDecalerFilsVersLeHaut.Click += new System.EventHandler(this.m_menuDecalerFilsVersLeHaut_Click);
            // 
            // m_menuProprietes
            // 
            this.m_menuProprietes.Index = 3;
            this.m_menuProprietes.Text = "Properties|20021";
            this.m_menuProprietes.Click += new System.EventHandler(this.m_menuProprietes_Click);
            // 
            // CPanelFiltreDataHotel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_arbreFiltre);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CPanelFiltreDataHotel";
            this.Size = new System.Drawing.Size(595, 398);
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Load += new System.EventHandler(this.CFormEditeProprietesFiltreDataHotel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_exStyle;
        private System.Windows.Forms.TreeView m_arbreFiltre;
        private System.Windows.Forms.ContextMenu m_menuArbre;
        private System.Windows.Forms.MenuItem m_menuAjouter;
        private System.Windows.Forms.MenuItem m_menuAddEt;
        private System.Windows.Forms.MenuItem m_menuAddOu;
        private System.Windows.Forms.MenuItem m_menuAddCondition;
        private System.Windows.Forms.MenuItem m_menuInsert;
        private System.Windows.Forms.MenuItem m_menuInsererEt;
        private System.Windows.Forms.MenuItem m_menuInsererOu;
        private System.Windows.Forms.MenuItem m_menuSupprimer;
        private System.Windows.Forms.MenuItem m_menuSupprimerElementEtFils;
        private System.Windows.Forms.MenuItem m_menuDecalerFilsVersLeHaut;
        private System.Windows.Forms.MenuItem m_menuProprietes;
    }
}