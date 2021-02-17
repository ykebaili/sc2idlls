namespace sc2i.win32.common.MemoryDb
{
    partial class C2iTextBoxSelectionEntiteMemoryDb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C2iTextBoxSelectionEntiteMemoryDb));
            this.m_btn = new System.Windows.Forms.Button();
            this.m_picType = new System.Windows.Forms.PictureBox();
            this.m_btnFlush = new System.Windows.Forms.Button();
            this.m_menuChooseType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_textBox = new sc2i.win32.common.C2iTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_picType)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btn
            // 
            this.m_btn.BackColor = System.Drawing.Color.White;
            this.m_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.m_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btn.Image = global::sc2i.win32.common.Properties.Resources.search;
            this.m_btn.Location = new System.Drawing.Point(206, 0);
            this.m_btn.Name = "m_btn";
            this.m_btn.Size = new System.Drawing.Size(24, 21);
            this.m_btn.TabIndex = 5;
            this.m_btn.TabStop = false;
            this.m_btn.UseVisualStyleBackColor = false;
            this.m_btn.Click += new System.EventHandler(this.m_btn_Click);
            // 
            // m_picType
            // 
            this.m_picType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_picType.Location = new System.Drawing.Point(0, 0);
            this.m_picType.Name = "m_picType";
            this.m_picType.Size = new System.Drawing.Size(20, 21);
            this.m_picType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_picType.TabIndex = 7;
            this.m_picType.TabStop = false;
            this.m_picType.Visible = false;
            // 
            // m_btnFlush
            // 
            this.m_btnFlush.BackColor = System.Drawing.Color.White;
            this.m_btnFlush.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnFlush.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnFlush.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.m_btnFlush.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.m_btnFlush.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btnFlush.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnFlush.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFlush.Image")));
            this.m_btnFlush.Location = new System.Drawing.Point(230, 0);
            this.m_btnFlush.Name = "m_btnFlush";
            this.m_btnFlush.Size = new System.Drawing.Size(24, 21);
            this.m_btnFlush.TabIndex = 6;
            this.m_btnFlush.TabStop = false;
            this.m_btnFlush.UseVisualStyleBackColor = false;
            this.m_btnFlush.Click += new System.EventHandler(this.m_btnFlush_Click);
            // 
            // m_menuChooseType
            // 
            this.m_menuChooseType.Name = "m_menuChooseType";
            this.m_menuChooseType.Size = new System.Drawing.Size(61, 4);
            // 
            // m_textBox
            // 
            this.m_textBox.AllowDrop = true;
            this.m_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_textBox.EmptyText = "";
            this.m_textBox.Location = new System.Drawing.Point(20, 0);
            this.m_textBox.LockEdition = false;
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(186, 20);
            this.m_textBox.TabIndex = 4;
            this.m_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_textBox_KeyDown);
            this.m_textBox.Leave += new System.EventHandler(this.m_textBox_Leave);
            this.m_textBox.Enter += new System.EventHandler(this.m_textBox_Enter);
            // 
            // C2iTextBoxSelectionEntiteMemoryDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_textBox);
            this.Controls.Add(this.m_btn);
            this.Controls.Add(this.m_picType);
            this.Controls.Add(this.m_btnFlush);
            this.Name = "C2iTextBoxSelectionEntiteMemoryDb";
            this.Size = new System.Drawing.Size(254, 21);
            ((System.ComponentModel.ISupportInitialize)(this.m_picType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C2iTextBox m_textBox;
        private System.Windows.Forms.Button m_btn;
        private System.Windows.Forms.PictureBox m_picType;
        private System.Windows.Forms.Button m_btnFlush;
        private System.Windows.Forms.ContextMenuStrip m_menuChooseType;
    }
}
