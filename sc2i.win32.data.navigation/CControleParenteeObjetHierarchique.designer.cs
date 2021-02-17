namespace sc2i.win32.data.navigation
{
    partial class CControleParenteeObjetHierarchique
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
            this.m_iconUp = new System.Windows.Forms.PictureBox();
            this.m_iconModifier = new System.Windows.Forms.PictureBox();
            this.m_panelHierarchie = new System.Windows.Forms.Panel();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_iconZoom = new System.Windows.Forms.PictureBox();
            this.m_toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_iconUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_iconModifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_iconZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // m_iconUp
            // 
            this.m_iconUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_iconUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_iconUp.Image = global::sc2i.win32.data.navigation.Properties.Resources._1346547808_arrow_up;
            this.m_iconUp.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_iconUp, sc2i.win32.common.TypeModeEdition.DisableSurEdition);
            this.m_iconUp.Name = "m_iconUp";
            this.m_iconUp.Size = new System.Drawing.Size(21, 24);
            this.m_iconUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_iconUp.TabIndex = 0;
            this.m_iconUp.TabStop = false;
            this.m_iconUp.Click += new System.EventHandler(this.m_iconUp_Click);
            // 
            // m_iconModifier
            // 
            this.m_iconModifier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_iconModifier.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_iconModifier.Image = global::sc2i.win32.data.navigation.Properties.Resources.edit;
            this.m_iconModifier.Location = new System.Drawing.Point(21, 0);
            this.m_extModeEdition.SetModeEdition(this.m_iconModifier, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_iconModifier.Name = "m_iconModifier";
            this.m_iconModifier.Size = new System.Drawing.Size(21, 24);
            this.m_iconModifier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_iconModifier.TabIndex = 1;
            this.m_iconModifier.TabStop = false;
            this.m_iconModifier.Click += new System.EventHandler(this.m_iconModifier_Click);
            // 
            // m_panelHierarchie
            // 
            this.m_panelHierarchie.BackColor = System.Drawing.Color.White;
            this.m_panelHierarchie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelHierarchie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelHierarchie.Location = new System.Drawing.Point(42, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelHierarchie, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelHierarchie.Name = "m_panelHierarchie";
            this.m_panelHierarchie.Size = new System.Drawing.Size(405, 24);
            this.m_panelHierarchie.TabIndex = 2;
            this.m_panelHierarchie.SizeChanged += new System.EventHandler(this.m_panelHierarchie_SizeChanged);
            // 
            // m_iconZoom
            // 
            this.m_iconZoom.BackColor = System.Drawing.Color.White;
            this.m_iconZoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_iconZoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_iconZoom.Image = global::sc2i.win32.data.navigation.Properties.Resources.search;
            this.m_iconZoom.Location = new System.Drawing.Point(447, 0);
            this.m_extModeEdition.SetModeEdition(this.m_iconZoom, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_iconZoom.Name = "m_iconZoom";
            this.m_iconZoom.Size = new System.Drawing.Size(21, 24);
            this.m_iconZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_iconZoom.TabIndex = 3;
            this.m_iconZoom.TabStop = false;
            this.m_iconZoom.Click += new System.EventHandler(this.m_iconZoom_Click);
            // 
            // CControleParenteeObjetHierarchique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelHierarchie);
            this.Controls.Add(this.m_iconModifier);
            this.Controls.Add(this.m_iconUp);
            this.Controls.Add(this.m_iconZoom);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleParenteeObjetHierarchique";
            this.Size = new System.Drawing.Size(468, 24);
            ((System.ComponentModel.ISupportInitialize)(this.m_iconUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_iconModifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_iconZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_iconUp;
        private System.Windows.Forms.PictureBox m_iconModifier;
        private System.Windows.Forms.Panel m_panelHierarchie;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.ToolTip m_toolTip;
        private System.Windows.Forms.PictureBox m_iconZoom;
    }
}
