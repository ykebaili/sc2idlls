namespace sc2i.win32.common.recherche
{
    partial class CFormResultatRechercheObjet
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
            this.components = new System.ComponentModel.Container();
            this.m_lblElementRecherché = new System.Windows.Forms.Label();
            this.m_arbreResultat = new sc2i.win32.common.recherche.CWndArbreResultatRequete();
            this.SuspendLayout();
            // 
            // m_lblElementRecherché
            // 
            this.m_lblElementRecherché.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_lblElementRecherché.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblElementRecherché.Location = new System.Drawing.Point(0, 0);
            this.m_lblElementRecherché.Name = "m_lblElementRecherché";
            this.m_lblElementRecherché.Size = new System.Drawing.Size(264, 23);
            this.m_lblElementRecherché.TabIndex = 0;
            this.m_lblElementRecherché.Text = "label1";
            // 
            // m_arbreResultat
            // 
            this.m_arbreResultat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbreResultat.ImageIndex = 0;
            this.m_arbreResultat.Location = new System.Drawing.Point(0, 23);
            this.m_arbreResultat.Name = "m_arbreResultat";
            this.m_arbreResultat.SelectedImageIndex = 0;
            this.m_arbreResultat.Size = new System.Drawing.Size(264, 343);
            this.m_arbreResultat.TabIndex = 1;
            this.m_arbreResultat.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_arbreResultat_NodeMouseDoubleClick);
            // 
            // CFormResultatRechercheObjet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(264, 366);
            this.Controls.Add(this.m_arbreResultat);
            this.Controls.Add(this.m_lblElementRecherché);
            this.Name = "CFormResultatRechercheObjet";
            this.Text = "Search result|20001";
            this.Load += new System.EventHandler(this.CFormResultatRechercheObjet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblElementRecherché;
        private CWndArbreResultatRequete m_arbreResultat;
    }
}