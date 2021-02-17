namespace sc2i.win32.data.Package
{
    partial class CControleOptionsForType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControleOptionsForType));
            this.m_lblTypeName = new System.Windows.Forms.Label();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_chkRecursive = new System.Windows.Forms.CheckBox();
            this.m_chkForcer = new System.Windows.Forms.CheckBox();
            this.m_chkIgnoreType = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_extModeEdition
            // 
            this.m_extModeEdition.ModeEdition = true;
            // 
            // m_lblTypeName
            // 
            this.m_lblTypeName.BackColor = System.Drawing.Color.White;
            this.m_lblTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblTypeName.Location = new System.Drawing.Point(84, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblTypeName, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblTypeName.Name = "m_lblTypeName";
            this.m_lblTypeName.Size = new System.Drawing.Size(451, 24);
            this.m_lblTypeName.TabIndex = 0;
            this.m_lblTypeName.Text = "label1";
            this.m_lblTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_chkRecursive
            // 
            this.m_chkRecursive.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkRecursive.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkRecursive.Image = global::sc2i.win32.data.Properties.Resources.Recursif;
            this.m_chkRecursive.Location = new System.Drawing.Point(56, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkRecursive, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkRecursive.Name = "m_chkRecursive";
            this.m_chkRecursive.Size = new System.Drawing.Size(28, 24);
            this.m_chkRecursive.TabIndex = 3;
            this.m_tooltip.SetToolTip(this.m_chkRecursive, "Automatic recurse search|20009");
            this.m_chkRecursive.UseVisualStyleBackColor = true;
            this.m_chkRecursive.CheckedChanged += new System.EventHandler(this.m_chkRecursive_CheckedChanged);
            // 
            // m_chkForcer
            // 
            this.m_chkForcer.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkForcer.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkForcer.Image = global::sc2i.win32.data.Properties.Resources.Forcer;
            this.m_chkForcer.Location = new System.Drawing.Point(28, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkForcer, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkForcer.Name = "m_chkForcer";
            this.m_chkForcer.Size = new System.Drawing.Size(28, 24);
            this.m_chkForcer.TabIndex = 2;
            this.m_tooltip.SetToolTip(this.m_chkForcer, "Force reference search|20008");
            this.m_chkForcer.UseVisualStyleBackColor = true;
            this.m_chkForcer.CheckedChanged += new System.EventHandler(this.m_chkForcer_CheckedChanged);
            // 
            // m_chkIgnoreType
            // 
            this.m_chkIgnoreType.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkIgnoreType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkIgnoreType.Image = ((System.Drawing.Image)(resources.GetObject("m_chkIgnoreType.Image")));
            this.m_chkIgnoreType.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_chkIgnoreType, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkIgnoreType.Name = "m_chkIgnoreType";
            this.m_chkIgnoreType.Size = new System.Drawing.Size(28, 24);
            this.m_chkIgnoreType.TabIndex = 1;
            this.m_tooltip.SetToolTip(this.m_chkIgnoreType, "Ignore elements of this type|20007");
            this.m_chkIgnoreType.UseVisualStyleBackColor = true;
            this.m_chkIgnoreType.CheckedChanged += new System.EventHandler(this.m_chkIgnoreType_CheckedChanged);
            // 
            // CControleOptionsForType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lblTypeName);
            this.Controls.Add(this.m_chkRecursive);
            this.Controls.Add(this.m_chkForcer);
            this.Controls.Add(this.m_chkIgnoreType);
            this.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleOptionsForType";
            this.Size = new System.Drawing.Size(535, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblTypeName;
        private System.Windows.Forms.CheckBox m_chkIgnoreType;
        private common.CToolTipTraductible m_tooltip;
        private System.Windows.Forms.CheckBox m_chkForcer;
        private System.Windows.Forms.CheckBox m_chkRecursive;
    }
}
