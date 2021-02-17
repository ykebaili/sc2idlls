namespace sc2i.win32.common
{
    partial class CCtrlShowErreur
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
			this.m_panIco = new System.Windows.Forms.Panel();
			this.m_txtBoxErr = new System.Windows.Forms.TextBox();
			this.m_btnElargir = new System.Windows.Forms.Button();
			this.m_txtBoxErrSingle = new System.Windows.Forms.TextBox();
			this.m_panLeft = new System.Windows.Forms.Panel();
			this.m_panRight = new System.Windows.Forms.Panel();
			this.m_panCenter = new System.Windows.Forms.Panel();
			this.m_timer = new System.Windows.Forms.Timer(this.components);
			this.m_panLeft.SuspendLayout();
			this.m_panRight.SuspendLayout();
			this.m_panCenter.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_panIco
			// 
			this.m_panIco.BackgroundImage = Properties.Resources.ImgErreur_old;
			this.m_panIco.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_panIco.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_panIco.Location = new System.Drawing.Point(0, 0);
			this.m_panIco.Name = "m_panIco";
			this.m_panIco.Size = new System.Drawing.Size(22, 20);
			this.m_panIco.TabIndex = 0;
			// 
			// m_txtBoxErr
			// 
			this.m_txtBoxErr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtBoxErr.BackColor = System.Drawing.Color.White;
			this.m_txtBoxErr.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_txtBoxErr.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_txtBoxErr.Location = new System.Drawing.Point(0, 0);
			this.m_txtBoxErr.Multiline = true;
			this.m_txtBoxErr.Name = "m_txtBoxErr";
			this.m_txtBoxErr.ReadOnly = true;
			this.m_txtBoxErr.Size = new System.Drawing.Size(195, 250);
			this.m_txtBoxErr.TabIndex = 1;
			this.m_txtBoxErr.MouseLeave += new System.EventHandler(this.m_txtBoxErr_MouseLeave);
			this.m_txtBoxErr.MouseEnter += new System.EventHandler(this.m_txtBoxErr_MouseEnter);
			// 
			// m_btnElargir
			// 
			this.m_btnElargir.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_btnElargir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_btnElargir.Location = new System.Drawing.Point(0, 0);
			this.m_btnElargir.Name = "m_btnElargir";
			this.m_btnElargir.Size = new System.Drawing.Size(28, 20);
			this.m_btnElargir.TabIndex = 2;
			this.m_btnElargir.Text = "\\/";
			this.m_btnElargir.UseVisualStyleBackColor = true;
			this.m_btnElargir.MouseLeave += new System.EventHandler(this.m_btnElargir_MouseLeave);
			this.m_btnElargir.Click += new System.EventHandler(this.m_btnElargir_Click);
			this.m_btnElargir.MouseEnter += new System.EventHandler(this.m_btnElargir_MouseEnter);
			// 
			// m_txtBoxErrSingle
			// 
			this.m_txtBoxErrSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtBoxErrSingle.BackColor = System.Drawing.Color.White;
			this.m_txtBoxErrSingle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_txtBoxErrSingle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_txtBoxErrSingle.Location = new System.Drawing.Point(0, 0);
			this.m_txtBoxErrSingle.Name = "m_txtBoxErrSingle";
			this.m_txtBoxErrSingle.ReadOnly = true;
			this.m_txtBoxErrSingle.Size = new System.Drawing.Size(195, 16);
			this.m_txtBoxErrSingle.TabIndex = 1;
			this.m_txtBoxErrSingle.MouseLeave += new System.EventHandler(this.m_txtBoxErr_MouseLeave);
			this.m_txtBoxErrSingle.MouseEnter += new System.EventHandler(this.m_txtBoxErr_MouseEnter);
			// 
			// m_panLeft
			// 
			this.m_panLeft.Controls.Add(this.m_panIco);
			this.m_panLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_panLeft.Location = new System.Drawing.Point(0, 0);
			this.m_panLeft.Name = "m_panLeft";
			this.m_panLeft.Size = new System.Drawing.Size(22, 250);
			this.m_panLeft.TabIndex = 3;
			// 
			// m_panRight
			// 
			this.m_panRight.Controls.Add(this.m_btnElargir);
			this.m_panRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.m_panRight.Location = new System.Drawing.Point(215, 0);
			this.m_panRight.Name = "m_panRight";
			this.m_panRight.Size = new System.Drawing.Size(28, 250);
			this.m_panRight.TabIndex = 4;
			// 
			// m_panCenter
			// 
			this.m_panCenter.Controls.Add(this.m_txtBoxErrSingle);
			this.m_panCenter.Controls.Add(this.m_txtBoxErr);
			this.m_panCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_panCenter.Location = new System.Drawing.Point(22, 0);
			this.m_panCenter.Name = "m_panCenter";
			this.m_panCenter.Size = new System.Drawing.Size(193, 250);
			this.m_panCenter.TabIndex = 5;
			// 
			// m_timer
			// 
			this.m_timer.Interval = 1000;
			this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
			// 
			// CCtrlShowErreur
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.m_panCenter);
			this.Controls.Add(this.m_panRight);
			this.Controls.Add(this.m_panLeft);
			this.Name = "CCtrlShowErreur";
			this.Size = new System.Drawing.Size(243, 250);
			this.m_panLeft.ResumeLayout(false);
			this.m_panRight.ResumeLayout(false);
			this.m_panCenter.ResumeLayout(false);
			this.m_panCenter.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		internal System.Windows.Forms.Panel m_panIco;
		private System.Windows.Forms.TextBox m_txtBoxErr;
		private System.Windows.Forms.Button m_btnElargir;
		private System.Windows.Forms.TextBox m_txtBoxErrSingle;
		private System.Windows.Forms.Panel m_panLeft;
		private System.Windows.Forms.Panel m_panRight;
		private System.Windows.Forms.Panel m_panCenter;
		private System.Windows.Forms.Timer m_timer;
    }
}
