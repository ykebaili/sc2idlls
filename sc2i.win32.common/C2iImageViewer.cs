using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iImageViewer.
	/// </summary>
	public class C2iImageViewer : System.Windows.Forms.UserControl
	{
		private bool m_bIsZoomOptimal = false;
		private double m_fZoom = 1;
		private Point m_ptOrigine;
		private bool m_bIsMoving = false;
		private Image m_image = null;
		private Rectangle m_rectSourceAffiche = new Rectangle(0,0,0,0);
		private bool m_bModeZoom = false;
		private System.Windows.Forms.Panel m_panelMenu;
		private System.Windows.Forms.PictureBox m_btnLoupe;
		private System.Windows.Forms.PictureBox m_btnMain;
		private System.Windows.Forms.Label m_lblZoom;
		private System.Windows.Forms.Timer m_timerZoom;
		private System.Windows.Forms.PictureBox m_wndImage;
		private System.ComponentModel.IContainer components;

		public C2iImageViewer()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(C2iImageViewer));
			this.m_panelMenu = new System.Windows.Forms.Panel();
			this.m_btnMain = new System.Windows.Forms.PictureBox();
			this.m_btnLoupe = new System.Windows.Forms.PictureBox();
			this.m_lblZoom = new System.Windows.Forms.Label();
			this.m_timerZoom = new System.Windows.Forms.Timer(this.components);
			this.m_wndImage = new System.Windows.Forms.PictureBox();
			this.m_panelMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_panelMenu
			// 
			this.m_panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_panelMenu.Controls.Add(this.m_btnMain);
			this.m_panelMenu.Controls.Add(this.m_btnLoupe);
			this.m_panelMenu.Location = new System.Drawing.Point(0, 0);
			this.m_panelMenu.Name = "m_panelMenu";
			this.m_panelMenu.Size = new System.Drawing.Size(64, 32);
			this.m_panelMenu.TabIndex = 0;
			this.m_panelMenu.Visible = false;
			this.m_panelMenu.Leave += new System.EventHandler(this.m_panelMenu_Leave);
			// 
			// m_btnMain
			// 
			this.m_btnMain.Image = ((System.Drawing.Image)(resources.GetObject("m_btnMain.Image")));
			this.m_btnMain.Location = new System.Drawing.Point(32, -1);
			this.m_btnMain.Name = "m_btnMain";
			this.m_btnMain.Size = new System.Drawing.Size(32, 32);
			this.m_btnMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_btnMain.TabIndex = 2;
			this.m_btnMain.TabStop = false;
			this.m_btnMain.Click += new System.EventHandler(this.m_btnMain_Click);
			// 
			// m_btnLoupe
			// 
			this.m_btnLoupe.Image = ((System.Drawing.Image)(resources.GetObject("m_btnLoupe.Image")));
			this.m_btnLoupe.Location = new System.Drawing.Point(0, 0);
			this.m_btnLoupe.Name = "m_btnLoupe";
			this.m_btnLoupe.Size = new System.Drawing.Size(32, 32);
			this.m_btnLoupe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_btnLoupe.TabIndex = 1;
			this.m_btnLoupe.TabStop = false;
			this.m_btnLoupe.Click += new System.EventHandler(this.m_btnLoupe_Click);
			// 
			// m_lblZoom
			// 
			this.m_lblZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_lblZoom.AutoSize = true;
			this.m_lblZoom.Location = new System.Drawing.Point(224, 0);
			this.m_lblZoom.Name = "m_lblZoom";
			this.m_lblZoom.Size = new System.Drawing.Size(21, 16);
			this.m_lblZoom.TabIndex = 1;
			this.m_lblZoom.Text = "X 1";
			this.m_lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.m_lblZoom.Visible = false;
			this.m_lblZoom.Click += new System.EventHandler(this.m_lblZoom_Click);
			// 
			// m_timerZoom
			// 
			this.m_timerZoom.Interval = 1000;
			this.m_timerZoom.Tick += new System.EventHandler(this.m_timerZoom_Tick);
			// 
			// m_wndImage
			// 
			this.m_wndImage.Location = new System.Drawing.Point(32, 48);
			this.m_wndImage.Name = "m_wndImage";
			this.m_wndImage.Size = new System.Drawing.Size(64, 56);
			this.m_wndImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.m_wndImage.TabIndex = 2;
			this.m_wndImage.TabStop = false;
			this.m_wndImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndImage_MouseUp);
			this.m_wndImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C2iImageViewer_MouseMove);
			this.m_wndImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C2iImageViewer_MouseDown);
			// 
			// C2iImageViewer
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.m_wndImage);
			this.Controls.Add(this.m_lblZoom);
			this.Controls.Add(this.m_panelMenu);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Name = "C2iImageViewer";
			this.Size = new System.Drawing.Size(272, 200);
			this.Resize += new System.EventHandler(this.C2iImageViewer_Resize);
			this.Load += new System.EventHandler(this.C2iImageViewer_Load);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C2iImageViewer_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C2iImageViewer_MouseDown);
			this.m_panelMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C2iImageViewer_Load(object sender, System.EventArgs e)
		{
		
		}

		

		private void C2iImageViewer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( !m_bIsMoving )
			{
				Point pt = new Point ( e.X, e.Y );
				if ( sender is Control )
					pt = ((Control)sender).PointToScreen ( pt );
				pt = PointToClient ( pt );
				if ( pt.X < m_panelMenu.Right && pt.Y < m_panelMenu.Height )
				{
                    if (Size.Width > m_panelMenu.Width * 4 && Size.Height > m_panelMenu.Height * 4)
                    {
                        m_panelMenu.Visible = true;
                        m_panelMenu.BringToFront();
                    }
				}
				else
					m_panelMenu.Visible = false;
			}
			else
			{
				Point pt = new Point ( e.X, e.Y );
				pt = ((Control)sender).PointToScreen ( pt );
				Point newPos = new Point ( m_wndImage.Left - m_ptOrigine.X + pt.X, m_wndImage.Top- m_ptOrigine.Y + pt.Y);
				if ( m_wndImage.Width > ClientRectangle.Width )
				{
					if ( newPos.X > 0 )
						newPos.X = 0;
					if ( newPos.X + m_wndImage.Width < ClientRectangle.Width )
						newPos.X = ClientRectangle.Width - m_wndImage.Width;
					pt.X = newPos.X - m_wndImage.Left+m_ptOrigine.X;
				}
				if ( m_wndImage.Height > ClientRectangle.Height)
				{
					if ( newPos.Y > 0 )
						newPos.Y= 0;
					if ( newPos.Y + m_wndImage.Height < ClientRectangle.Height )
						newPos.Y = ClientRectangle.Height - m_wndImage.Height;
					pt.Y = newPos.Y - m_wndImage.Top+m_ptOrigine.Y;
				}
				m_wndImage.Location = newPos;
				m_ptOrigine = pt;
			}
		}

		private void m_btnLoupe_Click(object sender, System.EventArgs e)
		{
			m_btnLoupe.BorderStyle = BorderStyle.Fixed3D;
			m_btnMain.BorderStyle = BorderStyle.None;
			m_bModeZoom = true;
			Cursor = new Cursor( GetType(), "CurseurLoupe.cur" );
		}

		private void m_panelMenu_Leave(object sender, System.EventArgs e)
		{
			m_panelMenu.Visible = false;
		}

		private void m_btnMain_Click(object sender, System.EventArgs e)
		{
			m_btnLoupe.BorderStyle = BorderStyle.None;
			m_btnMain.BorderStyle = BorderStyle.Fixed3D;
			m_bModeZoom = false;
			Cursor = Cursors.Hand;
		}

		public Image Image
		{
			get
			{
				return m_image;
			}
			set
			{
				m_image = value;
				if ( m_image == null )
				{
					m_wndImage.Image = null;
					return;
				}
				CalcZoomOptimal();
				
				m_wndImage.Visible = true;
				m_wndImage.Image = value;
				CenterImage();
				ShowZoom();
			}
		}

		public void CalcZoomOptimal()
		{
			m_fZoom = 1;
			int nX = m_image.Width, nY = m_image.Height;
			double fRatioImage = (double)(nX) / (double)nY;
			double fRatioThis = (double)(ClientRectangle.Width)/(double)ClientRectangle.Height;
			if ( fRatioImage < fRatioThis )
				m_fZoom = (double)(ClientRectangle.Height) / (double)nY;
			else
				m_fZoom = (double)(ClientRectangle.Width)/(double)nX;

			/*while ( nX > ClientRectangle.Width || nY > ClientRectangle.Height && m_fZoom > 1/100)
			{
				m_fZoom /= 2;*/
				nX = (int)(m_image.Width*m_fZoom);
				nY = (int)(m_image.Height*m_fZoom);
			//}
			m_bIsZoomOptimal = true;
			m_wndImage.Width = nX;
			m_wndImage.Height = nY;
		}

		//-----------------------------------------------------
		public double Zoom
		{
			get
			{
				return m_fZoom;
			}
			set
			{
				m_fZoom = value;
				if ( m_image != null )
				{
					int nX = (int)(m_image.Width*m_fZoom);
					int nY = (int)(m_image.Height*m_fZoom);
					m_wndImage.Width = nX;
					m_wndImage.Height = nY;
					CenterImage();
				}
			}
		}


		private void CenterImage()
		{
			m_wndImage.Left = ClientRectangle.Width/2 - m_wndImage.Width/2;
			m_wndImage.Top = ClientRectangle.Height/2-m_wndImage.Height/2;
		}

		private void CaleImage()
		{
			if ( m_image == null )
				return;

			if ( m_wndImage.Right < ClientRectangle.Width )
				m_wndImage.Left = ClientRectangle.Width/2- m_wndImage.Width/2;
			if ( m_wndImage.Bottom < ClientRectangle.Height )
				m_wndImage.Top = ClientRectangle.Height/2-m_wndImage.Height/2;
			if ( m_wndImage.Width > ClientRectangle.Width && m_wndImage.Left > 0)
				m_wndImage.Left = 0;
			if ( m_wndImage.Height > ClientRectangle.Height && m_wndImage.Top > 0 )
				m_wndImage.Top = 0 ;
		}

		private void ShowZoom()
		{
			/*if ( m_image == null )
				return;

			if (m_fZoom>=1)
				m_lblZoom.Text = "x"+m_fZoom.ToString("0");
			else
				m_lblZoom.Text = "x1/"+((1/m_fZoom).ToString("0"));
			m_lblZoom.Left = ClientRectangle.Width-m_lblZoom.Width;
			m_lblZoom.Visible = true;
			m_timerZoom.Enabled = false;
			m_timerZoom.Enabled = true;*/
		}


		private void C2iImageViewer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( m_image == null )
				return;
			if ( m_bModeZoom )
			{
				Point ptOnThis = PointToClient ( ((Control)sender).PointToScreen ( new Point ( e.X, e.Y ) ) );
				double fZoom = m_fZoom;
				double fMult = 1;
				if ( e.Button== MouseButtons.Left )
					fMult = 2;
				else
					fMult = 1.0/2;
				double fPosX = (ptOnThis.X - m_wndImage.Left);
				double fPosY = (ptOnThis.Y - m_wndImage.Top);
				fZoom *= fMult;
				if ( m_image.Width * fZoom > 32000 || m_image.Height*fZoom > 32000 ||
					m_image.Width * fZoom < 4 || m_image.Height * fZoom < 4 )
					return;
				m_wndImage.Width = (int)(m_image.Width*fZoom);
				m_wndImage.Height = (int)(m_image.Height*fZoom);
				m_wndImage.Left = (int)(ptOnThis.X - fPosX*fMult);
				m_wndImage.Top = (int)(ptOnThis.Y - fPosY*fMult);

				m_fZoom = fZoom;
				CaleImage();
				Refresh();
				ShowZoom();
			}
			else
			{
				if ( !m_bIsMoving && sender == m_wndImage )
				{
					m_ptOrigine = ((Control)sender).PointToScreen ( new Point ( e.X, e.Y ) );
					m_bIsMoving = true;
					m_wndImage.Capture = true;
				}
			}
		}

		private void m_lblZoom_Click(object sender, System.EventArgs e)
		{
		
		}

		private void m_timerZoom_Tick(object sender, System.EventArgs e)
		{
			m_timerZoom.Enabled = false;
			m_lblZoom.Visible = false;
		}

		private void C2iImageViewer_Resize(object sender, System.EventArgs e)
		{
			if ( m_bIsZoomOptimal )
				CalcZoomOptimal();
			CenterImage();
		}

		private void m_wndImage_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (m_bIsMoving )
			{
				m_wndImage.Capture = false;
				m_bIsMoving = false;
			}
		}

	}
}
