namespace sc2i.win32.data.navigation
{
    partial class CFormListeExtraiteOld
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
                m_listeFormesVisibles.Remove(this);
                if (m_imageDeListe != null)
                    m_imageDeListe.Dispose();
                m_imageDeListe = null;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormListeExtraiteOld));
            this.m_menuForm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_picListe = new System.Windows.Forms.PictureBox();
            this.m_chktopMost = new System.Windows.Forms.CheckBox();
            this.m_chkAutoArrange = new System.Windows.Forms.CheckBox();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelListe = new sc2i.win32.data.navigation.CPanelListeSpeedStandard();
            this.m_panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picListe)).BeginInit();
            this.SuspendLayout();
            // 
            // m_menuForm
            // 
            this.m_menuForm.Name = "m_menuForm";
            this.m_menuForm.Size = new System.Drawing.Size(61, 4);
            this.m_extStyle.SetStyleBackColor(this.m_menuForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_menuForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_picListe);
            this.m_panelTop.Controls.Add(this.m_chktopMost);
            this.m_panelTop.Controls.Add(this.m_chkAutoArrange);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(325, 20);
            this.m_extStyle.SetStyleBackColor(this.m_panelTop, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelTop, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelTop.TabIndex = 2;
            // 
            // m_picListe
            // 
            this.m_picListe.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_picListe.Location = new System.Drawing.Point(0, 0);
            this.m_picListe.Name = "m_picListe";
            this.m_picListe.Size = new System.Drawing.Size(20, 20);
            this.m_extStyle.SetStyleBackColor(this.m_picListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_picListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_picListe.TabIndex = 3;
            this.m_picListe.TabStop = false;
            // 
            // m_chktopMost
            // 
            this.m_chktopMost.AutoSize = true;
            this.m_chktopMost.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_chktopMost.Location = new System.Drawing.Point(180, 0);
            this.m_chktopMost.Name = "m_chktopMost";
            this.m_chktopMost.Size = new System.Drawing.Size(123, 20);
            this.m_extStyle.SetStyleBackColor(this.m_chktopMost, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chktopMost, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chktopMost.TabIndex = 1;
            this.m_chktopMost.Text = "Always visible|20006";
            this.m_chktopMost.UseVisualStyleBackColor = true;
            this.m_chktopMost.CheckedChanged += new System.EventHandler(this.m_chktopMost_CheckedChanged);
            // 
            // m_chkAutoArrange
            // 
            this.m_chkAutoArrange.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkAutoArrange.AutoSize = true;
            this.m_chkAutoArrange.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_chkAutoArrange.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.m_chkAutoArrange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkAutoArrange.Image = global::sc2i.win32.data.navigation.Properties.Resources.Organize_Vertical;
            this.m_chkAutoArrange.Location = new System.Drawing.Point(303, 0);
            this.m_chkAutoArrange.Name = "m_chkAutoArrange";
            this.m_chkAutoArrange.Size = new System.Drawing.Size(22, 20);
            this.m_extStyle.SetStyleBackColor(this.m_chkAutoArrange, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chkAutoArrange, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkAutoArrange.TabIndex = 2;
            this.m_chkAutoArrange.UseVisualStyleBackColor = true;
            this.m_chkAutoArrange.CheckedChanged += new System.EventHandler(this.m_chkAutoArrange_CheckedChanged);
            // 
            // m_panelListe
            // 
            this.m_panelListe.AllowArbre = true;
            this.m_panelListe.AllowCustomisation = true;
            this.m_panelListe.AllowSerializePreferences = true;
            this.m_panelListe.BoutonAjouterVisible = false;
            this.m_panelListe.BoutonListesVisible = false;
            this.m_panelListe.BoutonSupprimerVisible = false;
            this.m_panelListe.ContexteUtilisation = "";
            this.m_panelListe.ControlFiltreStandard = null;
            this.m_panelListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListe.ElementSelectionne = null;
            this.m_panelListe.EnableCustomisation = true;
            this.m_panelListe.FiltreDeBase = null;
            this.m_panelListe.FiltreDeBaseEnAjout = false;
            this.m_panelListe.FiltrePrefere = null;
            this.m_panelListe.FiltreRapide = null;
            this.m_panelListe.HasImages = false;
            this.m_panelListe.ListeObjets = null;
            this.m_panelListe.Location = new System.Drawing.Point(0, 20);
            this.m_panelListe.LockEdition = false;
            this.m_panelListe.ModeQuickSearch = false;
            this.m_panelListe.ModeSelection = false;
            this.m_panelListe.MultiSelect = true;
            this.m_panelListe.Name = "m_panelListe";
            this.m_panelListe.Navigateur = null;
            this.m_panelListe.ProprieteObjetAEditer = null;
            this.m_panelListe.QuickSearchText = "";
            this.m_panelListe.ShortIcons = true;
            this.m_panelListe.Size = new System.Drawing.Size(325, 474);
            this.m_extStyle.SetStyleBackColor(this.m_panelListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelListe.TabIndex = 0;
            this.m_panelListe.TrierAuClicSurEnteteColonne = true;
            this.m_panelListe.UseCheckBoxes = false;
            // 
            // CFormListeExtraite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 494);
            this.Controls.Add(this.m_panelListe);
            this.Controls.Add(this.m_panelTop);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CFormListeExtraite";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Load += new System.EventHandler(this.CFormListeExtraite_Load);
            this.SizeChanged += new System.EventHandler(this.CFormListeExtraite_SizeChanged);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picListe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelListeSpeedStandard m_panelListe;
        private System.Windows.Forms.ContextMenuStrip m_menuForm;
        private System.Windows.Forms.Panel m_panelTop;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.CheckBox m_chktopMost;
        private System.Windows.Forms.CheckBox m_chkAutoArrange;
        private System.Windows.Forms.PictureBox m_picListe;
    }
}