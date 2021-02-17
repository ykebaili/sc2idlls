using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using Crownwood.Magic.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iTabControl.
	/// </summary>
	public class C2iTabControl : Crownwood.Magic.Controls.TabControl
	{
		private bool m_bHasOmbre = true;
		private System.Windows.Forms.PictureBox m_ombreBasDroite;
		private System.Windows.Forms.PictureBox m_ombreDroite;
		private System.Windows.Forms.PictureBox m_ombreBas;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>

		public C2iTabControl(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			container.Add(this);
			InitializeComponent();
            DragFromControl = true;
            TabPages.Inserted += TabPages_Inserted;
			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

        public event EventHandler TabPagesChanged;
        void TabPages_Inserted(int index, object value)
        {
            if (TabPagesChanged != null)
                TabPagesChanged(this, new EventArgs());
        }

		public C2iTabControl()
		{
			/// <summary>
			/// Requis pour la prise en charge du Concepteur de composition de classes Windows.Forms
			/// </summary>
			InitializeComponent();
            DragFromControl = true;
			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(C2iTabControl));
			this.m_ombreBasDroite = new System.Windows.Forms.PictureBox();
			this.m_ombreDroite = new System.Windows.Forms.PictureBox();
			this.m_ombreBas = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// _hostPanel
			// 
//			this._hostPanel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(247)), ((System.Byte)(184)));
//			this._hostPanel.Location = new System.Drawing.Point(0, 25);
//			this._hostPanel.Size = new System.Drawing.Size(480, 231);
			// 
			// _closeButton
			// 
			this._closeButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(220)));
			// 
			// _leftArrow
			// 
			this._leftArrow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(220)));
			// 
			// _rightArrow
			// 
			this._rightArrow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(220)));
			// 
			// m_ombreBasDroite
			// 
			this.m_ombreBasDroite.Image = ((System.Drawing.Bitmap)(resources.GetObject("m_ombreBasDroite.Image")));
			this.m_ombreBasDroite.Location = new System.Drawing.Point(416, 216);
			this.m_ombreBasDroite.Name = "m_ombreBasDroite";
			this.m_ombreBasDroite.Size = new System.Drawing.Size(16, 16);
			this.m_ombreBasDroite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_ombreBasDroite.TabIndex = 9;
			this.m_ombreBasDroite.TabStop = false;
			// 
			// m_ombreDroite
			// 
			this.m_ombreDroite.Image = ((System.Drawing.Bitmap)(resources.GetObject("m_ombreDroite.Image")));
			this.m_ombreDroite.Location = new System.Drawing.Point(416, 0);
			this.m_ombreDroite.Name = "m_ombreDroite";
			this.m_ombreDroite.Size = new System.Drawing.Size(16, 216);
			this.m_ombreDroite.TabIndex = 8;
			this.m_ombreDroite.TabStop = false;
			// 
			// m_ombreBas
			// 
			this.m_ombreBas.Image = ((System.Drawing.Bitmap)(resources.GetObject("m_ombreBas.Image")));
			this.m_ombreBas.Location = new System.Drawing.Point(0, 216);
			this.m_ombreBas.Name = "m_ombreBas";
			this.m_ombreBas.Size = new System.Drawing.Size(432, 16);
			this.m_ombreBas.TabIndex = 7;
			this.m_ombreBas.TabStop = false;
			// 
			// C2iTabControl
			// 
            this.BackColor = System.Drawing.SystemColors.Control;
			this.BoldSelectedPage = true;
			this.ControlBottomOffset = 16;
			this.ControlRightOffset = 16;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_ombreDroite,
																		  this.m_ombreBasDroite,
																		  this.m_ombreBas,
																		  this._closeButton,
																		  this._leftArrow,
																		  this._rightArrow});
			this.IDEPixelArea = false;
			this.Name = "C2iTabControl";
			this.PositionTop = true;
			this.Size = new System.Drawing.Size(496, 272);
			this.SizeChanged += new System.EventHandler(this.C2iTabControl_SizeChanged);
			this.ResumeLayout(false);

		}
		#endregion

        

		private void C2iTabControl_SizeChanged(object sender, System.EventArgs e)
		{
			m_ombreDroite.Left = ClientSize.Width-m_ombreDroite.Width;
			m_ombreDroite.Top = 0;
			m_ombreDroite.Height = ClientSize.Height - m_ombreBasDroite.Height;
			m_ombreBas.Top = ClientSize.Height-m_ombreBas.Height;
			m_ombreBas.Left = 0;
			m_ombreBas.Width = ClientSize.Width - m_ombreBasDroite.Width;
			m_ombreBasDroite.Left = ClientSize.Width-m_ombreDroite.Width;
			m_ombreBasDroite.Top = ClientSize.Height-m_ombreBas.Height;
		}


		public bool Ombre
		{
			get
			{
				return m_bHasOmbre;
			}
			set
			{
				m_bHasOmbre = value;
				if ( m_bHasOmbre )
				{
					ControlBottomOffset = m_ombreBas.Height;
					ControlRightOffset = m_ombreDroite.Width;
					m_ombreBas.Visible = true;
					m_ombreDroite.Visible = true;
					m_ombreBasDroite.Visible = true;
				}
				else
				{
					ControlBottomOffset = 0;
					ControlRightOffset = 0;
					m_ombreBas.Visible = false;
					m_ombreDroite.Visible = false;
					m_ombreBasDroite.Visible = false;
				}
				Refresh();
			}
		}
	}
}
