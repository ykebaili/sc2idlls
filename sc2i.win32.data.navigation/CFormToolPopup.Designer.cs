namespace sc2i.win32.data.navigation
{
    partial class CFormToolPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormToolPopup));
            this.m_menuForm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_chktopMost = new System.Windows.Forms.CheckBox();
            this.m_chkAutoArrange = new System.Windows.Forms.CheckBox();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_zonePourControlesFils = new System.Windows.Forms.Panel();
            this.m_panelTop.SuspendLayout();
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
            // m_chktopMost
            // 
            this.m_chktopMost.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chktopMost.AutoSize = true;
            this.m_chktopMost.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_chktopMost.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chktopMost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chktopMost.Image = ((System.Drawing.Image)(resources.GetObject("m_chktopMost.Image")));
            this.m_chktopMost.Location = new System.Drawing.Point(281, 0);
            this.m_chktopMost.Name = "m_chktopMost";
            this.m_chktopMost.Size = new System.Drawing.Size(22, 20);
            this.m_extStyle.SetStyleBackColor(this.m_chktopMost, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_chktopMost, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chktopMost.TabIndex = 1;
            this.m_chktopMost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // m_zonePourControlesFils
            // 
            this.m_zonePourControlesFils.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_zonePourControlesFils.Location = new System.Drawing.Point(0, 20);
            this.m_zonePourControlesFils.Name = "m_zonePourControlesFils";
            this.m_zonePourControlesFils.Size = new System.Drawing.Size(325, 474);
            this.m_extStyle.SetStyleBackColor(this.m_zonePourControlesFils, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_zonePourControlesFils, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_zonePourControlesFils.TabIndex = 3;
            // 
            // CFormToolPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 494);
            this.Controls.Add(this.m_zonePourControlesFils);
            this.Controls.Add(this.m_panelTop);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CFormToolPopup";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.SizeChanged += new System.EventHandler(this.CFormToolPopup_SizeChanged);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip m_menuForm;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.CheckBox m_chktopMost;
        private System.Windows.Forms.CheckBox m_chkAutoArrange;
        protected System.Windows.Forms.Panel m_panelTop;
        public System.Windows.Forms.Panel m_zonePourControlesFils;
    }
}