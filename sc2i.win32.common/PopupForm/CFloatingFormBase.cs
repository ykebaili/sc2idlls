using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace sc2i.win32.common
{
	/// <summary>
	/// Summary description for frmFloatingBase.
	/// </summary>
	public class CFloatingFormBase : System.Windows.Forms.Form,IFloatingPopup
	{
		private System.ComponentModel.IContainer components;
		[DllImport("user32.dll")]
		public extern static int GetWindowRect(IntPtr hWnd,out Rect lpRect);

        private bool m_bAutoPlacement = true;

		public CFloatingFormBase()
		{
			InitializeComponent();
		}

        public bool AutoPlacement
        {
            get
            {
                return m_bAutoPlacement;
            }
            set
            {
                m_bAutoPlacement = value;
            }
        }

		/// <summary>
		/// Clean up any resources being used.
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tmrForceActivate = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// tmrForceActivate
			// 
			this.tmrForceActivate.Tick += new System.EventHandler(this.tmrForceActivate_Tick);
			// 
			// frmFloatingBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(152, 144);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmFloatingBase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "frmFloatingBase";
			this.Deactivate += new System.EventHandler(this.CheckToHide);
			this.Leave += new System.EventHandler(this.CheckToHide);
			this.ResumeLayout(false);

		}
		#endregion

		private void CheckToHide(object sender, EventArgs e)
		{
			if ( !m_bDesactiveHideAuto )
            {
                CancelEventArgs args = new CancelEventArgs();
                args.Cancel = false;
                if (PopupHiding != null)
                    PopupHiding(this, args);
                if ( !args.Cancel  )
				    this.Hide();}
		}

		private void  CFloatingFormBase_Deactivate(object sender, EventArgs e)
		{
			Focus();
		} 

		System.Windows.Forms.Timer tmrForceActivate;
		System.Windows.Forms.UserControl userControl;
		protected virtual void OnPopupHiding(CancelEventArgs e)
		{
			if(PopupHiding!=null)
				PopupHiding(this,e);
			if(e.Cancel==false)
			{
				base.Hide();
				OnPopupHidden(new EventArgs());
				if(this.Owner!=null)
					this.Owner.BringToFront();
			}
			else
			{
				this.tmrForceActivate.Enabled=true;
			}
			
		}
		protected virtual void OnPopupShowing(CancelEventArgs e)
		{
			if(PopupShowing!=null)
			{
				PopupShowing(this,e);
			}
			if(e.Cancel==false)
			{
				SetAutoLocation();
				base.Show();
				OnPopupShown(new EventArgs());
			}
		}
		protected virtual void OnPopupHidden(EventArgs e)
		{
			if(PopupHidden!=null)
				PopupHidden(this,e);
		}
		protected virtual void OnPopupShown(EventArgs e)
		{
			if(PopupShown!=null)
				PopupShown(this,e);
		}


		private void tmrForceActivate_Tick(object sender, System.EventArgs e)
		{
			this.tmrForceActivate.Enabled=false;
			this.Activate();
		}
		

		#region IFloatingPopup Members

		public event System.ComponentModel.CancelEventHandler PopupHiding;

		public event System.ComponentModel.CancelEventHandler PopupShowing;

		public event System.EventHandler PopupHidden;

		public event System.EventHandler PopupShown;

		private Form m_formQuiDemandeAffichage = null;

		private bool m_bDesactiveHideAuto = false;


		public void Show( Form formQuiDemandeAffichage)
		{
			m_formQuiDemandeAffichage = formQuiDemandeAffichage;
			if ( formQuiDemandeAffichage is CFloatingFormBase )
				((CFloatingFormBase)formQuiDemandeAffichage).m_bDesactiveHideAuto = true;
			OnPopupShowing(new CancelEventArgs());
		}

		public new void Show()
		{
			Show(null);
		}

		public new void Hide()
		{
			if (m_formQuiDemandeAffichage is CFloatingFormBase)
			{
				m_formQuiDemandeAffichage.Focus();
				((CFloatingFormBase)m_formQuiDemandeAffichage).m_bDesactiveHideAuto = false;
			}
			// TODO:  Add frmFloatingBase.Hide implementation
			OnPopupHiding(new CancelEventArgs());
		}

		public void ForceShow()
		{
			// TODO:  Add frmFloatingBase.ForceShow implementation
			this.tmrForceActivate.Enabled=true;
		}

		public UserControl UserControl
		{
			get
			{
				// TODO:  Add frmFloatingBase.UserControl getter implementation
				return userControl;
			}
			set
			{
				// TODO:  Add frmFloatingBase.UserControl setter implementation
				userControl=value;
			}
		}

		public void SetAutoLocation()
		{
            if (!AutoPlacement)
                return;
			Rect rect;
			int nWidth, nHeight;
			if (UserControl != null)
			{
				// TODO:  Add frmFloatingBase.SetAutoLocation implementation

				GetWindowRect(UserControl.Handle, out rect);
			}
			else
			{
				rect.left = Cursor.Position.X;
				rect.top = Cursor.Position.Y;
				rect.bottom = rect.top + 1;
				rect.right = rect.left + 1;
			}
			nWidth = rect.right - rect.left;
			nHeight = rect.bottom - rect.top;


			Point tergatePoint;
			tergatePoint = new Point(rect.left, rect.top + nHeight);
			if (rect.left + nWidth - this.Width < 0)
			{
				tergatePoint.X = 0;
			}
			else
			{
				tergatePoint.X = rect.left - this.Width + nWidth;
			}
			if (tergatePoint.X + this.Width > System.Windows.Forms.SystemInformation.WorkingArea.Right)
			{
				tergatePoint.X = System.Windows.Forms.SystemInformation.WorkingArea.Right - this.Width;
			}
			else if (tergatePoint.X < 0)
				tergatePoint.X = 0;
			if (tergatePoint.Y + this.Height > System.Windows.Forms.SystemInformation.WorkingArea.Bottom)
			{
				tergatePoint.Y = rect.top - this.Height;
			}
			if (tergatePoint.Y < 0)
			{
				tergatePoint.Y = 0;
			}
			if (tergatePoint.X < 0)
			{
				tergatePoint.X = 0;
			}
			this.Location = tergatePoint;



		}
		public Form PopupForm
		{
			get
			{
				return this;
			}
		}
		#endregion

	}
	public struct Rect
	{
		internal int left, top, right, bottom;
	}
	
}
