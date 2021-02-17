namespace sc2i.win32.common
{
	partial class CCtrlUpDownBase
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
			this.m_btnBas = new System.Windows.Forms.Button();
			this.m_btnHaut = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_btnBas
			// 
			this.m_btnBas.BackgroundImage = global::sc2i.win32.common.Properties.Resources.down_blue;
			this.m_btnBas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_btnBas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_btnBas.Location = new System.Drawing.Point(-1, -1);
			this.m_btnBas.Name = "m_btnBas";
			this.m_btnBas.Size = new System.Drawing.Size(27, 22);
			this.m_btnBas.TabIndex = 1;
			this.m_btnBas.UseVisualStyleBackColor = true;
			this.m_btnBas.MouseLeave += new System.EventHandler(this.Btns_MouseLeave);
			this.m_btnBas.Click += new System.EventHandler(this.m_btnBas_Click);
			this.m_btnBas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btns_MouseDown);
			this.m_btnBas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btns_MouseMove);
			this.m_btnBas.MouseHover += new System.EventHandler(this.Btns_MouseHover);
			this.m_btnBas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btns_MouseUp);
			// 
			// m_btnHaut
			// 
			this.m_btnHaut.BackgroundImage = global::sc2i.win32.common.Properties.Resources.up_blue;
			this.m_btnHaut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.m_btnHaut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_btnHaut.Location = new System.Drawing.Point(25, -1);
			this.m_btnHaut.Name = "m_btnHaut";
			this.m_btnHaut.Size = new System.Drawing.Size(27, 22);
			this.m_btnHaut.TabIndex = 0;
			this.m_btnHaut.UseVisualStyleBackColor = true;
			this.m_btnHaut.MouseLeave += new System.EventHandler(this.Btns_MouseLeave);
			this.m_btnHaut.Click += new System.EventHandler(this.m_btnHaut_Click);
			this.m_btnHaut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btns_MouseDown);
			this.m_btnHaut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Btns_MouseMove);
			this.m_btnHaut.MouseHover += new System.EventHandler(this.Btns_MouseHover);
			this.m_btnHaut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btns_MouseUp);
			// 
			// CCtrlUpDownBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.m_btnBas);
			this.Controls.Add(this.m_btnHaut);
			this.Name = "CCtrlUpDownBase";
			this.Size = new System.Drawing.Size(51, 20);
			this.MouseLeave += new System.EventHandler(this.Ctrl_MouseLeave);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button m_btnHaut;
		private System.Windows.Forms.Button m_btnBas;
	}
}
