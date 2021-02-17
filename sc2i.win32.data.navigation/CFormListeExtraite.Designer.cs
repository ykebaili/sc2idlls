namespace sc2i.win32.data.navigation
{
    partial class CFormListeExtraite
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
            this.m_panelListe = new sc2i.win32.data.navigation.CPanelListeSpeedStandard();
            this.SuspendLayout();
            // 
            // m_zonePourControlesFils
            // 
            this.m_zonePourControlesFils.Location = new System.Drawing.Point(0, 0);
            this.m_zonePourControlesFils.Size = new System.Drawing.Size(325, 494);
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
            this.m_panelListe.TabIndex = 3;
            this.m_panelListe.TrierAuClicSurEnteteColonne = true;
            this.m_panelListe.UseCheckBoxes = false;
            // 
            // CFormListeExtraite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 494);
            this.Controls.Add(this.m_panelListe);
            this.Name = "CFormListeExtraite";
            this.Text = "CFormListeExtraite";
            this.Load += new System.EventHandler(this.CFormListeExtraite_Load);
            this.Controls.SetChildIndex(this.m_zonePourControlesFils, 0);
            this.Controls.SetChildIndex(this.m_panelTop, 0);
            this.Controls.SetChildIndex(this.m_panelListe, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelListeSpeedStandard m_panelListe;
    }
}